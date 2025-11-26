---
layout: "post"
title: "Exchange Apps - Information for AutoCAD and AutoCAD vertical developers"
date: "2012-07-17 19:39:32"
author: "Madhukar Moogala"
categories:
  - "2013"
  - "AutoCAD"
  - "Stephen Preston"
original_url: "https://adndevblog.typepad.com/autocad/2012/07/exchange-apps-information-for-autocad-and-autocad-vertical-developers.html "
typepad_basename: "exchange-apps-information-for-autocad-and-autocad-vertical-developers"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/stephen-preston.html">Stephen Preston</a></p>
<p>This guide is for developers and content providers new to publishing plug-ins and other content on Autodesk® Exchange Apps – either free, trial or for fee versions. It outlines best practice guidelines and a few requirements for publishers to follow when creating products for the Autodesk Exchange Apps. These guidelines are designed to ensure that users on Autodesk Exchange have a consistent experience when downloading multiple products from the store.</p>
<p><strong><span style="font-size: medium;">Requirements</span></strong></p>
<p>You will be presented with a detailed list of requirements for publishing on Exchange when you first register to be a publisher. The information that follows is a summary. If there are any differences, then the online Publisher Agreement takes precedence.</p>
<p><strong><em>All content types</em></strong></p>
<p>Most of the information we need from you is collected via the web form you complete when submitting your content. This includes gathering information to auto-generate a HTML ‘quick start’ page that is included with the download of your product and viewable online. Other requirements are:</p>
<p>· Your product must be relevant to (and usable with) AutoCAD 2013, or any of these AutoCAD 2013 vertical products: AutoCAD Architecture, AutoCAD Electrical, AutoCAD ecscad, AutoCAD Mechanical, AutoCAD MEP, AutoCAD Civil 3D and must run on any Windows operating system supported by the AutoCAD 2013 product (including both 32-bit and 64-bit versions). If you indicate compatibility with one or more vertical products you are responsible for testing your App with that product. For AutoCAD Apps only, you may also indicate compatibility with AutoCAD 2012.</p>
<p>· The ‘documentation’ information you provide when submitting your app will be used to create a standard format HTML page, and must allow the user to quickly understand how to use your product. You can reference additional information (for example, additional helpfiles posted on your website) from the standard HTML documentation.</p>
<p>· We strongly recommend you make use of the standard Exchange store app installer we create for you. However, if you don’t use our standard installer template, or if your installer or product requires elevated user privileges (greater than a Windows 7 Standard User) to install, then this must be very clearly documented in the description of your product displayed on the Store. Autodesk may reject your app if it is not compatible with our standard installer template.</p>
<p>· Your product must be ‘ready to go’ as soon as it’s installed. It must not require the user to manually copy or register files, or manually edit AutoCAD setting (such as support paths). The new autoloader mechanism we’ll describe later will help you avoid this.</p>
<p>· Your product should be stable, and not behave or alter the behavior of AutoCAD in a way that we deem unsuitable (for example, blocking standard AutoCAD functionality, blocking the functionality of another plug-in, causing data loss etc.).</p>
<p><strong><em>Plug-ins</em></strong></p>
<p>Additional requirements for plug-ins are:</p>
<p>· Your plug-in must include a partial CUIX file to add UI elements for your plug-in to the AutoCAD RibbonBar. We’ll explain more later in this document.</p>
<p><strong><em>Block Libraries</em></strong></p>
<p>Additional requirements for block libraries are:</p>
<p>· Your Block Library must include a partial CUIX file to add UI elements for your plug-in to the AutoCAD RibbonBar. The RibbonBar UI must either provide access to the block library or launch a helpfile explaining how to access it. We’ll explain this later in this document.</p>
<p>· Your block libraries (in DWG file format) must be installed in the following folder, so it can be indexed by the AutoCAD Content Explorer feature (CONTENTEXPLORER command).</p>
<blockquote>
<p>Windows 7/Vista: %PUBLIC%\Documents\Autodesk\Downloaded Content (typically C:\Users\Public\Documents\Autodesk\Downloaded Content)</p>
<p>Windows XP: %ALLUSERSPROFILE%\Documents\Autodesk\Downloaded Content (typically C:\Documents and Settings\All Users\Documents\Autodesk\Downloaded Content)</p>
</blockquote>
<p><strong><em>Standalone applications and other content</em></strong></p>
<p>There are no additional requirements for products that are not integrated with AutoCAD. Such products might include eBooks, video tutorials, industry specific calculators, and the like.</p>
<p>&#0160;</p>
<p><strong>&#0160;</strong></p>
<p><strong><span style="font-size: medium;">Guidelines</span></strong></p>
<p><strong>&#0160;</strong></p>
<p><strong><em>Use the Autoloader system</em></strong></p>
<p>We strongly encourage you to make use of the autoloader mechanism to deploy your plug-in. Information on the required format for autoloader ‘bundles’ is included in the AutoCAD 2013 helpfiles – see Help-&gt;Customization Guide-&gt;Introduction to Programming Interfaces-&gt;Install and Uninstall Plug-in Applications (or search the help for ‘appautoloader’); and the “<a>DevTV –Autoloader Format Explained</a>” video walks through some simple examples for .NET, ObjectARX, LISP and CUIX plug-ins.</p>
<p>You can also download some of the free plug-ins already available on Autodesk Exchange and study their format – such as one of the Autodesk Plug-in of the Month samples (for example, OffsetInXref, DimensionPatrol, or ClipboardManager).</p>
<p><strong><em>Use a Registered Developer Symbol</em></strong></p>
<p>If your plug-in makes use of support paths to locate files (and the autoloader mechanism uses support paths extensively), we strongly recommend you prefix your filenames with your Registered Developer Symbol (RDS) to avoid potential naming conflict with other plug-ins. You can reserve an RDS for free at <a href="http://www.autodesk.com/symbolreg">www.autodesk.com/symbolreg</a>. (If you don’t use an RDS, and your plug-in files clash with another plug-in, then we will ask you to rename your files).</p>
<p>This applies to content (such as block libraries) as well as plug-in modules.</p>
<p><strong><em>Use demand loading</em></strong></p>
<p>Unless your ObjectARX® or .NET plug-in absolutely has to load as soon as AutoCAD launches, you should design your plug-in to load only when it’s needed – most commonly ‘load on command invocation’. This is to minimize the impact of installed plug-ins on AutoCAD startup performance. The autoloader system makes it very easy to setup demand load settings for the various components that make up your plug-in.</p>
<p><strong><em>Follow the Interop Guidelines</em></strong></p>
<p>As well as using a Registered Developer Symbol and demand load settings, there are many ways to ensure your plug-in will work well with other plug-ins a user may install. These are described in detail in the ObjectARX Application Interoperability Guidelines – part of the helpfiles in the ObjectARX SDK (downloadable from www.objectarx.com). Although the Interop Guidelines is part of the ObjectARX SDK, many of these guidelines apply to all plug-in types. Don’t worry if you’ve designed your application without reading these guidelines – they are guidelines and not requirements. However, we recommend you consider them when designing your next project or when updating existing ones.</p>
<p><strong><em>Use the RibbonBar</em></strong></p>
<p>Using a partial CUIX file to add RibbonBar elements for your application is required, but how you do this will depend on your plug-in design. As a minimum, every plug-in or block library must add a panel to the ‘Plug-Ins’ tab that either invokes the main command defined by the plug-in or displays a helpfile explaining how to use the block library. The “<a>DevTV – Creating a Partial CUI</a>” video posted with these guidelines shows how to setup a partial CUIX file to add a panel to a tab already defined in the main CUIX.</p>
<p>Plug-ins that install a single panel should normally add that panel to the ‘Plug-Ins’ tab.</p>
<p>Plug-ins that create several RibbonBar panels may prefer to create a new Tab specific to that Plug-In (and normally with the Tab name being the name of the Plug-in).</p>
<p>You can add any other UI elements to your partial CUIX (e.g. menubars and toolbars) as well, but you must still include a basic RibbonBar UI.</p>
<p><strong><span style="font-size: medium;">Video Tutorials</span></strong></p>
<p>The following videos provide additional information on some of the topics covered in this guide:</p>
<p><a>· </a><a href="http://download.autodesk.com/media/adn/DevTV_Autoloader_Format_Explained/DevTV_Autoloader_Format_Explained.html">DevTV – Autoloader Format Explained</a> – A screencast explaining how to create your AutoCAD Plug-in in the new ‘autoloader’ format, the simple plug-in deployment system used by Apps plug-ins.</p>
<p><a>· </a><a href="http://download.autodesk.com/media/adn/DevTV_Creating_a_Partial_CUI/DevTV_Creating_a_Partial_CUI.html">DevTV – Creating a Partial CUI</a> – A short screencast demonstrating how to integrate your UI with the AutoCAD Plug-ins ribbonbar tab.</p>
<p><strong><span style="font-size: medium;">More information</span></strong></p>
<p><strong>&#0160;</strong></p>
<p>The ADN team is here to help you be a successful publisher on Autodesk Exchange store. We’ll do whatever we can do to help you. You are welcome to email <a href="mailto:appsinfo@autodesk.com">appsinfo@autodesk.com</a> if you have any further questions after reviewing these guidelines and the other documentation on <a href="http://www.autodesk.com/developapps">www.autodesk.com/developapps</a>.</p>
<p>Thank you for participating on Autodesk Apps.</p>
