---
layout: "post"
title: "&ldquo;Nothing to Redo&rdquo; message appears straight after UNDOing in AutoCAD for .NET and ObjectARX"
date: "2012-06-22 15:44:25"
author: "Fenton Webb"
categories:
  - ".NET"
  - "2010"
  - "2011"
  - "2012"
  - "2013"
  - "AutoCAD"
  - "Fenton Webb"
  - "ObjectARX"
original_url: "https://adndevblog.typepad.com/autocad/2012/06/nothing-to-redo-message-appears-straight-after-undoing-in-autocad-for-net-and-objectarx.html "
typepad_basename: "nothing-to-redo-message-appears-straight-after-undoing-in-autocad-for-net-and-objectarx"
typepad_status: "Publish"
---

<p>by <a href="http://adndevblog.typepad.com/autocad/fenton-webb.html">Fenton Webb</a></p>  <p>I wonder how many people have this issue? </p>  <p>A message appears when running the REDO command in AutoCAD straight after running the UNDO command… There’s a relatively simple explanation as to why this is happening. </p>  <p>In short, calling StartTransaction() or opening anything for kForWrite whenever UNDO or REDO is occurring will cause this issue to happen.</p>  <p>A lot of developers want to make special updates to objects while AutoCAD is running commands, perhaps they want to relink interconnecting entities, update parts schedules or move objects in relation to others. </p>  <p>Some examples of places where they might run this type of update code are:</p>  <ol>   <li>In a CommandEnded event or reactor notification</li>    <li>In an ObjectModified, ObjectAppended, etc. event or reactor notification</li>    <li>In a Overrule or Custom entity callback</li>    <li>any other event or reactor notification</li> </ol>  <p>The problem stems from the fact that when you run the UNDO or REDO commands inside of AutoCAD, those commands also invoke the same callback mechanisms that all of the other normal commands do. </p>  <p>If that happens, and you call StartTransaction() or you open something for kForWrite from within one of those callback mechanisms, the REDO will fail. The reason is that StartTransaction() and kForWrite both initialize the UNDO filing system, and if you do that initialization while UNDO or REDO is happening then you trash the UNDO filer.</p>  <p>Here’s some tips on how to operate on entities from within these events or reactor notifications:</p>  <ol>   <li>Don’t use StartTransaction(), simply calling this function while UNDO or REDO is running will blitz the UNDO filer – instead call StartOpenCloseTransaction() or use Open(kForRead).</li>    <li>Opening something for kForRead is fine, even when UNDOing, so you can safely record any updates that are happening in your own buffers.</li>    <li>There’s no need to update anything while the UNDO or REDO is happening – why? Because the UNDO and REDO will restore it for you anyway…</li> </ol>  <p>So what are our options…</p>    <ol>   <li><strong>In a CommandEnded event or reactor notification</strong></li>    <ol>     <li>you just need to make sure that UNDO and/or REDO is not happening by testing the command string that is passed to the event callback.</li>   </ol>    <li><strong>In an ObjectModified, ObjectAppended, etc event or reactor notification</strong></li>    <ol>     <li>you can test the entity that is passed to see if the IsUndoing property is set to true, if it is, do nothing.</li>   </ol>    <li><strong>In a Overrule or Custom entity callback</strong></li>    <ol>     <li>simply testing the host (this or me) object to see if the callback is occurring with IsUndoing is set to true, if it is, do nothing.</li>   </ol> </ol>
