---
layout: "post"
title: "RESTful website architecture with Node and Angular"
date: "2015-07-24 00:58:23"
author: "Daniel Du"
categories:
  - "Client"
  - "Daniel Du"
  - "HTML"
  - "Javascript"
  - "Server"
  - "Web Server"
original_url: "https://adndevblog.typepad.com/cloud_and_mobile/2015/07/restful-website-architecture-with-node-and-angular.html "
typepad_basename: "restful-website-architecture-with-node-and-angular"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/cloud_and_mobile/daniel-du.html">Daniel Du</a></p>  <p>As a web programmer starting from the traditional ASP/ASP.net, sometimes my mind was constricted to the old days. My understanding of web application was how asp.net works, the HTML is generated at server side, and send it to browser to render. One guy asked me how to display some data which retrieved from server during a hackathon, while I am just in the middle of something, my distinctive response is to ask him to do something like &lt;%= something%&gt;, like ASP/ASP.NET does. But nowadays, it has been changed a lot, we have very different architecture – RESTful, which separates the server side and client side completely, and it also makes it easy for the server to serve different clients, including browsers and mobile devices. </p>  <p>Here is an architecture graph I “steal” from <a href="http://www.strongloop.com/">strongloop</a>. As you can see, with RESTful architecture, the web server exposes some REST APIs, the client communicates with the server with REST call. This make it very flexible, the client can be a browser as a common website, it also can be mobile device like iPhone/iPad or Android phone or pad. For the browser, you can user different libraries or frameworks to build your UI, you can use JQuery, Angular.Js or anything else you like. </p>  <p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb085766ee970d-pi"><img title="clip_image002" style="border-left-width: 0px; border-right-width: 0px; background-image: none; border-bottom-width: 0px; padding-top: 0px; padding-left: 0px; display: inline; padding-right: 0px; border-top-width: 0px" border="0" alt="clip_image002" src="/assets/image_70291a.jpg" width="451" height="218" /></a></p>  <p>Let’s make a simplest sample to demo the idea, a web server with Node.Js + Express, and a browser client with Angular.Js. The idea is to make a todo list. Firstly I will create a simplest web server with Node.Js and Express. The express CLI make it really easy. If you do not have express installed, install it first with help of google. And then create a simplest express web application with express generator:</p>  <blockquote>   <p>mkdir simpleWebApp</p>    <p>cd simpleWebApp</p>    <p>npm install</p> </blockquote>  <p>This will create a simplest web application with node.js and express, if you run “npm start” and go to “http://localhost:3000” you will see the first welcome page of express. </p>  <p>Now let’s do some simple work to add a REST API:</p>  <blockquote>   <p>GET /todos&#160;&#160; - Get all todo items</p> </blockquote>  <p>Before that, we will add some test data in JSON, in practice, you will get these todo items from a database like MongoDb. For simplicity, we make it a JSON file named as “todoItems.json”</p>  <pre class="csharpcode">[ 
      {    
          <span class="str">&quot;task&quot;</span>: <span class="str">&quot;buy milk&quot;</span>, 
        <span class="str">&quot;complete&quot;</span>:<span class="kwrd">false</span> 
    }, 
    { 
        <span class="str">&quot;task&quot;</span>: <span class="str">&quot;toast bread&quot;</span>, 
        <span class="str">&quot;complete&quot;</span>:<span class="kwrd">false</span> 
    }, 
    { 
        <span class="str">&quot;task&quot;</span>: <span class="str">&quot;pick up newspaper&quot;</span>, 
        <span class="str">&quot;complete&quot;</span>:<span class="kwrd">false</span> 
    }, 
    { 
        <span class="str">&quot;task&quot;</span>: <span class="str">&quot;water flowers&quot;</span>, 
        <span class="str">&quot;complete&quot;</span>:<span class="kwrd">false</span> 
    }

]</pre>
<style type="text/css">
.csharpcode, .csharpcode pre
{
	font-size: small;
	color: black;
	font-family: consolas, "Courier New", courier, monospace;
	background-color: #ffffff;
	/*white-space: pre;*/
}
.csharpcode pre { margin: 0em; }
.csharpcode .rem { color: #008000; }
.csharpcode .kwrd { color: #0000ff; }
.csharpcode .str { color: #006080; }
.csharpcode .op { color: #0000c0; }
.csharpcode .preproc { color: #cc6633; }
.csharpcode .asp { background-color: #ffff00; }
.csharpcode .html { color: #800000; }
.csharpcode .attr { color: #ff0000; }
.csharpcode .alt 
{
	background-color: #f4f4f4;
	width: 100%;
	margin: 0em;
}
.csharpcode .lnum { color: #606060; }</style>

<p>Then go to /routes/index.js, add a new route, response all todo items in json:</p>

<pre class="csharpcode"><span class="kwrd">var</span> express = require(<span class="str">'express'</span>);
<span class="kwrd">var</span> router = express.Router();

<span class="rem">/* GET home page. */</span>
router.get(<span class="str">'/'</span>, <span class="kwrd">function</span>(req, res) {
  res.render(<span class="str">'index'</span>, { title: <span class="str">'Express'</span> });
});


<strong><span class="kwrd">var</span> sampleToDoItems = require(<span class="str">'../todoTasks.json'</span>);

<span class="rem">/* GET Todo listing. */</span>
<span class="rem">//GET /Todos/</span>
router.get(<span class="str">'/todos'</span>, <span class="kwrd">function</span>(req, res) {

  res.type(<span class="str">'application/json'</span>);
  res.status(200);

  res.json(sampleToDoItems);
});</strong>

module.exports = router;</pre>
<style type="text/css">
.csharpcode, .csharpcode pre
{
	font-size: small;
	color: black;
	font-family: consolas, "Courier New", courier, monospace;
	background-color: #ffffff;
	/*white-space: pre;*/
}
.csharpcode pre { margin: 0em; }
.csharpcode .rem { color: #008000; }
.csharpcode .kwrd { color: #0000ff; }
.csharpcode .str { color: #006080; }
.csharpcode .op { color: #0000c0; }
.csharpcode .preproc { color: #cc6633; }
.csharpcode .asp { background-color: #ffff00; }
.csharpcode .html { color: #800000; }
.csharpcode .attr { color: #ff0000; }
.csharpcode .alt 
{
	background-color: #f4f4f4;
	width: 100%;
	margin: 0em;
}
.csharpcode .lnum { color: #606060; }</style>

<p>Now, the REST API is ready, if I start the web application, and go to postman with GET <a href="http://localhost:3000/todos">http://localhost:3000/todos</a>, or go to the url in browser directly, I will get all todo items:</p>

<p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c7b35667970b-pi"><img title="Screen Shot 2015-07-24 at 3.10.52 PM" style="border-top: 0px; border-right: 0px; background-image: none; border-bottom: 0px; padding-top: 0px; padding-left: 0px; border-left: 0px; display: inline; padding-right: 0px" border="0" alt="Screen Shot 2015-07-24 at 3.10.52 PM" src="/assets/image_ad5ab0.jpg" width="447" height="262" /></a></p>

<p>&#160;</p>

<p>The server side is done, next we will do the client side. We will use Angular.JS. Actually the client does not have to be in the same project of web server, but for simplicity, I put it together. The express has already setup the structure, the /public folder contains the static contents. I create a webpage named as “index.html” in it, and then I edit “route/index.js” file to change the route so that when I go to “http://localhost:3000/” it goes to my index.html page: </p>

<pre class="csharpcode"><span class="rem">/* GET home page. */</span>
router.get(<span class="str">'/'</span>, <span class="kwrd">function</span>(req, res) {
  <span class="rem">//res.render('index', { title: 'Express' });</span>
  <strong>res.redirect(<span class="str">'/index.html'</span>);</strong>
});</pre>
<style type="text/css">
.csharpcode, .csharpcode pre
{
	font-size: small;
	color: black;
	font-family: consolas, "Courier New", courier, monospace;
	background-color: #ffffff;
	/*white-space: pre;*/
}
.csharpcode pre { margin: 0em; }
.csharpcode .rem { color: #008000; }
.csharpcode .kwrd { color: #0000ff; }
.csharpcode .str { color: #006080; }
.csharpcode .op { color: #0000c0; }
.csharpcode .preproc { color: #cc6633; }
.csharpcode .asp { background-color: #ffff00; }
.csharpcode .html { color: #800000; }
.csharpcode .attr { color: #ff0000; }
.csharpcode .alt 
{
	background-color: #f4f4f4;
	width: 100%;
	margin: 0em;
}
.csharpcode .lnum { color: #606060; }</style>

<p>&#160;</p>

<p>Let’s do the angular part, in “index.html” to include the angular js library, and create our angular module and controller. It is very simple one, so I put them into a single JS file. The idea is just to send a GET request to the web server and get all todo items, the import part is to inject the $http into the controller:</p>

<pre class="csharpcode"><span class="kwrd">var</span> app = angular.module(<span class="str">'myTodoApp'</span>,[]);

app.controller(<span class="str">'myTodoCtrl'</span>, <span class="kwrd">function</span>($scope,<strong> $http</strong>){

    $http.get(<span class="str">'/todos'</span>)
        .success(<span class="kwrd">function</span>(response){
     
            <strong>$scope.todoItems = response</strong>;
        });
 
});</pre>
<style type="text/css">
.csharpcode, .csharpcode pre
{
	font-size: small;
	color: black;
	font-family: consolas, "Courier New", courier, monospace;
	background-color: #ffffff;
	/*white-space: pre;*/
}
.csharpcode pre { margin: 0em; }
.csharpcode .rem { color: #008000; }
.csharpcode .kwrd { color: #0000ff; }
.csharpcode .str { color: #006080; }
.csharpcode .op { color: #0000c0; }
.csharpcode .preproc { color: #cc6633; }
.csharpcode .asp { background-color: #ffff00; }
.csharpcode .html { color: #800000; }
.csharpcode .attr { color: #ff0000; }
.csharpcode .alt 
{
	background-color: #f4f4f4;
	width: 100%;
	margin: 0em;
}
.csharpcode .lnum { color: #606060; }</style>

<p>And then go to the “index.html” page, I put the important port in bold so that it is easy to follow. Firstly we need an “ng-app”, otherwise angular.Js will not run. And then put the “ng-controller” to the table, and build the table with<strong> ng-repeat=</strong><span class="str"><strong>&quot;todo in todoItems&quot;</strong>, p</span>lease note that the “todoItems” is populated in controller by http get request, they are completed in client side already:</p>

<p><strong>&#160;&#160;&#160;&#160; $scope.todoItems = response</strong>; </p>

<p> The the databind can be done by “ng-bind” or “{{&#160; something&#160; }}”.&#160; Here is the complete code: </p>

<pre class="csharpcode"><span class="kwrd">&lt;!</span><span class="html">DOCTYPE</span> <span class="attr">html</span><span class="kwrd">&gt;</span>
<span class="kwrd">&lt;</span><span class="html">html</span> <span class="attr">lang</span><span class="kwrd">=&quot;en&quot;</span><span class="kwrd">&gt;</span>
    <span class="kwrd">&lt;</span><span class="html">head</span><span class="kwrd">&gt;</span>
        <span class="kwrd">&lt;</span><span class="html">meta</span> <span class="attr">charset</span><span class="kwrd">=&quot;utf-8&quot;</span><span class="kwrd">&gt;</span>
        <span class="kwrd">&lt;</span><span class="html">meta</span> <span class="attr">http-equiv</span><span class="kwrd">=&quot;X-UA-Compatible&quot;</span> <span class="attr">content</span><span class="kwrd">=&quot;IE=edge&quot;</span><span class="kwrd">&gt;</span>
        <span class="kwrd">&lt;</span><span class="html">meta</span> <span class="attr">name</span><span class="kwrd">=&quot;viewport&quot;</span> <span class="attr">content</span><span class="kwrd">=&quot;width=device-width, initial-scale=1&quot;</span><span class="kwrd">&gt;</span>
        <span class="kwrd">&lt;</span><span class="html">title</span><span class="kwrd">&gt;</span>Title Page<span class="kwrd">&lt;/</span><span class="html">title</span><span class="kwrd">&gt;</span>

        <span class="rem">&lt;!-- Bootstrap CSS --&gt;</span>
        <span class="kwrd">&lt;</span><span class="html">link</span> <span class="attr">href</span><span class="kwrd">=&quot;//netdna.bootstrapcdn.com/bootstrap/3.2.0/css/bootstrap.min.css&quot;</span> <span class="attr">rel</span><span class="kwrd">=&quot;stylesheet&quot;</span><span class="kwrd">&gt;</span>

        <span class="rem">&lt;!-- HTML5 Shim and Respond.js IE8 support of HTML5 elements and media queries --&gt;</span>
        <span class="rem">&lt;!-- WARNING: Respond.js doesn't work if you view the page via file:// --&gt;</span>
        <span class="rem">&lt;!--[if lt IE 9]&gt;</span>
<span class="rem">            &lt;script src=&quot;https://oss.maxcdn.com/libs/html5shiv/3.7.0/html5shiv.js&quot;&gt;&lt;/script&gt;</span>
<span class="rem">            &lt;script src=&quot;https://oss.maxcdn.com/libs/respond.js/1.4.2/respond.min.js&quot;&gt;&lt;/script&gt;</span>
<span class="rem">        &lt;![endif]--&gt;</span>
    &lt;/head&gt;
    &lt;body&gt;
        &lt;h1 <span class="kwrd">class</span>=<span class="str">&quot;text-center&quot;</span>&gt;Hello World&lt;/h1&gt;

        &lt;div <span class="kwrd">class</span>=<span class="str">&quot;container&quot;</span> <strong>ng-app=</strong><span class="str"><strong>&quot;myTodoApp</strong>&quot;</span>&gt;
            &lt;table <span class="kwrd">class</span>=<span class="str">&quot;table table-striped table-hover&quot;</span> <strong>ng-controller=</strong><span class="str"><strong>&quot;myTodoCtrl</strong>&quot;</span>&gt;
                &lt;thead&gt;
                    &lt;tr&gt;
                        &lt;th&gt;&lt;/th&gt;
                    &lt;/tr&gt;
                &lt;/thead&gt;
                &lt;tbody&gt;
                    &lt;tr<strong> ng-repeat=<span class="str">&quot;todo in todoItems&quot;</span>&gt;</strong>
                        &lt;td<strong>&gt;{{todo.task}}&lt;/</strong>td&gt;
                        &lt;td&gt;&lt;div <span class="kwrd">class</span>=<span class="str">&quot;checkbox&quot;</span>&gt;
                            &lt;label&gt;
                                &lt;input type=<span class="str">&quot;checkbox&quot;</span> value=<span class="str">&quot;&quot;</span> <strong>ng-model=</strong><span class="str"><strong>&quot;todo.complete</strong>&quot;</span>&gt;
                            &lt;/label&gt;
                        &lt;/div&gt;&lt;/td&gt;
                    &lt;/tr&gt;
                &lt;/tbody&gt;
            &lt;/table&gt;

        &lt;/div&gt;

        &lt;!-- jQuery --&gt;
        &lt;script src=<span class="str">&quot;//code.jquery.com/jquery.js&quot;</span>&gt;&lt;/script&gt;
        &lt;!-- Bootstrap JavaScript --&gt;
        &lt;script src=<span class="str">&quot;//netdna.bootstrapcdn.com/bootstrap/3.2.0/js/bootstrap.min.js&quot;</span>&gt;&lt;/script&gt;

        <strong>&lt;script src=<span class="str">&quot;//www.runoob.com/try/angularjs/1.2.5/angular.min.js&quot;</span>&gt;&lt;/script&gt;</strong>

        <strong>&lt;script src=<span class="str">&quot;/javascripts/myTodoApp.js&quot;</span>&gt;&lt;/script&gt;</strong>
    <span class="kwrd">&lt;/</span><span class="html">body</span><span class="kwrd">&gt;</span>
<span class="kwrd">&lt;/</span><span class="html">html</span><span class="kwrd">&gt;</span></pre>

<p>And this is how it looks like when running the app:</p>

<p><style type="text/css">
.csharpcode, .csharpcode pre
{
	font-size: small;
	color: black;
	font-family: consolas, "Courier New", courier, monospace;
	background-color: #ffffff;
	/*white-space: pre;*/
}
.csharpcode pre { margin: 0em; }
.csharpcode .rem { color: #008000; }
.csharpcode .kwrd { color: #0000ff; }
.csharpcode .str { color: #006080; }
.csharpcode .op { color: #0000c0; }
.csharpcode .preproc { color: #cc6633; }
.csharpcode .asp { background-color: #ffff00; }
.csharpcode .html { color: #800000; }
.csharpcode .attr { color: #ff0000; }
.csharpcode .alt 
{
	background-color: #f4f4f4;
	width: 100%;
	margin: 0em;
}
.csharpcode .lnum { color: #606060; }</style><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c7b3566b970b-pi"><img title="Screen Shot 2015-07-24 at 3.41.05 PM" style="border-top: 0px; border-right: 0px; background-image: none; border-bottom: 0px; padding-top: 0px; padding-left: 0px; border-left: 0px; display: inline; padding-right: 0px" border="0" alt="Screen Shot 2015-07-24 at 3.41.05 PM" src="/assets/image_ae64df.jpg" width="543" height="222" /></a></p>

<p>&#160;</p>

<p>OK, that’s the idea, very simple, of cause in practical, if will be more complex, and you can user various frameworks, like <a href="http://mean.io">mean.io</a>, <a href="http://cleverstack.io/">cleverstack</a>, <a href="http://sailsjs.org/">sail.js</a>. </p>
