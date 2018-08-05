using FabricGroup.FamilyTree.Domain.Repositories.Interfaces.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FabricGroup.FamilyTree.Infrastructure.Repositories
{
    public class FamilyTreeContext : DbContext
    {
        public FamilyTreeContext(DbContextOptions<FamilyTreeContext> options)
            : base(options)
        {
        }

        /*
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new PersonConfiguration());
            modelBuilder.ApplyConfiguration(new CoupleConfiguration());
            modelBuilder.ApplyConfiguration(new ChildConfiguration());
        }
        */

        public DbSet<Person> PersonList { get; set; }

        public DbSet<Couple> CoupleList { get; set; }

        public DbSet<Child> ChildList { get; set; }
    }

    /*
    public class PersonConfiguration : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> builder)
        {
            builder.HasKey(p => p.PersonId);
            
            builder.Property(p => p.PersonId).IsRequired().ValueGeneratedOnAdd();
            builder.Property(p => p.Name).IsRequired();
        }
    }

    public class CoupleConfiguration : IEntityTypeConfiguration<Couple>
    {
        public void Configure(EntityTypeBuilder<Couple> builder)
        {
            builder.HasKey(p => p.CoupleId);

            builder.Property(p => p.CoupleId).IsRequired().ValueGeneratedOnAdd();
            builder.Property(p => p.PersonIdOfPartner1).IsRequired();
            builder.Property(p => p.PersonIdOfPartner2).IsRequired();
        }
    }

    public class ChildConfiguration : IEntityTypeConfiguration<Child>
    {
        public void Configure(EntityTypeBuilder<Child> builder)
        {
            builder.HasKey(p => p.ChildId);

            builder.Property(p => p.ChildId).IsRequired().ValueGeneratedOnAdd();
            builder.Property(p => p.CoupleId).IsRequired();
            builder.Property(p => p.PersonId).IsRequired();
        }
    }
    */
}