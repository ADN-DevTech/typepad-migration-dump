---
layout: "post"
title: "FBX 2013.3 plugin for Maya2013.5 on Linux is not installed"
date: "2012-10-10 02:59:00"
author: "Cyrille Fauvel"
categories:
  - "Cyrille Fauvel"
  - "FBX"
  - "Linux"
  - "Maya"
original_url: "https://around-the-corner.typepad.com/adn/2012/10/fbx-20133-plugin-for-maya20135-on-linux-is-not-installed.html "
typepad_basename: "fbx-20133-plugin-for-maya20135-on-linux-is-not-installed"
typepad_status: "Publish"
---

<h3>To
install the Maya FBX plug-in: </h3>
<ol>
<li>Find the 2013.3 FBX installer which is included with the Maya
2013.5 install package. </li>
<li>Remove
the old plug-in from the <em>/usr/Autodesk/maya&lt;ver&gt;/bin/plug-ins/
(</em>where <em>&lt;ver&gt;</em>
is the version of Maya you have installed,) directory by entering <em>rm –f fbxmaya.so</em> at the
command prompt. </li>
<li>Type
the following command to install the Maya FBX
plug-in: </li>
</ol>
<pre>./fbx&lt;ver&gt;_maya&lt;ver&gt;_linux_enu</pre>
<p>(Where <em>FBX&lt;ver&gt;</em> is the
version of the Maya FBX plug-in you have
installed). </p>
<p>This
installs the plug-in to the following directory: </p>
<p>/usr/Autodesk/maya&lt;<em>ver</em>&gt;/bin/plug-ins/
(where &lt;<em>ver</em>&gt;
is the version of Maya you have installed). </p>
<p>If you
install Maya in another folder, you can specify the path on the Command Line
after: ./fbx&lt;ver&gt;_maya&lt;ver&gt;_linux_enu /home/maya/mypath/ </p>
<p>(Where <em>FBX&lt;ver&gt;</em> is the
version of the Maya FBX plug-in you have
installed.) </p>
<ol>
<li>Start
Maya and navigate to <strong>Window</strong> &gt; <strong>Settings/Preferences</strong> &gt; <strong>Plug-in
Manager</strong>. </li>
<li>Activate
the <strong>Loaded</strong> option for <em>Fbxmaya.so</em> in the <strong>Plug-in Manager</strong>. </li>
<li>Activate
the <em>Fbxmaya.so </em><strong>Autoload</strong> option so Maya loads the Maya FBX plug-in automatically at start-up.</li>
</ol>
<p>&#0160;</p>
