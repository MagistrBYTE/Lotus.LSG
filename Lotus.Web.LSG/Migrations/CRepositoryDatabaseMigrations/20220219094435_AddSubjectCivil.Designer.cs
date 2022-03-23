﻿// <auto-generated />
using Lotus.LSG;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Lotus.Web.LSG.Migrations.CRepositoryDatabaseMigrations
{
    [DbContext(typeof(CRepositoryDatabase))]
    [Migration("20220219094435_AddSubjectCivil")]
    partial class AddSubjectCivil
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Lotus.LSG.CAddressElement", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    b.Property<string>("CadastralNumber")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("cadastral_number");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("сode");

                    b.Property<int>("ElementType")
                        .HasColumnType("int")
                        .HasColumnName("element_type");

                    b.Property<string>("Number")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)")
                        .HasColumnName("number");

                    b.Property<long>("StreetId")
                        .HasColumnType("bigint")
                        .HasColumnName("street_id");

                    b.HasKey("Id");

                    b.HasIndex("Id")
                        .IsUnique();

                    b.HasIndex("StreetId");

                    b.ToTable("address_item", (string)null);
                });

            modelBuilder.Entity("Lotus.LSG.CAddressStreet", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("varchar(40)")
                        .HasColumnName("names");

                    b.Property<int>("StreetType")
                        .HasColumnType("int")
                        .HasColumnName("street_type");

                    b.Property<long>("VillageId")
                        .HasColumnType("bigint")
                        .HasColumnName("village_id");

                    b.HasKey("Id");

                    b.HasIndex("Id")
                        .IsUnique();

                    b.HasIndex("VillageId");

                    b.ToTable("address_street", (string)null);
                });

            modelBuilder.Entity("Lotus.LSG.CAddressVillage", b =>
                {
                    b.Property<long>("Id")
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("varchar(40)")
                        .HasColumnName("names");

                    b.Property<string>("OKATO")
                        .HasColumnType("longtext")
                        .HasColumnName("okato");

                    b.Property<string>("OKTMO")
                        .HasColumnType("longtext")
                        .HasColumnName("oktmo");

                    b.Property<string>("PostalCode")
                        .HasColumnType("longtext");

                    b.Property<long>("VillageSettlementId")
                        .HasColumnType("bigint")
                        .HasColumnName("village_sett_id");

                    b.Property<int>("VillageType")
                        .HasColumnType("int")
                        .HasColumnName("village_type");

                    b.HasKey("Id");

                    b.HasIndex("Id")
                        .IsUnique();

                    b.HasIndex("VillageSettlementId");

                    b.ToTable("address_village", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 100L,
                            Name = "Андреевский",
                            VillageSettlementId = 1L,
                            VillageType = 0
                        },
                        new
                        {
                            Id = 101L,
                            Name = "Мариинский",
                            VillageSettlementId = 1L,
                            VillageType = 0
                        },
                        new
                        {
                            Id = 200L,
                            Name = "Атамановский",
                            VillageSettlementId = 2L,
                            VillageType = 0
                        },
                        new
                        {
                            Id = 201L,
                            Name = "Степной",
                            VillageSettlementId = 2L,
                            VillageType = 0
                        });
                });

            modelBuilder.Entity("Lotus.LSG.CAddressVillageSettlement", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("varchar(40)")
                        .HasColumnName("names");

                    b.Property<string>("ShortName")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("varchar(40)")
                        .HasColumnName("sname");

                    b.Property<string>("VillageSettlementType")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("village_type");

                    b.HasKey("Id");

                    b.HasIndex("Id")
                        .IsUnique();

                    b.ToTable("address_village_settlement", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            Name = "Андреевское сельское поселение",
                            ShortName = "Андреевское СП",
                            VillageSettlementType = "Cельское поселение"
                        },
                        new
                        {
                            Id = 2L,
                            Name = "Атамановское сельское поселение",
                            ShortName = "Атамановское СП",
                            VillageSettlementType = "Cельское поселение"
                        },
                        new
                        {
                            Id = 3L,
                            Name = "Белокаменское сельское поселение",
                            ShortName = "Белокаменское СП",
                            VillageSettlementType = "Cельское поселение"
                        },
                        new
                        {
                            Id = 4L,
                            Name = "Боровское сельское поселение",
                            ShortName = "Боровское СП",
                            VillageSettlementType = "Cельское поселение"
                        },
                        new
                        {
                            Id = 5L,
                            Name = "Брединское сельское поселение",
                            ShortName = "Брединское СП",
                            VillageSettlementType = "Cельское поселение"
                        },
                        new
                        {
                            Id = 6L,
                            Name = "Калининское сельское поселение",
                            ShortName = "Калининское СП",
                            VillageSettlementType = "Cельское поселение"
                        },
                        new
                        {
                            Id = 7L,
                            Name = "Княженское сельское поселение",
                            ShortName = "Княженское СП",
                            VillageSettlementType = "Cельское поселение"
                        },
                        new
                        {
                            Id = 8L,
                            Name = "Комсомольское сельское поселение",
                            ShortName = "Комсомольское СП",
                            VillageSettlementType = "Cельское поселение"
                        },
                        new
                        {
                            Id = 9L,
                            Name = "Наследницкое сельское поселение",
                            ShortName = "Наследницкое СП",
                            VillageSettlementType = "Cельское поселение"
                        },
                        new
                        {
                            Id = 10L,
                            Name = "Павловское сельское поселение",
                            ShortName = "Павловское СП",
                            VillageSettlementType = "Cельское поселение"
                        },
                        new
                        {
                            Id = 11L,
                            Name = "Рымникское сельское поселение",
                            ShortName = "Рымникское СП",
                            VillageSettlementType = "Cельское поселение"
                        });
                });

            modelBuilder.Entity("Lotus.LSG.CSubjectCivil", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("INN")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)")
                        .HasColumnName("inn");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("varchar(200)")
                        .HasColumnName("names");

                    b.Property<string>("ShortName")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("varchar(40)")
                        .HasColumnName("sname");

                    b.Property<int>("SubjectCivilType")
                        .HasColumnType("int")
                        .HasColumnName("civil_type");

                    b.HasKey("Id");

                    b.HasIndex("Id")
                        .IsUnique();

                    b.ToTable("subject_civil", (string)null);

                    b.HasDiscriminator<string>("Discriminator").HasValue("CSubjectCivil");
                });

            modelBuilder.Entity("Lotus.LSG.CIndividualPerson", b =>
                {
                    b.HasBaseType("Lotus.LSG.CSubjectCivil");

                    b.Property<int>("IndividualType")
                        .HasColumnType("int")
                        .HasColumnName("individual_type");

                    b.Property<string>("OGRN")
                        .IsRequired()
                        .ValueGeneratedOnUpdateSometimes()
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)")
                        .HasColumnName("ogrn");

                    b.HasDiscriminator().HasValue("CIndividualPerson");
                });

            modelBuilder.Entity("Lotus.LSG.CLegalEntityBase", b =>
                {
                    b.HasBaseType("Lotus.LSG.CSubjectCivil");

                    b.Property<string>("KPP")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)")
                        .HasColumnName("kpp");

                    b.Property<string>("LeaderName")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("varchar(40)")
                        .HasColumnName("leader_name");

                    b.Property<string>("LeaderPost")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)")
                        .HasColumnName("leader_post");

                    b.Property<string>("OGRN")
                        .IsRequired()
                        .ValueGeneratedOnUpdateSometimes()
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)")
                        .HasColumnName("ogrn");

                    b.Property<string>("OKPO")
                        .IsRequired()
                        .HasMaxLength(16)
                        .HasColumnType("varchar(16)")
                        .HasColumnName("okpo");

                    b.Property<string>("OKVED")
                        .IsRequired()
                        .HasMaxLength(16)
                        .HasColumnType("varchar(16)")
                        .HasColumnName("okved");

                    b.HasDiscriminator().HasValue("CLegalEntityBase");
                });

            modelBuilder.Entity("Lotus.LSG.CLegalEntity", b =>
                {
                    b.HasBaseType("Lotus.LSG.CLegalEntityBase");

                    b.Property<int>("EntityOwnership")
                        .HasColumnType("int")
                        .HasColumnName("entity_ownership");

                    b.Property<int>("EntityType")
                        .HasColumnType("int")
                        .HasColumnName("entity_type");

                    b.HasDiscriminator().HasValue("CLegalEntity");
                });

            modelBuilder.Entity("Lotus.LSG.CPublicAuthority", b =>
                {
                    b.HasBaseType("Lotus.LSG.CLegalEntityBase");

                    b.Property<int>("PublicType")
                        .HasColumnType("int")
                        .HasColumnName("public_type");

                    b.HasDiscriminator().HasValue("CPublicAuthority");
                });

            modelBuilder.Entity("Lotus.LSG.CAddressElement", b =>
                {
                    b.HasOne("Lotus.LSG.CAddressStreet", "Street")
                        .WithMany()
                        .HasForeignKey("StreetId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Street");
                });

            modelBuilder.Entity("Lotus.LSG.CAddressStreet", b =>
                {
                    b.HasOne("Lotus.LSG.CAddressVillage", "Village")
                        .WithMany("Streets")
                        .HasForeignKey("VillageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Village");
                });

            modelBuilder.Entity("Lotus.LSG.CAddressVillage", b =>
                {
                    b.HasOne("Lotus.LSG.CAddressVillageSettlement", "VillageSettlement")
                        .WithMany("Villages")
                        .HasForeignKey("VillageSettlementId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("VillageSettlement");
                });

            modelBuilder.Entity("Lotus.LSG.CAddressVillage", b =>
                {
                    b.Navigation("Streets");
                });

            modelBuilder.Entity("Lotus.LSG.CAddressVillageSettlement", b =>
                {
                    b.Navigation("Villages");
                });
#pragma warning restore 612, 618
        }
    }
}
