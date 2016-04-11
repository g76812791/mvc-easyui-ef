// JavaScript Document
//下面控制点击后链接字体颜色
var LastTitle;

function SetColor()
{
var objEvent = window.event || arguments.callee.caller.arguments[0];
var srcElement = objEvent.srcElement;
var moren = document.getElementById("moren");
if (!srcElement)
{
    srcElement = objEvent.target;
}

  if (LastTitle)
  {
	  LastTitle.style.color = "#5A5A5A";
	   LastTitle.style.fontWeight ="";
	  LastTitle.style.backgroundPosition = "100% 0px";
	  LastTitle.parentNode.style.backgroundPosition = "0% 0px";	
   }
   
  srcElement.style.color="#000000";
 srcElement.style.fontWeight ="bold";
   srcElement.style.backgroundPosition = "100% -33px";
   srcElement.parentNode.style.backgroundPosition = "0% -33px";
   LastTitle = srcElement;   
   
  if(moren.nodeName=="#text")
  { moren.className="moren";
  }
	moren.className="li";
}