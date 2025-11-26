---
layout: "post"
title: "Docs, Lookup, MEP Calculation and APS DevCon"
date: "2023-07-07 05:00:00"
author: "Jeremy Tammik"
categories:
  - "2024"
  - "Algorithm"
  - "Analysis"
  - "APS"
  - "Docs"
  - "Forma"
  - "Fun"
  - "Migration"
  - "RevitLookup"
  - "RME"
  - "Update"
original_url: "https://thebuildingcoder.typepad.com/blog/2023/07/docs-lookup-mep-calculation-and-aps-devcon.html "
typepad_basename: "docs-lookup-mep-calculation-and-aps-devcon"
typepad_status: "Publish"
---

<p>Lots of good news on APS and the Revit API to keep us busy for the weekend:</p>

<ul>
<li><a href="#2">APS DevCon in Munich and SF</a></li>
<li><a href="#3">Forma for AEC</a></li>
<li><a href="#4">RevitApiDocs supports Revit 2024</a></li>
<li><a href="#5">RevitLookup 2024.0.8</a></li>
<li><a href="#6">User MEP calculation 2024</a></li>
<li><a href="#7">Wastewater pipe calculation</a></li>
<li><a href="#8">The password game</a></li>
</ul>

<h4><a name="2"></a> APS DevCon in Munich and SF</h4>

<p>Registration for the Munich APS DevCon on September 11th and 12th is now open.
Attendees can register for both San Francisco and Munich.
Here is all information on how
to:</p>

<p><center>
<a href="https://aps.autodesk.com/blog/register-autodesk-devcon-2023">Register for Autodesk DevCon 2023</a></p>

<p></p>

<p></center></p>

<h4><a name="3"></a> Forma for AEC</h4>

<p>Autodesk is also clarifying its vision
of <a href="https://www.autodesk.com/company/autodesk-platform/aec">Forma for AEC</a>
and <a href="https://thebuildingcoder.typepad.com/files/forma_sustainability_flyer-1.pdf">sustainability (flyer)</a> announced
at <a href="https://thebuildingcoder.typepad.com/blog/2022/09/aps-au-and-miter-wall-join-for-full-face.html#3">AU 2022</a>:</p>

<ul>
<li><a href="https://blogs.autodesk.com/forma/2023/05/08/sustainability-solutions">Design a better future with Forma’s suite of sustainability solutions</a>:</li>
</ul>

<blockquote>
  <p>Cities consume more than two-thirds of the world’s energy and account for over 70% of global carbon emissions
  (<a href="https://unfccc.int/news/urban-climate-action-is-crucial-to-bend-the-emissions-curve#:~:text=Cities%20consume%20over%20two%2Dthirds,Asia%20and%20Sub%2DSaharan%20Africa">source</a>).
  This means architects, real estate developers, and urban planners have an exceptional opportunity to mitigate the environmental impact of our cities by designing buildings and communities with sustainable outcomes in mind.</p>
  
  <p>Sustainable outcomes are best achieved through a proactive, data-driven approach that starts at the earliest stages of design before it becomes costly and difficult to make changes.
  Autodesk Forma’s powerful suite of real-time analyses equips design teams with the quick, visual insights needed to prioritize sustainability from day one of a project.</p>
</blockquote>

<p>Take a quick look at
the 3-minute video <a href="https://youtu.be/6iKM0fsk_Jw">Autodesk Forma: Make tomorrow's cities</a>:</p>

<p><center>
<iframe width="560" height="315" src="https://www.youtube.com/embed/6iKM0fsk_Jw" title="YouTube video player" frameborder="0" allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture; web-share" allowfullscreen></iframe>
</center></p>

<ul>
<li><a href="https://www.autodesk.com/products/forma/free-trial">Free 30-day trial</a></li>
<li><a href="https://www.autodesk.com/forma">More about Autodesk Forma</a></li>
<li><a href="http://blogs.autodesk.com/forma/">Visit the Forma blog</a></li>
</ul>

<h4><a name="4"></a> RevitApiDocs Supports Revit 2024</h4>

<p><a href="https://twitter.com/gtalarico">Gui Talarico</a> updated the online Revit API documentation for Revit 2024, both:</p>

<ul>
<li><a href="https://apidocs.co/apps/revit/2024/d4648875-d41a-783b-d5f4-638df39ee413.htm#">apidocs</a> and</li>
<li><a href="https://www.revitapidocs.com">revitapidocs</a></li>
</ul>

<p>Notifications of new features are published on twitter at:</p>

<ul>
<li><a href="https://twitter.com/ApiDocsCo">@ApiDocsCo</a> and</li>
<li><a href="https://twitter.com/RevitApiDocs">@RevitApiDocs</a></li>
</ul>

<p>Very many thanks to Gui for his maintenance of these invaluable resources!</p>

<p><center></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e16897883302b751aa5ad7200c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883302b751aa5ad7200c image-full img-responsive" alt="Revit API Docs 2024" title="Revit API Docs 2024" src="/assets/image_9c126d.jpg" border="0" style="display: block; margin-left: auto; margin-right: auto;" /></a><br /></p>

<p></center></p>

<h4><a name="5"></a> RevitLookup 2024.0.8</h4>

<p>Another RevitLookup update is available, now
for <a href="https://github.com/jeremytammik/RevitLookup/releases/tag/2024.0.8">RevitLookup release 2024.0.8</a>:</p>

<ul>
<li>Computing Time Tracking &ndash;
This feature includes the ability to monitor the computing time taken to invoke a member, such as methods or properties.
By tracking the execution time, you can identify and analyse slow-performing methods or properties, gaining insights into their overall performance.
The computing time is displayed in a separate column and a tooltip, providing you with detailed information. This feature is optional and disabled by default</li>
<li>Context Menu &ndash;
A convenient context menu has been added to the table, providing you with additional options to manage columns and update contents.
This menu enables you to customize your table view and effortlessly perform actions to enhance your experience.</li>
<li>Enhanced Visualization &ndash;
Icons have been added to the context menu, making it more visually appealing and intuitive for users to navigate and interact with the available options.</li>
<li>Added async support for unit dialogs</li>
<li>Added API for external programs https://github.com/jeremytammik/RevitLookup/issues/171</li>
<li>Added FamilyParameter support by @CADBIMDeveloper in https://github.com/jeremytammik/RevitLookup/pull/174</li>
<li>Added FamilyManager.GetAssociatedFamilyParameter extension by @CADBIMDeveloper in https://github.com/jeremytammik/RevitLookup/pull/175</li>
<li>Fixed shortcuts reloading leading to incorrect ribbon update https://github.com/jeremytammik/RevitLookup/issues/177</li>
<li><a href="https://github.com/jeremytammik/RevitLookup/compare/2024.0.7...2024.0.8">Full changelog</a></li>
</ul>

<p><center></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e16897883302c1b258cf2c200d-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883302c1b258cf2c200d image-full img-responsive" alt="Computing time tracking" title="Computing time tracking"  src="/assets/image_173fd4.jpg" border="0" style="display: block; margin-left: auto; margin-right: auto;" /></a></p>

<p style="font-size: 80%; font-style:italic">Computing time tracking</p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e16897883302c1a6cceded200b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883302c1a6cceded200b image-full img-responsive" alt="Context menu" title="Context menu"  src="/assets/image_5cabc5.jpg" border="0" style="display: block; margin-left: auto; margin-right: auto;" /></a></p>

<p style="font-size: 80%; font-style:italic">Context menu</p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e16897883302c1b258cf38200d-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883302c1b258cf38200d img-responsive" style="width: 192px; display: block; margin-left: auto; margin-right: auto;" alt="Enhanced visualization" title="Enhanced visualization"  src="/assets/image_0d2aa0.jpg" /></a></p>

<p style="font-size: 80%; font-style:italic">Enhanced visualization</p>

<p></center></p>

<h4><a name="6"></a> User MEP Calculation 2024</h4>

<p>Reynaldo Lorente, Ingeniero Hidraulico of the Centro de Ingeneria e Investigaciones Quimicas in Cuba has been very helpful with several tricky MEP issues, e.g.,
on a <a href="https://forums.autodesk.com/t5/revit-api-forum/user-mep-calculation-error/td-p/12063928">User MEP Calculation error</a>:</p>

<p><strong>Question:</strong> Researching User MEP calculation, I encountered some errors:</p>

<ul>
<li>Error: "FormatUtils doesn't exist in the current context."</li>
<li>Error: "CS1061 'Selection' does not contain a definition for 'Elements' and no accessible extension method 'Elements' accepting a first argument of type 'Selection' could be found (are you missing a using directive or an assembly reference?)"</li>
</ul>

<p>Could someone with experience in this area please help?
I'm new to this and would greatly appreciate your assistance.</p>

<p><strong>Answer 1:</strong> <code>Formatutils</code> is an obsolete command that "Formats a number with units into a string based on the units formatting settings for a document."
You will have to find another way to do this.</p>

<p><strong>Answer 2:</strong> I have updated</p>

<p>Hello, here I leave you <a href="https://github.com/jeremytammik/UserMepCalculation">UserMepCalculation</a> for Revit 2024:</p>

<ul>
<li><a href="https://thebuildingcoder.typepad.com/files/usermepcalculation2024.rar">UserMepCalculation2024.rar</a></li>
</ul>

<p>However, the Revit SDK 2024 also includes a new add-in named <code>NetworkPressureLossReport</code> that might also be useful for you:</p>

<blockquote>
  <p>This add-in sample shows how to access the MEP analytical model data and traverse the network. 
  The flow and pressure loss results are exported to a csv file or displayed in Analysis Visualization Framework (AVF).</p>
</blockquote>

<p>I hope it helps you, and good luck</p>

<p><strong>Response:</strong> Thank you very much for sharing. It's helping me a lot.</p>

<p><strong>Answer 3:</strong> While editing this blog post, I found three previous related  articles:</p>

<p>I am in the process of editing this thread for a blog post. I found three existing related blog posts:</p>

<ul>
<li><a href="http://thebuildingcoder.typepad.com/blog/2013/07/user-mep-calculation-sample.html">User MEP Calculation Sample</a></li>
<li><a href="http://thebuildingcoder.typepad.com/blog/2013/11/user-mep-calculation-sample-on-github.html">User MEP Calculation Sample on GitHub</a></li>
<li><a href="http://thebuildingcoder.typepad.com/blog/2015/12/external-services.html">External Services</a></li>
</ul>

<p>The third of these on Arnošt Löbel's class SD10752 on Revit External Services at Autodesk University 2015 is especially interesting:</p>

<p>It includes another Revit MEP calculation external service sample for a pressure drop calculation.
It was implemented specifically for this AU class and uses external commands to add and remove the calculator.
A proper calculator would be implemented as an <code>ExternalDBApplication</code> with no external commands involved.
Apparently, at the time, the Revit 2014 UserMepCalculation sample was superseded by this one and should be replaced.
However, I have not compared them myself, nor looked at the new SDK sample that Reynaldo points out.</p>

<p>Many thanks to Reynaldo for his very kind and competent support in this area!</p>

<h4><a name="7"></a> Wastewater Pipe Calculation</h4>

<p>Reynaldo also solved another MEP question
on <a href="https://forums.autodesk.com/t5/revit-api-forum/watsewater-pipe-calculation/m-p/12075059">wastewater pipe calculation</a>:</p>

<p><strong>Question:</strong> I am researching plumbing calculations and discovered a method called "User MEP Calculation".
However, it only deals with water supply calculations.
I would greatly appreciate it if someone could suggest a method for wastewater pipe calculation.</p>

<p><strong>Answer:</strong> The SRwD Sewage and Rainwater Drainage System add-in might help.
Its last year of edition is 2023, but it might serve, at least a guide.</p>

<p><strong>Response:</strong> I feel very grateful because you always provide me with valuable answers.
You seem to be an expert in this field, while I am just a beginner.</p>

<p>Another question on water supply calculations because I am currently working on that:</p>

<p>When calculating the flow rate of a pipe, flow rate depends on the <code>FlowConversionMode</code>.
How can Revit understand whether the project is "predominantly flush valves" or "predominantly flush tanks"?</p>

<p><strong>Answer:</strong> Here are some images that helped me formulate an answer:</p>

<p><center></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e16897883302c1b258cf47200d-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883302c1b258cf47200d image-full img-responsive" alt="Ejemplo de un sistema &ndash; example system" title="Ejemplo de un sistema &ndash; example system"  src="/assets/image_bdabff.jpg" border="0" style="display: block; margin-left: auto; margin-right: auto;" /></a></p>

<p style="font-size: 80%; font-style:italic">Ejemplo de un sistema &ndash; example system</p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e16897883302c1a6ccedfa200b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883302c1a6ccedfa200b img-responsive" alt="Wastewater Pipe Calculation" title="Wastewater Pipe Calculation"  src="/assets/image_b86db2.jpg" border="0" style="display: block; margin-left: auto; margin-right: auto;" /></a><br /></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e16897883302c1a6ccedfe200b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883302c1a6ccedfe200b image-full img-responsive" alt="Wastewater Pipe Calculation" title="Wastewater Pipe Calculation"  src="/assets/image_25d404.jpg" border="0" style="display: block; margin-left: auto; margin-right: auto;" /></a><br /></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e16897883302c1b258cf4b200d-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883302c1b258cf4b200d image-full img-responsive" alt="Wastewater Pipe Calculation" title="Wastewater Pipe Calculation"  src="/assets/image_353647.jpg" border="0" style="display: block; margin-left: auto; margin-right: auto;" /></a><br /></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e16897883302b751aa5b00200c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883302b751aa5b00200c image-full img-responsive" alt="Wastewater Pipe Calculation" title="Wastewater Pipe Calculation"  src="/assets/image_a00842.jpg" border="0" style="display: block; margin-left: auto; margin-right: auto;" /></a><br /></p>

<p></center></p>

<p>If you look at the tab areas, one of them turned yellow or orange.
Click on that tab; another menu opens; in the properties table, click Edit Type again; a box opens (Type Properties); where it says Flow Conversion method, you can change it to Predominantly Flush Tanks (you can also do it in the System browser to get to the box (Type Properties).
Remember that you have to change it in Mechanical Setting, in Pipe Settings, where you have the Flow tab, to add the Addin that you made or, alternatively, another one such as the UserMepCalculation 2024 that I sent you.
You have to create a system first to execute these steps.</p>

<p><strong>Response:</strong> I was able to do it because you provided me with an incredibly detailed guide.
Thank you very much for dedicating your valuable time to me.</p>

<p><strong>Answer:</strong> I am very glad that it has served you.
One question, are you taking into account the height of the water intake of the Plumbing Fixtures or the pressure with which it arrives?
The UserMepCalculation makes no assumptions in the calculations of the Plumbing system.</p>

<p>Thank you again, Reynaldo!</p>

<h4><a name="8"></a> The Password Game</h4>

<p>Let's close
with <a href="https://neal.fun/password-game">The Password Game</a>.</p>

<p><center></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e16897883302b751aa5b06200c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883302b751aa5b06200c img-responsive" style="width: 400px; display: block; margin-left: auto; margin-right: auto;" alt="The Password Game" title="The Password Game" src="/assets/image_0587eb.jpg" /></a><br /></p>

<p></center></p>

<blockquote>
  <ul>
  <li><p>A little Friday frustration fun, in a geeky sort of way.</p></li>
  <li><p>I made it to the whole "find a youtube video of X mins and Y seconds" one and gave up.</p></li>
  <li><p>I got to 18, but I just can't from there....</p></li>
  </ul>
</blockquote>

<p><center></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e16897883302c1b258cf58200d-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883302c1b258cf58200d img-responsive" style="width: 400px; display: block; margin-left: auto; margin-right: auto;" alt="The Password Game" title="The Password Game"  src="/assets/image_ecdc58.jpg" /></a><br /></p>

<p></center></p>
