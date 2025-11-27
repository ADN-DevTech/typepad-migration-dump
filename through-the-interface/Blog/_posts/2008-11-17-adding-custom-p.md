---
layout: "post"
title: "Adding custom properties to AutoCAD's rollover tooltip and its quick properties panel"
date: "2008-11-17 06:41:12"
author: "Kean Walmsley"
categories:
  - "AutoCAD"
  - "AutoCAD .NET"
  - "Object properties"
  - "ObjectARX"
  - "User interface"
original_url: "https://www.keanw.com/2008/11/adding-custom-p.html "
typepad_basename: "adding-custom-p"
typepad_status: "Publish"
---

<p>This topic is a little on the advanced side - it requires the use of C++ and some knowledge of COM - but I felt it was worth covering, as you can get some interesting results without a huge amount of effort.</p>

<p>Since we introduced the Properties Palette (once known as the Object Property Manager (OPM) and otherwise referred to as the Properties Window) back in AutoCAD 2004 (I think it was) it has become a core tool for viewing and editing properties of AutoCAD objects. In AutoCAD 2009 we have taken the concept further, allowing properties to be added to the &quot;rollover tooltip&quot; - for easy viewing of important properties - and to the &quot;quick properties&quot; panel - for editing core properties on object selection, rather than requiring a separate command or window.</p>

<p>All these features use COM to communicate with the objects they're viewing or editing. Various COM interfaces are used, whether the features are accessing <em>static</em> or <em>dynamic</em> properties. Static properties are the standard properties of a COM object: those exposed (typically) by the provider of the object, or at least of the COM wrapper. Dynamic properties are additional properties that can be provided by anyone for any object: the property provider simply chooses the objects they wish to store and expose dynamic properties for, and they then do so (typically by storing the data as extended entity data (XData) or via a custom object or XRecord in the extension dictionary of an object).</p>

<p>Static and dynamic properties are actually very nicely analogous to concepts from the world of object-oriented programming. Static properties are like the standard, compiled-in protocol for a C++ (or .NET, for that matter) class, while dynamic properties correspond to what in ObjectARX we call &quot;protocol extensions&quot; - additional methods that allow you to extend the compiled-in protocol of a class - and what are now called &quot;extension methods&quot; in C# 3.0.</p>

<p>A quick word on the &quot;dynamic&quot; nomenclature: dynamic properties are really a development-centric concept - the user shouldn't care whether something is static or dynamic. Now that dynamic blocks (a user-centric feature) have been introduced into AutoCAD, it can get a little confusing as to whether you're talking about properties of dynamic blocks or dynamic properties (of, say, blocks). So searching on ADN, etc., for help with dynamic (COM) properties is tricky: your best bet in general is to search for IDynamicProperty, the COM interface objects that must be implemented to provide dynamic properties for AutoCAD objects.</p>

<p>Today we're going to focus on getting dynamic properties onto the rollover tooltip and into the quick properties panel. As a starting point we're going to take the SimpleDynProps sample on the ObjectARX 2009 SDK (under samples/editor/simpledynprops). After building the project and loading the SimpleDynProps.arx module into AutoCAD 2009, run the ASSIGNDATA command and select some objects to which we will attach our custom properties with blank values (they'll be set to 0). The data gets stored in the extension dictionary of the selected objects, and it's from there that our dynamic property implementation will pick it up and display it in the Properties Window.<a href="http://through-the-interface.typepad.com/through_the_interface/WindowsLiveWriter/Dynamic%20properties%20on%20a%20line.png"><img height="484" alt="Dynamic properties on a line" src="/assets/Dynamic%20properties%20on%20a%20line_thumb.png" width="269" align="right" border="0" style="BORDER-TOP-WIDTH: 0px; BORDER-LEFT-WIDTH: 0px; BORDER-BOTTOM-WIDTH: 0px; MARGIN: 20px 40px 20px 45px; BORDER-RIGHT-WIDTH: 0px" /></a> </p>

<p>If you select one of the objects to which you've assigned data and run the PROPS command, you should see our dynamic properties in their own category at the bottom of the window, as shown in the image to the right. </p>

<p>So we can see now that dynamic properties can be implemented for standard AutoCAD objects, and these can be exposed by a custom ObjectARX module (if it's not loaded, the properties won't get shown).</p>

<p>Now let's make a few changes to our project, to enable these properties for the new property display/editing functionality in AutoCAD 2009. The modified project - along with the .arx file, for those who don't want to have to build it, but would like to get a feel for how it works - can be <a href="http://through-the-interface.typepad.com/through_the_interface/files/simpledynprops2.zip">downloaded from here</a>.</p>

<p>Start with SimpleProperty.h, which is the base for our custom dynamic property implementation. At the top of this file, we're going to make the CSimpleProperty class implement the new IFilterableProperty interface (inserted code in <span style="color: #ff0000;">red</span>):</p>

<div style="FONT-SIZE: 8pt; BACKGROUND: white; COLOR: black; FONT-FAMILY: courier new"><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue; LINE-HEIGHT: 140%">class</span><span style="LINE-HEIGHT: 140%"> ATL_NO_VTABLE CSimpleProperty : </span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; </span><span style="COLOR: blue; LINE-HEIGHT: 140%">public</span><span style="LINE-HEIGHT: 140%"> CComObjectRootEx&lt;CComSingleThreadModel&gt;,</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; </span><span style="COLOR: blue; LINE-HEIGHT: 140%">public</span><span style="LINE-HEIGHT: 140%"> IDynamicProperty<span style="color: #ff0000;">,</span></span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; </span><span style="color: #ff0000;"><span style="COLOR: red; LINE-HEIGHT: 140%">public</span><span style="LINE-HEIGHT: 140%"> IFilterableProperty</span></span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">{</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue; LINE-HEIGHT: 140%">public</span><span style="LINE-HEIGHT: 140%">:</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; CSimpleProperty();</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">BEGIN_COM_MAP(CSimpleProperty)</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; COM_INTERFACE_ENTRY(IDynamicProperty)</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; <span style="color: #ff0000;">COM_INTERFACE_ENTRY(IFilterableProperty)</span></span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">END_COM_MAP()</span></p></div>

<p>Further down in the file we will add the public method that is required for the IFilterableProperty implementation (no need to put anything in red, here - it's all new):</p>

<div style="FONT-SIZE: 8pt; BACKGROUND: white; COLOR: black; FONT-FAMILY: courier new"><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">STDMETHOD(ShowFilterableProperty)(THIS_</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: green; LINE-HEIGHT: 140%"><span style="LINE-HEIGHT: 140%"><span style="color: #000000;">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; </span></span>/*[in]*/</span><span style="LINE-HEIGHT: 140%">DISPID dispID,</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%"><span style="COLOR: green; LINE-HEIGHT: 140%"><span style="LINE-HEIGHT: 140%"><span style="color: #000000;">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; </span></span></span></span><span style="COLOR: green; LINE-HEIGHT: 140%">/*[in]*/</span><span style="LINE-HEIGHT: 140%">AcFilterablePropertyContext context,</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%"><span style="COLOR: green; LINE-HEIGHT: 140%"><span style="LINE-HEIGHT: 140%"><span style="color: #000000;">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; </span></span></span></span><span style="COLOR: green; LINE-HEIGHT: 140%">/*[out]*/</span><span style="LINE-HEIGHT: 140%">BOOL* pbShow);</span></p></div>

<p>In my code I added this after the declaration for SetCurrentValueData and before the notifications section, but it can basically go anywhere as long as it's declared as public.</p>

<p>Then we switch across to the SimpleProperty.cpp file, where we'll implement the function:</p>

<div style="FONT-SIZE: 8pt; BACKGROUND: white; COLOR: black; FONT-FAMILY: courier new"><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">STDMETHODIMP</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">CSimpleProperty::ShowFilterableProperty(</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: green; LINE-HEIGHT: 140%"><span style="LINE-HEIGHT: 140%"><span style="color: #000000;"><span style="COLOR: green; LINE-HEIGHT: 140%"><span style="LINE-HEIGHT: 140%"><span style="color: #000000;">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; </span></span></span></span></span>/*[in]*/</span><span style="LINE-HEIGHT: 140%">DISPID dispID,</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%"><span style="COLOR: green; LINE-HEIGHT: 140%"><span style="LINE-HEIGHT: 140%"><span style="color: #000000;">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; </span></span></span></span><span style="COLOR: green; LINE-HEIGHT: 140%">/*[in]*/</span><span style="LINE-HEIGHT: 140%">AcFilterablePropertyContext context,</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%"><span style="LINE-HEIGHT: 140%"><span style="COLOR: green; LINE-HEIGHT: 140%"><span style="LINE-HEIGHT: 140%"><span style="color: #000000;">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; </span></span></span></span></span><span style="COLOR: green; LINE-HEIGHT: 140%">/*[out]*/</span><span style="LINE-HEIGHT: 140%">BOOL* pbShow)</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">{</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; *pbShow = TRUE;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; </span><span style="COLOR: blue; LINE-HEIGHT: 140%">return</span><span style="LINE-HEIGHT: 140%"> S_OK;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">}</span></p></div>

<p>The function definition can go pretty much anywhere in the class implementation, but I put it (once again) after SetCurrentValueData and before the implementation of the Connect/Disconnect notification event handlers.</p>

<p>Well, that's almost all there is to it. The implementation we've put in place says &quot;these properties are available&quot; for all the dynamic properties we expose, both for inclusion on rollover tooltips and in the quick properties panel. You could very easily check the dispID argument to see for which dynamic property the &quot;filterable&quot; property is being queried, or the context argument to see whether the question relates to rollover tooltips or quick properties. We're just keeping things simple, here.</p>

<p>The project should now build, and the output .ARX module should be loadable into AutoCAD. Once you've done so, if you launch the CUI command, you should be able to enable our properties for the rollover tooltip and/or the quick properties panel for various object types:</p>

<p><a href="http://through-the-interface.typepad.com/through_the_interface/WindowsLiveWriter/Editing%20the%20rollover%20tooltip%20properties%20for%20a%20line%20in%20the%20CUI%20dialog.png"><img height="400" alt="Editing the rollover tooltip properties for a line in the CUI dialog" src="/assets/Editing%20the%20rollover%20tooltip%20properties%20for%20a%20line%20in%20the%20CUI%20dialog_thumb.png" width="475" border="0" style="BORDER-TOP-WIDTH: 0px; BORDER-LEFT-WIDTH: 0px; BORDER-BOTTOM-WIDTH: 0px; BORDER-RIGHT-WIDTH: 0px" /></a> </p>

<p>Using the CUI command I enabled our new properties for the Line object in both the rollover tooltip and the quick properties panel. Let's now take a look at both these in action - looking at objects who have data assigned via our ASSIGNDATA command.</p>

<p>Rollover tooltip:</p>

<p><a href="http://through-the-interface.typepad.com/through_the_interface/WindowsLiveWriter/Rollover%20tooltip%20with%20our%20custom%20properties.png"><img height="326" alt="Rollover tooltip with our custom properties" src="/assets/Rollover%20tooltip%20with%20our%20custom%20properties_thumb.png" width="349" border="0" style="BORDER-TOP-WIDTH: 0px; BORDER-LEFT-WIDTH: 0px; BORDER-BOTTOM-WIDTH: 0px; BORDER-RIGHT-WIDTH: 0px" /></a>&nbsp; </p>

<p>Quick properties:</p>

<p><a href="http://through-the-interface.typepad.com/through_the_interface/WindowsLiveWriter/Quick%20properties%20panel%20with%20our%20custom%20properties.png"><img height="310" alt="Quick properties panel with our custom properties" src="/assets/Quick%20properties%20panel%20with%20our%20custom%20properties_thumb.png" width="398" border="0" style="BORDER-TOP-WIDTH: 0px; BORDER-LEFT-WIDTH: 0px; BORDER-BOTTOM-WIDTH: 0px; BORDER-RIGHT-WIDTH: 0px" /></a> </p>

<p>I haven't yet found a way to add custom properties into the &quot;general properties&quot; set - those shown for every class type. For now it seems you need to add them in at the individual class level (for each type of object), which does make this type of customization slightly less easy to roll-out. Hopefully I'm just missing something obvious, though - if so I'll follow up with a note or comment.</p>
