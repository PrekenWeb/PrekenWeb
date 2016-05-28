<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FileManager.aspx.cs" Inherits="Prekenweb.Website.Areas.Mijn.FileManagerWebForm" %>

<%@ Register Assembly="IZ.WebFileManager" Namespace="IZ.WebFileManager" TagPrefix="iz" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <iz:FileManager ID="FileManager" runat="server" Height="400" Width="600">
            <%--<RootDirectories>
                <iz:RootDirectory DirectoryPath="~/" Text="My Documents" />
            </RootDirectories>--%>
            <FileTypes>
                <iz:FileType Extensions="zip, rar, iso" Name="Archive" SmallImageUrl="~/Content/Images/16x16/compressed.gif"
                    LargeImageUrl="~/Content/Images/32x32/compressed.gif">
                </iz:FileType>
                <iz:FileType Extensions="doc, rtf" Name="Microsoft Word Document" SmallImageUrl="~/Content/Images/16x16/word.gif"
                    LargeImageUrl="~/Content/Images/32x32/word.gif">
                </iz:FileType>
                <iz:FileType Extensions="xls, csv" Name="Microsoft Excel Worksheet" SmallImageUrl="~/Content/Images/16x16/excel.gif"
                    LargeImageUrl="~/Content/Images/32x32/excel.gif">
                </iz:FileType>
                <iz:FileType Extensions="ppt, pps" Name="Microsoft PowerPoint Presentation" SmallImageUrl="~/Content/Images/16x16/PowerPoint.gif"
                    LargeImageUrl="~/Content/Images/32x32/PowerPoint.gif">
                </iz:FileType>
                <iz:FileType Extensions="gif, jpg, png" Name="Image" SmallImageUrl="~/Content/Images/16x16/image.gif"
                    LargeImageUrl="~/Content/Images/32x32/image.gif">
                </iz:FileType>
                <iz:FileType SmallImageUrl="~/Content/Images/16x16/media.gif" Name="Windows Media File" Extensions="mp3,wma,vmv,avi,divx"
                    LargeImageUrl="~/Content/Images/32x32/media.gif">
                </iz:FileType>
                <iz:FileType Extensions="txt" Name="Text Document">
                    <Commands>
                        <iz:FileManagerCommand Name="Edit" CommandName="EditText" SmallImageUrl="~/Content/Images/16x16/edit.gif" />
                    </Commands>
                </iz:FileType>
                <iz:FileType Extensions="xml, xsl, xsd" Name="XML Document" LargeImageUrl="~/Content/Images/32x32/xml.gif"
                    SmallImageUrl="~/Content/Images/16x16/xml.gif">
                    <Commands>
                        <iz:FileManagerCommand Name="Edit" CommandName="EditText" SmallImageUrl="~/Content/Images/16x16/edit.gif" />
                    </Commands>
                </iz:FileType>
                <iz:FileType Extensions="css" Name="Cascading Style Sheet" LargeImageUrl="~/Content/Images/32x32/styleSheet.gif"
                    SmallImageUrl="~/Content/Images/16x16/styleSheet.gif">
                    <Commands>
                        <iz:FileManagerCommand Name="Edit" CommandName="EditText" SmallImageUrl="~/Content/Images/16x16/edit.gif" />
                    </Commands>
                </iz:FileType>
                <iz:FileType Extensions="js, vbs" Name="Script File" LargeImageUrl="~/Content/Images/32x32/script.gif"
                    SmallImageUrl="~/Content/Images/16x16/script.gif">
                    <Commands>
                        <iz:FileManagerCommand Name="Edit" CommandName="EditText" SmallImageUrl="~/Content/Images/16x16/edit.gif" />
                    </Commands>
                </iz:FileType>
                <iz:FileType Extensions="htm, html" Name="HTML Document" LargeImageUrl="~/Content/Images/32x32/html.gif"
                    SmallImageUrl="~/Content/Images/16x16/html.gif">
                    <Commands>
                        <iz:FileManagerCommand Name="Edit with WYSWYG editor" CommandName="WYSWYG" />
                        <iz:FileManagerCommand Name="Edit" CommandName="EditText" SmallImageUrl="~/Content/Images/16x16/edit.gif" />
                    </Commands>
                </iz:FileType>
            </FileTypes>
        </iz:FileManager>
    </form>
</body>
</html>
