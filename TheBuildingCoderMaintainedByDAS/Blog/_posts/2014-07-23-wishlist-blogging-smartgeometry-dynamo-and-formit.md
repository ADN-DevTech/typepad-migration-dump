---
layout: "post"
title: "Wishlist, Blogging, Smartgeometry, Dynamo and FormIt"
date: "2014-07-23 05:00:00"
author: "Jeremy Tammik"
categories:
  - "Dynamo"
  - "FormIt"
  - "Geometry"
  - "News"
original_url: "https://thebuildingcoder.typepad.com/blog/2014/07/wishlist-blogging-smartgeometry-dynamo-and-formit.html "
typepad_basename: "wishlist-blogging-smartgeometry-dynamo-and-formit"
typepad_status: "Publish"
---

<p>Here is today's bunch of exciting topics:</p>

<ul>
<li><a href="#2">Revit API wishlist survey results</a></li>
<li><a href="#3">Blogging tips and tricks</a></li>
<li><a href="#4">Smartgeometry 2014 conference</a></li>
<li><a href="#5">Dynamo enhancements</a></li>
<li><a href="#6">Dynamo and FormIt win Best in Show at AIA 2014</a></li>
</ul>

<p>Completely lacking hardcore API stuff, for a change.</p>


<a name="2"></a>

<h4>Revit API Wishlist Survey Results</h4>

<p>The results of the

<a href="http://thebuildingcoder.typepad.com/blog/2014/05/revit-api-wishlist-survey.html">
Revit API wishlist survey</a> are

in.</p>

<p>Most participants:</p>

<ul>
<li>Are shipping a Revit add-in used in production</li>
<li>Work in architectural design</li>
<li>Automate repetitive tasks</li>
<li>Ask for more access to 3D model elements and enhancements to the Revit geometry API for geometry creation and analysis</li>
</ul>

<!-- <p>Here is the <a href="">detailed results report</a>.</p> -->



<a name="3"></a>

<h4>Blogging Tips and Tricks</h4>

<p>Here are some basic tips and tricks that I consider essential for taking the first steps striving towards ultimate perfection in blogging:</p>

<ul>
<li>Spell check your text before publishing, and proof read it, preferably several times over, and at least once before and once after publishing.
I almost always notice new additional enhancement possibilities after publication.</li>

<li>Ensure that all source code is well formatted and preferably colour coded.
You can use

<a href="http://code.google.com/p/google-code-prettify">google-code-prettify</a> for most languages.

It obviously cannot parse the .NET libraries referenced by VB or C# code, though.
For that, I use the J.T. Leigh <a href="http://copysourceashtml.codeplex.com/">CopySourceAsHtml</a> utility inside my Visual Studio project environment.
For more details, please refer to various discussions of The Building Coder

<a href="http://thebuildingcoder.typepad.com/blog/about-the-author.html#5.36">source code colourizer</a>.</li>

<li>Image justification: place small images inline with the text as you please. Large images that take the major part of the column width are best centred.</li>

<li>Strive for consistency.
For instance, if you have a paragraph marked 'Question:' and another marked 'Answer:', ensure that the two markers are formatted similarly.
They can either live on an own line each, or be prefixed to the paragraph they mark.
They can be either bold or not.
Just ensure they are recognisably similar, to make the logical structure of your text as clear as possible.</li>

<li>Do not mix fonts.</li>

<li>If you mention any classes or methods, be absolutely sure they exist.
For instance, when talking about the Revit API FamilyType class, you should not make any mention of FamilyTypes.
If you need a plural, use normal words instead, e.g. 'family types'.</li>

<li>Watch out for using special HTML characters.
For instance, the ampersand character '&amp;' has a special meaning in HTML.
To use it in your HTMP text, you have to escape it using the special HTML entity <code>&amp;amp;</code>.</li>

<li>Use consistent capitalisation in your titles.</li>
</ul>


<a name="4"></a>

<h4>Smartgeometry 2014 Conference</h4>

<p>Autodesk is sponsoring the

<a href="http://smartgeometry.org">smartgeometry.org</a>

<a href="http://smartgeometry.org/index.php?option=com_content&view=article&id=239&Itemid=199">2014 conference</a> in

Hong Kong and will play a key role in the BIM workshops there with a special focus on significant advancements in the open source

<a href="http://dynamobim.org">Dynamo</a> technology.</p>

<center>

<a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e16897883301a73df29020970d-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883301a73df29020970d img-responsive" style="width: 433px; " alt="Sg2014" title="Sg2014" src="/assets/image_dd295b.jpg" /></a><br />

</center>

<p>For more details on this, please refer to the official Autodesk press release on

<a href="http://inthefold.autodesk.com/in_the_fold/2014/07/autodesk-sponsors-smartgeometry-2014-and-spotlights-latest-advancements-for-open-source-dynamo.html">
sponsoring Smartgeometry 2014 and advancements for Dynamo</a>.</p>


<a name="5"></a>

<h4>Dynamo Enhancements</h4>

<p><a href="http://dynamobim.org">Dynamo</a> provides a visual programming environment accessible to designers, allowing them to visually create logic that drives the geometry and behaviour of elements and data created within Autodesk Revit. A dedicated team at Autodesk actively participates in the Dynamo open source community.</p>

<center>

<a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e16897883301a3fd37934e970b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883301a3fd37934e970b img-responsive" style="width: 433px; " alt="Visual programming tools like Dynamo can be used to script and quantify anything from furniture to buildings and bridges." title="Visual programming tools like Dynamo can be used to script and quantify anything from furniture to buildings and bridges." src="/assets/image_0cd4a6.jpg" /></a><br />

<p style="font-style:italic;text-align:center;margin-top:0.4em;">Visual programming tools like Dynamo can be used to script and quantify anything from furniture to buildings and bridges.</p>
</center>

<p>New enhancements for Dynamo include:</p>

<ul>
<li><p>Significant refactoring of the underlying code to help users expand the basic Dynamo capabilities.
The new code base was rebuilt from the ground up to encourage exploratory programming, readability and scaling to very large datasets.</p>
<center>

<a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e16897883301a73df28fce970d-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883301a73df28fce970d img-responsive" style="width: 400px; " alt="Surface Modelling in Dynamo driving Revit elements" title="Surface Modelling in Dynamo driving Revit elements" src="/assets/image_3cae7d.jpg" /></a><br />

</center>
<p style="font-style:italic;text-align:center;margin-top:0.4em;">Surface Modelling in Dynamo driving Revit elements</p>
</li>

<li><p>Expansion of geometric capabilities:  Dynamo's new library of surface and solids tools is built on the same code as Autodesk Fusion and Inventor, enabling the combination of the flexibility and versatility of industrial design and manufacturing tools with the rational and exploratory power of computation.</p>
<center>

<a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e16897883301a3fd3792d3970b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883301a3fd3792d3970b img-responsive" style="width: 400px; " alt="Nodes are nice, but sometimes people want to write code." title="Nodes are nice, but sometimes people want to write code." src="/assets/image_d51c01.jpg" /></a><br />

</center>
<p style="font-style:italic;text-align:center;margin-top:0.4em;">Nodes are nice, but sometimes people want to write code.</p>
</li>


<li><p>Rich new set of tools for scripting: In addition to Python programming language support in dedicated nodes, Dynamo now allows for

<a href="http://dynamobim.org/cbns-for-dummies">
direct input of compact and readable code</a> into

the graph.
Users can script small or large pieces depending on their comfort level, and interact with the text-based code on the same level as the graph environment.</p>
<center>

<a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e16897883301a511e74bb1970c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883301a511e74bb1970c img-responsive" style="width: 400px; " alt="Dynamo driving an ABB Robot arm after importing the manufacturer's code library." title="Dynamo driving an ABB Robot arm after importing the manufacturer's code library." src="/assets/image_aecb93.jpg" /></a><br />

</center>
<p style="font-style:italic;text-align:center;margin-top:0.4em;">Dynamo driving an ABB Robot arm after importing the manufacturer's code library.</p>
</li>


<li>Library Import:  Enables the direct loading of additional tools (such as analysis and extra modelling capabilities) from user-authored libraries of code.
This helps increase the rate at which users can expand the functionality of Dynamo to fit their needs and share workflows with others.
We expect more development here from 3rd party contributors.
For example, check out how participants at a <a href="http://enr.construction.com/video/?video_uuid=c136x005&categoryId=39093">hackathon</a> used this functionality to drive robots!</li>
</ul>

<p>Again, for more details, please refer to the official Autodesk press release on

<a href="http://inthefold.autodesk.com/in_the_fold/2014/07/autodesk-sponsors-smartgeometry-2014-and-spotlights-latest-advancements-for-open-source-dynamo.html">
sponsoring Smartgeometry 2014 and advancements for Dynamo</a>.</p>


<a name="6"></a>

<h4>Dynamo and FormIt Win Best in Show at AIA 2014</h4>

<p>The Autodesk conceptual design team responsible for Dynamo also works on FormIt.</p>

<p>Both of these applications won the

<a href="http://architosh.com/2014/07/aia-architosh-awards-aia-national-best-of-show-honors-to-software-and-technology-vendors">
Architosh's 'Best in Show' award</a> for

desktop and mobile respectively at the

<a href="http://convention.aia.org">
AIA convention 2014</a> in Chicago.</p>

<center>

<a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e16897883301a3fd3792ad970b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883301a3fd3792ad970b img-responsive" style="width: 380px; " alt="Architosh desktop award" title="Architosh desktop award" src="/assets/image_3f1e0e.jpg" /></a><br />

</center>

<p>'<a href="http://dynamobim.org">Dynamo</a> continues to evolve into an exciting option for visual programming with 3D geometry components and this latest release in alpha runs in stand-alone mode no longer requiring Revit,' says Anthony Frausto-Robledo, LEED AP. 'Dynamo .07 alpha also runs on the Autodesk Shape Manager (ASM) geometry modelling kernel and contains a new scripting interface. Visual scripting and parametric modelling platforms like Dynamo will increasingly serve the field of architecture,' adds Frausto-Robledo, 'as more analytics service the iterative architectural design process and building design in general becomes more performance-based.'</p>

<center>

<a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e16897883301a3fd379236970b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883301a3fd379236970b img-responsive" style="width: 380px; " alt="Architosh mobile award" title="Architosh mobile award" src="/assets/image_515b5f.jpg" /></a><br />

</center>

<p>'<a href="http://autodeskformit.com">Autodesk FormIt</a> won BEST of SHOW last year in this category and since that release has continued to improve nicely,' remarks Frausto-Robledo, AIA LEED AP. 'It remains an exemplar of the combined power of mobile and the cloud with its integrated Autodesk 360 cloud connections and utilization of Google's maps technology to locate and use project sites. In the latest release it also contains early stage building performance capabilities tapping into local climate station data. This is quite exciting to see front-ended analytics right at the site, it is a tremendous example of the power of tablets connected to the cloud.'</p>
