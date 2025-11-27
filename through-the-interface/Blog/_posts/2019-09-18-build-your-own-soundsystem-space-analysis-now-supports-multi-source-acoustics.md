---
layout: "post"
title: "Build your own SoundSystem: Space Analysis now supports multi-source acoustics"
date: "2019-09-18 21:49:28"
author: "Kean Walmsley"
categories:
  - "Autodesk Research"
  - "Dynamo"
  - "Generative design"
original_url: "https://www.keanw.com/2019/09/build-your-own-soundsystem-space-analysis-now-supports-multi-source-acoustics.html "
typepad_basename: "build-your-own-soundsystem-space-analysis-now-supports-multi-source-acoustics"
typepad_status: "Publish"
---

<p>After somehow adding support for acoustic analysis to the Space Analysis package, Rhys Goldstein has taken it to the next level by adding support for multiple sound sources. When Rhys first did this he was a little surprised by the results he was seeing. There were some really interesting interference patterns at the boundaries between sound sources:</p>
<p><a href="https://through-the-interface.typepad.com/.a/6a00d83452464869e20240a4aed8f7200d-pi" rel="noopener" target="_blank"><img alt="Interference between multiple voronoi points" border="0" height="300" src="/assets/image_756932.jpg" style="margin: 30px auto 10px; border: 0px currentcolor; border-image: none; float: none; display: block; background-image: none;" title="Interference between multiple voronoi points" width="480" /></a></p>
<p style="text-align: center;"><font size="1">Find this example at <em>%appdata%\Dynamo\Dynamo Core\2.3\packages\SpaceAnalysis\extra\spaceanalysis-acoustics-06-six-points-voronoi-pattern.dyn.</em><br />(You may need to change the Dynamo version number.)</font></p>
<p>Rhys dug into the field a little more deeply and came across <a href="https://xmdemo.wordpress.com/2014/03/20/016/" rel="noopener" target="_blank">this interesting blog post</a> that highlights the possibility for sounds of similar frequencies to interfere constructively – resulting in louder sounds – or destructively – resulting in quieter sounds or even silence. Here’s a video from the blog post showing this type of effect:</p>
<p style="text-align: center;"><br /></p>
<p style="text-align: center;"><iframe allow="accelerometer; autoplay; encrypted-media; gyroscope; picture-in-picture" allowfullscreen="" frameborder="0" height="283" src="https://www.youtube.com/embed/xS4xrazild8" width="500"></iframe></p>
<p><br /></p>
<p>Having seen this, Rhys thought it would be fun to see if the Space Analysis package could be used to reproduce the results of this experiment in Dynamo.</p>
<p><a href="https://through-the-interface.typepad.com/.a/6a00d83452464869e20240a4858379200c-pi" rel="noopener" target="_blank"><img alt="Interference between two points" border="0" height="300" src="/assets/image_629178.jpg" style="margin: 30px auto 10px; border: 0px currentcolor; border-image: none; float: none; display: block; background-image: none;" title="Interference between two points" width="480" /></a></p>
<p style="text-align: center;"><font size="1">Find this example at <em>%appdata%\Dynamo\Dynamo Core\2.3\packages\SpaceAnalysis\extra\spaceanalysis-acoustics-05-two-points-interference-validation.dyn.</em><br />(You may need to change the Dynamo version number.)</font></p>
<p>As you can see – and you can see for yourselves by running this graph and counting the centimetre-spaced dots along the top of the screen – there’s a noticeable pattern about 19cm apart, centered on the mid-point between the two sound sources. The blog posts predicts this should be around 21cm apart, so that’s pretty close, especially considering this tool hasn’t been callibrated physically.</p>
<p>In the real world sound sources will often have different frequencies (especially people talking!), so this technique only really makes sense if you have multiple speakers broadcasting the same sound, which is one of the reasons we’ve called the object a SoundSystem: it’s both a system of sounds in an abstract sense, but also could be used to model a sound system in the more literal (hi-fi) sense. SoundSystems are created with a list of points and their relative intensities. It’s also possible to set a global intensity when you create the SoundField: this is a multiplicative intensity that gets applied to all the individual sources in a system.</p>
<p>Here’s a video showing the global intensity being varied on the <em>spaceanalysis-acoustics-04-three-points-sound-system.dyn</em> example, for instance:</p>
<p><a href="https://through-the-interface.typepad.com/.a/6a00d83452464869e20240a48583e7200c-pi" rel="noopener" target="_blank"><img alt="Varying acoustic intensity" height="312" src="/assets/image_146664.jpg" style="margin: 30px auto; float: none; display: block;" title="Varying acoustic intensity" width="500" /></a></p>
<p>If you need to model multiple distinct sources with different frequencies, you can do so by aggregating the results from multiple SoundFields via AudibilityGrid.ByUnion. At some point we may find a way to allow you to model sound sources transmitting at different (and varying) frequencies in the same simulation (rather than aggregating results of multiple simulations), but that’s a performance optimisation we’ll consider if it makes sense down the road.</p>
<p>You can try out multi-source acoustics analysis today in Space Analysis v0.3.3 or higher via the Dynamo Package Manager.</p>
<p>We’d love to get your feedback on this capability, especially if you have some clear use-case for this kind of acoustic analysis in Dynamo. Our goal with the Space Analysis package is to help support the needs of generative design workflows – when combining Dynamo with Refinery – but we’d definitely be interested in hearing about other usage scenarios, too.</p>
