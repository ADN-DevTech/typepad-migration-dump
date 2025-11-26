---
layout: "post"
title: "Two quick API tips for Maya - image sample in devkit and registerCommand in Maya 2016.5"
date: "2016-11-22 01:31:04"
author: "Cheng Xi Li"
categories:
  - "C++"
  - "Cheng Xi Li"
  - "Maya"
  - "Plug-in"
  - "Samples"
original_url: "https://around-the-corner.typepad.com/adn/2016/11/two-quick-api-tips-for-maya-image-sample-in-devkit-and-registercommand-in-maya-20165.html "
typepad_basename: "two-quick-api-tips-for-maya-image-sample-in-devkit-and-registercommand-in-maya-20165"
typepad_status: "Publish"
---

<style type="text/css">
pre, code, tt {
  font-size: 12px;
  font-family: Consolas, "Liberation Mono", Courier, monospace;
}

code, tt {
  margin: 0 0px;
  padding: 0px 0px;
  white-space: nowrap;
  background-color: #f8f8f8;
  border-radius: 3px;

}
}


</style>
<p>We have encountered two simple API changes recently, but they each can cause some confusion.</p>
<p><strong>Image Sample issues</strong></p>
<p>The first one is the image extension sample in Maya devkit. <a href="http://help.autodesk.com/view/MAYAUL/2017/ENU/?guid=__files_Appendices_Appendix_F_Adding_Image_Plugins_htm" target="_blank">It is used for creating image extension for adding extra image format support in Maya.</a> It is located in devkit/image folder.</p>
<p>It appears above mentioned sample and its document haven't been updated for a while, and there are two small issues inside of it. Imf.lib is renamed to imfbase.lib now and the project is missing a preprocessor definition (PLUGIN_DLL).</p>
<p><strong>registerCommand in Maya 2016.5</strong></p>
<p>In Maya 2016.5, if you are registering a command with MFnPlugin::registerCommand without giving a newsyntax function. Maya will call creator of your command.</p>
<p>For example, following code is taken from blastCmd sample in Maya devkit.</p>
<p style="border: 1px solid #eaeaea; background-color: #f8f8f8;  line-height:1;"><code>status = plugin.registerCommand( commandName,<br /> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;blastCmd::creator,<br /> // Remove the parameter below will make Maya call creator when the plugin is loaded<br /> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;blastCmd::newSyntax);</code></p>
<p>I've tested with Maya 2016, Maya 2016.5 and Maya 2017 Update 1, this problem only exists in Maya 2016.5. If you are going to build a command plugin for Maya 2016.5, please take it into consideration.</p>
