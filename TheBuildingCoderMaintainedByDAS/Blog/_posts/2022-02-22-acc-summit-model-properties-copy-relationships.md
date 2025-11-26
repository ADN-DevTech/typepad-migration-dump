---
layout: "post"
title: "ACC Summit, Model Properties, Copy Relationships"
date: "2022-02-22 05:00:00"
author: "Jeremy Tammik"
categories:
  - "360"
  - "ACC"
  - "BIM"
  - "Element Relationships"
  - "Forge"
  - "Fun"
  - "Open Source"
  - "Photo"
original_url: "https://thebuildingcoder.typepad.com/blog/2022/02/acc-summit-model-properties-copy-relationships.html "
typepad_basename: "acc-summit-model-properties-copy-relationships"
typepad_status: "Publish"
---

<p>We take a look at maintaining relationships between Revit elements when copying, at ACC, the Autodesk Construction Cloud, and its APIs:</p>

<ul>
<li><a href="#2">ACC Model Properties API</a></li>
<li><a href="#3">ACC integration partner summit</a></li>
<li><a href="#4">Maintain relationships copying elements</a></li>
<li><a href="#5">Unsplash with free images</a></li>
<li><a href="#6">Happy Twosday</a></li>
</ul>

<h4><a name="2"></a> ACC Model Properties API</h4>

<p>A new exciting Forge API may prove especially useful to Revit users,
the <a href="https://forge.autodesk.com/blog/bim-360acc-model-properties-api">BIM 360/ACC Model Properties API</a>.</p>

<p>It provides a powerful new tool to query, filter and compare properties of models. </p>

<p>For example, previously, if you were interested in only MEP elements (e.g., pipes and ducts) with a subset of their properties, you were required to download the entire model data and to parse the massive body if BIM data yourself. With this new tool, you can download only the elements and properties that you are interested in.
The actual query operation is performed in the cloud, so you have much less data to download.
This causes a considerable performance gain, especially dealing with large models.
Of course, as a developer, you have more functionality to take advantage of with less coding.
With more and more people interested in analysing models, moving toward a model-based approach, and sharing models saved in Docs, among various disciplines and phases, we foresee a lot of potential use cases using this tool.</p>

<p>For more information, check out the article mentioned above,
the comparison of <a href="https://forge.autodesk.com/blog/model-properties-api-vs-model-derivative-api">Model Properties API versus Model Derivative API</a> and
the other <a href="https://forge.autodesk.com/apis-and-services/autodesk-construction-cloud-acc-apis">Forge community blog posts about ACC APIs</a>.</p>

<h4><a name="3"></a> ACC Integration Partner Summit</h4>

<p>If you are interested in learning more about Autodesk Construction Cloud products and making use of 
the <a href="https://forge.autodesk.com/apis-and-services/autodesk-construction-cloud-acc-apis">Autodesk Construction Cloud (ACC) APIs</a>,
you are invited you to join the <a href="https://autodesk.registration.goldcast.io/events/636f754d-f617-4a4f-8fa9-38108c6f19d7">ACC Integration Partner Summit 2022</a>,
a virtual event on March 17, 2022.</p>

<p>Your Autodesk hosts will be Jim Lynch, SVP &amp; GM of ACS, Josh Cheney, Senior Manager of Strategic Alliances, Jim Gray, Director of Product, ACS Service Infrastructure, and Anna Lazar, Strategic Alliances &amp; Partnerships.</p>

<p>For more information about the event, please refer to the community blog article
on <a href="https://forge.autodesk.com/blog/acs-integration-partner-summit-2022">ACS Integration Partner Summit 2022</a>,
or jump directly to
the <a href="https://autodesk.registration.goldcast.io/events/636f754d-f617-4a4f-8fa9-38108c6f19d7">registration form</a>.</p>

<p><center></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e1689788330282e1459300200b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e1689788330282e1459300200b img-responsive" style="width: 400px; display: block; margin-left: auto; margin-right: auto;" alt="ACC Integration Partner Summit" title="ACC Integration Partner Summit" src="/assets/image_06b224.jpg" /></a><br /></p>

<p></center></p>

<h4><a name="4"></a> Maintain Relationships Copying Elements</h4>

<p>Returning to the pure .NET desktop Revit API, some interesting aspects of maintaining relationships between elements were discussed in
the <a href="http://forums.autodesk.com/t5/revit-api-forum/bd-p/160">Revit API discussion forum</a> thread
on <a href="https://forums.autodesk.com/t5/revit-api-forum/phasecreated-amp-phasedemolished-after-using-copyelements/m-p/10964247">PhaseCreated and PhaseDemolished after using <code>CopyElements</code></a>:</p>

<p><strong>Question:</strong> I'm trying to copy elements from one document into another, which I'm managing fine.
The issue I'm facing is that, despite having the origin document phases present in the destination document (same phase names at least), the copied elements' <code>Phase Created</code> and <code>Phase Demolished</code> do not match the original elements'.</p>

<p>I checked if the ElementIds returned by <code>CopyElements</code> come up in the same order as the given ElementIds, but unfortunately that was not the case, and once the elements are copied, I'm struggling to make a connection between origin and destination so I can match the phases.</p>

<p>I read the discussion
on <a href="https://forums.autodesk.com/t5/revit-api-forum/copying-elements-types-and-parameters/m-p/9542634">copying elements, types and parameters</a>, 
but it didn't help.</p>

<p><strong>Answer 1:</strong> I believe that when you collect a bunch of elements and copy them all together in one single operation, Revit will try to maintain and restore all their mutual relationships in the target database.
Therefore, it might help if you add all possible references these elements have to other source database elements to the set of elements to copy.
These references will include the element instances themselves, their types, phases, levels, views, materials, and whatever other objects you are interested in.
Then you will have to test and see what Revit can do to try to avoid creating duplicates of them in the target database and map them to existing target objects instead.</p>

<p>This behaviour is hinted at in the list
of <a href="https://thebuildingcoder.typepad.com/blog/2011/06/extensible-storage-features.html#7">extensible storage features</a>.</p>

<p><strong>Answer 2:</strong> That’s standard behaviour for the Revit User Interface.
Elements that are pasted into a view do not get their phases from the copied elements.
Instead, they get their phases from the phase of the view they are pasted into.
For example, if the view pasted into has a new construction phase, that’s the phase that the elements will inherit for their created in phase.</p>

<p>It’s possible to check the phases of every element copied, store those in an array, and then apply the phases to the new elements.
I created an add-in that does exactly that and posted the code online:</p>

<p><ul>
<li><a href="https://sites.google.com/site/revitapi123/copy-similar-code">Copy Similar Code</a></p>

<blockquote>
  <p>Here is the C# code for an app I wrote that copies and pastes Revit elements and gives the pasted elements the same phase and workset of the copied elements. </li>
  </ul>
  You will have to do some extra work to deal with the fact that you are pasting into a different document, but that should be fairly easy to do.</p>
</blockquote>

<p><strong>Response:</strong> Thanks for the help, @jeremy.tammik and @sragan.</p>

<p>@sragan I was doing exactly what you suggested but I was getting inconsistent results when comparing the elements from the source model against the destination model. I then tried a few different ways and ended up realising that the <code>CopyElements</code> does return the same order of the given <code>ElementId</code> list, which then solved my problem.</p>

<p>When I collected the elements in my source document, I also collected their <code>Phase Created</code> and <code>Phase Demolished</code> and stored all 3 in a list:</p>

<pre class="code">
  List&lt;KeyValuePair&lt;ElementId, KeyValuePair&lt;string, string&gt;&gt;&gt;()
</pre>

<p>This allowed me to compare the output of <code>CopyElements</code> with that list and apply the settings of each element individually.</p>

<p>Many thanks to Fabio for raising this issue and Steve Ragan for sharing and explaining his effective solution!</p>

<h4><a name="5"></a> Unsplash with Free Images</h4>

<p>I am a fan of open source, the creative commons license, free stuff, good will, sharing and learning together as a community.
Consequently, I share everything I can in public in the hope that it will come in useful for others as well and help make the world a better place.</p>

<p>In a similar vein, a colleague involved in community work pointed
out <a href="https://unsplash.com">Unsplash</a>:</p>

<blockquote>
  <p>Unsplash has over a million free high-resolution photos grouped in popular photo categories.
  All photos are free to download and use under
  the <a href="https://unsplash.com/license">Unsplash License</a>.</p>
</blockquote>

<p><center></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e16897883302942f9aaf4a200c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883302942f9aaf4a200c img-responsive" style="width: 400px; display: block; margin-left: auto; margin-right: auto;" alt="Unsplash" title="Unsplash" src="/assets/image_42b318.jpg" /></a><br /></p>

<p></center></p>

<h4><a name="6"></a> Happy Twosday</h4>

<p>This Tuesday, February 22, 2022, is both a Tuesday and a spectacular, unique Twosday, the date being both a palindrome and an ambigram:</p>

<p><center>
<img src="img/2022-02-22_twosday.png" alt="Twosday" title="Twosday" width="400"/> <!-- 1196 -->
</center></p>

<p>According to the <a href="https://en.wikipedia.org/wiki/ISO_8601">ISO 8601</a> international standard covering the worldwide exchange and communication of date- and time-related data, this is wrong, of course, and should be written the other way around, as 2022-02-22...</p>
