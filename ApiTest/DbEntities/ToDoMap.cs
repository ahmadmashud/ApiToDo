using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiTest.DbEntities
{
    public class ToDoMap
    {
        public ToDoMap(EntityTypeBuilder<ToDo> entityBuilder)
        {
            entityBuilder.HasKey(t => t.Id);
            entityBuilder.Property(t => t.Title).IsRequired();
            entityBuilder.Property(t => t.Description).IsRequired();
            entityBuilder.Property(t => t.CompletedPresentage).IsRequired();
            entityBuilder.Property(t => t.ExpiryDate).IsRequired();
            entityBuilder.Property(t => t.CreatedAt).IsRequired(); 
            entityBuilder.Property(t => t.IsCompleted).IsRequired();
        }
    }
}
