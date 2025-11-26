---
layout: "post"
title: "Unload partial CUIx when AutoCAD quits"
date: "2012-07-20 13:36:00"
author: "Adam Nagy"
categories:
  - ".NET"
  - "Adam Nagy"
  - "AutoCAD"
original_url: "https://adndevblog.typepad.com/autocad/2012/07/unload-partial-cuix-when-autocad-quits.html "
typepad_basename: "unload-partial-cuix-when-autocad-quits"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/adam-nagy.html" target="_self">Adam Nagy</a></p>
<p>AutoCAD 2011 introduced new API functions to load/unload partial CUIx files. This is what I'm trying to use as well - see below code. It loads the CUIx fine, but the unloading does not seem to work as my CUIx is still loaded the next time I start up AutoCAD.</p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">using</span><span style="line-height: 140%;"> System;</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">using</span><span style="line-height: 140%;"> Autodesk.AutoCAD.Runtime;</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">using</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">acApp</span><span style="line-height: 140%;"> = Autodesk.AutoCAD.ApplicationServices.</span><span style="color: #2b91af; line-height: 140%;">Application</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">using</span><span style="line-height: 140%;"> Autodesk.AutoCAD.Customization;</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">namespace</span><span style="line-height: 140%;"> CuixUnloaderDll</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; </span><span style="color: blue; line-height: 140%;">public</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">class</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">MyAddIn</span><span style="line-height: 140%;"> : </span><span style="color: #2b91af; line-height: 140%;">IExtensionApplication</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">private</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">string</span><span style="line-height: 140%;"> menuPath = </span><span style="color: #a31515; line-height: 140%;">@"C:\Temp\CuixUnloaderDll\MyCui.cuix"</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;"><span style="line-height: 15px;"><br /></span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">public</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">void</span><span style="line-height: 140%;"> Initialize()</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; </span><span style="color: #2b91af; line-height: 140%;">acApp</span><span style="line-height: 140%;">.LoadPartialMenu(menuPath);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; }</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">public</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">void</span><span style="line-height: 140%;"> Terminate()</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; </span><span style="color: #2b91af; line-height: 140%;">acApp</span><span style="line-height: 140%;">.UnloadPartialMenu(menuPath);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">}</span></p>
</div>
<p><strong>Solution</strong></p>
<p>If you check the return value of UnloadPartialMenu() then you'll see it's false, which means that the function did not succeed.</p>
<p>It is probably called too late and by that time the UI is unloaded.</p>
<p>Instead of using UnloadPartialMenu() you could use the CUI API to modify the acad.cuix file. This seemed to work fine:</p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">using</span><span style="line-height: 140%;"> System;</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">using</span><span style="line-height: 140%;"> Autodesk.AutoCAD.Runtime;</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">using</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">acApp</span><span style="line-height: 140%;"> = Autodesk.AutoCAD.ApplicationServices.</span><span style="color: #2b91af; line-height: 140%;">Application</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">using</span><span style="line-height: 140%;"> Autodesk.AutoCAD.Customization;</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">namespace</span><span style="line-height: 140%;"> CuixUnloaderDll</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; </span><span style="color: blue; line-height: 140%;">public</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">class</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">MyAddIn</span><span style="line-height: 140%;"> : </span><span style="color: #2b91af; line-height: 140%;">IExtensionApplication</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">private</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">string</span><span style="line-height: 140%;"> menuPath = </span><span style="color: #a31515; line-height: 140%;">@"C:\Temp\CuixUnloaderDll\MyCui.cuix"</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">private</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">string</span><span style="line-height: 140%;"> menuGroupName = </span><span style="color: #a31515; line-height: 140%;">"MYCUI"</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">public</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">void</span><span style="line-height: 140%;"> Initialize()</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">bool</span><span style="line-height: 140%;"> ret = </span><span style="color: #2b91af; line-height: 140%;">acApp</span><span style="line-height: 140%;">.LoadPartialMenu(menuPath);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; }</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">public</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">void</span><span style="line-height: 140%;"> Terminate()</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">string</span><span style="line-height: 140%;"> mainCuiFile = </span><span style="color: blue; line-height: 140%;">string</span><span style="line-height: 140%;">.Format(</span><span style="color: #a31515; line-height: 140%;">"{0}.cuix"</span><span style="line-height: 140%;">, </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; (</span><span style="color: blue; line-height: 140%;">string</span><span style="line-height: 140%;">)</span><span style="color: #2b91af; line-height: 140%;">acApp</span><span style="line-height: 140%;">.GetSystemVariable(</span><span style="color: #a31515; line-height: 140%;">"MENUNAME"</span><span style="line-height: 140%;">));</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; </span><span style="color: #2b91af; line-height: 140%;">CustomizationSection</span><span style="line-height: 140%;"> cs = </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">new</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">CustomizationSection</span><span style="line-height: 140%;">(mainCuiFile);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; cs.RemovePartialMenu(menuPath, menuGroupName);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;"> (cs.IsModified == </span><span style="color: blue; line-height: 140%;">true</span><span style="line-height: 140%;">)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; cs.Save();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">}</span></p>
</div>
<p>Since AutoCAD 2012 you could also use the <a href="http://exchange.autodesk.com/autocad/enu/online-help/browse#WS73099cc142f4875533992bfb12ce8a5f915-7e53.htm" target="_self">AutoLoader</a> mechanism to take care of loading/unloading your user interface customisation files (*.cuix)</p>
