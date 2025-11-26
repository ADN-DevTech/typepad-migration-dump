---
layout: "post"
title: "Using View and Data API with Meteor"
date: "2016-01-27 22:18:42"
author: "Daniel Du"
categories:
  - "Cloud"
  - "Daniel Du"
  - "View and Data API"
original_url: "https://adndevblog.typepad.com/cloud_and_mobile/2016/01/using-view-and-data-api-with-meteor.html "
typepad_basename: "using-view-and-data-api-with-meteor"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/cloud_and_mobile/daniel-du.html">Daniel Du</a></p>  <p>I have been studying <a href="https://www.meteor.com/" target="_blank">Meteor</a> these days, and find that Meteor is really a mind-blowing framework, I can talk about this latter. I was inspired by <a href="http://forums.autodesk.com/t5/view-and-data-api/using-view-and-data-api-with-meteor-js/td-p/5991758" target="_blank">this question</a> on forum and started to looking at the possibilities of using View and Data API in Meteor. Since the way of Meteor works is so different, I have to say that it is not pleasant experience to do that especially for a meteor starter like me. Anyway, after struggling for days, trying this and that, I finally made a simple working site and deployed it as <a title="http://lmv.meteor.com" href="http://lmv.meteor.com">http://lmv.meteor.com</a>. In this post I will write down how I did this, hopefully it is helpful in case you are doing similar stuff. </p>  <p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb08b289b6970d-pi"><img title="Screen Shot 2016-01-28 at 1.48.12 PM" style="border-left-width: 0px; border-right-width: 0px; background-image: none; border-bottom-width: 0px; padding-top: 0px; padding-left: 0px; display: inline; padding-right: 0px; border-top-width: 0px" border="0" alt="Screen Shot 2016-01-28 at 1.48.12 PM" src="/assets/image_582ae8.jpg" width="453" height="457" /></a></p>  <p>Firstly I created a Meteor project with “meteor create myproject” command, which creates a “hello world” project. To make it look nice, I refactored the folder structure according to the <a href="http://docs.meteor.com/#/basic/filestructure" target="_blank">document of meteor about file structure</a> as below:     <br />.     <br />├── README.md     <br />├── client     <br />│&#160;&#160; ├── index.html     <br />│&#160;&#160; ├── index.js     <br />│&#160;&#160; ├── style.css     <br />│&#160;&#160; └── viewer     <br />│&#160;&#160;&#160;&#160;&#160;&#160; ├── viewer.html     <br />│&#160;&#160;&#160;&#160;&#160;&#160; └── viewer.js     <br />├── lib     <br />└── server     <br />&#160;&#160;&#160; └── index.js</p>  <p>The “client” folder contains the contents which are running at client side, “server” folder contains the scripts which are running at server side.</p>  <p>To use View and Data API, we need to do the authentication process to get access token with consumer key/ secret key, which can be applied from <a href="http://developer.autodesk.com">http://developer.autodesk.com</a> .&#160; The authentication should be done at server side, otherwise your secret key will be peeked by hackers, so I will do the authentication in “\server\index.js”. But first let me add the “http” package to send REST request to Autodesk authentication server from meteor. You can do this by running command “meteor add http” from command line, and you can also edit “./meteor/packages” file directly, so here is my packages file: </p>  <p>===========================</p>  <p># Meteor packages used by this project, one per line.    <br /># Check this file (and the other files in this directory) into your repository.     <br />#     <br /># 'meteor add' and 'meteor remove' will edit this file for you,     <br /># but you can also edit it by hand.</p>  <p>meteor-base&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; # Packages every Meteor app needs to have    <br />mobile-experience&#160;&#160;&#160;&#160;&#160;&#160; # Packages for a great mobile UX     <br />mongo&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; # The database Meteor supports right now     <br />blaze-html-templates&#160;&#160;&#160; # Compile .html files into Meteor Blaze views     <br />session&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; # Client-side reactive dictionary for your app     <br />jquery&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; # Helpful client-side library     <br />tracker&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; # Meteor's client-side reactive programming library</p>  <p>standard-minifiers&#160;&#160;&#160;&#160;&#160; # JS/CSS minifiers run for production mode    <br />es5-shim&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; # ECMAScript 5 compatibility for older browsers.     <br />ecmascript&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; # Enable ECMAScript2015+ syntax in app code</p>  <p>autopublish&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; # Publish all data to the clients (for prototyping)    <br />insecure&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; # Allow all DB writes from clients (for prototyping)</p>  <p><strong># Allow to send REST calls to authentication server      <br />http       <br /></strong></p> <style type="text/css">

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
.csharpcode .lnum { color: #606060; }</style>  <p>=============================</p>  <p>With that, I can add a Meteor method to do authentication from “/server/index.js”,. It can be called from client side with “Meteor.call()”. Here is the code snippet, please note that I am using synchronous mode when doing “Meteor.http.post”, as I found that I cannot get the returned access token from client side afterwards if I use async mode.</p>  <pre class="csharpcode">Meteor.startup(<span class="kwrd">function</span> () {
    <span class="rem">// code to run on server at startup</span>
});


Meteor.methods({

    getAccessToken: <span class="kwrd">function</span> () {

        <span class="kwrd">this</span>.unblock();

        <span class="kwrd">var</span> credentials = {

            credentials: {
                <span class="rem">// Replace placeholder below by the Consumer Key and Consumer Secret you got from</span>
                <span class="rem">// http://developer.autodesk.com/ for the production server</span>
                client_id: process.env.CONSUMERKEY || <span class="str">'replace with your consumer key'</span>,
                client_secret: process.env.CONSUMERSECRET || <span class="str">'your secrete key'</span>,
                grant_type: <span class="str">'client_credentials'</span>
            },

            <span class="rem">// If you which to use the Autodesk View &amp; Data API on the staging server, change this url</span>
            BaseUrl: <span class="str">'https://developer.api.autodesk.com'</span>,
            Version: <span class="str">'v1'</span>
        };

        credentials.AuthenticationURL = credentials.BaseUrl + <span class="str">'/authentication/'</span> + credentials.Version + <span class="str">'/authenticate'</span>

        <span class="rem">//must use synchronous mode</span>
        <span class="kwrd">var</span> result = Meteor.http.post(
            credentials.AuthenticationURL,
            {<span class="kwrd">params</span>: credentials.credentials}
        );
        <span class="rem">//get the access token object</span>
        <span class="kwrd">return</span> result.data;


    }
})</pre>
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

<p>Now let’s back to the client side, in “/client/viewer/viewer.html” I created a simple template as below: </p>

<pre class="csharpcode">&lt;Template name=<span class="str">&quot;viewer&quot;</span>&gt;

    &lt;h2&gt;Autodesk View and Data API&lt;/h2&gt;
    &lt;div id=<span class="str">&quot;viewer&quot;</span> <span class="kwrd">class</span>=<span class="str">&quot;viewer&quot;</span>&gt;

    &lt;/div&gt;
&lt;/Template&gt;</pre>

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
.csharpcode .lnum { color: #606060; }</style>In the “\viewer\viewer.js”, I will try to get the access token first with following code:&#160; <br /></p>

<pre class="csharpcode">Template.viewer.onCreated(<span class="kwrd">function</span>(){

    <span class="rem">//console.log('viewer template created.')</span>
    Meteor.call(<span class="str">'getAccessToken'</span>, <span class="kwrd">function</span> (error, result) {
        <span class="kwrd">if</span> (error) {
            console.log(error);
        }
        <span class="kwrd">else</span> {
            <span class="kwrd">var</span> token = result.access_token;
            console.log(token);

            <span class="rem">//initialize the viewer</span>
            initViewer(token);

        }

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



<p>When the viewer template is created, I call to the server side meteor method to do authentication and get the access token, once I get the access token, I can initialize a viewer at client side with View and Data JavaScript API. Now I can see the token from console of developer tool, so far so good. </p>

<p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb08b289be970d-pi"><img title="Screen Shot 2016-01-28 at 1.45.51 PM" style="border-left-width: 0px; border-right-width: 0px; background-image: none; border-bottom-width: 0px; padding-top: 0px; padding-left: 0px; display: inline; padding-right: 0px; border-top-width: 0px" border="0" alt="Screen Shot 2016-01-28 at 1.45.51 PM" src="/assets/image_9a8944.jpg" width="464" height="96" /></a></p>

<p>&#160;</p>

<p>To use View and Data API, we need to add reference to viewer JavaScript libraries. It seems a very basic thing but it turns out to be the difficult part when it comes to Meteor. <a href="http://www.kaplankomputing.com/blog/tutorials/two-ways-to-add-a-script-tag-in-meteor/" target="_blank">This blog</a> introduced two ways to add a script tag into meteor. I tried this solution by creating a script template and load the “viewer3d.js” and viewer style file on the fly, but when I am trying to create a viewer with View and Data JavaScript API, I run to the problem as described in the forum post:</p>

<p>&quot;Uncaught ReferenceError: AutodeskNamespace is not defined&quot;</p>

<p>If I examined to the network tab of browser development tool, the “viewer3d.min.js” has not been loaded yet when I was trying to use it. </p>

<p>Meteor controls the load process of JS files and it is not easy to control the load order, here is the load order as described on meteor document: </p>

<blockquote>
  <p>The JavaScript and CSS files in an application are loaded according to these rules:</p>

  <p>Files in the lib directory at the root of your application are loaded first.</p>

  <p>Files that match main.* are loaded after everything else.</p>

  <p>Files in subdirectories are loaded before files in parent directories, so that files in the deepest subdirectory are loaded first (after lib), and files in the root directory are loaded last (other than main.*).</p>

  <p>Within a directory, files are loaded in alphabetical order by filename.</p>

  <p>These rules stack, so that within lib, for example, files are still loaded in alphabetical order; and if there are multiple files named main.js, the ones in subdirectories are loaded earlier.</p>
</blockquote>

<p>So since viewer js lib is loaded very late, I cannot use it in viewer.js to initialize the viewer. Luckily, I found that if I put the &lt;script src=””/&gt; tag into &lt;head&gt;, it will be loaded first, so in “/client/index.html”:</p>

<pre class="csharpcode"><span class="kwrd">&lt;</span><span class="html">head</span><span class="kwrd">&gt;</span>
  <span class="kwrd">&lt;</span><span class="html">title</span><span class="kwrd">&gt;</span>hello<span class="kwrd">&lt;/</span><span class="html">title</span><span class="kwrd">&gt;</span>
 <strong>   <span class="kwrd">&lt;</span><span class="html">link</span> <span class="attr">rel</span><span class="kwrd">=&quot;stylesheet&quot;</span> <span class="attr">type</span><span class="kwrd">=&quot;text/css&quot;</span> <span class="attr">href</span><span class="kwrd">=&quot;https://developer.api.autodesk.com/viewingservice/v1/viewers/style.css&quot;</span><span class="kwrd">/&gt;</span>
    <span class="kwrd">&lt;</span><span class="html">script</span> <span class="attr">src</span><span class="kwrd">=&quot;https://developer.api.autodesk.com/viewingservice/v1/viewers/viewer3D.min.js&quot;</span><span class="kwrd">&gt;&lt;/</span><span class="html">script</span><span class="kwrd">&gt;</span></strong>
<span class="kwrd">&lt;/</span><span class="html">head</span><span class="kwrd">&gt;</span>

<span class="kwrd">&lt;</span><span class="html">body</span><span class="kwrd">&gt;</span>
  <span class="kwrd">&lt;</span><span class="html">h1</span><span class="kwrd">&gt;</span>Welcome to Meteor!<span class="kwrd">&lt;/</span><span class="html">h1</span><span class="kwrd">&gt;</span>

  {{<span class="kwrd">&gt;</span> hello}}

  {{<span class="kwrd">&gt;</span> viewer }}
<span class="kwrd">&lt;/</span><span class="html">body</span><span class="kwrd">&gt;</span>

<span class="kwrd">&lt;</span><span class="html">template</span> <span class="attr">name</span><span class="kwrd">=&quot;hello&quot;</span><span class="kwrd">&gt;</span>
  <span class="kwrd">&lt;</span><span class="html">button</span><span class="kwrd">&gt;</span>Click Me<span class="kwrd">&lt;/</span><span class="html">button</span><span class="kwrd">&gt;</span>
  <span class="kwrd">&lt;</span><span class="html">p</span><span class="kwrd">&gt;</span>You've pressed the button {{counter}} times.<span class="kwrd">&lt;/</span><span class="html">p</span><span class="kwrd">&gt;</span>
<span class="kwrd">&lt;/</span><span class="html">template</span><span class="kwrd">&gt;</span></pre>
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

<p>OK, with that I can initialized viewer in “/client/viewer/viewer.js” file, the code snippet is below: </p>

<pre class="csharpcode">Template.viewer.onCreated(<span class="kwrd">function</span>(){

    <span class="rem">//console.log('viewer template created.')</span>
    Meteor.call(<span class="str">'getAccessToken'</span>, <span class="kwrd">function</span> (error, result) {
        <span class="kwrd">if</span> (error) {
            console.log(error);
        }
        <span class="kwrd">else</span> {
            <span class="kwrd">var</span> token = result.access_token;
            console.log(token);

            <span class="rem">//initialize the viewer</span>
            initViewer(token);

        }

    });

});



<span class="kwrd">var</span> initViewer = <span class="kwrd">function</span> (token) {

    <span class="kwrd">var</span> defaultUrn = <span class="str">'dXJuOmFkc2sub2JqZWN0czpvcy5vYmplY3Q6bW9kZWwyMDE2LTAxLTI4LTAyLTQ0LTM2LWlkbWpjajl5ZXlnYzhwN3h5bDBwZXB5dm54OWkvZ2F0ZWhvdXNlLm53ZA=='</span>;

    <span class="kwrd">if</span> (defaultUrn.indexOf(<span class="str">'urn:'</span>) !== 0)
        defaultUrn = <span class="str">'urn:'</span> + defaultUrn;

    <span class="kwrd">function</span> initializeViewer(containerId, documentId, role) {
        <span class="kwrd">var</span> viewerContainer = document.getElementById(containerId);
        <span class="kwrd">var</span> viewer = <span class="kwrd">new</span> Autodesk.Viewing.Private.GuiViewer3D(
            viewerContainer);
        viewer.start();

        Autodesk.Viewing.Document.load(documentId,
            <span class="kwrd">function</span> (document) {
                <span class="kwrd">var</span> rootItem = document.getRootItem();
                <span class="kwrd">var</span> geometryItems = Autodesk.Viewing.Document.getSubItemsWithProperties(
                    rootItem,
                    { <span class="str">'type'</span>: <span class="str">'geometry'</span>, <span class="str">'role'</span>: role },
                    <span class="kwrd">true</span>);

                viewer.load(document.getViewablePath(geometryItems[0]));
            },

            <span class="rem">// onErrorCallback</span>
            <span class="kwrd">function</span> (msg) {
                console.log(<span class="str">&quot;Error loading document: &quot;</span> + msg);
            }
        );
    }

    <span class="kwrd">function</span> initialize() {
        <span class="kwrd">var</span> options = {
            env: <span class="str">&quot;AutodeskProduction&quot;</span>,
            <span class="rem">//getAccessToken: getToken,</span>
            <span class="rem">//refreshToken: getToken</span>
            accessToken : token
        };

        Autodesk.Viewing.Initializer(options, <span class="kwrd">function</span> () {
            initializeViewer(<span class="str">'viewer'</span>, defaultUrn, <span class="str">'3d'</span>);
        });
    }

    <span class="rem">//call</span>
    initialize();


}</pre>

<p>Now I have a running meteor application with viewer embedded. I also posted my sample on github, so you may want to take a look to check the complete code. Hope it helps some. </p>

<p><a title="https://github.com/Developer-Autodesk/meteor-view.and.data.api" href="https://github.com/Developer-Autodesk/meteor-view.and.data.api">https://github.com/Developer-Autodesk/meteor-view.and.data.api</a></p>
