---
layout: "post"
title: "The Defined API"
date: "2013-11-01 09:07:08"
author: "Doug Redmond"
categories:
  - "Concepts"
  - "PLM 360"
original_url: "https://justonesandzeros.typepad.com/blog/2013/11/the-defined-api.html "
typepad_basename: "the-defined-api"
typepad_status: "Publish"
---

<p><img alt="" src="/assets/PLM4.png" />    <br /><img alt="" src="/assets/Concepts4.png" />     </p>
<p>When it comes to API/customization features, I think of things in terms of 3 buckets:&#0160; Supported, Unsupported, and Forbidden.&#0160; Let me explain what these things mean and how they apply to the PLM 360 API.&#0160; </p>
<hr noshade="noshade" style="color: #5acb04;" />
<p><strong>Supported</strong> means it’s publically documented.&#0160; If you run into difficulties, you can expect help.&#0160; If you run into defects, you can expect them to be addressed.&#0160; If an API changes, you can expect proper notice.&#0160; </p>
<p>In the case of PLM 360, it’s easy to tell what is supported.&#0160; </p>
<table border="1" cellpadding="2" cellspacing="0" width="470">
<tbody>
<tr>
<td valign="top" width="470"><strong>If it’s part of the </strong><a href="http://help.autodesk.com/view/PLM/ENU/"><strong>public documentation</strong></a><strong>, then it’s supported.&#0160; If it’s not part of the public documentation, then it’s not supported.</strong></td>
</tr>
</tbody>
</table>
<br />  
<hr noshade="noshade" style="color: #5acb04;" />
<p><strong>Unsupported</strong> means “use at your own risk”.&#0160; Undocumented functions are the best examples of this category.&#0160; It’s something that you technically can use, but don’t expect help or documentation.&#0160; Things can change without notice.</p>
<p>In the cloud world, I highly recommend you avoid using unsupported APIs.&#0160; For an installed product, these features are less risky because you control the upgrade.&#0160; You are control when the upgrade happens and can roll back if something goes wrong.&#0160; PLM 360 doesn’t give you that flexibility.&#0160; If you use an unsupported API, you may find that your app, which was working on Friday, doesn’t work any more on Monday morning.</p>
<p>For PLM 360, the undocumented REST URLs fall into this category.&#0160; There <em>are</em> actually parts of REST v2 that are undocumented, so don’t assume that everything with <strong>/api/v2</strong> in the URL is supported.</p>
<hr noshade="noshade" style="color: #5acb04;" />
<p><strong>Forbidden </strong>means we at Autodesk don’t want you touching a certain part of the system.&#0160; This includes things that violate the license agreement (like reverse engineering) and things that violate data integrity (database modification in Vault).&#0160; </p>
<p>For the PLM 360 API, forbidden mainly applies to security features and things that are blatantly illegal.&#0160; Attempts to circumvent our security system and misuse of user data fall under this category.&#0160; If you are ever in doubt, <a href="http://forums.autodesk.com/t5/PLM-360-General/bd-p/705" target="_self">ask</a>.&#0160; We will be happy to point you in the right direction.</p>
<hr noshade="noshade" style="color: #5acb04;" />
