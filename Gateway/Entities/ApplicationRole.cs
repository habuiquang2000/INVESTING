using Microsoft.AspNetCore.Identity;

namespace CodeMega.Entities;

public class ApplicationRole : IdentityRole<Guid>
{
    public string? Description { get; set; }
    // Thêm các trường mong muốn khác ở đây
}