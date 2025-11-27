---
layout: "post"
title: "Exchange Apps &ndash; Information for Vault developers"
date: "2012-07-17 19:34:15"
author: "Madhukar Moogala"
categories:
  - "Announcements"
  - "Vault"
original_url: "https://adndevblog.typepad.com/manufacturing/2012/07/exchange-apps-information-for-vault-developers.html "
typepad_basename: "exchange-apps-information-for-vault-developers"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/stephen-preston.html">Stephen Preston</a></p>
<p>This guide is for developers and content providers new to publishing plug-ins and other content on Autodesk® Exchange Apps – either free, trial or for fee versions. It outlines best practice guidelines and a few requirements for publishers to follow when creating products for the Autodesk Exchange Apps. These guidelines are designed to ensure that users on Autodesk Exchange have a consistent experience when downloading multiple products from the store.</p>
<p><strong><span style="font-size: medium;">Requirements</span></strong></p>
<table border="0" cellpadding="0" cellspacing="0">
<tbody>
<tr>
<td valign="top" width="686">
<p>You will be presented with a detailed list of requirements for publishing on Exchange when you first register to be a publisher. The information that follows is a summary. If there are any differences, then the online Publisher Agreement takes precedence.</p>
<h5><span style="font-weight: bold;"><em>All content types</em></span></h5>
<p>Most of the information we need from you is collected via the web form you complete when submitting your content. This includes gathering information to auto-generate a HTML ‘quick start’ page that is included with the download of your product and viewable online. Other requirements are:</p>
<ul>
<li>Your product must be relevant to (and usable with) Vault 2013 onwards, and must run on any Windows operating system supported by Vault 2013 (including 32- and 64- bit versions). </li>
<li>Your product must work with any higher tiered version of the Vault product. For example if your application works with Vault Workgroup it must also work with Vault Collaboration and Professional.</li>
<li>All applications should interact with Vault only through the public API (as defined by the Vault SDK). There should be no direct interaction with server data such as the database tables or filestore folders.</li>
<li>The information you provide on the ‘documentation’ form when you submit your app is used to create a standard format HTML page. This information must allow the user to quickly understand how to use your product. You can reference additional information (for example, additional helpfiles posted on your website) from the standard HTML documentation. This HTML page will be populated using information you provide when submitting your product to Exchange Apps. </li>
<li>If you don’t use the standard installer template we provide, or if your installer or product requires elevated user privileges (greater than a Windows 7 Standard User) to install, then this must be very clearly documented in the description of your product displayed on the Store. </li>
<li>Your product must be ‘ready to go’ as soon as it’s installed. It must not require the user to manually copy or register files, or manually edit settings (such as support paths). </li>
<li>Your product should be stable, and not behave or alter the behavior of Vault in a way that we deem unsuitable (for example, blocking standard functionality, adding functionality already existing in a higher tier of Vault, blocking the functionality of another plug-in, causing data loss etc.). </li>
<li>Applications that check out and check in files should be preserving meta-data in the case of Autodesk file types.</li>
<li>All extensions for Vault 2013 and higher should have a strong name</li>
<li>All applications should work in and be compatible with a multi-site or replicated environment.</li>
</ul>
<h5><span style="font-weight: bold;"><em>Standalone applications and other content</em></span></h5>
<p>There are no additional requirements for products that are not integrated with Vault. If you wonder what kinds of products this might include – consider eBooks, video tutorials, industry specific calculators, connectors to Cloud based services and the like.</p>
</td>
</tr>
</tbody>
</table>
<p><strong>&#0160;</strong></p>
<p><strong><span style="font-size: medium;">More information</span></strong></p>
<p><strong>&#0160;</strong></p>
<p>The ADN team is here to help you be a successful publisher on Autodesk Exchange store. We’ll do whatever we can do to help you. You are welcome to email <a href="mailto:appsinfo@autodesk.com">appsinfo@autodesk.com</a> if you have any further questions after reviewing these guidelines and the other documentation on <a href="http://www.autodesk.com/developapps">www.autodesk.com/developapps</a>.</p>
<p>Thank you for participating on Autodesk Apps.</p>
