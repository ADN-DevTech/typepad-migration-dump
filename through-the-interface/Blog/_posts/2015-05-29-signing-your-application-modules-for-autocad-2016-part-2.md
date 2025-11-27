---
layout: "post"
title: "Signing your application modules for AutoCAD 2016 &ndash; Part 2"
date: "2015-05-29 16:45:51"
author: "Kean Walmsley"
categories:
  - "AutoCAD"
  - "AutoCAD .NET"
  - "AutoLISP / Visual LISP"
  - "Installation"
  - "Runtime"
  - "Security"
original_url: "https://www.keanw.com/2015/05/signing-your-application-modules-for-autocad-2016-part-2.html "
typepad_basename: "signing-your-application-modules-for-autocad-2016-part-2"
typepad_status: "Publish"
---

<p>Yesterday we introduced <a href="http://through-the-interface.typepad.com/through_the_interface/2015/05/signing-your-application-modules-for-autocad-2016-part-1.html" target="_blank">the need to sign program modules for AutoCAD 2016</a>. Today we’re going to see how AutoCAD behaves when loading signed and unsigned modules, as well as what the innards of a signed LISP module look like.</p>
<p>Here’s a simple piece of AutoLISP code that I’ve placed in a file called <em>c:/temp/MyModule.lsp</em>:</p>
<div style="font-size: 8pt; font-family: courier new; background: white; color: black; line-height: 140%;">
<p style="margin: 0px;">(defun c:test()</p>
<p style="margin: 0px;">&#0160; (princ &quot;\nThis is a test command.&quot;)</p>
<p style="margin: 0px;">&#0160; (princ)</p>
<p style="margin: 0px;">)</p>
</div>
<p>Here’s what AutoCAD displays when we try to load this module:</p>
<p><a href="http://through-the-interface.typepad.com/.a/6a00d83452464869e201b8d11eaf4d970c-pi" target="_blank"><img alt="Security warning" border="0" height="200" src="/assets/image_728869.jpg" style="background-image: none; float: none; padding-top: 0px; padding-left: 0px; margin: 20px auto; display: block; padding-right: 0px; border-width: 0px;" title="Security warning" width="344" /></a></p>
<p>We can use <em>AcSignApply.exe</em> to sign this module with our digital certificate, as we discussed yesterday:</p>
<p><a href="http://through-the-interface.typepad.com/.a/6a00d83452464869e201bb083955cd970d-pi" target="_blank"><img alt="AcSignApply about to sign a LSP file" border="0" height="325" src="/assets/image_315851.jpg" style="background-image: none; float: none; padding-top: 0px; padding-left: 0px; margin: 20px auto; display: block; padding-right: 0px; border-width: 0px;" title="AcSignApply about to sign a LSP file" width="348" /></a></p>
<p>Here are the contents of the file once it has been signed:</p>
<div style="word-wrap: break-word; font-size: 8pt; font-family: courier new; background: white; color: black; line-height: 140%;">
<p style="margin: 0px;">(defun c:test()</p>
<p style="margin: 0px;">&#0160; (princ &quot;\nThis is a test command.&quot;)</p>
<p style="margin: 0px;">&#0160; (princ)</p>
<p style="margin: 0px;">)</p>
<p style="margin: 0px;">;;;-----BEGIN-SIGNATURE-----</p>
<p style="margin: 0px;">;;; awUAADCCBWcGCSqGSIb3DQEHAqCCBVgwggVUAgEBMQ8wDQYJKoZIhvcNAQEFBQAw</p>
<p style="margin: 0px;">;;; CwYJKoZIhvcNAQcBoIIDATCCAv0wggHloAMCAQICEBENNIof/K6LQXlK3euL2qUw</p>
<p style="margin: 0px;">;;; DQYJKoZIhvcNAQEFBQAwEjEQMA4GA1UEAxMHd2FsbXNsazAgFw0xMTA2MjkwNzI1</p>
<p style="margin: 0px;">;;; MTdaGA8yMTExMDYwNTA3MjUxN1owEjEQMA4GA1UEAxMHd2FsbXNsazCCASIwDQYJ</p>
<p style="margin: 0px;">;;; KoZIhvcNAQEBBQADggEPADCCAQoCggEBAOHaTSHh3lEysSuRgYt0eilDszvLn9ez</p>
<p style="margin: 0px;">;;; WLHm2XNrmqfkCDM2bx3YeELKmcavXY7JUTEWzpcfsHDFjHQMJZ4MbdN4y+pj8s/9</p>
<p style="margin: 0px;">;;; 9CLUZ8X6GdU7ZeliBMFYGmn/G1PgI7D7imwZn3yHNehqztMxO3Ng9llSBc7QW8sM</p>
<p style="margin: 0px;">;;; XnK83BS2HdJwdCO60T1XKnLo6IqpCjYwgGwdB9Vq0EGAGsvAzg8S4sjqo4NbIpXr</p>
<p style="margin: 0px;">;;; VkED/1QLMKyk5xOxG3Uo4Wuf3AzaopDrWgdEAs1czNSSGMhLDqyfiS2nDXL2cACL</p>
<p style="margin: 0px;">;;; qFn2ln/cj6PDwztgvXNh6ro72vl5/FmT0tU7VuQKHr4UYfKFv5pGYHcCAwEAAaNN</p>
<p style="margin: 0px;">;;; MEswFQYDVR0lBA4wDAYKKwYBBAGCNwoDBDAnBgNVHREEIDAeoBwGCisGAQQBgjcU</p>
<p style="margin: 0px;">;;; AgOgDgwMd2FsbXNsa0BBRFMAMAkGA1UdEwQCMAAwDQYJKoZIhvcNAQEFBQADggEB</p>
<p style="margin: 0px;">;;; AEroAr+Kwf9LSqh0mdDq7//eY0fVv7rSnCyjZJn2wrX75HVuWv1Yaltowa0wvdS6</p>
<p style="margin: 0px;">;;; +E9Jt/O0VyAXcQQnKMD8cPnRD4XsAxHBdv02mlpPBKAJQTEpNlfpRK/OB7ViuPmB</p>
<p style="margin: 0px;">;;; V/IhCsMTIDrl8cIjrPVCFfJzH19ynjOKrmiZ36/5TUkhwafxF/I67nMNp8xJLnzp</p>
<p style="margin: 0px;">;;; ueZJTg+DJ4sARwQx0I6xGooqPP+3K+I32poXclLq2xyiD1lQAlusBgdRpLyQGztM</p>
<p style="margin: 0px;">;;; n65jUBC5UAQsCpfWLZEvwMZ4s/pBVYItxQvguwVJuiNteQ30/8UolyFdsNToUoWH</p>
<p style="margin: 0px;">;;; SbypkUHgeGt3kddzndVYP9sxggIqMIICJgIBATAmMBIxEDAOBgNVBAMTB3dhbG1z</p>
<p style="margin: 0px;">;;; bGsCEBENNIof/K6LQXlK3euL2qUwDQYJKoZIhvcNAQEFBQAwDQYJKoZIhvcNAQEB</p>
<p style="margin: 0px;">;;; BQAEggEAljG2hxJiepuetcU2B0hP//TNKrk888ImhxOggB9sU0duZtSrBdfX25VQ</p>
<p style="margin: 0px;">;;; lBtxlXZzKYkb+ob7+vFFTLY9hsVugzAzsJH2Bz1Ow6N1zidG4SJkxK2+In65ANTQ</p>
<p style="margin: 0px;">;;; KB7yx9T///hFP/FS2PM+ZKe9G4pDnZ6Xz2dM2+CQUZjb8hZDZJjTCEjnODEve0F/</p>
<p style="margin: 0px;">;;; Y2g+2tajS6tjtqa++bGTaDkvqeQyB7mGKHYtEDYcHyJz/DmjjTFvgcb+H4JaOfy6</p>
<p style="margin: 0px;">;;; Nib24lLU661pIspuaD1do4yjoRkXhqaOIR2Rc4bNg4Y6F42LWw4E9zzkRpAsqreu</p>
<p style="margin: 0px;">;;; 0CMm73m3NpsJFDkxuwG8gSq5IYeJl6GB1jCB0wYDVR0OMYHLBIHINQA3ADsANQAv</p>
<p style="margin: 0px;">;;; ADIAOAAvADIAMAAxADUALwAxADYALwA1AC8AMwA0AC8AVABpAG0AZQAgAGYAcgBv</p>
<p style="margin: 0px;">;;; AG0AIAB0AGgAaQBzACAAYwBvAG0AcAB1AHQAZQByACAAKABOAEUAVQBLAEUAVwBB</p>
<p style="margin: 0px;">;;; AE4AQgAxADAALQAyACkAUwBpAGcAbgBlAGQAIABmAG8AcgAgACIAVABoAHIAbwB1</p>
<p style="margin: 0px;">;;; AGcAaAAgAHQAaABlACAASQBuAHQAZQByAGYAYQBjAGUAIgAgAHAAbwBzAHQAAAA=</p>
<p style="margin: 0px;">;;; AA==</p>
<p style="margin: 0px;">;;; -----END-SIGNATURE-----</p>
</div>
<p>The digital signature has essentially been encoded in plain text and appended to the file&#39;s contents. Now let’s see what AutoCAD says when we attempt to load this newly signed module:</p>
<p><a href="http://through-the-interface.typepad.com/.a/6a00d83452464869e201b8d11eaf55970c-pi" target="_blank"><img alt="Different when signed" border="0" height="264" src="/assets/image_850355.jpg" style="background-image: none; float: none; padding-top: 0px; padding-left: 0px; margin: 20px auto; display: block; padding-right: 0px; border-width: 0px;" title="Different when signed" width="323" /></a></p>
<p>As this module was not found in one of our “trusted locations”, we do still get a message, despite its being signed. So there are a couple of important things, here…</p>
<p>Firstly, we need to consider whether the module should be in a <strong>trusted location</strong>. The locations that are trusted implicitly by AutoCAD are:</p>
<ul>
<li><em>C:\Program Files\Autodesk\ApplicationPlugins</em></li>
<li><em>C:\Program Files (x86)\Autodesk\ApplicationPlugins</em></li>
<li><em>C:\Program Files\Autodesk\AutoCAD 2016</em> (or wherever AutoCAD is installed to)</li>
</ul>
<p>This is a change from prior versions, where an ApplicationPlugins folder under the current user’s (and all users’) application data was also trusted. We realised this was actually a bad idea and so removed automatic trust for such folders in 2016.</p>
<p>It has previously been suggested that applications should append their installation folder to the TRUSTEDPATHS system variable (or the Registry entry in which it is stored), but this is actually not such a good idea: this setting is really intended for users and CAD managers to control what is considered trusted. If applications start writing to it, behind the scenes, then bad things could happen. So you should expect access to this system variable to become more controlled, over time.</p>
<p>This means that to trust an application based on its location, you can either place your application in AutoCAD’s install folder –&#0160; its sub-directories are also trusted, by the way – or in a trusted ApplicationPlugins folder/sub-folder. This latter option is intended for applications in the Autoloader bundle format, but also works for non-Autoloader applications. While not ideal from a logical perspective, if applications don’t contain a valid XML manifest then they simply won’t get loaded by the Autoloader.</p>
<p>The good news is that if an application module was signed using a <span style="text-decoration: underline;">trusted</span> digital certificate, then it doesn’t matter if it’s in a trusted location or not*. Which takes us to our next point…</p>
<p>[* It’s also worth noting that the inverse is true: modules found in trusted locations don’t have their digital signatures checked by AutoCAD, as they get trusted automatically.]</p>
<p>Secondly, it’s possible for modules <strong>signed</strong> with a particular <strong>digital certificate</strong> to be trusted. If the “Always trust applications from” option is selected, that’s what happens: the user won’t be asked the question for any other application modules, or when the module is loaded in future. Checking this option results in the digital certificate being added to the Windows certificate store.</p>
<p>If you launch <em>certmgr.msc</em>, you’ll see the contents of this store on your system:</p>
<p><a href="http://through-the-interface.typepad.com/.a/6a00d83452464869e201b7c79526df970b-pi" target="_blank"><img alt="Trusted publishers in the Windows Certificate Store" border="0" height="251" src="/assets/image_508873.jpg" style="background-image: none; float: none; padding-top: 0px; padding-left: 0px; margin: 20px auto; display: block; padding-right: 0px; border-width: 0px;" title="Trusted publishers in the Windows Certificate Store" width="394" /></a></p>
<p>It’s possible to add this certificate to the store at install time, even though MSI doesn’t support it directly. <a href="http://adndevblog.typepad.com/autocad/2015/04/how-to-avoid-trust-this-publisher-dialog.html" target="_blank">Over on the ADN DevBlog</a>, Madhukar has described a number of options for achieving this using a custom action, from calling the <em>certutil.exe</em> to using the Microsoft CryptoAPI from C# or C++. I haven’t yet had to do this, myself, but it seems that adding this certificate on installation isn’t unreasonable.</p>
<p><strong>Conclusion</strong></p>
<p>Now is a really good time to start signing your program modules. This will allow AutoCAD to inform your users in case your application gets tampered with by a malicious process (or person!). Once you’ve signed your modules, you can either have your digital certificate added to the certificate store at install time or the first time one of your modules is loaded (documenting this manual step is clearly an option, if you’d rather make the trusting of your application an explicit user action).</p>
<p>You might also rely on the trusted locations mechanism, by installing to AutoCAD’s program files folder or to ApplicationPlugins. Signing modules is a good idea, irrespective of where they get installed to, so my own preference would be to follow that path.</p>
<p>That’s it for this series, unless someone raises some concerns that haven’t been addressed by these two posts. On Monday I’m planning to unveil a new look for this blog, so get your mobile devices ready… :-)</p>
<p><strong><em>Update:</em></strong></p>
<p>I’ve been told (thanks, George!) of the existence of a <a href="http://knowledge.autodesk.com/support/autocad/downloads/caas/downloads/content/autodesk-C2-AE-autocad-C2-AE-2016-lisp-signing-command-line-tool.html" target="_blank">command-line tool for signing AutoLISP modules</a>. This is clearly helpful when integrating digital signing into your build process.</p>
<p><strong><em>Appendix</em></strong></p>
<ul>
<li>Product documentation
<ul>
<li><a href="http://knowledge.autodesk.com/search-result/caas/CloudHelp/cloudhelp/2016/ENU/AutoCAD-Customization/files/GUID-0A93626D-8389-45FC-969B-B86A2F37D691-htm.html" target="_blank">About Digitally Signing Custom Program Files</a></li>
<li><a href="http://knowledge.autodesk.com/search-result/caas/CloudHelp/cloudhelp/2016/ENU/AutoCAD-Core/files/GUID-EDBB6671-94A9-4B0C-A6F2-BC3EFBBBCBC2-htm.html" target="_blank">About Digital Signatures for Executable Files</a></li>
<li><a href="http://knowledge.autodesk.com/search-result/caas/CloudHelp/cloudhelp/2016/ENU/AutoCAD-Customization/files/GUID-5CE8D264-FB3B-4E66-A637-183EB999F210-htm.html" target="_blank">About Loading A Digitally Signed Custom Program File</a></li>
<li><a href="http://knowledge.autodesk.com/search-result/caas/CloudHelp/cloudhelp/2016/ENU/AutoCAD-Customization/files/GUID-26D7B31C-4165-410C-9FC4-2D556749D517-htm.html" target="_blank">To Make a Digital Certificate</a></li>
<li><a href="http://knowledge.autodesk.com/search-result/caas/CloudHelp/cloudhelp/2016/ENU/AutoCAD-Customization/files/GUID-DC1B25FE-E063-486C-B90C-565AB5E87BBC-htm.html" target="_blank">To Create A Personal Information Exchange (PFX) File</a></li>
<li><a href="http://knowledge.autodesk.com/search-result/caas/CloudHelp/cloudhelp/2016/ENU/AutoCAD-Customization/files/GUID-19D6716A-0AD1-4A7A-82BA-A067E6D65F66-htm.html" target="_blank">To Import a Digital Certificate</a></li>
<li><a href="http://knowledge.autodesk.com/search-result/caas/CloudHelp/cloudhelp/2016/ENU/AutoCAD-Customization/files/GUID-A63E8C40-6870-4874-BF7E-FD75E87268AA-htm.html" target="_blank">To Digitally Sign an AutoLISP File</a></li>
<li><a href="http://knowledge.autodesk.com/search-result/caas/CloudHelp/cloudhelp/2016/ENU/AutoCAD-Customization/files/GUID-3DA95353-9EF3-4E29-9671-6AEB7704EBE6-htm.html" target="_blank">To Digitally Sign a Binary (ObjectARX or Managed .NET) File</a></li>
<li><a href="http://knowledge.autodesk.com/search-result/caas/CloudHelp/cloudhelp/2016/ENU/AutoCAD-Customization/files/GUID-AA7BBBED-98D0-4003-8C80-D66173664DBA-htm.html" target="_blank">To Digitally Sign a Binary (ObjectARX or Managed .NET) File with a Post-Build Event in Microsoft Visual Studio</a></li>
</ul>
</li>
<li>ADN DevBlog
<ul>
<li><a href="http://adndevblog.typepad.com/autocad/2015/05/autocad-2016-trusted-paths-and-autoloader.html" target="_blank">AutoCAD 2016: Trusted paths and AutoLoader</a></li>
<li><a href="http://adndevblog.typepad.com/autocad/2015/04/how-to-avoid-trust-this-publisher-dialog.html" target="_blank">How to avoid ‘Trust This Publisher’ Dialog</a></li>
<li><a href="http://adndevblog.typepad.com/autocad/2015/01/digitally-signing-plug-in-files.html" target="_blank">Digitally signing plug-in files</a></li>
</ul>
</li>
<li>Dieter Schlaepfer’s posts on Lynn Allen’s blog
<ul>
<li><a href="http://lynn.blogs.com/lynn_allens_blog/2015/04/autocad-hacked-part-1.html" target="_blank">AutoCAD Hacked! - Part 1</a></li>
<li><a href="http://lynn.blogs.com/lynn_allens_blog/2015/04/autocad-hacked-part-2.html" target="_blank">AutoCAD Hacked! - Part 2</a></li>
</ul>
</li>
</ul>
