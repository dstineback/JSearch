using JSearch.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.IO;
using System.Text;
using JSearch.Services;
using Microsoft.Extensions.Configuration;

namespace JSearch.Pages
{
    public class SubmissionsModel : PageModel
    {
        private readonly IConfiguration _configuration;

        [BindProperty]
        public Submissions SubmissionsData { get; set; }

        public SubmissionsModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public void OnGet()
        {

            

        }


        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            string connectionString = _configuration.GetConnectionString("Connection");
            // Set default values if needed
            Submissions submissions = new Submissions();
            SubmissionsData.FirstContact = DateTime.Now;
            SubmissionsData.FollowUpContact = DateTime.Now.AddDays(7);

            Submissions.Add(SubmissionsData, connectionString);


            return RedirectToPage("/Dashboard"); // Redirect to a different page after successful submission
        }

        
    }
    
}
