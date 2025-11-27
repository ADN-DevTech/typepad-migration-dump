---
layout: "post"
title: "User-less Login to PLM 360"
date: "2014-02-06 14:44:23"
author: "Doug Redmond"
categories:
  - "PLM 360"
  - "Tips and Tricks"
original_url: "https://justonesandzeros.typepad.com/blog/2014/02/user-less-login-to-plm-360.html "
typepad_basename: "user-less-login-to-plm-360"
typepad_status: "Publish"
---

<p><img alt="" src="/assets/PLM4.png" /> <br /><img alt="" src="/assets/TipsAndTricks4.png" /></p>
<p>Let’s say you have a service that synchronizes data to PLM 360 in the background.&#0160; It’s a headless system, so there is no UI.&#0160; The problem is that PLM uses 3-legged OAuth 1.0, and “human” is one of the 3 legs.&#0160; There are lots of blockers to prevent a non-human from authenticating.&#0160; For example, there is no API to pass in the username and password.</p>
<p>The solution is to log in once as a human and save off the OAuth access token.&#0160; From an OAuth point of view, the access token is just as good as being logged in to the system.&#0160; This means that a background service can use that access token instead of needing to store username/password data.</p>
<p>Access tokens from Autodesk have an active lifetime of 2 days, but they can be renewed up to 14 days after they have been issued.&#0160; When an access token is renewed, a new token issued and the old one is obsolete.&#0160; This process can be continued indefinitely.&#0160; The only time a human would have to get involved is if the 14 day period expires before a token can get renewed.&#0160; In these cases, a human would again need to log in manually and store off the access token.</p>
<hr noshade="noshade" style="color: #d09219;" />
<p>Let’s go over a specific example in PLM 360.&#0160; In step <a href="http://help.autodesk.com/view/PLM/ENU/?guid=GUID-752816A2-0100-4A56-A023-2108075CD012" target="_blank">5 of the authentication process</a>, the human user has already logged in and your app gets the access token.&#0160; If you want to do an auto-login in the future, you should store off some of the data coming back from the OAuth provider.&#0160;</p>
<table border="1" cellpadding="2" cellspacing="0" width="470">
<tbody>
<tr>
<td valign="top" width="470">
<p><span style="color: #c0504d;">oauth_token=KLe2eC3792uDhz1rnznFVjr9ibI%3D <br /></span>&amp;<span style="color: #c0504d;">oauth_token_secret=7nxoZ0MXDIdx25xjqIRnMLgzvCs%3D <br /></span>&amp;<span style="color: #c0504d;">oauth_session_handle=CuXWXi9fQ4JIKs%2FxksFdeWLqDCs%3D <br /></span>&amp;<span style="color: #c0504d;">oauth_authorization_expires_in=1209599 <br /></span>&amp;<span style="color: #c0504d;">oauth_expires_in=172799 <br /></span>&amp;x_oauth_user_name=Doe.J <br />&amp;x_oauth_user_guid=200815661123455 <br />&amp;x_consumerscope=https%253A%252F%252Fmysite.autodeskplm360.net</p>
</td>
</tr>
</tbody>
</table>
<p>Here is a summary of the stuff we care about:</p>
<ul>
<li><strong>oauth_token</strong> - The Access Token public key.</li>
<li><strong>oauth_token_secret</strong> - The Access Token secret key.</li>
<li><strong>oauth_session_handle</strong> - The session handle.&#0160; Needed to renew or force expire an access token.</li>
<li><strong>oauth_expires_in</strong> - How long, in seconds, the access token will continue allowing access.</li>
<li><strong>oauth_authorization_expires_in</strong> - How long, in seconds, the access token will allow a renewal.</li>
</ul>
<p>You should store off the oauth_token, oauth_secret, oauth_session_handle, oauth_authorization_expires_in and oauth_expires_in.&#0160; You should also record the timestamp of when the token was issued.&#0160; The timestamp allows the application to calculate if a token is still active, needs renewing or is past the renewal point.</p>
<p>NOTE:&#0160; Make sure to store off the Access Token in a secure manner.&#0160; A compromised token is not as bad as a compromised username/password, but an attacker can still do a lot of damage with just the Access Token.</p>
<p>Now your app has a way to log in to PLM without user intervention.&#0160; It can just read in the saved OAuth data and continue to the <a href="http://help.autodesk.com/view/PLM/ENU/?guid=GUID-9D1F0E16-57E3-40B9-8987-EB617C4FCD76" target="_blank">next step</a> in the authentication process.&#0160; The part that involved a human has already passed, so things can be automated from here on out.</p>
<hr noshade="noshade" style="color: #d09219;" />
<p>When you want to renew your token, you will again call POST on https://accounts.autodesk.com/<strong>OAuth/AccessToken</strong>, but the input parameters are a bit different then when you first get the token. The oauth_token should be the current access token and the oauth_session_handle should be passed in too.&#0160; No verifier is needed.&#0160; The request should be signed by your Consumer Secret and Access Secret.</p>
<p>Example Request: </p>
<table border="1" cellpadding="2" cellspacing="0" width="470">
<tbody>
<tr>
<td valign="top" width="470">
<p>oauth_nonce=d5baf3f6-5c2c-461e-8340-26f885c80640 <br />oauth_version=1.0 <br />oauth_signature_method=HMAC-SHA1 <br />oauth_consumer_key=213f6e53-19fa-4b22-9bfb-5215fbfa7a93 <br />oauth_token=36+dZ74Ig72QxyGtCA8mGJKrCxM= <br />oauth_timestamp=1375903880 <br />oauth_signature=XK45m8XQE9MLVOz734VDwzbb6Ox <br /><span style="color: #c0504d;">oauth_session_handle=wr00U6DPDT8sDgiqYspXw4oIp/s=</span></p>
</td>
</tr>
</tbody>
</table>
<p>The response looks the same as when the token was first issued, but there are now new values for the Access Token, Access Secret and Session Handle. The old values are now invalid and cannot be used to access PLM 360.</p>
<hr noshade="noshade" style="color: #d09219;" />
<p>Lastly, there may be cases where you want to invalidate the access token <em>right now</em>.&#0160; You don’t want to wait 2 days for it to expire.&#0160; You can do this by making a POST call to</p>
<p>https://accounts.autodesk.com/<strong>OAuth/InvalidateToken</strong>. As usual, this must be a signed request (use your Consumer Secret and Access Secret), with a bunch of oauth parameters passed in. It also requires that you pass in the session handle from the last /OAuth/AccessToken call.</p>
<p>Example Request: </p>
<table border="1" cellpadding="2" cellspacing="0" width="470">
<tbody>
<tr>
<td valign="top" width="470">
<p>oauth_nonce=d5baf3f6-5c2c-461e-8340-26f885c80640 <br />oauth_version=1.0 <br />oauth_signature_method=HMAC-SHA1 <br />oauth_consumer_key=213f6e53-19fa-4b22-9bfb-5215fbfa7a93 <br />oauth_token=36+dZ74Ig72QxyGtCA8mGJKrCxM= <br />oauth_timestamp=1375903880 <br />oauth_signature=XK45m8XQE9MLVOz734VDwzbb6Ox <br />oauth_session_handle=wr00U6DPDT8sDgiqYspXw4oIp/s=</p>
</td>
</tr>
</tbody>
</table>
<p>If the call is successful, the Access Token, Access Secret and Session Handle are invalid and cannot be renewed.</p>
<hr noshade="noshade" style="color: #d09219;" />
