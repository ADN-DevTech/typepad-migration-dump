---
layout: "post"
title: "Avoid freezing or memory leak of AIMS"
date: "2012-05-20 20:46:12"
author: "Daniel Du"
categories:
  - "AIMS 2012"
  - "AIMS 2013"
  - "Daniel Du"
  - "MapGuide Enterprise 2011"
original_url: "https://adndevblog.typepad.com/infrastructure/2012/05/avoid-freezing-or-memory-leak-of-aims.html "
typepad_basename: "avoid-freezing-or-memory-leak-of-aims"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/infrastructure/daniel-du.html">Daniel Du</a></p>  <p>You many run into the issue of memory leak when working with Autodesk Infrastructure Map Server or MapGuide Enterprise. The memory may increase to a very high level and finally brings down the server, or the server just gets frozen and does not response any more.</p>  <p>Here are some suggestions, hope it is helpful for you. </p>  <p>Firstly, if you are running custom applications, please check your code carefully, to make sure to close the feature reader and release resources as early as possible. You can use following code snippet: </p>  <pre class="csharpcode">MgFeatureReader reader = featureService.SelectFeatures();
<span class="kwrd">try</span>
{
 <span class="rem">//your code here ...</span>
}
<span class="kwrd">finally</span>
{
 Reader.close();
}</pre>
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

<p>Or :</p>

<pre class="csharpcode">MgFeatureService featureService <br />            = siteConnection.CreateService(..)</pre>

<pre class="csharpcode"><span class="kwrd">try</span>
{
<span class="kwrd">  using</span>(MgFeatureReader reader <br />            = featureService.SelectFeatures(..)) 
  { 
<span class="rem">    // your code here …</span>
    Reader.close();
  }

}
<span class="kwrd">finally</span>
{
  featureService.dispose();
}</pre>

<p>Autodesk Infrastructure Map Server is designed very carefully to avoid memory leak, there are sometimes memory leaks in web-based applications and application servers, given the complexity and number of dependent processes. What might appear to be a minor memory leak can accumulate over time and affect general server performance.</p>

<p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b016766a3854e970b-pi"><img style="background-image: none; border-right-width: 0px; padding-left: 0px; padding-right: 0px; display: inline; border-top-width: 0px; border-bottom-width: 0px; border-left-width: 0px; padding-top: 0px" title="image" border="0" alt="image" src="/assets/image_3dce0e.jpg" width="353" height="394" /></a></p>

<p>One workaround is to write a routine to occasionally restart your web server service, thereby freeing up orphaned resources. Setting the Autodesk MapGuide service to restart automatically may also help in the event that the service has shut down. Following script to stop and start the service: </p>

<blockquote>
  <p>net stop InfrastructureMapServer2012 
    <br />net start InfrastructureMapServer2012</p>
</blockquote>

<p>You might want to look at the service log files to determine the cause for the service to stop and rectify it to prevent it in the future.</p>

<p>For more suggestions of best practice, please refer to the <a href="http://sandbox.mapguide.com/index.php/Main_Page" target="_blank">MapGuide Best Practice Wiki</a>. </p>
