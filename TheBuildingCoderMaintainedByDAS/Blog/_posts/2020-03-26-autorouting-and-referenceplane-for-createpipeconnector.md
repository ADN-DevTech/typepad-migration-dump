---
layout: "post"
title: "Autorouting and Reference for CreatePipeConnector"
date: "2020-03-26 06:00:00"
author: "Jeremy Tammik"
categories:
  - "Algorithm"
  - "Element Creation"
  - "Failure"
  - "Geometry"
  - "News"
  - "RME"
  - "User Interface"
original_url: "https://thebuildingcoder.typepad.com/blog/2020/03/autorouting-and-referenceplane-for-createpipeconnector.html "
typepad_basename: "autorouting-and-referenceplane-for-createpipeconnector"
typepad_status: "Publish"
---

<p>Today, let's pick up two recent MEP related discussions, on creating a new pipe connector for a hydraulic fitting family and on automatic pipe system routing, and a couple of other odd items of interest:</p>

<ul>
<li><a href="#2">The names they are a-changin</a></li>
<li><a href="#3">Getting a <code>ReferencePlane</code> for <code>CreatePipeConnector</code></a></li>
<li><a href="#4">Auto-routing a pipe system between plumbing fixtures</a></li>
<li><a href="#5">Handling dialogue and failure messages</a></li>
<li><a href="#6">Retrieving a geometry reference</a></li>
</ul>

<h4><a name="2"></a>The Names They are A-Changin</h4>

<p>In these times of accelerating change, the name of my team has changed as well.</p>

<p>From now on, I now work in the Autodesk DAS team:</p>

<ul>
<li>RDP Registered Developer Program 1987-1995</li>
<li>ADN Autodesk Developer Network 1995-2015</li>
<li>FPD Forge Partner Development 2015-2020</li>
<li>DAS Developer Advocacy and Support 2020+</li>
</ul>

<p><center></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e1689788330240a4f5ee2e200d-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e1689788330240a4f5ee2e200d image-full img-responsive" alt="Developer Advocacy and Support" title="Developer Advocacy and Support" src="/assets/image_e3eeef.jpg" border="0" style="display: block; margin-left: auto; margin-right: auto;" /></a><br /></p>

<p></center></p>

<h4><a name="3"></a>Getting a ReferencePlane for CreatePipeConnector</h4>

<p>I had an interesting and fruitful conversation with Edouard Vnuk in 
the <a href="http://forums.autodesk.com/t5/revit-api-forum/bd-p/160">Revit API discussion forum</a> thread
on <a href="https://forums.autodesk.com/t5/revit-api-forum/createpipeconnector-from-referenceplane/m-p/9396013">CreatePipeConnector from ReferencePlane</a>:</p>

<p><strong>Question:</strong> I would like to create a connector from a reference plane.
The <code>CreatePipeConnector</code> function requires a <code>PlanarFace</code>.
Is there another function, or how can I transform a <code>ReferencePlane</code> into <code>PlanarFace</code>?</p>

<p><strong>Answer:</strong> The <a href="https://www.revitapidocs.com/2020/e7003ec7-1dbe-50a2-fb3d-a83a5a3b5b9f.htm">ReferencePlane documentation sample code</a> shows
how to call GetPlane to retrieve the DB Plane.</p>

<p>More to the point, the CreateAirHandler SDK sample to how to use the <code>CreatePipeConnector</code> method.</p>

<p>When in doubt about how to call a Revit API method, one of the first places to always consult is the collection of Revit SDK samples.
That step often helps and may save time and effort for you and others.</p>

<p><strong>Response:</strong> I tried several codes without success.</p>

<p>First code:</p>

<pre class="code">
  Autodesk.Revit.DB.Plane plane
    = Reference_plane.GetPlane();

  ConnectorElement connector = ConnectorElement
    .CreatePipeConnector( family_document,
      PipeSystemType.Global, plane );
</pre>

<p>However, I can't compile this code, because the parameter must be a reference of planar face, and I don't know how to get it from the plane.</p>

<p>Second code:</p>

<pre class="code">
  ConnectorElement connector = ConnectorElement
    .CreatePipeConnector( family_document,
      PipeSystemType.Global,
      Reference_plane.GetReference() );
</pre>

<p>This throws an exception during execution: The reference is not a planar face. Parameter name: planarFace.</p>

<p>These are the user interface steps I would like to complete programmatically:</p>

<p><center></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e1689788330240a51a9e8c200b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e1689788330240a51a9e8c200b img-responsive" style="width: 454px; display: block; margin-left: auto; margin-right: auto;" alt="Pipe connector placement user interface" title="Pipe connector placement user interface" src="/assets/image_d6e8f6.jpg" /></a><br /></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e168978833025d9b408089200c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e168978833025d9b408089200c image-full img-responsive" alt="Pipe connector placement user interface" title="Pipe connector placement user interface" src="/assets/image_97d3cf.jpg" border="0" style="display: block; margin-left: auto; margin-right: auto;" /></a><br /></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e1689788330240a51a9e9e200b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e1689788330240a51a9e9e200b img-responsive" style="width: 391px; display: block; margin-left: auto; margin-right: auto;" alt="Pipe connector placement user interface" title="Pipe connector placement user interface" src="/assets/image_652e58.jpg" /></a><br /></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e168978833025d9b408092200c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e168978833025d9b408092200c image-full img-responsive" alt="Pipe_connector_ui_4" title="Pipe_connector_ui_4" src="/assets/image_be01dc.jpg" border="0" style="display: block; margin-left: auto; margin-right: auto;" /></a><br /></p>

<p></center></p>

<p>I looked the CreateAirHandler SDK sample example.
However, if I understood correctly, it creates the connectors from an extruded volume that provides the required planar faces.</p>

<p><strong>Answer:</strong> Good. I see your point.
Such situations arise regularly.
You do have a real existing surface somewhere in the model that you can mount your connector on, don't you?
Otherwise, you are modelling something that cannot be built.
The existing surface is part of some BIM element geometry.
You can identify the appropriate element and retrieve its geometry from the document by calling its Geometry property and providing an Options object with ComputeReferences set to true.
Then, iterate through all its surfaces to identify the one you need.
Et voila, that surface is equipped with a reference that you can use to create the connector.</p>

<p><strong>Response:</strong> What you told me inspired me to think about it.
I am creating a converter.
And so, I create automatically families from basic geometric shapes.
To add my connectors that do not rest on an existing face, I artificially added a cylinder that I masked where I wanted a connector.
Here's what I get :</p>

<p><center></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e1689788330240a4f5eeb6200d-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e1689788330240a4f5eeb6200d img-responsive" style="width: 260px; display: block; margin-left: auto; margin-right: auto;" alt="Pipe connectors in family" title="Pipe connectors in family" src="/assets/image_1da12f.jpg" /></a></p>

<p style="font-size: 80%; font-style:italic">Pipe connectors in family</p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e168978833025d9b408128200c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e168978833025d9b408128200c image-full img-responsive" alt="Pipe connectors in project" title="Pipe connectors in project"  src="/assets/image_95ab21.jpg" border="0" style="display: block; margin-left: auto; margin-right: auto;" /></a></p>

<p style="font-size: 80%; font-style:italic">Pipe connectors in project</p>

<p></center></p>

<p>Thank you so much.</p>

<p>Many thanks to Edouard for raising this, the in-depth discussion and sharing his clean solution!</p>

<h4><a name="4"></a>Auto-routing a Pipe System Between Plumbing Fixtures</h4>

<p>The other item I would like to pick up here is
the <a href="http://forums.autodesk.com/t5/revit-api-forum/bd-p/160">Revit API discussion forum</a> thread
on <a href="https://forums.autodesk.com/t5/revit-api-forum/auto-route-a-pipe-system-between-plumbing-fixtures/m-p/9387502">auto routing a pipe system between plumbing fixtures</a>:</p>

<p><strong>Question:</strong> I'm wondering if there is something within the API that will allow me to place plumbing fixture families and then generate a plumbing layout like you can in Revit by using the "Create Plumbing System --> Generate Layout" or is this something I have to do one pipe, one connector at a time?</p>

<p><center></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e1689788330240a4f5ef2d200d-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e1689788330240a4f5ef2d200d image-full img-responsive" alt="Select elements and click create system" title="Select elements and click create system" src="/assets/image_e28ff1.jpg" border="0" style="display: block; margin-left: auto; margin-right: auto;" /></a></p>

<p style="font-size: 80%; font-style:italic">Select elements and click create system</p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e1689788330240a51a9f93200b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e1689788330240a51a9f93200b img-responsive" style="width: 142px; display: block; margin-left: auto; margin-right: auto;" alt="Generate layout button" title="Generate layout button" src="/assets/image_e216ab.jpg" /></a></p>

<p style="font-size: 80%; font-style:italic">Generate layout button</p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e1689788330240a4f5ef4f200d-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e1689788330240a4f5ef4f200d image-full img-responsive" alt="Auto generated pipes" title="Auto generated pipes"  src="/assets/image_9759f5.jpg" border="0" style="display: block; margin-left: auto; margin-right: auto;" /></a></p>

<p style="font-size: 80%; font-style:italic">Auto generated pipes</p>

<p></center></p>

<p><strong>Answer:</strong> You
can <a href="https://thebuildingcoder.typepad.com/blog/2011/07/mep-placeholders.html">create a placeholder piping system programmatically</a> as well.</p>

<p>Or you can create a piping system right away.
Here is a series of samples that create a minimal bunch of pipes for
a <a href="http://thebuildingcoder.typepad.com/blog/2014/01/final-rolling-offset-using-pipecreate.html">rolling offset in lots of different ways</a>.</p>

<p><strong>Response:</strong> I am assuming from your response that if I want to connect from say, a water closet to a water heater, with pipes, I will have to explicitly call PipeCreate for each pipe, find connectors, insert each fitting... and then that process over and over until they connect?
Is there a better work flow for what I'm trying to accomplish?
Something similar to what is in the Revit user interface?</p>

<p><strong>Answer:</strong> The discussions I pointed out already explain everything and cover your question in full.
Actually, here are some additional relevant discussions of this topic:</p>

<ul>
<li><a href="https://thebuildingcoder.typepad.com/blog/2018/03/connector-neighbour-conduit-transition.html">Connector, neighbour, conduit, transition</a></li>
<li><a href="https://thebuildingcoder.typepad.com/blog/2019/08/mep-ductwork-and-changing-pipe-direction.html">MEP ductwork and changing pipe direction</a></li>
</ul>

<p>The short answer is simply:</p>

<ul>
<li>You can go any way you like.</li>
<li>You can just insert the pipes, no fittings. Then, if you connect the pipe connectors, Revit will automatically select and place and connect the appropriate fittings according to your routing preferences and adjust the pipe endpoints so they fit.</li>
<li>You can just insert all the fittings, no pipes. Then, if you connect the fitting connectors, Revit will automatically place and connect the appropriate pipes between them.</li>
</ul>

<p>That is the gist of what I learned researching and implementing the rolling offset.</p>

<h4><a name="5"></a>Handling Dialogue and Failure Messages</h4>

<p>A recurring question on how to handle dialogue and failure messages keeps popping up when driving Revit programmatically, e.g.,
on <a href="https://stackoverflow.com/questions/60831658/saving-families-out-vie-revit-api">saving families out via Revit API</a>:</p>

<p><strong>Question:</strong> I am collecting all the families in a project and saving them out via the API using </p>

<pre class="code">
  familyDocument.SaveAs(fileName);
</pre>

<p>Is there a way to catch the following dialogue box and perform an action?</p>

<p><center></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e168978833025d9b408196200c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e168978833025d9b408196200c img-responsive" style="width: 479px; display: block; margin-left: auto; margin-right: auto;" alt="Failure message" title="Failure message" src="/assets/image_5ec479.jpg" /></a><br /></p>

<p></center></p>

<p>For instance, record the warning and close the dialogue box?</p>

<p><strong>Answer:</strong> The Revit API offers two different mechanisms to react to and handle dialogue and failure messages:</p>

<ul>
<li>The <a href="https://www.revitapidocs.com/2020/cb46ea4c-2b80-0ec2-063f-dda6f662948a.htm">DialogBoxShowing event</a> and</li>
<li>The <a href="https://www.revitapidocs.com/2020/d0795bd6-f092-90f2-5c2c-3876e616454c.htm">Failure API</a>.</li>
</ul>

<p>If all of these fail, a third mechanism is provided by the Windows API, which enables hooking into and reacting to almost any system event, including a dialogue showing.</p>

<p>All three approaches are discussed and compared by The Building Coder in the topic group
on <a href="https://thebuildingcoder.typepad.com/blog/about-the-author.html#5.32">detecting and handling dialogues and failures</a>.</p>

<h4><a name="6"></a>Retrieving a Geometry Reference</h4>

<p>Let's round off with a quick question 
on <a href="https://forums.autodesk.com/t5/revit-api-forum/get-reference-upper-on-rebar-51/m-p/9396627">getting a reference to the upper face on rebar 51</a>:</p>

<p><strong>Question:</strong> This line of code works on some rebar '51' to retrieve a reference to the upper face, but not all:</p>

<pre class="prettyprint">
var srep = $"{rebar51.UniqueId}:2000000:{1002000+typ}:LINEAR";

var refr = Reference.ParseFromStableRepresentation(
  rebar51.Document, srep );
</pre>

<p>Is there a simpler method to get a <code>Reference</code> to the upper top face of the rebar form '51'?</p>

<p><strong>Answer:</strong> You can ask the rebar for its geometry using
the <a href="https://www.revitapidocs.com/2020/d8a55a5b-2a69-d5ab-3e1f-6cf1ee43c8ec.htm">Element.Geometry property</a>.</p>

<p>It takes an <code>Options</code> argument in which you can specify <code>ComputeReferences</code> = <code>true</code>.</p>

<p>Iterate over the geometry solids and their faces, pick the face that you want, e.g., based on its normal vector (pointing upwards) and vertex locations (maximal Z values), and use the reference that it comes equipped with.</p>
