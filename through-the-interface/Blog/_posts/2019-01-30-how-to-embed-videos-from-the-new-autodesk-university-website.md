---
layout: "post"
title: "How to embed videos from the new Autodesk University website"
date: "2019-01-30 15:57:36"
author: "Kean Walmsley"
categories:
  - "AU"
  - "Autodesk"
  - "Conferences"
  - "Weblogs"
original_url: "https://www.keanw.com/2019/01/how-to-embed-videos-from-the-new-autodesk-university-website.html "
typepad_basename: "how-to-embed-videos-from-the-new-autodesk-university-website"
typepad_status: "Publish"
---

<p>This post goes out to my blogging brethren (and sistren) around the world… I’m sure more than a few of you have tried this, yourselves, so hopefully it’ll be of some help to people.</p>
<p>I recently looked back at <a href="https://www.keanw.com/2018/11/au-2018-class-recordings-are-now-online.html" rel="noopener" target="_blank">a post of mine embedding Autodesk University 2018 videos</a> and noticed that they no longer worked: I was sure they did when I wrote the post, but it seemed that something had changed, probably with the viewer component being used. I gnashed my teeth for a while and then <a href="http://community.ooyala.com/t5/Developers-Forum/Error-when-embedding-video/td-p/10972" rel="noopener" target="_blank">posted to the Ooyala developer forum</a> to see if anyone there could help.</p>
<p>It turns out that the embed codes currently provided by the <a href="https://www.autodesk.com/autodesk-university/?" rel="noopener" target="_blank">AU website</a> are wrong. <a href="https://www.autodesk.com/autodesk-university/class/Generative-Urban-Design-collaboration-between-Autodesk-Research-and-Van-Wijnen-2018-0#video" rel="noopener" target="_blank">Click on this link</a>, start to play the video, and then select the “&lt;/&gt;” in the bottom right to get its embed code.</p>
<p><a href="https://through-the-interface.typepad.com/.a/6a00d83452464869e2022ad39536f7200c-pi" rel="noopener" target="_blank"><img alt="An embed code from the AU website" border="0" height="291" src="/assets/image_688539.jpg" style="margin: 30px auto; border: 0px currentcolor; float: none; display: block; background-image: none;" title="An embed code from the AU website" width="500" /></a></p>
<p>The code that gets provided is (at the time of writing) this:</p>
<pre>&lt;iframe width=&quot;640&quot; height=&quot;480&quot; src=&quot;//player.ooyala.com/static/v4/candidate/latest/skin-plugin/iframe.html?ec=w0dzhvZzE6U5BgkUokxObHgCHya_km3N&amp;pbid=e7917642de4a4f1eae0a331375e03784&amp;pcode=o3OG46qoFLfL76WkTkb1a9k23qBd&quot; frameborder=&quot;0&quot; allowfullscreen=&quot;&quot;&gt;&lt;/iframe&gt;</pre>
<p>The source URL refers to an unstable version of the iframe player, but it also has a parameter missing. All you need to do is to change <span style="font-family: Courier New;">candidate</span> to <span style="font-family: Courier New;">production</span> in the URL, and then add&#0160; <span style="font-family: Courier New;">&amp;=inline{}</span> to the URL parameters. (I found this last piece out via browser debugging… there’s a little piece of code that deserializes <span style="font-family: Courier New;">params.inline</span> as JSON and then checks whether it’s defined: if <span style="font-family: Courier New;">params.inline</span> isn’t defined in the first place we get a JSON parsing error.)</p>
<pre>&lt;iframe width=&quot;640&quot; height=&quot;480&quot; src=&quot;//player.ooyala.com/static/v4/production/latest/skin-plugin/iframe.html?ec=w0dzhvZzE6U5BgkUokxObHgCHya_km3N&amp;pbid=e7917642de4a4f1eae0a331375e03784&amp;pcode=o3OG46qoFLfL76WkTkb1a9k23qBd&amp;inline={}&quot; frameborder=&quot;0&quot; allowfullscreen=&quot;&quot;&gt;&lt;/iframe&gt;</pre>
<p>I don’t know whether the root issue is with the AU website’s integration of Ooyala technology or with the technology itself. Either way, I’ve reported it to the AU website team and have heard back from them that they’re aware of the situation, so with any luck this will already have been fixed by the time you read this post. Here’s the (hopefully) working embedded video, for you to test:</p>
<p style="text-align: center">&#0160;</p>
<p style="text-align: center"><iframe allowfullscreen="" frameborder="0" height="283" src="//player.ooyala.com/static/v4/production/latest/skin-plugin/iframe.html?ec=w0dzhvZzE6U5BgkUokxObHgCHya_km3N&amp;pbid=e7917642de4a4f1eae0a331375e03784&amp;pcode=o3OG46qoFLfL76WkTkb1a9k23qBd&amp;inline={}" width="500"></iframe></p>
<p style="text-align: center">&#0160;</p>
<p>Have fun embedding your favourite Autodesk University videos into your blogs and websites. Thanks for helping spread this knowledge!</p>
