﻿// <auto-generated />
using System;
using LSM.SsoService.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace LSM.SsoService.Infrastructure.Persistence.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20240906175128_AddResetPasswordTokens")]
    partial class AddResetPasswordTokens
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("LSM.SsoService.Domain.Entities.Session", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<DateTime?>("EndDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("end_date");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("start_date");

                    b.Property<Guid?>("UserId")
                        .HasColumnType("uuid")
                        .HasColumnName("user_id");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("session", (string)null);
                });

            modelBuilder.Entity("LSM.SsoService.Domain.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("email");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("password");

                    b.Property<string>("Patronymic")
                        .HasColumnType("text")
                        .HasColumnName("patronymic");

                    b.Property<byte>("Role")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("smallint")
                        .HasDefaultValue((byte)1)
                        .HasColumnName("role");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("surname");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("user", (string)null);
                });

            modelBuilder.Entity("LSM.SsoService.Domain.ValueObjects.Tokens.ResetPasswordToken", b =>
                {
                    b.Property<DateTime>("TokenExpiryDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("token_expiry_date");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid")
                        .HasColumnName("user_id");

                    b.Property<string>("Token")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("token");

                    b.HasKey("TokenExpiryDate", "UserId");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("reset_password_token", (string)null);
                });

            modelBuilder.Entity("LSM.SsoService.Domain.Entities.Session", b =>
                {
                    b.HasOne("LSM.SsoService.Domain.Entities.User", "User")
                        .WithMany("Sessions")
                        .HasForeignKey("UserId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("LSM.SsoService.Domain.ValueObjects.Tokens.ResetPasswordToken", b =>
                {
                    b.HasOne("LSM.SsoService.Domain.Entities.User", "User")
                        .WithOne("ResetPasswordToken")
                        .HasForeignKey("LSM.SsoService.Domain.ValueObjects.Tokens.ResetPasswordToken", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("LSM.SsoService.Domain.Entities.User", b =>
                {
                    b.Navigation("ResetPasswordToken");

                    b.Navigation("Sessions");
                });
#pragma warning restore 612, 618
        }
    }
}
