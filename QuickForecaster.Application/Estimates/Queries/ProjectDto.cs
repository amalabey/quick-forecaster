using QuickForecaster.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace QuickForecaster.Application.Estimates.Queries
{
    public class ProjectDto
    {
        public int Id { get; set; }
        public string ProjectName { get; set; }

        public static Expression<Func<Estimate, ProjectDto>> Projection
        {
            get
            {
                return e => new ProjectDto
                {
                    Id = e.Id,
                    ProjectName = e.ProjectName
                };
            }
        }
    }
}
