---
layout: "post"
title: "API for representation view of assembly"
date: "2012-06-14 03:09:19"
author: "Xiaodong Liang"
categories:
  - "Inventor"
  - "Xiaodong Liang"
original_url: "https://adndevblog.typepad.com/manufacturing/2012/06/api-for-representation-view-of-assembly.html "
typepad_basename: "api-for-representation-view-of-assembly"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/manufacturing/xiaodong-liang.html" target="_self">Xiaodong Liang</a></p>  <p><b>Issue</b></p>  <p>&quot;Is there an API to control Design View Representations?&#160; In the Model browser in assembly mode we see:</p>  <p>Representations   <br />&#160;&#160;&#160;&#160; --&gt;View     <br />&#160;&#160;&#160;&#160;&#160;&#160;&#160; --&gt; Private    <br />&#160;&#160;&#160;&#160;&#160;&#160;&#160; --&gt; default    <br />&#160;&#160;&#160;&#160;&#160;&#160;&#160; --&gt; view ...</p>  <p>Is there a way to get the current active view and other settings?</p>  <p><a name="section2"></a></p>  <p><b>Solution</b></p>  <p>The Inventor API supports the manipulation to Design View Representations. Here is a code example: </p>  <p>&#160;</p>  <p><strong><em>VBA code</em></strong></p>  <pre><p>Public Sub test_VBA() <p> ' get the current assembly<br /> Dim doc As AssemblyDocument<br /> Set doc = _<br />&#160;&#160;&#160; ThisApplication.ActiveDocument<br /> ' get AssemblyComponentDefinition<br /> Dim AssemblyDef As AssemblyComponentDefinition<br /> Set AssemblyDef = _<br />&#160;&#160;&#160; doc.ComponentDefinition<br /> ' get Manager of Representations<br /> Dim dViewRepMgr As RepresentationsManager<br /> Set dViewRepMgr = _<br />&#160;&#160;&#160; AssemblyDef.RepresentationsManager<br /> ' get active Representation view<br /> Dim dViewRep As DesignViewRepresentation<br /> Set dViewRep = _<br />&#160;&#160;&#160; dViewRepMgr.ActiveDesignViewRepresentation<p> Debug.Print dViewRep.Name<br />' get the first Representation view<br /> Set dViewRep = _<br />&#160;&#160;&#160; dViewRepMgr.DesignViewRepresentations.Item(1)<br /> ' activate the first view<br /> dViewRep.Activate<br />&#160; ' dump all Representations views<br />&#160; Dim dViewReps As DesignViewRepresentations<br />&#160; Set dViewReps = _<br />&#160;&#160;&#160; dViewRepMgr.DesignViewRepresentations<br />&#160; Debug.Print dViewReps.Count<br />&#160; For Each dViewRep In dViewReps<br />&#160;&#160;&#160;&#160;&#160;&#160; Debug.Print dViewRep.Name<br />&#160; Next dViewRep<br />End Sub</p></pre>

<p><em><strong>VB.NET code</strong></em></p>

<div style="font-family: courier new; background: white; color: black; font-size: 8pt">
  <p style="margin: 0px"><span style="line-height: 140%; color: blue">Public</span><span style="line-height: 140%"> </span><span style="line-height: 140%; color: blue">Sub</span><span style="line-height: 140%"> test_VBNet()</span></p>

  <p style="margin: 0px">&#160;</p>

  <p style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160; </span><span style="line-height: 140%; color: green">' assume we have had Inventor application</span></p>

  <p style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160; </span><span style="line-height: 140%; color: green">'&#160; _InvApplication</span></p>

  <p style="margin: 0px">&#160;</p>

  <p style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160; </span><span style="line-height: 140%; color: green">' get the current assembly</span></p>

  <p style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160; </span><span style="line-height: 140%; color: blue">Dim</span><span style="line-height: 140%"> doc </span><span style="line-height: 140%; color: blue">As</span><span style="line-height: 140%"> </span><span style="line-height: 140%; color: #2b91af">AssemblyDocument</span><span style="line-height: 140%"> =&#160; </span></p>

  <p style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160;&#160;&#160;&#160;&#160; _InvApplication.ActiveDocument</span></p>

  <p style="margin: 0px">&#160;</p>

  <p style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160; </span><span style="line-height: 140%; color: green">' get AssemblyComponentDefinition</span></p>

  <p style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160; </span><span style="line-height: 140%; color: blue">Dim</span><span style="line-height: 140%"> AssemblyDef </span><span style="line-height: 140%; color: blue">As</span><span style="line-height: 140%"> </span><span style="line-height: 140%; color: #2b91af">AssemblyComponentDefinition</span><span style="line-height: 140%"> =</span></p>

  <p style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; doc.ComponentDefinition</span></p>

  <p style="margin: 0px">&#160;</p>

  <p style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160; </span><span style="line-height: 140%; color: green">' get Manager of Representations</span></p>

  <p style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160; </span><span style="line-height: 140%; color: blue">Dim</span><span style="line-height: 140%"> dViewRepMgr </span><span style="line-height: 140%; color: blue">As</span><span style="line-height: 140%"> </span><span style="line-height: 140%; color: #2b91af">RepresentationsManager</span><span style="line-height: 140%"> =</span></p>

  <p style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160;&#160;&#160;&#160;&#160; AssemblyDef.RepresentationsManager</span></p>

  <p style="margin: 0px">&#160;</p>

  <p style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160;&#160;&#160;&#160;&#160; </span><span style="line-height: 140%; color: green">' get active Representation view</span></p>

  <p style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160; </span><span style="line-height: 140%; color: blue">Dim</span><span style="line-height: 140%"> dViewRep </span><span style="line-height: 140%; color: blue">As</span><span style="line-height: 140%"> </span><span style="line-height: 140%; color: #2b91af">DesignViewRepresentation</span><span style="line-height: 140%"> = </span></p>

  <p style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160;&#160;&#160;&#160;&#160; dViewRepMgr.ActiveDesignViewRepresentation</span></p>

  <p style="margin: 0px">&#160;</p>

  <p style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160; </span><span style="line-height: 140%; color: #2b91af">Debug</span><span style="line-height: 140%">.Print(dViewRep.Name)</span></p>

  <p style="margin: 0px">&#160;</p>

  <p style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160; </span><span style="line-height: 140%; color: green">' get the first Representation view</span></p>

  <p style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160; dViewRep = </span></p>

  <p style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160;&#160;&#160;&#160;&#160; dViewRepMgr.DesignViewRepresentations.Item(1)</span></p>

  <p style="margin: 0px">&#160;</p>

  <p style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160; </span><span style="line-height: 140%; color: green">' activate the first view</span></p>

  <p style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160; dViewRep.Activate()</span></p>

  <p style="margin: 0px">&#160;</p>

  <p style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160; </span><span style="line-height: 140%; color: blue">Dim</span><span style="line-height: 140%"> dViewReps </span><span style="line-height: 140%; color: blue">As</span><span style="line-height: 140%"> </span><span style="line-height: 140%; color: #2b91af">DesignViewRepresentations</span><span style="line-height: 140%"> = </span></p>

  <p style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160;&#160;&#160;&#160;&#160; dViewRepMgr.DesignViewRepresentations</span></p>

  <p style="margin: 0px">&#160;</p>

  <p style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160; </span><span style="line-height: 140%; color: #2b91af">Debug</span><span style="line-height: 140%">.Print(dViewReps.Count)</span></p>

  <p style="margin: 0px">&#160;</p>

  <p style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160;&#160; </span><span style="line-height: 140%; color: green">' dump all Representations views</span></p>

  <p style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160; </span><span style="line-height: 140%; color: blue">For</span><span style="line-height: 140%"> </span><span style="line-height: 140%; color: blue">Each</span><span style="line-height: 140%"> dViewRep </span><span style="line-height: 140%; color: blue">In</span><span style="line-height: 140%"> dViewReps</span></p>

  <p style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160;&#160;&#160;&#160;&#160; </span><span style="line-height: 140%; color: #2b91af">Debug</span><span style="line-height: 140%">.Print(dViewRep.Name)</span></p>

  <p style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160; </span><span style="line-height: 140%; color: blue">Next</span><span style="line-height: 140%"> dViewRep</span></p>

  <p style="margin: 0px">&#160;</p>

  <p style="margin: 0px"><span style="line-height: 140%; color: blue">End</span><span style="line-height: 140%"> </span><span style="line-height: 140%; color: blue">Sub</span></p>

  <p style="margin: 0px">&#160;</p>
</div>
