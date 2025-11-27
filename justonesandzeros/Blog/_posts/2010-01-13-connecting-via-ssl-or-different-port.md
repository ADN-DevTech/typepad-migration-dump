---
layout: "post"
title: "Connecting via SSL or different port"
date: "2010-01-13 07:36:25"
author: "Doug Redmond"
categories:
  - "Tips and Tricks"
original_url: "https://justonesandzeros.typepad.com/blog/2010/01/connecting-via-ssl-or-different-port.html "
typepad_basename: "connecting-via-ssl-or-different-port"
typepad_status: "Publish"
---

<p><img src="/assets/TipsAndTricks2.png" /> </p> <p>Client-server communication in Vault is done via HTTP connections.&#0160; This means that Vault can be set up with various HTTP features.&#0160; Some of those features may keep your client from connecting, so let&#39;s go over how to handle things when the server is on SSL or a non-standard port. </p> <p><strong>Connecting on a different port</strong></p> <p>You can change your IIS settings to use a different port than 80.&#0160; At the API level, you handle this when you set the URL on your web service object.&#0160; For example, if you are using port 2112, you would set your DocumentService.URL value to <font color="#008080">http://servername:2112/AutodeskDM/Services/DocumentService.asmx</font></p> <p>For login dialogs, Autodesk clients use the convention of adding the port to the end of the server name.&#0160; For example, &quot;servername:2112&quot;.</p> <p><strong>Connecting via SSL</strong></p> <p>If you have your server configured for SSL, you still handle things with the URL property.&#0160; In this case your, DocumentService.URL would have the value of <font color="#008080">https://servername/AutodeskDM/Services/DocumentService.asmx</font></p> <p>For login dialogs, the Autodesk convention is to have &quot;https://&quot; before the server name.&#0160; For example, &quot;https://servername&quot;.</p> <p><strong>Connecting via SSL on a different port</strong></p> <p>The default SSL port is 443.&#0160; If you are using SSL on a different port, you can just combine the rules from above.&#0160; For example:&#0160; <font color="#008080">https://servername:2112/AutodeskDM/Services/DocumentService.asmx</font></p> <p><strong>Source Code</strong></p> <p>The VaultFileBrowser sample in the SDK handles these cases.&#0160; So feel free to borrow that code.&#0160; The login dialog assumes the same server name syntax as the rest of the Autodesk clients.</p>
