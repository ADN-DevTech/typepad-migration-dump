---
layout: "post"
title: "Replace Object Defaults Style"
date: "2013-02-13 12:39:02"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "Inventor"
original_url: "https://adndevblog.typepad.com/manufacturing/2013/02/replace-object-defaults-style.html "
typepad_basename: "replace-object-defaults-style"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/manufacturing/adam-nagy.html" target="_self">Adam Nagy</a></p>
<p>In the <strong>Style and Standard Editor</strong> dialog you can do a <strong>Replace Style</strong> on an <strong>Object Defaults Style</strong>. This will go through all the standards and check if any of them uses the selected style as the <strong>Active Object Defaults</strong>. If so, then it will activate the other <strong>Object Defaults Style</strong> that you provide in the <strong>Replace Style</strong> dialog. Once it&#39;s not used anywhere you may as well purge it / delete it.</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b017c36daae35970b-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="ReplaceStyle" class="asset  asset-image at-xid-6a0167607c2431970b017c36daae35970b" src="/assets/image_99c16c.jpg" title="ReplaceStyle" /></a></p>
<p>You can achieve the above using the API as well.</p>
<p><strong>VBA</strong></p>
<pre>Sub ReplaceObjectDefaultsStyle( _
    ByVal stylesMgr As DrawingStylesManager, _
    ByVal replaceStyle As ObjectDefaultsStyle, _
    ByVal withStyle As ObjectDefaultsStyle, _
    purge As Boolean)
    
    &#39; Go through each standard and replace the
    &#39; Active Defaults with withStyle if currently
    &#39; replaceStyle is being used as Active Defaults
    
    Dim oDwgStd As DrawingStandardStyle
    For Each oDwgStd In stylesMgr.StandardStyles
        If oDwgStd.ActiveObjectDefaults Is replaceStyle Then
            oDwgStd.ActiveObjectDefaults = withStyle
        End If
    Next
    
    &#39; If it is (or is also) local then we can delete it
    &#39; i.e. not library only
    
    If purge And replaceStyle.StyleLocation &lt;&gt; kLibraryStyleLocation _
    Then
        replaceStyle.Delete
    End If
    
End Sub

Sub TestReplace()

    Dim oDwg As DrawingDocument
    Set oDwg = ThisApplication.ActiveDocument
    
    Dim oStlMgr As DrawingStylesManager
    Set oStlMgr = oDwg.StylesManager
    
    &#39; The style we want to replace
    Dim oDefStyle1 As ObjectDefaultsStyle
    Set oDefStyle1 = oStlMgr.ObjectDefaultsStyles(3)
    
    &#39; The style we want to replace the active style with
    Dim oDefStyle2 As ObjectDefaultsStyle
    Set oDefStyle2 = oStlMgr.ObjectDefaultsStyles(1)
    
    ReplaceObjectDefaultsStyle oStlMgr, oDefStyle1, oDefStyle2, True

End Sub</pre>
<p><strong>C#</strong></p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px; line-height: 120%;"><span style="color: blue;">static</span> <span style="color: blue;">void</span> ReplaceObjectDefaultsStyle(</p>
<p style="margin: 0px; line-height: 120%;">&#0160; <span style="color: #2b91af;">DrawingStylesManager</span> stylesManager,</p>
<p style="margin: 0px; line-height: 120%;">&#0160; <span style="color: #2b91af;">ObjectDefaultsStyle</span> replaceStyle,</p>
<p style="margin: 0px; line-height: 120%;">&#0160; <span style="color: #2b91af;">ObjectDefaultsStyle</span> withStyle,</p>
<p style="margin: 0px; line-height: 120%;">&#0160; <span style="color: blue;">bool</span> purge)</p>
<p style="margin: 0px; line-height: 120%;">{</p>
<p style="margin: 0px; line-height: 120%;">&#0160; <span style="color: blue;">foreach</span> (<span style="color: #2b91af;">DrawingStandardStyle</span> standardStyle</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; <span style="color: blue;">in</span> stylesManager.StandardStyles)</p>
<p style="margin: 0px; line-height: 120%;">&#0160; {</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; <span style="color: blue;">if</span> (standardStyle.ActiveObjectDefaults == replaceStyle)</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; standardStyle.ActiveObjectDefaults = withStyle;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; }</p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; <span style="color: blue;">if</span> (purge &amp;&amp; replaceStyle.StyleLocation !=</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; <span style="color: #2b91af;">StyleLocationEnum</span>.kLibraryStyleLocation)</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; replaceStyle.Delete();</p>
<p style="margin: 0px; line-height: 120%;">}</p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;"><span style="color: blue;">static</span> <span style="color: blue;">private</span> <span style="color: blue;">void</span> test()</p>
<p style="margin: 0px; line-height: 120%;">{</p>
<p style="margin: 0px; line-height: 120%;">&#0160; <span style="color: #2b91af;">Application</span> app = (<span style="color: #2b91af;">Application</span>)</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; System.Runtime.InteropServices.<span style="color: #2b91af;">Marshal</span>.</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; GetActiveObject(<span style="color: #a31515;">&quot;Inventor.Application&quot;</span>);</p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; <span style="color: #2b91af;">DrawingDocument</span> dwg = (<span style="color: #2b91af;">DrawingDocument</span>)app.ActiveDocument;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; <span style="color: #2b91af;">DrawingStylesManager</span> stylesManager = dwg.StylesManager;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; <span style="color: #2b91af;">ObjectDefaultsStyle</span> style1 = stylesManager.ObjectDefaultsStyles[1];</p>
<p style="margin: 0px; line-height: 120%;">&#0160; <span style="color: #2b91af;">ObjectDefaultsStyle</span> style2 = stylesManager.ObjectDefaultsStyles[2];</p>
<p style="margin: 0px; line-height: 120%;">&#0160; ReplaceObjectDefaultsStyle(stylesManager, style1, style2, <span style="color: blue;">true</span>);</p>
<p style="margin: 0px; line-height: 120%;">}</p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
</div>
<p>And you could also do it from an <strong>iLogic Rule</strong> - see <a href="https://adndevblog.typepad.com/manufacturing/2015/11/convert-vba-to-net-ilogic.html">Convert VBA to .NET / iLogic</a></p>
<pre>Sub Main()
    Dim oDwg As DrawingDocument
    oDwg = ThisApplication.ActiveDocument
    
    Dim oStlMgr As DrawingStylesManager
    oStlMgr = oDwg.StylesManager
    
    &#39; The style we want to replace
    Dim oDefStyle1 As ObjectDefaultsStyle
    oDefStyle1 = oStlMgr.ObjectDefaultsStyles(3)
    
    &#39; The style we want to replace the active style with
    Dim oDefStyle2 As ObjectDefaultsStyle
    oDefStyle2 = oStlMgr.ObjectDefaultsStyles(1)
    
    ReplaceObjectDefaultsStyle(oStlMgr, oDefStyle1, oDefStyle2, True)
End Sub

Sub ReplaceObjectDefaultsStyle(
    stylesMgr As DrawingStylesManager, 
    replaceStyle As ObjectDefaultsStyle, 
    withStyle As ObjectDefaultsStyle, 
    purge As Boolean)
    
    &#39; Go through each standard and replace the
    &#39; Active Defaults with withStyle if currently
    &#39; replaceStyle is being used as Active Defaults
    
    Dim oDwgStd As DrawingStandardStyle
    For Each oDwgStd In stylesMgr.StandardStyles
        If oDwgStd.ActiveObjectDefaults Is replaceStyle Then
            oDwgStd.ActiveObjectDefaults = withStyle
        End If
    Next
    
    &#39; If it is (or is also) local then we can delete it
    &#39; i.e. not library only
    
    If purge And replaceStyle.StyleLocation &lt;&gt; kLibraryStyleLocation _
    Then
        replaceStyle.Delete
    End If
End Sub</pre>
