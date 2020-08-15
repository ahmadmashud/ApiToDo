using Microsoft.EntityFrameworkCore;
using ApiTest.DbEntities;

namespace ApiTest.DbEntities
{
    public class CRUDContext:DbContext
    {
        public CRUDContext(DbContextOptions<CRUDContext> options):base(options) {
        }
        

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);
            new ToDoMap(modelBuilder.Entity<ToDo>());
        }

        public DbSet<ApiTest.Models.ToDoViewModel> ToDoViewModel { get; set; }
        public DbSet<ApiTest.DbEntities.ToDo> ToDo { get; set; }
    }
}
