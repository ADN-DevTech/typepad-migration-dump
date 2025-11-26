---
layout: "post"
title: "Set Parcel UserDefined Property using Civil 3D API"
date: "2012-08-22 03:25:21"
author: "Partha Sarkar"
categories:
  - ".NET"
  - "AutoCAD Civil 3D 2011"
  - "AutoCAD Civil 3D 2012"
  - "AutoCAD Civil 3D 2013"
  - "Partha Sarkar"
original_url: "https://adndevblog.typepad.com/infrastructure/2012/08/set-parcel-userdefined-property-from-api.html "
typepad_basename: "set-parcel-userdefined-property-from-api"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/infrastructure/partha-sarkar.html" target="_self">Partha Sarkar</a></p>
<p>One of our
valued Civil 3D customer and application developer had asked the following question:</p>
<p><span style="font-family: tahoma, arial, helvetica, sans-serif;"><em>I am trying to set the value of the existing userdefine​d
property &quot;Address&quot; of Parcels using the com method SetUserDefinedPropertyValue
but have had no luck.....</em></span></p>
<p>&#0160;</p>
<p>If you have the same question and looking for a solution
using the <strong>SetUserDefinedPropertyValue()</strong> API, here you go :</p>
<p>&#0160;</p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">//start a transaction </span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">using</span><span style="line-height: 140%;"> (</span><span style="color: #2b91af; line-height: 140%;">Transaction</span><span style="line-height: 140%;"> trans = db.TransactionManager.StartTransaction())</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: green; line-height: 140%;">//get the list of properties</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; Autodesk.AECC.Interop.Land.</span><span style="color: #2b91af; line-height: 140%;">AeccUserDefinedPropertyClassification</span><span style="line-height: 140%;"> parcelUDPClass =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; aeccDb.ParcelUserDefinedPropertyClassifications.Item(0);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: green; line-height: 140%;">//get one property - create or access</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; Autodesk.AECC.Interop.Land.</span><span style="color: #2b91af; line-height: 140%;">AeccUserDefinedProperty</span><span style="line-height: 140%;"> propItem;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;"> (parcelUDPClass.UserDefinedProperties.Count == 0)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: green; line-height: 140%;">//create a new property</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; propItem = propItem = parcelUDPClass.UserDefinedProperties.Add(</span><span style="color: #a31515; line-height: 140%;">&quot;Address&quot;</span><span style="line-height: 140%;">, </span><span style="color: #a31515; line-height: 140%;">&quot;DESC&quot;</span><span style="line-height: 140%;">,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; Autodesk.AECC.Interop.Land.</span><span style="color: #2b91af; line-height: 140%;">AeccUDPPropertyFieldType</span><span style="line-height: 140%;">.aeccUDPPropertyFieldTypeInteger,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; </span><span style="color: blue; line-height: 140%;">true</span><span style="line-height: 140%;">, </span><span style="color: blue; line-height: 140%;">false</span><span style="line-height: 140%;">, 10, </span><span style="color: blue; line-height: 140%;">true</span><span style="line-height: 140%;">, </span><span style="color: blue; line-height: 140%;">false</span><span style="line-height: 140%;">, 20, </span><span style="color: blue; line-height: 140%;">true</span><span style="line-height: 140%;">, 15, </span><span style="color: blue; line-height: 140%;">null</span><span style="line-height: 140%;">);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: blue; line-height: 140%;">else</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: green; line-height: 140%;">//or access an existing one</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; propItem = parcelUDPClass.UserDefinedProperties.Item(0);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: green; line-height: 140%;">//apply classification changes</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; aeccDb.Sites.Item(0).Parcels.Properties.SetUserDefinedPropertyClassification(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; Autodesk.AECC.Interop.Land.</span><span style="color: #2b91af; line-height: 140%;">AeccUDPClassificationApplyWay</span><span style="line-height: 140%;">.aeccUDPClassificationApplyAll, </span><span style="color: #a31515; line-height: 140%;">&quot;Unclassified&quot;</span><span style="line-height: 140%;">);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: green; line-height: 140%;">//assuming the drawing contain at least one site, with at least one parcel</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;"> (aeccDb.Sites.Item(0).Parcels.Count &gt; 0)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; Autodesk.AECC.Interop.Land.</span><span style="color: #2b91af; line-height: 140%;">AeccParcel</span><span style="line-height: 140%;"> parcel = aeccDb.Sites.Item(0).Parcels.Item(0);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: green; line-height: 140%;">//get the property by name</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: blue; line-height: 140%;">object</span><span style="line-height: 140%;"> value = parcel.GetUserDefinedPropertyValue(propItem.Name);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: green; line-height: 140%;">//also the SET the value</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; parcel.SetUserDefinedPropertyValue(propItem.Name, 13);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; }</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; trans.Commit();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">}</span></p>
</div>
<p>&#0160;</p>
<p>Here is the result of running the above code snippet:</p>
<p>&#0160;</p>
<p>
<a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b0176175fdeaf970c-pi" style="display: inline;"><img alt="Parcel_UDP" class="asset  asset-image at-xid-6a0167607c2431970b0176175fdeaf970c" src="/assets/image_8d9997.jpg" title="Parcel_UDP" /></a><br /><br /></p>
<p>&#0160;</p>
<p>Hope this helps!</p>
