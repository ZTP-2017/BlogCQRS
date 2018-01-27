﻿// <auto-generated />
using Blog.WriteSide;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace Blog.WriteSide.Migrations
{
    [DbContext(typeof(MySqlDbContext))]
    [Migration("20180127193717_Init")]
    partial class Init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125");

            modelBuilder.Entity("Blog.WriteSide.Model.ReadSide.ArticleDetailsRecord", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Date");

                    b.Property<string>("ImageUrl");

                    b.Property<int>("SectionId");

                    b.Property<string>("Text");

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.ToTable("ArticleDetails");
                });

            modelBuilder.Entity("Blog.WriteSide.Model.ReadSide.SectionDetailsRecord", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ArticlesCount");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("SectionDetails");
                });

            modelBuilder.Entity("Blog.WriteSide.Model.WriteSide.ArticleRecord", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Date");

                    b.Property<string>("ImageUrl");

                    b.Property<int>("SectionId");

                    b.Property<string>("Text");

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.HasIndex("SectionId");

                    b.ToTable("Article");
                });

            modelBuilder.Entity("Blog.WriteSide.Model.WriteSide.SectionRecord", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Section");
                });

            modelBuilder.Entity("Blog.WriteSide.Model.WriteSide.ArticleRecord", b =>
                {
                    b.HasOne("Blog.WriteSide.Model.WriteSide.SectionRecord", "Section")
                        .WithMany("Articles")
                        .HasForeignKey("SectionId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
