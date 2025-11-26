---
layout: "post"
title: "Reload Add-in for Debug Without Restart"
date: "2012-12-05 05:00:00"
author: "Jeremy Tammik"
categories:
  - "AU"
  - "Debugging"
  - "Utilities"
original_url: "https://thebuildingcoder.typepad.com/blog/2012/12/reload-add-in-for-debug-without-restart.html "
typepad_basename: "reload-add-in-for-debug-without-restart"
typepad_status: "Publish"
---

<p>We explored and learned a lot of interesting areas during the DevLab at AU on Tuesday last week.

<p>One recurring theme is how to effectively debug an add-in without having to restart Revit and reload the entire model each time a change is made to the source code.</p>

<p>A long, long time back, right in the beginning of the Revit .NET API, it was briefly possible to use the Visual Studio Edit and Continue feature for that.
That was obviously an absolutely perfect solution.
Alas, those times are past, and unlikely to return.
I almost find it hard to believe I am not imagining things.</p>

<p>There are in fact several other known solutions to this problem, which I would classify as follows:

<ul>
<li>Stupid: set up the Revit project and Visual Studio debug settings to bring you to the exact required debugging position and context in one F5 'start debugging' click.
Every time a code modification is required, simply stop debugging, modify the code, recompile and restart Revit.
That is the approach I use myself, since I can get by working in absolutely minimal test models.

<li>Cool: use an interactive scripting language such as

<!-- 865_python_ui_server_api.htm -->

<a href="http://thebuildingcoder.typepad.com/blog/2012/11/au-classes-on-python-ui-server-and-framework-apis.html#1">
Python</a> or

<!-- 797_revitrubyshell.htm -->

<a href="http://thebuildingcoder.typepad.com/blog/2012/07/meetings-football-and-revitrubyshell.html#2">
Ruby</a>.

In fact, the wish be able to modify the API code interactively without requiring the tedious recompile and reload cycle was one of the main motivating factors in implementing the latter.
The use of this functionality is obviously just a side effect of the much larger main advantage of being able to play interactively with the code in complete freedom, since it is interpreted on the fly and no compilation at all is required.

<li>Effective and laborious: convert the code to be debugged to a macro and use the built-in SharpDevelop IDE.
This means that you convert the part of your add-in that your are debugging and modifying to a macro.
The rest can be left as a compiled DLL.
The two components can communicate with each other.
When finished debugging, you move the code out of the macro environment into the external DLL again.

<li>Efficient: use the

<a href="http://thebuildingcoder.typepad.com/blog/2010/03/addinmanager.html">
AddInManager</a> and

attach to process to reload repeatedly, with compile and edit cycles.
</ul>

<p>I already described all three of the solutions not involving Python and Ruby in an earlier discussion on

<a href="http://thebuildingcoder.typepad.com/blog/2011/05/debugging-an-add-in-without-restarting-revit.html">
debugging an add-in without restarting Revit</a>.

<p>Melissa Manalac of

<a href="http://www.s-frame.com">S-frame</a>

was making such efficient use of the AddInManager approach at the DevLab that I asked her to document it for you once again.</p>

<p>She points out that James LeVieux already summarized the process in his

<a href="http://thebuildingcoder.typepad.com/blog/2011/05/debugging-an-add-in-without-restarting-revit.html?cid=6a00e553e16897883301538f2ad15a970b#comment-6a00e553e16897883301538f2ad15a970b">
comment</a> on

that discussion:</p> <!--  mentioned above -->

<p>Here are the required steps:</p>

<ol>
<li>Start Revit 2013.
<li>Before coding, make sure all processes are detached.
<li>After coding, rebuild your solution and attach to the Revit process.
<li>In Revit, go to the Add-Ins tab &gt; Add-In Manager and reload the desired add-in DLL.
<li>Run desired external command.
<li>Repeat steps 2 to 5.
</ol>

<p>You also need to be aware that the AddInManager requires the external commands it loads to use manual transaction mode.
This is no significant restriction, since

<a href="http://thebuildingcoder.typepad.com/blog/2012/05/read-only-and-automatic-transaction-modes.html">
automatic transaction mode is not recommended</a> anyway.

<p>Here are some screen snapshots to further clarify these steps:</p>

<a name="1"></a>

<p><b>1</b>. Start Revit 2013.</p>


<a name="2"></a>

<p><b>2</b>. Before coding, make sure all processes are detached:</p>

<center>

<a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e168978833017ee5efe0ed970d-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e168978833017ee5efe0ed970d" alt="Detach all processes" title="Detach all processes" src="/assets/image_6ada82.jpg" border="0" /></a><br />

</center>


<a name="3"></a>

<p><b>3</b>. After coding, rebuild the add-in solution and attach to Revit process.</p>

<p>Rebuild solution:</p>

<center>

<a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e168978833017c344c4e76970b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e168978833017c344c4e76970b" alt="Rebuild solution" title="Rebuild solution" src="/assets/image_73afb4.jpg" border="0" /></a><br />

</center>

<p>Attach to process:</p>

<center>

<a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e168978833017ee5efe692970d-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e168978833017ee5efe692970d" alt="Attach to Revit.exe process" title="Attach to Revit.exe process" src="/assets/image_9676eb.jpg" border="0" /></a><br />

</center>

<p>Select the Revit.exe process:</p>

<center>

<a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e168978833017ee5efe7f9970d-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e168978833017ee5efe7f9970d image-full" alt="Attach to Revit.exe process" title="Attach to Revit.exe process" src="/assets/image_8fac1a.jpg" border="0" /></a><br />

</center>



<a name="4"></a>

<p><b>4</b>. In Revit, go to the Add-Ins tab &gt; Add-In Manager and reload the desired add-in DLL.</p>

<p>Launch the Add-in Manager:</p>

<center>

<a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e168978833017d3e7b3653970c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e168978833017d3e7b3653970c image-full" alt="Launch the Add-in Manager" title="Launch the Add-in Manager" src="/assets/image_b01468.jpg" border="0" /></a><br />

</center>

<p>Load the add-in DLL:</p>

<center>

<a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e168978833017ee5efeb48970d-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e168978833017ee5efeb48970d" alt="Load the add-in DLL" title="Load the add-in DLL" src="/assets/image_2d1aa3.jpg" border="0" /></a><br />

</center>


<a name="5"></a>

<p><b>5</b>. Run the desired external command:</p>

<center>

<a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e168978833017ee5efecdb970d-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e168978833017ee5efecdb970d" alt="Run the add-in command" title="Run the add-in command" src="/assets/image_a608d5.jpg" border="0" /></a><br />

</center>


<a name="6"></a>

<p><b>6</b>. Repeat steps 2 to 5 as desired.</p>

<p>Many thanks to Melissa for the detailed up-to-date description!</p>



<a name="20"></a>

<h4>Source Code Coloriser</h4>

<p>As I mentioned quite a while back, I use the Visual Studio

<a href="http://thebuildingcoder.typepad.com/blog/2011/04/updated-sdk-2012-products-and-source-code-colourisation.html#4">
CopyToHtml</a> plug-in

to copy and paste colour coded C# and VB source code to HTML for my blog posts.

<p>Harri Mattison of

<a href="http://thebuildingcoder.typepad.com/blog/2012/12/boost-your-bim-and-dance-with-an-elephant.html">
Boost Your BIM</a> now

pointed out that this utility obviously cannot be used in the built-in Revit SharpDevelop macro environment.

<p>He found this

<a href="http://www.pvladov.com/p/code-colorizer.html">
online C# code coloriser tool</a> by

Pavel Vladov that is quite nice and can be used instead.

<p>Oh, and I found another one myself,

<a href="http://tohtml.com">tohtml.com</a>,

that supports a huge number of different languages besides C#.
Unfortunately, it does not escape all the HTML characters, e.g. '&lt;' remains '&lt;' instead of being converted to '&amp;lt;'.</p>
