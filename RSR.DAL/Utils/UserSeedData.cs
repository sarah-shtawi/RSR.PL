using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using RSR.DAL.Data;
using RSR.DAL.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSR.DAL.Utils
{
    public class UserSeedData : ISeedData
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;

        public UserSeedData(UserManager<ApplicationUser> userManager , ApplicationDbContext context , IConfiguration configuration)
        {
            _userManager = userManager;
            _context = context;
            _configuration = configuration;
        }
        public async Task DataSeed()
        {
            if(! await _userManager.Users.AnyAsync())
            {
                var coordinator = new ApplicationUser
                {
                    FullName = "Sarah Jalal Shtawi",
                    UserName = "SarahJalal123",
                    Email = "sarah.sht03@gmail.com",
                    IsActive = true,
                    EmailConfirmed = true   
                };
                // Add to Data Base 
               var result =  await _userManager.CreateAsync(coordinator, _configuration["SeedData:Password"]);
                if (result.Succeeded) 
                {
                    // Add Role To Coordinator 
                    await _userManager.AddToRoleAsync(coordinator, "Coordinator");
                    // create coordinator profile 
                    var coordinatorProfile = new CoordinatorProfile
                    {
                        UserId = coordinator.Id,
                        CoordinatorNumber = "201055223",
                    };
                   await _context.Coordinators.AddAsync(coordinatorProfile);
                   await _context.SaveChangesAsync();
                }
               
            }
        }
    }
}
