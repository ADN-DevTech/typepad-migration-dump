---
layout: "post"
title: "Fusion API: Toggle [Capture Design History]"
date: "2016-08-05 03:31:03"
author: "Xiaodong Liang"
categories:
  - "Fusion 360"
  - "Xiaodong Liang"
original_url: "https://adndevblog.typepad.com/manufacturing/2016/08/fusion-api-toggle-capture-design-history.html "
typepad_basename: "fusion-api-toggle-capture-design-history"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/manufacturing/xiaodong-liang.html" target="_self">Xiaodong Liang</a></p>  <p>There are two primary ways to work in the &quot;Model&quot; environment; parametrically, which focuses on the relationships between features, or directly, which deals only with selected model faces. The main difference is that &quot;Direct Modeling&quot; functions by manipulating faces without regard for previously established relationships.</p>  <p>There are quite a lot of articles on such topic on internet. I just picked two of them:</p>  <p><a href="http://www.engineering.com/tutorials/turning-off-caption-design-history-in-autodesk-fusion-360/">http://www.engineering.com/tutorials/turning-off-caption-design-history-in-autodesk-fusion-360/</a></p>  <p><a href="https://www.youtube.com/watch?v=YJU4avXux2s">https://www.youtube.com/watch?v=YJU4avXux2s</a></p>  <p>There is a switch in [Capture Design History] of the context menu of root node of the browser pane. </p>  <p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c883b286970b-pi"><img style="background-image: none; border-right-width: 0px; padding-left: 0px; padding-right: 0px; display: inline; border-top-width: 0px; border-bottom-width: 0px; border-left-width: 0px; padding-top: 0px" title="clip_image001" border="0" alt="clip_image001" src="/assets/image_60d8f5.jpg" width="252" height="334" /></a></p>  <p>With API, the corresponding object is <strong>adsk.fusion.DesignTypes</strong>.</p>  <pre><code>
def toogleCaptureHistory(isEnabled):
     app = adsk.core.Application.get()
     des = adsk.fusion.Design.cast(app.activeProduct)
     
     if isEnabled:
         des.designType = adsk.fusion.DesignTypes.ParametricDesignType 
     else:
         des.designType = adsk.fusion.DesignTypes.DirectDesignType
    
</code></pre>
