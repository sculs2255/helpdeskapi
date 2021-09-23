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
    public class CaseController : ControllerBase
    {
        private readonly DataContext _context;
        public CaseController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult> List([FromQuery] CaseFilter filter)
        {
            try
            {
                //Linq Left Join
                var query = (from c in _context.HD_Case
                             join ic1 in _context.IncidentCase on c.CaseID equals ic1.CaseID into ic2
                             from ic in ic2.DefaultIfEmpty()
                             join rc1 in _context.RequestCase on c.CaseID equals rc1.CaseID into rc2
                             from rc in rc2.DefaultIfEmpty()
                                 //  where c.CaseID == ic.CaseID
                             select new
                             {
                                 //HD_Case data
                                 c.CaseID,
                                 c.CaseTypeID,
                                 c.CaseDate,
                                 c.PriorityID,
                                 c.StatusID,
                                 //IncidentCase data
                                 IcSystemID = ic.SystemID,
                                 IcModuleID = ic.ModuleID,
                                 IcProgramID = ic.ProgramID,
                                 IcTopic = ic.Topic,
                                 IcDescription = ic.Description,
                                 IcFile = ic.File,
                                 IcNote = ic.Note,
                                 IcCCMail = ic.CCMail,
                                 //RequestCase data
                                 RcSystemID = rc.SystemID,
                                 RcTopicID = rc.TopicID,
                                 RcDescription = rc.Description,
                                 RcFile = rc.File,
                                 RcNote = rc.Note,
                                 RcCCMail = rc.CCMail,
                             });
                // var data = await query.FirstOrDefaultAsync();

                //Linq Inner Join
                // var query =(from c in _context.HD_Case 
                //                         join ic in _context.IncedenCase on ic.CaseID equals ic.CaseID 
                //                         select new
                //                         {
                //                         });
                var DbF = Microsoft.EntityFrameworkCore.EF.Functions;

                if (filter.caseTypeID > 0)
                {
                    query = query.Where(q => q.CaseTypeID == filter.caseTypeID);
                }

                /*
                if (!String.IsNullOrEmpty(filter.textSearch))
                {
                    query = query.Where(q => DbF.Like(q.Name, "%" + filter.textSearch + "%"));
                }
                */

                switch (filter.sortOrder)
                {
                    case "priorityID":
                        query = query.OrderBy(q => q.PriorityID);
                        break;
                    case "priorityID_desc":
                        query = query.OrderByDescending(q => q.PriorityID);
                        break;
                    case "caseID_desc":
                        query = query.OrderByDescending(q => q.CaseID);
                        break;
                    default:
                        query = query = query.OrderBy(q => q.CaseID);
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
                var existingData = await _context.HD_Case.FindAsync(id);
                if (existingData.CaseTypeID == 1)
                {
                    var query = (from c in _context.HD_Case
                                 join ic1 in _context.IncidentCase on c.CaseID equals ic1.CaseID into ic2
                                 from ic in ic2.DefaultIfEmpty()
                                 where c.CaseID == existingData.CaseID && c.CaseID == ic.CaseID
                                 select new
                                 {
                                     //HD_Case data
                                     c.CaseID,
                                     c.CaseTypeID,
                                     c.CaseDate,
                                     c.PriorityID,
                                     c.StatusID,
                                     //IncidentCase data
                                     SystemID = ic.SystemID,
                                     ModuleID = ic.ModuleID,
                                     ProgramID = ic.ProgramID,
                                     Topic = ic.Topic,
                                     Description = ic.Description,
                                     File = ic.File,
                                     Note = ic.Note,
                                     CCMail = ic.CCMail,
                                 });
                    var dataIc = await query.FirstOrDefaultAsync();
                    return Ok(new
                    {
                        data = dataIc,
                        isSuccess = true
                    });
                }
                else
                {
                    var query = (from c in _context.HD_Case
                                 join rc1 in _context.RequestCase on c.CaseID equals rc1.CaseID into rc2
                                 from rc in rc2.DefaultIfEmpty()
                                 where c.CaseID == existingData.CaseID && c.CaseID == rc.CaseID
                                 select new
                                 {
                                     //HD_Case data
                                     c.CaseID,
                                     c.CaseTypeID,
                                     c.CaseDate,
                                     c.PriorityID,
                                     c.StatusID,
                                     //RequestCase data
                                     SystemID = rc.SystemID,
                                     TopicID = rc.TopicID,
                                     Description = rc.Description,
                                     File = rc.File,
                                     Note = rc.Note,
                                     CCMail = rc.CCMail,
                                 });
                    var dataRc = await query.FirstOrDefaultAsync();
                    return Ok(new
                    {
                        data = dataRc,
                        isSuccess = true
                    });
                }

                //var existingData = await _context.HD_Case.FindAsync(id);
                // // var existingDataIc = await _context.IncidentCase.Where(i => i.CaseID == existingData.CaseID).ToListAsync();
                // // var existingDataRc = await _context.RequestCase.Where(r => r.CaseID == existingData.CaseID).ToListAsync();

                // //Chack Case Type == Incident?
                // if (existingData.CaseID == id && existingData.CaseTypeID == 1)
                // {
                //     var existingDataIc = await _context.IncidentCase.Where(i => i.CaseID == existingData.CaseID).FirstOrDefaultAsync();

                //     return Ok(new
                //     {
                //         data = existingData,
                //         dataIc = existingDataIc,
                //         isSuccess = true
                //     });
                // }

                // //Chack Case Type == Request?
                // if (existingData.CaseID == id && existingData.CaseTypeID == 2)
                // {
                //     var existingDataRc = await _context.RequestCase.Where(r => r.CaseID == existingData.CaseID).ToListAsync();

                //     return Ok(new
                //     {
                //         data = existingData,
                //         dataRc = existingDataRc,
                //         isSuccess = true
                //     });
                // }
                //Chack DataCase == Null?
                // if (data == null)
                // {
                //     return BadRequest(new
                //     {
                //         message = "Data NotFound",
                //         isSuccess = false
                //     });
                // }
                // return Ok(new
                // {

                //     data = existingData,
                //     // dataIc = existingDataIc,
                //     // dataRc = existingDataRc,
                //     isSuccess = true
                // });
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

                if (temp.CaseTypeID == 1)
                {
                    var transICase = new IncidentCase
                    {
                        CaseID = temp.CaseID,
                        SystemID = request.SystemID,
                        ModuleID = request.ModuleID,
                        ProgramID = request.ProgramID,
                        Topic = request.Topic,
                        Description = request.Description,
                        File = request.File,
                        Note = request.Note,
                        CCMail = request.CCMail
                    };
                    _context.IncidentCase.Add(transICase);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    Console.WriteLine(temp.CaseTypeID);
                    var transRCase = new RequestCase
                    {
                        CaseID = temp.CaseID,
                        SystemID = request.SystemID,
                        TopicID = request.TopicID,
                        Description = request.Description,
                        File = request.File,
                        Note = request.Note,
                        CCMail = request.CCMail
                    };
                    _context.RequestCase.Add(transRCase);
                    await _context.SaveChangesAsync();
                }



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
