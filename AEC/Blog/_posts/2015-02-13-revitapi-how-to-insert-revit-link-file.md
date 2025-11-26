---
layout: "post"
title: "RevitAPI: How to insert Revit Link file"
date: "2015-02-13 00:04:32"
author: "Aaron Lu"
categories:
  - "Aaron Lu"
  - "Revit"
original_url: "https://adndevblog.typepad.com/aec/2015/02/revitapi-how-to-insert-revit-link-file.html "
typepad_basename: "revitapi-how-to-insert-revit-link-file"
typepad_status: "Publish"
---

<a href="http://blog.csdn.net/lushibi/article/details/43410475">中文链接</a>

<p><small>By <a href="http://adndevblog.typepad.com/aec/aaron-lu.html" target="_self">Aaron Lu</a></small></p>

<script type="text/javascript" src="https://adndevblog.typepad.com/files/run_prettify-3.js"></script>


<p>
  In Revit, we can insert a link file easily via &quot;Insert &gt; Link Revit&quot;, but how to do that from API?
</p>
<p>
  <br />
  
</p>
<p>
First I thought there might be some methods like Link or Load under Document class, unfortunately, there are, but they are only for DWG, DGN etc. rather than Revit Link file.
</p>
<p>
And also, there is no method named: Document.Create.NewRevitLinkInstance<br />
</p>
<p>
So I inspected the RevitLinkInstance class itself, found a method: <strong>RevitLinkInstance.Create</strong>.
</p>
<p>
But new problem comes, the method takes an argument which is element id of RevitLinkType, how to create a RevitLinkType?
</p>
<p>
  <br />
</p>
<p>
It comes to my mind that maybe LoadFamilySymbol is the one, but the document says it is only for loading .rfa file
</p>
<p>
so again, I inspect RevitLinkType class. Got this: <strong>RevitLinkType.Create</strong>
</p>
<p>
  <br />
  
</p>
<p>
Well, the RevitLinkType.Create needs a ModelPath as one of its argument, but ModelPath can neigther be created from new (no public constructor), nor from Application.Create or Document.Create. Then how?
</p>
<p>
Fortunately I saw a class named ModelPathUtils, which contains a method <strong>ModelPathUtils.ConvertUserVisiblePathToModelPath</strong>, that is it!
</p>
<p>
  <br />
  
</p>
<p>
Here comes the code:
</p>
<p>
</p>
<div>
  <pre name="code" class="csharp prettyprint">ModelPath mp = ModelPathUtils.ConvertUserVisiblePathToModelPath(
    @&quot;D:\Wall.rvt&quot;);
RevitLinkOptions rlo = new RevitLinkOptions(false);
var linkType = RevitLinkType.Create(RevitDoc, mp, rlo);
var instance = RevitLinkInstance.Create(RevitDoc, linkType.ElementId);</pre>
  <br />
  Note, the above code should be enclosed in a transaction<br />
</div>
<br />
