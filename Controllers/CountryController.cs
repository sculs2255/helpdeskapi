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
    public class CountryController : ControllerBase
    {
        private readonly DataContext _context;
        public  CountryController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult> List([FromQuery] CountryFilter filter)
        {
            try
            {

                var query = (from p in _context.Country
                             select new
                             {
                                 CountryID = p.CountryID,
                                 CountryName = p.CountryName,
                                 

                             });

                var DbF = Microsoft.EntityFrameworkCore.EF.Functions;

                if (filter.CountryID > 0)
                {
                    query = query.Where(q => q.CountryID == filter.CountryID);
                }

                /*
               if (!String.IsNullOrEmpty(filter.textSearch))
               {
                   query = query.Where(q => DbF.Like(q.Name, "%" + filter.textSearch + "%"));
               }
               */

              

                switch (filter.sortOrder)
                {
                    case "countryName":
                        query = query.OrderBy(q => q.CountryName);
                        break;
                    case "countryName_desc":
                        query = query.OrderByDescending(q => q.CountryName);
                        break;
                    case "countryID":
                        query = query.OrderBy(q => q.CountryID);
                        break;
                    case "countryID_desc":
                        query = query.OrderByDescending(q => q.CountryID);
                        break;
                    default:
                        query = query = query.OrderBy(q => q.CountryID);
                        break;
                }

                int totalItems = query.Count();
                var data = await query.Skip((filter.pageNumber - 1) * filter.pageSize).Take(filter.pageSize).ToListAsync();

                return Ok(new
                {
                    totalItems = totalItems,
                    data = data,
                    filter=filter,
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
                var existingData = await _context.Country.FindAsync(id);
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
        public async Task<IActionResult> Update(int id, [FromBody] CountryRequest request, int CountryName)
        {
            try
            {
                var existingData = await _context.Country.FindAsync(id);
                if (existingData == null)
                {
                    return BadRequest(new
                    {

                        message = "Data NotFound",
                        isSuccess = false
                    });
                }
                existingData.CountryID = request.CountryID;
                existingData.CountryName = request.CountryName;

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
        public async Task<IActionResult> Create([FromBody] CountryRequest request)
        {
            try
            {
                var temp = new Country
                {
                    CountryID = request.CountryID,
                    CountryName = request.CountryName
                };

                _context.Country.Add(temp);
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

                var existingData = await _context.Country.FindAsync(id);

                if (existingData == null)
                {
                    return BadRequest(new
                    {
                        message = "Data NotFound",
                        isSuccess = false
                    });
                }

                _context.Country.Remove(existingData);
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
