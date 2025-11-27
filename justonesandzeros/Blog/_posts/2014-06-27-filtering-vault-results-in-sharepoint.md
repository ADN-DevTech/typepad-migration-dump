---
layout: "post"
title: "Filtering Vault content in SharePoint"
date: "2014-06-27 18:17:43"
author: "Doug Redmond"
categories:
  - "Concepts"
  - "Vault"
original_url: "https://justonesandzeros.typepad.com/blog/2014/06/filtering-vault-results-in-sharepoint.html "
typepad_basename: "filtering-vault-results-in-sharepoint"
typepad_status: "Publish"
---

<p><img alt="" src="/assets/Vault4.png" /> <br /><img alt="" src="/assets/Concepts4.png" /></p>
<p>One of the biggest confusions about the Vault/SharePoint integrations is how filtering and paging works.&#0160; It confuses me from time to time, and I wrote the integration.&#0160; In this article, I’ll try to describe what’s going on behind-the-scenes to give you a better picture of the integration and how to configure it correctly.&#0160; Ahh, the joys of middleware.</p>
<hr noshade="noshade" style="color: #5acb04;" />
<p>Let’s start with a SharePoint list showing some Vault data.&#0160;</p>
<p><a href="http://justonesandzeros.typepad.com/images/2014/SharepointFilter/list.png" target="_blank"><img alt="" src="/assets/list_scaled.png" /></a><span style="color: #666666;">(click image for full view)</span></p>
<p>Take a minute to guess how that data got from Vault to SharePoint.&#0160; Whatever you came up with in your head is probably wrong.&#0160; I’m not saying you are dumb or anything; I’m saying that the workflow is counter-intuitive.</p>
<p>Basically there are three components in play here:&#0160; SharePoint, Vault and the integration between the two.&#0160;</p>
<p><img alt="" src="/assets/integration.png" /></p>
<p>When the user goes to view the list, SharePoint invokes the Integration, the Integration invokes Vault.&#0160; The data is then pass up through the Integration back to SharePoint.&#0160; Pretty standard so far.</p>
<p>My example screenshot shows 10 items on the page.&#0160; So you would think that 10 objects flowed through the Integration.&#0160; But that’s not what happened.&#0160; The fact that SharePoint is showing 10 rows/page is a <strong>view setting</strong>, which include page size, column layout, sorting, etc.</p>
<p>Here is the important thing that you need to understand:</p>
<table border="1" cellpadding="2" cellspacing="0" width="470">
<tbody>
<tr>
<td valign="top" width="470"><strong>The integration does not know what the view settings are.</strong></td>
</tr>
</tbody>
</table>
<p>For example, the Vault Integration does not know to ask Vault for 30 objects.&#0160; It also doesn&#39;t know, which columns to sort by, which page the user is viewing or any of the other things in the view model.</p>
<hr noshade="noshade" style="color: #5acb04;" />
<p>So, what does the Integration use as criteria when it pulls data from the Vault?&#0160; The answer is <strong>Data Source Filters</strong>.&#0160; These are the only things that SharePoint communicates to the Integration.&#0160; The filters themselves are defined by the integration and will be different depending on the Vault object being shown.&#0160; In this example, the Integration defined four filters.&#0160; The values are configurable, but you can&#39;t add or remove filters.</p>
<p><a href="http://justonesandzeros.typepad.com/images/2014/SharepointFilter/settings.png" target="_blank"><img alt="" src="/assets/settings_scaled.png" /></a> <br /><span style="color: #666666;">(click image for full view)</span></p>
<p>Basically, the integration only gets to see the part in red.&#0160; The rest of the list settings are the SharePoint’s view model.</p>
<p>Let’s take a look at the <strong>Limit</strong> filter.&#0160; This one is important because that’s how many objects the integration asks for.&#0160; It has nothing to do with how much SharePoint shows on the screen.&#0160; If you leave this field blank, the Integration uses the <strong>default value of 100</strong>.&#0160; You can set the limit higher if you want, but that may slow things down.</p>
<p>The other filter values are pretty straightforward.&#0160; Set the State to “Released” and the integration will ask for only Released objects in the Vault.&#0160; Set the Vault Folder to “$/Projects” and only files under that folder will be returned from Vault.</p>
<p>When the Vault Objects are handed from the Integration over to SharePoint, the view settings are then applied.&#0160; This includes sorting and filtering.&#0160; If there are 100 Vault objects, but the view has only 10 items on a page, then the other 90 objects are discarded.&#0160;</p>
<p>In the end, you have two levels of filtering:</p>
<ul>
<li><strong>First Level</strong> - Data that the Integration requests from Vault.&#0160; This is controlled by the Data Source Filters.</li>
<li><strong>Second Level</strong> - SharePoint view settings.&#0160; This is controlled by the other SharePoint settings.</li>
</ul>
<hr noshade="noshade" style="color: #5acb04;" />
<p>Let’s walk through the whole process, just to drive the point home...</p>
<ol>
<li>A SharePoint user views a Vault list.</li>
<li>SharePoint invokes the Integration, passing in the Data Source Filters for that list view. <em>(ex. limit=100)</em></li>
<li>Integration queries objects in Vault, based on the Data Source Filter values.&#0160; <em>(ex. the Vault query is limited to 100 results)</em></li>
<li>Vault returns the object set.&#0160; <em>(ex. 100 objects)</em></li>
<li>Integration passes the object set back to SharePoint.&#0160; <em>(ex. 100 objects)</em></li>
<li>SharePoint constructs the view, discarding anything not in the view. <em>(ex. display first 10 rows sorted by name, discard other 90 objects)</em></li>
</ol><hr noshade="noshade" style="color: #5acb04;" />
