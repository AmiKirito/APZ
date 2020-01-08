using Microsoft.EntityFrameworkCore;
using PollutionReports.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PollutionReports.Models
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Company>()
                .HasOne<User>(c => c.Owner)
                .WithMany(o => o.Companies)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Report>()
                .HasOne<Company>(r => r.Company)
                .WithMany(c => c.Reports)
                .HasForeignKey(r => r.CompanyId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Survey>()
                .HasOne<Company>(s => s.Company)
                .WithMany(c => c.Surveys)
                .HasForeignKey(s => s.CompanyId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Survey>()
                .HasOne<User>(s => s.User)
                .WithMany(u => u.Surveys)
                .HasForeignKey(s => s.UserId);
        }
    }
}
