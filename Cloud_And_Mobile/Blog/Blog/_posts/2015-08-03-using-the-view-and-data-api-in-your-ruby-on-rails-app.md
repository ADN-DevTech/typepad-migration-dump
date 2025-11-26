---
layout: "post"
title: "Using the View and Data API in your Ruby on Rails app"
date: "2015-08-03 03:13:28"
author: "Pratham Alag"
categories:
  - "Pratham Alag"
  - "View and Data API"
  - "Web Server"
original_url: "https://adndevblog.typepad.com/cloud_and_mobile/2015/08/using-the-view-and-data-api-in-your-ruby-on-rails-app.html "
typepad_basename: "using-the-view-and-data-api-in-your-ruby-on-rails-app"
typepad_status: "Publish"
---

<p>By Pratham Makhni Alag&#0160;</p>
<p>If you&#39;re planning to use the View and Data API to incorporate a 3D viewer in your Ruby on Rails app, there is a fully functional Ruby gem out, called adn_viewer. You can use it for authentication, bucket creation, uploading files and even registering your uploaded model for translation and subsequent viewing.&#0160;<br /><br />The source code and documentation may be found on <a href="https://github.com/Developer-Autodesk/adn_viewer" target="_blank">adn_viewer</a>.<br /><br />This gem is installed in a Ruby script or Ruby or Rails app simply by adding the gem and its gem dependencies, namely curb, curb-fu, json and gon to the gemfile and installing the bundle.&#0160;<br /><br />The following functions are provided as of now:&#0160;</p>
<p>1. Getting an access token (required for any call to the server):</p>
<pre><code>Adn_Viewer.token(key, secret)<br /><br /></code></pre>
<p>2. Creating a bucket (required to store a model):</p>
<pre><code>Adn_Viewer.create_bucket(token, name, policy)</code><br /><br /></pre>
<p>3. Getting bucket details:</p>
<pre><code>Adn_Viewer.check_bucket(token, name)<br /><br /></code></pre>
<p>4. Getting a list of formats supported by the viewer (for pre-validification of uploaded files):</p>
<pre><code>Adn_Viewer.supported_formats(token)</code><br /><br /></pre>
<p>5. Uploading a file (replace name with name of bucket you want to upload the file to):</p>
<pre><code>Adn_Viewer.upload_file(token, name, filename, filepath)</code><br /><br /></pre>
<p>6. Register your uploaded file for translation:</p>
<pre><code>Adn_Viewer.register(token, urn)<br /><br /></code></pre>
<pre><code style="white-space: normal;"><span style="font-family: Arial, sans-serif; font-size: small;">The success responses on these calls are parsed, in usable JSON format and similar to the ones on the <a href="http://developer.api.autodesk.com/documentation/v1/vs_quick_start.html" target="_blank">Quickstart Tutorial</a>.</span></code></pre>
<p><span style="font-family: Arial, sans-serif; font-size: small;">Also, the gem documentation contains a few notes on correct formatting, as required with a few commands such as to extract the key after calling the upload_file function.&#0160;<br /><br />This gem requires Ruby &gt;= 2.0.0 and a key and secret generated from developer.autodesk.com! Finally, the current stable version of adn_viewer is 1.0.0.<br /></span></p>
<p>&#0160;</p>
<p><span style="font-family: Arial, sans-serif; font-size: small;"> <a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d142f09b970c-pi" style="display: inline;"><img alt="Screen Shot 2015-08-03 at 2.57.39 am" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b8d142f09b970c image-full img-responsive" src="/assets/image_44f28b.jpg" title="Screen Shot 2015-08-03 at 2.57.39 am" /></a></span></p>
<p><span style="font-family: Arial, sans-serif; font-size: small;">&#0160;</span></p>
<p><span style="font-family: Arial, sans-serif; font-size: small;">Additionally, two sample apps that utilize the View and Data api are provided.&#0160;<br /><br />The&#0160;adn_viewer_gem_test_app is a simple app that uses all the functions of the gem. It contains a tutorial on creating an RoR app with viewer incorporated: <a href="https://github.com/prathamalag1994/adn_viewer_gem_test_app" target="_blank">adn_viewer_gem_test_app</a><br /><br />The&#0160;sample-ruby-on-rails-app-prototyping is a more advanced app that doesn&#39;t use the gem, but shows what you can do with the API in RoR: <a href="https://github.com/Developer-Autodesk/sample-ruby-on-rails-app-prototyping" target="_blank">sample-ruby-on-rails-app-prototyping&#0160;</a><br /><br />The app for the same is setup on <a href="http://viewanddata.herokuapp.com/" target="_self">viewanddata.herokuapp.com</a></span></p>
<p>&#0160;</p>
<p><span style="font-family: Arial, sans-serif; font-size: small;"> <a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d142f0f2970c-pi" style="display: inline;"><img alt="Screen Shot 2015-08-03 at 3.09.04 am" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b8d142f0f2970c image-full img-responsive" src="/assets/image_3214d9.jpg" title="Screen Shot 2015-08-03 at 3.09.04 am" /></a><br /></span></p>
<pre><code>&#0160;</code></pre>
