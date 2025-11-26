---
layout: "post"
title: "MotionBuilder eventAddPose callback called twice "
date: "2012-12-19 18:53:54"
author: "Cyrille Fauvel"
categories:
  - "Cyrille Fauvel"
  - "MotionBuilder"
  - "Plug-in"
original_url: "https://around-the-corner.typepad.com/adn/2012/12/motionbuilder-eventaddpose-callback-called-twice-.html "
typepad_basename: "motionbuilder-eventaddpose-callback-called-twice-"
typepad_status: "Publish"
---

<p>The reason that the eventAddPose callback is called twice is because the new pose is connected to the scene and to the folder that contains the pose.
Just check the <em>lEvent.DstPlug</em> pointer and only consider the event when the DstPlug is the FBScene.</p>
<p>Note that for the
eventUpdatePose callback, you may receive it because the scene image
thumbnail shown in the pose tool is connected to the pose. However, there is no really good way of
finding out that a pose as been updated. Only internal data is updated on a pose when an update is done in MoBu, no connection is made, excepted for the
thumbnail - this is why you may see it there and not at other time.</p>
<p>&#0160;</p>
