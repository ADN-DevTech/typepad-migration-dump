---
layout: "post"
title: "Vault Query"
date: "2010-07-19 08:32:39"
author: "Doug Redmond"
categories:
  - "Sample Applications"
original_url: "https://justonesandzeros.typepad.com/blog/2010/07/vault-query.html "
typepad_basename: "vault-query"
typepad_status: "Publish"
---

<p><img alt="" src="/assets/SampleApp2.png" /> </p>
<p>Update:&#0160; The <a href="http://justonesandzeros.typepad.com/blog/2012/12/vault-query-2013.html" target="_self">Vault 2013 version</a> is now available.&#0160; Sorry, there is no 2012 version.</p>
<p>Time for another sample application.&#0160; This one is built specifically to help out Vault programmers, however I suspect that people will find other uses.</p>
<p>In a nutshell, Vault Query lets you make direct calls to the Web Service API.&#0160; The results are then displayed in a tree view, similar to the Visual Studio debugger.&#0160; This is a good way to sift through complex data, such as property mappings, lifecycle states or BOM data.</p>
<p>Just for fun, I decided to write this in WPF and give it an awesome UI.&#0160; <br /><a href="http://justonesandzeros.typepad.com/images/2010/VaultQuery/Screenshot.png" target="_blank"><img alt="" border="0" src="/assets/Screenshot_scaled.png" style="border: 0px none;" /></a>   <br /><span style="color: #808080;">(click image for full view)</span></p>
<p><strong>Requirements:</strong></p>
<ul>
<li>To use the EXE   
<ul>
<li>.NET 3.5 </li>
<li>Vault 2011 Server (any version) installed on the network. </li>
</ul>
</li>
<li>To use the source code   
<ul>
<li>Visual Studio 2010 </li>
<li>Expression Blend 3 or 4 </li>
</ul>
</li>
</ul>
<p><a href="http://justonesandzeros.typepad.com/Apps/VaultQuery/VaultQuery-1.0.1.0-bin.zip">Click here to download the application</a>   <br /><a href="http://justonesandzeros.typepad.com/Apps/VaultQuery/VaultQuery-1.0.1.0-src.zip">Click here to download the source code</a></p>
<p><strong>To use:   <br /></strong>This is a stand-alone EXE, so you don&#39;t need the Vault Client installed.&#0160; </p>
<p>On the left you have a list of operations.&#0160; Each operation represents a single web service call with specific input parameters.&#0160; Double click on the operation and it runs.&#0160; The output is presented in the right pane.</p>
<p>I set up a bunch of operations out-of-the-box, but you can add your own.&#0160; You can either use the &quot;Add Actions&quot; command or edit the Operations.xml file.&#0160; In theory, any API call can be made into an operation.&#0160; However the UI is picky about what input types it supports.&#0160; You may need to edit Operations.xml for many cases.</p>
<p><img alt="" src="/assets/NewOperation.png" /></p>
