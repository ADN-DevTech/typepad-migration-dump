---
layout: "post"
title: "List typepad articles using jQuery"
date: "2013-09-19 08:27:01"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "HTML"
  - "Javascript"
original_url: "https://adndevblog.typepad.com/cloud_and_mobile/2013/09/list-typepad-articles-using-jquery.html "
typepad_basename: "list-typepad-articles-using-jquery"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/cloud_and_mobile/adam-nagy.html" target="_self">Adam Nagy</a></p>
<p>I was trying to find a way to list all the articles / blog posts we have on typepad. I found how to export (or backup) all the blog posts from typepad, but did not find a way inside typepad to get a list of all the titles with the URL links.&#0160;</p>
<p>I played with the typepad API before when I created an <a href="http://adndevblog.typepad.com/cloud_and_mobile/2012/08/using-typepad-json-api-with-jquery.html" target="_blank" title="html page that shows statistics on our blogs">html page that shows statistics on our blogs</a>. So I decided to modify it to list the blog post titles instead. The calls I needed to use to get information about the blog posts in order to collect statistical information (who posted how many blog articles) contained already all the information necessary to link the blog post titles, so I just had to modify that part of the code to list the titles instead.</p>
<p>
<a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b019aff7d0b93970c-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="DevBlogList" class="asset  asset-image at-xid-6a0167607c2431970b019aff7d0b93970c" src="/assets/image_4c09a6.jpg" style="width: 450px;" title="DevBlogList" /></a>&#0160;</p>
<p>You can find <strong>DevBlogList.html</strong> on <a href="https://github.com/ADN-DevTech/Typepad-jQuery" target="_blank" title="https://github.com/ADN-DevTech/Typepad-jQuery"></a><a href="https://github.com/ADN-DevTech/Typepad-jQuery" target="_blank">https://github.com/ADN-DevTech/Typepad-jQuery</a>&#0160;</p>
