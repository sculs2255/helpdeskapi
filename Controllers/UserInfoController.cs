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
    public class UserInfoController : ControllerBase
    {
        private readonly DataContext _context;
        public  UserInfoController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult> List([FromQuery]  UserInfoFilter filter)
        {
            try
            {

                var query = (from s in _context.UserInfo
                             select new
                             {
                                  UserInfoID = s.UserInfoID,
                                  UserID  = s.UserID ,
                                  Firstname = s.Firstname,
                                  Lastname = s.Lastname,
                                  Gender = s.Gender,
                                  UserPicture = s.UserPicture

                             });

                var DbF = Microsoft.EntityFrameworkCore.EF.Functions;

                if (filter.UserInfoID> 0)
                {
                    query = query.Where(q => q.UserInfoID == filter.UserInfoID);
                }

                /*
               if (!String.IsNullOrEmpty(filter.textSearch))
               {
                   query = query.Where(q => DbF.Like(q.Name, "%" + filter.textSearch + "%"));
               }
               */

               
                switch (filter.sortOrder)
                {
                    case " topicName":
                        query = query.OrderBy(q => q.Firstname);
                        break;
                    case " topicName_desc":
                        query = query.OrderByDescending(q => q.Firstname);
                        break;
                    case " topicID":
                        query = query.OrderBy(q => q.Firstname);
                        break;
                    case " topicID_desc":
                        query = query.OrderByDescending(q => q.Firstname);
                        break;
                    default:
                        query = query = query.OrderBy(q => q.Firstname);
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
                var existingData = await _context.UserInfo.FindAsync(id);
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
        public async Task<IActionResult> Update(int id, [FromBody]  UserInfoRequest request)
        {
            try
            {
                var existingData = await _context.UserInfo.FindAsync(id);
                if (existingData == null)
                {
                    return BadRequest(new
                    {

                        message = "Data NotFound",
                        isSuccess = false
                    });
                }
                existingData.UserInfoID= request.UserInfoID ;
                existingData.UserID  = request.UserID ;
                existingData.Firstname = request.Firstname ;
                existingData.Lastname = request.Lastname ;
                existingData.Gender  = request.Gender ;
                existingData.UserPicture  = request.UserPicture ;


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
        public async Task<IActionResult> Create([FromBody]  UserInfoRequest request)
        {
            try
            {
                var temp = new UserInfo
                {
                    
                UserInfoID= request.UserInfoID ,
                UserID  = request.UserID ,
                Firstname = request.Firstname ,
                Lastname = request.Lastname ,
                Gender  = request.Gender ,
                UserPicture  = request.UserPicture ,
                };

                _context.UserInfo.Add(temp);
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

                var existingData = await _context.UserInfo.FindAsync(id);

                if (existingData == null)
                {
                    return BadRequest(new
                    {
                        message = "Data NotFound",
                        isSuccess = false
                    });
                }

                _context.UserInfo.Remove(existingData);
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
