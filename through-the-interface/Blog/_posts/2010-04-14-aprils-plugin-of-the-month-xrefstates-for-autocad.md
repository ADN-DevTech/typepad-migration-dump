---
layout: "post"
title: "April's plugin of the Month: XrefStates for AutoCAD"
date: "2010-04-14 10:12:01"
author: "Kean Walmsley"
categories:
  - "AutoCAD"
  - "AutoCAD .NET"
  - "Drawing structure"
  - "Plugin of the Month"
original_url: "https://www.keanw.com/2010/04/aprils-plugin-of-the-month-xrefstates-for-autocad.html "
typepad_basename: "aprils-plugin-of-the-month-xrefstates-for-autocad"
typepad_status: "Publish"
---

<p>Yes, I know, I know – we’re halfway through the month, already. This plugin has been <a href="http://labs.autodesk.com/utilities/ADN_plugins" target="_blank">live on Autodesk Labs</a> since the beginning of the month, but I’ve been a little distracted by <a href="http://through-the-interface.typepad.com/through_the_interface/2010/04/childs-play-a-new-tool-for-generating-3d-models-from-childrens-drawings.html" target="_blank">April Fools’ jokes</a>, <a href="http://through-the-interface.typepad.com/through_the_interface/2010/04/importing-photosynth-point-clouds-into-autocad-2011-part-1.html" target="_blank">fooling around</a> <a href="http://through-the-interface.typepad.com/through_the_interface/2010/04/importing-photosynth-point-clouds-into-autocad-2011---part-2.html" target="_blank">with Photosynth</a> <a href="http://through-the-interface.typepad.com/through_the_interface/2010/04/adding-to-autocads-application-menu-and-quick-access-toolbar-using-net.html" target="_blank">point clouds</a> as well as finishing up some internal activities which always tend to take time away from blogging at this time of year.</p>
<p>Anyway, I’ve been remiss talking about this very cool application, but at least Scott was there <a href="http://feedproxy.google.com/~r/ItsAliveInTheLab/~3/Xl5Cj4y6BcE/xrefstates-for-autocad-april-adn-plugin-of-the-month-now-available.html" target="_blank">to announce it on day one</a>.</p>
<p>The application was developed by Glenn Ryan, and there are a number of notable things about this tool. Firstly, it implements something that’s very useful – the ability to save and restore the “loaded/unloaded” state of the various external references in a drawing – and, secondly, it does so in a very elegant way. It’s rare that I come across such a well-structure codebase, and I, for one, learned a number of new tricks when looking into it. Glenn has done a great job with this one, and I fully recommend taking the time to check it out.</p>
<p>There’s quite a lot to the code, so I’m not going to duplicate it here. I will show the main application dialog – in this case with a number of XrefStates created – to give you a feel for its capabilities:</p>
<p><a href="http://through-the-interface.typepad.com/.a/6a00d83452464869e201347fd8c3c8970c-pi"><img alt="XrefStates dialog inside AutoCAD" border="0" height="313" src="/assets/image_830508.jpg" style="BORDER-RIGHT-WIDTH: 0px; DISPLAY: block; FLOAT: none; BORDER-TOP-WIDTH: 0px; BORDER-BOTTOM-WIDTH: 0px; MARGIN-LEFT: auto; BORDER-LEFT-WIDTH: 0px; MARGIN-RIGHT: auto" title="XrefStates dialog inside AutoCAD" width="484" /></a> </p>
<p>Once you’ve set up the XrefStates for your project they get saved inside the master drawing, which allows them to be used in future editing sessions. To do this Glenn chose to use Xrecords, which store the data in a relatively open manner (as opposed to it being locked up in a custom object requiring a enabler).</p>
<p>A big thanks to Glenn for providing this very useful – and well-written – application.</p>
