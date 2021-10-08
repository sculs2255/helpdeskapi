using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HelpDeskApi.Data;
using HelpDeskApi.Models;
using HelpDeskApi.Models.DTOs.Requests;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System.Text;

namespace HelpDeskApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CommentController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly DataContext _context;

        public CommentController(
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            DataContext context)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }


        [HttpGet]
        [Route("List")]
        public async Task<ActionResult> List()
        {

            try
            {

                var query = await (from cm in _context.Comment
                                   join u1 in _context.Users on cm.UserID equals u1.Id into cmr
                                   from u in cmr.DefaultIfEmpty()

                                   join c in _context.HD_Case on cm.CaseID equals c.CaseID into cr
                                   from cResult in cr.DefaultIfEmpty()
                                   select new
                                   {
                                       cm.CommentID,
                                       UserID = u.Id,
                                       CaseID = cm.CaseID,
                                       cm.Title,
                                       cm.Detail,
                                       cm.File,
                                       cm.CmDate
                                   }
                            ).ToListAsync();

                return Ok(new
                {
                    data = query,
                    isSuccess = true
                });
            }
            catch (Exception)
            {
                return Ok(new
                {
                    isSuccess = false
                });

            }

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var existingData = await _context.HD_Case.FindAsync(id);
                var query = (from cm in _context.Comment
                             join u1 in _context.Users on cm.UserID equals u1.Id into u2
                             from u in u2.DefaultIfEmpty()
                             join c1 in _context.HD_Case on cm.CaseID equals c1.CaseID into c2
                             from c in c2.DefaultIfEmpty()
                             where cm.UserID == u.Id && cm.CaseID == c.CaseID && existingData.CaseID == cm.CaseID

                             select new
                             {
                                 
                                 cm.CaseID,
                                 firstName = u.FirstName,
                                 lastName = u.LastName,
                                 cm.Title,
                                 cm.Detail,
                                 cm.CmDate

                             }
                            );
                var comment = await query.ToListAsync();
                return Ok(new
                {
                    data = comment,
                    isSuccess = true
                });
            }
            catch (Exception)
            {
                return Ok(new
                {
                    isSuccess = false
                });

            }

        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CommentRequest request)
        {
            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                IList<Claim> claim = identity.Claims.ToList();
                var userInfo = await _userManager.FindByEmailAsync(claim[1].Value);

                var newComment = new Comment
                {
                    CaseID = request.CaseID,
                    UserID = userInfo.Id,
                    Title = request.Title,
                    Detail = request.Detail,
                    File = request.File,
                    CmDate = DateTime.Now
                };

                _context.Comment.Add(newComment);
                await _context.SaveChangesAsync();

                return BadRequest(new
                {
                    isSuccess = true
                });

            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    message = ex,
                    isSuccess = false
                });
            }
        }

    }
}