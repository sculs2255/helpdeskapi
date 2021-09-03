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
    public class IsmDbController : ControllerBase
    {
        private readonly DataContext _context;
        public IsmDbController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult> List([FromQuery] IsmDbFilter filter)
        {
            try
            {
                

                var query = (from s in _context.IsmDb
                             select new
                             {
                                 IsmID = s.IsmID,
                                 IsmName = s.IsmName

                             });

                var DbF = Microsoft.EntityFrameworkCore.EF.Functions;

                if (filter.IsmID > 0)
                {
                    query = query.Where(q => q.IsmID == filter.IsmID);
                }

                /*
               if (!String.IsNullOrEmpty(filter.textSearch))
               {
                   query = query.Where(q => DbF.Like(q.Name, "%" + filter.textSearch + "%"));
               }
               */

             
                switch (filter.sortOrder)
                {
                    case "IsmName":
                        query = query.OrderBy(q => q.IsmName);
                        Console.WriteLine("1");
                        break;
                    case "IsmName_desc":
                        query = query.OrderByDescending(q => q.IsmName);
                        Console.WriteLine("2");
                        break;
                    case "IsmID":
                        query = query.OrderBy(q => q.IsmID);
                        Console.WriteLine("3");
                        break;
                    case "IsmID_desc":
                        query = query.OrderByDescending(q => q.IsmID);
                        Console.WriteLine("4");
                        break;
                    default:
                        query = query.OrderBy(q => q.IsmID);
                        Console.WriteLine("0");
                        break;
                }

                int totalItems = query.Count();
                var data = await query.Skip((filter.pageNumber - 1) * filter.pageSize).Take(filter.pageSize).ToListAsync();

                return Ok(new
                {
                    totalItems = totalItems,
                    data = data,
                    filter = filter,
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
                var existingData = await _context.IsmDb.FindAsync(id);
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
        public async Task<IActionResult> Update(int id, [FromBody] IsmDbRequest request)
        {
            try
            {
                var existingData = await _context.IsmDb.FindAsync(id);
                if (existingData == null)
                {
                    return BadRequest(new
                    {

                        message = "Data NotFound",
                        isSuccess = false
                    });
                }
                existingData.IsmID = request.IsmID;
                existingData.IsmName = request.IsmName;

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
        public async Task<IActionResult> Create([FromBody] IsmDbRequest request)
        {
            try
            {
                var temp = new IsmDb
                {
                    IsmID = request.IsmID,
                    IsmName = request.IsmName
                };

                _context.IsmDb.Add(temp);
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

                var existingData = await _context.IsmDb.FindAsync(id);

                if (existingData == null)
                {
                    return BadRequest(new
                    {
                        message = "Data NotFound",
                        isSuccess = false
                    });
                }

                _context.IsmDb.Remove(existingData);
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
