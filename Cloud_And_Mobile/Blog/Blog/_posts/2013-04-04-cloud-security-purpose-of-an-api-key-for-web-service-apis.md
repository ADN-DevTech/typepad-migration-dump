---
layout: "post"
title: "Cloud Security: Purpose of an API Key for web service APIs"
date: "2013-04-04 18:46:34"
author: "Gopinath Taget"
categories:
  - "Cloud"
  - "Security"
original_url: "https://adndevblog.typepad.com/cloud_and_mobile/2013/04/cloud-security-purpose-of-an-api-key-for-web-service-apis.html "
typepad_basename: "cloud-security-purpose-of-an-api-key-for-web-service-apis"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/cloud_and_mobile/gopinath-taget.html">Gopinath Taget</a></p>  <p>There are several useful web services out there that expose APIs like the <a href="https://developers.facebook.com/">Facebook APIs</a>, <a href="https://dev.twitter.com/">Twitter APIs</a>, <a href="https://developers.google.com/+/api/">Google Plus APIs</a>, <a href="https://us.etrade.com/active-trading/api">Etrade APIs</a> and so on.</p>  <p>Most of these web platforms do not expose their web service APIs freely. They require you to register as a developer and are issued an API Key (Eg: <a href="https://dev.twitter.com/docs/faq#7447">twitter</a>, <a href="https://developers.google.com/api-client-library/python/guide/aaa_apikeys">google</a>).</p>  <p>I have been trying to understand how the API keys work and if they hold any value as a security mechanism. The short answer is, using API keys as a security mechanism is a bad idea. This is because there is no easy way to hide an API key. Consider this: You write a .NET based desktop application that accesses twitter accounts and displays tweets associated with an account. For this your .NET application makes use of the twitter API. But to use this API, your application needs to use API keys.</p>  <p>However, this means the API key needs to be distributed with your application to your clients. You could try to secure it by encrypting it or embedding it in a binary file etc. But a determined developer with reasonable skills will be able to get to your key eventually. </p>  <p>So what then is the purpose of the API key? From what I have learnt so far, the API key is used more as a tool to collect metrics and identify the application that is making API calls. This is so the web service provider can determine how much their service is being used and by whom. This could help them charge the application user based on how much of the service (storage, compute power, network traffic etc) is being used.</p>
