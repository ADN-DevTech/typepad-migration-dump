---
layout: "post"
title: "Embedding Node.js in your web page with Tonic "
date: "2015-10-23 13:13:19"
author: "Philippe Leefsma"
categories:
  - "Philippe Leefsma"
  - "View and Data API"
original_url: "https://adndevblog.typepad.com/cloud_and_mobile/2015/10/embedding-nodejs-in-your-web-page-with-tonic-.html "
typepad_basename: "embedding-nodejs-in-your-web-page-with-tonic-"
typepad_status: "Publish"
---

<p>By&nbsp;<a href="http://adndevblog.typepad.com/cloud_and_mobile/philippe-leefsma.html">Philippe Leefsma</a></p>

<a class="asset-img-link"  style="display: inline;" href="https://tonicdev.com/" target="_blank"><img style="height:200px; margin:auto;" class="asset  asset-image at-xid-6a0167607c2431970b01b7c7e231f8970b image-full img-responsive" alt="Screen Shot 2015-10-23 at 22.22.23" title="Screen Shot 2015-10-23 at 22.22.23" src="/assets/image_2f7bce.jpg" border="0" /></a><br />

I stumbled across <a href="https://tonicdev.com/">Tonic</a> few days ago and decided to share it on the blog today. This is another pretty cool tool that revolves around node.js: basically it's a <a href="https://en.wikipedia.org/wiki/Read%E2%80%93eval%E2%80%93print_loop">REPL</a> for node that you can embed in any web page. Purpose? It could be used for example in your API documentation to let developers experiment the API or showcase your latest node package without even leaving the page...
<br><br>
Below are two examples that illustrates the use of our <a href="https://github.com/Developer-Autodesk/view-and-data-npm">View & Data npm package</a>. Oh did you say ES7...? Another nice feature is that Tonic is already supporting the <a href="http://blogs.msdn.com/b/eternalcoding/archive/2015/09/30/javascript-goes-to-asynchronous-city.aspx">async</a> syntax for JavaScript... Spoiled! 
<br>
You just have to replace ConsumerKey and ConsumerSecret with your own credentials obtained from our <a href="http://developer.autodesk.com">developer portal</a> and you are ready to go:

<br>
<br>

<script src="https://embed.tonicdev.com" data-element-id="tonic-script1"></script>
<script src="https://embed.tonicdev.com" data-element-id="tonic-script2"></script>

<div id="tonic-script1">
////////////////////////////////////////////////////
// Written by Philippe Leefsma 2015 - 
// ADN/Developer Technical Services
//
// Request View & Data token using async syntax
//
////////////////////////////////////////////////////
var request = require("request-promise");

var BASE_URL = 'https://developer.api.autodesk.com';
var VERSION = 'v1';

var config = {

  credentials: {
    ConsumerKey: "Your ConsumerKey goes here...",
    ConsumerSecret: "Your ConsumerSecret goes here..."
  },
    
  //API EndPoints
  endPoints:{
    authenticate: BASE_URL + 
       '/authentication/' + VERSION + '/authenticate'
  }
}

async function getToken() {

    var params = {
      client_secret: config.credentials.ConsumerSecret,
      client_id: config.credentials.ConsumerKey,
      grant_type: 'client_credentials'
    };
    
    return request.post(
        config.endPoints.authenticate,
        { form: params });
}

var response = await getToken();
</div>

<div id="tonic-script2">
////////////////////////////////////////////////////
// Written by Philippe Leefsma 2015 - 
// ADN/Developer Technical Services
//
// Creates or retrieve a bucket using View & Data npm package
//
////////////////////////////////////////////////////
var Lmv = require('view-and-data');
var request = require('request');
var fs = require('fs');

var config = {

  credentials: {
    ConsumerKey: "Your ConsumerKey goes here...",
    ConsumerSecret: "Your ConsumerSecret goes here..."
  }
}

////////////////////////////////////////////////////
// Create or Access a bucket
//
////////////////////////////////////////////////////
function bucketTest(bucketName) {

    var lmv = new Lmv(config);

    function onError(error) {
      console.log("Mayday we're going dooooooowwwwwwnnn!");  
      console.log(error);
    }
    
    function onInitialized(response) {
        
      console.log('onInitialized');  

      var createIfNotExists = true;

      var bucketCreationData = {
        bucketKey: bucketName,
        servicesAllowed: [],
        policy: "transient"
      };

      lmv.getBucket(bucketName,
        createIfNotExists,
        bucketCreationData).then(
          onBucketCreated,
          onError);
    }
    
    function onBucketCreated(response) {

        console.log('onBucketCreated');

        console.log(response);
    }
        
    lmv.initialise().then(onInitialized, onError);
    
    return 'bucketTest returned';
}

//Change bucket name too, needs to be unique...
bucketTest('adn-tonic-bucket');
</div>
