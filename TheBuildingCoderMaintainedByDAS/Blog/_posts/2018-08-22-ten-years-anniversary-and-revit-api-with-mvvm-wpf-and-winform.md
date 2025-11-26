---
layout: "post"
title: "Ten Years Anniversary, MVVM, WPF and WinForm"
date: "2018-08-22 05:00:00"
author: "Jeremy Tammik"
categories:
  - ".NET"
  - "Events"
  - "External"
  - "Idling"
  - "Modeless"
  - "SDK Samples"
  - "User Interface"
  - "WPF"
original_url: "https://thebuildingcoder.typepad.com/blog/2018/08/ten-years-anniversary-and-revit-api-with-mvvm-wpf-and-winform.html "
typepad_basename: "ten-years-anniversary-and-revit-api-with-mvvm-wpf-and-winform"
typepad_status: "Publish"
---

<p>Today is The Building Coder's tenth birthday!</p>

<p>The first post was
a <a href="http://thebuildingcoder.typepad.com/blog/2008/08/welcome.html">warm welcome on August 22, 2008</a>.</p>

<p>Very many thanks to the entire community for all your support, interest, comments and above all numerous contributions over the years!</p>

<p>A suitable quote for the day:</p>

<p><center>
<i>Out of clutter, find Simplicity.
<br/>From discord, find Harmony.
<br/>In the middle of difficulty lies Opportunity.
<br/><br/>Albert Einstein &ndash; Three Rules of Work</i>
<br/><br/><a class="asset-img-link"  href="http://thebuildingcoder.typepad.com/.a/6a00e553e168978833022ad3650b76200c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e168978833022ad3650b76200c img-responsive" style="width: 290px; display: block; margin-left: auto; margin-right: auto;" alt="Bumble bee in lotus flower"" title="Bumble bee in lotus flower"" src="/assets/image_675e29.jpg" /></a><br />
</center></p>

<p>Today, let's pick up the recurring topic of accessing the Revit API from a modeless context.</p>

<p>It was discussed fruitfully in some depth in 
the <a href="http://forums.autodesk.com/t5/revit-api-forum/bd-p/160">Revit API discussion forum</a> thread
on <a href="https://forums.autodesk.com/t5/revit-api-forum/revit-api-with-wpf/m-p/8209618">Revit API with WPF</a>.</p>

<p>I'll skip most of the answer, because it is convoluted.</p>

<p>The most important aspect, though, is short and sweet:</p>

<p><a href="http://thebuildingcoder.typepad.com/blog/2015/12/external-event-and-10-year-forum-anniversary.html#2">The Revit API cannot be used in a modeless context</a>.</p>

<p>It can only be used in a valid Revit API context.</p>

<p>A valid Revit API context is provided within the handlers executed by Revit call-backs, and nowhere else.</p>

<p>For instance,
an <a href="http://www.revitapidocs.com/2018/ad99887e-db50-bf8f-e4e6-2fb86082b5fb.htm">external command</a> defines
an <code>Execute</code> methods and registers itself with Revit.</p>

<p>Revit calls that method and supplies a valid Revit API context to it via its <code>ExternalCommandData</code> input argument.</p>

<p>A Windows form can be displayed either using <code>Show</code> or <code>ShowDialog</code>.</p>

<p>If you use <code>Show</code>, it becomes a modeless dialogue and all interaction which it is outside of any valid Revit API context.</p>

<p>If you use <code>ShowDialog</code>, it becomes modal and remains in the same thread that launched it, possibly inheriting a valid Revit API context.</p>

<p>If you happen to be in a modeless context, you can implement a solution to access a valid Revit API context by implementing
an <a href="http://www.revitapidocs.com/2018/05089477-4612-35b2-81a2-89c4f44370ea.htm">external event</a> and calling that.</p>

<p>Similarly to the external command, it also defines an <code>Execute</code> methods and registers itself with Revit.</p>

<p>Revit calls that method and supplies a valid Revit API context to it when it is prompted by an external trigger.</p>

<p>The official approach for implementing an external event is demonstrated by a Revit SDK sample:</p>

<ul>
<li>ModelessDialog/ModelessForm_ExternalEvent</li>
</ul>

<p>It may require some redesign of your application logic to use.</p>

<p>Ryan Singleton added some good real-world advice to this:</p>

<p>To add my experience to what Jeremy said (you need External Events to make your transactions work modelessly), I have built a dockable dialog with WPF that used a lot of external events. Autodesk recommends that you try to use modal dialogs as much as you can instead of modeless. It will make your project much easier.</p>

<p>However, I can tell you that a fully functional suite of tools with external events is possible. But you need to be sure that the added complexity is worth it.</p>

<p>If you are going to have multiple external events, I strongly recommend that you look at the Revit SDK sample for External Events specifically. It shows how to use enums and a handler to swap out external events so that you do not have a sloppy mess of external events in your application.</p>

<p>You can also have a modeless dialog that, when you press a button to start a transaction, will open a modal window (progress bar or some other window), and that window will start the transaction instead. Once done, it closes, and the user feels like they are using a modeless context. A system like that would be much easier to maintain.</p>

<p>I hope this helps.</p>

<p>Here are some other recent threads raising the same question:</p>

<ul>
<li><a href="https://stackoverflow.com/questions/51918495/revit-pick-element-from-winform">Revit Pick element from WinForm</a></li>
<li><a href="https://forums.autodesk.com/t5/revit-api-forum/using-mvvm-amp-wpf-with-transaction-amp-doc-regenerate/m-p/8207716">Using MVVM and WPF with transaction and <code>doc.regenerate</code></a></li>
</ul>

<p>Many other discussions and solutions are listed by The Building Coder in the topic group
on <a href="http://thebuildingcoder.typepad.com/blog/about-the-author.html#5.28">Idling and external events for modeless access and driving Revit from outside</a>.</p>
