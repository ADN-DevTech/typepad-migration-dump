---
layout: "post"
title: "Exchange Apps &ndash; Information for Inventor developers"
date: "2012-07-17 19:32:55"
author: "Madhukar Moogala"
categories:
  - "Inventor"
original_url: "https://adndevblog.typepad.com/manufacturing/2012/07/exchange-apps-information-for-inventor-developers.html "
typepad_basename: "exchange-apps-information-for-inventor-developers"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/stephen-preston.html">Stephen Preston</a></p>
<p>This guide is for developers and content providers new to publishing plug-ins and other content on Autodesk® Exchange Apps – either free, trial or for fee versions. It outlines best practice guidelines and a few requirements for publishers to follow when creating products for the Autodesk Exchange Apps. These guidelines are designed to ensure that users on Autodesk Exchange have a consistent experience when downloading multiple products from the store.</p>
<p><strong><span style="font-size: medium;">Requirements</span></strong></p>
<table border="0" cellpadding="0" cellspacing="0">
<tbody>
<tr>
<td valign="top" width="686">
<p>You will be presented with a detailed list of requirements for publishing on Exchange when you first register to be a publisher. The information that follows is a summary. If there are any differences, then the online Publisher Agreement takes precedence.</p>
<h5><span style="font-weight: bold;">All content types</span></h5>
<p>Most of the information we need from you is collected via the web form you complete when submitting your content. This includes gathering information to auto-generate a HTML ‘quick start’ page that is included with the download of your product and viewable online. Other requirements are:</p>
<ul>
<li>Your product must be relevant to (and usable with) Inventor 2013 onwards, and must run on any Windows operating system supported by Inventor 2013 (including 32- and 64-bit versions). </li>
<li>The information you provide on the ‘documentation’ form when you submit your app is used to create a standard format HTML page. This information must allow the user to quickly understand how to use your product. You can reference additional information (for example, additional helpfiles posted on your website) from the standard HTML documentation. This HTML page will be populated using information you provide when submitting your product to Exchange Apps. </li>
<li>If you don’t use the standard ‘.bundle’ installer template we provide, or if your installer or product requires elevated user privileges (greater than a Windows 7 Standard User) to install, then this must be very clearly documented in the description of your product displayed on the Store. Autodesk may reject your app if it is not compatible with our standard installer template.</li>
<li>Your product must be ‘ready to go’ as soon as it’s installed. It must not require the user to manually copy or register files, or manually edit settings (such as support paths).
<ul>
<li>If you use a licensing system then it must allow your product to run as soon as it is installed by the user. This means either instant activation (e.g.online activation), or your product must run fully functional for a time-bombed ‘grace period’ that is long enough for you to send activation information to the customer.</li>
<li>The new registry free Add-in mechanism greatly simplifies the automatic installation steps. </li>
</ul>
</li>
<li>Your product should be stable, and not behave or alter the behavior of Inventor in a way that we deem unsuitable (for example, blocking standard functionality, blocking the functionality of another Add-in, causing data loss etc.). </li>
</ul>
<h5><span style="font-weight: bold;">Add-ins</span></h5>
<p>Additional requirements for Add-ins are:</p>
<ul>
<li>Use the registry-free Add-in mechanism - We have a new registry-free Add-In mechanism in Inventor software (from 2012 onwards), and we use a standard ‘.bundle’ installer template to deploy content downloaded from the store. There have been some enhancements added to the registry-free Add-In mechanism in Inventor 2013 to make creating and installing applications easier. (We can help you migrate your app to use this mechanism).</li>
<li>Your Add-in should add relevant UI elements to the Inventor RibbonBar where applicable.</li>
<li>Translator Add-ins should use the standard translator add-in mechanism for Inventor.</li>
</ul>
<h5><span style="font-weight: bold;">Part and Assembly Libraries</span></h5>
<p>Additional requirements for component libraries are:</p>
<ul>
<li>Your Library must add relevant UI elements for your Add-in to the Inventor RibbonBar. The RibbonBar UI must either provide access to the library components, or launch a helpfile explaining how to access it. </li>
<li>Your libraries (in ipt/iam file format) must be installed in a subfolder of the following folder.</li>
</ul>
<blockquote>
<p><strong>Windows 7/Vista</strong>: %PUBLIC%\Documents\Autodesk\Inventor (typically C:\Users\Public\Documents\Autodesk\Inventor)</p>
<p><br /><strong>Windows XP</strong>: %ALLUSERSPROFILE%\Documents\Autodesk\Inventor (typically C:\Documents and Settings\All Users\Documents\Autodesk\Inventor)</p>
</blockquote>
<blockquote>
<p>Again, we’ll create the MSI installer for you and we can help you modify your libraries so they work in this new location. You just need to provide the content.</p>
</blockquote>
<h5><span style="font-weight: bold;">Standalone applications and other content</span></h5>
<p>There are no additional requirements for products that are not integrated with Inventor. If you wonder what kinds of products this might include – consider eBooks, video tutorials, industry specific calculators, connectors to Cloud based services and the like.</p>
</td>
</tr>
</tbody>
</table>
<p><strong>&#0160;</strong></p>
<p><strong><span style="font-size: medium;">Guidelines</span></strong></p>
<p><strong>&#0160;</strong></p>
<h5><span style="font-weight: bold;">Use the Registry Free mechanism</span></h5>
<p>We strongly encourage you to make use of the Registry Free mechanism to deploy your Add-in. Information on the required format for the associated ‘.addin’ file is included in the Inventor helpfiles – see Help-&gt;Community Resources-&gt;Programming Help in the Inventor menubar and search for “add-in-registration”.</p>
<p>You can also download some of the free Add-ins already available on Autodesk Exchange and study their format - the Autodesk Plug-in of the Month samples posted to Exchange Apps, for example:-</p>
<ul>
<li>Inventor LinkParameters</li>
<li>PointLinker for Inventor</li>
<li>ThreadModeler for Inventor</li>
<li>FeatureMigrator for Inventor</li>
<li>Screenshot for Inventor</li>
</ul>
<h5><span style="font-weight: bold;">Use the RibbonBar</span></h5>
<p>Adding RibbonBar elements for your application is recommended when relevant, but how you do this will depend on your Add-in design:</p>
<ul>
<li>As a minimum, your Add-in or content library can add a panel to the ‘Add-Ins’ tab that either invokes the main command defined by the Add-in or displays a helpfile explaining how to use the library. <strong>&#0160;</strong></li>
<li>Add-ins that install a single panel should normally add that panel to the ‘Add-Ins’ tab. </li>
<li>Add-ins that create several RibbonBar panels may prefer to create a new Tab specific to that Add-In (and normally with the Tab name being the name of the Add-in).</li>
<li>You can add any other UI elements via your Add-In (e.g. menubars and toolbars) as well, but you should still include a basic RibbonBar UI where applicable.</li>
</ul>
<h5><span style="font-weight: bold;">Use delay loading</span></h5>
<p>Unless your Add-In absolutely has to load as soon as Inventor launches, you should design it to load only when it’s needed. This is to minimize the impact of installed Add-ins on Inventor startup performance. The .addin mechanism makes it very easy to setup demand load settings for your Add-in.</p>
<p><strong><span style="font-size: medium;">More information</span></strong></p>
<p><strong></strong></p>
<p>The ADN team is here to help you be a successful publisher on Autodesk Exchange store. We’ll do whatever we can do to help you. You are welcome to email <a href="mailto:appsinfo@autodesk.com">appsinfo@autodesk.com</a> if you have any further questions after reviewing these guidelines and the other documentation on <a href="http://www.autodesk.com/developapps">www.autodesk.com/developapps</a>.</p>
<p>Thank you for participating on Autodesk Apps.</p>
