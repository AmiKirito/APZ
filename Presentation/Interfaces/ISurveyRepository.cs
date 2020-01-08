using PollutionReports.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PollutionReports.Interfaces
{
    public interface ISurveyRepository
    {
        Survey GetSurvey(int id);
        Survey[] GetAllSurveys();
        Survey Add(Survey survey);
        Survey DeleteSurvey(int id);
    }
}
