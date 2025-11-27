---
layout: "post"
title: "VDF Client Properties"
date: "2014-01-03 10:27:09"
author: "Doug Redmond"
categories:
  - "Concepts"
  - "Vault"
original_url: "https://justonesandzeros.typepad.com/blog/2014/01/vdf-client-properties.html "
typepad_basename: "vdf-client-properties"
typepad_status: "Publish"
---

<p><img alt="" src="/assets/Vault4.png" /> <br /><img alt="" src="/assets/Concepts4.png" /></p>
<p>The VDF tracks two types of properties: <strong>client properties</strong> and <strong>server properties</strong>.&#0160; If you have been using the web service API for a while, you are probably already familiar with server properties.&#0160; But client properties is a new concept in the Vault API.</p>
<p>Both client and server properties are displayed in the Vault grid controls.&#0160; As an end user, you probably didn’t care which was which.&#0160; But if you program with the VDF, you will start to notice and appreciate the differences.</p>
<hr noshade="noshade" style="color: #5acb04;" />
<p><strong>Server Properties</strong></p>
<p>These are properties that live on the server.&#0160; If you want to interact with these properties, it involves a web service call.&#0160; Likewise, adding a new property is done by making web service calls.&#0160; A server property value will be the same for all users.&#0160; Lastly, server properties are indexed and can be used in a search.</p>
<hr noshade="noshade" style="color: #5acb04;" />
<p><strong>Client Properties</strong></p>
<p>These properties are set by the client and are not known by the server.&#0160; Since the server doesn’t know about these properties, it can’t index or search on them.&#0160; You interact with client properties through the VDF.&#0160; You can add your own properties by implementing an interface and registering it with the VDF.&#0160; I’ll cover that in another article.&#0160; </p>
<p>Client properties may have different values depending on the user.&#0160; The file status icon is a good example.&#0160; One user may see a file as &quot;out of date&quot; while the other sees it as &quot;no local file&quot;. &#0160; </p>
<p>You can tell if a property is a client or server property by checking <strong>IsCalculated</strong> on <strong>PropertyDefinition</strong>.&#0160; That’s the VDF PropertyDefinition class, which is in Autodesk.DataManagement.Client.Framework.Vault.Currency.Properties.&#0160; Not to be confused with the web service class in Autodesk.Connectivity.WebServices.PropDef.&#0160; Anyway, if IsCalculated is true, then it’s a client property.</p>
<hr noshade="noshade" style="color: #5acb04;" />
<p><strong>Uses for Client Properties</strong></p>
<p>Since you can define your own client properties, the possibilities are limitless.&#0160; Out of the box, here are some of the ways that client properties are used.</p>
<p><span style="text-decoration: underline;">Mixing client and server data</span> - The algorithm involves grabbing some server data, cross-referencing it with local data and displaying a value unique to the current user.&#0160; Examples include the File Status Icon and the File Extension Icon.</p>
<p><span style="text-decoration: underline;">Modification of server properties</span> - This is basically a server property displayed it a different way.&#0160; Examples include (Date Only) and (Time Only) properties and the Entity Icon.</p>
<p><span style="text-decoration: underline;">Display of non-property server data</span> - Sometimes data from a server is not a property but you want to show it in the grid.&#0160; The Path property is a good example.</p>
<hr noshade="noshade" style="color: #5acb04;" />
