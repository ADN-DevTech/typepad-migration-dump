---
layout: "post"
title: "Exchange Apps &ndash; Information for Revit developers"
date: "2012-07-17 19:36:18"
author: "Madhukar Moogala"
categories:
  - "Announcements"
  - "Revit"
original_url: "https://adndevblog.typepad.com/aec/2012/07/exchange-apps-information-for-revit-developers.html "
typepad_basename: "exchange-apps-information-for-revit-developers"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/stephen-preston.html">Stephen Preston</a></p>
<p>This guide is for developers and content providers new to publishing plug-ins and other content on Autodesk® Exchange Apps – either free, trial or for fee versions. It outlines best practice guidelines and a few requirements for publishers to follow when creating products for the Autodesk Exchange Apps. These guidelines are designed to ensure that users on Autodesk Exchange have a consistent experience when downloading multiple products from the store.</p>
<p><strong><span style="font-size: medium;">Requirements</span></strong></p>
<table border="0" cellpadding="0" cellspacing="0">
<tbody>
<tr>
<td valign="top" width="686">
<p>You will be presented with a detailed list of requirements for publishing on Exchange when you first register to be a publisher on Autodesk Exchange Apps. The information that follows is a summary specific to Revit.</p>
<h5><a name="_For_All_Content"></a><strong><em>For All Content Types</em></strong></h5>
<p>Most of the information we need from you is collected from the product submission form that you complete in the publisher system. This includes gathering information to auto-generate a ‘quick start’ page in HTML format that is included with the download of your product and is viewable online. Other requirements are:</p>
<ul>
<li><strong>Compatibility:</strong> Your product must be relevant to (and usable with) Revit<sup>®</sup> 2013, and must run on any Windows operating system supported by Revit<sup>®</sup> 2013 (including 32- and 64-bit versions). Your product may be compatible with any Revit<sup>®</sup> 2013 based vertical application (i.e., Autodesk<sup>®</sup> Revit<sup>®</sup>, Autodesk<sup>®</sup> Revit<sup>®</sup> Architecture, Autodesk<sup>®</sup> Revit<sup>®</sup> Structure, and Autodesk<sup>®</sup> Revit<sup>®</sup> MEP). If it is not compatible with all Revit verticals, then please specify in your submission which Revit vertical it is compatible with. </li>
<li><strong>HTML help page: </strong>The documentation information you provide as part of the submission process is used to populate a standard, auto-generated HTML page. This information must allow the user to quickly understand how to use your product. You can reference additional information (for example, additional help files posted on your website) from this standard documentation documentation. The auto-generated HTML page will be populated using information you provide when submitting your product to the store – you will be prompted to supply it as part of the submission process. </li>
<li><strong>User privileges:</strong> The default user privilege for the store apps is Windows 7 Standard User. If your product or installer requires elevated user privileges, or if you don’t use the standard installer template we provide, then this must be very clearly documented in the description of your product displayed on the store. </li>
<li><strong>Ready to run:</strong> Your product must be “ready to run” as soon as it’s installed. It must not require the user to manually copy or register files, or manually edit Revit setting (such as support paths). 
<ul>
<li>If you use a licensing system, then it must allow your product to run as soon as it is installed by the user. This means that your application allows either instant activation (e.g., online activation), or full functionality with a time-bombed “grace period” that is long enough for you to send activation information to the customer. </li>
</ul>
</li>
<li><strong>Product Stability</strong>: Your product should be stable, and not behave or alter the behavior of Revit in a way that we deem unsuitable (for example, blocking standard Revit functionality, blocking the functionality of another plug-in, causing data loss, etc.). </li>
</ul>
<h5><a name="_Plug-ins"></a><span style="font-weight: bold;"><em>Plug-ins</em></span></h5>
<p>Additional requirements for plug-ins are:</p>
<ul>
<li>You must use an <strong>add-in manifest</strong> as the loading mechanism. </li>
<li>Your plug-in must include a <strong>ribbon button</strong> to access your main command. If your application has many buttons, you may also choose to have a separate custom ribbon tab.</li>
</ul>
<h5><a name="_Family_Libraries"></a><span style="font-weight: bold;"><em>Family Libraries</em></span></h5>
<p>Additional requirements for family libraries are:</p>
<ul>
<li>Your Family Library must include a ribbon button to the Revit ribbon bar. The ribbon bar UI must either provide access to the family library or launch a help file explaining how to access it. </li>
<li>Your family libraries (in rfa file format) must be installed in the following folder: </li>
</ul>
<blockquote>
<p><strong>Windows 7/Vista</strong>: %PUBLIC%\Documents\Autodesk\Downloaded Content <br />(typically C:\Users\Public\Documents\Autodesk\Downloaded Content).</p>
<p><strong>Windows XP</strong>: %ALLUSERSPROFILE%\Documents\Autodesk\Downloaded Content <br />(typically C:\Documents and Settings\All Users\Documents\Autodesk\Downloaded Content)</p>
</blockquote>
<blockquote>
<p>Note: This is to be consistent with other Autodesk products - for a repeatable cross-product user experience (for example, when using products in a Suite).</p>
<p>Again, we’ll create the installer for you and we can help you modify your libraries so that they work in this new location.</p>
</blockquote>
<h5><a name="_Standalone_Applications_and"></a><span style="font-weight: bold;"><em>Standalone Applications and Other Contents</em></span></h5>
<p>There are no additional requirements for products that are not integrated with Revit. If you wonder what kinds of products this might include – consider eBooks, video tutorials, industry specific calculators, connectors to Cloud based services and the like.</p>
</td>
</tr>
</tbody>
</table>
<p><strong>&#0160;</strong></p>
<p><strong><span style="font-size: medium;">Guidelines</span></strong></p>
<p><strong>&#0160;</strong></p>
<h5><span style="font-weight: bold;"><em>Use of the Add-in Manifest </em></span></h5>
<p>As a default location, we’ll be using per-user location to place the add-in manifest:</p>
<blockquote>
<p>%appdata%\Autodesk\Revit\Addins\2013</p>
</blockquote>
<p>In addition to the add-in manifest, a folder containing all the supporting materials that your plugin-in needs will also be placed in the same location. The name of the folder is exactly the same as the name of the add-in manifest except that it has “.bundle” instead of “.addin”. e.g.:</p>
<p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01774368a694970d-pi"><img alt="image001" border="0" height="57" src="/assets/image_353029.jpg" style="background-image: none; padding-left: 0px; padding-right: 0px; display: block; float: none; margin-left: auto; margin-right: auto; padding-top: 0px; border-width: 0px;" title="image001" width="244" /></a></p>
<p>The Add-in manifest accepts relative path in the &lt;Assembly/&gt; tag:</p>
<blockquote>
<p>&lt;Assembly&gt;.\ADNPlugin-RoomRenumbering.bundle\ADNPlugin-RoomRenumbering.dll&lt;/Assembly&gt;</p>
</blockquote>
<p>In this way, your plug-in can be simply installed by copying files without requiring the add-in manifest to be modified during installation.</p>
<p>To see how this looks, you can download some of the free plug-ins already available on Autodesk Exchange store and study their format. For example, the following Autodesk Plugins of the Month are currently available:</p>
<ul>
<li>Duct Fitting Table Viewer </li>
<li>Filer Upgrader </li>
<li>String Search </li>
<li>Wall Opening Area </li>
</ul>
<h5><a name="_Use_a_Registered"></a><span style="font-weight: bold;"><em>Use a Registered Developer Symbol</em></span></h5>
<p>We strongly recommend you prefix your filenames with your Registered Developer Symbol (RDS) to avoid potential naming conflict with other plug-ins. You can reserve an RDS for free at www.autodesk.com/symbolreg. (If you don’t use an RDS, and your plug-in files clash with another plug-in, then we will ask you to rename your files).</p>
<p>This applies to contents (such as family libraries) as well as plug-in modules.</p>
<h5><a name="_Use_the_Ribbon"></a><span style="font-weight: bold;"><em>Use the Ribbon</em></span></h5>
<p>Using ribbon elements for your application is requirement, but how you do this will depend on your plug-in design. As a minimum, every plug-in or family library must add a panel to the ‘Add-Ins’ tab that either invokes the main command defined by the plug-in or displays a help file explaining how to use the family library.</p>
<p>However, if your plug-in defines more than one command, then you will have to create ribbon layout for your plug-in in a way that is most helpful to the user (for example, using large buttons for most frequently used commands and small buttons for lesser used commands).</p>
<ul>
<li>Plug-ins that install a single panel should normally add that panel to the ‘Add-Ins’ tab. </li>
<li>Plug-ins that creates several ribbon bar panels may prefer to create a new tab specific to that Plug-In (and preferably with the Tab name being the name of the plug-in). </li>
</ul>
<h5><a name="_Use_the_Contextual"></a><span style="font-weight: bold;"><em>Use the Contextual Help (F1 Help) with a Ribbon Item</em></span></h5>
<p>Contextual or F1 Help support is a new feature in Revit 2013 API. Please refer to the RibbonItem.SetContextualHelp() method and the ContextualHelp class in the Revit API documentation. The Autodesk Plug-in of the Month sample apps posted to the Exchange Apps store demonstarte how to implement F1 help.</p>
<p>We encourage you to use a help mechanism that is consistent with the Revit help, such as tooltips and contextual help placed on a ribbon item. The options supported for contextual help include linking to an external URL, launching a locally installed help file, or linking to a topic on the Autodesk help wiki.</p>
<h5><a name="_End_User_License"></a><span style="font-weight: bold;"><em>End User License Agreement</em></span></h5>
<p>The installer that the ADN team creates for your app includes a button that allows the user to view the standard End User License Agreement (EULA) during the app installation. This EULA is NOT modifiable. If you wish to include your own EULA to your app, you can either: 1) reference your EULA from the standard HTM help file text, or 2) display your EULA when the app runs for the first time, and require the user to accept it before the app will work.</p>
<h5><a name="_Special_Considerations"></a><span style="font-weight: bold;"><em>Special Considerations</em></span></h5>
<h5>If your app or content has any special requirements and the standard installer template cannot handle as is, please talk to us. For example, we understand that for a large application, installing to all-user location may be desirable. It is possible to install at: %programdata%\Autodesk\Revit\Addins\2013.</h5>
<p>For any other special needs that require a custom installation, you can provide them in the form of Windows Installer Merge Modules (.msm) files. We will merge your msm file with the Windows Installer (.msi) file that we create for your app. Examples of such a scenario would be: writing entries to the registry for a licensing system you are using, installing dependent components by other vendors, and running custom scripts. For additional information, please contact us at <a href="mailto:appsinfo@autodesk.com">appsinfo@autodesk.com</a>.</p>
<p><strong><span style="font-size: medium;">More information</span></strong></p>
<p><strong>&#0160;</strong></p>
<p>The ADN team is here to help you be a successful publisher on Autodesk Exchange store. We’ll do whatever we can do to help you. You are welcome to email <a href="mailto:appsinfo@autodesk.com">appsinfo@autodesk.com</a> if you have any further questions after reviewing these guidelines and the other documentation on <a href="http://www.autodesk.com/developapps">www.autodesk.com/developapps</a>.</p>
<p>Thank you for participating on Autodesk Apps.</p>
