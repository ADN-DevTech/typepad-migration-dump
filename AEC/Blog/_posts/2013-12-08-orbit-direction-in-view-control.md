---
layout: "post"
title: "Orbit Direction in View Control"
date: "2013-12-08 22:34:58"
author: "Xiaodong Liang"
categories:
  - ".NET"
  - "Navisworks"
  - "Xiaodong Liang"
original_url: "https://adndevblog.typepad.com/aec/2013/12/orbit-direction-in-view-control.html "
typepad_basename: "orbit-direction-in-view-control"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/aec/xiaodong-liang.html" target="_self">Xiaodong Liang</a></p>  <p><strong>Question</strong>    <br />In product, the model orbits along the same direction of the mouse moving, i.e. move to left, model orbits to left. While in view control, the direction is flipped in NavigateOrbit mode. </p>  <p><strong>Solution     <br /></strong>If you look at the Autodesk.Navisworks.Api.Tool enum, you will see the complete list of navigation modes. The “CommonXXX” ones correspond to the AutoCAM navigation modes that are the default for the product. While the NavigateOrbit is the same as the legacy orbit mode.</p>  <p>But in the View Control, you cannot actually use the new AutoCAM navigation modes (all the ones that start “Common…”) as we currently don’t support AutoCAM in the View Control.</p>  <p>In order to let your user to have the same behavior in product and View Control, you can have NavisWorks set to use the legacy navigation modes (which in my opinion are much better anyway), then the ViewControl will behave the same way. i.e. orbit direction is flipped as that of mouse direction.</p>  <p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b019b02676d9f970b-pi"><img style="background-image: none; border-bottom: 0px; border-left: 0px; padding-left: 0px; padding-right: 0px; display: inline; border-top: 0px; border-right: 0px; padding-top: 0px" title="image" border="0" alt="image" src="/assets/image_243998.jpg" width="314" height="256" /></a></p>
