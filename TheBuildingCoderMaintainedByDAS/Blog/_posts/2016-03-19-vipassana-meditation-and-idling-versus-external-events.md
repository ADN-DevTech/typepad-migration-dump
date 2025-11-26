---
layout: "post"
title: "Vipassana and Idling versus External Events"
date: "2016-03-19 06:00:00"
author: "Jeremy Tammik"
categories:
  - "Discipline"
  - "Events"
  - "External"
  - "Idling"
  - "Philosophy"
  - "SDK Samples"
original_url: "https://thebuildingcoder.typepad.com/blog/2016/03/vipassana-meditation-and-idling-versus-external-events.html "
typepad_basename: "vipassana-meditation-and-idling-versus-external-events"
typepad_status: "Publish"
---

<p>I am leaving for
a ten-day <a href="http://www.sumeru.dhamma.org">Vipassana meditation retreat</a> next
week, so I'll cram in this quick Saturday post
on the <a href="#2">meditation retreat</a>
and <a href="#3">mindfulness</a> before
I have too much to do getting prepared next week.</p>

<p>It includes yet another discussion
of <a href="#4">Idling versus external events for modeless dialogues and dockable panels</a>.</p>

<h4><a name="2"></a>Vipassana Meditation Retreat</h4>

<p><a href="https://en.wikipedia.org/wiki/Vipassan%C4%81">Vipassana</a> means 'seeing'.</p>

<p>In this case, seeing is enhanced by concentration on my own self, my body, my mind, my thoughts.</p>

<p>No talking, no communication whatsoever, no Internet, no mobile devices, no pen and paper, no reading.</p>

<p>No input, no output.</p>

<p>No distraction.</p>

<p>Sitting.</p>

<p>Time and space, listening inwards.</p>

<p>I already participated in the same ten-day course taught by <a href="http://en.wikipedia.org/wiki/S._N._Goenka">S. N. Goenka</a> and organised
by <a href="http://www.dhamma.org">www.dhamma.org</a>
after <a href="http://thebuildingcoder.typepad.com/blog/2009/12/uk-electrical-schedule-sample.html">Christmas 2009</a>
and in <a href="http://thebuildingcoder.typepad.com/blog/2010/01/happy-new-year-2010.html">January 2010</a>.</p>

<p>It was by far the most relaxing thing I have ever done.</p>

<p><a href="http://www.dhamma.org">Dhamma.org</a> organises absolutely identical courses over the entire world, completely free of charge, completely volunteer driven.</p>

<h4><a name="3"></a>Jon Kabat-Zinn Mindfulness</h4>

<p>Talking about meditation and relaxation, I will also
mention <a href="https://en.wikipedia.org/wiki/Jon_Kabat-Zinn">Jon Kabat-Zinn</a>,
who teaches <a href="https://en.wikipedia.org/wiki/Mindfulness_(psychology)">mindfulness</a> and developed
the <a href="https://en.wikipedia.org/wiki/Mindfulness-based_stress_reduction">mindfulness-based stress reduction</a> program
offered by medical centres, hospitals, and health maintenance organizations.</p>

<p>Interestingly, these techniques are also applicable, useful and extremely effective in the context
of <a href="https://en.wikipedia.org/wiki/Mindfulness#Law">law</a>
and <a href="https://en.wikipedia.org/wiki/Mindfulness#Government">government</a>.</p>

<p>He gave a very nice interview for the Swiss television channel <a href="http://www.srf.ch/play/tv">SRF</a> in <em>Sternstunde Philosophie</em>:
<a href="http://www.srf.ch/play/tv/sternstunde-philosophie/video/jon-kabat-zinn-achtsamkeit-die-neue-gluecksformel?id=a5475697-96e6-492c-8a82-dc92e9620581">Jon Kabat-Zinn &ndash; Achtsamkeit &ndash; die neue Gluecksformel</a>?</p>

<p>Before diving into my own internal workings and doing something apparently extremely idle, let's take another look at the Revit API Idling and external events:</p>

<h4><a name="4"></a>Idling versus External Events for Modeless Dialogues and Dockable Panels</h4>

<p>This question was raised by Miroslav Schonauer, Solution Architect for Autodesk Consulting, and answered by Arnošt Löbel, Senior Principal Engineer of the Revit development team.</p>

<p><strong>Miro:</strong> For the first time I need to use a complex Modeless dialog in Revit. Eventually I will be using a RVT-docking dialog, as in the Revit SDK DockableDialogs sample or
the <a href="http://thebuildingcoder.typepad.com/blog/2013/05/a-simpler-dockable-panel-sample.html">simpler dockable panel sample</a>,
but for the time being I'm only prototyping the 'communication' between the dialogue and Revit, so exploring two options: external events vs. the Idling event, as implemented in the ModelessDialog projects in the Revit SDK Samples.</p>

<p>Can you please confirm:</p>

<p><strong>Q1)</strong> As far as just the 'communication' between a modeless dialog and Revit is concerned, there is not really anything that IDockablePaneProvider additionally provides compared to a simple modeless Form, i.e., we still have the 2 options: External Event or Idling Event.</p>

<p>I've done some research on External event vs Idling Event and read Jeremy's analysis
on <a href="http://thebuildingcoder.typepad.com/blog/2013/12/replacing-an-idling-event-handler-by-an-external-event.html">replacing an Idling event handler by an external event</a>.</p>

<p><strong>Q2)</strong> Can you please comment or expand on my conclusions:</p>

<ol type="a">
<li>If one needs to be periodically checking external sources for whatever, an external event is much better than an Idling event.</li>
<li>If one needs just to react on triggering the controls in the modeless dialog, there is no real difference between an external event and the Idling event.</li>
</ol>

<p>My case is (b), but just in case I still intend to use external event, I would finally like to know:</p>

<p><strong>Q3)</strong> Does anyone see ANY potential advantage (in any area) of the Idling event over external events (I hope not ;-)?</p>

<p><strong>Arnošt:</strong> Here are my answers:</p>

<p><strong>A1)</strong> That is correct. There is no extra communication available for docking dialogs. Docking is the advantage, and possibly some focus control (I am guessing).</p>

<p><strong>A2)</strong> In my opinion it is the other way around. I would use Idling for your (a) case and External event for your (b) case &ndash; the case you seem to be focused on. My reasoning is that if I want to periodically, and possibly quite often, check readiness of external data, Idling may be better because it can be more responsive (it has a flag for controlling how fast it should repeat). If, however, I do not expect my external data be updated so often, I would use an external event here too. For the case (b) &ndash; I would definitely use an EE.</p>

<p><strong>A3)</strong> There is no real difference between those two besides the fact I already mentioned &ndash; Idling can be triggered more often (= several times during just one Idling.) Under the hood, though, both IE and EE start in the same routine deep inside Revit &ndash; OnIdling. There one branch serves subscribed External Event, and another servers Idling handlers.</p>

<p>Of course it goes without saying that EEs have the big benefit of not blocking anything when no communication with Revit is not needed at the moment. That was a huge disadvantage in many situations in which Idling had to be used and EE was not available yet. Since application needed to stay subscribed, Revit had to always call the handlers (even though they mostly had nothing to do) and just such calling and crossing the native/managed barrier had significant impact on the performance of the machine (for the processor had always something to do). External Events address this problem.</p>

<h4><a name="2"></a>Picking an Object from a Dockable Panel versus a Modeless Dialogue</h4>

<p><strong>Aaron:</strong> By the way, you cannot call the Selection.PickObject or PickElementsByRectangle methods by clicking a button in DockableDialog with EE or IE, because they trigger an Autodesk.Revit.Exceptions.InvalidOperationException saying, 'The active view is non-graphical and does not support capture of the focus for pick operations'.</p>

<p>When clicking a button in the dockable dialog, the active view will lose focus because the focus is on the dockable dialog, causing the exception to be thrown.</p>

<p><strong>Arnošt:</strong> Naturally, this is not a problem directly related to either EE or IE. A dockable palette could easily experience the same problem when trying to pick something at a time of any other event (or any API context, in fact). I would qualify this as an issue with the picking functions.  It seems to me that the methods should be either extended by allowing the caller to specify a graphic window in which picking is to occur, or at the very least it should realize that the current view is not a graphic one and should thus automatically switch to the previously active graphic view instead.</p>

<p>Many thanks to Miro, Aaron and Arnošt for this clarification!</p>

<p>By the way, numerous previous discussions and a lot more background information and examples on this area are provided by The Building Coder topic group on <a href="http://thebuildingcoder.typepad.com/blog/about-the-author.html#5.28">Idling and external events for modeless access and driving Revit from outside</a>.</p>

<p>Now I wish you a happy relaxing weekend, with both aliveness and idling time:</p>

<p><center></p>

<p><a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e16897883301bb08cb09f3970d-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883301bb08cb09f3970d img-responsive" style="width: 400px; " alt="Idling event" title="Idling event" src="/assets/image_6ba56c.jpg" /></a><br /></p>

<p></center></p>

<p>I hope to get another post in next week before leaving for my meditation retreat:</p>

<p><center></p>

<p><a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e16897883301b8d1b10887970c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883301b8d1b10887970c img-responsive" style="width: 250px; " alt="External sitting event" title="External sitting event" src="/assets/image_d9261f.jpg" /></a><br /></p>

<p></center></p>
