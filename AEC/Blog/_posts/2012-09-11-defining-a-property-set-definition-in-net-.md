---
layout: "post"
title: "Defining a property set definition in .NET "
date: "2012-09-11 22:04:13"
author: "Mikako Harada"
categories:
  - ".NET"
  - "AutoCAD Architecture"
  - "Mikako Harada"
original_url: "https://adndevblog.typepad.com/aec/2012/09/defining-a-property-set-definition-in-net-.html "
typepad_basename: "defining-a-property-set-definition-in-net-"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/aec/mikako-harada.html" target="_self" title="Mikako Harada">Mikako Harada</a></p>
<p><strong>Issue</strong></p>
<p>I&#39;m trying to define a property set definition in .NET.&#0160; Could you provide a code sample? </p>
<p><strong>Solution</strong></p>
<p>Below is a code sample in VB.NET.&#0160; It&#0160;defines a property set definition&#0160;called &quot;ACADoorObjects&quot;, which applies to door objects. It adds a manual and two automatic property definitions.&#0160;</p>
<p>Not shown here, but a formula property&#0160;is possible to add programmatically. Note that&#0160;in UI, you can also&#0160;add location, classification, materials, project, anchor and graphics properties.&#0160; They are not possible with API, yet.&#0160;A workaround is to&#0160;define them in a separate dwg and import it using cloning helper utility class which I posted&#0160;earlier. &#0160;&#0160;&#0160;</p>
<div style="font-family: Consolas; font-size: 10pt; color: black; background: white;">
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; </span><span style="color: green; line-hight: 140%;">&#39; Create a minimum set of property set definition.</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; </span><span style="color: green; line-hight: 140%;">&#39;</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; </span><span style="color: green; line-hight: 140%;">&#39; Minimum error checking for code readability. </span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; </span><span style="color: green; line-hight: 140%;">&#39; In practice, you may want to check its existace, for example.</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &lt;</span><span style="color: #2b91af; line-hight: 140%;">CommandMethod</span><span style="line-hight: 140%;">(</span><span style="color: #a31515; line-hight: 140%;">&quot;ACANetScheduleLabs&quot;</span><span style="line-hight: 140%;">, </span><span style="color: #a31515; line-hight: 140%;">&quot;AcaCreatePropSetDef&quot;</span><span style="line-hight: 140%;">, _</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; </span><span style="color: #2b91af; line-hight: 140%;">CommandFlags</span><span style="line-hight: 140%;">.Modal)&gt; _</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; </span><span style="color: blue; line-hight: 140%;">Public</span><span style="line-hight: 140%;"> </span><span style="color: blue; line-hight: 140%;">Shared</span><span style="line-hight: 140%;"> </span><span style="color: blue; line-hight: 140%;">Sub</span><span style="line-hight: 140%;"> CreatePropertySetDefinition()</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; </span><span style="color: blue; line-hight: 140%;">Dim</span><span style="line-hight: 140%;"> doc </span><span style="color: blue; line-hight: 140%;">As</span><span style="line-hight: 140%;"> </span><span style="color: #2b91af; line-hight: 140%;">Document</span><span style="line-hight: 140%;"> =</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; </span><span style="color: #2b91af; line-hight: 140%;">Application</span><span style="line-hight: 140%;">.DocumentManager.MdiActiveDocument</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; </span><span style="color: blue; line-hight: 140%;">Dim</span><span style="line-hight: 140%;"> db </span><span style="color: blue; line-hight: 140%;">As</span><span style="line-hight: 140%;"> </span><span style="color: #2b91af; line-hight: 140%;">Database</span><span style="line-hight: 140%;"> = doc.Database</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; </span><span style="color: blue; line-hight: 140%;">Dim</span><span style="line-hight: 140%;"> ed </span><span style="color: blue; line-hight: 140%;">As</span><span style="line-hight: 140%;"> </span><span style="color: #2b91af; line-hight: 140%;">Editor</span><span style="line-hight: 140%;"> = doc.Editor</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; </span><span style="color: green; line-hight: 140%;">&#39; The name of the property set def. </span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; </span><span style="color: green; line-hight: 140%;">&#39; Could be for objects or styles. Hard coding for simplicity.</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; </span><span style="color: green; line-hight: 140%;">&#39; This will be the key in the dictionary </span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; </span><span style="color: blue; line-hight: 140%;">Dim</span><span style="line-hight: 140%;"> propSetDefName </span><span style="color: blue; line-hight: 140%;">As</span><span style="line-hight: 140%;"> </span><span style="color: blue; line-hight: 140%;">String</span><span style="line-hight: 140%;"> = </span><span style="color: #a31515; line-hight: 140%;">&quot;ACADoorObjects&quot;</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; </span><span style="color: green; line-hight: 140%;">&#39;Dim propSetDefName As String = &quot;ACANetDoorStyles&quot; </span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; </span><span style="color: blue; line-hight: 140%;">Try</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; </span><span style="color: green; line-hight: 140%;">&#39; (1) create prop set def </span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; </span><span style="color: blue; line-hight: 140%;">Dim</span><span style="line-hight: 140%;"> propSetDef </span><span style="color: blue; line-hight: 140%;">As</span><span style="line-hight: 140%;"> </span><span style="color: blue; line-hight: 140%;">New</span><span style="line-hight: 140%;"> </span><span style="color: #2b91af; line-hight: 140%;">PropertySetDefinition</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; propSetDef.SetToStandard(db)</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; propSetDef.SubSetDatabaseDefaults(db)</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; </span><span style="color: green; line-hight: 140%;">&#39; alternatively, you can use dictionary&#39;s NewEntry </span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; </span><span style="color: green; line-hight: 140%;">&#39;Dim dictPropSetDef = New DictionaryPropertySetDefinitions(db)</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; </span><span style="color: green; line-hight: 140%;">&#39;Dim propSetDef As PropertySetDefinition = </span></p>
<p style="margin: 0px;"><span style="color: green; line-hight: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; &#39;&#0160; dictPropSetDef.NewEntry()</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; </span><span style="color: green; line-hight: 140%;">&#39; General tab </span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; propSetDef.Description = </span><span style="color: #a31515; line-hight: 140%;">&quot;Property Set Definition by ACA.NET&quot;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; </span><span style="color: green; line-hight: 140%;">&#39; Applies To tab</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; </span><span style="color: green; line-hight: 140%;">&#39; apply to objects or styles. True if style, False if objects</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; </span><span style="color: blue; line-hight: 140%;">Dim</span><span style="line-hight: 140%;"> isStyle </span><span style="color: blue; line-hight: 140%;">As</span><span style="line-hight: 140%;"> </span><span style="color: blue; line-hight: 140%;">Boolean</span><span style="line-hight: 140%;"> = </span><span style="color: blue; line-hight: 140%;">False</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; </span><span style="color: blue; line-hight: 140%;">Dim</span><span style="line-hight: 140%;"> appliedTo = </span><span style="color: blue; line-hight: 140%;">New</span><span style="line-hight: 140%;"> </span><span style="color: #2b91af; line-hight: 140%;">StringCollection</span><span style="line-hight: 140%;">()</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; appliedTo.Add(</span><span style="color: #a31515; line-hight: 140%;">&quot;AecDbDoor&quot;</span><span style="line-hight: 140%;">)&#0160; &#0160; &#0160;&#0160; </span><span style="color: green; line-hight: 140%;">&#39; apply to a door object&#0160; </span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; </span><span style="color: green; line-hight: 140%;">&#39;appliedTo.Add(&quot;AecDbDoorStyle&quot;) &#39; apply to a door style</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; </span><span style="color: green; line-hight: 140%;">&#39; apply to more than one object type, add more here. </span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; </span><span style="color: green; line-hight: 140%;">&#39;appliedTo.Add(&quot;AecDbWindow&quot;)&#0160; &#0160; </span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; propSetDef.SetAppliesToFilter(appliedTo, isStyle)</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; </span><span style="color: green; line-hight: 140%;">&#39; Definition tab </span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; </span><span style="color: green; line-hight: 140%;">&#39; (2) we can add a set of property definitions.&#0160; </span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; </span><span style="color: green; line-hight: 140%;">&#39; We first make a container to hold them.</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; </span><span style="color: green; line-hight: 140%;">&#39; This is the main part. A property set definition can contain </span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; </span><span style="color: green; line-hight: 140%;">&#39; a set of property definition.</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; </span><span style="color: green; line-hight: 140%;">&#39; (2.1) let&#39;s first add manual property. </span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; </span><span style="color: green; line-hight: 140%;">&#39; Here we use text type </span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; </span><span style="color: blue; line-hight: 140%;">Dim</span><span style="line-hight: 140%;"> propDefManual = </span><span style="color: blue; line-hight: 140%;">New</span><span style="line-hight: 140%;"> </span><span style="color: #2b91af; line-hight: 140%;">PropertyDefinition</span><span style="line-hight: 140%;">()</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; propDefManual.SetToStandard(db)</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; propDefManual.SubSetDatabaseDefaults(db)</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; propDefManual.Name = </span><span style="color: #a31515; line-hight: 140%;">&quot;ACAManualProp&quot;</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; propDefManual.Description = </span><span style="color: #a31515; line-hight: 140%;">&quot;Manual property by ACA.NET&quot;</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; propDefManual.DataType =</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; &#0160; Autodesk.Aec.PropertyData.</span><span style="color: #2b91af; line-hight: 140%;">DataType</span><span style="line-hight: 140%;">.Text</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; propDefManual.DefaultData = </span><span style="color: #a31515; line-hight: 140%;">&quot;ACA Default&quot;</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; </span><span style="color: green; line-hight: 140%;">&#39; add to the prop set def </span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; propSetDef.Definitions.Add(propDefManual)</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; </span><span style="color: green; line-hight: 140%;">&#39; (2.2) let&#39;s add another one, automatic one this time</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; </span><span style="color: blue; line-hight: 140%;">Dim</span><span style="line-hight: 140%;"> propDefAutomatic = </span><span style="color: blue; line-hight: 140%;">New</span><span style="line-hight: 140%;"> </span><span style="color: #2b91af; line-hight: 140%;">PropertyDefinition</span><span style="line-hight: 140%;">()</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; propDefAutomatic.SetToStandard(db)</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; propDefAutomatic.SubSetDatabaseDefaults(db)</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; propDefAutomatic.Name = </span><span style="color: #a31515; line-hight: 140%;">&quot;ACAWidth&quot;</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; propDefAutomatic.Description = </span><span style="color: #a31515; line-hight: 140%;">&quot;Automatic property by ACA.NET&quot;</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; propDefAutomatic.SetAutomaticData(</span><span style="color: #a31515; line-hight: 140%;">&quot;AecDbDoor&quot;</span><span style="line-hight: 140%;">, </span><span style="color: #a31515; line-hight: 140%;">&quot;Width&quot;</span><span style="line-hight: 140%;">)</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; </span><span style="color: green; line-hight: 140%;">&#39; add to the prop set def </span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; propSetDef.Definitions.Add(propDefAutomatic)</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; </span><span style="color: green; line-hight: 140%;">&#39; similarly, add one with height </span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; propDefAutomatic = </span><span style="color: blue; line-hight: 140%;">New</span><span style="line-hight: 140%;"> </span><span style="color: #2b91af; line-hight: 140%;">PropertyDefinition</span><span style="line-hight: 140%;">()</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; propDefAutomatic.SetToStandard(db)</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; propDefAutomatic.SubSetDatabaseDefaults(db)</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; propDefAutomatic.Name = </span><span style="color: #a31515; line-hight: 140%;">&quot;ACAHeight&quot;</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; propDefAutomatic.Description = </span><span style="color: #a31515; line-hight: 140%;">&quot;Automatic property by ACA.NET&quot;</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; propDefAutomatic.SetAutomaticData(</span><span style="color: #a31515; line-hight: 140%;">&quot;AecDbDoor&quot;</span><span style="line-hight: 140%;">, </span><span style="color: #a31515; line-hight: 140%;">&quot;Height&quot;</span><span style="line-hight: 140%;">)</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; </span><span style="color: green; line-hight: 140%;">&#39;&#0160; add to the prop set def </span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; propSetDef.Definitions.Add(propDefAutomatic)</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; </span><span style="color: green; line-hight: 140%;">&#39; (3)&#0160; finally add the prop set def to the database </span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; </span><span style="color: blue; line-hight: 140%;">Using</span><span style="line-hight: 140%;"> tr </span><span style="color: blue; line-hight: 140%;">As</span><span style="line-hight: 140%;"> </span><span style="color: #2b91af; line-hight: 140%;">Transaction</span><span style="line-hight: 140%;"> =</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; &#0160; db.TransactionManager.StartTransaction</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; &#0160; </span><span style="color: green; line-hight: 140%;">&#39;&#0160; check the name </span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; &#0160; </span><span style="color: blue; line-hight: 140%;">Dim</span><span style="line-hight: 140%;"> dictPropSetDef = </span><span style="color: blue; line-hight: 140%;">New</span><span style="line-hight: 140%;"> </span><span style="color: #2b91af; line-hight: 140%;">DictionaryPropertySetDefinitions</span><span style="line-hight: 140%;">(db)</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; &#0160; </span><span style="color: blue; line-hight: 140%;">If</span><span style="line-hight: 140%;"> dictPropSetDef.Has(propSetDefName, tr) </span><span style="color: blue; line-hight: 140%;">Then</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; ed.WriteMessage(</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="color: #a31515; line-hight: 140%;">&quot;error - the property set defintion already exists: &quot;</span><span style="line-hight: 140%;"> +</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; propSetDefName + vbCrLf)</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="color: blue; line-hight: 140%;">Return</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; &#0160; </span><span style="color: blue; line-hight: 140%;">End</span><span style="line-hight: 140%;"> </span><span style="color: blue; line-hight: 140%;">If</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; &#0160; dictPropSetDef.AddNewRecord(propSetDefName, propSetDef)</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; &#0160; tr.AddNewlyCreatedDBObject(propSetDef, </span><span style="color: blue; line-hight: 140%;">True</span><span style="line-hight: 140%;">)</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; &#0160; tr.Commit()</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; </span><span style="color: blue; line-hight: 140%;">End</span><span style="line-hight: 140%;"> </span><span style="color: blue; line-hight: 140%;">Using</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; </span><span style="color: blue; line-hight: 140%;">Catch</span><span style="line-hight: 140%;"> ex </span><span style="color: blue; line-hight: 140%;">As</span><span style="line-hight: 140%;"> </span><span style="color: #2b91af; line-hight: 140%;">Exception</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; ed.WriteMessage(</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; &#0160; </span><span style="color: #a31515; line-hight: 140%;">&quot;error in CreatePropSetDef: &quot;</span><span style="line-hight: 140%;"> + ex.ToString + vbCrLf)</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; </span><span style="color: blue; line-hight: 140%;">Return</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; </span><span style="color: blue; line-hight: 140%;">End</span><span style="line-hight: 140%;"> </span><span style="color: blue; line-hight: 140%;">Try</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; ed.WriteMessage(</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; </span><span style="color: #a31515; line-hight: 140%;">&quot;property set definition &quot;</span><span style="line-hight: 140%;"> + propSetDefName +</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; </span><span style="color: #a31515; line-hight: 140%;">&quot; is successfully created.&quot;</span><span style="line-hight: 140%;"> + vbCrLf)</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; </span><span style="color: blue; line-hight: 140%;">End</span><span style="line-hight: 140%;"> </span><span style="color: blue; line-hight: 140%;">Sub</span></p>
</div>
