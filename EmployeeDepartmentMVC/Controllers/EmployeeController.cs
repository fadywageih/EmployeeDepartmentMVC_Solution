using EmployeeDepartment.BLL.Models.Employees;
using EmployeeDepartment.BLL.Services.Department;
using EmployeeDepartment.BLL.Services.Employee;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeDepartmentMVC.Controllers
{
    [Authorize]

    public class EmployeeController : Controller
    {
        #region Service

        private readonly IEmployeeService _employeeService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ILogger<EmployeeController> _logger;

        public EmployeeController(
            IEmployeeService employeeService,
            IWebHostEnvironment webHostEnvironment,
            ILogger<EmployeeController> logger

            ) // ASK CLR for Creating Object from EmployeeService Implicitly
        {
            _logger = logger;
            _webHostEnvironment = webHostEnvironment;
            _employeeService = employeeService;
        }

        #endregion
        #region Index
        [HttpGet]
        public async Task<IActionResult> Index(string search)
        {
            var employees = await _employeeService.GetAllEmployeesAsync(search);

            return View(employees);
        }
        #endregion
        #region Create
        #region Get
        [HttpGet]//Employee/Create
        public async Task<IActionResult> Create([FromServices]IDepartmentService _departmentService)
        {
            ViewData["Departments"] = await _departmentService.GetAllDepartmentsAsync();
            return View();
        }
        #endregion
        #region Post
        [HttpPost]
        [ValidateAntiForgeryToken] // To Prevent CSRF Attacks
        public async Task<IActionResult> Create(CreatedEmployeeDto employeeDto) 
        {
            if(!ModelState.IsValid)
            {
                return View(employeeDto);
            }
            var message = string.Empty;
            try
            {
                var result = await _employeeService.CreateEmployeeAsync(employeeDto);
                if (result > 0)
                {
                    return RedirectToAction(nameof(Index));

                }
                else
                {
                    message = "Employee not been Created";
                    ModelState.AddModelError(string.Empty, message);
                    return View(employeeDto);
                }
            }
            catch (Exception ex)
            {

                _logger.LogError(ex, ex.Message);
                message = _webHostEnvironment.IsDevelopment() ? ex.Message : "Sorry An Error Here";
            }
            ModelState.AddModelError(string.Empty, message);
            return View(employeeDto);
        }
        #endregion
        #endregion
        #region Details
        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if(id is null)
            {
                return BadRequest();

            }
            var employee = await _employeeService.GetEmployeeByIdAsync(id.Value);
            if(employee == null)
            {
                return NotFound();

            }
            return View(employee);
        }
        #endregion
        #region Edit
        #region Get
        [HttpGet]
        public async Task<IActionResult> Edit(int? id, [FromServices] IDepartmentService departmentService) 
        {
            if (id is null)
                return BadRequest();
            var employee = await _employeeService.GetEmployeeByIdAsync(id.Value);
            if(employee is null)
            {
                return NotFound();
                
            }
            ViewData["Departments"] = departmentService.GetAllDepartmentsAsync();
            return View(new UpdatedEmployeeDto()
            {
                Name=employee.Name,
                Address=employee.Address,
                Email=employee.Email,
                Age=employee.Age,
                Salary=employee.Salary,
                PhoneNumber= employee.PhoneNumber,
                IsActive=employee.IsActive,
                EmployeeType=employee.EmployeeType,
                Gender=employee.Gender,
                HiringDate=employee.HiringDate

            });
        }
        #endregion
        #region Post
        [HttpPost]
        [ValidateAntiForgeryToken] // To Prevent CSRF Attacks
        public async Task<IActionResult> Edit([FromRoute]int id, UpdatedEmployeeDto employeeDto)
        {
            if (!ModelState.IsValid)
                return View(employeeDto);
            var massage = string.Empty;
            try
            {
                var updated = await _employeeService.UpdateEmployeeAsync(employeeDto) > 0;
                if (updated)
                    return RedirectToAction(nameof(Index));
                massage = "Employee is not Updated";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                if (_webHostEnvironment.IsDevelopment())
                    massage = ex.Message;
                else massage = "the employee is not Created";
                
            }
            ModelState.AddModelError(string.Empty, massage);
            return View(employeeDto);

        }
        #endregion
        #endregion
        #region Delete
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var massage = string.Empty;
            try
            {
                var deleted = await _employeeService.DeleteEmployeeAsync(id);
                if (deleted)
                    return RedirectToAction(nameof(Index));
                massage = "An Error Occured During The Deleting of department";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                massage = _webHostEnvironment.IsDevelopment() ? ex.Message : "sorry an error ";
                
            }
            return RedirectToAction(nameof(Index));
;        }
        #endregion
    }
}
