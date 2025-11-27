---
layout: "post"
title: "Vault 2014 API Language Pack"
date: "2013-07-02 08:00:00"
author: "Doug Redmond"
categories:
  - "Concepts"
  - "Vault"
original_url: "https://justonesandzeros.typepad.com/blog/2013/07/vault-2014-api-language-pack.html "
typepad_basename: "vault-2014-api-language-pack"
typepad_status: "Publish"
---

<p><img alt="" src="/assets/Vault3.png" /></p>
<p><img alt="" src="/assets/Concepts3.png" /></p>
<p>One of the neat things about the UI components in the SDK is that they are also used by the Vault clients.&#0160; This means that when the product gets localized, the SDK components get localized automatically.</p>
<p>All you need to do is grab the satellite assemblies and distribute them with your app.&#0160; Oh wait, you don’t need to do that.&#0160; Dan Dulzo has already done this for you.&#0160; Thanks Dan.</p>
<p><a href="http://justonesandzeros.typepad.com/Files/LanguagePacks/Vault2014SDKLocalizedResourceDLLs.zip" target="_blank">Click here to download the Vault 2014 API Language Pack</a></p>
<p>This should include all the langauges that Vault supports.&#0160; If we missed one, let us know.</p>
<hr noshade="noshade" style="color: #5acb04;" />
<p><strong>When to use      <br /></strong>You should only use this for EXEs you write or when plugging in to a non-Autodesk application.&#0160; If you are plugging in to an Autodesk product, the localization pack(s) should already be in place.</p>
<p><strong>How to use      <br /></strong>In the zip file are a bunch of folders with language codes for names.&#0160; Grab the folders you want and place them under your EXE folder.&#0160; For example, if your app is installed at c:\myApp\Program.exe, then your German language back would be at c:\myApp\de.</p>
<p>Don’t feel you need to have one language folder.&#0160; Feel free to dump them all in your EXE folder.&#0160; At runtime, Windows will load the appropriate language pack based on the OS settings.&#0160; If your language isn’t in the language pack, the UI defaults to English, but you knew that already.</p>
<p>This language pack only supplies localizations of UI in the Vault SDK.&#0160; Any UI you create yourself will have to be handled by you.&#0160; </p>
<hr noshade="noshade" style="color: #5acb04;" />
<p><strong>Screenshots</strong></p>
<p>Here is the login dialog, which is invoked by the following code:</p>
<p class="MsoNormal" style="line-height: normal; margin: 0in 0in 0pt; mso-layout-grid-align: none;"><span><span style="font-family: Consolas;"><span style="color: #000000;"><span style="font-size: 9.5pt;">connection = VDF.Vault.Forms.</span></span><span style="font-size: 9.5pt;"><span><span style="color: #2b91af;">Library</span></span><span style="color: #000000;">.Login(</span><span><span style="color: #0000ff;">null</span></span><span style="color: #000000;">);</span></span></span></span></p>
<p>English:    <br /><img alt="" src="/assets/login-en.png" /></p>
<p>German:    <br /><img alt="" src="/assets/login-de.png" /></p>
<p>&#0160;</p>
<p>Here is an error dialog, which is being invoked from the following code:    <br /><span style="font-family: Consolas;"><span><span style="color: #0000ff;"><span style="font-size: 9.5pt;">catch</span></span></span><span><span style="font-size: 9.5pt;"><span style="color: #000000;"> (</span><span><span style="color: #2b91af;">Exception</span></span><span style="color: #000000;"> ex)           <br /></span></span></span></span><span><span style="font-family: Consolas;"><span style="font-size: 9.5pt; color: #000000;">{</span></span></span></p>
<p class="MsoNormal" style="line-height: normal; margin: 0in 0in 0pt; mso-layout-grid-align: none;"><span><span style="font-family: Consolas;"><span style="color: #000000;"><span style="mso-spacerun: yes;"><span style="font-size: 9.5pt;">&#0160;&#0160;&#0160; </span></span><span style="font-size: 9.5pt;">VDF.Forms.</span></span><span style="font-size: 9.5pt;"><span><span style="color: #2b91af;">Library</span></span><span style="color: #000000;">.ShowError(ex, ex.Message);</span></span></span></span></p>
<p class="MsoNormal" style="line-height: normal; margin: 0in 0in 0pt; mso-layout-grid-align: none;"><span><span style="font-family: Consolas;"><span style="font-size: 9.5pt; color: #000000;">}</span></span></span></p>
<p>English:    <br /><img alt="" src="/assets/error-en.png" /></p>
<p>German:    <br /><img alt="" src="/assets/error-de.png" /></p>
<p><img alt="" src="/assets/Concepts3-1.png" /></p>
