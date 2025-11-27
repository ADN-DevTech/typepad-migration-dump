---
layout: "post"
title: "Visual clustering is now live in Dasher"
date: "2020-06-03 17:19:15"
author: "Kean Walmsley"
categories:
  - "APS (Forge)"
  - "Autodesk Research"
  - "IoT"
original_url: "https://www.keanw.com/2020/06/visual-clustering-is-now-live-in-dasher.html "
typepad_basename: "visual-clustering-is-now-live-in-dasher"
typepad_status: "Publish"
---

<p>I have a quick update to share on Dasher, today: I’ve just pushed live a version to the <a href="http://dasher360.com" target="_blank">main Dasher site</a> that enables a core Forge viewer extension to perform visual clustering of model contents.</p><p><a href="https://www.keanw.com/2020/01/visual-clustering-model-contents-using-the-forge-viewer.html" target="_blank">I talked about this back at the beginning of the year</a>, but there was a core change needed to the Forge viewer to make it work for Dasher: specific details are in the blog post I just linked to, but it relates to the fact we rely on the Navisworks format for our models, and these have a different property structure when loaded through Forge. Passing in a “searchAncestors” option as true when you load the extension will allow it to work with models published to NWC, for Forge developers interested in doing so.</p><p>I’m proud to say it’s a little piece of my code that allows this feature to work with Navisworks models. Many thanks to Henrik Buchholz for his help guiding me through the process of getting a code submission accepted in the Forge viewer (my first!) as well as to the rest of the BIM 360 Design for their work on this very cool feature. </p><p>You can give this feature a try for yourself on the demo page for Dasher, by clicking on the button in the main Forge viewer toolbar:</p><p><a href="https://through-the-interface.typepad.com/.a/6a00d83452464869e20264e2dff9f9200d-pi" target="_blank"><img width="504" height="143" title="Visual Clustering is live in Dasher" style="margin: 30px auto; float: none; display: block; background-image: none;" alt="Visual Clustering is live in Dasher" src="/assets/image_495135.jpg" border="0"></a></p><p>Here’s an animation of the feature being used (although bear in mind that you’ll almost certainly see some lag after you click the button that isn’t reflected in this animation):</p><p><a href="https://through-the-interface.typepad.com/.a/6a00d83452464869e20264e2dff9fd200d-pi" target="_blank"><img width="500" height="313" title="Visual Clusters in Dasher" style="margin: 30px auto; float: none; display: block;" alt="Visual Clusters in Dasher" src="/assets/image_883612.jpg"></a></p><p>While this perhaps doesn’t directly relate to the core IoT-centric features of Dasher, it is likely to be relevant to features we build into Dasher longer-term. And in the near-term it’s an opportunity for people to take a look at how it works and see whether it’s of any immediate value.</p><p>A few more changes have been made recently in Dasher that I’ll describe in more detail before too long.</p>
