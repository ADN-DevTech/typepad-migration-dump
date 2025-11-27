---
layout: "post"
title: "Asking for Help - Do&rsquo;s and Don&rsquo;ts"
date: "2012-12-04 15:43:10"
author: "Doug Redmond"
categories:
  - "Tips and Tricks"
original_url: "https://justonesandzeros.typepad.com/blog/2012/12/asking-for-help-dos-and-donts.html "
typepad_basename: "asking-for-help-dos-and-donts"
typepad_status: "Publish"
---

<p><img alt="" src="/assets/TipsAndTricks3.png" /></p>
<p>Although my official job title is “Software Engineer,” my API knowledge makes me an honorary member of the tech support team.&#0160; More often than not, I read a problem and don’t have enough information to answer it properly.&#0160; This requires me to respond asking for more formation.&#0160; Usually one or two more back-and-forth sessions are needed before the question is finally answered.&#0160; This process is a wasteful, especially when different time zones are involved.&#0160; If you figure that each back-and-forth takes 24 hours, there is a definite benefit to reducing these round-trips.</p>
<p>So here is a list of things you can do to make the process more efficient.&#0160; The goal is to provide all the needed information upfront so that I don’t have to go back and ask you to elaborate in more detail.</p>
<hr noshade="noshade" style="color: #ff5a00;" />
<p><strong>DO... include the Vault product and version      <br /></strong>Is it Vault Basic?&#0160; Vault Professional?&#0160; Is is the 2013 version?&#0160; Each product has a different feature set and the API may change between release years.</p>
<p><strong>DON’T... use the phrase “It doesn’t work”      <br /></strong>That statement is way too vague.&#0160; You need to be more specific like “it crashes the program” or “it locks up and I have to restart the app” or “it times out” or “it returns error 303”</p>
<p><strong>DO... include the error code      <br /></strong>If the server returns an error, don’t just say “the server throws an error”.&#0160; Tell me what the error code is.&#0160; Don’t leave me in suspense.&#0160; I even have <a href="http://justonesandzeros.typepad.com/blog/2011/11/getting-the-restriction-codes.html" target="_blank">some code snippets</a> you can use to extract the error and restriction codes (if any).</p>
<p><strong>DON’T... provide too much source code      <br /></strong>Sometimes people will post their entire project and expect me to debug it.&#0160; That’s not a good use of anybody’s time.&#0160; If you are going to include source code, it should be isolated to a single function at most.</p>
<p><strong>DO... isolate the line of code that fails      <br /></strong>If you are sending a block of code and are getting Exceptions, you need to indicate clearly which line is causing the breakage.</p>
<p><strong>DO... take screenshots      <br /></strong>It’s amazing how much information is conveyed in a screenshot.&#0160; Even if the Vault is in a different language, I can usually figure out what is going on based on the icons and dialogs.&#0160; Videos are great too.&#0160; If you find yourself taking 4 or more screenshots, it may be easier to just record a video.</p>
<p><strong>DO... use Exception.ToString()      <br /></strong>ToString is the best thing to happen to debugging since the print statement.&#0160; Don’t be afraid to use it and include the full stack trace in your problem report.&#0160; Even if you don’t understand it all, it may make sense to me since I have access to the Vault source code.</p>
<p><img alt="" src="/assets/TipsAndTricks3-1.png" /></p>
