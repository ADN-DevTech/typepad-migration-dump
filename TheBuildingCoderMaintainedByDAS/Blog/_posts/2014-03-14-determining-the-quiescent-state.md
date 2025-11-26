---
layout: "post"
title: "Determining the Quiescent State"
date: "2014-03-14 06:00:00"
author: "Jeremy Tammik"
categories:
  - "Algorithm"
  - "Events"
  - "External"
  - "Idling"
  - "Modeless"
original_url: "https://thebuildingcoder.typepad.com/blog/2014/03/determining-the-quiescent-state.html "
typepad_basename: "determining-the-quiescent-state"
typepad_status: "Publish"
---

<p>I am still going through my emails after returning from the desert.
Here is a summary of an interesting discussion between Katsuaki Takamizawa, Arno&scaron;t L&ouml;bel, Partha Sarkar, Joe Ye and Miroslav Schonauer on how to determine the quiescent state of Revit for accessing the API from a modeless context, and some recommendations to simply avoid that entirely, since other more efficient and stream-lined interaction models end up being easier to implement and more reliable to use.</p>

<p><strong>Question:</strong> I am using an external event to interact with the Revit API from a modeless dialogue.</p>

<p>Is there any way to find if Revit is in a busy state so that the request won't be executed right away?
I would like to disable the modeless dialogue during such a busy state, so the user can clearly see when the dialogue box is usable.</p>

<p>Are there any events like 'begin/end the idling state' that I can use to achieve this?</p>

<p><strong>Answer:</strong> There is no begin-end Idling state.
There is just the Idling event itself.
External events are executed at the same (literary) time &ndash; that means that during each internal idling event Revit executes all delegates of API Idling event and also all external events that are signalled at the same time.</p>

<p>The recommendation for using external events in modeless dialogues is this:</p>

<ol>
<li>The dialogue acquires an instance of ExternalEvent</li>
<li>It remains enabled until the end user interacts with it, assuming that while the end user is doing something in the modeless dialogue he or she is unlikely working in Revit at the same time.</li>
<li>After the user starts an action in the dialogue, it raises the external event and waits for its handler to be called back. At that time the dialogue's control should be disabled and remain so until the handler has been executed.</li>
<li>When Revit calls the handler back, the handler does what it needs to, depending on the action requested by the dialogue user.</li>
<li>After the handler completes its API calls, its controls are enabled again</li>
<li>Back to step #2</li>
</ol>

<p>This is demonstrated by the

<a href="http://thebuildingcoder.typepad.com/blog/2012/04/idling-enhancements-and-external-events.html">
ModelessDialog ModelessForm_ExternalEvent SDK sample</a>.</p>

<p>Although the Application class provides an IsQuiescent property that one would assume could be used to check if the current application is quiescent, it is unfortunately useless in Revit. It does not do anything. If an application can legally call it, it will return false. If it returns true, it means the caller calls it illegally and may not make any other API calls anyway. IsQuiescent is not thread-safe either.</p>

<p>We already presented a more detailed discussion on the uselessness of the

<a href="http://thebuildingcoder.typepad.com/blog/2013/01/what-i-do-wall-layers-and-open-transactions.html#4">
Application IsQuiescent property</a> in

January 2013.</p>


<p><strong>Response:</strong> Thank you for the explanations.</p>

<p>My modeless dialogue displays an 'Apply' button which modifies the Revit model when pressed.
I would like to enable the button only if the modification may be executed right away.</p>

<p>If the following scenario makes sense (and unless it imposes any issue or side effect), I would like to file a wish list item for a 'begin/end the idling state' event.</p>

<ol>
<li>Disable the 'Apply' button.</li>
<li>The application receives the 'begin idling state' event.</li>
<li>Enable the 'Apply' button.</li>
<li>The user then presses the 'Apply' button. The external event Raise method is called, the event handler is called and Revit model modification performed.</li>
<li>The application receives the 'end idling state' event. Go back to the step 1.</li>
</ol>


<p><strong>Answer:</strong> As said, using Idling is probably not so great an idea for this.</p>

<p>There is no Begin or End event for Idling.
There is only the Idling event itself.
Besides, using Idling would negate the benefits of using External Events.</p>

<p>There is unfortunately no way in Revit API to achieve exactly what you want to do &ndash; that is, having the dialogue controls enabled only and only if Revit is ready to execute external events immediately.</p>

<p>Choosing the best approach depends on what the dialogue actually wants to do with Revit.</p>

<p>In typical scenarios, end users interact with an external modeless dialogue only when they do not do anything else In Revit at that time. That is why using External events works very well for that kind of applications. The dialogue's controls are enabled all the time except for the time between raising an external event and handling it.</p>

<p>You can subscribe to the Idling event and unsubscribe immediately in the event handler the first time it is called. This almost provides the functionality of a 'begin Idling' event. It also avoids the idling handler being called repeatedly, to avoid degrading system performance.</p>

<p>However, this still does not enable any reliable way to get 'Idling Started' and 'Idling Ended' events.</p>

<p>The scenario below will work to get only <i><b>one</b></i> Idling event that is 'quasi-Idling-started', but then you can't do anything further after that single catch &ndash; the problem is that you cannot do the reverse, i.e. cause Idling to <i><b>restart</b></i> again from the Idling event handler, simply because it's not running.</p>

<p>It therefore does not help at all in this situation, which is better addressed by using an external event.
Both the Idling and external events are raised at the same time (assuming the external event has been signalled).</p>

<p>Summary: there is currently no way for external applications to know when their UI should be disabled and when it can be enabled based on the current state of Revit, <i><b>and</b></i> simple and effective alternative approaches are available to eliminate the need for this functionality.</p>

<p>Many thanks to everybody involved for this illuminating discussion and clarification!</p>
