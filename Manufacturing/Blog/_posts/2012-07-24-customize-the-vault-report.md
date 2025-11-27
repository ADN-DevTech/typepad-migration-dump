---
layout: "post"
title: "Customize the Vault Report"
date: "2012-07-24 03:48:40"
author: "Barbara Han"
categories:
  - "Barbara Han"
  - "Vault"
original_url: "https://adndevblog.typepad.com/manufacturing/2012/07/customize-the-vault-report.html "
typepad_basename: "customize-the-vault-report"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/manufacturing/barbara-han.html" target="_self">Barbara Han</a></p>  <p>As you may already know, Vault can produce a search report (.RDLC file), which can be customized. If you are not familiar with this feature, I’d like to recommend this link first:</p>  <p><a title="http://images.autodesk.com/adsk/files/report_template_authoring.pdf  " href="http://images.autodesk.com/adsk/files/report_template_authoring.pdf&nbsp; ">http://images.autodesk.com/adsk/files/report_template_authoring.pdf&#160; </a></p>  <p>The report can be customized in a variety of ways.&#160; Here is a simple sample illustrating how to do some customization upon the report template:</p>  <p>1. Ensure there is an existing UDP for the file. If no, create one through Vault Explorer UI before advancing to next step.</p>  <p>2. Use &lt;Vault Professional 2013 installation path&gt;\Explorer\ReportTemplateAuthoringUtility.exe tool to create a new .rdlc file (let’s say “myProp.rdlc”) with just one UDP property that I want to use.   <br />3. Make a copy of an existing report template (e.g. File Details.rdlc) from Vault installation path. Let’s name it as &quot;Copy of File Details.rdlc&quot;.    <br />4. Open the myProp.rdlc file with notepad, and copy the &lt;Field&gt;...&lt;/Field&gt;, use the notepad to open &quot;Copy of File Details.rdlc&quot;, and pasted &lt;Field&gt;...&lt;/Field&gt; into &lt;Fields&gt;...&lt;/Fields&gt; node, save this file.    <br />5. Use Visual Studio 2008 to open this new &quot;Copy of File Details.rdlc&quot;, make some UI design, for example adding two textboxes from toolbox and dragging the UDP from the field list to the design form. Save the .rdlc file and quit VS 2008.    <br />6. Use the result RDLC file template to generate file search report from Vault Explorer .</p>  <p>In my test, I found the .rdlc file won't work as expected if I use Visual Studio 2010 to open it and convert it to RDLC 2008 format. Only Yes and No option are available when Visual Studio 2010 opens the .rdlc file. Yes means you agree to convert, No means VS 2010 uses XML Editor to open it. I have to choose Yes because I want to make some UI design and don’t know how I can make the RDLC design with XML Editor, so…The only way seems to use VS 2008 to do the UI design, or you will get a problematic .rdlc template.</p>
