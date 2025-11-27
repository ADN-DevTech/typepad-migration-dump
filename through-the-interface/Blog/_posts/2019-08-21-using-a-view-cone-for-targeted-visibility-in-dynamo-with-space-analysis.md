---
layout: "post"
title: "Using a view cone for targeted visibility in Dynamo with Space Analysis"
date: "2019-08-21 14:24:18"
author: "Kean Walmsley"
categories:
  - "AU"
  - "Autodesk"
  - "Autodesk Research"
  - "Dynamo"
  - "Generative design"
original_url: "https://www.keanw.com/2019/08/using-a-view-cone-for-targeted-visibility-in-dynamo-with-space-analysis.html "
typepad_basename: "using-a-view-cone-for-targeted-visibility-in-dynamo-with-space-analysis"
typepad_status: "Publish"
---

<p>I feel as though I’ve been neglecting this blog: I’ve barely managed a post a week for several weeks, now: partly because of family holidays, but also because I’ve been heads-down on <a href="http://dasher360.com" rel="noopener" target="_blank">Dasher 360</a>, working on a number of cool new features as well as shoring up some of our older code. Anyway, the kids are now back at school and I expect to have a lot more to talk about during the remaining months leading up to <a href="https://www.keanw.com/2019/08/registration-is-open-for-the-forge-devcon-and-au-2019.html" rel="noopener" target="_blank">AU 2019</a>.</p>
<p>For now I wanted to share some information about the Space Analysis package. Today’s post takes a look at a feature that’s been in the public package for some time. In a coming post I’ll focus on some more experimental features that we’re yet to publish relating to acoustics.</p>
<p>This post is about a new visibility-related capability: the ViewCone. This is an object that allows you to perform a more limited visibility analysis within a particular cone of view (i.e. from a point towards a direction with a specified field of view). This is very handy when analysing the view someone has from their desk, for instance.</p>
<p><a href="https://through-the-interface.typepad.com/.a/6a00d83452464869e20240a4a2380b200d-pi"><img alt="View cone in Space Analysis" border="0" height="304" src="/assets/image_85072.jpg" style="margin: 30px auto; border: 0px currentcolor; float: none; display: block; background-image: none;" title="View cone in Space Analysis" width="500" /></a></p>
<p>The object is easy to create, and has a number of parameters that will effect the results once used to create a ViewField. Here’s a quick animation showing what happens when you tweak the various parameters:</p>
<p><a href="https://through-the-interface.typepad.com/.a/6a00d83452464869e20240a47910c0200c-pi" rel="noopener" target="_blank"><img alt="ViewCone" height="245" src="/assets/image_636025.jpg" style="margin: 30px auto; float: none; display: block;" title="ViewCone" width="500" /></a></p>
<p>If you want to have a play with the Dynamo graph, <a href="https://through-the-interface.typepad.com/files/spaceanalysis-visibility-04-one-point-view-cone-kean.dyn" rel="noopener" target="_blank">you can get it from here</a>. It only has cosmetic changes from the one posted with the Space Analysis package (it creates an arrow for the view direction and has thicker/higher walls, too), but it may still be of interest to people.</p>
<p>Incidentally, I’ve been using <a href="https://dynamobim.org/dynamo-core-2-3-release/" rel="noopener" target="_blank">Dynamo Core 2.3</a> for today’s post. I strongly recommend getting a hold of it from <a href="http://dynamobuilds.com" rel="noopener" target="_blank">DynamoBuilds.com</a>. There are <a href="https://github.com/DynamoDS/Dynamo/wiki/Release-Notes" rel="noopener" target="_blank">a number of important enhancements</a>. I’m particularly excited about the team from the <a href="https://www.keanw.com/2019/04/the-first-dynamo-hackathon.html" rel="noopener" target="_blank">London Hackathon</a> getting the API hooks they need to do <a href="https://twitter.com/alvpickmans/status/1163509279325925377" rel="noopener" target="_blank">runtime performance analysis of Dynamo graphs</a>: that’s going to be an extremely useful tool, once completed. I’ll let people know as soon as I hear it’s available, of course!</p>
