---
layout: "post"
title: "Using Autoloader to setup contextual ribbon tab"
date: "2014-06-10 11:26:10"
author: "Balaji"
categories:
  - ".NET"
  - "2015"
  - "AutoCAD"
  - "Balaji Ramamoorthy"
  - "UI"
original_url: "https://adndevblog.typepad.com/autocad/2014/06/using-autoloader-to-setup-contextual-ribbon-tab.html "
typepad_basename: "using-autoloader-to-setup-contextual-ribbon-tab"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/balaji-ramamoorthy.html" target="_self">Balaji Ramamoorthy</a></p>
<p>For details on displaying contextual ribbon tab, please refer to <a href="http://adndevblog.typepad.com/autocad/2012/05/using-custom-contextual-ribbon-tabs.html">this</a> blog post. In this blog post, we will look at using the Autoloader to deploy the contextual ribbon tab xaml file and the custom dll that it uses.&nbsp;In this example, the bundle implements a command to insert a smiley. The smiley is a block reference with XData to distinguish it from any other block reference. When the smiley is selected, a contextual ribbon tab defined in the CUIX is displayed. Here are the steps to get this working using the Autoloader.</p>
<p>Step 1 : Create the contextual tab selector rule xaml file.</p>
<p>&nbsp;</p>
<!-- Start block. Created with Code4Blog for Microsoft Visual Studio 2010. Copyright (c)2010 Vitaly Zayko http://zayko.net -->
<div style="color:black;overflow:auto;width:99.5%;">
<pre style="margin:0em;"> &lt;?xml version=<span style="color:#a31515">&quot;1.0&quot;</span><span style="color:#000000">  encoding=&quot;utf-8&quot;?&gt;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> &lt;TabSelectorRules </pre>
<pre style="margin:0em;"> xmlns=&quot;clr-namespace:Autodesk.AutoCAD.Ribbon;assembly=AcWindows&quot; </pre>
<pre style="margin:0em;"> Ordering=&quot;0&quot;&gt;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> &lt;TabSelectorRules.References&gt;</pre>
<pre style="margin:0em;"> &lt;AssemblyReference <span style="color:#0000ff">Namespace</span><span style="color:#000000"> =&quot;Aen1ContextualTabSelectorHelper&quot; </pre>
<pre style="margin:0em;">                     <span style="color:#0000ff">Assembly</span><span style="color:#000000"> =&quot;Aen1ContextualTabSelectorHelper&quot;/&gt;</pre>
<pre style="margin:0em;"> &lt;/TabSelectorRules.References&gt;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> &lt;Rule Uid=<span style="color:#a31515">&quot;Aen1SelectionRule&quot;</span><span style="color:#000000">  DisplayName=&quot;Aen1 Selection Rule&quot; </pre>
<pre style="margin:0em;">                             Theme=&quot;Green&quot; Trigger=&quot;Selection&quot;&gt;</pre>
<pre style="margin:0em;">     &lt;![CDATA[</pre>
<pre style="margin:0em;">     Aen1ContextualTabSelectorHelper.Methods.ShowMyTab(Selection)</pre>
<pre style="margin:0em;">     ]]&gt;</pre>
<pre style="margin:0em;"> &lt;/Rule&gt;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> &lt;/TabSelectorRules&gt;</pre>
<pre style="margin:0em;"> </pre>
</div>
<!-- End block -->
<p>Step 2 : Create a custom dll that our contextual tab selector rule will use to determine if the contextual tab is to be displayed. In this example, we look for block references that contain a specific XData.</p>
<p><span style="color: #0000ff;">namespace</span><span style="color: #000000;"> Aen1ContextualTabSelectorHelper</span></p>
<div style="color: black; overflow: auto; width: 99.5%;">
<pre style="margin: 0em;"> <span style="color: #000000;">{</span></pre>
<pre style="margin: 0em;">         <span style="color: #0000ff;">Public</span><span style="color: #000000;">  <span style="color: #0000ff;">Class</span><span style="color: #000000;">  <span style="color: #2b91af;">Methods</span></span></span></pre>
<pre style="margin: 0em;">     <span style="color: #000000;">{</span></pre>
<pre style="margin: 0em;">         <span style="color: #0000ff;">public</span><span style="color: #000000;">  <span style="color: #0000ff;">static</span><span style="color: #000000;">  bool ShowMyTab(object selObj)</span></span></pre>
<pre style="margin: 0em;">         <span style="color: #000000;">{</span></pre>
<pre style="margin: 0em;">             bool showTab = <span style="color: #0000ff;">false</span><span style="color: #000000;"> ;</span></pre>
<pre style="margin: 0em;">&nbsp;</pre>
<pre style="margin: 0em;">             Selection sel = (Selection)selObj;</pre>
<pre style="margin: 0em;">             <span style="color: #0000ff;">if</span><span style="color: #000000;">  (sel.Count != 1 || !sel.ContainsOnly</span></pre>
<pre style="margin: 0em;">                     (new <span style="color: #0000ff;">string</span><span style="color: #000000;"> [] <span style="color: #000000;">{</span> "BlockReference" <span style="color: #000000;">}</span>))</span></pre>
<pre style="margin: 0em;">                 <span style="color: #0000ff;">return</span><span style="color: #000000;">  showTab;</span></pre>
<pre style="margin: 0em;">&nbsp;</pre>
<pre style="margin: 0em;">             IDataItem item = sel[0] <span style="color: #0000ff;">as</span><span style="color: #000000;">  IDataItem;</span></pre>
<pre style="margin: 0em;">             <span style="color: #0000ff;">if</span><span style="color: #000000;"> (item == null)</span></pre>
<pre style="margin: 0em;">                 <span style="color: #0000ff;">return</span><span style="color: #000000;">  showTab;</span></pre>
<pre style="margin: 0em;">&nbsp;</pre>
<pre style="margin: 0em;">             IDataItemTransaction trans </pre>
<pre style="margin: 0em;">                     = item.StartTransaction(OpenMode.ForRead);</pre>
<pre style="margin: 0em;">             <span style="color: #0000ff;">if</span><span style="color: #000000;">  (trans != null)</span></pre>
<pre style="margin: 0em;">             <span style="color: #000000;">{</span></pre>
<pre style="margin: 0em;">                 BlockReference bref </pre>
<pre style="margin: 0em;">                             = trans.Item <span style="color: #0000ff;">as</span><span style="color: #000000;">  BlockReference;</span></pre>
<pre style="margin: 0em;">&nbsp;</pre>
<pre style="margin: 0em;">                 <span style="color: #0000ff;">if</span><span style="color: #000000;">  (bref != null)</span></pre>
<pre style="margin: 0em;">                 <span style="color: #000000;">{</span></pre>
<pre style="margin: 0em;">                     ResultBuffer resBuf </pre>
<pre style="margin: 0em;">                         = bref.GetXDataForApplication("SMILEY");</pre>
<pre style="margin: 0em;">&nbsp;</pre>
<pre style="margin: 0em;">                     <span style="color: #0000ff;">if</span><span style="color: #000000;">  (resBuf != null)</span></pre>
<pre style="margin: 0em;">                     <span style="color: #000000;">{</span></pre>
<pre style="margin: 0em;">                         // Show tab <span style="color: #0000ff;">if</span><span style="color: #000000;">  the block reference</span></pre>
<pre style="margin: 0em;">                         // has our XData</pre>
<pre style="margin: 0em;">                         showTab = <span style="color: #0000ff;">true</span><span style="color: #000000;"> ;</span></pre>
<pre style="margin: 0em;">                         resBuf.Dispose();</pre>
<pre style="margin: 0em;">                     <span style="color: #000000;">}</span></pre>
<pre style="margin: 0em;">                 <span style="color: #000000;">}</span></pre>
<pre style="margin: 0em;">                 trans.Commit();</pre>
<pre style="margin: 0em;">             <span style="color: #000000;">}</span></pre>
<pre style="margin: 0em;">&nbsp;</pre>
<pre style="margin: 0em;">             <span style="color: #0000ff;">return</span><span style="color: #000000;">  showTab;</span></pre>
<pre style="margin: 0em;">         <span style="color: #000000;">}</span></pre>
<pre style="margin: 0em;">     <span style="color: #000000;">}</span></pre>
<pre style="margin: 0em;"> <span style="color: #000000;">}</span></pre>
<pre style="margin: 0em;">&nbsp;</pre>
</div>
<p>Step 3 : Modify the PackageContents.xml of your Autoloader bundle to load the Contextual tab selector rule xaml and its custom dll during startup.</p>
<p>&lt;ComponentEntry&nbsp;<br />AppName="MyTestPlugin1"&nbsp;<br />AppDescription="Aen1 Contextual Tab Rule"&nbsp;<br />ModuleName="./Contents/Windows/Aen1ContextualTabSelectorRules.xaml"&nbsp;<br />XamlType="ContextualTabRule"/&gt;</p>
<p>&lt;ComponentEntry&nbsp;<br />AppName="MyTestPlugin1"&nbsp;<br />AppDescription="Aen1 Contextual Tab Rule"&nbsp;<br />ModuleName="./Contents/Windows/Aen1ContextualTabSelectorHelper.dll"&nbsp;<br />LoadOnAutoCADStartup="True"/&gt;</p>
<p>The complete Autoloader bundle can be downloaded here :</p>
<span class="asset  asset-generic at-xid-6a0167607c2431970b01a73dd632bf970d img-responsive"><a href="http://adndevblog.typepad.com/files/mytestplugin.bundle.zip">Download MyTestPlugin.bundle</a></span>
<p>To try the bundle, run the "InsertSmiley" command and place a smiley. Zoom to extents if necessary. Select the smiley and a contextual tab should appear as shown in the screenshot.</p>
<p></p>
<a class="asset-img-link"  style="float: left;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01a3fd1b75f3970b-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b01a3fd1b75f3970b img-responsive" alt="Smiley" title="Smiley" src="/assets/image_90446.jpg" style="margin: 0px 5px 5px 0px;" /></a>
