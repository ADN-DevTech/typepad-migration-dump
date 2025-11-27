---
layout: "post"
title: "Google Map Extension"
date: "2011-03-22 16:26:54"
author: "Doug Redmond"
categories:
  - "Sample Applications"
original_url: "https://justonesandzeros.typepad.com/blog/2011/03/google-map-extension.html "
typepad_basename: "google-map-extension"
typepad_status: "Publish"
---

<p><img alt="" src="https://justonesandzeros.typepad.com/images/2011/4apps/4 apps-title2.png" /></p>
<p>To celebrate the release of the Vault 2012, I will be releasing 1 sample app per week for the next 4 weeks.</p>
<p>Let&#39;s start things off with the <strong>Google Map Extension for Autodesk Vault</strong>.&#0160; This one is authored by <strong>Gavin Guo</strong>, one of the developers on Vault.</p>
<p><a href="http://justonesandzeros.typepad.com/images/2011/GoogleMap/tabView.png" target="_blank"><img alt="" border="0" src="/assets/tabView_scaled.png" style="border: 0px;" /></a> <br /><span style="color: #000000; font-size: xx-small;">(click image for full view)</span></p>
<p><img alt="" src="https://justonesandzeros.typepad.com/images/2011/4apps/4 apps-hr2.png" /></p>
<p><strong>Summary: <br /></strong>The Google Map Extension for Autodesk Vault allows you to set user defined property (UDP) values containing location information for files and folders in Vault.&#0160; You can then view and update these files and folders using Google Map technology.</p>
<p><a href="http://justonesandzeros.typepad.com/images/2011/GoogleMap/historicalVersions.png" target="_blank"><img alt="" border="0" src="/assets/historicalVersions_scaled.png" style="border: 0px;" /></a> <br /><span style="color: #000000; font-size: xx-small;">(click image for full view)</span></p>
<p><img alt="" src="https://justonesandzeros.typepad.com/images/2011/4apps/4 apps-hr2.png" /></p>
<p><strong>Features:</strong></p>
<ul>
<li>View and edit location information for files and folders in Vault.</li>
<li>View single objects in the custom tab.</li>
<li>View multiple objects using the Show Google Map command.</li>
<li>Use either an address or a latitude/longitude point to set the location.</li>
<li>Works with the GeoRSS property in AutoCAD drawings.</li>
<li>Customize the information displayed on the Google Map marker.</li>
</ul>
<p><img alt="" src="https://justonesandzeros.typepad.com/images/2011/4apps/4 apps-hr2.png" /></p>
<p><strong>Requirements:</strong></p>
<ul>
<li>Vault Workgroup 2012, Vault Collaboration 2012 or Vault Professional 2012</li>
</ul>
<p><a href="http://justonesandzeros.typepad.com/Apps/GoogleMap/GoogleMapExt-1.0.2.0-bin.zip" target="_blank">Click here to download application</a> <br /><a href="http://justonesandzeros.typepad.com/Apps/GoogleMap/GoogleMapExt-1.0.2.0-src.zip" target="_blank">Click here to download source code</a></p>
<p><img alt="" src="https://justonesandzeros.typepad.com/images/2011/4apps/4 apps-hr2.png" /></p>
<p><strong>To Configure: <br /></strong>Before the extension can be used, a Vault administrator needs to configure some settings.</p>
<ol>
<li>Download the application.</li>
<li>Unzip and run the installer.</li>
<li>Run Vault Explorer and log in as an Administrator.</li>
<li>Under the Tools menu.&#0160; Select Administration-&gt;Vault Settings.</li>
<li>Select the Behaviors tab.</li>
<li>Click the Properties button.</li>
<li>Decide which UDP you want to use to store Address information and which UDP you want to store latitude/longitude information.&#0160; Either create new properties or decide on existing ones.</li>
<li>Make sure that the UDPs are associated to at least File and Folder types. <br /><img alt="" src="/assets/UDPsetup.png" /> </li>
<li>Exit out the the Property Definitions window and the Vault Settings window.</li>
<li>Select Tools-&gt;Configure Google Map</li>
<li>Select the UDPs you want to address and coordinate properties.&#0160; Select the key property, which is what Google Map will use to set the markers.&#0160; Select the coordinate format and select the properties you want to show up in the map markers (thumbnails are supported). <br /><img alt="" src="/assets/ConfigDialog_scaled.png" />&#0160; </li>
<li>Click OK to save.&#0160; At this point the extension has been configured.</li>
</ol>
<p><img alt="" src="https://justonesandzeros.typepad.com/images/2011/4apps/4 apps-hr2.png" /></p>
<p><strong>To Use:</strong></p>
<ol>
<li>Download the application.</li>
<li>Unzip and run the installer.</li>
<li>Run Vault Explorer.</li>
<li>Select a file or folder.&#0160; </li>
<li>Update either the address or coordinate property, depending on which is the key.<br /><img alt="" src="/assets/PropEdit.png" /></li>
<li>Save the property update.</li>
<li>Select the update object and select the Google Map tab.&#0160; You should see a map correlating to the property data.</li>
<li>To update, you can drag and drop the map marker to a different location and select Update Map Information.</li>
<li>To show multiple objects on a single map, multi-select all the files and folders you want to display on any Vault grid.&#0160; Next, right-click and select Show Google Map command.</li>
</ol>
<p><span style="font-size: xx-small;">As with all the samples on this site, the </span><a href="http://justonesandzeros.typepad.com/blog/disclaimer.html"><span style="font-size: xx-small;">legal disclaimer</span></a><span style="font-size: xx-small;"> applies. </span></p>
<p><img alt="" src="https://justonesandzeros.typepad.com/images/2011/4apps/4 apps-hr2.png" /></p>
<p><strong></strong>1 app down, 3 apps to go.&#0160; Keep checking back for more apps and Vault 2012 API details.</p>
