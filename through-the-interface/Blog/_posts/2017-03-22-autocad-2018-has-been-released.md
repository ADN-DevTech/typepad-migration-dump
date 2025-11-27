---
layout: "post"
title: "AutoCAD 2018 has been released"
date: "2017-03-22 10:39:38"
author: "Kean Walmsley"
categories:
  - "AutoCAD"
  - "AutoCAD .NET"
  - "Autodesk"
original_url: "https://www.keanw.com/2017/03/autocad-2018-has-been-released.html "
typepad_basename: "autocad-2018-has-been-released"
typepad_status: "Publish"
---

<p>While I’m not spending much time working with AutoCAD, these days, I’ve been waiting impatiently for the release of AutoCAD 2018 (codenamed “Omega”). There’s one key feature, in particular, that I’ve been waiting for – but more on that later.</p>
<p><a href="http://through-the-interface.typepad.com/.a/6a00d83452464869e201b7c8e3053f970b-pi" rel="noopener noreferrer" target="_blank"><img alt="AutoCAD 2018 splashscreen" border="0" height="291" src="/assets/image_613012.jpg" style="background-image: none; float: none; padding-top: 0px; padding-left: 0px; margin: 30px auto; display: block; padding-right: 0px; border-width: 0px;" title="AutoCAD 2018 splashscreen" width="500" /></a></p>
<p>From a user perspective you can find information on the new release in <a href="http://blogs.autodesk.com/autocad/autocad-2018/" rel="noopener noreferrer" target="_blank">this blog post</a> and <a href="http://blogs.autodesk.com/autocad/wp-content/uploads/sites/35/2017/03/AutoCAD2018WinPreviewGuide_ENU.pdf" rel="noopener noreferrer" target="_blank">this preview guide</a>. There’s also a subset covered in this intro video, if that’s your preference:</p>
<p style="text-align: center">&#0160;</p>
<p style="text-align: center"><iframe allowfullscreen="" frameborder="0" height="281" src="https://www.youtube-nocookie.com/embed/DBPewa4HMVg?rel=0&amp;showinfo=0?ecver=1" width="500"></iframe></p>
<p style="text-align: center">&#0160;</p>
<p>Here are a few notes on the major user features. Some of these features were already part of the 2017.1 release, but many people will be seeing these for the first time. The above-mentioned links help make it clear what’s brand new and what’s a little older.</p>
<ul>
<li>Heavy users of <strong>Xrefs</strong> are going to appreciate the<strong> UI improvements</strong>
<ul>
<li>You can much more easily manage and repath Xrefs in AutoCAD 2018</li>
</ul>
</li>
<li><strong>Offscreen selection</strong> – where you start a crossing or window selection and auto-pan across geometry that you’re zoomed too far in to see in its entirety – is going to be very helpful<br />
<ul>
<li>It was a real pain to have to zoom out – losing important detail – for this to work</li>
</ul>
</li>
<li>People working with <strong>PDFs</strong> will like the<strong> character recognition</strong> for <strong>SHX</strong> text that has been reimported
<ul>
<li>In general the tools and controls related to this appear to have received some attention to detail (no doubt informed by external feedback)</li>
</ul>
</li>
<li>Significant <strong>2D and 3D performance</strong> improvements have been made
<ul>
<li>Lots more GPU usage, for instance for hatch rendering</li>
<li>3D navigation is now much quicker</li>
</ul>
</li>
<li><strong>Linetype gap selection</strong> and snapping now works for custom linetypes</li>
<li><strong>Save performance</strong> has improved noticably</li>
<li>And the one I’ve been waiting for: <strong>high DPI screen support</strong>
<ul>
<li>I mainly use AutoCAD inside a Parallels VM on a MacBook Pro with a retina screen</li>
<li>AutoCAD 2018 looks <u>so much better</u> at native resolution (I always had to use the “scaled” option before)</li>
</ul>
</li>
</ul>
<p>But the angel’s in the details, too. Some smaller improvements will make a big difference to people:</p>
<ul>
<li>Easy adding of the current layer pulldown to the quick access toolbar</li>
<li>The ability to reset “broken” sysvars via a right-click on the status bar is a nice tweak</li>
<li>Similarly, saving the ordering for file dialog boxes is a small but helpful enhancement</li>
</ul>
<p>From a <strong>developer</strong> perspective the main things to note are that the <strong>DWG format has changed</strong>, as well as there being a<strong> break in binary ObjectARX application compatibility</strong>. This doesn’t affect .NET or LISP developers, of course. (Incidentally, <a href="https://www.nuget.org/packages/AutoCAD.NET/22.0.0" rel="noopener noreferrer" target="_blank">the NuGet packages for AutoCAD 2018</a> are now available for .NET projects.)</p>
<p>Now for a little commentary…</p>
<p>The main reason for the break in compatibility is some longer-term work that’s going on inside the AutoCAD codebase. For now this is really only surfacing in small ways – I expect it’s contributing some performance benefits, for instance – but the work is absolutely critical to the long-term viability of the product. AutoCAD continues to be a core part of Autodesk’s business – and it continues to receive significant investment in terms of development resources – but don’t expect that to translate to buckets of shiny new features: AutoCAD’s feature maturity means the investment is rightly being focused in other areas (at least for now).</p>
<p>This is a tricky balance – and could easily be interpreted as a big company not caring about (some of) its users and only being interested in milking its cash-cow – but the work happening behind the scenes is significant and I believe will ultimately prove to be of real value to our customers.</p>
<p>[Climbs down off soapbox.]</p>
<p>Oh, and remember that my current role is inside <a href="http://autodeskresearch.com" rel="noopener noreferrer" target="_blank">Autodesk Research</a> – I’m not part of the AutoCAD team, these days – so I’m far from being an official spokesperson for either that team or their product. I just felt the need to add my tuppence-worth to make sure people understand that Autodesk still cares a great deal about both AutoCAD and its users, and that the product continues to have a very interesting future ahead of it.</p>
