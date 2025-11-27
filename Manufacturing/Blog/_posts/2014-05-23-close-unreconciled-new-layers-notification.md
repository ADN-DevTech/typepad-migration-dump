---
layout: "post"
title: "Close \"Unreconciled New Layers\" notification"
date: "2014-05-23 05:32:00"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "AutoCAD Mechanical"
original_url: "https://adndevblog.typepad.com/manufacturing/2014/05/close-unreconciled-new-layers-notification.html "
typepad_basename: "close-unreconciled-new-layers-notification"
typepad_status: "Publish"
---

<p>By&#0160;<a href="http://adndevblog.typepad.com/manufacturing/adam-nagy.html" target="_self">Adam Nagy</a></p>
<p>For some reason in AutoCAD Mechanical 2015 the &quot;Unreconciled New Layers&quot; bubble notification does not close automatically, and this can caase issues later on.</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01a3fd0e3c90970b-pi" style="display: inline;"><img alt="Unreconciled" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01a3fd0e3c90970b img-responsive" src="/assets/image_c0ba04.jpg" title="Unreconciled" /></a></p>
<p>Kean already posted about how to create your own bubble notifications:<br /><a href="http://through-the-interface.typepad.com/through_the_interface/2008/04/different-ways.html" target="_self" title="">http://through-the-interface.typepad.com/through_the_interface/2008/04/different-ways.html</a><br /><br />The same API also enables you to simply close these notifications. Here is a C# command that does that:</p>
<pre>using System;

using Autodesk.AutoCAD.Runtime;
using acApp = Autodesk.AutoCAD.ApplicationServices.Application;
using Autodesk.AutoCAD.ApplicationServices; 

namespace CsAcadAddIn
{
    public class Class1
    {
        [CommandMethod(&quot;MyCommand&quot;)]
        public void MyCommand()
        {
            Application.StatusBar.CloseBubbleWindows(); 
        }
    }
}</pre>
<pre>&#0160;</pre>
