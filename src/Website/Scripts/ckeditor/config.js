/*
Copyright (c) 2003-2012, CKSource - Frederico Knabben. All rights reserved.
For licensing, see LICENSE.html or http://ckeditor.com/license
*/

CKEDITOR.editorConfig = function (config) {
    // Define changes to default configuration here. For example:
    // config.language = 'fr';
    // config.uiColor = '#AADC6E';
    config.pasteFromWordRemoveFontStyles = false;
    config.pasteFromWordRemoveStyles = false;
    if (window.location.href.indexOf("localhost") > -1)
        config.filebrowserImageUploadUrl = '/PrekenWeb/nl/Mijn/Pagina/UploadImage';
    else
        config.filebrowserImageUploadUrl = '/nl/Mijn/Pagina/UploadImage';
    //config.extraPlugins = 'youtube';
};


