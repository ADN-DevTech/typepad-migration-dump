---
layout: "post"
title: "Create .NET view control at runtime on 64bits"
date: "2013-03-12 02:41:00"
author: "Xiaodong Liang"
categories:
  - ".NET"
  - "Navisworks"
  - "Xiaodong Liang"
original_url: "https://adndevblog.typepad.com/aec/2013/03/create-net-view-control-at-runtime-on-64bits.html "
typepad_basename: "create-net-view-control-at-runtime-on-64bits"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/aec/xiaodong-liang.html" target="_self">Xiaodong Liang</a>    </p>
<p>Typically, to work with the application of view control of .NET, we need to add the control to our application&#0160; (say an Windows Form) firstly. In the designer of Visual Studio, we will add the reference<strong> Autodesk.Navisworks.Controls.dll</strong>. It will generate two controls ( View control and Document control )&#0160; in the designer:</p>
<p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b017d41b8e718970c-pi"><img alt="image" border="0" height="731" src="/assets/image_780367.jpg" style="display: inline; border: 0px;" title="image" width="607" /></a> </p>
<p>&#0160;</p>
<p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b017ee92cc134970d-pi"><img alt="image" border="0" height="545" src="/assets/image_812761.jpg" style="display: inline; border: 0px;" title="image" width="242" /></a> </p>
<p>After adding the instances of the document control and view control, we can start to use them in the application. The help reference [Developer Guide] &gt;&gt; [Using the Controls] tells in details. </p>
<p>Unfortunately, this workflow is only applicable on 32bits. On 64bits,&#0160; if you try to add the reference <strong>Autodesk.Navisworks.Controls.dll</strong> in the [Designer ] &gt;&gt; [Choose Toolbox Items], you will receive an error:</p>
<p>&quot;<strong>Autodesk.Navisworks.Controls.dll</strong> is not a valid .NET module&quot;.</p>
<p>Actually, this is not an issue of Navisworks API, but a known issue of Visual Studio. In short,&#0160; the behavior is by design. Visual Studio is a 32-bit process, and therefore can only execute 32-bit modules. While Visual Studio allows you to add a reference to a 64-bit assembly, it cannot actually JIT compile it to 64-bit and execute it in process. This article of MSDN knowledge base tell more:</p>
<p><a href="http://support.microsoft.com/kb/963017" title="http://support.microsoft.com/kb/963017">http://support.microsoft.com/kb/963017</a></p>
<p>The solutions are:</p>
<p>1) either create the application on 32bits with the option [Any CPU], and copy it to 64bits. You are able to run the application directly because it will load the 64bits control at runtime. Even you can see it in the designer, so you can still re-design the view control with its properties. </p>
<p>2) or, you can add the controls at runtime. If you dig into the normal workflow of a Windows Form application, you can see each form class has one partial class in the MyForm.Designer.cs, where all the controls are initialized and added to the form. So we can mimic the workflow and add the controls dynamically. Of course, you need to be careful to set location/width/height of the view control to make it nice in visual. </p>
<div style="font-family: courier new; background: white; color: black; font-size: 8pt;">
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">static</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">class</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #2b91af;">Program</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;"> {</span></p>
</div>
<div style="font-family: courier new; background: white; color: black; font-size: 8pt;">
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">private</span><span style="line-height: 140%;"> Autodesk.Navisworks.Api.Controls.</span><span style="line-height: 140%; color: #2b91af;">DocumentControl</span><span style="line-height: 140%;"> documentControl1;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">private</span><span style="line-height: 140%;"> Autodesk.Navisworks.Api.Controls.</span><span style="line-height: 140%; color: #2b91af;">ViewControl</span><span style="line-height: 140%;"> viewControl1;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">public</span><span style="line-height: 140%;"> Form1()</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// Initialize other controls</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; InitializeComponent();</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">//Initialize document control</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">if</span><span style="line-height: 140%;">(</span><span style="line-height: 140%; color: blue;">this</span><span style="line-height: 140%;">.components == </span><span style="line-height: 140%; color: blue;">null</span><span style="line-height: 140%;">)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">this</span><span style="line-height: 140%;">.components = </span><span style="line-height: 140%; color: blue;">new</span><span style="line-height: 140%;"> System.ComponentModel.</span><span style="line-height: 140%; color: #2b91af;">Container</span><span style="line-height: 140%;">();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">this</span><span style="line-height: 140%;">.documentControl1 = </span><span style="line-height: 140%; color: blue;">new</span><span style="line-height: 140%;"> Autodesk.Navisworks.Api.Controls.</span><span style="line-height: 140%; color: #2b91af;">DocumentControl</span><span style="line-height: 140%;">(</span><span style="line-height: 140%; color: blue;">this</span><span style="line-height: 140%;">.components);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// Initialize view control</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">this</span><span style="line-height: 140%;">.viewControl1 = </span><span style="line-height: 140%; color: blue;">new</span><span style="line-height: 140%;"> Autodesk.Navisworks.Api.Controls.</span><span style="line-height: 140%; color: #2b91af;">ViewControl</span><span style="line-height: 140%;">();</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// attach the document control to the view control</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">this</span><span style="line-height: 140%;">.viewControl1.DocumentControl = </span><span style="line-height: 140%; color: blue;">this</span><span style="line-height: 140%;">.documentControl1;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">//set the location, width and height of the control</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">this</span><span style="line-height: 140%;">.viewControl1.Location = </span><span style="line-height: 140%; color: blue;">new</span><span style="line-height: 140%;"> System.Drawing.</span><span style="line-height: 140%; color: #2b91af;">Point</span><span style="line-height: 140%;">(Left + 10 , Top -10 );&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">this</span><span style="line-height: 140%;">.viewControl1.Name = </span><span style="line-height: 140%; color: #a31515;">&quot;viewControl1&quot;</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">this</span><span style="line-height: 140%;">.viewControl1.Size = </span><span style="line-height: 140%; color: blue;">new</span><span style="line-height: 140%;"> System.Drawing.</span><span style="line-height: 140%; color: #2b91af;">Size</span><span style="line-height: 140%;">(Width - 20 , Height&#0160; -20);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">this</span><span style="line-height: 140%;">.viewControl1.TabIndex = 0;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">this</span><span style="line-height: 140%;">.viewControl1.Text = </span><span style="line-height: 140%; color: #a31515;">&quot;viewControl1&quot;</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// add it to the layout</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">this</span><span style="line-height: 140%;">.Controls.Add(viewControl1); </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">}</span></p>
</div>
<p>Finally, do not forget to add the lines of initializing application of control:</p>
<div style="font-family: courier new; background: white; color: black; font-size: 8pt;">
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">static</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">void</span><span style="line-height: 140%;"> Main()</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">//Set to single document mode</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; Autodesk.Navisworks.Api.Controls.</span><span style="line-height: 140%; color: #2b91af;">ApplicationControl</span><span style="line-height: 140%;">.ApplicationType = </span><span style="line-height: 140%; color: #2b91af;">ApplicationType</span><span style="line-height: 140%;">.SingleDocument;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">//Initialise the api</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; Autodesk.Navisworks.Api.Controls.</span><span style="line-height: 140%; color: #2b91af;">ApplicationControl</span><span style="line-height: 140%;">.Initialize();</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #2b91af;">Application</span><span style="line-height: 140%;">.EnableVisualStyles();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #2b91af;">Application</span><span style="line-height: 140%;">.SetCompatibleTextRenderingDefault(</span><span style="line-height: 140%; color: blue;">false</span><span style="line-height: 140%;">);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #2b91af;">Application</span><span style="line-height: 140%;">.Run(</span><span style="line-height: 140%; color: blue;">new</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #2b91af;">Form1</span><span style="line-height: 140%;">());</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">//Finish use of the API.</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; Autodesk.Navisworks.Api.Controls.</span><span style="line-height: 140%; color: #2b91af;">ApplicationControl</span><span style="line-height: 140%;">.Terminate();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">}</span></p>
</div>
