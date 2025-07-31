using FurnitureManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureManagement.Infrastructure.Data.Config
{
    public class SubcomponentConfiguration : IEntityTypeConfiguration<Subcomponent>
    {
        public void Configure(EntityTypeBuilder<Subcomponent> builder)
        {
            builder.ToTable("Subcomponents");

            builder.HasKey(s => s.Id);
            builder
               .Property(p => p.Id)
               .ValueGeneratedOnAdd();
            builder.Property(s => s.Name).IsRequired().HasMaxLength(100);
            builder.Property(s => s.Material).HasMaxLength(100);
            builder.Property(s => s.CustomNotes).HasMaxLength(500);

            builder.Property(s => s.Count).IsRequired();
          

            builder.Property(s => s.DetailLength);
            builder.Property(s => s.DetailWidth);
            builder.Property(s => s.DetailThickness);

            builder.Property(s => s.CuttingLength);
            builder.Property(s => s.CuttingWidth);
            builder.Property(s => s.CuttingThickness);

            builder.Property(s => s.FinalLength);
            builder.Property(s => s.FinalWidth);
            builder.Property(s => s.FinalThickness);

            builder.Property(s => s.VeneerInner).HasMaxLength(100);
            builder.Property(s => s.VeneerOuter).HasMaxLength(100);
           
        }
    }

}
