---
layout: "post"
title: "Pattern for Semi-Asynchronous Idling API Access"
date: "2010-11-28 17:58:50"
author: "Jeremy Tammik"
categories:
  - ".NET"
  - "2011"
  - "Algorithm"
  - "Events"
  - "News"
  - "Transaction"
  - "User Interface"
original_url: "https://thebuildingcoder.typepad.com/blog/2010/11/pattern-for-semi-asynchronous-idling-api-access.html "
typepad_basename: "pattern-for-semi-asynchronous-idling-api-access"
typepad_status: "Publish"
---

<p>Here is another wonderful contribution from Daren Thomas: 

<a href="http://darenatwork.blogspot.com/2010/11/pattern-for-asynchronously-updating.html">
A Pattern for Asynchronously Updating Revit Documents</a>.

<p>As an attentive reader of this blog, you will certainly remember one of my favourite and most powerful recent projects, the 

<a href="http://thebuildingcoder.typepad.com/blog/2010/07/modeless-loose-connectors.html">
modeless loose connector navigator</a>.

It retrieves and displays a list of unconnected MEP connectors in a modeless dialogue box.
Being modeless, the dialogue is not within the context of a Revit external command Execute method, nor any other Revit API call-back, and thus has no access to the Revit API, which does not permit 

<a href="http://thebuildingcoder.typepad.com/blog/2010/04/asynchronous-api-calls-and-idling.html">
asynchronous access</a>.

Happily, the Idling event provides a workaround for that.

<p>The modeless loose connector navigator demonstrates a solution for handling a very specialised need, accessing the Revit API semi-asynchronously to highlight the elements with loose connectors.

<p>Daren's post generalises this solution, allowing a modeless dialogue to queue up a whole collection of actions to be taken, which can then be picked up and processed by the Idling event handler the next time it becomes active.
A wonderful generic solution, including neat features such as:

<ul>
<li>Use of the generic Queue template class.
<li>Use of the generic Action delegate, cf. <a href="http://geekswithblogs.net/BlackRabbitCoder/archive/2010/09/09/c.net-five-final-little-wonders-that-make-code-better-3.aspx">5. Generic Delegates</a>.
<li>Locking support to protect against simultaneous access to the queue from the modeless dialogue and the Idling event.
<li>Use of .NET =&gt; lambda statements to execute the queued-up tasks.
</ul>

<p>A truly beautiful job, Daren. 
Thank you!
