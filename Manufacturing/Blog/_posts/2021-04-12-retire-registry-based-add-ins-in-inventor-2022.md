---
layout: "post"
title: "Retire Registry-based Add-ins in Inventor 2022"
date: "2021-04-12 06:04:26"
author: "Chandra Shekar Gopal"
categories:
  - "Chandra Shekar Gopal"
  - "Inventor"
original_url: "https://adndevblog.typepad.com/manufacturing/2021/04/retire-registry-based-add-ins-in-inventor-2022.html "
typepad_basename: "retire-registry-based-add-ins-in-inventor-2022"
typepad_status: "Publish"
---

<p><span style="display: inline !important; float: none; background-color: #ffffff; color: #000000; cursor: text; font-family: Georgia; font-size: 14px; font-style: normal; font-variant: normal; font-weight: 400; letter-spacing: normal; orphans: 2; text-align: left; text-decoration: none; text-indent: 0px; text-transform: none; -webkit-text-stroke-width: 0px; white-space: normal; word-spacing: 0px;">by </span><a href="https://adndevblog.typepad.com/manufacturing/chandra-shekar-gopal.html" rel="noopener" style="color: #0066cc; font-family: Georgia; font-size: 14px; font-style: normal; font-variant: normal; font-weight: 400; letter-spacing: normal; orphans: 2; text-align: left; text-decoration: underline; text-indent: 0px; text-transform: none; -webkit-text-stroke-width: 0px; white-space: normal; word-spacing: 0px;" target="_blank">Chandra shekar Gopal</a>,</p>
<p>Before Inventor 2010, add-ins are loaded into Inventor by running registry mechanism in a command prompt. It was complex procedure. So, the registry free add-in mechanism was introduced into Inventor 2010.</p>
<p>Now in Inventor 2022, the registry based add-ins will not be listed in the Inventor Add-Ins Manager dialog as default. So, it is recommended to convert registry based add-ins into registry free add-ins. The guidelines to convert registered add-ins into registry free add-ins is explained in the below article.</p>
<p><a href="https://help.autodesk.com/view/INVNTOR/2021/ENU/?guid=GUID-CFFA5CC6-38E6-4ACD-A2BC-8B8732727996">https://help.autodesk.com/view/INVNTOR/2021/ENU/?guid=GUID-CFFA5CC6-38E6-4ACD-A2BC-8B8732727996</a></p>
<p>In case, registered add-ins are can&#39;t convert.&#0160; The registered add-ins in current version can be enabled by modifying below registry key.</p>
<p><span style="display: inline !important; float: none; background-color: #ffffff; color: #000000; cursor: text; font-family: Georgia; font-size: 14px; font-style: normal; font-variant: normal; font-weight: 400; letter-spacing: normal; orphans: 2; text-align: left; text-decoration: none; text-indent: 0px; text-transform: none; -webkit-text-stroke-width: 0px; white-space: normal; word-spacing: 0px;">HKEY_CURRENT_USER\Software\Autodesk\Inventor\RegistryVersion26.0\System\Preferences</span><br style="color: #000000; font-family: Georgia; font-size: 14px; font-style: normal; font-variant: normal; font-weight: 400; letter-spacing: normal; orphans: 2; text-align: left; text-decoration: none; text-indent: 0px; text-transform: none; -webkit-text-stroke-width: 0px; white-space: normal; word-spacing: 0px;" /><span style="display: inline !important; float: none; background-color: #ffffff; color: #000000; cursor: text; font-family: Georgia; font-size: 14px; font-style: normal; font-variant: normal; font-weight: 400; letter-spacing: normal; orphans: 2; text-align: left; text-decoration: none; text-indent: 0px; text-transform: none; -webkit-text-stroke-width: 0px; white-space: normal; word-spacing: 0px;">LoadRegisterBasedAddins = dword:00000001</span></p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0263e99d7267200b-pi" style="display: inline;"><img alt="Register Editor" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0263e99d7267200b img-responsive" src="/assets/image_39e409.jpg" title="Register Editor" /></a></p>
<p>&#0160;</p>
<p>&#0160;</p>
