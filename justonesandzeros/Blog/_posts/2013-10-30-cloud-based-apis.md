---
layout: "post"
title: "Cloud Based APIs"
date: "2013-10-30 09:48:06"
author: "Doug Redmond"
categories:
  - "Concepts"
  - "PLM 360"
original_url: "https://justonesandzeros.typepad.com/blog/2013/10/cloud-based-apis.html "
typepad_basename: "cloud-based-apis"
typepad_status: "Publish"
---

<p><img alt="" src="/assets/PLM4.png" /><img alt="" src="/assets/Concepts4.png" /></p>
<p>Is using a cloud API really any different than any other client/server API?&#0160; If you have developed with the Vault API, then you should already be familiar with many of the core concepts.&#0160; Things like latency, user permissions and multi-user environments all still apply to the cloud.&#0160; So is the cloud just a larger scale version of client/server programming?</p>
<p>No, there are some fundamental differences.&#0160; Differences that need to be accounted for in your code.</p>
<hr noshade="noshade" style="color: #5acb04;" />
<p><strong>Frequent updates, whether you want them or not&#0160; </strong></p>
<p>When a new version of PLM 360 goes live, customers have no choice but to accept the new version.&#0160; For the end-user, this is usually a good thing.&#0160; At the API level, it adds an element of instability.&#0160; Gone are the days where you get your customization running and breathe a sigh of relief, knowing that things are working and stable for the next year or two until the next upgrade.</p>
<p>PLM 360 upgrades are about every 4 weeks, and everyone gets the update.&#0160; These upgrades are not something you can plan for like with installed software.&#0160; For example, a typical Vault upgrade involves the admin testing the new version, then planning when the upgrade should take place.&#0160; PLM 360 is not like that.&#0160; It updates without warning, so you can’t plan for it.&#0160; And you definitely can’t pick the date.</p>
<p>Sometimes a Vault upgrade will go wrong and the customer will decide to stick with the older version for a while longer.&#0160; Which brings me to my next point...</p>
<hr noshade="noshade" style="color: #5acb04;" />
<p><strong>You can’t go back </strong></p>
<p>Once the PLM 360 upgrade happens, that’s it.&#0160; You can’t go back to the older version.&#0160; In fact, the old version doesn’t even exist anymore.</p>
<p>This fact adds an extra complexity to debugging.&#0160; If you notice a new bug, it’s hard to determine if it’s because of a PLM update or if it’s a bug that was always there, and you just never noticed.&#0160; With a desktop app, you could always install the prior version and get a definitive answer, but you don’t get that with the cloud.</p>
<p>Sometimes it’s not a bug, but your app is... different.&#0160; For example, the data is being displayed differently or a command has different results than before... you think.&#0160; Again, it’s hard to know if it’s because of the PLM update or old age.</p>
<hr noshade="noshade" style="color: #5acb04;" />
<p><strong>Tips for working with the cloud</strong></p>
<p>Here are some pieces of advice for working with a cloud API.&#0160; If you have an more advice, let me know in the comment section.</p>
<ul>
<li><strong>Forward Compatibility</strong> - Your code should always expect the unexpected.&#0160; Avoid assumptions as much as possible.&#0160; For example, PLM 360 currently has 6 types of workspaces.&#0160; Next week, it may have 7.&#0160; You code can’t crash if it comes across a workspace type it has never seen before. </li>
<li><strong>Smoke Tests</strong> - Create an automated test of your app’s functionality.&#0160; The advantages are numerous.&#0160; You have an easy way to spot issues before users do.&#0160; You have a mechanism for pinpointing the exact failure.&#0160; Saving the smoke test results gives you a document of how things worked in past PLM 360 versions. </li>
<li><strong>Documentation</strong> - Be thorough in documenting your app’s behavior.&#0160; Record videos if you can.&#0160; If not, then take lots of screenshots.&#0160; Again, the goal is to have something to reference if you believe there is a change in behavior. </li>
</ul>
<hr noshade="noshade" style="color: #5acb04;" />
<p><strong>The Autodesk side of things</strong></p>
<p>Obviously, we here at Autodesk also want your apps to be as stable as possible.&#0160; Here is what you can expect from us.</p>
<ul>
<li><strong>Backward Compatibility</strong> - Just like your app needs to be forward compatible, the PLM 360 API needs to be backward compatible.&#0160; Although content will be added to REST v2, existing content will not be changed or removed.&#0160; If a situation arises where the data model needs to change, that new model will go into REST v3. </li>
<li><strong>Defect Fixing</strong> - If you believe there is a defect in the API, <a href="http://forums.autodesk.com/t5/PLM-360-General/bd-p/705">let us know</a>.&#0160; One of the major advantages of the cloud is the ability for us to fix defects quickly. </li>
<li><strong>Developer Services</strong> - We are working on some initiatives that should alleviate some of the pain points that I mentioned.&#0160; But, I can’t say anything more at this time.&#0160; </li>
</ul>
<hr noshade="noshade" style="color: #5acb04;" />
