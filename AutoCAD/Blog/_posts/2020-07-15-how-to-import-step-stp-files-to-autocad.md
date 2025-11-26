---
layout: "post"
title: "How to Import Step (.stp) Files to AutoCAD"
date: "2020-07-15 19:25:00"
author: "Madhukar Moogala"
categories:
  - ".NET"
  - "Madhukar Moogala"
original_url: "https://adndevblog.typepad.com/autocad/2020/07/how-to-import-step-stp-files-to-autocad.html "
typepad_basename: "how-to-import-step-stp-files-to-autocad"
typepad_status: "Publish"
---

<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js?skin=sunburst"></script>
<p>By <a href="http://adndevblog.typepad.com/autocad/Madhukar-Moogala.html" target="_self">Madhukar 
Moogala</a></p>

<p>Though there is not a direct API to import a <a href="https://en.wikipedia.org/wiki/ISO_10303-21">Step</a> file in to AutoCAD drawing unlike <code> <a href="https://adndevblog.typepad.com/autocad/2012/05/acisin-and-acisout-in-net.html">Body.AcisIn</a></code>, we use <code>Editor.Command</code> API to invoke Import command</p>
<blockquote><pre class="prettyprint">public void StepIn()
{
Document doc = Application.DocumentManager.MdiActiveDocument;
Editor ed = doc.Editor;            
ed.Command(new object[] { "_.IMPORT",
	                 "D:\\Work\\CADFiles\\Scafolf_Bracket_Asy.stp"});
}
</pre></blockquote>
