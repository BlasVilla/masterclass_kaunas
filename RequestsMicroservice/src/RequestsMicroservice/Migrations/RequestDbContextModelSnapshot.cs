using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using RequestsMicroservice.Database;

namespace RequestsMicroservice.Migrations
{
    [DbContext(typeof(RequestsDbContext))]
    partial class RequestDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.0-rtm-22752");

            modelBuilder.Entity("RequestsMicroservice.Database.Batch", b =>
                {
                    b.Property<Guid>("BatchId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Created");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(64);

                    b.HasKey("BatchId");

                    b.ToTable("Batches");
                });

            modelBuilder.Entity("RequestsMicroservice.Database.Request", b =>
                {
                    b.Property<Guid>("RequestId")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("BatchId");

                    b.Property<DateTime>("Created");

                    b.Property<int>("Index");

                    b.Property<double>("X");

                    b.HasKey("RequestId");

                    b.HasIndex("BatchId");

                    b.ToTable("Requests");
                });

            modelBuilder.Entity("RequestsMicroservice.Database.Request", b =>
                {
                    b.HasOne("RequestsMicroservice.Database.Batch", "Batch")
                        .WithMany("Requests")
                        .HasForeignKey("BatchId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
