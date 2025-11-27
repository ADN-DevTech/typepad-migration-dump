---
layout: "post"
title: "Change Front camera view of model "
date: "2020-02-19 16:24:13"
author: "Adam Nagy"
categories:
  - "Adam"
  - "Inventor"
original_url: "https://modthemachine.typepad.com/my_weblog/2020/02/change-front-camera-view-of-model-.html "
typepad_basename: "change-front-camera-view-of-model-"
typepad_status: "Publish"
---

<p>In the <strong>UI</strong> there is an option to modify the <strong>Front</strong>/<strong>Top</strong>/etc orientation of camera for the model. <br />It&#39;s available under the <strong>View Cube</strong>&#39;s menu item &quot;<strong>Set Current View as</strong>&quot; - see left side of picture</p>
<p>You can also run that command programmatically to achieve the same result.&#0160;<br />First set the camera to the direction that you want to set as the <strong>Front</strong> then run the command &quot;AppViewCubeViewFrontCmd&quot;</p>
<pre>Dim c As Camera
Set c = ThisApplication.ActiveView.Camera
c.ViewOrientationType = kRightViewOrientation
Call c.ApplyWithoutTransition
 
Dim cds As ControlDefinitions
Set cds = ThisApplication.CommandManager.ControlDefinitions
 
Dim cd As ControlDefinition
Set cd = cds.Item(&quot;AppViewCubeViewFrontCmd&quot;)
Call cd.Execute</pre>
<p><a class="asset-img-link" href="https://modthemachine.typepad.com/.a/6a00e553fcbfc688340240a50daa10200b-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="SetCurrentView" border="0" class="asset  asset-image at-xid-6a00e553fcbfc688340240a50daa10200b image-full img-responsive" src="/assets/image_449628.jpg" title="SetCurrentView" /></a></p>
