using System.ComponentModel.DataAnnotations;

namespace Shelfie.Identity.Api.Models;

public class LoginModel
{
    public string? Username { get; set; }

    public string? Password { get; set; }
}
