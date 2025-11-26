---
layout: "post"
title: "Test REST API in an automatic way"
date: "2015-05-14 01:09:26"
author: "Daniel Du"
categories:
  - "Daniel Du"
  - "Javascript"
  - "Script"
  - "Server"
  - "View and Data API"
original_url: "https://adndevblog.typepad.com/cloud_and_mobile/2015/05/test-rest-api-in-an-automatic-way.html "
typepad_basename: "test-rest-api-in-an-automatic-way"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/cloud_and_mobile/daniel-du.html">Daniel Du</a></p>

<p>In <a href="http://adndevblog.typepad.com/cloud_and_mobile/2015/05/continuous-integration-practice-of-view-and-data-api-project-.html">previous post</a>, I learned that it is possible to integrate the build and test process automatically on new github push with Travis-CI, but as you can see, actually there is no test case yet. In this post I will keep sharing what I learned about the unit testing. Behavior Driven Development(BDD) or Test Driven Development(TDD) is very popular and recommended, which makes your code more robust and also makes you feel safe when refactoring your code.</p>

<p>I will start from the unit testing of the REST API, I use our basic view and data API sample <a href="https://github.com/duchangyu/workflow-node.js-view.and.data.api/tree/auto-testing-frisby">workflow-node.js-view.and.data</a>, the server side exposes a REST API to get access token, the viewer client will call this REST API to initialize the viewer and show models on web page. So I will do the unit test of the server side REST API first. There are quite a lot different test frameworks in node.js world, in this post I will use <a href="https://github.com/mhevery/jasmine-node">Jasmine-node</a> and <a href="">Frisby</a>, I do not have strong oponion of these frameworks, you may have your own faverite ones, I use them just becase they come to me first :) </p>

<h2>Install Jasmine and Frisby</h2>

<p>If you do not have [Node.js] installed, you should install it first, I will skip this part. </p>

<p><a href="https://github.com/jasmine/jasmine">Jasmine</a> is a Behavior Driven Development testing framework for JavaScript. I am using Jasmine-node here since I am working on a node.js project. You can install it goblally:</p>

<pre><code>npm install jasmine-node -g
</code></pre>

<p>Another test framework I use is <a href="http://frisbyjs.com/">Frisby</a>, which is a REST API testing framework built on node.js and Jasmine that makes testing API endpoints easy, fast, as it desclimed on it's homepage. You can install it with NPM:</p>

<pre><code>npm install frisby -g
</code></pre>

<p>To make them run on other environment, I need to add them into package.json of my node.js project so that they can be installed with 'npm install' command:</p>

<pre><code>{
  "name": "AdnViewerBasic",
  "version": "0.0.0",
  "dependencies": {

    "jasmine-node" : "*",
    "frisby" : "*"
  }
</code></pre>

<h2>Write test case and API implementation</h2>

<p>Firstly let's write our test case with Jasmine. The convention of Jasmine is that the test case file name should end with 'spec.js', for convenience I put all the specification files into a folder named as 'spec', when I run the test case, I use following command, Jasmine will run all the '*spec.js' in this folder: </p>

<pre><code>jasmine-node spec
</code></pre>

<p>Now I create an specification file for the REST API. To use Autodesk View and Data API, we need to create an App at http://developer.autodesk.com to get the consumer key and consummer secret, with this key pair to get the access token.Please refer to our <a href="http://developer-autodesk.github.io">code samples</a> if you are not familiar with view and data API. I need to create such REST API for the access token. For example, with the HTTP GET /api/token, returns the token in JSON similar like blow:</p>

<pre><code>{ token_type: 'Bearer',
    expires_in: 1799,
    access_token: 'agY5OU74WejC9fdWKZVMzsYvLYFU' 
}
</code></pre>

<p>Here is the simplest specification file, check whether the API end point is correct by returning HTTP status 200:</p>

<pre><code>var frisby = require('frisby');

frisby.create('Get access token')
  .get('http://localhost:3000/api/token')
    .expectStatus(200)

.toss();
</code></pre>

<p>And here is the implementation of the token API，please find the complete code from the <a href="https://github.com/duchangyu/workflow-node.js-view.and.data.api/tree/auto-testing-frisby">repo on github</a>.</p>

<pre><code>var express = require('express');
var request = require('request');

var router = express.Router();


///////////////////////////////////////////////////////////////////////////////
// Generates access token
///////////////////////////////////////////////////////////////////////////////
router.get('/token', function (req, res) {
    var params = {
        client_id:  process.env.ConsumerKey , 
        client_secret:  process.env.ConsumerSecret, 
        grant_type: 'client_credentials'
    }

    request.post(
        process.env.BaseUrl + '/authentication/v1/authenticate',
        { form: params },

        function (error, response, body) {
            if (!error &amp;&amp; response.statusCode == 200) {

                res.send(body);
            }
        });
});

module.exports = router;
</code></pre>

<h2>Run the test</h2>

<p>Now it is time to run the test. Wee launch the test with following command, which runs all <code>*spec.js</code> files in <code>spec</code> folder. </p>

<pre><code>jasmine-node spec
</code></pre>

<p><img src="/assets/image_fd19a4.jpg" alt="First time run, http status fail" /></p>

<p>As you can see from the screen-shot above, the test failed, it is expected to get http 200, but we got 500.</p>

<pre><code>Failures:

  1) Frisby Test: Get access token 
    [ GET http://localhost:3000/api/token ]
   Message:
     Expected 500 to equal 200.
   Stacktrace:
     Error: Expected 500 to equal 200.
</code></pre>

<p>Why? Oh, because we have not start our project yet. So we need to the launch the project in another tab of terminal, and then run the test again.</p>

<p>The test is passed:</p>

<p><img src="/assets/image_d5bd3e.jpg" alt="First time run, http status fail" /></p>

<pre><code>workflow-node.js-view.and.data.api dudaniel$ jasmine-node spec
.
Finished in 2.406 seconds
1 test, 1 assertion, 0 failures, 0 skipped
</code></pre>

<p>Next, let's do more work, the token API should return a JSON result, the response content type should be JSON, so the spec should be similar like below:</p>

<pre><code>var frisby = require('frisby');
frisby.create('Get access token')
  .get('http://localhost:3000/api/token')
    .expectStatus(200)
    .expectHeaderContains('content-type', 'application/json')

.toss();
</code></pre>

<p>If we run the test again, you will notice that the pass is failed, then we need to work on the token API to pass the test:</p>

<pre><code>   request.post(
        process.env.BaseUrl + '/authentication/v1/authenticate',
        { form: params },

        function (error, response, body) {
            if (!error &amp;&amp; response.statusCode == 200) {

                //set the content type to JSON
                res.setHeader('Content-Type', 'application/json');

                res.send(body);
            }
        });
</code></pre>

<p>Run the test again, now the test is passed:</p>

<pre><code>workflow-node.js-view.and.data.api dudaniel$ jasmine-node spec
.

Finished in 2.27 seconds
1 test, 2 assertions, 0 failures, 0 skipped
</code></pre>

<p>So, that's it about basic REST API testing with Frisby, you can learn more about frisby at <a href="http://frisbyjs.com/docs/api/">here</a>. Here is my complete test case for my token API</p>

<pre><code>var frisby = require('frisby');

frisby.create('Get access token')
  .get('http://localhost:3000/api/token')
    .expectStatus(200)
    .expectHeaderContains('content-type', 'application/json')
    .expectBodyContains('access_token')
    .expectJSONTypes({
        access_token : String,
        token_type : String,
        expires_in : Number
    })
    //the access token should contains {token_type : 'Bearer'}
    .expectJSON({token_type : 'Bearer'})
    .expectJSON({
        access_token : function(val) {
            //this is a valid sample access token
            var sample_token = '2974LErhmJIlyeewjp34lmfZaBpl';

            expect(val.length).toEqual(sample_token.length);
        }
    })


.toss();
</code></pre>

<h2>Hookup the test script with npm</h2>

<p>The continuous integration tool run the test automaticaly by running <code>npm test</code>, so we need to add our jasmine-node test to the package.json file as below:</p>

<pre><code>{
  "name": "AdnViewerBasic",
  "version": "0.0.0",
  "dependencies": {
    "jasmine-node" : "*",
    "frisby" : "*"
  },

  "scripts" : {
    "test": "jasmine-node spec"

  }
}
</code></pre>

<p>With that we can run the test by following command or continuous integration tool.</p>

<pre><code>$ npm test

&gt; AdnViewerBasic@0.0.0 test /Users/dudaniel/github/local/duchangyu/workflow-node.js-view.and.data.api
&gt; jasmine-node spec

.

Finished in 2.585 seconds
1 test, 8 assertions, 0 failures, 0 skipped
</code></pre>
