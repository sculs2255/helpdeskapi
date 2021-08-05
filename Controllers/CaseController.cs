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

namespace HelpDeskApi.Controllers
{
     [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CaseController : ControllerBase
    {
        private readonly DataContext _context;
        public CaseController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult> List()
        {
            try
            {
                                 
                var data = await (from c in _context.HD_Case
                            select new
                            {
                                CaseID = c.CaseID,
                                CaseTypeID = c.CaseTypeID,
                                CaseDate = c.CaseDate,
                                PriorityID = c.PriorityID,
                                StatusID = c.StatusID
                               
                            }).ToListAsync();
                return Ok(new
                {
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
                var existingData = await _context.HD_Case.FindAsync(id);
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
        public async Task<IActionResult> Update(int id, [FromBody] CaseRequest request)
        {
            try
            { 
                var existingData = await _context.HD_Case.FindAsync(id);
                if (existingData == null)
                {
                      return BadRequest(new
                    {
                        message = "Data NotFound",
                        isSuccess = false
                    });
                }

                existingData.CaseTypeID = request.CaseTypeID;
                existingData.PriorityID = request.PriorityID;
                existingData.StatusID = request.StatusID;
                
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
        public async Task<IActionResult> Create([FromBody] CaseRequest request)
        {
             try
            { 
                var temp = new Case
                {
                    CaseTypeID = request.CaseTypeID,
                    CaseDate = DateTime.Now,
                    PriorityID = request.PriorityID,
                    StatusID = request.StatusID
                };

                 _context.HD_Case.Add(temp);
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
                
                var existingData = await _context.HD_Case.FindAsync(id);

                if (existingData == null)
                {
                      return BadRequest(new
                    {
                        message = "Data NotFound",
                        isSuccess = false
                    });
                }

                _context.HD_Case.Remove(existingData);
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
