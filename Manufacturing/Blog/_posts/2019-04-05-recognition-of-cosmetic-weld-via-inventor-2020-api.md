---
layout: "post"
title: "Identification of Cosmetic weld via Inventor 2020 API"
date: "2019-04-05 03:14:58"
author: "Chandra Shekar Gopal"
categories:
  - "Chandra Shekar Gopal"
  - "Inventor"
original_url: "https://adndevblog.typepad.com/manufacturing/2019/04/recognition-of-cosmetic-weld-via-inventor-2020-api.html "
typepad_basename: "recognition-of-cosmetic-weld-via-inventor-2020-api"
typepad_status: "Publish"
---

<p>by <a href="https://adndevblog.typepad.com/manufacturing/chandra-shekar-gopal.html" target="_blank" rel="noopener">Chandra shekar Gopal</a></p>
<p>In Inventor 2019 and lower versions,&nbsp; Cosmetic welds were unable to recognize via Inventor API. Now, Inventor 2020 API is able to identify Cosmetic weld in weldment assembly.</p>
<p>Try below VBA code to know length of Cosmetic weld which is tested with attached weldment assembly (I109269.iam)</p>
<blockquote>
<p>Sub Cosmetic_weld()</p>
<p>&nbsp;&nbsp;&nbsp; Dim oDoc As AssemblyDocument<br />&nbsp;&nbsp;&nbsp;&nbsp; Set oDoc = ThisApplication.ActiveDocument</p>
<p>&nbsp;&nbsp;&nbsp; Dim oDef As WeldmentComponentDefinition<br />&nbsp;&nbsp;&nbsp;&nbsp; Set oDef = oDoc.ComponentDefinition</p>
<p>&nbsp;&nbsp;&nbsp; Dim oWeld As Weld<br />&nbsp;&nbsp;&nbsp;&nbsp; For Each oWeld In oDef.Welds</p>
<p>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Dim TotalLength As Double<br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; TotalLength = 0<br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Dim oEdge As Edge<br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; For Each oEdge In oWeld.Edges</p>
<p>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Dim oCurveEval As CurveEvaluator<br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Set oCurveEval = oEdge.Evaluator</p>
<p>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Dim MinParam As Double<br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Dim MaxParam As Double<br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Dim Length As Double<br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Call oCurveEval.GetParamExtents(MinParam, MaxParam)<br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Call oCurveEval.GetLengthAtParam(MinParam, MaxParam, Length)<br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; TotalLength = TotalLength + Length</p>
<p>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Next</p>
<p>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Debug.Print "Length of " &amp; oWeld.Name &amp; " is " &amp; ThisApplication.UnitsOfMeasure.GetStringFromValue(TotalLength, kMillimeterLengthUnits)</p>
<p>&nbsp;&nbsp;&nbsp; Next<br />End Sub</p>
</blockquote>
<p>Output of VBA code for attached weldment assembly would look like below.</p>
<blockquote>
<p>Length of Cosmetic Weld 1 is 189.699 mm<br />Length of Cosmetic Weld 2 is 229.323 mm<br />Length of Cosmetic Weld 3 is 229.323 mm<br />Length of Cosmetic Weld 4 is 189.699 mm<br />Length of Cosmetic Weld 5 is 189.699 mm<br />Length of Cosmetic Weld 6 is 229.323 mm<br />Length of Cosmetic Weld 7 is 229.323 mm<br />Length of Cosmetic Weld 8 is 189.699 mm</p>
</blockquote>
<p><span class="asset  asset-generic at-xid-6a0167607c2431970b0240a4786361200d img-responsive"><a href="https://adndevblog.typepad.com/files/weldmentassembly-1.zip">Download WeldmentAssembly</a></span></p>
