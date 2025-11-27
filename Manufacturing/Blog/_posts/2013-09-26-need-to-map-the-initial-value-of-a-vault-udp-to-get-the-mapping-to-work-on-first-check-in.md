---
layout: "post"
title: "Need to map the Initial Value of a Vault UDP to get the mapping to work on first Check In"
date: "2013-09-26 09:55:33"
author: "Wayne Brill"
categories:
  - "Vault"
  - "Wayne Brill"
original_url: "https://adndevblog.typepad.com/manufacturing/2013/09/need-to-map-the-initial-value-of-a-vault-udp-to-get-the-mapping-to-work-on-first-check-in.html "
typepad_basename: "need-to-map-the-initial-value-of-a-vault-udp-to-get-the-mapping-to-work-on-first-check-in"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/manufacturing/wayne-brill.html" target="_self">Wayne Brill</a></p>  <p>The initial value of a user defined property that is mapped to an iProperty in an Inventor file is not filled in properly when the file is first checked in unless the “Initial Value” of the UDP is also mapped. This screenshot of Vault Explorer shows the edit dialog with the Initial Value being mapped.&#160; </p>  <p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b019aff9f299b970c-pi"><img style="background-image: none; border-right-width: 0px; padding-left: 0px; padding-right: 0px; display: inline; border-top-width: 0px; border-bottom-width: 0px; border-left-width: 0px; padding-top: 0px" title="image" border="0" alt="image" src="/assets/image_10c229.jpg" width="524" height="359" /></a></p>  <p>Here is the mapping of the UDP. Notice that this is the mapping on the “Mapping” tab while the “Initial Value” is on the “Settings” tab. </p>  <p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b019aff9fa5da970d-pi"><img style="background-image: none; border-right-width: 0px; padding-left: 0px; padding-right: 0px; display: inline; border-top-width: 0px; border-bottom-width: 0px; border-left-width: 0px; padding-top: 0px" title="image" border="0" alt="image" src="/assets/image_44bcda.jpg" width="522" height="196" /></a></p>  <p>With the initial value set on the settings tab the UDPs are mapped when the Inventor file is first added to the vault: </p>  <p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b019aff9fa5e7970d-pi"><img style="background-image: none; border-right-width: 0px; padding-left: 0px; padding-right: 0px; display: inline; border-top-width: 0px; border-bottom-width: 0px; border-left-width: 0px; padding-top: 0px" title="image" border="0" alt="image" src="/assets/image_eb7868.jpg" width="529" height="249" /></a></p>  <p>If the “Initial Value” of the UDP is not mapped, you have to edit the properties and manually add them. After doing this the UDPs will be mapped and updated when the file is checked out and checked back in. To get it to work when the file is first added to the vault remember to map the “Initial Value” too.</p>
