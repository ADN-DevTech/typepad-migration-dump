---
layout: "post"
title: "Setting The Project File Search Path"
date: "2012-12-12 16:25:26"
author: "Gopinath Taget"
categories:
  - "2010"
  - "2011"
  - "2012"
  - "2013"
  - "ActiveX"
  - "AutoCAD"
  - "Gopinath Taget"
original_url: "https://adndevblog.typepad.com/autocad/2012/12/setting-the-project-file-search-path.html "
typepad_basename: "setting-the-project-file-search-path"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/gopinath-taget.html">Gopinath Taget</a></p> <p>If you use the Drawing projects feature of AutoCAD regularly, you might wonder how to get/set the project file search path programmatically?</p> <p>To get the project file path name, use the GetProjectFilePath method of AcadPreferencesFiles object accessible using the ActiveX API. You can also set the project file path name using SetProjectFilePath method of AcadPreferencesFiles object. And the current project name is stored in the system variable "PROJECTNAME".  <p>There are however no ActiveX methods available to obtain the list of project names that you can access through Options dialog in the UI. One work around would be to directly read the information from the registry. The project names will be available under the following key. <p>HKEY_CURRENT_USER\Software\Autodesk\AutoCAD\RXX.X\ACAD-XXXX:XXX\Profiles\&lt;&lt;Unnamed Profile&gt;&gt;\Project Settings\ <p>The following sample VBA code gets the path information for the Project called "TestProject". Remember to add a project by name TestProject before testing this sample.<pre></pre>
<div style="font-family: ; background: white">
<p style="margin: 0px"><font face="Consolas"><span style="line-height: 11pt"><font color="#0000ff"><font style="font-size: 8pt">Sub</font></font></span><span style="line-height: 11pt"><font style="font-size: 8pt" color="#000000"> f_test()</font></span></font></p>
<p style="margin: 0px"><font face="Consolas"><span style="line-height: 11pt"><font color="#000000"><font style="font-size: 8pt">&nbsp; </font></font></span><font style="font-size: 8pt"><span style="line-height: 11pt"><font color="#0000ff">Dim</font></span><span style="line-height: 11pt"><font color="#000000"> po_pref </font></span><span style="line-height: 11pt"><font color="#0000ff">As</font></span></font><span style="line-height: 11pt"><font style="font-size: 8pt" color="#000000"> AcadPreferences</font></span></font></p>
<p style="margin: 0px"><span style="line-height: 11pt"><font face="Consolas"><font style="font-size: 8pt" color="#000000">&nbsp; Set po_pref = ThisDrawing.Application.Preferences</font></font></span></p>
<p style="margin: 0px"><font face="Consolas"><font style="font-size: 8pt" color="#000000">&nbsp;</font></font></p>
<p style="margin: 0px"><font face="Consolas"><span style="line-height: 11pt"><font color="#000000"><font style="font-size: 8pt">&nbsp; MsgBox po_pref.Files.GetProjectFilePath(</font></font></span><font style="font-size: 8pt"><span style="line-height: 11pt"><font color="#a31515">"TestProject"</font></span></font><span style="line-height: 11pt"><font style="font-size: 8pt" color="#000000">)</font></span></font></p>
<p style="margin: 0px"><font face="Consolas"><span style="line-height: 11pt"><font color="#000000"><font style="font-size: 8pt">&nbsp; </font></font></span><font style="font-size: 8pt"><span style="line-height: 11pt"><font color="#0000ff">Call</font></span><span style="line-height: 11pt"><font color="#000000"> po_pref.Files.SetProjectFilePath(</font></span><span style="line-height: 11pt"><font color="#a31515">"TestProject"</font></span><span style="line-height: 11pt"><font color="#000000">, </font></span></font></font></p>
<p style="margin: 0px"><font face="Consolas"><font style="font-size: 8pt"><span style="line-height: 11pt"><font color="#000000">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </font></span><span style="line-height: 11pt"><font color="#a31515">"c:\my documents"</font></span></font><span style="line-height: 11pt"><font style="font-size: 8pt" color="#000000">)</font></span></font></p>
<p style="margin: 0px"><font face="Consolas"><span style="line-height: 11pt"><font color="#0000ff"><font style="font-size: 8pt">End</font></font></span><font style="font-size: 8pt"><span style="line-height: 11pt"><font color="#000000"> </font></span></font><span style="line-height: 11pt"><font style="font-size: 8pt" color="#0000ff">Sub</font></span></font></p></div>
