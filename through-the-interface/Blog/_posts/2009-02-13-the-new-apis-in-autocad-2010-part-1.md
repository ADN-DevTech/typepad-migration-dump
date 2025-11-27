---
layout: "post"
title: "The new APIs in AutoCAD 2010 - Part 1"
date: "2009-02-13 14:57:39"
author: "Kean Walmsley"
categories:
  - "AutoCAD"
  - "AutoCAD .NET"
  - "Notification / Events"
  - "Solid modeling"
original_url: "https://www.keanw.com/2009/02/the-new-apis-in-autocad-2010---part-1.html "
typepad_basename: "the-new-apis-in-autocad-2010---part-1"
typepad_status: "Publish"
---

<p><em>This is the first post in a series looking at the new APIs in AutoCAD 2010, and follows on from <a href="http://through-the-interface.typepad.com/through_the_interface/2009/02/migrating-your-application-to-work-with-autocad-2010.html">this post</a> looking at the migration steps required. I&#39;ve copied the information in this post from the recently-published <a href="http://adn.autodesk.com/adn/servlet/item?siteID=4814862&amp;id=12537713">Platform Technologies Customization Newsletter</a>, a quarterly newsletter available to ADN members. A big thank you to Stephen Preston, Fenton Webb and Gopinath Taget for putting the material together.</em></p>
<p><strong>AutoCAD 2010 New API Overview</strong></p>
<p>AutoCAD 2010 has some really cool APIs. Please download the ObjectARX 2010 Beta SDK and review the Migration Guide for a complete list of changes and additions. <em>[This is currently available to ADN members on the ADN extranet.]</em></p>
<p>Here are the highlights:</p>
<p><strong>Overrule API</strong></p>
<p>One of the most powerful ObjectARX APIs is the custom objects API. The custom object API allows you to create your own entities in a drawing that behave in the same way as standard AutoCAD entities. So, for example, where AutoCAD has a line, you might develop a custom entity that looks like a ‘pipe’. You can define how your pipe displays itself, the pipes grip- and snap- points, how the pipe behaves when moves or copied, etc.</p>
<p>However, with great power comes great responsibility. Custom objects are saved to a drawing. Without your Object Enabler, your custom object is loaded into AutoCAD as a dumb proxy object. So when you are considering creating a custom object, you need to consider whether you’re prepared to make a commitment to your application users that you will continue to support your custom object through multiple AutoCAD releases. If you’re not prepared to make that commitment, then you really shouldn’t be creating custom objects.</p>
<p>And because your custom object is responsible for filing itself when a drawing is saved or opened, you also have an extremely powerful mechanism for corrupting all your customers drawings if you make a mistake in your implementation.<br />To provide you with an alternative to custom objects – an alternative that requires less long term support commitment from you – AutoCAD 2010 introduces the new Overrule API. Think of Overrule as customized objects, rather than custom objects. It’s essentially a mechanism for AutoCAD to call your implementation of certain object functions instead of immediately calling the functions for that object. Your implementation can then choose whether to refer the call back to the native object. Unlike custom objects, the overrule definitions are not filed to the DWG file, so it’s a lot harder to corrupt your drawing. Instead, the Overrule API will only customize an entity when your application is loaded. (Although, you can save data used by your Overrul<a href="http://through-the-interface.typepad.com/.a/6a00d83452464869e2011168600713970c-pi"><img align="right" alt="Overrule API thermometer" border="0" height="240" src="/assets/image_859461.jpg" style="BORDER-TOP-WIDTH: 0px; BORDER-LEFT-WIDTH: 0px; BORDER-BOTTOM-WIDTH: 0px; MARGIN: 10px 10px 10px 30px; BORDER-RIGHT-WIDTH: 0px" title="Overrule API thermometer" width="197" /></a>e as Xdata or in Xrecords).</p>
<p>As a simple example, you can overrule an entity’s worldDraw function and draw your own graphical representation instead. (In the simple sample we demonstrated at Developer Days, we took a Line and turned it into a Thermometer (see image).</p>
<br />
<p><em>Image: Two Lines – Can you tell which one has been Overruled? ;-).</em></p>
<br />
<br />
<p>The Overrule API is available in ObjectARX (C++) and .NET. Here’s a simple VB.NET example of how you’d create an overrule…</p>
<p>First, create your custom Overrule class, inheriting from one of the available Overrules, and overriding the functions you want to overrule. In this case, we’re overruling an entity’s WorldDraw function. WorldDraw is part of the DrawableOverrule.</p>
<div style="FONT-SIZE: 8pt; BACKGROUND: white; COLOR: black; FONT-FAMILY: courier new">
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue; LINE-HEIGHT: 140%">Imports</span><span style="LINE-HEIGHT: 140%"> Autodesk.AutoCAD.GraphicsInterface</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160;</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue; LINE-HEIGHT: 140%">Public</span><span style="LINE-HEIGHT: 140%"> </span><span style="COLOR: blue; LINE-HEIGHT: 140%">Class</span><span style="LINE-HEIGHT: 140%"> MyDrawOverrule</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; </span><span style="COLOR: blue; LINE-HEIGHT: 140%">Inherits</span><span style="LINE-HEIGHT: 140%"> DrawableOverrule</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160;</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; </span><span style="COLOR: green; LINE-HEIGHT: 140%">&#39;This is the function that gets called to add/replace</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; </span><span style="COLOR: green; LINE-HEIGHT: 140%">&#39;an entity&#39;s WorldDraw graphics</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160;</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; </span><span style="COLOR: blue; LINE-HEIGHT: 140%">Public</span><span style="LINE-HEIGHT: 140%"> </span><span style="COLOR: blue; LINE-HEIGHT: 140%">Overrides</span><span style="LINE-HEIGHT: 140%"> </span><span style="COLOR: blue; LINE-HEIGHT: 140%">Function</span><span style="LINE-HEIGHT: 140%"> WorldDraw( _</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="COLOR: blue; LINE-HEIGHT: 140%">ByVal</span><span style="LINE-HEIGHT: 140%"> drawable </span><span style="COLOR: blue; LINE-HEIGHT: 140%">As</span><span style="LINE-HEIGHT: 140%"> Drawable, _</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="COLOR: blue; LINE-HEIGHT: 140%">ByVal</span><span style="LINE-HEIGHT: 140%"> wd </span><span style="COLOR: blue; LINE-HEIGHT: 140%">As</span><span style="LINE-HEIGHT: 140%"> WorldDraw) </span><span style="COLOR: blue; LINE-HEIGHT: 140%">As</span><span style="LINE-HEIGHT: 140%"> </span><span style="COLOR: blue; LINE-HEIGHT: 140%">Boolean</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160;</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="COLOR: green; LINE-HEIGHT: 140%">&#39;Draw my own graphics here ...</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160;</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="COLOR: green; LINE-HEIGHT: 140%">&#39;Call the object&#39;s own worldDraw function (if you want to)</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="COLOR: blue; LINE-HEIGHT: 140%">Return</span><span style="LINE-HEIGHT: 140%"> </span><span style="COLOR: blue; LINE-HEIGHT: 140%">MyBase</span><span style="LINE-HEIGHT: 140%">.WorldDraw(drawable, wd)</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160;</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; </span><span style="COLOR: blue; LINE-HEIGHT: 140%">End</span><span style="LINE-HEIGHT: 140%"> </span><span style="COLOR: blue; LINE-HEIGHT: 140%">Function</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160;</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue; LINE-HEIGHT: 140%">End</span><span style="LINE-HEIGHT: 140%"> </span><span style="COLOR: blue; LINE-HEIGHT: 140%">Class</span></p></div>
<p>Next, instantiate your Overrule, add it to the entity you want to overrule, and turn Overruling on. (You can also specify how the overrule is applied – you can apply it to every object of that type, apply it depending on Xdata or Xrecords, maintain a list of ObjectIds of entities to be overruled, or define your own custom filter).</p>
<p><em>[Note that you will need to have Imported Autodesk,AutoCAD.Runtime and DatabaseServices for the below code to build.]</em></p>
<div style="FONT-SIZE: 8pt; BACKGROUND: white; COLOR: black; FONT-FAMILY: courier new">
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: green; LINE-HEIGHT: 140%">&#39;mDrawOverrule is a class member variable</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: green; LINE-HEIGHT: 140%">&#39;we declared elsewhere</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160;</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">mDrawOverrule = </span><span style="COLOR: blue; LINE-HEIGHT: 140%">New</span><span style="LINE-HEIGHT: 140%"> MyDrawOverrule</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160;</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: green; LINE-HEIGHT: 140%">&#39;Add the Overrule to the entity class - in this case Line</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">Overrule.AddOverrule( _</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; RXObject.GetClass(</span><span style="COLOR: blue; LINE-HEIGHT: 140%">GetType</span><span style="LINE-HEIGHT: 140%">(Line)), _</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; mDrawOverrule, </span><span style="COLOR: blue; LINE-HEIGHT: 140%">False</span><span style="LINE-HEIGHT: 140%">)</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160;</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: green; LINE-HEIGHT: 140%">&#39;Optional - specify filter</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: green; LINE-HEIGHT: 140%">&#39;(In this case we only apply overrule to Lines with entry</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: green; LINE-HEIGHT: 140%">&#39; named &quot;RDS_MyData&quot; in Extension Dictionary) </span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160;</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">mDrawOverrule.SetExtensionDictionaryEntryFilter(</span><span style="COLOR: #a31515; LINE-HEIGHT: 140%">&quot;RDS_MyData&quot;</span><span style="LINE-HEIGHT: 140%">)</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160;</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: green; LINE-HEIGHT: 140%">&#39;Turn overruling on</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">Overrule.Overruling = </span><span style="COLOR: blue; LINE-HEIGHT: 140%">True</span></p></div>
<p>And that’s all there is to it.</p>
<p>You can find a (simple) working Overrule sample <a href="http://adn.autodesk.com/adn/servlet/item?siteID=4814862&amp;id=12073572">with the Developer Days material posted on the ADN website</a>. <em>[I will post my own C# sample to this blog over the coming weeks, as I play around with the API myself - Kean]</em> We’ll be extending that sample soon and using it as the basis of a webcast after AutoCAD 2010 has shipped. And look at the ‘Behavior Overrules’ section of the ObjectARX Developers Guide for information on the ObjectARX implementation of this API, and for details of methods affected by this API.</p>
<p><strong>Freeform Modeling API</strong></p>
<p>3D modeling in AutoCAD tends to be a bit ‘blocky’. It’s hard to create a shape that looks really organic. That’s where Freeform modeling comes in. It’s hard to describe succinctly the power of this feature, so I’d encourage you to review <a href="http://adn.autodesk.com/adn/servlet/item?siteID=4814862&amp;id=12408957&amp;linkID=4900509">Heidi’s product demonstration</a>&#0160;<em>[Once again, this link is to the ADN site - I will post more about the freeform modelling capabilities of AutoCAD 2010, in due course - Kean]</em>. The basic idea is to take a solid or mesh, twist it around a bit by pushing and pulling at its edges, vertices and faces, and then smooth it and crease it. The smoothing is performed using <a href="http://en.wikipedia.org/wiki/Subdivision_surface">Subdivision</a> – we use the Catmull-Clark algorithm that is already being used by other Autodesk products.<br /></p>
<p>The API centers on the Sub-division mesh object – AcDbSubDMesh in ObjectARX, DatabaseServices.SubDMesh in .NET, and AcadSubDMesh in ActiveX. The API allows you to do essentially everything a user can through the UI. Here’s a simple VB.NET sample showing how to generate a SubDMesh from a Solid3d and then apply level 1 smoothing to it.</p>
<div style="FONT-SIZE: 8pt; BACKGROUND: white; COLOR: black; FONT-FAMILY: courier new">
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue; LINE-HEIGHT: 140%">Imports</span><span style="LINE-HEIGHT: 140%"> Autodesk.AutoCAD.ApplicationServices</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue; LINE-HEIGHT: 140%">Imports</span><span style="LINE-HEIGHT: 140%"> Autodesk.AutoCAD.DatabaseServices</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue; LINE-HEIGHT: 140%">Imports</span><span style="LINE-HEIGHT: 140%"> Autodesk.AutoCAD.EditorInput</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue; LINE-HEIGHT: 140%">Imports</span><span style="LINE-HEIGHT: 140%"> Autodesk.AutoCAD.Geometry</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue; LINE-HEIGHT: 140%">Imports</span><span style="LINE-HEIGHT: 140%"> Autodesk.AutoCAD.Runtime</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160;</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue; LINE-HEIGHT: 140%">Public</span><span style="LINE-HEIGHT: 140%"> </span><span style="COLOR: blue; LINE-HEIGHT: 140%">Class</span><span style="LINE-HEIGHT: 140%"> FreeFormSample</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160;</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &lt;CommandMethod(</span><span style="COLOR: #a31515; LINE-HEIGHT: 140%">&quot;CREATEMESH&quot;</span><span style="LINE-HEIGHT: 140%">)&gt; _</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; </span><span style="COLOR: blue; LINE-HEIGHT: 140%">Public</span><span style="LINE-HEIGHT: 140%"> </span><span style="COLOR: blue; LINE-HEIGHT: 140%">Sub</span><span style="LINE-HEIGHT: 140%"> MySub()</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160;</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; </span><span style="COLOR: green; LINE-HEIGHT: 140%">&#39;Select a solid.</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; </span><span style="COLOR: blue; LINE-HEIGHT: 140%">Dim</span><span style="LINE-HEIGHT: 140%"> ed </span><span style="COLOR: blue; LINE-HEIGHT: 140%">As</span><span style="LINE-HEIGHT: 140%"> Editor = _</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160; Application.DocumentManager.MdiActiveDocument.Editor</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; </span><span style="COLOR: blue; LINE-HEIGHT: 140%">Dim</span><span style="LINE-HEIGHT: 140%"> opts </span><span style="COLOR: blue; LINE-HEIGHT: 140%">As</span><span style="LINE-HEIGHT: 140%"> _</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="COLOR: blue; LINE-HEIGHT: 140%">New</span><span style="LINE-HEIGHT: 140%"> PromptEntityOptions(vbCrLf + </span><span style="COLOR: #a31515; LINE-HEIGHT: 140%">&quot;Select Solid:&quot;</span><span style="LINE-HEIGHT: 140%">)</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; opts.SetRejectMessage(vbCrLf &amp; </span><span style="COLOR: #a31515; LINE-HEIGHT: 140%">&quot;That&#39;s not a solid!&quot;</span><span style="LINE-HEIGHT: 140%">)</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; opts.AddAllowedClass(</span><span style="COLOR: blue; LINE-HEIGHT: 140%">GetType</span><span style="LINE-HEIGHT: 140%">(Solid3d), </span><span style="COLOR: blue; LINE-HEIGHT: 140%">False</span><span style="LINE-HEIGHT: 140%">)</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; </span><span style="COLOR: blue; LINE-HEIGHT: 140%">Dim</span><span style="LINE-HEIGHT: 140%"> res </span><span style="COLOR: blue; LINE-HEIGHT: 140%">As</span><span style="LINE-HEIGHT: 140%"> PromptEntityResult = ed.GetEntity(opts)</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160;</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; </span><span style="COLOR: green; LINE-HEIGHT: 140%">&#39;Exit sub if user cancelled selection.</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; </span><span style="COLOR: blue; LINE-HEIGHT: 140%">If</span><span style="LINE-HEIGHT: 140%"> res.Status &lt;&gt; PromptStatus.OK </span><span style="COLOR: blue; LINE-HEIGHT: 140%">Then</span><span style="LINE-HEIGHT: 140%"> </span><span style="COLOR: blue; LINE-HEIGHT: 140%">Exit</span><span style="LINE-HEIGHT: 140%"> </span><span style="COLOR: blue; LINE-HEIGHT: 140%">Sub</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160;</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; </span><span style="COLOR: green; LINE-HEIGHT: 140%">&#39;Usual transaction stuff</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; </span><span style="COLOR: blue; LINE-HEIGHT: 140%">Dim</span><span style="LINE-HEIGHT: 140%"> db </span><span style="COLOR: blue; LINE-HEIGHT: 140%">As</span><span style="LINE-HEIGHT: 140%"> Database = _</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160; Application.DocumentManager.MdiActiveDocument.Database</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; </span><span style="COLOR: blue; LINE-HEIGHT: 140%">Using</span><span style="LINE-HEIGHT: 140%"> tr </span><span style="COLOR: blue; LINE-HEIGHT: 140%">As</span><span style="LINE-HEIGHT: 140%"> Transaction = _</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160; db.TransactionManager.StartTransaction</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160;</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="COLOR: blue; LINE-HEIGHT: 140%">Dim</span><span style="LINE-HEIGHT: 140%"> mySolid </span><span style="COLOR: blue; LINE-HEIGHT: 140%">As</span><span style="LINE-HEIGHT: 140%"> Solid3d = _</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; tr.GetObject( _</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; res.ObjectId, _</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; OpenMode.ForRead, </span><span style="COLOR: blue; LINE-HEIGHT: 140%">False</span><span style="LINE-HEIGHT: 140%">)</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="COLOR: blue; LINE-HEIGHT: 140%">Dim</span><span style="LINE-HEIGHT: 140%"> ext </span><span style="COLOR: blue; LINE-HEIGHT: 140%">As</span><span style="LINE-HEIGHT: 140%"> Extents3d = mySolid.Bounds</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="COLOR: blue; LINE-HEIGHT: 140%">Dim</span><span style="LINE-HEIGHT: 140%"> vec </span><span style="COLOR: blue; LINE-HEIGHT: 140%">As</span><span style="LINE-HEIGHT: 140%"> Vector3d = (ext.MaxPoint - ext.MinPoint)</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160;</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="COLOR: green; LINE-HEIGHT: 140%">&#39;Define params governing mesh generation algorithm</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="COLOR: green; LINE-HEIGHT: 140%">&#39;(See ObjectARX helpfiles for explanation of params –</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="COLOR: green; LINE-HEIGHT: 140%">&#39; you may need to change them depending on the scale</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="COLOR: green; LINE-HEIGHT: 140%">&#39; of the solid)</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="COLOR: blue; LINE-HEIGHT: 140%">Dim</span><span style="LINE-HEIGHT: 140%"> myFaceterData </span><span style="COLOR: blue; LINE-HEIGHT: 140%">As</span><span style="LINE-HEIGHT: 140%"> _</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="COLOR: blue; LINE-HEIGHT: 140%">New</span><span style="LINE-HEIGHT: 140%"> MeshFaceterData( _</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; 0.01 * vec.Length, _</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; 40 * Math.PI / 180, _</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; 2, 2, 15, 5, 5, 0)</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160;</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="COLOR: green; LINE-HEIGHT: 140%">&#39;Create new mesh from solid (smoothing level 1)</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="COLOR: blue; LINE-HEIGHT: 140%">Dim</span><span style="LINE-HEIGHT: 140%"> meshData </span><span style="COLOR: blue; LINE-HEIGHT: 140%">As</span><span style="LINE-HEIGHT: 140%"> MeshDataCollection = _</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; SubDMesh.GetObjectMesh(mySolid, myFaceterData)</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="COLOR: blue; LINE-HEIGHT: 140%">Dim</span><span style="LINE-HEIGHT: 140%"> myMesh </span><span style="COLOR: blue; LINE-HEIGHT: 140%">As</span><span style="LINE-HEIGHT: 140%"> </span><span style="COLOR: blue; LINE-HEIGHT: 140%">New</span><span style="LINE-HEIGHT: 140%"> SubDMesh</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160; myMesh.SetSubDMesh( _</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; meshData.VertexArray, meshData.FaceArray, 1)</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160;</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="COLOR: green; LINE-HEIGHT: 140%">&#39;Add mesh to database. (Don&#39;t remove solid).</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160; myMesh.SetDatabaseDefaults()</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="COLOR: blue; LINE-HEIGHT: 140%">Dim</span><span style="LINE-HEIGHT: 140%"> btr </span><span style="COLOR: blue; LINE-HEIGHT: 140%">As</span><span style="LINE-HEIGHT: 140%"> BlockTableRecord = _</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; tr.GetObject(db.CurrentSpaceId, OpenMode.ForWrite)</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160; btr.AppendEntity(myMesh)</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160; tr.AddNewlyCreatedDBObject(myMesh, </span><span style="COLOR: blue; LINE-HEIGHT: 140%">True</span><span style="LINE-HEIGHT: 140%">)</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160;</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="COLOR: green; LINE-HEIGHT: 140%">&#39;Our work here is done</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160; tr.Commit()</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; </span><span style="COLOR: blue; LINE-HEIGHT: 140%">End</span><span style="LINE-HEIGHT: 140%"> </span><span style="COLOR: blue; LINE-HEIGHT: 140%">Using</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; </span><span style="COLOR: blue; LINE-HEIGHT: 140%">End</span><span style="LINE-HEIGHT: 140%"> </span><span style="COLOR: blue; LINE-HEIGHT: 140%">Sub</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160;</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue; LINE-HEIGHT: 140%">End</span><span style="LINE-HEIGHT: 140%"> </span><span style="COLOR: blue; LINE-HEIGHT: 140%">Class</span></p></div>
<p><em>In the next post we&#39;ll look at the Parametric Drawing API, CUI API Enhancements, RibbonBar Controls, PDF Underlays and the new AutoCAD .NET Developer&#39;s Guide.</em></p>
