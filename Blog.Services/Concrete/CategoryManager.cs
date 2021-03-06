using AutoMapper;
using Blog.Data.Abstract;
using Blog.Entities.Concrete;
using Blog.Entities.Dtos;
using Blog.Services.Abstract;
using Blog.Shared.Utilities.Results.Abstract;
using Blog.Shared.Utilities.Results.ComplexTypes;
using Blog.Shared.Utilities.Results.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Services.Concrete
{
    public class CategoryManager : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CategoryManager(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IDataResult<CategoryDto>> Add(CategoryAddDto categoryAddDto, string createdByName)
        {
            var category = _mapper.Map<Category>(categoryAddDto);
            category.CreatedByName = createdByName;
            category.ModifiedByName = createdByName;
            var addedCategory = await _unitOfWork.Categories.AddAsync(category);//.ContinueWith(async t =>await _unitOfWork.SaveAsync());
            await _unitOfWork.SaveAsync();
            return new DataResult<CategoryDto>(ResultStatus.Success, $"{categoryAddDto.Name} kategorisi başarıyla eklendi",new CategoryDto {
                Category = addedCategory,
                Message = $"{categoryAddDto.Name} kategorisi başarıyla eklendi",
                ResultStatus = ResultStatus.Success
            });
        }

        public async Task<IDataResult<CategoryDto>> Delete(int categoryId, string modifiedByName)
        {
            var category = await _unitOfWork.Categories.GetAsync(c => c.Id == categoryId);
            if (category != null)
            {
                category.isDeleted = true;
                category.ModifiedByName = modifiedByName;
                category.ModifiedDate = DateTime.Now;
                var deletedCategory = await _unitOfWork.Categories.UpdateAsync(category);
                await _unitOfWork.SaveAsync();
                return new DataResult<CategoryDto>(ResultStatus.Success, $"{deletedCategory.Name} kategorisi başarıyla silindi", new CategoryDto
                {
                    Category = deletedCategory,
                    Message = $"{deletedCategory.Name} kategorisi başarıyla silindi",
                    ResultStatus = ResultStatus.Success
                });
            }
            return new DataResult<CategoryDto>(ResultStatus.Error, "Kategori bulunamadı", new CategoryDto
            {
                Category = null,
                Message = "Kategori bulunamadı",
                ResultStatus = ResultStatus.Error
            });
        }

        public async Task<IDataResult<CategoryDto>> Get(int categoryId)
        {
            var category = await _unitOfWork.Categories.GetAsync(c => c.Id == categoryId, c => c.Articles);
            if (category != null)
            {
                return new DataResult<CategoryDto>(ResultStatus.Success, new CategoryDto
                {
                    Category = category,
                    ResultStatus = ResultStatus.Success
                });
            }
            return new DataResult<CategoryDto>(ResultStatus.Error, "Kategori çağırılırken hata oluştu", new CategoryDto {
                Category = null,
                Message = "Kategori çağırılırken hata oluştu",
                ResultStatus = ResultStatus.Error
            });
        }

        public async Task<IDataResult<CategoryListDto>> GetAll()
        {
            var categories = await _unitOfWork.Categories.GetAllAsync(null, c => c.Articles);
            if (categories.Count > -1)
            {
                return new DataResult<CategoryListDto>(ResultStatus.Success, new CategoryListDto
                {
                    Categories = categories,
                    ResultStatus = ResultStatus.Success
                });
            }
            return new DataResult<CategoryListDto>(ResultStatus.Error, "Kategoriler çağırılırken hata oluştu", new CategoryListDto {
                Categories = null,
                ResultStatus = ResultStatus.Error,
                Message = "Kategoriler çağırılırken hata oluştu"
            });
        }

        public async Task<IDataResult<CategoryListDto>> GetAllByNonDeleted()
        {
            var categories = await _unitOfWork.Categories.GetAllAsync(c => !c.isDeleted, c => c.Articles);
            if (categories.Count > -1)
            {
                return new DataResult<CategoryListDto>(ResultStatus.Success, new CategoryListDto
                {
                    Categories = categories,
                    ResultStatus = ResultStatus.Success
                });
            }
            return new DataResult<CategoryListDto>(ResultStatus.Error, "Kategoriler çağırılırken hata oluştu", new CategoryListDto
            {
                Categories = null,
                ResultStatus = ResultStatus.Error,
                Message = "Kategoriler çağırılırken hata oluştu"
            });
        }

        public async Task<IDataResult<CategoryListDto>> GetAllByNonDeletedAndActive()
        {
            var categories = await _unitOfWork.Categories.GetAllAsync(c => !c.isDeleted && c.isActive, c => c.Articles);
            if (categories.Count > -1)
            {
                return new DataResult<CategoryListDto>(ResultStatus.Success, new CategoryListDto
                {
                    Categories = categories,
                    ResultStatus = ResultStatus.Success
                });
            }
            return new DataResult<CategoryListDto>(ResultStatus.Error, "Kategoriler çağırılırken hata oluştu", null);
        }

        public async Task<IDataResult<CategoryUpdateDto>> GetCategoryUpdateDto(int categoryId)
        {
            var result = await _unitOfWork.Categories.AnyAsync(c=>c.Id == categoryId);
            if (result)
            {
                var category = await _unitOfWork.Categories.GetAsync(c => c.Id == categoryId);
                var categoryUpdateDto = _mapper.Map<CategoryUpdateDto>(category);
                return new DataResult<CategoryUpdateDto>(ResultStatus.Success, categoryUpdateDto);
            }
            else
            {
                return new DataResult<CategoryUpdateDto>(ResultStatus.Error, "Kategori bulunamadı", null);
            }
        }

        public async Task<IResult> HardDelete(int categoryId)
        {
            var category = await _unitOfWork.Categories.GetAsync(c => c.Id == categoryId);
            if (category != null)
            {
                await _unitOfWork.Categories.Delete(category);
                await _unitOfWork.SaveAsync();
                return new Result(ResultStatus.Success, $"{category.Name} kategorisi başarıyla veritabanından silindi");
            }
            return new Result(ResultStatus.Error, "Kategori bulunamadı");
        }

        public async Task<IDataResult<CategoryDto>> Update(CategoryUpdateDto categoryUpdateDto, string modifiedByName)
        {
            var oldcategory = await _unitOfWork.Categories.GetAsync(c => c.Id == categoryUpdateDto.Id);
            var category = _mapper.Map<CategoryUpdateDto, Category>(categoryUpdateDto,oldcategory);
            category.ModifiedByName = modifiedByName;
            var updatedCategory = await _unitOfWork.Categories.UpdateAsync(category);
            await _unitOfWork.SaveAsync();
            return new DataResult<CategoryDto>(ResultStatus.Success, $"{categoryUpdateDto.Name} kategorisi başarıyla güncellendi",new CategoryDto {
                Category = updatedCategory,
                Message = $"{categoryUpdateDto.Name} kategorisi başarıyla güncellendi",
                ResultStatus = ResultStatus.Success
            });
        }
    }
}
