using PollutionReports.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PollutionReports.Interfaces
{
    public interface IReportRepository
    {
        Report GetReport(int id);
        Report[] GetAllReports();
        Report Add(Report report);
        Report DeleteReport(int id);

    }
}
