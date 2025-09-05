using BootCamp1_Api.Dtos;
using BootCamp1_Api.ViewModels;
using BootCamp1_AspMVC.Dtos;
using BootCamp1_AspMVC.Models;
using BootCamp1_AspMVC.Repository.Base;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BootCamp1_Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeesController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public EmployeesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: api/employees
        [HttpGet]
        public IActionResult GetAll()
        {
            var list = _unitOfWork.Employees.FindAllemployee(); // بافتراض أنها ترجع IEnumerable<Employee>
            var data = list.Select(e => new EmployeeDto
            {
                Id = e.Id,
                FirstName = e.FirstName,
                LastName = e.LastName,
                Email = e.Email,
                Phone = e.Phone,
                Address = e.Address,
                Username = e.Username,
                Islock = e.Islock,
                DepartmentId = e.DepartmentId,
                DepartmentName = e.Department?.Name
            });

            return Ok(new ApiResponse<object>
            {
                Msg = "✅ تم استرجاع الموظفين بنجاح.",
                IsSuccess = true,
                Data = data
            });
        }

        // GET: api/employees/5
        [HttpGet("{id:int}")]
        public IActionResult GetById(int id)
        {
            var e = _unitOfWork.Employees.FindById(id);
            if (e == null)
            {
                return NotFound(new ApiResponse<object>
                {
                    Msg = "❌ لم يتم العثور على الموظف.",
                    IsSuccess = false,
                    Data = new { }
                });
            }

            var dto = new EmployeeDto
            {
                Id = e.Id,
                FirstName = e.FirstName,
                LastName = e.LastName,
                Email = e.Email,
                Phone = e.Phone,
                Address = e.Address,
                Username = e.Username,
                Islock = e.Islock,
                DepartmentId = e.DepartmentId,
                DepartmentName = e.Department?.Name
            };

            return Ok(new ApiResponse<EmployeeDto>
            {
                Msg = "✅ تم استرجاع الموظف بنجاح.",
                IsSuccess = true,
                Data = dto
            });
        }

        // POST: api/employees
        [HttpPost]
        public IActionResult Create([FromBody] EmployeeCreateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ApiResponse<object>
                {
                    Msg = "⚠️ بيانات غير صالحة.",
                    IsSuccess = false,
                    Data = ModelState
                });

            // (اختياري) تحقق من تكرار Username أو Email إن توفر لديك Exists
            // if (_unitOfWork.Employees.Exists(u => u.Username == dto.Username)) ...

            var entity = new Employee
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                Phone = dto.Phone,
                Address = dto.Address,
                Username = dto.Username,
                Password = dto.Password, // يفضّل التخزين بشكل مُعمّى (هاش) لاحقًا
                Islock = dto.Islock,
                DepartmentId = dto.DepartmentId
            };

            _unitOfWork.Employees.Insert(entity);
            // _unitOfWork.Save();

            return Ok(new ApiResponse<object>
            {
                Msg = "✅ تم إضافة الموظف بنجاح.",
                IsSuccess = true,
                Data = new { id = entity.Id }
            });
        }

        // PUT: api/employees/5
        [HttpPut("{id:int}")]
        public IActionResult Update(int id, [FromBody] EmployeeUpdateDto dto)
        {
            if (id != dto.Id)
                return BadRequest(new ApiResponse<object>
                {
                    Msg = "⚠️ المعرّف في العنوان لا يطابق المعرّف في البيانات.",
                    IsSuccess = false,
                    Data = new { }
                });

            if (!ModelState.IsValid)
                return BadRequest(new ApiResponse<object>
                {
                    Msg = "⚠️ بيانات غير صالحة.",
                    IsSuccess = false,
                    Data = ModelState
                });

            var e = _unitOfWork.Employees.FindById(id);
            if (e == null)
                return NotFound(new ApiResponse<object>
                {
                    Msg = "❌ لم يتم العثور على الموظف.",
                    IsSuccess = false,
                    Data = new { }
                });

            e.FirstName = dto.FirstName;
            e.LastName = dto.LastName;
            e.Email = dto.Email;
            e.Phone = dto.Phone;
            e.Address = dto.Address;
            e.Username = dto.Username;
            e.Islock = dto.Islock;
            e.DepartmentId = dto.DepartmentId;

            _unitOfWork.Employees.Update(e);
            // _unitOfWork.Save();

            return Ok(new ApiResponse<object>
            {
                Msg = "✅ تم تحديث بيانات الموظف بنجاح.",
                IsSuccess = true,
                Data = new { id = e.Id }
            });
        }



        // PATCH: api/employees/5
        [HttpPatch("{id:int}")]
        public IActionResult Patch(int id, [FromBody] EmployeePatchDto dto)
        {
            var emp = _unitOfWork.Employees.FindById(id);
            if (emp == null)
            {
                return NotFound(new ApiResponse<object>
                {
                    Msg = "❌ لم يتم العثور على الموظف.",
                    IsSuccess = false,
                    Data = new { }
                });
            }

            // تحديث الحقول اللي مش null فقط
            if (dto.FirstName != null) emp.FirstName = dto.FirstName;
            if (dto.LastName != null) emp.LastName = dto.LastName;
            if (dto.Email != null) emp.Email = dto.Email;
            if (dto.Phone != null) emp.Phone = dto.Phone;
            if (dto.Address != null) emp.Address = dto.Address;
            if (dto.Username != null) emp.Username = dto.Username;
            if (dto.Islock.HasValue) emp.Islock = dto.Islock.Value;
            if (dto.DepartmentId.HasValue) emp.DepartmentId = dto.DepartmentId.Value;

            _unitOfWork.Employees.Update(emp);
            // _unitOfWork.Save();

            return Ok(new ApiResponse<object>
            {
                Msg = "✅ تم تحديث الموظف (جزئيًا) بنجاح.",
                IsSuccess = true,
                Data = new { id = emp.Id }
            });
        }









        // DELETE: api/employees/5
        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            var e = _unitOfWork.Employees.FindById(id);
            if (e == null)
            {
                return NotFound(new ApiResponse<object>
                {
                    Msg = "❌ لم يتم العثور على الموظف المراد حذفه.",
                    IsSuccess = false,
                    Data = new { }
                });
            }

            _unitOfWork.Employees.Delete(e);
            // _unitOfWork.Save();

            return Ok(new ApiResponse<object>
            {
                Msg = "🗑️ تم حذف الموظف بنجاح.",
                IsSuccess = true,
                Data = new { id = e.Id }
            });
        }

        // POST: api/employees/login
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequestDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ApiResponse<object>
                {
                    Msg = "⚠️ بيانات تسجيل الدخول غير صالحة.",
                    IsSuccess = false,
                    Data = ModelState
                });

            var emp = _unitOfWork.Employees.LoginByUser(dto.Username, dto.Password);
            if (emp == null)
            {
                return Unauthorized(new ApiResponse<object>
                {
                    Msg = "❌ اسم المستخدم أو كلمة المرور غير صحيحة.",
                    IsSuccess = false,
                    Data = new { }
                });
            }

            if (emp.Islock)
            {
                return StatusCode(403, new ApiResponse<object>
                {
                    Msg = "⛔ المستخدم محظور من قبل المدير.",
                    IsSuccess = false,
                    Data = new { }
                });
            }

            // في API لا نستخدم Session، فقط نُرجع نجاح + بيانات أساسية (بدون Password)
            var dtoEmp = new EmployeeDto
            {
                Id = emp.Id,
                FirstName = emp.FirstName,
                LastName = emp.LastName,
                Email = emp.Email,
                Phone = emp.Phone,
                Address = emp.Address,
                Username = emp.Username,
                Islock = emp.Islock,
                DepartmentId = emp.DepartmentId,
                DepartmentName = emp.Department?.Name
            };

            return Ok(new ApiResponse<EmployeeDto>
            {
                Msg = "✅ تم تسجيل الدخول بنجاح.",
                IsSuccess = true,
                Data = dtoEmp
            });
        }

        // GET: api/employees/departments (لملء القوائم في الفرونت)
        [HttpGet("departments")]
        public IActionResult GetDepartments()
        {
            var depts = _unitOfWork.Departments.FindAll();
            var data = depts.Select(d => new DepartmentOptionDto { Id = d.Id, Name = d.Name });

            return Ok(new ApiResponse<object>
            {
                Msg = "✅ تم استرجاع الأقسام بنجاح.",
                IsSuccess = true,
                Data = data
            });
        }
    }
}
