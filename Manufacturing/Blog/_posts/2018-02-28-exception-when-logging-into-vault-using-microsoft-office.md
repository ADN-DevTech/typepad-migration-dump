---
layout: "post"
title: "Exception when logging into Vault using Microsoft Office"
date: "2018-02-28 22:32:24"
author: "Sajith Subramanian"
categories:
  - "Sajith Subramanian"
  - "Vault"
original_url: "https://adndevblog.typepad.com/manufacturing/2018/02/exception-when-logging-into-vault-using-microsoft-office.html "
typepad_basename: "exception-when-logging-into-vault-using-microsoft-office"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/manufacturing/sajith-subramanian.html" target="_self">Sajith Subramanian</a></p>
<p><p><strong>Issue:</strong> It was recently reported, that when logging into Vault using a Microsoft Office product (e.g. MS Word), the login will fail with the below error.</p><p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c9544920970b-pi"><img width="449" height="160" title="image" style="display: inline; background-image: none;" alt="image" src="/assets/image_61d71e.jpg" border="0"></a></p><p><strong>Cause:</strong> This seems to be caused when deploying a WebServiceExtension Add-in. This issue can be seen, even when using the RestrictOperations sample that comes along with the SDK.</p><p><strong>Resolution:</strong> This issue appears when there is a mismatch between the bit-ness of Office (x64 vs x86) and that of the RestrictOperations extension. E.g. I have Office x86 installed and can see the issue if RestrictOperations is built for x64 -&nbsp; but when RestrictOperations is built for x86 then it runs correctly.&nbsp; </p><p>
Until this is fixed, the simplest solution to this, is to change the Platform Target of the extension project to AnyCPU (see below). When this is done, the extension will subsequently run correctly in both Word (x86) and Vault Explorer (x64) without change.</p><p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c9544924970b-pi"><img width="469" height="242" title="restrictoperations (002)" style="display: inline; background-image: none;" alt="restrictoperations (002)" src="/assets/image_953bb4.jpg" border="0"></a></p>
