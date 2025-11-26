---
layout: "post"
title: "Are you looking to rewrite some of our Maya default scripts?"
date: "2012-07-01 17:25:00"
author: "Kristine Middlemiss"
categories:
  - "Kristine Middlemiss"
  - "Maya"
  - "MEL"
  - "Python"
  - "UI"
original_url: "https://around-the-corner.typepad.com/adn/2012/07/are-you-looking-to-rewrite-some-of-our-maya-default-scripts.html "
typepad_basename: "are-you-looking-to-rewrite-some-of-our-maya-default-scripts"
typepad_status: "Publish"
---

<p style="text-align: center;">First things first Happy Canada Day to all my fellow Canadians :)</p>
<p><a href="http://around-the-corner.typepad.com/.a/6a0163057a21c8970d017742cd132a970d-pi" style="display: inline;"><img alt="Images" border="0" src="/assets/image_17772c.jpg" style="margin-right: auto; margin-left: auto; display: block;" title="Images" /></a></p>
<p>Are you looking to rewrite some of our Maya default scripts? In your environment, likely you will get to the point where you want to customize some of the MEL scripts that either builds the Maya UI or that create the functionality in Maya. To do this it means you will have to rewrite/change some of Maya’s default scripts.</p>
<h2>Rewrite/Change a MEL script</h2>
<p>Let’s examine all the ways to rewrite/change a MEL script with the help of Dean Edmonds our API Architect:</p>
<h3>Option 1: Use the &#39;callbacks&#39; command</h3>
<blockquote>
<p><strong><em>PROS:</em></strong> Least intrusive.Easy to implement. Least chance of hidden surprises. Doesn&#39;t interfere with other plugi-ns&#39; overrides. Can be done and undone dynamically (e.g. when plugin loads and unloads).<br /> <br /> <strong><em>CONS: </em></strong>Will only work if the &#39;callbacks&#39; command happens to provide the override that you&#39;re looking for.</p>
</blockquote>
<h3>Option 2: Place your own version of the script in a directory which appears earlier in MAYA_SCRIPT_PATH than Maya&#39;s version</h3>
<blockquote>
<p><em><strong>PROS:</strong></em> Easy to implement. Few surprises. Can be done and undone dynamically with a bit of extra work. Works well in a small shop.</p>
<p><em><strong>CONS:</strong></em> Can interfere with other plugins&#39; overrides of the same script. Can be difficult to manage as a 3rd-party plugin or in a large shop where you don&#39;t have control over paths.</p>
</blockquote>
<h3>Option 3: Source your own script which  overrides only those procedures you&#39;re interested in</h3>
<blockquote>
<p>For example, let&#39;s say that you wanted to replace Maya&#39;s &#39;strip&#39; and  &#39;startsWith&#39; commands, which are both implemented in MEL, with your own  versions, whenever your plugin was loaded. First, you would create a script  file, with a different name, which contained your versions of the strip() and  startsWith() global procs:<br /> myOverrides.mel:<br /> <br /> global proc string strip(string $str)<br /> {<br /> &#0160;&#0160;&#0160; string $result = $str;<br /> &#0160;&#0160;&#0160; &lt;do your own stuff&gt;<br /> &#0160;&#0160;&#0160; return $result;<br /> }<br /> <br /> global proc int startsWith(string $s, string $prefix)<br /> {<br /> &#0160;&#0160;&#0160; int $result = false;<br /> &#0160;&#0160;&#0160; &lt;do your own stuff&gt;<br /> &#0160;&#0160;&#0160; return $result;<br /> }<br /> <br /> When your plugin loads it would do the following:<br /> source strip.mel; &#0160; &#0160; &#0160; &#0160;&#0160; &#0160;  &#0160;&#0160;&#0160; // To make sure that Maya&#39;s versions are loaded.<br /> source startsWith.mel;<br /> source myOverrides.mel;&#0160;&#0160;&#0160; // To override Maya&#39;s versions with  your own.<br /> <br /> When your plugin unloads, it should restore Maya&#39;s version:<br /> source strip.mel;<br /> source startsWith.mel;<br /> <br /> <em><strong>PROS:</strong></em> Your script doesn&#39;t need to override every procedure in the target  script, only the ones you want to change. Doesn&#39;t require any control over  paths, so works well in any environment. Can be done and undone dynamically.  Less chance of interfering with other plugins&#39; overrides than option (b).<br /> <br /> <em><strong>CONS:</strong></em> You can only override global procs: if you need to override a local proc  then you will need to override every proc which calls it to call your own  instead. You cannot call local procs from Maya&#39;s version of the script: if you  need to then you&#39;ll have to reproduce them in your own script. Some places in  Maya explicitly source certain script files: if that happens to the one you&#39;re  overriding then your override will be undone. There&#39;s still a possibility of  interfering with other plugins&#39; overrides, though the chances are lessened.</p>
</blockquote>
<h2>Rewrite/Change a Python script</h2>
<p>Okay that&#39;s great, but what about super cool Python :) Can we do the same as MEL? For Python there&#39;s really just one best way of doing it: replace methods  dynamically at runTime.</p>
<h3>Only Option: replace methods dynamically at runTime</h3>
<blockquote>
<p>Let&#39;s say that you want to add a hook into the listCommandPorts() method in  CommandPort.py. You could do that using the following script at runTime:<br /> import maya.app.general.CommandPort as CommandPort<br /> <br /> originalMethod = CommandPort.listCommandPorts<br /> <br /> def myListPorts():<br /> &#0160;&#0160;&#0160; myHook()<br /> &#0160;&#0160;&#0160; return originalMethod()<br /> <br /> CommandPort.listCommandPorts = myListPorts<br /> <br /> Now whenever anyone in the current Maya process calls  maya.app.general.CommandPort.listCommandPorts() they&#39;ll actually be calling  your myListPorts() method.<br /> <br /> When you no longer need the hook, be sure to put the original method back:<br /> CommandPort.listCommandPorts = originalMethod<br /> <br /> One thing to keep in mind is that if you&#39;re replacing a non-static member  method of a class, remember to include the &#39;self&#39; parameter at the start of the  parameter list.<br /> <br /> <em><strong>PROS:</strong></em> Easy to implement. If you&#39;re just adding a hook and then calling through  to the original method, it works very well with other plugins&#39; overrides since  they just chain together.<br /> <br /> <em><strong>CONS</strong></em>: If your override can&#39;t call through to the original method then it can  interfere other plugins&#39; overrides.</p>
</blockquote>
<p>Enjoy!</p>
<p>Kristine</p>
