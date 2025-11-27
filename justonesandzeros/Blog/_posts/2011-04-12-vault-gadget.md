---
layout: "post"
title: "Vault Gadget"
date: "2011-04-12 08:05:03"
author: "Doug Redmond"
categories:
  - "Sample Applications"
original_url: "https://justonesandzeros.typepad.com/blog/2011/04/vault-gadget.html "
typepad_basename: "vault-gadget"
typepad_status: "Publish"
---

<p><img alt="" src="https://justonesandzeros.typepad.com/images/2011/4apps/4 apps-title2.png" /></p>
<p>The worklist feature in Vault Professional is a nice feature.&#0160; It pops up information informing you of any change orders you need to take action on.&#0160; The only problem is that you need to launch the Vault Professional client to get that feature.&#0160; Chris Sawicki and <a href="http://underthehood-autodesk.typepad.com/">Brian Schanen</a> decided to change that with <strong>Vault Gadget</strong>.</p>
<p><a href="http://justonesandzeros.typepad.com/images/2011/VaultGadget/desktop-full.png" target="_blank"><img alt="" border="0" src="/assets/desktop-scaled.png" /></a> <br /><span style="font-size: xx-small;">(click image for full view)</span></p>
<p>Vault Gadget allows you to view your worklist from your Windows desktop, without the need to run Vault Professional.&#0160;</p>
<p><img alt="" src="https://justonesandzeros.typepad.com/images/2011/4apps/4 apps-hr2.png" /></p>
<p><strong>Requirements:</strong></p>
<ul>
<li>Vault Professional 2012 </li>
</ul>
<p><a href="http://justonesandzeros.typepad.com/Apps/VaultGadget/VaultGadget-1.0.1.0.zip" target="_blank">Click here to download</a></p>
<p><img alt="" src="https://justonesandzeros.typepad.com/images/2011/4apps/4 apps-hr2.png" /></p>
<p><strong>To use: </strong></p>
<ol>
<li>Extract the contents of the ZIP file to “%localappdata%\Microsoft\Windows Sidebar\Gadgets”. The result of this step is a folder named VaultCO.Gadget in the “%localappdata%\Microsoft\Windows Sidebar\Gadgets” folder. </li>
<li>Right-click on the desktop and select the “Gadgets” menu item. The Gadget Gallery will be displayed. </li>
<li>Find the “Vault CO Worklist” gadget (you may have to page through the gallery to find it). Click on the gadget and drag it onto your desktop. The gadget will appear on the desktop and error information will be displayed.</li>
<li>Enter in your Vault connection settings.</li>
</ol>
<p><img alt="" src="https://justonesandzeros.typepad.com/images/2011/4apps/4 apps-hr2.png" /></p>
<p><strong>Features:</strong></p>
<ul>
<li>See the change orders in your worklist without running Vault Professional. </li>
<li>Refresh timer - adjust how often Vault Gadget checks for worklist items. </li>
<li>Click on a change order to view the properties. </li>
<li>Click on the &quot;Go To CO&quot; link to launch Vault Professional and navigate to the Change Order. </li>
</ul>
<p><img alt="" src="/assets/propertiesView.png" /></p>
<p><img alt="" src="https://justonesandzeros.typepad.com/images/2011/4apps/4 apps-hr2.png" /></p>
<p><strong>Source Code:      <br /></strong>Vault Gadget is a set of HTML pages with embedded javascript.&#0160; So the application <em>is</em> the source code.&#0160; Just use an HTML editor to see how it works.&#0160; Communication with the Vault Server is very low level in this example.&#0160; The javascript is creating and parsing the raw XML.&#0160;</p>
<p><span style="font-size: xx-small;">As with all the samples on this site, the </span><a href="http://justonesandzeros.typepad.com/blog/disclaimer.html"><span style="font-size: xx-small;">legal disclaimer</span></a><span style="font-size: xx-small;"> applies. </span></p>
<p><img alt="" src="https://justonesandzeros.typepad.com/images/2011/4apps/4 apps-hr2.png" /></p>
<p>4 weeks have passed and, as promised, 4 apps have been posted.&#0160; Special thanks to all the members of the Vault team that submitted apps.&#0160; We now return you to your regular Vault API blog.</p>
