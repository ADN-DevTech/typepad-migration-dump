---
layout: "post"
title: "Accessing And Changing The AutoCAD Preferences"
date: "2013-02-01 09:51:44"
author: "Augusto Goncalves"
categories:
  - "ActiveX"
  - "Augusto Goncalves"
  - "AutoCAD"
  - "ObjectARX"
original_url: "https://adndevblog.typepad.com/autocad/2013/02/accessing-and-changing-the-autocad-preferences.html "
typepad_basename: "accessing-and-changing-the-autocad-preferences"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/augusto-goncalves.html">Augusto Goncalves</a></p>  <p>This could be done by manipulating the AutoCAD registry directly, or by using the AutoCAD Preferences ActiveX object. Note that if you make a change in the registry directly, those changes will not affect the current running AutoCAD session. However, the ActiveX Preferences object exports all the AutoCAD preferences properties through an interface that you can use in VBA / VB or C++ / MFC applications. Below is sample code snippets (in VB and C++) which appends a new path to the existing AutoCAD support path.</p>  <p>Below you can find a VB and C++ code sample.</p> <font color="#000000">   <pre><font color="#0000a0">Public</font> acadApp <font color="#0000a0">As</font> <font color="#0000a0">Object</font> <font color="green">' AcadApplication </font>
<font color="#0000a0">Public</font> acadPrefFiles <font color="#0000a0">As</font> <font color="#0000a0">Object</font> <font color="green">'AcadPreferencesFiles</font>

<font color="#0000a0">Sub</font> f_preferences()
    <font color="#0000a0">On</font> <font color="#0000a0">Error</font> <font color="#0000a0">Resume</font> <font color="#0000a0">Next</font>
    <font color="#0000a0">Set</font> acadApp = GetObject(, &quot;AutoCAD.Application&quot;)
    <font color="#0000a0">If</font> Err <font color="#0000a0">Then</font>
       Err.Clear
       <font color="#0000a0">Set</font> acadApp = CreateObject(&quot;AutoCAD.Application&quot;)
       <font color="#0000a0">If</font> Err <font color="#0000a0">Then</font>
     MsgBox Err.Description &amp; &quot;  &quot; &amp; Err.Number
     <font color="#0000a0">Exit</font> <font color="#0000a0">Sub</font>
       <font color="#0000a0">End</font> <font color="#0000a0">If</font>
    <font color="#0000a0">End</font> <font color="#0000a0">If</font>
    <font color="#0000a0">On</font> <font color="#0000a0">Error</font> <font color="#0000a0">GoTo</font> 0
    acadApp.Visible = <font color="#0000a0">True</font>
    <font color="#0000a0">Set</font> acadPrefFiles = acadApp.Preferences.Files
   
    <font color="#0000a0">Dim</font> strCurrentSuppPath <font color="#0000a0">As</font> <font color="#0000a0">String</font>
<font color="#008000">    ' set the current path to temporary variable</font>
    strCurrentSuppPath = acadPrefFiles.SupportPath   
<font color="#008000">    ' example: You want to add this </font>
<font color="#008000">    ' &quot;c:\test&quot; path to the support path</font>
    acadPrefFiles.SupportPath = strCurrentSuppPath &amp; _
        &quot;;&quot; &amp; &quot;c:\test&quot;
<font color="#0000a0">End</font> <font color="#0000a0">Sub</font>
</font><font face="Courier New"><span><font color="#0000ff"><font style="font-size: 8pt"></font></font></span></font></pre>

  <pre><font face="Courier New"><span><font color="#0000ff"><font style="font-size: 8pt">void</font></font></span><font style="font-size: 8pt" color="#000000"> fSetSupportPath()</font></font></pre>

  <div style="font-family: ; background: white">
    <p style="margin: 0px"><font face="Courier New"><font style="font-size: 8pt" color="#000000">{</font></font></p>

    <p style="margin: 0px"><font face="Courier New"><font style="font-size: 8pt" color="#000000">&#160; IAcadApplicationPtr pApp = NULL;</font></font></p>

    <p style="margin: 0px"><font face="Courier New"><font style="font-size: 8pt" color="#000000">&#160; IAcadPreferencesPtr pPref = NULL;</font></font></p>

    <p style="margin: 0px"><font face="Courier New"><font style="font-size: 8pt" color="#000000">&#160; IAcadPreferencesFilesPtr pPrefFiles = NULL;</font></font></p>

    <p style="margin: 0px"><font face="Courier New"><font style="font-size: 8pt" color="#000000">&#160;</font></font></p>

    <p style="margin: 0px"><font face="Courier New"><font style="font-size: 8pt" color="#000000">&#160; pApp = acedGetAcadWinApp()-&gt;GetIDispatch(TRUE);</font></font></p>

    <p style="margin: 0px"><font face="Courier New"><font style="font-size: 8pt" color="#000000">&#160; pPref = pApp-&gt;Preferences;</font></font></p>

    <p style="margin: 0px"><font face="Courier New"><font style="font-size: 8pt" color="#000000">&#160;</font></font></p>

    <p style="margin: 0px"><font face="Courier New"><font style="font-size: 8pt" color="#000000">&#160; pPrefFiles = pPref-&gt;Files;</font></font></p>

    <p style="margin: 0px"><font face="Courier New"><font style="font-size: 8pt" color="#000000">&#160;</font></font></p>

    <p style="margin: 0px"><font face="Courier New"><font style="font-size: 8pt" color="#000000">&#160; _bstr_t strOldPath;</font></font></p>

    <p style="margin: 0px"><font face="Courier New"><font style="font-size: 8pt" color="#000000">&#160; strOldPath = pPrefFiles-&gt;GetSupportPath();</font></font></p>

    <p style="margin: 0px"><font face="Courier New"><font style="font-size: 8pt" color="#000000">&#160;</font></font></p>

    <p style="margin: 0px"><font face="Courier New"><font color="#000000"><font style="font-size: 8pt">&#160; </font></font><span><font style="font-size: 8pt" color="#008000">//print old support path</font></span></font></p>

    <p style="margin: 0px"><font face="Courier New"><font color="#000000"><font style="font-size: 8pt">&#160; acutPrintf(_T(</font></font><font style="font-size: 8pt"><span><font color="#a31515">&quot;\nOld Support path: %s&quot;</font></span><font color="#000000">),</font></font></font></p>

    <p style="margin: 0px"><font face="Courier New"><font style="font-size: 8pt" color="#000000">&#160;&#160;&#160; (ACHAR *)(_bstr_t)pPrefFiles-&gt;GetSupportPath());</font></font></p>

    <p style="margin: 0px"><font face="Courier New"><font style="font-size: 8pt" color="#000000">&#160;</font></font></p>

    <p style="margin: 0px"><font face="Courier New"><font color="#000000"><font style="font-size: 8pt">&#160; </font></font><span><font style="font-size: 8pt" color="#008000">//set the new support path</font></span></font></p>

    <p style="margin: 0px"><font face="Courier New"><font color="#000000"><font style="font-size: 8pt">&#160; pPrefFiles-&gt;PutSupportPath(strOldPath + _bstr_t(</font></font><font style="font-size: 8pt"><span><font color="#a31515">&quot;;c:\\temp&quot;</font></span><font color="#000000">));</font></font></font></p>

    <p style="margin: 0px"><font face="Courier New"><font style="font-size: 8pt" color="#000000">}</font></font></p>

    <p style="margin: 0px"><font face="Courier New"><span><font color="#0000ff"><font style="font-size: 8pt">catch</font></font></span><font style="font-size: 8pt" color="#000000">(_com_error &amp;es)</font></font></p>

    <p style="margin: 0px"><font face="Courier New"><font style="font-size: 8pt" color="#000000">{</font></font></p>

    <p style="margin: 0px"><font face="Courier New"><font style="font-size: 8pt" color="#000000">}</font></font></p>
  </div>
