---
layout: "post"
title: "Vault Integration Pattern - Provide a View"
date: "2012-12-18 10:10:20"
author: "Doug Redmond"
categories:
  - "Concepts"
original_url: "https://justonesandzeros.typepad.com/blog/2012/12/vault-integration-pattern-provide-a-view.html "
typepad_basename: "vault-integration-pattern-provide-a-view"
typepad_status: "Publish"
---

<p><img alt="" src="/assets/Concepts3.png" /></p>
<p>By far, the most common use of the Vault API is to provide some sort of integrations with something else.&#0160; Sure there are some good utilities up on the <a href="http://apps.exchange.autodesk.com/VLTC/Home/Index" target="_blank">App Store</a>, but they don’t represent the majority of the cases.&#0160; Consulting is where it’s at, and there are a surprising number of systems out there to connect Vault to.</p>
<p>Each integration is different, so there is no way for me to tell you everything you ever need to know.&#0160; However, there are certain patterns that emerge, and it’s the patterns that I will be focusing on.</p>
<p>The first pattern is the one I used when designing the <a href="http://justonesandzeros.typepad.com/blog/2011/07/autodesk-vault-for-sharepoint-2010.html" target="_blank">SharePoint 2010 integration</a>.&#0160; <strong>One system provides a view of data in another system</strong>.&#0160; In this case, SharePoint 2010 provides a view Vault data.</p>
<p>If you do it right, the end user should not know (and not care) that the data is coming from an outside system.&#0160; The SP integration does this very well.&#0160; Here is a game you can play.&#0160; Look at the following video and try to figure out what is the SharePoint data and what is the Vault data.</p>
<iframe allowfullscreen="allowfullscreen" frameborder="0" height="315" src="http://www.youtube.com/embed/ekIvy94C538" width="420"></iframe>
<p>One cool thing about BCS, the SP feature I used for the integration, is that it doesn’t just provide a view of external data.&#0160; BCS allows you to <strong>link</strong> SharePoint data with external data.&#0160; This is a pretty useful feature for integrations like this.&#0160; If you can’t link the data between the two systems, then the integration loses a lot of value.&#0160; The end user may as well just use two separate applications.</p>
<hr noshade="noshade" style="color: #5acb04;" />
<p>I’ll end the article with some quick bullets on the advantages and disadvantages of this pattern.&#0160; If I left anything out, please let me know in the comments section.</p>
<p><strong>Advantages:</strong></p>
<ul>
<li><strong>No duplication of data</strong> - Everything is just cleaner when data lives in one and only one place.&#0160; You don’t have to worry about stale data, and you don’t have to worry about conflicts </li>
</ul>
<p><strong>Disadvantages:</strong></p>
<ul>
<li><strong>Performance</strong> - Constantly pulling data from another source is time consuming.&#0160; No matter how much you optimize things, it will never be as fast as having the data in the same system.&#0160; For SharePoint, we found that the distance between the SP server and the Vault server was the biggest factor in how quick the Vault pages came up in SP. </li>
<li><strong>Network Dependency</strong> - If the network ever goes down between the two systems, the integration is effectively broken.&#0160; People who rely on the data will no longer be able to access it. </li>
</ul>
<p><img alt="" src="/assets/Concepts3-1.png" /></p>
