---
layout: "post"
title: "Custom Shapes in the Maya API"
date: "2012-09-30 23:00:00"
author: "Cyrille Fauvel"
categories:
  - "Custom Nodes"
  - "Cyrille Fauvel"
  - "Geometry"
  - "Maya"
  - "Modeling"
original_url: "https://around-the-corner.typepad.com/adn/2012/09/custom-shapes-in-the-maya-api-1.html "
typepad_basename: "custom-shapes-in-the-maya-api-1"
typepad_status: "Publish"
---

<p>A
user-defined surface shape is one of the most complex types of nodes that can
be implemented within Maya. A number of supporting classes are required, and a
number of virtual methods must be properly implemented to get your class
working right with the various parts of Maya. The goal of this article is to
break down the process of creating the shape into manageable chunks, and to
provide details of each step of the process so that you can make good design
decisions as you decide how and what to implement for your custom shape.</p>
<p>We will start by
discussing the motivation for user-defined shapes. And continue step-by-step by
adding the methods used to support drawing, selection, components,
manipulation, history, sets and deformation. At each step we will show the minimum
needed to implement each level of functionality so that you can decide which
level of complexity is appropriate for the particular type of shape you want to
implement. As we add virtual methods to our shape, we will discuss the purpose
and the circumstances under which the methods are called. This paper also
covers some related areas such as deformers, sets (groupParts, groupIds), and
tweaks. See this previous <a href="http://around-the-corner.typepad.com/adn/2012/09/maya-deformer-architecture.html" target="_self">article</a> for more details.</p>
<p>In conjunction
with the post, I have provided a number of <a href="http://around-the-corner.typepad.com/files/customShapeSamples.zip" target="_self">example plug-ins</a>. These plug-ins
demonstrate the shape implementation at various stages: draw/select, settable
components, manipulatable components, etc... The shape represented in all of
these plug-ins is extremely simple à a 4-point line segment sometimes
drawn as a hermite curve. The goal of using such a simple, immediately
understandable shape is that we didn’t want the details of the shape type to
obscure details of the implementation of the node itself.</p>
<h3>MPxLocatorNode vs. MPxSurfaceShape</h3>
<p>Two classes
can be used to implement a shape within Maya. The most basic is the
MPxLocatorNode class. If your shape needs to support only drawing and
selection, it is best to derive from the MPxLocatorNode class. The
MPxLocatorNode class is derived from the same nodes as Maya’s internal locator
(crosshair) node.</p>
<p>The Maya
Developer’s Toolkit includes an example MPxLocatorNode called
footPrintNode.cpp. This example shows that deriving from MPxLocatorNode is
quite straightforward. Typically, the only virtual methods that need to be
overridden are:</p>
<ul>
<li>MPxLocatorNode::draw()</li>
<li>MPxLocatorNode::boundingBox()</li>
<li>MPxLocatorNode::isBounded()</li>
</ul>
<p>The draw
implementation typically would contain OpenGL calls to draw the shape. The
boundingBox and isBounded methods allow Maya to determine whether or not your
object is within the camera’s view. When the object is not within the view, the
draw method will not be invoked.</p>
<p>Selection of
nodes derived from MPxLocatorNode works automatically based on the
implementation of the above three routines. When the mouse click/drag falls
within the bounding box returned by your shape, Maya invokes the draw method
using OpenGL selection to determine whether the shape has been selected.</p>
<p>If you want
your shape to include any of the following, more advanced capabilities, you
must instead derive from MPxSurfaceShape:</p>
<ul>
<li>Component
selection or manipulation</li>
<li>History
operations</li>
<li>Sets</li>
<li>Deformers</li>
</ul>
<h3>Introduction to MPxSurfaceShape</h3>
<p>As a learning
tool, we begin our look at the MPxSurfaceShape class by implementing the same
functionality as an MPxLocatorNode using the MPxSurfaceShape class. In other
words, this section covers what is needed to implement <em>drawing and selection
without component</em>s for an MPxSurfaceShape.</p>
<p> An example plug-in implementing
this level of functionality has been provided with the post in the folder
called <strong><em>curveDrawSel</em></strong>. This example plug-in simply draws a
non-editable 4-point line segment, and is extremely similar to the
footPrintNode in its capabilities.</p>
<p>The
Developer’s Toolkit for Maya has an example MPxSurfaceShape example plug-in
called apiMeshShape. One improvement to note in the curveDrawSel plug-in over
the apiMeshShape is that we use OpenGL selection rather than simply saying that
the shape is selected any time a selection occurs within its bounding box.</p>
<h4>The UI class: MPxSurfaceShapeUI</h4>
<p>An MPxSurfaceShape cannot be
implemented as a single class in isolation. Instead, for any MPxSurfaceShape
that is implemented, a corresponding MPxSurfaceShapeUI class must also be
implemented. The duties of the UI class are drawing and selection. The
MPxSurfaceShape node class performs all other shape-related operations.</p>
<p>The MPxSurfaceShapeUI
methods that must be implemented are:</p>
<ul>
<li>MPxSurfaceShapeUI::getDrawRequests()</li>
<li>MPxSurfaceShapeUI::draw()</li>
<li>MPxSurfaceShapeUI::select()</li>
</ul>
<p>Maya documentation covers
the motivation and usage of these methods, and can be found under <strong><em>Developer
Resources: API Guide -&gt; Shapes -&gt; Drawing and Refresh</em></strong>.</p>
<h4>The shape node class: MPxSurfaceShape</h4>
<p>Now that the
UI class is handling drawing and selection, the only required methods within
MPxSurfaceShape are:</p>
<ul>
<li>MPxSurfaceShape::boundingBox()</li>
<li>MPxSurfaceShape::isBounded()</li>
</ul>
<p>These methods
are called by Maya to determine whether or not to invoke your node’s selection
and drawing methods based on the view frustum of the camera.</p>
<h4>Summary</h4>
<p>If you simply
want a shape to draw itself and be selectable, use the MPxLocatorShape as your
base class. Doing the same thing with the surfaceShape class wasn’t a whole lot
more difficult, but we had to use 2 classes instead of one, and we had to
implement 2 new methods (select and getDrawRequests) to implement functionality
that is handled automatically for the locator shape.</p>
<p>On the plus
side, we have more flexibility. If we wanted to do some alternative tricky
implementation of select rather than OpenGL select, the surfaceShape class does
allow this sort of customization while the locatorShape does not. But the main
reason to derive from MPxSurfaceShape is so that we can expand our shape to add
component handling.</p>
<h3>Component Handling</h3>
<p>In this
section, we add basic component handling to the shape. The example plug-in for
this section is provided in the folder called <strong><em>compCurveDrawSel</em></strong>.
Here we add support for customizing the shape of the curve by specifying the
component locations using the setAttr command. We do not yet allow the user to
select or move the components, only to specify their locations using setAttr or
connections.</p>
<h4>The component attribute</h4>
<p>The first
decision to be made is how we want to represent the components within the node.
In this example, like the existing apiMeshShape.cpp example, we will store the
components in the same attribute as some existing Maya shapes: the
“controlPoints” attribute. The controlPoints attribute is inherited from the
“controlPoint” node type – one of the base classes of MPxSurfaceShape. The
controlPoint node is also a base class of Maya’s nurbsSurface, mesh,
subdivSurface, nurbsCurve and lattice shapes. The nurbsSurface, nurbsCurve and
lattice types all make use of the “controlPoints” attribute. Meshes use their
own attribute to store vertices because of an architectural decision to use
floating point rather than double storage.</p>
<p style="text-align: center;">&#0160;
<a class="asset-img-link" href="http://around-the-corner.typepad.com/.a/6a0163057a21c8970d017744de77a0970d-pi" style="display: inline;"><img alt="ControlPoints" border="0" class="asset  asset-image at-xid-6a0163057a21c8970d017744de77a0970d image-full" src="/assets/image_f9ef7d.jpg" title="ControlPoints" /><br /></a><em>Attribute documentation from the
controlPoint node’s controlPoints attribute.</em></p>
<p>The reason
for storing the control points in an attribute is threefold. First, because the
individual xyz components are numeric attributes, the attribute can be animated
and connected to other numeric attributes. Second, the Maya file format is
organized around attributes, and the controlPoints attribute data is saved to
file. The third reason is simply consistency. All the other shape nodes within
Maya store their points in a similar manner.</p>
<h4>Internal shape data</h4>
<p>We could
implement our shape by storing the controlPoint data solely in the datablock of
the plug-in node. The setAttr and getAttr commands will get and set data
directly into the datablock by default. If we did store the data solely in the
datablock, each time the draw method asked for the points, our code would use
an MArrayDataHandle to get the value of the controlPoints attribute from the
MDataBlock. For such a small shape as the curve in our example plug-in, these
operations would not be very expensive. However for a large shape with many
controlPoints, accessing points from the MArrayDataHandle class can be rather
tedious. </p>
<p>In addition,
many shapes have more information per component than solely the position. You
may want to store per-vertex tangency or connectivity information. For this
reason, many shapes within Maya choose to store their control point position
data separately from the datablock (for example in a geometry data attribute or
in an internal class member).</p>
<p>We chose to
store our shape data as an internal member of the node class. At this stage of
our example, our shape data can consist simply of an MPointArray. We call it
fCurvePoints and store it as a member of our node class.</p>
<h4>setInternalValue</h4>
<p>In order to
transfer the values that the user sets or connects to the controlPoints
attribute into our MPointArray class member, we implement the virtual method <strong><em>setInternalValue</em></strong>.
We take the data passed in and place it into our internal array. We also call </p>
<pre class="brush: cpp; toolbar: false;">MPxSurfaceShape::setInternalValue (plug, handle);
</pre>
<p>from our
override of <strong><em>setInternalValue</em></strong>. This call puts the controlPoint
data into the node’s datablock in addition to storing it in our MPointArray
class, and means that we do not need to implement getInternalValue. Instead, if
anyone (such as the getAttr command) asks for the value of the controlPoints
attribute, it will be handled by the DG, and will retrieve the value from the
datablock. Alternatively, if storage space was an issue, we would not need to
place the controlPoints into the datablock at all. In this case, we would
implement getInternalValue to retrieve and return the appropriate value from
our internal MPointArray.</p>
<p>One other
detail is necessary to keep our shape on screen up-to-date when the
controlPoints attribute is keyed (or is given some other input connection). We
need to change the method called from our UI class to retrieve the points. We
add the 2 lines shown below in bold:</p>
<pre class="brush: cpp; highlight: [2, 3]; toolbar: false;">void compCurveDrawSel::points(MPointArray points) {
  MDataBlock block =forceCache ();
  block.inputArrayValue (mControlPoints);
  points.setLength (sCurvePointCount);
  for (unsigned ii =0; ii &lt; sCurvePointCount; ii++)
    points.set (fCurvePoints [ii], ii);
}
</pre>
<p>This is only
necessary when there is an input connection to the controlPoints attribute
because of how dirty propagation and evaluation work within Maya. In other
words, when the user does a “setAttr” MEL command on the controlPoints
attribute, the data will be set immediately into the node and setInternalValue will be called. However, when there is
a connection to the controlPoints attribute, our node will be marked dirty
dirty, but setInternalValue does not get called unless our node
asks for the controlPoints value. When the controlPoints value is requested, it
will be dirty and will trigger evaluation of the connection. The addition of
the above 2 lines to the points method accomplishes the evaluation, and will
cause setInternalValue to be invoked, thereby updating the
fCurvePoints to the proper value.</p>
<h4>The bounding box, an optional performance improvement</h4>
<p>Our shape is
now controllable by the user, so its bounding box is variable. That means we
need to improve our bounding box handling. The boundingBox
method is called quite frequently -- each time the window refreshes. Therefore,
we don’t want to have an expensive calculation inside the boundingBox method. </p>
<p>To address
this, we will add 2 attributes to our shape representing the 2 corners of the
bounding box. We make these attributes affected by the controlPoints attributes
so that they will be marked dirty when the controlPoints are modified. These
are output attributes, so we implement a compute method that calculates the boundingBox based on the location of the controlPoints.</p>
<p>Now, when the
boundingBox method is called, it retrieves the
bounding corners from the datablock of the node. If the bounding corners are
not dirty, this is a very fast operation that simply references into the
datablock. If the corners are dirty, compute will be invoked to recalculate and
set the bounding box.</p>
<h4>Summary</h4>
<p>In this
section, we simply added support for user-settable and animatable control point
locations. We didn’t have to change our UI class at all. However, we did end up
implementing 2 new virtual methods (setInternalValue
and compute) and adding attributes to our node to improve the performance
of our bounding box calculation. </p>
<h3>Manipulation of components</h3>
<p>In this
section, we will add the ability to select and move individual components or
sets of components using both the manipulator and the move/rotate/scale
commands. The example plug-in demonstrating this additional capability is in
the folder called <strong><em>compCurveMovable</em></strong>.</p>
<p>Adding
support for component motion is a lot of work. This section covers the methods
and the additional class required, their purpose and when they are called from
Maya.</p>
<h4>The transformUsing() method</h4>
<p>The transformUsing method is called from the transformation MEL commands
such as move, rotate and scale. It is also called from the transform-related
manipulators such as translate, rotate and scale manips.</p>
<pre class="brush: cpp; toolbar: false;">virtual void transformUsing (
  const MMatrix&amp; mat,
  const MObjectArray&amp; componentList,
  MVertexCachingMode cachingMode,
  MPointArray* pointCache
);
</pre>
<p>Part of the
complexity of the transformUsing method is related to undo. The transformUsing method is called during both the original transform
command and during undo. During the initial command, the cachingMode will be
set to “save”. In undo, the cachingMode will be restored.</p>
<p>During
manipulator use, the cachingMode will be set to “save” initially, and “update”
subsequently. Undo of manipulations will have the cachingMode set to “restore”.
These settings can be better understood if we consider the motivation. Imagine
the user starts moving an object using the translate manipulator. In a single
mouse drag, the screen will refresh multiple times and the object will be
transformed in small increments by the many calls to transformUsing method. If the user then hits “Undo”, Maya wants to
restore the object to its original location in one operation, not multiple
calls to the transformUsing method.&#0160;</p>
<p>One thing to
note about our implementation of the transformUsing method is that we have implemented it
to transfer the motions directly into our class’s internal shape data
(fCurvePoints). One by-product of this is that it is now possible for our
internal data to be out of sync with the controlPoints attribute. The
manipulator links itself to the controlPoints attribute, and it needs to be
able to query the controlPoints attribute for the proper location of the point.
Therefore, we need to implement getInternalValue for our node.</p>
<h4>The getInternalValue() method</h4>
<p>Typically
you’ll implement getInternalValue for your node at the same time as you
implement setInternalValue. In the last chapter, we explained
why we were able to avoid implementing getInternalValue. Now there is no way to avoid it.
With the addition of transformation support, getInternalValue
is required to keep our node working right. The reason is that since transformUsing does not go through setInternalValue,
the transformation data never goes into the datablock. We implement getInternalValue so that it returns the requested
values from our internal fCurvePoints into the supplied handle argument.</p>
<h4>The componentToPlugs() method</h4>
<p>When a
transform manipulator is used on selected components, they call the componentToPlugs method to learn which plugs on your
shape node correspond to the selected components. The manipulator will query
these attribute values when it is determining where to draw itself.</p>
<p>This method is
also called when a command such as the setKeyframe command needs to determine
where to connect new animationCurves when a selected component is keyframed.</p>
<h4>Iterator Support</h4>
<p>Manipulators
in Maya require iterator support on shapes. This means that you need to add an
additional class, derived from MPxGeometryIterator. The following three virtual
methods also need to be added:</p>
<ul>
<li>geometryIteratorSetup</li>
<li>acceptsGeometryIterator(bool)</li>
<li>acceptsGeometryIterator(MObject&amp;,
     bool, bool)</li>
</ul>
<p>If you are
writing multiple user-defined shapes, you may well be able to share the same
iterator class amongst your shapes, although that will depend somewhat on how
you store the data within your shape class. If each shape has a very unique
data structure, it will probably make more sense to have a separate iterator
for each type of data.</p>
<p>How you
design your iterator will depend on your class itself, and what it needs to
support. As the seminar continues, we will be enhancing the iterator. For this
example, it is satisfactory to simply make the iterator store a pointer to our
shape, and loop through the shape’s points. The primary thing to consider when
writing your iterator is that it should not rely on any data that could be
destructed while the iterator is using it. For example, if geometryIteratorSetup is invoked with readOnly = false,
then you can expect the setPoint method to be called more than once.
Be sure that nothing in your implementation of setPoint
could cause the data held onto by the iterator to become stale.</p>
<h4>The UI Class</h4>
<p>The UI class
needs to be enhanced to support drawing and selection of the individual
components separately from the shape itself. This means expanding the getDrawRequests implementation to handle the case where the 2<sup>nd</sup>
argument (objectAndActiveOnly) is false.</p>
<pre class="brush: cpp; toolbar: false;">void compCurveMovableUI::getDrawRequests (
  const MDrawInfo &amp; info,
  bool objectAndActiveOnly,
  MDrawRequestQueue &amp; queue
);
</pre>
<p>When
objectAndActiveOnly is false, we will create and add an additional drawRequest
to the queue corresponding to the point components of our shape. If any of our
components are active, we also create a drawRequest for them.</p>
<p>When
components are drawn, our draw method will now get called more than once. We
modify our draw method to use the MDrawRequest::token() method to determine what should be
drawn.</p>
<p>When the user
selects in component mode, our select method needs to be able to handle it. In
component select mode, our shape will be hilited, and we use this as a cue to
determine whether or not to check for selected points.</p>
<h4>The matchComponent method</h4>
<p>Having
implemented all of the above, your node already supports the selection tool and
the transformation manipulators. But what if you manually enter the select or
move command and pass it a string that corresponds to your vertices?&#0160; The matchComponent method is used by Maya to create a
component corresponding to a string typed in by the user. </p>
<h3>Simple History Support</h3>
<p>The next
modification we will make to our shape is to support “simple” history. By
simple history, what we mean is that we are going to modify our shape so that
any node that outputs a data of type “vectorArray” can be provided as input to
our node. The figure below shows an example where we’ve connected the
particleShape’s position attribute into our curve node’s inputSurface
attribute.</p>
<p style="text-align: center;">
<a class="asset-img-link" href="http://around-the-corner.typepad.com/.a/6a0163057a21c8970d017d3c2f3f20970c-pi" style="display: inline;"><img alt="History" border="0" class="asset  asset-image at-xid-6a0163057a21c8970d017d3c2f3f20970c image-full" src="/assets/image_df5b2c.jpg" title="History" /><br /></a><em>The particleShape position drives the
location of the curve’s points.</em></p>
<p>By referring
to this as “simple” history, we are distinguishing it from more advanced types
of history covered in the next chapter such as deformer support and support for
component sets with history (groupParts nodes).</p>
<h4>Two forms of input: history and tweaks</h4>
<p>The trickiest
part of basic history support on a shape is that there are now two sources of
input for the position of the shape’s components.&#0160; Positions may be coming from the history
attribute.&#0160; In addition, the user can
make “tweaks” to the points, i.e. select and move them around or animate their
position. This data comes in as before, through the controlPoints attribute. As
a node designer, you must make a decision how you want to combine the history
data with the tweak data.&#0160; </p>
<p>In writing
this example shape, we chose to implement tweak handling in the same manner as
Maya’s internal shapes (nurbs, meshes, subds, lattices). These nodes all treat
tweaks as <em>relative</em> when the shape has history, and <em>absolute</em> when
the shape does not have history. The benefit of this approach is that the
tweaks made by the user can be combined with the input history even when the
history is animated.</p>
<p style="text-align: center;">&#0160;
<a class="asset-img-link" href="http://around-the-corner.typepad.com/.a/6a0163057a21c8970d017744de7dd3970d-pi" style="display: inline;"><img alt="Tweak_radius" border="0" class="asset  asset-image at-xid-6a0163057a21c8970d017744de7dd3970d" src="/assets/image_144ca1.jpg" title="Tweak_radius" /><br /></a><em>Adding tweaks on sphere: radius=1 &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160;Modifying history: radius=1.5</em></p>
<p>An
alternative approach would be to store the tweaks as absolute, regardless of
whether the shape has history. However, that would mean that when the history
changed, the tweaked points would need to either:</p>
<ul>
<li>lose
their tweaks to follow the history, or </li>
<li>stay
at the position defined by the tweaks, ignoring the new history</li>
</ul>
<p>Keep in mind that tweaks can be animated/connected
as well, so resetting the tweak attribute values on the fly as the history
changes is not really an option unless you want to prevent connections to them
when there is history. In the long run, how you implement your shape depends on
what you are trying to accomplish.</p>
<h4>Shape Input Data</h4>
<p>Thus far in
our example plug-ins, we have stored the shape data as a simple type,
MPointArray. In continuing this example, adding support for history might seem
like the appropriate time to switch our data to a more complex type, derived
from MPxGeometryData. Indeed, if we want to support component sets and
deformers, we must store our geometry data as an MPxGeometryData type. However,
at this stage in the example, we want to demonstrate the use of internal Maya
data types as the history attribute, so we will continue storing our data as an
MPointArray, and define our inputSurface attribute to accept data of type
MFnData::kVectorArray.</p>
<h4>The example plug-in</h4>
<p>The example
plug-in for this chapter is in the folder called <strong><em>compCurveSimpHist</em></strong>.
There is a MEL script in the folder called compCurveSimpSetup.mel which creates
the curve and attaches it to a particle shape. If you run the setup MEL script
and push play, you can see the curve points move along with the particles. You
can select points on the curve and tweak them to see how the tweaks interact
with the history.</p>
<h4>Adding the input surface attribute</h4>
<p>The most
obvious requirement is that we now need an input surface attribute. As
mentioned before, we are using an input data type of MFnData::kVectorArray for
the inputSurface. The inputSurface affects the boundingBox, and we will make it
an internal attribute, so that any time we evaluate it, we can transfer the
values from the input data into a local data member we’ll call fInputPoints.</p>
<p>When we add
the input attribute, it is also useful to override the virtual method:</p>
<pre class="brush: cpp; toolbar: false;">MObject compCurveSimpHist::localShapeInAttr() const;
</pre>
<p>This method
is called from a number of places such as:</p>
<ul>
<li>the attribute editor, when it
     builds its related node tabs</li>
<li>the channel box when it builds
     its “Inputs” list</li>
<li>the delete construction history
     operation (delete –ch MEL command)</li>
</ul>
<h4>Handling relative vs. absolute tweaks</h4>
<p>The other
primary job to be done to support history is to put in our history-dependent
handling for tweaks. </p>
<p><em>Internal
Data</em></p>
<p>Our previous
implementation of the shape had only one piece of internal data: the curve
points in absolute space. Now we will instead store 2 pieces of data: </p>
<ul>
<li>fCurvePoints (the history plus
     the tweaks)</li>
<li>fInputPoints (the history)</li>
</ul>
<p>We could
store a third array with the tweaks, but instead we will store the tweak data
directly in the datablock. There is no obvious advantage or disadvantage to
storing it in the datablock, but it does serve as an example of a different way
to store the data. You can decide how you want to store the tweaks in your own
node.</p>
<p><em>setInternalValue
</em>and<em>
getInternalValue</em></p>
<p>We modify
these virtual methods. They already had handling for the controlPoints
attribute, but we modify them so that when the shape has history, the data will
go directly to the datablock.</p>
<p><em>transformUsing</em></p>
<p>We implemented
transformUsing in the previous chapter. Its
responsibility is to tranfer motion from transform commands/manips into the
shape, and it bypasses setInternalValue. Since we are storing our tweaks in
the datablock when there is history, we need a way to transfer the motion from transformUsing into the datablock. We add a utility method called
updateControlPoints to do this, and call it from transformUsing
when there is history.</p>
<p><em>Getting the points for drawing/boundingBox</em></p>
<p>When we are
asked for the bounding box or need to draw the points, we want to make sure we
always have the most up-to-date representation of them. We add the lines shown
below to our boundingBox method and our points
method:</p>
<pre class="brush: cpp; toolbar: false;">if (fHasHistory) {
  block.inputValue (inputSurface);
  applyTweaks (block);
}
</pre>
<p>If the
history has changed, inputSurface will be dirty, and the call to inputValue
will invoke evaluation and a call to setInternalValue to make sure our fInputPoints are
up-to-date. The applyTweaks method will then add all of the tweaks
from the datablock to the fInputPoints.</p>
<p><em>The mHasHistoryOnCreateAttribute</em></p>
<p>One of the
attributes we inherit from the base class is the “mHasHistoryOnCreate”
attribute. This is an internal attribute which you can use to your advantage if
you want to know during file IO whether your shape will have history or not.
This is useful because during file IO, connectAttrs always come after setAttrs.
So when setInternalValue gets called for the controlPoints
attribute and you want to have different handling for history vs. non-history,
you can’t simply look for the connection to see if the shape has history or
not. By implementing setInternalValue and getInternalValue
for “mHasHistoryOnCreate” we are able to know during file IO whether there will
be history on our shape.</p>
<p>Because there
are several places in our node where we want to know whether or not there is
history, we also add overrides of the virtual methods</p>
<ul>
<li>connectionMade</li>
<li>connectionBroken</li>
</ul>
<p>This allows
us to set a boolean flag indicating whether or not there is history. This way
of storing whether there is history is optional, but checking this flag is
quite a bit more efficient than checking for input connections to the
inputSurface attribute.</p>
<h4>Summary</h4>
<p>Until now,
all of our previous examples have been fairly cut and dry. If you wanted to
make a user-defined shape with movable components, you basically have to follow
the basic plan outlined in chapters II-V. Where the creativity and design on
your part really starts coming into play is when you start adding support for
history. The implementation presented here is one way to go, and closely mimics
how shapes inside of Maya are implemented.</p>
<h3>Deformer Support – Background Info</h3>
<p>The term
deformer refers to any type of node derived from the base class “geometryFilter”.
Examples of deformers are lattices, clusters, nonlinears, and MPxDeformerNodes.
When you add a deformer to a shape, a number of things happen:</p>
<ul>
<li>The shape is copied. This copy of
     the shape is called the “intermediate object” and is placed in the history
     of the shape. (Subsequent deformers will reuse the existing intermediate
     object. New copies will not be added.)</li>
<li>a set gets created to store the
     components affected by the deformer</li>
<li>a tweak node is created to store
     subsequent tweaks made to the shape</li>
<li>the deformer node and any related
     auxiliary objects are created</li>
</ul>
<p>A typical
dependency graph set-up including deformers is shown below:</p>
<p style="text-align: center;">&#0160;
<a class="asset-img-link" href="http://around-the-corner.typepad.com/.a/6a0163057a21c8970d017d3c2f4491970c-pi" style="display: inline;"><img alt="Deformer_shape" border="0" class="asset  asset-image at-xid-6a0163057a21c8970d017d3c2f4491970c image-full" src="/assets/image_820759.jpg" title="Deformer_shape" /><br /></a><em>Hypergraph view of deformers in
the history of a shape</em></p>
<p>At the far
right, pSphereShape is the deformed shape node. At the far left,
pSphereShapeOrig is the intermediate object. The intermediate object is
typically hidden (unless you disable its intermediateObject attribute), and is
at the head of every chain of deformers. It serves as a storage place for the
original undeformed positions of the shape.</p>
<p>In a typical
deformer chain, shape data flows out of the intermediate object and then flows
through each of the deformers. In this example, the deformers that are
traversed are tweak1, cluster1 and ffd1. In this example, only a single shape
flows through ffd1. However, most deformers can operate on any number of shapes
since the input and output geometry attributes of the deformer base class are
multis. The two exceptions to this rule are skinClusters and the wrap node.
Because of implementation details, the skinCluster and the wrap node can
operate on only a single geometry. Additional geometries connected to the
skinCluster and the wrap will be ignored.</p>
<h4>Sets on shapes with history</h4>
<p>Each deformer
is associated with a single set. It will deform only the points that are
included in its set. If you add points to a deformers using the sets command,
the deformer will automatically be wired up to the construction history of the
shape. Similarly, if you remove points from a set, the deformer will stop
acting upon those points. </p>
<p>Most sets in
Maya are composed of only a single node: the objectSet node. However, component
sets in Maya are different. Component sets are stored within the shape data
rather than within the objectSet node. The set is connected to a groupId node
per shape, and a groupParts node per &quot;construction-historied&quot;
shape.&#0160; If you look at the history of a
shape in the hypergraph, by default Maya will hide the groupParts and the groupId
because they clutter the view and are not of interest. If you turn on display
of &quot;auxiliary nodes&quot;, the groupParts and groupId will be drawn.</p>
<p style="text-align: center;">
<a class="asset-img-link" href="http://around-the-corner.typepad.com/.a/6a0163057a21c8970d017d3c2f45ae970c-pi" style="display: inline;"><img alt="Deformer_dg_group" border="0" class="asset  asset-image at-xid-6a0163057a21c8970d017d3c2f45ae970c image-full" src="/assets/image_f719ae.jpg" title="Deformer_dg_group" /><br /></a><em>Hypergraph view showing the groupParts, groupId and objectSet node for an ffd
deformer. Note that the tweak node was deleted from this shape&#39;s history in order
to make the figure less cluttered.</em></p>
<p>The groupId
node is simply a means of storing an id that is unique within the scene for
fast look-up of the membership of the set within the shape&#39;s list of sets.</p>
<p>The
groupParts node stores component sets for shapes with construction history.
This is because as the shape data flows through the history, it may change its
numbering. Component lists are stored using the vertex index and some
construction history nodes modify the vertex numbering - for example, &quot;polySplit&quot;,
&quot;deleteComponent&quot;, etc. Because the groupParts is in the construction
history, it remains immune to the vertex numbering changes. It receives the
shape data before the vertices are renumbered. This architecture has proved
useful for certain poly modeling operations.</p>
<h4>Tweak Nodes</h4>
<p>A tweak is
the name given to translations of vertices on shapes in Maya. Shapes without
construction history are able to add tweak deltas directly into their vertex
position data. However, shapes with construction history need to store the
tweaks as deltas that get added to the shape data coming in through the history
attribute.</p>
<p>When a
deformer is added to a shape in Maya, we do lot of work to prevent tweaks from
existing on the shape. Why? In the previous chapter, we discussed how shapes
store tweaks as relative moves which get added to the history. The example
below shows how such a relative tweak would get added into a smooth skinned
surface.</p>
<p style="text-align: center;">&#0160;&#0160;<a class="asset-img-link" href="http://around-the-corner.typepad.com/.a/6a0163057a21c8970d017d3c2f4629970c-pi" style="display: inline;"><img alt="Tweak_node" border="0" class="asset  asset-image at-xid-6a0163057a21c8970d017d3c2f4629970c image-full" src="/assets/image_213a57.jpg" title="Tweak_node" /><br /></a><em><strong>Left:</strong> Salty the seal&#39;s
initial state.</em><br /><em><strong>Middle:</strong> Salty&#39;s nose was
tweaked (think Pinocchio).</em><br /><em><strong>Right:</strong> The neck joint is
rotated, causing the skin to deform. The tweak does not get deformed since is
added after the deformation.</em></p>
<p>In the above
picture (left), the user has skinned the seal shape to a skeleton so that
rotating the various joints creates a smooth skinning deformation. In a later
plot twist, Salty is beset by a strange Pinocchio-like spell. The user needed
to make Salty&#39;s nose grow, and accomplished this by translating a few vertices
over in X. For this example, there is no tweak node, so the vertex deltas are
stored on the shape node. It all looks ok until we rotate the neck. Now since
the tweaks get added after the skinning deformation, the deltas move the nose
vertices over in X even though Salty&#39;s nose is no longer aligned with the
X-axis.&#0160; In order for Salty to rotate his
head successfully, we need the skinning algorithm to act on Salty&#39;s tweaked
nose, instead of having the tweaks act on Salty&#39;s skinned nose.</p>
<p>To address
this issue, whenever there are deformers in the history of a shape, a tweak
deformer is created automatically and inserted at the front of the deformer
chain. If the shape being deformed had existing tweaks or animated CVs, the
tweaks &amp; animation are moved onto the tweak node. Otherwise, the tweak node
is left empty until the user grabs some cvs and moves them (or keys them). New
tweaks are also added to the tweak node (unless the user manually deletes it,
in which case we revert to the old behavior).</p>
<h3>Deformer Support – Implementation</h3>
<p>In this
section, we’ll present the changes necessary to upgrade our example plug-in so
that it will support deformers. The example code referred to here is in the
directory called <strong><em>compCurveDeformable</em></strong>. To try out this shape, use
the MEL script compCurveDefSetup to create this shape. Then add some deformers
to your shape, and move some vertices around.</p>
<h4>Geometry Data</h4>
<p>In order to
support either sets or deformers, it is necessary for the shape-related
attributes on the shape to be geometry data. You could use one of Maya’s
internal geometry data types, such as mesh data. Or you can write your own type
of geometry data derived from MPxGeometryData. For the purposes of this
example, we created our own data type, compCurveData. The data itself contains
a simple MPointArray. When implementing your own type of data, you need to
implement a number of virtual methods to support file io, geometry iteration
over the data, copy, and set creation. </p>
<p>Inside our
example plug-in, you will see that we have gotten rid of all of our internal
data (fInputPoints and fCurvePoints). We are now storing all of our data in the
datablock in the geometry attributes described next.</p>
<p>The change
from storing the data internally as a class member to storing it in a data type
required a lot of small changes to the code. Basically every place where we
were using fInputPoints, we now use the point array that is stored in the data
for the input shape. Every place where we were using fCurvePoints, we now use
the point array stored in the data for the cached shape.</p>
<h4>Geometry Attributes</h4>
<p>Any
deformable geometry requires at a minimum the following three attributes:</p>
<ul>
<li>input shape</li>
<li>cached shape</li>
<li>local output shape</li>
<li>world output shape</li>
</ul>
<p>The input
shape is where history gets connected into your node, as in the previous
example. The cached shape is the input shape plus the tweaks – generally
treated to be your node’s private storage place. The local and world output
shape attributes give other nodes the ability to connect downstream from your
shape’s data. The world output shape simply bundles the world matrix of the
shape with the local shape data. The shape data itself is still in local space.</p>
<h4>Geometry Attribute Overrides</h4>
<p>4 virtual
methods should be implemented to return the attributes corresponding to the
above 4 attributes:</p>
<ul>
<li>localShapeInAttr</li>
<li>cachedAttr</li>
<li>localShapeOutAttr</li>
<li>worldShapeOutAttr</li>
</ul>
<p>Of these
attributes, 2 are outputs and required us to add to our existing override of
the compute method to handle our 2 new outputs.</p>
<p>These
attributes are used in commands that want to know about history/future shape
attributes on your node. For example, the listHistory command will utilize
these methods to find the history or future (listHistory –future) of your node.
When a deformer is attached to a shape, it connects to the attribute returned
by the worldShapeOutAttr of the intermediate object and to the
localShapeInAttr of the deformed object.</p>
<h4>Overrides required for set support</h4>
<p>Three methods
must be added to our shape to support the creation of sets of our components:</p>
<ul>
<li>match: This method is used by the set creation code to
     determine whether the selected component type is valid for inclusion in a
     vertex-restricted sets. Since deformer sets must be vertex-restricted,
     your shape must override match to specify that its control point
     components satisfy the vertex restriction.</li>
<li>createFullVertexGroup: This method is used by the set
     creation code when it wants to create a set containing all of the control
     points in the entire shape. An example of when this would occur is if you
     select your shape as an object (rather than as in component-mode), and
     create a deformer. The deformer’s set would contain all of the vertex
     components of your shape as defined by the createFullVertexGroup method
     implementation.</li>
<li>geometryData: This method is used by the set
     creation and editing code to access the geometry data. Set information for
     components is stored with the geometryData. </li>
</ul>
<h4>Tweak node support</h4>
<p>You must
override the tweakUsing method to support movement of
controlPoints on shapes with tweak nodes in their history. The tweakUsing method is fairly similar to the transformUsing method, but it allows the tweaks to be transferred to
the tweak node rather than directly into the shape data itself.</p>
<h3>Conclusion</h3>
<p>Writing a
custom shapes is not trivial. However, if you start simple, and then add more
details, it’s not so difficult. We recomment that you build up your custom
shape piece by piece and get each part of it solid before you proceed to the
next. Hopefully the example plug-ins and material presented in this article will
provide a good starting point for this process.</p>
