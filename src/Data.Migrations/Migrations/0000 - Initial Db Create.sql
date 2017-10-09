IF OBJECT_ID(N'dbo.Afbeelding', N'U') IS NOT NULL
BEGIN
	GOTO Done
END

CREATE TABLE [dbo].[Afbeelding](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Omschrijving] [nvarchar](255) NOT NULL,
	[Bestandsnaam] [nvarchar](255) NOT NULL,
	[ContentType] [nvarchar](255) NOT NULL,
 CONSTRAINT [PK_dbo.Afbeelding] PRIMARY KEY CLUSTERED
(
	[Id] ASC
)
) ON [PRIMARY]

CREATE TABLE [dbo].[AspNetRoles](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](256) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetRoles] PRIMARY KEY CLUSTERED
(
	[Id] ASC
)
) ON [PRIMARY]


CREATE TABLE [dbo].[AspNetUserClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
	[Gebruiker_Id] [int] NULL,
 CONSTRAINT [PK_dbo.AspNetUserClaims] PRIMARY KEY CLUSTERED
(
	[Id] ASC
)
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]


CREATE TABLE [dbo].[AspNetUserLogins](
	[LoginProvider] [nvarchar](128) NOT NULL,
	[ProviderKey] [nvarchar](128) NOT NULL,
	[UserId] [int] NOT NULL,
	[Gebruiker_Id] [int] NULL,
 CONSTRAINT [PK_dbo.AspNetUserLogins] PRIMARY KEY CLUSTERED
(
	[LoginProvider] ASC,
	[ProviderKey] ASC,
	[UserId] ASC
)
) ON [PRIMARY]


CREATE TABLE [dbo].[AspNetUserRoles](
	[UserId] [int] NOT NULL,
	[RoleId] [int] NOT NULL,
	[Gebruiker_Id] [int] NULL,
 CONSTRAINT [PK_dbo.AspNetUserRoles] PRIMARY KEY CLUSTERED
(
	[UserId] ASC,
	[RoleId] ASC
)
) ON [PRIMARY]


CREATE TABLE [dbo].[Boek](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Boeknaam] [nvarchar](255) NOT NULL,
	[Sortering] [int] NOT NULL,
	[OudId] [int] NULL,
	[Afkorting] [nvarchar](50) NULL,
	[ToonHoofdstukNummer] [bit] NOT NULL,
	[TaalId] [int] NOT NULL,
 CONSTRAINT [PK_dbo.Boek] PRIMARY KEY CLUSTERED
(
	[Id] ASC
)
) ON [PRIMARY]


CREATE TABLE [dbo].[BoekHoofdstuk](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[BoekId] [int] NOT NULL,
	[Omschrijving] [nvarchar](255) NOT NULL,
	[Sortering] [int] NULL,
	[OudId] [int] NULL,
 CONSTRAINT [PK_dbo.BoekHoofdstuk] PRIMARY KEY CLUSTERED
(
	[Id] ASC
)
) ON [PRIMARY]



CREATE TABLE [dbo].[BoekHoofdstukTekst](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[BoekHoofdstukId] [int] NOT NULL,
	[Hoofdstuk] [int] NOT NULL,
	[Vers] [int] NOT NULL,
	[Tekst] [nvarchar](max) NOT NULL,
	[Sortering] [int] NOT NULL,
 CONSTRAINT [PK_dbo.BoekHoofdstukTekst] PRIMARY KEY CLUSTERED
(
	[Id] ASC
)
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]


CREATE TABLE [dbo].[Gebeurtenis](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Omschrijving] [nvarchar](255) NOT NULL,
	[OudId] [int] NULL,
	[Sortering] [int] NOT NULL,
	[TaalId] [int] NOT NULL,
 CONSTRAINT [PK_dbo.Gebeurtenis] PRIMARY KEY CLUSTERED
(
	[Id] ASC
)
) ON [PRIMARY]

CREATE TABLE [dbo].[Gebruiker](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Naam] [nvarchar](255) NOT NULL,
	[LaatstIngelogd] [datetime] NULL,
	[Email] [nvarchar](max) NULL,
	[EmailConfirmed] [bit] NOT NULL DEFAULT ((0)),
	[PasswordHash] [nvarchar](max) NULL,
	[SecurityStamp] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	[PhoneNumberConfirmed] [bit] NOT NULL DEFAULT ((0)),
	[TwoFactorEnabled] [bit] NOT NULL DEFAULT ((0)),
	[LockoutEndDateUtc] [datetime] NULL,
	[LockoutEnabled] [bit] NOT NULL DEFAULT ((0)),
	[AccessFailedCount] [int] NOT NULL DEFAULT ((0)),
	[UserName] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.Gebruiker] PRIMARY KEY CLUSTERED
(
	[Id] ASC
)
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]


CREATE TABLE [dbo].[GebruikerMailing](
	[GebruikerId] [int] NOT NULL,
	[MailingId] [int] NOT NULL,
 CONSTRAINT [PK_dbo.GebruikerMailing] PRIMARY KEY CLUSTERED
(
	[GebruikerId] ASC,
	[MailingId] ASC
)
) ON [PRIMARY]


CREATE TABLE [dbo].[Gemeente](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Omschrijving] [nvarchar](255) NULL,
 CONSTRAINT [PK_dbo.Gemeente] PRIMARY KEY CLUSTERED
(
	[Id] ASC
)
) ON [PRIMARY]


CREATE TABLE [dbo].[Inbox](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[InboxTypeId] [int] NOT NULL,
	[Omschrijving] [nvarchar](255) NOT NULL,
	[Inhoud] [nvarchar](max) NOT NULL,
	[GebruikerId] [int] NULL,
	[Afgehandeld] [bit] NOT NULL,
	[Aangemaakt] [datetime] NOT NULL,
	[VanNaam] [nvarchar](255) NOT NULL,
	[VanEmail] [nvarchar](255) NOT NULL,
	[AanNaam] [nvarchar](255) NOT NULL,
	[AanEmail] [nvarchar](255) NOT NULL,
	[PreekId] [int] NULL,
 CONSTRAINT [PK_dbo.Inbox] PRIMARY KEY CLUSTERED
(
	[Id] ASC
)
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

CREATE TABLE [dbo].[InboxOpvolging](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[InboxId] [int] NOT NULL,
	[GebruikerId] [int] NULL,
	[Aangemaakt] [datetime] NOT NULL,
	[Onderwerp] [nvarchar](max) NULL,
	[Tekst] [nvarchar](max) NULL,
	[VerstuurAlsMail] [bit] NOT NULL,
	[Verstuurd] [datetime] NULL,
 CONSTRAINT [PK_dbo.InboxOpvolging] PRIMARY KEY CLUSTERED
(
	[Id] ASC
)
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

CREATE TABLE [dbo].[InboxType](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Omschrijving] [nvarchar](255) NOT NULL,
 CONSTRAINT [PK_dbo.InboxType] PRIMARY KEY CLUSTERED
(
	[Id] ASC
)
) ON [PRIMARY]

CREATE TABLE [dbo].[LezingCategorie](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Omschrijving] [nvarchar](255) NOT NULL,
	[OudId] [int] NULL,
 CONSTRAINT [PK_dbo.LezingCategorie] PRIMARY KEY CLUSTERED
(
	[Id] ASC
)
) ON [PRIMARY]

CREATE TABLE [dbo].[Mailing](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Omschrijving] [nvarchar](255) NOT NULL,
	[MailChimpId] [nvarchar](255) NOT NULL,
	[TaalId] [int] NOT NULL,
 CONSTRAINT [PK_dbo.Mailing] PRIMARY KEY CLUSTERED
(
	[Id] ASC
)
) ON [PRIMARY]

CREATE TABLE [dbo].[NieuwsbriefInschrijving](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Naam] [nvarchar](255) NOT NULL,
	[Email] [nvarchar](255) NOT NULL,
	[Aangemeld] [datetime] NOT NULL,
	[Afgemeld] [datetime] NULL,
	[TaalId] [int] NOT NULL,
 CONSTRAINT [PK_dbo.NieuwsbriefInschrijving] PRIMARY KEY CLUSTERED
(
	[Id] ASC
)
) ON [PRIMARY]

CREATE TABLE [dbo].[Pagina](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Gepubliceerd] [bit] NOT NULL DEFAULT ((0)),
	[Identifier] [nvarchar](255) NOT NULL,
	[Aangemaakt] [datetime] NOT NULL DEFAULT (getdate()),
	[Bijgewerkt] [datetime] NOT NULL DEFAULT (getdate()),
	[AangemaaktDoor] [int] NOT NULL,
	[BijgewerktDoor] [int] NOT NULL,
	[TonenOpHomepage] [bit] NOT NULL DEFAULT ((0)),
 CONSTRAINT [PK_dbo.Pagina] PRIMARY KEY CLUSTERED
(
	[Id] ASC
)
) ON [PRIMARY]


CREATE TABLE [dbo].[Predikant](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Titels] [nvarchar](50) NULL,
	[Voorletters] [nvarchar](50) NULL,
	[Achternaam] [nvarchar](255) NOT NULL,
	[Gemeente] [nvarchar](255) NULL,
	[LevensPeriode] [nvarchar](255) NULL,
	[OudID] [int] NULL,
	[GemeenteId] [int] NULL,
	[Tussenvoegsels] [nvarchar](50) NULL,
	[Opmerking] [nvarchar](max) NULL,
	[TaalId] [int] NOT NULL,
 CONSTRAINT [PK_dbo.Predikant] PRIMARY KEY CLUSTERED
(
	[Id] ASC
)
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

CREATE TABLE [dbo].[Preek](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[BoekhoofdstukId] [int] NULL,
	[BijbeltekstOmschrijving] [nvarchar](max) NULL,
	[SerieId] [int] NULL,
	[GebeurtenisId] [int] NULL,
	[DatumAangemaakt] [datetime] NULL DEFAULT (getdate()),
	[DatumBijgewerkt] [datetime] NULL DEFAULT (getdate()),
	[Bestandsnaam] [nvarchar](max) NULL,
	[AantalKeerGedownload] [int] NOT NULL DEFAULT ((0)),
	[OudID] [int] NULL,
	[PredikantId] [int] NULL,
	[Hoofdstuk] [int] NULL,
	[VanVers] [nvarchar](255) NULL,
	[TotVers] [nvarchar](255) NULL,
	[Punt1] [nvarchar](255) NULL,
	[Punt2] [nvarchar](255) NULL,
	[Punt3] [nvarchar](255) NULL,
	[Punt4] [nvarchar](255) NULL,
	[Punt5] [nvarchar](255) NULL,
	[GemeenteId] [int] NULL,
	[DatumPreek] [datetime] NULL,
	[Informatie] [nvarchar](max) NULL,
	[ThemaOmschrijving] [nvarchar](max) NULL,
	[AfbeeldingId] [int] NULL,
	[PreekTypeId] [int] NOT NULL,
	[LezingCategorieId] [int] NULL,
	[TaalId] [int] NOT NULL,
	[Gepubliceerd] [bit] NOT NULL DEFAULT ((0)),
	[LezingOmschrijving] [nvarchar](max) NULL,
	[Duur] [time](7) NULL,
	[Bestandsgrootte] [int] NULL,
	[VersVanId] [int] NULL,
	[VersTotId] [int] NULL,
	[GedeelteVanVersId] [int] NULL,
	[GedeelteTotVersId] [int] NULL,
	[VersOmschrijving] [nvarchar](50) NULL,
	[AutomatischeTeksten] [bit] NOT NULL DEFAULT ((0)),
	[AangemaaktDoor] [int] NULL,
	[AangepastDoor] [int] NULL,
	[LeesPreekTekst] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.Preek] PRIMARY KEY CLUSTERED
(
	[Id] ASC
)
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

CREATE TABLE [dbo].[PreekCookie](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PreekId] [int] NOT NULL,
	[DateTime] [datetime] NULL,
	[Opmerking] [nvarchar](max) NULL,
	[BladwijzerGelegdOp] [datetime] NULL,
	[AfgespeeldTot] [time](7) NULL,
	[GebruikerId] [int] NOT NULL,
 CONSTRAINT [PK_dbo.PreekCookie] PRIMARY KEY CLUSTERED
(
	[Id] ASC
)
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

CREATE TABLE [dbo].[PreekLezenEnZingen](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PreekId] [int] NOT NULL,
	[Sortering] [tinyint] NOT NULL,
	[Soort] [nvarchar](255) NULL,
	[Omschrijving] [nvarchar](255) NULL,
 CONSTRAINT [PK_dbo.PreekLezenEnZingen] PRIMARY KEY CLUSTERED
(
	[Id] ASC
)
) ON [PRIMARY]

CREATE TABLE [dbo].[PreekType](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Omschrijving] [nvarchar](255) NOT NULL,
 CONSTRAINT [PK_dbo.PreekType] PRIMARY KEY CLUSTERED
(
	[Id] ASC
)
) ON [PRIMARY]

CREATE TABLE [dbo].[SchemaVersions](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ScriptName] [nvarchar](255) NOT NULL,
	[Applied] [datetime] NOT NULL,
 CONSTRAINT [PK_SchemaVersions_Id] PRIMARY KEY CLUSTERED
(
	[Id] ASC
)
) ON [PRIMARY]

CREATE TABLE [dbo].[Serie](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Omschrijving] [nvarchar](255) NOT NULL,
	[OudId] [int] NULL,
	[TaalId] [int] NOT NULL,
 CONSTRAINT [PK_dbo.Serie] PRIMARY KEY CLUSTERED
(
	[Id] ASC
)
) ON [PRIMARY]

CREATE TABLE [dbo].[Spotlight](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Titel] [nvarchar](255) NOT NULL,
	[Subtitel] [nvarchar](255) NOT NULL,
	[LinkTitel] [nvarchar](255) NOT NULL,
	[Url] [nvarchar](max) NOT NULL,
	[AfbeeldingId] [int] NOT NULL,
	[Sortering] [int] NOT NULL,
	[TaalId] [int] NOT NULL,
	[NieuwVenster] [bit] NOT NULL,
 CONSTRAINT [PK_dbo.Spotlight] PRIMARY KEY CLUSTERED
(
	[Id] ASC
)
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

CREATE TABLE [dbo].[Taal](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Code] [nvarchar](10) NOT NULL,
	[Omschrijving] [nvarchar](255) NOT NULL,
 CONSTRAINT [PK_dbo.Taal] PRIMARY KEY CLUSTERED
(
	[Id] ASC
)
) ON [PRIMARY]

CREATE TABLE [dbo].[Tekst](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Kop] [nvarchar](255) NOT NULL,
	[Tekst] [nvarchar](max) NOT NULL,
	[TaalId] [int] NOT NULL,
	[PaginaId] [int] NULL,
 CONSTRAINT [PK_dbo.Tekst] PRIMARY KEY CLUSTERED
(
	[Id] ASC
)
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

CREATE TABLE [dbo].[ZoekOpdracht](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PubliekeSleutel] [uniqueidentifier] NOT NULL,
	[LeesPreken] [bit] NOT NULL,
	[AudioPreken] [bit] NOT NULL,
	[Lezingen] [bit] NOT NULL,
	[PredikantId] [int] NULL,
	[Predikant] [nvarchar](max) NULL,
	[BoekHoofdstukId] [int] NULL,
	[BoekHoofdstuk] [nvarchar](max) NULL,
	[BoekId] [int] NULL,
	[Boek] [nvarchar](max) NULL,
	[LezingCategorieId] [int] NULL,
	[LezingCategorie] [nvarchar](max) NULL,
	[Hoofdstuk] [int] NULL,
	[GebeurtenisId] [int] NULL,
	[Gebeurtenis] [nvarchar](max) NULL,
	[GemeenteId] [int] NULL,
	[Gemeente] [nvarchar](max) NULL,
	[SerieId] [int] NULL,
	[Serie] [nvarchar](max) NULL,
	[TaalId] [int] NOT NULL,
	[GebruikerId] [int] NOT NULL,
	[Zoekterm] [nvarchar](max) NULL,
	[SorteerOp] [int] NULL,
	[SorteerVolgorde] [int] NOT NULL,
 CONSTRAINT [PK_dbo.ZoekOpdracht] PRIMARY KEY CLUSTERED
(
	[Id] ASC
)
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

SET IDENTITY_INSERT [dbo].[Afbeelding] ON
INSERT [dbo].[Afbeelding] ([Id], [Omschrijving], [Bestandsnaam], [ContentType]) VALUES (1, N'Testafbeelding', N'', N'')
SET IDENTITY_INSERT [dbo].[Afbeelding] OFF

SET IDENTITY_INSERT [dbo].[AspNetRoles] ON
INSERT [dbo].[AspNetRoles] ([Id], [Name]) VALUES (10, N'Bestandsbeheer')
INSERT [dbo].[AspNetRoles] ([Id], [Name]) VALUES (11, N'BestandsbeheerPrekenWeb')
INSERT [dbo].[AspNetRoles] ([Id], [Name]) VALUES (1, N'Gebruikers')
INSERT [dbo].[AspNetRoles] ([Id], [Name]) VALUES (8, N'Inbox')
INSERT [dbo].[AspNetRoles] ([Id], [Name]) VALUES (6, N'Nieuwsbrief')
INSERT [dbo].[AspNetRoles] ([Id], [Name]) VALUES (5, N'Pagina')
INSERT [dbo].[AspNetRoles] ([Id], [Name]) VALUES (3, N'PreekFiatteren')
INSERT [dbo].[AspNetRoles] ([Id], [Name]) VALUES (2, N'PreekToevoegen')
INSERT [dbo].[AspNetRoles] ([Id], [Name]) VALUES (9, N'PrekenVanAnderenBewerken')
INSERT [dbo].[AspNetRoles] ([Id], [Name]) VALUES (4, N'Spotlight')
INSERT [dbo].[AspNetRoles] ([Id], [Name]) VALUES (7, N'Stamgegevens')
SET IDENTITY_INSERT [dbo].[AspNetRoles] OFF

INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId], [Gebruiker_Id]) VALUES (1, 1, 1)
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId], [Gebruiker_Id]) VALUES (1, 2, 1)
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId], [Gebruiker_Id]) VALUES (1, 3, 1)
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId], [Gebruiker_Id]) VALUES (1, 4, 1)
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId], [Gebruiker_Id]) VALUES (1, 5, 1)
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId], [Gebruiker_Id]) VALUES (1, 6, 1)
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId], [Gebruiker_Id]) VALUES (1, 7, 1)
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId], [Gebruiker_Id]) VALUES (1, 8, 1)
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId], [Gebruiker_Id]) VALUES (1, 9, 1)
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId], [Gebruiker_Id]) VALUES (1, 10, 1)
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId], [Gebruiker_Id]) VALUES (1, 11, 1)

SET IDENTITY_INSERT [dbo].[Boek] ON
INSERT [dbo].[Boek] ([Id], [Boeknaam], [Sortering], [OudId], [Afkorting], [ToonHoofdstukNummer], [TaalId]) VALUES (1, N'Oude Testament', 0, NULL, N'OT', 1, 1)
SET IDENTITY_INSERT [dbo].[Boek] OFF

SET IDENTITY_INSERT [dbo].[BoekHoofdstuk] ON
INSERT [dbo].[BoekHoofdstuk] ([Id], [BoekId], [Omschrijving], [Sortering], [OudId]) VALUES (1, 1, N'Genesis', 0, NULL)
INSERT [dbo].[BoekHoofdstuk] ([Id], [BoekId], [Omschrijving], [Sortering], [OudId]) VALUES (2, 1, N'Leviticus', 2, NULL)
INSERT [dbo].[BoekHoofdstuk] ([Id], [BoekId], [Omschrijving], [Sortering], [OudId]) VALUES (3, 1, N'Exodus', 1, NULL)
SET IDENTITY_INSERT [dbo].[BoekHoofdstuk] OFF

SET IDENTITY_INSERT [dbo].[BoekHoofdstukTekst] ON
INSERT [dbo].[BoekHoofdstukTekst] ([Id], [BoekHoofdstukId], [Hoofdstuk], [Vers], [Tekst], [Sortering]) VALUES (1, 1, 0, 3, N'En God zeide: Daar zij licht! en daar werd licht.', 2)
INSERT [dbo].[BoekHoofdstukTekst] ([Id], [BoekHoofdstukId], [Hoofdstuk], [Vers], [Tekst], [Sortering]) VALUES (2, 1, 0, 1, N'In den beginne schiep God den hemel en de aarde.', 0)
INSERT [dbo].[BoekHoofdstukTekst] ([Id], [BoekHoofdstukId], [Hoofdstuk], [Vers], [Tekst], [Sortering]) VALUES (3, 2, 0, 3, N'Indien zijn offerande een brandoffer van runderen is, zo zal hij een volkomen mannetje offeren; aan de deur van de tent der samenkomst zal hij dat offeren, naar zijn welgevallen, voor het aangezicht des HEEREN.', 2)
INSERT [dbo].[BoekHoofdstukTekst] ([Id], [BoekHoofdstukId], [Hoofdstuk], [Vers], [Tekst], [Sortering]) VALUES (4, 2, 0, 1, N'En de HEERE riep Mozes, en sprak tot hem uit de tent der samenkomst, zeggende:', 0)
INSERT [dbo].[BoekHoofdstukTekst] ([Id], [BoekHoofdstukId], [Hoofdstuk], [Vers], [Tekst], [Sortering]) VALUES (5, 1, 0, 2, N'De aarde nu was woest en ledig, en duisternis was op den afgrond; en de Geest Gods zweefde op de wateren.', 1)
INSERT [dbo].[BoekHoofdstukTekst] ([Id], [BoekHoofdstukId], [Hoofdstuk], [Vers], [Tekst], [Sortering]) VALUES (6, 2, 0, 2, N'Spreek tot de kinderen Israels, en zeg tot hen: Als een mens uit u den HEERE een offerande zal offeren, gij zult uw offeranden offeren van het vee, van runderen en van schapen.', 1)
SET IDENTITY_INSERT [dbo].[BoekHoofdstukTekst] OFF

SET IDENTITY_INSERT [dbo].[Gebeurtenis] ON
INSERT [dbo].[Gebeurtenis] ([Id], [Omschrijving], [OudId], [Sortering], [TaalId]) VALUES (1, N'Pasen', NULL, 1, 1)
SET IDENTITY_INSERT [dbo].[Gebeurtenis] OFF

SET IDENTITY_INSERT [dbo].[Gebruiker] ON
INSERT [dbo].[Gebruiker] ([Id], [Naam], [LaatstIngelogd], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (1, N'Testgebruiker 1', CAST(N'2015-10-08 14:50:22.737' AS DateTime), N'test1@prekenweb.nl', 1, N'AFO33v1wphsnVS+Kl0sGgmQT2dppYQj2USf4ybiBC8KpXmN1O5o8U29I1bcz/G0IgA==', N'3bc2837e-840d-4844-a245-4e07fbeca00a', NULL, 0, 0, NULL, 0, 0, N'test1@prekenweb.nl')
INSERT [dbo].[Gebruiker] ([Id], [Naam], [LaatstIngelogd], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (2, N'Testgebruiker 2', NULL, N'test2@prekenweb.nl', 0, N'AFO33v1wphsnVS+Kl0sGgmQT2dppYQj2USf4ybiBC8KpXmN1O5o8U29I1bcz/G0IgA==', N'3bc2837e-840d-4844-a245-4e07fbeca00a', NULL, 0, 0, NULL, 0, 0, N'test2@prekenweb.nl')
SET IDENTITY_INSERT [dbo].[Gebruiker] OFF

SET IDENTITY_INSERT [dbo].[Gemeente] ON
INSERT [dbo].[Gemeente] ([Id], [Omschrijving]) VALUES (1, N'Rotterdam')
INSERT [dbo].[Gemeente] ([Id], [Omschrijving]) VALUES (2, N'Amsterdam')
SET IDENTITY_INSERT [dbo].[Gemeente] OFF

SET IDENTITY_INSERT [dbo].[Mailing] ON
INSERT [dbo].[Mailing] ([Id], [Omschrijving], [MailChimpId], [TaalId]) VALUES (1, N'Nieuwsbrief', N'', 1)
SET IDENTITY_INSERT [dbo].[Mailing] OFF

SET IDENTITY_INSERT [dbo].[Pagina] ON
INSERT [dbo].[Pagina] ([Id], [Gepubliceerd], [Identifier], [Aangemaakt], [Bijgewerkt], [AangemaaktDoor], [BijgewerktDoor], [TonenOpHomepage]) VALUES (1, 1, N'boeken', CAST(N'2015-01-01 12:00:00.000' AS DateTime), CAST(N'2015-01-01 12:00:00.000' AS DateTime), 1, 1, 0)
INSERT [dbo].[Pagina] ([Id], [Gepubliceerd], [Identifier], [Aangemaakt], [Bijgewerkt], [AangemaaktDoor], [BijgewerktDoor], [TonenOpHomepage]) VALUES (2, 1, N'contact', CAST(N'2015-01-01 12:00:00.000' AS DateTime), CAST(N'2015-01-01 12:00:00.000' AS DateTime), 1, 1, 0)
INSERT [dbo].[Pagina] ([Id], [Gepubliceerd], [Identifier], [Aangemaakt], [Bijgewerkt], [AangemaaktDoor], [BijgewerktDoor], [TonenOpHomepage]) VALUES (3, 1, N'financiele-bijdrage', CAST(N'2015-01-01 12:00:00.000' AS DateTime), CAST(N'2015-01-01 12:00:00.000' AS DateTime), 1, 1, 0)
INSERT [dbo].[Pagina] ([Id], [Gepubliceerd], [Identifier], [Aangemaakt], [Bijgewerkt], [AangemaaktDoor], [BijgewerktDoor], [TonenOpHomepage]) VALUES (4, 1, N'gegevens-aanvullen', CAST(N'2015-01-01 12:00:00.000' AS DateTime), CAST(N'2015-01-01 12:00:00.000' AS DateTime), 1, 1, 0)
INSERT [dbo].[Pagina] ([Id], [Gepubliceerd], [Identifier], [Aangemaakt], [Bijgewerkt], [AangemaaktDoor], [BijgewerktDoor], [TonenOpHomepage]) VALUES (5, 1, N'gegevens-aanvullen-compleet', CAST(N'2015-01-01 12:00:00.000' AS DateTime), CAST(N'2015-01-01 12:00:00.000' AS DateTime), 1, 1, 0)
INSERT [dbo].[Pagina] ([Id], [Gepubliceerd], [Identifier], [Aangemaakt], [Bijgewerkt], [AangemaaktDoor], [BijgewerktDoor], [TonenOpHomepage]) VALUES (6, 1, N'home-welkom', CAST(N'2015-01-01 12:00:00.000' AS DateTime), CAST(N'2015-01-01 12:00:00.000' AS DateTime), 1, 1, 0)
INSERT [dbo].[Pagina] ([Id], [Gepubliceerd], [Identifier], [Aangemaakt], [Bijgewerkt], [AangemaaktDoor], [BijgewerktDoor], [TonenOpHomepage]) VALUES (7, 1, N'hulp', CAST(N'2015-01-01 12:00:00.000' AS DateTime), CAST(N'2015-01-01 12:00:00.000' AS DateTime), 1, 1, 0)
INSERT [dbo].[Pagina] ([Id], [Gepubliceerd], [Identifier], [Aangemaakt], [Bijgewerkt], [AangemaaktDoor], [BijgewerktDoor], [TonenOpHomepage]) VALUES (8, 1, N'Inloggen', CAST(N'2015-01-01 12:00:00.000' AS DateTime), CAST(N'2015-01-01 12:00:00.000' AS DateTime), 1, 1, 0)
INSERT [dbo].[Pagina] ([Id], [Gepubliceerd], [Identifier], [Aangemaakt], [Bijgewerkt], [AangemaaktDoor], [BijgewerktDoor], [TonenOpHomepage]) VALUES (9, 1, N'InschrijvenNieuwsbrief', CAST(N'2015-01-01 12:00:00.000' AS DateTime), CAST(N'2015-01-01 12:00:00.000' AS DateTime), 1, 1, 0)
INSERT [dbo].[Pagina] ([Id], [Gepubliceerd], [Identifier], [Aangemaakt], [Bijgewerkt], [AangemaaktDoor], [BijgewerktDoor], [TonenOpHomepage]) VALUES (10, 1, N'iTunes-podcast', CAST(N'2015-01-01 12:00:00.000' AS DateTime), CAST(N'2015-01-01 12:00:00.000' AS DateTime), 1, 1, 0)
INSERT [dbo].[Pagina] ([Id], [Gepubliceerd], [Identifier], [Aangemaakt], [Bijgewerkt], [AangemaaktDoor], [BijgewerktDoor], [TonenOpHomepage]) VALUES (11, 1, N'links ', CAST(N'2015-01-01 12:00:00.000' AS DateTime), CAST(N'2015-01-01 12:00:00.000' AS DateTime), 1, 1, 0)
INSERT [dbo].[Pagina] ([Id], [Gepubliceerd], [Identifier], [Aangemaakt], [Bijgewerkt], [AangemaaktDoor], [BijgewerktDoor], [TonenOpHomepage]) VALUES (12, 1, N'nieuwsbrief', CAST(N'2015-01-01 12:00:00.000' AS DateTime), CAST(N'2015-01-01 12:00:00.000' AS DateTime), 1, 1, 0)
INSERT [dbo].[Pagina] ([Id], [Gepubliceerd], [Identifier], [Aangemaakt], [Bijgewerkt], [AangemaaktDoor], [BijgewerktDoor], [TonenOpHomepage]) VALUES (13, 1, N'preek-toevoegen', CAST(N'2015-01-01 12:00:00.000' AS DateTime), CAST(N'2015-01-01 12:00:00.000' AS DateTime), 1, 1, 0)
INSERT [dbo].[Pagina] ([Id], [Gepubliceerd], [Identifier], [Aangemaakt], [Bijgewerkt], [AangemaaktDoor], [BijgewerktDoor], [TonenOpHomepage]) VALUES (14, 1, N'reactie-geven', CAST(N'2015-01-01 12:00:00.000' AS DateTime), CAST(N'2015-01-01 12:00:00.000' AS DateTime), 1, 1, 0)
INSERT [dbo].[Pagina] ([Id], [Gepubliceerd], [Identifier], [Aangemaakt], [Bijgewerkt], [AangemaaktDoor], [BijgewerktDoor], [TonenOpHomepage]) VALUES (15, 1, N'Registreer', CAST(N'2015-01-01 12:00:00.000' AS DateTime), CAST(N'2015-01-01 12:00:00.000' AS DateTime), 1, 1, 0)
INSERT [dbo].[Pagina] ([Id], [Gepubliceerd], [Identifier], [Aangemaakt], [Bijgewerkt], [AangemaaktDoor], [BijgewerktDoor], [TonenOpHomepage]) VALUES (16, 1, N'RegistreerSuccesvol', CAST(N'2015-01-01 12:00:00.000' AS DateTime), CAST(N'2015-01-01 12:00:00.000' AS DateTime), 1, 1, 0)
INSERT [dbo].[Pagina] ([Id], [Gepubliceerd], [Identifier], [Aangemaakt], [Bijgewerkt], [AangemaaktDoor], [BijgewerktDoor], [TonenOpHomepage]) VALUES (17, 1, N'ResetWachtwoord', CAST(N'2015-01-01 12:00:00.000' AS DateTime), CAST(N'2015-01-01 12:00:00.000' AS DateTime), 1, 1, 0)
INSERT [dbo].[Pagina] ([Id], [Gepubliceerd], [Identifier], [Aangemaakt], [Bijgewerkt], [AangemaaktDoor], [BijgewerktDoor], [TonenOpHomepage]) VALUES (18, 1, N'ResetWachtwoordBevestiging', CAST(N'2015-01-01 12:00:00.000' AS DateTime), CAST(N'2015-01-01 12:00:00.000' AS DateTime), 1, 1, 0)
INSERT [dbo].[Pagina] ([Id], [Gepubliceerd], [Identifier], [Aangemaakt], [Bijgewerkt], [AangemaaktDoor], [BijgewerktDoor], [TonenOpHomepage]) VALUES (19, 1, N'verbetering-doorgeven', CAST(N'2015-01-01 12:00:00.000' AS DateTime), CAST(N'2015-01-01 12:00:00.000' AS DateTime), 1, 1, 0)
INSERT [dbo].[Pagina] ([Id], [Gepubliceerd], [Identifier], [Aangemaakt], [Bijgewerkt], [AangemaaktDoor], [BijgewerktDoor], [TonenOpHomepage]) VALUES (20, 1, N'WachtwoordVergeten', CAST(N'2015-01-01 12:00:00.000' AS DateTime), CAST(N'2015-01-01 12:00:00.000' AS DateTime), 1, 1, 0)
INSERT [dbo].[Pagina] ([Id], [Gepubliceerd], [Identifier], [Aangemaakt], [Bijgewerkt], [AangemaaktDoor], [BijgewerktDoor], [TonenOpHomepage]) VALUES (21, 1, N'WachtwoordVergetenBevestiging', CAST(N'2015-01-01 12:00:00.000' AS DateTime), CAST(N'2015-01-01 12:00:00.000' AS DateTime), 1, 1, 0)
INSERT [dbo].[Pagina] ([Id], [Gepubliceerd], [Identifier], [Aangemaakt], [Bijgewerkt], [AangemaaktDoor], [BijgewerktDoor], [TonenOpHomepage]) VALUES (22, 1, N'Wat-is-mijn-PrekenWeb', CAST(N'2015-01-01 12:00:00.000' AS DateTime), CAST(N'2015-01-01 12:00:00.000' AS DateTime), 1, 1, 0)
INSERT [dbo].[Pagina] ([Id], [Gepubliceerd], [Identifier], [Aangemaakt], [Bijgewerkt], [AangemaaktDoor], [BijgewerktDoor], [TonenOpHomepage]) VALUES (23, 1, N'nieuwsbericht', CAST(N'2015-01-01 12:00:00.000' AS DateTime), CAST(N'2015-01-01 12:00:00.000' AS DateTime), 1, 1, 1)
SET IDENTITY_INSERT [dbo].[Pagina] OFF

SET IDENTITY_INSERT [dbo].[Predikant] ON
INSERT [dbo].[Predikant] ([Id], [Titels], [Voorletters], [Achternaam], [Gemeente], [LevensPeriode], [OudID], [GemeenteId], [Tussenvoegsels], [Opmerking], [TaalId]) VALUES (1, N'Ds', N'J.', N'Janssen', N'Rotterdam', NULL, NULL, NULL, N'', N'Opmerking', 1)
INSERT [dbo].[Predikant] ([Id], [Titels], [Voorletters], [Achternaam], [Gemeente], [LevensPeriode], [OudID], [GemeenteId], [Tussenvoegsels], [Opmerking], [TaalId]) VALUES (2, N'Ds', N'W. D.', N'Wit', N'Rotterdam', NULL, NULL, NULL, N'de', N'Opmerking', 1)
SET IDENTITY_INSERT [dbo].[Predikant] OFF

SET IDENTITY_INSERT [dbo].[Preek] ON
INSERT [dbo].[Preek] ([Id], [BoekhoofdstukId], [BijbeltekstOmschrijving], [SerieId], [GebeurtenisId], [DatumAangemaakt], [DatumBijgewerkt], [Bestandsnaam], [AantalKeerGedownload], [OudID], [PredikantId], [Hoofdstuk], [VanVers], [TotVers], [Punt1], [Punt2], [Punt3], [Punt4], [Punt5], [GemeenteId], [DatumPreek], [Informatie], [ThemaOmschrijving], [AfbeeldingId], [PreekTypeId], [LezingCategorieId], [TaalId], [Gepubliceerd], [LezingOmschrijving], [Duur], [Bestandsgrootte], [VersVanId], [VersTotId], [GedeelteVanVersId], [GedeelteTotVersId], [VersOmschrijving], [AutomatischeTeksten], [AangemaaktDoor], [AangepastDoor], [LeesPreekTekst]) VALUES (1, 1, NULL, NULL, 1, CAST(N'2015-01-01 12:00:00.000' AS DateTime), CAST(N'2015-01-01 13:10:00.000' AS DateTime), NULL, 0, NULL, 1, 1, NULL, NULL, N'Punt1', N'Punt2', N'Punt3', N'Punt4', NULL, 1, NULL, N'Informatie', NULL, NULL, 1, NULL, 1, 1, NULL, CAST(N'01:12:22' AS Time), NULL, 2, 1, NULL, NULL, N'1 tot 3', 1, 1, 1, NULL)
INSERT [dbo].[Preek] ([Id], [BoekhoofdstukId], [BijbeltekstOmschrijving], [SerieId], [GebeurtenisId], [DatumAangemaakt], [DatumBijgewerkt], [Bestandsnaam], [AantalKeerGedownload], [OudID], [PredikantId], [Hoofdstuk], [VanVers], [TotVers], [Punt1], [Punt2], [Punt3], [Punt4], [Punt5], [GemeenteId], [DatumPreek], [Informatie], [ThemaOmschrijving], [AfbeeldingId], [PreekTypeId], [LezingCategorieId], [TaalId], [Gepubliceerd], [LezingOmschrijving], [Duur], [Bestandsgrootte], [VersVanId], [VersTotId], [GedeelteVanVersId], [GedeelteTotVersId], [VersOmschrijving], [AutomatischeTeksten], [AangemaaktDoor], [AangepastDoor], [LeesPreekTekst]) VALUES (2, 2, NULL, NULL, NULL, CAST(N'2015-01-02 11:00:00.000' AS DateTime), CAST(N'2015-01-02 16:10:00.000' AS DateTime), NULL, 0, NULL, 2, 1, NULL, NULL, N'Punt1', N'Punt2', N'Punt3', N'Punt4', NULL, 2, NULL, N'Informatie', NULL, NULL, 3, NULL, 1, 1, NULL, NULL, NULL, 4, 3, NULL, NULL, N'1 tot 3', 1, 2, 2, NULL)
INSERT [dbo].[Preek] ([Id], [BoekhoofdstukId], [BijbeltekstOmschrijving], [SerieId], [GebeurtenisId], [DatumAangemaakt], [DatumBijgewerkt], [Bestandsnaam], [AantalKeerGedownload], [OudID], [PredikantId], [Hoofdstuk], [VanVers], [TotVers], [Punt1], [Punt2], [Punt3], [Punt4], [Punt5], [GemeenteId], [DatumPreek], [Informatie], [ThemaOmschrijving], [AfbeeldingId], [PreekTypeId], [LezingCategorieId], [TaalId], [Gepubliceerd], [LezingOmschrijving], [Duur], [Bestandsgrootte], [VersVanId], [VersTotId], [GedeelteVanVersId], [GedeelteTotVersId], [VersOmschrijving], [AutomatischeTeksten], [AangemaaktDoor], [AangepastDoor], [LeesPreekTekst]) VALUES (3, 2, NULL, NULL, NULL, CAST(N'2015-10-08 14:50:00.087' AS DateTime), CAST(N'2015-10-08 14:50:00.087' AS DateTime), NULL, 1, NULL, 2, 1, NULL, NULL, N'Punt1', N'Punt2', N'Punt3', N'Punt4', NULL, 2, NULL, N'Informatie', NULL, NULL, 3, NULL, 1, 1, NULL, NULL, NULL, 4, 3, NULL, NULL, N'1 tot 3', 1, 2, 2, NULL)
SET IDENTITY_INSERT [dbo].[Preek] OFF

SET IDENTITY_INSERT [dbo].[PreekType] ON
INSERT [dbo].[PreekType] ([Id], [Omschrijving]) VALUES (1, N'Preek')
INSERT [dbo].[PreekType] ([Id], [Omschrijving]) VALUES (2, N'Lezing')
INSERT [dbo].[PreekType] ([Id], [Omschrijving]) VALUES (3, N'LeesPreek')
SET IDENTITY_INSERT [dbo].[PreekType] OFF

SET IDENTITY_INSERT [dbo].[Spotlight] ON
INSERT [dbo].[Spotlight] ([Id], [Titel], [Subtitel], [LinkTitel], [Url], [AfbeeldingId], [Sortering], [TaalId], [NieuwVenster]) VALUES (1, N'Titel', N'Subtitel', N'LinkTitel', N'http://www.prekenweb.nl', 1, 1, 1, 0)
SET IDENTITY_INSERT [dbo].[Spotlight] OFF

SET IDENTITY_INSERT [dbo].[Taal] ON
INSERT [dbo].[Taal] ([Id], [Code], [Omschrijving]) VALUES (1, N'nl', N'Nederlands')
INSERT [dbo].[Taal] ([Id], [Code], [Omschrijving]) VALUES (2, N'en', N'Engels')
SET IDENTITY_INSERT [dbo].[Taal] OFF

SET IDENTITY_INSERT [dbo].[Tekst] ON
INSERT [dbo].[Tekst] ([Id], [Kop], [Tekst], [TaalId], [PaginaId]) VALUES (1, N'boeken', N'boeken', 1, 1)
INSERT [dbo].[Tekst] ([Id], [Kop], [Tekst], [TaalId], [PaginaId]) VALUES (2, N'boeken', N'boeken', 2, 1)
INSERT [dbo].[Tekst] ([Id], [Kop], [Tekst], [TaalId], [PaginaId]) VALUES (3, N'contact', N'contact', 1, 2)
INSERT [dbo].[Tekst] ([Id], [Kop], [Tekst], [TaalId], [PaginaId]) VALUES (4, N'contact', N'contact', 2, 2)
INSERT [dbo].[Tekst] ([Id], [Kop], [Tekst], [TaalId], [PaginaId]) VALUES (5, N'financiele-bijdrage', N'financiele-bijdrage', 1, 3)
INSERT [dbo].[Tekst] ([Id], [Kop], [Tekst], [TaalId], [PaginaId]) VALUES (6, N'financiele-bijdrage', N'financiele-bijdrage', 2, 3)
INSERT [dbo].[Tekst] ([Id], [Kop], [Tekst], [TaalId], [PaginaId]) VALUES (7, N'gegevens-aanvullen', N'gegevens-aanvullen', 1, 4)
INSERT [dbo].[Tekst] ([Id], [Kop], [Tekst], [TaalId], [PaginaId]) VALUES (8, N'gegevens-aanvullen', N'gegevens-aanvullen', 2, 4)
INSERT [dbo].[Tekst] ([Id], [Kop], [Tekst], [TaalId], [PaginaId]) VALUES (9, N'gegevens-aanvullen-compleet', N'gegevens-aanvullen-compleet', 1, 5)
INSERT [dbo].[Tekst] ([Id], [Kop], [Tekst], [TaalId], [PaginaId]) VALUES (10, N'gegevens-aanvullen-compleet', N'gegevens-aanvullen-compleet', 2, 5)
INSERT [dbo].[Tekst] ([Id], [Kop], [Tekst], [TaalId], [PaginaId]) VALUES (11, N'home-welkom', N'home-welkom', 1, 6)
INSERT [dbo].[Tekst] ([Id], [Kop], [Tekst], [TaalId], [PaginaId]) VALUES (12, N'home-welkom', N'home-welkom', 2, 6)
INSERT [dbo].[Tekst] ([Id], [Kop], [Tekst], [TaalId], [PaginaId]) VALUES (13, N'hulp', N'hulp', 1, 7)
INSERT [dbo].[Tekst] ([Id], [Kop], [Tekst], [TaalId], [PaginaId]) VALUES (14, N'hulp', N'hulp', 2, 7)
INSERT [dbo].[Tekst] ([Id], [Kop], [Tekst], [TaalId], [PaginaId]) VALUES (15, N'Inloggen', N'Inloggen', 1, 8)
INSERT [dbo].[Tekst] ([Id], [Kop], [Tekst], [TaalId], [PaginaId]) VALUES (16, N'Inloggen', N'Inloggen', 2, 8)
INSERT [dbo].[Tekst] ([Id], [Kop], [Tekst], [TaalId], [PaginaId]) VALUES (17, N'InschrijvenNieuwsbrief', N'InschrijvenNieuwsbrief', 1, 9)
INSERT [dbo].[Tekst] ([Id], [Kop], [Tekst], [TaalId], [PaginaId]) VALUES (18, N'InschrijvenNieuwsbrief', N'InschrijvenNieuwsbrief', 2, 9)
INSERT [dbo].[Tekst] ([Id], [Kop], [Tekst], [TaalId], [PaginaId]) VALUES (19, N'iTunes-podcast', N'iTunes-podcast', 1, 10)
INSERT [dbo].[Tekst] ([Id], [Kop], [Tekst], [TaalId], [PaginaId]) VALUES (20, N'iTunes-podcast', N'iTunes-podcast', 2, 10)
INSERT [dbo].[Tekst] ([Id], [Kop], [Tekst], [TaalId], [PaginaId]) VALUES (21, N'links ', N'links ', 1, 11)
INSERT [dbo].[Tekst] ([Id], [Kop], [Tekst], [TaalId], [PaginaId]) VALUES (22, N'links ', N'links ', 2, 11)
INSERT [dbo].[Tekst] ([Id], [Kop], [Tekst], [TaalId], [PaginaId]) VALUES (23, N'nieuwsbrief', N'nieuwsbrief', 1, 12)
INSERT [dbo].[Tekst] ([Id], [Kop], [Tekst], [TaalId], [PaginaId]) VALUES (24, N'nieuwsbrief', N'nieuwsbrief', 2, 12)
INSERT [dbo].[Tekst] ([Id], [Kop], [Tekst], [TaalId], [PaginaId]) VALUES (25, N'preek-toevoegen', N'preek-toevoegen', 1, 13)
INSERT [dbo].[Tekst] ([Id], [Kop], [Tekst], [TaalId], [PaginaId]) VALUES (26, N'preek-toevoegen', N'preek-toevoegen', 2, 13)
INSERT [dbo].[Tekst] ([Id], [Kop], [Tekst], [TaalId], [PaginaId]) VALUES (27, N'reactie-geven', N'reactie-geven', 1, 14)
INSERT [dbo].[Tekst] ([Id], [Kop], [Tekst], [TaalId], [PaginaId]) VALUES (28, N'reactie-geven', N'reactie-geven', 2, 14)
INSERT [dbo].[Tekst] ([Id], [Kop], [Tekst], [TaalId], [PaginaId]) VALUES (29, N'Registreer', N'Registreer', 1, 15)
INSERT [dbo].[Tekst] ([Id], [Kop], [Tekst], [TaalId], [PaginaId]) VALUES (30, N'Registreer', N'Registreer', 2, 15)
INSERT [dbo].[Tekst] ([Id], [Kop], [Tekst], [TaalId], [PaginaId]) VALUES (31, N'RegistreerSuccesvol', N'RegistreerSuccesvol', 1, 16)
INSERT [dbo].[Tekst] ([Id], [Kop], [Tekst], [TaalId], [PaginaId]) VALUES (32, N'RegistreerSuccesvol', N'RegistreerSuccesvol', 2, 16)
INSERT [dbo].[Tekst] ([Id], [Kop], [Tekst], [TaalId], [PaginaId]) VALUES (33, N'ResetWachtwoord', N'ResetWachtwoord', 1, 17)
INSERT [dbo].[Tekst] ([Id], [Kop], [Tekst], [TaalId], [PaginaId]) VALUES (34, N'ResetWachtwoord', N'ResetWachtwoord', 2, 17)
INSERT [dbo].[Tekst] ([Id], [Kop], [Tekst], [TaalId], [PaginaId]) VALUES (35, N'ResetWachtwoordBevestiging', N'ResetWachtwoordBevestiging', 1, 18)
INSERT [dbo].[Tekst] ([Id], [Kop], [Tekst], [TaalId], [PaginaId]) VALUES (36, N'ResetWachtwoordBevestiging', N'ResetWachtwoordBevestiging', 2, 18)
INSERT [dbo].[Tekst] ([Id], [Kop], [Tekst], [TaalId], [PaginaId]) VALUES (37, N'verbetering-doorgeven', N'verbetering-doorgeven', 1, 19)
INSERT [dbo].[Tekst] ([Id], [Kop], [Tekst], [TaalId], [PaginaId]) VALUES (38, N'verbetering-doorgeven', N'verbetering-doorgeven', 2, 19)
INSERT [dbo].[Tekst] ([Id], [Kop], [Tekst], [TaalId], [PaginaId]) VALUES (39, N'WachtwoordVergeten', N'WachtwoordVergeten', 1, 20)
INSERT [dbo].[Tekst] ([Id], [Kop], [Tekst], [TaalId], [PaginaId]) VALUES (40, N'WachtwoordVergeten', N'WachtwoordVergeten', 2, 20)
INSERT [dbo].[Tekst] ([Id], [Kop], [Tekst], [TaalId], [PaginaId]) VALUES (41, N'WachtwoordVergetenBevestiging', N'WachtwoordVergetenBevestiging', 1, 21)
INSERT [dbo].[Tekst] ([Id], [Kop], [Tekst], [TaalId], [PaginaId]) VALUES (42, N'WachtwoordVergetenBevestiging', N'WachtwoordVergetenBevestiging', 2, 21)
INSERT [dbo].[Tekst] ([Id], [Kop], [Tekst], [TaalId], [PaginaId]) VALUES (43, N'Wat-is-mijn-PrekenWeb', N'Wat-is-mijn-PrekenWeb', 1, 22)
INSERT [dbo].[Tekst] ([Id], [Kop], [Tekst], [TaalId], [PaginaId]) VALUES (44, N'Wat-is-mijn-PrekenWeb', N'Wat-is-mijn-PrekenWeb', 2, 22)
INSERT [dbo].[Tekst] ([Id], [Kop], [Tekst], [TaalId], [PaginaId]) VALUES (45, N'nieuwsbericht', N'nieuwsbericht', 1, 23)
INSERT [dbo].[Tekst] ([Id], [Kop], [Tekst], [TaalId], [PaginaId]) VALUES (46, N'nieuwsbericht', N'nieuwsbericht', 2, 23)
SET IDENTITY_INSERT [dbo].[Tekst] OFF

ALTER TABLE [dbo].[Inbox] ADD  DEFAULT ((0)) FOR [Afgehandeld]
ALTER TABLE [dbo].[Inbox] ADD  DEFAULT (getdate()) FOR [Aangemaakt]
ALTER TABLE [dbo].[ZoekOpdracht] ADD  DEFAULT (newid()) FOR [PubliekeSleutel]
ALTER TABLE [dbo].[AspNetUserClaims]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserClaims_dbo.Gebruiker_Gebruiker_Id] FOREIGN KEY([Gebruiker_Id])REFERENCES [dbo].[Gebruiker] ([Id])
ALTER TABLE [dbo].[AspNetUserClaims] CHECK CONSTRAINT [FK_dbo.AspNetUserClaims_dbo.Gebruiker_Gebruiker_Id]
ALTER TABLE [dbo].[AspNetUserLogins]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserLogins_dbo.Gebruiker_Gebruiker_Id] FOREIGN KEY([Gebruiker_Id])REFERENCES [dbo].[Gebruiker] ([Id])
ALTER TABLE [dbo].[AspNetUserLogins] CHECK CONSTRAINT [FK_dbo.AspNetUserLogins_dbo.Gebruiker_Gebruiker_Id]
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId] FOREIGN KEY([RoleId])REFERENCES [dbo].[AspNetRoles] ([Id])ON DELETE CASCADE
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId]
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.Gebruiker_Gebruiker_Id] FOREIGN KEY([Gebruiker_Id])REFERENCES [dbo].[Gebruiker] ([Id])
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.Gebruiker_Gebruiker_Id]
ALTER TABLE [dbo].[Boek]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Boek_dbo.Taal_TaalId] FOREIGN KEY([TaalId])REFERENCES [dbo].[Taal] ([Id])ON DELETE CASCADE
ALTER TABLE [dbo].[Boek] CHECK CONSTRAINT [FK_dbo.Boek_dbo.Taal_TaalId]
ALTER TABLE [dbo].[BoekHoofdstuk]  WITH CHECK ADD  CONSTRAINT [FK_dbo.BoekHoofdstuk_dbo.Boek_BoekId] FOREIGN KEY([BoekId])REFERENCES [dbo].[Boek] ([Id])ON DELETE CASCADE
ALTER TABLE [dbo].[BoekHoofdstuk] CHECK CONSTRAINT [FK_dbo.BoekHoofdstuk_dbo.Boek_BoekId]
ALTER TABLE [dbo].[BoekHoofdstukTekst]  WITH CHECK ADD  CONSTRAINT [FK_dbo.BoekHoofdstukTekst_dbo.BoekHoofdstuk_BoekHoofdstukId] FOREIGN KEY([BoekHoofdstukId])REFERENCES [dbo].[BoekHoofdstuk] ([Id])ON DELETE CASCADE
ALTER TABLE [dbo].[BoekHoofdstukTekst] CHECK CONSTRAINT [FK_dbo.BoekHoofdstukTekst_dbo.BoekHoofdstuk_BoekHoofdstukId]
ALTER TABLE [dbo].[Gebeurtenis]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Gebeurtenis_dbo.Taal_TaalId] FOREIGN KEY([TaalId])REFERENCES [dbo].[Taal] ([Id])ON DELETE CASCADE
ALTER TABLE [dbo].[Gebeurtenis] CHECK CONSTRAINT [FK_dbo.Gebeurtenis_dbo.Taal_TaalId]
ALTER TABLE [dbo].[GebruikerMailing]  WITH CHECK ADD  CONSTRAINT [FK_dbo.GebruikerMailing_dbo.Gebruiker_GebruikerId] FOREIGN KEY([GebruikerId])REFERENCES [dbo].[Gebruiker] ([Id])ON DELETE CASCADE
ALTER TABLE [dbo].[GebruikerMailing] CHECK CONSTRAINT [FK_dbo.GebruikerMailing_dbo.Gebruiker_GebruikerId]
ALTER TABLE [dbo].[GebruikerMailing]  WITH CHECK ADD  CONSTRAINT [FK_dbo.GebruikerMailing_dbo.Mailing_MailingId] FOREIGN KEY([MailingId])REFERENCES [dbo].[Mailing] ([Id])ON DELETE CASCADE
ALTER TABLE [dbo].[GebruikerMailing] CHECK CONSTRAINT [FK_dbo.GebruikerMailing_dbo.Mailing_MailingId]
ALTER TABLE [dbo].[Inbox]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Inbox_dbo.Gebruiker_GebruikerId] FOREIGN KEY([GebruikerId])REFERENCES [dbo].[Gebruiker] ([Id])
ALTER TABLE [dbo].[Inbox] CHECK CONSTRAINT [FK_dbo.Inbox_dbo.Gebruiker_GebruikerId]
ALTER TABLE [dbo].[Inbox]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Inbox_dbo.InboxType_InboxTypeId] FOREIGN KEY([InboxTypeId])REFERENCES [dbo].[InboxType] ([Id])ON DELETE CASCADE
ALTER TABLE [dbo].[Inbox] CHECK CONSTRAINT [FK_dbo.Inbox_dbo.InboxType_InboxTypeId]
ALTER TABLE [dbo].[Inbox]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Inbox_dbo.Preek_PreekId] FOREIGN KEY([PreekId])REFERENCES [dbo].[Preek] ([Id])
ALTER TABLE [dbo].[Inbox] CHECK CONSTRAINT [FK_dbo.Inbox_dbo.Preek_PreekId]
ALTER TABLE [dbo].[InboxOpvolging]  WITH CHECK ADD  CONSTRAINT [FK_dbo.InboxOpvolging_dbo.Gebruiker_GebruikerId] FOREIGN KEY([GebruikerId])REFERENCES [dbo].[Gebruiker] ([Id])
ALTER TABLE [dbo].[InboxOpvolging] CHECK CONSTRAINT [FK_dbo.InboxOpvolging_dbo.Gebruiker_GebruikerId]
ALTER TABLE [dbo].[InboxOpvolging]  WITH CHECK ADD  CONSTRAINT [FK_dbo.InboxOpvolging_dbo.Inbox_InboxId] FOREIGN KEY([InboxId])REFERENCES [dbo].[Inbox] ([Id])ON DELETE CASCADE
ALTER TABLE [dbo].[InboxOpvolging] CHECK CONSTRAINT [FK_dbo.InboxOpvolging_dbo.Inbox_InboxId]
ALTER TABLE [dbo].[Mailing]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Mailing_dbo.Taal_TaalId] FOREIGN KEY([TaalId])REFERENCES [dbo].[Taal] ([Id])ON DELETE CASCADE
ALTER TABLE [dbo].[Mailing] CHECK CONSTRAINT [FK_dbo.Mailing_dbo.Taal_TaalId]
ALTER TABLE [dbo].[NieuwsbriefInschrijving]  WITH CHECK ADD  CONSTRAINT [FK_dbo.NieuwsbriefInschrijving_dbo.Taal_TaalId] FOREIGN KEY([TaalId])REFERENCES [dbo].[Taal] ([Id])ON DELETE CASCADE
ALTER TABLE [dbo].[NieuwsbriefInschrijving] CHECK CONSTRAINT [FK_dbo.NieuwsbriefInschrijving_dbo.Taal_TaalId]
ALTER TABLE [dbo].[Pagina]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Pagina_dbo.Gebruiker_AangemaaktDoor] FOREIGN KEY([AangemaaktDoor])REFERENCES [dbo].[Gebruiker] ([Id])
ALTER TABLE [dbo].[Pagina] CHECK CONSTRAINT [FK_dbo.Pagina_dbo.Gebruiker_AangemaaktDoor]
ALTER TABLE [dbo].[Pagina]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Pagina_dbo.Gebruiker_BijgewerktDoor] FOREIGN KEY([BijgewerktDoor])REFERENCES [dbo].[Gebruiker] ([Id])
ALTER TABLE [dbo].[Pagina] CHECK CONSTRAINT [FK_dbo.Pagina_dbo.Gebruiker_BijgewerktDoor]
ALTER TABLE [dbo].[Predikant]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Predikant_dbo.Gemeente_GemeenteId] FOREIGN KEY([GemeenteId])REFERENCES [dbo].[Gemeente] ([Id])
ALTER TABLE [dbo].[Predikant] CHECK CONSTRAINT [FK_dbo.Predikant_dbo.Gemeente_GemeenteId]
ALTER TABLE [dbo].[Predikant]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Predikant_dbo.Taal_TaalId] FOREIGN KEY([TaalId])REFERENCES [dbo].[Taal] ([Id])ON DELETE CASCADE
ALTER TABLE [dbo].[Predikant] CHECK CONSTRAINT [FK_dbo.Predikant_dbo.Taal_TaalId]
ALTER TABLE [dbo].[Preek]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Preek_dbo.Afbeelding_AfbeeldingId] FOREIGN KEY([AfbeeldingId])REFERENCES [dbo].[Afbeelding] ([Id])
ALTER TABLE [dbo].[Preek] CHECK CONSTRAINT [FK_dbo.Preek_dbo.Afbeelding_AfbeeldingId]
ALTER TABLE [dbo].[Preek]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Preek_dbo.BoekHoofdstuk_BoekhoofdstukId] FOREIGN KEY([BoekhoofdstukId])REFERENCES [dbo].[BoekHoofdstuk] ([Id])
ALTER TABLE [dbo].[Preek] CHECK CONSTRAINT [FK_dbo.Preek_dbo.BoekHoofdstuk_BoekhoofdstukId]
ALTER TABLE [dbo].[Preek]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Preek_dbo.BoekHoofdstukTekst_GedeelteTotVersId] FOREIGN KEY([GedeelteTotVersId])REFERENCES [dbo].[BoekHoofdstukTekst] ([Id])
ALTER TABLE [dbo].[Preek] CHECK CONSTRAINT [FK_dbo.Preek_dbo.BoekHoofdstukTekst_GedeelteTotVersId]
ALTER TABLE [dbo].[Preek]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Preek_dbo.BoekHoofdstukTekst_GedeelteVanVersId] FOREIGN KEY([GedeelteVanVersId])REFERENCES [dbo].[BoekHoofdstukTekst] ([Id])
ALTER TABLE [dbo].[Preek] CHECK CONSTRAINT [FK_dbo.Preek_dbo.BoekHoofdstukTekst_GedeelteVanVersId]
ALTER TABLE [dbo].[Preek]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Preek_dbo.BoekHoofdstukTekst_VersTotId] FOREIGN KEY([VersTotId])REFERENCES [dbo].[BoekHoofdstukTekst] ([Id])
ALTER TABLE [dbo].[Preek] CHECK CONSTRAINT [FK_dbo.Preek_dbo.BoekHoofdstukTekst_VersTotId]
ALTER TABLE [dbo].[Preek]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Preek_dbo.BoekHoofdstukTekst_VersVanId] FOREIGN KEY([VersVanId])REFERENCES [dbo].[BoekHoofdstukTekst] ([Id])
ALTER TABLE [dbo].[Preek] CHECK CONSTRAINT [FK_dbo.Preek_dbo.BoekHoofdstukTekst_VersVanId]
ALTER TABLE [dbo].[Preek]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Preek_dbo.Gebeurtenis_GebeurtenisId] FOREIGN KEY([GebeurtenisId])REFERENCES [dbo].[Gebeurtenis] ([Id])
ALTER TABLE [dbo].[Preek] CHECK CONSTRAINT [FK_dbo.Preek_dbo.Gebeurtenis_GebeurtenisId]
ALTER TABLE [dbo].[Preek]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Preek_dbo.Gebruiker_AangemaaktDoor] FOREIGN KEY([AangemaaktDoor])REFERENCES [dbo].[Gebruiker] ([Id])
ALTER TABLE [dbo].[Preek] CHECK CONSTRAINT [FK_dbo.Preek_dbo.Gebruiker_AangemaaktDoor]
ALTER TABLE [dbo].[Preek]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Preek_dbo.Gebruiker_AangepastDoor] FOREIGN KEY([AangepastDoor])REFERENCES [dbo].[Gebruiker] ([Id])
ALTER TABLE [dbo].[Preek] CHECK CONSTRAINT [FK_dbo.Preek_dbo.Gebruiker_AangepastDoor]
ALTER TABLE [dbo].[Preek]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Preek_dbo.Gemeente_GemeenteId] FOREIGN KEY([GemeenteId])REFERENCES [dbo].[Gemeente] ([Id])
ALTER TABLE [dbo].[Preek] CHECK CONSTRAINT [FK_dbo.Preek_dbo.Gemeente_GemeenteId]
ALTER TABLE [dbo].[Preek]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Preek_dbo.LezingCategorie_LezingCategorieId] FOREIGN KEY([LezingCategorieId])REFERENCES [dbo].[LezingCategorie] ([Id])
ALTER TABLE [dbo].[Preek] CHECK CONSTRAINT [FK_dbo.Preek_dbo.LezingCategorie_LezingCategorieId]
ALTER TABLE [dbo].[Preek]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Preek_dbo.Predikant_PredikantId] FOREIGN KEY([PredikantId])REFERENCES [dbo].[Predikant] ([Id])
ALTER TABLE [dbo].[Preek] CHECK CONSTRAINT [FK_dbo.Preek_dbo.Predikant_PredikantId]
ALTER TABLE [dbo].[Preek]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Preek_dbo.PreekType_PreekTypeId] FOREIGN KEY([PreekTypeId])REFERENCES [dbo].[PreekType] ([Id])ON DELETE CASCADE
ALTER TABLE [dbo].[Preek] CHECK CONSTRAINT [FK_dbo.Preek_dbo.PreekType_PreekTypeId]
ALTER TABLE [dbo].[Preek]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Preek_dbo.Serie_SerieId] FOREIGN KEY([SerieId])REFERENCES [dbo].[Serie] ([Id])
ALTER TABLE [dbo].[Preek] CHECK CONSTRAINT [FK_dbo.Preek_dbo.Serie_SerieId]
ALTER TABLE [dbo].[Preek]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Preek_dbo.Taal_TaalId] FOREIGN KEY([TaalId])REFERENCES [dbo].[Taal] ([Id])ON DELETE CASCADE
ALTER TABLE [dbo].[Preek] CHECK CONSTRAINT [FK_dbo.Preek_dbo.Taal_TaalId]
ALTER TABLE [dbo].[PreekCookie]  WITH CHECK ADD  CONSTRAINT [FK_dbo.PreekCookie_dbo.Gebruiker_GebruikerId] FOREIGN KEY([GebruikerId])REFERENCES [dbo].[Gebruiker] ([Id])ON DELETE CASCADE
ALTER TABLE [dbo].[PreekCookie] CHECK CONSTRAINT [FK_dbo.PreekCookie_dbo.Gebruiker_GebruikerId]
ALTER TABLE [dbo].[PreekCookie]  WITH CHECK ADD  CONSTRAINT [FK_dbo.PreekCookie_dbo.Preek_PreekId] FOREIGN KEY([PreekId])REFERENCES [dbo].[Preek] ([Id])ON DELETE CASCADE
ALTER TABLE [dbo].[PreekCookie] CHECK CONSTRAINT [FK_dbo.PreekCookie_dbo.Preek_PreekId]
ALTER TABLE [dbo].[PreekLezenEnZingen]  WITH CHECK ADD  CONSTRAINT [FK_dbo.PreekLezenEnZingen_dbo.Preek_PreekId] FOREIGN KEY([PreekId])REFERENCES [dbo].[Preek] ([Id])ON DELETE CASCADE
ALTER TABLE [dbo].[PreekLezenEnZingen] CHECK CONSTRAINT [FK_dbo.PreekLezenEnZingen_dbo.Preek_PreekId]
ALTER TABLE [dbo].[Serie]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Serie_dbo.Taal_TaalId] FOREIGN KEY([TaalId])REFERENCES [dbo].[Taal] ([Id])ON DELETE CASCADE
ALTER TABLE [dbo].[Serie] CHECK CONSTRAINT [FK_dbo.Serie_dbo.Taal_TaalId]
ALTER TABLE [dbo].[Spotlight]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Spotlight_dbo.Afbeelding_AfbeeldingId] FOREIGN KEY([AfbeeldingId])REFERENCES [dbo].[Afbeelding] ([Id])ON DELETE CASCADE
ALTER TABLE [dbo].[Spotlight] CHECK CONSTRAINT [FK_dbo.Spotlight_dbo.Afbeelding_AfbeeldingId]
ALTER TABLE [dbo].[Spotlight]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Spotlight_dbo.Taal_TaalId] FOREIGN KEY([TaalId])REFERENCES [dbo].[Taal] ([Id])ON DELETE CASCADE
ALTER TABLE [dbo].[Spotlight] CHECK CONSTRAINT [FK_dbo.Spotlight_dbo.Taal_TaalId]
ALTER TABLE [dbo].[Tekst]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Tekst_dbo.Pagina_PaginaId] FOREIGN KEY([PaginaId])REFERENCES [dbo].[Pagina] ([Id])
ALTER TABLE [dbo].[Tekst] CHECK CONSTRAINT [FK_dbo.Tekst_dbo.Pagina_PaginaId]
ALTER TABLE [dbo].[Tekst]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Tekst_dbo.Taal_TaalId] FOREIGN KEY([TaalId])REFERENCES [dbo].[Taal] ([Id])ON DELETE CASCADE
ALTER TABLE [dbo].[Tekst] CHECK CONSTRAINT [FK_dbo.Tekst_dbo.Taal_TaalId]
ALTER TABLE [dbo].[ZoekOpdracht]  WITH CHECK ADD  CONSTRAINT [FK_dbo.ZoekOpdracht_dbo.Gebruiker_GebruikerId] FOREIGN KEY([GebruikerId])REFERENCES [dbo].[Gebruiker] ([Id])ON DELETE CASCADE
ALTER TABLE [dbo].[ZoekOpdracht] CHECK CONSTRAINT [FK_dbo.ZoekOpdracht_dbo.Gebruiker_GebruikerId]

Done:
print 'done'