---
layout: "post"
title: "Integration of Salesforce.com and AutoCAD WS - Part 2"
date: "2012-05-10 19:22:31"
author: "Daniel Du"
categories:
  - "AutoCAD"
  - "Cloud"
  - "Daniel Du"
original_url: "https://adndevblog.typepad.com/autocad/2012/05/integration-of-salesforcecom-and-autocad-ws-part-2.html "
typepad_basename: "integration-of-salesforcecom-and-autocad-ws-part-2"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/daniel-du.html">Daniel Du</a></p>
<p><img alt="" height="66" src="/assets/image_845925.jpg" width="211" /></p>
<p>In <a href="http://adndevblog.typepad.com/autocad/2012/05/integration-of-salesforcecom-and-autocad-ws-part-1.html" target="_blank">last post</a>, I introduced how to setup the browser based “Development Mode Tools” and how to create a helloworld visualforce page. You can get all your job done with browser when developing applications on force.com. At the same time, I also understand some developers prefer to work on a desktop based IDE. Salesforce provides such IDE as well – <a href="http://wiki.developerforce.com/page/An_Introduction_to_Force_IDE" target="_blank">force.com IDE</a>—which is an Eclipse plugin. If you are familiar with Eclipse, force.com IDE may be a good option for you.</p>
<p>To install force.com IDE, please make sure you have already signed up for a free <a href="http://www.apexdevnet.com/events/regular/registration.php?d=701300000009Ur7">Developer Edition</a> account, then follow the <a href="http://wiki.developerforce.com/page/Force.com_IDE_Installation" target="_blank">Force.com IDE installation</a> instructions. Once installed, start up the IDE and select <strong>Window &gt; Open Perspective &gt; Other &gt; Force.com</strong> to access the main interface of the Force.com IDE. You should see something similar to following screen:</p>
<p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b016766630da2970b-pi"><img alt="image" border="0" height="256" src="/assets/image_867907.jpg" style="background-image: none; padding-left: 0px; padding-right: 0px; display: inline; padding-top: 0px; border-width: 0px;" title="image" width="424" /></a></p>
<p>The easiest way to get started is to choose <strong>File &gt; New &gt; Force.com Project</strong>. You will see the new Force.com Project dialogue, you are recommended to use your force.com account as project name.</p>
<p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b0163056f0e90970d-pi"><img alt="image" border="0" height="458" src="/assets/image_740598.jpg" style="background-image: none; padding-left: 0px; padding-right: 0px; display: inline; padding-top: 0px; border-width: 0px;" title="image" width="360" /></a></p>
<p>You may be wondering: “what is the “Security Token”? OK, when accessing <a href="http://salesforce.com/">salesforce.com</a> from outside of your company’s trusted networks, you must add a security token to your password to log in to a desktop client, such as Connect for Outlook, Connect Offline, Connect for Office, Connect for Lotus Notes, or the Data Loader, as well as Force.com IDE. New security tokens are automatically sent to you by mail when your <a href="http://salesforce.com/">salesforce.com</a> password is changed or when you request to reset your security token. If you do not have the token, please visit the <strong>Setup &gt; My Personal Information &gt; Reset Security Token</strong> page.</p>
<p>Once you created your force.com project, Force.com IDE will download the schema from cloud and make a local copy.</p>
<p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b016766630dd9970b-pi"><img alt="image" border="0" height="540" src="/assets/image_485824.jpg" style="background-image: none; padding-left: 0px; padding-right: 0px; display: inline; padding-top: 0px; border-width: 0px;" title="image" width="378" /></a></p>
<p>You will see that all meta data of apex classes, pages, layouts, etc are downloaded,</p>
<p>In <a href="http://adndevblog.typepad.com/autocad/2012/05/integration-of-salesforcecom-and-autocad-ws-part-1.html" target="_blank">last post</a>, we introduced to create a visual force page by inputting the page name in address bar and let the error page to create it automatically. If you prefer to use browser, you can also create a new visualforce page by clicking<strong> YourName &gt; Setup &gt; Develop &gt; Pages</strong> and click “New” button.</p>
<p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b0168eb64f1a5970c-pi"><img alt="image" border="0" height="301" src="/assets/image_817333.jpg" style="background-image: none; padding-left: 0px; padding-right: 0px; display: inline; padding-top: 0px; border-width: 0px;" title="image" width="234" /></a></p>
<p>In Force.com IDE, it can be done by <strong>File &gt; New &gt; Visualforce page</strong>, input the label and name and click “Finish” button, force.com IDE will communicate with server, and a new vf page is created on cloud.</p>
<p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b0163056f0ed7970d-pi"><img alt="image" border="0" height="303" src="/assets/image_545585.jpg" style="background-image: none; padding-left: 0px; padding-right: 0px; display: inline; padding-top: 0px; border-width: 0px;" title="image" width="424" /></a></p>
<p>Similarly, you can create apex classes in Force.com IDE and start programming. Every time you save your doc, it will be complied and save up to cloud if everything is OK. Please pay attention to the left top, you will be notified if something is wrong, and the page is just saved locally.</p>
<p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b0163056f0eee970d-pi"><img alt="image" border="0" height="128" src="/assets/image_999311.jpg" style="background-image: none; padding-left: 0px; padding-right: 0px; display: inline; padding-top: 0px; border-width: 0px;" title="image" width="304" /></a></p>
<p>You can still use browser to open the visual force page to check whether it works as expected or not, in input the address&#0160; <a href="https://c.&lt;yourinstance&gt;.visual.force.com/apex/&lt;yourpagename&gt;">.visual.force.com/apex/&quot;&gt;.visual.force.com/apex/&quot;&gt;.visual.force.com/apex/&quot;&gt;https://c.&lt;yourinstance&gt;.visual.force.com/apex/&lt;yourpagename&gt;</a>.</p>
<p>To create an apex class, we choose <strong>File(or right click package explorer) &gt; new Apex Class</strong> in Force.com IDE. It also can be done in browser, clicking <strong>YourName &gt; Setup &gt; Develop &gt; Apex Classes</strong> and click “New” button.</p>
<p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b0167666a1d02970b-pi"><img alt="image" border="0" height="242" src="/assets/image_78601.jpg" style="background-image: none; padding-left: 0px; padding-right: 0px; display: inline; padding-top: 0px; border-width: 0px;" title="image" width="291" /></a>&#0160;</p>
<p>Force.com Apex Code is an object oriented programming language that allows developers to develop on-demand business applications on the Force.com platform. We need to create some visual force pages and some Apex classes to get our job done. I will introduce them in latter parts.</p>
<p>With that we are drawing to the end of this post, in next post, I will introduce how to create more useful visual force page.</p>
<p>Stay tuned and have fun!</p>
