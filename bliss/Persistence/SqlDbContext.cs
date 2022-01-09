using Microsoft.EntityFrameworkCore;
using Models;

namespace Persistence
{
    public class SqlDbContext : DbContext
    {
        public SqlDbContext(DbContextOptions options)
            : base(options)
        {

        }
        public DbSet<PossibleAnswer> Choices { get; set; } = default!;
        public DbSet<Question> Questions { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PossibleAnswer>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Answer)
                    .IsRequired();
                entity.Property(e => e.Votes)
                    .IsRequired();
                entity.Property(e => e.QuestionId)
                    .IsRequired();
            });

            modelBuilder.Entity<Question>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.ImageUrl)
                    .IsRequired();
                entity.Property(e => e.ThumbUrl)
                    .IsRequired();
                entity.Property(e => e.PublishedAt)
                    .IsRequired();
                entity.Property(e => e.Description)
                    .IsRequired();
            });

        }
    }
}
