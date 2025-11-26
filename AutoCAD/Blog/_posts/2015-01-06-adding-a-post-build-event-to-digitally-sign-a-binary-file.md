---
layout: "post"
title: "Adding a Post-Build Event to Digitally Sign a Binary File"
date: "2015-01-06 22:33:15"
author: "Madhukar Moogala"
categories:
  - ".NET"
  - "2015"
  - "Madhukar Moogala"
original_url: "https://adndevblog.typepad.com/autocad/2015/01/adding-a-post-build-event-to-digitally-sign-a-binary-file.html "
typepad_basename: "adding-a-post-build-event-to-digitally-sign-a-binary-file"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/Madhukar-Moogala.html" target="_self">Madhukar Moogala</a></p>
<p>&#0160;</p>
<p>This blog post assumes reader is aware of digital signatures ,if not please refer following link <a href="http://msdn.microsoft.com/en-us/library/ie/ms537361%28v=vs.85%29.aspx">Code Signing</a></p>
<p>I will demonstrate how you can you add a&#0160; simple code command line to digital sign your executable files like .exe \.dll.</p>
<p>If you are not aware of post build event feature in VS project , please go through following link. <a href="http://msdn.microsoft.com/en-us/library/ms180786%28v=vs.90%29.aspx">Post Build Event</a></p>
<p>&#0160;</p>
<p>On a Windows machine SignTool.exe is available from Microsoft that enables signing executable files. It can be used to sign both the managed (.NET) and native files. The sign tool takes the pfx file with the public and the private key as the input.</p>
<p>Post build Command Line :</p>
<p><strong>Usage</strong> : signtool sign&#0160; /f &lt;yourPfxfile&gt; /p &lt;password&gt;&#0160; &lt;executablefile&gt;</p>
<p><strong>/f</strong> is signcertfile , Specifies the signing certificate in a file. Only the Personal Information Exchange (PFX) file format is supported.If the file is in PFX format protected by a password, use the <strong>/p</strong> option to specify the password.</p>
<p>&#0160;</p>
<p>&#0160;</p>
<p><strong>Example</strong> : call &quot;C:\Program Files (x86)\Windows Kits\8.0\bin\x86\signtool.exe&quot; sign /f &quot;C:\mkCert\mypfxfile.pfx&quot; /p &quot;autodesk123&quot; $(TargetPath)</p>
<p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d0b98d68970c-pi"><img alt="PostBuildEvent" border="0" height="244" src="/assets/image_114837.jpg" style="background-image: none; padding-top: 0px; padding-left: 0px; display: inline; padding-right: 0px; border-width: 0px;" title="PostBuildEvent" width="230" /></a></p>
<p>For further reading and other command line options ,please refer</p>
<p><a href="http://msdn.microsoft.com/en-us/library/aa387764%28v=vs.90%29.aspx">SignTool</a></p>
<p>&#0160;</p>
<h3>What is PFX file and How to get one ?</h3>
<p>Personal Format Exchange is a Microsoft format extension for <a href="http://en.wikipedia.org/wiki/PKCS_12"><strong>PKCS#12</strong></a><strong> , </strong>contains public and private keys for the associated certificate , these certificates are issued by Certificate Authorities and can be purchased from vendors like Verisign&#0160; etc, more information on issuing authorities can be found <a href="http://en.wikipedia.org/wiki/Certificate_authority">Certificate Authority</a>.</p>
<p>We can also create our own certificate for testing and internal distribution, paraphrasing wiki definition of Digital Certificate which we are about to create.</p>
<blockquote>
<p><em>A digital certificate certifies the ownership of a public key by the named subject of the certificate. This allows others (relying parties) to rely upon </em><a href="http://en.wikipedia.org/wiki/Digital_signature"><em>signatures</em></a><em> or on assertions made by the private key that corresponds to the certified public key. In this model of trust relationships, a CA is a </em><a href="http://en.wikipedia.org/wiki/Trusted_third_party"><em>trusted third party</em></a><em> - trusted both by the subject (owner) of the certificate and by the party relying upon the certificate.</em></p>
</blockquote>
<p>&#0160;</p>
<p>&#0160;</p>
<p>We need two tools to create Certificate “MakeCert” and “Pvk2Pfx” these are shipped with&#0160; Visual Studio, launch Developer Command Prompt</p>
<p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d0b98d6c970c-pi"><img alt="DeveloperCommandPrompt" border="0" height="199" src="/assets/image_560562.jpg" style="background-image: none; padding-top: 0px; padding-left: 0px; display: inline; padding-right: 0px; border-width: 0px;" title="DeveloperCommandPrompt" width="244" /></a></p>
<h6>&#0160;</h6>
<h4>1. MakeCert</h4>
<blockquote>
<h6><span style="font-size: small;">This tool outputs the certificate as a .cer file and generates a pvk file it is not exist.</span></h6>
<p>Usage :</p>
<p>makecert <strong>-sv</strong> yourprivatekeyfile.pvk <strong>-n</strong> &quot;cert name&quot; yourcertfile.cer <strong>-b</strong> mm/dd/yyyy <strong>-e</strong> mm/dd/yyyy <strong>–r</strong></p>
<p>Example :</p>
<p>makecert -sv C:\DigiCertificate\myPrivateKey.pvk -n &quot;CN=\&quot;Madhukar,M\&quot; &quot; C:\DigiCertificate\myCertifacte.cer -b 01/07/2015 -e 01/30/2015 –r</p>
<p>while mentioning&#0160; name against ‘-n’ option , you need follow X500 naming standard , characters like quotes comma etc. are reserved ,you need to use escape codes to get them or else makecert throws an error</p>
</blockquote>
<blockquote>
<p><em>Error: CryptCertStrToNameW failed =&gt; 0x80092023 (-2146885597) <br />Failed</em></p>
<p>For more information on <a href="http://msdn.microsoft.com/en-us/library/aa366101%28v=vs.85%29.aspx">Distinguished Names</a></p>
</blockquote>
<blockquote>
<p>You will be prompted to set the password for the private key file.</p>
<p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d0b98d75970c-pi"><img alt="CreatePVKPwd" border="0" height="161" src="/assets/image_841728.jpg" style="background-image: none; padding-top: 0px; padding-left: 0px; display: inline; padding-right: 0px; border-width: 0px;" title="CreatePVKPwd" width="244" /></a></p>
</blockquote>
<blockquote>
<p>You will again be prompted to enter the password to sign the certificate. Please note that this is a self-signed certificate and so you will be signing the public key with the private key.</p>
<p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb07d40726970d-pi"><img alt="EnterPvkPassword" border="0" height="156" src="/assets/image_25545.jpg" style="background-image: none; padding-top: 0px; padding-left: 0px; display: inline; padding-right: 0px; border-width: 0px;" title="EnterPvkPassword" width="244" /></a></p>
<p>Here is a snap shot of a self-signed certificate ,aka cer file.</p>
<p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d0b98d7c970c-pi"><img alt="CerFile" border="0" height="244" src="/assets/image_500508.jpg" style="background-image: none; padding-top: 0px; padding-left: 0px; display: inline; padding-right: 0px; border-width: 0px;" title="CerFile" width="180" /></a></p>
</blockquote>
<blockquote>
<p>The certificate has the following information.</p>
</blockquote>
<ul>
<li>Version number of the certificate, the current version is 3.</li>
<li>Certificate serial number, the unique serial number is specific to a CA.</li>
<li>Signature, it includes the algorithm used for signing.</li>
<li>Issuer name, the certificate authority name.</li>
<li>Validity period of the certificate.</li>
<li>Subject name, in this case the publisher name.</li>
<li>Subject public key info, the public key of the publisher.</li>
</ul>
<blockquote>
<p>Here is a snap shot of cer file details.</p>
<p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d0b98d86970c-pi"><img alt="CerDetails" border="0" height="244" src="/assets/image_560617.jpg" style="background-image: none; padding-top: 0px; padding-left: 0px; display: inline; padding-right: 0px; border-width: 0px;" title="CerDetails" width="189" /></a></p>
</blockquote>
<h4>2. Pvk2pfx</h4>
<blockquote>
<p>This tool is used to create a pfx file that can be used for signing. The pfx file has the certificate, the public and the private key of the publisher.</p>
<p>Usage:</p>
<p>pvk2pfx <strong>–pvk</strong> yourprivatekeyfile.pvk -<strong>pi</strong> password <strong>–spc</strong> yourcertfile.cer <strong>–pfx</strong> yourpfxfile.pfx <strong>–po </strong>yourpfxpassword</p>
<p>Example:</p>
<p>pvk2pfx -pvk C:\DigiCertificate\myPrivateKey.pvk –pi pvkpassword -spc C:\DigiCertificate\myCertifacte.cer -pfx C:\DigiCertificate\myPfxfile.pfx –po pfxpassword</p>
<p>Note : -pi &lt;pvk-pswd&gt;&#0160;&#0160; - PVK password. <br />&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; -po &lt;pfx-pswd&gt;&#0160;&#0160; - PFX password; same as -pi if not given.</p>
</blockquote>
<p>At the end of this operation you will have a .pfx file that can be used for signing as stated above in the post.</p>
