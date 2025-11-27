---
layout: "post"
title: "Migrating add-in to latest product version"
date: "2025-02-14 06:30:33"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "Inventor"
  - "Vault"
original_url: "https://adndevblog.typepad.com/manufacturing/2025/02/migrating-add-in-to-latest-product-version.html "
typepad_basename: "migrating-add-in-to-latest-product-version"
typepad_status: "Publish"
---

<p>In the case of <strong>Inventor</strong> for many years the migration effort has been rather low - often you didn&#39;t even have to touch your <strong>add-in</strong>, it just worked with the new release as well.</p>
<p>Every now and then, however, we introduce changes that require testing and even rethink of certain parts of the code.</p>
<p>One such big change was moving from <strong>LOD</strong> (Level Of Detail) in <strong>Inventor 2021</strong> to <strong>Model States </strong>in <strong>Inventor 2022</strong>. The changes not only affected code directly working with <strong>LOD</strong>, but other parts too:<br /><a href="https://adndevblog.typepad.com/manufacturing/2022/05/porting-guide-from-level-of-details-to-model-states.html">Porting guide from Level of Details to Model states</a></p>
<p>Now we had another one: moving from <strong>.NET Framework 4.8</strong> in <strong>Inventor 2024</strong> to <strong>.NET 8</strong> in <strong>Inventor 2025</strong> - other products like <strong>Vault</strong> did the same.<br />If you are relying on libraries that do not work with <strong>.NET 8</strong>, that can be a problem.<br />It&#39;s worth checking, however, if your existing <strong>.NET Framework 4.8</strong> add-in works with <strong>Inventor 2025</strong> - it might, which could give you some extra time to migrate.<br /><a href="https://www.youtube.com/watch?v=RyHx5-CqKZM">Autodesk Desktop API Updates: .NET Core 8.0 Migration | Webinar recording</a><br /><a href="https://blogs.autodesk.com/vault/2024/04/autodesk-vault-2025-sdk-breaking-news/">Autodesk Vault 2025 SDK – Breaking News</a></p>
<p>If you are going from <strong>Inventor 2021</strong> (or before) to <strong>Inventor 2025</strong> you&#39;ll have to deal with both changes at the same time, which could be a big effort.</p>
<p>The bottom line of all the above is that <strong>Inventor</strong> developers should be testing their <strong>add-in</strong> in the latest versions, and don&#39;t just assume or hope that things will work.<br />You can get ahead of things by testing your <strong>add-in</strong> with <strong>Inventor</strong> <strong>betas</strong> and <strong>release candidates</strong> available on the <a href="https://feedback.autodesk.com/">feedback site</a>, months before the final product is published.&#0160;<br /><br />In general with <strong>Autodesk</strong> add-in and plug-ins it&#39;s very important to plan for migration activities before rolling out the tools in a large production environment. Without adequate testing in a &quot;sandbox&quot; environment, you can end up with incompatibilities and other issues after the rollout.&#0160;</p>
<p>The <strong>API</strong> documentation contains a <strong>What&#39;s New</strong> section that can be a good starting point when migrating to the new release. In the case of <strong>Inventor 2025</strong> you can find it <a href="https://help.autodesk.com/view/INVNTOR/2025/ENU/?guid=WhatsNew">here</a>.<br />You can find the same for <strong>Vault 2025</strong> <a href="https://adndevblog.typepad.com/manufacturing/2025/02/migrating-add-in-to-latest-product-version.html">here</a> - if you have developed solutions for that, those too should be tested for each new release.</p>
<p>If you&#39;re an <a href="https://aps.autodesk.com/adn">ADN member</a> then you can get additional licenses for development purposes at no extra cost which could also be useful to create a dedicated testing environment.</p>
