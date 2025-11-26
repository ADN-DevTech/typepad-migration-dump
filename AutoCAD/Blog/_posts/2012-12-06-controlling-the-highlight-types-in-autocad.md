---
layout: "post"
title: "Controlling the Highlight Types in AutoCAD"
date: "2012-12-06 10:48:33"
author: "Fenton Webb"
categories:
  - ".NET"
  - "2013"
  - "AutoCAD"
  - "Fenton Webb"
  - "ObjectARX"
  - "UI"
original_url: "https://adndevblog.typepad.com/autocad/2012/12/controlling-the-highlight-types-in-autocad.html "
typepad_basename: "controlling-the-highlight-types-in-autocad"
typepad_status: "Publish"
---

<p>by <a href="http://adndevblog.typepad.com/autocad/fenton-webb.html"><font color="#0066cc">Fenton Webb</font></a></p>  <p>When calling AcDbEntity::highlight() or Entity.Highlight() (in .NET) as we all know you get the normal AutoCAD dashed highlight draw.</p>  <p>However, using these functions before and after your highlight call you can really get some nice new highlight types of your own.</p>  <p><strong>void acgsSetHighlightColor( Adesk::UInt16 colorIndex );</strong></p>  <p><strong>void acgsSetHighlightLinePattern( AcGs::LinePattern pattern );</strong></p>  <p><strong>void acgsSetHighlightLineWeight( Adesk::UInt16 weight );</strong></p>  <p>&#160;</p>  <p>If you need to PInvoke these from .NET, here are the C# DLLImport signatures for 2013…</p>  <p><font color="#000000"><strong>[DllImport(&quot;acdb19.dll&quot;, CallingConvention = CallingConvention.Cdecl,       <br />EntryPoint = &quot;</strong></font><a href="mailto:?acgsSetHighlightColor@@YAXG@Z"><font color="#000000"><strong>?acgsSetHighlightColor@@YAXG@Z</strong></font></a><font color="#000000"><strong>&quot;]       <br />public static extern void&#160; acgsSetHighlightColor(uint colorIndex);</strong></font></p>  <p><font color="#000000"><strong>[DllImport(&quot;acdb19.dll&quot;, CallingConvention = CallingConvention.Cdecl,       <br />EntryPoint = &quot;?acgsSetHighlightLinePattern@@YAXW4LinePattern@AcGs@@@Z</strong></font><font color="#000000"><strong>&quot;]       <br />public static extern void&#160; <font color="#232323">acgsSetHighlightLinePattern</font>(int pattern);</strong></font></p>  <p><font color="#000000"><strong>[DllImport</strong></font><font color="#000000"><strong>(&quot;acdb19.dll&quot;, CallingConvention = CallingConvention.Cdecl,       <br />EntryPoint = &quot;?acgsSetHighlightLineWeight@@YAXG@Z</strong></font><font color="#000000"><strong>&quot;]       <br />public static extern void&#160; acgsSetHighlightLineWeight(uint weight);</strong></font></p>
