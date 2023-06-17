using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using core_strength_yoga_products_api.Data.Contexts;
using core_strength_yoga_products_api.DTO;
using core_strength_yoga_products_api.Models;
using core_strength_yoga_products_api.Models.Enums;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;


namespace core_strength_yoga_products_api.Controllers
{
    [Microsoft.AspNetCore.Mvc.Route("api/v1/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        
        private readonly ILogger<AuthController> _logger;
        private readonly CoreStrengthYogaProductsApiDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        
        public AuthController(ILogger<AuthController> logger, CoreStrengthYogaProductsApiDbContext context,UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IConfiguration configuration)
        {
            _logger = logger;
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
        }
        
       
       [Microsoft.AspNetCore.Mvc.HttpPost]
        [Microsoft.AspNetCore.Mvc.Route("login")]
        public async Task<IActionResult> Login([Microsoft.AspNetCore.Mvc.FromBody] LoginDTO model)
        {
            var user = await _userManager.FindByEmailAsync(model.Username);
            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                var userJson = JsonConvert.SerializeObject(user);

                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }

               var token = GetToken(authClaims);

                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    //expiration = token.ValidTo
                });
            }
            return Unauthorized();
        }

        [Microsoft.AspNetCore.Mvc.HttpPost]
        [Microsoft.AspNetCore.Mvc.Route("register")]
        public async Task<IActionResult> Register([Microsoft.AspNetCore.Mvc.FromBody] CustomerDTO model)
        {
            var userExists = await _userManager.FindByEmailAsync(model.CustomerDetail.Email);
            if (userExists != null)
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new ResponseDTO() { Status = "Error", Message = "User already exists!" });

            IdentityUser user = new()
            {
                Email = model.CustomerDetail.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.IdentityUserName
            };
            var result = await _userManager.CreateAsync(user, model.CustomerDetail.Password);
            if (!result.Succeeded)
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new ResponseDTO()
                    {
                        Status = "Error", Message = "User creation failed! Please check user details and try again."
                    });
            
            if (!await _roleManager.RoleExistsAsync(UserRoles.Admin))
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
            if (!await _roleManager.RoleExistsAsync(UserRoles.User))
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.User));
            
            if (await _roleManager.RoleExistsAsync(UserRoles.User))
            {
                await _userManager.AddToRoleAsync(user, UserRoles.User);
            }


            CustomerDetail customerDetail = new()
            {
                Email = model.CustomerDetail.Email,
                Surname = model.CustomerDetail.Surname,
                PhoneNo = model.CustomerDetail.PhoneNo,
                FirstName = model.CustomerDetail.FirstName,
            };

            Customer customer = new()
            {
                IdentityUserName = model.IdentityUserName,
                CreatedAt = DateTime.Now,
                IsActive = true,
                IsGdpr = false,
                CustomerDetail = customerDetail
            };

            _context.Customers.Add(customer);
            _context.SaveChanges();

            
                var userRoles = await _userManager.GetRolesAsync(user);
                var userJson = JsonConvert.SerializeObject(user);

                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }

                var token = GetToken(authClaims);

                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo
                });
            

            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        [Microsoft.AspNetCore.Mvc.HttpPost]
        [Microsoft.AspNetCore.Mvc.Route("register-admin")]
        public async Task<IActionResult> RegisterAdmin([Microsoft.AspNetCore.Mvc.FromBody] CustomerDTO model)
        {
            var userExists = await _userManager.FindByEmailAsync(model.CustomerDetail.Email);
            if (userExists != null)
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseDTO() { Status = "Error", Message = "User already exists!" });

            IdentityUser user = new()
            {
                Email = model.CustomerDetail.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.IdentityUserName
            };
            var result = await _userManager.CreateAsync(user, model.CustomerDetail.Password);
            if (!result.Succeeded)
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseDTO() { Status = "Error", Message = "User creation failed! Please check user details and try again." });

            if (!await _roleManager.RoleExistsAsync(UserRoles.Admin))
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
            if (!await _roleManager.RoleExistsAsync(UserRoles.User))
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.User));

            if (await _roleManager.RoleExistsAsync(UserRoles.Admin))
            {
                await _userManager.AddToRoleAsync(user, UserRoles.Admin);
            }
            if (await _roleManager.RoleExistsAsync(UserRoles.Admin))
            {
                await _userManager.AddToRoleAsync(user, UserRoles.User);
            }
            return Ok(new ResponseDTO() { Status = "Success", Message = "User created successfully!" });
        }

        private JwtSecurityToken GetToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
            var credentials = new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256);


            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddHours(3),
                claims: authClaims,
                signingCredentials: credentials
                );

            return token;
        }


        [Authorize, Microsoft.AspNetCore.Mvc.HttpGet, Microsoft.AspNetCore.Mvc.Route("dummy")]
        public async Task<IActionResult> Dummy()
        {
            
            return Ok(new ResponseDTO() { Status = "Success", Message = "User created successfully!" });
        }
    }
    
       
    }
    
    
    
