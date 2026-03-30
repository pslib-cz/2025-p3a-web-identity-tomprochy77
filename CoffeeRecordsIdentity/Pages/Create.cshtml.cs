using CoffeeRecordsIdentity.Data;
using CoffeeRecordsIdentity.InputModels;
using CoffeeRecordsIdentity.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace CoffeeRecordsIdentity.Pages
{
    [Authorize]
    public class CreateModel(ILogger<CreateModel> logger, CoffeeRecordsIdentityContext context) : PageModel
    {
        private readonly ILogger<CreateModel> _logger = logger;
        private readonly CoffeeRecordsIdentityContext _context = context;

        [BindProperty]
        public CoffeeCupIM Input { get; set; } = new();

        public IActionResult OnGet()
        {
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            _logger.LogInformation("OnPostAsync called with MachineNo: {MachineNo} and UserName: {UserName}", Input.MachineNo, Input.UserName);
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            CoffeeCup cc = new CoffeeCup { MachineNo = Input.MachineNo, UserName = Input.UserName, Created = DateTime.Now, UserId = userId };
            _context.Cups.Add(cc);
            await _context.SaveChangesAsync();
            _logger.LogInformation("CoffeeCup created with Id: {CoffeeCupId}", cc.CoffeeCupId);

            return RedirectToPage("./Index");
        }
    }
}
