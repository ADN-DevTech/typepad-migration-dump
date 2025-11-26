---
layout: "post"
title: "Navisworks API: Export Model or Model Items to FBX "
date: "2017-03-29 20:04:16"
author: "Xiaodong Liang"
categories:
  - "Navisworks"
  - "Xiaodong Liang"
original_url: "https://adndevblog.typepad.com/aec/2017/03/navisworks-api-export-model-or-model-items-to-fbx-.html "
typepad_basename: "navisworks-api-export-model-or-model-items-to-fbx-"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/aec/xiaodong-liang.html" target="_self">Xiaodong Liang</a></p>
<p>Navisworks product can export whole model to FBX.&nbsp;</p>
<p><a class="asset-img-link" style="display: inline;" href="http://a1.typepad.com/6a016764cbbcf9970b01b8d270b931970c-pi"><img class="asset  asset-image at-xid-6a016764cbbcf9970b01b8d270b931970c img-responsive" title="Model" src="/assets/image_681000.jpg" alt="Model" /></a></p>
<p>There is not a direct way to export some model items only. However, it can be workaround by hiding unnecessary items, and exporting. Then, only those visible items will be exported.</p>
<p><a class="asset-img-link" style="display: inline;" href="http://a6.typepad.com/6a016764cbbcf9970b01b7c8e65596970b-pi"><img class="asset  asset-image at-xid-6a016764cbbcf9970b01b7c8e65596970b img-responsive" title="Modelitems" src="/assets/image_719037.jpg" alt="Modelitems" /></a></p>
<p>The snapshot below is after the FBX is translated by the <a href="http://forge.autodesk.com/">services of Autodesk Forge,</a> and rendered in <a href="https://developer.autodesk.com/en/docs/viewer/v2">Forge Viewer</a>. I simply played with the Autodesk app that is based on the related technologies of Forge:&nbsp;https://a360.autodesk.com/viewer/</p>
<p><a class="asset-img-link" style="display: inline;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb09899eed970d-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b01bb09899eed970d img-responsive" title="Forge" src="/assets/image_87325.jpg" alt="Forge" /></a></p>
<p>From API perspective, no direct method to export FBX, but you can find out the built-in plugin of Exporting and execute it. The code snippet below is a demo.&nbsp;</p>
<p>&nbsp;</p>
<p>&nbsp;</p>

<pre><code>
public override int Execute(params string[] parameters)
{   
  //********
  // hide unnecessary model items. remain those items you wanted to export
  //********

  PluginRecord FBXPluginrecord =
               Autodesk.Navisworks.Api.Application.Plugins.
         FindPlugin("NativeExportPluginAdaptor_LcFbxExporterPlugin_Export.Navisworks");

  if (FBXPluginrecord != null)
  {
        if (!FBXPluginrecord.IsLoaded)
        {
               FBXPluginrecord.LoadPlugin();
        } 

         //save path of the FBX
         string[] pa = { "c:\\temp\\mytest1.fbx" };

         //way 1: by base class of plugin

        //Plugin FBXplugin =
       //           FBXPluginrecord.LoadedPlugin as Plugin;

             
       //FBXplugin.GetType().InvokeMember("Execute",
       //    System.Reflection.BindingFlags.InvokeMethod,
       //    null, FBXplugin, pa);

       //way 2: by specific class of export plugin

       NativeExportPluginAdaptor FBXplugin =
            FBXPluginrecord.LoadedPlugin as NativeExportPluginAdaptor;

                FBXplugin.Execute(pa);
       }

 
    return 0;  
 }
</code></pre>
