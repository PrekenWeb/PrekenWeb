# PrekenWeb / SermonWeb
Deze repository bevat de broncode van prekenweb.nl en sermonweb.nl.

[![Build Status](https://dev.azure.com/prekenweb/Website/_apis/build/status/PrekenWeb.Website?branchName=master)](https://dev.azure.com/prekenweb/Website/_build/latest?definitionId=4&branchName=master)

# Bijdrage leveren
We staan open voor bijdrages, voel vrij om code aan te bieden, een issue aan te maken of in contact met ons te komen over een goed idee! Het is verstandig om, voordat je ergens aan begint, even met ons af te stemmen of we open staan voor het specifieke idee. Daarnaast rekenen we er wel op dat mensen die een bijdrage willen leveren voldoende kennis hebben van de hieronder genoemde technieken.

# Contact
Voor vragen/ideeen, neem contact op met [info@prekenweb.nl](mailto:info@prekenweb.nl)

# Installatie / database
De website is gemaakt om te werken onder IIS (7.5) en werkt met .NET framework 4.7.2. 
Als de connectiestring verwijst naar de juiste SQL Server en de application-pool user voldoende rechten heeft zal de database automatisch worden aangemaakt met daarin testdata. Op verzoek is een volledig gevulde kopie van de productiedatabase beschikbaar. In de testdatabase wordt automatisch een gebruiker aangemaakt: test1@prekenweb.nl met wachtwoord prekenweb.

# Tooling & technieken
- Visual Studio 2019
- ASP.NET MVC5
- MSSQL Server '12 of '14
- NuGet
- IIS (met [URL Rewrite module](http://www.iis.net/downloads/microsoft/url-rewrite))

# Taal
Deze codebase is ooit gestart voor prekenweb.nl, pas later is de engelstalige variant sermonweb.org toegevoegd. Vanwege deze Nederlanstalige start is de codebase en de voertaal in deze repository overwegend Nederlands.

