---
layout: "post"
title: "Using a Named User License with a Custom Web Application"
date: "2021-12-10 02:36:18"
author: "Sajith Subramanian"
categories:
  - "Sajith Subramanian"
  - "Vault"
original_url: "https://adndevblog.typepad.com/manufacturing/2021/12/using-a-named-user-license-with-a-custom-web-application.html "
typepad_basename: "using-a-named-user-license-with-a-custom-web-application"
typepad_status: "Publish"
---

<p><p>By <a href="http://adndevblog.typepad.com/manufacturing/sajith-subramanian.html" target="_self">Sajith Subramanian</a></p><p>Users having a Named User license and working with a custom web application, can now login into Vault 
with their Named User license, with the existing Standard / Windows authentication. </p><p>With the Vault 2021.2 update onwards, the <strong>LogInWithUserLicense</strong>() method was added to support this workflow.</p>
<p>The sample code syntax for this method would be similar to:</p><p><pre>VDF.Vault.Results.LoginResult results = VDF.Vault.Library.ConnectionManager.LogInWithUserLicense(<br>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; tbServerName.Text, tbVaultName.Text, tbUserName.Text, tbPassword.Text, <strong>Token</strong>, VDF.Vault.Currency.Connections.AuthenticationFlags.Standard, null);<br>
m_conn = results.Connection;<br></pre>
<p>The above method takes in an additional input parameter "Token". You can refer to the Forge documentation on how to generate this token:<br>
<a href="https://forge.autodesk.com/en/docs/oauth/v2/tutorials/get-3-legged-token/">https://forge.autodesk.com/en/docs/oauth/v2/tutorials/get-3-legged-token/</a></p>
<p>The Vault SDK also contains a ready to compile sample that demonstrates this workflow. On a default installation of Vault, you can find it here:<br><em>
C:\Program Files\Autodesk\Autodesk Vault 2022 SDK\VS19\CSharp\WebAppUserLicense</em><br>
Do take a look at the included <em>Readme.txt</em> file that contains additional details to help build and run this sample.</p>
