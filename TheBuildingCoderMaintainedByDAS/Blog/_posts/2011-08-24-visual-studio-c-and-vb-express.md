---
layout: "post"
title: "Visual Studio, C# and VB Express"
date: "2011-08-24 05:00:00"
author: "Jeremy Tammik"
categories:
  - ".NET"
  - "Debugging"
  - "Getting Started"
  - "Installation"
  - "News"
  - "Settings"
  - "Utilities"
  - "VB"
original_url: "https://thebuildingcoder.typepad.com/blog/2011/08/visual-studio-c-and-vb-express.html "
typepad_basename: "visual-studio-c-and-vb-express"
typepad_status: "Publish"
---

<p>Yesterday Kean mentioned that 

<a href="http://through-the-interface.typepad.com/through_the_interface/2011/08/debugging-autocad-net-projects-using-express-editions.html">
debugging AutoCAD.NET projects using the Visual C# and VB Express editions</a> is

now directly supported by the 

<a href="http://images.autodesk.com/adsk/files/AutoCAD_2010-2012_dotNET_Wizards.zip">
latest version of the AutoCAD .NET Wizard</a>.

<p>That prompted me to have a look and see what the current situation is with Revit add-ins.

<p>Obviously, we poor Revit developers do not have such a sophisticated wizard available ... or so I thought!

<p>I installed 

<a href="http://www.microsoft.com/visualstudio/en-us/products/2010-editions/visual-csharp-express">
Visual C# Express</a> and started my experiment:

<p>I launched the new installation and selected New Project &gt; Visual C#.

<p>My intention was to select 'Class Library' next and manually set up the Revit API references and all that stuff, then move on to explore how to define appropriate debug settings.

<p>However, guess my surprise to see the Revit Add-in option already listed:</p>

<center>

<a style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e168978833014e8ae26094970d-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e168978833014e8ae26094970d image-full" alt="Visual C# Express 2010 New Project dialogue" title="Visual C# Express 2010 New Project dialogue" src="/assets/image_b70f25.jpg" border="0" /></a> <br />

</center>

<p>Apparently, the 

<a href="http://thebuildingcoder.typepad.com/blog/2011/04/visual-studio-add-in-wizards-for-revit-2012.html">
Visual Studio Add-In Wizards for Revit 2012</a> that

I have installed for the full version of Visual Studio are also picked up by Visual C# Express.

<p>Well, that simplifies matters a lot, doesn't it?

<p>There is actually nothing else whatsoever to do.

<p>Debugging is already set up by the add-in wizard, and Revit has been specified as the start-up project, although it is not displayed in the Visual C# Express debug properties:</p>

<center>

<a style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e168978833014e8ae2619d970d-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e168978833014e8ae2619d970d image-full" alt="Visual C# Express debug properties" title="Visual C# Express debug properties" src="/assets/image_2ba97d.jpg" border="0" /></a> <br />

</center>

<p>Simply hitting F5 to start debugging launches Revit, and on opening a project and looking at the Add-Ins tab external tools, I see my new Visual C# Express add-in loaded and raring to go:</p>

<center>

<a style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e168978833015390eec372970b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e168978833015390eec372970b" alt="Visual C# Express add-in loaded" title="Visual C# Express add-in loaded" src="/assets/image_7a65c2.jpg" border="0" /></a> <br />

</center>

<p>After setting a breakpoint using F9 in Visual C# Express and picking the external command in Revit, the breakpoint is hit and full debugging functionality is available in Visual C#:</p>

<center>

<a style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e168978833015390eec422970b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e168978833015390eec422970b image-full" alt="Visual C# Express debugging the Revit add-in" title="Visual C# Express debugging the Revit add-in" src="/assets/image_c3f965.jpg" border="0" /></a> <br />

</center>

<p>Wow.
I expected this exploration and blog post to take an hour or two, but I was done after fifteen minutes.

<p>I am assuming that all of the above remains valid analogously for 

<a href="http://www.microsoft.com/visualstudio/en-us/products/2010-editions/visual-basic-express">
Visual Basic 2010 Express</a> as well.

Please let me know if you can verify this, or have any problems with either that or the C# version.

<p>So the short and sweet message is: all you need to do is install Visual C# or Visual VB Express and the appropriate 

<a href="http://thebuildingcoder.typepad.com/blog/2011/04/visual-studio-add-in-wizards-for-revit-2012.html">
Visual Studio add-in wizard for Revit</a>,

and you will be all set to create stand-alone add-ins and develop and debug them using the Express tools.
I hope you have fun!

<p><strong>Addendum:</strong> Here are some additonal notes on this by Augusto Gon&ccedil;alves of Autodesk Brazil, the original implementer of the Revit add-in wizards:

<ul>
<li>One issue with Express is that the project is initially created under \Temp\, and therefore the add-in manifest file is also configured to use this path. 
Usually on my trainings (with Express), I recommend immediately saving the project after creating it by clicking on 'Save All', selecting a folder, and then updating the path in the add-in manifest file &lt;Assembly&gt; tag.

<li>Also, keep in mind that Express builds the Debug version when you press F5 (or Star Debugging), and the Release version when you select the command in the Build menu.
</ul>

<p>Thank you, Augusto, for pointing this out!
