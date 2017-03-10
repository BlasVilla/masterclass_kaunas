using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using ResultsMicroservice.Database;

namespace ResultsMicroservice.Migrations
{
    [DbContext(typeof(ResultsDbContext))]
    partial class ResultsDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.0-rtm-22752");

            modelBuilder.Entity("ResultsMicroservice.Database.Result", b =>
                {
                    b.Property<Guid>("RequestId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Create");

                    b.Property<string>("Method")
                        .IsRequired()
                        .HasMaxLength(32);

                    b.Property<double>("Value");

                    b.HasKey("RequestId");

                    b.ToTable("Results");
                });
        }
    }
}
