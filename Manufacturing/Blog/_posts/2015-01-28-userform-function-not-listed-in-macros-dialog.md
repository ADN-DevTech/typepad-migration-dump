---
layout: "post"
title: "UserForm function not listed in Macros dialog"
date: "2015-01-28 16:41:34"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "Inventor"
original_url: "https://adndevblog.typepad.com/manufacturing/2015/01/userform-function-not-listed-in-macros-dialog.html "
typepad_basename: "userform-function-not-listed-in-macros-dialog"
typepad_status: "Publish"
---

<p>By&#0160;<a href="http://adndevblog.typepad.com/manufacturing/adam-nagy.html">Adam Nagy</a></p>
<p>Let&#39;s say we have a <strong>VBA</strong> project with a <strong>Class</strong>, a <strong>UserForm</strong> and a <strong>Module</strong>, each with a single function in it:</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d0ca9e52970c-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="VBA1" class="asset  asset-image at-xid-6a0167607c2431970b01b8d0ca9e52970c img-responsive" src="/assets/image_580fb9.jpg" style="width: 420px;" title="VBA1" /></a><br />When checking in the <strong>Macros</strong> dialog only the <strong>Module</strong>&#39;s function (<strong>MyModuleFunction</strong>) will be shown:</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d0caa9c2970c-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="VBA2" class="asset  asset-image at-xid-6a0167607c2431970b01b8d0caa9c2970c img-responsive" src="/assets/image_76ba59.jpg" style="width: 420px;" title="VBA2" /></a></p>
<p>This is as designed. Both the <strong>Class</strong> and the <strong>UserForm</strong>&#0160;hide their properties and functions from outside because those are instance specific. In other words, if you want to call a function of a <strong>UserForm</strong> you have to create a function in a <strong>Module</strong> which will create an instance of the <strong>UserForm</strong> and through that then you can access the form&#39;s properties and call its functions, including the one to show the form:</p>
<pre>Public myUserForm As UserForm1

Sub MyModuleFunction()
  &#39; We create an instance of the form
  Set myUserForm = New UserForm1
 
  &#39; Now we can access the form&#39;s functions
  &#39; and properties through the variable 
  &#39; named &quot;myUserForm&quot; 

  &#39; If you want to show it as modal,
  &#39; i.e. all the other UI is disabled
  &#39; until the dialog is dismissed, then
  &#39; just delete the word &quot;vbModeless&quot;
  &#39; from the below code
  Call myUserForm.Show(vbModeless)
End Sub</pre>
<p>Now if you call <strong>MyModuleFunction</strong>, it will show our form:</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d0ca9ff1970c-pi" style="display: inline;"><img alt="VBA3" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b8d0ca9ff1970c img-responsive" src="/assets/image_2a454d.jpg" title="VBA3" /></a></p>
<p>&#0160;&#0160;</p>
