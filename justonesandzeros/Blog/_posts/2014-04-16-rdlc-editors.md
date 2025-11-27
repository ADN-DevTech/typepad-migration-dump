---
layout: "post"
title: "RDLC Editors"
date: "2014-04-16 08:08:00"
author: "Doug Redmond"
categories:
  - "Tips and Tricks"
  - "Vault"
original_url: "https://justonesandzeros.typepad.com/blog/2014/04/rdlc-editors.html "
typepad_basename: "rdlc-editors"
typepad_status: "Publish"
---

<p><img alt="" src="/assets/Vault4.png" /> <br /><img alt="" src="/assets/TipsAndTricks4.png" /></p>
<p><strong>Update:</strong>&#0160; One of my readers was able to find a link to <a href="http://www.asp.net/downloads/essential" target="_blank">Visual Studio 2008 Express Web Edition</a>.&#0160; After that, install the <a href="http://www.microsoft.com/en-us/download/details.aspx?id=16682" target="_blank">Reporting add-on</a>, and you should be good for RDLC editing.&#0160; Thanks Alex.</p>
<p>&#0160;</p>
<p>So, you want to make custom Vault reports.&#0160; Good for you.&#0160; All you need is a Vault and Visual Studio 2008 *<em>record scratch</em>*</p>
<p>Wait, you <em>do</em> have Visual Studio 2008, don’t you?&#0160; If not, then you can just download it from Microsoft.com *<em>record scratch</em>*</p>
<p>Oh, it’s not up there anymore.&#0160; Well, lets go over our options then.&#0160; Before my record gets scratched up too much, I’m going to rule out time travel and breaking into ancient history museums.&#0160; Here are some <em>helpful</em> ways to edit your RDLC file.</p>
<hr noshade="noshade" style="color: #d09219;" />
<p><strong>RDL Designer from fyiReporting</strong> <br /><a href="http://www.fyireporting.com/" title="http://www.fyireporting.com/">http://www.fyireporting.com/</a></p>
<p>This is a free editor from a set of RDL / RDLC tools from fyiReporting.&#0160; It’s almost as good as Visual Studio 2008 was.&#0160; Unlike VS 2008, the RDL Designer is still available for download, which makes it the best RDLC editor by default.</p>
<p>I’ve used this tool myself, and I like it a lot.&#0160; My only complaint is that there is no snap feature or ability to easily line up your UI controls.&#0160; So you end up with a bunch of text boxes that are off by a few pixels.&#0160; If you are obsessive compulsive, this will drive you nuts.</p>
<p>You can line things up by modifying directly editing the properties, which is also extra painful.&#0160; Because RDLC is built around printing, all the dimensions are in units like inch, centimeter and point.&#0160; You can&#39;t define things in pixels, like I am used to in UI design.</p>
<p>Aside from the alignment issues, everything else is done well.&#0160; For example, you can easily create tables by selecting the fields you want, and it will build the data for you.</p>
<hr noshade="noshade" style="color: #d09219;" />
<p><strong>An XML Editor</strong></p>
<p>RDLC files are just XML files.&#0160; So you can use any text or XML editor to make changes.&#0160; It’s obviously harder to do it this way because you are designing a UI in a non-UI environment.&#0160; But it’s a good option for people who like to challenge themselves for no reason at all.</p>
<p>I should also note that the fyi RDL Designer has an XML mode.&#0160; So you can easily switch between the UI view and the XML view.&#0160; Very useful.&#0160; If you happen to run into any of those fyi guys, tell them I said thanks.</p>
<hr noshade="noshade" style="color: #d09219;" />
