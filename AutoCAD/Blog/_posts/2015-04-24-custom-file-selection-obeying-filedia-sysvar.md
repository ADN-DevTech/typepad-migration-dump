---
layout: "post"
title: "Custom file selection obeying FILEDIA sysvar"
date: "2015-04-24 06:43:38"
author: "Augusto Goncalves"
categories:
  - ".NET"
  - "Augusto Goncalves"
  - "AutoCAD"
original_url: "https://adndevblog.typepad.com/autocad/2015/04/custom-file-selection-obeying-filedia-sysvar.html "
typepad_basename: "custom-file-selection-obeying-filedia-sysvar"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/augusto-goncalves.html">Augusto Goncalves</a></p>  <p>When we create a custom command, we want it to integrate with AutoCAD built-in behavior. One of those expected behaviors is the FILEDIA system variable, that indicates if a dialog will show when selecting files. </p>  <p>Many developer make direct use of Autodesk.AutoCAD.Windows.<span style="color: #2b91af">OpenFileDialog</span> class, but this will call directly a dialog. The alternative is <span style="color: #2b91af">PromptSaveFileOptions</span>, that can used with the Editor and has the FILEDIA behavior implemented. </p>  <p>The following code snippet show how to implement it.</p>  <pre style="font-size: 13px; font-family: consolas; background: white; color: black"><font size="2"><span style="color: #2b91af">Editor</span> ed = <span style="color: #2b91af">Application</span>.DocumentManager.MdiActiveDocument.Editor;<br /><span style="color: #2b91af">PromptSaveFileOptions</span> pfso = <span style="color: blue">new</span>&#160;<span style="color: #2b91af">PromptSaveFileOptions</span>(<span style="color: #a31515">&quot;Select a file: &quot;</span>);<br /><span style="color: #2b91af">PromptFileNameResult</span> pfnr = ed.GetFileNameForSave(pfso);<br /><span style="color: blue">if</span> (pfnr.Status != <span style="color: #2b91af">PromptStatus</span>.OK) <span style="color: blue">return</span>;<br /> <br /><span style="color: blue">string</span> fileName = pfnr.StringResult;<br /><span style="color: green"></span></font></pre>

<pre style="font-size: 13px; font-family: consolas; background: white; color: black"><font size="2"><span style="color: green">// do something with the file</span></font></pre>
