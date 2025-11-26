---
layout: "post"
title: "Using 3 legged OAuth for Forge API's with Postman"
date: "2016-06-27 12:44:00"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "Web Development"
original_url: "https://adndevblog.typepad.com/cloud_and_mobile/2016/06/using-3-legged-oauth-for-forge-apis-with-postman.html "
typepad_basename: "using-3-legged-oauth-for-forge-apis-with-postman"
typepad_status: "Publish"
---

<p>By&#0160;<a href="http://adndevblog.typepad.com/cloud_and_mobile/adam-nagy.html" target="_self">Adam Nagy</a></p>
<p>Already mentioned <a href="https://www.getpostman.com/">Postman</a> in a previous blog post. Now I&#39;ll provide the steps to test the <strong>Autodesk</strong>&#0160;<a href="https://developer.autodesk.com/">Forge API&#39;s</a> using <a href="http://oauthbible.com/#oauth-2-three-legged">3 legged authentication</a> inside <strong>Postman</strong>.</p>
<p>First you need to create a <strong>Forge</strong> application with the correct &quot;<strong>Callback URL</strong>&quot;: <a href="https://www.getpostman.com/oauth2/callback">https://www.getpostman.com/oauth2/callback<br /></a>Just go to <a href="https://developer.autodesk.com/">https://developer.autodesk.com/</a>, log in with your <strong>Autodesk ID</strong> and create a new app:</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c872f414970b-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="CreateApp" class="asset  asset-image at-xid-6a0167607c2431970b01b7c872f414970b img-responsive" src="/assets/image_4002ff.jpg" title="CreateApp" /></a></p>
<p>Then you can select the <strong>API</strong>&#39;s you want to use and also provide information about the app. Here just make sure that you set the &quot;<strong>Callback URL</strong>&quot; correctly:</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d1fcb9a1970c-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="CallbackUrl" class="asset  asset-image at-xid-6a0167607c2431970b01b8d1fcb9a1970c img-responsive" src="/assets/image_df249a.jpg" title="CallbackUrl" /></a></p>
<p>Now you can use the credentials of the app with <strong>Postman</strong>. Just click the &quot;<strong>Authorization</strong>&quot; tab and select &quot;<strong>OAuth 2.0</strong>&quot; as &quot;<strong>Type</strong>&quot; then click &quot;<strong>Get New Access Token</strong>&quot;.&#0160;</p>
<p>You should use the following values:<br />- <strong>Token Name</strong> = You can choose anything here<br />-<strong> Auth URL</strong> =&#0160;https://developer.api.autodesk.com/authentication/v1/authorize<br />- <strong>Access Token URL</strong> = https://developer.api.autodesk.com/authentication/v1/gettoken<br />- <strong>Client ID</strong> = <strong>Client ID</strong> of your <strong>Forge</strong> app<br />- <strong>Client Secret</strong> = <strong>Client Secret</strong> of your <strong>Forge</strong> app<br />- <strong>Scope</strong> = the list of values depend on the functionality of the <strong>Forge API</strong>&#39;s you want to use later on<br />- <strong>Grant Type</strong> = Authorization Code</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d1fcbb49970c-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="GetNewToken" class="asset  asset-image at-xid-6a0167607c2431970b01b8d1fcbb49970c img-responsive" src="/assets/image_b2140a.jpg" title="GetNewToken" /></a></p>
<p>Now you can just click &quot;<strong>Request Token</strong>&quot; to get a token which then will show up under the &quot;<strong>Existing Tokens</strong>&quot; section, and you can start using them in your requests.&#0160;When you click &quot;<strong>Use Token</strong>&quot; then its value will be automatically added to the header of the request:</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb09165e63970d-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="UseToken" class="asset  asset-image at-xid-6a0167607c2431970b01bb09165e63970d img-responsive" src="/assets/image_835d92.jpg" title="UseToken" /></a></p>
<p>Now you can just click &quot;<strong>Send</strong>&quot; to test the request using the newly created access token.</p>
<p>That should be it :)</p>
