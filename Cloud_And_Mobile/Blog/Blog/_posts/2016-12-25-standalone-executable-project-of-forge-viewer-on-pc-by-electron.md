---
layout: "post"
title: "Standalone Executable Project of Forge Viewer on PC without Internet by Electron"
date: "2016-12-25 22:44:19"
author: "Xiaodong Liang"
categories:
  - "Javascript"
  - "View and Data API"
  - "Xiaodong Liang"
original_url: "https://adndevblog.typepad.com/cloud_and_mobile/2016/12/standalone-executable-project-of-forge-viewer-on-pc-by-electron.html "
typepad_basename: "standalone-executable-project-of-forge-viewer-on-pc-by-electron"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/cloud_and_mobile/xiaodong-liang.html" target="_self">Xiaodong Liang</a></p>
<p>&#0160;<img alt="" src="/assets/Forge%20Viewer-2.11-green.svg" /><img alt="" src="/assets/Electron-1.4.11-orange.svg" /></p>
<p><a href="https://developer.autodesk.com/en/docs/viewer/v2">Forge Viewer</a> is a browser based technology, i.e. structure of BS. Although Forge Viewer supports <a href="http://adndevblog.typepad.com/cloud_and_mobile/2015/10/offline-support-with-view-data-api.html">offline mode</a>, you will still need to setup a server-client environment. It is not a problem to run a localhost on the PC, however it would not much be practicable if you configure a localhost on the PC of your customers.&#0160;</p>
<p>Recently, I got to know <a href="http://electron.atom.io/">Electron</a>. As it says: Electron provides the way to build cross platform desktop apps with JavaScript, HTML, and CSS. By this mean, your project will be looked like a standalone executable project, similar to&#0160;typical desktop application. Electron accomplishes this by combining Chromium and Node.js into a single runtime and apps can be packaged for Mac, Windows, and Linux. So if you worked with a Node.js project, it will be more easier to migrate it to an Election stuff.&#0160;</p>
<p>Quite a lot of tutorials are available with <a href="http://electron.atom.io/">Electron</a> community. In this blog, I will simply describe how to build a skeleton with a demo of Forge Viewer in offline mode.</p>
<p>1. Clone the minimal Electron app, install the npm modules and test&#0160;the app.&#0160;</p>
<pre><code>
   # Clone this repository
   git clone https://github.com/electron/electron-quick-start
   # Go into the repository
   cd electron-quick-start
   # Install dependencies
   npm install
   # Run the app
   npm start
</code></pre>
<p>The app will look like the snapshot below. We can even debug, check network, element etc. These are similar to the practices in Chrome.</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d24a8b6c970c-pi" style="float: left;"><img alt="Electron-hello-world" class="asset  asset-image at-xid-6a0167607c2431970b01b8d24a8b6c970c img-responsive" src="/assets/image_0bf960.jpg" style="margin: 0px 5px 5px 0px;" title="Electron-hello-world" /></a></p>
<p>&#0160;</p>
<p>&#0160;</p>
<p>&#0160;</p>
<p>&#0160;</p>
<p>&#0160;</p>
<p>&#0160;</p>
<p>&#0160;</p>
<p>&#0160;</p>
<p>For more details of the skeleton, please refer to <a href="http://electron.atom.io/docs/tutorial/quick-start">http://electron.atom.io/docs/tutorial/quick-start</a>.</p>
<p>2. On <a href="https://extract.autodesk.io/">Extract tool of Forge Viewer</a>, upload your model file and follow those steps to get the offline package. Unzip the package</p>
<p>3. Create a new folder in the root of Electron project (say ‘Forge’), copy the contents of the package in #2 to this folder.</p>
<p>4. Edit main.js of Electron project to load the <em>index.html</em> in Forge folder.&#0160;</p>
<pre><code>
  // and load the index.html of the app.
  mainWindow.loadURL(url.format({
    pathname: path.join(__dirname, &#39;./Forge/index.html&#39;),
    protocol: &#39;file:&#39;,
    slashes: true
  }))
</code></pre>
<p>5. npm start to run the application. a failure would probably occur that indicates jQuery is not defined. This is because of this code:</p>
<pre><code>
  if ( typeof module === &quot;object&quot; &amp;&amp; typeof module.exports === &quot;object&quot; ) {
  // set jQuery in `module`
   } else {
  // set jQuery in `window`
  }
</code></pre>
<p>The jQuery code &quot;sees&quot; that its running in a CommonJS environment and ignores window. With the <a href="http://stackoverflow.com/questions/32621988/electron-jquery-is-not-defined">workaround</a>, add two lines in ./Forge/index.html:</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb0963b438970d-pi" style="float: left;"><img alt="Electron-workaround" class="asset  asset-image at-xid-6a0167607c2431970b01bb0963b438970d img-responsive" src="/assets/image_ccf165.jpg" style="margin: 0px 5px 5px 0px;" title="Electron-workaround" /></a></p>
<p>&#0160;</p>
<p>&#0160;</p>
<p>&#0160;</p>
<p>now, you can see the project is running well. To make it fits well with the window, I adjusted height of div of viewer to 550px. In addition, the application icon can be modified by editing the line in main.js.&#0160;</p>
<pre><code>
function createWindow () {
  // Create the browser window.
  mainWindow = new BrowserWindow({width: 800, height: 600,icon: __dirname + &#39;./Forge/Tools.ico&#39;})<br /><span style="font-family: Georgia;"><br /></span></code></pre>
<p>6. The steps above are for debugging mode. To make a release version, install the <a href="https://www.npmjs.com/package/electron-packager">electron-packager</a>. in the terminal, type:</p>
<pre><code>
npm install electron-packager -g
</code></pre>
<p>next, follow the format below to package:</p>
<pre><code>
      electron-packager &lt; sourcedir &gt; &lt; appname &gt; --platform=&lt; platform &gt; --arch=&lt; arch &gt; [optional flags...]
</code></pre>
<p>e.g. on my Windows 10 OS, electron-packager will produce Windows version by default.</p>
<pre><code>
   electron-packager  “C:\temp\electron-quick-start” MyForgeApp
</code></pre>
<p>after the packing, a new folder will be generated. There is an executable application in the folder. Now, you can distribute the package as a typcical desktop application to your customers.</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c8c0c5eb970b-pi" style="float: left;"><img alt="Electron-release" class="asset  asset-image at-xid-6a0167607c2431970b01b7c8c0c5eb970b img-responsive" src="/assets/image_6c188a.jpg" style="margin: 0px 5px 5px 0px;" title="Electron-release" /></a></p>
<p>&#0160;</p>
<p>&#0160;</p>
<p>&#0160;</p>
<p>&#0160;</p>
<p>&#0160;</p>
<p>&#0160;</p>
<p>The full source of my demo is available at:</p>
<p><a href="https://github.com/xiaodongliang/Forge-Viewer-Electron-PC">https://github.com/xiaodongliang/Forge-Viewer-Electron-PC</a></p>
<p>From what I googled, it is a bit pity Electron has not well supported the version for mobile. I am looking for other approaches such as Cordovar. If you know any ways, I&#39;d love to have your sharing. Thank you.</p>
<p>Update: I have figured out one way to produce app of iOS by Cordova :) This is the other blog which tells more:</p>
<p><a href="http://adndevblog.typepad.com/cloud_and_mobile/2017/01/standalone-app-of-forge-viewer-on-ios-by-cordova.html%20">http://adndevblog.typepad.com/cloud_and_mobile/<span id="permalink-year">2017</span>/<span id="permalink-month">01</span>/<span id="permalink-text-preview">standalone-app-of-forge-viewer-on-ios-by-cordova.html</span> </a></p>
