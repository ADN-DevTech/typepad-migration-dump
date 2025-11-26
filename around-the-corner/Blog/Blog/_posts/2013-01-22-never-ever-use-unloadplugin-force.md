---
layout: "post"
title: "Never ever use \"unloadPlugin -force\"!!!"
date: "2013-01-22 03:29:23"
author: "Cyrille Fauvel"
categories:
  - "Cyrille Fauvel"
  - "Maya"
  - "Plug-in"
original_url: "https://around-the-corner.typepad.com/adn/2013/01/never-ever-use-unloadplugin-force.html "
typepad_basename: "never-ever-use-unloadplugin-force"
typepad_status: "Publish"
---

<p>I would encourage everyone to completely avoid use of the MEL command &quot;unloadPlugin –force pluginName&quot; or its Python equivalent cmds.unloadPlugin(&quot;pluginName&quot;, force=true). And, especially when writing vital tests.</p>
<p>The force flag of the unloadPlugin is a documented (and almost guaranteed way) of creating dangling pointers. Here&#39;s what Maya&#39;s User Manual has to say about it:</p>
<p style="padding-left: 30px;"><em>Unload the plug-in even if it is providing services. <span style="color: red;">This is not recommended.</span> If you unload a plug-in that implements a node or data type in the scene, those instances will be converted to unknown nodes or data and the scene will no longer behave properly. <span style="color: red;">Maya may become unstable or even crash.</span> If you use this flag you are advised to save your scene in MayaAscii format and restart Maya as soon as possible.</em></p>
<p>A plug-in which has a node in the DG (or a command in the undo queue) cannot be be safely unloaded. Forcing the unload will only create dangling pointers, memory corruption and might lead to crashes. The normal solution is simply to empty the DG and the undo queue by performing a &quot;file –new&quot;. Normally, you should be able to unload the plug-in afterward.<br /> <br />Unfortunately, there are some issues with MFnPlugin::registerModelEditorCommand().  Any plug-in registering a model editor command would immediately be marked as being unsafe to unload forever. Emptying the scene and the undo queue would not help. Getting of all UI elements created by the invocation on the registered model editor command would not help neither. I.e. There would be absolutely no way to safely unload the plug-in without creating dangling pointers, memory corruptions and potential crashes…</p>
<p>Forcing the plug-in to unload while it still has nodes present in the scene still isn&#39;t harmless. There&#39;s information about the node which Maya will need to make or break connections, set or get attribute values, delete the node, etc, and some of that information is lost when the plug-in unloads. Maya won&#39;t know that the data is no longer there so it can end up chasing bogus pointers with all the instability that brings.<br /><br />Alternatively if you want to store some settings in a scene without using a plug-in node, you can do one of the following:</p>
<ul>
<li>Store them in the scene&#39;s fileInfo database. See the &#39;fileInfo&#39; command for more details.</li>
<li>Create an instance of a low-overhead built-in node, like &#39;choose&#39;, and then add dynamic attrs to it to store your settings.</li>
<li>Same as the previous suggestion except that as of Maya 2013.5 you can also store the settings as metadata on the node.</li>
</ul>
