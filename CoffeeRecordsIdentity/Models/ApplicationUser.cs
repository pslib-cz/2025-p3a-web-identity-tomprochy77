using Microsoft.AspNetCore.Identity;

namespace CoffeeRecordsIdentity.Models;

public class ApplicationUser : IdentityUser
{

    public string Name { get; set; } = string.Empty;
    public ICollection<CoffeeCup> Cups { get; set; } = new List<CoffeeCup>();
}
