---
layout: "post"
title: "Nesting instincts: getting more out of transactions inside AutoCAD using .NET"
date: "2009-01-26 14:19:04"
author: "Kean Walmsley"
categories:
  - "AutoCAD"
  - "AutoCAD .NET"
  - "Transactions"
original_url: "https://www.keanw.com/2009/01/nesting-instincts-getting-more-out-of-transactions-inside-autocad-using-net.html "
typepad_basename: "nesting-instincts-getting-more-out-of-transactions-inside-autocad-using-net"
typepad_status: "Publish"
---

<p>I received this question by email last week:</p>
<blockquote>
<p><em>Is it ever required to use more than one transaction per program?</em></p></blockquote>
<p>The simple answer is that you mostly only need one transaction active per command: you shouldn&#39;t leave a transaction active outside of a command, as this is likely to cause problems at some point, and within your own command one transaction is typically enough to do what you want.</p>
<p>That said, the transaction mechanism inside AutoCAD has some pretty cool nesting capabilities that make it very flexible and a great way to manage sets of database operation and to roll them back should they no longer be necessary.</p>
<p>In this post we&#39;re going to look at some code which uses some nested transactions and allows the user to choose whether to commit or abort each one.</p>
<p>Here&#39;s what the ObjectARX Developer&#39;s Guide says about &quot;Nesting Transactions&quot;:</p>
<blockquote>
<p><em>Transactions can be nested—that is, you can start one transaction inside another and end or abort the recent transaction. The transaction manager maintains transactions in a stack, with the most recent transaction at the top of the stack. When you start a new transaction using AcTransactionManager::startTransaction(), the new transaction is added to the top of the stack and a pointer to it is returned (an instance of AcTransaction). When someone calls AcTransactionManager::endTransaction() or AcTransactionManager::abortTransaction(), the transaction at the top of the stack is ended or aborted.</em></p>
<p><em>When object pointers are obtained from object IDs, they are always associated with the most recent transaction. You can obtain the recent transaction using AcTransactionManager::topTransaction(), then use AcTransaction::getObject() or AcTransactionManager::getObject() to obtain a pointer to an object. The transaction manager automatically associates the object pointers obtained with the recent transaction. You can use AcTransaction::getObject() only with the most recent transaction. </em></p>
<p><em>When nested transactions are started, the object pointers obtained in the outer containing transactions are also available for operation in the innermost transaction. If the recent transaction is aborted, all the operations done on all the objects (associated with either this transaction or the containing ones) since the beginning of the recent transaction are canceled and the objects are rolled back to the state at the beginning of the recent transaction. The object pointers obtained in the recent transaction cease to be valid once it&#39;s aborted. </em></p>
<p><em>If the innermost transaction is ended successfully by calling AcTransactionManager::endTransaction(), the objects whose pointers were obtained in this transaction become associated with the containing transaction and are available for operation. This process is continued until the outermost (first) transaction is ended, at which time modifications on all the objects are committed. If the outermost transaction is aborted, all the operations on all the objects are canceled and nothing is committed.</em></p></blockquote>
<p>While this was written for ObjectARX, it also applies (with some minor changes to the terminology) to the world of .NET programming in AutoCAD.</p>
<p>Here&#39;s the idea... we&#39;re going to have an outer transaction, within which we&#39;re going to create a grid of filled circles (hatches, in fact). We&#39;re then going to nest a number of transactions (for now only at one level deep, although there&#39;s nothing stopping us nesting more deeply), which modify the colour of various items in the grid.</p>
<p>Here&#39;s another way of looking at the transaction, hierarchically:</p>
<ul>
<li>[Outer] Create a grid of filled circles 
<ul>
<li>[Nested 1] Make all these circles red 
<li>[Nested 2] Make alternate lines yellow 
<li>[Nested 3] Draw a magenta sine wave on the grid </li>
</li></li></ul>
</li>
</ul>
<p>At the end of each transaction, the user will be given the choice to commit or abort it.</p>
<p>Here&#39;s the C# code:</p>
<div style="FONT-SIZE: 8pt; BACKGROUND: white; COLOR: black; FONT-FAMILY: courier new">
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue; LINE-HEIGHT: 140%">using</span><span style="LINE-HEIGHT: 140%"> System;</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue; LINE-HEIGHT: 140%">using</span><span style="LINE-HEIGHT: 140%"> Autodesk.AutoCAD.ApplicationServices;</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue; LINE-HEIGHT: 140%">using</span><span style="LINE-HEIGHT: 140%"> Autodesk.AutoCAD.DatabaseServices;</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue; LINE-HEIGHT: 140%">using</span><span style="LINE-HEIGHT: 140%"> Autodesk.AutoCAD.EditorInput;</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue; LINE-HEIGHT: 140%">using</span><span style="LINE-HEIGHT: 140%"> Autodesk.AutoCAD.Runtime;</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue; LINE-HEIGHT: 140%">using</span><span style="LINE-HEIGHT: 140%"> Autodesk.AutoCAD.Geometry;</span></p><br />
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue; LINE-HEIGHT: 140%">namespace</span><span style="LINE-HEIGHT: 140%"> Transactionality</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">{</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; </span><span style="COLOR: blue; LINE-HEIGHT: 140%">public</span><span style="LINE-HEIGHT: 140%"> </span><span style="COLOR: blue; LINE-HEIGHT: 140%">class</span><span style="LINE-HEIGHT: 140%"> </span><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">Commands</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; {</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; [</span><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">CommandMethod</span><span style="LINE-HEIGHT: 140%">(</span><span style="COLOR: #a31515; LINE-HEIGHT: 140%">&quot;nt&quot;</span><span style="LINE-HEIGHT: 140%">)]</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; </span><span style="COLOR: blue; LINE-HEIGHT: 140%">public</span><span style="LINE-HEIGHT: 140%"> </span><span style="COLOR: blue; LINE-HEIGHT: 140%">void</span><span style="LINE-HEIGHT: 140%"> NestedTransactions()</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; {</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">Document</span><span style="LINE-HEIGHT: 140%"> doc =</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">Application</span><span style="LINE-HEIGHT: 140%">.DocumentManager.MdiActiveDocument;</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">Database</span><span style="LINE-HEIGHT: 140%"> db = doc.Database;</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">Editor</span><span style="LINE-HEIGHT: 140%"> ed = doc.Editor;</span></p><br />
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">Transaction</span><span style="LINE-HEIGHT: 140%"> tr =</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; db.TransactionManager.StartTransaction();</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="COLOR: blue; LINE-HEIGHT: 140%">using</span><span style="LINE-HEIGHT: 140%"> (tr)</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160; {</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="COLOR: green; LINE-HEIGHT: 140%">// Our outermost transaction starts by creating</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="COLOR: green; LINE-HEIGHT: 140%">// a load of circles, the ids of which are in</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="COLOR: green; LINE-HEIGHT: 140%">// a collection we then pass around</span></p><br />
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">ObjectIdCollection</span><span style="LINE-HEIGHT: 140%"> ids =</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; CreateLotsOfCircles(tr, db, 0.5, 1.2, 30);</span></p><br />
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; tr.TransactionManager.QueueForGraphicsFlush();</span></p><br />
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">Transaction</span><span style="LINE-HEIGHT: 140%"> tr2 =</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; tr.TransactionManager.StartTransaction();</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="COLOR: blue; LINE-HEIGHT: 140%">using</span><span style="LINE-HEIGHT: 140%"> (tr2)</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; {</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="COLOR: green; LINE-HEIGHT: 140%">// Our first nested transaction turns the</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="COLOR: green; LINE-HEIGHT: 140%">// circles red</span></p><br />
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; ChangeColor(tr2, ids, 1);</span></p><br />
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; tr2.TransactionManager.QueueForGraphicsFlush();</span></p><br />
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; CommitOrAbort(</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; ed,</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; tr2,</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="COLOR: #a31515; LINE-HEIGHT: 140%">&quot;transaction to make the circles red&quot;</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; );</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; }</span></p><br />
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">Transaction</span><span style="LINE-HEIGHT: 140%"> tr3 =</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; tr.TransactionManager.StartTransaction();</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="COLOR: blue; LINE-HEIGHT: 140%">using</span><span style="LINE-HEIGHT: 140%"> (tr3)</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; {</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="COLOR: green; LINE-HEIGHT: 140%">// Our second nested transaction turns the</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="COLOR: green; LINE-HEIGHT: 140%">// circles yellow</span></p><br />
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">ObjectIdCollection</span><span style="LINE-HEIGHT: 140%"> alternates =</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="COLOR: blue; LINE-HEIGHT: 140%">new</span><span style="LINE-HEIGHT: 140%"> </span><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">ObjectIdCollection</span><span style="LINE-HEIGHT: 140%">();</span></p><br />
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="COLOR: blue; LINE-HEIGHT: 140%">for</span><span style="LINE-HEIGHT: 140%"> (</span><span style="COLOR: blue; LINE-HEIGHT: 140%">int</span><span style="LINE-HEIGHT: 140%"> i = 0; i &lt; ids.Count; i++)</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; {</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="COLOR: blue; LINE-HEIGHT: 140%">if</span><span style="LINE-HEIGHT: 140%"> (i % 2 == 0)</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; alternates.Add(ids[i]);</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; }</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; ChangeColor(tr3, alternates, 2);</span></p><br />
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; tr3.TransactionManager.QueueForGraphicsFlush();</span></p><br />
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; CommitOrAbort(</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; ed,</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; tr3,</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="COLOR: #a31515; LINE-HEIGHT: 140%">&quot;transaction to make alternate circles yellow&quot;</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; );</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; }</span></p><br />
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">Transaction</span><span style="LINE-HEIGHT: 140%"> tr4 =</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; tr.TransactionManager.StartTransaction();</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="COLOR: blue; LINE-HEIGHT: 140%">using</span><span style="LINE-HEIGHT: 140%"> (tr4)</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; {</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="COLOR: green; LINE-HEIGHT: 140%">// Our third nested transaction draws a sine wave</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="COLOR: green; LINE-HEIGHT: 140%">// on the grid of circles</span></p><br />
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; SineWave(tr4, ids, 6);</span></p><br />
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; tr4.TransactionManager.QueueForGraphicsFlush();</span></p><br />
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; CommitOrAbort(</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; ed,</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; tr4,</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="COLOR: #a31515; LINE-HEIGHT: 140%">&quot;transaction to draw a magenta sine wave&quot;</span><span style="LINE-HEIGHT: 140%">);</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; }</span></p><br />
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; CommitOrAbort(</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; ed,</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; tr,</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="COLOR: #a31515; LINE-HEIGHT: 140%">&quot;top-level transaction&quot;</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; );</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160; }</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; }</span></p><br />
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; </span><span style="COLOR: green; LINE-HEIGHT: 140%">// Helper function to handle the user-input around</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; </span><span style="COLOR: green; LINE-HEIGHT: 140%">// the decision to commit/abort a particular</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; </span><span style="COLOR: green; LINE-HEIGHT: 140%">// transaction (and then the actual commit/abort</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; </span><span style="COLOR: green; LINE-HEIGHT: 140%">// operation)</span></p><br />
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; </span><span style="COLOR: blue; LINE-HEIGHT: 140%">private</span><span style="LINE-HEIGHT: 140%"> </span><span style="COLOR: blue; LINE-HEIGHT: 140%">void</span><span style="LINE-HEIGHT: 140%"> CommitOrAbort(</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">Editor</span><span style="LINE-HEIGHT: 140%"> ed,</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">Transaction</span><span style="LINE-HEIGHT: 140%"> tr,</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="COLOR: blue; LINE-HEIGHT: 140%">string</span><span style="LINE-HEIGHT: 140%"> desc</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; )</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; {</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">PromptKeywordOptions</span><span style="LINE-HEIGHT: 140%"> pko =</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="COLOR: blue; LINE-HEIGHT: 140%">new</span><span style="LINE-HEIGHT: 140%"> </span><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">PromptKeywordOptions</span><span style="LINE-HEIGHT: 140%">(</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="COLOR: #a31515; LINE-HEIGHT: 140%">&quot;\nCommit or abort the &quot;</span><span style="LINE-HEIGHT: 140%"> + desc + </span><span style="COLOR: #a31515; LINE-HEIGHT: 140%">&quot;?&quot;</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; );</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160; pko.AllowNone = </span><span style="COLOR: blue; LINE-HEIGHT: 140%">true</span><span style="LINE-HEIGHT: 140%">;</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160; pko.Keywords.Add(</span><span style="COLOR: #a31515; LINE-HEIGHT: 140%">&quot;Commit&quot;</span><span style="LINE-HEIGHT: 140%">);</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160; pko.Keywords.Add(</span><span style="COLOR: #a31515; LINE-HEIGHT: 140%">&quot;Abort&quot;</span><span style="LINE-HEIGHT: 140%">);</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160; pko.Keywords.Default = </span><span style="COLOR: #a31515; LINE-HEIGHT: 140%">&quot;Commit&quot;</span><span style="LINE-HEIGHT: 140%">;</span></p><br />
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">PromptResult</span><span style="LINE-HEIGHT: 140%"> pkr =</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; ed.GetKeywords(pko);</span></p><br />
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="COLOR: blue; LINE-HEIGHT: 140%">if</span><span style="LINE-HEIGHT: 140%"> (pkr.StringResult == </span><span style="COLOR: #a31515; LINE-HEIGHT: 140%">&quot;Abort&quot;</span><span style="LINE-HEIGHT: 140%">)</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160; {</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; tr.Abort();</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160; }</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="COLOR: blue; LINE-HEIGHT: 140%">else</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160; {</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; tr.Commit();</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160; }</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; }</span></p><br />
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; </span><span style="COLOR: green; LINE-HEIGHT: 140%">// Create a grid of filled circles (well, actually</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; </span><span style="COLOR: green; LINE-HEIGHT: 140%">// hatches) based on the information provided</span></p><br />
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; </span><span style="COLOR: blue; LINE-HEIGHT: 140%">private</span><span style="LINE-HEIGHT: 140%"> </span><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">ObjectIdCollection</span><span style="LINE-HEIGHT: 140%"> CreateLotsOfCircles(</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">Transaction</span><span style="LINE-HEIGHT: 140%"> tr,</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">Database</span><span style="LINE-HEIGHT: 140%"> db,</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="COLOR: blue; LINE-HEIGHT: 140%">double</span><span style="LINE-HEIGHT: 140%"> radius,</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="COLOR: blue; LINE-HEIGHT: 140%">double</span><span style="LINE-HEIGHT: 140%"> offset,</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="COLOR: blue; LINE-HEIGHT: 140%">int</span><span style="LINE-HEIGHT: 140%"> numOnSide</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; )</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; {</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">ObjectIdCollection</span><span style="LINE-HEIGHT: 140%"> ids =</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="COLOR: blue; LINE-HEIGHT: 140%">new</span><span style="LINE-HEIGHT: 140%"> </span><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">ObjectIdCollection</span><span style="LINE-HEIGHT: 140%">();</span></p><br />
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">BlockTable</span><span style="LINE-HEIGHT: 140%"> bt =</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; (</span><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">BlockTable</span><span style="LINE-HEIGHT: 140%">)tr.GetObject(</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; db.BlockTableId,</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">OpenMode</span><span style="LINE-HEIGHT: 140%">.ForRead</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; );</span></p><br />
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">BlockTableRecord</span><span style="LINE-HEIGHT: 140%"> btr =</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; (</span><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">BlockTableRecord</span><span style="LINE-HEIGHT: 140%">)tr.GetObject(</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; bt[</span><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">BlockTableRecord</span><span style="LINE-HEIGHT: 140%">.ModelSpace],</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">OpenMode</span><span style="LINE-HEIGHT: 140%">.ForWrite</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; );</span></p><br />
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="COLOR: blue; LINE-HEIGHT: 140%">for</span><span style="LINE-HEIGHT: 140%"> (</span><span style="COLOR: blue; LINE-HEIGHT: 140%">int</span><span style="LINE-HEIGHT: 140%"> i = 0; i &lt; numOnSide; i++)</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160; {</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="COLOR: blue; LINE-HEIGHT: 140%">for</span><span style="LINE-HEIGHT: 140%"> (</span><span style="COLOR: blue; LINE-HEIGHT: 140%">int</span><span style="LINE-HEIGHT: 140%"> j = 0; j &lt; numOnSide; j++)</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; {</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="COLOR: green; LINE-HEIGHT: 140%">// To get a filled circle we&#39;re going to create a</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="COLOR: green; LINE-HEIGHT: 140%">// circle and then hatch it with the &quot;solid&quot; pattern</span></p><br />
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="COLOR: green; LINE-HEIGHT: 140%">// Start with the hatch itself...</span></p><br />
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">Hatch</span><span style="LINE-HEIGHT: 140%"> hat = </span><span style="COLOR: blue; LINE-HEIGHT: 140%">new</span><span style="LINE-HEIGHT: 140%"> </span><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">Hatch</span><span style="LINE-HEIGHT: 140%">();</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; hat.SetDatabaseDefaults();</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; hat.SetHatchPattern(</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">HatchPatternType</span><span style="LINE-HEIGHT: 140%">.PreDefined,</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="COLOR: #a31515; LINE-HEIGHT: 140%">&quot;SOLID&quot;</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; );</span></p><br />
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">ObjectId</span><span style="LINE-HEIGHT: 140%"> id = btr.AppendEntity(hat);</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; tr.AddNewlyCreatedDBObject(hat, </span><span style="COLOR: blue; LINE-HEIGHT: 140%">true</span><span style="LINE-HEIGHT: 140%">);</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; ids.Add(id);</span></p><br />
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="COLOR: green; LINE-HEIGHT: 140%">// Now we create the loop, which we make db-resident</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="COLOR: green; LINE-HEIGHT: 140%">// (appending a transient loop caused problems, so</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="COLOR: green; LINE-HEIGHT: 140%">// we&#39;re going to use the circle and then erase it)</span></p><br />
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">Circle</span><span style="LINE-HEIGHT: 140%"> cir = </span><span style="COLOR: blue; LINE-HEIGHT: 140%">new</span><span style="LINE-HEIGHT: 140%"> </span><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">Circle</span><span style="LINE-HEIGHT: 140%">();</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; cir.Radius = radius;</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; cir.Center =</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="COLOR: blue; LINE-HEIGHT: 140%">new</span><span style="LINE-HEIGHT: 140%"> </span><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">Point3d</span><span style="LINE-HEIGHT: 140%">(i * offset, j * offset, 0);</span></p><br />
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">ObjectId</span><span style="LINE-HEIGHT: 140%"> lid = btr.AppendEntity(cir);</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; tr.AddNewlyCreatedDBObject(cir, </span><span style="COLOR: blue; LINE-HEIGHT: 140%">true</span><span style="LINE-HEIGHT: 140%">);</span></p><br />
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="COLOR: green; LINE-HEIGHT: 140%">// Have the hatch use the loop we created</span></p><br />
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">ObjectIdCollection</span><span style="LINE-HEIGHT: 140%"> loops =</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="COLOR: blue; LINE-HEIGHT: 140%">new</span><span style="LINE-HEIGHT: 140%"> </span><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">ObjectIdCollection</span><span style="LINE-HEIGHT: 140%">();</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; loops.Add(lid);</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; hat.AppendLoop(</span><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">HatchLoopTypes</span><span style="LINE-HEIGHT: 140%">.Default, loops);</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; hat.EvaluateHatch(</span><span style="COLOR: blue; LINE-HEIGHT: 140%">true</span><span style="LINE-HEIGHT: 140%">);</span></p><br />
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="COLOR: green; LINE-HEIGHT: 140%">// Now we erase the loop</span></p><br />
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; cir.Erase();</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; }</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160; }</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="COLOR: blue; LINE-HEIGHT: 140%">return</span><span style="LINE-HEIGHT: 140%"> ids;</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; }</span></p><br />
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; </span><span style="COLOR: green; LINE-HEIGHT: 140%">// Loop through a list of objects and change their colour</span></p><br />
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; </span><span style="COLOR: blue; LINE-HEIGHT: 140%">private</span><span style="LINE-HEIGHT: 140%"> </span><span style="COLOR: blue; LINE-HEIGHT: 140%">void</span><span style="LINE-HEIGHT: 140%"> ChangeColor(</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">Transaction</span><span style="LINE-HEIGHT: 140%"> tr,</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">ObjectIdCollection</span><span style="LINE-HEIGHT: 140%"> ids,</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="COLOR: blue; LINE-HEIGHT: 140%">int</span><span style="LINE-HEIGHT: 140%"> col</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; )</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; {</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="COLOR: blue; LINE-HEIGHT: 140%">foreach</span><span style="LINE-HEIGHT: 140%"> (</span><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">ObjectId</span><span style="LINE-HEIGHT: 140%"> id </span><span style="COLOR: blue; LINE-HEIGHT: 140%">in</span><span style="LINE-HEIGHT: 140%"> ids)</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160; {</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">Entity</span><span style="LINE-HEIGHT: 140%"> ent =</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; tr.GetObject(id, </span><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">OpenMode</span><span style="LINE-HEIGHT: 140%">.ForRead)</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="COLOR: blue; LINE-HEIGHT: 140%">as</span><span style="LINE-HEIGHT: 140%"> </span><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">Entity</span><span style="LINE-HEIGHT: 140%">;</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="COLOR: blue; LINE-HEIGHT: 140%">if</span><span style="LINE-HEIGHT: 140%"> (ent != </span><span style="COLOR: blue; LINE-HEIGHT: 140%">null</span><span style="LINE-HEIGHT: 140%">)</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; {</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="COLOR: blue; LINE-HEIGHT: 140%">if</span><span style="LINE-HEIGHT: 140%"> (!ent.IsWriteEnabled)</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; ent.UpgradeOpen();</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; ent.ColorIndex = col;</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; }</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160; }</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; }</span></p><br />
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; </span><span style="COLOR: green; LINE-HEIGHT: 140%">// Draw a sine wave on a grid of objects</span></p><br />
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; </span><span style="COLOR: blue; LINE-HEIGHT: 140%">private</span><span style="LINE-HEIGHT: 140%"> </span><span style="COLOR: blue; LINE-HEIGHT: 140%">void</span><span style="LINE-HEIGHT: 140%"> SineWave(</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">Transaction</span><span style="LINE-HEIGHT: 140%"> tr,</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">ObjectIdCollection</span><span style="LINE-HEIGHT: 140%"> ids,</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="COLOR: blue; LINE-HEIGHT: 140%">int</span><span style="LINE-HEIGHT: 140%"> col</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; )</span></p><br />
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; {</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="COLOR: green; LINE-HEIGHT: 140%">// Assume that we&#39;re working with a square grid</span></p><br />
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="COLOR: blue; LINE-HEIGHT: 140%">int</span><span style="LINE-HEIGHT: 140%"> numOnSide =</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; (</span><span style="COLOR: blue; LINE-HEIGHT: 140%">int</span><span style="LINE-HEIGHT: 140%">)</span><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">Math</span><span style="LINE-HEIGHT: 140%">.Sqrt(ids.Count);</span></p><br />
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="COLOR: green; LINE-HEIGHT: 140%">// Loop along the x axis</span></p><br />
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="COLOR: blue; LINE-HEIGHT: 140%">for</span><span style="LINE-HEIGHT: 140%"> (</span><span style="COLOR: blue; LINE-HEIGHT: 140%">int</span><span style="LINE-HEIGHT: 140%"> i = 0; i &lt; numOnSide; i++)</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160; {</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="COLOR: green; LINE-HEIGHT: 140%">// Get a result between -1 and 1</span></p><br />
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="COLOR: blue; LINE-HEIGHT: 140%">double</span><span style="LINE-HEIGHT: 140%"> res =</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">Math</span><span style="LINE-HEIGHT: 140%">.Sin(2 * </span><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">Math</span><span style="LINE-HEIGHT: 140%">.PI * i / (numOnSide - 1));</span></p><br />
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="COLOR: green; LINE-HEIGHT: 140%">// Normalise to between 0 and 1</span></p><br />
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; res = (res + 1) / 2;</span></p><br />
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="COLOR: green; LINE-HEIGHT: 140%">// Get the corresponding index in the collection</span></p><br />
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="COLOR: blue; LINE-HEIGHT: 140%">int</span><span style="LINE-HEIGHT: 140%"> j = (</span><span style="COLOR: blue; LINE-HEIGHT: 140%">int</span><span style="LINE-HEIGHT: 140%">)(res * numOnSide),</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; idx = i * numOnSide + j;</span></p><br />
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="COLOR: green; LINE-HEIGHT: 140%">// Open and modify the appropriate &quot;y&quot; object</span></p><br />
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">Entity</span><span style="LINE-HEIGHT: 140%"> ent =</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; tr.GetObject(</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; ids[idx],</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">OpenMode</span><span style="LINE-HEIGHT: 140%">.ForWrite</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; ) </span><span style="COLOR: blue; LINE-HEIGHT: 140%">as</span><span style="LINE-HEIGHT: 140%"> </span><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">Entity</span><span style="LINE-HEIGHT: 140%">;</span></p><br />
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; ent.ColorIndex = col;</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160; }</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; }</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; }</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">}</span></p></div>
<p>Here&#39;s what happens as we run the NT command, committing at each step:</p>
<div style="FONT-SIZE: 8pt; BACKGROUND: white; COLOR: black; FONT-FAMILY: courier new">
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">Command: <font color="#ff0000">NT</font></span></p></div>
<p><a href="http://through-the-interface.typepad.com/.a/6a00d83452464869e2010536ede9fe970b-pi"><img alt="Red grid" border="0" height="416" src="/assets/image_990062.jpg" style="BORDER-TOP-WIDTH: 0px; BORDER-LEFT-WIDTH: 0px; BORDER-BOTTOM-WIDTH: 0px; BORDER-RIGHT-WIDTH: 0px" width="412" /></a> </p>
<div style="FONT-SIZE: 8pt; BACKGROUND: white; COLOR: black; FONT-FAMILY: courier new">
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">Commit or abort the transaction to make the circles red? [Commit/Abort] &lt;Commit&gt;: <font color="#ff0000">[Enter]</font></span></p></div>
<p><a href="http://through-the-interface.typepad.com/.a/6a00d83452464869e2010536edea07970b-pi"><img alt="Alternate yellow rows" border="0" height="415" src="/assets/image_423094.jpg" style="BORDER-TOP-WIDTH: 0px; BORDER-LEFT-WIDTH: 0px; BORDER-BOTTOM-WIDTH: 0px; BORDER-RIGHT-WIDTH: 0px" width="412" /></a> </p>
<div style="FONT-SIZE: 8pt; BACKGROUND: white; COLOR: black; FONT-FAMILY: courier new">
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">Commit or abort the transaction to make alternate circles yellow? [Commit/Abort] &lt;Commit&gt;: <font color="#ff0000">[Enter]</font></span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%"></span>&#0160;</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><a href="http://through-the-interface.typepad.com/.a/6a00d83452464869e2010536f76061970c-pi"><img alt="Show me a sine" border="0" height="415" src="/assets/image_820411.jpg" style="BORDER-TOP-WIDTH: 0px; BORDER-LEFT-WIDTH: 0px; BORDER-BOTTOM-WIDTH: 0px; BORDER-RIGHT-WIDTH: 0px" width="412" /></a> </p>
<div style="FONT-SIZE: 8pt; BACKGROUND: white; COLOR: black; FONT-FAMILY: courier new">
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">Commit or abort the transaction to draw a magenta sine wave? [Commit/Abort] &lt;Commit&gt;: <font color="#ff0000">[Enter]</font></span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">Commit or abort the top-level transaction? [Commit/Abort] &lt;Commit&gt;: <font color="#ff0000">[Enter]</font></span></p></div></div>
<p>Now let&#39;s see what happens if choose to abort some of the transactions as we run through the NT command...</p>
<p>[Abort the one to make the grid red, which means the circles start off being black]</p>
<p><a href="http://through-the-interface.typepad.com/.a/6a00d83452464869e2010536edea25970b-pi"><img alt="Sine on a bumble-bee" border="0" height="415" src="/assets/image_714953.jpg" style="BORDER-TOP-WIDTH: 0px; BORDER-LEFT-WIDTH: 0px; BORDER-BOTTOM-WIDTH: 0px; BORDER-RIGHT-WIDTH: 0px" width="412" /></a> </p>
<p>[Abort the one to make the grid alternately yellow]</p>
<p><a href="http://through-the-interface.typepad.com/.a/6a00d83452464869e2010536f76077970c-pi"><img alt="Colour blindness test" border="0" height="417" src="/assets/image_637957.jpg" style="BORDER-TOP-WIDTH: 0px; BORDER-LEFT-WIDTH: 0px; BORDER-BOTTOM-WIDTH: 0px; BORDER-RIGHT-WIDTH: 0px" width="414" /></a> </p>
<p>[Abort both the above-mentioned transactions]</p>
<p><a href="http://through-the-interface.typepad.com/.a/6a00d83452464869e2010536f76085970c-pi"><img alt="Cool sine" border="0" height="416" src="/assets/image_351904.jpg" style="BORDER-TOP-WIDTH: 0px; BORDER-LEFT-WIDTH: 0px; BORDER-BOTTOM-WIDTH: 0px; BORDER-RIGHT-WIDTH: 0px" width="413" /></a> </p>
<p>Whenever you abort the outer-most transaction, everything will disappear, of course.</p>
<p>Hopefully this gives you some idea of how nested transactions might be useful to you, allowing different sets of operations to be applied during one of your command based on changing circumstances or user-input.</p>
<p>This &quot;graphing&quot; concept is quite fun, and one I think I&#39;m going to plat around with a little more. There&#39;s definite scope for making this more of a pipelining application, which passes the data from function to function - a perfect application of F#, in fact. Hmmm. :-)</p>
