---
layout: "post"
title: "ExtentElem and Square Face Dimensioning References"
date: "2017-09-12 05:00:00"
author: "Jeremy Tammik"
categories:
  - "2018"
  - "Dimensioning"
  - "Family"
  - "Filters"
  - "Geometry"
original_url: "https://thebuildingcoder.typepad.com/blog/2017/09/extentelem-and-square-face-dimensioning-references.html "
typepad_basename: "extentelem-and-square-face-dimensioning-references"
typepad_status: "Publish"
---

<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>

<p>Alexander Ignatovich, <a href="https://github.com/CADBIMDeveloper">@CADBIMDeveloper</a>, aka Александр Игнатович,
answered several interesting questions in 
the <a href="http://forums.autodesk.com/t5/revit-api-forum/bd-p/160">Revit API discussion forum</a> and elsewhere:</p>

<ul>
<li><a href="#2">The <code>ExtentElem</code> and duplicating legend components</a></li>
<li><a href="#3">Obtaining generic model square face references for dimensioning</a></li>
<li><a href="#3.1">Preparing family with reference planes for dimensioning</a></li>
<li><a href="#4">Creating a line perpendicular to another</a></li>
</ul>

<h4><a name="2"></a>The ExtentElem and Duplicating Legend Components</h4>

<p>In Alexander's own words:</p>

<p>I recently developed a bunch of automation tools for legend views.</p>

<p>I faced and solved a tricky thing I want to share. </p>

<p>I had to copy legend components from view A to view B. I looked at <code>ElementTransformUtils.CopyElements</code> with partial success. However, instead of copying all legend components between the views, Revit created a new view for them.</p>

<p>My code at that moment looked like this:</p>

<pre class="code">
&nbsp;&nbsp;<span style="color:blue;">var</span>&nbsp;collector&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">FilteredElementCollector</span>(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;doc,&nbsp;legendView.Id&nbsp;);

&nbsp;&nbsp;<span style="color:blue;">var</span>&nbsp;elementsIds&nbsp;=&nbsp;collector
&nbsp;&nbsp;&nbsp;&nbsp;.WhereElementIsNotElementType()
&nbsp;&nbsp;&nbsp;&nbsp;.ToElementIds();

&nbsp;&nbsp;<span style="color:#2b91af;">ElementTransformUtils</span>.CopyElements(&nbsp;legendView,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;elementsIds,&nbsp;destLegendView,&nbsp;<span style="color:#2b91af;">Transform</span>.Identity,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">CopyPasteOptions</span>()&nbsp;);
</pre>

<p>I searched for a solution and found the old blog post
on <a href="http://thebuildingcoder.typepad.com/blog/2010/05/duplicate-legend-component.html">duplicating a legend component</a>.</p>

<p>That solution puts elements into a group, places a group instance and then ungroups it. This method still remains working with little modification, such as:</p>

<ul>
<li>Rename method <code>Ungroup</code> to <code>UngroupMembers</code>.</li>
<li>Pass a generic list of element ids instead of a list of elements to the <code>NewGroup</code> method.</li>
<li>Open and activate the destination view  before placing new group.</li>
</ul>

<p>Once I had that working, I was pretty sure that it included unnecessary overhead, so I continued my research.</p>

<p>Later I realized that the list of element ids included an <code>ExtentElem</code> element.</p>

<p>This needs to be removed from <code>elementsIds</code> to make this code work as expected.</p>

<p>As always, when using a filtered element collector, the question arises on how to identify it.</p>

<p>In this case, all other elements have a valid category, and this one does not, so we can use:</p>

<pre class="code">
&nbsp;&nbsp;<span style="color:blue;">var</span>&nbsp;elementsIds&nbsp;=&nbsp;collector
&nbsp;&nbsp;&nbsp;&nbsp;.WhereElementIsNotElementType()
&nbsp;&nbsp;&nbsp;&nbsp;.Where(&nbsp;x&nbsp;=&gt;&nbsp;x.Category&nbsp;!=&nbsp;<span style="color:blue;">null</span>&nbsp;)&nbsp;<span style="color:green;">//&nbsp;I&nbsp;don't&nbsp;want&nbsp;to&nbsp;use&nbsp;name,&nbsp;but&nbsp;I've&nbsp;found&nbsp;that&nbsp;all&nbsp;other&nbsp;use&nbsp;elements&nbsp;in&nbsp;legend&nbsp;view&nbsp;has&nbsp;category</span>
&nbsp;&nbsp;&nbsp;&nbsp;.Select(&nbsp;x&nbsp;=&gt;&nbsp;x.Id&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;.ToList();
</pre>

<p>So, this code remains simple and clear. The <code>ExtentElem</code> is a common problem and its id should not be passed to the <code>CopyElements</code> method.</p>

<p>Since I don't see this type of element mentioned by The Building Coder, I thought it worthwhile to point out.</p>

<p>Very many thanks to Alexander for this deep research and valuable insight!</p>

<h4><a name="3"></a>Obtaining Generic Model Square Face References for Dimensioning</h4>

<p>Next, Alexander implemented an add-in to help answer 
the <a href="http://forums.autodesk.com/t5/revit-api-forum/bd-p/160">Revit API discussion forum</a> forum thread
on <a href="https://forums.autodesk.com/t5/revit-api-forum/how-to-create-line-based-on-angle/m-p/7372910">obtaining references from edges in Python</a> to 
put together a routine that will automatically create dimensions on a square face of a generic model. i.e. the user selects the face and dimensions appear:</p>

<p><strong>Question:</strong> I've got some of the code working, obtaining the edges from a user selected 3D face, this returns the 4 edges (which are Edge class objects), it should simply be a case of obtaining the edge's reference that can be used to create the dimensions.</p>

<p>The Edge class has a Property which should return a reference to the edge - but when I run the code, it returns Null - and I can't see why.</p>

<p>This is the code:</p>

<pre class="prettyprint">
 # Dynamo
import clr
clr.AddReference('RevitAPI')
clr.AddReference('RevitAPIUI')
from Autodesk.Revit.DB import *
from Autodesk.Revit.UI import *

clr.AddReference("RevitServices")
import RevitServices
from RevitServices.Persistence import DocumentManager
from RevitServices.Transactions import TransactionManager

doc = DocumentManager.Instance.CurrentDBDocument
uiapp = DocumentManager.Instance.CurrentUIApplication
app = uiapp.Application
uidoc = DocumentManager.Instance.CurrentUIApplication.ActiveUIDocument

#The inputs to this node will be stored as a list in the IN variables.
dataEnteringNode = IN
selobject       = UnwrapElement(IN[0])  # Object to select

#Get user to pick a face
selob = uidoc.Selection.PickObject(Selection.ObjectType.PointOnElement, "Pick something now")

#Get Id of element thats picked
selobid = selob.ElementId

#Get element thats picked
getob = doc.GetElement(selobid)

#Get face thats picked
getface = getob.GetGeometryObjectFromReference(selob)

#Get edges of face (returns a list the first object is the list of edges)
edgeloops = getface.EdgeLoops

#Select the first edge
dimedge1 = edgeloops[0][0]

#Select the third edge (the one opposite the first)
dimedge2 = edgeloops[0][2]

#Obtain a reference of the first edge
edgeref1 = dimedge1.Reference

#Obtain a reference of the thord edge
edgeref2 = dimedge2.Reference

#Assign your output to the OUT variable.
OUT = [selob, selobid, getob, getface, edgeloops, dimedge1, dimedge2, edgeref1, edgeref2]
</pre>

<p>If you execute it, you'll see that <code>edgeref1</code> and <code>edgeref2</code> variables both contain <code>Null</code>.</p>

<p>Any idea why?</p>

<p><strong>Answer:</strong> I found the solution for your problem.</p>

<p>The main idea is to retrieve the element geometry with the option set to <code>ComputeReference = True</code> and then find the appropriate face by reference.</p>

<p>Sorry, I don't know Python too much, so I created an add-in in C# for you. You may get it from
the <a href="https://github.com/CADBIMDeveloper/PutRevitDimensionsToSquareFaces">PutRevitDimensionsToSquareFaces add-in GitHub repository</a>.</p>

<p>It includes a lot of tricks with Revit references to make this work as expected with families.</p>

<p>Initially, I only tested it only with floors; now it works with family instances too.</p>

<p>Many thanks to Alexander for this work!</p>

<p>Jeremy adds: I added a readme and license to the code for him, because:</p>

<ul>
<li><a href="http://thebuildingcoder.typepad.com/blog/2016/10/how-to-create-a-new-line-style.html#4">A readme is a must</a>!</li>
<li><a href="http://thebuildingcoder.typepad.com/blog/2013/10/the-building-coder-samples-on-github.html#2">People cannot use the code unless you specify a license</a>!</li>
<li><a href="http://thebuildingcoder.typepad.com/blog/2013/10/wall-compound-layer-and-other-geometry.html#2">More on licenses</a>...</li>
</ul>

<p>I myself use the <a href="http://opensource.org/licenses/MIT">MIT License</a> for my samples, "a lax, permissive non-copyleft free software license. For substantial programs, it is better to use the Apache 2.0 license since it blocks patent treachery".</p>

<p>My samples are not substantial.</p>

<h4><a name="3.1"></a>Preparing Family with Reference Planes for Dimensioning</h4>

<p>Alexander later added another, simpler solution to the conversation:</p>

<p>If you use Revit 2018 or later, you can prepare your family for easier dimensioning by adding specific reference planes in the family definition, e.g., xLeft, xRight, yTop, yBottom, and then access them on the family instance in the project environment like this:</p>

<pre class="prettyprint">
def CreateDimension(instance, refNames, direction):
  references = ReferenceArray()

  for x in refNames:
    references.Append(instance.GetReferenceByName(x))

  origin = instance.Location.Point

  transform = instance.GetTotalTransform()
  transform.Origin = XYZ.Zero

  dimensionDirection = transform.OfPoint(direction)

  dimensionLine = Line.CreateUnbound(origin, dimensionDirection)

  doc.Create.NewDimension(doc.ActiveView, dimensionLine, references)


famInst = selection[0]

tx = Transaction(doc, "create dimensions")
tx.Start()

CreateDimension(famInst, ["xLeft", "xRight"], XYZ.BasisX)
CreateDimension(famInst, ["yBottom", "yTop"], XYZ.BasisY)

tx.Commit()
</pre>

<h4><a name="4"></a>Creating a Line Perpendicular to Another</h4>

<p>Finally, a trivial question to round this off, though very useful for the geometrically challenged, from
the <a href="http://forums.autodesk.com/t5/revit-api-forum/bd-p/160">Revit API discussion forum</a> forum thread
on <a href="https://forums.autodesk.com/t5/revit-api-forum/how-to-create-line-based-on-angle/m-p/7372910">how to create a line based on an angle</a>:</p>

<p><strong>Question:</strong> I have a line element in Revit.</p>

<p>I want to add a second line perpendicular to it.</p>

<p>The second line start point can be any point on the line which is 90 degrees from the first line axis.</p>

<p>How can I create the second line based on this angle?</p>

<p>Here is an example of the line I need:</p>

<p><center></p>

<p><a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e16897883301b8d2a9b48b970c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883301b8d2a9b48b970c image-full img-responsive" alt="Perpendicular line" title="Perpendicular line" src="/assets/image_7f3c50.jpg" border="0" /></a><br /></p>

<p></center></p>

<p>I have been playing around with the methods <code>CreateTransformed</code>, <code>CreateRotationAtPoint</code> and <code>CrossProduct</code>, but I still don't get the result I want.</p>

<p><strong>Answer:</strong> It is simple. Just determine a) the line direction and b) the line sketch plane normal.</p>

<p>Then, <code>normal.CrossProduct(direction)</code> defines your second line direction, or maybe you need <code>-1*normal.CrossProduct(direction)</code>.</p>

<p>Take any point on first line, for example, <code>pt1 = fistLine.Evaluate(0.5, true)</code>, set <code>pt2 = pt1 + length * direction</code>, create the second line using <code>Line.CreateBound(pt1, pt2)</code>, then create a model curve on the first line's sketch plane, based on this second line.</p>

<p>Thanks again to Alexander for this succinct explanation!</p>
