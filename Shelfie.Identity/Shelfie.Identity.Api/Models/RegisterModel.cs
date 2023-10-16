using System.ComponentModel.DataAnnotations;

namespace Shelfie.Identity.Api.Models;

public class RegisterModel
{
    public string? Username { get; set; }

    public string? Email { get; set; }

    public string? Password { get; set; }
}
