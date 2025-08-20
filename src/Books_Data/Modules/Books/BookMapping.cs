using Books_Business.Modules.Books;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Books_Data.Modules.Books
{
    public class BookMapping : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.ToTable("Books");

            builder.HasKey(b => b.Id);

            builder.Property(b => b.Id).ValueGeneratedOnAdd();

            builder.Property(b => b.Title)
                .HasColumnType($"varchar({BookMaxLength.TitleMax})")
                .IsRequired();

            builder.Property(b => b.SubTitle)
                .HasColumnType($"varchar({BookMaxLength.SubTitleMax})");

            builder.Property(b => b.ISBN)
                .HasColumnType($"varchar({BookMaxLength.ISBNMax})")
                .IsRequired();

            builder.HasIndex(b => b.ISBN).HasDatabaseName("IX_ISBN").IsUnique(true);

            builder.Property(b => b.Barcode)
                .HasColumnType($"varchar({BookMaxLength.BarcodeMax})")
                .IsRequired();

            builder.HasIndex(b => b.Barcode).HasDatabaseName("IX_BarCode").IsUnique(true);

            builder.Property(b => b.Value)
                .HasColumnType("numeric(9,2)");

            builder.Property(b => b.Dedication).IsRequired();

            builder.Property(b => b.Observation)
                .HasColumnType($"varchar({BookMaxLength.Observation})");

            builder.HasOne(b => b.BookImage)
                .WithOne(b => b.Book)
                .HasForeignKey<BookImage>(b => b.Id);
        }
    }
}