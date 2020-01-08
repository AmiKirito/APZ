using PollutionReports.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PollutionReports.Interfaces
{
    public interface ICompanyRepository
    {
        Company GetCompany(int id);
        Company[] GetAllCompanies();
        Company Add(Company company, User user);
        Company Update(Company companyChange);
        Company Delete(int id);
    }
}
