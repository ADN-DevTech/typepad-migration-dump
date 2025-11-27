---
layout: "post"
title: "September&rsquo;s Plugin of the Month: DigSigStamp"
date: "2010-09-02 13:34:08"
author: "Kean Walmsley"
categories:
  - "AutoCAD"
  - "AutoCAD .NET"
  - "Blocks"
  - "Graphics system"
  - "Overrules"
  - "Plugin of the Month"
original_url: "https://www.keanw.com/2010/09/septembers-plugin-of-the-month-digsigstamp.html "
typepad_basename: "septembers-plugin-of-the-month-digsigstamp"
typepad_status: "Publish"
---

<p>Yesterday Scott Sheppard <a href="http://labs.blogs.com/its_alive_in_the_lab/2010/09/september-adn-plugin-of-the-month-digsigstamp-for-autocad-now-available.html" target="_blank">announced the availability of this plugin over on It’s Alive in the Lab</a>.</p>  <p>We originally received a request for this <a href="http://labs.autodesk.com/utilities/adn_plugins" target="_blank">Plugin of the Month</a> some time ago. Fenton Webb, from our DevTech Americas team, developed the initial version using an ObjectARX custom entity – as the requester required support for versions of AutoCAD prior to 2010 – but for this public release, Stephen Preston went ahead and re-implemented the mechanism in a .NET application using the Overrules API introduced in AutoCAD 2010.</p>  <p>This plugin basically allows you to see graphically when an AutoCAD drawing is digitally signed, which is apparently a requirement for some branches of government. The idea is that you specify a couple of different block definitions to indicate what should be displayed when the drawing is signed and when it’s not. <a href="http://through-the-interface.typepad.com/.a/6a00d83452464869e20133f3848eb7970b-pi"><img style="border-right-width: 0px; margin: 20px 15px 20px 20px; display: inline; border-top-width: 0px; border-bottom-width: 0px; border-left-width: 0px" title="This drawing has a valid digital signature" border="0" alt="This drawing has a valid digital signature" align="right" src="/assets/image_831960.jpg" width="298" height="146" /></a>You then choose a particular block reference – which probably sits somewhere on your title sheet – to have it’s WorldDraw() display function overruled to display the appropriate block definition, depending on whether or not the drawing has been signed and has a valid signature attached. The “valid” block definition can contain attributes that get populated with information provided in the digital signature. </p>  <p>A number of fairly innocuous activities – such as panning and zooming – invalidate a drawing’s digital signature. While we choose to display the appropriate “invalid” block in the editor, when the drawing is plotted we do a little more work, to make sure the drawing gets plotted as “valid”.</p>  <p>I’m actually not going to post the code for this one to my blog (you can download it from the Labs site, of course). The plugin uses a few tricks to implement a relatively rudimentary security mechanism: if people need something more secure, they will need to invest some effort themselves. If we went the whole hog and implemented industrial-strength security for this plugin, we clearly wouldn’t be able to publish the source code. We therefore decided to leave it fairly lightweight and open for people to extend as they see fit. To benefit those people choosing to use the implementation as-is, I’ve left the implementation details in the source that comes along with the plugin.</p>
