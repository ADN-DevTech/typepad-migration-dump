---
layout: "post"
title: "First try at Cloud9, View & Data API boilerplate"
date: "2015-09-23 06:42:07"
author: "Philippe Leefsma"
categories:
  - "Browser"
  - "Client"
  - "Cloud"
  - "Javascript"
  - "Philippe Leefsma"
  - "View and Data API"
original_url: "https://adndevblog.typepad.com/cloud_and_mobile/2015/09/first-try-at-cloud9-viewdata-api-boilerplate.html "
typepad_basename: "first-try-at-cloud9-viewdata-api-boilerplate"
typepad_status: "Publish"
---

<p>By&nbsp;<a href="http://adndevblog.typepad.com/cloud_and_mobile/philippe-leefsma.html">Philippe Leefsma</a></p>
<p>I discovered <a href="https://c9.io/" target="_self">Cloud9</a> last week while working with external developers during our EMEA Cloud Accelerator in Prague, first feeling: impressed!</p>
<blockquote class="twitter-tweet" lang="en"><p lang="en" dir="ltr">EMEA Cloud Accelerator is starting in our fancy Prague office! One busy week of work around our cloud APIs ... <a href="http://t.co/20XoxmhqAm">pic.twitter.com/20XoxmhqAm</a></p>&mdash; Philippe Leefsma (@F3lipek) <a href="https://twitter.com/F3lipek/status/643424927970906116">September 14, 2015</a></blockquote>
<script async src="//platform.twitter.com/widgets.js" charset="utf-8"></script>

This is essentially a slick browser based IDE which allows you to easily deploy your project directly on the cloud, so you don't need to worry where you are going to host it - at least for quick prototyping and testing purpose. <br>
When creating new a project - named as workspace - you can use a predefined template or clone an existing repository from github or bitbucket and this is very straightforward.
<br>
<br>
In order to test that feature, I created a minimalistic boilerplate for the View & Data API project based on node.js : 

<a href="https://github.com/leefsmp/view.and.data-boilerplate">view.and.data-boilerplate</a>
<br>
<br>
Using that as template in Cloud9, I can literally deploy a viewer webapp on the cloud in couple of minutes, while at the same time being able to tweak it to my will. Hard to beat to be honest!
<br>
<br>
Here are the steps to get that up-and-running:
<br>
<br>
<strong>1.</strong> Sign up for View & Data API keys on our <a href="https://developer.autodesk.com/myapps/create?api=f509dfb8-d15c-49e0-9ca6-eedfce7cff7a">developer portal</a>   
<br>
<br>
<strong>2.</strong> Once you've got your keys, upload a test model. You can use one of our tool to do that without setup: <a href="http://models.autodesk.io">http://models.autodesk.io</a>
<br>
<br>
<strong>3.</strong> Sign up for a Cloud9 account if you haven't done it already, then create a new workspace using my boilerplate as template:


<a class="asset-img-link"  style="display: inline;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d15c58bc970c-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b01b8d15c58bc970c image-full img-responsive" alt="Screen Shot 2015-09-21 at 4.29.02 PM" title="Screen Shot 2015-09-21 at 4.29.02 PM" src="/assets/image_cec5a6.jpg" border="0" /></a>


<strong>4.</strong> Fire <em>"npm install"</em> from Cloud9 command line:

<br>
<br>

<a class="asset-img-link"  style="display: inline;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c7d2950c970b-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b01b7c7d2950c970b image-full img-responsive" alt="Screen Shot 2015-09-21 at 4.30.06 PM" title="Screen Shot 2015-09-21 at 4.30.06 PM" src="/assets/image_13ab3b.jpg" border="0" /></a>

<br>

<strong>5.</strong> In <em>www/js/viewer.js</em> replace the var <em>urn = '...'</em> with the urn of your model from step 2

<br>
<br>


<a class="asset-img-link"  style="display: inline;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb0876c328970d-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b01bb0876c328970d image-full img-responsive" alt="Screen Shot 2015-09-23 at 4.04.50 PM" title="Screen Shot 2015-09-23 at 4.04.50 PM" src="/assets/image_f1d5a2.jpg" border="0" /></a><br />

<strong>6.</strong> Create environment variables <em>CONSUMERKEY</em> and <em>CONSUMERSECRET</em> using your API keys from step 1

<br>
<br>

<a class="asset-img-link"  style="display: inline;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d15c5bd9970c-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b01b8d15c5bd9970c image-full img-responsive" alt="Screen Shot 2015-09-21 at 4.32.25 PM" title="Screen Shot 2015-09-21 at 4.32.25 PM" src="/assets/image_e1f4a7.jpg" border="0" /></a><br />



<strong>7.</strong> Start the Cloud9 project... you've got a running cloud viewer!

<br>
<br>

<a class="asset-img-link"  style="display: inline;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb0876c207970d-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b01bb0876c207970d image-full img-responsive" alt="Screen Shot 2015-09-21 at 4.33.43 PM" title="Screen Shot 2015-09-21 at 4.33.43 PM" src="/assets/image_143e95.jpg" border="0" /></a><br />




<a class="asset-img-link"  style="display: inline;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c7d29575970b-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b01b7c7d29575970b image-full img-responsive" alt="Screen Shot 2015-09-21 at 4.34.24 PM" title="Screen Shot 2015-09-21 at 4.34.24 PM" src="/assets/image_3829fb.jpg" border="0" /></a><br />
