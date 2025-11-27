---
layout: "post"
title: "List painted area sizes"
date: "2015-10-23 05:41:33"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "Inventor"
original_url: "https://adndevblog.typepad.com/manufacturing/2015/10/list-painted-area-sizes.html "
typepad_basename: "list-painted-area-sizes"
typepad_status: "Publish"
---

<p>By&#0160;<a href="http://adndevblog.typepad.com/manufacturing/adam-nagy.html">Adam Nagy</a></p>
<p>If you want to find out how big the areas are in the assembly that need to be covered with certain paints then you can write a code to collect that data.</p>
<p>In this case to take into account any face colour that might have been overridden at the main assembly level, we&#39;ll iterate through all the faces in context of the top assembly. We just have to keep a list of all the materials we encountered, and the area they cover altogether.&#0160;</p>
<p>To help with this process I&#39;m using a <strong>Dictionary</strong> object. In case of <strong>VBA</strong> you can use the one available from &quot;<strong>Microsoft Scripting Runtime</strong>&quot; library:</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d16bc65b970c-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="MSScripting" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b8d16bc65b970c image-full img-responsive" src="/assets/image_267f01.jpg" title="MSScripting" /></a></p>
<p>Other languages provide their own <strong>Dictionary</strong> implementation that you can use - e.g. <strong>.NET</strong> has it as well.&#0160;</p>
<p>For testing purposes I&#39;m using the &quot;<strong>Personal Computer.iam</strong>&quot; sample that is available from the <a href="http://knowledge.autodesk.com/support/inventor-products/downloads/caas/downloads/content/inventor-sample-files.html" target="_self"><strong>Autodesk Inventor 2016 Samples</strong></a> under &quot;<strong>Models\Assemblies\Personal Computer</strong>&quot;:</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb0885bc98970d-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="PersonalComputer" class="asset  asset-image at-xid-6a0167607c2431970b01bb0885bc98970d img-responsive" src="/assets/image_8e5b9e.jpg" title="PersonalComputer" /></a></p>
<p>Here is our code:</p>
<pre>Sub CollectPaintedAreas()
  Dim doc As AssemblyDocument
  Set doc = ThisApplication.ActiveDocument
  
  Dim cd As AssemblyComponentDefinition
  Set cd = doc.ComponentDefinition
  
  &#39; This object type is from 
  &#39; &quot;Microsoft Scripting Runtime&quot; library
  Dim dict As Dictionary
  Set dict = New Dictionary
  
  &#39; Check all the faces for their appearance and area
  On Error Resume Next
  Dim co As ComponentOccurrence
  For Each co In cd.Occurrences.AllLeafOccurrences
    Dim sb As SurfaceBody
    For Each sb In co.SurfaceBodies
      Dim f As Face
      For Each f In sb.Faces
        Dim a As Asset
        Set a = f.Appearance
        &#39; Some faces might have issues with
        &#39; Appearance property
        If Err = 0 Then
          If dict.Exists(a.DisplayName) Then
            dict(a.DisplayName) = _
              dict(a.DisplayName) + f.Evaluator.Area
          Else
            Call dict.Add( _
              a.DisplayName, f.Evaluator.Area)
          End If
        End If
        Err.Clear
      Next
    Next
  Next
  <br />  &#39; If you also want to handle weldment assemblies and their weld beads <br />  If TypeOf cd Is WeldmentComponentDefinition Then <br />    Dim wcd As WeldmentComponentDefinition <br />    Set wcd = cd Dim wbead As WeldBead <br /><br />    For Each wbead In wcd.Welds.WeldBeads <br />      Dim fs As Faces <br />      Set fs = wbead.BeadFaces <br />      Set f = fs(fs.Count) <br />      Set a = f.Appearance <br />      &#39; Some faces might have issues with <br />      &#39; Appearance property <br />      If Err = 0 Then <br />        If dict.Exists(a.DisplayName) Then <br />          dict(a.DisplayName) = _ <br />            dict(a.DisplayName) + f.Evaluator.area <br />        Else <br />          Call dict.Add( _ <br />            a.DisplayName, f.Evaluator.area) <br />        End If <br />      Else <br />        Const name = &quot;Unknown&quot; <br />        If dict.Exists(name) Then <br />          dict(name) = _ <br />            dict(name) + f.Evaluator.area <br />        Else <br />          Call dict.Add( _ <br />            name, f.Evaluator.area) <br />        End If <br />      End If <br />      Err.Clear <br />    Next <br />  End If<br />
  &#39; List result
  Dim n As Variant
  For Each n In dict
    Debug.Print n + &quot; = &quot; + Str(dict(n)) + &quot; cm2&quot;
  Next
End Sub</pre>
<p>The result:</p>
<pre>Silicon Nitride - Polished =  12916.9688783868 cm2
Glossy - Black =  2339.14399193464 cm2
Chrome - Polished =  3851.9558804525 cm2
Smooth - Ivory =  42.4976735204942 cm2
Clear - Green 1 =  1.40193572166445 cm2
Zinc =  777.846133032108 cm2
Cool White =  130.026627637732 cm2
Snow =  497.991799292123 cm2
Aluminum - Polished =  1798.11482682528 cm2
Copper - Polished =  23.2457401847515 cm2
Pink Rose =  1.94839840336866 cm2
Blue - Wall Paint - Glossy =  66.2601255359018 cm2
Violet =  2.30497698015977 cm2
Smooth - Dark Forest Green =  1746.5581771357 cm2
Green Pastel =  2.03418124319939 cm2
Pink =  2.03418124319939 cm2
Sky Blue Medium =  2.03418124319939 cm2
Smooth - Yellow =  30.2747833343406 cm2
Dark Gray =  289.6377248152 cm2
Light Gray =  86.5221761930381 cm2
Glossy - Gold =  1.89552392144668 cm2
Light Red =  29.8206048586813 cm2
Teflon =  23.5951135218548 cm2
Wheat =  13.1217919533048 cm2
Aluminum - Polished-1 =  2.91547594742265E-02 cm2
Default =  421.056440220586 cm2
Red =  57.6774842076835 cm2
Rubber-Black =  214.031238785277 cm2
White =  1.21634005726582 cm2
Light Beige =  2.96880505764235 cm2
Orange (Pale) =  6117.16485607271 cm2
Orange (Inventor) =  12.359525846104 cm2</pre>
<p>If you want to do the same inside a part document, then you could do it like this:</p>
<pre>Sub CollectPaintedAreasInPartDocument()
  Dim doc As PartDocument
  Set doc = ThisApplication.ActiveDocument
    
  Dim cd As PartComponentDefinition
  Set cd = doc.ComponentDefinition
    
  &#39; This object type is from
  &#39; &quot;Microsoft Scripting Runtime&quot; library
  Dim dict As Dictionary
  Set dict = New Dictionary
    
  &#39; Check all the faces for their appearance and area
  On Error Resume Next
  Dim sb As SurfaceBody
  For Each sb In cd.SurfaceBodies
    Dim f As Face
    For Each f In sb.Faces
      Dim a As Asset
      Set a = f.Appearance
      &#39; Some faces might have issues with
      &#39; Appearance property
      If Err = 0 Then
        If dict.Exists(a.DisplayName) Then
          dict(a.DisplayName) = _
            dict(a.DisplayName) + f.Evaluator.area
        Else
          Call dict.Add( _
            a.DisplayName, f.Evaluator.area)
        End If
      End If
      Err.Clear
    Next
  Next
    
  &#39; List result
  Dim n As Variant
  For Each n In dict
    Debug.Print n + &quot; = &quot; + Str(dict(n)) + &quot; cm2&quot;
  Next
End Sub</pre>
