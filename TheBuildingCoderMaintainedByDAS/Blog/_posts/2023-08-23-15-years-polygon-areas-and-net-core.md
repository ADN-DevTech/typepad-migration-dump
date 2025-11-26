---
layout: "post"
title: "15 Years, Polygon Areas and .NET Core"
date: "2023-08-23 05:00:00"
author: "Jeremy Tammik"
categories:
  - "AI"
  - "Algorithm"
  - "Geometry"
  - "News"
  - "Sustainability"
original_url: "https://thebuildingcoder.typepad.com/blog/2023/08/15-years-polygon-areas-and-net-core.html "
typepad_basename: "15-years-polygon-areas-and-net-core"
typepad_status: "Publish"
---

<p>A birthday celebration,
a <a href="https://en.wikipedia.org/wiki/Forward-looking_statement">forward-looking statement</a> or two
and observations on geometry, AI and emissions:</p>

<ul>
<li><a href="#2">15 years of The Building Coder</a>
<ul>
<li><a href="#2.1">Congratulations</a></li>
</ul></li>
<li><a href="#3">Revit API with .NET Core</a></li>
<li><a href="#4">Bye-bye document macro?</a></li>
<li><a href="#5">Polygon area algorithms</a></li>
<li><a href="#6">AI recreates Pink Floyd from brain activity</a></li>
<li><a href="#7">Create ML model with one sentence?</a></li>
<li><a href="#8">Compress greenhouse gas emissions</a></li>
</ul>

<h4><a name="2"></a> 15 Years of The Building Coder</h4>

<p>We celebrated The Building Coder's 15th birthday yesterday, August 22.</p>

<p><center></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e16897883302b751af508e200c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883302b751af508e200c image-full img-responsive" alt="The Building Coder's 15th birthday" title="The Building Coder's 15th birthday"  src="/assets/image_2108fd.jpg" border="0" style="display: block; margin-left: auto; margin-right: auto;" /></a><br /></p>

<p></center></p>

<p>It has soon passed its puberty now and is almost a full grown-up blog now, preparing to stand on its own legs.
And, in case you didn't know, this is blog post number 2005.
We silently crossed into the third millennium in July.</p>

<h4><a name="2.1"></a> Congratulations</h4>

<p>On <a href="https://www.linkedin.com/posts/jeremytammik_15-years-polygon-areas-and-net-core-activity-7100129768016093185-SO6O">LinkedIn</a>:
and <a href="https://www.linkedin.com/feed/update/urn:li:activity:7100129808394641409">BIM Experts</a>:</p>

<ul>
<li>Tim Hoffeller:
Thank you Jeremy, for all the inspiration through all those years! The BIM world would be a less creative one without you 😊</li>
<li>Prasad Sumanasekara, MagiCAD Group AB:
Congratulations ...!!!</li>
<li>Alex Vila Ortega, WSP, Sydney:
Congrats!!!</li>
<li>Diego Mendoza Acosta, Ingeniero Civil:
Congratulations !!!</li>
<li>Simon Jones, Freelance BIM Consultant &amp; Developer, AEC BIM Tools:
Congratulations Jeremy &ndash; glad to see it still going!</li>
<li>José Ignacio Montes Herraiz, North London Heat Power Project:
Congratulations Jeremy!</li>
<li>Stephen Preston, Developer Relations and Platform Ecosystems leader:
Congratulations Jeremy. This is a great achievement. Its heartwarming to think of the many people you've helped in their careers, businesses and hobbies by sharing your knowledge.</li>
<li>Matt Taylor, WSP Aotearoa:
Happy birthday TBC/Jeremy! Thanks for all you do.</li>
<li>Emile Kfouri, Senior Technology Product Leader:
Amazing! I remember your first blog post and thinking what an amazing thing it was going to be for Revit. Thank you for helping all of us be better developers and solve problems we could not solve on our own.</li>
<li>Madhukar Moogala, Principal Developer Advocate at Autodesk:
Congrats Jeremy an epic milestone ! 😀</li>
<li>Chris Theobald, Technology Officer at SCS:
Happy 15th!</li>
<li>Ali Najmi, BIMLOGiQ:
Congrats Jeremy, Thanks for all that hard work over the past 15 years</li>
<li>João Teixeira, Software Solutions for AEC and Manufacturing:
Congrats for all the contributions to the community over the last 15 years! Keep up the excellent work!</li>
<li>Kean Walmsley, Software Architect &amp; Senior Manager, Convergence Engineering, Autodesk Research:
Congratulations, Jeremy! It's been wonderful seeing TBC go from strength to strength with your continued focus and investment. A fantastic milestone and achievement!</li>
<li>Chuong Ho, Computational Design Researcher:
Thank you Jeremy, your blog is awesome.</li>
<li>Carlos Ernesto Suárez Guerra, EMPAI:
Congratulations!!!!!</li>
<li>Nastya Baranouskaya, Software engineer and BIM Manager:
Congratulations!!!</li>
<li>Wouter Hilhorst, Projectleider bij OPL architecten / re-designers:
Congratulations! The next 15 years From API to AI?</li>
<li>Lester Molina Espinosa, Sr. Principal CAD/BIM Designer:
Happy 15th birthday!!! Well done Jeremy. Keep up the good work!</li>
<li>Michelangelo Capraro, Autodesk:
I reference your pages constantly, what a great community resource, congrats!</li>
<li>Boy d'Hont, Senior Architect / BIM Programmer at WGA Architects:
Thank you for all your input for the API. You brought the polish that made that rusty old Revit shine.</li>
<li>Luís Filipe Santos, Architect at OSLO WORKS:
Congrats for the great blog and all the help building plugins for Revit!</li>
<li>Jakob Hirn, BIM, digital twins, building data platforms, Revit training, CAD-CAM automation:
Happy Birthday and thanks a lot for all the work you’ve done for the Revit community in these years. 🙏</li>
</ul>

<p>Thank you all and the entire Revit add-in developer community for your appreciation and support!</p>

<h4><a name="3"></a> Revit API with .NET Core</h4>

<p>A number of questions were raised in
the <a href="http://forums.autodesk.com/t5/revit-api-forum/bd-p/160">Revit API discussion forum</a> on
Revit API support for .NET Core, e.g.:</p>

<ul>
<li><a href="https://forums.autodesk.com/t5/revit-api-forum/does-revit-target-net-standard/m-p/9792894?search-action-id=812462935117&amp;search-result-uid=9792894">Does Revit target .NET standard</a></li>
<li><a href="https://forums.autodesk.com/t5/revit-api-forum/revitapi-should-support-net-5/m-p/10533160?search-action-id=812462935117&amp;search-result-uid=10533160">Revit API should support .NET 5+</a></li>
<li><a href="https://forums.autodesk.com/t5/revit-api-forum/typeloadexception-on-addin-startup-after-changing-project-to-net/m-p/10341283?search-action-id=812462935117&amp;search-result-uid=10341283">TypeLoadException on addin startup after changing project to .NET 5</a></li>
<li><a href="https://forums.autodesk.com/t5/revit-api-forum/has-anyone-been-successful-in-building-a-netstandard-2-0-or-net/m-p/10694884?search-action-id=812462935117&amp;search-result-uid=10694884">Has anyone been successful in building a NetStandard-2.0 or Net-5.0 plug-in?</a></li>
</ul>

<p>More significantly, we have 
a <a href="https://forums.autodesk.com/t5/revit-ideas/idb-p/302">Revit Idea Station</a> 
<a href="https://forums.autodesk.com/t5/revit-ideas/net-version/idc-p/12196972">wishlist item for .NET version 6</a> that
has now been accepted:</p>

<ul>
<li><strong>Request:</strong> Could you use .NET 6 for the Revit API?
That would greatly improve the quality of the tools I develop.
The use of .NET 6 would also improve Revit's performance as it is faster and more efficient than the .NET framework.
Also, with a .NET version greater than .NET 5 comes C# 9, which offers newer and more functionality for developing with the Revit API.
Furthermore, with the latest version of .NET comes the latest version of WPF, which offers better looks and overall improvements.</li>
<li><strong>Response:</strong> Status changed to: Accepted. Congrats!
We think this is a great idea, so we've decided to add it to our roadmap.
Thanks for the suggestion!
To follow the progress of features in development, please see
the <a href="https://blogs.autodesk.com/revit/roadmap/">Revit Public Roadmap</a> and join
the <a href="https://feedback.autodesk.com/key/LHMJFVHGJK085G2M">Revit Preview Release</a> to
participate in feature testing.
(Note that Accepted Ideas may not be immediately available.)</li>
</ul>

<p class="author">The Factory</p>

<p>Sol Amour has covered most of what can be said on this topic in his overview of <a href="https://forum.dynamobim.com/t/dynamo-upgrading-to-net-6">Dynamo upgrading to .NET 6</a>.</p>

<p>Madhukar Moogala and Kean Walmsley have already published some information about the situation in AutoCAD and Civil3D:</p>

<ul>
<li><a href="https://adndevblog.typepad.com/autocad/2023/08/call-for-action-next-release-of-autocad.html">Call for Action : Next Release of AutoCAD API</a></li>
<li><a href="https://www.keanw.com/2023/08/the-next-release-of-autocad-and-net.html">The next release of AutoCAD and .NET</a></li>
</ul>

<p>AutoCAD and Civil3D have published preview versions with .NET Core 6 support for developers to explore.
However, it is by no means clear yet which version of .NET Core will be targeted by their next major releases.
Microsoft may release <a href="https://devblogs.microsoft.com/dotnet/announcing-dotnet-8-preview-7/">.NET Core 8 in November</a>.</p>

<p>So, as you can imagine, we will probably be facing a similar transition in the Revit API as well.
The situation for Revit is complicated by dependencies, addons, and other components and relationships to consider.
The development team is still working out the details.</p>

<p>So, basically, all we can say about this at the moment is that we are working on it, and it remains a moving target.</p>

<p>Please keep your eyes peeled for the Revit preview releases.
There is no guarantee yet by when the internal dependencies will have settled enough to include a version of the Revit API supporting .NET Core in a Revit preview release.
It may take until the end of the year.</p>

<p>What can I do right now?</p>

<p>Above all, if you are interested in this topic, please ensure that you have joined the Revit feedback community and have access to the upcoming preview releases for evaluation.
Then, you can also participate in the feedback forum discussions.
Familiarise yourself with the Revit feedback portal now.
Then you will be ready to jump in and actively join the fray as soon as possible.</p>

<h4><a name="4"></a> Bye-Bye Document Macro?</h4>

<p>In the course of revamping the Revit API, the development team also took a look at the macro environment.
Support for .NET Core will obviously affect that as well.
Last year, they asked for feedback from the add-in developer community
on <a href="https://forums.autodesk.com/t5/revit-api-forum/research-how-do-you-use-revit-macros/m-p/11158305">how you use Revit macros</a>
and <a href="https://thebuildingcoder.typepad.com/blog/2022/05/analysis-of-macros-journals-and-add-in-manager.html#2">shared back the results</a>.</p>

<p>As a result of this and other usage analysis, the current plan is to drop support for document macros.
Converting a document macro to an application macro is easy, and I hope to share some simple instructions on that anon.</p>

<h4><a name="5"></a> Polygon Area Algorithms</h4>

<p>Moving on from plans and speculations about what the future might bring,
Richard <a href="https://forums.autodesk.com/t5/user/viewprofilepage/user-id/1035859">RPThomas108</a> Thomas
shared some valuable hints and examples of polygon area algorithms answering a question on how to obtain
the <a href="https://forums.autodesk.com/t5/revit-api-forum/area-of-a-wall-opening/m-p/12174104">area of a wall opening</a>:</p>

<p><strong>Question:</strong> How can I calculate the area of a wall opening?
I cannot delete any object.</p>

<p><strong>Answer:</strong> You can delete an object temporarily inside a transaction that is never committed, so the changes are never stored in the database:</p>

<ul>
<li><a href="http://thebuildingcoder.typepad.com/blog/2015/03/calculating-gross-and-net-wall-areas.html">Calculating gross and net wall areas</a></li>
<li><a href="https://thebuildingcoder.typepad.com/blog/2015/04/gross-and-net-wall-area-calculation-enhancement-and-events.html">Gross and net wall area calculation enhancement</a></li>
</ul>

<p><strong>Response:</strong> I cannot delete elements, even transitory.</p>

<p><strong>Answer:</strong> Here is a possible solution, except for ruled faces (for ruled faces, there must be a mathematical approximation similar to below):</p>

<p>Generally, for other types of faces you can find the inner loops of the face and use <code>Edge.GetCurveUV</code> to create a loop in the dimensions of the face parameters.
Unfortunately, there is no tessellate for <code>CurveUV</code>, but you can evaluate points along the loop to get a set of ordered UVs.
Each <code>U</code> and <code>V</code> can then be multiplied based on how the face is parameterised in that direction, i.e., for a planar face, it 1 in both directions, but for a cylindrical face the <code>U</code> is based on angles, so, instead you have to multiply it based on radius to get the segment length (<code>V</code> is still in length, so you can use 1 for that).
Prior to the existence of <code>CurveUV</code>, you would likely have had to have used <code>Edge.EvaluateOnFace</code> to create the tessellated points.</p>

<p>Once you've multiplied the UV's, you can create a polygon and use
the <a href="https://en.wikipedia.org/wiki/Shoelace_formula">shoelace formula</a> etc. to find the area of the polygon and so the surface area of the opening.</p>

<p>It is hard for faces with normalised parametrisation (such as ruled faces), because the multiple used to convert normalised to raw in one direction changes along the other direction.
For example, if you have a ruled surface between two lines of different length, one at the base (Vmin) and one at the top (Vmax), the length varies from base to top, so how you convert <code>U</code> to raw parameter isn't constant, but varies according to the height of <code>V</code> where <code>U</code> is being measured.</p>

<p>How we measure openings in surfaces can also be a bit subjective to a degree.
If you have a cylindrical wall, then the area for the same opening on the outer face will be larger than the inner, but neither, I suspect, will likely be the thing that is useful to an MEP engineer.
I assume they would likely want the projected (flat opening area).
For this, you would probably have to take the worst case (inner area) and project it onto a plane to establish what can fit into that 2D area (or how much ventilation you have).</p>

<p>Another method I used in the past for 2D is to tesselate the perimeter and fill the opening with a grid of points, then
use <a href="https://en.wikipedia.org/wiki/Delaunay_triangulation">Delaunay triangulation</a> etc., adding up the sum area of resulting triangles.
The area is always slightly underestimated for concave edges and slightly overestimated for convex edges.
Using smaller triangles obviously improves that but also increases processing time.</p>

<p>Another option is to create a single faced solid over the opening with one of the shape builders.
Some surface types are not supported by all shape builders, however.</p>

<p><center></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e16897883302c1b25dae50200d-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883302c1b25dae50200d image-full img-responsive" alt="Area of wall opening" title="Area of wall opening" src="/assets/image_3ff350.jpg" border="0" style="display: block; margin-left: auto; margin-right: auto;" /></a><br /></p>

<p></center></p>

<p>You can then extract the surface area of those.</p>

<p><center></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e16897883302c1b25dae57200d-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883302c1b25dae57200d image-full img-responsive" alt="Area of wall opening" title="Area of wall opening" src="/assets/image_1318f2.jpg" border="0" style="display: block; margin-left: auto; margin-right: auto;" /></a><br /></p>

<p></center></p>

<p>I think walls are quite simple compared to floors.
In shaped floors, especially, you don't always get inner loops.
For example, in the below there are no inner loops.
The thing you do know however is that the actual outline edges always have vertical faces adjacent.
Therefore, fold edges always contain two horizontal or quasi-horizontal faces.
So, by elimination of those that way, you are left with the outline edge curves and it is then just a case of ordering them into loops and determining if they are outer or inner.
You can't rely on direction of curve for that because in reality they are all outer edges to their face.</p>

<p><center></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e16897883302c1b25dae5b200d-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883302c1b25dae5b200d image-full img-responsive" alt="Area of wall opening" title="Area of wall opening" src="/assets/image_99af52.jpg" border="0" style="display: block; margin-left: auto; margin-right: auto;" /></a><br /></p>

<p></center></p>

<p>So, there isn't a universal solution to these things.</p>

<p>Many thanks to Richard for sharing his thoughts and extensive experience on this!</p>

<h4><a name="6"></a> AI Recreates Pink Floyd from Brain Activity</h4>

<p>Let me briefly point out two AI-related news items that I found interesting:</p>

<ul>
<li><a href="https://www.newscientist.com/article/2387343-ai-recreates-clip-of-pink-floyd-song-from-recordings-of-brain-activity/">AI recreates clip of Pink Floyd song from recordings of brain activity</a></li>
</ul>

<h4><a name="7"></a> Create ML Model with one Sentence?</h4>

<p>For a lengthier discussion on how ML can be used or misused, check out the several hundred comments
on <a href="https://www.linkedin.com/posts/alliekmiller_you-can-now-write-one-sentence-to-train-an-activity-7097974848001331200-DtJE">Allie K. Miller's post on LinkedIn: You can now write one sentence to train an entire ML model</a>:</p>

<blockquote>
  <p>How does it work?
  You just describe the ML model you want...
  a chain of AI systems will take that...</p>
</blockquote>

<h4><a name="8"></a> Compress Greenhouse Gas Emissions</h4>

<p>Please compress stuff!</p>

<p>It is worthwhile paying attention to the carbon footprint of today's widespread and growing usage of the Internet and digital devices.</p>

<p>Some estimates deem it comparable with the pollution generated by airlines and flying.</p>

<p>I just checked out the effect of reduction of resolution and compression (using <a href="https://compressjpeg.com/">compressjpeg.com</a>) on an image that I emailed to some friends:</p>

<ul>
<li>a0.jpg &ndash; 3506 pixel height original &ndash; 1.759.026 bytes</li>
<li>a1.jpg &ndash; 3506 pixel height compressed &ndash; 850.770 bytes</li>
<li>a2.jpg &ndash; 900 pixel height  &ndash; 149.291 bytes</li>
<li>a3.jpg &ndash; 900 pixel height compressed &ndash; 97.220 bytes</li>
</ul>

<p>The shrunk and compressed image was virtually indistinguishable from the original &ndash; it was even a bit easier to read due to slightly higher contrast.</p>

<p>The combination of lower pixel count and compression reduced the size from over 1.7 MB to less than 100 kB, by a factor of over 18.</p>

<p>Some supporting articles that I checked out:</p>

<p><ul>
<li>Wikipedia entry on <a href="https://en.wikipedia.org/wiki/Greenhouse_gas">Greenhouse gas</a></li>
<li><a href="https://ourworldindata.org/emissions-by-sector">Greenhouse emissions by sector</a></li>
<li><a href="https://www.webfx.com/blog/marketing/carbon-footprint-internet/">Powering the Internet: your virtual carbon footprint infographic</a></li>
<li><a href="https://foundation.mozilla.org/en/blog/ai-internet-carbon-footprint/">The Internet’s invisible carbon footprint</a></p>

<blockquote>
  <p>Did you know that writing an email can send 17 grams or more of carbon dioxide into the atmosphere?
  Or that going audio-only on Zoom calls reduces carbon emissions by up to 96%?</li>
  </ul>
  <center></p>
</blockquote>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e16897883302b751af5098200c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883302b751af5098200c img-responsive" alt="Greenhouse gas emissions by sector" title="Greenhouse gas emissions by sector" src="/assets/image_a12572.jpg" border="0" style="display: block; margin-left: auto; margin-right: auto;" /></a><br /></p>

<p></center></p>
