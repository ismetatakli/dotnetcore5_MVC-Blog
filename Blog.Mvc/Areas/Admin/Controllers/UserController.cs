using AutoMapper;
using Blog.Entities.Concrete;
using Blog.Entities.Dtos;
using Blog.Mvc.Areas.Admin.Models;
using Blog.Shared.Utilities.Extensions;
using Blog.Shared.Utilities.Results.ComplexTypes;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace Blog.Mvc.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly IWebHostEnvironment _env;
        private readonly IMapper _mapper;
        public UserController(UserManager<User> userManager, IWebHostEnvironment env, IMapper mapper)
        {
            _userManager = userManager;
            _env = env;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index()
        {
            var users = await _userManager.Users.ToListAsync();
            return View(new UserListDto { Users = users, ResultStatus = ResultStatus.Success });
        }
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userManager.Users.ToListAsync();
            var userListDto = JsonSerializer.Serialize(
                new UserListDto
                {
                    Users = users,
                    ResultStatus = ResultStatus.Success
                }, new JsonSerializerOptions
                {
                    ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve
                }
            );
            return Json(userListDto);
        }
        [HttpGet]
        public IActionResult Add()
        {
            return PartialView("_UserAddPartial");
        }
        public async Task<JsonResult> Delete(int userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            var result = await _userManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                var deletedUser = JsonSerializer.Serialize(new UserDto
                {
                    ResultStatus = ResultStatus.Success,
                    Message = $"{user.UserName} adlı kullanıcı başarıyla silindi",
                    User = user
                });
                return Json(deletedUser);
            }
            else
            {
                string errMessages = String.Empty;
                foreach (var err in result.Errors)
                {
                    errMessages = $"*{err.Description}\n";
                }
                var deletedUserErrModel = JsonSerializer.Serialize(new UserDto
                {
                    ResultStatus = ResultStatus.Error,
                    Message = $"{user.UserName} adlı kullanıcı silinirken bir hata oluştu. \n {errMessages}",
                    User = user
                });
                return Json(deletedUserErrModel);
            }

        }
        [HttpPost]
        public async Task<IActionResult> Add(UserAddDto userAddDto)
        {
            if (ModelState.IsValid)
            {
                userAddDto.Picture = await ImageUpload(userAddDto);
                var user = _mapper.Map<User>(userAddDto);
                var result = await _userManager.CreateAsync(user, userAddDto.Password);
                if (result.Succeeded)
                {
                    var userAddAjaxModel = JsonSerializer.Serialize(new UserAddAjaxViewModel
                    {
                        UserDto = new UserDto
                        {
                            ResultStatus = ResultStatus.Success,
                            Message = $"{user.UserName} kullanıcısı başarıyla eklendi.",
                            User = user
                        },
                        UserAddPartial = await this.RenderViewToStringAsync("_UserAddPartial", userAddDto)
                    });
                    return Json(userAddAjaxModel);
                }
                else
                {
                    foreach (var err in result.Errors)
                    {
                        ModelState.AddModelError("", err.Description);
                    }
                    var userAddAjaxModelError = JsonSerializer.Serialize(new UserAddAjaxViewModel
                    {
                        UserAddDto = userAddDto,
                        UserAddPartial = await this.RenderViewToStringAsync("_UserAddPartial", userAddDto)
                    });
                    return Json(userAddAjaxModelError);
                }
            }
            var userAddAjaxModelStateError = JsonSerializer.Serialize(new UserAddAjaxViewModel
            {
                UserAddDto = userAddDto,
                UserAddPartial = await this.RenderViewToStringAsync("_UserAddPartial", userAddDto)
            });
            return Json(userAddAjaxModelStateError);
        }
        public async Task<PartialViewResult> Update(int userId)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == userId);

            var userUpdateDto = _mapper.Map<UserUpdateDto>(user);
            return PartialView("_UserUpdatePartial", userUpdateDto);

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
