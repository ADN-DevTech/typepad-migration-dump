---
layout: "post"
title: "Maya Custom  Manipulators "
date: "2012-10-21 23:03:00"
author: "Cyrille Fauvel"
categories:
  - "C++"
  - "Cyrille Fauvel"
  - "Maya"
  - "Plug-in"
  - "Samples"
original_url: "https://around-the-corner.typepad.com/adn/2012/10/maya-custom-manipulators-.html "
typepad_basename: "maya-custom-manipulators-"
typepad_status: "Publish"
---

<p><a class="asset-img-link" style="display: inline;" href="http://around-the-corner.typepad.com/.a/6a0163057a21c8970d017d3cbd4d63970c-pi"><img class="asset  asset-image at-xid-6a0163057a21c8970d017d3cbd4d63970c" style="display: block; margin-left: auto; margin-right: auto;" title="Swiss-tool" src="/assets/image_3d0b89.jpg" alt="Swiss-tool" width="250" /></a></p>
<h1>I. General Introduction</h1>
<p>A manipulator is a node that draws itself using 3D graphical
elements that respond to user events. While it is possible to change a node’s attribute values using Channel
Box, Graph Editor and etc., manipulators provides a more intuitive way of changing
attribute values: Manipulators provide a series of visual controls that users can
interactively move or adjust. The modification of these visual controls subsequently
is translated by manipulators into value changes and then applied to the nodes
attributes. The attribute values are modified directly by the manipulator and
not through the standard plug and connection mechanism used by other dependency
graph nodes.</p>
<p>Using the
manipulator, it is easier than typing modified values in the editors such as
Attribute Editor, Channel Box and etc. Users can see the immediate visual
feedback during operations. Therefore, manipulators quite often are used as an
interactive tool for animators to manipulate a specific object or custom nodes
in a scene. With custom manipulators, you will be able to create all types of
interactive custom tools (modeling tool, rigging tool, etc…) for animators in
different stages of animation production pipeline.</p>
<p>There are
several ways to create and activate manipulators: First, you can create a manipulator
on any node in the current scene at any time. You can also create and assign a
manipulator for a specific type of object. Another approach is to create a
context tool and associate the manipulator to the context tool so that whenever
it is active, the manipulator is active and ready to use. Manipulators are only
active when the “Show Manipulator Tool” or the associated context is active,
and the object that they correspond to is selected. For more information, see
Section 5 “Register Manipulators”.</p>
<p>When
manipulators are created, even though they are nodes, they are not visible in
the Hypergraph or Outliner, and they are not added to the Maya selection list.
Additionally, their attributes are not accessible from MEL or the attribute
editor, and they are not written to file. </p>
<p>Manipulators
are designed to operate on data types, ranging from integer and floating point
values to matrix data and can operate on one or more attribute values at the
same time.&nbsp;</p>
<h1>II. Maya Base Manipulators</h1>
<p>Maya defines a set of simple manipulators,
called <em>base manipulators</em>. These operate on a range of data types from a
single boolean, integer, or floating point value, to vectors of floating point
values of different lengths. The OpenMaya API
supports 12 base manipulator classes that can be combined to form a composite
manipulator. All the function set classes for these Maya base manipulators are
derived from MFnManip3D.</p>
<h2>FreePointTriadManip </h2>
<p>
<a class="asset-img-link" style="display: inline;" href="http://around-the-corner.typepad.com/.a/6a0163057a21c8970d017ee4329bd3970d-pi"><img class="asset  asset-image at-xid-6a0163057a21c8970d017ee4329bd3970d" title="Triadmanip" src="/assets/image_f7f1d8.jpg" border="0" alt="Triadmanip" width="120" /></a><br />The
FreePointTriadManip provides a moveable point that can be moved anywhere. It
has axes for constrained x, y, and z movement and obeys grid snapping, point
snapping, and curve snapping. The FreePointTriadManip generates the 3D position
of the point. It is useful for specifying the position of an object in space.</p>
<p>One thing to note is that the
FreePointTriadManip is not corresponding to Maya’s internal moveManip, and it
is actually a subset of the moveManip. </p>
<p>The
API class for FreePointTriadManip is MFnFreePointTriadManip. This function set
class can be used to create the manipulator and set up some properties of this
manipulator. For example, MFnFreePointTriadManip::setDrawAxes() can be used to
set whether or not to draw the axes of the FreePointTriadManip.
MFnFreePointTriadManip::setSnapMode() can be used to set whether or not the
FreePointTriadManip should be in snap mode etc...</p>
<h2>RotateManip </h2>
<p>
<a class="asset-img-link" style="display: inline;" href="http://around-the-corner.typepad.com/.a/6a0163057a21c8970d017c328eb97d970b-pi"><img class="asset  asset-image at-xid-6a0163057a21c8970d017c328eb97d970b" title="Rotatemanip" src="/assets/image_d4f034.jpg" border="0" alt="Rotatemanip" width="120" /></a><br />The RotateManip corresponds
to Maya built-in rotate manipulator, and it allows you to manipulate a 3d
rotation vector. &nbsp;The manipulator
consists of three constrained-axis rotation rings, a view rotation ring, and an
invisible trackball that allows the user to rotate in arbitrary directions on
the sphere. It supports the 3 rotation modes of the built-in rotate manipulator
(object space, global, gimbal) and allows constrained rotation on the x, y, z
and viewing axes. The vector generated by the manipulator is an Euler rotation
that is suitable for input to a rotation plug. &nbsp;</p>
<p>The API class for
RotateManip is MFnRotateManip. Simliar to MFnFreePointTriadManip, it provides
functions to create the manipulator and set up properties.&nbsp;</p>
<h2>ScaleManip </h2>
<p>
<a class="asset-img-link" style="display: inline;" href="http://around-the-corner.typepad.com/.a/6a0163057a21c8970d017c328f1094970b-pi"><img class="asset  asset-image at-xid-6a0163057a21c8970d017c328f1094970b" title="Scalemanip" src="/assets/image_d3c8e8.jpg" border="0" alt="Scalemanip" width="120" /></a><br />The ScaleManip
corresponds to Maya built-in scale manipulator and lets you manipulate relative
x, y, and z scaling values. The scale manipulator provides a central handle for
proportional scaling, and x, y, and z axis handles for non-proportional scaling
on each axis. The vector generated by the manipulator is a relative scaling
vector that is suitable for input to a scale plug.</p>
<p>The API class for
scaleManip is MFnScaleManip. Simliar to MFnFreePointTriadManip, it provides
functions to create the manipulator and set up properties.&nbsp;</p>
<h2>DirectionManip</h2>
<p>
<a class="asset-img-link" style="display: inline;" href="http://around-the-corner.typepad.com/.a/6a0163057a21c8970d017ee432f5d8970d-pi"><img class="asset  asset-image at-xid-6a0163057a21c8970d017ee432f5d8970d" title="Dirmanip" src="/assets/image_6a6c95.jpg" border="0" alt="Dirmanip" width="120" /></a><br />The DirectionManip lets
you specify a direction as defined by the vector from the start point to the
manipulator position. It uses a FreePointTriadManip to specify the end point of
a vector relative to a given start point. This manipulator generates a vector
from the start point to the end point.</p>
<p>The API class for DirectionManip
is MFnDirectionManip. Simliar to MFnFreePointTriadManip, it provides functions
to create the manipulator and set up properties of the manipulator.</p>
<h2><strong>DistanceManip</strong></h2>
<p>
<a class="asset-img-link" style="display: inline;" href="http://around-the-corner.typepad.com/.a/6a0163057a21c8970d017c328f11ff970b-pi"><img class="asset  asset-image at-xid-6a0163057a21c8970d017c328f11ff970b" title="Distmanip" src="/assets/image_4e1cd4.jpg" border="0" alt="Distmanip" width="120" /></a><br />The DistanceManip lets
you manipulate a point that is constrained to move along a line. The distance
value is calculated from the start point of the line to the manipulated point.
This manipulator generates a single floating point value. Scaling factors can
be used to determine how long the manipulator appears when it is drawn. The API
class for DistanceManip is MFnDistanceManip.</p>
<h2>PointOnCurveManip </h2>
<p>The PointOnCurveManip
lets you manipulate a point constrained to move along a curve, in order to
specify the "u" curve parameter value. This manipulator generates a
single floating point value corresponding to the curve parameter. The API class
for PointOnCurveManip is MFnPointOnCurveManip.</p>
<h2>PointOnSurfaceManip</h2>
<p>
<a class="asset-img-link" style="display: inline;" href="http://around-the-corner.typepad.com/.a/6a0163057a21c8970d017d3cbdaa1f970c-pi"><img class="asset  asset-image at-xid-6a0163057a21c8970d017d3cbdaa1f970c" title="Pointonsurf" src="/assets/image_b1f6cf.jpg" border="0" alt="Pointonsurf" width="120" /></a><br />The PointOnSurfaceManip
lets you manipulate a point constrained to move along a surface, in order to
specify the (u, v) surface parameter values. This manipulator generates two
floating point values corresponding to the surface (u, v) parameters. The API
class for PointOnSurfaceManip is MFnPointOnSurfaceManip.</p>
<h2>DiscManip</h2>
<p>
<a class="asset-img-link" style="display: inline;" href="http://around-the-corner.typepad.com/.a/6a0163057a21c8970d017c328f1417970b-pi"><img class="asset  asset-image at-xid-6a0163057a21c8970d017c328f1417970b" title="Discmanip" src="/assets/image_8d5d89.jpg" border="0" alt="Discmanip" width="120" /></a><br />The DiscManip lets you
rotate a disc in order to specify a rotation about an axis. This manipulator
generates a single floating point value corresponding to the rotation. The API
class for DiscManip is MFnDiscManip.</p>
<h2>CircleSweepManip</h2>
<p>
<a class="asset-img-link" style="display: inline;" href="http://around-the-corner.typepad.com/.a/6a0163057a21c8970d017c328f16ef970b-pi"><img class="asset  asset-image at-xid-6a0163057a21c8970d017c328f16ef970b" title="Circlesweep" src="/assets/image_3cf616.jpg" border="0" alt="Circlesweep" width="120" /></a><br />The CircleSweepManip
lets you manipulate a point constrained to move around a circle, in order to
specify a sweep angle. This manipulator generates a single floating point value
corresponding to the sweep angle. Although very similar to DiscManip, it
provides more options for specifying rotating angles. The API class for CircleSweepManip
is MFnCircleSweepManip.</p>
<h2>ToggleManip</h2>
<p>
<a class="asset-img-link" style="display: inline;" href="http://around-the-corner.typepad.com/.a/6a0163057a21c8970d017d3cbdae58970c-pi"><img class="asset  asset-image at-xid-6a0163057a21c8970d017d3cbdae58970c" title="Togglemanip" src="/assets/image_66ac38.jpg" border="0" alt="Togglemanip" width="120" /></a><br />The ToggleManip lets
you switch between two modes or some on/off state. It is drawn as a circle with
or without a dot. When the mode is on, the dot is drawn in the circle and when
the mode is off, the circle is drawn without the dot. This manipulator
generates a boolean value corresponding to whether or not the mode is on or
off. The API class for ToggleManip is MFnToggleManip.</p>
<h2>StateManip</h2>
<p>
<a class="asset-img-link" style="display: inline;" href="http://around-the-corner.typepad.com/.a/6a0163057a21c8970d017d3cbdb6d6970c-pi"><img class="asset  asset-image at-xid-6a0163057a21c8970d017d3cbdb6d6970c" title="Statemanip" src="/assets/image_df625e.jpg" border="0" alt="Statemanip" /></a><br />The StateManip lets you
switch between multiple states. It is drawn as a circle with a notch. Each
click on the circle increments the value of the state (modulo the maximum
number of states). This manipulator generates an integer value corresponding to
the state of the manipulator. The API class for StateManip is MFnStateManip.</p>
<h2>CurveSegmentManip </h2>
<p>The CurveSegmentManip
lets you manipulate two points on a curve to specify a curve segment. This
manipulator generates two floating point values, which correspond to the
parameters of the start and end of the curve segment. The API class for CurveSegmentManip
is MFnCurveSegmentManip.</p>
<h1>III. Custom Manipulators</h1>
<p>There are several ways to work with manipulators, you could
either create a base manipulator
on any node in the current scene on the fly, or you could also create a
manipulator container and compose a custom manipulator by adding Maya base
manipulators into the container, furthermore, you can also create a brand new
type of manipulator node by using MPxManipulatorNode class and defining custom
drawing and picking of the manipulator node. Before we jump into more advanced topics
of creating a manipulator with MPxManipulatorNode, let’s first examine how to
work with Maya base manipulators to create the simplest manipulator on a node.</p>
<h2>a. Working with Maya Base Manipulators</h2>
<p>In the “Maya Base Manipulators”
section, we listed all the function set classes for Maya base manipulators. The
function set classes are responsible to create the base manipulators with corresponding
create() functions, and also set up some properties of the base manipulators depending
on their individual types. Most importantly, the function set classes are used
to set up relationships between values of visual controls on the manipulators
and values of the attributes (plugs) on nodes. In other words, these base
manipulator function set classes are responsible to communicate with attribute
(plugs) on nodes to set their values based on visual control values on
manipulator and also to set manipulator visual control values appropriately
with respect to the values of the attributes (plugs). In the following section,
we will take a closer look at how this communication works. We will use MFnDistanceManip
as an example.</p>
<h3>i. Communication between
Manipulators and Plugs on Nodes</h3>
<p>The communication between manipulators
and nodes can be done in one of two ways: simple one-to-one associations, or
through the use of more complex conversion functions. </p>
<p>The properties of the visual controls on a manipulator are
represented by float values or other types of data values, and these properties
are all registered on the manipulator with unique identifiers. The values are
called manip values, and the unique identifiers are manip indices. Some of the values
define the key property relate directly to an affordance of the manipulator.
For example, the MFnDistanceManip::distanceIndex() returns the index of the
manipulator property that relates directly to the distance of a DiscManip. &nbsp;Other values do not relate to an affordance of
the manipulator, but provide important information on the position or
orientation of the manipulator such as MFnDistanceManip::startPointIndex() and MFnDistanceManip::directionIndex().</p>
<p>
<a class="asset-img-link" style="display: inline;" href="http://around-the-corner.typepad.com/.a/6a0163057a21c8970d017c328f25c9970b-pi"><img class="asset  asset-image at-xid-6a0163057a21c8970d017c328f25c9970b image-full" style="display: block; margin-left: auto; margin-right: auto;" title="Manipplugs" src="/assets/image_8bed19.jpg" border="0" alt="Manipplugs" /></a><br /><strong>One-to-One Mapping</strong></p>
<p>As the name indicates, one-to-one mapping basically synchronizes
manip values with attribute/plug values directly. In all the function set classes
for Maya base manipulators, there is always one function that has some form of
“connectToXXXPlug”. This is the function to establish the one-to-one mapping
relationship between manip values and attribute/plug values. In other words,
the manip value equals to the attribute/plug value, and vice versa.</p>
<p>In the case of MFnDistanceManip, the function
MFnDistanceManip::connectToDistancePlug() sets up the one-to-one relationship
between the manip value of distance manip and the corresponding “distance” attribute/plug
on a node. The following code in devkit project footPrintManip demonstrates a
good example of using this one-to-one mapping.</p>
<pre class="brush:cpp; toolbar:false;">MFnDistanceManip distanceManipFn (fDistanceManip) ;
MFnDependencyNode nodeFn (node) ;
MPlug sizePlug =nodeFn.findPlug ("size", &amp;stat) ;
if ( stat != MStatus::kFailure ) {
	distanceManipFn.connectToDistancePlug (sizePlug) ;
	…
}
</pre>
<p>The
following graph demonstrates the one-to-one mapping relationship between the
distance manip value and the value of “size” plug on a node.</p>
<p>
<a class="asset-img-link" style="display: inline;" href="http://around-the-corner.typepad.com/.a/6a0163057a21c8970d017d3cbdc015970c-pi"><img class="asset  asset-image at-xid-6a0163057a21c8970d017d3cbdc015970c image-full" style="display: block; margin-left: auto; margin-right: auto;" title="Onetoone" src="/assets/image_c595f3.jpg" border="0" alt="Onetoone" /></a><br />After you set up the one-to-one mapping relationship,
whenever the manip value changes, the corresponding plug/attribute value
changes with it and vice versa.</p>
<p>One-to-one mapping is a very straightforward and convenient
approach to set up relationship between manip values and attribute/plug values,
however it may not be acceptable for more complex requirements. For example, in the above case what if you want to set
up the manip value to be 5 * the value of plug “size”? There is no way you can
set this up with one-to-one mapping. Also, when you move the node, the distance
manipulator will appear not showing up at the center of the node, and there is
no way to make the manipulator move along with the position of the node. In
situations where the position of a manipulator needs to be affected by the
position of an object, or a group of manipulators need to move together in a
specific way; a new approach (conversion functions) needs to be implemented.</p>
<p><strong>Conversion Functions</strong></p>
<p>Conversion functions are used to set up more
complex relationship between manip values and attribute/plug values. As the
name indicates, they are responsible to convert manip values to attribute/plug
values, and vice versa. In C++, they are implemented as callback methods. </p>
<p>There are two
types of conversion callback functions:&nbsp;</p>
<p><strong>plugToManip conversion</strong></p>
<p>A plugToManip conversion callback is used to convert
attribute/plug values to corresponding manip values. The implementation of
plugToManip conversion is achieved by calling MPxManipContainer::addPlugToManipConversionCallback().
Using manipulator container is the recommended approach to create a custom
manipulator from May base manipulators. In the following section “Manipulator
Containers”, all aspects of using MPxManipContainer class will be discussed in
detail.</p>
<p>As discussed earlier, every property of visual
controls has a corresponding index registered within the manipulator. In order
to set up plugToManip conversion relationship, when MPxManipContainer::addPlugToManipConversionCallback()
is called, the index of the manip value needs to be passed in. In every base
manipulator function set class, there are functions to retrieve different
indices for individual properties.</p>
<p>The following code in devkit project footPrintManip
illustrates how to retrieve index of the starting point of the manipulator and
pass it into addPlugToManipConversionCallback function call. The second
parameter of this function call is the callback conversion function, which is
responsible to calculate the manip value of this starting point based on some
attribute/plug values.&nbsp;</p>
<pre class="brush:cpp; toolbar:false;">unsigned startPointIndex =distanceManipFn.startPointIndex () ;
addPlugToManipConversionCallback (startPointIndex, (plugToManipConversionCallback)&amp;footPrintLocatorManip::startPointCallback) ;
</pre>
<p>The plugToManip conversion callback is responsible to
access attribute/plug values, calculate manip values based on custom algorithm
and return them. In this specific case, the callback function doesn’t request
attribute/plug values, instead, it retrieves the node translation in world
space and return this value to Maya. By doing so, it actually sets the start
point manip value to be the same as the node translation. The outcome of this
setting is that whenever the node moves, the manipulator moves along with it
and always appears in the center of the node.</p>
<pre class="brush:cpp; toolbar:false;">MManipData footPrintLocatorManip::startPointCallback (unsigned index) const {
	MFnNumericData numData ;
	MObject numDataObj =numData.create (MFnNumericData::k3Double) ;
	MVector vec =nodeTranslation () ;
	numData.setData (vec.x, vec.y, vec.z) ;
	return (MManipData (numDataObj)) ;
}
</pre>
<p><a class="asset-img-link" style="display: inline;" href="http://around-the-corner.typepad.com/.a/6a0163057a21c8970d017ee43312c0970d-pi"><img class="asset  asset-image at-xid-6a0163057a21c8970d017ee43312c0970d image-full" style="display: block; margin-left: auto; margin-right: auto;" title="DistanceManip" src="/assets/image_6ba5bf.jpg" border="0" alt="DistanceManip" /></a></p>
<p>One thing to note is that the conversion callback returns
a data type called MManipData. By returning this to Maya, the conversion
callback sets the manip value to what the MManipData represents. MManipData can
represent data that is either simple or complex. When it comes to complex data
types (such as such as matrices, curves, and arrays of data), MFnData or its
derived classes need to be used to create the data.</p>
<p><strong>manipToPlug conversion</strong></p>
<p>A manipToPlug conversion
callback is used to convert manip values to attribute/plug values. The
implementation of manipToPlug conversion is achieved by calling MPxManipContainer::addManipToPlugConversionCallback().The
corresponding plug needs to be passed in as the first parameter because the
callback needs to set attribute/plug values, it needs to know which
attribute/plug values to change.</p>
<p>In general, manipToPlug conversions are less
commonly used and there are several examples in the devkit. The following code
from rotateManip example project demonstrates using this technique with the
node’s “rotate” plug passed in.</p>
<pre class="brush:cpp; toolbar:false;">MFnDependencyNode nodeFn (node) ;
MPlug rPlug =nodeFn.findPlug ("rotate", &amp;stat) ;
…
rotatePlugIndex =addManipToPlugConversionCallback (rPlug, (manipToPlugConversionCallback)&amp;exampleRotateManip::rotationChangedCallback) ;
</pre>
<p>The addManipToPlugConversionCallback function call returns
the index (“rotatePlugIndex”) to identify which plug has been registered with
this callback. When the actual callback function is invoked, the index of the
plug value to be calculated will be passed in as the parameter of the function
call. The callback function then needs to distinguish if the passed-in value is
the index that is registered for this callback. If there is more than one plug
registered with this callback, a condition statement can be used to distinguish
different passed-in plug index and calculate corresponding manip values. &nbsp;In the rotationChangedCallback() below,&nbsp; the passed-in “index” is compared with
“rotatePlugIndex”. If they are equal, getConverterManipValue() is called with rotate
manip index to retrieve rotate manip value. By returning the rotate manip value
to Maya as a MManipData object, this callback function is setting the rotate
plug value to be the same as the rotate manip value. Depending on your specific
requirement, more complex value relationship between rotate plug value and
rotate manip value can be set up with this conversion callback function.</p>
<pre class="brush:cpp; toolbar:false;">MManipData exampleRotateManip::rotationChangedCallback (unsigned index) {
	MObject obj =MObject::kNullObj ;
	// If we entered the callback with an invalid index, print an error and
	// return.  Since we registered the callback only for one plug, all 
	// invocations of the callback should be for that plug.
	if ( index != rotatePlugIndex ) {
		MGlobal::displayError ("Invalid index in rotation changed callback!") ;
		// For invalid indices, return vector of 0's
		MFnNumericData numericData ;
		obj =numericData.create (MFnNumericData::k3Double) ;
		numericData.setData (0.0, 0.0, 0.0) ;
		return (obj);
	}
	MFnNumericData numericData ;
	obj =numericData.create (MFnNumericData::k3Double) ;

	MEulerRotation manipRotation ;
	if ( !getConverterManipValue (rotateManip.rotationIndex (), manipRotation) ) {
		MGlobal::displayError ("Error retrieving manip value") ;
		numericData.setData (0.0, 0.0, 0.0) ;
	} else {
		numericData.setData (manipRotation.x, manipRotation.y, manipRotation.z) ;
	}
	return MManipData (obj) ;
}
</pre>
<p><a class="asset-img-link" style="display: inline;" href="http://around-the-corner.typepad.com/.a/6a0163057a21c8970d017ee433177e970d-pi"><img class="asset  asset-image at-xid-6a0163057a21c8970d017ee433177e970d image-full" style="display: block; margin-left: auto; margin-right: auto;" title="RotateManip" src="/assets/image_636eda.jpg" border="0" alt="RotateManip" /></a></p>
<p>For more complete and in-depth explanation, you can go to
Maya API documentationà“API
Guide”à
“Manipulators”à
“Communication between manipulators and nodes”.</p>
<p>
Tips:</p>
<ol>
<li>C++
Conversion Functions vs. Python Conversion Functions<br />When looking at the MPxManipContainer class
documentation, people usually get confused by the many conversion and
conversion callback function names.&nbsp; Here
is a clarification: If you are writing python plug-in,
addManipToPlugConversion() and addPlugToManipConversion() (that are
specifically designed for writing python plug-ins) are the only functions to
register conversion callback functions. If you are writing C++ plug-ins, you
can either use addPlugToManipConversionCallback()/addManipToPlugConversionCallback()
functions or addManipToPlugConversion()/addPlugToManipConversion() functions to
register conversion callback functions.</li>
<li>Manip
index vs. plug index<br />Properties of visual controls have individual
index registered within the manipulator. For example, MFnDistanceManip::startPointIndex()
returns the index of the start point of the DistanceManip, MFnRotateManip::rotationIndex()
returns the index of rotation manip Value of the manipulator, etc. In order to
specify which manip value is set up with plugToManip conversion callback, manip
index needs to used when addPlugToManipConversionCallback() or addPlugToManipConversion()
is called.<br /><br />There is another concept of index called
plug index that is returned by addManipToPlugConversionCallback() and
addManipToPlugConversion() to identify which plug has been registered with
manipToPlug conversion callback. &nbsp;This
index usually is saved as a member variable and whenever the callback function
is invoked, it is used to compare with the requested-value plug index to decide
what values need to return for the requested-value plug.<br /><br />Manip indexes also are used to retrieve manip
values from getConverterManipValue() function call, and plug indexes are used
to retrieve plug values from getConverterPlugValue() function call.</li>
</ol>
<h2>b. Manipulator Containers</h2>
<p>It is recommended to create a manipulator
container and add one or more base manipulators to the container to compose a
new type of manipulator. It is also the most common way to create a custom
manipulator. This approach involves the following steps:</p>
<p style="padding-left: 30px;">i. create a custom manip container class derived from MPxManipContainer</p>
<p style="padding-left: 30px;">ii. &nbsp;add base manipulators to the
manip container class</p>
<p style="padding-left: 30px;">iii. define associations between the manipulator and the attribute on
the nodes they affect</p>
<p>Let’s examine these steps in detail:</p>
<h3>i. Create a custom
manip container class derived from MPxManipContainer</h3>
<p>In order to create
a custom manipulator you need to derive from MPxManipContainer and implement
some common functions of MPxNode such as the create() and initialize() functions.
Also when registering this custom node with MFnPlugin::registerNode in
initializePlugin() function, MPxNode::kManipContainer needs to used.</p>
<h3>ii. Add base
manipulators to the manip container class</h3>
<p>Maya base manipulators are added into custom manipulator
container classes in the MPxManipContainer::createChildren() function. This
method is usually called by Maya after this custom manip container class is set
up. MPxManipContainer class provides a set of member functions to add
individual Maya base manipulators, most of them named like this “addXXXManip”
with XXX being the manip name. The function returns an MDagPath object
representing the created base manipulator, which you can then use the corresponding
function set class to operate with later. &nbsp;Here is an example in the devkit project
footPrintManip adding distance manip into a custom manip container:</p>
<pre class="brush:cpp; toolbar:false;">MStatus footPrintLocatorManip::createChildren () {
	MStatus stat =MStatus::kSuccess ;

	MString manipName ("distanceManip") ;
	MString distanceName ("distance") ;

	MPoint startPoint (0.0, 0.0, 0.0) ;
	MVector direction (0.0, 1.0, 0.0) ;
	fDistanceManip =addDistanceManip (manipName,distanceName) ;

	MFnDistanceManip distanceManipFn (fDistanceManip) ;
	distanceManipFn.setStartPoint (startPoint) ;
	distanceManipFn.setDirection (direction) ;
	
	return (stat) ;
}
</pre>
<h3>iii. Define
associations between the manipulator and the attribute on the nodes they affect</h3>
<p>As its name indicates,
MPxManipContainer::connectToDependNode() method connects the manipulator to the
dependency node. It is also where the association is made between the
manipulator and the attribute(s)/plug(s) that it will communicate with. All the
operations to set up communication between manipulators and attribute(s)/plugs
(including one-on-one mapping and conversion functions) need to put into this
function call.</p>
<p>After mapping
relationships between manipulators and plugs are set up, there are two
additional methods that need to be called. The methods are
MPxManipContainer::finishAddingManips() and MPxManipContainer::connectToDependNode()
and must be called in that order. Also, MPxManipContainer::finishAddingManips()
must be called after all calls to connect to plugs have been made. MPxManipContainer::finishAddingManips
must be called only once. Devkit sample footPrintManip demonstrates the
following operations: creating a distance manip; applying MFnDistanceManip
function set class on the created distance manip; setting up one-on-one mapping
relationship between plug “size” and distance manip value; and setting up
plugToManip conversion to set up manip start point position.</p>
<pre class="brush:cpp; toolbar:false;">MStatus footPrintLocatorManip::connectToDependNode (const MObject &amp;node) {
	MStatus stat ;

	// Get the DAG path
	MFnDagNode dagNodeFn (node) ;
	dagNodeFn.getPath (fNodePath) ;

	// Connect the plugs
	//
	MFnDistanceManip distanceManipFn (fDistanceManip) ;
	MFnDependencyNode nodeFn (node) ;

	MPlug sizePlug =nodeFn.findPlug ("size", &amp;stat) ;
	if ( stat != MStatus::kFailure ) {
		distanceManipFn.connectToDistancePlug (sizePlug) ;
		unsigned startPointIndex =distanceManipFn.startPointIndex () ;
		addPlugToManipConversionCallback (startPointIndex, (plugToManipConversionCallback)&amp;footPrintLocatorManip::startPointCallback) ;

		finishAddingManips () ;
		MPxManipContainer::connectToDependNode (node) ;
	}
	return (stat) ;
}
</pre>
<h2>c. Custom Drawing of Custom Manipulators</h2>
<p>Constructing a manipulator by creating a manipulator container and
adding one or more base manipulators to it is the most common approach to
create a custom manipulator, and Maya base manipulators provide a great variety
of possibilities of custom manipulator. However there are still situations
where you want to have more customized control over different perspectives of a
manipulator. For example, with the above approach, the shape and drawing of
custom manipulators is limited/restricted to Maya base manipulators because MPxManipContainer::draw()
function only provides a M3dView parameter to add supplementary drawing onto
the manipulator. The custom manipulator’s shape is already decided by the
combination of Maya base manipulator shapes that are added into the current manip
container. Also, you don’t have control over the selection
on the manipulator components and the selecting behavior when user clicks on
the manipulator that is defined by Maya base manipulators internal
implementation.</p>
<p>A new manipulator class MPxManipulatorNode was introduced
with Maya 2009 to provide a new way to implement custom manipulators with
custom OpenGL drawing code. It offers developers with options for selecting
manipulator components (activating different handles) on custom manipulators.
This class can either work alone or work with the MPxManipContainer class.</p>
<h3>i. Custom Drawing </h3>
<p>MPxManipulatorNode::draw() method
is overridden to implement custom drawing of this custom manipulator. In OpenGL,
drawing and picking is done together. This method is also responsible to set up
for selection. Several functions are important to use within this method:</p>
<p>glFirstHandle(): When drawing a OpenGL pickable component, &nbsp;a name uniquely representing the OpenGL
component must be set. This method returns the unsigned int value which
represents the first available OpenGL handle to use. When one OpenGL component
drawing is finished, the int value can be added by 1 to represent the next OpenGL
component.</p>
<p>colorAndName(): This
method is used to set the color of the OpenGL component that is being drawn
next. It is also used to set the OpenGL name of the component so that picking
can be supported. The second parameter “glName” is the OpenGL name that
represents the component you are going to draw in the code next. The last
parameter “colorIndex” presents the color you want to use on the GL
component.&nbsp; In MPxManipulatorNode class,
there are several color methods which return color indexes that Maya use to
allow custom manipulators to have a similar look.&nbsp;</p>
<pre class="brush:cpp; toolbar:false;">void triadScaleManip::draw (M3dView &amp;view, const MDagPath &amp;path, M3dView::DisplayStyle style, M3dView::DisplayStatus status) {
	……

	// Begin the drawing
	view.beginGL () ;

	// Place before you draw the manipulator component that can
	// be pickable.
	MGLuint glPickableItem ;
	glFirstHandle (glPickableItem) ;

	// Top
	topName =glPickableItem ;
	colorAndName (view, glPickableItem, true, mainColor ()) ;
	gGLFT-&gt;glBegin (GL_LINES) ;
	gGLFT-&gt;glVertex3fv (tl) ;
	gGLFT-&gt;glVertex3fv (tr) ;
	gGLFT-&gt;glEnd () ;

	// Right
	glPickableItem++;
	rightName =glPickableItem ;
	colorAndName (view, glPickableItem, true, mainColor ()) ;
	gGLFT-&gt;glBegin (GL_LINES) ;
	gGLFT-&gt;glVertex3fv (tr) ;
	gGLFT-&gt;glVertex3fv (br) ;
	gGLFT-&gt;glEnd () ;

	// ...

	// End the drawing
	view.endGL () ;
}
</pre>
<h3>ii.
Updating Attributes/Plugs Values on Nodes</h3>
There are 2 ways for MPxManipulatorNode to
update attributes/plugs values:<ol>
<li>Update attribute/plugs values directly by
implementing functions related with mouse movement event:<br />In MPxManipulator class there are three functions to use to watch for
mouse movement events:<br />&nbsp; MPxManipulatorNode::doPress (M3dView &amp;view);<br />&nbsp; MPxManipulatorNode::doDrag (M3dView &amp;view);<br />&nbsp; MPxManipulatorNode::doRelease (M3dView &amp;view);<br />These are called when the manipulator receives
a corresponding mouse event including mouse
down, mouse drag and mouse release. Algorithms can be implemented to update
attributes/plugs values when these functions are called. For example, when
doPress() gets called, record mouse position as original position; when
doDrag() gets called, record mouse movement (direction and distance); when
doRelease() gets called, calculate the actual mouse movement values and apply
to node attributes/plugs.</li>
<li>Connect to a dependent node:<br />In order to
connect to a dependent node, the following steps need to be taken:<br />&nbsp; Call add*Value() in the postConstructor() of the manipulator node;<br />&nbsp; Call conectPlugToValue() in connectToDependNode();<br />&nbsp; Call set*Value() in one of the do*() functions;<br />In order to connect attributes/plugs directly to
manipulator values, manipulator values need to be created first on the
manipulator. There are several add*Value() to add manipulator values of
different types. After the manipulator values are created and added onto the
manipulator, one-to-one mapping relationship can be achieved by calling
connectPlugToValue() method within MPxManipulatorNode::connectToDependNode(). In
one of the do*() function, which gets triggered when the manipulator is
receiving corresponding mouse event, calling set*Value() will set the
corresponding manipulator value that will consequently set the attributes/plugs
value which is connected to current manipulator value.</li>
</ol>
<h1>V. Apply Manipulators</h1>
<p>After a
custom manipulator is created, there are two ways to apply the manipulator to
nodes. The manipulator can either be applied to work only on one specific type
of node, or it can be added to a custom context/tool so that it is applicable
to any node.</p>
<h2>a. Apply on one type of node</h2>
<p>In order to make a custom
manipulator work on one specific type of node, there are several steps to set
up the relationship between custom manipulator and node type:</p>
<p>i. The manipulator must be named
after the node type appended with “Manip”: For example, if the node type name
is “myNode”, the custom manipulator name should be “myNodeManip”.</p>
<p>ii. Register the node type in the
manipulator connect table in node class initialization. In the node class initialize()
function, add one function call:</p>
<p>MPxManipContainer::addToManipConnectTable(id);&nbsp; where “id” is the custom node type ID.</p>
<p>After you select the custom node in
the Maya scene and go to “Show Manipulator Tool”, the custom manipulator will show
up on the custom node.&nbsp;</p>
<h2>b. Apply in user-defined context</h2>
<p>Another approach to apply custom
manipulators is adding them into a custom context. Custom contexts are proxy
classes where you can implement interactive tools based on mouse events occurring within an interactive panel in Maya. </p>
<p>In MPxContext class, there are two functions to
work with manipulators: addManipulators() and deleteManipulators(). &nbsp;MPxContext::addManipulators() takes a
parameter of MObject that represents a custom manipulator. MPxManipContainer::newManipulator()
or MPxManipulatorNode::newManipulator() can be used to create a custom
manipulator that is passed into MPxContext::addManipulator() call. </p>
<p>In addition, MPxSelectionContext::toolOnSetup()
and MPxContext::toolOffCleanup() should be overridden so that toolOnSetup adds
a callback to update custom manipulators to work on different objects, and
toolOffCleanup removes the callback when the current tool is inactive.</p>
<h1>VI. Manipulator Examples</h1>
<p>Here is the list of manipulator examples available in the Maya
installation devkit folder:</p>
<p>customAttrManip:</p>
<p>This plug-in
demonstrates how to create user-defined manipulators on custom attributes of
nodes within a user-defined context. This custom manipulator is called customAttrManip
and is composed of three DistanceManips.</p>
<p>customTriadManip:</p>
<p>This plug-in
demonstrates how to create user-defined manipulators from a user-defined
context and apply the manipulator to custom attributes defined on a custom
transform node.&nbsp; The custom transform
node has three custom attributes define, TnoiseX, TnoiseY, and TnoiseZ.&nbsp; Three distance base manips are defined as the
custom manipulator and get attached to the noise attributes when selected. The
attachment of the manipulator is performed by an event callback that is registered
for toolOnSetup and SelectionChanged events.&nbsp;</p>
<p>moveManip:</p>
<p>It shows how
to create a manipulator from a context. The user-defined manipulator in
moveToolManip.cpp is called moveManip and consists of two base manipulators: a
FreePointTriadManip and a DistanceManip. </p>
<p>footPrintManip:</p>
<p>This plug-in example demonstrates how to use the
Show Manipulator Tool with a user-defined node and a user-defined manipulator.
The user-defined manipulator consists of a DistanceManip. It also demonstrates
how to write plugToManip conversion callback functions so that DistanceManip is
always following the location of the foot. </p>
<p>rotateManip:</p>
<p>This plug-in demonstrates the different modes of
the rotate base manipulator. The user-defined manipulator in rotateManip.cpp
consists of a rotate base manipulator and a state base manipulator. The state
manipulator is used to control the mode of the rotate manipulator: object mode,
world space mode, gimbal mode, and object mode with snapping. </p>
<p>componentScaleManip: </p>
<p>This plug-in demonstrates how to use the scale base
manipulator and also demonstrates a method for manipulating components. The
plug-in componentScaleManip.cpp consists of a scale base manipulator. The
manipulator works by attaching manipToPlug conversion callbacks for every
selected vertex. The conversion function computes the new vertex positions
using stored initial vertex positions and the scale manipValue. </p>
<p>surfaceBumpManip:</p>
<p>This plug-in example demonstrates how the
pointOnSurface base manipulator can be used to modify vertices close to the
param manipValue. The plug-in uses a manipToPlug conversion function as a
callback to update vertex positions on the NURBS surface.</p>
<p>swissArmyManip:</p>
<p>This plug-in is an
example of a user-defined manipulator, that is comprised of a variety of the
base manipulators: CircleSweepManip, DirectionManip, DiscManip, DistanceManip,
FreePointTriadManip, StateManip, ToggleManip, RotateManip, ScaleManip.</p>
<p>lineManip:</p>
<p>This example
demonstrates how to use the MPxManipulatorNode class along with a command to
create a user defined manipulator.&nbsp; The
manipulator created is a simple line which is an OpenGL pickable
component.&nbsp; As you move the pickable
component, selected transforms have their scale attribute modified. The line's
movements are restricted in a plane. A corresponding command is used to create
and delete the manipulator node and to support undo/redo etc.</p>
<p>squareScaleManip:</p>
<p>This example demonstrates how to use the
MPxManipulatorNode class along with a command to create a user defined
manipulator.&nbsp; The manipulator created is
a simple square with the 4 sides as OpenGL pickable components.&nbsp; As you move the pickable component, selected
transforms have their scale attribute modified. A corresponding command is used
to create and delete the manipulator node and to support undo/redo etc.</p>
<p>Find <a href="http://around-the-corner.typepad.com/files/triadscalemanip.rar" target="_self">here</a> a conpanion sample for this post.</p>
<p>This article was written by Naiqi Weng who is a member of the ADN team working with me at Autodesk. Naiqi is an expert on the Maya and MotionBuilder API.</p>
