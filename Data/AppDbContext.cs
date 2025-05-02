using Microsoft.EntityFrameworkCore;

namespace KinadecWebsite.Data
{

        public class AppDbContext : DbContext
        {
            public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

            public DbSet<ImageMetadata> ImageMetadata { get; set; }
        }

        public class ImageMetadata
        {
            public int Id { get; set; }
            public string PublicId { get; set; }
            public string? Name { get; set; } // Optional at first
            public string Url { get; set; }
            public string Format { get; set; }
            public string ResourceType { get; set; }
            public DateTime CreatedAt { get; set; }
        }
}
