---
layout: "post"
title: "Search model items within a volume and apply transformation"
date: "2012-05-06 20:32:38"
author: "Xiaodong Liang"
categories:
  - ".NET"
  - "Navisworks"
  - "Xiaodong Liang"
original_url: "https://adndevblog.typepad.com/aec/2012/05/search-model-items-within-a-volume-and-apply-transformation.html "
typepad_basename: "search-model-items-within-a-volume-and-apply-transformation"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/aec/xiaodong-liang.html" target="_self">Xiaodong Liang</a></p>
<p>If you want to search the model items within a volume, the bounding box could help you. Bounding Box is the extents which aligns 3D Axis. It Identifies a cuboid-shaped bounded area in 3D space.</p>
<p>- ModelGeometry.BoundingBox:&#0160; extents of a model item<br />- ModelItemCollection.BoundingBox: maximum extents of all items contained in the collection</p>
<p>The code below searches the model items whose bounding box are within the specific box and transforms them. The code is tested with &lt;Navisworks Simulate/Manage&gt;\ Samples\gatehouse\ gatehouse_pub.nwd. Its units is millimeters. The bounding box uses the units of the document Modal.Units. While the UI uses meter.</p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">using</span><span style="line-height: 140%;"> Autodesk.Navisworks.Api;</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">using</span><span style="line-height: 140%;"> ComApiBridge = Autodesk.Navisworks.Api.ComApi;</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">using</span><span style="line-height: 140%;"> ComApi = Autodesk.Navisworks.Api.Interop.ComApi;</span></p>
<p style="margin: 0px;">&#0160;</p>
</div>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">private</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">void</span><span style="line-height: 140%;"> transObjWithBox()</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{</span></p>
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">&#0160;&#0160; // get current document</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160; </span><span style="color: #2b91af; line-height: 140%;">Document</span><span style="line-height: 140%;"> oDoc =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; Autodesk.Navisworks.Api.</span><span style="color: #2b91af; line-height: 140%;">Application</span><span style="line-height: 140%;">.ActiveDocument;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160; </span><span style="color: green; line-height: 140%;">//create the box (volume) you want to check</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;</span><span style="color: #2b91af; line-height: 140%;">BoundingBox3D</span><span style="line-height: 140%;"> oNewBox = </span><span style="color: blue; line-height: 140%;">new</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">BoundingBox3D</span><span style="line-height: 140%;">(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="color: blue; line-height: 140%;">new</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">Point3D</span><span style="line-height: 140%;">(-1000, -1000, 0),</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; </span><span style="color: blue; line-height: 140%;">new</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">Point3D</span><span style="line-height: 140%;">(1000, 1000, 1000));</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;</span><span style="color: green; line-height: 140%;">// in the first model, check the items whose boxes are </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;</span><span style="color: green; line-height: 140%;">// within the specified&#0160; box.</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;</span><span style="color: #2b91af; line-height: 140%;">IEnumerable</span><span style="line-height: 140%;">&lt;</span><span style="color: #2b91af; line-height: 140%;">ModelItem</span><span style="line-height: 140%;">&gt; items =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; oDoc.Models[0].RootItem.DescendantsAndSelf.</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; Where(x =&gt;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; oNewBox.Contains(x.BoundingBox())</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; );</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: green; line-height: 140%;">// Select the items in the model that are </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: green; line-height: 140%;">// contained in the collection</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; oDoc.CurrentSelection.CopyFrom(items);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;</span><span style="color: green; line-height: 140%;">// create a collection to store the items</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: #2b91af; line-height: 140%;">ModelItemCollection</span><span style="line-height: 140%;">oModelColl =</span><span style="color: blue; line-height: 140%;">new</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">ModelItemCollection</span><span style="line-height: 140%;">();</span></p>
<p style="margin: 0px;">&#0160;<span style="line-height: 140%;"> oModelColl.CopyFrom(items);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;</span><span style="color: green; line-height: 140%;">//transform the objects. Currently this needs COM API</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;ComApi.</span><span style="color: #2b91af; line-height: 140%;">InwOpState10</span><span style="line-height: 140%;">oState =</span><span style="line-height: 140%;">ComApiBridge.</span><span style="color: #2b91af; line-height: 140%;">ComApiBridge</span><span style="line-height: 140%;">.State;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;ComApi.</span><span style="color: #2b91af; line-height: 140%;">InwOpSelection</span><span style="line-height: 140%;"> oSel =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160; ComApiBridge.</span><span style="color: #2b91af; line-height: 140%;">ComApiBridge</span><span style="line-height: 140%;">.ToInwOpSelection(oModelColl);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;</span><span style="color: green; line-height: 140%;">// create transform matrix</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;ComApi.</span><span style="color: #2b91af; line-height: 140%;">InwLTransform3f3</span><span style="line-height: 140%;"> oTrans =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; (ComApi.</span><span style="color: #2b91af; line-height: 140%;">InwLTransform3f3</span><span style="line-height: 140%;">)oState.ObjectFactory</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; (ComApi.</span><span style="color: #2b91af; line-height: 140%;">nwEObjectType</span><span style="line-height: 140%;">.eObjectType_nwLTransform3f,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="color: blue; line-height: 140%;">null</span><span style="line-height: 140%;">, </span><span style="color: blue; line-height: 140%;">null</span><span style="line-height: 140%;">);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;ComApi.</span><span style="color: #2b91af; line-height: 140%;">InwLVec3f</span><span style="line-height: 140%;"> oVec =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; (ComApi.</span><span style="color: #2b91af; line-height: 140%;">InwLVec3f</span><span style="line-height: 140%;">)oState.ObjectFactory</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; (ComApi.</span><span style="color: #2b91af; line-height: 140%;">nwEObjectType</span><span style="line-height: 140%;">.eObjectType_nwLVec3f,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="color: blue; line-height: 140%;">null</span><span style="line-height: 140%;">, </span><span style="color: blue; line-height: 140%;">null</span><span style="line-height: 140%;">);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;oVec.SetValue(5000, 5000, 0);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;oTrans.MakeTranslation(oVec);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;</span><span style="color: green; line-height: 140%;">// transform</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;oState.OverrideTransform(oSel, oTrans);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">}</span></p>
</div>
