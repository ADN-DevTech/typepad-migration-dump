---
layout: "post"
title: "Identify plotting during draw overrule"
date: "2016-07-18 05:06:23"
author: "Virupaksha Aithal"
categories:
  - ".NET"
  - "Virupaksha Aithal"
original_url: "https://adndevblog.typepad.com/autocad/2016/07/identify-plotting-during-draw-overrule.html "
typepad_basename: "identify-plotting-during-draw-overrule"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/virupaksha-aithal.html" target="_self">Virupaksha Aithal</a></p>
<p>Recently, I received questions from couple of developers on identifying the plotting state during overrule. Though the context of questions were different (like one developer wanted to avoid doing draw overrule during plot and other wanted kind of applying plot stamp) I felt I need to make the solution as blog.&#0160;To identify the plotting state, use “Context.IsPlotGeneration” as shown in below code</p>
<pre>public override bool WorldDraw(Autodesk.AutoCAD.GraphicsInterface.Drawable drawable, 
            Autodesk.AutoCAD.GraphicsInterface.WorldDraw wd)
{
    if (wd.Context.IsPlotGeneration)
    {
        //code while ploting 
    }
    else
    {
        //code while not ploting 
    }

    return base.WorldDraw(drawable, wd);
}
</pre>
