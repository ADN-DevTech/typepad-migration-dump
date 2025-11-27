---
layout: "post"
title: "Listing the Positional Representations in iLogic Form"
date: "2015-07-05 10:16:12"
author: "Balaji"
categories:
  - "Balaji Ramamoorthy"
  - "iLogic"
  - "Inventor"
original_url: "https://adndevblog.typepad.com/manufacturing/2015/07/listing-the-positional-representations-in-ilogic-form.html "
typepad_basename: "listing-the-positional-representations-in-ilogic-form"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/manufacturing/balaji-ramamoorthy.html" target="_self">Balaji Ramamoorthy</a></p>
<p>The Positional Representations in an assembly document can change as you add or remove any of the representations. If you need to list the names of the representations in a combo-box inside your iLogic form, here is a way to do that.</p>
<p>Step-1 : Create a Multi-value user parameter of Text type. In the below example, we name this parameter as "PosReps"</p>
<p>Step-2 : In your iLogic form, drag the user-parameter created in Step-1. As it is a multi-value parameter, a combo-box will display its values</p>
<p>Step-3 : In your iLogic Rule, before you display the iLogic form, update the user parameter by iterating through the positional representation in the assembly document. Here is a sample iLogic rule that updates the user parameter named "PosReps" :</p>
<p></p>
<!-- Start block. Created with Code4Blog for Microsoft Visual Studio 2010. Copyright (c)2010 Vitaly Zayko http://zayko.net -->
<div style="color:black;overflow:auto;width:99.5%;">
<pre style="margin:0em;"> <span style="color:#0000ff">Dim</span><span style="color:#000000">  oAsmDoc <span style="color:#0000ff">As</span><span style="color:#000000">  <span style="color:#2b91af">AssemblyDocument</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> oAsmDoc = ThisApplication.ActiveDocument</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> <span style="color:#0000ff">Dim</span><span style="color:#000000">  oAsmCompDef <span style="color:#0000ff">As</span><span style="color:#000000">  <span style="color:#2b91af">AssemblyComponentDefinition</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> oAsmCompDef = oAsmDoc.ComponentDefinition</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> <span style="color:#0000ff">Dim</span><span style="color:#000000">  oPosReps <span style="color:#0000ff">As</span><span style="color:#000000">  <span style="color:#2b91af">PositionalRepresentations</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> oPosReps = oAsmCompDef.RepresentationsManager _</pre>
<pre style="margin:0em;"> .PositionalRepresentations</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> <span style="color:#0000ff">Dim</span><span style="color:#000000">  List() <span style="color:#0000ff">As</span><span style="color:#000000">  <span style="color:#0000ff">String</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> <span style="color:#0000ff">ReDim</span><span style="color:#000000">  List(0 <span style="color:#0000ff">To</span><span style="color:#000000">  oPosReps.Count-1)</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> <span style="color:#0000ff">Dim</span><span style="color:#000000">  cnt <span style="color:#0000ff">As</span><span style="color:#000000">  <span style="color:#0000ff">Integer</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> cnt = 0</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> <span style="color:#0000ff">Dim</span><span style="color:#000000">  oPosRep <span style="color:#0000ff">As</span><span style="color:#000000">  <span style="color:#2b91af">PositionalRepresentation</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> <span style="color:#0000ff">For</span><span style="color:#000000">  <span style="color:#0000ff">Each</span><span style="color:#000000">  oPosRep <span style="color:#0000ff">In</span><span style="color:#000000">  oPosReps</pre>
<pre style="margin:0em;"> 	List(cnt) = oPosRep.Name</pre>
<pre style="margin:0em;"> 	cnt = cnt + 1</pre>
<pre style="margin:0em;"> <span style="color:#0000ff">Next</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> <span style="color:#0000ff">Dim</span><span style="color:#000000">  oPars <span style="color:#0000ff">As</span><span style="color:#000000">  <span style="color:#2b91af">UserParameters</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> oPars = _</pre>
<pre style="margin:0em;"> oAsmDoc.ComponentDefinition.Parameters.UserParameters</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> <span style="color:#0000ff">Dim</span><span style="color:#000000">  oPar <span style="color:#0000ff">As</span><span style="color:#000000">  <span style="color:#2b91af">UserParameter</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> oPar = oPars.Item(&quot;PosReps&quot;)</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> <span style="color:#0000ff">Dim</span><span style="color:#000000">  oExprList <span style="color:#0000ff">As</span><span style="color:#000000">  <span style="color:#2b91af">ExpressionList</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> oExprList = oPar.ExpressionList</pre>
<pre style="margin:0em;"> <span style="color:#0000ff">Call</span><span style="color:#000000">  oExprList.SetExpressionList(List, <span style="color:#0000ff">False</span><span style="color:#000000"> )</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> oAsmDoc.Update</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> iLogicForm.Show(&quot;Positional Reps&quot;)</pre>
<pre style="margin:0em;"> </pre>
</div>
<!-- End block -->
<p>Here is a screenshot of the iLogic form displaying the positional representations:
</p>
<a class="asset-img-link"  style="display: inline;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb084da9c5970d-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b01bb084da9c5970d image-full img-responsive" alt="ILogic_PosReps" title="ILogic_PosReps" src="/assets/image_5898ff.jpg" border="0" /></a><br />
