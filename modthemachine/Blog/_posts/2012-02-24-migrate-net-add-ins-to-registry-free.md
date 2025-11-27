---
layout: "post"
title: "Migrate .Net Add-Ins to registry free"
date: "2012-02-24 02:28:01"
author: "Wayne Brill"
categories:
  - "Add-In Creation"
original_url: "https://modthemachine.typepad.com/my_weblog/2012/02/migrate-net-add-ins-to-registry-free.html "
typepad_basename: "migrate-net-add-ins-to-registry-free"
typepad_status: "Publish"
---

<table cellpadding="0" cellspacing="0">
<tbody>
<tr>
<td valign="top">
<p>Inventor 2012 allows your Add-Ins to be loaded without them being registered as a COM component in the Windows registry. These Add-Ins are called “registry free Add-Ins”. I was sure happy when I could have my Add-In load without having to use the Windows registry. It really makes installing custom applications much&#0160; easier.&#0160; </p>
<p>The Inventor 2012 Add-In wizard provided with the SDK uses a new template that generates a Registry Free Add-In. If you want to migrate your existing Add-Ins without re-creating them from scratch (using the new template) you can follow these five steps. </p>
<p>Happy migration!</p>
<table cellpadding="0" cellspacing="0">
<tbody>
<tr>
<td valign="top">
<p><strong><span style="font-size: small;"><em>1. Remove COM Registration</em><em>&#0160;</em></span></strong></p>
<p>Uncheck the <strong>Register for COM interop</strong> check box on the “Compile” tab of the Application Properties dialog, as shown below: </p>
</td>
</tr>
</tbody>
</table>
</td>
</tr>
</tbody>
</table>
<a href="http://modthemachine.typepad.com/.a/6a00e553fcbfc688340168e7e204ad970c-pi"><img alt="image" border="0" height="610" src="/assets/image_644915.jpg" style="background-image: none; padding-left: 0px; padding-right: 0px; display: inline; padding-top: 0px; border-width: 0px;" title="image" width="470" /></a>   
<table cellpadding="0" cellspacing="0">
<tbody>
<tr>
<td valign="top">
<p>Note: In Visual Studio Express Edition this setting does not exist.</p>
<p><a href="http://modthemachine.typepad.com/.a/6a00e553fcbfc68834016762e015b6970b-pi"></a></p>
</td>
</tr>
</tbody>
</table>
<table cellpadding="0" cellspacing="0">
<tbody>
<tr>
<td valign="top">
<p>Remove any code associated with registering the add-in in the registry. This typically just means removing the <strong><em>Register</em></strong> and <strong><em>Unregister</em></strong> methods from your add-in class. The <strong><em>AddInGuid</em></strong> property is in the same region as the registration functions, so if you intend on using this property in other areas of your add-in you’ll want to be careful not to delete it. </p>
</td>
</tr>
</tbody>
</table>
<table cellpadding="0" cellspacing="0">
<tbody>
<tr>
<td valign="top">
<p><strong><span style="font-size: small;"><em>2. </em><em>Create the manifest file</em></span></strong></p>
</td>
</tr>
</tbody>
</table>
<table cellpadding="0" cellspacing="0">
<tbody>
<tr>
<td valign="top">
<p>Create a new file in the same directory as your project file and name it “<em>YourAddin</em>.X.manifest”, where <em>YourAddin</em> will be replaced with the name of your add-in. Add the following to the manifest file. The portions highlighted in yellow need to be edited to match your add-in properties: </p>
</td>
</tr>
</tbody>
</table>
<div style="font-family: courier new; background: #eeeeee; color: black; font-size: 9pt;">
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">&lt;?</span><span style="line-height: 140%; color: #a31515;">xml</span><span style="line-height: 140%; color: blue;"> </span><span style="line-height: 140%; color: red;">version</span><span style="line-height: 140%; color: blue;">=</span><span style="line-height: 140%;">&quot;</span><span style="line-height: 140%; color: blue;">1.0</span><span style="line-height: 140%;">&quot;</span><span style="line-height: 140%; color: blue;"> </span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: red;">encoding</span><span style="line-height: 140%; color: blue;">=</span><span style="line-height: 140%;">&quot;</span><span style="line-height: 140%; color: blue;">UTF-8</span><span style="line-height: 140%;">&quot;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: red;">standalone</span><span style="line-height: 140%; color: blue;">=</span><span style="line-height: 140%;">&quot;</span><span style="line-height: 140%; color: blue;">yes</span><span style="line-height: 140%;">&quot;</span><span style="line-height: 140%; color: blue;">?&gt;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">&lt;</span><span style="line-height: 140%; color: #a31515;">assembly</span><span style="line-height: 140%; color: blue;"> </span><span style="line-height: 140%; color: red;">xmlns</span><span style="line-height: 140%; color: blue;">=</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%;">&quot;</span><span style="line-height: 140%; color: blue;">urn:schemas-microsoft-com:asm.v1</span><span style="line-height: 140%;">&quot;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: red;">manifestVersion</span><span style="line-height: 140%; color: blue;">=</span><span style="line-height: 140%;">&quot;</span><span style="line-height: 140%; color: blue;">1.0</span><span style="line-height: 140%;">&quot;</span><span style="line-height: 140%; color: blue;">&gt;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">&#0160; &lt;</span><span style="line-height: 140%; color: #a31515;">assemblyIdentity</span><span style="line-height: 140%; color: blue;"> </span><span style="line-height: 140%; color: red;">name</span><span style="line-height: 140%; color: blue;">=</span><span style="line-height: 140%;">&quot;</span><span style="line-height: 140%; color: blue;"><span style="background-color: #ffff00;">RegFreeVbAddin</span></span><span style="line-height: 140%;">&quot;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: red;">version</span><span style="line-height: 140%; color: blue;">=</span><span style="line-height: 140%;">&quot;</span><span style="line-height: 140%; color: blue;"><span style="background-color: #ffff00;">1.0.0.0</span></span><span style="line-height: 140%;">&quot;</span><span style="line-height: 140%; color: blue;">&gt;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">&#0160; &lt;/</span><span style="line-height: 140%; color: #a31515;">assemblyIdentity</span><span style="line-height: 140%; color: blue;">&gt;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">&#0160; &lt;</span><span style="line-height: 140%; color: #a31515;">clrClass</span><span style="line-height: 140%; color: blue;"> </span><span style="line-height: 140%; color: red;">clsid</span><span style="line-height: 140%; color: blue;">=</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%;">&quot;</span><span style="line-height: 140%; color: blue;">{<span style="background-color: #ffff00;">6e41e3fb-439a-4cbe-b648-ed6a47eeac8d</span>}</span><span style="line-height: 140%;">&quot;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: red;">progid</span><span style="line-height: 140%; color: blue;">=</span><span style="line-height: 140%;">&quot;</span><span style="line-height: 140%; color: blue;"><span style="background-color: #ffff00;">RegFreeVbAddin.StandardAddInServer</span></span><span style="line-height: 140%;">&quot;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: red;">threadingModel</span><span style="line-height: 140%; color: blue;">=</span><span style="line-height: 140%;">&quot;</span><span style="line-height: 140%; color: blue;">Both</span><span style="line-height: 140%;">&quot;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: red;">name</span><span style="line-height: 140%; color: blue;">=</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&quot;</span><span style="line-height: 140%; color: blue;"><span style="background-color: #ffff00;">RegFreeVbAddin.RegFreeVbAddin.StandardAddInServer</span></span><span style="line-height: 140%;">&quot;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: red;">runtimeVersion</span><span style="line-height: 140%; color: blue;">=</span><span style="line-height: 140%;">&quot;</span><span style="line-height: 140%; color: blue;"><span style="background-color: #ffff00;">1.0</span></span><span style="line-height: 140%;">&quot;</span><span style="line-height: 140%; color: blue;">&gt;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">&#0160; &lt;/</span><span style="line-height: 140%; color: #a31515;">clrClass</span><span style="line-height: 140%; color: blue;">&gt;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">&#0160; &lt;</span><span style="line-height: 140%; color: #a31515;">file</span><span style="line-height: 140%; color: blue;"> </span><span style="line-height: 140%; color: red;">name</span><span style="line-height: 140%; color: blue;">=</span><span style="line-height: 140%;">&quot;</span><span style="line-height: 140%; color: blue;"><span style="background-color: #ffff00;">RegFreeVbAddin.dll</span></span><span style="line-height: 140%;">&quot;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: red;">hashalg</span><span style="line-height: 140%; color: blue;">=</span><span style="line-height: 140%;">&quot;</span><span style="line-height: 140%; color: blue;">SHA1</span><span style="line-height: 140%;">&quot;</span><span style="line-height: 140%; color: blue;">&gt;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">&#0160; &lt;/</span><span style="line-height: 140%; color: #a31515;">file</span><span style="line-height: 140%; color: blue;">&gt;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">&lt;/</span><span style="line-height: 140%; color: #a31515;">assembly</span><span style="line-height: 140%; color: blue;">&gt;</span></p>
</div>
<table cellpadding="0" cellspacing="0">
<tbody>
<tr>
<td valign="top">
<p>The “<em>name</em>” attribute of the clrClass element consists of three parts, separated by dots: </p>
<p><strong><em>Assembly.Namespace.ClassName</em></strong></p>
<p><strong><em>&#0160;</em></strong></p>
<p>To make sure, you can retrieve the assembly name and root namespace from your project properties:</p>
</td>
</tr>
</tbody>
</table>
<p>&#0160;</p>
<p><a href="http://modthemachine.typepad.com/.a/6a00e553fcbfc688340168e7e204d3970c-pi"><img alt="image" border="0" height="83" src="/assets/image_724792.jpg" style="background-image: none; padding-left: 0px; padding-right: 0px; display: inline; padding-top: 0px; border-width: 0px;" title="image" width="479" /></a></p>
<table cellpadding="0" cellspacing="0">
<tbody>
<tr>
<td valign="top">
<p><strong>Note:</strong> The manifest file is not Unicode!<span style="text-decoration: underline;"> </span></p>
</td>
</tr>
</tbody>
</table>
<table cellpadding="0" cellspacing="0">
<tbody>
<tr>
<td valign="top">
<p><strong><span style="font-size: small;"><em>3. </em><em>Embed the manifest during post-build</em></span></strong></p>
</td>
</tr>
</tbody>
</table>
<table cellpadding="0" cellspacing="0">
<tbody>
<tr>
<td valign="top">
<p>The next step is to add a post-build process to incorporate this manifest into your dll. The post-build process calls <strong><em>mt.exe</em></strong> which is a Microsoft utility that will embed the manifest into your AddIn’s dll. You define a post-build step through the Compile tab of the Application Properties dialog. On the Compile tab, click the “Build Events…” button and then on the “Build Events” dialog click the “Edit Post-build…” button. Finally enter the following string by replacing the correct Add-In name into the “Post-build Event Command Line” dialog, as shown below:</p>
<p><strong><em>call &quot;%VS90COMNTOOLS%vsvars32&quot; mt.exe -manifest &quot;$(ProjectDir)<span style="background-color: #ffff00;">RegFreeVbAddin</span>.X.manifest&quot; -outputresource:&quot;$(TargetPath)&quot;;#2</em></strong></p>
</td>
</tr>
</tbody>
</table>
<a href="http://modthemachine.typepad.com/.a/6a00e553fcbfc68834016762e015cc970b-pi"><img alt="image" border="0" height="449" src="/assets/image_318577.jpg" style="background-image: none; padding-left: 0px; padding-right: 0px; display: inline; padding-top: 0px; border-width: 0px;" title="image" width="477" /></a>   
<table cellpadding="0" cellspacing="0">
<tbody>
<tr>
<td valign="top">
<p>Compile your project. This will cause the manifest to be embedded within your dll. If you get any errors when compiling, verify that the command line that you entered matches the one above. </p>
<p>You can verify that the manifest has been correctly embedded by dragging the Add-In dll into Visual Studio. You should see the “RT_MANIFEST” folder, as shown below. Double clicking on the “2” icon will open a window to show you actual manifest data.</p>
</td>
</tr>
</tbody>
</table>
<a href="http://modthemachine.typepad.com/.a/6a00e553fcbfc688340168e7e204f5970c-pi"><img alt="image" border="0" height="250" src="/assets/image_601821.jpg" style="background-image: none; padding-left: 0px; padding-right: 0px; display: inline; padding-top: 0px; border-width: 0px;" title="image" width="477" /></a>   
<table cellpadding="0" cellspacing="0">
<tbody>
<tr>
<td valign="top">
<p>It appears that the “build events” functionality is not provided for Visual Basic Express. It is there for C# Express and of course regular versions of Visual Studio. If you use VB Express you can manually invoke the mt utility from the command line or create a batch file.</p>
</td>
</tr>
</tbody>
</table>
<table cellpadding="0" cellspacing="0">
<tbody>
<tr>
<td valign="top">
<p><strong><span style="font-size: small;"><em>4. </em><em>Create the .addin file</em></span></strong></p>
</td>
</tr>
</tbody>
</table>
<table cellpadding="0" cellspacing="0">
<tbody>
<tr>
<td valign="top">
<p>A requirement for all registry-free Add-Ins is to have an .addin file. With a registry based Add-In, Inventor relied completely on the registry to get information about the Add-In. The .addin file takes the place of the registry for this same information. </p>
<p>The addin filename is in the form of <strong><em>Company.AddInName.Inventor.addin</em></strong></p>
<p>Below is the .addin file for my example Add-In with the portions to change highlighted:</p>
</td>
</tr>
</tbody>
</table>
<table cellpadding="0" cellspacing="0">
<tbody>
<tr>
<td valign="top">
<div style="font-family: courier new; background: #eeeeee; color: black; font-size: 9pt;">
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">&lt;</span><span style="line-height: 140%; color: #a31515;">Addin</span><span style="line-height: 140%; color: blue;"> </span><span style="line-height: 140%; color: red;">Type</span><span style="line-height: 140%; color: blue;">=</span><span style="line-height: 140%;">&quot;</span><span style="line-height: 140%; color: blue;">Standard</span><span style="line-height: 140%;">&quot;</span><span style="line-height: 140%; color: blue;">&gt;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">&#0160; &lt;!--</span><span style="line-height: 140%; color: green;">Created for Autodesk Inventor Version 16.0</span><span style="line-height: 140%; color: blue;">--&gt;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">&#0160; &lt;!--</span><span style="line-height: 140%; color: green;">Override settings for ApplicationAddIn: RegFreeVbAddin</span><span style="line-height: 140%; color: blue;">--&gt;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">&#0160; &lt;</span><span style="line-height: 140%; color: #a31515;">ClassId</span><span style="line-height: 140%; color: blue;">&gt;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; {<span style="background-color: #ffff00;">6e41e3fb-439a-4cbe-b648-ed6a47eeac8d</span>}</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">&#0160; &lt;/</span><span style="line-height: 140%; color: #a31515;">ClassId</span><span style="line-height: 140%; color: blue;">&gt;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">&#0160; &lt;</span><span style="line-height: 140%; color: #a31515;">ClientId</span><span style="line-height: 140%; color: blue;">&gt;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; {<span style="background-color: #ffff00;">6e41e3fb-439a-4cbe-b648-ed6a47eeac8d</span>}</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">&#0160; &lt;/</span><span style="line-height: 140%; color: #a31515;">ClientId</span><span style="line-height: 140%; color: blue;">&gt;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">&#0160; &lt;</span><span style="line-height: 140%; color: #a31515;">DisplayName</span><span style="line-height: 140%; color: blue;">&gt;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; <span style="background-color: #ffff00;">RegFreeVbAddin</span></span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">&#0160; &lt;/</span><span style="line-height: 140%; color: #a31515;">DisplayName</span><span style="line-height: 140%; color: blue;">&gt;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">&#0160; &lt;</span><span style="line-height: 140%; color: #a31515;">Description</span><span style="line-height: 140%; color: blue;">&gt;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; <span style="background-color: #ffff00;">A Registry-free Inventor Addin</span></span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">&#0160; &lt;/</span><span style="line-height: 140%; color: #a31515;">Description</span><span style="line-height: 140%; color: blue;">&gt;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">&#0160; &lt;</span><span style="line-height: 140%; color: #a31515;">Assembly</span><span style="line-height: 140%; color: blue;">&gt;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; <span style="background-color: #ffff00;">C:\Autodesk\Inventor\MyAddins\RegFreeVbAddin.dll</span></span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">&#0160; &lt;/</span><span style="line-height: 140%; color: #a31515;">Assembly</span><span style="line-height: 140%; color: blue;">&gt;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">&#0160; &lt;</span><span style="line-height: 140%; color: #a31515;">LoadOnStartUp</span><span style="line-height: 140%; color: blue;">&gt;</span><span style="line-height: 140%;"><span style="background-color: #ffff00;">1</span></span><span style="line-height: 140%; color: blue;">&lt;/</span><span style="line-height: 140%; color: #a31515;">LoadOnStartUp</span><span style="line-height: 140%; color: blue;">&gt;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">&#0160; &lt;</span><span style="line-height: 140%; color: #a31515;">UserUnloadable</span><span style="line-height: 140%; color: blue;">&gt;</span><span style="line-height: 140%;"><span style="background-color: #ffff00;">1</span></span><span style="line-height: 140%; color: blue;">&lt;/</span><span style="line-height: 140%; color: #a31515;">UserUnloadable</span><span style="line-height: 140%; color: blue;">&gt;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">&#0160; &lt;</span><span style="line-height: 140%; color: #a31515;">Hidden</span><span style="line-height: 140%; color: blue;">&gt;</span><span style="line-height: 140%;"><span style="background-color: #ffff00;">0</span></span><span style="line-height: 140%; color: blue;">&lt;/</span><span style="line-height: 140%; color: #a31515;">Hidden</span><span style="line-height: 140%; color: blue;">&gt;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">&#0160; &lt;</span><span style="line-height: 140%; color: #a31515;">SupportedSoftwareVersionGreaterThan</span><span style="line-height: 140%; color: blue;">&gt;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; <span style="background-color: #ffff00;">15..</span></span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">&#0160; &lt;/</span><span style="line-height: 140%; color: #a31515;">SupportedSoftwareVersionGreaterThan</span><span style="line-height: 140%; color: blue;">&gt;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">&#0160; &lt;</span><span style="line-height: 140%; color: #a31515;">DataVersion</span><span style="line-height: 140%; color: blue;">&gt;</span><span style="line-height: 140%;"><span style="background-color: #ffff00;">1</span></span><span style="line-height: 140%; color: blue;">&lt;/</span><span style="line-height: 140%; color: #a31515;">DataVersion</span><span style="line-height: 140%; color: blue;">&gt;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">&#0160; &lt;</span><span style="line-height: 140%; color: #a31515;">UserInterfaceVersion</span><span style="line-height: 140%; color: blue;">&gt;</span><span style="line-height: 140%;"><span style="background-color: #ffff00;">1</span></span><span style="line-height: 140%; color: blue;">&lt;/</span><span style="line-height: 140%; color: #a31515;">UserInterfaceVersion</span><span style="line-height: 140%; color: blue;">&gt;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">&#0160; &lt;</span><span style="line-height: 140%; color: #a31515;">LoadBehavior</span><span style="line-height: 140%; color: blue;">&gt;</span><span style="line-height: 140%;"><span style="background-color: #ffff00;">0</span></span><span style="line-height: 140%; color: blue;">&lt;/</span><span style="line-height: 140%; color: #a31515;">LoadBehavior</span><span style="line-height: 140%; color: blue;">&gt;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">&lt;/</span><span style="line-height: 140%; color: #a31515;">Addin</span><span style="line-height: 140%; color: blue;">&gt;</span></p>
</div>
</td>
</tr>
</tbody>
</table>
<table cellpadding="0" cellspacing="0">
<tbody>
<tr>
<td valign="top">
<p>For most AddIns, the <strong>ClassId </strong>(required)<strong> </strong>and <strong>ClientId </strong>(required)<strong> </strong>elements will be the same value, which is the GUID at the top of the StandardAddInServer class. </p>
<p>The value of the <strong>DisplayName </strong>(required)<strong> </strong>element can be anything and is the name of the add-ins as shown in the AddIn Manager. </p>
<p>The <strong>Description </strong>(required)<strong> </strong>element can also be anything and is the description of the add-in as shown in the AddIn Manager.</p>
<p>The <strong>Assembly </strong>(required)<strong> </strong>element defines the name of the AddIn dll. It also defines the path to the dll.</p>
<p>There are 3 ways to specify the location of the binaries:</p>
<ol>
<li>A full path including the dll name. </li>
<li>Only the dll name. Inventor will look for this dll in the &lt;InstallPath&gt;\bin\ folder followed by the folder in which the .addin manifest was found (i.e. same folder as the .addin file). </li>
<li>A relative path including the dll name. Inventor will search for the dll in the specified relative path starting at the &lt;InstallPath&gt;\bin\ folder followed by a search in the specified relative path starting at the folder in which the .addin manifest was found. </li>
</ol>
<p>The <strong>LoadOnStartUp</strong> (optional - obsolete) element specifies whether the add-in should load on start up. Assumed to be true if this value is not specified. Value can be 0 or 1.</p>
<p>The <strong>UserUnloadable</strong> (optional) element specifies whether the user can unload the add-in. Assumed to be true if this value is not specified (i.e. user can unload the add-in). Value can be 0 or 1.</p>
<p>The <strong>Hidden </strong>(optional)<strong> </strong>element defines whether the add-in is visible in the Add-In Manager or not. A value of 1 indicates that it is hidden, although the end-user can still right-click within the Add-In Manager and choose “Show hidden members” to display all add-ins.</p>
<p>The <strong>SupportedSoftwareVersionXXX</strong> (optional) element specifies the version(s) of Inventor that the add-in should be available in. Combinations of these can be used. These values are ignored if the .addin file is located in a version-specific folder (i.e. the add-in is always available for that Inventor version).</p>
<p>The <strong>DataVersion</strong> (optional) element specifies the version of add-in data contained within Inventor documents. To be used by add-ins that store migrating data in Inventor documents, indicated by “DocumentInterests”.</p>
<p>The <strong>UserInterfaceVersion</strong> (optional) element specifies the version of the user interface of the add-in. Incrementing this version results in all of the addin’s UI getting cleaned up during Inventor start-up.</p>
<p>The <strong>LoadBehavior</strong> (optional) element allows you to control a brand new feature of Inventor 2012: this will determine for which document type the addin will be loaded. Basically this feature will improve the start-up time of Inventor by delaying the loading of the addin the first time a specific document type is created or open, for example if your addin is only providing part-specific functionalities, you can choose to let Inventor load it only when the user will open his first part in the session.</p>
<p>Here is a table that recaps the various values this flag can take and its effect:</p>
</td>
</tr>
<tr>
<td valign="top">&#0160;</td>
</tr>
</tbody>
</table>
<table border="1" cellpadding="0" cellspacing="0">
<tbody>
<tr>
<td valign="top" width="216">
<p>LoadBehavior</p>
</td>
<td valign="top" width="369">
<p>Effect</p>
</td>
</tr>
<tr>
<td valign="top" width="216">
<p>0</p>
</td>
<td valign="top" width="369">
<p>Load on <strong><em>Start-up </em></strong><em>(not recommended)</em></p>
</td>
</tr>
<tr>
<td valign="top" width="216">
<p>1</p>
</td>
<td valign="top" width="369">
<p>Load when any <strong><em>document</em></strong> is open/created</p>
</td>
</tr>
<tr>
<td valign="top" width="216">
<p>2</p>
</td>
<td valign="top" width="369">
<p>Load when first <strong><em>Assembly</em></strong> is open/created</p>
</td>
</tr>
<tr>
<td valign="top" width="216">
<p>3</p>
</td>
<td valign="top" width="369">
<p>Load when first <strong><em>Presentation</em></strong> is open/created</p>
</td>
</tr>
<tr>
<td valign="top" width="216">
<p>4</p>
</td>
<td valign="top" width="369">
<p>Load when first <strong><em>Drawing</em></strong> is open/created</p>
</td>
</tr>
<tr>
<td valign="top" width="216">
<p>10</p>
</td>
<td valign="top" width="369">
<p>Load <strong><em>OnDemand</em></strong></p>
</td>
</tr>
</tbody>
</table>
<table cellpadding="0" cellspacing="0">
<tbody>
<tr>
<td valign="top">
<p>Assumed to be 0 if this value is not specified.</p>
<p><strong>Installer considerations:</strong></p>
<p>In the case you are building an installer to deploy your Add-In, you may wonder how you would handle the situation where you let the user select the location where they will install the Add-In dll itself, as the .addin file needs to reflect this path, and the answer is that you have 2 choices here:</p>
<ol>
<li>Do not let the user select a custom location for the install and provide a relative path or no path at all: Inventor will try to load the addin from the locations described above in the “Assembly” element section. </li>
<li>Let the user select a custom location, in which case you will have to create a custom dll called from a custom action inside your installer that will dynamically perform the editing of the .addin file based on the selected path. </li>
</ol>       </td>
</tr>
</tbody>
</table>
<table cellpadding="0" cellspacing="0">
<tbody>
<tr>
<td valign="top">
<p><strong><span style="font-size: small;"><em>5. </em><em>Choose a location for .addin file</em></span></strong></p>
</td>
</tr>
</tbody>
</table>
<table cellpadding="0" cellspacing="0">
<tbody>
<tr>
<td valign="top">
<p>Copy the .addin file to one of the following directories, depending on the scope of your add-in and the operating system. </p>
<p><em>&#0160;</em></p>
<p><strong>Version Independent:</strong></p>
<p><strong><em>&#0160;</em></strong><strong><em>&#0160;</em></strong></p>
<p>The presence of a .addin file in the following locations indicates that the AddIn should be loaded in multiple versions of Inventor based on the values of the “SupportedSoftwareVersionXXX” settings in the manifest file. </p>
<p><em>&#0160;</em></p>
<p><em>Windows XP: </em></p>
<p>C:\Documents and Settings\All Users\Application Data\Autodesk\Inventor Addins\ </p>
<p><em>&#0160;</em></p>
<p><em>Windows7/Vista: </em></p>
<p>C:\ProgramData\Autodesk\Inventor Addins\ </p>
<p><em>&#0160;</em></p>
<p><em>&#0160;</em></p>
<p><strong>Version Dependent:</strong></p>
<p><strong><em>&#0160;</em></strong><em>&#0160;</em></p>
<p>The presence of a manifest file in the following locations indicates that the AddIn should be loaded in a specific version of Inventor. The values of the “SupportedSoftwareVersionXXX” settings in the manifest file are ignored, if present.</p>
<p><em>Windows XP: </em></p>
<p>C:\Documents and Settings\All Users\Application Data\Autodesk\Inventor 2012\Addins\ </p>
<p><em>&#0160;</em></p>
<p><em>Windows7/Vista: </em></p>
<p>C:\ProgramData\Autodesk\Inventor 2012\Addins\<em> </em></p>
<p><strong><em>&#0160;</em></strong></p>
<p><strong><em>&#0160;</em></strong></p>
<p><strong>Per User Override:</strong></p>
<p><strong><em>&#0160;</em></strong><em></em></p>
<p>The manifest file in the following locations can be created when per user overrides need to be saved. The settings in these manifest files override the setting values in the manifest files found in the ‘all users’ folder. The names of these override manifest files match the names of the “master” files. </p>
<p><em></em></p>
<p><em>Windows XP: </em></p>
<p>C:\Documents and Settings\&lt;user&gt;\Application Data\Autodesk\Inventor 2012\Addins\ </p>
<p><em></em></p>
<p><em>Windows7/Vista: </em></p>
<p>C:\Users\&lt;user&gt;\AppData\Roaming\Autodesk\Inventor 2012\Addins\</p>
<p>After following these steps your Add-In should be found and loaded by Inventor without any entries in the registry.</p>
<p>Wayne Brill </p>
</td>
</tr>
</tbody>
</table>
