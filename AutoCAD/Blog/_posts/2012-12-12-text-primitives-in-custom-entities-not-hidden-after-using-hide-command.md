---
layout: "post"
title: "Text primitives in custom entities not hidden after using HIDE command"
date: "2012-12-12 15:59:52"
author: "Gopinath Taget"
categories:
  - "2010"
  - "2011"
  - "2012"
  - "2013"
  - "AutoCAD"
  - "Gopinath Taget"
  - "ObjectARX"
original_url: "https://adndevblog.typepad.com/autocad/2012/12/text-primitives-in-custom-entities-not-hidden-after-using-hide-command.html "
typepad_basename: "text-primitives-in-custom-entities-not-hidden-after-using-hide-command"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/gopinath-taget.html">Gopinath Taget</a></p>  <p>Consider this: You have created a Custom Entity and among other primitives in the worldDraw, you draw a text (mode-&gt;geometry().text). After placing the custom entity, When you run the HIDE command in 2D wireframe mode, the text in the custom entity is not hidden. Furthermore, when the text is drawn as a part of the custom entity, some of the other primitives may not be hidden correctly. This problem does not occur when the custom entity&#160; does not have any text primitives.</p>  <p>You might have also noticed that the standard AcDbText entities are not hidden some times. So what is the problem?</p>  <p>You could first try setting the &quot;HIDETEXT&quot; system variable to ON (if it is OFF) at the AutoCAD command prompt. If this does not work, you could try setting thickness to the text. This is because Text objects such as those created by the TEXT, DTEXT, or MTEXT commands are in some cases not affected by the HIDE command. In such cases, for text to be hidden, you need to assign a thickness to the text. The value set for the thickness only needs to be a non-zero value, such as .0001. The programmatic equivalent of this is:</p>  <p><font face="Consolas"><span style="line-height: 11pt"><font color="#0000ff"><font style="font-size: 8pt">double</font></font></span><span style="line-height: 11pt"><font style="font-size: 8pt" color="#000000"> thickOld = mode-&gt;subEntityTraits().thickness();&#160;&#160;&#160;&#160;&#160;&#160;&#160; </font></span></font></p>  <div style="font-family: ; background: white">   <p style="margin: 0px"><span style="line-height: 11pt"><font face="Consolas"><font style="font-size: 8pt" color="#000000">mode-&gt;subEntityTraits().setThickness(0.001);&#160;&#160; </font></font></span></p>    <p style="margin: 0px"><span style="line-height: 11pt"><font face="Consolas"><font style="font-size: 8pt" color="#000000">mode-&gt;geometry().text( ... );&#160;&#160; </font></font></span></p>    <p style="margin: 0px"><span style="line-height: 11pt"><font face="Consolas"><font style="font-size: 8pt" color="#000000">mode-&gt;subEntityTraits().setThickness(thickOld);</font></font></span></p> </div>  <p>Of course, for standard AcDb(M)Text, setThickness() should be used directly on the objects.</p>
