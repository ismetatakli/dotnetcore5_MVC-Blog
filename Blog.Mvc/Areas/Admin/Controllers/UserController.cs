using Blog.Entities.Concrete;
using Blog.Entities.Dtos;
using Blog.Shared.Utilities.Extensions;
using Blog.Shared.Utilities.Results.ComplexTypes;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Blog.Mvc.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly IWebHostEnvironment _env;
        public UserController(UserManager<User> userManager, IWebHostEnvironment env)
        {
            _userManager = userManager;
            _env = env;
        }
        public async Task<IActionResult> Index()
        {
            var users = await _userManager.Users.ToListAsync();
            return View(new UserListDto { Users = users, ResultStatus = ResultStatus.Success });
        }
        [HttpGet]
        public IActionResult Add()
        {
            return PartialView("_UserAddPartial");
        }
        public async Task<string> ImageUpload(UserAddDto userAddDto)
        {
            string wwwroot = _env.WebRootPath;
            string filename = Path.GetFileNameWithoutExtension(userAddDto.PictureFile.FileName);
            string fileExtension = Path.GetExtension(userAddDto.PictureFile.FileName);
            DateTime date = DateTime.Now;
            string fileNameCreate = $"{userAddDto.UserName}_{date.FullDateAndTimeStringWithUnderScore()}{fileExtension}";
            var path = Path.Combine(wwwroot, "img", fileNameCreate);
            using (var stream = new FileStream(path, FileMode.Create))
            {
                await userAddDto.PictureFile.CopyToAsync(stream);
            }
            return fileNameCreate;
        }
    }
}
