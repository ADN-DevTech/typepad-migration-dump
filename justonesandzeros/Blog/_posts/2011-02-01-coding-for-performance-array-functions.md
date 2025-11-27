---
layout: "post"
title: "Coding for Performance - Array Functions"
date: "2011-02-01 07:39:56"
author: "Doug Redmond"
categories:
  - "Tips and Tricks"
original_url: "https://justonesandzeros.typepad.com/blog/2011/02/coding-for-performance-array-functions.html "
typepad_basename: "coding-for-performance-array-functions"
typepad_status: "Publish"
---

<p><img alt="" src="/assets/TipsAndTricks2.png" /></p>
<p>Welcome to a new ongoing series which will be focusing on writing optimized Vault code.&#0160; In may cases, there are multiple ways to get data from the Vault server, but some ways may be quicker than others depending on your needs.</p>
<p>There are many factors in Vault performance.&#0160; Network speed, server configuration, available memory, concurrent users, and so on.&#0160; These articles will not be focusing on any of that.&#0160; I will be covering only the <strong>coding</strong> aspect of performance.</p>
<p>&#0160;</p>
<p>This article I will be focusing on what I like to call the <em>Golden Rule of Vault API performance</em>.</p>
<table border="1" cellpadding="2" cellspacing="0" width="450">
<tbody>
<tr>
<td valign="top" width="450">Golden Rule: <strong>Minimize the number of Web Service calls.</strong></td>
</tr>
</tbody>
</table>
<p>You should always follow the Golden Rule... except in cases where you don&#39;t.&#0160; I&#39;ll be sure to point these cases out when they come up.&#0160;</p>
<p>Looking through the API, you probably noticed some functions come in pairs, one that deals with a single object and one that deals with arrays of objects.&#0160; For this example, I will focus on <strong>GetFileById</strong> and <strong>GetFilesByIds</strong> in the Document Service.</p>
<table border="1" cellpadding="2" cellspacing="0" width="450">
<tbody>
<tr>
<td valign="top" width="450">
<p>File <strong>GetFileById (</strong> <br />&#0160;&#0160;&#0160; Long <em>fileId</em> <br /><strong>);</strong></p>
<p>File [] <strong>GetFilesByIds (</strong> <br />&#0160;&#0160;&#0160; Long [] <em>fileIds</em> <br /><strong>);</strong></p>
</td>
</tr>
</tbody>
</table>
<p>The 2 functions do the same thing, they take a file ID and return the File object.&#0160; But one function is capable of getting multiple objects and the other one can only get a single object.&#0160;</p>
<p>According to the Golden Rule, you should always use GetFilesByIds since it minimizes the number of API calls.&#0160; Technically, GetFileById can be removed from the API entirely.&#0160; It&#39;s a bit easier to use if you want to get a single object, but that&#39;s the only advantage.&#0160; Most of these non-array functions are hold-overs from early versions of the API.&#0160; When new API functions are added, there is usually only the the array version.</p>
<p>I ran some tests using a vault that I loaded with 100,000 files.&#0160; I then proceeded to get arrays of File objects using the two mechanisms.&#0160; For example, I would get 100 File objects with 1 GetFilesbyIds call and compare the time with getting the same data with 100 GetFileById calls.</p>
<p><br />Here is the resulting graph.<img alt="" src="/assets/Graph.png" /></p>
<p>The results are what you would expect.&#0160; Both methods increase in a linear fashion based on the number of Files being returned.&#0160; However the Single Method is a much steeper line.&#0160; In my case, it was about 17x steeper and that was with client and server on the same machine.&#0160;&#0160;</p>
<p>&#0160;</p>
<p><strong>The upper boundary      <br /></strong>The array functions are good for moderate to large data sets.&#0160; But what about very large data sets?&#0160; For example, you want 1 million File objects.&#0160; Problems start to arise when you try to do it all in a single call.&#0160;</p>
<p>One common problem is the HTTP transfer limit, which is 50 MB.&#0160; So if your data is larger than 50 MB when it gets transferred, you can&#39;t do it in one call.&#0160; If you go over the limit, you will get an exception from the HTTP or WSE framework.</p>
<p>Another problem is server performance.&#0160; Don&#39;t forget that you application is probably not the only Vault client.&#0160; There may be others using Vault at the same time and you don&#39;t want to use up all the server cycles.&#0160; Sometimes it&#39;s better to deliberately slow down your program to make the system more usable for everyone.</p>
<p>Lastly, there are timeouts in place, both at the web server and at the database level.&#0160; I believe that our installer recommends 900 seconds (15 minutes) for the web server timeout.&#0160; The database timeouts are more complex, but most queries have a 6 minute timeout.</p>
<p>For the GetFilesByIds case, I suggest, 10,000 as your upper bound.&#0160; This number may vary depending on how good or bad your system is.&#0160; But overall. you should be safe using this number.</p>
<p><strong>Future Articles     <br /></strong>This example was pretty basic since it was the first in the series.&#0160; In future articles, you can look forward to more complex workflows with multiple solutions.&#0160; I&#39;ll try my best to draw from real-world situations, so feel free to leave a comment with something you have had issues with.</p>
