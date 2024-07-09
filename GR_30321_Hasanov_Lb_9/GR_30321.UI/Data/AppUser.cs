using Microsoft.AspNetCore.Identity;

namespace GR_30321.UI.Data
{
    public class AppUser : IdentityUser
    {
        public byte[]? Avatar { get; set; } = null;
        public string MimeType { get; set; } = string.Empty;

    }
}