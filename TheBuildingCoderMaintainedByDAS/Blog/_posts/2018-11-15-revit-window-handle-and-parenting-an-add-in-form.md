---
layout: "post"
title: "Revit Window Handle and Parenting an Add-In Form"
date: "2018-11-15 05:00:00"
author: "Jeremy Tammik"
categories:
  - ".NET"
  - "2019"
  - "Migration"
  - "Update"
  - "User Interface"
  - "Win32"
original_url: "https://thebuildingcoder.typepad.com/blog/2018/11/revit-window-handle-and-parenting-an-add-in-form.html "
typepad_basename: "revit-window-handle-and-parenting-an-add-in-form"
typepad_status: "Publish"
---

<p>Access to the Revit main window handle changed in Revit 2019, raising a couple of questions:</p>

<ul>
<li><a href="#2">Making Revit the add-in parent</a> </li>
<li><a href="#3">The Revit 2019 <code>MainWindowHandle</code> API</a> </li>
<li><a href="#4">Docking system and multiple main window explanation</a> </li>
<li><a href="#5">Updating The Building Coder samples</a> </li>
</ul>

<p><center></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e168978833022ad3c03efc200b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e168978833022ad3c03efc200b img-responsive" alt="Shattered window" title="Shattered window" src="/assets/image_9d79ea.jpg" style="display: block; margin-left: auto; margin-right: auto;" /></a><br /></p>

<p style="font-size: 80%; font-style:italic">Shattered window &#169; Benoit Brummer, <a href="https://commons.wikimedia.org/wiki/User:Trougnouf">@Trougnouf</a></p>

<p></center></p>

<h4><a name="2"></a> Making Revit the Add-In Parent</h4>

<p>A question came up in 
the <a href="http://forums.autodesk.com/t5/revit-api-forum/bd-p/160">Revit API discussion forum</a> question
on <a href="https://forums.autodesk.com/t5/revit-api-forum/how-to-get-plugin-ui-to-be-at-the-same-level-as-revit/m-p/8392848">how to get plugin UI to be at the same level as Revit</a> that
has in fact been asked repeatedly in the past.</p>

<p>Some of my past answers can be found by searching the forum for 'jtwindowhandle'.</p>

<p>As of Revit 2019, however, the answer needs to be modified and updated, so let's do so here and now:</p>

<p><strong>Question:</strong> I'm currently setting my plug-in's UI to <code>TopMost</code>.</p>

<p>However, if I minimize Revit, my plugin stays on top.</p>

<p>Is there a way to have my plug-in's UI to match the functionality of Revit?</p>

<p><strong>Answer:</strong> You have to ensure that your control is assigned the Revit main window as parent.</p>

<p>For instance, if you display your form using
the <a href="https://docs.microsoft.com/en-us/dotnet/api/system.windows.forms.form.showdialog">.NET <code>ShowDialog</code> method</a>,
make use of its overload taking an IWin32Window argument:</p>

<ul>
<li><code>ShowDialog(IWin32Window)</code> &ndash; Shows the form as a modal dialog box with the specified owner.</li>
</ul>

<p>This approach was explained back in 2010 in the discussion on setting 
the <a href="https://thebuildingcoder.typepad.com/blog/2010/06/revit-parent-window.html">Revit parent window</a>.</p>

<p>Please note that
the <a href="https://thebuildingcoder.typepad.com/blog/2018/08/whats-new-in-the-revit-20191-api.html#3.1.4">Revit 2019 API provides direct access to the Revit main window handle</a>.</p>

<h4><a name="3"></a> The Revit 2019 MainWindowHandle API</h4>

<p>Here is a brief quote from
the <a href="https://thebuildingcoder.typepad.com/blog/2018/08/whats-new-in-the-revit-20191-api.html">Revit 2019 API news</a>
on the <a href="https://thebuildingcoder.typepad.com/blog/2018/08/whats-new-in-the-revit-20191-api.html#3.1.4">direct access to the Revit main window handle</a>:</p>

<blockquote>
  <p><b>1.4. UI API changes</b></p>
  
  <p><b>1.4.1. Main window handle access</b></p>
  
  <p>Two new properties in the <code>Autodesk.Revit.UI</code> namespace provide access to the handle of the Revit main window:</p>
  
  <ul>
  <li><code>UIApplication.MainWindowHandle</code></li>
  <li><code>UIControlledApplication.MainWindowHandle</code></li>
  </ul>
  
  <p>This handle should be used when displaying modal dialogs and message windows to ensure that they are properly parented.
  Use these properties instead of System.Diagnostics.Process.GetCurrentProcess().MainWindowHandle,
  which is no longer a reliable method for retrieving the main window handle starting with Revit 2019.</p>
</blockquote>

<p>The change was also pointed out by The Building Coder in October 2017 with
a <a href="https://thebuildingcoder.typepad.com/blog/2017/10/modeless-form-keep-revit-focus-and-on-top.html#10">warning that things will change in the next release</a>.</p>

<h4><a name="4"></a> Docking System and Multiple Main Window Explanation</h4>

<p>Revitalizer explains the need for the new property in his notes
on <a href="https://forums.autodesk.com/t5/revit-api-forum/assign-a-name-to-a-string-c-process-getcurrentprocess/m-p/8365316">assigning a name to a string in C# and <code>Process.GetCurrentProcess().MainWindowTitle</code></a>:</p>

<blockquote>
  <p>Due to the new docking system introduced in Revit 2019, both the <code>UIApplication</code> and <code>UIControlledApplication</code> classes now sport a <code>MainWindowHandle</code> property.</p>
  
  <p>It returns an <code>IntPtr</code> window handle that you can <a href="http://pinvoke.net/default.aspx/user32/GetWindowText.html">P/Invoke <code>GetWindowText</code></a> on to retrieve the window caption text.</p>
  
  <p>In Revit 2019, if view windows are pulled off the main window, there may be more than one Revit application window.</p>
  
  <p>If you open views in just one single Revit 2019 window, of course the 2018 code might still function, since it just finds the only one.</p>
</blockquote>

<h4><a name="5"></a> Updating The Building Coder Samples</h4>

<p><a href="https://github.com/jeremytammik/the_building_coder_samples">The Building Coder samples</a> were
still using the now obsolete <code>JtWindowHandle</code> class up
until <a href="https://github.com/jeremytammik/the_building_coder_samples/releases/tag/2019.0.143.9">release 2019.0.143.9</a>.</p>

<p>The update to switch to the new Revit <code>MainWindowHandle</code> property instead was prompted
by <a href="https://github.com/jeremytammik/the_building_coder_samples/issues/8">issue #8</a> about
a keyboard shortcut problem with <code>CmdPressKeys</code> in Revit 2019.</p>

<p>The code in CmdPressKeys.cs was still retrieving the Revit main window handle via a call to <code>GetCurrentProcess</code>:</p>

<pre class="code">
    IntPtr revitHandle = System.Diagnostics.Process
     .GetCurrentProcess().MainWindowHandle;
</pre>

<p>I modified it to use <code>UiApplication MainWindowHandle</code> instead and removed the use of <code>JtWindowHandle</code>
in <a href="https://github.com/jeremytammik/the_building_coder_samples/releases/tag/2019.0.143.10">release 2019.0.143.10</a>.</p>

<p>Look at the modifications made to the modules CmdPlaceFamilyInstance.cs, CmdPressKeys.cs and JtWindowHandle.cs in
the <a href="https://github.com/jeremytammik/the_building_coder_samples/compare/2019.0.143.9...2019.0.143.10">diff between the two versions</a>.</p>
