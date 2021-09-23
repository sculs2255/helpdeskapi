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
<<<<<<< HEAD:Controllers/BranchController.cs
        public  BranchController(DataContext context)
=======
        public IsmDbController(DataContext context)
>>>>>>> 8318f1ec7da2072c666fde64ec81f69329da7c74:Controllers/IsmDbController.cs
        {
            _context = context;
        }

        [HttpGet]
<<<<<<< HEAD:Controllers/BranchController.cs
        public async Task<ActionResult> List([FromQuery] BranchFilter filter)
        {
            try
            {

                var query = (from m in _context.Branch
                             select new
                             {
                                 BranchID = m.BranchID,
                                 BranchName= m.BranchName,
                                 m.CountryID 

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
                    case "BranchName":
                        query = query.OrderBy(q => q.BranchName);
=======
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
>>>>>>> 8318f1ec7da2072c666fde64ec81f69329da7c74:Controllers/IsmDbController.cs
                        break;
                    case "BranchName_desc":
                        query = query.OrderByDescending(q => q.BranchName);
                        break;
                    case "BranchID":
                        query = query.OrderBy(q => q.BranchID);
                        break;
                    case "BranchID_desc":
                        query = query.OrderByDescending(q => q.BranchID);
                        break;
                    default:
                        query = query = query.OrderBy(q => q.BranchName);
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
<<<<<<< HEAD:Controllers/BranchController.cs
        public async Task<IActionResult> Update(int id, [FromBody] BranchRequest request, int BranchName)
        {
            try
            {
                var existingData = await _context.Branch.FindAsync(id);
=======
        public async Task<IActionResult> Update(int id, [FromBody] IsmDbRequest request)
        {
            try
            {
                var existingData = await _context.IsmDb.FindAsync(id);
>>>>>>> 8318f1ec7da2072c666fde64ec81f69329da7c74:Controllers/IsmDbController.cs
                if (existingData == null)
                {
                    return BadRequest(new
                    {

                        message = "Data NotFound",
                        isSuccess = false
                    });
                }
<<<<<<< HEAD:Controllers/BranchController.cs
                existingData.BranchID = request.BranchID;
                existingData.BranchName = request.BranchName;
=======
                existingData.IsmID = request.IsmID;
                existingData.IsmName = request.IsmName;
>>>>>>> 8318f1ec7da2072c666fde64ec81f69329da7c74:Controllers/IsmDbController.cs

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
<<<<<<< HEAD:Controllers/BranchController.cs
                var temp = new Branch
                {
                    BranchID = request.BranchID,
                    BranchName = request.BranchName
                };

                _context.Branch.Add(temp);
=======
                var temp = new IsmDb
                {
                    IsmID = request.IsmID,
                    IsmName = request.IsmName
                };

                _context.IsmDb.Add(temp);
>>>>>>> 8318f1ec7da2072c666fde64ec81f69329da7c74:Controllers/IsmDbController.cs
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

<<<<<<< HEAD:Controllers/BranchController.cs
                var existingData = await _context.Branch.FindAsync(id);
=======
                var existingData = await _context.IsmDb.FindAsync(id);
>>>>>>> 8318f1ec7da2072c666fde64ec81f69329da7c74:Controllers/IsmDbController.cs

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
