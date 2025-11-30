using AutoMapper;
using HRM.Domain;

namespace HRM.Application.Mappings
{
    internal class DesignationProfile: Profile
    {
        public DesignationProfile()
        {
            CreateMap<Designation, Designation>();
        }
    }
}
