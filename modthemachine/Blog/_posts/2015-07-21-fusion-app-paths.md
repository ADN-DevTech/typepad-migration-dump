---
layout: "post"
title: "Fusion app paths"
date: "2015-07-21 12:54:43"
author: "Adam Nagy"
categories:
  - "Adam"
  - "Fusion 360"
original_url: "https://modthemachine.typepad.com/my_weblog/2015/07/fusion-app-paths.html "
typepad_basename: "fusion-app-paths"
typepad_status: "Publish"
---

<p>If you are developing apps for the&#0160;<a href="https://apps.exchange.autodesk.com/FUSION/en/Home/Index" target="_self">Fusion app store</a> then it could be useful to know where exactly <strong>Fusion</strong> is looking for add-in bundles to load, so that you can test the loading on your side. To see what a <strong>.bundle</strong> should contain you can just download any of the free apps&#0160;from the Fusion app store.&#0160;<br /><strong>Note</strong>: on <strong>MacOS</strong> you have to right-click the <strong>.bundle</strong> folder and select <strong>Show Package Contents</strong> to see what&#39;s inside it:<br /><br /> <a class="asset-img-link" href="http://modthemachine.typepad.com/.a/6a00e553fcbfc6883401b8d13ad9f1970c-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="Bundle" class="asset  asset-image at-xid-6a00e553fcbfc6883401b8d13ad9f1970c img-responsive" src="/assets/image_297616.jpg" title="Bundle" /></a></p>
<p>The <strong>Windows</strong> and <strong>MacOS</strong> file structure is different so the <strong>add-in</strong> folder locations will be different. But on&#0160;<strong>MacOS</strong> we also have two install types of <strong>Fusion</strong>:<br />- web install<br />- Mac App Store (<strong>MAS</strong>) install</p>
<p>All <strong>MAS</strong> apps run in a sandbox environment, and they cannot load additional modules from outside their folder structure without user interaction. That&#39;s why the <strong>MAS</strong> version of <strong>Fusion</strong> is using a different path for <strong>Fusion</strong> apps.</p>
<p><strong>Windows</strong>: <br />%APPDATA%\Autodesk\ApplicationPlugins<br /><br /><strong>MacOS web install</strong>:<br />~/Library/Application Support/Autodesk/ApplicationPlugins&#0160;</p>
<p><strong>MacOS MAS install</strong>: <br />~/Library/Containers/com.autodesk.mas.fusion360/Data/Library/Application Support/Autodesk/ApplicationPlugins&#0160;</p>
<p><strong>Note</strong>: if you want to use the above paths inside a script on <strong>MacOS</strong>, you need to deal with the spaces in folder names - e.g. in <strong>/Application Support/</strong>. Unlike in <strong>Windows</strong>, here you cannot put the whole string between apostrophes, because then the special character &#39;<strong>~</strong>&#39;&#0160;(which means the current user&#39;s folder) would not get resolved. So either you can just escape each space character in the string with a backslash &#39;<strong>\</strong>&#39; e.g.:<br />~/Library/<strong>Application\ Support</strong>/Autodesk/ApplicationPlugins<br />... or put the part after the &#39;<strong>~</strong>&#39; character between apostrophes: &#0160;<br />~&#39;<strong>/Library/Application Support/Autodesk/ApplicationPlugins</strong>&#39;&#0160;</p>
<p>-Adam</p>
