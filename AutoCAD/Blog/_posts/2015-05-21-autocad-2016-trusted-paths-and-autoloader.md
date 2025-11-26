---
layout: "post"
title: "AutoCAD 2016: Trusted paths and AutoLoader"
date: "2015-05-21 01:46:49"
author: "Virupaksha Aithal"
categories:
  - "2016"
  - "Virupaksha Aithal"
original_url: "https://adndevblog.typepad.com/autocad/2015/05/autocad-2016-trusted-paths-and-autoloader.html "
typepad_basename: "autocad-2016-trusted-paths-and-autoloader"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/virupaksha-aithal.html" target="_self">Virupaksha Aithal</a></p>
<p>In AutoCAD 2014, we introduced Trusted Locations (TRUSTEDPATHS). &#0160; “Trusted Paths” are in concept a “white list” of locations that the CAD manager can audit and maintain for add-ins and customizations loaded into AutoCAD. &#0160;AutoCAD allows <strong>signed</strong> files to be loaded from outside of this “whitelist” without SECURELOAD warnings, with the exception that AutoCAD 2016 will check if the publisher of the signed app is in the users trusted publisher certificate store. &#0160;The “best practice” is to ensure the “trusted locations” are only writable with Administrator permissions.</p>
<p>Applications utilizing the &quot;autoloader&quot; functionality within AutoCAD have the options to install to the following locations:</p>
<p>%APPDATA%\Autodesk\ApplicationPlugins<br />%ALLUSERSPROFILE%\Autodesk\ApplicationPlugins<br />%ProgramFiles%\Autodesk\ApplicationPlugins <br />%ProgramFiles(x86)%\Autodesk\ApplicationPlugins (In 64-bit OS)</p>
<p>In AutoCAD&#0160;2014 &amp; 2015 - %ALLUSERSPROFILE%\Autodesk\ApplicationPlugins and %Appdata%\Autodesk\ApplicationPlugins – are by default trusted paths.</p>
<p>With AutoCAD 2016, only the Program files folder (C:\Program Files\Autodesk\ApplicationPlugins and C:\Program Files (x86)\Autodesk\ApplicationPlugins ) is trusted by default.<br />This means, when you try to load an <strong>unsigned</strong> add-in from any location outside of the &quot;trusted locations&quot; you will get a warning message like the one shown below. Note that in AutoCAD 2016 the user can choose to “always trust this app” – if they do, the warning will not be triggered again.</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c78fe320970b-pi" style="float: left;"><img alt="Secureload1" class="asset  asset-image at-xid-6a0167607c2431970b01b7c78fe320970b img-responsive" src="/assets/image_509943.jpg" style="margin: 0px 5px 5px 0px;" title="Secureload1" /></a></p>
<p>&#0160;</p>
<p>&#0160;</p>
<p>&#0160;</p>
<p>&#0160;</p>
<p>&#0160;</p>
<p>&#0160;</p>
<p>&#0160;</p>
<p>Signed add-ins with publishers that haven’t been “trusted” by the user will trigger this kind of warning, below. &#0160;Note the user can add the publisher to the certificate store by selecting “always trust applications from…” and then they won’t be asked again for that publisher.</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d119708e970c-pi" style="float: left;"><img alt="Secureload2" class="asset  asset-image at-xid-6a0167607c2431970b01b8d119708e970c img-responsive" src="/assets/image_29164.jpg" style="margin: 0px 5px 5px 0px;" title="Secureload2" /></a></p>
<p>&#0160;</p>
<p>&#0160;</p>
<p>&#0160;</p>
<p>&#0160;</p>
<p>&#0160;</p>
<p>&#0160;</p>
<p>&#0160;</p>
<p>&#0160;</p>
<p>&#0160;</p>
<p>&#0160;</p>
<p><strong>To avoid warnings, you need to:</strong></p>
<p>Sign all your add-in files with your own digital signature and add your certificate to the local machine’s trusted certificates cache. Attaching a digital signature affords a basic level of security to help designate the publisher of the application and to help guarantee that the application hasn&#39;t been tampered with since it was distributed by the signer. We recommend that an app be signed regardless of where it is installed. <br />OR<br />Install to a trusted folder (for example C:\Program Files\Autodesk\ApplicationPlugins.) Note that AutoCAD implicitly trusts the AutoCAD install folder and all subfolders under it and C:\Program Files\Autodesk\ApplicationPlugins and all its subfolders. These are considered &quot;trusted locations.&quot;</p>
<p>It is strongly recommend to sign your add-in as more and more of AutoCAD customers - particularly larger customers – are requiring any files installed on their networks to be signed.</p>
<p>Related blogs:</p>
<p><a href="http://adndevblog.typepad.com/autocad/2015/01/digitally-signing-plug-in-files.html" target="_self">Digitally signing plug-in files</a></p>
<p><a href="http://adndevblog.typepad.com/autocad/2015/04/how-to-avoid-trust-this-publisher-dialog.html" target="_self">Trusted publishers</a></p>
<p><a href="http://adndevblog.typepad.com/autocad/2015/03/autocad-2016.html" target="_self">DevTV AutoCAD 2016</a></p>
<p><a href="http://through-the-interface.typepad.com/through_the_interface/security/" target="_self">Through the Interface – Security</a></p>
<p>Autodesk Help:</p>
<p><a href="http://knowledge.autodesk.com/search-result/caas/CloudHelp/cloudhelp/2016/ENU/AutoCAD-Customization/files/GUID-0A93626D-8389-45FC-969B-B86A2F37D691-htm.html" target="_self">About Digitally Signing Custom Program Files</a></p>
