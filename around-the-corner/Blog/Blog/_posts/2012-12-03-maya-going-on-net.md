---
layout: "post"
title: "Maya on .NET"
date: "2012-12-03 10:09:52"
author: "Cyrille Fauvel"
categories:
  - ".Net"
  - "C++"
  - "Conferences"
  - "Cyrille Fauvel"
  - "Debugging"
  - "Events"
  - "LINQ"
  - "Linux"
  - "Mac"
  - "Maya"
  - "Plug-in"
  - "UI"
  - "Visual Studio"
  - "WCF"
  - "Windows"
  - "WPF"
original_url: "https://around-the-corner.typepad.com/adn/2012/12/maya-going-on-net.html "
typepad_basename: "maya-going-on-net"
typepad_status: "Publish"
---

<p>For those of you who are already on the Maya Feedback Community forum (beta forum), it is probably not really new. But for all others, this is quite a big change - Maya will get a native .NET API for its Windows platform version.</p>
<p>So I am proud to announce the Maya .NET API here.</p>
<p>
<a class="asset-img-link" href="http://around-the-corner.typepad.com/.a/6a0163057a21c8970d017d3e6aa8f5970c-pi" style="display: inline;"><img alt="DotNet" border="0" class="asset  asset-image at-xid-6a0163057a21c8970d017d3e6aa8f5970c" src="/assets/image_2c040d.jpg" style="display: block; margin-left: auto; margin-right: auto;" title="DotNet" /></a><br />The Maya .NET API is a new technology that 
allows Maya plug-in developers to harness the full power of Microsoft’s .NET 
Framework. With the Maya .NET API, developers extending Maya are now able 
to:
</p>
<ul>
<li>Access the Maya API in a friendly, idiomatic style that is familiar to .NET 
developers 
</li>
<li>Develop code faster and explore the SDK with Intellisense for Microsoft 
Visual Studio IDE 
</li>
<li>Leverage their development skillsets by using their favorite .NET language, 
such as C#, VB</li>
<li>Unleash the rich set of .NET Framework services when building Maya plug-ins, 
including Remoting, Language Integrated Query, Serialization, and advanced Web 
services 
</li>
<li>Integrate WPF/XAML user interfaces into their plugins by docking them into 
Maya’s Qt-based panels </li>
</ul>
The .NET API is currently still in BETA 
release, and we invite you to try it out and provide early feedback. Your 
participation, comments, and defect reports will all be greatly appreciated and 
help us to provide YOU with better software for developing the very best Maya 
plug-ins to make your production run better and make your Maya Artists 
happier!<br /><br />You can download the Maya .NET API BETA from the Maya 
Community Feedback homepage in the Downloads section. Please carefully read the 
included User’s Guide for limitations, important notes, and tips. Defects and 
comments can be logged with the Report Problem button in Maya Feedback 
Community. Also, you are invited to post actively in the .NET API User Forum, 
where Autodesk developers will be able to interact with you.<br />
<p>It is for Windows only and the .Net API is a thin C++/CLI layer around the MPx classes, whereas other classes where generated using SWIG and P/Invoke. That means that it does not support Mono and therefore do not support Linux and OSX.</p>
<p>The API will not be backward compatible while in theory it could be back-ported to previous releases (see my <a href="http://around-the-corner.typepad.com/adn/2012/05/enabling-net-in-a-maya-plug-in-.html" target="_self">post </a>from May 28th).</p>
<p>On the last beta version, we added many new things, and some were based on the feedback we received from Studios working with us on developing/finalizing that API.&#0160;</p>
<ol>
<li>The Maya’s plug-in manager now natively recognizes .NET plug-ins. .NET plug-ins 
now behave exactly like C++ plug-ins (except they must be named with a &quot;.nll.dll&quot; 
extension). That includes them being listed in the plug-in manager window, being 
able to load/unload them, using the same MEL commands to manage them as C++ 
plug-ins, and support for Maya Modules and MAYA_PLUG_IN_PATH. 
</li>
<li>Lots and lots of examples. See devkit\dotnet for a collection of many plugin 
examples ported to C# 
</li>
<li>Nifty WPF C# example. See devkit\dotnet\wpfexamples for a plugin shows off 
dynamic code compilation, LINQ queries over the Maya scene graph, a grid 
control, WPF user interface that docks with Qt, a 3D viewer in C#, and more! 
</li>
<li>Scene message callbacks are modeled as .NET events, and are even listed in 
Intellisense in Visual Studio 
</li>
<li>Visual Studio project template for generating boilerplate plug-in code. See 
the User’s Guide for the embedded ZIP file.</li>
</ol>
<p>What is really new and cool when using that API, is that you really feel developing on .Net vs. using the old Maya API approach. For example, you do not really need an entrypoint like in C++ or Python or even the creator() or initialize() function. Nor you need to register anything yourself by code. Instead you decorate your classes with attribute(s). Let me give you a simple &quot;Hello World&quot; sample and this is a very simple example.</p>
<p>This is the Python code :(</p>
<pre class="brush: python; toolbar: false;">import sys
import maya.OpenMaya as OpenMaya
import maya.OpenMayaMPx as OpenMayaMPx

# command
class HelloWorldCmd(OpenMayaMPx.MPxCommand):
	kPluginCmdName = &quot;spHelloWorld&quot;

	def __init__(self):
		OpenMayaMPx.MPxCommand.__init__(self)

	@staticmethod
	def cmdCreator():
		return OpenMayaMPx.asMPxPtr( HelloWorldCmd() )

	def doIt(self,argList):
		print &quot;Hello World!&quot;


# Initialize the script plug-in
def initializePlugin(plugin):
	pluginFn = OpenMayaMPx.MFnPlugin(plugin)
	try:
		pluginFn.registerCommand(
			HelloWorldCmd.kPluginCmdName, HelloWorldCmd.cmdCreator
		)
	except:
		sys.stderr.write(
			&quot;Failed to register command: %s\n&quot; % HelloWorldCmd.kPluginCmdName
		)
		raise

# Uninitialize the script plug-in
def uninitializePlugin(plugin):
	pluginFn = OpenMayaMPx.MFnPlugin(plugin)
	try:
		pluginFn.deregisterCommand(HelloWorldCmd.kPluginCmdName)
	except:
		sys.stderr.write(
			&quot;Failed to unregister command: %s\n&quot; % HelloWorldCmd.kPluginCmdName
		)
		raise
</pre>
<p>And the Maya .Net equivalent</p>
<pre class="brush: csharp; toolbar: false;">using System;
using Autodesk.Maya.Runtime;
using Autodesk.Maya.OpenMaya;
using Autodesk.Maya.OpenMayaMPx;

[assembly: MPxCommandClass(typeof(MayaNetPlugin.helloWorldCmd), &quot;helloWorldCmd&quot;)]

namespace MayaNetPlugin
{
	public class helloWorldCmd : MPxCommand, IMPxCommand {
		public override MStatus doIt(MArgList argl) {
			MGlobal.displayInfo(&quot;Hello World\n&quot;);
			return MStatus.kSuccess;
		}
	}
}
</pre>
<p>Nothing more, nothing less needed - Really!</p>
<p>And just to give you an idea on how simple it is - here is an abstract on how you could initialize node&#39;s attributes in .Net.</p>
<pre class="brush: csharp; toolbar: false;">[assembly: MPxNodeClass(typeof(MayaNetPlugin.myNode), &quot;myCSharpNode&quot;, 0x00000000)]

class myNode : MPxNode, IMPxNode {
  [MPxNodeNumeric(&quot;in&quot;, &quot;input&quot;, MFnNumericData.Type.kFloat, Storable = true)]
  public static MObject input = null;

  [MPxNodeNumeric(&quot;out&quot;, &quot;output&quot;, MFnNumericData.Type.kFloat, Storable = false, Writable = false)]
  [MPxNodeAffectedBy(&quot;input&quot;)]
  public static MObject output = null;

  override public MStatus compute(MPlug plug, MDataBlock dataBlock) {
    ...
  }
}
</pre>
<p>Excepted the code required in compute() this is it for your class node implementation.</p>
<p>If you interested helping us, please join on the beta forum or drop me a line!</p>
