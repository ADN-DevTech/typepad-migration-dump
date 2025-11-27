---
layout: "post"
title: "Getting room information for Revit models in the Forge Viewer"
date: "2016-06-20 15:20:27"
author: "Kean Walmsley"
categories:
  - "APS (Forge)"
  - "Autodesk"
  - "Autodesk Research"
  - "Conferences"
  - "IoT"
  - "PaaS"
  - "Revit"
original_url: "https://www.keanw.com/2016/06/getting-room-information-for-revit-models-in-the-forge-viewer.html "
typepad_basename: "getting-room-information-for-revit-models-in-the-forge-viewer"
typepad_status: "Publish"
---

<p>This question has come up more than a few times over the last year or so: I remember a number of Revit developers hitting it when creating Viewer applications at the accelerators in Munich and Prague, for instance. The problem appears to be that RVT files – when translated and loaded into the Viewer – do not have the concept of room objects: they’re just spaces. Which presents a challenge for developers who want to work at the room level.</p> <p><a href="http://through-the-interface.typepad.com/.a/6a00d83452464869e201b8d1faa6ed970c-pi" target="_blank"><img title="Dasher 360 with rooms and levels" style="border-top: 0px; border-right: 0px; background-image: none; border-bottom: 0px; float: none; padding-top: 0px; padding-left: 0px; margin: 30px auto; border-left: 0px; display: block; padding-right: 0px" border="0" alt="Dasher 360 with rooms and levels" src="/assets/image_336355.jpg" width="500" height="368"></a></p> <p>Last week I showed a demo during our <a href="http://through-the-interface.typepad.com/through_the_interface/2016/06/autodesk-research-and-iot.html" target="_blank">“Autodesk Research and IoT”</a> session, which showed room-centric navigation built into the Viewer. There’s a reason this works, it seems: because the Revit model we used as a basis was actually comprised of multiple RVT files, we decided to aggregate it into a single NWC file – an export option from Revit – before importing that into our Viewer scene. </p> <p>From talking to a few developers, this seems to be the important step: Navisworks has the concept of rooms and brings this along when imported into Forge. So aside from being a convenience – we could upload and translate a single NWC file, rather than dealing with multiple RVT files – we also get room objects and associated information in the model and its properties.</p> <p>I showed this to Philippe Leefsma and Jeremy Tammik before leaving San Francisco on Friday, who suggested getting the information posted somewhere. So here it is. :-)</p>
