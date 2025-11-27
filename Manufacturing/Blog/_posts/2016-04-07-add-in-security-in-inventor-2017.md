---
layout: "post"
title: "Add-In security in Inventor 2017"
date: "2016-04-07 06:46:05"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "Inventor"
original_url: "https://adndevblog.typepad.com/manufacturing/2016/04/add-in-security-in-inventor-2017.html "
typepad_basename: "add-in-security-in-inventor-2017"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/manufacturing/adam-nagy.html" target="_self">Adam Nagy</a></p>
<p>Couple of changes in <strong>Inventor 2017</strong>:</p>
<p><strong>1) Digitally signed add-in</strong></p>
<p><strong>Inventor 2017</strong>&#0160;provides&#0160;a new way of making an <strong>add-in</strong> load without the security dialog popping up: the <strong>add-in dll</strong> needs to be <strong>digitally signed&#0160;</strong>with a certificate ...</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c82f8c9b970b-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="SignedAddIn" class="asset  asset-image at-xid-6a0167607c2431970b01b7c82f8c9b970b img-responsive" src="/assets/image_96094a.jpg" title="SignedAddIn" /></a></p>
<p>... and the <strong>publisher</strong>&#0160;of the certificate needs to be in the &quot;<strong>Trusted Publishers</strong>&quot; list:</p>
<p><img alt="TrustedPublisher" class="asset  asset-image at-xid-6a0167607c2431970b01bb08d42856970d img-responsive" src="/assets/image_01e1b7.jpg" title="TrustedPublisher" /></p>
<p>This is something that could be done by your app&#39;s&#0160;installer - e.g. the installer provided for <a href="https://apps.autodesk.com/INVNTOR/en/Home/Index">App&#0160;Store</a>&#0160;plugins will do it as well if your <strong>add-in</strong> is <strong>digitally signed</strong>.</p>
<p>You can follow the same procedures as in case of signing any other files, including an <strong>AutoCAD</strong> add-in:<br /><a href="http://adndevblog.typepad.com/autocad/2015/01/digitally-signing-plug-in-files.html">http://adndevblog.typepad.com/autocad/2015/01/digitally-signing-plug-in-files.html</a></p>
<p>Here is also an article listing a few digital certificate providers:<br /><a href="http://adndevblog.typepad.com/manufacturing/2015/04/do-you-want-to-load-this-add-in.html">http://adndevblog.typepad.com/manufacturing/2015/04/do-you-want-to-load-this-add-in.html</a>&#0160;</p>
<p><strong>2) AddInLoadRules in Inventor 2017</strong></p>
<p><strong>&quot;<span class="s1"><strong>AddInLoadRules.xml</strong></span>&quot; </strong>files were introduced in <strong>Inventor 2016</strong> and are used to track which <strong>add-ins</strong> should be allowed to load:&#0160;<strong><br /><a href="https://knowledge.autodesk.com/support/inventor-products/troubleshooting/caas/CloudHelp/cloudhelp/2016/ENU/Inventor-Install/files/GUID-84B221D3-979B-420D-B955-9DCBDC0C5619-htm.html">https://knowledge.autodesk.com/support/inventor-products/troubleshooting/caas/CloudHelp/cloudhelp/2016/ENU/Inventor-Install/files/GUID-84B221D3-979B-420D-B955-9DCBDC0C5619-htm.html</a></strong></p>
<p><strong>Inventor 2017</strong> is still using <strong>AddInLoadRules</strong> files with some changes compared to <strong>Inventor 2016</strong>.&#0160;In <strong>Inventor 2017</strong> we have the following files:</p>
<ul class="ul1">
<li class="li1"><span class="s2"><strong>Administrator settings</strong> are stored in an&#0160;<strong>xml</strong> file &quot;</span><span class="s3"><strong>C:\Program Files</strong>\Autodesk\Inventor 2017\Preferences\<strong>AddInLoadRules.xml</strong>&quot;</span></li>
<li class="li1"><span class="s2"><strong>User override settings</strong> are now stored in a <strong>binary</strong> file &quot;<strong>%APPDATA%</strong>\Autodesk\Inventor 2017\Addins\<strong>AddInLoadRules</strong>&quot;</span>
<ul>
<li class="li2"><span class="s3">If the user hasn’t saved any overrides, that <strong>binary</strong> file won’t exist yet</span></li>
<li class="li2"><span class="s3">Additionally, since in prior releases the user override settings were in an <strong>xml</strong>&#0160;file, we still honour those settings if that file is present - but only if the <strong>binary</strong> file is not present</span></li>
<li class="li2"><span class="s3">Once user&#0160;makes a change and save their settings, the new <strong>binary</strong> file is&#0160;created</span></li>
</ul>
</li>
</ul>
<p>&#0160;</p>
