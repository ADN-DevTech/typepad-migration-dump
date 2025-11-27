---
layout: "post"
title: "Entitlement API in Inventor"
date: "2015-03-28 10:54:00"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "Inventor"
original_url: "https://adndevblog.typepad.com/manufacturing/2015/03/entitlement-api-in-inventor.html "
typepad_basename: "entitlement-api-in-inventor"
typepad_status: "Publish"
---

<p>By&#0160;<a href="http://adndevblog.typepad.com/manufacturing/adam-nagy.html" target="_self">Adam Nagy</a></p>
<p><strong>Important</strong>: <a href="https://modthemachine.typepad.com/my_weblog/2019/09/entitlement-api-changes-in-inventor-2020.html">Entitlement API changes in Inventor 2020</a></p>
<p>When you write AddIn&#39;s for the exchange store then you&#39;ll probably want to check if the user logged into the <strong>Autodesk</strong> product actually bought your AddIn or just copied it from somewhere. In the <strong>2016</strong> releases you will be able to test this with the help of the functionality provided by the&#0160;<strong>AdWebServices.dll</strong>.</p>
<p>The <strong>API</strong> is in <strong>C++</strong>&#0160;and so can be accessed from both <strong>C++</strong> AddIn&#39;s and other ones using <strong>Platform Invoke</strong> (P/Invoke) - this is how I&#39;m using it from the sample <strong>.NET</strong> AddIn too.</p>
<p>The main function you&#39;ll need is the <strong>GetUserId</strong> which will provide the currently logged in user&#39;s <strong>Internal Autodesk ID</strong>. Then using the&#0160;<a href="http://adndevblog.typepad.com/cloud_and_mobile/2014/03/how-to-protect-my-intellectual-property-of-my-app-on-autodesk-exchange-part-1.html" target="_self">Exchange Store API</a>&#0160;you can find out if the user with the given <strong>Internal Autodesk ID</strong>&#0160;has actually bought your application.</p>
<p>You can find the sample project here: <a href="https://github.com/ADN-DevTech/EntitlementAPI_Inventor" target="_self" title="">https://github.com/ADN-DevTech/EntitlementAPI_Inventor</a>&#0160;</p>
<p>Because of new security measures in <strong>Inventor 2016</strong>, when loading the sample project for the first time you&#39;ll get a dialog like this, where you&#39;ll need to <strong>Allow</strong> the AddIn to load:</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d0f787b8970c-pi" style="display: inline;"><img alt="Entitlement1" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b8d0f787b8970c image-full img-responsive" src="/assets/image_88ea46.jpg" title="Entitlement1" /></a></p>
<p>If there is a user currently logged into <strong>Inventor</strong>, then you&#39;ll get this message:</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c76de90a970b-pi" style="display: inline;"><img alt="Entitlement2" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b7c76de90a970b img-responsive" src="/assets/image_1ef8ac.jpg" title="Entitlement2" /></a></p>
<p>If no user is logged in or something goes wrong then you&#39;ll get this message:</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb0811cfc2970d-pi" style="display: inline;"><img alt="Entitlement3" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01bb0811cfc2970d img-responsive" src="/assets/image_695b4d.jpg" title="Entitlement3" /></a></p>
<p>&#0160;</p>
