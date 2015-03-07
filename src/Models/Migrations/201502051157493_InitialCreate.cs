namespace Prekenweb.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Afbeelding",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Omschrijving = c.String(nullable: false, maxLength: 255),
                        Bestandsnaam = c.String(nullable: false, maxLength: 255),
                        ContentType = c.String(nullable: false, maxLength: 255),
                    })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.Preek",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BoekhoofdstukId = c.Int(),
                        BijbeltekstOmschrijving = c.String(),
                        SerieId = c.Int(),
                        GebeurtenisId = c.Int(),
                        DatumAangemaakt = c.DateTime(defaultValueSql: "GETDATE()"),
                        DatumBijgewerkt = c.DateTime(defaultValueSql: "GETDATE()"),
                        Bestandsnaam = c.String(),
                        AantalKeerGedownload = c.Int(nullable: false, defaultValue: 0),
                        OudID = c.Int(),
                        PredikantId = c.Int(),
                        Hoofdstuk = c.Int(),
                        VanVers = c.String(maxLength: 255),
                        TotVers = c.String(maxLength: 255),
                        Punt1 = c.String(maxLength: 255),
                        Punt2 = c.String(maxLength: 255),
                        Punt3 = c.String(maxLength: 255),
                        Punt4 = c.String(maxLength: 255),
                        Punt5 = c.String(maxLength: 255),
                        GemeenteId = c.Int(),
                        DatumPreek = c.DateTime(),
                        Informatie = c.String(),
                        ThemaOmschrijving = c.String(),
                        AfbeeldingId = c.Int(),
                        PreekTypeId = c.Int(nullable: false),
                        LezingCategorieId = c.Int(),
                        TaalId = c.Int(nullable: false),
                        Gepubliceerd = c.Boolean(nullable: false, defaultValue: false),
                        LezingOmschrijving = c.String(),
                        Duur = c.Time(precision: 7),
                        Bestandsgrootte = c.Int(),
                        VersVanId = c.Int(),
                        VersTotId = c.Int(),
                        GedeelteVanVersId = c.Int(),
                        GedeelteTotVersId = c.Int(),
                        VersOmschrijving = c.String(maxLength: 50),
                        AutomatischeTeksten = c.Boolean(nullable: false, defaultValue: false),
                        AangemaaktDoor = c.Int(),
                        AangepastDoor = c.Int(),
                        LeesPreekTekst = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Afbeelding", t => t.AfbeeldingId)
                .ForeignKey("dbo.BoekHoofdstuk", t => t.BoekhoofdstukId)
                .ForeignKey("dbo.BoekHoofdstukTekst", t => t.GedeelteTotVersId)
                .ForeignKey("dbo.BoekHoofdstukTekst", t => t.GedeelteVanVersId)
                .ForeignKey("dbo.BoekHoofdstukTekst", t => t.VersTotId)
                .ForeignKey("dbo.BoekHoofdstukTekst", t => t.VersVanId)
                .ForeignKey("dbo.Gebeurtenis", t => t.GebeurtenisId)
                .ForeignKey("dbo.Gebruiker", t => t.AangemaaktDoor)
                .ForeignKey("dbo.Gebruiker", t => t.AangepastDoor)
                .ForeignKey("dbo.Gemeente", t => t.GemeenteId)
                .ForeignKey("dbo.LezingCategorie", t => t.LezingCategorieId)
                .ForeignKey("dbo.Predikant", t => t.PredikantId)
                .ForeignKey("dbo.PreekType", t => t.PreekTypeId, cascadeDelete: true)
                .ForeignKey("dbo.Serie", t => t.SerieId)
                .ForeignKey("dbo.Taal", t => t.TaalId, cascadeDelete: true)
                .Index(t => t.BoekhoofdstukId)
                .Index(t => t.SerieId)
                .Index(t => t.GebeurtenisId)
                .Index(t => t.PredikantId)
                .Index(t => t.GemeenteId)
                .Index(t => t.AfbeeldingId)
                .Index(t => t.PreekTypeId)
                .Index(t => t.LezingCategorieId)
                .Index(t => t.TaalId)
                .Index(t => t.VersVanId)
                .Index(t => t.VersTotId)
                .Index(t => t.GedeelteVanVersId)
                .Index(t => t.GedeelteTotVersId)
                .Index(t => t.AangemaaktDoor)
                .Index(t => t.AangepastDoor);

            CreateTable(
                "dbo.BoekHoofdstuk",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BoekId = c.Int(nullable: false),
                        Omschrijving = c.String(nullable: false, maxLength: 255),
                        Sortering = c.Int(),
                        OudId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Boek", t => t.BoekId, cascadeDelete: true)
                .Index(t => t.BoekId);

            CreateTable(
                "dbo.Boek",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Boeknaam = c.String(nullable: false, maxLength: 255),
                        Sortering = c.Int(nullable: false),
                        OudId = c.Int(),
                        Afkorting = c.String(maxLength: 50),
                        ToonHoofdstukNummer = c.Boolean(nullable: false),
                        TaalId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Taal", t => t.TaalId, cascadeDelete: true)
                .Index(t => t.TaalId);

            CreateTable(
                "dbo.Taal",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Code = c.String(nullable: false, maxLength: 10),
                        Omschrijving = c.String(nullable: false, maxLength: 255),
                    })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.Gebeurtenis",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Omschrijving = c.String(nullable: false, maxLength: 255),
                        OudId = c.Int(),
                        Sortering = c.Int(nullable: false),
                        TaalId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Taal", t => t.TaalId, cascadeDelete: true)
                .Index(t => t.TaalId);

            CreateTable(
                "dbo.Mailing",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Omschrijving = c.String(nullable: false, maxLength: 255),
                        MailChimpId = c.String(nullable: false, maxLength: 255),
                        TaalId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Taal", t => t.TaalId, cascadeDelete: true)
                .Index(t => t.TaalId);

            CreateTable(
                "dbo.Gebruiker",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Naam = c.String(nullable: false, maxLength: 255),
                        LaatstIngelogd = c.DateTime(),
                        Email = c.String(),
                        EmailConfirmed = c.Boolean(nullable: false, defaultValue: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false, defaultValue: false),
                        TwoFactorEnabled = c.Boolean(nullable: false, defaultValue: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false, defaultValue: false),
                        AccessFailedCount = c.Int(nullable: false, defaultValue: 0),
                        UserName = c.String(),
                    })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                        Gebruiker_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Gebruiker", t => t.Gebruiker_Id)
                .Index(t => t.Gebruiker_Id);

            CreateTable(
                "dbo.Inbox",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        InboxTypeId = c.Int(nullable: false),
                        Omschrijving = c.String(nullable: false, maxLength: 255),
                        Inhoud = c.String(nullable: false),
                        GebruikerId = c.Int(),
                        Afgehandeld = c.Boolean(nullable: false, defaultValue: false),
                        Aangemaakt = c.DateTime(nullable: false, defaultValueSql: "GETDATE()"),
                        VanNaam = c.String(nullable: false, maxLength: 255),
                        VanEmail = c.String(nullable: false, maxLength: 255),
                        AanNaam = c.String(nullable: false, maxLength: 255),
                        AanEmail = c.String(nullable: false, maxLength: 255),
                        PreekId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Gebruiker", t => t.GebruikerId)
                .ForeignKey("dbo.InboxType", t => t.InboxTypeId, cascadeDelete: true)
                .ForeignKey("dbo.Preek", t => t.PreekId)
                .Index(t => t.InboxTypeId)
                .Index(t => t.GebruikerId)
                .Index(t => t.PreekId);

            CreateTable(
                "dbo.InboxOpvolging",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        InboxId = c.Int(nullable: false),
                        GebruikerId = c.Int(),
                        Aangemaakt = c.DateTime(nullable: false),
                        Onderwerp = c.String(),
                        Tekst = c.String(),
                        VerstuurAlsMail = c.Boolean(nullable: false),
                        Verstuurd = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Gebruiker", t => t.GebruikerId)
                .ForeignKey("dbo.Inbox", t => t.InboxId, cascadeDelete: true)
                .Index(t => t.InboxId)
                .Index(t => t.GebruikerId);

            CreateTable(
                "dbo.InboxType",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Omschrijving = c.String(nullable: false, maxLength: 255),
                    })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.Int(nullable: false),
                        Gebruiker_Id = c.Int(),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.Gebruiker", t => t.Gebruiker_Id)
                .Index(t => t.Gebruiker_Id);

            CreateTable(
                "dbo.Pagina",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Gepubliceerd = c.Boolean(nullable: false, defaultValue: false),
                        Identifier = c.String(nullable: false, maxLength: 255),
                        Aangemaakt = c.DateTime(nullable: false, defaultValueSql: "GETDATE()"),
                        Bijgewerkt = c.DateTime(nullable: false, defaultValueSql: "GETDATE()"),
                        AangemaaktDoor = c.Int(nullable: false),
                        BijgewerktDoor = c.Int(nullable: false),
                        TonenOpHomepage = c.Boolean(nullable: false, defaultValue: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Gebruiker", t => t.AangemaaktDoor, cascadeDelete: false)
                .ForeignKey("dbo.Gebruiker", t => t.BijgewerktDoor, cascadeDelete: false)
                .Index(t => t.AangemaaktDoor)
                .Index(t => t.BijgewerktDoor);

            CreateTable(
                "dbo.Tekst",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Kop = c.String(nullable: false, maxLength: 255),
                        Tekst = c.String(nullable: false),
                        TaalId = c.Int(nullable: false),
                        PaginaId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Pagina", t => t.PaginaId)
                .ForeignKey("dbo.Taal", t => t.TaalId, cascadeDelete: true)
                .Index(t => t.TaalId)
                .Index(t => t.PaginaId);

            CreateTable(
                "dbo.PreekCookie",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PreekId = c.Int(nullable: false),
                        DateTime = c.DateTime(),
                        Opmerking = c.String(),
                        BladwijzerGelegdOp = c.DateTime(),
                        AfgespeeldTot = c.Time(precision: 7),
                        GebruikerId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Gebruiker", t => t.GebruikerId, cascadeDelete: true)
                .ForeignKey("dbo.Preek", t => t.PreekId, cascadeDelete: true)
                .Index(t => t.PreekId)
                .Index(t => t.GebruikerId);

            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.Int(nullable: false),
                        RoleId = c.Int(nullable: false),
                        Gebruiker_Id = c.Int(),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.Gebruiker", t => t.Gebruiker_Id)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.RoleId)
                .Index(t => t.Gebruiker_Id);

            CreateTable(
                "dbo.ZoekOpdracht",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PubliekeSleutel = c.Guid(nullable: false, defaultValueSql: "newid()"),
                        LeesPreken = c.Boolean(nullable: false),
                        AudioPreken = c.Boolean(nullable: false),
                        Lezingen = c.Boolean(nullable: false),
                        PredikantId = c.Int(),
                        Predikant = c.String(),
                        BoekHoofdstukId = c.Int(),
                        BoekHoofdstuk = c.String(),
                        BoekId = c.Int(),
                        Boek = c.String(),
                        LezingCategorieId = c.Int(),
                        LezingCategorie = c.String(),
                        Hoofdstuk = c.Int(),
                        GebeurtenisId = c.Int(),
                        Gebeurtenis = c.String(),
                        GemeenteId = c.Int(),
                        Gemeente = c.String(),
                        SerieId = c.Int(),
                        Serie = c.String(),
                        TaalId = c.Int(nullable: false),
                        GebruikerId = c.Int(nullable: false),
                        Zoekterm = c.String(),
                        SorteerOp = c.Int(),
                        SorteerVolgorde = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Gebruiker", t => t.GebruikerId, cascadeDelete: true)
                .Index(t => t.GebruikerId);

            CreateTable(
                "dbo.NieuwsbriefInschrijving",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Naam = c.String(nullable: false, maxLength: 255),
                        Email = c.String(nullable: false, maxLength: 255),
                        Aangemeld = c.DateTime(nullable: false),
                        Afgemeld = c.DateTime(),
                        TaalId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Taal", t => t.TaalId, cascadeDelete: true)
                .Index(t => t.TaalId);

            CreateTable(
                "dbo.Predikant",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Titels = c.String(maxLength: 50),
                        Voorletters = c.String(maxLength: 50),
                        Achternaam = c.String(nullable: false, maxLength: 255),
                        Gemeente = c.String(maxLength: 255),
                        LevensPeriode = c.String(maxLength: 255),
                        OudID = c.Int(),
                        GemeenteId = c.Int(),
                        Tussenvoegsels = c.String(maxLength: 50),
                        Opmerking = c.String(),
                        TaalId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Gemeente", t => t.GemeenteId)
                .ForeignKey("dbo.Taal", t => t.TaalId, cascadeDelete: true)
                .Index(t => t.GemeenteId)
                .Index(t => t.TaalId);

            CreateTable(
                "dbo.Gemeente",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Omschrijving = c.String(maxLength: 255),
                    })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.Serie",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Omschrijving = c.String(nullable: false, maxLength: 255),
                        OudId = c.Int(),
                        TaalId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Taal", t => t.TaalId, cascadeDelete: true)
                .Index(t => t.TaalId);

            CreateTable(
                "dbo.Spotlight",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Titel = c.String(nullable: false, maxLength: 255),
                        Subtitel = c.String(nullable: false, maxLength: 255),
                        LinkTitel = c.String(nullable: false, maxLength: 255),
                        Url = c.String(nullable: false),
                        AfbeeldingId = c.Int(nullable: false),
                        Sortering = c.Int(nullable: false),
                        TaalId = c.Int(nullable: false),
                        NieuwVenster = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Afbeelding", t => t.AfbeeldingId, cascadeDelete: true)
                .ForeignKey("dbo.Taal", t => t.TaalId, cascadeDelete: true)
                .Index(t => t.AfbeeldingId)
                .Index(t => t.TaalId);

            CreateTable(
                "dbo.BoekHoofdstukTekst",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BoekHoofdstukId = c.Int(nullable: false),
                        Hoofdstuk = c.Int(nullable: false),
                        Vers = c.Int(nullable: false),
                        Tekst = c.String(nullable: false),
                        Sortering = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.BoekHoofdstuk", t => t.BoekHoofdstukId, cascadeDelete: true)
                .Index(t => t.BoekHoofdstukId);

            CreateTable(
                "dbo.LezingCategorie",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Omschrijving = c.String(nullable: false, maxLength: 255),
                        OudId = c.Int(),
                    })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.PreekLezenEnZingen",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PreekId = c.Int(nullable: false),
                        Sortering = c.Byte(nullable: false),
                        Soort = c.String(maxLength: 255),
                        Omschrijving = c.String(maxLength: 255),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Preek", t => t.PreekId, cascadeDelete: true)
                .Index(t => t.PreekId);

            CreateTable(
                "dbo.PreekType",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Omschrijving = c.String(nullable: false, maxLength: 255),
                    })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.ELMAH_Error",
                c => new
                    {
                        ErrorId = c.Guid(nullable: false),
                        Application = c.String(nullable: false, maxLength: 60),
                        Host = c.String(nullable: false, maxLength: 50),
                        Type = c.String(nullable: false, maxLength: 100),
                        Source = c.String(nullable: false, maxLength: 60),
                        Message = c.String(nullable: false, maxLength: 500),
                        User = c.String(nullable: false, maxLength: 50),
                        StatusCode = c.Int(nullable: false),
                        TimeUtc = c.DateTime(nullable: false),
                        Sequence = c.Int(nullable: false, identity: true),
                        AllXml = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ErrorId);

            CreateTable(
                "dbo.__RefactorLog",
                c => new
                    {
                        OperationKey = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.OperationKey);

            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");

            Sql("INSERT INTO dbo.AspNetRoles (Name) VALUES('Gebruikers')");
            Sql("INSERT INTO dbo.AspNetRoles (Name) VALUES('PreekToevoegen')");
            Sql("INSERT INTO dbo.AspNetRoles (Name) VALUES('PreekFiatteren')");
            Sql("INSERT INTO dbo.AspNetRoles (Name) VALUES('Spotlight')");
            Sql("INSERT INTO dbo.AspNetRoles (Name) VALUES('Pagina')");
            Sql("INSERT INTO dbo.AspNetRoles (Name) VALUES('Nieuwsbrief')");
            Sql("INSERT INTO dbo.AspNetRoles (Name) VALUES('Stamgegevens')");
            Sql("INSERT INTO dbo.AspNetRoles (Name) VALUES('Inbox')");
            Sql("INSERT INTO dbo.AspNetRoles (Name) VALUES('PrekenVanAnderenBewerken')");
            Sql("INSERT INTO dbo.AspNetRoles (Name) VALUES('Bestandsbeheer')");
            Sql("INSERT INTO dbo.AspNetRoles (Name) VALUES('BestandsbeheerPrekenWeb')");

            CreateTable(
                "dbo.GebruikerMailing",
                c => new
                    {
                        GebruikerId = c.Int(nullable: false),
                        MailingId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.GebruikerId, t.MailingId })
                .ForeignKey("dbo.Gebruiker", t => t.GebruikerId, cascadeDelete: true)
                .ForeignKey("dbo.Mailing", t => t.MailingId, cascadeDelete: true)
                .Index(t => t.GebruikerId)
                .Index(t => t.MailingId);

            CreateStoredProcedure("ELMAH_GetErrorsXml", c => new
            {
                Application = c.String(maxLength: 60),
                PageIndex = c.Int(defaultValue: 0),
                PageSize = c.Int(defaultValue: 0),
                TotalCount = c.Int(outParameter: true)
            }, @"  
                    SET NOCOUNT ON

                    DECLARE @FirstTimeUTC DATETIME
                    DECLARE @FirstSequence INT
                    DECLARE @StartRow INT
                    DECLARE @StartRowIndex INT

                    SELECT 
                        @TotalCount = COUNT(1) 
                    FROM 
                        [ELMAH_Error]
                    WHERE 
                        [Application] = @Application

                    -- Get the ID of the first error for the requested page

                    SET @StartRowIndex = @PageIndex * @PageSize + 1

                    IF @StartRowIndex <= @TotalCount
                    BEGIN

                        SET ROWCOUNT @StartRowIndex

                        SELECT  
                            @FirstTimeUTC = [TimeUtc],
                            @FirstSequence = [Sequence]
                        FROM 
                            [ELMAH_Error]
                        WHERE   
                            [Application] = @Application
                        ORDER BY 
                            [TimeUtc] DESC, 
                            [Sequence] DESC

                    END
                    ELSE
                    BEGIN

                        SET @PageSize = 0

                    END

                    -- Now set the row count to the requested page size and get
                    -- all records below it for the pertaining application.

                    SET ROWCOUNT @PageSize

                    SELECT 
                        errorId     = [ErrorId], 
                        application = [Application],
                        host        = [Host], 
                        type        = [Type],
                        source      = [Source],
                        message     = [Message],
                        [user]      = [User],
                        statusCode  = [StatusCode], 
                        time        = CONVERT(VARCHAR(50), [TimeUtc], 126) + 'Z'
                    FROM 
                        [ELMAH_Error] error
                    WHERE
                        [Application] = @Application
                    AND
                        [TimeUtc] <= @FirstTimeUTC
                    AND 
                        [Sequence] <= @FirstSequence
                    ORDER BY
                        [TimeUtc] DESC, 
                        [Sequence] DESC
                    FOR
                        XML AUTO ");
            CreateStoredProcedure("ELMAH_GetErrorXml", c => new
            {
                Application = c.String(maxLength: 60),
                ErrorId = c.Guid() 
            }, @" 
                    SET NOCOUNT ON

                    SELECT 
                        [AllXml]
                    FROM 
                        [ELMAH_Error]
                    WHERE
                        [ErrorId] = @ErrorId
                    AND
                        [Application] = @Application  ");

            CreateStoredProcedure("ELMAH_LogError", c => new
            {
                ErrorId = c.Guid(),
                Application = c.String(maxLength: 60),
                Host = c.String(maxLength: 30),
                Type = c.String(maxLength: 100),
                Source = c.String(maxLength: 60),
                Message = c.String(maxLength: 500),
                User = c.String(maxLength: 50),
                AllXml = c.String(storeType:"NTEXT"),
                StatusCode = c.Int(),
                TimeUtc = c.DateTime()
            }, @"   
                    SET NOCOUNT ON

                    INSERT
                    INTO
                        [ELMAH_Error]
                        (
                            [ErrorId],
                            [Application],
                            [Host],
                            [Type],
                            [Source],
                            [Message],
                            [User],
                            [AllXml],
                            [StatusCode],
                            [TimeUtc]
                        )
                    VALUES
                        (
                            @ErrorId,
                            @Application,
                            @Host,
                            @Type,
                            @Source,
                            @Message,
                            @User,
                            @AllXml,
                            @StatusCode,
                            @TimeUtc
                        ) "); 
        }

        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Preek", "TaalId", "dbo.Taal");
            DropForeignKey("dbo.Preek", "SerieId", "dbo.Serie");
            DropForeignKey("dbo.Preek", "PreekTypeId", "dbo.PreekType");
            DropForeignKey("dbo.PreekLezenEnZingen", "PreekId", "dbo.Preek");
            DropForeignKey("dbo.Preek", "PredikantId", "dbo.Predikant");
            DropForeignKey("dbo.Preek", "LezingCategorieId", "dbo.LezingCategorie");
            DropForeignKey("dbo.Preek", "GemeenteId", "dbo.Gemeente");
            DropForeignKey("dbo.Preek", "AangepastDoor", "dbo.Gebruiker");
            DropForeignKey("dbo.Preek", "AangemaaktDoor", "dbo.Gebruiker");
            DropForeignKey("dbo.Preek", "GebeurtenisId", "dbo.Gebeurtenis");
            DropForeignKey("dbo.Preek", "VersVanId", "dbo.BoekHoofdstukTekst");
            DropForeignKey("dbo.Preek", "VersTotId", "dbo.BoekHoofdstukTekst");
            DropForeignKey("dbo.Preek", "GedeelteVanVersId", "dbo.BoekHoofdstukTekst");
            DropForeignKey("dbo.Preek", "GedeelteTotVersId", "dbo.BoekHoofdstukTekst");
            DropForeignKey("dbo.Preek", "BoekhoofdstukId", "dbo.BoekHoofdstuk");
            DropForeignKey("dbo.BoekHoofdstukTekst", "BoekHoofdstukId", "dbo.BoekHoofdstuk");
            DropForeignKey("dbo.BoekHoofdstuk", "BoekId", "dbo.Boek");
            DropForeignKey("dbo.Boek", "TaalId", "dbo.Taal");
            DropForeignKey("dbo.Spotlight", "TaalId", "dbo.Taal");
            DropForeignKey("dbo.Spotlight", "AfbeeldingId", "dbo.Afbeelding");
            DropForeignKey("dbo.Serie", "TaalId", "dbo.Taal");
            DropForeignKey("dbo.Predikant", "TaalId", "dbo.Taal");
            DropForeignKey("dbo.Predikant", "GemeenteId", "dbo.Gemeente");
            DropForeignKey("dbo.NieuwsbriefInschrijving", "TaalId", "dbo.Taal");
            DropForeignKey("dbo.Mailing", "TaalId", "dbo.Taal");
            DropForeignKey("dbo.ZoekOpdracht", "GebruikerId", "dbo.Gebruiker");
            DropForeignKey("dbo.AspNetUserRoles", "Gebruiker_Id", "dbo.Gebruiker");
            DropForeignKey("dbo.PreekCookie", "PreekId", "dbo.Preek");
            DropForeignKey("dbo.PreekCookie", "GebruikerId", "dbo.Gebruiker");
            DropForeignKey("dbo.Tekst", "TaalId", "dbo.Taal");
            DropForeignKey("dbo.Tekst", "PaginaId", "dbo.Pagina");
            DropForeignKey("dbo.Pagina", "BijgewerktDoor", "dbo.Gebruiker");
            DropForeignKey("dbo.Pagina", "AangemaaktDoor", "dbo.Gebruiker");
            DropForeignKey("dbo.GebruikerMailing", "MailingId", "dbo.Mailing");
            DropForeignKey("dbo.GebruikerMailing", "GebruikerId", "dbo.Gebruiker");
            DropForeignKey("dbo.AspNetUserLogins", "Gebruiker_Id", "dbo.Gebruiker");
            DropForeignKey("dbo.Inbox", "PreekId", "dbo.Preek");
            DropForeignKey("dbo.Inbox", "InboxTypeId", "dbo.InboxType");
            DropForeignKey("dbo.InboxOpvolging", "InboxId", "dbo.Inbox");
            DropForeignKey("dbo.InboxOpvolging", "GebruikerId", "dbo.Gebruiker");
            DropForeignKey("dbo.Inbox", "GebruikerId", "dbo.Gebruiker");
            DropForeignKey("dbo.AspNetUserClaims", "Gebruiker_Id", "dbo.Gebruiker");
            DropForeignKey("dbo.Gebeurtenis", "TaalId", "dbo.Taal");
            DropForeignKey("dbo.Preek", "AfbeeldingId", "dbo.Afbeelding");
            DropIndex("dbo.GebruikerMailing", new[] { "MailingId" });
            DropIndex("dbo.GebruikerMailing", new[] { "GebruikerId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.PreekLezenEnZingen", new[] { "PreekId" });
            DropIndex("dbo.BoekHoofdstukTekst", new[] { "BoekHoofdstukId" });
            DropIndex("dbo.Spotlight", new[] { "TaalId" });
            DropIndex("dbo.Spotlight", new[] { "AfbeeldingId" });
            DropIndex("dbo.Serie", new[] { "TaalId" });
            DropIndex("dbo.Predikant", new[] { "TaalId" });
            DropIndex("dbo.Predikant", new[] { "GemeenteId" });
            DropIndex("dbo.NieuwsbriefInschrijving", new[] { "TaalId" });
            DropIndex("dbo.ZoekOpdracht", new[] { "GebruikerId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "Gebruiker_Id" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.PreekCookie", new[] { "GebruikerId" });
            DropIndex("dbo.PreekCookie", new[] { "PreekId" });
            DropIndex("dbo.Tekst", new[] { "PaginaId" });
            DropIndex("dbo.Tekst", new[] { "TaalId" });
            DropIndex("dbo.Pagina", new[] { "BijgewerktDoor" });
            DropIndex("dbo.Pagina", new[] { "AangemaaktDoor" });
            DropIndex("dbo.AspNetUserLogins", new[] { "Gebruiker_Id" });
            DropIndex("dbo.InboxOpvolging", new[] { "GebruikerId" });
            DropIndex("dbo.InboxOpvolging", new[] { "InboxId" });
            DropIndex("dbo.Inbox", new[] { "PreekId" });
            DropIndex("dbo.Inbox", new[] { "GebruikerId" });
            DropIndex("dbo.Inbox", new[] { "InboxTypeId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "Gebruiker_Id" });
            DropIndex("dbo.Mailing", new[] { "TaalId" });
            DropIndex("dbo.Gebeurtenis", new[] { "TaalId" });
            DropIndex("dbo.Boek", new[] { "TaalId" });
            DropIndex("dbo.BoekHoofdstuk", new[] { "BoekId" });
            DropIndex("dbo.Preek", new[] { "AangepastDoor" });
            DropIndex("dbo.Preek", new[] { "AangemaaktDoor" });
            DropIndex("dbo.Preek", new[] { "GedeelteTotVersId" });
            DropIndex("dbo.Preek", new[] { "GedeelteVanVersId" });
            DropIndex("dbo.Preek", new[] { "VersTotId" });
            DropIndex("dbo.Preek", new[] { "VersVanId" });
            DropIndex("dbo.Preek", new[] { "TaalId" });
            DropIndex("dbo.Preek", new[] { "LezingCategorieId" });
            DropIndex("dbo.Preek", new[] { "PreekTypeId" });
            DropIndex("dbo.Preek", new[] { "AfbeeldingId" });
            DropIndex("dbo.Preek", new[] { "GemeenteId" });
            DropIndex("dbo.Preek", new[] { "PredikantId" });
            DropIndex("dbo.Preek", new[] { "GebeurtenisId" });
            DropIndex("dbo.Preek", new[] { "SerieId" });
            DropIndex("dbo.Preek", new[] { "BoekhoofdstukId" });
            DropTable("dbo.GebruikerMailing");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.__RefactorLog");
            DropTable("dbo.ELMAH_Error");
            DropTable("dbo.PreekType");
            DropTable("dbo.PreekLezenEnZingen");
            DropTable("dbo.LezingCategorie");
            DropTable("dbo.BoekHoofdstukTekst");
            DropTable("dbo.Spotlight");
            DropTable("dbo.Serie");
            DropTable("dbo.Gemeente");
            DropTable("dbo.Predikant");
            DropTable("dbo.NieuwsbriefInschrijving");
            DropTable("dbo.ZoekOpdracht");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.PreekCookie");
            DropTable("dbo.Tekst");
            DropTable("dbo.Pagina");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.InboxType");
            DropTable("dbo.InboxOpvolging");
            DropTable("dbo.Inbox");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.Gebruiker");
            DropTable("dbo.Mailing");
            DropTable("dbo.Gebeurtenis");
            DropTable("dbo.Taal");
            DropTable("dbo.Boek");
            DropTable("dbo.BoekHoofdstuk");
            DropTable("dbo.Preek");
            DropTable("dbo.Afbeelding");
        }
    }
}
