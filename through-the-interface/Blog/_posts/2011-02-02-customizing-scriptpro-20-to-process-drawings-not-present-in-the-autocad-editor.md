---
layout: "post"
title: "Customizing ScriptPro 2.0 to process drawings not present in the AutoCAD editor"
date: "2011-02-02 05:40:00"
author: "Kean Walmsley"
categories:
  - "AutoCAD"
  - "AutoCAD .NET"
  - "Batch processing"
  - "Drawing structure"
  - "Plugin of the Month"
original_url: "https://www.keanw.com/2011/02/customizing-scriptpro-20-to-process-drawings-not-present-in-the-autocad-editor.html "
typepad_basename: "customizing-scriptpro-20-to-process-drawings-not-present-in-the-autocad-editor"
typepad_status: "Publish"
---

<p>It’s time to wrap up the series on batch-reporting Registered Application IDs. For reference, here’s how we got to where we are today:</p>
<ol>
<li><a href="http://through-the-interface.typepad.com/through_the_interface/2011/01/counting-the-regappids-for-the-active-autocad-drawing-and-its-external-references-using-net.html" target="_blank">Implement a command to collect RegAppId information for the active document</a> </li>
<li><a href="http://through-the-interface.typepad.com/through_the_interface/2011/01/counting-the-regappids-for-a-specific-autocad-drawing-and-its-external-references-using-net.html" target="_blank">Extend this command to work on a drawing not loaded in the editor</a> </li>
<li><a href="http://through-the-interface.typepad.com/through_the_interface/2011/01/persisting-regappid-data-from-an-external-autocad-drawing-to-xml-using-net.html" target="_blank">Save our RegAppId information to some persistent location (XML)</a> 
<ul>
<li><a href="http://through-the-interface.typepad.com/through_the_interface/2011/01/transforming-a-list-of-regappids-from-xml-to-html-using-xslt.html" target="_blank">Transform the resulting XML file to HTML using XSLT</a> </li>
</ul>
</li>
<li>Create a modified version of ScriptPro 2.0 (one of our <a href="http://labs.autodesk.com/utilities/ADN_plugins/" target="_blank">Plugins of the Month</a>) to call our command without opening the drawing </li>
</ol>
<p>The broader point of today’s post – other than simply to deliver on step 4, above – is to demonstrate that there are definitely simple options when it comes to architecting and building your own batch-processing tool. It’s quite possible that ScriptPro 2.0 does what you want, out of the box, but in our case it does not: we do not want to load a drawing into the AutoCAD editor to process it, we want to run a custom command to load drawings in side databases in order to process them more quickly.</p>
<p>But as the full source code for ScriptPro 2.0 is available on Autodesk Labs, it’s a simple matter of editing it for our own purposes. I won’t go into great detail on the changes that were needed – most of which were cosmetic in nature – but I will list a few areas:</p>
<ul>
<li>Changed the name of the application from ScriptPro 2.0 to RegAppReport </li>
<li>Stripped down the UI of the main dialog 
<ul>
<li>Reduced unwanted items from the ribbon (load SCP, for instance) </li>
<li>Removed the script selection box and associated code </li>
<li>I didn’t bother changing the options dialog, at all, but I did change the load/save of the drawing lists not to include that information </li>
</ul>
</li>
<li>Removed the code to open the selected document in the AutoCAD editor </li>
<li>Customized the code to call our XRA command – rather than SCRIPT – passing in the DWG we want to process via the command-line </li>
<li>Added some code to copy the file placed at <em>C:\RegAppData.xml</em> to the executable location, where our XSLT resides 
<ul>
<li>I also decided to back up the previous contents of this file, in case they were needed </li>
<li>We could also have specified the XML as an argument to the command, but felt it less disruptive (in terms of changes to the previous implementation) to do it this way </li>
</ul>
</li>
<li>We give the option of automatically launching the XML report (rendered to HTML, of course) after processing </li>
</ul>
<p>It should be noted that the code developed in the previous posts, defining the XRA command, should be built into a plugin and set up to load <a href="http://through-the-interface.typepad.com/through_the_interface/2006/09/automatic_loadi.html" target="_blank">automatically into AutoCAD</a> at startup or when the XRA command is called (you can, of course, <a href="http://through-the-interface.typepad.com/through_the_interface/2010/03/creating-demand-loading-entries-automatically-for-your-autocad-application-using-c-f-or-vbnet.html" target="_blank">set this up programmatically</a>).</p>
<p>Here are <a href="http://through-the-interface.typepad.com/files/RegAppReport.zip" target="_blank">the C# source project and executable files for the modified ScriptPro 2.0 application</a>.</p>
<p>Here’s the application in action…</p>
<p><a href="http://through-the-interface.typepad.com/.a/6a00d83452464869e20147e231f57f970b-pi"><img alt="Our customized ScriptPro 2.0 dialog" border="0" height="256" src="/assets/image_272732.jpg" style="background-image: none; margin: 0px auto; padding-left: 0px; padding-right: 0px; display: block; float: none; padding-top: 0px; border-width: 0px;" title="Our customized ScriptPro 2.0 dialog" width="479" /></a></p>
<p><a href="http://through-the-interface.typepad.com/.a/6a00d83452464869e20147e231f5d1970b-pi"><img alt="With files loaded" border="0" height="256" src="/assets/image_268756.jpg" style="background-image: none; margin: 0px auto; padding-left: 0px; padding-right: 0px; display: block; float: none; padding-top: 0px; border-width: 0px;" title="With files loaded" width="479" /></a></p>
<p><a href="http://through-the-interface.typepad.com/.a/6a00d83452464869e20148c83b2851970c-pi"><img alt="During processing 1" border="0" height="256" src="/assets/image_666206.jpg" style="background-image: none; margin: 0px auto; padding-left: 0px; padding-right: 0px; display: block; float: none; padding-top: 0px; border-width: 0px;" title="During processing 1" width="479" /></a></p>
<p><a href="http://through-the-interface.typepad.com/.a/6a00d83452464869e20147e231f64d970b-pi"><img alt="During processing 2" border="0" height="256" src="/assets/image_704152.jpg" style="background-image: none; margin: 0px auto; padding-left: 0px; padding-right: 0px; display: block; float: none; padding-top: 0px; border-width: 0px;" title="During processing 2" width="479" /></a></p>
<p><a href="http://through-the-interface.typepad.com/.a/6a00d83452464869e20148c83b28e9970c-pi"><img alt="Executed" border="0" height="256" src="/assets/image_586022.jpg" style="background-image: none; margin: 0px auto; padding-left: 0px; padding-right: 0px; display: block; float: none; padding-top: 0px; border-width: 0px;" title="Executed" width="479" /></a></p>
<p>And finally the results…</p>
<p><a href="http://through-the-interface.typepad.com/.a/6a00d83452464869e20147e231f6ca970b-pi"><img alt="Our RegAppId report" border="0" height="431" src="/assets/image_35601.jpg" style="background-image: none; margin: 0px auto; padding-left: 0px; padding-right: 0px; display: block; float: none; padding-top: 0px; border-width: 0px;" title="Our RegAppId report" width="370" /></a></p>
<p>It’s altogether possible to extend this application to actually – even selectively – purge RegAppIds from drawings, but that’s being left as an exercise for the reader.</p>
