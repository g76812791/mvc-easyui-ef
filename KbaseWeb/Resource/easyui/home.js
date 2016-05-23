$(function() {
   // InitLeftMenu();
    tabClose();
    tabCloseEven();
});

//初始化左侧menue
/*function InitLeftMenu() {
    $(".easyui-accordion").empty();
    var menulist = "";
    // _menus.menus = _menus.menus.sort(by('menuid'));
    $.each(_menus.menus, function (i, n) {
        menulist += '<ul>';
        $.each(n.menus, function (j, o) {
            menulist += '<li><div><a target="mainFrame" ghref="' + o.url + '" ><span class="icon ' + o.icon + '" >&nbsp;</span>' + o.menuname + '</a></div></li> ';
        });

        menulist += '</ul>';
        $('#menu').accordion('add', {
            animate: true,
            title: n.menuname,
            iconCls: n.icon,
            selected: i == 0 ? true : false,
            content: '<div style="overflow:auto;">' + menulist + '</div>',
        });
        menulist = "";
    });

    $('.easyui-accordion li a').click(function () {
        debugger
        var tabTitle = $(this).text();
        var url = $(this).attr("ghref");
        addTab(tabTitle, url);
        $('.easyui-accordion li div').removeClass("selected");
        $(this).parent().addClass("selected");
    }).hover(function () {
        $(this).parent().addClass("hover");
    }, function () {
        $(this).parent().removeClass("hover");
    });


}*/


//初始化左侧
function InitLeftMenu(Rid) {
    // $(".easyui-accordion").empty();
        var menulist = "";
    //_menus.menus = _menus.menus.sort(by('menuid'));

    ajaxOp.url = "/Back/Home/GetTree?Rid="+Rid;
    ajaxOp.su = function (result) {
        
        if (result != null) {
            $.each(result, function (i, n) {
                menulist += '<ul>';
                if (n.menus) {
                    $.each(n.menus, function (j, o) {
                        menulist += '<li><div><a target="mainFrame" ghref="' + o.url + '" ><span class="icon ' + o.icon + '" >&nbsp;</span>' + o.menuname + '</a></div></li> ';
                    });
                }
                debugger 
                menulist += '</ul>';
                $('#menu').accordion('add', {
                    animate: true,
                    title: n.menuname,
                    iconCls: n.icon,
                    selected: i == 0 ? true : false,
                    content: '<div style="overflow:auto;">' + menulist + '</div>',
                });
                menulist = "";
            });


            $('.easyui-accordion li a').click(function () {
                
                var tabTitle = $(this).text();
                var url = $(this).attr("ghref");
                addTab(tabTitle, url);
                $('.easyui-accordion li div').removeClass("selected");
                $(this).parent().addClass("selected");
            }).hover(function () {
                $(this).parent().addClass("hover");
            }, function () {
                $(this).parent().removeClass("hover");
            });
        } else {
            $.messager.alert('提示信息', '加载异常！', 'warning');
        }
    };
    ajaxOp.Exec();
}

function addTab(subtitle,url){
    if(!$('#tabs').tabs('exists',subtitle)){
        $('#tabs').tabs('add',{
            title:subtitle,
            content:createFrame(url),
            closable:true,
            width:$('#mainPanle').width()-10,
            height:$('#mainPanle').height()-26
        });
    }else{
        $('#tabs').tabs('select',subtitle);
    }
    tabClose();
}

function createFrame(url)
{
    var s = '<iframe name="mainFrame" scrolling="auto" frameborder="0"  src="'+url+'" style="width:100%;height:100%;"></iframe>';
    return s;
}

function tabClose() {
    /*双击关闭TAB选项卡*/
    $(".tabs-inner").dblclick(function () {
        var subtitle = $(this).children(".tabs-closable").text();
        $('#tabs').tabs('close', subtitle);
    })
    /*为选项卡绑定右键*/
    $(".tabs-inner").bind('contextmenu', function (e) {
        $('#mm').menu('show', {
            left: e.pageX,
            top: e.pageY
        });

        var subtitle = $(this).children(".tabs-closable").text();
        $('#mm').data("currtab", subtitle);
        $('#tabs').tabs('select', subtitle);

        debugger
        //判断除此之外
        var tabcount = $('#tabs').tabs('tabs').length; //tab选项卡的个数
        if (tabcount <= 1) {
            $('#mm-tabcloseother').attr("disabled", "disabled").css({ "cursor": "default", "opacity": "0.4" });
            $('#mm-tabcloseother').off('click');
        }
        else {
            $('#mm-tabcloseother').removeAttr("disabled").css({ "cursor": "pointer", "opacity": "1" });
            $('#mm-tabcloseother').off('click').on('click', chucizhiwai);
        }
        //判断右侧全部关闭
        var tabs = $('#tabs').tabs('tabs');     //获得所有的Tab选项卡
        var lasttab = tabs[tabcount - 1];  //获得最后一个Tab选项卡
        var lasttitle = lasttab.panel('options').tab.text(); //最后一个Tab选项卡的Title
        var currtab_title = $('#mm').data("currtab");  //当前Tab选项卡的Title

        if (lasttitle == currtab_title) {
            $('#mm-tabcloseright').attr("disabled", "disabled").css({ "cursor": "default", "opacity": "0.4" });
            $('#mm-tabcloseright').off('click');
        }
        else {
            $('#mm-tabcloseright').removeAttr("disabled").css({ "cursor": "pointer", "opacity": "1" });
            $('#mm-tabcloseright').off('click').on('click', youce);
        }

        //判断左侧全部关闭

        var onetab = tabs[0];  //第一个Tab选项卡
        var onetitle = onetab.panel('options').tab.text();  //第一个Tab选项卡的Title
        if (onetitle == currtab_title) {
            $('#mm-tabcloseleft').attr("disabled", "disabled").css({ "cursor": "default", "opacity": "0.4" });
            $('#mm-tabcloseleft').off('click');
        }
        else {
            $('#mm-tabcloseleft').removeAttr("disabled").css({ "cursor": "pointer", "opacity": "1" });
            $('#mm-tabcloseleft').off('click').on('click', zuoce);
        }

        return false;
    });
}

var chucizhiwai = function () {
    $('#mm-tabcloseright').click();
    $('#mm-tabcloseleft').click();
}
var youce = function () {
    var nextall = $('.tabs-selected').nextAll();
    nextall.each(function (i, n) {
        var t = $('a:eq(0) span', $(n)).text();
        $('#tabs').tabs('close', t);
    });
    $('#mm').menu('hide');
    return false;
}
var zuoce = function () {
    var prevall = $('.tabs-selected').prevAll();
    prevall.each(function (i, n) {
        var t = $('a:eq(0) span', $(n)).text();
        $('#tabs').tabs('close', t);
    });

    $("#tabs").tabs("select", 0);
    $('#mm').menu('hide');
    return false;
}
//绑定右键菜单事件
function tabCloseEven() {
    //刷新
    $('#mm-tabupdate').click(function () {
        var currTab = $('#tabs').tabs('getSelected');
        var url = $(currTab.panel('options').content).attr('src');
        $('#tabs').tabs('update', {
            tab: currTab,
            options: {
                content: createFrame(url)
            }
        })
    })
    //关闭当前
    $('#mm-tabclose').click(function() {
        var currtab_title = $('#mm').data("currtab");
        $('#tabs').tabs('close', currtab_title);
    });
    //全部关闭
    $('#mm-tabcloseall').click(function () {
        $('.tabs-inner span').each(function (i, n) {
            var t = $(n).text();
            $('#tabs').tabs('close', t);
        });
    });
    //关闭除当前之外的TAB
    $('#mm-tabcloseother').click(chucizhiwai);

    //关闭当前右侧的TAB
    $('#mm-tabcloseright').click(youce);
    //关闭当前左侧的TAB
    $('#mm-tabcloseleft').click(zuoce);

    //退出
    $("#mm-exit").click(function() {
        $('#mm').menu('hide');
    });
}

//弹出信息窗口 title:标题 msgString:提示信息 msgType:信息类型 [error,info,question,warning]
function msgShow(title, msgString, msgType) {
    $.messager.alert(title, msgString, msgType);
}

//排序待处理
var by = function(name){
    return function(o,p){  var a, b;
        if(typeof o === "object"&& typeof p === "object"&& o && p) {
            a= o[name];
            b= p[name];
            if(a === b) {
                return 0;
            }
            if(typeof a === typeof b) {
                return a < b ? -1 : 1;
            }
            return typeof  a < typeof  b ? -1 : 1;
        }
        else {
            throw ("error");
        }
    }
}