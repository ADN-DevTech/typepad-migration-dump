---
layout: "post"
title: "Using View and Data API with Meteor&ndash;part 2"
date: "2016-01-28 00:30:00"
author: "Daniel Du"
categories:
  - "Daniel Du"
  - "View and Data API"
original_url: "https://adndevblog.typepad.com/cloud_and_mobile/2016/01/using-view-and-data-api-with-meteorpart-2.html "
typepad_basename: "using-view-and-data-api-with-meteorpart-2"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/cloud_and_mobile/daniel-du.html">Daniel Du</a></p>  <p>In <a href="http://adndevblog.typepad.com/cloud_and_mobile/2016/01/using-view-and-data-api-with-meteor.html" target="_blank">previous post</a>, I managed to embed a viewer into Meteor application with View and Data API. But it is just a start, embedding a&#160; viewer with built-in functionalities. As a developer, we always want to add some additional functions to it, we do that by creating viewer extensions. As you know, we have quite a few extensions on <a href="http://gallery.autodesk.io">http://gallery.autodesk.io</a>. Is it possible to load extension into viewer with Meteor? In this post, I will keep on my investigation.</p>  <p>As said in previous post, meter has its order to load JS files, it is not easy to change it, but our extensions should be loaded after the viewer is initialized. It would be nice if meteor has “lazy load” mechanism, and then I found the package “<a href="https://atmospherejs.com/aramk/requirejs" target="_blank">aramk:requirejs</a>”, which works just like what I am expecting. To install the package, I just edit the “./meteor/packages” file and append it to the end of lines.</p>  <p>Most viewer extensions are expected to load after viewer is initialized or geometries are loaded, so I add an event listener to listen to “GEOMETRY_LOADED_EVENT” , in the event handler I am going to load my custom viewer extensions. We have many extensions samples <a href="http://gallery.autodesk.io">http://gallery.autodesk.io</a> and github, I am going to use some of them as a demo. Firstly we need ask meteor to load these extensions. As we discussed earlier, we have to choose suitable folders for these viewer extension files, otherwise these JS files may be loaded and executed by Meteor before viewer3D.js library is loaded, which causes “AutodeskNamespace is not defined” error. According to meteor document:</p>  <blockquote>   <p>All files inside a top-level directory called <code>public</code> are served as-is to the client. When referencing these assets, do not include <code>public/</code> in the URL, write the URL as if they were all in the top level. For example, reference <code>public/bg.png</code> as <code>&lt;img src='/bg.png' /&gt;</code>. This is the best place for <code>favicon.ico</code>, <code>robots.txt</code>, and similar files.</p> </blockquote>  <p>so “public” folder seems a good place, I just want meteor treats these extensions as static files and then load them when needed with requireJS, so here is my updated project folder structure with some extensions added:</p>  <p>.   <br />├── README.md    <br />├── client    <br />│&#160;&#160; ├── compatibility    <br />│&#160;&#160; ├── index.html    <br />│&#160;&#160; ├── index.js    <br />│&#160;&#160; ├── style.css    <br />│&#160;&#160; └── viewer    <br />│&#160;&#160;&#160;&#160;&#160;&#160; ├── viewer.html    <br />│&#160;&#160;&#160;&#160;&#160;&#160; └── viewer.js    <br />├── lib    <br />├── public    <br />│&#160;&#160; └── extensions    <br />│&#160;&#160;&#160;&#160;&#160;&#160; ├── Autodesk.ADN.Viewing.Extension.Basic    <br />│&#160;&#160;&#160;&#160;&#160;&#160; │&#160;&#160; └── Autodesk.ADN.Viewing.Extension.Basic.js    <br />│&#160;&#160;&#160;&#160;&#160;&#160; ├── Autodesk.ADN.Viewing.Extension.DockingPanel    <br />│&#160;&#160;&#160;&#160;&#160;&#160; │&#160;&#160; └── Autodesk.ADN.Viewing.Extension.DockingPanel.js    <br />│&#160;&#160;&#160;&#160;&#160;&#160; └── Autodesk.ADN.Viewing.Extension.Workshop    <br />│&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; └── Autodesk.ADN.Viewing.Extension.Workshop.js    <br />└── server    <br />&#160;&#160;&#160; └── index.js    <br /></p>  <p>&#160;</p>  <p>Now I am going to load these extension files as GEOMETRY_LOADED_EVENT” event handler, following code demos the idea, firstly load the extension js files with requireJS, and load the extension with viewer API: </p>  <pre class="csharpcode">        viewer.addEventListener(Autodesk.Viewing.GEOMETRY_LOADED_EVENT, <span class="kwrd">function</span>(){

            console.log(<span class="str">'GEOMETRY_LOADED_EVENT'</span>);

            <span class="rem">//load extensions</span>
            require([<span class="str">'/extensions/Autodesk.ADN.Viewing.Extension.Basic/Autodesk.ADN.Viewing.Extension.Basic.js'</span>],
                <span class="kwrd">function</span>(){
                viewer.loadExtension(<span class="str">'Autodesk.ADN.Viewing.Extension.Basic'</span>);
            });

            <span class="rem">//load extensions</span>
            require([<span class="str">'/extensions/Autodesk.ADN.Viewing.Extension.DockingPanel/Autodesk.ADN.Viewing.Extension.DockingPanel.js'</span>],
                <span class="kwrd">function</span>(){
                    viewer.loadExtension(<span class="str">'Autodesk.ADN.Viewing.Extension.DockingPanel'</span>);
                });

            <span class="rem">//load extensions</span>
            require([<span class="str">'/extensions/Autodesk.ADN.Viewing.Extension.Workshop/Autodesk.ADN.Viewing.Extension.Workshop.js'</span>],
                <span class="kwrd">function</span>(){
                    viewer.loadExtension(<span class="str">'Autodesk.ADN.Viewing.Extension.Workshop'</span>);
                });


        });</pre>

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
.csharpcode .lnum { color: #606060; }</style>With that I can load my custom extensions, here is the output in developer console:</p>

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
.csharpcode .lnum { color: #606060; }</style><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb08b2b99f970d-pi"><img title="Screen Shot 2016-01-29 at 12.09.52 AM" style="border-top: 0px; border-right: 0px; background-image: none; border-bottom: 0px; padding-top: 0px; padding-left: 0px; border-left: 0px; display: inline; padding-right: 0px" border="0" alt="Screen Shot 2016-01-29 at 12.09.52 AM" src="/assets/image_14df43.jpg" width="798" height="295" /></a></p>

<p>And here is how the custom panel extension is running: </p>

<p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c80defc0970b-pi"><img title="Screen Shot 2016-01-29 at 12.09.34 AM" style="border-top: 0px; border-right: 0px; background-image: none; border-bottom: 0px; padding-top: 0px; padding-left: 0px; border-left: 0px; display: inline; padding-right: 0px" border="0" alt="Screen Shot 2016-01-29 at 12.09.34 AM" src="/assets/image_bf327f.jpg" width="733" height="724" /></a></p>

<p>OK, hope this helps if you are developing your app with Meteor and want to add some cool stuff with View and Data API. </p>

<p>&#160;</p>

<p>Finally, I have to confess I made quite mistakes before get the result, I put it here&#160; in case you run into the same issue.</p>

<p>Error : “<strong>Uncaught SyntaxError: Unexpected token &lt;</strong>” . I got this error message, which makes me very confused. The extension code works fine on <a href="http://gallery.autodesk.io">http://gallery.autodesk.io</a>, but when I added it to my meteor application, it throw this weird error message:</p>

<p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c80dee39970b-pi"><img title="Screen Shot 2016-01-29 at 12.16.51 AM" style="border-top: 0px; border-right: 0px; background-image: none; border-bottom: 0px; padding-top: 0px; padding-left: 0px; border-left: 0px; display: inline; padding-right: 0px" border="0" alt="Screen Shot 2016-01-29 at 12.16.51 AM" src="/assets/image_fecaac.jpg" width="529" height="282" /></a>&#160;</p>

<p>Finally I found out that it is due to the path of require function, the path should be correct, and it is recommended to add “/” before the path to make it an absolute path. </p>

<pre class="csharpcode"><span class="rem">//load extensions</span>
require([<span class="str">'<strong>/</strong>extensions/Autodesk.ADN.Viewing.Extension.Basic/Autodesk.ADN.Viewing.Extension.Basic.js'</span>],
    <span class="kwrd">function</span>(){
       viewer.loadExtension(<span class="str">'Autodesk.ADN.Viewing.Extension.Basic'</span>);
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

<p>And if you are using other extensions like “Autodesk.ADN.Extensions.Chart ”, which is also include code snippet with requireJS in extension code, you need also make sure the path is correct, otherwise you will get the “Unexpected token &lt;” error. I was trying to load “Autodesk.ADN.Extensions.Chart ” extension but finally give it up as it always complains “async is undefined” even I required the async.min.js dependency. If you figured it out, I would be happy to hear from you. </p>

<p>As always, the complete source code is on github, you can check it out on <a title="https://github.com/Developer-Autodesk/meteor-view.and.data.api" href="https://github.com/Developer-Autodesk/meteor-view.and.data.api">https://github.com/Developer-Autodesk/meteor-view.and.data.api</a></p>
