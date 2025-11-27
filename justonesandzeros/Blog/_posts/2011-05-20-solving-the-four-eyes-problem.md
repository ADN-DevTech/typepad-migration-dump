---
layout: "post"
title: "Solving the Four Eyes Problem"
date: "2011-05-20 14:59:02"
author: "Doug Redmond"
categories:
  - "Tips and Tricks"
original_url: "https://justonesandzeros.typepad.com/blog/2011/05/solving-the-four-eyes-problem.html "
typepad_basename: "solving-the-four-eyes-problem"
typepad_status: "Publish"
---

<p><img alt="" src="/assets/TipsAndTricks2.png" /></p>
<p>Here is how you can solve the &quot;Four Eyes&quot; problem with the new events feature in the API.&#0160; First, I should define the Four Eyes so we are all on the same page.</p>
<p><strong>The Four Eyes Problem:     <br /></strong>Anyone on the Engineering group can change the lifecycle state of a file from &quot;Work In Progress&quot; to &quot;For Review&quot;.&#0160; Any engineer can also change the lifecycle state from &quot;For Review&quot; to &quot;Released&quot;.&#0160; However, you don&#39;t want the same person to do both these transitions for a given file.&#0160; In other words, you want &quot;four eyes&quot; to see the file before it gets released.</p>
<p>Using the default Vault features, you can&#39;t solve the problem.&#0160; But with events, you can.&#0160; By hooking to the UpdateFileLifecycleStateEvents event, you can check the file history and add a restriction if you detect only &quot;two eyes&quot; on the file.</p>
<p><img alt="" src="/assets/restriction.png" /></p>
<p>I was going to just paste the code in-line with this post, but it turned out to be more code than I expected.&#0160; This is one of those things that the human brain has no problem with, but it takes a huge amount of work to put it into computer terms.&#0160; I can&#39;t complain though since that&#39;s how I stay employed.</p>
<p><a href="http://justonesandzeros.typepad.com/Files/FourEyes/FourEyes.cs" target="_blank">Click here for the C# code</a>&#0160; <br /><a href="http://justonesandzeros.typepad.com/Files/FourEyes/FourEyes.vb" target="_blank">Click here for the VB.NET code</a></p>
<p>Aside from the large amount of code, there are a few other ugly, yet unavoidable, aspects to this solution.</p>
<ul>
<li><strong>Redundant API calls</strong> - It would be nice to save off the reviewState and releaseState LfCycDef objects once we look them up.&#0160; We can&#39;t do that because we can&#39;t assume we are communicating with the same server or vault between events.&#0160; So, we are forced to look up the LfCycDef objects each time.</li>
<li><strong>Requires consecutive versions</strong> - There is no easy way to pinpoint which version is the one with the lifecycle state change.&#0160; We are forced to order all the versions and cycle through the set.&#0160; We compare version N with version N-1 and see if the lifecycle state is different.&#0160; If a purge creates gaps in the version history, it may be impossible to figure out who changed the state on the file.</li>
<li><strong>File.FileLfCyc.LfCycStateId and File.FileLfCyc.LfCycStateName</strong> - This one tricks me every time I run into it.&#0160; The LfCycStateId has the lifecycle state ID of the <em><strong><span style="text-decoration: underline;">latest</span></strong></em> file in the revision.&#0160; The LfCycStateName has the lifecycle state name of the <em><strong><span style="text-decoration: underline;">current</span></strong></em> file version.&#0160; That&#39;s why the code compares state name instead of the ID.</li>
</ul>
