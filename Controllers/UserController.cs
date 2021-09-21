using HelpDeskApi.Data;
using HelpDeskApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using HelpDeskApi.Models.DTOs.Requests;

namespace HelpDeskApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class UserController : ControllerBase
    {
        private readonly DataContext _context;
        public UserController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult> List([FromQuery] UserFilter  filter)
        {
            try
            {
                                 
                var query =  (from c in _context.User
                            select new
                            {
                                UserID = c.UserID,
                                Username = c.Username,
                                Password = c.Password,
                                UserTypeID = c.UserTypeID,
                                Active = c.Active
                               
                            });

                 var DbF = Microsoft.EntityFrameworkCore.EF.Functions;
               
                if (filter.UserID>0)
                {
                   query = query.Where(q => q.UserID == filter.UserID);
                }
                
                /*
                if (!String.IsNullOrEmpty(filter.textSearch))
                {
                    query = query.Where(q => DbF.Like(q.Name, "%" + filter.textSearch + "%"));
                }
                */

               

                 switch (filter.sortOrder)
                {
                    case "userID":
                        query = query.OrderBy(q => q.UserID);
                        break;
                    case "userID_desc":
                        query = query.OrderByDescending(q => q.UserID);
                        break;
                      case "username":
                        query = query.OrderBy(q => q.Username);
                        break;
                    case "username_desc":
                        query = query.OrderByDescending(q => q.Username);
                        break;
                      case "password":
                        query = query.OrderBy(q => q.Password);
                        break;
                    case "password_desc":
                        query = query.OrderByDescending(q => q.Password);
                        break;
                      case "active":
                        query = query.OrderBy(q => q.Active);
                        break;
                    case "active_desc":
                        query = query.OrderByDescending(q => q.Active);
                        break;

                    default:
                        query = query = query.OrderBy(q => q.Active);
                        break;
                }

                int totalItems = query.Count();
                var data = await query.Skip((filter.pageNumber - 1) * filter.pageSize).Take(filter.pageSize).ToListAsync();

                return Ok(new
                {
                    totalItems = totalItems,
                    data = data,
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

        [HttpGet("{id}")]
        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var existingData = await _context.User.FindAsync(id);
                if (existingData == null)
                {
                    return BadRequest(new
                    {
                        message = "Data NotFound",
                        isSuccess = false
                    });
                }
                
                return Ok(new
                {
                    data = existingData,
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
      

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UserRequest request)
        {
            try
            { 
                var existingData = await _context.User.FindAsync(id);
                if (existingData == null)
                {
                      return BadRequest(new
                    {
                        message = "Data NotFound",
                        isSuccess = false
                    });
                }

                existingData.UserID = request.UserID;
                existingData.Username = request.Username;
                existingData.Password = request.Password;
                existingData.UserTypeID = request.UserTypeID;
                existingData.Active = request.Active;
                
                await _context.SaveChangesAsync();
                
                return Ok(new
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

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] UserRequest request)
        {
             try
            { 
                var temp = new User
                {
                    UserID = request.UserID ,
                    Username = request.Username,
                    Password = request.Password,
                    UserTypeID = request.UserTypeID,
                    Active =request.Active
                };

                 _context.User.Add(temp);
                await _context.SaveChangesAsync();
                
                
                return Ok(new
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


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                
                var existingData = await _context.User.FindAsync(id);

                if (existingData == null)
                {
                      return BadRequest(new
                    {
                        message = "Data NotFound",
                        isSuccess = false
                    });
                }

                _context.User.Remove(existingData);
                await _context.SaveChangesAsync();

                return Ok(new
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
