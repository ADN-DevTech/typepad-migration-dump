---
layout: "post"
title: "Status Bar Text"
date: "2011-02-09 05:00:00"
author: "Jeremy Tammik"
categories:
  - ".NET"
  - "Events"
  - "External"
  - "User Interface"
  - "Win32"
original_url: "https://thebuildingcoder.typepad.com/blog/2011/02/status-bar-text.html "
typepad_basename: "status-bar-text"
typepad_status: "Publish"
---

<p>The ski tour season in the alps has opened again, and I went on my first tour of the year last weekend, to the Ramoz hut and the Arosa Rothorn:</p>

<center>

<a style="display: inline;" href="https://picasaweb.google.com/lh/sredir?uname=ruppchrissi&target=ALBUM&id=5570993040937253329&authkey=Gv1sRgCPrZv8zbi-mCXg&invite=CNHl7asN&feat=email" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e1689788330147e270f4d5970b" alt="Ski tour on Arosa Rothorn" title="Ski tour on Arosa Rothorn" src="/assets/image_fd9fab.jpg" border="0" /></a> <br />

</center>

<p>Many thanks to Chris for the 

<a href="https://picasaweb.google.com/lh/sredir?uname=ruppchrissi&target=ALBUM&id=5570993040937253329&authkey=Gv1sRgCPrZv8zbi-mCXg&invite=CNHl7asN&feat=email">
beautiful pictures</a>!

<p>Here is another idea from Rudolf Honke of

<a href="http://www.acadgraph.de">
acadGraph CADstudio GmbH</a>. 

He says:

<p>I previously explained how you can use 

<a href="http://thebuildingcoder.typepad.com/blog/2011/01/subscribing-to-ui-automation-events.html">
UIAutomation event handlers</a> in Revit.

<p>When playing around a bit further with this, I thought about how to <strong>display</strong> the events.

<p>Using good old P/Invoke, you can simply display any text in the Revit status bar (<a href="http://www.pinvoke.net">http://www.pinvoke.net</a> helps a lot):

<pre class="code">
&nbsp; [<span class="teal">DllImport</span>( <span class="maroon">&quot;user32.dll&quot;</span>, 
&nbsp; &nbsp; SetLastError = <span class="blue">true</span>, 
&nbsp; &nbsp; CharSet = <span class="teal">CharSet</span>.Auto )]
&nbsp; <span class="blue">static</span> <span class="blue">extern</span> <span class="blue">int</span> SetWindowText( 
&nbsp; &nbsp; <span class="teal">IntPtr</span> hWnd, 
&nbsp; &nbsp; <span class="blue">string</span> lpString );
&nbsp;
&nbsp; [<span class="teal">DllImport</span>( <span class="maroon">&quot;user32.dll&quot;</span>, 
&nbsp; &nbsp; SetLastError = <span class="blue">true</span> )]
&nbsp; <span class="blue">static</span> <span class="blue">extern</span> <span class="teal">IntPtr</span> FindWindowEx( 
&nbsp; &nbsp; <span class="teal">IntPtr</span> hwndParent, 
&nbsp; &nbsp; <span class="teal">IntPtr</span> hwndChildAfter, 
&nbsp; &nbsp; <span class="blue">string</span> lpszClass, 
&nbsp; &nbsp; <span class="blue">string</span> lpszWindow );
&nbsp;
&nbsp; <span class="blue">public</span> <span class="blue">static</span> <span class="blue">void</span> SetStatusText( <span class="blue">string</span> text )
&nbsp; {
&nbsp; &nbsp; <span class="teal">IntPtr</span> statusBar = FindWindowEx( 
&nbsp; &nbsp; &nbsp; m_mainWndFromHandle, <span class="teal">IntPtr</span>.Zero, 
&nbsp; &nbsp; &nbsp; <span class="maroon">&quot;msctls_statusbar32&quot;</span>, <span class="maroon">&quot;&quot;</span> );
&nbsp;
&nbsp; &nbsp; <span class="blue">if</span>( statusBar != <span class="teal">IntPtr</span>.Zero )
&nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; SetWindowText( statusBar, text );
&nbsp; &nbsp; }
&nbsp; }
</pre>

<p>This is a comfortable way to show the events being fired.
Here is an example of resizing the main Revit window:</p>

<center>

<a style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e1689788330147e270f44d970b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e1689788330147e270f44d970b image-full" alt="Status bar displaying UI Automation event" title="Status bar displaying UI Automation event" src="/assets/image_b6f308.jpg" border="0" /></a> <br />

</center>

<p>Resizing the main Revit window again:</p>

<center>

<a style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e1689788330148c879f10a970c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e1689788330148c879f10a970c image-full" alt="Status bar displaying UI Automation event" title="Status bar displaying UI Automation event" src="/assets/image_8d436f.jpg" border="0" /></a> <br />

</center>

<p>Selecting the Home ribbon tab:</p>

<center>

<a style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e1689788330148c879f08f970c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e1689788330148c879f08f970c image-full" alt="Status bar displaying UI Automation event" title="Status bar displaying UI Automation event" src="/assets/image_2a12cd.jpg" border="0" /></a> <br />

</center>

<p>Selecting the Insert ribbon tab:</p>

<center>

<a style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e1689788330147e270f259970b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e1689788330147e270f259970b image-full" alt="Status bar displaying UI Automation event" title="Status bar displaying UI Automation event" src="/assets/image_97127b.jpg" border="0" /></a> <br />

</center>

<p>Selecting a button on the Annotation ribbon tab:</p>

<center>

<a style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e1689788330148c879ef20970c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e1689788330148c879ef20970c image-full" alt="Status bar displaying UI Automation event" title="Status bar displaying UI Automation event" src="/assets/image_c20749.jpg" border="0" /></a> <br />

</center>

<p>One point to keep in mind is that Revit will overwrite your text as soon as it sees fit, which may be within a few milliseconds, depending on the command you invoked.</p>

<p>Here is another button being selected and the corresponding UI Automation event displayed:</p>

<center>

<a style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e1689788330147e270f039970b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e1689788330147e270f039970b image-full" alt="Status bar displaying UI Automation event" title="Status bar displaying UI Automation event" src="/assets/image_a79224.jpg" border="0" /></a> <br />

</center>

<p>In this case, it is immediately overwritten by Revit:</p>

<center>

<a style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e1689788330148c879ed81970c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e1689788330148c879ed81970c image-full" alt="Status bar displaying UI Automation event" title="Status bar displaying UI Automation event" src="/assets/image_0ba2de.jpg" border="0" /></a> <br />

</center>

<p>Actually, this function could be combined with another function to replace the global variable 'm_mainWndFromHandle':

<pre class="code">
<span class="blue">public</span> <span class="blue">static</span> <span class="blue">void</span> SetStatusText( <span class="blue">string</span> text )
{
&nbsp; <span class="teal">IntPtr</span> mainWindowHandle = <span class="teal">IntPtr</span>.Zero;
&nbsp;
&nbsp; <span class="teal">Process</span>[] processes 
&nbsp; &nbsp; = <span class="teal">Process</span>.GetProcessesByName( <span class="maroon">&quot;Revit&quot;</span> );
&nbsp;
&nbsp; <span class="blue">if</span>( 0 &lt; processes.Length )
&nbsp; {
&nbsp; &nbsp; mainWindowHandle 
&nbsp; &nbsp; &nbsp; = processes[0].MainWindowHandle;
&nbsp;
&nbsp; &nbsp; <span class="teal">IntPtr</span> statusBar = FindWindowEx( 
&nbsp; &nbsp; &nbsp; mainWindowHandle, <span class="teal">IntPtr</span>.Zero, 
&nbsp; &nbsp; &nbsp; <span class="maroon">&quot;msctls_statusbar32&quot;</span>, <span class="maroon">&quot;&quot;</span> );
&nbsp;
&nbsp; &nbsp; <span class="blue">if</span>( statusBar != <span class="teal">IntPtr</span>.Zero )
&nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; SetWindowText( statusBar, text );
&nbsp; &nbsp; }
&nbsp; }
}
</pre>

<p>So, this function could be called without filling the global Revit app handle before.
Or even shorter:

<pre class="code">
<span class="blue">public</span> <span class="blue">static</span> <span class="blue">void</span> SetStatusText( <span class="blue">string</span> text )
{
&nbsp; <span class="teal">Process</span>[] processes 
&nbsp; &nbsp; = <span class="teal">Process</span>.GetProcessesByName( <span class="maroon">&quot;Revit&quot;</span> );
&nbsp;
&nbsp; <span class="blue">if</span>( 0 &lt; processes.Length )
&nbsp; {
&nbsp; &nbsp; <span class="teal">IntPtr</span> statusBar = FindWindowEx( 
&nbsp; &nbsp; &nbsp; processes[0].MainWindowHandle, <span class="teal">IntPtr</span>.Zero, 
&nbsp; &nbsp; &nbsp; <span class="maroon">&quot;msctls_statusbar32&quot;</span>, <span class="maroon">&quot;&quot;</span> );
&nbsp;
&nbsp; &nbsp; <span class="blue">if</span>( statusBar != <span class="teal">IntPtr</span>.Zero )
&nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; SetWindowText( statusBar, text );
&nbsp; &nbsp; }
&nbsp; }
}
</pre>

<p>Think of a situation there more than one instance of Revit is running, RAC and MEP, for example.
Using the original code, the wrong window might be addressed.

<p>Jeremy adds: I went ahead and implemented a minimal new external command CmdStatusBar for The Building Coder samples to demonstrate this.
I actually make use of the GetCurrentProcess method instead, since I am inside the Revit process, like this:

<pre class="code">
<span class="blue">public</span> <span class="blue">static</span> <span class="blue">void</span> SetStatusText( 
&nbsp; <span class="teal">IntPtr</span> mainWindow,
&nbsp; <span class="blue">string</span> text )
{
&nbsp; <span class="teal">IntPtr</span> statusBar = FindWindowEx(
&nbsp; &nbsp; mainWindow, <span class="teal">IntPtr</span>.Zero,
&nbsp; &nbsp; <span class="maroon">&quot;msctls_statusbar32&quot;</span>, <span class="maroon">&quot;&quot;</span> );
&nbsp;
&nbsp; <span class="blue">if</span>( statusBar != <span class="teal">IntPtr</span>.Zero )
&nbsp; {
&nbsp; &nbsp; SetWindowText( statusBar, text );
&nbsp; }
}
&nbsp;
<span class="blue">public</span> <span class="teal">Result</span> Execute(
&nbsp; <span class="teal">ExternalCommandData</span> commandData,
&nbsp; <span class="blue">ref</span> <span class="blue">string</span> message,
&nbsp; <span class="teal">ElementSet</span> elements )
{
&nbsp; <span class="teal">IntPtr</span> revitHandle = System.Diagnostics.<span class="teal">Process</span>
&nbsp; &nbsp; .GetCurrentProcess().MainWindowHandle;
&nbsp;
&nbsp; SetStatusText( revitHandle, <span class="maroon">&quot;Kilroy was here.&quot;</span> );
&nbsp;
&nbsp; <span class="blue">return</span> <span class="teal">Result</span>.Succeeded;
}
</pre>

<p>It works fine.
Here is

<!-- C:\a\doc\revit\blog\zip\bc_11_83.zip -->

<span class="asset  asset-generic at-xid-6a00e553e1689788330147e270ee86970b"><a href="http://thebuildingcoder.typepad.com/files/bc_11_83.zip">version 2011.0.83.0</a></span>

of The Building Coder samples including the complete source code and Visual Studio solution with the new command.
