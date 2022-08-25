using Microsoft.EntityFrameworkCore;
using QnAPlatformBackend.Data.Entities;
using QnAPlatformBackend.Utilties;

namespace QnAPlatformBackend.Data
{
    public class QnADbContext : DbContext
    {
        public QnADbContext(DbContextOptions<QnADbContext> options) : base(options)
        { }

        public DbSet<User> Users { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<Vote> Votes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasData(new List<User>()
                {
                    new User() { Id = 1, UserName = "user 1", Password = "11111".Sha256()},
                    new User() { Id = 2, UserName = "user 2", Password = "22222".Sha256()},
                    new User() { Id = 3, UserName = "user 3", Password = "33333".Sha256()},
                    new User() { Id = 4, UserName = "user 4", Password = "44444".Sha256()},
                    new User() { Id = 5, UserName = "user 5", Password = "55555".Sha256()}
                });
        }
    }
}
