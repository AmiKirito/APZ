using PollutionReports.Models;
using PollutionReports.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;

namespace PollutionReports.Implementation
{
    public class SQLSurvey : ISurveyRepository
    {
        private readonly AppDbContext context;
        private readonly IWebHostEnvironment hostingEnvironment;
        public SQLSurvey(AppDbContext appDbContext, IWebHostEnvironment hosting)
        {
            context = appDbContext;
            hostingEnvironment = hosting;
        }
        public Survey Add(Survey survey)
        {
            context.Surveys.Add(survey);
            context.SaveChanges();
            return survey;
        }

        public Survey DeleteSurvey(int id)
        {
            var survey = context.Surveys.Find(id);
            context.Surveys.Remove(survey);
            context.SaveChanges();
            return survey;
        }

        public Survey[] GetAllSurveys()
        {
            return context.Surveys.ToArray();
        }

        public Survey GetSurvey(int id)
        {
            return context.Surveys.Find(id);
        }
    }
}
