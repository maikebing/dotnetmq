using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using static DotNetMQ.Controllers.InstallerController;

namespace DotNetMQ.Data
{
    public class ApplicationDBInitializer
    {
        private readonly RoleManager<IdentityRole> _role;

        private ApplicationDbContext _context;
        private ILogger _logger;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly SignInManager<IdentityUser> _signInManager;

        public ApplicationDBInitializer(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            IConfiguration configuration, ILogger<ApplicationDBInitializer> logger,
            ApplicationDbContext context, RoleManager<IdentityRole> role
            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _logger = logger;
            _context = context;
            _role = role;
        }

        public async Task SeedRoleAsync()
        {
            var roles = new IdentityRole[]{new IdentityRole(nameof(UserRole.Anonymous))
                    , new IdentityRole(nameof(UserRole.NormalUser))
                    , new IdentityRole(nameof(UserRole.Administrator))
                   };
            foreach (var role in roles)
            {
                if (!await _role.RoleExistsAsync(role.Name))
                {
                    if ((await _role.CreateAsync(role)).Succeeded)
                    {
                        await _role.UpdateNormalizedRoleNameAsync(role);
                    }
                };
            }
        }

        public async Task SeedUserAsync(InstallDto model)
        {
         
            IdentityUser user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                user = new IdentityUser
                {
                    Email = model.Email,
                    UserName = model.Email,
                    PhoneNumber = model.PhoneNumber
                };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, false);
                    await _signInManager.UserManager.AddClaimAsync(user, new Claim(ClaimTypes.Email, model.Email));
         
                    await _signInManager.UserManager.AddToRoleAsync(user, nameof(UserRole.Anonymous));
                    await _signInManager.UserManager.AddToRoleAsync(user, nameof(UserRole.NormalUser));
                    await _signInManager.UserManager.AddToRoleAsync(user, nameof(UserRole.Administrator));
                    
                }
                else
                {
                    throw new Exception(string.Join(',', result.Errors.ToList().Select(ie => $"code={ie.Code},msg={ie.Description}")));
                }
            }
           
            await _context.SaveChangesAsync();
        }
    }
}