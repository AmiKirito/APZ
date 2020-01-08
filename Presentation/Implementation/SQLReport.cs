using PollutionReports.Models;
using PollutionReports.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;

namespace PollutionReports.Implementation
{
    public class SQLReport : IReportRepository
    {
        private readonly AppDbContext context;
        private readonly IWebHostEnvironment hostingEnvironment;
        public SQLReport(AppDbContext appDbContext, IWebHostEnvironment hosting)
        {
            context = appDbContext;
            hostingEnvironment = hosting;
        }
        public Report Add(Report report)
        {
            context.Reports.Add(report);
            context.SaveChanges();
            return report;
        }

        public Report DeleteReport(int id)
        {
            var report = context.Reports.Find(id);
            context.Reports.Remove(report);
            context.SaveChanges();
            return report;
        }

        public Report[] GetAllReports()
        {
            return context.Reports.ToArray();
        }

        public Report GetReport(int id)
        {
            return context.Reports.Find(id);
        }
    }
}
