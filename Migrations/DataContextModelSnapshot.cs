﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebApiAssignemnt.Data;

#nullable disable

namespace WebApiAssignemnt.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("WebApiAssignemnt.Models.LogRequests", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedDateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("IPAddress")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RequestBody")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("LogRequests");
                });

            modelBuilder.Entity("WebApiAssignemnt.Models.MessageDetails", b =>
                {
                    b.Property<int>("MessageId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MessageId"));

                    b.Property<string>("MessageContent")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("MessageTimestamp")
                        .HasColumnType("datetime2");

                    b.Property<int?>("receiverId")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<int?>("senderId")
                        .HasColumnType("int");

                    b.HasKey("MessageId");

                    b.HasIndex("receiverId");

                    b.HasIndex("senderId");

                    b.ToTable("MessageDetails");
                });

            modelBuilder.Entity("WebApiAssignemnt.Models.UserDetail", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("UserDeatails");
                });

            modelBuilder.Entity("WebApiAssignemnt.Models.MessageDetails", b =>
                {
                    b.HasOne("WebApiAssignemnt.Models.UserDetail", "ReceiverDetails")
                        .WithMany("receivedMessages")
                        .HasForeignKey("receiverId")
                        .IsRequired();

                    b.HasOne("WebApiAssignemnt.Models.UserDetail", "SenderDetails")
                        .WithMany("sentMessages")
                        .HasForeignKey("senderId");

                    b.Navigation("ReceiverDetails");

                    b.Navigation("SenderDetails");
                });

            modelBuilder.Entity("WebApiAssignemnt.Models.UserDetail", b =>
                {
                    b.Navigation("receivedMessages");

                    b.Navigation("sentMessages");
                });
#pragma warning restore 612, 618
        }
    }
}
