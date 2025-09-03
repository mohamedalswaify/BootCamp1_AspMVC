using BootCamp1_Api.Dtos;
using BootCamp1_Api.ViewModels;
using BootCamp1_AspMVC.Models;
using BootCamp1_AspMVC.Repository.Base;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BootCamp1_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {



        private readonly IUnitOfWork _unitOfWork;
        public CategoriesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }



        [HttpGet("GetAllCategories")]
        public IActionResult Index()
        {
            //var categories = _context.Categories.ToList();
            var categories = _unitOfWork.Categories.FindAll();
            return Ok(categories);

        }


        [HttpGet("GetCategoryById/{id}")]
        public IActionResult GetById(int id) {
            var category = _unitOfWork.Categories.FindById(id);
            if (category == null) return NotFound(new {msg = "لم يتم العثور علي الفئات.",isRead =false});

            var categoryDto = new CategoryDto
            {
                Id = category.Id,
                Name = category.Name,
                Description = category.Description
            };

            return Ok(categoryDto);

        }


        [HttpGet("GetCategoryByQueryId")]
        public IActionResult GetByqQeryId([FromQuery] int id)
        {
            var category = _unitOfWork.Categories.FindById(id);
        
            if (category == null) return NotFound(new { msg = "لم يتم العثور علي الفئات.", isRead = false, data =new {} });

            var categoryDto = new CategoryDto
            {
                Id = category.Id,
                Name = category.Name,
                Description = category.Description
            };

            return Ok(new {msg="تم استرجاع الفئات بنجاح.",isRead=true ,data = categoryDto });

        }


        [HttpGet("GetCategoryByQueryId_v2")]
        public IActionResult GetByqQeryId_v2([FromQuery] int id)
        {
            var category = _unitOfWork.Categories.FindById(id);
            if (category == null) return NotFound(new ApiResponse<object> {
                Msg = "❌ لم يتم العثور على الفئة.",
                IsSuccess = false,
                Data = new { }
            });

            var categoryDto = new CategoryDto
            {
                Id = category.Id,
                Name = category.Name,
                Description = category.Description
            };


            return Ok(new ApiResponse<CategoryDto>
            {
                Msg = "✅ تم استرجاع الفئة بنجاح.",
                IsSuccess = true,
                Data = categoryDto
            });

        }





        [HttpPost]
        public IActionResult Create(CategoryCreateDto category)
        {

            if (!ModelState.IsValid)
              return BadRequest(ModelState);
            
      
            var cate = new Category
            {
                Name = category.Name,
                Description = category.Description
            };

            _unitOfWork.Categories.Insert(cate);

            return Ok(new { message = "✅ تم إضافة الفئة بنجاح" });
        }





        [HttpPut("{id}")]
        public IActionResult Update(int id ,CategoryUpdateDto dto) 
        {
        if(id != dto.Id)
                return BadRequest(new { message = "❌ المعرف غير متطابق" });

        if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var cate = _unitOfWork.Categories.FindById(id);
            if (cate == null)
                return NotFound(new { message = "❌ لم يتم العثور على الفئة" });
            cate.Name = dto.Name;
            cate.Description = dto.Description;
            _unitOfWork.Categories.Update(cate);
            return Ok(new { message = "✅ تم تحديث الفئة بنجاح" });

        }




        [HttpPatch("{id}")]
        public IActionResult Patch(int id, CategoryPatchDto dto)
        {
          
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var cate = _unitOfWork.Categories.FindById(id);
            if (cate == null)
                return NotFound(new { message = "❌ لم يتم العثور على الفئة" });



           
            
            
       
           if(dto.Name !=null) cate.Name = dto.Name;
            if (dto.Description != null) cate.Description = dto.Description;
            _unitOfWork.Categories.Update(cate);
            return Ok(new { message = "✅ تم تحديث الفئة بنجاح" });

        }




        [HttpDelete("{id}")]
        public IActionResult Delete(int id) {

            var cate = _unitOfWork.Categories.FindById(id);
            if (cate == null)
                return NotFound(new { message = "❌ لم يتم العثور على الفئة" });

            _unitOfWork.Categories.Delete(cate);
            return Ok(new { message = "✅ تم حذف الفئة بنجاح" });



        }





    }
}
