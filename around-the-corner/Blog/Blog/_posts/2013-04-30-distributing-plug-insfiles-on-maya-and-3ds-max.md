---
layout: "post"
title: "Distributing plug-ins/files on Maya (and 3ds Max)"
date: "2013-04-30 08:57:45"
author: "Cyrille Fauvel"
categories:
  - "3ds Max"
  - "Autodesk Exchange"
  - "Cyrille Fauvel"
  - "Linux"
  - "Mac"
  - "Maya"
  - "Plug-in"
  - "Windows"
original_url: "https://around-the-corner.typepad.com/adn/2013/04/distributing-plug-insfiles-on-maya-and-3ds-max.html "
typepad_basename: "distributing-plug-insfiles-on-maya-and-3ds-max"
typepad_status: "Publish"
---

<p>You probably remember this <a href="http://around-the-corner.typepad.com/adn/2012/07/distributing-files-on-maya-maya-modules.html" target="_self">article</a>&#0160;where I described in details how to work with Maya Modules and integrate your files and plug-ins in Maya. This time, I am going to explain what changes were made in Maya to support the <a href="http://apps.exchange.autodesk.com" target="_self">Autodesk Exchange Store</a>.</p>
<p><a class="asset-img-link" href="http://around-the-corner.typepad.com/.a/6a0163057a21c8970d016768395890970b-pi" style="display: inline;"><img alt="Installer" border="0" class="asset  asset-image at-xid-6a0163057a21c8970d016768395890970b" src="/assets/image_9aa6bf.jpg" style="display: block; margin-left: auto; margin-right: auto;" title="Installer" /></a></p>
<p>To submit an application / content package on Autodesk Exchange Store, you need to follow few but important rules. The application should be self-contained and easy to install for example. Self-contained means that all files should go in one single directory structure and outside the host application directory structure. In the past, I know, many people were copying their files directly into &#39;c:\Program Files\Autodesk\MayaNNNN&#39; or &#39;/Applications/Autodesk/MayaNNNN&#39;, but for Exchange this is not allowed and unacceptable. </p>
<p>The other thing with the legacy Maya Module approach was that you needed to have a text file posted under the user profile at a specific place, meaning the installer had to determine which Maya version you wanted to use in case you had many on your computer. Many people were instead having a readme to explain people to copy files here and here, to make changes in their Maya.env or&#0160;userPrefs.mel file, etc... and that was true as well for the Maya Bonus Tools ;)</p>
<p>The last but not least limitation was about initializing the application in Maya. Like the Maya Bonus Tools, you could provide a userSetup.mel to initialize your menu, shelve, plug-ins, ... but in remember that Maya load one and only one userSetup.mel file (and only the first one found :(</p>
<p>The Exchange approach is that you now put your application in one common place, and describe your application. Any Autodesk software watch that folder and if the application says &#39;I am for Maya 2014&#39;, Maya 2014 will load the application. If it says &#39;I am for Maya 2014, AutoCAD 2014 and 3ds Max 2014 &amp; 2015&#39;, these 4 app/version will load the application but not the others.</p>
<h3>Describe your application</h3>
<p>Let&#39;s take a quick look to my favorite little Maya Math node sample in Python. The XML description file looks like this:</p>
<pre class="brush: xml; toolbar: false;">&lt;?xml version=&quot;1.0&quot; encoding=&quot;utf-8&quot;?&gt;
&lt;ApplicationPackage SchemaVersion=&quot;1.0&quot;
	ProductType=&quot;Application&quot;
	AutodeskProduct=&quot;Maya&quot;
	Name=&quot;MathNode&quot;
	Description=&quot;Autodesk Maya MathNode&quot;
	AppVersion=&quot;1.0.0&quot;
	Author=&quot;Autodesk&quot;
	AppNameSpace=&quot;com.autodesk.exchange.maya.mathnode&quot;
	HelpFile=&quot;./Contents/docs/index.html&quot;
	OnlineDocumentation=&quot;http://www.autodesk.com/maya&quot;
	ProductCode=&quot;*&quot;
	UpgradeCode=&quot;{52c87085-07d5-4cfa-b76e-e348553c30ac}&quot; &gt;
	
	&lt;CompanyDetails Name=&quot;Autodesk&quot;
		Phone=&quot; &quot;
		Url=&quot;http://www.autodesk.com&quot;
		Email=&quot;labs.plugins@autodesk.com&quot; /&gt;

	&lt;!-- Prevent to load in older version than Maya 2008 --&gt;
	&lt;RuntimeRequirements SupportPath=&quot;./Contents/docs&quot; OS=&quot;win64|macOS|linux&quot; Platform=&quot;Maya&quot; SeriesMin=&quot;2008&quot; &#0160;/&gt;

	&lt;Components&gt;
		&lt;RuntimeRequirements SupportPath=&quot;./Contents/docs&quot; OS=&quot;win64|macOS|linux&quot; Platform=&quot;Maya&quot; SeriesMin=&quot;2008&quot; /&gt;
		&lt;MayaEnv expr=&quot;MAYA_SCRIPT_PATH+:=shelves&quot; /&gt;
		&lt;ComponentEntry ModuleName=&quot;./Contents/plug-ins/asdkMathNode.py&quot; AutoLoad=&quot;True&quot; /&gt;
		&lt;ComponentEntry ModuleName=&quot;./Contents/scripts/AEasdkMathNodeTemplate.mel&quot; /&gt;
		&lt;ComponentEntry ModuleName=&quot;./Contents/scripts/MathNode_load.mel&quot; /&gt;
		&lt;ComponentEntry ModuleName=&quot;./Contents/icons/MathNode.png&quot; /&gt;
		&lt;ComponentEntry ModuleName=&quot;./Contents/shelves/MathNode_shelf.mel&quot; /&gt;
	&lt;/Components&gt;
	
&lt;/ApplicationPackage&gt;
</pre>
<p>The &#39;ApplicationPackage&#39; and the first &#39;RuntimeRequirements&#39; tags are the ones all Autodesk Application are watching to determine if the package if something they should eventually consider loading. The OS, Platform, can contain multiple values.</p>
<p>Each following &#39;Components&#39; tag will describe an entire application like here, or components. In short, I could have had something like:</p>
<pre class="brush: xml; toolbar: false;">	&lt;Components&gt;
		&lt;RuntimeRequirements SupportPath=&quot;./Contents/docs&quot; OS=&quot;win64&quot; Platform=&quot;3dsMax&quot; SeriesMin=&quot;2013&quot; SeriesMax=&quot;2014&quot; /&gt;
		&lt;ComponentEntry ModuleName=&quot;./Contents/plug-ins/asdkMathNode-x64.dlu&quot; AutoLoad=&quot;True&quot; /&gt;
	&lt;/Components&gt;
	&lt;Components&gt;
		&lt;RuntimeRequirements SupportPath=&quot;./Contents/docs&quot; OS=&quot;win32&quot; Platform=&quot;3dsMax&quot; SeriesMin=&quot;2013&quot; SeriesMax=&quot;2013&quot; /&gt;
		&lt;ComponentEntry ModuleName=&quot;./Contents/plug-ins/asdkMathNode-x86.dlu&quot; AutoLoad=&quot;True&quot; /&gt;
	&lt;/Components&gt;

	&lt;Components&gt;
		&lt;RuntimeRequirements SupportPath=&quot;./Contents/docs&quot; OS=&quot;win64|macOS|linux&quot; Platform=&quot;Maya&quot; SeriesMin=&quot;2008&quot; /&gt;
		&lt;ComponentEntry ModuleName=&quot;./Contents/plug-ins/asdkMathNode.py&quot; AutoLoad=&quot;True&quot; /&gt;
	&lt;/Components&gt;

	&lt;Components&gt;
		&lt;RuntimeRequirements SupportPath=&quot;./Contents/docs&quot; OS=&quot;win64|macOS|linux&quot; Platform=&quot;Maya|3dsMax&quot; /&gt;
		&lt;ComponentEntry ModuleName=&quot;./Contents/docs/ReadMeFirst.txt&quot; /&gt;
	&lt;/Components&gt;
</pre>
<p>Which tells there is an application for:</p>
<ul>
<li>3ds Max 2013 - 32-bits</li>
<li>3ds Max 2013 &amp; 2014 - x64</li>
<li>Maya 2008 and beyond on Windows x64 / Max OSX, Linux</li>
</ul>
<p>and a readme for</p>
<ul>
<li>all OS,</li>
<li>3ds Max and Maya (all releases)</li>
</ul>
<p>Nothing really new for Maya, but no more user profile text file to tell where to find a module on disk. For 3ds Max this is a completely new way for plug-ins to work with 3ds Max and would recommend using that approach instead of copying into the 3ds Max directory structure.</p>
<h3>Autoload</h3>
<p>One new thing for Maya is that Maya will now call a &lt;moduleName&gt;_load.mel/.py file as soon as a module is detected. Is that _load script, it is your chance to initialize menus, shelves, or anything you want. Note this _load script is source&#39;d right before the userSetup.mel script which gives you a chance to setup your application environment and let the user to change it if he needs to.</p>
<p>But, Maya will also load modules at runtime. That means you would not need to restart Maya if you decide to install an Exchange compliant application (this can be turned off if you unload the &#39;autoLoader&#39; plug-in in the Plug-in Manager).</p>
<h3>Installer</h3>
<p>Because the files are self-contained, and we got an XML file which describe the application, it is very easy to create installers for the 3 platforms in few minutes. The MSI, PKG, and Shell script installers for Maya are entirely automated and takes only 5 minutes once you got your application working. There is no additional work to do - you give the path where to find the files, and the AppPacker tool reads the XML file and directory structure to produce all the installers. The ADN team will create these installers for you and will help you to make changes in your application for a better Exchange Store integration.</p>
<p>You are unsure where to start - go <a href="http://around-the-corner.typepad.com/adn/2013/04/a-new-distribution-channel-for-maya-3ds-max.html" target="_self">here</a>&#0160;and/or&#0160;<a href="http://usa.autodesk.com/adsk/servlet/index?id=17183245&amp;siteID=123112" target="_self">Autodesk Developer Center</a>&#0160;</p>
<p>You got plug-ins for year without installer and interested to have one without knowing where to start? I got the scripts and tools on GitHub and willing to share them with individuals. If interested, drop me a line with your <a href="https://github.com/" target="_self">GitHub</a> account name. In future, I&#39;ll make them in public repository, but at this time, I am still testing couple of more complex scenario.</p>
<p>&#0160;</p>
