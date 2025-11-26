---
layout: "post"
title: "How do I set the base point for your custom entity when doing a &quot;pasteclip&quot;?"
date: "2013-01-14 17:56:37"
author: "Gopinath Taget"
categories:
  - "2010"
  - "2011"
  - "2012"
  - "2013"
  - "AutoCAD"
  - "Gopinath Taget"
  - "ObjectARX"
original_url: "https://adndevblog.typepad.com/autocad/2013/01/how-do-i-set-the-base-point-for-your-custom-entity-when-doing-a-pasteclip.html "
typepad_basename: "how-do-i-set-the-base-point-for-your-custom-entity-when-doing-a-pasteclip"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/gopinath-taget.html">Gopinath Taget</a></p>  <p>When you use the &quot;copyclip&quot;, then &quot;pasteclip&quot;, AutoCAD assumes the insertion point as the bottom left hand corner of the extents of the objects copied, this is as designed and there is no custom entity function that we can override to achieve this.</p>  <p>However, it can be accomplished using reactors. You will need to implement an AcEditorReactor which watches for the commandWillStart() &quot;pasteclip&quot;, if the &quot;pasteclip&quot; command is started then set a flag which in turn is watched for in the AcDbEntity::transformBy() function. In the AcDbEntity::transformBy() you can modify the &quot;pasteclip&quot; insertion point of your object.</p>
