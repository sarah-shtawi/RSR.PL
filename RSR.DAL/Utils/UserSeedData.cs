using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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

        public UserSeedData(UserManager<ApplicationUser> userManager , ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }
        public async Task DataSeed()
        {
            if(! await _userManager.Users.AnyAsync())
            {
                var coordinator = new ApplicationUser
                {
                    FullName = "Anas Melhem",
                    UserName = "AnasMelhem123",
                    Email = "Anas@gmail.com",
                    IsActive = true,
                    EmailConfirmed = true   
                };
                // Add to Data Base 
               var result =  await _userManager.CreateAsync(coordinator, "Anas@123");
                if (result.Succeeded) 
                {
                    // Add Role To Coordinator 
                    await _userManager.AddToRoleAsync(coordinator, "Coordinator");
                    // create coordinator profile 
                    var coordinatorProfile = new CoordinatorProfile
                    {
                        UserId = coordinator.Id,
                        CoordinatorNumber = "201055223",
                        Department = "Computer Engineering" 
                    };
                   await _context.Coordinators.AddAsync(coordinatorProfile);
                    await _context.SaveChangesAsync();
                }
               
            }
        }
    }
}
