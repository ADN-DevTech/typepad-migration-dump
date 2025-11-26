---
layout: "post"
title: "C&#35; Book, Chinese Revit API Tutorial and Insight"
date: "2020-01-16 05:00:00"
author: "Jeremy Tammik"
categories:
  - "360"
  - "Analysis"
  - "BIM"
  - "Events"
  - "Getting Started"
  - "Insight360"
original_url: "https://thebuildingcoder.typepad.com/blog/2020/01/c-book-chinese-revit-api-tutorial-and-insight.html "
typepad_basename: "c-book-chinese-revit-api-tutorial-and-insight"
typepad_status: "Publish"
---

<p>I very much enjoyed my quick visit
to <a href="https://www.swissbau.ch">Swissbau Basel</a> yesterday and meeting so many wonderful people there.
Kean shares details and photos of the event in his post
on <a href="https://www.keanw.com/2020/01/swissbau-2020.html">Swissbau 2020</a>.
Thanks to Kean for animating me to go!</p>

<p>Meanwhile, exciting topics keep piling in from
the <a href="http://forums.autodesk.com/t5/revit-api-forum/bd-p/160">Revit API discussion forum</a> and
elsewhere.</p>

<p>Here are some recent getting started tips on various areas: programming in general, C#, the Revit API and Insight:</p>

<ul>
<li><a href="#2">The C&#35; Yellow Book</a></li>
<li><a href="#3">Chinese Revit API tutorial</a></li>
<li><a href="#4">Insight into Insight</a></li>
</ul>

<h4><a name="2"></a>The C&#35; Yellow Book</h4>

<p>Steve R recommends a very comprehensive tutorial book on C# in his answer
to <a href="https://forums.autodesk.com/t5/revit-api-forum/what-programming-language/m-p/9244502">what programming language</a>:</p>

<p>The <a href="https://www.robmiles.com/c-yellow-book">C# Programming Yellow Book</a>
(<a href="/j/doc/book/cs_programming_yellow_book/csharp_book_2019_refresh.pdf">^</a>)
by <a href="https://www.robmiles.com">Rob Miles</a>.</p>

<p>It addresses just about everything you will ever need to know about C#, including questions such as 'what is a computer?' and 'what is a programmer?', a huge amount of other very good general programming advice, and is well worth browsing even for experienced programmers.</p>

<p>Many thanks to Steve for this nice pointer!</p>

<p><center></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e1689788330240a4b4759e200c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e1689788330240a4b4759e200c img-responsive" style="width: 302px; display: block; margin-left: auto; margin-right: auto;" alt="The C&#35; Yellow Book" title="The C&#35; Yellow Book" src="/assets/image_903675.jpg" /></a><br /></p>

<p></center></p>

<h4><a name="3"></a>Chinese Revit API Tutorial</h4>

<p>HeiYe DeQiShi announced
a <a href="https://forums.autodesk.com/t5/revit-api-forum/revit-api-tutorial-revit-er-ci-kai-fa-jiao-xue-shi-pin/m-p/9253875">Revit API tutorial &ndash; Revit二次开发教学视频</a>,
the 26-minute video presentation <a href="https://youtu.be/upYNPAkw2Xg">Revit之C#二次开发01vs的认识</a>:</p>

<blockquote>
  <p>黑夜de骑士
  本系列课程，由黑夜de骑士创作, qq交流群：711844216。主要是针对零基础的工程人员的revit二次开发课程，主要分为基础知识C#模块，和revit二次开发两个模块。
  contact me: 1056291511@qq.com
  &ndash; A course created by Knight de Knight, qq exchange group: 711844216, teaching basic knowledge of C# and the Revit API beginners.</p>
</blockquote>

<p><center>
<iframe width="480" height="270" src="https://www.youtube.com/embed/upYNPAkw2Xg" frameborder="0" allow="accelerometer; autoplay; encrypted-media; gyroscope; picture-in-picture" allowfullscreen></iframe>
</center></p>

<h4><a name="4"></a>Insight into Insight</h4>

<p>An email conversation with a colleague of mine provides some insight on Insight, including links to learning materials and new features added in 2020.1 to get an idea about what is going on in this area:</p>

<p><strong>Question:</strong> Our MEP design team is really interested in Insight.
Can you provide a demonstration of it?</p>

<p><strong>Answer:</strong> Which Insight?</p>

<p>The <a href="http://help.autodesk.com/view/BIM360D/ENU/?guid=GUID-EC46253E-130E-4CE9-B0C1-2FB8333E1116">one in BIM 360</a>?</p>

<p>Or <a href="https://insight.autodesk.com/oneenergy">Insight 360</a>?</p>

<p>Are you asking about a product feature demo?</p>

<p>Currently, there is no API for either.</p>

<p>But, depending on what you are trying to do, certain things might be possible, e.g., by writing a Revit add-in.</p>

<p><strong>Response:</strong> I meant Insight 360, an add-in Revit.</p>

<p>Yes, I was asking about the product feature demo.</p>

<p>Later, we would also like to know about the API and customization.</p>

<p>Mostly we would like to know the capability of Insight 360 on Revit, like lighting and mechanical load calculation, simulation and analysis.</p>

<p>Our engineers are mainly using Revit, so we have basic knowledge of that.</p>

<p>Regarding the API, is it possible to customize it according to our preferences?</p>

<p><strong>Answer:</strong>  I would suggest looking at the following webinar recordings:</p>

<ul>
<li>For Insight (Energy and Lighting, mostly about the Energy side): 
<a href="http://blogs.autodesk.com/revit/2018/07/06/autodesk-insight-webinar-series">Learn everything about Autodesk Insight – Episodes 1-3</a></li>
<li>For Insight Lighting, this is a little old but a good introduction, especially under the hood: 
<a href="https://www.youtube.com/watch?v=mtZXEAYNd00">Lighting Analysis in Revit</a></li>
<li>For Mechanical and Load Analysis, check out the following about new features in Revit 2020.1+:
<a href="https://www.youtube.com/watch?v=8kvSB5abVH4">Webinar &ndash; An Introduction to Revit Systems Analysis with Revit 2020.1</a></li>
<li><a href="https://www.autodesk.com/autodesk-university/class/Revit-Systems-Analysis-Features-and-Framework-Introduction-2019">AU Class Recording &ndash; Revit Systems Analysis Features and Framework Introduction</a></li>
</ul>

<p><center></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e1689788330240a4b4a38a200c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e1689788330240a4b4a38a200c img-responsive" alt="Dr. Wissam Wahbeh of FHNW showing his digital and physical twin model at Swissbau 2020" title="Dr. Wissam Wahbeh of FHNW showing his digital and physical twin model at Swissbau 2020" src="/assets/image_c8a558.jpg" border="0" style="display: block; margin-left: auto; margin-right: auto;" /></a><br /></p>

<p style="font-size: 80%; font-style:italic">Dr. Wissam Wahbeh of FHNW showing Kean and me his digital and physical twin model at Swissbau 2020</p>

<p></center></p>
