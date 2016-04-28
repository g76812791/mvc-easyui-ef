CKEDITOR.editorConfig = function (config) {
    config.toolbarGroups = [
		{ name: 'document', groups: ['document', 'mode', 'doctools'] },
		{ name: 'clipboard', groups: ['clipboard', 'undo'] },
		{ name: 'editing', groups: ['find', 'selection', 'spellchecker', 'editing'] },
		{ name: 'forms', groups: ['forms'] },
		{ name: 'paragraph', groups: ['list', 'indent', 'blocks', 'align', 'bidi', 'paragraph'] },
		'/',
		{ name: 'basicstyles', groups: ['cleanup', 'basicstyles'] },
		{ name: 'links', groups: ['links'] },
		'/',
		{ name: 'styles', groups: ['styles'] },
		{ name: 'insert', groups: ['insert'] },
		{ name: 'colors', groups: ['colors'] },
		{ name: 'tools', groups: ['tools'] },
		{ name: 'others', groups: ['others'] },
		{ name: 'about', groups: ['about'] }
    ];

    config.removeButtons = 'Save,NewPage,Preview,Print,Templates,Cut,Copy,PasteText,PasteFromWord,Undo,Redo,Find,Replace,SelectAll,Scayt,Form,Checkbox,Radio,TextField,Textarea,Select,Button,ImageButton,HiddenField,Italic,Underline,Strike,Subscript,Superscript,RemoveFormat,NumberedList,BulletedList,Outdent,Indent,Blockquote,CreateDiv,JustifyLeft,JustifyCenter,JustifyRight,JustifyBlock,Language,BidiRtl,BidiLtr,Link,Unlink,Anchor,Flash,Table,HorizontalRule,Smiley,SpecialChar,PageBreak,Iframe,About,ShowBlocks,Bold,Paste,Source';

    config.format_tags = 'p;h1;h2;h3;pre';

    // Simplify the dialog windows.
    config.removeDialogTabs = 'image:advanced;link:advanced';

    config.language = 'zh-cn';

    //	config.filebrowserBrowseUrl = '/Content/JQueryTools/ckfinder/ckfinder.html'; //上传文件时浏览服务文件夹
    //	config.filebrowserImageBrowseUrl = '/Content/JQueryTools/ckfinder/ckfinder.html?Type=Images'; //上传图片时浏览服务文件夹
    //	config.filebrowserFlashBrowseUrl = 'der/ckfinder.html?Type=Flash';  //上传Flash时浏览服务文件夹
    //	config.filebrowserUploadUrl = '/connector/aspx/connector.aspx?command=QuickUpload&type=Files'; //上传文件按钮(标签) 
    //  config.filebrowserImageUploadUrl = '/Content/JQueryTools/ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Images'; //上传图片按钮(标签) 
    config.filebrowserImageUploadUrl = '/Image/Add.do?command=QuickUpload&type=Images'; //上传图片按钮(标签) 
    //	config.filebrowserFlashUploadUrl = '/Content/JQueryTools/ckfinder/connector/aspx/connector.aspx?command=QuickUpload&type=Flash'; //上传Flash按钮(标签)

};