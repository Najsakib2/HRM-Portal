using AutoMapper;
using HRM.Applicatin;
using HRM.Domain;

namespace HRM.Application.Mappings
{
    internal class DepartmentProfile: Profile
    {
        public DepartmentProfile()
        {
            CreateMap<Department, Department>();
        }
    }
}
