---
layout: "post"
title: "Discussion on Client Graphics"
date: "2012-08-03 19:52:41"
author: "Wayne Brill"
categories:
  - "Inventor"
  - "Parts"
  - "Wayne"
original_url: "https://modthemachine.typepad.com/my_weblog/2012/08/discussion-on-client-graphics.html "
typepad_basename: "discussion-on-client-graphics"
typepad_status: "Publish"
---

<p>My colleague Xiaodong Liang created a technical paper focusing on client graphics. I thought it would be interesting to present this material. Here is the first installment.</p>
<p><strong>Client Graphics – Introduction</strong></p>
<p>Client graphics provide the ability to draw custom graphics in the Inventor® modeling window. Some common uses of client graphics are to represent custom objects and to create interactive previews during commands, displaying the results of various analyses and for creating custom manipulators within the command. An example is a milling application in CAM, where the toolpath is shown using client graphics. Another example is to show the results of a finite element analysis. </p>
<p>The Inventor API allows you to create these graphics (referred to as Client Graphics) using primitives (points, lines, triangles, text, etc). Client Graphics allow the developer to provide visual cues. Inventor uses this feature itself in many scenarios, for instance, when the user creates an extrusion, a preview of the extruded part is displayed. While defining the extrude feature, a visual cue indicates how the final result will display.&#0160; A developer can provide similar visual cues within their own application.</p>
<p><a href="http://modthemachine.typepad.com/.a/6a00e553fcbfc68834017616fcc135970c-pi"><img alt="image" border="0" height="226" src="/assets/image_450971.jpg" style="background-image: none; padding-left: 0px; padding-right: 0px; display: inline; padding-top: 0px; border-width: 0px;" title="image" width="464" /></a></p>
<p>&#0160;</p>
<p><strong>API Object Model:</strong></p>
<p><a href="http://modthemachine.typepad.com/.a/6a00e553fcbfc68834017743e2f82c970d-pi"><img alt="image" border="0" height="336" src="/assets/image_642384.jpg" style="background-image: none; padding-left: 0px; padding-right: 0px; display: inline; padding-top: 0px; border-width: 0px;" title="image" width="462" /></a></p>
<p>&#0160;</p>
<p><strong>Types of Client Graphics: <br /></strong></p>
<p><em><strong>Regular Client Graphics <br /></strong></em>This type of client graphics is associated with a document. They are transient unless associated with a client feature. One document/sheet/drawing view can have more than one client graphics object associated with it. Each client graphics object can have many graphics nodes. Each node can contain any number and any type of graphic primitives. </p>
<p><em><strong>Interaction Client Graphics <br /></strong></em>This type of client graphics is available when using Interaction Events. It is always transient. The InteractionEvents object can have any number of nodes and graphics primitives, like Regular Client Graphics. </p>
<p><strong>Graphics Data:</strong></p>
<p>In Inventor, most client graphics are defined using two different types of objects; graphics data, and graphic primitives. The graphics data defines the low-level information that can be used to define primitives.&#0160; By separating the data from the primitives, the data can be re-used by multiple primitives.</p>
<p><em><strong>Data Objects <br /></strong></em>For Point, Line, LineStrip, Triangle, TriangleStrip, and TriangleFans, the data object provides the list of coordinates that your graphics will be based upon, and other necessary data sets such as the color and normals.</p>
<p><a href="http://modthemachine.typepad.com/.a/6a00e553fcbfc6883401676907ddee970b-pi"><img alt="image" border="0" height="304" src="/assets/image_231052.jpg" style="background-image: none; padding-left: 0px; padding-right: 0px; display: inline; padding-top: 0px; border-width: 0px;" title="image" width="468" /></a></p>
<p><strong>Definitions</strong></p>
<p><strong>Coordinate Sets:</strong> The coordinates used to specify the vertices for the graphic sets. <br /><strong>Color Sets:</strong> A set of colors used by a graphic set. It overrides any other color information assigned to the set. Colors in the color set can be bound to the entire graphic set, each individual primitive in the set (e.g. each triangle in a triangle strip), or each vertex in the set (i.e. interpolated color). <br /><strong>Normal Sets:</strong> Contains a set of normal vectors to define how the lighting is calculated for the triangles. <br /><strong>Index Sets:</strong> Each graphics primitive can access this set to more efficiently use an associated coordinate or color set.</p>
<p><strong>Subsidiary Objects</strong></p>
<p><strong>TransientGeometry</strong>: The TransientGeoemtry object is a utility object you use to create various points, curves, surfaces, and mathematical objects like vectors, and matrices. In association with client graphics, the ability to create different types of curves is useful because you can use these as input to define Curve graphics. <br /><strong>TransientBrep:</strong> The TransientBRep object is used to create transient surface and solid models. These can be used to create Surface graphics </p>
<p><strong>General Procedure <br /></strong>Creation of Client Graphics follows the general procedure: <br />1. Get the GraphicsDataSetsCollection object from the Document <br />2. Add GraphicsDataSets object and create graphics data <br />(for Curve or Surface, create graphics data by TransientGeometry and Transient) <br />3. Get the ClientGraphicsCollection from the graphics owner:&#0160; <br />&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; Component in part/assembly&#0160; <br />&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; View/Sheet in drawing <br />&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; InteractionEvents for Interaction Client Graphics <br />&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; ClientFeatureDefintion for Client Feature <br />4.&#0160;&#0160;&#0160; Add a ClientGraphics object to ClientGraphicsCollection <br />5.&#0160;&#0160;&#0160; Add as many GraphicsNodes as you need <br />6.&#0160;&#0160;&#0160; Add GraphicsPrimitive objects to the node(s) <br />7.&#0160;&#0160;&#0160; Assign graphics data to the primitives <br />8.&#0160;&#0160;&#0160; Update the view(s) </p>
<p><strong>VB.NET Example: <br /></strong>This example will create a simple client graphic using LineGraphics. See the comments in the code for more details:&#0160; </p>
<div style="font-family: consolas; background: #eeeeee; color: black; font-size: 10pt;">
<p style="margin: 0px;"><span style="color: blue;">Imports</span> Inventor</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: blue;">Public</span> <span style="color: blue;">Class</span> <span style="color: #2b91af;">Form1</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">Dim</span> m_inventorApp <span style="color: blue;">As</span> Inventor.<span style="color: #2b91af;">Application</span> _</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; = <span style="color: blue;">Nothing</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">Private</span> <span style="color: blue;">Sub</span> Button1_Click(<span style="color: blue;">ByVal</span> sender <span style="color: blue;">As</span>&#0160; _</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; System.<span style="color: #2b91af;">Object</span>, _</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">ByVal</span> e <span style="color: blue;">As</span> System.<span style="color: #2b91af;">EventArgs</span>) _</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">Handles</span> Button1.Click</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">&#39; Get an active instance of Inventor</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">Try</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; m_inventorApp = System.Runtime. _</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; InteropServices.<span style="color: #2b91af;">Marshal</span>. _</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; GetActiveObject(<span style="color: #a31515;">&quot;Inventor.Application&quot;</span>)</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">Catch</span> <span style="color: green;">&#39;Inventor not started</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; System.Windows.Forms.<span style="color: #2b91af;">MessageBox</span>. _</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; Show(<span style="color: #a31515;">&quot;Start an Inventor session&quot;</span>)</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">Exit Sub</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">End</span> <span style="color: blue;">Try</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">&#39;Call the Sub</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; DrawLineGraphics()</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">End</span> <span style="color: blue;">Sub</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">&#39;Draw LineGraphics (two points)</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">Public</span> <span style="color: blue;">Sub</span> DrawLineGraphics()</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">If</span> m_inventorApp.Documents.Count _</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; &lt; 1 <span style="color: blue;">Then</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; System.Windows.Forms.<span style="color: #2b91af;">MessageBox</span>. _</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; Show(<span style="color: #a31515;">&quot;Make Assembly or Part current&quot;</span>)</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">Exit Sub</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">End</span> <span style="color: blue;">If</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">Dim</span> oDoc <span style="color: blue;">As</span> <span style="color: #2b91af;">Document</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; oDoc = m_inventorApp.ActiveDocument</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">If</span> oDoc.DocumentType = <span style="color: #2b91af;">DocumentTypeEnum</span> _</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; .kPartDocumentObject <span style="color: blue;">Or</span> _</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; oDoc.DocumentType = _</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: #2b91af;">DocumentTypeEnum</span>.kAssemblyDocumentObject <span style="color: blue;">Then</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">Try</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">&#39;Get GraphicsDataSetsCollection</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">&#39; object from the Document. </span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">&#39;Add a GraphicsDataSets object</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">Dim</span> oDataSets <span style="color: blue;">As</span> <span style="color: #2b91af;">GraphicsDataSets</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; oDataSets = oDoc. _</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; GraphicsDataSetsCollection.Add(<span style="color: #a31515;">&quot;TestCG&quot;</span>)</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">Dim</span> oCompDef <span style="color: blue;">As</span> <span style="color: #2b91af;">ComponentDefinition</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; oCompDef = oDoc.ComponentDefinition</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">&#39;Get the ClientGraphicsCollection </span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">&#39;from the graphics owner. </span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">&#39;Add a ClientGraphics</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">Dim</span> oClientGraphics <span style="color: blue;">As</span> <span style="color: #2b91af;">ClientGraphics</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; oClientGraphics = oCompDef. _</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; ClientGraphicsCollection.Add(<span style="color: #a31515;">&quot;TestCG&quot;</span>)</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">Dim</span> coordSet <span style="color: blue;">As</span> <span style="color: #2b91af;">GraphicsCoordinateSet</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; coordSet = oDataSets. _</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; CreateCoordinateSet(1)</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">&#39;Create graphics data.&#0160; </span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">&#39;In this case, prepare two points </span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">&#39;for line graphics</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">Dim</span> oPointCoords(5) <span style="color: blue;">As</span> <span style="color: blue;">Double</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; oPointCoords(0) = 0 _</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; : oPointCoords(1) = 0 _</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; : oPointCoords(2) = 0&#0160;&#0160; <span style="color: green;">&#39;point 1</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; oPointCoords(3) = 1 _</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; : oPointCoords(4) = 0 _</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; : oPointCoords(5) = 0&#0160;&#0160; <span style="color: green;">&#39;point 2</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">Call</span> coordSet.PutCoordinates _</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; (oPointCoords)</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">&#39;Add GraphicsNodes</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">Dim</span> oGraphicsNode <span style="color: blue;">As</span> <span style="color: #2b91af;">GraphicsNode</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; oGraphicsNode = oClientGraphics. _</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; AddNode(1)</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">&#39;Add GraphicsPrimitive to node</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">Dim</span> oGraphic <span style="color: blue;">As</span> <span style="color: #2b91af;">LineGraphics</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; oGraphic = oGraphicsNode. _</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; AddLineGraphics</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">&#39;Assign graphics data </span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">&#39;to the primitives</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; oGraphic.CoordinateSet = coordSet</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">&#39;update the view</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; m_inventorApp.ActiveView.Update()</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">Catch</span> ex <span style="color: blue;">As</span> <span style="color: #2b91af;">Exception</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; System.Windows.Forms. _</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: #2b91af;">MessageBox</span>.Show(ex.ToString())</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; System.Windows.Forms. _</p>
<p style="margin: 0px;">&#0160;<span style="color: #2b91af;">MessageBox</span>.Show(<span style="color: #a31515;">&quot;Prob creating Client Graphics&quot;</span>)</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">End</span> <span style="color: blue;">Try</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">Else</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; System.Windows.Forms. _</p>
<p style="margin: 0px;">&#0160;&#0160; <span style="color: #2b91af;">MessageBox</span>.Show(<span style="color: #a31515;">&quot;Make Assembly or Part Active&quot;</span>)</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">End</span> <span style="color: blue;">If</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">End</span> <span style="color: blue;">Sub</span></p>
<p style="margin: 0px;"><span style="color: blue;">End</span> <span style="color: blue;">Class</span></p>
</div>
