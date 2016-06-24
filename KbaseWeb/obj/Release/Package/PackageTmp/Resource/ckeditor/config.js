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

    //	config.filebrowserBrowseUrl = '/Content/JQueryTools/ckfinder/ckfinder.html'; //�ϴ��ļ�ʱ��������ļ���
    //	config.filebrowserImageBrowseUrl = '/Content/JQueryTools/ckfinder/ckfinder.html?Type=Images'; //�ϴ�ͼƬʱ��������ļ���
    //	config.filebrowserFlashBrowseUrl = 'der/ckfinder.html?Type=Flash';  //�ϴ�Flashʱ��������ļ���
    //	config.filebrowserUploadUrl = '/connector/aspx/connector.aspx?command=QuickUpload&type=Files'; //�ϴ��ļ���ť(��ǩ) 
    //  config.filebrowserImageUploadUrl = '/Content/JQueryTools/ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Images'; //�ϴ�ͼƬ��ť(��ǩ) 
    config.filebrowserImageUploadUrl = '/Image/Add.do?command=QuickUpload&type=Images'; //�ϴ�ͼƬ��ť(��ǩ) 
    //	config.filebrowserFlashUploadUrl = '/Content/JQueryTools/ckfinder/connector/aspx/connector.aspx?command=QuickUpload&type=Flash'; //�ϴ�Flash��ť(��ǩ)

};