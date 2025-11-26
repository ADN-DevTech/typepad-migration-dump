---
layout: "post"
title: "More on Transaction Groups and Assimilation"
date: "2018-11-10 05:00:00"
author: "Jeremy Tammik"
categories:
  - "Events"
  - "External"
  - "Group"
  - "Transaction"
original_url: "https://thebuildingcoder.typepad.com/blog/2018/11/more-on-transaction-groups-and-assimilation.html "
typepad_basename: "more-on-transaction-groups-and-assimilation"
typepad_status: "Publish"
---

<p>This is The Building Coder post number 1700.</p>

<p>It is a great pleasure to return to
the <a href="https://forums.autodesk.com/t5/revit-api-forum/bd-p/160">Revit API discussion forum</a> question on how
to <a href="https://forums.autodesk.com/t5/revit-api-forum/combine-multiple-transactions-into-one-undo/m-p/8393152">combine multiple transactions into one undo</a> and
highlight a comeback and new answer by Arnošt Löbel, ex Senior Principal Engineer of the Revit development team.</p>

<p>The beginning of that thread and Arnošt's previous answers were already presented here in the blog in the discussion
on <a href="https://thebuildingcoder.typepad.com/blog/2015/02/using-transaction-groups.html">using transaction groups</a>.</p>

<p><strong>Question:</strong> Hi Arnost Lobel:</p>

<p>I have read your reply, and looked into the <code>TransactionGroup.Assimilate</code> method in the Revit API help document <code>.chm</code> file.</p>

<p>I found that this method takes no parameters.</p>

<p>What  I want to ask is:</p>

<ol>
<li>How do I know how many transactions, and which ones, are assimilated into group?</li>
<li>What if I start a transaction before starting a group (in the same API context) and then call <code>Assimilate</code>, will this ahead started one also be assimilated into the group?</li>
<li>Another interesting thing that I found is that I can leave the API context (external event or <code>Execute</code> method) with the transaction group still open. See my code:</li>
</ol>

<pre class="code">
&nbsp;&nbsp;<span style="color:blue;">public</span>&nbsp;<span style="color:blue;">void</span>&nbsp;Execute(&nbsp;RvtUiApplication&nbsp;app&nbsp;)
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">if</span>(&nbsp;WpfTarget.Transactions.ContainsKey(&nbsp;<span style="color:blue;">this</span>.GetName()&nbsp;)&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Transaction&nbsp;=&nbsp;WpfTarget.Transactions[GetName()];
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">if</span>(&nbsp;<span style="color:#2b91af;">Transaction</span>&nbsp;==&nbsp;<span style="color:blue;">null</span>&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Transaction&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">Transaction</span>(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;WpfTarget.CmdVars.DbDoc,&nbsp;GetName()&nbsp;);

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;WpfTarget.Transactions[GetName()]&nbsp;=&nbsp;<span style="color:blue;">this</span>.Transaction;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">else</span>
&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;WpfTarget.Transactions.Add(&nbsp;GetName(),&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">Transaction</span>(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;WpfTarget.CmdVars.DbDoc,&nbsp;GetName()&nbsp;)&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;}

&nbsp;&nbsp;&nbsp;&nbsp;WpfTarget.TransGroup.Start();

&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Transaction</span>.Start();

&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Level</span>.Create(&nbsp;WpfTarget.CmdVars.DbDoc,&nbsp;30&nbsp;);

&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Transaction</span>.Commit();

&nbsp;&nbsp;&nbsp;&nbsp;WpfTarget.TransGroup.Assimilate();
&nbsp;&nbsp;}
</pre>

<p>Just ignore the head part of this <code>Execute</code> method, they are for my own WPF window. Notice that before leaving this external event handler context, I called only <code>Assimilated</code>, but not <code>Rollback</code> or <code>Commit</code>.</p>

<p>I ran these codes without debugging, and Revit threw no errors.</p>

<p>Tested in API ver 2018.</p>

<p>If running in VS debugger, single stepping out of this method displays a page like this:</p>

<p><center></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e168978833022ad37945e0200c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e168978833022ad37945e0200c image-full img-responsive" alt="APIMFC.pdb not loaded" title="APIMFC.pdb not loaded" src="/assets/image_666261.jpg" border="0" style="display: block; margin-left: auto; margin-right: auto;" /></a><br /></p>

<p></center></p>

<p>Does it mean that I can 'modelessly' use <code>TransactionGroup</code>?</p>

<p><strong>Answer:</strong> Now this is rather funny, I think. I have found this post accidentally while googling up something else which had my name in it. I was surprised to see a new question to a very old post I had participated in, and even more I was surprised to find the question unanswered. I do not work for Autodesk anymore, but since the transaction API was kind of a favourite of mine when I worked on the API, I kind of feel somehow obligated to answer such questions. I mean &ndash; I would not look for them purposely, of course, but if I come across one, I’ll answer it if I can.</p>

<p>To your questions:</p>

<ol>
<li>You would not know how many transactions would be assimilated in a transaction group unless you keep a count of the committed transactions by yourself (though I do not know why you would need to do that). A transaction groups simply assimilates (when assimilated) all transactions that started and were committed inside the group.</li>
<li>The previous answer sort of also answers your second question &ndash; you cannot start a transaction group while there is a transaction still open. The <code>TransactionGroup.Start</code> method would throw an exception in such a situation. To test whether there is a transaction open, you can use the <code>Document.IsModifiable</code> method.</li>
<li>To your third question &ndash; it should not be possible to leave your command context with either a transaction or transaction group still open. Well, technically you can do that, of course, but if you do, Revit will roll them back for you and you would lose all changes made in your external command. The internal implementation might have changed in the last three years, I suppose, but it used to be the following way: Before an external command was launched, Revit would start an internal transaction group. Then the command was executed and upon returning from it, be that a natural exit or an exception, Revit would test whether there were any transactions or transaction groups open. If there are, Revit would force them to be rolled back and then it would follow with rolling back the internal transaction group which started before the external command was invoked. This is all for safety reasons; Revit is cautious about external commands that forgot to close all their transactions and transaction groups.</li>
</ol>

<p>I hope it makes sense what I wrote.</p>

<p>I also have one comment about your code. One thing that is not quite clear to me is what happens if your container of transactions does not have an entry for <code>this.GetName</code>; The code would proceed to the <code>Else</code> block in which a new transaction is instantiated and added to your container of transactions. However, the member <code>this.Transaction</code> is not updated, which seems strange with respect to the rest of your code. After you leave your if-else block and you start your transaction group, you then call <code>Transaction.start</code> &ndash; which transaction would that be? Do you have a global <code>Transaction</code> instance? I assume you do, because in this particular case your <code>this.Transaction</code> would not be set to the new transaction you instantiated inside the <code>Else</code> block. My guess is that you would start and commit a different transaction (if there was one instantiated before), which would have a different name. Of course, you would not notice that if you successfully assimilated the transaction group, which is probably why that detail escaped you.</p>

<p>Very many thanks to Arnošt for jumping in and helping out again!</p>

<p>I hope you are having a great time and enjoying life post-Revit!</p>
