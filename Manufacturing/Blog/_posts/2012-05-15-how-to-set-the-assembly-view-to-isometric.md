---
layout: "post"
title: "How to set the assembly view to isometric?"
date: "2012-05-15 07:31:51"
author: "Gary Wassell"
categories:
  - "Gary Wassell"
  - "Inventor"
original_url: "https://adndevblog.typepad.com/manufacturing/2012/05/how-to-set-the-assembly-view-to-isometric.html "
typepad_basename: "how-to-set-the-assembly-view-to-isometric"
typepad_status: "Publish"
---

<p><strong>Issue</strong></p>
<p>I&#39;m trying to achieve the same through the API as clicking &quot;Isometric View F6&quot; in the view&#39;s context menu. I&#39;m using this code. It usually works but in case of some drawings it&#39;s not the same result. What is causing this difference?</p>
<pre><br />&#0160; Set oView = MainAssembly.Views(1)<br />&#0160; Set oCamera = oView.Camera<br />&#0160; oCamera.ViewOrientationType = kIsoTopRightViewOrientation<br />&#0160; oCamera.Apply<br /></pre>
<p><a name="section2"></a></p>
<p><strong>Solution</strong></p>
<p>In different assembly documents (and templates) the Isometric View can be set differently, and can also be overridden by the user via the &quot;Redefine ISO View&quot; command. This setting however has no effect on the built in view orientations in the API. E.g. kIsoTopRightViewOrientation always means that Red(X) axis is pointing bottom right, Green(Y) axis is pointing upwards, and Blue(Z) axis of the coordinate system is pointing bottom left on the screen.</p>
<p>Therefore if you want to get the same result as by the &quot;Isometric View F6&quot; command, then just run it from the API:</p>
<p>In VB.NET</p>
<pre> <p>Dim oCtrlDef As ControlDefinition<br />oCtrlDef = m_inventorApplication.CommandManager.ControlDefinitions.Item(&quot;AppIsometricViewCmd&quot;)<br />oCtrlDef.Execute()</p></pre>
<p>VBA Code Snippet:</p>
<pre><br />ThisApplication.CommandManager.ControlDefinitions(&quot;AppIsometricViewCmd&quot;).Execute<br /></pre>
