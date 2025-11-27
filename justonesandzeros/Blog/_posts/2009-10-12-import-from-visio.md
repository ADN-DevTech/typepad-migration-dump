---
layout: "post"
title: "Import from Visio"
date: "2009-10-12 16:13:38"
author: "Doug Redmond"
categories:
  - "Sample Applications"
original_url: "https://justonesandzeros.typepad.com/blog/2009/10/import-from-visio.html "
typepad_basename: "import-from-visio"
typepad_status: "Publish"
---

<P><strong><img src="/assets/SampleApp2.png" /> </strong></P>
<P><strong>Overview: <br></strong>“Import from Visio” creates a lifecycle definition in Vault based on information within a Microsoft Visio file.</P>
<P><img src="/assets/ImportUtil.png" />&nbsp; </p>

<br>
<P><strong>Requirements: <br></strong><em>Vault Workgroup 2010</em> or <em>Vault Collaboration 2010</em> or <em>Vault Manufacturing 2010 <br>Microsoft Office Visio 2007</em></p>

<br>
<P><strong>Introduction:</strong> <br>On <A href="http://mfgcommunity.autodesk.com/blogs/blog/view/5/vw_Lifecycle_youtube/" target=_blank>Brian Schanen’s blog</A>, he suggested using Visio to map out your lifecycle definition.&nbsp; Using Visio is a good idea, but once the diagram is complete, you need to set up the states and transitions by hand in Vault.&nbsp; Not any more!&nbsp; Import from Visio automates the process of creating lifecycle states.</P>
<P><A href="http://justonesandzeros.typepad.com/Apps/ImportFromVisio/ImportFromVisio-1.0.1.0-bin.zip" target=_blank>Click here to download the application</A> <br><A href="http://justonesandzeros.typepad.com/Apps/ImportFromVisio/ImportFromVisio-1.0.1.0-src.zip" target=_blank>Click here to download the source code</A></P>
<P><br><strong>How to use:</strong></P>
<ol>
<li>Create your Visio file.&nbsp; Use <strong>Process</strong> boxes for lifecycle states and <strong>Dynamic Connectors</strong> for state transitions.&nbsp; The dynamic connectors can have double arrows. 
<li>Save your diagram and close Visio. 
<li>Run ImportFromVisio.exe. 
<li>Log in as an administrator. 
<li>Select the .VSD file.&nbsp; Visio will automatically launch then immediately close.&nbsp; This is normal.&nbsp; <br><br><img src="/assets/screenshot.png" /> <br>
<li>Fill in the rest of the data. 
<li>Click the “Create Lifecycle Definition” button.&nbsp; You should see an “Import Completed” message after a few seconds. 
<li>Exit ImportFromVisio.exe 
<li>Log into Vault as an administrator. 
<li>Verify that your lifecycle definition exists with the proper states and transitions. 
<li>Add in other data, such as security restrictions, transition criteria and associated categories. </li>
</ol>
<br>
<P>Further information can be found in the readme.</P>
<P>Enjoy.</P>
