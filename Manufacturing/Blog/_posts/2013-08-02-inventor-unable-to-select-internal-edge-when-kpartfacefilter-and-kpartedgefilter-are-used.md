---
layout: "post"
title: "Inventor: Unable to select internal edge when kPartFaceFilter and kPartEdgeFilter are used"
date: "2013-08-02 12:35:06"
author: "Vladimir Ananyev"
categories:
  - "Inventor"
  - "Vladimir Ananyev"
original_url: "https://adndevblog.typepad.com/manufacturing/2013/08/inventor-unable-to-select-internal-edge-when-kpartfacefilter-and-kpartedgefilter-are-used.html "
typepad_basename: "inventor-unable-to-select-internal-edge-when-kpartfacefilter-and-kpartedgefilter-are-used"
typepad_status: "Publish"
---

<p>by <a href="http://adndevblog.typepad.com/manufacturing/vladimir-ananyev.html">Vladimir Ananyev</a></p>  <p><b>Issue</b></p>  <p>I am using InteractionEvents.SelectEvents and I would like to allow the user to select an edge that is inside of the part and hidden by an outside face (bottom of a hole for instance). If kPartEdgeFilter is used I am unable to select this inside edge. Is there a way to do this?</p>  <p><a name="section2"></a><b>Solution</b></p>  <p>There is a logged Change Request for the ability to select hidden edges with both kPartFaceFilter and kPartEdgeFilter are enabled. However this behavior is consistent with the UI behavior. When no command is active and the selection priority is set to “Select Faces and Edges”, you are not be able to select a hidden edge. The only way to get to it is using the “Select Other” option and loop through the various possibilities. Now the API does not officially support “Select Other” within InteractionEvents. The normal UI behavior of the “Select Other” toolbar popping up after a few seconds has been disabled within InteractionEvents. </p>  <p>However, the user can get to it from the context menu and select that hidden edge (if the mouse points at approximately the same screen location as the edge). And the OnSelect event will fire, albeit with some information (such as ModelPoint of selection) missing.</p>
