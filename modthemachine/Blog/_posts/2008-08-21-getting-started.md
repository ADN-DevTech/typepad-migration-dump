---
layout: "post"
title: "Inventor API Fundamentals 001 - Getting Started"
date: "2008-08-21 18:00:16"
author: "Adam Nagy"
categories:
  - "Beginning API"
  - "Brian"
original_url: "https://modthemachine.typepad.com/my_weblog/2008/08/getting-started.html "
typepad_basename: "getting-started"
typepad_status: "Publish"
---

<p>I plan on having a series of posts that are intended for those of you that are new to programming and/or Inventor&#39;s programming interface and want to get started writing programs for Inventor.&#160; I&#39;ll also break in on the series occasionally with various other topics that are usually more advanced or that I find interesting at the moment.&#160; This post is the first in the getting started series.&#160; Here is the process I would suggest for anyone wanting to program Inventor.</p>
<ol>
<li>Learn how to use Inventor interactively.&#160; This is more important than you might think.&#160; Most of Inventor’s programming interface is just an alternative way of doing the same things you do interactively through the user-interface.&#160; For example, if you create an extrude feature through the user-interface you see the required input; profile, solid or surface output, operation, extent information, and taper angle.&#160; You supply all of this information through the Extrude dialog and by interacting with the model.&#160; To create an Extrude through the API you call a method that has exactly the same type of input.&#160; The same set of information is being gathered by both the command (user-interface) and the programming interface and then both interfaces end up calling the same internal function to create the actual extrusion.<br /><br />Understanding how Inventor works and the requirements for the various commands is easiest to learn through the user-interface.&#160; Since these same concepts also apply when using the programming interface it will make learning the programming interface much easier. <br /><br />
<li>Choose and learn a programming language.&#160; To effectively program you need to have a basic understanding of the programming language you’re using.&#160; To effectively customize Inventor you DO NOT need to be an expert in that language.&#160; Any language has a lot of features and intricacies that you could spend more time than most of us have learning them.&#160; A basic understanding of the language should be enough.&#160; In my next posting I’ll discuss several different languages and the pros and cons of each. <br /><br />
<li>Begin learning Inventor’s programming interface.&#160; There are some basic concepts to learn to get started and then you can expand into different functional areas of Inventor.&#160; You don’t need to learn the entire API at once.&#160; Looking at different areas of the Inventor&#39;s API will be a running theme of this blog.&#160; </li>
</ol>
<p>So, for now just keep working with Inventor and we’ll look at programming languages next time.</p>
