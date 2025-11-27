---
layout: "post"
title: "\"The installation failed\" for app store plug-ins "
date: "2018-04-04 22:24:18"
author: "Adam Nagy"
categories:
  - "Adam"
  - "Fusion 360"
original_url: "https://modthemachine.typepad.com/my_weblog/2018/04/the-installation-failed-for-app-store-plug-ins.html "
typepad_basename: "the-installation-failed-for-app-store-plug-ins"
typepad_status: "Publish"
---

<p>When installing a <strong>Fusion 360</strong> app from the <a href="https://apps.autodesk.com/FUSION/en/Home/Index">Autodesk App Store</a>, the installer simply copies the so called <strong>bundle</strong> (hence the <strong>.bundle</strong> extension on the folder) into the appropriate &quot;<strong>ApplicationPlugins</strong>&quot; folder - the location depends on where you installed <strong>Fusion 360</strong> from:</p>
<ul>
<li><strong>Fusion 360</strong> installed from the&#0160;<strong>Autodesk web site</strong>: ~/Library/Application Support/Autodesk/<strong>ApplicationPlugins</strong></li>
<li><strong>Fusion 360</strong> installed from the <strong>Mac App Store</strong>: ~/Library/Containers/com.autodesk.mas.fusion360/Data/Library/Application Support/Autodesk/<strong>ApplicationPlugins</strong></li>
</ul>
<p>For some reason, you may have the access rights for the folder set to &quot;<strong>Read only</strong>&quot;, and so the installer cannot write into it:</p>
<p><a class="asset-img-link" href="http://modthemachine.typepad.com/.a/6a00e553fcbfc6883401b8d2c6ff66970c-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="ReadOnly" class="asset  asset-image at-xid-6a00e553fcbfc6883401b8d2c6ff66970c img-responsive" src="/assets/image_335236.jpg" title="ReadOnly" /></a></p>
<p>And that can cause the installation to fail:</p>
<p><a class="asset-img-link" href="http://modthemachine.typepad.com/.a/6a00e553fcbfc6883401b8d2c6feb5970c-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="InstallationFailed" class="asset  asset-image at-xid-6a00e553fcbfc6883401b8d2c6feb5970c img-responsive" src="/assets/image_567773.jpg" title="InstallationFailed" /></a></p>
<p>You can simply <strong>right-click</strong> on the correct &quot;<strong>ApplicationPlugins</strong>&quot; folder, select &quot;<strong>Get Info</strong>&quot; and provide &quot;<strong>Read &amp; Write</strong>&quot; in the &quot;<strong>Privilege</strong>&quot; column for your account.</p>
<p>Hopefully, that should solve the issue.</p>
<p><strong>If that does not help</strong>, then you could follow these steps:</p>
<p>1) Get an app that can read <strong>pkg</strong> files, like&#0160;<a href="http://www.mothersruin.com/software/SuspiciousPackage/">http://www.mothersruin.com/software/SuspiciousPackage/</a></p>
<p>2) In that program, open the <strong>pkg</strong> file that you downloaded from the <strong>Autodesk App Store</strong></p>
<p>3) Select the <strong>.bundle</strong> folder, click &quot;<strong>Export</strong>&quot; and navigate to the appropriate &quot;<strong>ApplicationPlugins</strong>&quot; folder:</p>
<ul>
<li><strong>Fusion 360</strong> installed from the&#0160;<strong>Autodesk web site</strong>: ~/Library/Application Support/Autodesk/<strong>ApplicationPlugins</strong></li>
<li><strong>Fusion 360</strong> installed from the <strong>Mac App Store</strong>: ~/Library/Containers/com.autodesk.mas.fusion360/Data/Library/Application Support/Autodesk/<strong>ApplicationPlugins</strong></li>
</ul>
<p><a class="asset-img-link" href="http://a0.typepad.com/6a0112791b8fe628a401b7c95dfed0970b-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="Bundle_export" class="asset  asset-image at-xid-6a0112791b8fe628a401b7c95dfed0970b img-responsive" src="/assets/image_759201.jpg" title="Bundle_export" /></a></p>
<p>4) Then click &quot;<strong>Save</strong>&quot;</p>
<p><a class="asset-img-link" href="http://modthemachine.typepad.com/.a/6a00e553fcbfc6883401b8d2e83dd9970c-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="Bundle_save" class="asset  asset-image at-xid-6a00e553fcbfc6883401b8d2e83dd9970c img-responsive" src="/assets/image_237762.jpg" title="Bundle_save" /></a></p>
<p>Once you restart <strong>Fusion 360</strong>, it should now be able to find and load the <strong>add-in</strong>.</p>
