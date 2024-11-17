using System.ComponentModel.DataAnnotations;
namespace Job_Web.Models;
public class User
{
    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(50)]
    public string Username { get; set; }
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    
    [Required]
    public string Password { get; set; }

    [Required]
    public string Role { get; set; } // Vai trò của người dùng: "Admin", "Employer", "Applicant"
}