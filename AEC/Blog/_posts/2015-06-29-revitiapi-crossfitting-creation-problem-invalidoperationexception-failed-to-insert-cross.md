---
layout: "post"
title: "RevitiAPI: CrossFitting creation problem - InvalidOperationException: failed to insert cross."
date: "2015-06-29 22:59:00"
author: "Aaron Lu"
categories:
  - "Aaron Lu"
  - "Revit"
  - "Revit MEP"
original_url: "https://adndevblog.typepad.com/aec/2015/06/revitiapi-crossfitting-creation-problem-invalidoperationexception-failed-to-insert-cross.html "
typepad_basename: "revitiapi-crossfitting-creation-problem-invalidoperationexception-failed-to-insert-cross"
typepad_status: "Publish"
---

<p><a href="http://blog.csdn.net/lushibi/article/details/46444227">中文链接</a></p>
<p><small>By <a href="http://adndevblog.typepad.com/aec/aaron-lu.html" target="_self">Aaron Lu</a></small></p>
<p>I was asked a problem about create Cross Fitting,</p>
<p>I remember that the cross fitting creation method Document.Create.NewCrossFitting needs 4 connectors as arguments, the order of those connectors is not arbitrary, as Jeremy said in this post http://thebuildingcoder.typepad.com/blog/2014/10/newcrossfitting-connection-order.html, it should be main-main-side-side rather than main-side-main-side.</p>
<p>But when I got the file and test it, it always throws InvalidOperationException: failed to insert cross, no matter what order I tried.</p>
<blockquote>
<p>Autodesk.Revit.Exceptions.InvalidOperationException: failed to insert cross.</p>
</blockquote>
<p>It is wired. But fortunately I found the cause: the Routing Preference is not set for Cross.</p>
<p>After setting it, the problem has gone!</p>
<p>Pick the pipe &gt; Edit type &gt; Routing Preferences &gt; Cross &gt; select &quot;All&quot;</p>
<p>as below:</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d125ee55970c-pi" style="display: inline;"><img alt="CreateCrossFailedBecauseOfRoutinePreferenceSetting" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b8d125ee55970c image-full img-responsive" src="/assets/image_216703.jpg" title="CreateCrossFailedBecauseOfRoutinePreferenceSetting" /></a></p>
<p>&#0160;</p>
