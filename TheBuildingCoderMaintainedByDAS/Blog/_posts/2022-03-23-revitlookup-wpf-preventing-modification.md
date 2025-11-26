---
layout: "post"
title: "RevitLookup, WPF, Preventing Modification"
date: "2022-03-23 06:00:00"
author: "Jeremy Tammik"
categories:
  - "Data Access"
  - "Modeless"
  - "RevitLookup"
  - "Selection"
  - "Update"
  - "WPF"
original_url: "https://thebuildingcoder.typepad.com/blog/2022/03/revitlookup-wpf-preventing-modification.html "
typepad_basename: "revitlookup-wpf-preventing-modification"
typepad_status: "Publish"
---

<p>Some notes on different approaches to prevent modification of certain elements and the latest news on RevitLookup:</p>

<ul>
<li><a href="#2">Prevent modification</a></li>
<li><a href="#3">RevitLookup updates</a></li>
<li><a href="#4">RevitLookupWpf</a></li>
<li><a href="#5">Pilcrow</a></li>
</ul>

<h4><a name="2"></a> Prevent Modification</h4>

<p>Revit is a design tool and as such targeted at people editing the BIM.</p>

<p>In many situations, orders of magnitude more people might need to view is or query certain properties.</p>

<p>For instance, a couple of dozen people might design an airport, thousands might build it, and millions might end up using it, wandering around and requiring a floor plan or other navigation support to do so effectively.</p>

<p>For such situations, you would use a viewer or other simplified or read-only access to certain aspects, subsets or properties of the BIM.</p>

<p>For read-only access to a model, you might want to consider using a pure viewer instead, first and foremost something globally accessible like the Forge platform.</p>

<p>I <a href="https://thebuildingcoder.typepad.com/blog/2022/03/drilling-holes-in-beams-and-other-projects.html#3">recently mentioned</a> my
current <a href="https://github.com/jeremytammik/RvtLock3r">RvtLock3r</a> side project
that ensures that certain specified parameter values have not been modified.</p>

<p>Two other recent queries on how to prevent model modification came up in
the <a href="http://forums.autodesk.com/t5/revit-api-forum/bd-p/160">Revit API discussion forum</a> threads
on how to <a href="https://forums.autodesk.com/t5/revit-api-forum/hide-or-block-explode-import-instance-cads/m-p/10829603">hide or block explode import instance</a>
and how to <a href="https://forums.autodesk.com/t5/revit-api-forum/make-instance-and-its-parameters-not-editable/m-p/9821547">make instance and its parameters NOT editable</a>:</p>

<p><strong>Question :</strong> At some point at creating a drawing, we have a status of instances in the drawing, that the user/architect is NOT allowed anymore to change ANY parameters in the instance and is NOT allowed to move the instance at all.</p>

<p>I found out that you can pin the elements and make them not selectable anymore:</p>

<pre class="code">
  element.Pinned = true;

  var options =  SelectionUIOptions.GetSelectionUIOptions();

  options.SelectPinned = false;
</pre>

<p>Also, I could add a Trigger to observe a custom parameter or a built in parameter.</p>

<p>BUT (!) the user can still check that checkbox of the "Select pinned elements" again and I cannot find an Event that observes these user action.</p>

<p>The <code>DocumentChanged</code> event is not fired, when I click on that checkbox.</p>

<p>Is there any other option to make instances not editable anymore? Or can I observe the action of the user, when he/she is checking or unchecking the Selection option?</p>

<p>My workaround would be, that if the user would want to change the element again and it has this kind of (locked) status set, to observe this and undo the action. But this is such a dirty workaround I would not be happy with it.</p>

<p>I would have to set a trigger for everything in this element. And in my opinion, this is a bad solution.</p>

<p><strong>Answer:</strong> Pining the element to prevent change is probably the wrong starting point, since that mechanism is mostly useful for datum elements and links, i.e., the items you more often than not want to prevent movement of.
The 'select pinned items in view' button I believe is more of a user preference than a document change, so I assume that setting would be per user and therefore not stored with the Document.
Additionally, if I pin something, I can still change its parameters.</p>

<p>There is a slow filter 'SelectableInViewFilter' which can be used to determine if something can be selected in view.
So, one approach could be to store the ElementIds of your protected items and run this filter for a view to ensure nothing is missing. This also seems more trouble than it is worth since it is a slow filter, and you'd probably have to run it quite often.</p>

<p>The best approach I know of for change control is to put the protected elements on mirrored worksets:</p>

<ul>
<li>GridsAndLevels &rarr; GridsAndLevelsReadOnly</li>
<li>PrimaryStructure &rarr; PrimaryStructureReadonly etc.</li>
</ul>

<p>The 'ReadOnly' worksets above would have an owner that isn't a normal user, i.e., you create a fictitious user and have that user take ownership of the worksets.
This was easier in the past when you could change the Revit username.
Depending on your product licensing arrangement, this may still be a viable option for you.
I think this is the best approach, since element ownership through worksets is an inherent aspect of Revit and so you can rely on the infrastructure already in place.</p>

<p>An alternative approach is to implement an <code>IUpdater</code>, mark the element with a parameter value or extensible storage and then, if that Element is changed, post a failure with the only option being undo.</p>

<p><strong>Response:</strong> Thank you for your suggestions &nbsp; :-)</p>

<p>I looked at
the video <a href="https://youtu.be/0KS2nwHWZRQ">BIMedge &ndash; Admin User to Lock out Worksets</a> that 
shows the Workset stuff that you mean &ndash; the thing is, that I can easily change my account into "Admin" and so every other user could do it like that (change username, etc.).</p>

<p>Another problem I had was that the workset option was greyed out in my document.
But I think I should first make sure what a "Workset" is and how to create and use it.</p>

<p>I already implemented an Updater Trigger for a user parameter (as mentioned in my question).
And then dynamically add another trigger which catches EVERY change of the element (with Element.GetChangeTypeAny) and undo it, if its status is set to our defined "not editable anymore" status.</p>

<p>I just thought that this solution is not nice and thought there must be a nicer and cleaner solution to this.</p>

<p>But it seems there isn't.</p>

<p>So I will implement it like I already planned.</p>

<p>Thanks a lot for your help!!</p>

<p><strong>Answer:</strong> That's fine; just bear in mind there is no absolute way of preventing a user from editing something, not even with the API. An add-in only enforces something if it is running.</p>

<p>I think there has to be a level of trust that the users will not act inappropriately; additionally, there needs to be a model audit / checking process.</p>

<p><strong>Response:</strong> Yes, you are right.
If the user wants to commit to our database I would check, if he changed something without the add-in.
The commit can only be done via the add-in.</p>

<p>Users are clever, they always find a way to "cheat" ... :-)</p>

<p><strong>Answer 2:</strong> The cleanest solution would be to prevent the user selecting the element.
So, program your own "SelectionChanged Event" as described in the thread
on <a href="https://forums.autodesk.com/t5/revit-api-forum/element-selection-changed-event-implementation-struggles/m-p/9229523">element selection changed event implementation struggles</a>.</p>

<p>In the "eventhandler", or, in my solution, the <code>IsCommandAvailable</code> method, find the selected elements in the active document <code>Selection</code> <code>GetElementIds</code>.
If your target Ids are present in the selection, remove them from the selection, effectively blocking the user from selecting the element.</p>

<p>I don't know how time-consuming the removal of id's will be with a large number of target Ids.</p>

<p><strong>Response:</strong> Thanks for your answer.</p>

<p>The thing is, the user would then see no more information about the element.</p>

<p>So, I think that my first suggestion with pinning the elements and making them not selectable is a very bad idea.</p>

<p>Later: so, I have talked to the support.</p>

<p>Atm there is no way to "lock" an instance in the model...</p>

<p>Unfortunately, the workaround(s) are the only ways to get such behaviour...</p>

<p>Thanks a lot again for your help!</p>

<p>Many thanks to Diana, Richard and Fair59 for the interesting discussion and solutions!</p>

<h4><a name="3"></a> RevitLookup Updates</h4>

<p>Several updates were added
to <a href="https://github.com/jeremytammik/RevitLookup">RevitLookup</a>
especially by very active contributor <a href="https://github.com/chuongmep">Chuong Ho</a>:</p>

<ul>
<li>Fixed problem with sending a print job <a href="https://github.com/jeremytammik/RevitLookup/pull/133">#133</a>:
A problem when user snoops to <code>PrintManager</code> and invokes the <code>SubmitPrint</code> method</li>
<li>Minimize and maximize window support <a href="https://github.com/jeremytammik/RevitLookup/pull/134">#134</a>:
Enable user to maximize all forms to full screen; useful to display long string data and to expand the form to full size for review </li>
<li>Minor UI changes <a href="https://github.com/jeremytammik/RevitLookup/pull/135">#135</a></li>
</ul>

<p><center></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e1689788330282e14b0ce8200b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e1689788330282e14b0ce8200b image-full img-responsive" alt="RevitLookup maximise button" title="RevitLookup maximise button" src="/assets/image_1cda04.jpg" border="0" style="display: block; margin-left: auto; margin-right: auto;" /></a><br /></p>

<p></center></p>

<p>Many thanks to
<a href="https://github.com/chuongmep">Chuong Ho</a> for these useful enhancements and to 
<a href="https://github.com/Nice3point">Roman 'Nice3point'</a> for managing the releases.</p>

<h4><a name="4"></a> RevitLookup in WPF</h4>

<p>Chuong Ho or <a href="https://chuongmep.com">Hồ Văn Chương</a> is very active in other areas as well.</p>

<p>Among many other projects, he is working
on <a href="https://github.com/weianweigan/RevitLookupWpf">RevitLookupWpf</a>:</p>

<blockquote>
  <p>Hello everyone,
  Today, I would like to introduce to you
  the <a href="https://github.com/weianweigan/RevitLookupWpf">RevitLookupWpf project</a>.
  This project deeply supports Revit API by me and a Chinese friend is dududu collaborate;
  this is one of our efforts to enhance development improvement in the construction industry.
  With Revitlookupwpf, you can look into every detail clearly inside and execute it directly, and many more exciting things we are working on.
  We are creating features that have never been available before and we are open to accepting any ideas and pull requests.
  This is opensource project and everyone can view it.</p>
</blockquote>

<p><center></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e168978833027880726430200d-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e168978833027880726430200d image-full img-responsive" alt="RevitLookupWpf" title="RevitLookupWpf" src="/assets/image_746700.jpg" border="0" style="display: block; margin-left: auto; margin-right: auto;" /></a><br /></p>

<p></center></p>

<p><strong>Question:</strong> How is this different than the original <a href="https://github.com/jeremytammik/RevitLookup">RevitLookup</a>?
Am I missing something?
Is the difference that yours is using WPF instead of WinForms?</p>

<p><strong>Answer:</strong>  Yes, and more.</p>

<p>Basically it is not the same.
I will write a document explaining all after complete feature.
It is an example, you can see description of methods and properties from code, and it is linked with <a href="https://www.revitapidocs.com">revitapidocs</a>.</p>

<p><strong>Question:</strong>  Is it a Modeless window now? Would be a great thing.</p>

<p><strong>Answer:</strong>  Yes, it works in a modeless window.</p>

<p><strong>Question:</strong>  Great! Is it free to download?</p>

<p><strong>Answer:</strong>  Sure, it free and you can download file <code>msi</code> to install from
the <a href="https://github.com/weianweigan/RevitLookupWpf/releases/latest">latest release</a>.</p>

<p>Many thanks to Chuong Ho for this great initiative, and the best of luck with it going forward!</p>

<!---
<center>
<img src="img/chuongmep.tiff" alt="Hồ Văn Chương" title="Hồ Văn Chương" width="256"/>
</center>
--->

<h4><a name="5"></a> Pilcrow</h4>

<p>I enjoy using words that I never used before, and sometimes discover new ones that I never even heard of.</p>

<p>An opportunity presented itself in
the <a href="http://forums.autodesk.com/t5/revit-api-forum/bd-p/160">Revit API discussion forum</a> thread
on <a href="https://forums.autodesk.com/t5/revit-api-forum/autocad-to-revit-text-importing/m-p/10822619">AutoCAD to Revit text importing</a>:</p>

<blockquote>
  <p>I just performed an Internet search for &para; and learned that it is named <a href="https://en.wikipedia.org/wiki/Pilcrow">Pilcrow</a>.
  Never heard that name before.</p>
</blockquote>
