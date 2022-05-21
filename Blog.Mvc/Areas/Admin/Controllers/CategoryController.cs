﻿using Blog.Entities.Dtos;
using Blog.Mvc.Areas.Admin.Models;
using Blog.Services.Abstract;
using Blog.Shared.Utilities.Extensions;
using Blog.Shared.Utilities.Results.ComplexTypes;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Blog.Mvc.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        public async Task<IActionResult> Index()
        {
            var result = await _categoryService.GetAllByNonDeleted();
            return View(result.Data);
        }
        [HttpGet]
        public IActionResult Add()
        {
            return PartialView("_CategoryAddPartial");
        }

        [HttpPost]
        public async Task<IActionResult> Add(CategoryAddDto categoryAddDto)
        {
            if (ModelState.IsValid)
            {
                var result = await _categoryService.Add(categoryAddDto,"İsmet Ataklı");
                if (result.ResultStatus == ResultStatus.Success)
                {
                    var categoryAddAjaxModel = JsonSerializer.Serialize(new CategoryAddAjaxViewModel {
                        CategoryDto=result.Data,
                        CategoryAddPartial = await this.RenderViewToStringAsync("_CategoryAddPartial",categoryAddDto),
                    });
                    return Json(categoryAddAjaxModel);
                }
                
            }
            var categoryAddAjaxErrorModel = JsonSerializer.Serialize(new CategoryAddAjaxViewModel
            {
                CategoryAddPartial = await this.RenderViewToStringAsync("_CategoryAddPartial", categoryAddDto),
            });
            return Json(categoryAddAjaxErrorModel);
        }

        public async Task<JsonResult> GetAllCategories()
        {
            var result = await _categoryService.GetAll();
            var categories = JsonSerializer.Serialize(result.Data,new JsonSerializerOptions {
                ReferenceHandler = ReferenceHandler.Preserve
            });
            return Json(categories);
        }

        [HttpPost]
        public async Task<JsonResult> Delete(int categoryId)
        {
            var result = await _categoryService.Delete(categoryId, "İsmet Ataklı");
            var deletedCategory = JsonSerializer.Serialize(result.Data);
            return Json(deletedCategory);
        }

    }
}
