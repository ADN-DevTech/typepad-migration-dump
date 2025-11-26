---
layout: "post"
title: "Adding TrimPlanes to Structural Members in ACA.NET"
date: "2014-10-08 02:00:00"
author: "Jeremy Tammik"
categories:
  - ".NET"
  - "AutoCAD Architecture"
  - "AutoCAD MEP"
  - "Jeremy Tammik"
original_url: "https://adndevblog.typepad.com/aec/2014/10/adding-trimplanes-to-structural-members-using-acanet.html "
typepad_basename: "adding-trimplanes-to-structural-members-using-acanet"
typepad_status: "Publish"
---

<p>By

<a href="http://adndevblog.typepad.com/cloud_and_mobile/jeremy-tammik.html">
Jeremy</a>

<a href="http://thebuildingcoder.typepad.com/blog/about-the-author.html">
Tammik</a>.</p>

<p><strong>Question:</strong> How can I add TrimPlanes to structural members using ACA .NET API?</p>

<p>I tried the

<a href="http://adndevblog.typepad.com/aec/2012/12/adding-trimplanes-to-structural-members-using-aca-net-api.html">
code provided in 2012</a> with little success.</p>

<p>When I try to write to the TrimPlanes property, e.g. like this, it returns a 'ReadOnly'" error:</p>

<pre class="code">
  member.TrimPlanes = trimPlanes
</pre>

<p>Any ideas how to fix this?</p>

<p><strong>Answer:</strong> Basically, the following sample code skeleton should work:</p>

<pre class="code">
&nbsp; trimPlanes= owner.TrimPlanes;
&nbsp;
&nbsp; plane1 = <span class="blue">new</span> TrimPlane();
&nbsp; ...
&nbsp; trimPlanes.Add(plane1);
</pre>

<p>Here is a complete command implementation sample:</p>

<pre class="code">
<style type="text/css">
.cf { font-family: Consolas; font-size: 10pt; color: black; background: white; }
.cl { margin: 0px; }
.cb1 { color: #a31515; }
.cb2 { color: blue; }
.cb3 { color: #2b91af; }
.cb4 { color: green; }
</style>
<div class="cf">
<pre class="cl">&nbsp; &nbsp; &lt;CommandMethod(<span class="cb1">&quot;testmemberTrim&quot;</span>)&gt; _</pre>
<pre class="cl">&nbsp; &nbsp; <span class="cb2">Public</span> <span class="cb2">Sub</span> testmemberTrim()</pre>
<pre class="cl">&nbsp; &nbsp; &nbsp; <span class="cb2">Dim</span> db <span class="cb2">As</span> Database = HostApplicationServices.WorkingDatabase</pre>
<pre class="cl">&nbsp; &nbsp; &nbsp; <span class="cb2">Dim</span> tm <span class="cb2">As</span> Autodesk.AutoCAD.DatabaseServices.TransactionManager = db.TransactionManager</pre>
<pre class="cl">&nbsp; &nbsp; &nbsp; <span class="cb2">Dim</span> trans <span class="cb2">As</span> <span class="cb3">Transaction</span> = tm.StartTransaction()</pre>
<pre class="cl">&nbsp; &nbsp; &nbsp; <span class="cb2">Dim</span> ed <span class="cb2">As</span> Editor = Application.DocumentManager.MdiActiveDocument.Editor</pre>
<pre class="cl">&nbsp; &nbsp; &nbsp; <span class="cb2">Try</span></pre>
<pre class="cl">&nbsp; &nbsp; &nbsp; &nbsp; <span class="cb2">Dim</span> member <span class="cb2">As</span> Member = <span class="cb2">New</span> Member()</pre>
<pre class="cl">&nbsp; &nbsp; &nbsp; &nbsp; member.MemberType = MemberType.Column</pre>
<pre class="cl">&nbsp; &nbsp; &nbsp; &nbsp; member.SetDatabaseDefaults(db)</pre>
<pre class="cl">&nbsp; &nbsp; &nbsp; &nbsp; member.SetToStandard(db)</pre>
<pre class="cl">&nbsp; &nbsp; &nbsp; &nbsp; <span class="cb4">' Set the start and end point of Member in WCS</span></pre>
<pre class="cl">&nbsp; &nbsp; &nbsp; &nbsp; member.Set(<span class="cb2">New</span> Point3d(0.0, 0.0, 0.0), <span class="cb2">New</span> Point3d(5000.0, 0.0, 0.0))</pre>
<pre class="cl">&nbsp; &nbsp; &nbsp; &nbsp; <span class="cb4">' create a trim plane at the start</span></pre>
<pre class="cl">&nbsp; &nbsp; &nbsp; &nbsp; <span class="cb2">Dim</span> ptOrigin <span class="cb2">As</span> Point3d = <span class="cb2">New</span> Point3d(1.0, 1.0, 1.0)</pre>
<pre class="cl">&nbsp; &nbsp; &nbsp; &nbsp; <span class="cb2">Dim</span> vec <span class="cb2">As</span> Vector3d = <span class="cb2">New</span> Vector3d(1, 0, 0)</pre>
<pre class="cl">&nbsp; &nbsp; &nbsp; &nbsp; <span class="cb2">Dim</span> tp1 <span class="cb2">As</span> TrimPlane = <span class="cb2">New</span> TrimPlane()</pre>
<pre class="cl">&nbsp; &nbsp; &nbsp; &nbsp; tp1.SubSetDatabaseDefaults(db)</pre>
<pre class="cl">&nbsp; &nbsp; &nbsp; &nbsp; tp1.SetToStandard(db)</pre>
<pre class="cl">&nbsp; &nbsp; &nbsp; &nbsp; tp1.End = TrimPlaneFrom.Start</pre>
<pre class="cl">&nbsp; &nbsp; &nbsp; &nbsp; tp1.Plane = <span class="cb2">New</span> <span class="cb3">Plane</span>(ptOrigin, vec.GetNormal())</pre>
<pre class="cl">&nbsp; &nbsp; &nbsp; &nbsp; member.TrimPlanes.Add(tp1)</pre>
<pre class="cl">&nbsp; &nbsp; &nbsp; &nbsp; <span class="cb4">' create another trim plane at the end</span></pre>
<pre class="cl">&nbsp; &nbsp; &nbsp; &nbsp; <span class="cb2">Dim</span> tp2 <span class="cb2">As</span> TrimPlane = <span class="cb2">New</span> TrimPlane()</pre>
<pre class="cl">&nbsp; &nbsp; &nbsp; &nbsp; tp2.SubSetDatabaseDefaults(db)</pre>
<pre class="cl">&nbsp; &nbsp; &nbsp; &nbsp; tp2.SetToStandard(db)</pre>
<pre class="cl">&nbsp; &nbsp; &nbsp; &nbsp; tp2.End = TrimPlaneFrom.End</pre>
<pre class="cl">&nbsp; &nbsp; &nbsp; &nbsp; tp2.Plane = <span class="cb2">New</span> <span class="cb3">Plane</span>(ptOrigin, vec.GetNormal())</pre>
<pre class="cl">&nbsp; &nbsp; &nbsp; &nbsp; member.TrimPlanes.Add(tp2)</pre>
<pre class="cl">&nbsp; &nbsp; &nbsp; &nbsp; <span class="cb2">Dim</span> blkTbl <span class="cb2">As</span> BlockTable = trans.GetObject(db.BlockTableId, <span class="cb3">OpenMode</span>.ForRead)</pre>
<pre class="cl">&nbsp; &nbsp; &nbsp; &nbsp; <span class="cb2">Dim</span> ms <span class="cb2">As</span> BlockTableRecord = trans.GetObject(blkTbl(BlockTableRecord.ModelSpace), <span class="cb3">OpenMode</span>.ForWrite)</pre>
<pre class="cl">&nbsp; &nbsp; &nbsp; &nbsp; ms.AppendEntity(member)</pre>
<pre class="cl">&nbsp; &nbsp; &nbsp; &nbsp; trans.AddNewlyCreatedDBObject(member, <span class="cb2">True</span>)</pre>
<pre class="cl">&nbsp; &nbsp; &nbsp; &nbsp; trans.Commit()</pre>
<pre class="cl">&nbsp; &nbsp; &nbsp; <span class="cb2">Catch</span></pre>
<pre class="cl">&nbsp; &nbsp; &nbsp; &nbsp; MsgBox(<span class="cb1">&quot;\nMember creation failed&quot;</span>)</pre>
<pre class="cl">&nbsp; &nbsp; &nbsp; &nbsp; trans.Abort()</pre>
<pre class="cl">&nbsp; &nbsp; &nbsp; <span class="cb2">Finally</span></pre>
<pre class="cl">&nbsp; &nbsp; &nbsp; &nbsp; MsgBox(<span class="cb1">&quot;\nMember created!&quot;</span>)</pre>
<pre class="cl">&nbsp; &nbsp; &nbsp; &nbsp; trans.Dispose()</pre>
<pre class="cl">&nbsp; &nbsp; &nbsp; <span class="cb2">End</span> <span class="cb2">Try</span></pre>
<pre class="cl">&nbsp; &nbsp; <span class="cb2">End</span> <span class="cb2">Sub</span></pre>
</div>
</pre>


<center>

<a class="asset-img-link"  style="display: inline;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c6efa8ee970b-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b01b7c6efa8ee970b image-full img-responsive" alt="Column trim planes" title="Column trim planes" src="/assets/image_448235.jpg" border="0" /></a><br />

</center>
