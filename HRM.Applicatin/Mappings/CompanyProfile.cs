using AutoMapper;
using HRM.Applicatin;
using HRM.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRM.Application.Mappings
{
    public class CompanyProfile: Profile
    {
        public CompanyProfile() 
        {
            CreateMap<CommandCompanyDto, Company>();
        }
    }
}
