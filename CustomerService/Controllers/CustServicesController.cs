using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TherapiCareTest.Data;
using TherapiCareTest.Models;

namespace TherapiCareTest.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.SignalR;
    //using therapicare.Hubs; // Ensure this namespace matches your actual namespace for ChatHub
    using System.Threading.Tasks;
    using Therapi.Utility;

    namespace TherapiCareTest.Areas.CustomerService.Controllers
    {
        [Area("CustomerService")]
        [Authorize(Roles = SD.Role_CustomerService)]
        public class CustServicesController : Controller
        {
            private readonly IHubContext<ChatHub> _chatHubContext;

            public CustServicesController(IHubContext<ChatHub> chatHubContext)
            {
                _chatHubContext = chatHubContext;
            }

            // GET: /CustService/CustServices/Index
            public IActionResult Index()
            {
                return View();
            }

            // POST: /CustService/CustServices/SendMessage
            [HttpPost]
            public async Task<IActionResult> SendMessage(string user, string message)
            {
                if (string.IsNullOrEmpty(user) || string.IsNullOrEmpty(message))
                {
                    return BadRequest("User and message cannot be empty");
                }

                // For demonstration purposes, assuming customer service name is "Customer Service"
                user = "Customer Service";

                await _chatHubContext.Clients.All.SendAsync("ReceiveMessage", user, message);
                return Ok();
            }
        }
    }

}