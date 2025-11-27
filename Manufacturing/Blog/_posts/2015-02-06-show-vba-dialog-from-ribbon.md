---
layout: "post"
title: "Show VBA dialog from Ribbon"
date: "2015-02-06 17:15:46"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "Inventor"
original_url: "https://adndevblog.typepad.com/manufacturing/2015/02/show-vba-dialog-from-ribbon.html "
typepad_basename: "show-vba-dialog-from-ribbon"
typepad_status: "Publish"
---

<p>By&#0160;<a href="http://adndevblog.typepad.com/manufacturing/adam-nagy.html">Adam Nagy</a></p>
<p>Anything that shows in the <strong>Macros</strong> dialog can also be hooked up to a new&#0160;<strong>Ribbon</strong> button:&#0160;<a href="http://adndevblog.typepad.com/manufacturing/2015/01/userform-function-not-listed-in-macros-dialog.html" target="_self" title="">http://adndevblog.typepad.com/manufacturing/2015/01/userform-function-not-listed-in-macros-dialog.html</a></p>
<p>There are two types of functions:<br />- <strong>Sub</strong>: that does not return a value<br />- <strong>Function</strong>: that returns a value and so its declaration <br />&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160;ends with an &quot;As &lt;object type&gt;&quot;, e.g.:</p>
<pre>Public Function GetMeAString() <strong>As String</strong>
  GetMeAString = &quot;Some string&quot;
End Function</pre>
<p>In the <strong>Macros</strong> dialog only <strong>Sub</strong>&#39;s will show up as running a macro does not require a result. The <strong>Customize</strong> dialog also only shows <strong>Sub</strong>&#39;s, but also only the ones from the <strong>Default VBA project</strong>&#0160;- the one set here:</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c7483972970b-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="VBA1" class="asset  asset-image at-xid-6a0167607c2431970b01b7c7483972970b img-responsive" src="/assets/image_66e593.jpg" style="width: 420px;" title="VBA1" /></a></p>
<p>Inside the <strong>Customize</strong> dialog you can narrow down the search to <strong>Macros</strong> to make it easier to find and then place it on any of the <strong>Ribbon</strong>&#0160;tabs:&#0160;</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb07ebdb3a970d-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="VBA2" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01bb07ebdb3a970d image-full img-responsive" src="/assets/image_35d684.jpg" title="VBA2" /></a></p>
<p>In order for our form to pop up when the button on the <strong>Ribbon</strong> is clicked we have to set things up the same way as it was done in the <a href="http://adndevblog.typepad.com/manufacturing/2015/01/userform-function-not-listed-in-macros-dialog.html" target="_self">other blog post</a>:</p>
<pre>Public myForm As UserForm1

Public Sub MySub()
  Set myForm = New UserForm1
  myForm.Show
End Sub</pre>
<p>The way you can edit your form should be the same in all <strong>VBA</strong> environments, like the one in <strong>Excel</strong>, so you can find other resources on that on the web, e.g. <a href="https://msdn.microsoft.com/en-us/library/office/aa192538(v=office.11).aspx" target="_self" title="">https://msdn.microsoft.com/en-us/library/office/aa192538(v=office.11).aspx</a>&#0160;</p>
