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
    public class BranchController : ControllerBase
    {
        private readonly DataContext _context;
        public BranchController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult> List([FromQuery] BranchFilter  filter)
        {
            try
            {
                                 
                var query =  (from c in _context.Branch
                            select new
                            {
                                BranchID = c.BranchID,
                                CountryID = c.CountryID,
                                BranchName = c.BranchName,
                                
                            });

                 var DbF = Microsoft.EntityFrameworkCore.EF.Functions;
               
                if (filter.BranchID>0)
                {
                   query = query.Where(q => q.BranchID == filter.BranchID);
                }
                
                /*
                if (!String.IsNullOrEmpty(filter.textSearch))
                {
                    query = query.Where(q => DbF.Like(q.Name, "%" + filter.textSearch + "%"));
                }
                */

                query = query.OrderBy(q => q.CountryID);

                 switch (filter.sortOrder)
                {
                    case "priority":
                        query = query.OrderBy(q => q.BranchName);
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
                var existingData = await _context.Branch.FindAsync(id);
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
        public async Task<IActionResult> Update(int id, [FromBody] BranchRequest request)
        {
            try
            { 
                var existingData = await _context.Branch.FindAsync(id);
                if (existingData == null)
                {
                      return BadRequest(new
                    {
                        message = "Data NotFound",
                        isSuccess = false
                    });
                }

                existingData.BranchID = request.BranchID;
                existingData.CountryID = request.CountryID;
                existingData.BranchName = request.BranchName;
                
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
        public async Task<IActionResult> Create([FromBody] BranchRequest request)
        {
             try
            { 
                var temp = new Branch
                {
                    BranchID = request.BranchID,
                    CountryID = request.CountryID,
                    BranchName = request.BranchName
                };

                 _context.Branch.Add(temp);
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
                
                var existingData = await _context.Branch.FindAsync(id);

                if (existingData == null)
                {
                      return BadRequest(new
                    {
                        message = "Data NotFound",
                        isSuccess = false
                    });
                }

                _context.Branch.Remove(existingData);
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
