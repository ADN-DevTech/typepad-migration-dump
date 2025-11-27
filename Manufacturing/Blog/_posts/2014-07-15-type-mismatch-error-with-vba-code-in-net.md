---
layout: "post"
title: "Type mismatch error with VBA code in .NET"
date: "2014-07-15 05:29:35"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "Inventor"
original_url: "https://adndevblog.typepad.com/manufacturing/2014/07/type-mismatch-error-with-vba-code-in-net.html "
typepad_basename: "type-mismatch-error-with-vba-code-in-net"
typepad_status: "Publish"
---

<p>By&#0160;<a href="http://adndevblog.typepad.com/manufacturing/adam-nagy.html" target="_self">Adam Nagy</a></p>
<p>When migrating <strong>VBA</strong> code to <strong>.NET</strong> there are a couple of things to watch out for and I hope all of them are listed in this document from Brian Ekins:&#0160;<a href="http://modthemachine.typepad.com/files/VBAtoAddIn.pdf" target="_self" title="">http://modthemachine.typepad.com/files/VBAtoAddIn.pdf</a></p>
<p>When passing in arrays of variable sizes you need to initialize the variable - this is discussed in the above mentioned document under <strong>5. Arrays of variable size are handled differently in VB.Net</strong>. Otherwise you&#39;ll get an error:&#0160;<strong>Type mismatch. (Exception from HRESULT: 0x80020005 (DISP_E_TYPEMISMATCH))</strong></p>
<p>So e.g. this VBA code:</p>
<pre>Dim MyLine As DrawingCurveSegment = invApp.ActiveDocument.SelectSet(1) 
Dim pts(1) As Double
Dim gp() As Double
Dim md() As Double 
Dim pm() As Double 
Dim st() As SolutionNatureEnum
pts(0) = MyLine.EndPoint.x 
pts(1) = MyLine.EndPoint.y
Call MyLine.Parent.Evaluator2D.GetParamAtPoint(pts, gp, md, pm, st)</pre>
<p>... would need to be converted to this in .NET:</p>
<pre>Dim MyLine As DrawingCurveSegment = invApp.ActiveDocument.SelectSet(1) 
Dim pts(1) As Double
Dim gp() As Double = {}  
Dim md() As Double = {} 
Dim pm() As Double = {} 
Dim st() As SolutionNatureEnum = {}
pts(0) = MyLine.EndPoint.x 
pts(1) = MyLine.EndPoint.y
Call MyLine.Parent.Evaluator2D.GetParamAtPoint(pts, gp, md, pm, st)</pre>
<p>&#0160;</p>
