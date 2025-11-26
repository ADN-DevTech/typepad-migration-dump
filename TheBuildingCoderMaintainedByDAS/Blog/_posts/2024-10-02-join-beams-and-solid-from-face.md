---
layout: "post"
title: "Join Beams and Solid from Face"
date: "2024-10-02 05:00:00"
author: "Jeremy Tammik"
categories:
  - "Element Relationships"
  - "Geometry"
  - "RST"
original_url: "https://thebuildingcoder.typepad.com/blog/2024/10/join-beams-and-solid-from-face.html "
typepad_basename: "join-beams-and-solid-from-face"
typepad_status: "Publish"
---

<p><link href="https://cdn.jsdelivr.net/npm/prismjs@1.29.0/themes/prism.min.css" rel="stylesheet" /></p>

<script src="https://cdn.jsdelivr.net/npm/prismjs@1.29.0/components/prism-core.min.js"></script>

<script src="https://cdn.jsdelivr.net/npm/prismjs@1.29.0/plugins/autoloader/prism-autoloader.min.js"></script>

<p><style> code[class*=language-], pre[class*=language-] { font-size : 90%; } </style></p>

<p>Let's look at creating an open shell, a solid extrusion from a face and defining a precise join between structural beams:</p>

<ul>
<li><a href="#2"><code>DirectShape</code> solid from planar face</a></li>
<li><a href="#3">Join between three beams</a></li>
</ul>

<h4><a name="2"></a> DirectShape Solid from Planar Face</h4>

<p>Luiz Henrique <a href="https://ricaun.com/">@ricaun</a> Cassettari explains how to create a DirectShape with only an open shell, with zero volume.
He also shared a nice sample demonstrating how
to <a href="https://forums.autodesk.com/t5/revit-api-forum/creating-solid-with-one-planarface-using-brepbuilder-based-on/m-p/13031540#M81609">create a solid from a PlanarFace using BRepBuilder based on slab sketch</a>:</p>

<p><strong>Question:</strong>
I need to create a Solid with a single <code>PlanarFace</code> using <code>BRepBuilder</code>.
The <code>PlanarFace</code> edges are based on a curves from sketch of a slab.</p>

<p><strong>Answer:</strong>
To start with, you can Look at the <a href="https://thebuildingcoder.typepad.com/blog/2017/05/revit-2017-and-2018-sdk-samples.html#4.4">BRepBuilderExample Revit SDK sample</a>.
It includes a creation of a non-planar face, with no solid.
The <a href="https://www.revitapidocs.com/2024/94c1fef4-2933-ce67-9c2d-361cbf8a42b4.htm">BRepBuilder class API documentation</a> demonstrates in detail the creation of faces, edges, coedges, including arcs.
Additional background information is provided by The Building Coder discussing <a href="https://thebuildingcoder.typepad.com/blog/2023/06/brepbuilder-and-toposurface-interior.html#2">BRepBuilder organisation</a>.</p>

<p>BRepBuilder can still be tricky to work with.
In your case, to create an open shell, you need to set the target <code>BRepType</code> in the constructor to <code>BRepType.OpenShell</code> to allow the builder to return a solid without a volume.
Then, the code you share should work.
Sometimes, the orientation matters; especially in <code>BRepType.Solid</code> and <code>BRepType.Void</code>, you need to make sure all edges is registered in the correct orientation.</p>

<p>Finally, here is a full sample to create a solid from a planar face:</p>

<p><center></p>

<p><a href="https://thebuildingcoder.typepad.com/img/ricaun_facetosolid.gif"><img class="asset  asset-image at-xid-6a00e553e16897883302e860ec5aee200d img-responsive" style="width: 300px; display: block; margin-left: auto; margin-right: auto;" alt="Face to solid" title="Face to solid" src="/assets/image_113209.jpg" /></a></p>

<p><a href="https://thebuildingcoder.typepad.com/img/ricaun_facetosolid.gif"><p style="font-size: 80%; font-style:italic">Click for animation</p></a></p>

<p></center></p>

<pre><code class="language-cs">using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;

namespace RevitAddin.Forum.Revit.Commands
{
  [Transaction(TransactionMode.Manual)]
  public class CommandFaceToSolid : IExternalCommand
  {
    public Result Execute(
      ExternalCommandData commandData,
      ref string message,
      ElementSet elementSet)
    {
      UIApplication uiapp = commandData.Application;
      Document document = uiapp.ActiveUIDocument.Document;

      try
      {
        var faceReference = uiapp.ActiveUIDocument.Selection.PickObject(
          Autodesk.Revit.UI.Selection.ObjectType.Face);
        var element = document.GetElement(faceReference);
        var face = element.GetGeometryObjectFromReference(faceReference)
         as Face;

        var solid = CreateSolidFromFace(face);
        var normal = face.ComputeNormal(new UV(0.5, 0.5));

        using (Transaction transaction = new Transaction(document))
        {
          transaction.Start("Create Solid");
          var ds = DirectShape.CreateElement(document,
            new ElementId(BuiltInCategory.OST_GenericModel));
          ds.SetName(ds.Category.Name);
          ds.SetShape(new[] { solid });
          ds.Location.Move(normal);
          transaction.Commit();
        }
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex);
      }

      return Result.Succeeded;
    }

    private Solid CreateSolidFromFace(Face face)
    {
      var surface = face.GetSurface();
      var brepBuilder = new BRepBuilder(BRepType.OpenShell);
      var faceIsReversed = !face.OrientationMatchesSurfaceOrientation;
      BRepBuilderGeometryId faceId = brepBuilder.AddFace(
        BRepBuilderSurfaceGeometry.Create(surface, null), faceIsReversed);
      foreach (CurveLoop curveLoop in face.GetEdgesAsCurveLoops())
      {
        BRepBuilderGeometryId loopId = brepBuilder.AddLoop(faceId);
        foreach (Curve curve in curveLoop)
        {
          var edge = BRepBuilderEdgeGeometry.Create(curve);
          BRepBuilderGeometryId edgeId = brepBuilder.AddEdge(edge);
          brepBuilder.AddCoEdge(loopId, edgeId, false);
        }
        brepBuilder.FinishLoop(loopId);
      }
      brepBuilder.SetFaceMaterialId(faceId, face.MaterialElementId);
      brepBuilder.FinishFace(faceId);
      brepBuilder.Finish();

      return brepBuilder.GetResult();
    }
  }
}</code></pre>

<p>Many thanks to Ricaun for the nice sample and explanation.</p>

<h4><a name="3"></a> Join Between Three Beams</h4>

<p>Dayanand Rakte raised and solved another issue, to apply
a <a href="https://forums.autodesk.com/t5/revit-api-forum/join-between-3-beams/m-p/13029976">join between 3 beams</a>:</p>

<p><strong>Question:</strong> I am struggling with join/connection between 3 beams.
The current result looks like this:</p>

<p><center></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e16897883302e860d537d3200b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883302e860d537d3200b image-full img-responsive" alt="Join three beams" title="Join three beams"  src="/assets/image_5974db.jpg" border="0" style="display: block; margin-left: auto; margin-right: auto;" /></a><br /></p>

<p></center></p>

<p>The point highlighted with orange color is the endpoint location of Beam2 and Beam1, and the same point is start point location for Beam3.
I want to connect these beams to achieve the following expected outcome:</p>

<p><center></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e16897883302c8d3bec0da200c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883302c8d3bec0da200c image-full img-responsive" alt="Join three beams" title="Join three beams"  src="/assets/image_3340b4.jpg" border="0" style="display: block; margin-left: auto; margin-right: auto;" /></a><br /></p>

<p></center></p>

<p>It will be ok if there is no join between these three beams, but they should be place as expected.
I tried the following approaches:</p>

<ul>
<li>First: I have not used JoinGeometry API and directly placed beams as per their location.
In this approach I am getting random results, any of the the beam is extending which I don't have control.</li>
<li>Second: I used the join geometry API as shown below.
It generates the situation in the first image.</li>
</ul>

<pre><code class="language-cs">private void JoinGeometryBeam1ToBeam2Beam3(
  Document activeDoc,
  FamilyInstance Beam1)
{
  XYZ minPt = primaryBeam.get_BoundingBox(activeDoc.ActiveView).Min;
  XYZ maxPt = primaryBeam.get_BoundingBox(activeDoc.ActiveView).Max;
  Outline outLine = new Outline(minPt, maxPt);
  outLine.Scale(1.5);
  BoundingBoxIntersectsFilter filter = new BoundingBoxIntersectsFilter(outLine);
  List&lt;FamilyInstance&gt; connectedBeams
    = new FilteredElementCollector(activeDoc)
      .WherePasses(filter)
      .OfCategory(BuiltInCategory.OST_StructuralFraming)
      .OfClass(typeof(FamilyInstance))
      .Cast&lt;FamilyInstance&gt;()
      .ToList();

  foreach (FamilyInstance beam in connectedBeams)
  {
    JoinGeometryUtils.JoinGeometry(this.ActiveDoc, beam, Beam1);
  }
}</code></pre>

<p>How can the desired result be achieved?</p>

<p><strong>Solution:</strong>
It is working for me now.
I disallowed join for Beam2 and Beam3 and Beam1 kept as it is.
To disallow the join, I use the following API call:</p>

<pre><code class="language-cs">StructuralFramingUtils.DisallowJoinAtEnd(girderInstance, 0);</code></pre>

<p>I make sure that Beam2 and Beam3 are inserted before Beam1.
Then, the join between 3 beams works as expected.</p>

<p>Many thanks to Dayanand Rakte for sharing this solution.</p>
