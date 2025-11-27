---
layout: "post"
title: "Service Pack 1 for AutoCAD 2013"
date: "2012-08-24 06:15:00"
author: "Kean Walmsley"
categories:
  - "AutoCAD"
  - "AutoLISP / Visual LISP"
  - "Security"
  - "Visual Basic &amp; VBA"
original_url: "https://www.keanw.com/2012/08/service-pack-1-for-autocad-2013.html "
typepad_basename: "service-pack-1-for-autocad-2013"
typepad_status: "Publish"
---

<p>As reported <a href="http://withoutanet.typepad.com/without_a_net/2012/08/new-security-controls-in-autocad-2013-sp1-help-combat-malware.html" target="_blank">by Tom Stoeckel over on Without a Net</a>, AutoCAD 2013 Service Pack 1 is <a href="http://usa.autodesk.com/getdoc/id=DL20327837" target="_blank">now available for download</a>. I’ve been waiting for this release with some impatience… in my new role I’ve been increasingly involved in discussions around the security of AutoCAD and our customers’ data, and this Service Pack makes significant progress in this area.</p>
<p><a href="http://through-the-interface.typepad.com/.a/6a00d83452464869e20177444c1a02970d-pi" target="_blank"><img align="right" alt="Padlock" border="0" height="166" src="/assets/image_56253.jpg" style="background-image: none; margin: 10px 0px 10px 10px; padding-left: 0px; padding-right: 0px; display: inline; float: right; padding-top: 0px; border-width: 0px;" title="Padlock" width="247" /></a>As Tom notes, malware attacks in AutoCAD typically<sup>†</sup> take advantage of the fact that when a drawing is loaded, AutoCAD tries automatically to load various types of acad.* files (acad.dvb, acad.lsp, acad.fas, acad.vlx, …) from the drawing’s folder. Which means that when projects are zipped and passed around, viruses can spread.</p>
<p>This Service Pack helps address this kind of threat in a couple of ways. The first is to introduce the idea of trusted paths from which files may be auto-loaded (and when I say this it refers to the legacy auto-loading mechanism mentioned above, not to the newer Autoloader). This will allow much finer control by users and CAD managers to secure systems against this type of virus.</p>
<p>The trusted locations are assigned via the AUTOLOADPATH system variable and controlled by its sibling AUTOLOAD. I expect this mechanism to broaden, over time, to cover other aspects of application loading inside AutoCAD, but this is certainly a helpful first step.</p>
<p>The second way in which the Service Pack helps is when a system has actually been infected. Once that happens – and this does depend greatly on the specific malware infection – it can be pretty tricky to work out what needs to be done to stop the infection from spreading. As most viruses currently spread via auto-loaded LISP files, the <em>/nolisp</em> command-line switch will help users on infected systems get back up and running more quickly, as AutoCAD will be loaded without the possibility of running LISP code. From here it should be more straightforward to at least export the relevant drawing data without that particular breed of virus being able to copy itself along to the project. Again, this is mostly a reaction to the way malware currently – and most commonly – infects AutoCAD systems, and I’d expect this also to need to broaden, over time.</p>
<p>† <em>There is an exception to this: not long after <a href="http://en.wikipedia.org/wiki/Melissa_(computer_virus)" target="_blank">the Melissa virus</a> attacking Microsoft Office hit the news in 1999 (remember that, anyone? :-) there was a similar virus targeting AutoCAD named ACAD.Star that took advantage of the same loop-hole related to embedded VBA macro security. But that’s so far the only other headline-making AutoCAD virus I can remember that doesn’t fit the above mold.</em></p>
<p><span style="color: #a5a5a5;"><em>photo credit: </em></span><a href="http://www.flickr.com/photos/notsogoodphotography/3666027378/"><span style="color: #a5a5a5;"><em>notsogoodphotography</em></span></a><span style="color: #a5a5a5;"><em> via </em></span><a href="http://photopin.com"><span style="color: #a5a5a5;"><em>photo pin</em></span></a><span style="color: #a5a5a5;"><em> </em></span><a href="http://creativecommons.org/licenses/by/2.0/"><span style="color: #a5a5a5;"><em>cc</em></span></a></p>
