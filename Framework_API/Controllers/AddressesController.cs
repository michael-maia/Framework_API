using Framework_API.DataSource.Interface;
using Framework_API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Framework_API.Controllers
{
    public class AddressesController : Controller
    {       
        private readonly IUserRepository _userRepository;
        private readonly IAddressRepository _addressRepository;
        private readonly ILogger<AddressesController> _logger;

        public AddressesController(IUserRepository userRepository, IAddressRepository addressRepository, ILogger<AddressesController> logger)
        {
            _userRepository = userRepository;
            _addressRepository = addressRepository;
            _logger = logger;
        }

        public async Task<IActionResult> Create()
        {
            _logger.LogInformation("New address");
            var user = await _userRepository.FetchLoggedUser(User);
            var address = new Address { UserId = user.Id };
            return View(address);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AddressId,Street,Number,District,City,State,CEP,UserId")] Address address)
        {
            if (ModelState.IsValid)
            {
                await _addressRepository.Insert(address);
                _logger.LogInformation("New address created");
                return RedirectToAction("Index", "Users");
            }
            _logger.LogError("Error while creating address");
            return View(address);
        }

        public async Task<IActionResult> Edit(int id)
        {
            _logger.LogInformation("Updating address");

            var address = await _addressRepository.FetchById(id);
            if (address == null)
            {
                _logger.LogError("Address not found");
                return NotFound();
            }
            return View(address);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AddressId,Street,Number,District,City,State,CEP,UserId")] Address address)
        {
            if (id != address.AddressId)
            {
                _logger.LogError("Address not found");
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _addressRepository.Update(address);
                _logger.LogInformation("Address updated");
                return RedirectToAction("Index", "Users");
            }
            _logger.LogError("Invalid address");
            return View(address);
        }

        [HttpPost]
        public async Task<JsonResult> Delete(int id)
        {
            await _addressRepository.Remove(id);
            _logger.LogInformation("Address removed");
            return Json("Address removed");
        }
    }    
}