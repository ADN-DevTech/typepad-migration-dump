---
layout: "post"
title: "Pyramid Builder, CommandLoader, et al"
date: "2023-02-14 05:00:00"
author: "Jeremy Tammik"
categories:
  - ".NET"
  - "Element Creation"
  - "Filters"
  - "Geometry"
  - "Mac"
  - "Material"
  - "Units"
  - "VB"
original_url: "https://thebuildingcoder.typepad.com/blog/2023/02/pyramid-builder-commandloader-et-al.html "
typepad_basename: "pyramid-builder-commandloader-et-al"
typepad_status: "Publish"
---

<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>

<p>Happy St. Valentine's Day!</p>

<p>Lots of activity in
the <a href="http://forums.autodesk.com/t5/revit-api-forum/bd-p/160">Revit API discussion forum</a> and
elsewhere:</p>

<ul>
<li><a href="#2">Dynamic load, compile and run code</a></li>
<li><a href="#3">DirectShape pyramids</a></li>
<li><a href="#4">Modify level element X and Y extents</a></li>
<li><a href="#5">How to filter for subsets of elements</a></li>
<li><a href="#6">Switch document display units</a></li>
<li><a href="#7">Material tags displaying '?'</a></li>
<li><a href="#8">Sublime Text</a></li>
</ul>

<h4><a name="2"></a> Dynamic Load, Compile and Run Code</h4>

<p>Recently, several <a href="http://forums.autodesk.com/t5/revit-api-forum/bd-p/160">Revit API discussion forum</a> threads
revolved around how to dynamically load and compile Revit add-ins.</p>

<p>Luiz Henrique <a href="https://github.com/ricaun">@ricaun</a> Cassettari now shared a solution for that,
<a href="https://forums.autodesk.com/t5/revit-api-forum/revitaddin-commandloader-compile-running-iexternalcommand-with/td-p/11742530">RevitAddin.CommandLoader &ndash; compile and run <code>IExternalCommand</code> with Revit open</a>:</p>

<p>I present my first Revit add-in open-source project CommandLoader.
With this plugin is possible to compile <code>IExternalCommand</code> directly in Revit, and the command is added as a <code>PushButton</code> in the Addins Tab.
Here is an 8-minute video explaining the features and some limitations, <a href="https://youtu.be/l4V4-vohcWY">compile and run 'IExternalCommand' with Revit open</a>:</p>

<p><center>
<iframe width="480" height="270" src="https://www.youtube.com/embed/l4V4-vohcWY" title="YouTube video player" frameborder="0" allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture; web-share" allowfullscreen></iframe>
</center></p>

<p>RevitAddin.CommandLoader project compiles <code>IExternalCommand</code> with Revit open using <code>CodeDom.Compiler</code> and creates a <code>PushButton</code> on the Revit ribbon.</p>

<ul>
<li><a href="https://github.com/ricaun-io/RevitAddin.CommandLoader">RevitAddin.CommandLoader GitHub repository</a></li>
</ul>

<h4><a name="3"></a> DirectShape Pyramids</h4>

<p>Richard <a href="https://forums.autodesk.com/t5/user/viewprofilepage/user-id/1035859">RPThomas108</a> Thomas
implemented a very nice little sample using the <code>TessellatedShapeBuilder</code> to create <code>DirectShape</code>
<a href="https://en.wikipedia.org/wiki/Pyramid_(geometry)">regular pyramids</a> to answer
the question <a href="https://forums.autodesk.com/t5/revit-api-forum/is-it-possible-to-create-a-solid-from-the-edges-of-pyramids/td-p/11729445">is it possible to create a solid from the edges of pyramids?</a></p>

<p><center></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e16897883302b685285ce1200d-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883302b685285ce1200d img-responsive" style="width: 400px; display: block; margin-left: auto; margin-right: auto;" alt="Pyramids" title="Pyramids" src="/assets/image_920ab1.jpg" /></a><br /></p>

<p></center></p>

<p><strong>Answer:</strong> Yes.
You can do so with <code>TessellatedShapeBuilder</code>, but you only need the points, not the edges:</p>

<pre class="prettyprint">
  Private Function Obj_230204a( _
    ByVal commandData As Autodesk.Revit.UI.ExternalCommandData,
    ByRef message As String,
    ByVal elements As Autodesk.Revit.DB.ElementSet) As Result

    Dim UIApp As UIApplication = commandData.Application
    Dim UIDoc As UIDocument = commandData.Application.ActiveUIDocument
    If UIDoc Is Nothing Then Return Result.Cancelled Else
    Dim IntDoc As Document = UIDoc.Document

    Const NumberOfSides As Integer = 6
    Const BaseRadius As Double = 1
    Const ApexHeight As Double = 2

    Dim Seg As Double = 2.0 / NumberOfSides
    Dim Points As XYZ() = New XYZ(NumberOfSides - 1) {}
    For i = 0 To NumberOfSides - 1
      Dim P As Double = i * Seg
      Dim X As Double = Math.Sin(Math.PI * P) * BaseRadius
      Dim Y As Double = Math.Cos(Math.PI * P) * BaseRadius

      Points(i) = New XYZ(X, Y, 0)
    Next
    Dim builder As New TessellatedShapeBuilder()
    builder.OpenConnectedFaceSet(True)

    'The bottom face
    builder.AddFace(New TessellatedFace(Points, ElementId.InvalidElementId))

    'Side faces
    Dim ApexPt As New XYZ(0, 0, ApexHeight)

    For i = 0 To Points.Length - 1
      Dim J As Integer = i + 1
      If i = Points.Length - 1 Then
        J = 0
      End If

      Dim P1 As XYZ = Points(i)
      Dim P2 As XYZ = Points(J)
      builder.AddFace(New TessellatedFace(New XYZ(2) {P1, P2, ApexPt}, ElementId.InvalidElementId))
    Next
    builder.CloseConnectedFaceSet()

    builder.Target = TessellatedShapeBuilderTarget.Solid
    builder.Fallback = TessellatedShapeBuilderFallback.Abort
    builder.Build()

    Dim Res As TessellatedShapeBuilderResult = builder.GetBuildResult
    If Res.Outcome = TessellatedShapeBuilderOutcome.Solid Then

      Using Tx As New Transaction(IntDoc, "Pyramid")
        If Tx.Start = TransactionStatus.Started Then

          Dim ds As DirectShape = DirectShape.CreateElement(IntDoc, New ElementId(BuiltInCategory.OST_GenericModel))
          ds.SetShape(Res.GetGeometricalObjects())

          Tx.Commit()
        End If
      End Using

    End If

    Return Result.Succeeded
  End Function
</pre>

<p>Rough C# translation from VB.NET:</p>

<pre class="prettyprint">
  public Result Obj_230204a(
    Autodesk.Revit.UI.ExternalCommandData commandData,
    ref string message,
    Autodesk.Revit.DB.ElementSet elements)
  {
    UIApplication UIApp = commandData.Application;
    UIDocument UIDoc = commandData.Application.ActiveUIDocument;
    if (UIDoc == null)
      return Result.Cancelled;
    Document IntDoc = UIDoc.Document;

    const int NumberOfSides = 6;
    const double BaseRadius = 1;
    const double ApexHeight = 2;

    double Seg = 2.0 / NumberOfSides;
    XYZ[] Points = new XYZ[NumberOfSides];
    for (int i = 0; i <= NumberOfSides - 1; i++)
    {
      double P = i * Seg;
      double X = Math.Sin(Math.PI * P) * BaseRadius;
      double Y = Math.Cos(Math.PI * P) * BaseRadius;

      Points[i] = new XYZ(X, Y, 0);
    }
    TessellatedShapeBuilder builder = new TessellatedShapeBuilder();
    builder.OpenConnectedFaceSet(true);

    //The bottom face
    builder.AddFace(new TessellatedFace(Points, ElementId.InvalidElementId));

    //Side faces
    XYZ ApexPt = new XYZ(0, 0, ApexHeight);

    for (int i = 0; i <= Points.Length - 1; i++)
    {
      int J = i + 1;
      if (i == Points.Length - 1)
      {
        J = 0;
      }

      XYZ P1 = Points[i];
      XYZ P2 = Points[J];
      builder.AddFace(new TessellatedFace(new XYZ[3] {
      P1,
      P2,
      ApexPt
    }, ElementId.InvalidElementId));
    }
    builder.CloseConnectedFaceSet();

    builder.Target = TessellatedShapeBuilderTarget.Solid;
    builder.Fallback = TessellatedShapeBuilderFallback.Abort;
    builder.Build();

    TessellatedShapeBuilderResult Res = builder.GetBuildResult();

    if (Res.Outcome == TessellatedShapeBuilderOutcome.Solid)
    {
      using (Transaction Tx = new Transaction(IntDoc, "Pyramid"))
      {

        if (Tx.Start() == TransactionStatus.Started)
        {
          DirectShape ds = DirectShape.CreateElement(IntDoc,
            new ElementId(BuiltInCategory.OST_GenericModel));
          ds.SetShape(Res.GetGeometricalObjects());

          Tx.Commit();
        }
      }
    }
    return Result.Succeeded;
  }
</pre>

<p>Many thanks to Richard for the nice sample!</p>

<h4><a name="4"></a> Modify Level Element X and Y Extents</h4>

<p>Richard also suggested <a href="https://forums.autodesk.com/t5/revit-api-forum/how-to-modify-levels-extents-x-and-y-direction/td-p/11731529">how to modify level extents in X and Y direction</a>:</p>

<p><strong>Question:</strong> I can get levels extents with <code>get_BoundingBox</code> and am looking for something like <code>set_BoundingBox</code>. I want to keep the level's Z elevation at the same level and stretch its bounding box in X and Y direction:</p>

<p><center></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e16897883302b7519569c4200c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883302b7519569c4200c img-responsive" alt="Level X Y extent" title="Level X Y extent" src="/assets/image_9eae50.jpg" style="display: block; margin-left: auto; margin-right: auto;" /></a><br /></p>

<p></center></p>

<p><strong>Answer:</strong> There is some functionality on the <code>DatumPlane</code> class that <code>Level</code> inherits from, e.g.:</p>

<ul>
<li>DatumPlane.SetCurveInView</li>
<li>DatumPlane.Maximize3DExtent</li>
<li>DatumPlane.PropagateToViews</li>
</ul>

<p>Seems better to maximize the extents and propagate to views rather than individually manipulating curves.</p>

<h4><a name="5"></a> How to Filter for Subsets of Elements</h4>

<p>Some very basic hints on generic filtering came up in this question:</p>

<p><strong>Question:</strong> ... on the parsed element structure of the Revit model; you could think of it as the model tree in Navisworks.
Users want to access the parsed structured data and graphic elements of the BIM, select objects by filtering Revit views, grids, family categories or MEP systems, and then create assemblies after selecting elements for documentation.</p>

<p>Example 1: a relatively complex building includes multiple piping systems.
The user needs to quickly select the circuit of a certain piping system on a certain floor.</p>

<p>Example 2: in a section of linear engineering, such as an elevated road, the user needs to quickly select the elements between two grids:</p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e16897883302b751711afc200b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883302b751711afc200b img-responsive" alt="Elevated road" title="Elevated road" src="/assets/image_262564.jpg" style="display: block; margin-left: auto; margin-right: auto;" /></a><br />
<center></p>

<p></center></p>

<p><strong>Answer:</strong> The Revit API provides many ways to filter down to the elements you are looking for.
It depends on the particular need.
In Example 1, you might want to start with the elements in the target system, but then filter further with an <code>ElementParameterFilter</code> for the reference level and/or with a geometric filter like <code>BoundingBoxIntersectsFilter</code> or <code>ElementIntersectsSolidFilter</code>.
Example 2 seems more geometric, so filter first by certain categories and then use the geometric filters after calculating a shape that represents the space between grids.
For more information on all the filters, please refer to the knowledgebase article
on <a href="https://knowledge.autodesk.com/support/revit/learn-explore/caas/CloudHelp/cloudhelp/2014/ENU/Revit/files/GUID-A2686090-69D5-48D3-8DF9-0AC4CC4067A5-htm.html">Applying Filters</a>.</p>

<h4><a name="6"></a> Switch Document Display Units</h4>

<p>In the thread
on <a href="https://forums.autodesk.com/t5/revit-api-forum/converting-all-parameter-values-from-imperial-units-to-metric/m-p/11728282">converting all parameter values from imperial to metric units</a>,
<i>nikolaEXEZM</i> shared two simple macros showing how to switch document display units between Imperial and Metric.</p>

<blockquote>
  <p>Works with both project and family documents.
  Just create a new Macro Module, and paste in the code below:</p>
</blockquote>

<pre class="prettyprint">
public void ChangeUnitsToImperial()
{
  Document doc = this.ActiveUIDocument.Document;

  Document templateDoc = Application.OpenDocumentFile(
    @"C:\ProgramData\Autodesk\RVT "
      + this.Application.VersionNumber
      + @"\Templates\English-Imperial\default.rte");

  using (Transaction ta = new Transaction(doc))
  {
    ta.Start("Change Project Units to Imperial");
    doc.SetUnits(templateDoc.GetUnits());
    ta.Commit();
  }
}

public void ChangeUnitsToMetric()
{
  Document doc = this.ActiveUIDocument.Document;

  Document templateDoc = Application.OpenDocumentFile(
    @"C:\ProgramData\Autodesk\RVT "
      + this.Application.VersionNumber
      + @"\Templates\English\DefaultMetric.rte");

  using (Transaction ta = new Transaction(doc))
  {
    ta.Start("Change Project Units to Metric");
    doc.SetUnits(templateDoc.GetUnits());
    ta.Commit();
  }
}
</pre>

<p>Many thanks to Nikola for sharing these.</p>

<h4><a name="7"></a> Material Tags Displaying '?'</h4>

<p>A couple of threads mentioned a problem with material tags displaying question marks '?' after minor changes to the model, forcing the user to waste time regenerating or nudging all material tags every time right before printing a drawing set.
A workaround for this was mentioned in the ticket <i>REVIT-20249</i>:</p>

<blockquote>
  <p>Standard Operating Procedure around here is right before printing, select a material tag &gt; right click &gt; select all instances in entire project &gt; nudge right &gt; nudge left &gt; print.</p>
</blockquote>

<h4><a name="8"></a> Sublime Text</h4>

<p>Closing with a non-Revit topic, I recently updated my computer to
the <a href="https://thebuildingcoder.typepad.com/blog/2022/12/exploring-arm-chatgpt-nairobi-and-the-tsp.html#11">MacBook Pro M1 ARM</a>.
Then, I updated the OS to MacOS Ventura, and my beloved and trusty old Komodo Edit text editor stopped working.
It has not been maintained for years.
Searching for a new minimalist text editor, I happened
upon <a href="https://www.sublimetext.com/">Sublime Text</a> and
started using that.
I am glad to report that it works perfectly for me.</p>

<p>I love the way that all settings are stored in JSON and take effect the moment you save the JSON file.</p>

<p>Today, I also added my first own key binding, also saved in JSON and taking immediate effect on saving the file.</p>

<p>Now, to round it off, I installed my first plugin, implemented in Python by Giampaolo Rodola:
<a href="https://gmpy.dev/blog/2022/sublime-text-remember-cursor-position-plugin">Sublime Text: remember cursor position plugin</a>.
Same procedure: install the Python file in the appropriate location
&ndash; <i>~/Library/Application Support/Sublime Text/Packages/User</i>, in my case
&ndash; and it immediately starts working.</p>

<p>I wish everything worked like this.</p>
