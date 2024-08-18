using System.ComponentModel.DataAnnotations;

namespace WebApiAuthenticate.Models;

public class ChangeEmailRequest
{
    [Required]
    public Guid Id { get; set; }
    public string NewEmail { get; set; }
}