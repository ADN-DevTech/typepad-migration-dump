---
layout: "post"
title: "Handle Your Own Exceptions"
date: "2013-10-09 05:00:00"
author: "Jeremy Tammik"
categories:
  - ".NET"
  - "Debugging"
  - "Failure"
  - "Getting Started"
  - "WPF"
original_url: "https://thebuildingcoder.typepad.com/blog/2013/10/handle-your-own-exceptions.html "
typepad_basename: "handle-your-own-exceptions"
typepad_status: "Publish"
---

<p>I was under the impression that it should be hard for an add-in to crash Revit.</p>

<p>Therefore, it was a surprise yesterday to receive an add-in displaying a form and instructions to reliably cause Revit to terminate unexpectedly with just two or three clicks.</p>

<p>The reason is simple, however, as we shall see below, and underlines once again how hard it is to properly implement and handle any modeless activity within a Revit add-in.

<p>Important aspects were discussed in the recommendation to

<a href="http://thebuildingcoder.typepad.com/blog/2012/01/avoid-idling.html">
avoid Idling</a> and

numerous other places.</p>

<p>Yesterday, an experienced Revit add-in developer provided this sample add-in causing an unrecoverable error using read-only transaction mode and making no database access at all.</p>

<p>It does however display a modeless form like this:</p>

<center>

<a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e168978833019affe17055970b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e168978833019affe17055970b" alt="Simple sample form" title="Simple sample form" src="/assets/image_bbd70f.jpg" border="0" /></a><br />

</center>

<p>An unrecoverable error in Revit can be triggered by simply:</p>

<ul>
<li>Launching the add-in external command.</li>
<li>Clicking on 'First'.</li>
<li>Clicking on 'b***'.</li>
<li>Hitting the up arrow keyboard key.</li>
</ul>

<p>Running it outside the Visual Studio debugger causes an unrecoverable error:</p>

<center>

<a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e168978833019affe1e817970d-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e168978833019affe1e817970d" alt="Unrecoverable error" title="Unrecoverable error" src="/assets/image_e35419.jpg" border="0" /></a><br />

</center>

<p>If Revit is running within the Visual Studio debugger, the following detailed reason for the crash is listed (copy and paste somewhere or view source to see truncated lines in full:</p>

<pre>
System.InvalidOperationException was unhandled
  HResult=-2146233079
  Message='System.Windows.Documents.Hyperlink' is not a Visual or Visual3D.
  Source=PresentationCore
  StackTrace:
    at MS.Internal.Media.VisualTreeUtils.AsVisual(DependencyObject element, Visual& visual, Visual3D& visual3D)
    at System.Windows.Media.VisualTreeHelper.GetParent(DependencyObject reference)
    at System.Windows.Controls.TreeViewItem.AllowHandleKeyEvent(FocusNavigationDirection direction)
    at System.Windows.Controls.TreeViewItem.HandleUpDownKey(Boolean up, KeyEventArgs e)
    at System.Windows.Controls.TreeViewItem.OnKeyDown(KeyEventArgs e)
    at System.Windows.RoutedEventArgs.InvokeHandler(Delegate handler, Object target)

    . . .

    at System.Windows.Threading.Dispatcher.LegacyInvokeImpl(DispatcherPriority priority, TimeSpan timeout, Delegate method, Object args, Int32 numArgs)
    at MS.Win32.HwndSubclass.SubclassWndProc(IntPtr hwnd, Int32 msg, IntPtr wParam, IntPtr lParam)
  InnerException:
</pre>

<p>I initially assumed that the Revit API framework should have prevented this error from causing any issues with the main Revit process and reported the issue to the development team, who provided some very illuminating clarification:</p>

<p>This add-in is running inside Revit's process space and did not handle an exception, so Revit's own built-in uncaught exception handler took over.
This seems to be working as best as it can &ndash; the uncaught exception will eventually unwind to end the process anyway.</p>

<p>Add-ins should manage their own exceptions to provide a more user-friendly response and avoid this situation.</p>

<p>Of course, Revit does protect itself from badly behaved add-ins.</p>

<p>It can only do so when it has any knowledge of them, though, for example when the add-in is called via events, commands, or interfaces.
In all those cases the exception is caught and swallowed, with the possibility of disabling some part of the add-in and/or notifying the user.</p>

<p>However, if an add-in launches its own thread and does illegal things outside of Revit's knowledge, there's nothing we can do to stop it.</p>

<p>Furthermore, Revit does indeed protect the user in that any modified document in this scenario causes a prompt to save it.
When retesting the steps listed above inside a modified project document, not running within the debugger, Revit's built-in unrecoverable error handler politely prompts me to save my changes:</p>

<center>

<a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e168978833019affe1798e970c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e168978833019affe1798e970c" alt="Prompt to save changes" title="Prompt to save changes" src="/assets/image_797538.jpg" border="0" /></a><br />

</center>

<p>So, to summarise, please make sure you handle any exceptions that your add-in might cause.</p>

<p>Revit does a very good job of encapsulating the add-in functionality.</p>

<p>Within every valid Revit API context, any exceptions you cause are caught and reported by Revit without affecting the main thread.
Still, you should probably handle them yourself to provide a more graceful report to the user.</p>

<p>Outside of the Revit API context, your exceptions cannot be treated any different from any other completely unexpected situation and will cause the Revit error handler to be triggered as explained above, which will presumably seriously irritate your users.
</p>

<p>
<a href="http://thebuildingcoder.typepad.com/blog/2013/10/handle-your-own-exceptions-and-edit-slab-boundaries.html">
Continued...</a>
</p>
