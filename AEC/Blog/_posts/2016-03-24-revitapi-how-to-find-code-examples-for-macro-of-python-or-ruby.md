---
layout: "post"
title: "RevitAPI: how to find code examples for macro of python or ruby?"
date: "2016-03-24 22:47:00"
author: "Aaron Lu"
categories:
  - "Aaron Lu"
  - "Revit"
original_url: "https://adndevblog.typepad.com/aec/2016/03/revitapi-how-to-find-code-examples-for-macro-of-python-or-ruby.html "
typepad_basename: "revitapi-how-to-find-code-examples-for-macro-of-python-or-ruby"
typepad_status: "Publish"
---

<p><a href="http://blog.csdn.net/lushibi/article/details/50519163">中文链接</a></p>
<p><small>By <a href="http://adndevblog.typepad.com/aec/aaron-lu.html" target="_self">Aaron Lu</a></small></p>
<p>One of the new features of Revit 2016 API is that Revit now supports macro written in Python or Ruby.</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c8178392970b-pi" style="display: inline;"><img alt="How to find python macro exmaples-create macro" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b7c8178392970b image-full img-responsive" src="/assets/image_332212.jpg" title="How to find python macro exmaples-create macro" /></a></p>
<p>But there is no Python or Ruby code examples in Revit 2016 SDK. So how to get started? Well, most of people did not notice that the code examples are hidden in the default macro projects created by macro manager, see below image, the project is the default python project, there are several &quot;if False&quot; statements, if you remove the line of &quot;if False&quot;, uncomment and unindent 4 spaces to methods below it, the methods will become available macros and appear in the macro manager, and you will see the same &quot;if false&quot; in Ruby projects too:</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb08bc52d9970d-pi" style="display: inline;"><img alt="How to find python macro exmaples" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01bb08bc52d9970d image-full img-responsive" src="/assets/image_838240.jpg" title="How to find python macro exmaples" /></a></p>
