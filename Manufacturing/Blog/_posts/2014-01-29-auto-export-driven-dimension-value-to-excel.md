---
layout: "post"
title: "Auto export driven dimension value to Excel"
date: "2014-01-29 16:35:43"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "iLogic"
  - "Inventor"
original_url: "https://adndevblog.typepad.com/manufacturing/2014/01/auto-export-driven-dimension-value-to-excel.html "
typepad_basename: "auto-export-driven-dimension-value-to-excel"
typepad_status: "Publish"
---

<p>By&#0160;<a href="http://adndevblog.typepad.com/manufacturing/adam-nagy.html" target="_self">Adam Nagy</a></p>
<p>Sometimes there could be a main dimension that causes the whole model to change, and so the values of the driven dimensions change as well. You could either monitor the main dimension for change using <strong>iLogic</strong> or one of the driven dimensions. In this case I&#39;m going to do the latter.</p>
<p>Let&#39;s say we have the following part with a sketch that defines the <strong>Width</strong> and <strong>Height</strong> of the base of the model. We also have at the bottom a <strong>Driven Dimension</strong> associated with a&#0160;<strong>Reference Parameter</strong> named&#0160;<strong>DrivenWidth</strong>&#0160;which changes when the <strong>Width</strong> changes:</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01a73d6a33bc970d-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="Dims" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01a73d6a33bc970d img-responsive" src="/assets/image_01b911.jpg" title="Dims" /></a></p>
<p>I then create a rule named <strong>DrivenWidthRule</strong> which has&#0160;<strong>DrivenWidth=<strong>DrivenWidth </strong></strong>in it so it will be recognized as a<strong><strong> Driving Rule </strong></strong>for<strong><strong> DrivenWidth</strong></strong> - even though it&#39;s only affected by <strong>Width:</strong></p>
<p><strong> <a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01a3fcaf2522970b-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="Params" class="asset  asset-image at-xid-6a0167607c2431970b01a3fcaf2522970b img-responsive" src="/assets/image_941944.jpg" style="width: 450px;" title="Params" /></a></strong></p>
<p><strong>DrivenWidthRule:</strong></p>
<pre>&#39; To fake a &quot;Driving Rule&quot;
DrivenWidth=DrivenWidth
&#39; Actually simply using the parameter would be enough to trigger the rule
&#39; Dim t = DrivenWidth

xlsFilePath = 
&quot;C:\Users\Administrator\Documents\Inventor\MyProject\parameters.xls&quot;

&#39; Write info to excel file
GoExcel.Open(xlsFilePath, &quot;Sheet1&quot;)

&#39; Find the relevant parameter
&#39; (in case we store many of them)
&#39; Names are in column A with title &quot;Name&quot;
i = GoExcel.FindRow(xlsFilePath, &quot;Sheet1&quot;, &quot;Name&quot;, &quot;=&quot;, &quot;DrivenWidth&quot;)
If i &gt; 0 Then
  &#39; Found it
  &#39; Values are in column B
  GoExcel.CellValue(xlsFilePath, &quot;Sheet1&quot;, &quot;B&quot; + i.ToString()) = 
    DrivenWidth
End If

GoExcel.Save
GoExcel.Close</pre>
<p>Now whenever <strong>Width</strong> changes, it will change <strong>DrivenWidth</strong> and so <strong>DrivenWidthRule</strong> will be called which then can update the values in the <strong>Excel</strong> file:</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01a3fcaf2530970b-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="Excel" class="asset  asset-image at-xid-6a0167607c2431970b01a3fcaf2530970b img-responsive" src="/assets/image_c3639f.jpg" style="width: 450px;" title="Excel" /></a></p>
<p>&#0160;</p>
