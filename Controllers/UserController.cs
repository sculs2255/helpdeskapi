<<<<<<< HEAD
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
=======
﻿using System;
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
>>>>>>> 8318f1ec7da2072c666fde64ec81f69329da7c74

namespace HelpDeskApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class UserController : ControllerBase
    {
<<<<<<< HEAD
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
=======
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly DataContext _context;

        public UserController(
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            DataContext context)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        
        [HttpGet]
        [Route("GetUserList")]
        public async Task<ActionResult> GetUserList()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            IList<Claim> claim = identity.Claims.ToList();
            var userInfo = await _userManager.FindByEmailAsync(claim[1].Value);
            var userRole = await _userManager.GetRolesAsync(userInfo);
            try
            {
                if (userRole[0] == "Admin")
                {
                   
                    var user =  await (from u in _context.Users
                                join ur in _context.UserRoles on u.Id equals ur.UserId into urs
                                from urResult in urs.DefaultIfEmpty()

                                join r in _context.Roles on  urResult.RoleId equals r.Id into rs
                                from rResult in rs.DefaultIfEmpty()
                                select new
                                {
                                    Id = u.Id,
                                    FirstName = u.FirstName,
                                    LastName = u.LastName,
                                    Email = u.Email,
                                    Role = rResult.Name,
                                    PhoneNumber = u.PhoneNumber,
                                    isEnabled = u.IsEnabled
                                } 
                                ).ToListAsync();
                  
                    return Ok(new
                    {
                        data = user,
                        isSuccess = true
                    });
                }else{
                     return BadRequest(new
                    {
                        isSuccess = false
                    });
                }
            }
            catch (Exception)
            {
                return Ok(new
                {
                    isSuccess = false
                });

            }

        }

        [HttpGet]
        [Route("List")]
        public async Task<ActionResult> GetUserEnableList()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            IList<Claim> claim = identity.Claims.ToList();
            var userInfo = await _userManager.FindByEmailAsync(claim[1].Value);
           
            try
            {
                var user = await (from Role in _context.Roles
                            join user_Role in _context.UserRoles
                            on Role.Id equals user_Role.RoleId
                            select new
                            {
                                UserId = user_Role.UserId,
                                Role = Role.NormalizedName,

                            } into intermediate
                            join users in _context.Users.Where(t=> t.IsEnabled == 1)
                            on intermediate.UserId equals users.Id
                            select new
                            {
                                id = users.Id,
                                FirstName = users.FirstName,
                                LastName = users.LastName,
                                Email = users.Email,
                                UserName = users.UserName,
                                Role = intermediate.Role,
                                PhoneNumber = users.PhoneNumber,
                            }
                            ).ToListAsync();

                
                return Ok(new
                {
                    data = user,
                    isSuccess = true
                });
             
>>>>>>> 8318f1ec7da2072c666fde64ec81f69329da7c74
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    message = ex,
                    isSuccess = false
                });
<<<<<<< HEAD
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
=======

            }

        }

        [HttpGet("{id}")]
        public async Task<ActionResult> EditUser(string id)
        {
             var user = await _userManager.FindByIdAsync(id);
           
            try
            {
                if (user != null)
                {
                    var userRoles = await _userManager.GetRolesAsync(user);

                    var userInfo = new UserEditRequest
                    {
                        Id = user.Id,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        PhoneNumber = user.PhoneNumber,
                        Roles = userRoles,
                        IsEnabled = user.IsEnabled
                    };

                    return Ok(new
                    {
                        data = userInfo,
                        isSuccess = true
                    });
                }
                else
                {
                    return Ok(new
                    {
                        message = id + "is null",
                        isSuccess = false
                    });
                }

            }
            catch (Exception)
            {
                return Ok(new
                {
                    message = "You do not have permission to access.",
                    isSuccess = false
                });
            }

        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> CreateUser(UserCreateRequest user)
        {
            try
            {
                var newUser = new ApplicationUser()
                {
                    UserName = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    PhoneNumber = user.PhoneNumber,
                    Email = user.Email,
                    IsEnabled = user.IsEnabled
                };     

                  var newRole = user.NewRole;       

                var isCreated = await _userManager.CreateAsync(newUser, user.Password);
                if (isCreated.Succeeded)
                {
                  
                    await _userManager.AddToRoleAsync(newUser, newRole);
                    return Ok(new
                    {
                        isSuccess = true,
                    });   
                }
                else
                {
                    return BadRequest(new
                    {
                        errors = isCreated.Errors,
                        isSuccess = false
                    });
                }
>>>>>>> 8318f1ec7da2072c666fde64ec81f69329da7c74
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

<<<<<<< HEAD
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
=======

        [HttpPost]
        public async Task<IActionResult> EditUser(UserEditRequest user)
        {
            var userInfo = await _userManager.FindByIdAsync(user.Id);
            var userRoles = await _userManager.GetRolesAsync(userInfo);

            if (userInfo == null)
            {
                 return BadRequest(new
                {
                    isSuccess = false
                });
            }
            else
            {
                var oldRoleName ="";
                if(userRoles.Count()>0){
                     oldRoleName = userRoles[0];
                }else{
                     oldRoleName = "";
                }

                var newRole = user.NewRole;

                if (oldRoleName != newRole && oldRoleName != null){
                    var result = await _userManager.RemoveFromRolesAsync(userInfo, userRoles);
                    if (result.Succeeded)
                    {
                        result = await _userManager.AddToRoleAsync(userInfo,newRole);   
                    } 
                }
                
                userInfo.FirstName = user.FirstName;
                userInfo.LastName = user.LastName;
                userInfo.PhoneNumber = user.PhoneNumber;
                userInfo.IsEnabled = user.IsEnabled;
               
                try
                {
                    await _userManager.UpdateAsync(userInfo);

                    return Ok(new
                    {
                        data = user,
                        isSuccess = true
                    });
                }
                catch (Exception)
                {
                    return BadRequest(new
                    {
                        isSuccess = false
                    });

                }
            }
        }

     

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(id);
                if (user == null)
                {
                    
                    return Ok(new
                    {
                        message = "Data is Null.",
                        isSuccess = false
                    });
                }
                else
                { 
                   var result = await _userManager.DeleteAsync(user);
                    return Ok(new
                    {
                        data = result,
                        isSuccess = true
                    });
                }
            }
            catch (Exception ex)
            {
                return Ok(new
>>>>>>> 8318f1ec7da2072c666fde64ec81f69329da7c74
                {
                    message = ex,
                    isSuccess = false
                });
            }
        }


<<<<<<< HEAD
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
=======
        [HttpPost("ResetPW")]
        public async Task<IActionResult> changePassword([FromBody] UserResetPWRequest user)
>>>>>>> 8318f1ec7da2072c666fde64ec81f69329da7c74
        {
            try
            {
                
<<<<<<< HEAD
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
=======
           
                var userInfo = await _userManager.FindByIdAsync(user.Id);

                var token = await _userManager.GeneratePasswordResetTokenAsync(userInfo);
                var isSuccess =   await _userManager.ResetPasswordAsync(userInfo, token,user.PasswordNew);
               
                /*
                EmailHelper emailHelper = new EmailHelper();
                bool emailResponse = emailHelper.SendEmail(user.Email, confirmationLink);
                */
                
                if (isSuccess.Succeeded)
                {
                    return Ok(new 
                    {
                        data = user,
                        isSuccess = true
                    });
                }
                else
                {
                   
                    return BadRequest(new 
                    {
                        Errors = isSuccess.Errors.Select(x => x.Description).ToList(),
                        Success = false
                    });
                    
                }
            }
            catch (Exception)
            {
                return Ok(new
                {
                    isSuccess = false
                });

            }
        }

        [HttpGet]
        [Route("GeneratePassword")]
        public string GeneratePassword()
        {
            var options = _userManager.Options.Password;

            int length = options.RequiredLength;

            bool nonAlphanumeric = options.RequireNonAlphanumeric;
            bool digit = options.RequireDigit;
            bool lowercase = options.RequireLowercase;
            bool uppercase = options.RequireUppercase;

            StringBuilder password = new StringBuilder();
            Random random = new Random();

            while (password.Length < length)
            {
                char c = (char)random.Next(32, 126);

                password.Append(c);

                if (char.IsDigit(c))
                    digit = false;
                else if (char.IsLower(c))
                    lowercase = false;
                else if (char.IsUpper(c))
                    uppercase = false;
                else if (!char.IsLetterOrDigit(c))
                    nonAlphanumeric = false;
            }

            if (nonAlphanumeric)
                password.Append((char)random.Next(33, 48));
            if (digit)
                password.Append((char)random.Next(48, 58));
            if (lowercase)
                password.Append((char)random.Next(97, 123));
            if (uppercase)
                password.Append((char)random.Next(65, 91));

            return password.ToString();
        }
        
>>>>>>> 8318f1ec7da2072c666fde64ec81f69329da7c74
    }
}
