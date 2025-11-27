---
layout: "post"
title: "An entomologist&rsquo;s view of Project Butterfly"
date: "2010-02-10 06:50:00"
author: "Kean Walmsley"
categories:
  - "SaaS"
original_url: "https://www.keanw.com/2010/02/an-entomologists-view-of-project-butterfly.html "
typepad_basename: "an-entomologists-view-of-project-butterfly"
typepad_status: "Publish"
---

<p><em>Thanks to Jim Quanci for helping with the title of this post. My working title was “Pulling the wings off Project Butterfly” (ouch) but that clearly wasn’t going to fly (groan :-).</em></p> <p>A few weeks ago I had a long discussion with Tal Weiss, one of the co-founders of the company Autodesk recently acquired along with the technology that has become <a href="http://labs.autodesk.com/technologies/butterfly/">Project Butterfly</a>. Tal provided me with some very interesting background information as to Butterfly’s architecture, which I thought I’d share with you. Tal tells me that Jonathan Seroussi, who also joined Autodesk with the acquisition, has been the chief driving force behind this architecture.</p> <p><a href="http://en.wikipedia.org/wiki/Software_as_a_service">SaaS</a> applications actually come in various shapes and sizes: on the one hand we have a server-based application such as that delivered by <a href="http://labs.autodesk.com/technologies/trials/">Project Twitch</a>, where the local application is dependent on graphics continually served up by applications hosted in the cloud. This means a pretty meaty infrastructure, both in terms of the compute power needed to host the application and the bandwidth needed to make it responsive. On the other hand we have technologies such as Project Butterfly with an almost diametrically opposed architecture (other than that they’re both applications that require nothing to be installed on the local machine beyond a very lightweight client – in Butterfly’s case the <a href="http://en.wikipedia.org/wiki/Adobe_Flash">Adobe Flash</a> player).</p> <p>So how is Butterfly different? It’s based on a “stateless” architecture, in that almost no work is actually being done on the server during the application’s execution. To understand this, let’s walk through a typical Butterfly session…</p> <ol>
  <li>The user imports a drawing into Butterfly.   <ul>
    <li>The drawing is converted into an intermediate format by a one-time process using <a href="http://autodesk.com/realdwg">RealDWG</a>. </li>
   </ul>
  </li>
  <li>The user is presented with a workspace into that drawing.   <ul>
    <li>The Flash player loads code locally into the client session, most of which doesn’t require regular communication with the cloud. </li>
   </ul>
  </li>
  <li>The user can perform various operations, whether to change the view or edit the contents of the drawing.   <ul>
    <li>Any editing operations are queued up for later application to the original DWG, should the user request it to be exported. </li>
   </ul>
  </li>
  <li>The user can request the contents of the hosted data to be exported to a local DWG file.   <ul>
    <li>The RealDWG process takes the most recently stored (which may be the original) DWG file and applies the editing operations to it, resulting in a full-fidelity DWG representation of the hosted data. </li>
   </ul>
  </li>
 </ol>
 <p>Now there’s clearly more to Butterfly in terms of the collaborative aspects of the system, but hopefully the above breakdown gives some idea of how the system works (and how the collaboration itself might occur).</p> <p>What’s particularly interesting to me is the potential to extend the capabilities of this system. As the client code is executed locally, the application has strong potential to be extended. It’s – in theory – possible to load additional code modules into the local Flash-based client environment which call into the core implementation via a set of strictly-defined interfaces. So an add-on application might add additional buttons to the ribbon and implement new editing operations that then become available to the user. You can expect me to be sharing more on this over the coming months.</p> <p>I asked Tal about some of the technology decisions behind Butterfly’s architecture…</p> <ol>
  <li>Client architecture: Adobe Flash vs. <a href="http://en.wikipedia.org/wiki/Microsoft_Silverlight">Microsoft Silverlight</a>    <ul>
    <li>At the time the original start-up made this decision – and this type of decision can make or break a small company – Flash was clearly the dominant player, and continues to be ubiquitous. </li>
    <li>While the team continues to be happy with this decision, the dependencies on the Flash player itself are reasonably modest: it would not be a huge effort to shift the implementation to competing platform should the technology landscape shift further. </li>
   </ul>
  </li>
  <li>Cloud architecture: <a href="http://en.wikipedia.org/wiki/Amazon_Web_Services">Amazon Web Services</a> vs. <a href="http://en.wikipedia.org/wiki/Google_App_Engine">Google App Engine</a> vs. <a href="http://en.wikipedia.org/wiki/Azure_Services_Platform">Windows Azure</a>    <ul>
    <li>Once again, due to the timing of the initial development, AWS was the logical choice. AWS was actually in Beta in the early days of Visual Tao, and the team were running much of the early builds on their own hosting infrastructure. </li>
    <li>Which highlights an interesting point: there is very little dependency on AWS in the Butterfly architecture. The client modules pulls down vectors via HTTP, and could easily be re-directed to another hosting provider should the need arise (or should the economics shift in favour of another provider or even private hosting). </li>
   </ul>
  </li>
 </ol>
 <p>OK, that’s it for today’s post. A huge thanks to Tal Weiss for taking the time to share this information with this blog’s readership. I’m looking forward to sharing more on the specifics of customizing Project Butterfly as the details become more clear.</p> <p>One final note: be sure to check out <a href="http://autodeskbutterfly.wordpress.com">the official Project Butterfly team blog</a>, when you get the chance…</p>
