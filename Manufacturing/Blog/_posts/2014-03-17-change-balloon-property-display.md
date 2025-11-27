---
layout: "post"
title: "Change Balloon Property Display"
date: "2014-03-17 11:39:35"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "Inventor"
original_url: "https://adndevblog.typepad.com/manufacturing/2014/03/change-balloon-property-display.html "
typepad_basename: "change-balloon-property-display"
typepad_status: "Publish"
---

<p>By&nbsp;<a href="http://adndevblog.typepad.com/manufacturing/adam-nagy.html" target="_self">Adam Nagy</a></p>
<p>If you want to find out how to set an object through the API to achieve what you want, then the best thing is to do it through the UI and then investigate the object through the API in e.g. <strong>VBA</strong>: <a title="" href="http://adndevblog.typepad.com/manufacturing/2013/10/discover-object-model.html" target="_self">http://adndevblog.typepad.com/manufacturing/2013/10/discover-object-model.html</a></p>
<p>In this case I set the <strong>Balloon</strong> style's <strong>Property Display</strong>&nbsp;to <strong>ITEM, QTY</strong> then check in <strong>VBA</strong> where that information is stored:</p>
<p><a class="asset-img-link" style="display: inline;" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01a3fcd800cd970b-popup"><img class="asset  asset-image at-xid-6a0167607c2431970b01a3fcd800cd970b img-responsive" style="width: 450px;" title="PropertyDisplay2" src="/assets/image_9ff321.jpg" alt="PropertyDisplay2" /></a></p>
<p>I can see that the <strong>Properties</strong> of the <strong>Style</strong> object is set to <strong>"PartsListProperty='45572';PartsListProperty='45575'"</strong>. I will now try to set it through the API. I first set&nbsp;the <strong>Property Display</strong> to something else, e.g.&nbsp;<strong>PART NUMBER, MASS</strong> throgh the UI and then run the below code:</p>
<pre>Sub BalloonStyleCheck()
  Dim doc As DrawingDocument
  Set doc = ThisApplication.ActiveDocument
  
  Dim dsm As DrawingStylesManager
  Set dsm = doc.StylesManager
  
  Dim ods As ObjectDefaultsStyle
  Set ods = dsm.ActiveStandardStyle.ActiveObjectDefaults
  
  Dim bs As BalloonStyle
  Set bs = ods.BalloonStyle
  bs.Properties = _
    "PartsListProperty='45572';PartsListProperty='45575'"
End Sub</pre>
<p>It seems to do the trick:</p>
<p><a class="asset-img-link" style="display: inline;" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01a3fcd801e5970b-popup"><img class="asset  asset-image at-xid-6a0167607c2431970b01a3fcd801e5970b img-responsive" style="width: 450px;" title="PropertyDisplay" src="/assets/image_29963e.jpg" alt="PropertyDisplay" /></a></p>
<p>&nbsp;</p>
