using CropDiseaseDetection.Models;
using CropPulse.Models;
using Microsoft.EntityFrameworkCore;

namespace CropDiseaseDetection.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Disease> Diseases { get; set; }
        public DbSet<Contact> Contacts { get; set; }
    }
}
