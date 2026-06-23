using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using RSR.DAL.Data;
using RSR.DAL.Models.User;

namespace RSR.DAL.Utils
{
    public class UserSeedData : ISeedData
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;

        public UserSeedData(UserManager<ApplicationUser> userManager, ApplicationDbContext context, IConfiguration configuration)
        {
            _userManager = userManager;
            _context = context;
            _configuration = configuration;
        }

        public async Task DataSeed()
        {
            await UnlockAllUsers();

            var password = _configuration["SeedData:Password"]!;

            await SeedCoordinators(password);
            await SeedSupervisors(password);
            await SeedExaminers(password);
            await SeedStudents(password);
        }

        private async Task UnlockAllUsers()
        {
            var users = await _userManager.Users.ToListAsync();
            foreach (var user in users)
            {
                if (user.LockoutEnabled || user.LockoutEnd != null || user.AccessFailedCount > 0)
                {
                    user.LockoutEnabled = false;
                    user.LockoutEnd = null;
                    user.AccessFailedCount = 0;
                    await _userManager.UpdateAsync(user);
                }
            }
        }

        // ── Helpers ─────────────────────────────────────────────────────────

        private async Task<ApplicationUser?> CreateUser(
            string fullName, string userName, string email, string role, string password)
        {
            var existing = await _userManager.FindByEmailAsync(email);
            if (existing != null)
            {
                if (!await _userManager.IsInRoleAsync(existing, role))
                    await _userManager.AddToRoleAsync(existing, role);
                return existing;
            }

            var user = new ApplicationUser
            {
                FullName = fullName,
                UserName = userName,
                Email = email,
                IsActive = true,
                EmailConfirmed = true,
                LockoutEnabled = false
            };

            var result = await _userManager.CreateAsync(user, password);
            if (!result.Succeeded) return null;

            await _userManager.AddToRoleAsync(user, role);
            return user;
        }

        // ── Coordinators ─────────────────────────────────────────────────────

        private async Task SeedCoordinators(string password)
        {
            var coordinators = new[]
            {
                ("Sarah Jalal Shtawi",  "SarahJalal123",   "sarah.sht03@gmail.com",     "201055223", "Computer Science"),
                ("Layla Hassan Nasser", "LaylaHassan456",   "layla.hassan@university.edu","201055224", "Information Technology"),
                ("Omar Khaled Younis",  "OmarKhaled789",   "omar.younis@university.edu", "201055225", "Software Engineering"),
            };

            foreach (var (fullName, userName, email, number, department) in coordinators)
            {
                var user = await CreateUser(fullName, userName, email, "Coordinator", password);
                if (user == null) continue;

                await _context.Coordinators.AddAsync(new CoordinatorProfile
                {
                    UserId = user.Id,
                    CoordinatorNumber = number,
                    Department = department
                });
            }

            await _context.SaveChangesAsync();
        }

        // ── Supervisors ──────────────────────────────────────────────────────

        private async Task SeedSupervisors(string password)
        {
            var supervisors = new[]
            {
                ("Ahmed Ramzi Khalil",    "AhmedRamzi101",   "ahmed.ramzi@university.edu",   "SUP-1001", "Computer Science"),
                ("Nour Salim Haddad",     "NourSalim202",    "nour.haddad@university.edu",   "SUP-1002", "Information Technology"),
                ("Rami Fouad Jaber",      "RamiFouad303",    "rami.jaber@university.edu",    "SUP-1003", "Software Engineering"),
                ("Hana Ziad Mustafa",     "HanaZiad404",     "hana.mustafa@university.edu",  "SUP-1004", "Cybersecurity"),
                ("Yousef Adel Barakat",   "YousefAdel505",   "yousef.barakat@university.edu","SUP-1005", "Data Science"),
            };

            foreach (var (fullName, userName, email, number, department) in supervisors)
            {
                var user = await CreateUser(fullName, userName, email, "Supervisor", password);
                if (user == null) continue;

                await _context.Supervisors.AddAsync(new SupervisorProfile
                {
                    UserId = user.Id,
                    SupervisorNumber = number,
                    Department = department
                });
            }

            await _context.SaveChangesAsync();
        }

        // ── Examiners ────────────────────────────────────────────────────────

        private async Task SeedExaminers(string password)
        {
            var examiners = new[]
            {
                ("Samer Wael Najjar",    "SamerWael601",   "samer.najjar@university.edu",  "EXM-2001", "Computer Science"),
                ("Dina Faris Sabbagh",   "DinaFaris702",   "dina.sabbagh@university.edu",  "EXM-2002", "Information Technology"),
                ("Khaled Mousa Issa",    "KhaledMousa803", "khaled.issa@university.edu",   "EXM-2003", "Software Engineering"),
                ("Ruba Naji Hamdan",     "RubaNaji904",    "ruba.hamdan@university.edu",   "EXM-2004", "Cybersecurity"),
            };

            foreach (var (fullName, userName, email, number, department) in examiners)
            {
                var user = await CreateUser(fullName, userName, email, "Examiner", password);
                if (user == null) continue;

                await _context.Examiners.AddAsync(new ExaminerProfile
                {
                    UserId = user.Id,
                    ExaminerNumber = number,
                    Department = department
                });
            }

            await _context.SaveChangesAsync();
        }

        // ── Students ─────────────────────────────────────────────────────────

        private async Task SeedStudents(string password)
        {
            var students = new[]
            {
                ("Ali Hassan Qasim",      "AliHassan001",    "ali.qasim@student.edu",       "STU-3001", "Faculty of Engineering", "Computer Science"),
                ("Fatima Nour Al-Din",    "FatimaNour002",   "fatima.aldin@student.edu",    "STU-3002", "Faculty of Engineering", "Software Engineering"),
                ("Bilal Samir Khalil",    "BilalSamir003",   "bilal.khalil@student.edu",    "STU-3003", "Faculty of IT",          "Information Technology"),
                ("Rasha Tariq Mansour",   "RashaTariq004",   "rasha.mansour@student.edu",   "STU-3004", "Faculty of IT",          "Cybersecurity"),
                ("Ziad Omar Halabi",      "ZiadOmar005",     "ziad.halabi@student.edu",     "STU-3005", "Faculty of Engineering", "Data Science"),
                ("Sana Bassam Houri",     "SanaBassam006",   "sana.houri@student.edu",      "STU-3006", "Faculty of Engineering", "Computer Science"),
                ("Majd Faris Nasser",     "MajdFaris007",    "majd.nasser@student.edu",     "STU-3007", "Faculty of IT",          "Software Engineering"),
                ("Lina Wael Saleh",       "LinaWael008",     "lina.saleh@student.edu",      "STU-3008", "Faculty of IT",          "Information Technology"),
                ("Kareem Nabil Ayyash",   "KareemNabil009",  "kareem.ayyash@student.edu",   "STU-3009", "Faculty of Engineering", "Computer Science"),
                ("Hala Adnan Khatib",     "HalaAdnan010",    "hala.khatib@student.edu",     "STU-3010", "Faculty of IT",          "Data Science"),
            };

            foreach (var (fullName, userName, email, number, college, major) in students)
            {
                var user = await CreateUser(fullName, userName, email, "Student", password);
                if (user == null) continue;

                await _context.Students.AddAsync(new StudentProfile
                {
                    UserId = user.Id,
                    StudentNumber = number,
                    College = college,
                    Major = major
                });
            }

            await _context.SaveChangesAsync();
        }
    }
}
