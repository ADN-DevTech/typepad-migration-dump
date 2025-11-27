---
layout: "post"
title: "Registry Free AddIn with Visual Studio 2010 Express"
date: "2012-10-08 09:22:55"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "Inventor"
original_url: "https://adndevblog.typepad.com/manufacturing/2012/10/registry-free-addin-with-visual-studio-2010-express.html "
typepad_basename: "registry-free-addin-with-visual-studio-2010-express"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/cloud_and_mobile/adam-nagy.html" target="_self">Adam Nagy</a></p>
<p>There are some articles on the steps of creating a <strong>Registry Free Inventor AddIn</strong>:<br /><a href="http://wikihelp.autodesk.com/Inventor_ETO/enu/2012/Help/0000-Inventor0/0012-Creating12/0013-Designin13/0018-Creating18/0030-Appendix30/0031-Making_y31" target="_self">Making your Visual Basic Add-In Registry Free<br /></a><a href="http://modthemachine.typepad.com/my_weblog/2012/02/migrate-net-add-ins-to-registry-free.html#comments" target="_self">Migrate .Net Add-Ins to registry free</a></p>
<p><a href="http://modthemachine.typepad.com/my_weblog/2012/02/migrate-net-add-ins-to-registry-free.html#comments" target="_self"></a>However, a thing worth pointing out is that you could also use the .NET Wizard to do the necessary steps.<br />If you go on the <a href="http://www.autodesk.com/developinventor" target="_self">http://www.autodesk.com/developinventor</a> page then you can download an updated version of <strong>DeveloperTools.msi</strong>, which also installs Project Wizards for both <strong>Visual Studio&#0160;2010</strong> and <strong>Visual Studio 2010 Express</strong>.</p>
<p>After the install, when you go into <strong>Visual Studio</strong> then you&#39;ll find a new project type called <strong>Autodesk Inventor 2013 AddIn</strong>.</p>
<p>
<a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b017d3c941fde970c-pi" style="display: inline;"><img alt="InventorWizard" border="0" class="asset  asset-image at-xid-6a0167607c2431970b017d3c941fde970c image-full" src="/assets/image_930b03.jpg" title="InventorWizard" /></a></p>
<p>This project will take care of the manifest file and addin file creation, plus the post build setting that embeds the manifest file in the created dll using <strong>mt.exe</strong>. If mt.exe is not available on your computer, then the build will fail.<br /><strong>Note: mt.exe</strong> is not part of the <strong>Visual Basic 2010 Express</strong> or <strong>Visual C# 2010 Express</strong> install, but is part of <strong>Visual C++ 2010 Express</strong>, so one way to get access to that file is through installing that.</p>
<p>Once you saved your project then you can locate its files (default save path is <strong>&lt;Documents folder&gt;\Visual Studio 2010\Projects</strong>), which also include <strong>Readme.txt</strong>. This provides you information about where to place the <strong>*.addin</strong> file and what it should contain&#0160;so that <strong>Inventor</strong> will load your AddIn .</p>
<p><strong>Visual Studio 2010 Express</strong> does not let you edit some of the project settings, inc. post build steps or startup exe for debug. However, you can edit the project file directly to get around this limitation.</p>
<p>If you close your project and then open up the project file (*.vbproj or *.csproj) in Notepad, then you can add the highlighted lines to it so that when you hit <strong>F5</strong>&#0160;when your project is loaded in <strong>Visual Studio</strong> then <strong>Inventor</strong> will be started:</p>
<p><span style="font-family: &#39;courier new&#39;, courier; font-size: 8pt;">...</span><br /><span style="font-family: &#39;courier new&#39;, courier; font-size: 8pt;">&lt;PropertyGroup Condition=&quot; &#39;$(Configuration)|$(Platform)&#39; == &#39;Debug|AnyCPU&#39; &quot;&gt;</span><br /><span style="font-family: &#39;courier new&#39;, courier; font-size: 8pt;"><strong>&#0160;&lt;StartAction&gt;Program&lt;/StartAction&gt;</strong></span><br /><span style="font-family: &#39;courier new&#39;, courier; font-size: 8pt;"><strong>&#0160;&lt;StartProgram&gt;C:\Program Files\Autodesk\Inventor 2013\Bin\Inventor.exe&lt;/StartProgram&gt;</strong></span><br /><span style="font-family: &#39;courier new&#39;, courier; font-size: 8pt;">...&#0160;</span></p>
<p>You can find more detailed information about this in the <strong>Autodesk Inventor 2013 API Help</strong> (C:\Program Files\Autodesk\Inventor 2013\Local Help\admapi_17_0.chm) under <strong>Inventor API User&#39;s Manual &gt;&gt; API Overviews &gt;&gt; Creating an Inventor Add-In</strong></p>
