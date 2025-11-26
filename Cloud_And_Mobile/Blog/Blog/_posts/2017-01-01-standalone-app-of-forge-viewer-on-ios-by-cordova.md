---
layout: "post"
title: "Standalone App of Forge Viewer on iOS without Internet by Cordova "
date: "2017-01-01 23:53:36"
author: "Xiaodong Liang"
categories:
  - "Forge"
  - "iOS"
  - "iPad"
  - "iPhone"
  - "Mobile"
  - "Viewer"
  - "WebGL"
  - "Xiaodong Liang"
original_url: "https://adndevblog.typepad.com/cloud_and_mobile/2017/01/standalone-app-of-forge-viewer-on-ios-by-cordova.html "
typepad_basename: "standalone-app-of-forge-viewer-on-ios-by-cordova"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/cloud_and_mobile/xiaodong-liang.html" target="_self">Xiaodong Liang</a></p>
<p><img alt="" src="/assets/Forge%20Viewer-2.11-green.svg" /> <img alt="" src="/assets/Cordova-6.4.0-brightgreen.svg" /> <img alt="" src="/assets/MacOS-Sierra-orange.svg" /> <img alt="" src="/assets/Xcode-8.2.1-yellow.svg" /> <img alt="" src="/assets/iPhone-6-green.svg" /></p>
<p>Happy New Year !</p>
<p>In the <a href="http://adndevblog.typepad.com/cloud_and_mobile/2016/12/standalone-executable-project-of-forge-viewer-on-pc-by-electron.html">last post</a>, I introduced how to produce a standalone executable application for&#0160;Forge Viewer. At that time, I was also looking for the way that can apply to mobile OS. <a href="https://cordova.apache.org/">Apache Cordova</a> is one of the approaches I was investigating. The practice proved my choice is correct :) &#0160;By&#0160;Cordova, the app is successfully running on my iPhone (<span style="background-color: #ffff80;"><strong>no internet connection</strong>)</span>.&#0160;</p>
<p><a class="asset-img-link" href="http://a0.typepad.com/6a016764cbbcf9970b01b8d24cb020970c-pi"><img alt="ForgeViewer-Real-Device" class="asset  asset-image at-xid-6a016764cbbcf9970b01b8d24cb020970c img-responsive" src="/assets/image_222d21.jpg" style="display: block; margin-left: auto; margin-right: auto;" title="ForgeViewer-Real-Device" /></a></p>
<p>Quoted from&#0160;<a href="https://cordova.apache.org/">Apache Cordova</a>&#0160;: <em>Cordova is an open-source mobile development framework. It allows you to use standard web technologies - HTML5, CSS3, and JavaScript for cross-platform development. Applications execute within wrappers targeted to each platform, and rely on standards-compliant API bindings to access each device&#39;s capabilities such as sensors, data, network status, etc.</em></p>
<p>The usage of Cordova is very clear&#0160;and easy, while I learnt a lot when playing with it such as configuring the environments with necessary packages, &#0160;debugging a hybrid app with JavaScript, diagnosing the issues of Forge Viewer that is specific in such scenario etc. I&#39;d like to share the main steps/tricks. The whole demo source project is available at:</p>
<p><a href="https://github.com/xiaodongliang/Forge-Viewer-iOS-Cordova">https://github.com/xiaodongliang/Forge-Viewer-iOS-Cordova</a></p>
<p>1. Follow the steps to <a href="https://cordova.apache.org/docs/en/latest/guide/cli/index.html">create the app</a>. I&#39;d suggest to test in <strong>Browser</strong> mode firstly. It can help you to figure out generic issues that are not related with the mobile app. Once it works, the browser will display a helloworld webpage</p>
<p><a class="asset-img-link" href="http://a0.typepad.com/6a016764cbbcf9970b01bb0965c4d8970d-pi"><img alt="Cordova-Basic-Browser" class="asset  asset-image at-xid-6a016764cbbcf9970b01bb0965c4d8970d img-responsive" src="/assets/image_dcb8ab.jpg" style="display: block; margin-left: auto; margin-right: auto;" title="Cordova-Basic-Browser" /></a>2. On <a href="https://extract.autodesk.io/">Extract tool of Forge Viewer</a>, upload your model file and follow those steps to get the offline package. Unzip the package</p>
<p>3. Create a new folder in the Cordova project root\www\ (say ‘Forge’), copy the contents of the package&#0160;in #2 to this folder. The files index.exe and index.bat are unnecessary for this project. You can remove them.&#0160;</p>
<p>&#0160; &#0160; &#0160; &#0160; &#0160; &#0160;&#0160; <a class="asset-img-link" href="http://a3.typepad.com/6a016764cbbcf9970b01bb0965cceb970d-pi"><img alt="Cordova-Folder" class="asset  asset-image at-xid-6a016764cbbcf9970b01bb0965cceb970d img-responsive" src="/assets/image_cfb123.jpg" style="display: block; margin-left: auto; margin-right: auto;" title="Cordova-Folder" /></a></p>
<p>4. Copy some headers from root\www\index.html to root\www\Forge\index.html. Cordova sets strict security policy by default. For demo only, I simply commented out those headers. In reality, please make sure to set appropriate <a href="https://github.com/apache/cordova-plugin-whitelist/blob/master/README.md#content-security-policy">security policy</a>.</p>
<p>&#0160;&#0160; &#0160; &#0160; &#0160; &#0160;&#0160; <a class="asset-img-link" href="http://a1.typepad.com/6a016764cbbcf9970b01bb0965ccf1970d-pi"><img alt="Cordova-Updated-Index" class="asset  asset-image at-xid-6a016764cbbcf9970b01bb0965ccf1970d img-responsive" src="/assets/image_43157d.jpg" style="display: block; margin-left: auto; margin-right: auto;" title="Cordova-Updated-Index" /></a>5. Update&#0160;Cordova project root\config.xml to direct to Forge\index.html&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160;</p>
<pre><code>
  &lt; content src=&quot;Forge/index.html&quot; /&gt;
</code></pre>
<p>6. Build and run with browser again, it will run like the webpage below. That means the Viewer has been successfully integrated with the browser mode. you might need to diagnose in console if there is any issues that caused any failures.&#0160;</p>
<p>&#0160;&#0160; <a class="asset-img-link" href="http://a1.typepad.com/6a016764cbbcf9970b01b7c8c2dfb9970b-pi"><img alt="Cordova-Forge-Browser" class="asset  asset-image at-xid-6a016764cbbcf9970b01b7c8c2dfb9970b img-responsive" src="/assets/image_7687b7.jpg" style="display: block; margin-left: auto; margin-right: auto;" title="Cordova-Forge-Browser" /></a></p>
<p>7. &#0160;Now, follow <a href="https://cordova.apache.org/docs/en/latest/guide/cli/index.html">the steps on Cordova</a> to add platform of iOS. Run it on the Emulator. The deployment of the page is shown up, but the model is not loaded to the viewer. After some debugging I finally found &#0160;when&#0160;Forge Viewer tries to load the offline dataset, it submits an XHR to request the resource in the local storage. In the native browser mode, this will return HTTP response = 200 if succeeded, while on the hybrid mode, it returns &#0160;HTTP response = 0. So the initialization of loading model will fail.&#0160;The relevant codes are within lmvworker.min.js. After adding the condition, rebuild the app, and run it, the model is now loaded successfully.</p>
<p>8. Because of the size of a mobile, I removed the sibling panel and other text areas of the demo page, in order to see the viewer only. In Emulator, the app is looked like:</p>
<p>&#0160; <a class="asset-img-link" href="http://a4.typepad.com/6a016764cbbcf9970b01bb0965cd04970d-pi"><img alt="Cordova-iPhone-ForgeViewer-Emulator" class="asset  asset-image at-xid-6a016764cbbcf9970b01bb0965cd04970d img-responsive" src="/assets/image_4a9124.jpg" style="display: block; margin-left: auto; margin-right: auto;" title="Cordova-iPhone-ForgeViewer-Emulator" /></a></p>
<p>8. If everything works well, you could follow the steps on Apple to <a href="https://developer.apple.com/library/content/documentation/IDEs/Conceptual/AppDistributionGuide/TestingYouriOSApp/TestingYouriOSApp.html">distribute the app to the real device</a>.</p>
<p>In reality, it will be bad, even not allowed by App Store if packaging an app with huge model dataset. So the app should provide the channel either loading model from local storage (assume the model data set has been&#0160;copied to the device by other ways), or downloading the dataset when internet is available.&#0160;Actually, the solution&#0160;in this blog is mainly for those scenarios when internet is not available such as construction field. With the solution, a worker can still manage the model in offline, and sync the management with administration platform when internet is on.</p>
<p>I was also investigating the possibility to make app of <span style="color: #bf005f;"><strong>Android</strong></span>. Unfortunately, from <a href="http://stackoverflow.com/questions/15395245/enabling-webgl-support-for-android-webview">some discussions on internet, </a>it seems&#0160;WebView of Android has not supported WebGL nicely. Even though I tried to build the Android app by <a href="https://crosswalk-project.org/">CrossWalker,&#0160;</a>Forge Viewer still failed to initialize THREE.js render. If you know about the tricks, I&#39;d appreciate if you could share the experience.&#0160;</p>
<p>&#0160;</p>
