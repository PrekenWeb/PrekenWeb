/**
 * @license Copyright (c) 2003-2020, CKSource - Frederico Knabben. All rights reserved.
 * For licensing, see https://ckeditor.com/legal/ckeditor-oss-license
 */

CKEDITOR.editorConfig = function( config ) {
	// Define changes to default configuration here.
	// For complete reference see:
	// https://ckeditor.com/docs/ckeditor4/latest/api/CKEDITOR_config.html

	config.pasteFromWordRemoveStyles = false;
	if (window.location.href.indexOf("localhost") > -1)
		config.filebrowserImageUploadUrl = '/PrekenWeb/nl/Mijn/Pagina/UploadImage';
	else
		config.filebrowserImageUploadUrl = '/nl/Mijn/Pagina/UploadImage';

	// The toolbar groups arrangement, optimized for two toolbar rows.
	config.toolbarGroups = [
		{ name: 'clipboard',   groups: [ 'clipboard', 'undo' ] },
		{ name: 'editing',     groups: [ 'find', 'selection', 'spellchecker' ] },
		{ name: 'links' },
		{ name: 'insert' },
		{ name: 'forms' },
		{ name: 'tools' },
		{ name: 'document',	   groups: [ 'mode', 'document', 'doctools' ] },
		{ name: 'others' },
		'/',
		{ name: 'basicstyles', groups: [ 'basicstyles', 'cleanup' ] },
		{ name: 'paragraph',   groups: [ 'list', 'indent', 'blocks', 'align', 'bidi' ] },
		{ name: 'styles' },
		{ name: 'colors' },
		{ name: 'about' }
	];

	// Remove some buttons provided by the standard plugins, which are
	// not needed in the Standard(s) toolbar.
	config.removeButtons = 'Underline,Subscript,Superscript';

	// Set the most common block elements.
	config.format_tags = 'p;h1;h2;h3;pre';

	// Simplify the dialog windows.
	config.removeDialogTabs = 'image:advanced;link:advanced';
};

CKEDITOR.on('dialogDefinition', function (ev) {
	try {
		var dialogName = ev.data.name;
		var dialogDefinition = ev.data.definition;

		if (dialogName == 'link') {
			var informationTab = dialogDefinition.getContents('target');
			var targetField = informationTab.get('linkTargetType');

			targetField['default'] = '_blank';
		}
	} catch (exception) {
		console.log('Error ' + ev.message);
	}
});