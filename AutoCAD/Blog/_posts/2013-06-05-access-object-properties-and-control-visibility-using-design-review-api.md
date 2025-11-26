---
layout: "post"
title: "Access Object Properties and control visibility using Design Review API"
date: "2013-06-05 02:55:23"
author: "Partha Sarkar"
categories:
  - ".NET"
  - "2011"
  - "2012"
  - "2013"
  - "2014"
  - "ADR API"
  - "Partha Sarkar"
original_url: "https://adndevblog.typepad.com/autocad/2013/06/access-object-properties-and-control-visibility-using-design-review-api.html "
typepad_basename: "access-object-properties-and-control-visibility-using-design-review-api"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/infrastructure/partha-sarkar.html" target="_self">Partha Sarkar</a></p>
<p><a href="http://usa.autodesk.com/design-review/">Autodesk Design Review</a> is a viewer application which allows us to view, print, annotate, and compare 2D and 3D DWF files. It has a set of public API which allows us to create custom application focused on viewing DWF files, Object properties, Printing etc. You can get more details on Autodesk Design Review API from the <a href="http://usa.autodesk.com/adsk/servlet/index?siteID=123112&amp;id=2418019">Developer Center Page</a>.&#0160;</p>
<p>In this blog post, I am sharing some code snippet using Design Review / DWF Viewer API which shows how to load a DWF file, how to get the object properties of the components of DWF object model, how to control visibility of a component in the DWF file and access the DWF file properties. Here is the screenshot of the sample Windows Forms application which embedding Design Review / DWF Viewer control and few buttons for specific task -</p>
<p>&#0160;</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b0192aac166b8970d-pi" style="float: left;"><img alt="ADR_API_01" class="asset  asset-image at-xid-6a0167607c2431970b0192aac166b8970d" src="/assets/image_566548.jpg" style="margin: 0px 5px 5px 0px;" title="ADR_API_01" /></a></p>
<p>&#0160;</p>
<p>And here is the relevant C# code snippet for each task -</p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">private</span><span style="color: blue; line-height: 140%;">void</span><span style="line-height: 140%;"> button1_Click(</span><span style="color: blue; line-height: 140%;">object</span><span style="line-height: 140%;"> sender, </span><span style="color: #2b91af; line-height: 140%;">EventArgs</span><span style="line-height: 140%;"> e)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: green; line-height: 140%;">// Option 2 - Using OpenFileDialog</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: #2b91af; line-height: 140%;">OpenFileDialog</span><span style="line-height: 140%;"> oFileDialog = </span><span style="color: blue; line-height: 140%;">new</span><span style="color: #2b91af; line-height: 140%;">OpenFileDialog</span><span style="line-height: 140%;">();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; oFileDialog.ShowDialog();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; axCExpressViewerControl1.SourcePath = oFileDialog.FileName;&#0160; &#0160; &#0160;&#0160; </span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">}</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">private</span><span style="color: blue; line-height: 140%;">void</span><span style="line-height: 140%;"> DWF_Property_Click(</span><span style="color: blue; line-height: 140%;">object</span><span style="line-height: 140%;"> sender, </span><span style="color: #2b91af; line-height: 140%;">EventArgs</span><span style="line-height: 140%;"> e)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; ECompositeViewer.</span><span style="color: #2b91af; line-height: 140%;">IAdECompositeViewer</span><span style="line-height: 140%;"> CompositeViewer;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; AdCommon.</span><span style="color: #2b91af; line-height: 140%;">IAdCollection</span><span style="line-height: 140%;"> Sections;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; CompositeViewer = (ECompositeViewer.</span><span style="color: #2b91af; line-height: 140%;">IAdECompositeViewer</span><span style="line-height: 140%;">)axCExpressViewerControl1.ECompositeViewer;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; Sections = (AdCommon.</span><span style="color: #2b91af; line-height: 140%;">IAdCollection</span><span style="line-height: 140%;">)CompositeViewer.Sections;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: #2b91af; line-height: 140%;">MessageBox</span><span style="line-height: 140%;">.Show(</span><span style="color: #a31515; line-height: 140%;">&quot;Pages Count : &quot;</span><span style="line-height: 140%;"> + Sections.Count);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: green; line-height: 140%;">//Loop through Sections Collection using foreach</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: blue; line-height: 140%;">foreach</span><span style="line-height: 140%;"> (ECompositeViewer.</span><span style="color: #2b91af; line-height: 140%;">IAdSection</span><span style="line-height: 140%;"> Section </span><span style="color: blue; line-height: 140%;">in</span><span style="line-height: 140%;"> Sections)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; {&#0160; &#0160; &#0160; &#0160; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: #2b91af; line-height: 140%;">MessageBox</span><span style="line-height: 140%;">.Show(Section.Title);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: #2b91af; line-height: 140%;">MessageBox</span><span style="line-height: 140%;">.Show(</span><span style="color: #a31515; line-height: 140%;">&quot;Order of the section \&quot;&quot;</span><span style="line-height: 140%;"> + Section.Title + </span><span style="color: #a31515; line-height: 140%;">&quot;\&quot; is &quot;</span><span style="line-height: 140%;"> + Section.Order);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">}</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">private</span><span style="color: blue; line-height: 140%;">void</span><span style="line-height: 140%;"> button_ObjProp_Click(</span><span style="color: blue; line-height: 140%;">object</span><span style="line-height: 140%;"> sender, </span><span style="color: #2b91af; line-height: 140%;">EventArgs</span><span style="line-height: 140%;"> e)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: blue; line-height: 140%;">try</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; ECompositeViewer.</span><span style="color: #2b91af; line-height: 140%;">IAdECompositeViewer</span><span style="line-height: 140%;"> compvwr =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">(ECompositeViewer.</span><span style="color: #2b91af; line-height: 140%;">IAdECompositeViewer</span><span style="line-height: 140%;">)axCExpressViewerControl1.ECompositeViewer;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; ECompositeViewer.</span><span style="color: #2b91af; line-height: 140%;">IAdSection</span><span style="line-height: 140%;"> sec = (ECompositeViewer.</span><span style="color: #2b91af; line-height: 140%;">IAdSection</span><span style="line-height: 140%;">)compvwr.Section;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; ECompositeViewer.</span><span style="color: #2b91af; line-height: 140%;">IAdContent</span><span style="line-height: 140%;"> con = (ECompositeViewer.</span><span style="color: #2b91af; line-height: 140%;">IAdContent</span><span style="line-height: 140%;">)sec.Content;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; AdCommon.</span><span style="color: #2b91af; line-height: 140%;">IAdCollection</span><span style="line-height: 140%;"> adobj = (AdCommon.</span><span style="color: #2b91af; line-height: 140%;">IAdCollection</span><span style="line-height: 140%;">)con.get_Objects(1); </span><span style="color: green; line-height: 140%;">//for selected object</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; AdCommon.</span><span style="color: #2b91af; line-height: 140%;">IAdObject</span><span style="line-height: 140%;"> obj = (AdCommon.</span><span style="color: #2b91af; line-height: 140%;">IAdObject</span><span style="line-height: 140%;">)adobj[1];</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: blue; line-height: 140%;">string</span><span style="line-height: 140%;"> str = </span><span style="color: #a31515; line-height: 140%;">&quot;&quot;</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: blue; line-height: 140%;">foreach</span><span style="line-height: 140%;"> (AdCommon.</span><span style="color: #2b91af; line-height: 140%;">IAdProperty</span><span style="line-height: 140%;"> prop </span><span style="color: blue; line-height: 140%;">in</span><span style="line-height: 140%;"> (AdCommon.</span><span style="color: #2b91af; line-height: 140%;">IAdCollection</span><span style="line-height: 140%;">)obj.Properties)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; str = str + </span><span style="color: #a31515; line-height: 140%;">&quot;Name: &quot;</span><span style="line-height: 140%;"> + prop.Name + </span><span style="color: #a31515; line-height: 140%;">&quot; Value: &quot;</span><span style="line-height: 140%;"> + prop.Value + </span><span style="color: #2b91af; line-height: 140%;">Environment</span><span style="line-height: 140%;">.NewLine;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: green; line-height: 140%;">// Properties of the selected object will be displayed in a Text Box&#0160; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; textBox1.Text = str;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: blue; line-height: 140%;">catch</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: green; line-height: 140%;">// TODO: Add your error handling here.</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; }</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">} </span><span style="color: green; line-height: 140%;">//</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">private</span><span style="color: blue; line-height: 140%;">void</span><span style="line-height: 140%;"> button_hideObj_Click(</span><span style="color: blue; line-height: 140%;">object</span><span style="line-height: 140%;"> sender, </span><span style="color: #2b91af; line-height: 140%;">EventArgs</span><span style="line-height: 140%;"> e)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: blue; line-height: 140%;">try</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; ECompositeViewer.</span><span style="color: #2b91af; line-height: 140%;">IAdECompositeViewer</span><span style="line-height: 140%;"> compvwr =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">(ECompositeViewer.</span><span style="color: #2b91af; line-height: 140%;">IAdECompositeViewer</span><span style="line-height: 140%;">)axCExpressViewerControl1.ECompositeViewer;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; ECompositeViewer.</span><span style="color: #2b91af; line-height: 140%;">IAdSection</span><span style="line-height: 140%;"> sec = (ECompositeViewer.</span><span style="color: #2b91af; line-height: 140%;">IAdSection</span><span style="line-height: 140%;">)compvwr.Section;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; ECompositeViewer.</span><span style="color: #2b91af; line-height: 140%;">IAdContent</span><span style="line-height: 140%;"> con = (ECompositeViewer.</span><span style="color: #2b91af; line-height: 140%;">IAdContent</span><span style="line-height: 140%;">)sec.Content;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; AdCommon.</span><span style="color: #2b91af; line-height: 140%;">IAdCollection</span><span style="line-height: 140%;"> adobj = (AdCommon.</span><span style="color: #2b91af; line-height: 140%;">IAdCollection</span><span style="line-height: 140%;">)con.get_Objects(1); </span><span style="color: green; line-height: 140%;">//for selected object</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; AdCommon.</span><span style="color: #2b91af; line-height: 140%;">IAdObject</span><span style="line-height: 140%;"> obj = (AdCommon.</span><span style="color: #2b91af; line-height: 140%;">IAdObject</span><span style="line-height: 140%;">)adobj[1];</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; compvwr.ExecuteCommand(</span><span style="color: #a31515; line-height: 140%;">&quot;HIDE&quot;</span><span style="line-height: 140%;">);&#0160; &#0160; &#0160; &#0160; </span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: blue; line-height: 140%;">catch</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: green; line-height: 140%;">// TODO: Add your error handling here.</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">}</span></p>
</div>
<p>&#0160;</p>
<p>Hope this is useful to you!</p>
<p>&#0160;</p>
<p>[ Updated on 20/Nov/2013 ]</p>
<p>I am updating this blog post with the following notes and screenshot to show what all components you need to reference from Autodesk Design Review to build this C# .NET code sample.</p>
<p>Autodesk Design Review 2013 <a href="http://images.autodesk.com/adsk/files/autodesk_design_review_api_2013.zip">API Reference document</a> has a topic - &quot;<em><strong>Setting References in C# .NET</strong></em>&quot; which lists the Autodesk Design Review components you need to reference in your project to build this sample.</p>
<p>&#0160;</p>
<p>Here is the screenshot of the Design Review components I have used in my project -</p>
<p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b019b015d990c970b-pi"><img alt="Autodesk_Design_Review_API_Ref" src="/assets/image_13496.jpg" title="Autodesk_Design_Review_API_Ref" /></a></p>
<p>You can also download this C# .NET sample application from here&#0160;<a href="http://adndevblog.typepad.com/files/adr2013winformappcs.zip">ADR2013WinFormAppCS</a>.&#0160;</p>
