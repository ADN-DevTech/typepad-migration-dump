---
layout: "post"
title: "How to protect my intellectual property of my App on Autodesk Exchange &ndash; part 1"
date: "2014-03-25 01:39:11"
author: "Daniel Du"
categories:
  - "Cloud"
  - "Daniel Du"
original_url: "https://adndevblog.typepad.com/cloud_and_mobile/2014/03/how-to-protect-my-intellectual-property-of-my-app-on-autodesk-exchange-part-1.html "
typepad_basename: "how-to-protect-my-intellectual-property-of-my-app-on-autodesk-exchange-part-1"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/cloud_and_mobile/daniel-du.html">Daniel Du</a></p>
<p>As a publisher selling Apps/Web services on Autodesk Exchange store, are you worrying about the license protection issue? As addressed in <a href="http://usa.autodesk.com/adsk/servlet/item?siteID=123112&amp;id=20149725">Exchange FAQ</a>, copy protection or license management is the responsibility of the Publisher. Now I am pleased to say that we are trying to help you with that. Exchange Store now have a new REST API to check the entitlement of an user. The new API is as below:</p>
<p>Base URL: <a href="https://apps.exchange.autodesk.com" title="https://apps.exchange.autodesk.com">https://apps.exchange.autodesk.com</a></p>
<p>End Point: webservices/checkentitlement</p>
<p>Http Method: GET</p>
<p>Parameters: ?userid=***&amp;appid=***</p>
<p>Return : Json object.</p>
<p>Here is a sample request URL: <a href="https://apps.exchange.autodesk.com/webservices/checkentitlement?userid=2N5FMZW9CCED&amp;appid=appstore.exchange.autodesk.com%3aautodesk360%3aen" title="https://apps.exchange.autodesk.com/webservices/checkentitlement?userid=2N9FMZW4CCED&amp;appid=appstore.exchange.autodesk.com%3aautodesk360%3aen">https://apps.exchange.autodesk.com/webservices/checkentitlement?userid=2N5FMZW9CCED&amp;appid=appstore.exchange.autodesk.com%3aautodesk360%3aen</a></p>
<p>The return Json is :</p>
<pre>{
  &quot;UserId&quot;:&quot;2N5FMZW9CCED&quot;,
  &quot;AppId&quot;:&quot;appstore.exchange.autodesk.com:autodesk360:en&quot;,
  &quot;IsValid&quot;:false,
  &quot;Message&quot;:&quot;Ok&quot;
}</pre>
<p>IsValid: If user is entitled to access an app, which means he bought it from Exchange Store and has already paid for it, then the IsValid value is “true”, otherwise is “false”.</p>
<p>Message:&#0160; <br />&#0160; “OK”&#0160; - current call is correct <br />&#0160;&#0160; “Invalid parameters(s)” – userid or appid is not set, please note the userID is the internal ID(GUID), which is not the meaningful user ID when you login Autodesk products or website. <br />&#0160;&#0160; “Please use https”&#0160; - the request is not using https</p>
<p>You can get your appId from the index page URL of your app, but you may wonder how can I know customer’s userId? This user ID is end user’s internal user ID.</p>
<p>As you know, most Autodesk desktop products allow users login with their Autodesk ID. For AutoCAD or vertical product, their is an easy way to get current login user’s username and userid, you can use following undocumented system variables: ONLINEUSERNAME and ONLINEUSERID. But for other products, like Revit or Inventor, you will have to implement the login process with Autodesk <a href="http://oauth.net/">OAuth</a>, to get the user ID. We will talk about Autodesk OAuth latter.</p>
<p>&#0160;<a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01a73d99161a970d-pi"><img alt="clip_image002" border="0" height="103" src="/assets/image_fe6900.jpg" style="display: inline; border: 0px;" title="clip_image002" width="442" /></a></p>
<p>For web services, if you are publishing a new web service on Exchange, you will noticed that you can specify login type of your web service. By selecting “Sign in with Autodesk account”, you customer can sign in your web service with their Autodesk ID, of cause, you need to implement Autodesk <a href="http://oauth.net/">OAuth</a>.</p>
<p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01a3fcde3eef970b-pi"><img alt="image" border="0" height="73" src="/assets/image_f2b608.jpg" style="display: inline; border-width: 0px;" title="image" width="479" /></a></p>
<p>Autodesk provides OAuth API, you can rely on Autodesk to do the authentication without maintaining your own user system. You can refer to our <a href="https://github.com/ADN-DevTech/AutodeskOAuthSamples">github site</a>&#0160; for samples of Autodesk OAuth, which is provided by Autodesk ADN. Platforms we have samples for include C# (Windows, ASP.NET and Windows RT), Java (Android), Objective-C (iOS), PHP, Python and JavaScript (Windows RT). However, as proof-of-concept samples, they are not necessarily fully ‘bullet-proof’, so please do not use them directly in your production environment.</p>
<p>Ok, let’s stop here in this part, next part I will introduce how to implement Autodesk OAuth and check user’s entitlement with an ASP.net MVC application.</p>
