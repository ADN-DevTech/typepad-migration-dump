---
layout: "post"
title: "Constrain to the middle of an edge"
date: "2012-11-20 07:00:54"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "Inventor"
original_url: "https://adndevblog.typepad.com/manufacturing/2012/11/constrain-to-the-middle-of-an-edge.html "
typepad_basename: "constrain-to-the-middle-of-an-edge"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/manufacturing/adam-nagy.html" target="_self">Adam Nagy</a></p>
<p>I&#39;m trying to create a mate constraint between a work plane and the mid point of an edge, but I do not see how I could get back the middle vertex from the edge.</p>
<p><strong>Solution</strong></p>
<p>If you don&#39;t know how to do something through the API then a good starting point is to create the object through the user interface and then examine it through the API. <br />If you  do that in this particular case then you&#39;d see that EntityTwo of the MateConstraint is EdgeProxy and EntityTwoInferredType is kInferredPoint. If you create your mate constraint with that in mind then all should be fine:</p>
<pre><span style="font-family: &#39;courier new&#39;, courier; font-size: 8pt;">Sub test()
    Dim asm As AssemblyDocument
    Set asm = ThisApplication.ActiveDocument
    
    &#39; Workplane from 1st occurrence
    Dim wp As WorkPlaneProxy
    Set wp = asm.SelectSet(1)
    
    &#39; Edge from 2nd occurrence
    Dim e As EdgeProxy
    Set e = asm.SelectSet(2)
    
    Dim acs As AssemblyConstraints
    Set acs = asm.ComponentDefinition.Constraints
    
    Dim mate As MateConstraint
    Set mate = acs.AddMateConstraint( _
      wp, e, 0, kNoInference, kInferredPoint)
End Sub</span></pre>
<p>&#0160;</p>
