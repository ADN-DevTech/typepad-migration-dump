---
layout: "post"
title: "RevitAPI: Close active document"
date: "2015-07-13 22:28:00"
author: "Aaron Lu"
categories:
  - "Aaron Lu"
  - "Revit"
original_url: "https://adndevblog.typepad.com/aec/2015/07/revitapi-close-active-document.html "
typepad_basename: "revitapi-close-active-document"
typepad_status: "Publish"
---

<p><a href="http://blog.csdn.net/lushibi/article/details/46723855">中文链接</a></p>
<p><small>By <a href="http://adndevblog.typepad.com/aec/aaron-lu.html" target="_self">Aaron Lu</a></small></p>
<p>In the forum, customer said he <a href="http://forums.autodesk.com/t5/revit-api/how-do-i-register-that-event-handler-has-finished/td-p/5693074">wants to close the active document and reopen it</a>, he took the suggestion from <a href="http://thebuildingcoder.typepad.com/blog/2012/12/closing-the-active-document.html">Jeremy&#39;s blog</a>, and had some events related questions, though I did not totally understand yet :(</p>
<p>&#0160;</p>
<p>As we know, to close document, we can use UIDocument.SaveAndClose() or Document.Close(), however, if the document is active, we got an InvalidOperationException: The active document may not be closed from the API.</p>
<p>So we need to use something else, Jeremy mentioned one way by <a href="http://thebuildingcoder.typepad.com/blog/2010/10/closing-the-active-document-and-why-not-to.html">sending &quot;Ctrl+F4&quot; message to Revit</a>, i.e.:</p>
<pre class="csharp prettyprint"> SendKeys.SendWait( &quot;^{F4}&quot; );</pre>
<p><br /> Another way is,<a href="http://thebuildingcoder.typepad.com/blog/2012/12/closing-the-active-document.html"> open and activate a placeholder document, and then close the original one</a>, that&#39;s what the customer tried to do, and it works. To summary what the article is about, I used a shorter code snippet:</p>
<pre class="csharp prettyprint">var placeholderFile = @&quot;C:\placeholder.rvt&quot;;
var doc = commandData.Application.ActiveUIDocument.Document;
var file = doc.PathName;
var docPlaceholder = commandData.Application.OpenAndActivateDocument(placeholderFile);
doc.Close(false);
var uidoc = commandData.Application.OpenAndActivateDocument(file);
docPlaceholder.Document.Close(false);</pre>
<p>Steps:</p>
<ul>
<li>Open and active the placeholder document</li>
<li>Close the doc</li>
<li>Open and activate the doc</li>
<li>Close the placeholder document</li>
</ul>
<p>So, we don&#39;t need any events for now :)</p>
<p>&#0160;</p>
