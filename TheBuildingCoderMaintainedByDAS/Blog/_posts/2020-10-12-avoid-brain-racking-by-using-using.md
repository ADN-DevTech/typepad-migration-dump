---
layout: "post"
title: "Avoid Brain Racking by Using Using"
date: "2020-10-12 05:00:00"
author: "Jeremy Tammik"
categories:
  - ".NET"
  - "Debugging"
  - "Music"
  - "Philosophy"
  - "Transaction"
original_url: "https://thebuildingcoder.typepad.com/blog/2020/10/avoid-brain-racking-by-using-using.html "
typepad_basename: "avoid-brain-racking-by-using-using"
typepad_status: "Publish"
---

<p>You should always keep things simple (I think).
The opposite can lead to racking your brain.
In one case at least, that can easily be avoided by using a <code>using</code> statement:</p>

<ul>
<li><a href="#2">Avoid brain racking by using <code>using</code></a></li>
<li><a href="#3">On the VS operation unspecified error</a></li>
<li><a href="#4">Native sons</a></li>
</ul>

<h4><a name="2"></a> Avoid Brain Racking by Using Using</h4>

<p>Here is a quick note on the importance
of <a href="http://thebuildingcoder.typepad.com/blog/2012/04/using-using-automagically-disposes-and-rolls-back.html">using <code>using</code> to automagically dispose and roll back</a> transactions
and transaction groups, and the grief and brain racking that can easily ensue from failing to do so:</p>

<p><strong>Question:</strong> For certain users, my add-in is hard crashing Revit to the desktop.</p>

<p>No exceptions, just everything closes.</p>

<p>I’ve got the journal files and I wanted to get some help seeing if there is anything to learn from them.</p>

<p><strong>Answer:</strong> Sorry to hear about the hard crashes. And on certain users' systems, to boot.</p>

<p>Is the problem reproducible?</p>

<p>Does it only happen on certain specific machines?</p>

<p>Do you have any idea at all where the problem might be?</p>

<p>Does it send error reports to Autodesk?</p>

<p>What context can cause it?</p>

<p>Do you think it is due to your add-in, or Revit, or both?</p>

<p><strong>Response:</strong> Thanks for the follow up.</p>

<p>I was able to get a hold of a customer’s computer that was seeing the error and track it down.</p>

<p>I had a <code>TransactionGroup</code> that was started but never completed &ndash; I did not call <code>Assimilate</code> or <code>Rollback</code>.</p>

<p>Adding the missing rollback fixed the error.</p>

<p>I did a review of all my other code and confirmed I closed all my other transaction groups and transactions after I opened them.</p>

<p>Relying on the destructor to call rollback obviously is a bad idea and was not intentional.</p>

<p>The relevant lines from the journal file look like this:</p>

<pre>
' 7:< REGEN_DOC_CONTEXT_INFO: Changing wrong atom in regeneration
' 7:< REGEN_DOC_CONTEXT_INFO: Document is changed in regeneration while it is not supposed to
</pre>

<p>The transaction wasn’t closed and Revit tried to regenerate.</p>

<p><strong>Answer:</strong> Congratulations on solving the issue!</p>

<p>Thank you very much for your research and useful explanation.  </p>

<p>Were your transaction groups originally encapsulated in a <code>using</code> statement?</p>

<p>The Building Coder recommends doing so and claims that will obviate the need to call <code>Assimilate</code> or <code>Rollback</code>:
<a href="http://thebuildingcoder.typepad.com/blog/2012/04/using-using-automagically-disposes-and-rolls-back.html">Using Using Automagically Disposes and Rolls Back</a>.</p>

<p>I would be a rather shocked if that would turn out to be false.</p>

<p>So, I hope your transaction group causing the problem was not enclosed in a <code>using</code> statement.</p>

<p>If that is the case, I would strongly recommend ALWAYS enclosing transactions and transaction groups in <code>using</code> statements.</p>

<p>That would (I still hope) reliably avoid this problem forever.</p>

<p>Can't wait to hear your response on this...</p>

<p><strong>Response:</strong> Brilliant!</p>

<p>No, I was not using <code>using</code> in my code.</p>

<p>Makes perfect sense to do so to prevent errors like mine.</p>

<p>I’m going to work on adding that to my code. There are 170 places I use a transaction, so it’s going to be a little work.</p>

<p>Your blog is a great resource. I use it quite regularly when trying to figure things out.</p>

<p>I don’t have any interesting code snippet to share.
It’s basically a <code>TransactionGroup</code> with an <code>Assimilate</code> call in an <code>IF</code> statement and a forgotten <code>Rollback</code> in the <code>ELSE</code>.</p>

<p><center></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e1689788330263e96dd58a200b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e1689788330263e96dd58a200b img-responsive" style="width: 300px; display: block; margin-left: auto; margin-right: auto;" alt="Brain rack" title="Brain rack" src="/assets/image_f50a40.jpg" /></a><br /></p>

<p></center></p>

<h4><a name="3"></a> On the VS Operation Unspecified Error</h4>

<p>A small note on the Visual Studio 'Operation could not be Completed, Unspecified Error':</p>

<p>You may have notices building Revit add-ins within Visual Studio getting strange build failures and a message along the lines of <em>21>Error: The operation could not be completed. Unspecified error</em>.</p>

<p>This seems to be an issue where the Visual Studio IDE gets itself into a strange state and the project in question has not been loaded properly for some reason.</p>

<p>I found it related to switching project configurations before the build or even switching solutions.</p>

<p>It could also have to do with do a merge in git outside of the IDE that causes projects to change and reload.</p>

<p>Anyway, without being clear the exact steps, when I do see the error, I found one of the following clears things up:</p>

<ul>
<li>Close and reopen the solution in question.</li>
<li>Fully exit and restart Visual Studio.</li>
</ul>

<p>Could it have something to do with switching header files, or switching from debug to release?</p>

<p>It may, but I have seen it just simple closing one solution and loading another.</p>

<p>I also got it in a standalone C# solution with some C++ projects.
It usually appears while trying to compile the C++ projects, which are a interop dependency for the C# projects &ndash; one theory is that somehow the C# binary locks the C++ one and doesn’t let msbuild query it for… god knows what, since interop is done at runtime afaik (never seen this error on rebuild, only build ).
And yes, only VS restart cures it; fortunately, we rarely touch the C++ projects, so I just ignored this error so far. </p>

<p>Jeremy adds: I never touch C++ code, only C# Revit add-ins, and I see this message now and then as well.
I just restart VS or even reboot Windows (under Parallels on Mac) and all is well.</p>

<h4><a name="4"></a> Native Sons</h4>

<p>John Candela pays tribute to Native Americans for Monday,
<a href="https://en.wikipedia.org/wiki/Indigenous_Peoples%27_Day">Indigenous People's Day</a>, with
his track <a href="https://soundcloud.com/jdcandela/native-sons">Native Sons</a>:</p>

<p><center></p>

<iframe width="500" height="100" scrolling="no" frameborder="no" allow="autoplay" src="https://w.soundcloud.com/player/?url=https%3A//api.soundcloud.com/tracks/908029177&color=%23ff5500&auto_play=false&hide_related=false&show_comments=true&show_user=true&show_reposts=false&show_teaser=true"></iframe>

<div style="font-size: 10px; color: #cccccc;line-break: anywhere;word-break: normal;overflow: hidden;white-space: nowrap;text-overflow: ellipsis; font-family: Interstate,Lucida Grande,Lucida Sans Unicode,Lucida Sans,Garuda,Verdana,Tahoma,sans-serif;font-weight: 100;"><a href="https://soundcloud.com/jdcandela" title="JC" target="_blank" style="color: #cccccc; text-decoration: none;">JC</a> · <a href="https://soundcloud.com/jdcandela/native-sons" title="Native Sons" target="_blank" style="color: #cccccc; text-decoration: none;">Native Sons</a></div>

<p></center></p>
