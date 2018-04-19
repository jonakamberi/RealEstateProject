using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RealEstateProject.Data;
using RealEstateProject.Models;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace RealEstateProject.Controllers
{
    [Authorize(Policy = "RequireAdminRole")]
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        IConfiguration _configuration;

        public AdminController(UserManager<ApplicationUser> userManager, ApplicationDbContext context, IConfiguration iconfiguration)
        {
            _userManager = userManager;
            _context = context;
            _configuration = iconfiguration;
        }
        // GET: /Admin/
        public IActionResult Index()
        {
            ViewData["ErrorMessage"] = TempData["ErrorMessage"] as string;
            ViewData["SuccessData"] = TempData["SuccessData"] as string;

            // Retrieving list of users, joined with the respective roles
            var listOfUsers = (from u in _context.Users
                               let query = (from ur in _context.Set<IdentityUserRole<string>>()
                                            where ur.UserId.Equals(u.Id)
                                            join r in _context.Roles on ur.RoleId equals r.Id
                                            select r.Name)
                               select new UserWithRolesViewModel() { User = u, Roles = query.ToList<string>() })
                .ToList();
            
            return View(listOfUsers);
        }


        // GET: Admin/MakeAdmin/{ID}
        public async Task<IActionResult> MakeAdmin(string id)
        {
            try
            {
                // Find user with id and add to the role Admin.
                var appUser = await _userManager.FindByIdAsync(id);
                await _userManager.AddToRoleAsync(appUser, "Admin");
                _context.SaveChanges();
            }
            catch
            {
                TempData["ErrorMessage"] = "An error occurred";
                return RedirectToAction("Index");
                throw;
            }
            TempData["SuccessData"] = "User added to admin role";
            return RedirectToAction(nameof(Index));
        }



        // GET: Admin/RemoveAdmin/{ID}
        public async Task<IActionResult> RemoveAdmin(string id)
        {
            try
            {
                var appUser = await _userManager.FindByIdAsync(id);

                if (appUser.Email == _configuration.GetSection("AppSettings")["UserEmail"]) {
                    TempData["ErrorMessage"] = "You cannot delete a super admin";
                    return RedirectToAction("Index");
                }
                await _userManager.RemoveFromRoleAsync(appUser, "Admin");
                _context.SaveChanges();
            }
            catch
            {
                TempData["ErrorMessage"] = "An error occurred";
                return RedirectToAction("Index");
                throw;
            }
            TempData["SuccessData"] = "User removed from admin role";
            return RedirectToAction(nameof(Index));
        }


    }
}
