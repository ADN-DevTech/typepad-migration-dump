---
layout: "post"
title: "Modifying an AutoCAD object&rsquo;s state via a dynamic property defined using .NET"
date: "2009-05-06 14:29:55"
author: "Kean Walmsley"
categories:
  - "AutoCAD"
  - "AutoCAD .NET"
  - "Custom objects"
  - "Documents"
  - "Graphics system"
  - "Notification / Events"
  - "Object properties"
  - "Overrules"
  - "Solid modeling"
  - "User interface"
original_url: "https://www.keanw.com/2009/05/modifying-an-autocad-objects-state-via-a-dynamic-property-defined-in-net.html "
typepad_basename: "modifying-an-autocad-objects-state-via-a-dynamic-property-defined-in-net"
typepad_status: "Publish"
---

<p>I’ve been meaning to get to this one for a while. This post takes the <a href="http://through-the-interface.typepad.com/through_the_interface/2009/03/exposing-autocads-properties-palette-functionality-to-net---part-1.html">OPM</a> <a href="http://through-the-interface.typepad.com/through_the_interface/2009/03/exposing-autocads-properties-palette-functionality-to-net---part-2.html">.NET</a> implementation and shows how to use it to allow modification of data persisted with an object: in this case we’re going to use the XData in which we store the “pipe radius” for <a href="http://through-the-interface.typepad.com/through_the_interface/2009/05/using-an-autocad-2010-overrule-to-control-the-copying-of-xdata-using-net.html">the AutoCAD 2010 overrule sample</a> we’ve recently been developing.</p>
<p>To start with, I needed to migrate the OPM .NET module to work with AutoCAD 2010, which meant installing Visual Studio 2008 SP1. Other than that the code migrated very easily, and the project (with the built asdkOPMNetExt.dll assembly) can be found <a href="http://through-the-interface.typepad.com/files/OPM_NET_2010.zip">here</a>. I recommend placing the module in AutoCAD’s&#0160;main program folder and having it <a href="http://through-the-interface.typepad.com/through_the_interface/2006/09/automatic_loadi.html">demand-load</a> on AutoCAD startup (if you choose to use it).</p>
<p>[A quick comment on that, as I know some people dislike doing this... it&#39;s highly recommended to place your .NET assemblies in AutoCAD&#39;s main program folder: you will avoid a whole category of subtle&#0160;problems by doing so. You needn&#39;t feel it&#39;s dangerous as long as you prefix the filename of each of your modules with your <a href="http://www.autodesk.com/symbolreg">Registered Developer Symbol (RDS)</a>.]</p>
<p>Here’s the C# code to add to our overrule application:</p>
<div style="FONT-FAMILY: courier new; BACKGROUND: white; COLOR: black; FONT-SIZE: 8pt">
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%; COLOR: blue">using</span><span style="LINE-HEIGHT: 140%"> Autodesk.AutoCAD.ApplicationServices;</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%; COLOR: blue">using</span><span style="LINE-HEIGHT: 140%"> Autodesk.AutoCAD.DatabaseServices;</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%; COLOR: blue">using</span><span style="LINE-HEIGHT: 140%"> Autodesk.AutoCAD.Runtime;</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%; COLOR: blue">using</span><span style="LINE-HEIGHT: 140%"> Autodesk.AutoCAD.Windows.OPM;</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%; COLOR: blue">using</span><span style="LINE-HEIGHT: 140%"> Autodesk.AutoCAD.Interop.Common;</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%; COLOR: blue">using</span><span style="LINE-HEIGHT: 140%"> System.Runtime.InteropServices;</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%; COLOR: blue">using</span><span style="LINE-HEIGHT: 140%"> System.Reflection;</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%; COLOR: blue">using</span><span style="LINE-HEIGHT: 140%"> System;</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%; COLOR: blue">using</span><span style="LINE-HEIGHT: 140%"> DrawOverrules;</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160;</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%; COLOR: blue">namespace</span><span style="LINE-HEIGHT: 140%"> PropertyEditing</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">{</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%; COLOR: blue">&#0160; #region</span><span style="LINE-HEIGHT: 140%"> Our Custom Property</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; [</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; Guid(</span><span style="LINE-HEIGHT: 140%; COLOR: #a31515">&quot;E64CAA14-EA92-46ea-82D6-420FA873F16F&quot;</span><span style="LINE-HEIGHT: 140%">),</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">ProgId</span><span style="LINE-HEIGHT: 140%">(</span><span style="LINE-HEIGHT: 140%; COLOR: #a31515">&quot;OverruleSample.PipeRadius.1&quot;</span><span style="LINE-HEIGHT: 140%">),</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">ClassInterface</span><span style="LINE-HEIGHT: 140%">(</span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">ClassInterfaceType</span><span style="LINE-HEIGHT: 140%">.None),</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">ComDefaultInterface</span><span style="LINE-HEIGHT: 140%">(</span><span style="LINE-HEIGHT: 140%; COLOR: blue">typeof</span><span style="LINE-HEIGHT: 140%">(</span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">IDynamicProperty2</span><span style="LINE-HEIGHT: 140%">)),</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">ComVisible</span><span style="LINE-HEIGHT: 140%">(</span><span style="LINE-HEIGHT: 140%; COLOR: blue">true</span><span style="LINE-HEIGHT: 140%">)</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; ]</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">public</span><span style="LINE-HEIGHT: 140%"> </span><span style="LINE-HEIGHT: 140%; COLOR: blue">class</span><span style="LINE-HEIGHT: 140%"> </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">CustomProp</span><span style="LINE-HEIGHT: 140%"> : </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">IDynamicProperty2</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; {</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">private</span><span style="LINE-HEIGHT: 140%"> </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">IDynamicPropertyNotify2</span><span style="LINE-HEIGHT: 140%"> m_pSink = </span><span style="LINE-HEIGHT: 140%; COLOR: blue">null</span><span style="LINE-HEIGHT: 140%">;</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160;</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// Unique property ID</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160;</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">public</span><span style="LINE-HEIGHT: 140%"> </span><span style="LINE-HEIGHT: 140%; COLOR: blue">void</span><span style="LINE-HEIGHT: 140%"> GetGUID(</span><span style="LINE-HEIGHT: 140%; COLOR: blue">out</span><span style="LINE-HEIGHT: 140%"> </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">Guid</span><span style="LINE-HEIGHT: 140%"> propGUID)</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; {</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160; propGUID =</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">new</span><span style="LINE-HEIGHT: 140%"> </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">Guid</span><span style="LINE-HEIGHT: 140%">(</span><span style="LINE-HEIGHT: 140%; COLOR: #a31515">&quot;F60AE3DA-0373-4d24-82D2-B2646517ABCB&quot;</span><span style="LINE-HEIGHT: 140%">);</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; }</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160;</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// Property display name</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160;</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">public</span><span style="LINE-HEIGHT: 140%"> </span><span style="LINE-HEIGHT: 140%; COLOR: blue">void</span><span style="LINE-HEIGHT: 140%"> GetDisplayName(</span><span style="LINE-HEIGHT: 140%; COLOR: blue">out</span><span style="LINE-HEIGHT: 140%"> </span><span style="LINE-HEIGHT: 140%; COLOR: blue">string</span><span style="LINE-HEIGHT: 140%"> szName)</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; {</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160; szName = </span><span style="LINE-HEIGHT: 140%; COLOR: #a31515">&quot;Pipe radius&quot;</span><span style="LINE-HEIGHT: 140%">;</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; }</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160;</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// Show/Hide property in the OPM, for this object instance</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160;</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">public</span><span style="LINE-HEIGHT: 140%"> </span><span style="LINE-HEIGHT: 140%; COLOR: blue">void</span><span style="LINE-HEIGHT: 140%"> IsPropertyEnabled(</span><span style="LINE-HEIGHT: 140%; COLOR: blue">object</span><span style="LINE-HEIGHT: 140%"> pUnk, </span><span style="LINE-HEIGHT: 140%; COLOR: blue">out</span><span style="LINE-HEIGHT: 140%"> </span><span style="LINE-HEIGHT: 140%; COLOR: blue">int</span><span style="LINE-HEIGHT: 140%"> bEnabled)</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; {</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160; bEnabled = 1;</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; }</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160;</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// Is property showing but disabled</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160;</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">public</span><span style="LINE-HEIGHT: 140%"> </span><span style="LINE-HEIGHT: 140%; COLOR: blue">void</span><span style="LINE-HEIGHT: 140%"> IsPropertyReadOnly(</span><span style="LINE-HEIGHT: 140%; COLOR: blue">out</span><span style="LINE-HEIGHT: 140%"> </span><span style="LINE-HEIGHT: 140%; COLOR: blue">int</span><span style="LINE-HEIGHT: 140%"> bReadonly)</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; {</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160; bReadonly = 0;</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; }</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160;</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// Get the property description string</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160;</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">public</span><span style="LINE-HEIGHT: 140%"> </span><span style="LINE-HEIGHT: 140%; COLOR: blue">void</span><span style="LINE-HEIGHT: 140%"> GetDescription(</span><span style="LINE-HEIGHT: 140%; COLOR: blue">out</span><span style="LINE-HEIGHT: 140%"> </span><span style="LINE-HEIGHT: 140%; COLOR: blue">string</span><span style="LINE-HEIGHT: 140%"> szName)</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; {</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160; szName =</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: #a31515">&quot;Radius of the pipe profile applied to this linear entity.&quot;</span><span style="LINE-HEIGHT: 140%">;</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; }</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160;</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// OPM will typically display these in an edit field</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// optional: meta data representing property type name,</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// ex. ACAD_ANGLE</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160;</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">public</span><span style="LINE-HEIGHT: 140%"> </span><span style="LINE-HEIGHT: 140%; COLOR: blue">void</span><span style="LINE-HEIGHT: 140%"> GetCurrentValueName(</span><span style="LINE-HEIGHT: 140%; COLOR: blue">out</span><span style="LINE-HEIGHT: 140%"> </span><span style="LINE-HEIGHT: 140%; COLOR: blue">string</span><span style="LINE-HEIGHT: 140%"> szName)</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; {</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">throw</span><span style="LINE-HEIGHT: 140%"> </span><span style="LINE-HEIGHT: 140%; COLOR: blue">new</span><span style="LINE-HEIGHT: 140%"> System.</span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">NotImplementedException</span><span style="LINE-HEIGHT: 140%">();</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; }</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160;</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// What is the property type, ex. VT_R8</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160;</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">public</span><span style="LINE-HEIGHT: 140%"> </span><span style="LINE-HEIGHT: 140%; COLOR: blue">void</span><span style="LINE-HEIGHT: 140%"> GetCurrentValueType(</span><span style="LINE-HEIGHT: 140%; COLOR: blue">out</span><span style="LINE-HEIGHT: 140%"> </span><span style="LINE-HEIGHT: 140%; COLOR: blue">ushort</span><span style="LINE-HEIGHT: 140%"> varType)</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; {</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// The Property Inspector supports the following data</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// types for dynamic properties:</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// VT_I2, VT_I4, VT_R4, VT_R8,VT_BSTR, VT_BOOL</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// and VT_USERDEFINED. </span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160;</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160; varType = 4; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// VT_R4?</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; }</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160;</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// Get the property value, passes the specific object</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// we need the property value for.</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160;</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">public</span><span style="LINE-HEIGHT: 140%"> </span><span style="LINE-HEIGHT: 140%; COLOR: blue">void</span><span style="LINE-HEIGHT: 140%"> GetCurrentValueData(</span><span style="LINE-HEIGHT: 140%; COLOR: blue">object</span><span style="LINE-HEIGHT: 140%"> pUnk, </span><span style="LINE-HEIGHT: 140%; COLOR: blue">ref</span><span style="LINE-HEIGHT: 140%"> </span><span style="LINE-HEIGHT: 140%; COLOR: blue">object</span><span style="LINE-HEIGHT: 140%"> pVarData)</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; {</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// Get the value and return it to AutoCAD</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160;</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">AcadObject</span><span style="LINE-HEIGHT: 140%"> obj = pUnk </span><span style="LINE-HEIGHT: 140%; COLOR: blue">as</span><span style="LINE-HEIGHT: 140%"> </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">AcadObject</span><span style="LINE-HEIGHT: 140%">;</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">if</span><span style="LINE-HEIGHT: 140%"> (obj != </span><span style="LINE-HEIGHT: 140%; COLOR: blue">null</span><span style="LINE-HEIGHT: 140%">)</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160; {</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">Document</span><span style="LINE-HEIGHT: 140%"> doc =</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">Application</span><span style="LINE-HEIGHT: 140%">.DocumentManager.MdiActiveDocument;</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">Transaction</span><span style="LINE-HEIGHT: 140%"> tr =</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; doc.TransactionManager.StartTransaction();</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">using</span><span style="LINE-HEIGHT: 140%"> (tr)</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; {</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">DBObject</span><span style="LINE-HEIGHT: 140%"> o =</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; tr.GetObject(</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">new</span><span style="LINE-HEIGHT: 140%"> </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">ObjectId</span><span style="LINE-HEIGHT: 140%">((</span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">IntPtr</span><span style="LINE-HEIGHT: 140%">)obj.ObjectID),</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">OpenMode</span><span style="LINE-HEIGHT: 140%">.ForRead</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; );</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; pVarData =</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">PipeDrawOverrule</span><span style="LINE-HEIGHT: 140%">.PipeRadiusForObject(o);</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; }</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160; }</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">else</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; pVarData = 0.0;</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; }</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160;</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// Set the property value, passes the specific object we</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// want to set the property value for</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160;</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">public</span><span style="LINE-HEIGHT: 140%"> </span><span style="LINE-HEIGHT: 140%; COLOR: blue">void</span><span style="LINE-HEIGHT: 140%"> SetCurrentValueData(</span><span style="LINE-HEIGHT: 140%; COLOR: blue">object</span><span style="LINE-HEIGHT: 140%"> pUnk, </span><span style="LINE-HEIGHT: 140%; COLOR: blue">object</span><span style="LINE-HEIGHT: 140%"> varData)</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; {</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// Save the value returned to you</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160;</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">AcadObject</span><span style="LINE-HEIGHT: 140%"> obj = pUnk </span><span style="LINE-HEIGHT: 140%; COLOR: blue">as</span><span style="LINE-HEIGHT: 140%"> </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">AcadObject</span><span style="LINE-HEIGHT: 140%">;</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">if</span><span style="LINE-HEIGHT: 140%"> (obj != </span><span style="LINE-HEIGHT: 140%; COLOR: blue">null</span><span style="LINE-HEIGHT: 140%">)</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160; {</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">Document</span><span style="LINE-HEIGHT: 140%"> doc =</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">Application</span><span style="LINE-HEIGHT: 140%">.DocumentManager.MdiActiveDocument;</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">DocumentLock</span><span style="LINE-HEIGHT: 140%"> dl =</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; doc.LockDocument(</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">DocumentLockMode</span><span style="LINE-HEIGHT: 140%">.ProtectedAutoWrite,</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">null</span><span style="LINE-HEIGHT: 140%">, </span><span style="LINE-HEIGHT: 140%; COLOR: blue">null</span><span style="LINE-HEIGHT: 140%">, </span><span style="LINE-HEIGHT: 140%; COLOR: blue">true</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; );&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">using</span><span style="LINE-HEIGHT: 140%"> (dl)</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; {</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">Transaction</span><span style="LINE-HEIGHT: 140%"> tr =</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; doc.TransactionManager.StartTransaction();</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">using</span><span style="LINE-HEIGHT: 140%"> (tr)</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; {</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">DBObject</span><span style="LINE-HEIGHT: 140%"> o =</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; tr.GetObject(</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">new</span><span style="LINE-HEIGHT: 140%"> </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">ObjectId</span><span style="LINE-HEIGHT: 140%">((</span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">IntPtr</span><span style="LINE-HEIGHT: 140%">)obj.ObjectID),</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">OpenMode</span><span style="LINE-HEIGHT: 140%">.ForWrite</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; );</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">PipeDrawOverrule</span><span style="LINE-HEIGHT: 140%">.SetPipeRadiusOnObject(</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; tr, o, (</span><span style="LINE-HEIGHT: 140%; COLOR: blue">float</span><span style="LINE-HEIGHT: 140%">)varData</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; );</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; tr.Commit();</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; }</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; }</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160; }</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; }</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160;</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// OPM passes its implementation of IDynamicPropertyNotify, you</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// cache it and call it to inform OPM your property has changed</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160;</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">public</span><span style="LINE-HEIGHT: 140%"> </span><span style="LINE-HEIGHT: 140%; COLOR: blue">void</span><span style="LINE-HEIGHT: 140%"> Connect(</span><span style="LINE-HEIGHT: 140%; COLOR: blue">object</span><span style="LINE-HEIGHT: 140%"> pSink)</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; {</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160; m_pSink = (</span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">IDynamicPropertyNotify2</span><span style="LINE-HEIGHT: 140%">)pSink;</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; }</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160;</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">public</span><span style="LINE-HEIGHT: 140%"> </span><span style="LINE-HEIGHT: 140%; COLOR: blue">void</span><span style="LINE-HEIGHT: 140%"> Disconnect()</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; {</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160; m_pSink = </span><span style="LINE-HEIGHT: 140%; COLOR: blue">null</span><span style="LINE-HEIGHT: 140%">;</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; }</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; }</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%; COLOR: blue">&#0160; #endregion</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160;</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%; COLOR: blue">&#0160; #region</span><span style="LINE-HEIGHT: 140%"> Application Entry Point</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">public</span><span style="LINE-HEIGHT: 140%"> </span><span style="LINE-HEIGHT: 140%; COLOR: blue">class</span><span style="LINE-HEIGHT: 140%"> </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">MyEntryPoint</span><span style="LINE-HEIGHT: 140%"> : </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">IExtensionApplication</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; {</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">protected</span><span style="LINE-HEIGHT: 140%"> </span><span style="LINE-HEIGHT: 140%; COLOR: blue">internal</span><span style="LINE-HEIGHT: 140%"> </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">CustomProp</span><span style="LINE-HEIGHT: 140%"> custProp = </span><span style="LINE-HEIGHT: 140%; COLOR: blue">null</span><span style="LINE-HEIGHT: 140%">;</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160;</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">public</span><span style="LINE-HEIGHT: 140%"> </span><span style="LINE-HEIGHT: 140%; COLOR: blue">void</span><span style="LINE-HEIGHT: 140%"> Initialize()</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; {</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">Assembly</span><span style="LINE-HEIGHT: 140%">.LoadFrom(</span><span style="LINE-HEIGHT: 140%; COLOR: #a31515">&quot;asdkOPMNetExt.dll&quot;</span><span style="LINE-HEIGHT: 140%">);</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160;</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// Add the Dynamic Property to Lines and Circles</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// (might add it at the Entity level, instead)</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160;</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">Dictionary</span><span style="LINE-HEIGHT: 140%"> classDict = </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">SystemObjects</span><span style="LINE-HEIGHT: 140%">.ClassDictionary;</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">RXClass</span><span style="LINE-HEIGHT: 140%"> lineDesc = (</span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">RXClass</span><span style="LINE-HEIGHT: 140%">)classDict.At(</span><span style="LINE-HEIGHT: 140%; COLOR: #a31515">&quot;AcDbLine&quot;</span><span style="LINE-HEIGHT: 140%">);</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">RXClass</span><span style="LINE-HEIGHT: 140%"> cirDesc = (</span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">RXClass</span><span style="LINE-HEIGHT: 140%">)classDict.At(</span><span style="LINE-HEIGHT: 140%; COLOR: #a31515">&quot;AcDbCircle&quot;</span><span style="LINE-HEIGHT: 140%">);</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160; custProp = </span><span style="LINE-HEIGHT: 140%; COLOR: blue">new</span><span style="LINE-HEIGHT: 140%"> </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">CustomProp</span><span style="LINE-HEIGHT: 140%">();</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">IPropertyManager2</span><span style="LINE-HEIGHT: 140%"> pPropMan =</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; (</span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">IPropertyManager2</span><span style="LINE-HEIGHT: 140%">)</span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">xOPM</span><span style="LINE-HEIGHT: 140%">.xGET_OPMPROPERTY_MANAGER(lineDesc);</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160; pPropMan.AddProperty((</span><span style="LINE-HEIGHT: 140%; COLOR: blue">object</span><span style="LINE-HEIGHT: 140%">)custProp);</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160; pPropMan =</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; (</span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">IPropertyManager2</span><span style="LINE-HEIGHT: 140%">)</span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">xOPM</span><span style="LINE-HEIGHT: 140%">.xGET_OPMPROPERTY_MANAGER(cirDesc);</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160; pPropMan.AddProperty((</span><span style="LINE-HEIGHT: 140%; COLOR: blue">object</span><span style="LINE-HEIGHT: 140%">)custProp);</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; }</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160;</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">public</span><span style="LINE-HEIGHT: 140%"> </span><span style="LINE-HEIGHT: 140%; COLOR: blue">void</span><span style="LINE-HEIGHT: 140%"> Terminate()</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; {</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// Remove the Dynamic Property</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160;</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">Dictionary</span><span style="LINE-HEIGHT: 140%"> classDict = </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">SystemObjects</span><span style="LINE-HEIGHT: 140%">.ClassDictionary;</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">RXClass</span><span style="LINE-HEIGHT: 140%"> lineDesc = (</span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">RXClass</span><span style="LINE-HEIGHT: 140%">)classDict.At(</span><span style="LINE-HEIGHT: 140%; COLOR: #a31515">&quot;AcDbLine&quot;</span><span style="LINE-HEIGHT: 140%">);</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">RXClass</span><span style="LINE-HEIGHT: 140%"> cirDesc = (</span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">RXClass</span><span style="LINE-HEIGHT: 140%">)classDict.At(</span><span style="LINE-HEIGHT: 140%; COLOR: #a31515">&quot;AcDbCircle&quot;</span><span style="LINE-HEIGHT: 140%">);</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">IPropertyManager2</span><span style="LINE-HEIGHT: 140%"> pPropMan =</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; (</span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">IPropertyManager2</span><span style="LINE-HEIGHT: 140%">)</span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">xOPM</span><span style="LINE-HEIGHT: 140%">.xGET_OPMPROPERTY_MANAGER(lineDesc);</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160; pPropMan.RemoveProperty((</span><span style="LINE-HEIGHT: 140%; COLOR: blue">object</span><span style="LINE-HEIGHT: 140%">)custProp);</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160; pPropMan =</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; (</span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">IPropertyManager2</span><span style="LINE-HEIGHT: 140%">)</span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">xOPM</span><span style="LINE-HEIGHT: 140%">.xGET_OPMPROPERTY_MANAGER(cirDesc);</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160; pPropMan.RemoveProperty((</span><span style="LINE-HEIGHT: 140%; COLOR: blue">object</span><span style="LINE-HEIGHT: 140%">)custProp);</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160; custProp = </span><span style="LINE-HEIGHT: 140%; COLOR: blue">null</span><span style="LINE-HEIGHT: 140%">;</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; }</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; }</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%; COLOR: blue">&#0160; #endregion</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">}</span></p></div>
<p>Some comments on the implementation:</p>
<ul>
<li>GetCurrentValueData() and SetCurrentValueData() both have to open the object to access it’s .NET protocol 
<ul>
<li>We might also have used COM to access the XData, but this approach reuses previously-developed code</li>
<li>To modify the object we need to lock the current document 
<ul>
<li>We use the ProtectedAutoWrite locking mode for this, so that all our property edits are grouped into a single undo group 
<ul>
<li>We use the “protected” version of the locking mode as there’s a lock needed elsewhere, probably in the drawing code. If we use the standard AutoWrite lock we get an eLockViolation message</li>
</ul>
</li>
</ul>
</li>
<li>We’re using a new transaction for each read/modification 
<ul>
<li>This feels like overkill, but then as we’re in an UI-bound operation it’s unlikely to have a perceived performance impact </li>
</ul>
</li>
</ul>
</li>
<li>We’re also using the static protocol from the DrawOverrule class for the XData retrieval/setting 
<ul>
<li>With hindsight this probably should live in its own helper class, which is the original way I had it :-S :-) </li>
</ul>
</li>
</ul>
<p>Here’s a model we’re going to modify using the Properties Palette:</p>
<p><a href="http://through-the-interface.typepad.com/.a/6a00d83452464869e201157071db6e970b-pi"><img alt="Our overruled objects with different radii" border="0" height="307" src="/assets/image_209867.jpg" style="BORDER-RIGHT-WIDTH: 0px; DISPLAY: inline; BORDER-TOP-WIDTH: 0px; BORDER-BOTTOM-WIDTH: 0px; BORDER-LEFT-WIDTH: 0px" title="Our overruled objects with different radii" width="485" /></a></p>
<p>Now we select one of our overruled objects – a circle – and see it&#39;s new dynamic property:</p>
<p><a href="http://through-the-interface.typepad.com/.a/6a00d83452464869e201156f7bcec2970c-pi"><img alt="Dynamic property of one overruled object" border="0" height="308" src="/assets/image_978193.jpg" style="BORDER-RIGHT-WIDTH: 0px; DISPLAY: inline; BORDER-TOP-WIDTH: 0px; BORDER-BOTTOM-WIDTH: 0px; BORDER-LEFT-WIDTH: 0px" title="Dynamic property of one overruled object" width="486" /></a> </p>
<p>When we select all our objects, we see the property varies:</p>
<p><a href="http://through-the-interface.typepad.com/.a/6a00d83452464869e201156f7bcee0970c-pi"><img alt="Dynamic property of our varying overruled objects" border="0" height="308" src="/assets/image_22745.jpg" style="BORDER-RIGHT-WIDTH: 0px; DISPLAY: inline; BORDER-TOP-WIDTH: 0px; BORDER-BOTTOM-WIDTH: 0px; BORDER-LEFT-WIDTH: 0px" title="Dynamic property of our varying overruled objects" width="486" /></a> </p>
<p>Now we modify the property to be the same for all our objects:</p>
<p><a href="http://through-the-interface.typepad.com/.a/6a00d83452464869e201156f7bcef4970c-pi"><img alt="Modifying the dynamic property on our varying overruled objects" border="0" height="307" src="/assets/image_793986.jpg" style="BORDER-RIGHT-WIDTH: 0px; DISPLAY: inline; BORDER-TOP-WIDTH: 0px; BORDER-BOTTOM-WIDTH: 0px; BORDER-LEFT-WIDTH: 0px" title="Modifying the dynamic property on our varying overruled objects" width="485" /></a> </p>
<p>And we can see the result of our modification:</p>
<p><a href="http://through-the-interface.typepad.com/.a/6a00d83452464869e201156f7bcf22970c-pi"><img alt="The result of our dynamic property modification" border="0" height="306" src="/assets/image_734098.jpg" style="BORDER-RIGHT-WIDTH: 0px; DISPLAY: inline; BORDER-TOP-WIDTH: 0px; BORDER-BOTTOM-WIDTH: 0px; BORDER-LEFT-WIDTH: 0px" title="The result of our dynamic property modification" width="482" /></a> </p>
<p>You can see that the property currently isn’t categorised: as mentioned previously, we would have to implement ICategorizedProperty in our OPM .NET module for this to be possible. Which I will attempt, one day.</p>
