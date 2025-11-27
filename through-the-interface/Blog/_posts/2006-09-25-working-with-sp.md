---
layout: "post"
title: "Working with specific AutoCAD object types in .NET"
date: "2006-09-25 09:00:00"
author: "Kean Walmsley"
categories:
  - "AutoCAD"
  - "AutoCAD .NET"
  - "ObjectARX"
original_url: "https://www.keanw.com/2006/09/working_with_sp.html "
typepad_basename: "working_with_sp"
typepad_status: "Publish"
---

<p>Most of the functions to access objects in the AutoCAD drawing return generic objects/entities. The base type for objects stored in the AutoCAD database is “DBObject” – which can be used to access common persistence information such as the handle, etc. – but there is no such thing as a pure DBObject: all DBObjects actually belong to a type that is derived from this base (whether that’s a “Line”, “Circle”, “BlockReference”, etc.).</p>

<p>The next level up in the hierarchy is “Entity”, from where you can access graphical properties such as color, layer, linetype, material, etc. The sample code in <a href="http://through-the-interface.typepad.com/through_the_interface/2006/09/accessing_objec.html">this previous entry</a> shows how to treat a DBObject as an Entity – in this entry we’re going to look at how to access information from an even more specific type, such as the center point and radius of a Circle.</p>

<p>In order to find out what kind of object you’re dealing with, you need to use some kind of runtime type system. A compile-time type system is not going to be enough, as in many cases you simply don’t know what type of object it is you’re going to find in a particular location in the drawing database – especially when asking the user to select entities or when reading them from one of the block table records such as the model-space.</p>

<p>C++ introduced a system for <a href="http://en.wikipedia.org/wiki/RTTI">RTTI</a> (RunTime Type Identification) after ObjectARX was first implemented, so AutoCAD maintains its own class hierarchy in memory. In order to use a more specific class in ObjectARX than the generic AcDbObject, you typically use isKindOf() or cast(), which ultimately use the AcDb class hierarchy behind the scenes to determine whether the pointer conversion operation is safe. The C++ standard now includes <a href="http://msdn2.microsoft.com/en-us/library/cby9kycs.aspx">dynamic_cast&lt;&gt;</a> to perform the equivalent task, but this is not enabled for standard ObjectARX types. As far as I recall, it is enabled for some of our other APIs (such as the Object Modeling Framework, the C++ API in Architectural Desktop that sits on top of ObjectARX), but for ObjectARX the existing type system has proven adequate until now.</p>

<p>Here’s some ObjectARX code showing this:</p>

<div style="background: white none repeat scroll 0%; font-size: 8pt; -moz-background-clip: -moz-initial; -moz-background-origin: -moz-initial; -moz-background-inline-policy: -moz-initial; color: black; font-family: Courier New;"><p style="margin: 0px; font-size: 8pt;"><span style="color: green;">// objId is the object ID of the entity we want to access</span></p>

<p style="margin: 0px; font-size: 8pt;">Acad::ErrorStatus es;</p>

<p style="margin: 0px; font-size: 8pt;">AcDbEntity *pEnt = NULL;</p>

<p style="margin: 0px; font-size: 8pt;">es = acdbOpenAcDbEntity( pEnt, objId, AcDb::kForRead );</p>

<p style="margin: 0px; font-size: 8pt;"><span style="color: blue;">if</span> ( es == Acad::eOk )</p>

<p style="margin: 0px; font-size: 8pt;">{</p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; AcDbCircle *pCircle = NULL;</p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; pCircle = AcDbCircle::cast( pEnt );</p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; <span style="color: blue;">if</span> ( pCircle )</p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; {</p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; &nbsp; <span style="color: green;">// Access circle-specific properties/methods here</span></p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; &nbsp; AcGePoint3d cen = pCircle-&gt;center();</p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; &nbsp; <span style="color: green;">// ...</span></p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; }</p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; pEnt-&gt;close();&nbsp; &nbsp; </p>

<p style="margin: 0px; font-size: 8pt;">}</p></div>

<p>In a managed environment you get access to the .NET type system. Here’s an example of what you might do in VB.NET:</p>

<p>[<em><strong>Note:</strong> the following two fragments have been pointed out in comments as being sub-optimal - please see further down for a better technique...</em>]</p>

<div style="background: white none repeat scroll 0%; font-size: 8pt; -moz-background-clip: -moz-initial; -moz-background-origin: -moz-initial; -moz-background-inline-policy: -moz-initial; color: black; font-family: Courier New;"><p style="margin: 0px; font-size: 8pt;"><span style="color: green;">' tr is the running transaction</span></p>

<p style="margin: 0px; font-size: 8pt;"><span style="color: green;">' objId is the object ID of the entity we want to access</span></p>

<p style="margin: 0px; font-size: 8pt;"><span style="color: blue;">Dim</span> obj <span style="color: blue;">As</span> DBObject = tr.GetObject(objId, OpenMode.ForRead)</p>

<p style="margin: 0px; font-size: 8pt;"><span style="color: blue;">Try</span></p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; <span style="color: blue;">Dim</span> circ <span style="color: blue;">As</span> Circle = <span style="color: blue;">CType</span>(obj, Circle)</p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; <span style="color: green;">' Access circle-specific properties/methods here</span></p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; <span style="color: green;">' ...</span></p>

<p style="margin: 0px; font-size: 8pt;"><span style="color: blue;">Catch</span> ex <span style="color: blue;">As</span> InvalidCastException</p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; <span style="color: green;">' That's fine - it's just not a circle...</span></p>

<p style="margin: 0px; font-size: 8pt;"><span style="color: blue;">End</span> <span style="color: blue;">Try</span></p>

<p style="margin: 0px; font-size: 8pt;">obj.Dispose()</p></div>

<p>And in C#:</p>

<div style="background: white none repeat scroll 0%; font-size: 8pt; -moz-background-clip: -moz-initial; -moz-background-origin: -moz-initial; -moz-background-inline-policy: -moz-initial; color: black; font-family: Courier New;">
<p style="margin: 0px; font-size: 8pt;">
<span style="color: green;">// tr is the running transaction</span></p>

<p style="margin: 0px; font-size: 8pt;"><span style="color: green;">// objId is the object ID of the entity we want to access</span></p>

<p style="margin: 0px; font-size: 8pt;"><span style="color: teal;">DBObject</span> obj = tr.GetObject(objId, <span style="color: teal;">OpenMode</span>.ForRead);</p>

<p style="margin: 0px; font-size: 8pt;"><span style="color: blue;">try</span></p>

<p style="margin: 0px; font-size: 8pt;">{</p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; <span style="color: teal;">Circle</span> circ = (circle)obj;</p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; <span style="color: green;">// Access circle-specific properties/methods here</span></p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; <span style="color: green;">// ...</span></p>

<p style="margin: 0px; font-size: 8pt;">}</p>

<p style="margin: 0px; font-size: 8pt;"><span style="color: blue;">catch</span> (InvalidCastException ex)</p>

<p style="margin: 0px; font-size: 8pt;">{</p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; <span style="color: green;">// That's fine - it's just not a circle...</span></p>

<p style="margin: 0px; font-size: 8pt;">}</p>

<p style="margin: 0px; font-size: 8pt;">obj.Dispose();</p></div>

<p>[<em>Here is the more elegant way to code this...</em>]</p>

<p>VB.NET:</p>

<div style="background: white none repeat scroll 0%; font-size: 8pt; -moz-background-clip: -moz-initial; -moz-background-origin: -moz-initial; -moz-background-inline-policy: -moz-initial; color: black; font-family: Courier New;"><p style="margin: 0px; font-size: 8pt;"><span style="color: green;">' tr is the running transaction</span></p>

<p style="margin: 0px; font-size: 8pt;"><span style="color: green;">' objId is the object ID of the entity we want to access</span></p>

<p style="margin: 0px; font-size: 8pt;"><span style="color: blue;">Dim</span> obj <span style="color: blue;">As</span> DBObject = tr.GetObject(objId, OpenMode.ForRead)</p>

<p style="margin: 0px; font-size: 8pt;"><span style="color: blue;">If</span> <span style="color: blue;">TypeOf</span> (obj) <span style="color: blue;">Is</span> Circle <span style="color: blue;">Then</span></p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; <span style="color: blue;">Dim</span> circ <span style="color: blue;">As</span> Circle = <span style="color: blue;">CType</span>(obj, Circle)</p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; <span style="color: green;">' Access circle-specific properties/methods here</span></p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; <span style="color: green;">' ...</span></p>

<p style="margin: 0px; font-size: 8pt;"><span style="color: blue;">End</span> <span style="color: blue;">If</span></p>

<p style="margin: 0px; font-size: 8pt;">obj.Dispose()</p></div>

<p>C#:</p>

<div style="background: white none repeat scroll 0%; font-size: 8pt; -moz-background-clip: -moz-initial; -moz-background-origin: -moz-initial; -moz-background-inline-policy: -moz-initial; color: black; font-family: Courier New;"><span style="color: teal;"><div style="background: white none repeat scroll 0%; font-size: 8pt; -moz-background-clip: -moz-initial; -moz-background-origin: -moz-initial; -moz-background-inline-policy: -moz-initial; color: black; font-family: Courier New;"><p style="margin: 0px; font-size: 8pt;"><span style="color: green;">// tr is the running transaction</span></p>

<p style="margin: 0px; font-size: 8pt;"><span style="color: green;">// objId is the object ID of the entity we want to access</span></p>

<p style="margin: 0px; font-size: 8pt;"><span style="color: teal;">DBObject</span> obj = tr.GetObject(objId, <span style="color: teal;">OpenMode</span>.ForRead);</p>

<p style="margin: 0px; font-size: 8pt;"><span style="color: teal;">Circle</span> circ = obj <span style="color: blue;">as</span> <span style="color: teal;">Circle</span>;</p>

<p style="margin: 0px; font-size: 8pt;"><span style="color: blue;">if</span> (circ != <span style="color: blue;">null</span>)</p>

<p style="margin: 0px; font-size: 8pt;">{</p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; <span style="color: green;">// Access circle-specific properties/methods here</span></p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; <span style="color: green;">// ...</span></p>

<p style="margin: 0px; font-size: 8pt;">}</p>

<p style="margin: 0px; font-size: 8pt;">obj.Dispose();</p></div></span></div>

<p>So now let's plug that technique into the previous sample. All we're going to do is check whether each entity that was selected is a Circle, and if so, print out its radius and center point in addition to the common entity-level properties we listed in last time.</p>

<p>Here's the VB.NET version:</p>

<div style="background: white none repeat scroll 0%; font-size: 8pt; -moz-background-clip: -moz-initial; -moz-background-origin: -moz-initial; -moz-background-inline-policy: -moz-initial; color: black; font-family: Courier New;"><span style="color: blue;"><div style="background: white none repeat scroll 0%; font-size: 8pt; -moz-background-clip: -moz-initial; -moz-background-origin: -moz-initial; -moz-background-inline-policy: -moz-initial; color: black; font-family: Courier New;"><p style="margin: 0px; font-size: 8pt;"><span style="color: blue;">Imports</span> Autodesk.AutoCAD</p>

<p style="margin: 0px; font-size: 8pt;"><span style="color: blue;">Imports</span> Autodesk.AutoCAD.Runtime</p>

<p style="margin: 0px; font-size: 8pt;"><span style="color: blue;">Imports</span> Autodesk.AutoCAD.ApplicationServices</p>

<p style="margin: 0px; font-size: 8pt;"><span style="color: blue;">Imports</span> Autodesk.AutoCAD.DatabaseServices</p>

<p style="margin: 0px; font-size: 8pt;"><span style="color: blue;">Imports</span> Autodesk.AutoCAD.EditorInput</p><br /><p style="margin: 0px; font-size: 8pt;"><span style="color: blue;">Namespace</span> SelectionTest</p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; <span style="color: blue;">Public</span> <span style="color: blue;">Class</span> PickfirstTestCmds</p><br /><p style="margin: 0px; font-size: 8pt;">&nbsp; &nbsp; <span style="color: green;">' Must have UsePickSet specified</span></p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; &nbsp; &lt;CommandMethod(<span style="color: maroon;">&quot;PFT&quot;</span>, _</p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; &nbsp;&nbsp; &nbsp;(CommandFlags.UsePickSet _</p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="color: blue;">Or</span> CommandFlags.Redraw _</p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="color: blue;">Or</span> CommandFlags.Modal))&gt; _</p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; &nbsp; <span style="color: blue;">Public</span> <span style="color: blue;">Shared</span> <span style="color: blue;">Sub</span> PickFirstTest()</p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; &nbsp;&nbsp; &nbsp;<span style="color: blue;">Dim</span> doc <span style="color: blue;">As</span> Document = _</p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; Application.DocumentManager.MdiActiveDocument</p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; &nbsp;&nbsp; &nbsp;<span style="color: blue;">Dim</span> ed <span style="color: blue;">As</span> Editor = doc.Editor</p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; &nbsp;&nbsp; &nbsp;<span style="color: blue;">Try</span></p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="color: blue;">Dim</span> selectionRes <span style="color: blue;">As</span> PromptSelectionResult</p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; selectionRes = ed.SelectImplied</p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="color: green;">' If there's no pickfirst set available...</span></p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="color: blue;">If</span> (selectionRes.Status = PromptStatus.Error) <span style="color: blue;">Then</span></p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="color: green;">' ... ask the user to select entities</span></p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="color: blue;">Dim</span> selectionOpts <span style="color: blue;">As</span> PromptSelectionOptions</p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; selectionOpts = <span style="color: blue;">New</span> PromptSelectionOptions</p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; selectionOpts.MessageForAdding = _</p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;vbLf &amp; <span style="color: maroon;">&quot;Select objects to list: &quot;</span></p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; selectionRes = ed.GetSelection(selectionOpts)</p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="color: blue;">Else</span></p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="color: green;">' If there was a pickfirst set, clear it</span></p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; ed.SetImpliedSelection(<span style="color: blue;">Nothing</span>)</p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="color: blue;">End</span> <span style="color: blue;">If</span></p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="color: green;">' If the user has not cancelled...</span></p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="color: blue;">If</span> (selectionRes.Status = PromptStatus.OK) <span style="color: blue;">Then</span></p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="color: green;">' ... take the selected objects one by one</span></p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="color: blue;">Dim</span> tr <span style="color: blue;">As</span> Transaction = _</p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;doc.TransactionManager.StartTransaction</p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="color: blue;">Try</span></p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="color: blue;">Dim</span> objIds() <span style="color: blue;">As</span> ObjectId = _</p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; selectionRes.Value.GetObjectIds</p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="color: blue;">For</span> <span style="color: blue;">Each</span> objId <span style="color: blue;">As</span> ObjectId <span style="color: blue;">In</span> objIds</p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="color: blue;">Dim</span> obj <span style="color: blue;">As</span> <span style="color: blue;">Object</span> = _</p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; tr.GetObject(objId, OpenMode.ForRead)</p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="color: blue;">Dim</span> ent <span style="color: blue;">As</span> Entity = _</p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="color: blue;">CType</span>(obj, Entity)</p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="color: green;">' This time access the properties directly</span></p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; ed.WriteMessage(vbLf + <span style="color: maroon;">&quot;Type:&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &quot;</span> + _</p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; ent.GetType().ToString)</p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; ed.WriteMessage(vbLf + <span style="color: maroon;">&quot;&nbsp; Handle:&nbsp; &nbsp; &quot;</span> + _</p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; ent.Handle().ToString)</p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; ed.WriteMessage(vbLf + <span style="color: maroon;">&quot;&nbsp; Layer:&nbsp; &nbsp;&nbsp; &nbsp;&quot;</span> + _</p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; ent.Layer().ToString)</p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; ed.WriteMessage(vbLf + <span style="color: maroon;">&quot;&nbsp; Linetype:&nbsp; &quot;</span> + _</p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; ent.Linetype().ToString)</p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; ed.WriteMessage(vbLf + <span style="color: maroon;">&quot;&nbsp; Lineweight: &quot;</span> + _</p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; ent.LineWeight().ToString)</p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; ed.WriteMessage(vbLf + <span style="color: maroon;">&quot;&nbsp; ColorIndex: &quot;</span> + _</p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; ent.ColorIndex().ToString)</p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; ed.WriteMessage(vbLf + <span style="color: maroon;">&quot;&nbsp; Color:&nbsp; &nbsp;&nbsp; &nbsp;&quot;</span> + _</p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; ent.Color().ToString)</p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="color: green;">' Let's do a bit more for circles...</span></p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="color: blue;">If</span> <span style="color: blue;">TypeOf</span> (obj) <span style="color: blue;">Is</span> Circle <span style="color: blue;">Then</span></p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="color: green;">' Let's do a bit more for circles...</span></p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="color: blue;">Dim</span> circ <span style="color: blue;">As</span> Circle = <span style="color: blue;">CType</span>(obj, Circle)</p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; ed.WriteMessage(vbLf + <span style="color: maroon;">&quot;&nbsp; Center:&nbsp; &quot;</span> + _</p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;circ.Center.ToString)</p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; ed.WriteMessage(vbLf + <span style="color: maroon;">&quot;&nbsp; Radius:&nbsp; &quot;</span> + _</p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;circ.Radius.ToString)</p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="color: blue;">End</span> <span style="color: blue;">If</span></p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; obj.Dispose()</p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="color: blue;">Next</span></p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="color: green;">' Although no changes were made, use Commit()</span></p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="color: green;">' as this is much quicker than rolling back</span></p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;tr.Commit()</p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="color: blue;">Catch</span> ex <span style="color: blue;">As</span> Autodesk.AutoCAD.Runtime.Exception</p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;ed.WriteMessage(ex.Message)</p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;tr.Abort()</p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="color: blue;">End</span> <span style="color: blue;">Try</span></p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="color: blue;">End</span> <span style="color: blue;">If</span></p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; &nbsp;&nbsp; &nbsp;<span style="color: blue;">Catch</span> ex <span style="color: blue;">As</span> Autodesk.AutoCAD.Runtime.Exception</p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; ed.WriteMessage(ex.Message)</p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; &nbsp;&nbsp; &nbsp;<span style="color: blue;">End</span> <span style="color: blue;">Try</span></p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; &nbsp; <span style="color: blue;">End</span> <span style="color: blue;">Sub</span></p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; <span style="color: blue;">End</span> <span style="color: blue;">Class</span></p>

<p style="margin: 0px; font-size: 8pt;"><span style="color: blue;">End</span> <span style="color: blue;">Namespace</span></p></div></span></div>

<p>And here it is in C#:</p>

<div style="background: white none repeat scroll 0%; font-size: 8pt; -moz-background-clip: -moz-initial; -moz-background-origin: -moz-initial; -moz-background-inline-policy: -moz-initial; color: black; font-family: Courier New;"><span style="color: blue;"><div style="background: white none repeat scroll 0%; font-size: 8pt; -moz-background-clip: -moz-initial; -moz-background-origin: -moz-initial; -moz-background-inline-policy: -moz-initial; color: black; font-family: Courier New;"><p style="margin: 0px; font-size: 8pt;"><span style="color: blue;">using</span> Autodesk.AutoCAD;</p>

<p style="margin: 0px; font-size: 8pt;"><span style="color: blue;">using</span> Autodesk.AutoCAD.Runtime;</p>

<p style="margin: 0px; font-size: 8pt;"><span style="color: blue;">using</span> Autodesk.AutoCAD.ApplicationServices;</p>

<p style="margin: 0px; font-size: 8pt;"><span style="color: blue;">using</span> Autodesk.AutoCAD.DatabaseServices;</p>

<p style="margin: 0px; font-size: 8pt;"><span style="color: blue;">using</span> Autodesk.AutoCAD.EditorInput;</p><br /><p style="margin: 0px; font-size: 8pt;"><span style="color: blue;">namespace</span> SelectionTest</p>

<p style="margin: 0px; font-size: 8pt;">{</p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; <span style="color: blue;">public</span> <span style="color: blue;">class</span> <span style="color: teal;">PickfirstTestCmds</span></p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; {</p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; &nbsp; <span style="color: green;">// Must have UsePickSet specified</span></p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; &nbsp; [<span style="color: teal;">CommandMethod</span>(<span style="color: maroon;">&quot;PFT&quot;</span>, <span style="color: teal;">CommandFlags</span>.UsePickSet |</p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="color: teal;">CommandFlags</span>.Redraw |</p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="color: teal;">CommandFlags</span>.Modal)</p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; &nbsp; ]</p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; &nbsp; <span style="color: blue;">static</span> <span style="color: blue;">public</span> <span style="color: blue;">void</span> PickFirstTest()</p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; &nbsp; {</p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; &nbsp;&nbsp; &nbsp;<span style="color: teal;">Document</span> doc =</p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="color: teal;">Application</span>.DocumentManager.MdiActiveDocument;</p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; &nbsp;&nbsp; &nbsp;<span style="color: teal;">Editor</span> ed = doc.Editor;</p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; &nbsp;&nbsp; &nbsp;<span style="color: blue;">try</span></p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; &nbsp;&nbsp; &nbsp;{</p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="color: teal;">PromptSelectionResult</span> selectionRes =</p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; ed.SelectImplied();</p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="color: green;">// If there's no pickfirst set available...</span></p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="color: blue;">if</span> (selectionRes.Status == <span style="color: teal;">PromptStatus</span>.Error)</p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; {</p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="color: green;">// ... ask the user to select entities</span></p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="color: teal;">PromptSelectionOptions</span> selectionOpts =</p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="color: blue;">new</span> <span style="color: teal;">PromptSelectionOptions</span>();</p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; selectionOpts.MessageForAdding =</p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="color: maroon;">&quot;\nSelect objects to list: &quot;</span>;</p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; selectionRes =</p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;ed.GetSelection(selectionOpts);</p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; }</p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="color: blue;">else</span></p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; {</p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="color: green;">// If there was a pickfirst set, clear it</span></p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; ed.SetImpliedSelection(<span style="color: blue;">new</span> <span style="color: teal;">ObjectId</span>[0]);</p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; }</p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="color: green;">// If the user has not cancelled...</span></p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="color: blue;">if</span> (selectionRes.Status == <span style="color: teal;">PromptStatus</span>.OK)</p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; {</p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="color: green;">// ... take the selected objects one by one</span></p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="color: teal;">Transaction</span> tr =</p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;doc.TransactionManager.StartTransaction();</p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="color: blue;">try</span></p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; {</p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="color: teal;">ObjectId</span>[] objIds =</p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; selectionRes.Value.GetObjectIds();</p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="color: blue;">foreach</span> (<span style="color: teal;">ObjectId</span> objId <span style="color: blue;">in</span> objIds)</p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;{</p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="color: teal;">DBObject</span> obj =</p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; tr.GetObject(objId, <span style="color: teal;">OpenMode</span>.ForRead);</p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="color: teal;">Entity</span> ent = (<span style="color: teal;">Entity</span>)obj;</p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="color: green;">// This time access the properties directly</span></p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; ed.WriteMessage(<span style="color: maroon;">&quot;\nType:&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &quot;</span> +</p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; ent.GetType().ToString());</p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; ed.WriteMessage(<span style="color: maroon;">&quot;\n&nbsp; Handle:&nbsp; &nbsp; &quot;</span> +</p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; ent.Handle.ToString());</p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; ed.WriteMessage(<span style="color: maroon;">&quot;\n&nbsp; Layer:&nbsp; &nbsp;&nbsp; &nbsp;&quot;</span> +</p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; ent.Layer.ToString());</p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; ed.WriteMessage(<span style="color: maroon;">&quot;\n&nbsp; Linetype:&nbsp; &quot;</span> + </p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; ent.Linetype.ToString());</p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; ed.WriteMessage(<span style="color: maroon;">&quot;\n&nbsp; Lineweight: &quot;</span> + </p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; ent.LineWeight.ToString());</p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; ed.WriteMessage(<span style="color: maroon;">&quot;\n&nbsp; ColorIndex: &quot;</span> + </p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; ent.ColorIndex.ToString());</p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; ed.WriteMessage(<span style="color: maroon;">&quot;\n&nbsp; Color:&nbsp; &nbsp;&nbsp; &nbsp;&quot;</span> + </p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; ent.Color.ToString());</p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="color: green;">// Let's do a bit more for circles...</span></p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="color: teal;">Circle</span> circ = obj <span style="color: blue;">as</span> <span style="color: teal;">Circle</span>;</p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="color: blue;">if</span> (circ != <span style="color: blue;">null</span>)</p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; {</p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; ed.WriteMessage(<span style="color: maroon;">&quot;\n&nbsp; Center:&nbsp; &quot;</span> +</p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;circ.Center.ToString());</p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; ed.WriteMessage(<span style="color: maroon;">&quot;\n&nbsp; Radius:&nbsp; &quot;</span> +</p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;circ.Radius.ToString());</p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; }</p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; obj.Dispose();</p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;}</p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="color: green;">// Although no changes were made, use Commit()</span></p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="color: green;">// as this is much quicker than rolling back</span></p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;tr.Commit();</p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; }</p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="color: blue;">catch</span> (Autodesk.AutoCAD.Runtime.<span style="color: teal;">Exception</span> ex)</p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; {</p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;ed.WriteMessage(ex.Message);</p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;tr.Abort();</p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; }</p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; }</p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; &nbsp;&nbsp; &nbsp;}</p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; &nbsp;&nbsp; &nbsp;<span style="color: blue;">catch</span>(Autodesk.AutoCAD.Runtime.<span style="color: teal;">Exception</span> ex)</p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; &nbsp;&nbsp; &nbsp;{</p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; ed.WriteMessage(ex.Message);</p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; &nbsp;&nbsp; &nbsp;}</p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; &nbsp; }</p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; }</p>

<p style="margin: 0px; font-size: 8pt;">}</p></div></span></div>

<p>You'll notice the copious use of ToString - this just saves us having to get (and print out) the individual values making up the center point co-ordinate, for instance.</p>

<p>Let's see this running with entities selected from one of AutoCAD's sample drawings. Notice the additional data displayed for the circle object:</p>

<div style="background: white none repeat scroll 0%; font-size: 8pt; -moz-background-clip: -moz-initial; -moz-background-origin: -moz-initial; -moz-background-inline-policy: -moz-initial; color: black; font-family: Courier New;"><p style="margin: 0px; font-size: 8pt;">Command: <span style="color: red;">PFT</span></p>

<p style="margin: 0px; font-size: 8pt;">Select objects to list: 1 found</p>

<p style="margin: 0px; font-size: 8pt;">Select objects to list: 1 found, 2 total</p>

<p style="margin: 0px; font-size: 8pt;">Select objects to list: 1 found, 3 total</p>

<p style="margin: 0px; font-size: 8pt;">Select objects to list: 1 found, 4 total</p>

<p style="margin: 0px; font-size: 8pt;">Select objects to list:</p><br /><p style="margin: 0px; font-size: 8pt;">Type:&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; Autodesk.AutoCAD.DatabaseServices.Circle</p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; Handle:&nbsp; &nbsp; 1AB</p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; Layer:&nbsp; &nbsp;&nbsp; &nbsp;Visible Edges</p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; Linetype:&nbsp; Continuous</p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; Lineweight: LineWeight035</p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; ColorIndex: 179</p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; Color:&nbsp; &nbsp;&nbsp; &nbsp;38,38,89</p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; Center:&nbsp; &nbsp; (82.1742895599028,226.146274397998,0)</p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; Radius:&nbsp; &nbsp; 26</p>

<p style="margin: 0px; font-size: 8pt;">Type:&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; Autodesk.AutoCAD.DatabaseServices.Line</p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; Handle:&nbsp; &nbsp; 205</p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; Layer:&nbsp; &nbsp;&nbsp; &nbsp;Visible Edges</p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; Linetype:&nbsp; Continuous</p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; Lineweight: LineWeight035</p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; ColorIndex: 179</p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; Color:&nbsp; &nbsp;&nbsp; &nbsp;38,38,89</p>

<p style="margin: 0px; font-size: 8pt;">Type:&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; Autodesk.AutoCAD.DatabaseServices.BlockReference</p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; Handle:&nbsp; &nbsp; 531</p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; Layer:&nbsp; &nbsp;&nbsp; &nbsp;Dimensions</p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; Linetype:&nbsp; ByLayer</p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; Lineweight: ByLayer</p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; ColorIndex: 256</p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; Color:&nbsp; &nbsp;&nbsp; &nbsp;BYLAYER</p>

<p style="margin: 0px; font-size: 8pt;">Type:&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; Autodesk.AutoCAD.DatabaseServices.Hatch</p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; Handle:&nbsp; &nbsp; 26B</p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; Layer:&nbsp; &nbsp;&nbsp; &nbsp;Hatch</p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; Linetype:&nbsp; Continuous</p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; Lineweight: LineWeight009</p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; ColorIndex: 179</p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; Color:&nbsp; &nbsp;&nbsp; &nbsp;30,30,71</p>

<p style="margin: 0px; font-size: 8pt;">Command:</p></div>
