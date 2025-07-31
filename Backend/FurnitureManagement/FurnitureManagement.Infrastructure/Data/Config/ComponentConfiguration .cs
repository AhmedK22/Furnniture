using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FurnitureManagement.Domain.Entities;
using System.Reflection.Emit;


namespace FurnitureManagement.Infrastructure.Data.Config
{
    public class ComponentConfiguration : IEntityTypeConfiguration<Component>
    {
        public void Configure(EntityTypeBuilder<Component> builder)
        {
            builder.ToTable("Components");

            builder.HasKey(c => c.Id);


           builder
                .Property(p => p.Id)
                .ValueGeneratedOnAdd();


            builder.Property(c => c.Name).IsRequired().HasMaxLength(100);
            builder.Property(c => c.Quantity).IsRequired();

            builder.HasMany(c => c.Subcomponents)
                   .WithOne(s => s.Component)
                   .HasForeignKey(s => s.ComponentId)
                   .IsRequired(false);
        }
    }

}
