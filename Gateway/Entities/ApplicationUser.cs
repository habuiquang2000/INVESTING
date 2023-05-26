using Microsoft.AspNetCore.Identity;

namespace CodeMega.Entities;
public class ApplicationUser : IdentityUser<Guid>
{
    public string? Picture { get; set; }
    public string? DateOfBirth { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public string? UpdatedBy { get; set; }
    public string? FullName { get; set; }
    public DateTime CreatedAt { get; set; }
    public string? CreatedBy { get; set; }
    // Thêm các trường mong muốn khác ở đây
}