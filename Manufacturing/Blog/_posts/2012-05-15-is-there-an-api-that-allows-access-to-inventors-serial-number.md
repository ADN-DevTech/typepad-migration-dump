---
layout: "post"
title: "Is there an API that allows access to Inventor's serial number?"
date: "2012-05-15 07:11:13"
author: "Gary Wassell"
categories:
  - "Gary Wassell"
  - "Inventor"
original_url: "https://adndevblog.typepad.com/manufacturing/2012/05/is-there-an-api-that-allows-access-to-inventors-serial-number.html "
typepad_basename: "is-there-an-api-that-allows-access-to-inventors-serial-number"
typepad_status: "Publish"
---

<p><strong>Issue</strong></p>
<p>Can I use the API to access Inventor&#39;s serial number?</p>
<p><a name="section2"></a></p>
<p><strong>Solution</strong></p>
<p>Currently there is no access to Inventor&#39;s serial number from Inventor&#39;s API.&#0160; Inventor serial numbers are stored in the Registry. However access to the Registry is provided for in Visual Basic and Visual C++ (consult the documentation for your platform of choice).</p>
<p>The area of the Registry that holds the serial number for Inventor is HKEY_LOCAL_MACHINE\SOFTWARE\Autodesk\Inventor\RegistryVersion12.0\System\License.</p>
<p>In that sub folder you should look for a value name like &quot;...:SerialNumber&quot; then the value data member contains the actual serial number.</p>
