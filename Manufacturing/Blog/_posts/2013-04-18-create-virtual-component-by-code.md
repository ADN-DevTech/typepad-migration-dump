---
layout: "post"
title: "Create Virtual Component by Code"
date: "2013-04-18 02:08:00"
author: "Xiaodong Liang"
categories:
  - "Inventor"
  - "Xiaodong Liang"
original_url: "https://adndevblog.typepad.com/manufacturing/2013/04/create-virtual-component-by-code.html "
typepad_basename: "create-virtual-component-by-code"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/manufacturing/xiaodong-liang.html" target="_self">Xiaodong Liang</a></p>
<p>The virtual component is defined by a VirtualComponentDefinition. Through the definition, you can also get the information of component settings.</p>
<p><em>Sub VirtualC()     <br />&#0160;&#0160;&#0160; &#39; assume an assembly document is opened      <br />&#0160;&#0160;&#0160; Dim oAssDoc As AssemblyDocument      <br />&#0160;&#0160;&#0160; Set oAssDoc = ThisApplication.activeDocument      <br />&#0160;&#0160;&#0160; Dim oAssDef As AssemblyComponentDefinition      <br />&#0160;&#0160;&#0160; Set oAssDef = oAssDoc.ComponentDefinition      <br />&#0160;&#0160;&#0160; Dim oMatrix As matrix      <br />&#0160;&#0160;&#0160; Set oMatrix = ThisApplication.TransientGeometry.CreateMatrix </em></p>
<p><em>&#0160;&#0160;&#0160; &#39; add one virtual occurrence     <br />&#0160;&#0160;&#0160; Dim oNewOcc As ComponentOccurrence      <br />&#0160;&#0160;&#0160; Set oNewOcc = oAssDef.Occurrences.AddVirtual(&quot;MyVirtual&quot;, oMatrix)      <br />&#0160;&#0160;&#0160; Dim oCVirtualCompDef As VirtualComponentDefinition      <br />&#0160;&#0160;&#0160; Set oCVirtualCompDef = oNewOcc.Definition      <br />&#0160;&#0160;&#0160; &#39; get BOMStructure      <br />&#0160;&#0160;&#0160; Debug.Print &quot;BOMStructure of Virtual Component: &quot; &amp; oCVirtualCompDef.BOMStructure </em></p>
<p><em>&#0160;&#0160;&#0160; Dim oQuantityType As BOMQuantityTypeEnum     <br />&#0160;&#0160;&#0160; Dim oBaseQuantity As Variant      <br />&#0160;&#0160;&#0160;&#0160; &#39; get QuantityType and Evaluated Quantity      <br />&#0160;&#0160;&#0160; Call oCVirtualCompDef.BOMQuantity.GetBaseQuantity(oQuantityType, oBaseQuantity)      <br />&#0160;&#0160;&#0160; Dim oEvaluatedQuantityType As BOMQuantityTypeEnum      <br />&#0160;&#0160;&#0160;&#0160; Debug.Print &quot;QuantityType: &quot; &amp; oEvaluatedQuantityType      <br />&#0160;&#0160;&#0160; Dim oEvaluatedQuantity As Double      <br />&#0160;&#0160;&#0160; oEvaluatedQuantity = oCVirtualCompDef.BOMQuantity.GetEvaluatedBaseQuantity(oEvaluatedQuantityType)      <br />&#0160;&#0160;&#0160; Debug.Print &quot;Quantity: &quot; &amp; oEvaluatedQuantity      <br />End Sub</em></p>
<p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b017d430ad5e9970c-pi"><img alt="image" border="0" height="539" src="/assets/image_0dd791.jpg" style="display: inline; border: 0px;" title="image" width="425" /></a></p>
