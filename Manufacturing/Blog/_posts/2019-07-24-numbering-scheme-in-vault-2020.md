---
layout: "post"
title: "Numbering Scheme in Vault 2020"
date: "2019-07-24 02:52:00"
author: "Sajith Subramanian"
categories:
  - "Sajith Subramanian"
  - "Vault"
original_url: "https://adndevblog.typepad.com/manufacturing/2019/07/numbering-scheme-in-vault-2020.html "
typepad_basename: "numbering-scheme-in-vault-2020"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/manufacturing/sajith-subramanian.html" target="_self">Sajith Subramanian</a></p><p>We’ve had several queries regarding numbering schemes, and how the methods related to numbering schemes have been shown as removed from the various services in the API help documentation.</p><p><a href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a499c06b200d-pi"><img width="699" height="304" title="image" style="border: 0px currentcolor; border-image: none; display: inline; background-image: none;" alt="image" src="/assets/image_c33c27.jpg" border="0"></a></p><p>The reason behind this is, in Vault 2020, Numbering Scheme methods have now been consolidated into a single Numbering Service as compared to earlier Vault versions where Files, Items and ChangeOrders each had their own implementations of Numbering service related methods.</p><p>
So now, all Numbering Scheme functionality that was previously available, carries over into this new Service and can be accessed from a single Numbering Service.</p><p>For example, while migrating your earlier code to Vault 2020, the older method to fetch Numbering schemes which would be something as:</p><pre>NumSchm[] schemes = connection.WebServiceManager.DocumentService.GetNumberingSchemesByType(NumSchmType.ApplicationDefault);</pre><p>would now be changed to something as:</p><pre>NumSchm[] schemes = connection.WebServiceManager.NumberingService.GetNumberingSchemes(EntityClassId , NumSchmType.ApplicationDefault); // pass in the appropriate entity class ID</pre>
