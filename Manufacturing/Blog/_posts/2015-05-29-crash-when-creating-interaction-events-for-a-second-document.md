---
layout: "post"
title: "Crash when creating interaction events for a second document"
date: "2015-05-29 14:45:09"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "Inventor"
original_url: "https://adndevblog.typepad.com/manufacturing/2015/05/crash-when-creating-interaction-events-for-a-second-document.html "
typepad_basename: "crash-when-creating-interaction-events-for-a-second-document"
typepad_status: "Publish"
---

<p>By&#0160;<a href="http://adndevblog.typepad.com/manufacturing/adam-nagy.html" target="_self">Adam Nagy</a></p>
<p>This is something a <strong>.NET</strong> developer ran into and I thought could be worth sharing. Spoiler alert: the gist of the article is to free objects you do not need anymore. :)</p>
<p>If you have a global variable that is assigned a newly created <strong>InteractionEvents&#0160;</strong>(or another object), then it won&#39;t be freed until you assign that something else: either a new <strong>InteractionEvents</strong> object or <strong>Nothing</strong>. When it gets freed, only then it will try to unsubscribe from the events the given object provides.</p>
<p>In our case we create an&#0160;<strong>InteractionEvents</strong> object, start the interaction, finish the interaction, then close the document and open a new one. Now we start a new interaction, at which point our global variable is still pointing at the old <strong>InteractionEvents</strong> object which by now is completely invalid, since even the document that it was started in is closed now:</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d11eeb4f970c-pi" style="display: inline;"><img alt="InteractionEvents" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b8d11eeb4f970c image-full img-responsive" src="/assets/image_2b423c.jpg" title="InteractionEvents" /></a></p>
<p>When we assign the new <strong>InteractionEvents</strong> object to <strong>oInteraction</strong>, only then it will try to unsubscribe from the previous object&#39;s events and that&#39;s when things go wrong. In theory it should not bring down the system, but in theory we should not hang on to invalid objects either. :)<br />The &quot;Basic Selection Using Interaction Events API Sample&quot; in the help file is also showing how to use <strong>InteractionEvents</strong>&#0160;and is also assigning <strong>Nothing</strong> to the variables once the interaction is finished:</p>
<pre>Public Function Pick(filter As SelectionFilterEnum) As Object
    &#39; Initialize flag.
    bStillSelecting = True

    &#39; Create an InteractionEvents object.
    Set oInteractEvents = ThisApplication.CommandManager.CreateInteractionEvents

    &#39; etc...

    &#39; Stop the InteractionEvents object.
    oInteractEvents.Stop

    &#39; Clean up.
<strong>    Set oSelectEvents = Nothing
    Set oInteractEvents = Nothing</strong>
End Function
</pre>
<p>Best thing is to follow the same practice in your code: assign <strong>Nothing</strong> to objects once you don&#39;t need them anymore.</p>
