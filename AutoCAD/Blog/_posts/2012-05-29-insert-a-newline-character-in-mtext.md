---
layout: "post"
title: "Insert a newline character in MText"
date: "2012-05-29 11:05:24"
author: "Augusto Goncalves"
categories:
  - ".NET"
  - "Augusto Goncalves"
  - "AutoCAD"
  - "ObjectARX"
original_url: "https://adndevblog.typepad.com/autocad/2012/05/insert-a-newline-character-in-mtext.html "
typepad_basename: "insert-a-newline-character-in-mtext"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/augusto-goncalves.html">Augusto Goncalves</a></p>  <p>For <strong>MText</strong> (.NET) or <strong>AcDbMText</strong> (ObjectARX C++) entities, the newline character is \P as opposed to the traditional '\n' escape character, so setting the content use <strong>\\P</strong> C#/C++ (double backslash is interpreted as one) or <strong>\P</strong> for VB.NET.</p>  <p>(C#)</p>  <div style="font-family: ; background: white">   <p style="margin: 0px"><font face="Courier New"><span><font color="#2b91af"><font style="font-size: 8pt">MText</font></font></span><font style="font-size: 8pt"><font color="#000000"> myText = </font></font><span><font style="font-size: 8pt" color="#008000">// open me here</font></span></font></p>    <p style="margin: 0px"><font face="Courier New"><font color="#000000"><font style="font-size: 8pt">myText.Contents = </font></font><font style="font-size: 8pt"><span><font color="#a31515">&quot;First line \\P Second line&quot;</font></span><font color="#000000">;</font></font></font></p> </div>  <p>(VB.NET)</p>  <div style="font-family: ; background: white">   <p style="margin: 0px"><font face="Courier New"><font style="font-size: 8pt"><span><font color="#0000ff">Dim</font></span><font color="#000000"> myText </font><span><font color="#0000ff">As</font></span><font color="#000000"> </font><span><font color="#2b91af">MText</font></span><font color="#000000"> = </font></font><span><font style="font-size: 8pt" color="#008000">' open me here</font></span></font></p>    <p style="margin: 0px"><font face="Courier New"><font color="#000000"><font style="font-size: 8pt">myText.Contents = </font></font><span><font style="font-size: 8pt" color="#a31515">&quot;First line \P Second line&quot;</font></span></font></p> </div>  <p>For a complete list of multi-line text formatting characters, visit <a href="http://docs.autodesk.com/ACD/2013/ENU/files/GUID-7D8BB40F-5C4E-4AE5-BD75-9ED7112E5967.htm" target="_blank">Format Codes for Alternate Text Editor Reference</a>.</p>
