﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TracePlot.Data;

namespace TracePlot.Migrations
{
    [DbContext(typeof(TraceRouteDbContext))]
    partial class TraceRouteDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.11");

            modelBuilder.Entity("TracePlot.Data.Hop", b =>
                {
                    b.Property<Guid>("HopId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Address")
                        .HasColumnType("TEXT");

                    b.Property<double>("AverageReplyTime")
                        .HasColumnType("REAL");

                    b.Property<double>("HigherQuartile")
                        .HasColumnType("REAL");

                    b.Property<int>("HopNumber")
                        .HasColumnType("INTEGER");

                    b.Property<double>("LowerQuartile")
                        .HasColumnType("REAL");

                    b.Property<long>("MaximumReplyTime")
                        .HasColumnType("INTEGER");

                    b.Property<double>("MedianReplyTime")
                        .HasColumnType("REAL");

                    b.Property<long>("MinimumReplyTime")
                        .HasColumnType("INTEGER");

                    b.Property<Guid>("ParentID")
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("TraceRouteCollectionID")
                        .HasColumnType("TEXT");

                    b.HasKey("HopId");

                    b.HasIndex("TraceRouteCollectionID");

                    b.ToTable("Hops");
                });

            modelBuilder.Entity("TracePlot.Data.TraceRouteCollection", b =>
                {
                    b.Property<Guid>("TraceRouteCollectionID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<int>("IntervalSize")
                        .HasColumnType("INTEGER");

                    b.Property<int>("NumberOfLoops")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("Start")
                        .HasColumnType("TEXT");

                    b.Property<string>("TargetHostname")
                        .HasColumnType("TEXT");

                    b.HasKey("TraceRouteCollectionID");

                    b.ToTable("TraceRouteCollections");
                });

            modelBuilder.Entity("TracePlot.Models.ReplyTime", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("HopID")
                        .HasColumnType("TEXT");

                    b.Property<long>("Time")
                        .HasColumnType("INTEGER");

                    b.HasKey("ID");

                    b.HasIndex("HopID");

                    b.ToTable("ReplyTimes");
                });

            modelBuilder.Entity("TracePlot.Data.Hop", b =>
                {
                    b.HasOne("TracePlot.Data.TraceRouteCollection", null)
                        .WithMany("Hops")
                        .HasForeignKey("TraceRouteCollectionID");
                });

            modelBuilder.Entity("TracePlot.Models.ReplyTime", b =>
                {
                    b.HasOne("TracePlot.Data.Hop", null)
                        .WithMany("ReplyTimes")
                        .HasForeignKey("HopID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TracePlot.Data.Hop", b =>
                {
                    b.Navigation("ReplyTimes");
                });

            modelBuilder.Entity("TracePlot.Data.TraceRouteCollection", b =>
                {
                    b.Navigation("Hops");
                });
#pragma warning restore 612, 618
        }
    }
}
