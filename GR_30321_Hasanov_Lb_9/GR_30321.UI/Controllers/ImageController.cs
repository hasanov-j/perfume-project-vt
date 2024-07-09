using GR_30321.UI.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Identity.Client;
using System.Security.Claims;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace GR_30321.UI.Controllers
{
    public class ImageController : Controller
    {
        private readonly UserManager<AppUser>  _userManager;
        public ImageController(UserManager<AppUser> userManager) 
        {
            _userManager=userManager;
        }
        public async Task<IActionResult> GetAvatar()
        {
            var email = User.FindFirst(ClaimTypes.Email)!.Value;
            var user = await _userManager.FindByEmailAsync(email);
            var imagePath = Path.Combine("images", "default-avatar.png");

            if (user == null) return NotFound();

            return user.Avatar != null ? File(user.Avatar, user.MimeType) : File(imagePath, "image/png");
        }
    }
}
