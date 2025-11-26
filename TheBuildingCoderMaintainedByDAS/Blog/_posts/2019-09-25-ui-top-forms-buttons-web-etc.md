---
layout: "post"
title: "UI Top Forms, Buttons, Web, etc."
date: "2019-09-25 05:00:00"
author: "Jeremy Tammik"
categories:
  - ".NET"
  - "Events"
  - "External"
  - "Modeless"
  - "Ribbon"
  - "User Interface"
  - "Win32"
  - "WPF"
original_url: "https://thebuildingcoder.typepad.com/blog/2019/09/ui-top-forms-buttons-web-etc.html "
typepad_basename: "ui-top-forms-buttons-web-etc"
typepad_status: "Publish"
---

<p>Several user interface related topics are being discussed in 
the <a href="http://forums.autodesk.com/t5/revit-api-forum/bd-p/160">Revit API discussion forum</a>:</p>

<ul>
<li><a href="#2">Keep my form on top of Revit!</a></li>
<li><a href="#3">Creating buttons and getting started with an add-in UI</a></li>
<li><a href="#4">Integrating a web-based UI</a></li>
</ul>

<p><center></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e1689788330240a4d6422a200b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e1689788330240a4d6422a200b img-responsive" style="width: 255px; display: block; margin-left: auto; margin-right: auto;" alt="Window handle" title="Window handle" src="/assets/image_59f1a3.jpg" /></a><br /></p>

<p></center></p>

<h4><a name="2"></a> Keep my Form on Top of Revit!</h4>

<p>This topic came up twice in the past few days, in
the <a href="http://forums.autodesk.com/t5/revit-api-forum/bd-p/160">Revit API discussion forum</a> thread
on <a href="https://forums.autodesk.com/t5/revit-api-forum/modeless-dialog-stays-on-top/m-p/9042359">modeless dialog stays on top</a>
and <a href="https://forums.autodesk.com/t5/revit-api-forum/dialog-form-visiblity/m-p/9043308">dialog / form visibility</a>:</p>

<p><strong>Question:</strong> I created an addon that runs along with the normal user use of Revit and allows the user to open certain views by displaying a modeless dialog.</p>

<p>This works well, but I've set the <code>TopMost</code> property of the dialog to <code>True</code>; otherwise, I keep losing the dialog behind other windows.</p>

<p>The issue is, that with this set, the dialog then stays on top and annoyingly covers other windows.</p>

<p>I'd like the window to follow Revit's window, minimise with it and stay the level with it, so when a window covers Revit, it covers the dialog too.</p>

<p><strong>Answer:</strong> This can be easily fixed by making the Revit main window the owner window of your modeless dialogue, and your modeless dialogue a child of the Revit main window.</p>

<p>I implemented the <code>JtWindowHandle</code> class to help achieve this in the past, so you
can <a href="https://www.google.com/search?q=JtWindowHandle&amp;as_sitesearch=thebuildingcoder.typepad.com">search The Building Coder for <code>JtWindowHandle</code></a> to
find a number of solutions.</p>

<p>Please note that the Revit API nowadays provides official access to the main Revit window via
the <a href="https://www.revitapidocs.com/2020/e28d23a9-6814-1e70-9943-1ee852887dae.htm">UIApplication class MainWindowHandle property</a>.</p>

<p>The <code>JtWindowHandle</code> class is defined like this:</p>

<pre class="code">
  <span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
  <span style="color:gray;">///</span><span style="color:green;">&nbsp;Wrapper&nbsp;class&nbsp;for&nbsp;converting&nbsp;</span>
  <span style="color:gray;">///</span><span style="color:green;">&nbsp;IntPtr&nbsp;to&nbsp;IWin32Window.</span>
  <span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;/</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
  <span style="color:blue;">public</span>&nbsp;<span style="color:blue;">class</span>&nbsp;<span style="color:#2b91af;">JtWindowHandle</span>&nbsp;:&nbsp;<span style="color:#2b91af;">IWin32Window</span>
  {
  &nbsp;&nbsp;<span style="color:#2b91af;">IntPtr</span>&nbsp;_hwnd;

  &nbsp;&nbsp;<span style="color:blue;">public</span>&nbsp;JtWindowHandle(&nbsp;<span style="color:#2b91af;">IntPtr</span>&nbsp;h&nbsp;)
  &nbsp;&nbsp;{
  &nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Debug</span>.Assert(&nbsp;<span style="color:#2b91af;">IntPtr</span>.Zero&nbsp;!=&nbsp;h,
  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#a31515;">&quot;expected&nbsp;non-null&nbsp;window&nbsp;handle&quot;</span>&nbsp;);

  &nbsp;&nbsp;&nbsp;&nbsp;_hwnd&nbsp;=&nbsp;h;
  &nbsp;&nbsp;}

  &nbsp;&nbsp;<span style="color:blue;">public</span>&nbsp;<span style="color:#2b91af;">IntPtr</span>&nbsp;Handle
  &nbsp;&nbsp;{
  &nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">get</span>
  &nbsp;&nbsp;&nbsp;&nbsp;{
  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">return</span>&nbsp;_hwnd;
  &nbsp;&nbsp;&nbsp;&nbsp;}
  &nbsp;&nbsp;}
  }
</pre>

<p>You instantiate it from the Revit main window handle like this:</p>

<pre class="code">
  <span style="color:#2b91af;">JtWindowHandle</span>&nbsp;owner_window_handle&nbsp;
    =&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">JtWindowHandle</span>(&nbsp;
      uiapp.MainWindowHandle&nbsp;);
</pre>

<p>Then, you can parent your form with it using either <code>Show</code> or <code>ShowDialog</code> for a modeless or modal form, respectively:</p>

<pre class="code">
  <span style="color:#2b91af;">MyForm</span>&nbsp;form&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">MyForm</span>();

  <span style="color:#2b91af;">DialogResult</span>&nbsp;rslt&nbsp;=&nbsp;form.ShowDialog(&nbsp;
  &nbsp;&nbsp;owner_window_handle&nbsp;);
</pre>

<h4><a name="3"></a> Creating Buttons and Getting Started with an Add-In UI</h4>

<p>Another thread was misunderstood and brought up a bunch of answers on how to get started creating buttons and a user interface in general, even though it was actually asking something completely different,
<a href="https://forums.autodesk.com/t5/revit-api-forum/how-to-modify-revit-button-picture/m-p/9034300">how to modify Revit button picture?</a></p>

<p>Here are the answers, once again, anyway:</p>

<ul>
<li><a href="https://www.bim365.tech">BIM365</a>
on <a href="https://www.bim365.tech/blog/programming-buttons-in-revit">programming buttons in Revit</a></li>
<li>Good explanation by <a href="http://archi-lab.net">archi+lab</a> on how
to <a href="http://archi-lab.net/create-your-own-tab-and-buttons-in-revit">create your own tab and buttons in Revit</a></li>
</ul>

<h4><a name="4"></a> Integrating a Web-Based UI</h4>

<p>Next, Balint raised a discussion on the implementation of 
an <a href="https://forums.autodesk.com/t5/revit-api-forum/external-application-with-web-ui/m-p/9036614">external application with web UI</a>:</p>

<p><strong>Question:</strong> I would like to create an external application which opens a dialog and do some stuff after it.
My dialog would be a WebView or something like that.
Is it possible?
Could someone recommend a good example?
Which direction is worth following?
I searched on the Autodesk AppStore and found BimObject or ProdLib plugins, which have really nice UIs.
Are they using WPF or some web framework?</p>

<p><strong>Answer:</strong> We recently discussed <a href="https://thebuildingcoder.typepad.com/blog/2019/09/scaling-an-add-in-for-a-4k-high-resolution-screen.html#9">WPF versus Winforms</a>.</p>

<p>Even though most of the Revit SDK samples demonstrate use of WinForms, this is hardly recommended anymore nowadays.</p>

<p>The Revit SDK DockableDialogs example is WPF.  It's good in that it shows the various API's necessary to build an external app, but it was not designed to be a great WPF reference.</p>

<p>You can see some other samples <a href="https://duckduckgo.com/?q=revit+api+wpf+sample">searching for 'Revit API WPF sample'</a>.</p>

<p>The Building Coder also shares <a href="https://thebuildingcoder.typepad.com/blog/wpf">some WPF related topics</a>.</p>

<p>Have you checked
out <a href="https://thebuildingcoder.typepad.com/blog/2019/01/room-boundaries-to-csv-and-wpf-template.html#3">Ali Asad's Visual Studio WPF MVVM Revit add-in template</a>?</p>

<p>This StackOverflow thread discusses how to <a href="https://stackoverflow.com/questions/40096793/best-starting-point-for-wpf-revit-add-in">create a WPF Revit add-in starting with the DockableDialogs SDK sample</a>.</p>

<p>Oh, and finally we must not forget the recent solution using
<a href="https://thebuildingcoder.typepad.com/blog/2019/04/set-floor-level-and-use-ipc-for-disentanglement.html">IPC to disentangle and connect with CefSharp</a>.</p>

<p><strong>Response:</strong> I hope I found the solution.
Using <a href="https://www.teamdev.com/dotnetbrowser">DotNetBrowser</a>,
I can integrate a Chromium-based browser into Revit app. I tried it and it works fine.</p>

<p>Many thanks to Balint for sharing this!</p>
