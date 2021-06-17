using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Framework_API.Data;
using Framework_API.Models;
using Framework_API.DataSource.Interface;
using Microsoft.Extensions.Logging;

namespace Framework_API.Controllers
{
    public class RolesController : Controller
    {
        private readonly IRoleRepository _roleRepository;
        private readonly ILogger<RolesController> _logger;

        public RolesController(IRoleRepository roleRepository, ILogger<RolesController> logger)
        {
            _roleRepository = roleRepository;
            _logger = logger;
        }

        // GET: Roles
        public async Task<IActionResult> Index()
        {
            _logger.LogInformation("Listing all registers");
            return View(await _roleRepository.FetchAll().ToListAsync());
        }

        // GET: Roles/Details/5
        /*public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var role = await _context.RolesTable
                .FirstOrDefaultAsync(m => m.Id == id);
            if (role == null)
            {
                return NotFound();
            }

            return View(role);
        }*/

        // GET: Roles/Create
        public IActionResult Create()
        {
            _logger.LogInformation("Starting roles creation");
            return View();
        }

        // POST: Roles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Description,Name")] Role role)
        {
            if (ModelState.IsValid)
            {
                _logger.LogInformation("Checking if role exist");
                bool roleExist = await _roleRepository.RoleExist(role.Name);

                if (!roleExist)
                {
                    role.NormalizedName = role.Name.ToUpper();
                    await _roleRepository.Insert(role);
                    _logger.LogInformation("New role created");

                    return RedirectToAction("Index", "Roles");
                }                
            }
            _logger.LogError("Invalid data");
            return View(role);
        }

        // GET: Roles/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            _logger.LogInformation("Updating role");
            if (id == null)
            {
                _logger.LogInformation("Role not found");
                return NotFound();
            }

            var role = await _roleRepository.FetchById(id);
            if (role == null)
            {
                return NotFound();
            }
            return View(role);
        }

        // POST: Roles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Description,Id,Name,NormalizedName,ConcurrencyStamp")] Role role)
        {
            if (id != role.Id)
            {
                _logger.LogInformation("Role not found");
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _roleRepository.Update(role);
                _logger.LogInformation("Role updated");
                return RedirectToAction("Index","Roles");
            }
            _logger.LogError("Invalid data");
            return View(role);
        }

        // GET: Roles/Delete/5
        /*public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var role = await _context.RolesTable
                .FirstOrDefaultAsync(m => m.Id == id);
            if (role == null)
            {
                return NotFound();
            }

            return View(role);
        }*/

        // POST: Roles/Delete/5
        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            await _roleRepository.Remove(id);
            _logger.LogInformation("Role removed");
            return RedirectToAction("Index", "Roles");
        }
    }
}