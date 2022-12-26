using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using JobRecommendationWeb.Models;
using System;
using System.Linq;

namespace JobRecommendationWeb.Models;

public static class SeedData
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using (var context = new JobRecommendationContext(
            serviceProvider.GetRequiredService<
                DbContextOptions<JobRecommendationContext>>()))
        {
            // Look for any movies.
            if (context.Kinangs.Any())
            {
                return;   // DB has been seeded
            }
            context.Kinangs.AddRange(
                new Kinang
                {
                    TenKiNang = "An hai",
                },
                new Kinang
                {
                    TenKiNang = "Pha Hoai",
                },
                new Kinang
                {
                    TenKiNang = "Luoi bieng",
                },
                new Kinang
                {
                    TenKiNang = "Ham choi",
                }
            );
            context.SaveChanges();
        }
    }
}
