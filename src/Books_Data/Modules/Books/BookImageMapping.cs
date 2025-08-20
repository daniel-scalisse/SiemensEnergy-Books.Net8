using Books_Business.Modules.Books;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Books_Data.Modules.Books
{
    public class BookImageMapping : IEntityTypeConfiguration<BookImage>
    {
        public void Configure(EntityTypeBuilder<BookImage> builder)
        {
            builder.ToTable("BookImages");

            builder.HasKey(b => b.Id);

            builder.Property(b => b.Id).ValueGeneratedNever();

            builder.HasIndex(b => b.Id).HasDatabaseName("IX_BookId").IsUnique(true);

            builder.Property(b => b.Image).IsRequired();
        }
    }
}