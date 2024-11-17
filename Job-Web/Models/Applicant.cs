using System;
using System.ComponentModel.DataAnnotations;
namespace Job_Web.Models;
public class Applicant
{
    [Key]
    public int Id { get; set; }

    [Required]
    public int JobId { get; set; } // Khóa ngoại đến công việc ứng tuyển

    [Required]
    public int UserId { get; set; } // Khóa ngoại đến người dùng ứng viên

    public DateTime AppliedDate { get; set; } = DateTime.Now; // Ngày ứng tuyển

    public Job Job { get; set; } // Quan hệ với công việc
    public User ApplicantUser { get; set; } // Quan hệ với người dùng ứng viên
}