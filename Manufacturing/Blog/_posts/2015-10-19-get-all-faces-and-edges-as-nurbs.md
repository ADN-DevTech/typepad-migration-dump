---
layout: "post"
title: "Get all faces and edges as NURBS"
date: "2015-10-19 09:49:30"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "Inventor"
original_url: "https://adndevblog.typepad.com/manufacturing/2015/10/get-all-faces-and-edges-as-nurbs.html "
typepad_basename: "get-all-faces-and-edges-as-nurbs"
typepad_status: "Publish"
---

<p>By&#0160;<a href="http://adndevblog.typepad.com/manufacturing/adam-nagy.html">Adam Nagy</a></p>
<p>Through the <strong>API</strong> it&#39;s possible to get back <strong>edges</strong> (lines, arcs, etc) and also the <strong>faces</strong> (plane, cylinder) as <strong>NURBS</strong>. You can use the <strong>AlternateBody</strong> property of the <strong>SurfaceBody</strong> object for this. Based on the parameters you provide either only the faces will be converted to <strong>NURBS</strong> or both the faces and edges.</p>
<p>Remarks in <strong>API Help file</strong>:</p>
<p><em>Valid values for the <strong>AlternateForm</strong> parameter are in the <strong>SurfaceGeometryFormEnum</strong> and are: </em><br /><em>-&#0160;<strong>SurfaceGeometryForm_NURBS:</strong>&#0160;Convert analytic surfaces to <strong>NURBS</strong>. This may result in splitting faces and edges as necessary. When used alone, only the faces are converted to <strong>NURBS</strong>. Edges will be represented by analytic curves. </em><br /><em>-&#0160;<strong>SurfaceGeometryForm_ProceduralToNURBS:</strong>&#0160;Convert procedural surfaces to more accurate <strong>NURB</strong> approximations. This may result in splitting faces and edges as necessary </em><br /><em>- These are bitwise values and combining <strong>SurfaceGeometryForm_NURBS and SurfaceGeometryForm_ProceduralToNURBS</strong> will create a result where the entire body or face is converted to NURBS, procedural surfaces are accurately approximated, <strong>and all edges will also be converted to NURBS</strong>.</em></p>
<p>The following code shows how to get the various types of geometry from the model. In this case we are testing it with this cylinder body shown in the picture:</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d169f076970c-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="Nurbs2" class="asset  asset-image at-xid-6a0167607c2431970b01b8d169f076970c img-responsive" src="/assets/image_43147e.jpg" title="Nurbs2" /></a></p>
<pre>Sub PrintBodyInfo(sb As SurfaceBody)
  Dim fs As FaceShell
  For Each fs In sb.FaceShells
    Debug.Print Space(1); &quot;FaceShell&quot;
    Dim f As Face
    For Each f In fs.Faces
      Debug.Print Space(2); &quot;Face: &quot; + TypeName(f.Geometry)
      Dim el As EdgeLoop
      For Each el In f.EdgeLoops
        Debug.Print Space(3); &quot;EdgeLoop&quot;
        Dim e As Edge
        For Each e In el.Edges
          Debug.Print Space(4); &quot;Edge: &quot; + TypeName(e.Geometry)
        Next
      Next
    Next
  Next
End Sub

Sub GetBodies()
  Dim pd As PartDocument
  Set pd = ThisApplication.ActiveDocument
    
  Dim cd As PartComponentDefinition
  Set cd = pd.ComponentDefinition
     
  Debug.Print &quot;&gt;&gt; Original geometry&quot;
  Dim sb1 As SurfaceBody
  Set sb1 = cd.SurfaceBodies(1)
  Call PrintBodyInfo(sb1)
    
  Debug.Print &quot;&gt;&gt; AlternateBody(SurfaceGeometryForm_NURBS)&quot;
  Dim sb2 As SurfaceBody
  Set sb2 = sb1.AlternateBody(SurfaceGeometryForm_NURBS)
  Call PrintBodyInfo(sb2)

  Debug.Print &quot;&gt;&gt; AlternateBody(SurfaceGeometryForm_NURBS &quot; + _
    &quot;Or SurfaceGeometryForm_ProceduralToNURBS)&quot;
  Dim sb3 As SurfaceBody
  Set sb3 = sb1.AlternateBody(SurfaceGeometryForm_NURBS Or _
    SurfaceGeometryForm_ProceduralToNURBS)
  Call PrintBodyInfo(sb3)
End Sub</pre>
<p>The result:</p>
<pre>&gt;&gt; Original geometry
 FaceShell
  Face: Cylinder
   EdgeLoop
    Edge: Circle
   EdgeLoop
    Edge: Circle
  Face: Plane
   EdgeLoop
    Edge: Circle
  Face: Plane
   EdgeLoop
    Edge: Circle
&gt;&gt; AlternateBody(SurfaceGeometryForm_NURBS)
 FaceShell
  Face: BSplineSurface
   EdgeLoop
    Edge: LineSegment
    Edge: Arc3d
    Edge: LineSegment
    Edge: Arc3d
  Face: BSplineSurface
   EdgeLoop
    Edge: LineSegment
    Edge: Arc3d
    Edge: LineSegment
    Edge: Arc3d
  Face: BSplineSurface
   EdgeLoop
    Edge: Arc3d
    Edge: Arc3d
  Face: BSplineSurface
   EdgeLoop
    Edge: Arc3d
    Edge: Arc3d
&gt;&gt; AlternateBody(SurfaceGeometryForm_NURBS Or 
SurfaceGeometryForm_ProceduralToNURBS)
 FaceShell
  Face: BSplineSurface
   EdgeLoop
    Edge: BSplineCurve
    Edge: BSplineCurve
    Edge: BSplineCurve
  Face: BSplineSurface
   EdgeLoop
    Edge: BSplineCurve
  Face: BSplineSurface
   EdgeLoop
    Edge: BSplineCurve</pre>
