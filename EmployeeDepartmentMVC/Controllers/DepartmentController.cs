using AutoMapper;
using EmployeeDepartment.BLL.Models.Departments;
using EmployeeDepartment.BLL.Services.Department;
using EmployeeDepartmentMVC.Models.Departments;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeDepartmentMVC.Controllers
{
    [Authorize]

    //inheritance :DepartmentController is a controller
    //compoistion: DepartmentController has a dependency on IDepartmentService
    public class DepartmentController : Controller
    {
        private readonly IDepartmentService _departmentService;
        private readonly ILogger<DepartmentController> _logger;
        private readonly IWebHostEnvironment _environment;
        private readonly IMapper _mapper;
        #region Service
        public DepartmentController(IDepartmentService departmentService,
            ILogger<DepartmentController> logger,IWebHostEnvironment environment,IMapper mapper)
        {
            _departmentService = departmentService;
            _logger = logger;
            _environment = environment;
            _mapper = mapper;
        }
        #endregion
        #region Index
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            ////1 viewData
            ViewData["Message"] = "Welcome to Department Page";
            ////2 viewBag transfer data from controller to view
            ViewBag.Message = "Welcome to Department Page";
            var departments = await _departmentService.GetAllDepartmentsAsync();
            return View(departments);
        }
        #endregion
        #region Create
        #region Get
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }
        #endregion
        #region post
        [HttpPost]
        public async Task<IActionResult> Create(CreatedDepartmentDto departmentDto)
        {
            if (!ModelState.IsValid) 
            {
                return View(departmentDto);
            }
            var message=string.Empty;
            try
            {
                var result = await _departmentService.CreateDepartmentAsync(departmentDto);
                if (result > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    message = "sorry the department has not been created";
                    ModelState.AddModelError(string.Empty, message);
                    return View(departmentDto);
                }
            }
            catch (Exception ex)
            {
                //1- log the exception
                _logger.LogError(ex, ex.Message);
                //2- set frindly message
                if(_environment.IsDevelopment())
                {
                    message = ex.Message;
                    return View(departmentDto);
                }
                else
                {
                    message = "sorry, we are facing a problem please try again later";
                    return View("Error", message);
                }
               
            }

           
        }
        #endregion
        #endregion
        #region Details
        [HttpGet] //Department/details/id
        public async Task<IActionResult> Details(int? id)
        {
            if (id is null)
            {
                return BadRequest();//  400
            }
            var department = await _departmentService.GetDepartmentByIdAsync(id.Value);
            if(department is null)
            {
                return NotFound();//404
            }
            return View(department);
        }
        #endregion
        #region Edit
        #region Get
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null)
            {
                return BadRequest();//  400
            }
            var department = await _departmentService.GetDepartmentByIdAsync(id.Value);
            if (department is null)
            {
                return NotFound();//404
            }
            var departmentEdit = _mapper.Map<DepartmentDetailsToReturnDto
                ,DepartmentEditViewModel>(department);
            return View(departmentEdit);
        }
        #endregion
        #region Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(DepartmentEditViewModel departmentEdit)
        {
            if (!ModelState.IsValid)
            {
                return View(departmentEdit);
            }
            var message = string.Empty;
            try
            {
                var UpdatedDepartment = _mapper.Map<UpdateDepartmentDto>(departmentEdit);
                var result = await _departmentService.UpdateDepartmentAsync(UpdatedDepartment)>0;
                if (result)
                {
                    return RedirectToAction(nameof(Index));
                }
                else  message = "sorry the department has not been updated"; 
            }
            catch (Exception ex)
            {
                //1- log the exception
                _logger.LogError(ex, ex.Message);
                //2- set frindly message
                if (_environment.IsDevelopment())
                {
                    message = ex.Message;
                    return View(departmentEdit);
                }
                else
                {
                    message = "sorry, we are facing a problem please try again later";
                    return View("Error", message);
                }
            }
            ModelState.AddModelError(string.Empty, message);
            return View(departmentEdit);
        }
        #endregion
        #endregion
        #region Delete
        #region Get
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null)
            {
                return BadRequest();
            }
            var department = await  _departmentService.GetDepartmentByIdAsync(id.Value);
            if (department is null)
            {
                return NotFound();
            }
            return View(department);
        }
        #endregion
        #region Post
        [HttpPost]
        public  async Task<IActionResult> Delete(int id) 
        {
            var massage=string.Empty;
            try
            {
                var delete = await _departmentService.DeletedDepartmentAsync(id);
                if (delete)
                {

                    return RedirectToAction(nameof(Index));

                }
                massage = "Sorry, an error ocurred during deleting the department";
            }
            catch (Exception ex)
            {

                //1-log exception
                _logger.LogError(ex, ex.Message);
                //2-set massage
                massage = _environment.IsDevelopment() ? ex.Message : "Sorry, an error ocurred during deleting";

            }
            return RedirectToAction(nameof(Index));
        }
        #endregion
        #endregion
    }
}
