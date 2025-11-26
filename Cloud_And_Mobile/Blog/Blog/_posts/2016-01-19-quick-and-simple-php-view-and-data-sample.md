---
layout: "post"
title: "Quick and simple PHP View and Data sample"
date: "2016-01-19 05:57:18"
author: "Augusto Goncalves"
categories:
  - "Augusto Goncalves"
  - "PHP"
  - "View and Data API"
original_url: "https://adndevblog.typepad.com/cloud_and_mobile/2016/01/quick-and-simple-php-view-and-data-sample.html "
typepad_basename: "quick-and-simple-php-view-and-data-sample"
typepad_status: "Publish"
---

<p>By Augusto Goncalves (@<a href="https://twitter.com/augustomaia">augustomaia</a>)</p>
<p>Last week we hosted a <a href="https://twitter.com/augustomaia/status/686541245196275713">DevDay</a> followed by Autodesk Cloud Accelerator in São Paulo, it was an interesting experience: some developers were working with ASP.NET (from their .NET experience), others with Java (server back-end with JBoss) and some with PHP. Quite a diverse environment!</p>
<p>I must say that Java is an old friend that I haven&#39;t seeing in a while (since colleague, actually) and PHP I just heard about (never used). So this week, back to regular work, I decided to give PHP a try: they say it&#39;s a web language :-)</p>
<p>Let&#39;s get started with PHP: first we need a server. After a quick search I found this cool article on <a href="http://jason.pureconcepts.net/2014/11/install-apache-php-mysql-mac-os-x-yosemite/">how enable PHP on Apache for Mac OS X Yosemite</a>. Couldn&#39;t be easier: just uncomment a line to enable PHP5 and restart Apache.</p>
<p>Now what about REST calls? Although <a href="http://php.net/manual/book.curl.php">cURL</a> is available on PHP, I prefer use a library that wraps the calls in a easy manner. The top search result pointed me to <a href="http://phphttpclient.com/docs/class-Httpful.Response.html">HttpFul PHP Library</a>. Again, couldn&#39;t be easier: here is a quick call to Autodesk Authenticate:</p>
<p><em>$response = \Httpful\Request</em><br /><em> ::post(&#39;https://developer.api.autodesk.com/authentication/v1/authenticate&#39;)</em><br /><em> -&gt;addHeader(&#39;Content-Type&#39;, &#39;application/x-www-form-urlencoded&#39;)</em><br /><em> -&gt;body(&#39;client_id=<span style="color: #8b8b8b;">Xyz</span>&amp;client_secret=<span style="color: #737373;">Xyz</span>&amp;grant_type=client_credentials&#39;)</em><br /><em> -&gt;send();</em></p>
<p>After a few hours learning and writing PHP code, here is the main result: authenticate method that calls the respective Autodesk API. At the code below, check the comments for more details on each line.</p>
<p>Note you need to edit and include your developer key &amp; secret. Also, as the PHP and HTML code are on the same file, the JavaScript call assume the file name as <strong>view.and.data.php</strong>. The PHP will check the URL and redirect the calls, in this sample, a call to <strong>view.and.data.php/authenticate</strong>&#0160;will run the <strong>authenticate()</strong> method. I&#39;m planing to expand this in the future to include other methods.</p>

<div>
<pre style="margin: 0; line-height: 125%;"><span style="color: #bc7a00;">&lt;?php</span>
<span style="color: #408080; font-style: italic;">// we need a REST Library, found HttpFul</span>
<span style="color: #408080; font-style: italic;">// Download from http://phphttpclient.com/</span>
<span style="color: #408080; font-style: italic;">// You may need to add permissions for MAC OS with Apache</span>
<span style="color: #408080; font-style: italic;">//     sudo chmod -R 755 /library/webserver/documents</span>
<span style="color: #408080; font-style: italic;">//     (assuming default folder)</span>
<span style="color: #008000; font-weight: bold;">include</span> <span style="color: #ba2121;">&#39;httpful.phar&#39;</span>;

<span style="color: #408080; font-style: italic;">// define some constants for this quick sample</span>
<span style="color: #008000;">define</span>(CONSUMER_KEY, <span style="color: #ba2121;">&quot;your key&quot;</span>);
<span style="color: #008000;">define</span>(CONSUMER_SECRET, <span style="color: #ba2121;">&quot;your secret&quot;</span>);
<span style="color: #008000;">define</span>(BASE_URL, <span style="color: #ba2121;">&#39;https://developer.api.autodesk.com&#39;</span>);

<span style="color: #408080; font-style: italic;">// if the request URL contains the method being requested</span>
<span style="color: #408080; font-style: italic;">// for instance, a call to view.and.data.php/authenticate</span>
<span style="color: #408080; font-style: italic;">// will redirect to the function with the same name</span>
<span style="color: #19177c;">$apiName</span> <span style="color: #666666;">=</span> <span style="color: #008000;">explode</span>(<span style="color: #ba2121;">&#39;/&#39;</span>, trim(<span style="color: #19177c;">$_SERVER</span>[<span style="color: #ba2121;">&#39;PATH_INFO&#39;</span>],<span style="color: #ba2121;">&#39;/&#39;</span>))[<span style="color: #666666;">0</span>];
<span style="color: #008000; font-weight: bold;">if</span> (<span style="color: #666666;">!</span><span style="color: #008000; font-weight: bold;">empty</span>(<span style="color: #19177c;">$apiName</span>)){
    <span style="color: #408080; font-style: italic;">// get the function by API name</span>
    <span style="color: #008000; font-weight: bold;">try</span>{ <span style="color: #19177c;">$apiFunction</span> <span style="color: #666666;">=</span> <span style="color: #008000; font-weight: bold;">new</span> ReflectionFunction(<span style="color: #19177c;">$apiName</span>);}
    <span style="color: #008000; font-weight: bold;">catch</span> (Exception <span style="color: #19177c;">$e</span>) { <span style="color: #008000; font-weight: bold;">echo</span> (<span style="color: #ba2121;">&#39;API not found&#39;</span>);}
    
    <span style="color: #408080; font-style: italic;">// run the function and &#39;echo&#39; it&#39;s reponse</span>
    <span style="color: #008000; font-weight: bold;">if</span> (<span style="color: #19177c;">$apiFunction</span> <span style="color: #666666;">!=</span> <span style="color: #008000; font-weight: bold;">null</span>) <span style="color: #008000; font-weight: bold;">echo</span> <span style="color: #19177c;">$apiFunction</span><span style="color: #666666;">-&gt;</span><span style="color: #7d9029;">invoke</span>();
    
    <span style="color: #008000; font-weight: bold;">exit</span>(); <span style="color: #408080; font-style: italic;">// no HTML output</span>
}

<span style="color: #408080; font-style: italic;">// now the APIs</span>
<span style="color: #008000; font-weight: bold;">function</span> <span style="color: #0000ff;">authenticate</span>(){
    <span style="color: #408080; font-style: italic;">// request body (client key &amp; secret)</span>
    <span style="color: #19177c;">$body</span> <span style="color: #666666;">=</span> <span style="color: #008000;">sprintf</span>(<span style="color: #ba2121;">&#39;client_id=%s&#39;</span> <span style="color: #666666;">.</span> 
                    <span style="color: #ba2121;">&#39;&amp;client_secret=%s&#39;</span> <span style="color: #666666;">.</span> 
                    <span style="color: #ba2121;">&#39;&amp;grant_type=client_credentials&#39;</span>,
                    CONSUMER_KEY, CONSUMER_SECRET);

    <span style="color: #408080; font-style: italic;">// prepare a POST request following the documentation</span>
    <span style="color: #19177c;">$response</span> <span style="color: #666666;">=</span> 
        \Httpful\Request<span style="color: #666666;">::</span><span style="color: #7d9029;">post</span>(
          BASE_URL <span style="color: #666666;">.</span> <span style="color: #ba2121;">&#39;/authentication/v1/authenticate&#39;</span>)
        <span style="color: #666666;">-&gt;</span><span style="color: #7d9029;">addHeader</span>(<span style="color: #ba2121;">&#39;Content-Type&#39;</span>, <span style="color: #ba2121;">&#39;application/x-www-form-urlencoded&#39;</span>)
        <span style="color: #666666;">-&gt;</span><span style="color: #7d9029;">body</span>(<span style="color: #19177c;">$body</span>)
        <span style="color: #666666;">-&gt;</span><span style="color: #7d9029;">send</span>(); <span style="color: #408080; font-style: italic;">// make the request</span>

    <span style="color: #008000; font-weight: bold;">if</span> ( <span style="color: #19177c;">$response</span><span style="color: #666666;">-&gt;</span><span style="color: #7d9029;">code</span> <span style="color: #666666;">==</span> <span style="color: #666666;">200</span>)
        <span style="color: #408080; font-style: italic;">// access the JSON response directly</span>
        <span style="color: #008000; font-weight: bold;">return</span> <span style="color: #19177c;">$response</span><span style="color: #666666;">-&gt;</span><span style="color: #7d9029;">body</span><span style="color: #666666;">-&gt;</span><span style="color: #7d9029;">access_token</span>; 
    <span style="color: #008000; font-weight: bold;">else</span>
        <span style="color: #408080; font-style: italic;">// something went wrong...</span>
        <span style="color: #008000; font-weight: bold;">throw</span> <span style="color: #008000; font-weight: bold;">new</span> Exception(<span style="color: #ba2121;">&#39;Cannot authenticate&#39;</span>);
}
<span style="color: #bc7a00;">?&gt;</span>
<span style="color: #bc7a00;">&lt;!DOCTYPE html&gt;</span>
<span style="color: #008000; font-weight: bold;">&lt;html</span> <span style="color: #7d9029;">xmlns=</span><span style="color: #ba2121;">&quot;http://www.w3.org/1999/xhtml&quot;</span><span style="color: #008000; font-weight: bold;">&gt;</span>
<span style="color: #008000; font-weight: bold;">&lt;head&gt;</span>
    <span style="color: #008000; font-weight: bold;">&lt;title&gt;</span>Minimum PHP View and Data Sample<span style="color: #008000; font-weight: bold;">&lt;/title&gt;</span>
    <span style="color: #008000; font-weight: bold;">&lt;link</span> <span style="color: #7d9029;">type=</span><span style="color: #ba2121;">&quot;text/css&quot;</span> <span style="color: #7d9029;">rel=</span><span style="color: #ba2121;">&quot;stylesheet&quot;</span> <span style="color: #7d9029;">href=</span><span style="color: #ba2121;">&quot;https://developer.api.autodesk.com/viewingservice/v1/viewers/style.css&quot;</span> <span style="color: #008000; font-weight: bold;">/&gt;</span>
<span style="color: #008000; font-weight: bold;">&lt;/head&gt;</span>
<span style="color: #008000; font-weight: bold;">&lt;script </span><span style="color: #7d9029;">src=</span><span style="color: #ba2121;">&quot;https://developer.api.autodesk.com/viewingservice/v1/viewers/viewer3D.min.js?v=1.2.23&quot;</span><span style="color: #008000; font-weight: bold;">&gt;&lt;/script&gt;</span>
<span style="color: #008000; font-weight: bold;">&lt;script&gt;</span>
<span style="color: #408080; font-style: italic;">// This is the basic JavaScript sample code available at the documentation</span>
<span style="color: #408080; font-style: italic;">// It&#39;s optimized for 3D models and slightly adjusted for this case</span>

<span style="color: #408080; font-style: italic;">// Show the model specified on the URN parameter</span>
<span style="color: #008000; font-weight: bold;">function</span> showModel() {
    <span style="color: #008000; font-weight: bold;">var</span> options <span style="color: #666666;">=</span> {
        <span style="color: #ba2121;">&#39;document&#39;</span><span style="color: #666666;">:</span> <span style="color: #ba2121;">&#39;urn:&#39;</span> <span style="color: #666666;">+</span> <span style="color: #008000;">document</span>.getElementById(<span style="color: #ba2121;">&#39;modelURN&#39;</span>).value,
        <span style="color: #ba2121;">&#39;env&#39;</span><span style="color: #666666;">:</span> <span style="color: #ba2121;">&#39;AutodeskProduction&#39;</span>,
        <span style="color: #ba2121;">&#39;getAccessToken&#39;</span><span style="color: #666666;">:</span> getToken,
        <span style="color: #ba2121;">&#39;refreshToken&#39;</span><span style="color: #666666;">:</span> getToken,
    };
    <span style="color: #008000; font-weight: bold;">var</span> viewerElement <span style="color: #666666;">=</span> <span style="color: #008000;">document</span>.getElementById(<span style="color: #ba2121;">&#39;viewer&#39;</span>);
    <span style="color: #008000; font-weight: bold;">var</span> viewer <span style="color: #666666;">=</span> <span style="color: #008000; font-weight: bold;">new</span> Autodesk.Viewing.Viewer3D(viewerElement, {});
    Autodesk.Viewing.Initializer(
        options,
        <span style="color: #008000; font-weight: bold;">function</span> () {
            viewer.initialize();
            loadDocument(viewer, options.<span style="color: #008000;">document</span>);
        }
    );
}

<span style="color: #408080; font-style: italic;">// Load the document (urn) on the view object</span>
<span style="color: #008000; font-weight: bold;">function</span> loadDocument(viewer, documentId) {
    <span style="color: #408080; font-style: italic;">// Find the first 3d geometry and load that.</span>
    Autodesk.Viewing.Document.load(
        documentId,
        <span style="color: #008000; font-weight: bold;">function</span> (doc) { <span style="color: #408080; font-style: italic;">// onLoadCallback</span>
            <span style="color: #008000; font-weight: bold;">var</span> geometryItems <span style="color: #666666;">=</span> [];
            geometryItems <span style="color: #666666;">=</span> Autodesk.Viewing.Document.getSubItemsWithProperties(doc.getRootItem(), {
                <span style="color: #ba2121;">&#39;type&#39;</span><span style="color: #666666;">:</span> <span style="color: #ba2121;">&#39;geometry&#39;</span>,
                <span style="color: #ba2121;">&#39;role&#39;</span><span style="color: #666666;">:</span> <span style="color: #ba2121;">&#39;3d&#39;</span>
            }, <span style="color: #008000; font-weight: bold;">true</span>);
            <span style="color: #008000; font-weight: bold;">if</span> (geometryItems.length <span style="color: #666666;">&gt;</span> <span style="color: #666666;">0</span>) {
                viewer.load(doc.getViewablePath(geometryItems[<span style="color: #666666;">0</span>]));
            }
        },
        <span style="color: #008000; font-weight: bold;">function</span> (errorMsg) { <span style="color: #408080; font-style: italic;">// onErrorCallback</span>
            alert(<span style="color: #ba2121;">&quot;Load Error: &quot;</span> <span style="color: #666666;">+</span> errorMsg);
        }
    );
}

<span style="color: #408080; font-style: italic;">// This calls are required if the models stays open for a long time and the token expires</span>
<span style="color: #008000; font-weight: bold;">function</span> getToken() {
    <span style="color: #008000; font-weight: bold;">return</span> makePOSTSyncRequest(<span style="color: #ba2121;">&quot;view.and.data.php/authenticate&quot;</span>);
}

<span style="color: #008000; font-weight: bold;">function</span> makePOSTSyncRequest(url) {
    <span style="color: #008000; font-weight: bold;">var</span> xmlHttp <span style="color: #666666;">=</span> <span style="color: #008000; font-weight: bold;">null</span>;
    xmlHttp <span style="color: #666666;">=</span> <span style="color: #008000; font-weight: bold;">new</span> XMLHttpRequest();
    xmlHttp.open(<span style="color: #ba2121;">&quot;POST&quot;</span>, url, <span style="color: #008000; font-weight: bold;">false</span>);
    xmlHttp.send(<span style="color: #008000; font-weight: bold;">null</span>);
    <span style="color: #008000; font-weight: bold;">return</span> xmlHttp.responseText;
}
<span style="color: #008000; font-weight: bold;">&lt;/script&gt;</span>
<span style="color: #008000; font-weight: bold;">&lt;body&gt;</span>
    <span style="color: #008000; font-weight: bold;">&lt;div&gt;</span>This is a minimum sample in PHP5.
        <span style="color: #008000; font-weight: bold;">&lt;br</span> <span style="color: #008000; font-weight: bold;">/&gt;</span> First edit this file and enter your consumer key and consumer secret. Request at <span style="color: #008000; font-weight: bold;">&lt;a</span> <span style="color: #7d9029;">href=</span><span style="color: #ba2121;">&quot;http://forge.autodesk.com&quot;</span><span style="color: #008000; font-weight: bold;">&gt;</span>Forge portal<span style="color: #008000; font-weight: bold;">&lt;/a&gt;</span>.
        <span style="color: #008000; font-weight: bold;">&lt;br</span> <span style="color: #008000; font-weight: bold;">/&gt;</span> To use this sample you need a model URN. Please upload a model at <span style="color: #008000; font-weight: bold;">&lt;a</span> <span style="color: #7d9029;">href=</span><span style="color: #ba2121;">&quot;http://models.autodesk.io&quot;</span><span style="color: #008000; font-weight: bold;">&gt;</span>Models.Autodesk.IO<span style="color: #008000; font-weight: bold;">&lt;/a&gt;&lt;/div&gt;</span>
    <span style="color: #008000; font-weight: bold;">&lt;hr</span> <span style="color: #008000; font-weight: bold;">/&gt;</span>
    <span style="color: #008000; font-weight: bold;">&lt;div&gt;</span>
        Specify a model URN:
        <span style="color: #008000; font-weight: bold;">&lt;input</span> <span style="color: #7d9029;">type=</span><span style="color: #ba2121;">&quot;text&quot;</span> <span style="color: #7d9029;">id=</span><span style="color: #ba2121;">&quot;modelURN&quot;</span> <span style="color: #008000; font-weight: bold;">/&gt;</span>
        <span style="color: #008000; font-weight: bold;">&lt;input</span> <span style="color: #7d9029;">type=</span><span style="color: #ba2121;">&quot;button&quot;</span> <span style="color: #7d9029;">value=</span><span style="color: #ba2121;">&quot;View model&quot;</span> <span style="color: #7d9029;">onclick=</span><span style="color: #ba2121;">&quot;showModel()&quot;</span><span style="color: #008000; font-weight: bold;">&gt;</span>
    <span style="color: #008000; font-weight: bold;">&lt;/div&gt;</span>
    <span style="color: #008000; font-weight: bold;">&lt;hr</span> <span style="color: #008000; font-weight: bold;">/&gt;</span>
    <span style="color: #008000; font-weight: bold;">&lt;div</span> <span style="color: #7d9029;">id=</span><span style="color: #ba2121;">&quot;viewer&quot;</span><span style="color: #008000; font-weight: bold;">&gt;</span>
    <span style="color: #008000; font-weight: bold;">&lt;/div&gt;</span>
<span style="color: #008000; font-weight: bold;">&lt;/body&gt;</span>
<span style="color: #008000; font-weight: bold;">&lt;/html&gt;</span>
</pre>
</div>
