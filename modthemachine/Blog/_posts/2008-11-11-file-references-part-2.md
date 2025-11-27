---
layout: "post"
title: "File References Part 2"
date: "2008-11-11 21:51:54"
author: "Adam Nagy"
categories:
  - "Assemblies"
  - "Brian"
original_url: "https://modthemachine.typepad.com/my_weblog/2008/11/file-references-part-2.html "
typepad_basename: "file-references-part-2"
typepad_status: "Publish"
---

<p>I received a question related to my last posting that I thought was good and also helps to illustrate the previous posting.&#0160; The question that came in asked about the new <strong>Open Drawing</strong> command that is part of the subscription pack.&#0160; When you have a part, assembly, or presentation document open and right-click on the top node in the browser you&#39;ll see the new <strong>Open Drawing</strong> command in the context menu, as shown below.</p>
<p><a href="http://modthemachine.typepad.com/.a/6a00e553fcbfc68834010535ec13ce970c-pi"><img alt="OpenDrawing" border="0" height="230" src="/assets/image_832933.jpg" style="BORDER-TOP-WIDTH: 0px; BORDER-LEFT-WIDTH: 0px; BORDER-BOTTOM-WIDTH: 0px; BORDER-RIGHT-WIDTH: 0px" width="244" /></a> </p>
<p>Based on the last posting, the obvious question is how can this work since we learned that the part doesn&#39;t know which documents it&#39;s referenced in.&#0160; Is there something I wasn&#39;t telling you?&#0160; The answer is that nothing has changed from the last posting and everything I explained there still applies.&#0160; But then how can this command work?&#0160; It&#39;s actually quite simple how the command works and could have easily been implemented as a small add-in command.</p>
<p>When you run the <strong>Open Drawing</strong> command it looks in the current directory and subdirectories for a file that has the same name as the active document, but with a .idw or .dwg file extension.&#0160; If it doesn&#39;t find the file in the current directory or subdirectory it will search the other directories defined by the active project.&#0160; If it still doesn&#39;t find the file then it displays the Open dialog to allow you to specify the file.&#0160; The part doesn&#39;t have any knowledge of the drawing, it&#39;s only the program logic that&#39;s making that association based on the filename of the part.</p>
<p>There was also a related question in the customization newsgroup that I thought was answered in the previous posting but I think probably needs to be emphasized.&#0160; The concept to understand is that a reference to another document is just the filename, i.e. MyPart.ipt, NOT the full filename, C:\Temp\MyPart.ipt.&#0160; When Inventor opens an assembly it reads the file reference list and looks for the referenced files on disk.&#0160; It does this using the file name and the active project to define the possible locations of the file.&#0160; <span style="text-decoration: underline;">The location is not part of the file reference</span>.&#0160; The important thing to get from this is that if you need to reorganize your files and move them around on disk you don&#39;t need to do anything about the file references.&#0160; You only need to make sure that the project specifies the directories that contain the files.&#0160; The only time you need to change file references is when you&#39;ve changed the filename of the file.&#0160; </p>
