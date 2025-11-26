---
layout: "post"
title: "RvtSamples for Revit 2017"
date: "2016-04-20 05:00:00"
author: "Jeremy Tammik"
categories:
  - "2017"
  - "Debugging"
  - "Getting Started"
  - "Migration"
  - "SDK Samples"
  - "Settings"
  - "Update"
original_url: "https://thebuildingcoder.typepad.com/blog/2016/04/rvtsamples-for-revit-2017.html "
typepad_basename: "rvtsamples-for-revit-2017"
typepad_status: "Publish"
---

<p>Prompted by</p>

<p>Dan Tartaglia <a href="http://thebuildingcoder.typepad.com/blog/2016/04/revit-2017-revitlookup-and-sdk-samples.html#comment-2633447580">commented</a> on
the <a href="http://thebuildingcoder.typepad.com/blog/2016/04/revit-2017-revitlookup-and-sdk-samples.html">Revit 2017 SDK sample compilation</a>, saying:</p>

<blockquote>
  <p>A couple of other issues to note about the SDK examples (RvtSamples).</p>
  
  <ol>
  <li>The Icons folder has to be copied to where the RvtSamples.dll exists (probably obvious to most).</li>
  <li>RvtSamples.txt: there are only 6 statements for PlacementOptions.dll (the description is missing).</li>
  <li>RvtSamples.txt: The assembly path for CreateShared.dll is not correct (for both paths).</li>
  </ol>
</blockquote>

<p>That was the first time in history that someone else took a look at RvtSamples for a new major release of Revit before I did.</p>

<p>Yipee!</p>

<p>So I guess I'd better get going with it as well:</p>

<ul>
<li><a href="#2">Setting up RvtSamples for Revit 2017</a></li>
<li><a href="#3">Copy Html Markup in Visual Studio 2015</a></li>
<li><a href="#4">Running Revit 2017 in the Visual Studio 2015 debugger</a></li>
<li><a href="#5">'Security &ndash; Unsigned Add-In' message</a></li>
<li><a href="#6">RvtSamples DLL and TXT should be together</a></li>
<li><a href="#7">Specifying the Revit SDK samples root path</a></li>
<li><a href="#8">Correcting errors in individual SDK sample entries</a></li>
<li><a href="#9">PlacementOptions description line is missing</a></li>
<li><a href="#10">The five FabricationPartLayout external commands</a></li>
<li><a href="#11">RvtSamples loads and RvtSamples.txt is cleaned up</a></li>
</ul>

<h4><a name="2"></a>Setting up RvtSamples for Revit 2017</h4>

<p>The first thing I did was add the RvtSamples add-in manifest and input text file to its project files:</p>

<p><center></p>

<p><a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e16897883301bb08e4b4de970d-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883301bb08e4b4de970d img-responsive" style="width: 299px; " alt="RvtSamples add-in manifest and input text file" title="RvtSamples add-in manifest and input text file" src="/assets/image_aed61b.jpg" /></a><br /></p>

<p></center></p>

<p>Then in <code>Application.cs</code>, I set the Boolean test variable <code>testClassName</code> to true:</p>

<pre class="code">
  <span style="color:blue;">bool</span>&nbsp;testClassName&nbsp;=&nbsp;<span style="color:blue;">true</span>;&nbsp;<span style="color:green;">//&nbsp;jeremy</span>
</pre>

<p>Search for 'jeremy' to find it.</p>

<p>Now RvtSamples will complain on any external command specified in <code>RvtSamples.txt</code> that cannot be found.</p>

<h4><a name="3"></a>Copy Html Markup in Visual Studio 2015</h4>

<p>While writing this, I notice that I have to install something like <code>CopySourceAsHtml</code> in my Visual Studio 2015 IDE before I can start describing my steps here.</p>

<p>First things first.</p>

<p>It was not completely trivial
to <a href="http://thebuildingcoder.typepad.com/blog/2014/04/migrating-roomeditorapp-to-revit-2015.html#5">enable CopySourceAsHtml for Visual Studio 2012</a>.</p>

<p>I tried to use a similar procedure for Visual Studio 2015, e.g.:</p>

<pre>
&gt; md "C:\Users\tammikj\Documents\Visual Studio 2015\Addins"

&gt; copy "C:\Users\tammikj\Documents\Visual Studio 2012\Addins" "C:\Users\tammikj\Documents\Visual Studio 2015\Addins"
C:\Users\tammikj\Documents\Visual Studio 2012\Addins\CopySourceAsHtml.AddIn
  1 file(s) copied.
</pre>

<p>I edited the add-in file analogously to the last installation, only to discover &ndash; after some significant effort and frustration &ndash;
<a href="http://visualstudioadventures.com/2015/04/25/visual-studio-2015-goodbye-add-ins-hello-vspackage-extensions">Visual Studio 2015: goodbye add-ins, hello VsPackage extensions</a>.</p>

<p>So I will have to set up an entirely new system for this now.</p>

<p>After quite a bit of fruitless searching I ended up installing
the <a href="https://visualstudiogallery.msdn.microsoft.com/34ebc6a2-2777-421d-8914-e29c1dfa7f5d">Productivity Power Tools 2015</a>.</p>

<p>Now I once again have my &ndash; or 'a' &ndash; 'Copy Html Markup' menu entry:</p>

<p><center></p>

<p><a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e16897883301bb08e4b546970d-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883301bb08e4b546970d img-responsive" style="width: 497px; " alt="Copy Html Markup menu entry" title="Copy Html Markup menu entry" src="/assets/image_c4456a.jpg" /></a><br /></p>

<p></center></p>

<p>It does a really good job!</p>

<p>This is the RvtSamples add-in manifest, retrieved via the new tool, pasted into the blog post text completely unmodified:</p>

<pre style="font-family:Consolas;font-size:13;color:black;background:white;"><span style="color:blue;">&lt;?</span><span style="color:#a31515;">xml</span><span style="color:blue;">&nbsp;</span><span style="color:red;">version</span><span style="color:blue;">=</span>&quot;<span style="color:blue;">1.0</span>&quot;<span style="color:blue;">&nbsp;</span><span style="color:red;">encoding</span><span style="color:blue;">=</span>&quot;<span style="color:blue;">utf-8</span>&quot;<span style="color:blue;">?&gt;</span>
<span style="color:blue;">&lt;</span><span style="color:#a31515;">RevitAddIns</span><span style="color:blue;">&gt;</span>
<span style="color:blue;">&nbsp;&nbsp;&lt;</span><span style="color:#a31515;">AddIn</span><span style="color:blue;">&nbsp;</span><span style="color:red;">Type</span><span style="color:blue;">=</span>&quot;<span style="color:blue;">Application</span>&quot;<span style="color:blue;">&gt;</span>
<span style="color:blue;">&nbsp;&nbsp;&nbsp;&nbsp;&lt;</span><span style="color:#a31515;">Name</span><span style="color:blue;">&gt;</span>External&nbsp;Tool<span style="color:blue;">&lt;/</span><span style="color:#a31515;">Name</span><span style="color:blue;">&gt;</span>
<span style="color:blue;">&nbsp;&nbsp;&nbsp;&nbsp;&lt;</span><span style="color:#a31515;">Assembly</span><span style="color:blue;">&gt;</span>C:\a\lib\revit\2017\SDK\Samples\RvtSamples\CS\bin\Debug\RvtSamples.dll<span style="color:blue;">&lt;/</span><span style="color:#a31515;">Assembly</span><span style="color:blue;">&gt;</span>
<span style="color:blue;">&nbsp;&nbsp;&nbsp;&nbsp;&lt;</span><span style="color:#a31515;">ClientId</span><span style="color:blue;">&gt;</span>42cb0a70-2ee7-4e64-a42d-87b9cdcc41c8<span style="color:blue;">&lt;/</span><span style="color:#a31515;">ClientId</span><span style="color:blue;">&gt;</span>
<span style="color:blue;">&nbsp;&nbsp;&nbsp;&nbsp;&lt;</span><span style="color:#a31515;">FullClassName</span><span style="color:blue;">&gt;</span>RvtSamples.Application<span style="color:blue;">&lt;/</span><span style="color:#a31515;">FullClassName</span><span style="color:blue;">&gt;</span>
<span style="color:blue;">&nbsp;&nbsp;&nbsp;&nbsp;&lt;</span><span style="color:#a31515;">VendorId</span><span style="color:blue;">&gt;</span>ADSK<span style="color:blue;">&lt;/</span><span style="color:#a31515;">VendorId</span><span style="color:blue;">&gt;</span>
<span style="color:blue;">&nbsp;&nbsp;&nbsp;&nbsp;&lt;</span><span style="color:#a31515;">VendorDescription</span><span style="color:blue;">&gt;</span>Autodesk,&nbsp;www.autodesk.com<span style="color:blue;">&lt;/</span><span style="color:#a31515;">VendorDescription</span><span style="color:blue;">&gt;</span>
<span style="color:blue;">&nbsp;&nbsp;&lt;/</span><span style="color:#a31515;">AddIn</span><span style="color:blue;">&gt;</span>
<span style="color:blue;">&lt;/</span><span style="color:#a31515;">RevitAddIns</span><span style="color:blue;">&gt;</span></pre>

<p>Now, to continue with RvtSamples...</p>

<h4><a name="4"></a>Running Revit 2017 in the Visual Studio 2015 Debugger</h4>

<p>Next, I set up the RvtSamples project to launch Revit 2017 in the Visual Studio debugger.</p>

<p>Starting up Revit 2017 in the Visual Studio 2015 debugger does not work at all initially:</p>

<p><center></p>

<p><a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e16897883301b8d1cb1dd0970c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883301b8d1cb1dd0970c image-full img-responsive" alt="Debugging Revit 2017 in Visual Studio 2015" title="Debugging Revit 2017 in Visual Studio 2015" src="/assets/image_7ff30c.jpg" border="0" /></a><br /></p>

<p></center></p>

<p>Happily, this issue has been addressed, e.g. in
the <a href="http://forums.autodesk.com/t5/revit-api/bd-p/160">Revit API discussion forum</a> thread discussing
<a href="http://forums.autodesk.com/t5/revit-api/cannot-start-revit-2015-for-api-debugging/td-p/4978060">cannot start Revit 2015 for API debugging</a> and
<a href="http://forums.autodesk.com/t5/revit-api/visual-studio-2015/td-p/5647387">Visual Studio 2015</a>.</p>

<p>They suggest to "Use Managed Compatibility Mode", found under Tools &gt; Options &gt; Debugging, and enabling native code debugging, respectively.</p>

<p>I opted for the former:</p>

<p><center></p>

<p><a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e16897883301bb08e4b596970d-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883301bb08e4b596970d image-full img-responsive" alt="Use Managed Compatibility Mode" title="Use Managed Compatibility Mode" src="/assets/image_07b77a.jpg" border="0" /></a><br /></p>

<p></center></p>

<p>With that setting, I am now able to start up Revit 2017 in the Visual Studio 2015 debugger and continue with my RvtSamples installation.</p>

<h4><a name="5"></a>'Security &ndash; Unsigned Add-In' Message</h4>

<p>Revit asked me to sign in using my Autodesk account.</p>

<p>It then brought up the new security dialogue, explaining that the RvtSamples add-in assembly that I am attempting to load is not equipped with a trusted signature, and asking me to confirm loading it:</p>

<p><center></p>

<p><a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e16897883301bb08e4b5ec970d-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883301bb08e4b5ec970d image-full img-responsive" alt="Security &ndash; Unsigned Add-In message" title="Security &ndash; Unsigned Add-In message" src="/assets/image_bbb595.jpg" border="0" /></a><br /></p>

<p></center></p>

<p>In text format, this reads:</p>

<pre>
[Window Title]
Security - Unsigned Add-In

[Main Instruction]
The publisher of this add-in could not be verified. What do you want to do?

[Content]
Name:       External Tool
Publisher:  Unknown Publisher
Location:   C:\a\lib\revit\2017\SDK\Samples\RvtSamples\CS\bin\Debug\RvtSamples.dll
Issuer:     None
Date:       2016-04-20 15:28:45

Make sure that this add-in comes from a trusted source.

[Always Load] [Load Once] [Do Not Load]

[Footer]
What are the risks?
</pre>

<p>I selected 'Always Load' for this one.</p>

<h4><a name="6"></a>RvtSamples DLL and TXT Should be Together</h4>

<p>Once RvtSamples itself is properly loaded, it reads its input text file <code>RvtSamples.txt</code>, which specifies the locations of all the external commands to add to the RvtSamples toolbox.</p>

<p>On my first attempt, the RvtSamples external application is loaded and is unable to locate its input text file:</p>

<p><center></p>

<p><a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e16897883301bb08e4b632970d-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883301bb08e4b632970d img-responsive" style="width: 366px; " alt="RvtSamples.txt not found" title="RvtSamples.txt not found" src="/assets/image_4fffbf.jpg" /></a><br /></p>

<p></center></p>

<p>We need to explicitly set up the path to it, or, alternatively and simpler, modify the RvtSamples.dll output location to the RvtSamples <code>CS</code> root folder, so they are both in the same directory; then it will be found automatically.</p>

<p>Obviously, the add-in manifest needs to be updated to that path as well, then.</p>

<h4><a name="7"></a>Specifying the Revit SDK Samples Root Path</h4>

<p>With the Boolean test variable <code>testClassName</code> set to true, as described above, every entry in the text file is verified.</p>

<p>Since I have not updated the paths specified in the input text file yet, the very first entry generates an error:</p>

<p><center></p>

<p><a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e16897883301b7c840c4b5970b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883301b7c840c4b5970b img-responsive" style="width: 366px; " alt="Invalid path specified in RvtSamples.txt" title="Invalid path specified in RvtSamples.txt" src="/assets/image_71c11b.jpg" /></a><br /></p>

<p></center></p>

<p>So let's edit those paths.</p>

<p>In RvtSamples.txt, I replaced all occurrences of <code>C:\Revit Copernicus SDK\Samples\</code> by my SDK samples root folder <code>C:\a\lib\revit\2017\SDK\Samples\</code>.</p>

<p>187 occurrences were replaced.</p>

<h4><a name="8"></a>Correcting Errors in Individual SDK Sample Entries</h4>

<p>Restart debugging.</p>

<p>This time, we get further.</p>

<p>The VB.NET sample DeleteObject generates an error in line 63.</p>

<p><center></p>

<p><a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e16897883301bb08e4b682970d-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883301bb08e4b682970d img-responsive" style="width: 366px; " alt="DeleteObject VB.NET sample error" title="DeleteObject VB.NET sample error" src="/assets/image_a08648.jpg" /></a><br /></p>

<p></center></p>

<p>That one, and several other VB.NET samples as well, have always caused some strange issues for me, as I already <a href="http://thebuildingcoder.typepad.com/blog/2013/04/compiling-the-revit-2014-sdk.html#9">pointed out in previous releases</a>.</p>

<!--- Its path is specified as `...\DeleteObject\CS\bin\Debug\DeleteObject.dll`, but for some reason, all the VB.NET samples end up in a differnt location on my system, as I already [pointed out in previous releases](http://thebuildingcoder.typepad.com/blog/2013/04/compiling-the-revit-2014-sdk.html#9). -->

<p>I ignored that one, and other VB.NET issues as well.</p>

<h4><a name="9"></a>PlacementOptions Description Line is Missing</h4>

<p>The issue that Dan pointed out about a missing description for the PlacementOptions sample is reported rather cryptically like this:</p>

<p><center></p>

<p><a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e16897883301bb08e4b6af970d-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883301bb08e4b6af970d img-responsive" style="width: 366px; " alt="Missing description for the PlacementOptions sample" title="Missing description for the PlacementOptions sample" src="/assets/image_0f1715.jpg" /></a><br /></p>

<p></center></p>

<p>I added the description line and restarted the debugger.</p>

<h4><a name="10"></a>The Five FabricationPartLayout External Commands</h4>

<p>Skipping a few more VB.NET errors, the FabricationPartLayout sample apparently has a wrong external command name:</p>

<p><center></p>

<p><a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e16897883301b7c840c4fc970b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883301b7c840c4fc970b img-responsive" style="width: 366px; " alt="Invalid external command name for the FabricationPartLayout sample" title="Invalid external command name for the FabricationPartLayout sample" src="/assets/image_fd7a3a.jpg" /></a><br /></p>

<p></center></p>

<p>Not only that, but it also has its <code>CopyLocal</code> flag set to <code>true</code> on the Revit API assemblies, thus creating local copies of them all.</p>

<p>This is not recommended, quite the contrary, so I filed an issue REVIT-90156 [API SDK Sample: FabricationPartLayout Revit API assemblies copy local is true] with the development team to have it fixed.</p>

<p>I also noticed another important omission.</p>

<p>The FabricationPartLayout sample defines five different external commands:</p>

<ul>
<li>ConvertToFabrication</li>
<li>FabricationPartLayout</li>
<li>OptimizeStraights</li>
<li>RenumberingPart</li>
<li>StretchAndFit</li>
</ul>

<pre>
C:\...\FabricationPartLayout\CS &gt; grep class.*IExtern *.cs
ConvertToFabrication.cs:   public class ConvertToFabrication : IExtern...
FabricationPartLayout.cs:   public class FabricationPartLayout : IExte...
OptimizeStraights.cs:   public class OptimizeStraights : IExternalCommand
RenumberingPart.cs:   public class RenumberingPart : IExternalCommand
StretchAndFit.cs:   public class StretchAndFit : IExternalCommand
</pre>

<p>All five should be added to RvtSamples.txt. e.g. like this:</p>

<pre>
MEP
FabricationPartLayout: ConvertToFabrication
Fabrication Part sample layout
LargeImage:
Image:
C:\...\FabricationPartLayout\CS\bin\Debug\FabricationPartLayout.dll
Revit.SDK.Samples.FabricationPartLayout.CS.ConvertToFabrication

MEP
FabricationPartLayout: FabricationPartLayout
Fabrication Part sample layout
LargeImage:
Image:
C:\...\FabricationPartLayout\CS\bin\Debug\FabricationPartLayout.dll
Revit.SDK.Samples.FabricationPartLayout.CS.FabricationPartLayout

MEP
FabricationPartLayout: OptimizeStraights
Fabrication Part sample layout
LargeImage:
Image:
C:\...\FabricationPartLayout\CS\bin\Debug\FabricationPartLayout.dll
Revit.SDK.Samples.FabricationPartLayout.CS.OptimizeStraights

MEP
FabricationPartLayout: RenumberingPart
Fabrication Part sample layout
LargeImage:
Image:
C:\...\FabricationPartLayout\CS\bin\Debug\FabricationPartLayout.dll
Revit.SDK.Samples.FabricationPartLayout.CS.RenumberingPart

MEP
FabricationPartLayout: StretchAndFit
Fabrication Part sample layout
LargeImage:
Image:
C:\...\FabricationPartLayout\CS\bin\Debug\FabricationPartLayout.dll
Revit.SDK.Samples.FabricationPartLayout.CS.StretchAndFit
</pre>

<p>I submitted another issue for this, REVIT-90157 [API SDK samples: RvtSamples lists wrong external command for FabricationPartLayout].</p>

<p>Next, the CreateShared DLL is reported as missing:</p>

<p><center></p>

<p><a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e16897883301bb08e4b6ff970d-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883301bb08e4b6ff970d img-responsive" style="width: 366px; " alt="CreateShared missing" title="CreateShared missing" src="/assets/image_9ca3e0.jpg" /></a><br /></p>

<p></center></p>

<p>That is another one of those pesky VB.NET samples, so I'll ignore it.</p>

<h4><a name="11"></a>RvtSamples Loads and RvtSamples.txt is Cleaned Up</h4>

<p>That was it!</p>

<p>RvtSamples loads, and RvtSamples.txt has been cleaned up a bit:</p>

<p><center></p>

<p><a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e16897883301bb08e4b73e970d-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883301bb08e4b73e970d image-full img-responsive" alt="RvtSamples in Revit 2017" title="RvtSamples in Revit 2017" src="/assets/image_46d2b0.jpg" border="0" /></a><br /></p>

<p></center></p>

<!--- /v/C/a/lib/revit/2017/SDK/Samples/RvtSamples/CS/RvtSamples.txt -->

<p>Here is my first final version of
<a href="http://thebuildingcoder.typepad.com/files/rvtsamples-1.txt">RvtSamples.txt</a> for Revit 2017.</p>

<p>If anyone feels like telling us how to fix the VB.NET sample errors, please be my guest.</p>

<p>Oh, and I must not forget to turn off the <code>testClassName</code> flag again, or it will slow down loading tremendously and pop up all those warning messages each time I restart Revit:</p>

<pre class="code">
  <span style="color:blue;">bool</span>&nbsp;testClassName&nbsp;=&nbsp;<span style="color:blue;">false</span>;&nbsp;<span style="color:green;">//&nbsp;jeremy</span>
</pre>

<p>I hope this will save some effort on your part.</p>

<p>Good luck and have fun with the Revit 2017 API!</p>

<p>Lots more to come on this subject!</p>
