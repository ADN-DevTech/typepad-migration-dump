---
layout: "post"
title: "a Mathematical DG Node"
date: "2012-08-06 00:00:00"
author: "Cyrille Fauvel"
categories:
  - "Animation"
  - "Custom Nodes"
  - "Cyrille Fauvel"
  - "Dynamics"
  - "Maya"
  - "MEL"
  - "Plugin of the Month"
  - "Python"
original_url: "https://around-the-corner.typepad.com/adn/2012/08/a-mathematical-dg-node.html "
typepad_basename: "a-mathematical-dg-node"
typepad_status: "Publish"
---

<p>Many of you probably knows about the sineNode available in the Maya devkit written in C++ and/or Python. This example is very useful if you interested running the sin() function. But there is several limitations which make this example a bit dry.</p>
<p>First, it is just the sin() function and with this approach it means you would need to create as many &#39;math&#39; node you want mathematical functions.</p>
<p>Second, it has one input and one output parameter only, so it limits you in a way that you need to combine many nodes to do more complicated expression such as sum(), degree to radian, etc... before going to sin(). And may be few more after...</p>
<p>So you&#39;ll hit the wall pretty fast... <a class="asset-img-link" href="http://around-the-corner.typepad.com/.a/6a0163057a21c8970d016769054697970b-pi" style="display: inline;"><img alt="Wall_crash" class="asset  asset-image at-xid-6a0163057a21c8970d016769054697970b" src="/assets/image_921956.jpg" style="width: 200px;" title="Wall_crash" /></a></p>
<p>with a big DG graph for a fairly simple operation.</p>
<p><strong>What are the solutions?</strong></p>
<p>One obvious is to use the Maya Expression system.&#0160;Maya&#39;s evaluation of Expressions depends on the contents of the expression – sometimes you have to force the evaluation - whereas, for a node, the Maya DG will always do it for you when needed. Debugging a Maya Expression is not easy either (no tools are available), and requires a knowledge of MEL. And it is pretty slow :( so I would not recommend using it everywhere.</p>
<p><img alt="Cgdna" border="0" class="asset  asset-image at-xid-6a0163057a21c8970d0167690558aa970b" src="/assets/image_cfa0c4.jpg" style="color: #0000ee; display: block; margin-left: auto; margin-right: auto;" title="Cgdna" /></p>
<div>Another one is to create a series of specialized nodes (one for each possible math function you want) and design them in a way that they will do anything you want. You would write them like the sineNode() and&#0160;probably choose C++ for getting best performance. There are very useful nodes written by <strong>moto @ cgdna</strong>&#0160;available <a href="http://www.cgdna.com/web/mathematic-nodes-main" target="_self">here</a>&#0160;(implemented in C++ and using Boost). A full description is also available <a href="http://www.creativecrash.com/maya/downloads/scripts-plugins/rendering/other-renderers/c/mathematic-nodes-mentalray-support---2" target="_self">here</a>.</div>
<div>
<p>Fortunately, because they are written in C++, they are fast - Unfortunately, it means it is a library of Nodes, which needs maintenance (be recompiled for every release/platform), and they are not extensible.</p>
<p>Still a very good solution, but there is another one which makes a single node the ultimate choice, with a small performance hit. This is using Python and the fact Python can evaluation expression on the fly vs C++ which can&#39;t.</p>
<p>In this example, we will use the Python fundamentals of the best of any interpreted languages. To start we will create 3 input float attributes and 1 output float attribute. But the number of inputs and outputs could be bigger, that won&#39;t cause any problem for Python and our node. In a future version, I&#39;ll make the number of input attributes dynamic. So anyone could decide he needs 5 vs 3, and do the same for outputs. Input attributes are of type &#39;float&#39;, but in theory it could potentially be strings too since the Python interpreter would cope with that easily. That is another advantage of using Python vs C++ that we may take a look in another post. But let&#39;s keep it simple for now and use floats.</p>
<p>Here is my node attribute definition:</p>
<pre class="brush: python; toolbar: false;"># Input
exprSt =OpenMaya.MFnStringData ()
exprStCreator =exprSt.create (&quot;a&quot;)
tAttr =OpenMaya.MFnTypedAttribute ()
asdkMathNode.expression =tAttr.create (&quot;expression&quot;, &quot;expr&quot;, OpenMaya.MFnStringData.kString, exprStCreator)
tAttr.setStorable (1)
tAttr.setKeyable (False)

nAttr =OpenMaya.MFnNumericAttribute ()
asdkMathNode.aIn =nAttr.create (&quot;aIn&quot;, &quot;a&quot;, OpenMaya.MFnNumericData.kFloat, 0.0)
nAttr.setStorable (1)
asdkMathNode.bIn =nAttr.create (&quot;bIn&quot;, &quot;b&quot;, OpenMaya.MFnNumericData.kFloat, 0.0)
nAttr.setStorable (1)
asdkMathNode.cIn =nAttr.create (&quot;cIn&quot;, &quot;c&quot;, OpenMaya.MFnNumericData.kFloat, 0.0)
nAttr.setStorable (1)
	
# Output
nAttr =OpenMaya.MFnNumericAttribute ()
asdkMathNode.result =nAttr.create (&quot;result&quot;, &quot;res&quot;, OpenMaya.MFnNumericData.kFloat, 0.0)
nAttr.setStorable (1)
nAttr.setWritable (1)
	
# Setup node attributes
asdkMathNode.addAttribute (asdkMathNode.expression)
asdkMathNode.addAttribute (asdkMathNode.aIn)
asdkMathNode.addAttribute (asdkMathNode.bIn)
asdkMathNode.addAttribute (asdkMathNode.cIn)
asdkMathNode.addAttribute (asdkMathNode.result)
	
asdkMathNode.attributeAffects (asdkMathNode.expression, asdkMathNode.result)
asdkMathNode.attributeAffects (asdkMathNode.aIn, asdkMathNode.result)
asdkMathNode.attributeAffects (asdkMathNode.bIn, asdkMathNode.result)
asdkMathNode.attributeAffects (asdkMathNode.cIn, asdkMathNode.result)
</pre>
<p>In this implementation, in addition to the float attributes, we created a string attribute to handle the mathematical expression. But we will come back on this one. What is important here is to note that each numerical attributes has a default value - otherwise Python won&#39;t be happy with us later.</p>
<p>Ok nothing really new yet, but let&#39;s now talk about the 2 Python principles we are going to use. Symbols defined in the global state are always available for use in a Python expression, and Python eval() function evaluate a formatted Python expression string as if it was code written by the developer. The <em>expression&#0160;</em>argument is parsed and evaluated as a Python expression (technically speaking, a condition list) using the&#0160;<em>globals</em> and <em>locals</em> dictionaries as global and local namespace. That simply means that the string can contain any Python valid code to be executed on the fly.</p>
<p>In that case, as long out a, b , c input attributes are copied into some a, b, c Python symbols in the local or global dictionaries, eval() will just use them and parse/execute the code contained into the string. Here we go! not only can&#0160;we&#0160;use a mathematical expression, but we can use plain Python code and custom Python functions.</p>
<p>Our compute() custom node method becomes:</p>
<pre class="brush: python; toolbar: false;">def compute (self, plug, dataBlock):
	if ( plug == asdkMathNode.result ):
		exprHandle =dataBlock.inputValue (asdkMathNode.expression)
		exprStData =OpenMaya.MFnStringData (exprHandle.data ())
		exprSt =exprStData.string ()
		# This is where our Maya Node&#39; Attributes become Python Symbols
		aHandle =dataBlock.inputValue (asdkMathNode.aIn)
		a =aHandle.asFloat ()
		bHandle =dataBlock.inputValue (asdkMathNode.bIn)
		b =bHandle.asFloat ()
		cHandle =dataBlock.inputValue (asdkMathNode.cIn)
		c =cHandle.asFloat ()

		# This where the magic plays
		# We also handle the fact that the Python Expression may not be valid
		try:
 			result =eval (exprSt)
 		except:
 			sys.stderr.write (&quot;Expression %s failed&quot; % exprSt)
			raise

  		outputHandle =dataBlock.outputValue (asdkMathNode.result)
 		outputHandle.setFloat (result)
 		dataBlock.setClean (plug)
 		return
 	return OpenMaya.kUnknownParameter
</pre>
<p>As long as exprSt contains valid Python code and the expression returns a number - our node will be fully functional. Python&#39;&#0160;mathematical functions being written in C/C++, the performance hit is very minimal.</p>
<p>You can find the ready code on <a href="http://labs.autodesk.com/" target="_self"></a>my github repo <a href="https://github.com/cyrillef/MayaMathNode">here</a>, and the installer on the Autodesk AppStore <a href="https://apps.autodesk.com/MAYA/en/Detail/Index?id=212304226072872821&amp;appLang=en&amp;os=Win64&amp;autostart=true">here</a>.</p>
</div>
