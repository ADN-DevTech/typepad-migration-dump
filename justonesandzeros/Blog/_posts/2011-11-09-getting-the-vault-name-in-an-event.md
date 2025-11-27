---
layout: "post"
title: "Getting the Vault name in an Event"
date: "2011-11-09 13:02:52"
author: "Doug Redmond"
categories:
  - "Tips and Tricks"
original_url: "https://justonesandzeros.typepad.com/blog/2011/11/getting-the-vault-name-in-an-event.html "
typepad_basename: "getting-the-vault-name-in-an-event"
typepad_status: "Publish"
---

<p><img alt="" src="/assets/TipsAndTricks3.png" /></p>
<p>Many people have noticed that with Web Service Command Events, there is no way to tell the name of the Vault you are connected to.&#0160; You can get a context to make API calls, but the Vault name is not part of the context.&#0160; It&#39;s a limitation of the architecture.</p>
<p>So here is my suggested workaround:&#0160; Create a Vault Option called &quot;VaultName&quot; and store the name there.&#0160; That way, your code can call KnowledgeVaultService﻿.GetVaultOption(&quot;VaultName&quot;) to get the Vault name.&#0160;</p>
<p>Of course, the option has to be set up ahead of time, like during the install or the initial configuration of your app.</p>
<p>&#0160;</p>
<p><em>PS. Sorry for the short article. I&#39;m busy finishing up the materials for </em><a href="http://au.autodesk.com/?nd=event_class&amp;session_id=9305&amp;jid=1750118" target="_blank"><em>my AU class on events</em></a><em>.</em></p>
<p><img alt="" src="/assets/TipsAndTricks3-1.png" /></p>
