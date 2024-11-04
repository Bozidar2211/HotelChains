﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

#nullable disable

namespace DataAcess.Migrations
{
    [DbContext(typeof(RepositoryDbContext))]
    partial class RepositoryDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Domain.Entities.Employee", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateOnly>("DateOfBirth")
                        .HasColumnType("date");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("HotelId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Position")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("HotelId");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("Domain.Entities.Hotel", b =>
                {
                    b.Property<Guid>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("HotelChainId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("NumberOfEmloyees")
                        .HasColumnType("int");

                    b.Property<int>("NumberOfRooms")
                        .HasColumnType("int");

                    b.Property<int>("YearOpened")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.HasIndex("HotelChainId");

                    b.ToTable("Hotels");
                });

            modelBuilder.Entity("Domain.Entities.HotelChain", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("YearEstablished")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("HotelChains");
                });

            modelBuilder.Entity("Domain.Entities.Employee", b =>
                {
                    b.HasOne("Domain.Entities.Hotel", "Hotel")
                        .WithMany("Employees")
                        .HasForeignKey("HotelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Hotel");
                });

            modelBuilder.Entity("Domain.Entities.Hotel", b =>
                {
                    b.HasOne("Domain.Entities.HotelChain", "HotelChain")
                        .WithMany("Hotels")
                        .HasForeignKey("HotelChainId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("HotelChain");
                });

            modelBuilder.Entity("Domain.Entities.Hotel", b =>
                {
                    b.Navigation("Employees");
                });

            modelBuilder.Entity("Domain.Entities.HotelChain", b =>
                {
                    b.Navigation("Hotels");
                });
#pragma warning restore 612, 618
        }
    }
}
