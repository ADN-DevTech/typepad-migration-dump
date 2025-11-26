---
layout: "post"
title: "Managing events at a per-document level"
date: "2012-08-10 01:19:57"
author: "Philippe Leefsma"
categories:
  - ".NET"
  - "AutoCAD"
  - "Philippe Leefsma"
original_url: "https://adndevblog.typepad.com/autocad/2012/08/managing-events-at-a-per-document-level.html "
typepad_basename: "managing-events-at-a-per-document-level"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/philippe-leefsma.html" target="_self">Philippe Leefsma</a></p>  <p><b>Q:</b></p>  <p>I need to keep track of events such as Database.ObjectAppended, Database.ObjectModified at a per-document level for all documents opened, created and existing in AutoCAD.</p>  <p>What is the best way to achieve this through the .Net API?</p>  <p><a name="section2"></a></p>  <p><b>A:</b></p>  <p>The attached VB.Net sample illustrates how to achieve this:</p>  <p>The idea is to create a custom class, called PerDocController in my example, that will be responsible for holding the events at a per-document level.</p>  <p>The command class is then holding a dictionary of <em>&lt;Document, PerDocController&gt;</em> pairs for each loaded document.</p>  <p>The events <em>DocumentActivated</em> and <em>DocumentToBeDestroyed</em> are then used to keep this dictionary up-to-date.</p>  <p>The complete code is provided in the attached project.</p>  <div style="padding-bottom: 0px; margin: 0px; padding-left: 0px; padding-right: 0px; display: inline; float: none; padding-top: 0px" id="scid:fb3a1972-4489-4e52-abe7-25a00bb07fdf:c48e5daa-d2b8-4862-b0bb-58b3061de5bc" class="wlWriterEditableSmartContent"><p> <a href="http://adndevblog.typepad.com/_perdocmanagervbnet.zip" target="_blank">_perdocmanagervbnet.zip</a></p></div>
