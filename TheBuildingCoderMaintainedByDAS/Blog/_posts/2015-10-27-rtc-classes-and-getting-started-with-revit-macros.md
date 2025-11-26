---
layout: "post"
title: "RTC Classes and Getting Started with Revit Macros"
date: "2015-10-27 06:00:00"
author: "Jeremy Tammik"
categories:
  - ".NET"
  - "CompHound"
  - "Getting Started"
  - "Macro"
  - "News"
  - "Photo"
  - "RTC"
  - "SDK Samples"
  - "Training"
  - "Update"
  - "User Interface"
original_url: "https://thebuildingcoder.typepad.com/blog/2015/10/rtc-classes-and-getting-started-with-revit-macros.html "
typepad_basename: "rtc-classes-and-getting-started-with-revit-macros"
typepad_status: "Publish"
---

<p>I have never dealt with Revit macros in depth before.</p>

<p>Now I am finally forced to, having been asked to take over a hands-on lab on the topic from someone else at
the Revit Technology Conference <a href="http://www.rtcevents.com/rtc2015eu">RTC Europe</a> in Budapest later this week.</p>

<p>I am presenting three classes at RTC.</p>

<p>Here they are, plus another important Revit-related news item and pictures from my recent travel preparation &ndash; some time out in nature before entering airports, hotels and conference rooms in the big city world:</p>

<ul>
<li><a href="#2">Autumn walk over Schauenburg</a></li>
<li><a href="#3">Full moon fire</a></li>
<li><a href="#4">Revit 2016 R2</a></li>
<li>Session #44 &ndash; <a href="#5">Connecting the Desktop and the Cloud</a> &ndash; Cloud-based universal component and asset usage analysis, visualisation and reporting</li>
<li>Session #130 &ndash; <a href="#6">The Building Coder Chatroom</a> &ndash; All you ever wanted to ask about Revit API, The Building Coder, and all other non-UI Revit topics</li>
<li>Session #161 &ndash; <a href="#7">Getting Started with Revit Macros</a>
<ul>
<li><a href="#11">What Are Macros?</a></li>
<li><a href="#12">Getting Started With Revit Macros</a></li>
<li><a href="#13">Write Your First Macro</a></li>
<li><a href="#14">Next Steps</a></li>
<li><a href="#15">Choose a Programming Language</a></li>
<li><a href="#16">Converting Code from One Language to Another</a></li>
<li><a href="#17">Learning the Revit API</a></li>
<li><a href="#18">Troubleshooting Macros</a></li>
<li><a href="#19">Using Debug.Print and the Output Window</a></li>
<li><a href="#20">Stepping Through Your Code</a></li>
<li><a href="#21">Using Break Points</a></li>
<li><a href="#22">Commenting Your Code</a></li>
<li><a href="#23">Exceptions</a></li>
<li><a href="#24">Macro Sample</a></li>
<li><a href="#25">Next Steps</a></li>
<li><a href="#26">Additional Resources</a></li>
<li><a href="#27">Conclusion</a></li>
</ul></li>
</ul>

<h4><a name="2"></a>Autumn walk over Schauenburg</h4>

<p>It was suddenly quite cold for a while, but now the weather wormed up again and nature is gifting us with a wonderful autumn.</p>

<p>Here is my <a href="https://flic.kr/s/aHsknYQ7yG">pictures from an autumn walk over Schauenburg</a> last Sunday afternoon:</p>

<p><center>
<a data-flickr-embed="true"  href="https://www.flickr.com/photos/jeremytammik/albums/72157660327361056" title="Autumn Walk over Schauenburg"><img src="/assets/22512038985_e8f12d3c78_n.jpg" width="320" height="240" alt="Autumn Walk over Schauenburg"></a><script async src="//embedr.flickr.com/assets/client-code.js" charset="utf-8"></script>
</center></p>

<h4><a name="3"></a>Full Moon Fire</h4>

<p>I often celebrate the full moon by spending a couple of hours on a nearby hill overlooking Basel, enjoying nature, space, a big view, the sky, wind, sunset, moonrise and some hours disconnected from all technology.</p>

<p>Here is the <a href="https://flic.kr/s/aHsknYTP1s">full moon fire photo album</a> from my time out yesterday:</p>

<p><center>
<a data-flickr-embed="true"  href="https://www.flickr.com/photos/jeremytammik/albums/72157660328082446" title="Full Moon Fire"><img src="/assets/22523540901_e132b9d87a_n.jpg" width="320" height="240" alt="Full Moon Fire"></a><script async src="//embedr.flickr.com/assets/client-code.js" charset="utf-8"></script>
</center></p>

<h4><a name="4"></a>Revit 2016 R2 is Available!</h4>

<p>The title says it all: <a href="http://insidethefactory.typepad.com/my_weblog/2015/10/revit-2016-r2-is-available.html">Revit 2016 R2 is available</a>.</p>

<p>R2 is a subscription-only release.</p>

<p>For a list of R2 features, check out <a href="http://insidethefactory.typepad.com/my_weblog/2015/08/whats-new-in-autodesk-revit-sunrise.html">what's new in Autodesk Revit Sunrise</a>.</p>

<p>I recently installed the
standard <a href="http://knowledge.autodesk.com/support/revit-products/downloads/caas/downloads/content/autodesk-revit-2016-service-pack-2.html">Revit 2016 service pack 2</a>.</p>

<p>The service pack 2 includes the following API related enhancements:</p>

<ul>
<li>Improves stability when using Dynamo and the function <em>Select Divided Surface Families</em>.</li>
<li>Improves stability when DimensionSegment ValueOverride API is called when the dimension value was not overridden.</li>
<li>Improves error handling with element methods and properties.</li>
<li>Improves consistency of UIApplication.PostCommand for pre-selection.</li>
<li>Improves error handling with ElementCategoryFilter.</li>
<li>Improves stability when editing families that use external resources for keynotes.</li>
<li>Adds a validator to the Rotate Tap API to guard against creating invalid models.</li>
<li>Improves error handling with UIDocument.PromptToPlaceViewOnSheet function.</li>
<li>Improves consistency of the RebarContainer element.</li>
</ul>

<p>Here are the service pack 1 API related enhancements:</p>

<ul>
<li>Corrects an issue with DimensionSegment.TextPostion API when handling a dimension with more than one segment.</li>
<li>Corrects an issue to make sure that third-party developers always have the correct value when using the public API to get the upper value or lower value of the conditions.</li>
<li>Corrects an issue that occurred when copying or mirroring an electrical circuit so that wire types are correctly copied as part of the electrical system.</li>
</ul>

<p>They are all obviously included in the R2 release as well.</p>

<p>Back to my RTC conference preparation classes.</p>

<h4><a name="5"></a>Connecting the Desktop and the Cloud</h4>

<p>I am still actively working on
the <a href="https://github.com/CompHound/CompHound.github.io">CompHound component tracker</a> project
for this topic and documenting the work on it extensively
on <a href="http://the3dwebcoder.typepad.com">The 3D Web Coder</a>.</p>

<p>CompHound is a cloud-based universal component and asset usage analysis, reporting, bill of materials, visualisation and navigation project.</p>

<p>It connects Revit and other CAD models to a cloud database consisting of a node.js web server, mongo database and Autodesk View and Data API viewer.</p>

<p>In this session, I plan to start out by discussing the simpler and long-completed FireRating in the Cloud sample, consisting of
the <a href="https://github.com/jeremytammik/FireRatingCloud">FireRatingCloud</a> C# .NET REST API client Revit add-in and
the <a href="https://github.com/jeremytammik/firerating">fireratingdb</a> node.js mongoDB web server.</p>

<p>Here is the class synopsis: We describe the nitty-gritty programming details to implement a cloud-based system to analyse, visualise and report on universal component and asset usage. The components could be Revit family instances used in BIM or any other kind of assets in any other kind of system. The focus is on the cloud-based database used to manage the component occurrences, either in global or project based coordinate systems. Searches can be made based on geographical location or keywords. Models are visualised using the Autodesk View and Data API, providing support for online viewing and model navigation.</p>

<h4><a name="6"></a>The Building Coder Chatroom</h4>

<p>Title: All you ever wanted to ask about the Revit API, The Building Coder, and other non-UI Revit topics.</p>

<p>Synopsis: An open discussion with Jeremy Tammik, The Building Coder and heavy-duty Revit API discussion forum supporter, about anything and everything in Revit that lacks a user interface. DIY, your questions, ideas and sharing experience reign supreme.</p>

<p>I don't know what will happen here &ndash; it depends completely on the participants &nbsp; :-)</p>

<p>I'll take notes and share them with you!</p>

<h4><a name="7"></a>Michael's Getting Started with Revit Macros Material</h4>

<p>Back to the class on Revit Macros.</p>

<p>The original presenter is Michael Kilkelly of <a href="http://spacecmd.com">Space Command</a>, who very kindly provided a fantastic set of class materials.</p>

<p>Michael already presented on this topic in the past, e.g. at Autodesk University 2013.</p>

<p>The materials and recording from his AU 2013
class <a href="http://au.autodesk.com/au-online/classes-on-demand/class-catalog/2013/revit-for-architects/cm2116">CM2116 &ndash; Revit Customization for Mere Mortals: Save Time (and Your Sanity) with Revit Macros</a> provide
a very good starting point. They include the handout document, presentation slides and full video recording:</p>

<ul>
<li>cm2116_revit_macro_handout.pdf (1443692 bytes)</li>
<li>cm2116_revit_macro_presentation.pdf (3311510 bytes)</li>
<li>cm2116_revit_macro_recording.mp4 (542324830 bytes)</li>
</ul>

<p>He is presenting a similar class this year as well, <a href="https://events.au.autodesk.com/connect/search.ww#loadSearch-searchPhrase=kilkelly&amp;searchType=session&amp;tc=1&amp;sortBy=&amp;i(11080)=&amp;i(11700)=">IT11135-L &ndash; Getting Started with Revit Macros</a>:</p>

<blockquote>
  <p>We've all been there &ndash; it's an hour until your deadline and your project manager wants to make a single little change. The problem is, this change will take hours of tedious work... hours you simply don't have. However, through the power of the Revit software API and some basic knowledge of computer programming, you can write macros to automate Revit software and save a ton of time on your next project. This lab is designed to get you started automating Revit software using macros written in the Microsoft Visual Basic .NET programming language. Over the course of this lab, we'll cover programming basics and dive into the Revit software API. We'll do this by writing useful macros you can take back to your office and put to good use. At the end of the class you'll have a solid foundation from which to start writing your own macros. Take command of your software and learn to program! This class is geared toward intermediate-to-advanced Revit software users with little or no programming experience.</p>
</blockquote>

<ul>
<li>Discover the differences between Revit macros, add-ins, and external applications.</li>
<li>Learn how to create custom macros in Revit using the Revit macro editor and the C# and VB.NET programming languages.</li>
<li>Learn how to utilize resources from the Revit Software Development Kit to get more information about .NET and the Revit API.</li>
<li>Learn how to create time-saving macros using a step-by-step process to break complex problems into manageable tasks.</li>
</ul>

<p>Here are the RTC handout document and slide deck that Michael created for me, providing full instructions on how to easily and efficiently get started with Revit macros:</p>

<ul>
<li><a href="/a/doc/revit/rtc/2015/doc/s1_2_pres_revit_macros_jtammik.pdf">Slide desk</a></li>
<li><a href="/a/doc/revit/rtc/2015/doc/s1_2_handout_revit_macros_jtammik.pdf">Handout document</a></li>
</ul>

<p>For the sake of completeness, the pleasure of Internet search engines and above all Revit API students, here is the full handout text direct and live:</p>

<h3><a name="10"></a>Getting Started with Revit Macros</h3>

<p>By Michael Kilkelly, <a href="http://spacecmd.com">Space Command</a>.</p>

<h4><a name="11"></a>What Are Macros?</h4>

<p>Macros are one of the easiest ways to Automate Revit and access the inner workings of the software. Macros do not require any additional software other than Revit and are a great way for beginners to learn programming.</p>

<p>So what exactly is a macro? A macro is a user created command that is coded using Revit’s API or Application Programming Interface. Macros are run directly inside of Revit and are saved in the project file. Other applications, like Microsoft Excel and Word, also have the ability to create macros. Revit macros are different from those in Excel and Word because you cannot record actions directly into a macro. Revit macros must be coded by hand.</p>

<h4><a name="12"></a>Getting Started With Revit Macros</h4>

<p>To get started writing your own macros, you should first install the Revit 2016 Software Development Kit or SDK. The SDK contains help files and sample code that will assist you as you learn to program macros. The Revit 2016 SDK be installed from the main page of the Revit installer or it can be downloaded from the Autodesk Developer Network Revit Developer page at</p>

<p><center>
<a href="http://www.autodesk.com/developrevit">www.autodesk.com/developrevit</a>.
</center></p>

<p>The SDK will install on your hard drive and create the following subfolders and files. Take some time to review the files. The macro samples are particularly useful as you get started creating your own macros.</p>

<p><center></p>

<p><a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e16897883301b8d16d6572970c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883301b8d16d6572970c img-responsive" style="width: 400px; " alt="Revit SDK" title="Revit SDK" src="/assets/image_a414df.jpg" /></a><br /></p>

<p></center></p>

<h4><a name="13"></a>Write Your First Macro</h4>

<p>Ready to write your first macro? As you’ll see, the process is very easy. Follow the steps below and you’ll be on your way to macro mastery.</p>

<h4><a name="131"></a>1. Open the Macro Manager</h4>

<p>Create a new project file. Click the Manage ribbon then click the Macro Manager icon. This will open the Macro Manager dialog.</p>

<p><center></p>

<p><a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e16897883301bb08876e7d970d-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883301bb08876e7d970d image-full img-responsive" alt="Manage tab" title="Manage tab" src="/assets/image_2f6bbd.jpg" border="0" /></a><br /></p>

<p></center></p>

<p>Macros can reside in a project file or within the Revit application. Macros saved in the project file can be used by any user who opens that file. Macros saved in the application are saved to the user’s Revit configuration. These macros can be used on any model file but only by the user who created the macro.</p>

<p><center></p>

<p><a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e16897883301bb08877163970d-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883301bb08877163970d img-responsive" style="width: 400px; " alt="Macro manager" title="Macro manager" src="/assets/image_5fb11b.jpg" /></a><br /></p>

<p></center></p>

<h4><a name="132"></a>2. Create a New Module</h4>

<p>Macros are organized in modules. When creating a macro in a new project file, you must first create a module. A module is simply a collection of macros. A single project file can contain several modules with each module having its own macros. Module names cannot contain spaces or special characters.</p>

<p>To create a module, click the “Project 1” tab then click the Module button in the “Create” section. In the “Create a New Module” dialog box, title your module “MyFirstModule”. You can write macros in C#, VB.NET, Python or Ruby. For this exercise, choose C# as the module’s language. Click OK to create the module.</p>

<p><center></p>

<p><a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e16897883301bb08876ea3970d-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883301bb08876ea3970d img-responsive" style="width: 387px; " alt="Create new module" title="Create new module" src="/assets/image_2ffe01.jpg" /></a><br /></p>

<p></center></p>

<p>Once Revit has created the module, SharpDevelop will launch. SharpDevelop is an open-source development environment that is built into Revit for programming macros.</p>

<h4><a name="133"></a>3. Create a New Macro</h4>

<p>Now that you have a module, you can create a macro inside the module. Click the Macro button in the “Create” section of the Macro Manager dialog. In the “Create a New Macro” dialog, title your first macro “MyFirstMacro” and set the language to C#. Click OK to create the macro.</p>

<p><center></p>

<p><a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e16897883301b8d16d65de970c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883301b8d16d65de970c img-responsive" style="width: 378px; " alt="Create new macro" title="Create new macro" src="/assets/image_eba073.jpg" /></a><br /></p>

<p></center></p>

<h4><a name="134"></a>4. Write the Macro</h4>

<p>Switch over to SharpDevelop. You’ll see the standard C# code that is automatically generated when you create a new module. Toward the bottom you’ll see the starting code for “MyFirstMacro”.</p>

<p>Your first macro is simply going to popup a message box in Revit. It only takes one line of code. After the “public void MyFirstMacro()”, type the following between the brackets:</p>

<p><center></p>

<p><a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e16897883301bb0887717c970d-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883301bb0887717c970d img-responsive" style="width: 400px; " alt="Macro source code" title="Macro source code" src="/assets/image_ef2624.jpg" /></a><br /></p>

<p></center></p>

<pre class="code">
&nbsp; <span class="blue">public</span> <span class="blue">void</span> MyFirstMacro()
&nbsp; {
&nbsp; &nbsp; <span class="teal">TaskDialog</span>.Show( <span class="maroon">&quot;My First Macro&quot;</span>, <span class="maroon">&quot;Hello World!&quot;</span> );
&nbsp; }
</pre>

<p>Make sure you add a semicolon at the end of the TaskDialog line. This indicates the end of a line to C#.</p>

<h4><a name="135"></a>5. Build the Macro</h4>

<p>Once you’ve typed the code, you’re ready to compile or “build” the macro. All macros must be built before Revit can run them. In the SharpDevelop menu bar, select “Build” then “Build Solution”.</p>

<p><center></p>

<p><a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e16897883301bb08876ee1970d-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883301bb08876ee1970d img-responsive" style="width: 400px; " alt="Build the macro" title="Build the macro" src="/assets/image_00110a.jpg" /></a><br /></p>

<p></center></p>

<p>SharpDevelop will compile your C# code into the .NET intermediate code. Any errors or warning will show up in the Errors and Warning window located at the bottom of the SharpDevelop interface.</p>

<p><center></p>

<p><a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e16897883301bb08876ef5970d-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883301bb08876ef5970d img-responsive" style="width: 400px; " alt="Error list" title="Error list" src="/assets/image_82b7be.jpg" /></a><br /></p>

<p></center></p>

<p>If you have an error, double-check your code. The code window will list errors by line number so they are easy to pinpoint.</p>

<h4><a name="136"></a>6. Run the Macro</h4>

<p>If your macro compiled correctly, go back to Revit and open the Macro Manager dialog (Manage > Macro Manager). You should see “MyFirstMacro” in the list below “MyFirstModule”.</p>

<p><center></p>

<p><a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e16897883301b8d16d663d970c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883301b8d16d663d970c img-responsive" style="width: 400px; " alt="Run macro" title="Run macro" src="/assets/image_354689.jpg" /></a><br /></p>

<p></center></p>

<p>Select “MyFirstMacro” from the list then click the Run button. This will execute your macro. You should see the following on your screen:</p>

<p><center></p>

<p><a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e16897883301b7c7e39fa8970b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883301b7c7e39fa8970b img-responsive" style="width: 252px; " alt="Macro message box" title="Macro message box" src="/assets/image_9b6435.jpg" /></a><br /></p>

<p></center></p>

<p>You did it! You wrote your first Revit macro. To take this further, you can modify the code to report back something more useful. Change your code to the following:</p>

<p><center></p>

<p><a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e16897883301bb08876f5c970d-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883301bb08876f5c970d image-full img-responsive" alt="Code to report project path name" title="Code to report project path name" src="/assets/image_fde198.jpg" border="0" /></a><br /></p>

<p></center></p>

<p>Whitespace and line breaks are ignored in C#, so you can spread the statement over several lines for better readability:</p>

<pre class="code">
&nbsp; <span class="blue">public</span> <span class="blue">void</span> MyFirstMacro()
&nbsp; {
&nbsp; &nbsp; <span class="teal">TaskDialog</span>.Show( <span class="maroon">&quot;My First Macro&quot;</span>,
&nbsp; &nbsp; &nbsp; <span class="maroon">&quot;The current model file is &quot;</span>
&nbsp; &nbsp; &nbsp; + <span class="blue">this</span>.Application.ActiveUIDocument.Document.PathName );
&nbsp; }
</pre>

<p>The “this.Application.ActiveUIDocument.” object represents the current model file. The “Document” object contains data pertaining to the current file itself. To see the active view in the current project file, change “Document.PathName” to “ActiveView.Name”.</p>

<h4><a name="14"></a>Next Steps</h4>

<p>Congratulations! You’re on your way to Revit macro mastery. The next challenge is learning to write code and utilize the Revit API. While teaching all the details of programming is beyond the scope of this workshop, I will highlight some key areas to guide you on your journey.</p>

<h4><a name="15"></a>Choose a Programming Language</h4>

<p>In the example above, we used C# to write the macro. C# is just one of four languages you can use to write macros. Since Revit uses the Microsoft .NET framework 4.0, you can write macros in either Python, Ruby, C# or VB.NET. All these languages compile into the same intermediate language so you have full access to Revit’s API from any of the languages.</p>

<p>Below is additional information about the supported language as well as pros and cons to each.</p>

<ul>
<li>Language: History, Pros and Cons</li>
<li>C#: Based on C and C++; Lots of Revit specific code samples available online. You can use C# to develop stand-alone desktop applications.
￼￼The language syntax is not as readable as other languages. The code is more terse, is case sensitive and uses obscure symbols</li>
<li>VB.NET: Evolved from Microsoft’s Visual Basic Language
￼￼VB.NET code is easier to read than C#. The language is not as strict as C#. You can use VB.NET to develop desktop applications.
￼￼VB.NET is “wordier” than C# it takes more lines of code to do the same thing. Some say the language isn’t as elegant as other languages.</li>
<li>Python: Created in 1991 by Guido van Rossum.
￼Lots of general code samples and learning resources available. Easy to learn. Python code is very readable. Can build web and desktop apps using Python.
￼ Not many Revit specific code samples available online yet. Some debugging features not available in SharpDevelop</li>
<li>Ruby: Created in 1995 by Yukihiro Matsumoto.
￼Lots of general code samples and learning resources available. Easy to learn. Code is very readable. Can build web apps with Ruby.
￼￼Not many Revit specific code samples available online yet.
￼￼
C# will be used for the code examples that follow.</li>
</ul>

<h4><a name="16"></a>Converting Code from One Language to Another</h4>

<p>SharpDevelop can convert code from one language to another. If you find a good Revit code sample written in VB.NET, you can easily convert it to C#.</p>

<p>To convert code, simply create a module and macro using the language of the code sample then, in SharpDevelop, select Project > Convert and choose the language to convert the code into.</p>

<p><center></p>

<p><a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e16897883301b7c7e39d5c970b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883301b7c7e39d5c970b img-responsive" style="width: 400px; " alt="Converting language" title="Converting language" src="/assets/image_61dcc5.jpg" /></a><br /></p>

<p></center></p>

<p>Note that the conversion process is not always perfect. Sometimes you may find the code converts into a string of gibberish, unfortunately.</p>

<h4><a name="17"></a>Learning the Revit API</h4>

<p>As you move beyond your first Revit macro, you’ll need to get familiar with the Revit API. The best way to do that is through the Revit API help file. The help file is your roadmap to learning the API. You can find the help file in the Revit 2016 SDK folder. Open the RevitAPI.chm file and click the “Content” tab. The help file lists all of the namespaces in the Revit API.</p>

<p>A namespace is essentially a hierarchical container for the elements within the API. A good analogy for namespaces is your computer’s folder structure. Each folder at the same level of the directory structure must have a unique name. The folders can contain similarly named files but the path to each file will be unique as the folder names are unique. Namespaces work the same way. There may be elements within the API that are named the same. For example, many elements have a “Geometry” property but namespaces provide a way to accurately identify which geometry you’re specifying. To reference the wall geometry property, you type Autodesk.Revit.DB.Wall.Geometry.</p>

<p>To find more information about a specific element within the API, simply drill down through the namespaces to find the element. For instance, if I want to learn more about the properties of wall objects, I click Autodesk.Revit.DB Namespace > Wall Class > Wall Properties. The help file lists all the properties of wall elements.</p>

<p><center></p>

<p><a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e16897883301b8d16d668b970c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883301b8d16d668b970c img-responsive" style="width: 400px; " alt="Revit API help file RevitAPI.chm" title="Revit API help file RevitAPI.chm" src="/assets/image_97004c.jpg" /></a><br /></p>

<p></center></p>

<p>Reading the API help file is not easy. It takes some practice as it is not written in plain English. Rather, it is a description of all the elements within the API. The help file does contain code samples but it not a learning tool. Much like a road map will not teach you to drive a car, the API help file will not teach you to code but it will help you get where you’re going.</p>

<h4><a name="18"></a>Troubleshooting Macros</h4>

<p>You will spend a lot of time troubleshooting and debugging your macros. One of the great things about coding is that the feedback is immediate. You write some code, compile it then run it. Your code will either work or it will not. Revit will tell you immediately if it does not work and you will feel a sense of accomplishment when it does work. SharpDevelop provides a number of tools to assist you while troubleshooting your code.</p>

<h4><a name="19"></a>Using Debug.Print and the Output Window</h4>

<p>While writing code, it is often useful to have your macro report back information while the macro is running. Writing code is an iterative process and you will need feedback as you develop your macro. SharpDevelop’s output window is useful for understanding what’s going on inside your macro.</p>

<p>To output information to the output window, use the Debug.Print command. Before you can use the command, however, you’ll need to add the Systems.Diagnostics namespace to your macro. You do this by adding “using System.Diagnostics” to the beginning of your macro code.</p>

<p><center></p>

<p><a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e16897883301b8d16d66a2970c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883301b8d16d66a2970c img-responsive" style="width: 300px; " alt="Systems.Diagnostics namespace" title="Systems.Diagnostics namespace" src="/assets/image_322714.jpg" /></a><br /></p>

<p></center></p>

<h4><a name="20"></a>Stepping Through Your Code</h4>

<p>When you compile your code and run it, Revit will run through the code sequentially. While writing macros however, it is often useful to step through your code line by line so you can see exactly what is going on. You can step through your code using the Step Into button in the “Macro Manager” dialog.</p>

<p><center></p>

<p><a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e16897883301b8d16d66c4970c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883301b8d16d66c4970c img-responsive" style="width: 400px; " alt="Step through code" title="Step through code" src="/assets/image_58e2de.jpg" /></a><br /></p>

<p></center></p>

<p>Press the F10 key to step through the next line of code. While you are walking through the code, you can view the Output window to see any output from your Debug.Print lines. You can also view the current values in your variables through the “Local Variables” tab.</p>

<h4><a name="21"></a>Using Break Points</h4>

<p>In addition to stepping through your code, you can set specific points where you want the code to stop running so you can check out the Output or Local Variables windows. Clicking on the grey area to the left of the line number row will create a break point. A break point is represented as a red dot. Any line containing a break point will also be highlighted in red. When Revit encounters a break point when running the code, it will stop executing the code. Pressing F10 will step through your code or press F5 to continue running the remainder of the macro.</p>

<p><center></p>

<p><a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e16897883301b7c7e39dc5970b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883301b7c7e39dc5970b image-full img-responsive" alt="Break point" title="Break point" src="/assets/image_0c0ad9.jpg" border="0" /></a><br /></p>

<p></center></p>

<h4><a name="22"></a>Commenting Your Code</h4>

<p>One of the most critical practices to follow when writing code is to add comments as you are writing your code. These comments should serve as a reminder for what the code does and why it’s structured in that particular way.</p>

<p>Each language has its own syntax for writing comments. Comments are identified in C# by a double slash (//) at the beginning of the line. SharpDevelop highlights all comments in green.</p>

<p><center></p>

<p><a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e16897883301b7c7e39ddc970b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883301b7c7e39ddc970b img-responsive" style="width: 201px; " alt="C# comment" title="C# comment" src="/assets/image_4701bc.jpg" /></a><br /></p>

<p></center></p>

<p>Comments can also be used to block code from running. Say you are testing some alternate approaches to a specific part of the macro. You can “comment out” parts of the code that you do not want to run. If you have three options for the code, comment out two and run the macro with one of the options. Commenting out can also be used to test very specific parts of your macro. If you are getting errors from one section of the macro, comment out everything else, build the macro and step through it. This focused approach will save you a lot of time while troubleshooting.</p>

<h4><a name="23"></a>Exceptions</h4>

<p>Face it, your code is not going to be perfect. Even if your code compiles without an error, it can still crash or throw an exception when you run it. This is simply the nature of coding. If you get an error, use the methods listed above to systematically work through your code to identify the problem. This can seem like finding a needle in a haystack when you are first starting out but as you code more and more macros, you will get better at identifying problems in your code.</p>

<h4><a name="24"></a>Macro Sample</h4>

<p>Our first macro was useful for illustrating the process for creating a macro but let us take what we just learned and put it to use on a macro that is more useful. The following code deletes unused views in the current model file. If a view is not on a sheet or does not have the “working” prefix, it is deleted. Note this macro does not work with dependent views.</p>

<p><center></p>

<p><a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e16897883301bb08876ff6970d-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883301bb08876ff6970d image-full img-responsive" alt="Real-world macro" title="Real-world macro" src="/assets/image_e2c2d8.jpg" border="0" /></a><br /></p>

<p></center></p>

<h4><a name="25"></a>Next Steps</h4>

<p>What else can you do with Revit macros? Pretty much anything you can think of! Good candidates are tasks that are fairly standardized or require lots of user input. Some examples include:</p>

<ul>
<li>Update all window family instances with manufacturer data from spreadsheets.</li>
<li>Check that all doors in fire-rated walls are actually fire-rated doors.</li>
<li>Rename all custom families in the project file using a specific prefix for your company.</li>
<li>Automatically place specific views on a sheet.</li>
</ul>

<p>Think about the tasks you do on a regular basis. Which of these do you like the least? Would you like to automate it? Could you write a macro that would do the task for you?</p>

<h4><a name="26"></a>Additional Resources</h4>

<p>Want to learn more about writing your own macros? Check out these resources for more information.</p>

<ul>
<li>Blogs
<ul>
<li>The Building Coder &ndash; http://thebuildingcoder.typepad.com/</li>
<li>ArchSmarter &ndash; http://archsmarter.com</li>
<li>Boost Your BIM &ndash; http://boostyourbim.wordpress.com/</li>
<li>The Proving Ground &ndash; http://wiki.theprovingground.org/revit-api</li>
</ul></li>
<li>Online Forums
<ul>
<li>AUGI &ndash; http://forums.augi.com/forumdisplay.php?218-Revit-API</li>
</ul></li>
<li>Online Courses
<ul>
<li>Learn to Program the Revit API &ndash; https://www.udemy.com/revitapi/</li>
<li>Mastering Revit Macros &ndash; http://learn.archsmarter.com</li>
</ul></li>
<li>Books
<ul>
<li>Autodesk Revit 2013 Customization with .NET How-to by Don Rudder</li>
</ul></li>
</ul>

<h4><a name="27"></a>Conclusion</h4>

<p>Learning to write macros and automate Revit will drastically improve your efficiency. A well-written macro can do more in five minutes than a regular user can accomplish in one hour. Learning to program takes time and patience. Start small and work systematically. You’ll be on your way to macro mastery in no time!</p>
