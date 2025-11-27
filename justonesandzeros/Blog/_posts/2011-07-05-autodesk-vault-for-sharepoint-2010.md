---
layout: "post"
title: "Autodesk Vault for SharePoint 2010"
date: "2011-07-05 16:51:28"
author: "Doug Redmond"
categories:
  - "Concepts"
original_url: "https://justonesandzeros.typepad.com/blog/2011/07/autodesk-vault-for-sharepoint-2010.html "
typepad_basename: "autodesk-vault-for-sharepoint-2010"
typepad_status: "Publish"
---

<p><img alt="" src="/assets/Concepts3.png" /></p>
<p>Normally I don&#39;t comment on <a href="http://www.businesswire.com/news/home/20110629005584/en/Autodesk-Vault-Works-Microsoft-SharePoint-2010-Provide">press releases</a> or <a href="http://underthehood-autodesk.typepad.com/blog/2011/06/new-videos-autodesk-vault-and-microsoft-sharepoint-2010-integration.html">demo videos</a>, but the SharePoint integration is different.&#0160; I was personally involved in this project, so I&#39;d like to share some of the technical details.</p>
<p><strong>The Project:</strong></p>
<p>This integration was a joint effort between Autodesk and Microsoft.&#0160; Overall, I loved working with the Microsoft team.&#0160; After all, my last name <em>is</em> Redmond.&#0160; They helped develop some of the features and were great at getting me up to speed on SharePoint development.</p>
<p><strong>The Architecture:</strong></p>
<p>The Vault integration is built on a SharePoint API piece called Business Connectivity Services (BCS).&#0160; The quick definition is that BCS makes data from an outside system look like SharePoint data so that the SharePoint framework can work with it.&#0160; In this case, Vault is the outside system.</p>
<p>The Vault integration contains no web parts.&#0160; Everything is done through BCS and standard SharePoint components.&#0160; This means that the Vault data can hook into existing SharePoint features.&#0160; As shown in the videos, this feature hooks into the list and workflow features.&#0160; The end users may not even realize that they are working with Vault data.</p>
<p><strong>Making it Your Own:</strong></p>
<p>Autodesk Vault for SharePoint 2010 comes with some basic functionality out of the box.&#0160; But the real power will probably come from customizations.&#0160; SharePoint has a dizzying amount of customization features from simple to complex.&#0160; By using BCS technology, Vault data can hook into many types of customizations, from basic lists to complex web parts.</p>
<p>I personally can&#39;t wait to see the type of things people do with the Vault integration.&#0160; I&#39;ll probably create some sample apps of my own just for fun.&#0160; So check back often.</p>
<p><img alt="" src="/assets/Concepts3-1.png" /></p>
