using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PrekenWeb.WebApi.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "HangFire");

            migrationBuilder.CreateTable(
                name: "Afbeelding",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Omschrijving = table.Column<string>(maxLength: 255, nullable: false),
                    Bestandsnaam = table.Column<string>(maxLength: 255, nullable: false),
                    ContentType = table.Column<string>(maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Afbeelding", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 256, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Gebruiker",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naam = table.Column<string>(maxLength: 255, nullable: false),
                    Email = table.Column<string>(nullable: true),
                    LaatstIngelogd = table.Column<DateTime>(type: "datetime", nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEndDateUtc = table.Column<DateTime>(type: "datetime", nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    UserName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gebruiker", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Gemeente",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Omschrijving = table.Column<string>(maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gemeente", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "InboxType",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Omschrijving = table.Column<string>(maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InboxType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LezingCategorie",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Omschrijving = table.Column<string>(maxLength: 255, nullable: false),
                    OudId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LezingCategorie", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PreekType",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Omschrijving = table.Column<string>(maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PreekType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SchemaVersions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ScriptName = table.Column<string>(maxLength: 255, nullable: false),
                    Applied = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SchemaVersions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Taal",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(maxLength: 10, nullable: false),
                    Omschrijving = table.Column<string>(maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Taal", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AggregatedCounter",
                schema: "HangFire",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Key = table.Column<string>(maxLength: 100, nullable: false),
                    Value = table.Column<long>(nullable: false),
                    ExpireAt = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AggregatedCounter", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Counter",
                schema: "HangFire",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Key = table.Column<string>(maxLength: 100, nullable: false),
                    Value = table.Column<short>(nullable: false),
                    ExpireAt = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Counter", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Hash",
                schema: "HangFire",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Key = table.Column<string>(maxLength: 100, nullable: false),
                    Field = table.Column<string>(maxLength: 100, nullable: false),
                    Value = table.Column<string>(nullable: true),
                    ExpireAt = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hash", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Job",
                schema: "HangFire",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StateId = table.Column<int>(nullable: true),
                    StateName = table.Column<string>(maxLength: 20, nullable: true),
                    InvocationData = table.Column<string>(nullable: false),
                    Arguments = table.Column<string>(nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    ExpireAt = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Job", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "JobQueue",
                schema: "HangFire",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    JobId = table.Column<int>(nullable: false),
                    Queue = table.Column<string>(maxLength: 50, nullable: false),
                    FetchedAt = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobQueue", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "List",
                schema: "HangFire",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Key = table.Column<string>(maxLength: 100, nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExpireAt = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_List", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Schema",
                schema: "HangFire",
                columns: table => new
                {
                    Version = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HangFire_Schema", x => x.Version);
                });

            migrationBuilder.CreateTable(
                name: "Server",
                schema: "HangFire",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 100, nullable: false),
                    Data = table.Column<string>(nullable: true),
                    LastHeartbeat = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Server", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Set",
                schema: "HangFire",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Key = table.Column<string>(maxLength: 100, nullable: false),
                    Score = table.Column<double>(nullable: false),
                    Value = table.Column<string>(maxLength: 256, nullable: false),
                    ExpireAt = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Set", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true),
                    Gebruiker_Id = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_dbo.AspNetUserClaims_dbo.Gebruiker_Gebruiker_Id",
                        column: x => x.Gebruiker_Id,
                        principalTable: "Gebruiker",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(maxLength: 128, nullable: false),
                    UserId = table.Column<int>(nullable: false),
                    Gebruiker_Id = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo.AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey, x.UserId });
                    table.ForeignKey(
                        name: "FK_dbo.AspNetUserLogins_dbo.Gebruiker_Gebruiker_Id",
                        column: x => x.Gebruiker_Id,
                        principalTable: "Gebruiker",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false),
                    RoleId = table.Column<int>(nullable: false),
                    Gebruiker_Id = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo.AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_dbo.AspNetUserRoles_dbo.Gebruiker_Gebruiker_Id",
                        column: x => x.Gebruiker_Id,
                        principalTable: "Gebruiker",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Pagina",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Gepubliceerd = table.Column<bool>(nullable: false),
                    Identifier = table.Column<string>(maxLength: 255, nullable: false),
                    Aangemaakt = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    Bijgewerkt = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    AangemaaktDoor = table.Column<int>(nullable: false, defaultValueSql: "((1))"),
                    BijgewerktDoor = table.Column<int>(nullable: false, defaultValueSql: "((1))"),
                    TonenOpHomepage = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pagina", x => x.Id);
                    table.ForeignKey(
                        name: "FK_dbo.Pagina_dbo.Gebruiker_AangemaaktDoor",
                        column: x => x.AangemaaktDoor,
                        principalTable: "Gebruiker",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_dbo.Pagina_dbo.Gebruiker_BijgewerktDoor",
                        column: x => x.BijgewerktDoor,
                        principalTable: "Gebruiker",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ZoekOpdracht",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PubliekeSleutel = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    LeesPreken = table.Column<bool>(nullable: false),
                    AudioPreken = table.Column<bool>(nullable: false),
                    Lezingen = table.Column<bool>(nullable: false),
                    PredikantId = table.Column<int>(nullable: true),
                    Predikant = table.Column<string>(nullable: true),
                    BoekHoofdstukId = table.Column<int>(nullable: true),
                    BoekHoofdstuk = table.Column<string>(nullable: true),
                    BoekId = table.Column<int>(nullable: true),
                    Boek = table.Column<string>(nullable: true),
                    LezingCategorieId = table.Column<int>(nullable: true),
                    LezingCategorie = table.Column<string>(nullable: true),
                    Hoofdstuk = table.Column<int>(nullable: true),
                    GebeurtenisId = table.Column<int>(nullable: true),
                    Gebeurtenis = table.Column<string>(nullable: true),
                    GemeenteId = table.Column<int>(nullable: true),
                    Gemeente = table.Column<string>(nullable: true),
                    SerieId = table.Column<int>(nullable: true),
                    Serie = table.Column<string>(nullable: true),
                    TaalId = table.Column<int>(nullable: false),
                    GebruikerId = table.Column<int>(nullable: false),
                    Zoekterm = table.Column<string>(nullable: true),
                    SorteerOp = table.Column<int>(nullable: true),
                    SorteerVolgorde = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ZoekOpdracht", x => x.Id);
                    table.ForeignKey(
                        name: "FK_dbo.ZoekOpdracht_dbo.Gebruiker_GebruikerId",
                        column: x => x.GebruikerId,
                        principalTable: "Gebruiker",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Boek",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Boeknaam = table.Column<string>(maxLength: 255, nullable: false),
                    Sortering = table.Column<int>(nullable: false),
                    OudId = table.Column<int>(nullable: true),
                    Afkorting = table.Column<string>(maxLength: 50, nullable: true),
                    ToonHoofdstukNummer = table.Column<bool>(nullable: false, defaultValueSql: "((1))"),
                    TaalId = table.Column<int>(nullable: false, defaultValueSql: "((1))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Boek", x => x.Id);
                    table.ForeignKey(
                        name: "FK_dbo.Boek_dbo.Taal_TaalId",
                        column: x => x.TaalId,
                        principalTable: "Taal",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Gebeurtenis",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Omschrijving = table.Column<string>(maxLength: 255, nullable: false),
                    OudId = table.Column<int>(nullable: true),
                    Sortering = table.Column<int>(nullable: false),
                    TaalId = table.Column<int>(nullable: false, defaultValueSql: "((1))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gebeurtenis", x => x.Id);
                    table.ForeignKey(
                        name: "FK_dbo.Gebeurtenis_dbo.Taal_TaalId",
                        column: x => x.TaalId,
                        principalTable: "Taal",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Mailing",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Omschrijving = table.Column<string>(maxLength: 255, nullable: false),
                    MailChimpId = table.Column<string>(maxLength: 255, nullable: false),
                    TaalId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mailing", x => x.Id);
                    table.ForeignKey(
                        name: "FK_dbo.Mailing_dbo.Taal_TaalId",
                        column: x => x.TaalId,
                        principalTable: "Taal",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NieuwsbriefInschrijving",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naam = table.Column<string>(maxLength: 255, nullable: false),
                    Email = table.Column<string>(maxLength: 255, nullable: false),
                    Aangemeld = table.Column<DateTime>(type: "datetime", nullable: false),
                    Afgemeld = table.Column<DateTime>(type: "datetime", nullable: true),
                    TaalId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NieuwsbriefInschrijving", x => x.Id);
                    table.ForeignKey(
                        name: "FK_dbo.NieuwsbriefInschrijving_dbo.Taal_TaalId",
                        column: x => x.TaalId,
                        principalTable: "Taal",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Predikant",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titels = table.Column<string>(maxLength: 50, nullable: true),
                    Voorletters = table.Column<string>(maxLength: 50, nullable: true),
                    Achternaam = table.Column<string>(maxLength: 255, nullable: false),
                    Gemeente = table.Column<string>(maxLength: 255, nullable: true),
                    LevensPeriode = table.Column<string>(maxLength: 255, nullable: true),
                    OudID = table.Column<int>(nullable: true),
                    GemeenteId = table.Column<int>(nullable: true),
                    Tussenvoegsels = table.Column<string>(maxLength: 50, nullable: true),
                    Opmerking = table.Column<string>(nullable: true),
                    TaalId = table.Column<int>(nullable: false),
                    HideFromIndexingRobots = table.Column<bool>(nullable: false),
                    HideFromPodcast = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Predikant", x => x.Id);
                    table.ForeignKey(
                        name: "FK_dbo.Predikant_dbo.Gemeente_GemeenteId",
                        column: x => x.GemeenteId,
                        principalTable: "Gemeente",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_dbo.Predikant_dbo.Taal_TaalId",
                        column: x => x.TaalId,
                        principalTable: "Taal",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Serie",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Omschrijving = table.Column<string>(maxLength: 255, nullable: false),
                    OudId = table.Column<int>(nullable: true),
                    TaalId = table.Column<int>(nullable: false, defaultValueSql: "((1))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Serie", x => x.Id);
                    table.ForeignKey(
                        name: "FK_dbo.Serie_dbo.Taal_TaalId",
                        column: x => x.TaalId,
                        principalTable: "Taal",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Spotlight",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titel = table.Column<string>(maxLength: 255, nullable: false),
                    Subtitel = table.Column<string>(maxLength: 255, nullable: false),
                    LinkTitel = table.Column<string>(maxLength: 255, nullable: false),
                    Url = table.Column<string>(nullable: false),
                    AfbeeldingId = table.Column<int>(nullable: false),
                    Sortering = table.Column<int>(nullable: false),
                    TaalId = table.Column<int>(nullable: false, defaultValueSql: "((1))"),
                    NieuwVenster = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Spotlight", x => x.Id);
                    table.ForeignKey(
                        name: "FK_dbo.Spotlight_dbo.Afbeelding_AfbeeldingId",
                        column: x => x.AfbeeldingId,
                        principalTable: "Afbeelding",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_dbo.Spotlight_dbo.Taal_TaalId",
                        column: x => x.TaalId,
                        principalTable: "Taal",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "JobParameter",
                schema: "HangFire",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    JobId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 40, nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobParameter", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HangFire_JobParameter_Job",
                        column: x => x.JobId,
                        principalSchema: "HangFire",
                        principalTable: "Job",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "State",
                schema: "HangFire",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    JobId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 20, nullable: false),
                    Reason = table.Column<string>(maxLength: 100, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    Data = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_State", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HangFire_State_Job",
                        column: x => x.JobId,
                        principalSchema: "HangFire",
                        principalTable: "Job",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tekst",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Kop = table.Column<string>(maxLength: 255, nullable: false),
                    Tekst = table.Column<string>(nullable: false, defaultValueSql: "('1')"),
                    TaalId = table.Column<int>(nullable: false),
                    PaginaId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tekst", x => x.Id);
                    table.ForeignKey(
                        name: "FK_dbo.Tekst_dbo.Pagina_PaginaId",
                        column: x => x.PaginaId,
                        principalTable: "Pagina",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_dbo.Tekst_dbo.Taal_TaalId",
                        column: x => x.TaalId,
                        principalTable: "Taal",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BoekHoofdstuk",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BoekId = table.Column<int>(nullable: false),
                    Omschrijving = table.Column<string>(maxLength: 255, nullable: false),
                    Sortering = table.Column<int>(nullable: true),
                    OudId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BoekHoofdstuk", x => x.Id);
                    table.ForeignKey(
                        name: "FK_dbo.BoekHoofdstuk_dbo.Boek_BoekId",
                        column: x => x.BoekId,
                        principalTable: "Boek",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GebruikerMailing",
                columns: table => new
                {
                    GebruikerId = table.Column<int>(nullable: false),
                    MailingId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo.GebruikerMailing", x => new { x.GebruikerId, x.MailingId });
                    table.ForeignKey(
                        name: "FK_dbo.GebruikerMailing_dbo.Gebruiker_GebruikerId",
                        column: x => x.GebruikerId,
                        principalTable: "Gebruiker",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_dbo.GebruikerMailing_dbo.Mailing_MailingId",
                        column: x => x.MailingId,
                        principalTable: "Mailing",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BoekHoofdstukTekst",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BoekHoofdstukId = table.Column<int>(nullable: false),
                    Hoofdstuk = table.Column<int>(nullable: false),
                    Vers = table.Column<int>(nullable: false),
                    Tekst = table.Column<string>(nullable: false),
                    Sortering = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BoekHoofdstukTekst", x => x.Id);
                    table.ForeignKey(
                        name: "FK_dbo.BoekHoofdstukTekst_dbo.BoekHoofdstuk_BoekHoofdstukId",
                        column: x => x.BoekHoofdstukId,
                        principalTable: "BoekHoofdstuk",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Preek",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BoekhoofdstukId = table.Column<int>(nullable: true),
                    BijbeltekstOmschrijving = table.Column<string>(nullable: true),
                    SerieId = table.Column<int>(nullable: true),
                    GebeurtenisId = table.Column<int>(nullable: true),
                    DatumAangemaakt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    DatumBijgewerkt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    Bestandsnaam = table.Column<string>(nullable: true),
                    AantalKeerGedownload = table.Column<int>(nullable: false),
                    OudID = table.Column<int>(nullable: true),
                    PredikantId = table.Column<int>(nullable: true),
                    Hoofdstuk = table.Column<int>(nullable: true),
                    VanVers = table.Column<string>(maxLength: 255, nullable: true),
                    TotVers = table.Column<string>(maxLength: 255, nullable: true),
                    Punt1 = table.Column<string>(maxLength: 255, nullable: true),
                    Punt2 = table.Column<string>(maxLength: 255, nullable: true),
                    Punt3 = table.Column<string>(maxLength: 255, nullable: true),
                    Punt4 = table.Column<string>(maxLength: 255, nullable: true),
                    Punt5 = table.Column<string>(maxLength: 255, nullable: true),
                    GemeenteId = table.Column<int>(nullable: true),
                    DatumPreek = table.Column<DateTime>(type: "date", nullable: true),
                    Informatie = table.Column<string>(nullable: true),
                    ThemaOmschrijving = table.Column<string>(nullable: true),
                    AfbeeldingId = table.Column<int>(nullable: true),
                    PreekTypeId = table.Column<int>(nullable: false),
                    LezingCategorieId = table.Column<int>(nullable: true),
                    TaalId = table.Column<int>(nullable: false),
                    Gepubliceerd = table.Column<bool>(nullable: false),
                    LezingOmschrijving = table.Column<string>(nullable: true),
                    Duur = table.Column<TimeSpan>(nullable: true),
                    Bestandsgrootte = table.Column<int>(nullable: true),
                    VersVanId = table.Column<int>(nullable: true),
                    VersTotId = table.Column<int>(nullable: true),
                    GedeelteVanVersId = table.Column<int>(nullable: true),
                    GedeelteTotVersId = table.Column<int>(nullable: true),
                    VersOmschrijving = table.Column<string>(maxLength: 50, nullable: true),
                    AutomatischeTeksten = table.Column<bool>(nullable: false),
                    AangemaaktDoor = table.Column<int>(nullable: true),
                    AangepastDoor = table.Column<int>(nullable: true),
                    LeesPreekTekst = table.Column<string>(nullable: true),
                    DatumGepubliceerd = table.Column<DateTime>(type: "datetime", nullable: true),
                    SourceFileName = table.Column<string>(unicode: false, nullable: true),
                    Video = table.Column<string>(unicode: false, nullable: true),
                    MeditatieTekst = table.Column<string>(unicode: false, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Preek", x => x.Id);
                    table.ForeignKey(
                        name: "FK_dbo.Preek_dbo.Gebruiker_AangemaaktDoor",
                        column: x => x.AangemaaktDoor,
                        principalTable: "Gebruiker",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_dbo.Preek_dbo.Gebruiker_AangepastDoor",
                        column: x => x.AangepastDoor,
                        principalTable: "Gebruiker",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_dbo.Preek_dbo.Afbeelding_AfbeeldingId",
                        column: x => x.AfbeeldingId,
                        principalTable: "Afbeelding",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_dbo.Preek_dbo.BoekHoofdstuk_BoekhoofdstukId",
                        column: x => x.BoekhoofdstukId,
                        principalTable: "BoekHoofdstuk",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_dbo.Preek_dbo.Gebeurtenis_GebeurtenisId",
                        column: x => x.GebeurtenisId,
                        principalTable: "Gebeurtenis",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_dbo.Preek_dbo.BoekHoofdstukTekst_GedeelteTotVersId",
                        column: x => x.GedeelteTotVersId,
                        principalTable: "BoekHoofdstukTekst",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_dbo.Preek_dbo.BoekHoofdstukTekst_GedeelteVanVersId",
                        column: x => x.GedeelteVanVersId,
                        principalTable: "BoekHoofdstukTekst",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_dbo.Preek_dbo.Gemeente_GemeenteId",
                        column: x => x.GemeenteId,
                        principalTable: "Gemeente",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_dbo.Preek_dbo.LezingCategorie_LezingCategorieId",
                        column: x => x.LezingCategorieId,
                        principalTable: "LezingCategorie",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_dbo.Preek_dbo.Predikant_PredikantId",
                        column: x => x.PredikantId,
                        principalTable: "Predikant",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_dbo.Preek_dbo.PreekType_PreekTypeId",
                        column: x => x.PreekTypeId,
                        principalTable: "PreekType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_dbo.Preek_dbo.Serie_SerieId",
                        column: x => x.SerieId,
                        principalTable: "Serie",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_dbo.Preek_dbo.Taal_TaalId",
                        column: x => x.TaalId,
                        principalTable: "Taal",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_dbo.Preek_dbo.BoekHoofdstukTekst_VersTotId",
                        column: x => x.VersTotId,
                        principalTable: "BoekHoofdstukTekst",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_dbo.Preek_dbo.BoekHoofdstukTekst_VersVanId",
                        column: x => x.VersVanId,
                        principalTable: "BoekHoofdstukTekst",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Inbox",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InboxTypeId = table.Column<int>(nullable: false),
                    Omschrijving = table.Column<string>(maxLength: 255, nullable: false),
                    Inhoud = table.Column<string>(nullable: false),
                    GebruikerId = table.Column<int>(nullable: true),
                    Afgehandeld = table.Column<bool>(nullable: false),
                    Aangemaakt = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    VanNaam = table.Column<string>(maxLength: 255, nullable: false),
                    VanEmail = table.Column<string>(maxLength: 255, nullable: false),
                    AanNaam = table.Column<string>(maxLength: 255, nullable: false),
                    AanEmail = table.Column<string>(maxLength: 255, nullable: false),
                    PreekId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inbox", x => x.Id);
                    table.ForeignKey(
                        name: "FK_dbo.Inbox_dbo.Gebruiker_GebruikerId",
                        column: x => x.GebruikerId,
                        principalTable: "Gebruiker",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_dbo.Inbox_dbo.InboxType_InboxTypeId",
                        column: x => x.InboxTypeId,
                        principalTable: "InboxType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_dbo.Inbox_dbo.Preek_PreekId",
                        column: x => x.PreekId,
                        principalTable: "Preek",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PreekCookie",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PreekId = table.Column<int>(nullable: false),
                    DateTime = table.Column<DateTime>(type: "datetime", nullable: true),
                    Opmerking = table.Column<string>(nullable: true),
                    AfgespeeldTot = table.Column<TimeSpan>(nullable: true),
                    GebruikerId = table.Column<int>(nullable: false),
                    BladwijzerGelegdOp = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PreekCookie", x => x.Id);
                    table.ForeignKey(
                        name: "FK_dbo.PreekCookie_dbo.Gebruiker_GebruikerId",
                        column: x => x.GebruikerId,
                        principalTable: "Gebruiker",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_dbo.PreekCookie_dbo.Preek_PreekId",
                        column: x => x.PreekId,
                        principalTable: "Preek",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PreekLezenEnZingen",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PreekId = table.Column<int>(nullable: false),
                    Sortering = table.Column<byte>(nullable: false),
                    Soort = table.Column<string>(maxLength: 255, nullable: true),
                    Omschrijving = table.Column<string>(maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PreekLezenEnZingen", x => x.Id);
                    table.ForeignKey(
                        name: "FK_dbo.PreekLezenEnZingen_dbo.Preek_PreekId",
                        column: x => x.PreekId,
                        principalTable: "Preek",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InboxOpvolging",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InboxId = table.Column<int>(nullable: false),
                    GebruikerId = table.Column<int>(nullable: true),
                    Aangemaakt = table.Column<DateTime>(type: "datetime", nullable: false),
                    Onderwerp = table.Column<string>(nullable: true),
                    Tekst = table.Column<string>(nullable: true),
                    VerstuurAlsMail = table.Column<bool>(nullable: false),
                    Verstuurd = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InboxOpvolging", x => x.Id);
                    table.ForeignKey(
                        name: "FK_dbo.InboxOpvolging_dbo.Gebruiker_GebruikerId",
                        column: x => x.GebruikerId,
                        principalTable: "Gebruiker",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_dbo.InboxOpvolging_dbo.Inbox_InboxId",
                        column: x => x.InboxId,
                        principalTable: "Inbox",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Gebruiker_Id",
                table: "AspNetUserClaims",
                column: "Gebruiker_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Gebruiker_Id",
                table: "AspNetUserLogins",
                column: "Gebruiker_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Gebruiker_Id",
                table: "AspNetUserRoles",
                column: "Gebruiker_Id");

            migrationBuilder.CreateIndex(
                name: "IX_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Boek_TaalId",
                table: "Boek",
                column: "TaalId");

            migrationBuilder.CreateIndex(
                name: "IX_BoekHoofdstuk_BoekId",
                table: "BoekHoofdstuk",
                column: "BoekId");

            migrationBuilder.CreateIndex(
                name: "IX_BoekHoofdstukTekst_BoekHoofdstukId",
                table: "BoekHoofdstukTekst",
                column: "BoekHoofdstukId");

            migrationBuilder.CreateIndex(
                name: "IX_Gebeurtenis_TaalId",
                table: "Gebeurtenis",
                column: "TaalId");

            migrationBuilder.CreateIndex(
                name: "IX_GebruikerMailing_MailingId",
                table: "GebruikerMailing",
                column: "MailingId");

            migrationBuilder.CreateIndex(
                name: "IX_Inbox_GebruikerId",
                table: "Inbox",
                column: "GebruikerId");

            migrationBuilder.CreateIndex(
                name: "IX_Inbox_InboxTypeId",
                table: "Inbox",
                column: "InboxTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Inbox_PreekId",
                table: "Inbox",
                column: "PreekId");

            migrationBuilder.CreateIndex(
                name: "IX_InboxOpvolging_GebruikerId",
                table: "InboxOpvolging",
                column: "GebruikerId");

            migrationBuilder.CreateIndex(
                name: "IX_InboxOpvolging_InboxId",
                table: "InboxOpvolging",
                column: "InboxId");

            migrationBuilder.CreateIndex(
                name: "IX_Mailing_TaalId",
                table: "Mailing",
                column: "TaalId");

            migrationBuilder.CreateIndex(
                name: "IX_NieuwsbriefInschrijving_TaalId",
                table: "NieuwsbriefInschrijving",
                column: "TaalId");

            migrationBuilder.CreateIndex(
                name: "IX_Pagina_AangemaaktDoor",
                table: "Pagina",
                column: "AangemaaktDoor");

            migrationBuilder.CreateIndex(
                name: "IX_Pagina_BijgewerktDoor",
                table: "Pagina",
                column: "BijgewerktDoor");

            migrationBuilder.CreateIndex(
                name: "IX_Predikant_GemeenteId",
                table: "Predikant",
                column: "GemeenteId");

            migrationBuilder.CreateIndex(
                name: "IX_Predikant_TaalId",
                table: "Predikant",
                column: "TaalId");

            migrationBuilder.CreateIndex(
                name: "IX_Preek_AangemaaktDoor",
                table: "Preek",
                column: "AangemaaktDoor");

            migrationBuilder.CreateIndex(
                name: "IX_Preek_AangepastDoor",
                table: "Preek",
                column: "AangepastDoor");

            migrationBuilder.CreateIndex(
                name: "IX_Preek_AfbeeldingId",
                table: "Preek",
                column: "AfbeeldingId");

            migrationBuilder.CreateIndex(
                name: "IX_Preek_BoekhoofdstukId",
                table: "Preek",
                column: "BoekhoofdstukId");

            migrationBuilder.CreateIndex(
                name: "IX_Preek_GebeurtenisId",
                table: "Preek",
                column: "GebeurtenisId");

            migrationBuilder.CreateIndex(
                name: "IX_Preek_GedeelteTotVersId",
                table: "Preek",
                column: "GedeelteTotVersId");

            migrationBuilder.CreateIndex(
                name: "IX_Preek_GedeelteVanVersId",
                table: "Preek",
                column: "GedeelteVanVersId");

            migrationBuilder.CreateIndex(
                name: "IX_Preek_GemeenteId",
                table: "Preek",
                column: "GemeenteId");

            migrationBuilder.CreateIndex(
                name: "IX_Preek_LezingCategorieId",
                table: "Preek",
                column: "LezingCategorieId");

            migrationBuilder.CreateIndex(
                name: "IX_Preek_PredikantId",
                table: "Preek",
                column: "PredikantId");

            migrationBuilder.CreateIndex(
                name: "IX_Preek_PreekTypeId",
                table: "Preek",
                column: "PreekTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Preek_SerieId",
                table: "Preek",
                column: "SerieId");

            migrationBuilder.CreateIndex(
                name: "IX_Preek_TaalId",
                table: "Preek",
                column: "TaalId");

            migrationBuilder.CreateIndex(
                name: "IX_Preek_VersTotId",
                table: "Preek",
                column: "VersTotId");

            migrationBuilder.CreateIndex(
                name: "IX_Preek_VersVanId",
                table: "Preek",
                column: "VersVanId");

            migrationBuilder.CreateIndex(
                name: "IX_PreekCookie_GebruikerId",
                table: "PreekCookie",
                column: "GebruikerId");

            migrationBuilder.CreateIndex(
                name: "IX_PreekCookie_PreekId",
                table: "PreekCookie",
                column: "PreekId");

            migrationBuilder.CreateIndex(
                name: "IX_PreekLezenEnZingen_PreekId",
                table: "PreekLezenEnZingen",
                column: "PreekId");

            migrationBuilder.CreateIndex(
                name: "IX_Serie_TaalId",
                table: "Serie",
                column: "TaalId");

            migrationBuilder.CreateIndex(
                name: "IX_Spotlight_AfbeeldingId",
                table: "Spotlight",
                column: "AfbeeldingId");

            migrationBuilder.CreateIndex(
                name: "IX_Spotlight_TaalId",
                table: "Spotlight",
                column: "TaalId");

            migrationBuilder.CreateIndex(
                name: "IX_Tekst_PaginaId",
                table: "Tekst",
                column: "PaginaId");

            migrationBuilder.CreateIndex(
                name: "IX_Tekst_TaalId",
                table: "Tekst",
                column: "TaalId");

            migrationBuilder.CreateIndex(
                name: "IX_ZoekOpdracht_GebruikerId",
                table: "ZoekOpdracht",
                column: "GebruikerId");

            migrationBuilder.CreateIndex(
                name: "UX_HangFire_CounterAggregated_Key",
                schema: "HangFire",
                table: "AggregatedCounter",
                columns: new[] { "Value", "Key" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_HangFire_Counter_Key",
                schema: "HangFire",
                table: "Counter",
                columns: new[] { "Value", "Key" });

            migrationBuilder.CreateIndex(
                name: "IX_HangFire_Hash_Key",
                schema: "HangFire",
                table: "Hash",
                columns: new[] { "ExpireAt", "Key" });

            migrationBuilder.CreateIndex(
                name: "IX_HangFire_Hash_ExpireAt",
                schema: "HangFire",
                table: "Hash",
                columns: new[] { "Id", "ExpireAt" });

            migrationBuilder.CreateIndex(
                name: "UX_HangFire_Hash_Key_Field",
                schema: "HangFire",
                table: "Hash",
                columns: new[] { "Key", "Field" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_HangFire_Job_StateName",
                schema: "HangFire",
                table: "Job",
                column: "StateName");

            migrationBuilder.CreateIndex(
                name: "IX_HangFire_Job_ExpireAt",
                schema: "HangFire",
                table: "Job",
                columns: new[] { "Id", "ExpireAt" });

            migrationBuilder.CreateIndex(
                name: "IX_HangFire_JobParameter_JobIdAndName",
                schema: "HangFire",
                table: "JobParameter",
                columns: new[] { "JobId", "Name" });

            migrationBuilder.CreateIndex(
                name: "IX_HangFire_JobQueue_QueueAndFetchedAt",
                schema: "HangFire",
                table: "JobQueue",
                columns: new[] { "Queue", "FetchedAt" });

            migrationBuilder.CreateIndex(
                name: "IX_HangFire_List_ExpireAt",
                schema: "HangFire",
                table: "List",
                columns: new[] { "Id", "ExpireAt" });

            migrationBuilder.CreateIndex(
                name: "IX_HangFire_List_Key",
                schema: "HangFire",
                table: "List",
                columns: new[] { "ExpireAt", "Value", "Key" });

            migrationBuilder.CreateIndex(
                name: "IX_HangFire_Set_ExpireAt",
                schema: "HangFire",
                table: "Set",
                columns: new[] { "Id", "ExpireAt" });

            migrationBuilder.CreateIndex(
                name: "UX_HangFire_Set_KeyAndValue",
                schema: "HangFire",
                table: "Set",
                columns: new[] { "Key", "Value" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_HangFire_Set_Key",
                schema: "HangFire",
                table: "Set",
                columns: new[] { "ExpireAt", "Value", "Key" });

            migrationBuilder.CreateIndex(
                name: "IX_HangFire_State_JobId",
                schema: "HangFire",
                table: "State",
                column: "JobId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "GebruikerMailing");

            migrationBuilder.DropTable(
                name: "InboxOpvolging");

            migrationBuilder.DropTable(
                name: "NieuwsbriefInschrijving");

            migrationBuilder.DropTable(
                name: "PreekCookie");

            migrationBuilder.DropTable(
                name: "PreekLezenEnZingen");

            migrationBuilder.DropTable(
                name: "SchemaVersions");

            migrationBuilder.DropTable(
                name: "Spotlight");

            migrationBuilder.DropTable(
                name: "Tekst");

            migrationBuilder.DropTable(
                name: "ZoekOpdracht");

            migrationBuilder.DropTable(
                name: "AggregatedCounter",
                schema: "HangFire");

            migrationBuilder.DropTable(
                name: "Counter",
                schema: "HangFire");

            migrationBuilder.DropTable(
                name: "Hash",
                schema: "HangFire");

            migrationBuilder.DropTable(
                name: "JobParameter",
                schema: "HangFire");

            migrationBuilder.DropTable(
                name: "JobQueue",
                schema: "HangFire");

            migrationBuilder.DropTable(
                name: "List",
                schema: "HangFire");

            migrationBuilder.DropTable(
                name: "Schema",
                schema: "HangFire");

            migrationBuilder.DropTable(
                name: "Server",
                schema: "HangFire");

            migrationBuilder.DropTable(
                name: "Set",
                schema: "HangFire");

            migrationBuilder.DropTable(
                name: "State",
                schema: "HangFire");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Mailing");

            migrationBuilder.DropTable(
                name: "Inbox");

            migrationBuilder.DropTable(
                name: "Pagina");

            migrationBuilder.DropTable(
                name: "Job",
                schema: "HangFire");

            migrationBuilder.DropTable(
                name: "InboxType");

            migrationBuilder.DropTable(
                name: "Preek");

            migrationBuilder.DropTable(
                name: "Gebruiker");

            migrationBuilder.DropTable(
                name: "Afbeelding");

            migrationBuilder.DropTable(
                name: "Gebeurtenis");

            migrationBuilder.DropTable(
                name: "BoekHoofdstukTekst");

            migrationBuilder.DropTable(
                name: "LezingCategorie");

            migrationBuilder.DropTable(
                name: "Predikant");

            migrationBuilder.DropTable(
                name: "PreekType");

            migrationBuilder.DropTable(
                name: "Serie");

            migrationBuilder.DropTable(
                name: "BoekHoofdstuk");

            migrationBuilder.DropTable(
                name: "Gemeente");

            migrationBuilder.DropTable(
                name: "Boek");

            migrationBuilder.DropTable(
                name: "Taal");
        }
    }
}
