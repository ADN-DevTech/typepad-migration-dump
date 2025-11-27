---
layout: "post"
title: "New API's in Inventor 2020.1"
date: "2019-07-22 12:25:03"
author: "Adam Nagy"
categories:
  - "Adam"
  - "Inventor"
original_url: "https://modthemachine.typepad.com/my_weblog/2019/07/new-apis-in-inventor-20201.html "
typepad_basename: "new-apis-in-inventor-20201"
typepad_status: "Publish"
---

<p>For the clearance hole <strong>APIs</strong> we expose the HoleFeatures.<strong>CreateClearanceInfo</strong> and also make the HoleFeature.<strong>ClearanceInfo</strong> writable in I<strong>nventor 2020.1</strong> so users now can create a clearance hole via <strong>API</strong>.</p>
<p>Here is a sample:</p>
<pre>Sub test() 
   &#39; open a part document with a hole feature
   Dim oDoc As PartDocument 
   Set oDoc = ThisApplication.ActiveDocument 
     
   Dim oHole As HoleFeature 
   Set oHole = oDoc.ComponentDefinition.Features.HoleFeatures(1) 
     
   Debug.Print oHole.IsClearanceHole 
   
   &#39; Create a HoleClearanceInfo(not associative with any HoleFeature) 
   Dim oHCI As HoleClearanceInfo 
   Set oHCI = oDoc.ComponentDefinition.Features.HoleFeatures.CreateClearanceInfo(&quot;Ansi Unified Screw Threads&quot;, &quot;Flat Head Machine Screw (100)&quot;, &quot;#1&quot;, kNormalFitType) 
   Debug.Print oHCI.FastenerStandard 
   Debug.Print oHCI.FastenerType 
   Debug.Print oHCI.FastenerSize 
   Debug.Print oHCI.FastenerFitType 
   
   &#39; we can change the property when the HoleClearanceInfo is not associative with a HoleFeature 
   oHCI.FastenerStandard = &quot;ISO&quot; 
   oHCI.FastenerType = &quot;Countersunk Flat Head Screw ISO 2009/7046&quot; 
   oHCI.FastenerSize = &quot;M1.6&quot; 
   oHCI.FastenerFitType = kCloseFitType 
   
   Debug.Print &quot;----------------------------&quot; 
   Debug.Print oHCI.FastenerStandard 
   Debug.Print oHCI.FastenerType 
   Debug.Print oHCI.FastenerSize 
   Debug.Print oHCI.FastenerFitType 
   
   &#39; set the hole as a clearance hole 
   oHole.ClearanceInfo = oHCI 
   Debug.Print oHole.IsClearanceHole 
   
   &#39; retrieve the HoleClearanceInfo from a HoleFeature 
   Set oHCI = oHole.ClearanceInfo 
   
   Debug.Print &quot;----------------------------&quot; 
   Debug.Print oHCI.FastenerStandard 
   Debug.Print oHCI.FastenerType 
   Debug.Print oHCI.FastenerSize 
   Debug.Print oHCI.FastenerFitType 
   
   &#39; now the oHCI is associative with a HoleFeature, we don&#39;t allow to change a property(like FastenerStandard) directly on the property 
   &#39; but you can call the SetClearanceInfo method to change the properties, and the change will affect the HoleFeature directly 
   Call oHCI.SetClearanceInfo(&quot;Ansi Unified Screw Threads&quot;, &quot;Flat Head Machine Screw (82)&quot;, &quot;#8&quot;, kNormalFitType) 
   Debug.Print &quot;----------------------------&quot; 
   Debug.Print oHCI.FastenerStandard 
   Debug.Print oHCI.FastenerType 
   Debug.Print oHCI.FastenerSize 
   Debug.Print oHCI.FastenerFitType 
   
   &#39; also you can create another HoleClearanceInfo(not associative with any HoleFeature), and assign it to the HoleFeature.ClearanceInfo to change its clearance info 
   Dim onewHCI As HoleClearanceInfo 
   Set onewHCI = oDoc.ComponentDefinition.Features.HoleFeatures.CreateClearanceInfo(&quot;DIN&quot;, &quot;Countersunk Flat Head Screw DIN EN ISO 2009&quot;, &quot;M1.6&quot;, kNormalFitType) 
   oHole.ClearanceInfo = onewHCI 
   
   Set oHCI = oHole.ClearanceInfo 
   Debug.Print &quot;----------------------------&quot; 
   Debug.Print oHCI.FastenerStandard 
   Debug.Print oHCI.FastenerType 
   Debug.Print oHCI.FastenerSize 
   Debug.Print oHCI.FastenerFitType 
 
End Sub</pre>
<p>&#0160;</p>
