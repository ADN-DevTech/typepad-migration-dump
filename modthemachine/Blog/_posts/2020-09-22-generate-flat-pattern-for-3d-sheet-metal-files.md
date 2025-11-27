---
layout: "post"
title: "Generate flat pattern for 3d folded sheet metal files"
date: "2020-09-22 20:04:31"
author: "Adam Nagy"
categories:
  - "Adam"
  - "Inventor"
  - "Sheet Metal"
original_url: "https://modthemachine.typepad.com/my_weblog/2020/09/generate-flat-pattern-for-3d-sheet-metal-files.html "
typepad_basename: "generate-flat-pattern-for-3d-sheet-metal-files"
typepad_status: "Publish"
---

<p>I vaguely remember now seeing this before, but could not recall it when someone asked about it the other day, that <strong>Inventor</strong> can generate <strong>flat pattern</strong> for any <strong>folded sheet metal</strong> model, without having to first create all the related <strong>sheet metal features</strong> (bend, flange, etc) by hand.</p>
<p><a class="asset-img-link" href="https://modthemachine.typepad.com/.a/6a00e553fcbfc68834026be413d162200d-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="ConvertToSheetMetalpng" border="0" class="asset  asset-image at-xid-6a00e553fcbfc68834026be413d162200d image-full img-responsive" src="/assets/image_893171.jpg" title="ConvertToSheetMetalpng" /></a></p>
<p>In this case I started with opening a <strong>STEP</strong> file of the <strong>3D folded sheet metal</strong>. When I select &quot;<strong>Convert To Sheet Metal</strong>&quot; then I need to select the <strong>base face</strong> for the sheet metal, and then the <strong>thickness</strong> will be automatically populated with the correct value.</p>
<p>There is no dedicated function in the <strong>Inventor API</strong> for that very last part about finding out the thickness. <br />However, we can use things like <strong>FindUsingRay()</strong> to find the face opposite the one we want to use as the base, and then check the distance between them</p>
<p>There are two ways to open a <strong>non-Inventor</strong> file: simply use the <strong>Documents.Open()</strong> function, in which case the file will be <strong>converted</strong>, or through <strong>ComponentDefinition.ImportedComponents</strong>, in which case you can also decide to <strong>reference</strong> the file - see <a href="https://modthemachine.typepad.com/my_weblog/2019/04/import-anycad-documents-associatively.html">Import AnyCAD documents associatively</a></p>
<p>When doing this whole thing through the <strong>API</strong>, then you have to find a way to figure out which <strong>face</strong> should be used as <strong>base</strong>. One option could be looking for the largest planar face. <br />Once you have that you need to find the face opposite that and check the distance between them. That should provide the thickness of the sheet metal.</p>
<p>Here is a <strong>VBA</strong> sample showing this:</p>
<pre>Function GetThickness(sb As SurfaceBody) As Double
  &#39; Find biggest face
  Dim f As Face
  Dim bf As Face
  Dim area As Double
  For Each f In sb.Faces
    &#39; Only care about planar faces
    If TypeOf f.Geometry Is Plane And f.Evaluator.area &gt; area Then
      Set bf = f
      area = f.Evaluator.area
    End If
  Next
  
  &#39; Find the opposite face
  Dim p As Plane
  Set p = bf.Geometry
  
  Dim pt1 As Point
  Set pt1 = bf.PointOnFace
  
  Dim tr As TransientGeometry
  Set tr = ThisApplication.TransientGeometry
  
  Dim objs As ObjectsEnumerator
  Dim pts As ObjectsEnumerator
  Dim n As UnitVector
  &#39; We have to search in the opposite direction
  &#39; of the face&#39;s normal vector
  If bf.IsParamReversed Then <br />    Set n = p.Normal <br />  Else <br />    Set n = tr.CreateUnitVector( _ <br />      -p.Normal.x, -p.Normal.y, -p.Normal.z) <br />  End If
  &#39; objs(2) should be the opposite face
  &#39; but we do not need it, the intersection point
  &#39; is enough, i.e. pts(2)
  Call sb.FindUsingRay(pt1, n, 0, objs, pts)
  
  &#39; The first point found will be on the same face
  &#39; The second one will be on the face opposite
  Dim pt2 As Point
  Set pt2 = pts(2)
  
  GetThickness = pt1.DistanceTo(pt2)
End Function

Sub ConvertToSheetMetal()
  Dim path As String: path = &quot;C:\Temp\SheetMetal_0.05in.stp&quot;
  
  Dim doc As PartDocument
  Set doc = ThisApplication.Documents.Open(path)
  
  &#39; Turn it into a sheet metal part
  doc.SubType = &quot;{9C464203-9BAE-11D3-8BAD-0060B0CE6BB4}&quot;
  
  Dim cd As SheetMetalComponentDefinition
  Set cd = doc.ComponentDefinition
 
  cd.UseSheetMetalStyleThickness = False
  cd.Thickness.Value = GetThickness(cd.SurfaceBodies(1))
  
  Call cd.Unfold
End Sub</pre>
<p>And the result:</p>
<p><a class="asset-img-link" href="https://modthemachine.typepad.com/.a/6a00e553fcbfc68834026be413d2ad200d-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="SheetMetalResult" class="asset  asset-image at-xid-6a00e553fcbfc68834026be413d2ad200d img-responsive" src="/assets/image_846257.jpg" title="SheetMetalResult" /></a></p>
<p>If the part is not really a <strong>folded</strong> sheet metal, then though the <strong>Unfold</strong> &quot;succeeds&quot;, the body of the &quot;<strong>Folded Model</strong>&quot; and the &quot;<strong>Flat Pattern</strong>&quot; will be the same.&#0160;&#0160;<br />You can test this by comparing the bodies:</p>
<pre>Sub TestFlatPattern()
  Dim doc As PartDocument
  Set doc = ThisApplication.ActiveDocument
  
  Dim cd As SheetMetalComponentDefinition
  Set cd = doc.ComponentDefinition
  
  Dim tr As TransientBRep
  Set tr = ThisApplication.TransientBRep
  
  Dim objs As ObjectCollection
  Set objs = ThisApplication.TransientObjects.CreateObjectCollection
  Call objs.Add(cd.SurfaceBodies(1))
  Call objs.Add(cd.FlatPattern.SurfaceBodies(1))
  Set objs = tr.GetIdenticalBodies(objs)
  
  If objs.Count &gt; 0 Then
    MsgBox (&quot;The flat pattern body is the same as the original body&quot;)
  End If
End Sub
</pre>
<p>-Adam</p>
