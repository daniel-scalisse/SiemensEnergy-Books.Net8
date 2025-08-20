using Books_Business.Modules.Genders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Books_Data.Modules.Genders
{
    public class GenderMapping : IEntityTypeConfiguration<Gender>
    {
        public void Configure(EntityTypeBuilder<Gender> builder)
        {
            builder.ToTable("Genders");

            builder.HasKey(g => g.Id);

            builder.Property(g => g.Id).ValueGeneratedOnAdd();

            builder.Property(g => g.Name)
                .HasColumnType($"varchar({GenderMaxLength.NameMax})")
                .IsRequired();

            builder.HasMany(g => g.Books)
                .WithOne(b => b.Gender)
                .HasForeignKey(b => b.GenderId);
        }
    }
}