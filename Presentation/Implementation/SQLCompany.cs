using PollutionReports.Models;
using PollutionReports.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Hosting;
using System.Linq;

namespace PollutionReports.Implementation
{
    class SQLCompany : ICompanyRepository
    {
        private readonly AppDbContext context;
        private readonly IWebHostEnvironment hostingEnvironment;
        public SQLCompany(AppDbContext appDbContext, IWebHostEnvironment hosting)
        {
            context = appDbContext;
            hostingEnvironment = hosting;
        }

        public Company Add(Company company, User user)
        {
            context.Companies.Add(company);
            context.Users.Find(user.Id).Companies.Add(company);
            context.SaveChanges();
            return company;
        }

        public Company Delete(int id)
        {
            Company company = context.Companies.Find(id);
            context.Companies.Remove(company);
            context.SaveChanges();
            return company;
        }

        public Company[] GetAllCompanies()
        {
            var model = context.Companies;
            return model.ToArray();
        }

        public Company GetCompany(int id)
        {
            Company company = context.Companies.Find(id);
            return company;
        }

        public Company Update(Company companyChange)
        {
            var company = context.Companies.Attach(companyChange);
            company.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return companyChange;
        }
    }
}
