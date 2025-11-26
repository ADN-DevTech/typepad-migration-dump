---
layout: "post"
title: "Making your Custom Entity worldDraw or viewportDraw fast"
date: "2013-04-12 13:58:15"
author: "Fenton Webb"
categories:
  - ".NET"
  - "2010"
  - "2011"
  - "2012"
  - "2013"
  - "AutoCAD"
  - "Fenton Webb"
  - "ObjectARX"
original_url: "https://adndevblog.typepad.com/autocad/2013/04/making-your-custom-entity-worlddraw-or-viewportdraw-fast.html "
typepad_basename: "making-your-custom-entity-worlddraw-or-viewportdraw-fast"
typepad_status: "Publish"
---

<p>by <a href="http://adndevblog.typepad.com/autocad/fenton-webb.html">Fenton Webb</a></p>  <p><b>Issue</b></p>  <p>My Custom Entity is rather complex, whenever my users call the Rotate or Move command or basically do something which requires continual updating of the graphics it becomes very jerky and slow. How can I improve this?</p>  <p><a name="section2"></a></p>  <p><b>Solution</b></p>  <p>All of the Graphics Primitive functions housed in the AcGiGeometry class return an Adesk::Boolean. It is this return value that must be checked for a value of True so that AutoCAD can efficiently allow degradation of the graphics redraw in order to maintain UI performance. </p>  <p>If the return value comes back as True it is because the Graphic system calculated that the Minimum Frames Per Second (FPS) setting in the Graphics Configuration is being reached *and* that there is a Mouse interaction already in the input queue waiting to be utilized. A condition of True requires that your worldDraw/viewportDraw returns as quickly as possible back to AutoCAD.</p>  <p>Here is an example:</p>  <p>Adesk::Boolean MyEntity::worldDraw(AcGiWorldDraw *wd)   <br />{    <br />&#160;&#160;&#160; assertReadEnabled();    <br />&#160;&#160;&#160; // do some enormous amount of work    <br />&#160;&#160;&#160; for (int i=0; i&lt;1000000; ++i)    <br />&#160;&#160;&#160; {    <br />&#160;&#160;&#160;&#160;&#160;&#160;&#160; // check if AutoCAD is telling you that the user has input pending    <br />&#160;&#160;&#160;&#160;&#160;&#160;&#160; if (wd-&gt;geometry().circle(.., .., ..))    <br />&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; return (false); // abort the work, because a new draw is required    <br />&#160;&#160;&#160; }    <br />}</p>
