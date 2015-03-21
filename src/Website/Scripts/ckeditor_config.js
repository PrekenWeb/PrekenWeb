CKEDITOR.editorConfig = function (config) {
    config.pasteFromWordRemoveFontStyles = false;
    config.pasteFromWordRemoveStyles = false;
    if (window.location.href.indexOf("localhost") > -1)
        config.filebrowserImageUploadUrl = '/PrekenWeb/nl/Mijn/Pagina/UploadImage';
    else
        config.filebrowserImageUploadUrl = '/nl/Mijn/Pagina/UploadImage';
};


