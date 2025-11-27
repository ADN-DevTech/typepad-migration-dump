---
layout: "post"
title: "List sheet names in iLogic Form"
date: "2014-07-24 01:32:00"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "iLogic"
  - "Inventor"
original_url: "https://adndevblog.typepad.com/manufacturing/2014/07/list-sheet-names-in-ilogic-form.html "
typepad_basename: "list-sheet-names-in-ilogic-form"
typepad_status: "Publish"
---

<p>By&#0160;<a href="http://adndevblog.typepad.com/manufacturing/adam-nagy.html" target="_self">Adam Nagy</a></p>
<p>You can get the list of drawing sheets from any language/environment that can use the Inventor API: <strong>.NET</strong>, <strong>VBA</strong>, <strong>iLogic, etc</strong>. If you want to store the last selection you could store that in a user parameter or iProperty, maybe an attribute. There is a series of articles on the possibilities:&#0160;<br />-&#0160;<a href="http://adndevblog.typepad.com/manufacturing/2013/03/save-extra-data-in-inventor-file-1.html" target="_self" title="">http://adndevblog.typepad.com/manufacturing/2013/03/save-extra-data-in-inventor-file-1.html<br /></a>-&#0160;<a href="http://adndevblog.typepad.com/manufacturing/2013/03/save-extra-data-in-inventor-file-2.html" target="_self" title="">http://adndevblog.typepad.com/manufacturing/2013/03/save-extra-data-in-inventor-file-2.html</a>&#0160;<br />-&#0160;<a href="http://adndevblog.typepad.com/manufacturing/2013/03/save-extra-data-in-inventor-file-3.html" target="_self" title="">http://adndevblog.typepad.com/manufacturing/2013/03/save-extra-data-in-inventor-file-3.html</a></p>
<p>If you want to show all the sheet names in the <strong>Form</strong>, then the easiest might be to use a <strong>Multi-Value User Parameter</strong>.&#0160;</p>
<p>1) Create a <strong>Multi-Value User Parameter</strong>&#0160;(named e.g. <strong>Sheets</strong>) in the drawing document through <strong>Manage</strong> &gt;&gt; <strong>Parameters</strong>, and turn it into a <strong>Multi-Value parameter</strong> by <strong>right-clicking</strong> on it and selecting <strong>Make Multi-Value </strong>- you don&#39;t have to set the values, the below rule will do that</p>
<p>2) Create a rule that will keep the values of that parameter up-to-date:<strong>&#0160;Sheets&#0160;</strong>rule</p>
<pre>Dim MyArrayList As New ArrayList
For Each s In ThisDoc.Document.Sheets
  MyArrayList.Add(s.Name)	
Next

MultiValue.List(&quot;Sheets&quot;) = MyArrayList</pre>
<p>3) Create a rule that the user can run once selecting the sheet (in this case e.g. print):<strong>&#0160;PrintSheet&#0160;</strong>rule</p>
<pre>&#39; We are getting the value through &quot;Parameter&quot;
&#39; If we tried to get it from &quot;Sheets&quot; directly
&#39; then this rule would run every time
&#39; the value of that parameter changed
MsgBox(&quot;Printing &quot; + Parameter(&quot;Sheets&quot;), , &quot;Print Sheet&quot;)</pre>
<p>4) Create the Form that will have the <strong>Sheets parameter</strong> and the <strong>PrintSheet rule</strong> in it</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01a511e7b368970c-pi" style="display: inline;"><img alt="Printsheet" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01a511e7b368970c image-full img-responsive" src="/assets/image_cdcf9b.jpg" title="Printsheet" /></a></p>
<p>Result of running the <strong>Print</strong>&#0160;<strong>Form</strong>, selecting a sheet and clicking the <strong>Print</strong> button:</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01a511e7b4ab970c-pi" style="display: inline;"><img alt="Result" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01a511e7b4ab970c img-responsive" src="/assets/image_3c34f3.jpg" title="Result" /></a></p>
<p>&#0160;</p>
