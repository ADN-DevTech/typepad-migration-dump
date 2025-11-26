---
layout: "post"
title: "RevitAPI: Those Exceptions"
date: "2014-12-10 20:30:56"
author: "Aaron Lu"
categories:
  - "Aaron Lu"
  - "Revit"
original_url: "https://adndevblog.typepad.com/aec/2014/12/revitapi-those-exceptions.html "
typepad_basename: "revitapi-those-exceptions"
typepad_status: "Publish"
---

<p><small>By <a href="http://adndevblog.typepad.com/aec/aaron-lu.html" target="_self">Aaron Lu</a></small></p>
<p>In this post, I would like to collect some regular exceptions thrown from RevitAPI and try to find the reason and how to handle them.</p>
<h2>ModificationOutsideTransactionException</h2>
<p>First, let&#39;s see ModificationOutsideTransactionException, this is the shock for all beginners of RevitAPI.<br /> It happens when we are trying to modify Revit model, for example,</p>
<script src="https://adndevblog.typepad.com/files/run_prettify-3.js" type="text/javascript"></script>
<pre class="prettyprint">RevitDoc.ProjectInformation.Author = &quot;Aaron Lu&quot;;
</pre>
<p>The exception is thrown because we need to start a transaction before modifying and commit the transaction after.<br /> So here it is how to do it properly:</p>
<pre class="prettyprint">Transaction transaction = 
    new Transaction(doc, &quot;Change author of project&quot;);
transaction.Start();
try
{
    //update Revit model here
    RevitDoc.ProjectInformation.Author = &quot;Aaron Lu&quot;;
    transaction.Commit();
}
catch (Exception ex)
{
    transaction.RollBack();
}
</pre>
<p><strong>Why we need to do that?</strong><br /> The benifit is our operation needs to be tracked by Undo-Redo mechanism in Revit, that is to say, our operation will be appeared in the Undo/Redo list.</p>
<p>&#0160;</p>
<h2>Autodesk.Revit.Exceptions.InvalidOperationException: The document is currently modifiable! Close the transaction before calling EditFamily</h2>
<p>We can&#39;t include Document.EditFamily() method in transaction, because the EditFamily itself will start a new transaction internally! otherwise this exception will be thrown.</p>
<p><a href="http://blog.csdn.net/lushibi/article/details/41863651">中文链接</a></p>
