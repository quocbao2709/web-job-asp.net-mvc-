using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Job_Web.Models;

public class Job
{
    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(150)]
    public string Title { get; set; } // Tiêu đề công việc

    [Required]
    public string Description { get; set; } // Mô tả công việc

    [Required]
    [Column(TypeName = "decimal(18,2)")]
    public decimal Salary { get; set; } // Mức lương

    public DateTime PostedDate { get; set; } = DateTime.Now; // Ngày đăng

    public bool IsApproved { get; set; } = false; // Xác định công việc đã được duyệt bởi admin hay chưa

    [Required]
    public int EmployerId { get; set; } // Khóa ngoại liên kết đến nhà tuyển dụng

    public Employer Employer { get; set; } // Quan hệ với nhà tuyển dụng
}
