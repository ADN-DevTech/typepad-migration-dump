---
layout: "post"
title: "The AutoCAD 2013 Core Console"
date: "2012-02-27 05:45:00"
author: "Kean Walmsley"
categories:
  - "AutoCAD"
  - "AutoCAD .NET"
  - "Batch processing"
  - "Concurrent programming"
  - "Core Console"
original_url: "https://www.keanw.com/2012/02/the-autocad-2013-core-console.html "
typepad_basename: "the-autocad-2013-core-console"
typepad_status: "Publish"
---

<p>In the first of this week’s posts on the new, developer-oriented features in AutoCAD 2013, we’re going to take a look at the AutoCAD Core Console. I predict that this one feature alone is going to be worth its weight in gold to many customers and developers.</p>
<p>Picture this: a stripped down version of AutoCAD – but <span style="text-decoration: underline;">only</span> from a UI perspective – that can be executed from a standard Command Prompt. We’re talking about a version with almost all the capabilities of full AutoCAD – with the ability to load certain applications and execute scripts – but launches in a couple of seconds (if you have a slow machine :-) and is absolutely perfect for batch operations.</p>
<p>[Note: you can only run this executable on systems that have AutoCAD 2013 (or one of its verticals) installed – you shouldn’t expect to be able to run it elsewhere.]</p>
<p><a href="http://through-the-interface.typepad.com/.a/6a00d83452464869e201630234753f970d-pi" target="_blank"><img alt="AutoCAD 2013 Core Console" border="0" height="241" src="/assets/image_688035.jpg" style="background-image: none; margin: 20px auto; padding-left: 0px; padding-right: 0px; display: block; float: none; padding-top: 0px; border: 0px;" title="AutoCAD 2013 Core Console" width="474" /></a></p>
<p>Regarding batch operations: <a href="http://labs.autodesk.com/utilities/ADN_plugins/catalog" target="_blank">the version of ScriptPro currently on Autodesk Labs</a> has been pre-enabled to work with the Core Console: all you have to do is specify the path to the executable in the settings.</p>
<p>And as well as being good at performing a set of sequential operations, the Core Console is a great way to get process-level parallelism (that harnesses all those spare cores many of us now have): you can launch multiple instances of the executable to run concurrently (or – for that matter – simply launch one to implement a “background” version of a command that would otherwise hog the editor during a time-consuming operation).</p>
<p>Before we get into what it means to be able to load “certain” applications, let’s talk a little about the background to this tool. It was made possible by a multi-year architectural project known internally as The Big Split: to make AutoCAD truly – and sustainably – cross-platform, we needed a core component that could be compiled for multiple (for now that means two) platforms. The majority of AutoCAD’s capabilities – including the drawing canvas and graphics sub-system – would be contained “inside” (if you think about it in logical – rather than physical – terms) this component.</p>
<p>This component is AcCore.dll on the Windows platform. If you take a look at acad.exe for AutoCAD 2013, for instance, you’ll see it is now about 5.7 MB, with AcCore.dll weighing in at 13.4 MB. Compare this with AutoCAD 2012, where we had a monolithic acad.exe of 17.5 MB.</p>
<p>Subsetting out this core functionality means we can share a common codebase for this OS-independent set of functionality, and expose an appropriate UI to it for whichever platform we’re targetting. For AutoCAD for Windows, this means using some MFC, some WPF, etc., while for AutoCAD for Mac this means using Cocoa.</p>
<p>And for the simplest possible, command-line only UI, we have the 24 KB AcCoreConsole.exe.</p>
<p>So what kinds of application can be loaded into the Core Console? It can certainly load DBX modules – which are coded against ObjectDBX/RealDWG, which is a subset of the “core” capabilities – but it can also load CRX modules and .NET DLLs that have been coded against the new AcCoreMgd.dll (rather than AcMgd.dll).</p>
<p>You can think of CRX modules as ARX modules developed to run against the core functionality: many of your commands – especially if using the command-line and jigs for their user-input – can be ported to CRX modules (or their .NET equivalent). Ideally you’d then have the GUI integration alone in your ARX modules (or .NET DLLs using AcMgd.dll) which then call into the “core” implementations.</p>
<p>This would allow them to be loaded into the Core Console and even in other environments, moving forwards (watch this space for more on that :-).</p>
<p>You can also load AutoLISP files into the Core Console – just as you can execute scripts – you just need to be aware that certain (mostly GUI-oriented) capabilities will not be available to you.</p>
<p>Let’s now take a look at a very concrete use for this tool, along with some code to drive it.</p>
<p>A very common requirement is for some kind of server-resident process publishing DWFs or PDFs from DWGs. Balaji Ramamoorthy, from DevTech India, wrote an elegant batch script that can sit on a server and use the Core Console to generate PDFs from DWGs (actually it’s a couple of batch files and an AutoCAD script, but anyway).</p>
<p>The main batch file sits and watches a particular “in” folder (which would ideally be network-accessible to be of much use). If it’s empty, the batch file waits for 10 seconds before checking again (to avoid unnecessary thrashing). If there’s something in the folder, the main batch file calls a secondary batch file (GenPDF.bat) to process the file(s) and create the results in the “out” folder.</p>
<p>Here’s the main batch file:</p>
<div style="font-family: courier new; background: white; color: black; font-size: 8pt;">
<p style="margin: 0px;"><span style="line-height: 140%;">:: AccoreConsoleDemo for AutoCAD 2013</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">::Make changes to these parameters as per requirement</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">SET ACCOREEXEPATH=&quot;C:\Program Files\Autodesk\AutoCAD 2013 - English\accoreconsole.exe&quot;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">SET DWGINDIR=&quot;C:\Temp\IN&quot;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">SET DWGOUTDIR=&quot;C:\Temp\OUT&quot;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">SET POLLINGTIME=10</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">echo off</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">IF NOT EXIST %DWGINDIR% MD %DWGINDIR%</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">IF NOT EXIST %DWGOUTDIR% MD %DWGOUTDIR%</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">cls</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">:LoopStart</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">SET PDFGENBATFILEPATH=&quot;%~dp0&quot;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">set PDFGENBATFILEPATH=%PDFGENBATFILEPATH:~1,-1%GenPDF.bat</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">for /f &quot;delims=&quot; %%a IN (&#39;dir %DWGINDIR% /b *.dwg&#39;) do call &quot;%PDFGENBATFILEPATH%&quot; %ACCOREEXEPATH% %DWGINDIR% %DWGOUTDIR% &quot;%%a&quot;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">timeout /t %POLLINGTIME% /NOBREAK</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">goto LoopStart</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">:End</span></p>
</div>
<p>Here’s the secondary batch file, <strong>GenPDF.bat</strong>:</p>
<div style="font-family: courier new; background: white; color: black; font-size: 8pt;">
<p style="margin: 0px;"><span style="line-height: 140%;">SET ACCOREEXEPATH=%1</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">IF NOT EXIST &quot;%ACCOREEXEPATH%&quot; goto ACCOREEXENOTFOUND</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">SET DWGINPATH=%2</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">SET DWGOUTPATH=%3</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">SET DWGFILENAME=%4</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">set DWGFILENAME=%DWGFILENAME:~1,-1%</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">set DWGINPATH=%DWGINPATH:~1,-1%\%DWGFILENAME%</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">set DWGOUTPATH=%DWGOUTPATH:~1,-1%\%DWGFILENAME%</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">:: Create the PDF file path</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">SET PDFINPATH=%DWGINPATH:dwg=pdf%</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">SET PDFOUTPATH=%DWGOUTPATH:dwg=pdf%</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">:: Get the script file path</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">SET SCRIPTFILEPATH=&quot;%~dp0&quot;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">set SCRIPTFILEPATH=%SCRIPTFILEPATH:~1,-1%SamplePDFGenScript.scr</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">::Generate the PDF file</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">%ACCOREEXEPATH% /i &quot;%DWGINPATH%&quot; /s &quot;%SCRIPTFILEPATH%&quot; /l en-US</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">move &quot;%DWGINPATH%&quot; &quot;%DWGOUTPATH%&quot;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">move &quot;%PDFINPATH%&quot; &quot;%PDFOUTPATH%&quot;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">goto END</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">:ACCOREEXENOTFOUND</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">echo %ACCOREEXEPATH%</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">echo &quot;Accoreconsole.exe path is incorrect.&quot;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">goto END</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">:DRAWINGNOTFOUND</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">echo %DWGINPATH%</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">echo &quot;Drawing file not found.&quot;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">goto END</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">:END</span></p>
</div>
<p>Which in turn needs an AutoCAD script file, <strong>SamplePDFGenScript.scr</strong>:</p>
<div style="font-family: courier new; background: white; color: black; font-size: 8pt;">
<p style="margin: 0px;"><span style="line-height: 140%;">(setq CurrDwgName (getvar &quot;dwgname&quot;))</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">(setq Fname (substr CurrDwgName 1 (- (strlen CurrDwgName) 4)))</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">(setq name (strcat (getvar &quot;DWGPREFIX&quot;) Fname &quot;.pdf&quot;))</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">;Command:</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">FILEDIA</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">;Enter new value for FILEDIA &lt;1&gt;:</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">0</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">;Command:</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">-PLOT</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">;Detailed plot configuration? [Yes/No] &lt;No&gt;: </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Yes</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">;Enter a layout name or [?] &lt;Model&gt;:</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Model</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">;Enter an output device name or [?] &lt;None&gt;:</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">DWG To PDF.pc3</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">;Enter paper size or [?] &lt;ANSI A (11.00 x 8.50 Inches)&gt;:</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">ANSI A (11.00 x 8.50 Inches)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">;Enter paper units [Inches/Millimeters] &lt;Inches&gt;:</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Inches</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">;Enter drawing orientation [Portrait/Landscape] &lt;Portrait&gt;: </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Landscape</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">;Plot upside down? [Yes/No] &lt;No&gt;:</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">No</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">;Enter plot area [Display/Extents/Limits/View/Window] &lt;Display&gt;: </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Extents</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">;Enter plot scale (Plotted Inches=Drawing Units) or [Fit] &lt;Fit&gt;:</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Fit</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">;Enter plot offset (x,y) or [Center] &lt;0.00,0.00&gt;:</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">;Plot with plot styles? [Yes/No] &lt;Yes&gt;:</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Yes</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">;Enter plot style table name or [?] (enter . for none) &lt;&gt;:</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">.</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">;Plot with lineweights? [Yes/No] &lt;Yes&gt;:</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Yes</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">;Enter shade plot setting [As displayed/legacy Wireframe/legacy Hidden/Visualstyles/Rendered] &lt;As displayed&gt;:</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">;Enter file name &lt;C:\Work\solids-Model.pdf&gt;:</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">!name</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">;Save changes to page setup? Or set shade plot quality? [Yes/No/Quality] &lt;N&gt;:</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">No</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">;Proceed with plot [Yes/No] &lt;Y&gt;:</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Yes</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">;Command:</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">FILEDIA</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">;;;Enter new value for FILEDIA &lt;1&gt;:</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">1 </span></p>
</div>
<p>In the next post we’ll take a look at another very interesting capability of AutoCAD 2013, Dynamic .NET.</p>
