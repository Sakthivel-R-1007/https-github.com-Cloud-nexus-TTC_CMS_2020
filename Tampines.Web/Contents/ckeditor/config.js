/// <reference path="../custom/airframer-sidebar-add.js" />
/**
 * @license Copyright (c) 2003-2019, CKSource - Frederico Knabben. All rights reserved.
 * For licensing, see https://ckeditor.com/legal/ckeditor-oss-license
 */

//CKEDITOR.editorConfig = function( config ) {
	// Define changes to default configuration here. For example:
	// config.language = 'fr';
	// config.uiColor = '#AADC6E';
    //config.autoParagraph = false;  
//};

CKEDITOR.editorConfig = function (config) {
	// Define changes to default configuration here.
	// For complete reference see:
	// http://docs.ckeditor.com/#!/api/CKEDITOR.config

	// The toolbar groups arrangement, optimized for two toolbar rows.
	config.toolbarGroups = [
		{ name: 'clipboard', groups: ['clipboard', 'undo'] },
		{ name: 'editing', groups: ['find', 'selection', 'spellchecker'] },
		{ name: 'links' },
		{ name: 'insert' },
		{ name: 'forms' },
		{ name: 'tools' },
		{ name: 'document', groups: ['mode', 'document', 'doctools'] },
		{ name: 'others' },
		'/',
		{ name: 'basicstyles', groups: ['basicstyles', 'cleanup'] },
		{ name: 'paragraph', groups: ['list', 'indent', 'blocks', 'align', 'bidi'] },
		{ name: 'styles' },
		{ name: 'colors' },
		{ name: 'about' }
	];

	// Remove some buttons provided by the standard plugins, which are
	// not needed in the Standard(s) toolbar.
	//config.removePlugins = 'elementspath,save,image,flash,iframe,link,smiley,tabletools,find,pagebreak,templates,about,maximize,showblocks,newpage,language,specialchar,spellchecker';
	//config.removeButtons = 'Underline,Subscript,Superscript,Source,Save,Preview,Templates,Print,NewPage,Find,About,Maximize,ShowBlocks,Form,Checkbox,Radio,Select,Button,ImageButton,HiddenField,SelectAll,ImageButton,Anchor,NumberedList,BulletedList,Outdent,Indent,TextField,Textarea,Undo,Redo,Table,TextColor,BGColor,Blockquote,CreateDiv,Spell';
	 
	//config.removeButtons = 'Blockquote,BidiLtr,BidiRtl,Image,Table,HorizontalRule,Source,Save,NewPage,Preview,Print,Templates,Undo,Redo,Find,Replace,SelectAll,Scayt,Form,Checkbox,Radio,TextField,Textarea,Select,Button,ImageButton,HiddenField,Subscript,Superscript,RemoveFormat,CopyFormatting,NumberedList,BulletedList,Outdent,CreateDiv,Link,Unlink,Anchor,Flash,SpecialChar,PageBreak,Iframe,TextColor,BGColor,Maximize,ShowBlocks,About,Cut,Copy,Paste,PasteText,PasteFromWord';
	config.removeButtons = 'Save,Preview,Print,Templates,Cut,PasteText,TextColor,Maximize,About,BGColor,ShowBlocks,BidiLtr,BidiRtl,Language,Flash,Table,HorizontalRule,Smiley,SpecialChar,PageBreak,Iframe,Blockquote,CreateDiv,Outdent,Indent,NumberedList,BulletedList,CopyFormatting,RemoveFormat,Form,Checkbox,Radio,TextField,Textarea,Select,Button,HiddenField,ImageButton,Scayt,SelectAll,Find,Undo,Redo,NewPage';

	// Set the most common block elements.
	config.format_tags = 'p;h1;h2;h3;pre';

	// Simplify the dialog windows.
	config.removeDialogTabs = 'image:advanced;link:advanced';

	config.autoParagraph = false;
	config.allowedContent = true
};

