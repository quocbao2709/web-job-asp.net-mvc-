using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace Job_Web.Models;
public class Employer
{
    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(100)]
    public string CompanyName { get; set; } // Tên công ty

    public string CompanyDescription { get; set; } // Mô tả về công ty

    public bool IsApproved { get; set; } = false; // Xác định nhà tuyển dụng đã được admin duyệt hay chưa

    public List<Job> Jobs { get; set; } = new List<Job>(); // Danh sách công việc của nhà tuyển dụng
}