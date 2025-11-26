---
layout: "post"
title: ".NET Runtime Security Blocks External Command"
date: "2016-04-05 05:00:00"
author: "Jeremy Tammik"
categories:
  - ".NET"
  - "Exchange"
  - "External"
  - "Getting Started"
  - "Installation"
  - "Security"
  - "Settings"
  - "Update"
original_url: "https://thebuildingcoder.typepad.com/blog/2016/04/windows-10-security-blocks-external-command.html "
typepad_basename: "windows-10-security-blocks-external-command"
typepad_status: "Publish"
---

<p>Here is a Revit API add-in installation issue that came up in various forms in the past and was now raised and resolved once again by Tim Burnham in the special context of .NET Runtime 4.0 configuration:</p>

<p><strong>Question:</strong> I have a plugin that has been working fine for all pre-Windows 10 platforms.</p>

<p>Under Win10, the DLL loads into Revit.exe correctly.
The <code>.addin</code> add-in manifest contents are displayed correctly in the Revit UI.
When I issue the command, however, nothing happens.</p>

<p>I trimmed down all dependencies and code to simply display a message box when the command is launched.</p>

<p>Still no luck.</p>

<p>Are there any known Windows 10 issues for Revit plugins, or Security issues that would prevent a command from being executed?</p>

<p>SP2 is installed.</p>

<p>I don’t think Revit 2016 is officially supported on Windows 10, but this is a really important project and Win 10 is the project platform.</p>

<p>Any hints on resolving this?</p>

<p><strong>Answer:</strong> I have a solution for my plugin not running.</p>

<p>It was indeed security related, and apparently a common one with regards to Autodesk plugins since .NET 4.0 was released.</p>

<p>After snooping around the journal files I saw this:</p>

<pre>
Jrn.RibbonEvent "Execute external command:aaa9bd72-930c-4da5-8305-94cde3a1c3ee:CommandRevitMetricsReporter"
' 0:&lt; DBG_WARN: Could not load file or assembly 'file:///C:\Program Files\Autodesk\Revit 2016\RevitMetricsReporter.dll' or one of its dependencies. Operation is not supported. (Exception from HRESULT: 0x80131515): line 188 of AddIn\AddInItem.cpp.
</pre>

<p>The <code>HRESULT</code> error code <code>0x80131515</code> led me to a couple of solutions:</p>

<h4><a name="1"></a>Solution #1</h4>

<p>Add the following to the <code>revit.exe.config</code> file:</p>

<pre>
<span class="blue">&nbsp; &lt;</span><span class="maroon">runtime</span><span class="blue">&gt;</span>
<span class="blue">&nbsp; &nbsp; &lt;</span><span class="maroon">loadFromRemoteSources</span><span class="blue"> </span><span class="red">enabled</span><span class="blue">=</span>&quot;<span class="blue">true</span>&quot;<span class="blue"> /&gt;</span>
<span class="blue">&nbsp; &lt;/</span><span class="maroon">runtime</span><span class="blue">&gt;</span>
</pre>

<p>This will apply to all plugins, so beware!</p>

<h4><a name="2"></a>Solution #2</h4>

<p>Unblock the DLL individually via its Windows properties.</p>

<p><center></p>

<p><a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e16897883301b7c82f061f970b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883301b7c82f061f970b img-responsive" style="width: 423px; " alt="Security Unblock" title="Security Unblock" src="/assets/image_cc1328.jpg" /></a><br /></p>

<p></center></p>

<p>This Block/Unblock property will only show itself if the component was unzipped or downloaded from the web.</p>

<p>Apparently .NET doesn’t distinguish between the two and will provide fewer privileges for the DLL when you attempt to load and run it.</p>

<h4><a name="3"></a>Solution #3</h4>

<p>Another option would be to deploy the add-in via a simple MSI installer package.</p>

<p>DLLs deployed via an installer are not automatically blocked by Windows, unlike simple deployment via a zip file.</p>

<p>I am currently developing an MSI package for this to avoid future headache.</p>

<p>Jeremy adds: Closely related issues arose repeatedly in the past.</p>

<p>I first mentioned it in relation to
the <a href="http://thebuildingcoder.typepad.com/blog/2011/10/revit-add-in-file-load-exception.html">Revit Add-in file load exception</a> loading
the RevitPythonShell on some non-XP machines,
and <a href="http://through-the-interface.typepad.com">Kean Walmsley</a> provided
a comprehensive explanation of it back in 2011
to <a href="http://labs.blogs.com/its_alive_in_the_lab/2011/05/unblock-net.html">unblock ZIP files before installing Plugins of the Month</a> from
Autodesk Labs.</p>

<p><a name="4"></a><strong>Addendum:</strong> For more background information, please refer to the StackOverflow thread on how to <a href="http://stackoverflow.com/questions/17527347/detect-if-a-file-has-been-blocked-by-the-operating-system">detect if a file has been blocked by the operating system</a>.</p>
