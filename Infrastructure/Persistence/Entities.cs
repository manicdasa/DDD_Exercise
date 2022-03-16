using GhostWriter.Infrastructure.Configurations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using GhostWriter.Infrastructure.Identity;
using GhostWriter.Domain.Entities;
using GhostWriter.Application.Common.Interfaces;
using GhostWriter.Domain.Defaults;
using GhostWriter.Infrastructure.Persistence.Configurations;

namespace GhostWriter.Infrastructure.Persistence
{
    public class Entities : IdentityDbContext<ApplicationUser, ApplicationRole, int, IdentityUserClaim<int>, ApplicationUserRole, IdentityUserLogin<int>, IdentityRoleClaim<int>, IdentityUserToken<int>>, IApplicationDbContext
    {
        public Entities(DbContextOptions<Entities> dbContextOptions) : base(dbContextOptions)
        {
        }
        public DbSet<Document> Documents { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Buzzword> Buzzwords { get; set; }
        public DbSet<Conversation> Conversations { get; set; }
        public DbSet<Degree> Degrees { get; set; }
        public DbSet<Dispute> Disputes { get; set; }
        public DbSet<ExpertiseArea> ExpertiseAreas { get; set; }
        public DbSet<HeadProposal> HeadProposals { get; set; }
        public DbSet<KindOfWork> KindOfWorks { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Milestone> Milestones { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Picture> Pictures { get; set; }
        public DbSet<Domain.Entities.Project> Projects { get; set; }
        public DbSet<Proposal> Proposals { get; set; }
        public DbSet<ProposalStatusHistory> ProposalStatusHistories { get; set; }
        public DbSet<BookingStatusHistory> BookingStatusHistories { get; set; }
        public DbSet<Rate> Rates { get; set; }
        public DbSet<ServiceCharge> ServiceCharges { get; set; }
        public DbSet<ServiceChargeType> ServiceChargeTypes { get; set; }
        public DbSet<UserRoleData> UserRoleDatas { get; set; }
        public DbSet<ApplicationRole> ApplicationRoles { get; set; }
        public DbSet<ApplicationUserRole> ApplicationUserRoles { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<PlagiarismCheckInformation> PlagiarismCheckInformations { get; set; }
        public DbSet<Notification> Notifications { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            #region Data Seeding

            builder.Entity<ApplicationRole>().HasData(new List<ApplicationRole>
            {
                new ApplicationRole {
                    Id = 1,
                     Name = UserRoleDefaults.AdminRoleName,
                    NormalizedName = UserRoleDefaults.AdminRoleName.ToUpper()
                },
                 new ApplicationRole {
                    Id = 2,
                    Name = UserRoleDefaults.CustomerRoleName,
                    NormalizedName = UserRoleDefaults.CustomerRoleName.ToUpper()
                },
                  new ApplicationRole {
                    Id = 3,
                    Name = UserRoleDefaults.GhostwriterRoleName,
                    NormalizedName = UserRoleDefaults.GhostwriterRoleName.ToUpper()
                }
            });

            var hasher = new PasswordHasher<ApplicationUser>();

            builder.Entity<ApplicationUser>().HasData(new List<ApplicationUser>
            {
                new ApplicationUser
                {
                    Id = 1,
                    UserName = "aleksandar_stojcic@code3profit.com",
                    Email = "aleksandar_stojcic@code3profit.com",
                    NormalizedUserName = "ALEKSANDAR_STOJCIC@CODE3PROFIT.COM",
                    NormalizedEmail = "ALEKSANDAR_STOJCIC@CODE3PROFIT.COM",
                    PasswordHash = hasher.HashPassword(null, "Test.123"),
                    SecurityStamp = Guid.NewGuid().ToString(),
                    DateCreated = DateTime.UtcNow
                }
            });

            builder.Entity<ApplicationUser>().HasData(new List<ApplicationUser>
            {
                new ApplicationUser
                {
                    Id = 2,
                    UserName = "dasa_manic@code3profit.com",
                    Email = "dasa_manic@code3profit.com",
                    NormalizedUserName = "DASA_MANIC@CODE3PROFIT.COM",
                    NormalizedEmail = "DASA_MANIC@CODE3PROFIT.COM",
                    PasswordHash = hasher.HashPassword(null, "Test.123"),
                    SecurityStamp = Guid.NewGuid().ToString(),
                    DateCreated = DateTime.UtcNow
                }
            });

            builder.Entity<ApplicationUserRole>().HasData(new List<ApplicationUserRole>
            {
                new ApplicationUserRole
                {
                    RoleId = 1,
                    UserId = 1
                }, new ApplicationUserRole
                {
                    RoleId = 1,
                    UserId = 2
                }, new ApplicationUserRole
                {
                    RoleId = 2,
                    UserId = 2
                }, new ApplicationUserRole
                {
                    RoleId = 3,
                    UserId = 2
                }
            });

            builder.Entity<ServiceChargeType>().HasData(new List<ServiceChargeType>
            {
                new ServiceChargeType
                {
                    Id = 1,
                    Description = "Default Service Charge",
                    Name = "Default Service Charge"
                },
                new ServiceChargeType
                {
                    Id = 2,
                    Description = "Service Charge",
                    Name = "Service Charge"
                }
            });

            builder.Entity<ServiceCharge>().HasData(new List<ServiceCharge>
            {
                new ServiceCharge
                {
                    Id = 1,
                    ChargeAmount = 19,
                    EndDate = null,
                    StartDate = DateTime.MaxValue.Date,
                    IsDefault = true,
                    IsPercentage = true
                    
                },
                 new ServiceCharge
                {
                     Id = 2,
                    ChargeAmount = 9,
                    EndDate = DateTime.UtcNow.Date,
                    StartDate = DateTime.MinValue.Date,
                    IsDefault = true,
                    IsPercentage = false
                }
            });

            builder.Entity<KindOfWork>().HasData(new List<KindOfWork> {
                new KindOfWork
                {
                    Id = 44,
                    FieldStatus = Domain.Enums.FieldStatus.Approved,
                    Value = "Wissenschaftliches Paper",
                    Description = "Wissenschaftliches Paper"
                },
                 new KindOfWork
                {
                     Id = 45,
                    FieldStatus = Domain.Enums.FieldStatus.Approved,
                    Value = "Hausarbeit",
                    Description = "Hausarbeit"
                },
                  new KindOfWork
                {
                    Id = 46,
                    FieldStatus = Domain.Enums.FieldStatus.Approved,
                    Value = "Bachelorarbeit",
                    Description = "Bachelorarbeit"
                },
                   new KindOfWork
                {
                    Id = 47,
                    FieldStatus = Domain.Enums.FieldStatus.Approved,
                    Value = "Diplomarbeit",
                    Description = "Diplomarbeit"
                },
                    new KindOfWork
                {
                    Id = 48,
                    FieldStatus = Domain.Enums.FieldStatus.Approved,
                    Value = "Habilitation",
                    Description = "Habilitation"
                },
                    new KindOfWork
                {
                    Id = 49,
                    FieldStatus = Domain.Enums.FieldStatus.Approved,
                    Value = "Habilitation",
                    Description = "Habilitation"
                },
                    new KindOfWork
                {
                    Id = 50,
                    FieldStatus = Domain.Enums.FieldStatus.Approved,
                    Value = "Doktorarbeit",
                    Description = "Doktorarbeit"
                }
            });

            builder.Entity<ExpertiseArea>().HasData(new List<ExpertiseArea> {
                new ExpertiseArea
                {
                    Id = 51,
                    FieldStatus = Domain.Enums.FieldStatus.Approved,
                    Value = "Erziehungswissenschaften/Pädagogik",
                    Description = "Erziehungswissenschaften/Pädagogik"
                },
                 new ExpertiseArea
                {
                     Id = 52,
                    FieldStatus = Domain.Enums.FieldStatus.Approved,
                    Value = "Politikwissenschaft",
                    Description = "Politikwissenschaft"
                },
                  new ExpertiseArea
                {
                    Id = 53,
                    FieldStatus = Domain.Enums.FieldStatus.Approved,
                    Value = "Psychologie",
                    Description = "Psychologie"
                },
                   new ExpertiseArea
                {
                    Id = 54,
                    FieldStatus = Domain.Enums.FieldStatus.Approved,
                    Value = "Wirtschaftspsychologie",
                    Description = "Wirtschaftspsychologie"
                },
                    new ExpertiseArea
                {
                    Id = 55,
                    FieldStatus = Domain.Enums.FieldStatus.Approved,
                    Value = "Soziologie",
                    Description = "Soziologie"
                },
                    new ExpertiseArea
                {
                    Id = 56,
                    FieldStatus = Domain.Enums.FieldStatus.Approved,
                    Value = "Sport",
                    Description = "Sport"
                },
                    new ExpertiseArea
                {
                    Id = 57,
                    FieldStatus = Domain.Enums.FieldStatus.Approved,
                    Value = "Theologie",
                    Description = "Theologie"
                },
                         new ExpertiseArea
                {
                    Id = 58,
                    FieldStatus = Domain.Enums.FieldStatus.Approved,
                    Value = "Bibliothekswesen",
                    Description = "Bibliothekswesen"
                },
                    new ExpertiseArea
                {
                     Id = 59,
                    FieldStatus = Domain.Enums.FieldStatus.Approved,
                    Value = "Design",
                    Description = "Design"
                },
                  new ExpertiseArea
                {
                    Id = 60,
                    FieldStatus = Domain.Enums.FieldStatus.Approved,
                    Value = "Germanistik",
                    Description = "Germanistik"
                },
                      new ExpertiseArea
                {
                     Id = 61,
                    FieldStatus = Domain.Enums.FieldStatus.Approved,
                    Value = "Geschichte",
                    Description = "Geschichte"
                },
                  new ExpertiseArea
                {
                    Id = 62,
                    FieldStatus = Domain.Enums.FieldStatus.Approved,
                    Value = "Gestaltung",
                    Description = "Gestaltung"
                },
                      new ExpertiseArea
                {
                     Id = 63,
                    FieldStatus = Domain.Enums.FieldStatus.Approved,
                    Value = "Architektur",
                    Description = "Architektur"
                },
                  new ExpertiseArea
                {
                    Id = 64,
                    FieldStatus = Domain.Enums.FieldStatus.Approved,
                    Value = "Journalismus",
                    Description = "Journalismus"
                },
                      new ExpertiseArea
                {
                     Id = 65,
                    FieldStatus = Domain.Enums.FieldStatus.Approved,
                    Value = "Kommunikationswissenschaft",
                    Description = "Kommunikationswissenschaft"
                },
                  new ExpertiseArea
                {
                    Id = 66,
                    FieldStatus = Domain.Enums.FieldStatus.Approved,
                    Value = "Kulturwissenschaft",
                    Description = "Kulturwissenschaft"
                },
                      new ExpertiseArea
                {
                     Id = 67,
                    FieldStatus = Domain.Enums.FieldStatus.Approved,
                    Value = "Kunstgeschichte",
                    Description = "Kunstgeschichte"
                },
                  new ExpertiseArea
                {
                    Id = 68,
                    FieldStatus = Domain.Enums.FieldStatus.Approved,
                    Value = "Medienwissenschaft",
                    Description = "Medienwissenschaft"
                },
                    new ExpertiseArea
                {
                    Id = 69,
                    FieldStatus = Domain.Enums.FieldStatus.Approved,
                    Value = "Musik",
                    Description = "Musik"
                },
                           new ExpertiseArea
                {
                    Id = 70,
                    FieldStatus = Domain.Enums.FieldStatus.Approved,
                    Value = "Philologie",
                    Description = "Philologie"
                },
                 new ExpertiseArea
                {
                     Id = 72,
                    FieldStatus = Domain.Enums.FieldStatus.Approved,
                    Value = "Philosophie",
                    Description = "Philosophie"
                },
                  new ExpertiseArea
                {
                    Id = 73,
                    FieldStatus = Domain.Enums.FieldStatus.Approved,
                    Value = "Publizistik",
                    Description = "Publizistik"
                },
                   new ExpertiseArea
                {
                    Id = 74,
                    FieldStatus = Domain.Enums.FieldStatus.Approved,
                    Value = "Religion",
                    Description = "Religion"
                },
                    new ExpertiseArea
                {
                    Id = 75,
                    FieldStatus = Domain.Enums.FieldStatus.Approved,
                    Value = "Sprachwissenschaften",
                    Description = "Sprachwissenschaften"
                },
                    new ExpertiseArea
                {
                    Id = 76,
                    FieldStatus = Domain.Enums.FieldStatus.Approved,
                    Value = "Theaterwissenschaft",
                    Description = "Theaterwissenschaft"
                },
                    new ExpertiseArea
                {
                    Id = 77,
                    FieldStatus = Domain.Enums.FieldStatus.Approved,
                    Value = "Biochemie",
                    Description = "Biochemie"
                },
                         new ExpertiseArea
                {
                    Id = 78,
                    FieldStatus = Domain.Enums.FieldStatus.Approved,
                    Value = "Biologie",
                    Description = "Biologie"
                },
                    new ExpertiseArea
                {
                     Id = 79,
                    FieldStatus = Domain.Enums.FieldStatus.Approved,
                    Value = "Chemie",
                    Description = "Chemie"
                },
                  new ExpertiseArea
                {
                    Id = 80,
                    FieldStatus = Domain.Enums.FieldStatus.Approved,
                    Value = "Ernährungswissenschaften",
                    Description = "Ernährungswissenschaften"
                },
                      new ExpertiseArea
                {
                     Id = 81,
                    FieldStatus = Domain.Enums.FieldStatus.Approved,
                    Value = "Geografie",
                    Description = "Geografie"
                },
                  new ExpertiseArea
                {
                    Id = 82,
                    FieldStatus = Domain.Enums.FieldStatus.Approved,
                    Value = "Geologie",
                    Description = "Geologie"
                },
                      new ExpertiseArea
                {
                     Id = 83,
                    FieldStatus = Domain.Enums.FieldStatus.Approved,
                    Value = "Physik",
                    Description = "Physik"
                },
                  new ExpertiseArea
                {
                    Id = 84,
                    FieldStatus = Domain.Enums.FieldStatus.Approved,
                    Value = "Statistik",
                    Description = "Statistik"
                },
                      new ExpertiseArea
                {
                     Id = 85,
                    FieldStatus = Domain.Enums.FieldStatus.Approved,
                    Value = "Mathematik",
                    Description = "Mathematik"
                },
                  new ExpertiseArea
                {
                    Id = 86,
                    FieldStatus = Domain.Enums.FieldStatus.Approved,
                    Value = "Verfahrenstechnik",
                    Description = "Verfahrenstechnik"
                },
                      new ExpertiseArea
                {
                     Id = 87,
                    FieldStatus = Domain.Enums.FieldStatus.Approved,
                    Value = "Informatik",
                    Description = "Informatik"
                },
                  new ExpertiseArea
                {
                    Id = 88,
                    FieldStatus = Domain.Enums.FieldStatus.Approved,
                    Value = "Haushalt",
                    Description = "Haushalt"
                },
                      new ExpertiseArea
                {
                    Id = 89,
                    FieldStatus = Domain.Enums.FieldStatus.Approved,
                    Value = "Medizin",
                    Description = "Medizin"
                },
                    new ExpertiseArea
                {
                    Id = 90,
                    FieldStatus = Domain.Enums.FieldStatus.Approved,
                    Value = "Gesundheitswissenschaften",
                    Description = "Gesundheitswissenschaften"
                },
                       new ExpertiseArea
                {
                     Id = 91,
                    FieldStatus = Domain.Enums.FieldStatus.Approved,
                    Value = "Pharmazie",
                    Description = "Pharmazie"
                },
                  new ExpertiseArea
                {
                    Id = 92,
                    FieldStatus = Domain.Enums.FieldStatus.Approved,
                    Value = "Pharmatechnik",
                    Description = "Pharmatechnik"
                },
                      new ExpertiseArea
                {
                     Id = 93,
                    FieldStatus = Domain.Enums.FieldStatus.Approved,
                    Value = "Zahnmedizin",
                    Description = "Zahnmedizin"
                },
                  new ExpertiseArea
                {
                    Id = 94,
                    FieldStatus = Domain.Enums.FieldStatus.Approved,
                    Value = "Tiermedizin",
                    Description = "Tiermedizin"
                },
                      new ExpertiseArea
                {
                     Id = 95,
                    FieldStatus = Domain.Enums.FieldStatus.Approved,
                    Value = "Agrarwissenschaft",
                    Description = "Agrarwissenschaft"
                },
                  new ExpertiseArea
                {
                    Id = 96,
                    FieldStatus = Domain.Enums.FieldStatus.Approved,
                    Value = "Forstwissenschaft",
                    Description = "Forstwissenschaft"
                },
                      new ExpertiseArea
                {
                     Id = 97,
                    FieldStatus = Domain.Enums.FieldStatus.Approved,
                    Value = "Gartenbau",
                    Description = "Gartenbau"
                },
                  new ExpertiseArea
                {
                    Id = 98,
                    FieldStatus = Domain.Enums.FieldStatus.Approved,
                    Value = "Holzwirtschaft",
                    Description = "Holzwirtschaft"
                },
                      new ExpertiseArea
                {
                    Id = 99,
                    FieldStatus = Domain.Enums.FieldStatus.Approved,
                    Value = "Holztechnik",
                    Description = "Holztechnik"
                },
                    new ExpertiseArea
                {
                    Id = 100,
                    FieldStatus = Domain.Enums.FieldStatus.Approved,
                    Value = "Landschaftsarchitektur",
                    Description = "Landschaftsarchitektur"
                },
                       new ExpertiseArea
                {
                     Id = 101,
                    FieldStatus = Domain.Enums.FieldStatus.Approved,
                    Value = "Landwirtschaft",
                    Description = "Landwirtschaft"
                },
                  new ExpertiseArea
                {
                    Id = 102,
                    FieldStatus = Domain.Enums.FieldStatus.Approved,
                    Value = "Betriebswirtschaft",
                    Description = "Betriebswirtschaft"
                },
                      new ExpertiseArea
                {
                     Id = 103,
                    FieldStatus = Domain.Enums.FieldStatus.Approved,
                    Value = "Volkswirtschaft",
                    Description = "Volkswirtschaft"
                },
                  new ExpertiseArea
                {
                    Id = 104,
                    FieldStatus = Domain.Enums.FieldStatus.Approved,
                    Value = "Wirtschaftspädagogik",
                    Description = "Wirtschaftspädagogik"
                },
                      new ExpertiseArea
                {
                     Id = 105,
                    FieldStatus = Domain.Enums.FieldStatus.Approved,
                    Value = "Bauingenieurwesen",
                    Description = "Bauingenieurwesen"
                },
                  new ExpertiseArea
                {
                    Id = 106,
                    FieldStatus = Domain.Enums.FieldStatus.Approved,
                    Value = "Drucktechnik",
                    Description = "Drucktechnik"
                },
                      new ExpertiseArea
                {
                     Id = 107,
                    FieldStatus = Domain.Enums.FieldStatus.Approved,
                    Value = "Elektrotechnik",
                    Description = "Elektrotechnik"
                },
                  new ExpertiseArea
                {
                    Id = 108,
                    FieldStatus = Domain.Enums.FieldStatus.Approved,
                    Value = "Informationstechnik",
                    Description = "Informationstechnik"
                },
                      new ExpertiseArea
                {
                    Id = 109,
                    FieldStatus = Domain.Enums.FieldStatus.Approved,
                    Value = "Lebensmitteltechnologie",
                    Description = "Lebensmitteltechnologie"
                },
                    new ExpertiseArea
                {
                    Id = 110,
                    FieldStatus = Domain.Enums.FieldStatus.Approved,
                    Value = "Maschinenbau",
                    Description = "Maschinenbau"
                },
              new ExpertiseArea
                {
                    Id = 111,
                    FieldStatus = Domain.Enums.FieldStatus.Approved,
                    Value = "Medientechnik",
                    Description = "Medientechnik"
                },
                     new ExpertiseArea
                {
                    Id = 112,
                    FieldStatus = Domain.Enums.FieldStatus.Approved,
                    Value = "Raumplanung",
                    Description = "Raumplanung"
                },
                      new ExpertiseArea
                {
                     Id = 113,
                    FieldStatus = Domain.Enums.FieldStatus.Approved,
                    Value = "Umweltschutz",
                    Description = "Umweltschutz"
                },
                  new ExpertiseArea
                {
                    Id = 114,
                    FieldStatus = Domain.Enums.FieldStatus.Approved,
                    Value = "Vermessungswesen",
                    Description = "Vermessungswesen"
                },
                      new ExpertiseArea
                {
                     Id = 115,
                    FieldStatus = Domain.Enums.FieldStatus.Approved,
                    Value = "Materialwissenschaften",
                    Description = "Materialwissenschaften"
                },
                  new ExpertiseArea
                {
                    Id = 116,
                    FieldStatus = Domain.Enums.FieldStatus.Approved,
                    Value = "Jura",
                    Description = "Jura"
                },
                      new ExpertiseArea
                {
                     Id = 117,
                    FieldStatus = Domain.Enums.FieldStatus.Approved,
                    Value = "Medienrecht",
                    Description = "Medienrecht"
                },
                  new ExpertiseArea
                {
                    Id = 118,
                    FieldStatus = Domain.Enums.FieldStatus.Approved,
                    Value = "Wirtschaftsrecht",
                    Description = "Wirtschaftsrecht"
                },
                      new ExpertiseArea
                {
                    Id = 119,
                    FieldStatus = Domain.Enums.FieldStatus.Approved,
                    Value = "Strafrecht",
                    Description = "Strafrecht"
                },
                    new ExpertiseArea
                {
                    Id = 120,
                    FieldStatus = Domain.Enums.FieldStatus.Approved,
                    Value = "Datenschutzrecht",
                    Description = "Datenschutzrecht"
                },
              new ExpertiseArea
                {
                    Id = 121,
                    FieldStatus = Domain.Enums.FieldStatus.Approved,
                    Value = "Öffentliches Recht",
                    Description = "Öffentliches Recht"
                },
                 new ExpertiseArea
                {
                    Id = 122,
                    FieldStatus = Domain.Enums.FieldStatus.Approved,
                    Value = "Essay",
                    Description = "Essay"
                },
                    new ExpertiseArea
                {
                    Id = 123,
                    FieldStatus = Domain.Enums.FieldStatus.Approved,
                    Value = "Case Study",
                    Description = "Case Study"
                },
            });

            builder.Entity<Degree>().HasData(new List<Degree> {
                new Degree
                {
                    Id = 3,
                    Stage = 1,
                    Value = "Kein Abschluss/Nicht festgelegt",
                    Description = "Kein Abschluss/Nicht festgelegt"
                },
                 new Degree
                {
                     Id = 4,
                    Stage = 1,
                    Value = "Hausarbeit",
                    Description = "Hausarbeit"
                },
                   new Degree
                {
                     Id = 5,
                    Stage = 1,
                    Value = "Kirchlicher Abschluss",
                    Description = "Kirchlicher Abschluss"
                },
                  new Degree
                {
                     Id = 6,
                    Stage = 2,
                    Value = "Bachelor of Arts (B.A.)",
                    Description = "Bachelor of Arts (B.A.)"
                },
                 new Degree
                {
                     Id = 7,
                    Stage = 2,
                    Value = "Bachelor of Engineering (B.Eng.)",
                    Description = "Bachelor of Engineering (B.Eng.)"
                },
                  new Degree
                {
                     Id = 8,
                    Stage = 2,
                    Value = "Bachelor of Education (B.Ed.)",
                    Description = "Bachelor of Education (B.Ed.)"
                },
                   new Degree
                {
                     Id = 9,
                    Stage = 2,
                    Value = "Bachelor of Science (B.Sc.)",
                    Description = "Bachelor of Science (B.Sc.)"
                },
                   new Degree
                {
                     Id = 10,
                    Stage = 2,
                    Value = "Bachelor of Laws (LL.B.)",
                    Description = "Bachelor of Laws (LL.B.)"
                },
                  new Degree
                {
                     Id = 11,
                    Stage = 2,
                    Value = "Bachelor of Science in Information Technology (B.Sc.IT)",
                    Description = "Bachelor of Science in Information Technology (B.Sc.IT)"
                },
                   new Degree
                {
                     Id = 12,
                    Stage = 2,
                    Value = "Bachelor sonstiges",
                    Description = "Bachelor sonstiges"
                },
                new Degree
                {
                     Id = 13,
                    Stage = 2,
                    Value = "Diplom (Uni)",
                    Description = "Diplom (Uni)"
                },
                new Degree
                {
                     Id = 14,
                    Stage = 2,
                    Value = "Diplom (FH)",
                    Description = "Diplom (FH)"
                },
                new Degree
                {
                     Id = 15,
                    Stage = 3,
                    Value = "Master of Engineering (M.Eng.)",
                    Description = "Master of Engineering (M.Eng.)"
                },
                new Degree
                {
                     Id = 16,
                    Stage = 3,
                    Value = "Master of Education (M.Ed.)",
                    Description = "Master of Education (M.Ed.)"
                },
                new Degree
                {
                     Id = 17,
                    Stage = 3,
                    Value = "Master of Science (M.Sc.)",
                    Description = "Master of Science (M.Sc.)"
                },
                new Degree
                {
                     Id = 18,
                    Stage = 3,
                    Value = "Master of Laws (LL.M.)",
                    Description = "Master of Laws (LL.M.)"
                },
                new Degree
                {
                     Id = 19,
                    Stage = 3,
                    Value = "Master of Business Administration (MBA)",
                    Description = "Master of Business Administration (MBA)"
                },
                new Degree
                {
                     Id = 20,
                    Stage = 4,
                    Value = "Habilitation",
                    Description = "Habilitation"
                },
                new Degree
                {
                     Id = 21,
                    Stage = 4,
                    Value = "Dr.",
                    Description = "Dr."
                },
                new Degree
                {
                     Id = 22,
                    Stage = 4,
                    Value = "Prof.",
                    Description = "Prof."
                },
                new Degree
                {
                     Id = 23,
                    Stage = 4,
                    Value = "Magister",
                    Description = "Magister"
                },
                     new Degree
                {
                     Id = 24,
                    Stage = 1,
                    Value = "Fakultätsexamen",
                    Description = "Fakultätsexamen"
                },
                     new Degree
                {
                     Id = 25,
                    Stage = 3,
                    Value = "Master of Arts (M.A.)",
                    Description = "Master of Arts (M.A.)"
                },
            });

            #endregion

            #region Entity relationship declarations

            builder.Entity<UserRoleData>()
            .HasMany(x => x.Languages)
            .WithMany(x => x.UserRoleDatas);

            builder.Entity<UserRoleData>()
            .HasMany(x => x.KindOfWorks)
            .WithMany(x => x.UserRoleDatas);

            builder.Entity<UserRoleData>()
            .HasMany(x => x.ExpertiseAreas)
            .WithMany(x => x.UserRoleDatas);

            builder.Entity<UserRoleData>()
            .HasMany(x => x.Buzzwords)
            .WithMany(x => x.UserRoleDatas);

            builder.Entity<Project>()
            .HasMany(x => x.Buzzwords)
            .WithMany(x => x.Projects);
            
            builder.Entity<Project>()
            .HasMany(x => x.ExpertiseAreas)
            .WithMany(x => x.Projects);

            builder.Entity<Project>()
            .HasMany(x => x.ServiceCharges)
            .WithMany(x => x.Projects);

            builder.Entity<ApplicationUser>()
                .HasMany(x => x.UserRoles)
                .WithOne()
                .HasForeignKey(ur => ur.UserId).IsRequired();

            builder.Entity<ApplicationRole>()
             .HasMany(x => x.UserRoles)
             .WithOne()
             .HasForeignKey(ur => ur.RoleId).IsRequired();

            builder.Entity<ApplicationUserRole>()
            .HasOne(b => b.UserRoleData)
            .WithOne(i => i.ApplicationUserRole)
            .HasForeignKey<ApplicationUserRole>(b => b.UserRoleDataId);

            builder.Entity<Conversation>()
            .HasOne(e => e.HeadProposal)
            .WithOne(e => e.Conversation)
            .HasForeignKey<Conversation>(p => p.HeadProposalId);

            builder.Entity<Proposal>()
            .HasOne(e => e.ChildProposal)
            .WithOne(e => e.ParentProposal)
            .HasForeignKey<Proposal>(p => p.ChildProposalId);

            #endregion

            #region Table Configurations

            //builder.ApplyConfiguration(new ApplicationUserConfig());
            //builder.ApplyConfiguration(new ApplicationUserRoleConfig());
            //builder.ApplyConfiguration(new ApplicationRoleConfig());
            //builder.ApplyConfiguration(new DocumentConfig());
            //builder.ApplyConfiguration(new BookingConfig());
            //builder.ApplyConfiguration(new BuzzwordConfig());
            //builder.ApplyConfiguration(new ConversationConfig());
            //builder.ApplyConfiguration(new DegreeConfig());
            //builder.ApplyConfiguration(new DisputeConfig());
            //builder.ApplyConfiguration(new ExpertiseAreaConfig());
            //builder.ApplyConfiguration(new HeadProposalConfig());
            //builder.ApplyConfiguration(new KindOfWorkConfig());
            //builder.ApplyConfiguration(new LanguageConfig());
            //builder.ApplyConfiguration(new MessageConfig());
            //builder.ApplyConfiguration(new MilestoneConfig());
            //builder.ApplyConfiguration(new TransactionConfig());
            //builder.ApplyConfiguration(new ProjectConfig());
            //builder.ApplyConfiguration(new PictureConfig());
            //builder.ApplyConfiguration(new ProposalConfig());
            //builder.ApplyConfiguration(new ProposalStatusHistoryConfig());
            //builder.ApplyConfiguration(new RateConfig());
            //builder.ApplyConfiguration(new ServiceChargeConfig());
            //builder.ApplyConfiguration(new ServiceChargeTypeConfig());
            //builder.ApplyConfiguration(new UserRoleDataConfig());
            //builder.ApplyConfiguration(new BookingStatusHistoryConfig());
            //builder.ApplyConfiguration(new PlagiarismCheckInformationConfig());
            //builder.ApplyConfiguration(new NotificationConfig());
            builder.ApplyConfigurationsFromAssembly(GetType().Assembly);

            #endregion
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
            optionsBuilder.EnableSensitiveDataLogging();
        }
    }
}
