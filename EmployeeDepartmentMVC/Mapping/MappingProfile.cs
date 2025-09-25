using AutoMapper;
using EmployeeDepartment.BLL.Models.Departments;
using EmployeeDepartmentMVC.Models.Departments;

namespace EmployeeDepartmentMVC.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            #region Department
            CreateMap<DepartmentEditViewModel, UpdateDepartmentDto>().ReverseMap();
            CreateMap<DepartmentDetailsToReturnDto, DepartmentEditViewModel>().ReverseMap();
            //CreateMap<DepartmentToReturnDto, DepartmentIndexViewModel>();
            #endregion
        }

    }
}
