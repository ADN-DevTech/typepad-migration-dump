---
layout: "post"
title: "Update TextBox in Sketch"
date: "2014-10-22 06:36:00"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "Inventor"
original_url: "https://adndevblog.typepad.com/manufacturing/2014/10/update-textbox-in-sketch.html "
typepad_basename: "update-textbox-in-sketch"
typepad_status: "Publish"
---

<p>By&#0160;<a href="http://adndevblog.typepad.com/autocad/adam-nagy.html" target="_self">Adam Nagy</a></p>
<p>If you want to show the current volume of the model inside a sketch text box, then you either need to keep updating a <strong>TextBox</strong> with the latest information yourself or base the <strong>TextBox</strong> information on a <strong>User</strong> or <strong>Model</strong> <strong>Parameter</strong> that is updated from the actual Volume of the model.</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb079c7ced970d-pi" style="display: inline;"><img alt="Model" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01bb079c7ced970d image-full img-responsive" src="/assets/image_4e5ef5.jpg" title="Model" /></a><br />In case of our simple model you could easily calculate the Volume yourself based on the 3 dimensions of this box model. However, in case of a complicated model you would need to rely on the calculated value provided by <strong>Inventor</strong>, e.g. <strong>iProperties.Volume</strong>. This however requires a <strong>model geometry recalculation</strong> to be correct. So if you use it right after a model parameter changed, but before the model geometry got recalculated, then it will be incorrect. &#0160;</p>
<p>If you subscribed the <strong>Rule</strong> to both&#0160;<strong>Any Model Parameter Change</strong>&#0160;and&#0160;<strong>Part Geometry Change </strong>and<strong>&#0160;</strong>your <strong>Rule</strong> relies on information that is calculated from geometry, like Volume, then <strong>Any Model Parameter Change</strong>&#0160;event is too early and better to remove it from the list of events that trigger your rule:</p>
<p><a class="asset-img-link" href="http://a7.typepad.com/6a0112791b8fe628a401b8d0815d97970c-pi" style="display: inline;"><img alt="Ruletrigger" class="asset  asset-image at-xid-6a0112791b8fe628a401b8d0815d97970c img-responsive" src="/assets/image_21b3ac.jpg" style="width: 400px;" title="Ruletrigger" /></a></p>
<p>If you did not do the above and your <strong>Rule</strong> is not forcing an update explicitly (e.g. using <span style="color: purple; font-family: &#39;Courier New&#39;; font-size: small;"><strong>InventorVb</strong></span><span style="font-family: &#39;Courier New&#39;; font-size: small;">.</span><span style="color: purple; font-family: &#39;Courier New&#39;; font-size: small;"><strong>DocumentUpdate</strong></span><span style="font-family: &#39;Courier New&#39;; font-size: small;"><strong>(</strong></span><span style="font-family: &#39;Courier New&#39;; font-size: small;">False</span><span style="font-family: &#39;Courier New&#39;; font-size: small;"><strong>)</strong></span>) before reading the <strong>iProperties.Volume</strong> property then this would happen when changing parameters directly in the <strong>Parameters</strong> dialog:</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c6f7516f970b-pi" style="display: inline;"><img alt="Parameters" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b7c6f7516f970b image-full img-responsive" src="/assets/image_7c5674.jpg" title="Parameters" /></a></p>
<p>So the above was enough to make sure that our own <strong>Volume</strong> User Parameter will always be up-to-date. Then to make the <strong>TextBox</strong> up-to-date as well, we can simply do this which will trigger an update after our <strong>Rule</strong> finished:</p>
<p><strong>iLogicVb</strong>.<strong>UpdateWhenDone</strong> <strong>=</strong> True</p>
<p>We are also checking the Volume based on liters, so we use <strong>UnitsOfMeasure</strong> object to convert the Volume to liters. Here is the <strong>Rule</strong> code:</p>
<pre>&#39; This is in mm^3 because of the 
&#39; Part Document&#39;s unit settings
&#39; You have to set the rule
&#39; so that it&#39;s only triggered for
&#39; &quot;Part Geometry Change&quot; event, so 
&#39; that the iProperties.Volume is correct
Volume = iProperties.Volume

&#39; If we want it in liters
Dim uom As UnitsOfMeasure
uom = ThisApplication.ActiveDocument.UnitsOfMeasure
  
Dim lengthUnit As String
lengthUnit = uom.GetStringFromType(uom.LengthUnits)

&#39; mm^3 to liters  
VV = uom.ConvertUnits(Volume, lengthUnit + &quot;^3&quot;, &quot;l&quot;)

&#39; In liters
V_needed = 2500

&#39; Need to use accuracy
acc = .0001

&#39; The values are equal or VV is bigger
If VV &gt; V_needed - acc Then
  iProperties.PartColor = &quot;Green Pastel&quot;
Else
  iProperties.PartColor = &quot;Red&quot;
End If

&#39; This is enough to get the sketch text updated
&#39; to the new Volume value
 
iLogicVb.UpdateWhenDone = True
</pre>
<p>Sample model:&#0160; <span class="asset  asset-generic at-xid-6a0167607c2431970b01b7c6f76b6b970b img-responsive"><a href="http://adndevblog.typepad.com/files/tank-volume-2014.ipt">Download Tank volume 2014</a></span></p>
