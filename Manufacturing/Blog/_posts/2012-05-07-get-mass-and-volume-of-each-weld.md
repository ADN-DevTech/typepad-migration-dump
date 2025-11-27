---
layout: "post"
title: "Get mass and volume of each weld"
date: "2012-05-07 03:46:14"
author: "Xiaodong Liang"
categories:
  - "Inventor"
  - "Xiaodong Liang"
original_url: "https://adndevblog.typepad.com/manufacturing/2012/05/get-mass-and-volume-of-each-weld.html "
typepad_basename: "get-mass-and-volume-of-each-weld"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/aec/xiaodong-liang.html" target="_self">Xiaodong Liang</a></p>

A weldment has a single special local occurrence of the welds component. We can get the MassProperties from ComponentOccurrence.MassProperties for this welds component instance. Just walk the occurrences of the weldment assembly looking for the local occurrence whose Definition is the WeldsComponentDefinition. 

But no API to get the mass/volume of each weld. This can be worked around by deleting each weld, calculating by the current mass/ volume with mass/ volume of the total weld. 

<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="line-height:140%;">&nbsp;&nbsp; </span><span style="color: blue; line-height:140%;">Sub</span><span style="line-height:140%;"> getWeldMassAndVolume()</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height:140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; </span><span style="color: green; line-height:140%;">' get active Inventor process</span></p>
<p style="margin: 0px;"><span style="line-height:140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; </span><span style="color: blue; line-height:140%;">Dim</span><span style="line-height:140%;"> InvApp </span><span style="color: blue; line-height:140%;">As</span><span style="line-height:140%;"> Inventor.Application = _</span></p>
<p style="margin: 0px;"><span style="line-height:140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp; Runtime.InteropServices.Marshal. _</span></p>
<p style="margin: 0px;"><span style="line-height:140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp; GetActiveObject(</span><span style="color: #a31515; line-height:140%;">&quot;Inventor.Application&quot;</span><span style="line-height:140%;">)</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height:140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; </span><span style="color: blue; line-height:140%;">Dim</span><span style="line-height:140%;"> oAssDoc </span><span style="color: blue; line-height:140%;">As</span><span style="line-height:140%;"> AssemblyDocument</span></p>
<p style="margin: 0px;"><span style="line-height:140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; oAssDoc = InvApp.ActiveDocument</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height:140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; </span><span style="color: blue; line-height:140%;">Dim</span><span style="line-height:140%;"> cd </span><span style="color: blue; line-height:140%;">As</span><span style="line-height:140%;"> ComponentDefinition</span></p>
<p style="margin: 0px;"><span style="line-height:140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; cd = oAssDoc.ComponentDefinition</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height:140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; </span><span style="color: blue; line-height:140%;">If</span><span style="line-height:140%;"> cd.Type = _</span></p>
<p style="margin: 0px;"><span style="line-height:140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp; ObjectTypeEnum.kWeldmentComponentDefinitionObject </span><span style="color: blue; line-height:140%;">Then</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height:140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; </span><span style="color: blue; line-height:140%;">Dim</span><span style="line-height:140%;"> wcd </span><span style="color: blue; line-height:140%;">As</span><span style="line-height:140%;"> WeldmentComponentDefinition</span></p>
<p style="margin: 0px;"><span style="line-height:140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; wcd = cd</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height:140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; Debug.Print(</span><span style="color: #a31515; line-height:140%;">&quot;Total mass of assembly&quot;</span><span style="line-height:140%;"> _</span></p>
<p style="margin: 0px;"><span style="line-height:140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &amp; wcd.MassProperties.Mass)</span></p>
<p style="margin: 0px;"><span style="line-height:140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; Debug.Print(</span><span style="color: #a31515; line-height:140%;">&quot; Total volume of assembly&quot;</span><span style="line-height:140%;"> _</span></p>
<p style="margin: 0px;"><span style="line-height:140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &amp; wcd.MassProperties.Volume)</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height:140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; </span><span style="color: green; line-height:140%;">'Suppose the weld occurrence is the </span></p>
<p style="margin: 0px;"><span style="line-height:140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; </span><span style="color: green; line-height:140%;">' first one in this assembly.</span></p>
<p style="margin: 0px;"><span style="line-height:140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; </span><span style="color: blue; line-height:140%;">Dim</span><span style="line-height:140%;"> oO </span><span style="color: blue; line-height:140%;">As</span><span style="line-height:140%;"> ComponentOccurrence</span></p>
<p style="margin: 0px;"><span style="line-height:140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; oO = wcd.Occurrences(1)</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height:140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; </span><span style="color: blue; line-height:140%;">Dim</span><span style="line-height:140%;"> oTotalWeldsMass </span><span style="color: blue; line-height:140%;">As</span><span style="line-height:140%;"> </span><span style="color: blue; line-height:140%;">Double</span></p>
<p style="margin: 0px;"><span style="line-height:140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; oTotalWeldsMass = oO.MassProperties.Mass</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height:140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; </span><span style="color: blue; line-height:140%;">Dim</span><span style="line-height:140%;"> oTotalWeldsVolume </span><span style="color: blue; line-height:140%;">As</span><span style="line-height:140%;"> </span><span style="color: blue; line-height:140%;">Double</span></p>
<p style="margin: 0px;"><span style="line-height:140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; oTotalWeldsVolume = oO.MassProperties.Volume</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height:140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; Debug.Print(</span><span style="color: #a31515; line-height:140%;">&quot; mass of welds&quot;</span><span style="line-height:140%;"> &amp; oTotalWeldsMass)</span></p>
<p style="margin: 0px;"><span style="line-height:140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; Debug.Print(</span><span style="color: #a31515; line-height:140%;">&quot; volume of welds&quot;</span><span style="line-height:140%;"> &amp; oTotalWeldsVolume)</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height:140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; </span><span style="color: blue; line-height:140%;">Dim</span><span style="line-height:140%;"> oEachWeldB </span><span style="color: blue; line-height:140%;">As</span><span style="line-height:140%;"> WeldBead</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height:140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; </span><span style="color: blue; line-height:140%;">For</span><span style="line-height:140%;"> </span><span style="color: blue; line-height:140%;">Each</span><span style="line-height:140%;"> oEachWeldB </span><span style="color: blue; line-height:140%;">In</span><span style="line-height:140%;"> wcd.Welds.WeldBeads</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height:140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; </span><span style="color: green; line-height:140%;">'oEachWeldB.FaceSetOne</span></p>
<p style="margin: 0px;"><span style="line-height:140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; </span><span style="color: green; line-height:140%;">'oEachWeldB.FaceSetTwo</span></p>
<p style="margin: 0px;"><span style="line-height:140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; </span><span style="color: green; line-height:140%;">'oEachWeldB.BeadLength</span></p>
<p style="margin: 0px;"><span style="line-height:140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; </span><span style="color: green; line-height:140%;">'oEachWeldB.WeldInfo</span></p>
<p style="margin: 0px;"><span style="line-height:140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; </span><span style="color: green; line-height:140%;">'oEachWeldB.SideFaces</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height:140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; </span><span style="color: blue; line-height:140%;">Next</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height:140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; </span><span style="color: green; line-height:140%;">' a workaround to get mass &amp; volum of each weldbead</span></p>
<p style="margin: 0px;"><span style="line-height:140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; </span><span style="color: blue; line-height:140%;">For</span><span style="line-height:140%;"> </span><span style="color: blue; line-height:140%;">Each</span><span style="line-height:140%;"> oEachWeldB </span><span style="color: blue; line-height:140%;">In</span><span style="line-height:140%;"> wcd.Welds.WeldBeads</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height:140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; </span><span style="color: blue; line-height:140%;">Dim</span><span style="line-height:140%;"> oStr </span><span style="color: blue; line-height:140%;">As</span><span style="line-height:140%;"> </span><span style="color: blue; line-height:140%;">String</span></p>
<p style="margin: 0px;"><span style="line-height:140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; oStr = oEachWeldB.Name</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height:140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; oEachWeldB.Delete()</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height:140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; Debug.Print(</span><span style="color: #a31515; line-height:140%;">&quot; mass of weld [&quot;</span><span style="line-height:140%;"> &amp; oStr &amp; </span><span style="color: #a31515; line-height:140%;">&quot;]: &quot;</span><span style="line-height:140%;"> _</span></p>
<p style="margin: 0px;"><span style="line-height:140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &amp; oTotalWeldsMass - oO.MassProperties.Mass)</span></p>
<p style="margin: 0px;"><span style="line-height:140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; Debug.Print(</span><span style="color: #a31515; line-height:140%;">&quot; volume of weld [&quot;</span><span style="line-height:140%;"> &amp; oStr &amp; </span><span style="color: #a31515; line-height:140%;">&quot;]: &quot;</span><span style="line-height:140%;"> _</span></p>
<p style="margin: 0px;"><span style="line-height:140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp; &amp; oTotalWeldsVolume - oO.MassProperties.Volume)</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height:140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; </span><span style="color: green; line-height:140%;">'undo delete</span></p>
<p style="margin: 0px;"><span style="line-height:140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; InvApp.CommandManager. _</span></p>
<p style="margin: 0px;"><span style="line-height:140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp; ControlDefinitions(</span><span style="color: #a31515; line-height:140%;">&quot;AppUndoCmd&quot;</span><span style="line-height:140%;">).Execute()</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height:140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; </span><span style="color: blue; line-height:140%;">Next</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height:140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; </span><span style="color: blue; line-height:140%;">End</span><span style="line-height:140%;"> </span><span style="color: blue; line-height:140%;">If</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height:140%;">&nbsp;&nbsp;&nbsp; </span><span style="color: blue; line-height:140%;">End</span><span style="line-height:140%;"> </span><span style="color: blue; line-height:140%;">Sub</span></p>
</div>
