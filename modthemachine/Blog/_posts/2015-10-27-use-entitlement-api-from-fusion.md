---
layout: "post"
title: "Use Entitlement API from Fusion add-in"
date: "2015-10-27 17:38:25"
author: "Adam Nagy"
categories:
  - "Adam"
  - "Beginning API"
  - "Fusion 360"
original_url: "https://modthemachine.typepad.com/my_weblog/2015/10/use-entitlement-api-from-fusion.html "
typepad_basename: "use-entitlement-api-from-fusion"
typepad_status: "Publish"
---

<p>If you have an app on the <a href="https://apps.autodesk.com/en" target="_self">Autodesk App Store</a> then you might want to check if the user actually paid for and downloaded your app from the store or just copied it from someone else&#39;s computer. This what the <a href="http://usa.autodesk.com/adsk/servlet/item?siteID=123112&amp;id=24243865" target="_self">Entitlement API</a> can help you with.&#0160;</p>
<p>In the September release the <strong>Fusion API</strong> exposed two new user specific properties under the <strong>Application</strong> object: <strong>userName</strong> and <strong>userId</strong>. The latter is needed to take advantage of the <strong>Entitlement API.</strong></p>
<p>Probably it makes most sense to use this <strong>API</strong> from a <strong>C++</strong> add-in since that is much better protected because of compilation. So that&#39;s what this article will focus on.</p>
<p>The <strong>Entitlement API</strong> is a simple <strong>RESTful API</strong> where you just need to send an <strong>HTTP</strong>&#0160;<strong>GET</strong> request to the <strong>App Store</strong> server to find out what you need. It&#39;s up to you what library you use for that as there are many options. I was looking into using a cross-platform library since <strong>Fusion</strong> is supporting both <strong>Mac</strong> and <strong>Windows</strong>, however the ones I tried were not straight-forward to use at all. In the end I decided to go with the <a href="http://curl.haxx.se/libcurl/c/libcurl.html" target="_self">cURL</a> library which is really simple to use from a <strong>MacOS</strong> add-in.</p>
<p>The steps I needed to take:</p>
<p><strong>1)</strong> Create a new <strong>C++ Fusion</strong> add-in:</p>
<p><a class="asset-img-link" href="http://modthemachine.typepad.com/.a/6a00e553fcbfc6883401b8d16d78e0970c-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="CreateAddIn" class="asset  asset-image at-xid-6a00e553fcbfc6883401b8d16d78e0970c img-responsive" src="/assets/image_949849.jpg" title="CreateAddIn" /></a></p>
<p><a class="asset-img-link" href="http://a6.typepad.com/6a0112791b8fe628a401b8d16d78f6970c-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="CreateAndEdit" border="0" class="asset  asset-image at-xid-6a0112791b8fe628a401b8d16d78f6970c image-full img-responsive" src="/assets/image_262481.jpg" title="CreateAndEdit" /></a></p>
<p><strong>2)</strong> Add <strong>cURL</strong> support to your add-in:</p>
<p>Once you&#39;re in <strong>Xcode</strong>, go into the <strong>Project</strong> settings and under &quot;<strong>Build Settings</strong> &gt;&gt; <strong>Linking</strong> &gt;&gt; <strong>Other Linker Flags</strong>&quot; add &quot;<strong>-lcurl</strong>&quot;</p>
<p><a class="asset-img-link" href="http://a6.typepad.com/6a0112791b8fe628a401b7c7e3ae2e970b-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="CURL" border="0" class="asset  asset-image at-xid-6a0112791b8fe628a401b7c7e3ae2e970b image-full img-responsive" src="/assets/image_801112.jpg" title="CURL" /></a></p>
<p><strong>3)</strong> Add the following statements at the top of the <strong>C++</strong> file:</p>
<pre>#include &lt;curl/curl.h&gt;
#include &lt;regex&gt;

using namespace std;</pre>
<p><strong>4)</strong> Also add this code which is using the <a href="https://en.wikipedia.org/wiki/Regular_expression" target="_self">Regular Expressions</a> library:<br />(there are quite a few <strong>RegEx</strong> testers on the web that you can use - I played with this:&#0160;<a href="https://www.myregextester.com/index.php" target="_self" title="">https://www.myregextester.com/index.php</a>)</p>
<pre>// Fetch the value of a given property
// inside a json text
string GetValue(string text, string find)
{
  regex IsValid(&quot;\\s*\&quot;&quot; + find + &quot;\&quot;\\s*:\\s*(.+?)[\\s,]&quot;);
  smatch m;
  
  if (regex_search(text, m, IsValid))
  {
    if (m.size() &gt; 1)
      return m[1];
  }
  
  return &quot;&quot;;
}

size_t callback_func(void *ptr, size_t size, size_t count, void *stream)
{
  // ptr - your string variable.
  // stream - data chunck you received
  
  string reply((char*)ptr);
  string ret = GetValue(reply, &quot;IsValid&quot;);
  
  // If the app is not valid
  if (ret != &quot;true&quot;)
    ui-&gt;messageBox(&quot;IsValid = false&quot;);
  else
    ui-&gt;messageBox(&quot;IsValid = true&quot;);
    
  return 0;
}

// Using Entitlement API to check if the
// app usage is valid
void CheckValidity()
{
  // e.g. the URL for Voronoi Sketch generator is:
  // https://apps.autodesk.com/FUSION/en/Detail/Index?id=appstore.exchange.autodesk.com%3avoronoisketchgenerator_macos%3aen
  // This cotains the &quot;id&quot;:
  // &quot;appstore.exchange.autodesk.com%3avoronoisketchgenerator_macos%3aen&quot;
  // so we can use that
  string userId = app-&gt;userId();
  string userName = app-&gt;userName();
  string appId =
    &quot;appstore.exchange.autodesk.com%3aaddinrename_windows32and64%3aen&quot;;
  string url =
    string(&quot;https://apps.exchange.autodesk.com/webservices/checkentitlement&quot;) +
    string(&quot;?userid=&quot;) + userId +
    string(&quot;&amp;appid=&quot;) + appId;
  
  CURL * curl = curl_easy_init();
  if(curl) {
    CURLcode res;
    curl_easy_setopt(curl, CURLOPT_URL, url.c_str());
    curl_easy_setopt(curl, CURLOPT_WRITEFUNCTION, callback_func);
    res = curl_easy_perform(curl);
    curl_easy_cleanup(curl);
  }
}</pre>
<p><strong>5)</strong> Now you can call <strong>CheckValidity()</strong> from inside your <strong>run()</strong> function</p>
<p>You can easily figure out the <strong>id</strong> of your app by finding it on the <strong>App Store</strong> and then checking the <strong>URL</strong> in the browser. That will contain an <strong>id</strong> parameter which is what you need. E.g. the&#0160;<strong>MacOS</strong> <strong>English</strong> version of the <strong>Voronoi Sketch Generator</strong> has this <strong>URL</strong> and I highlighted the important part with the <strong>id</strong>:<br /><a href="https://apps.autodesk.com/FUSION/en/Detail/Index?id=appstore.exchange.autodesk.com%3avoronoisketchgenerator_macos%3aen%20" target="_self">https://apps.autodesk.com/FUSION/en/Detail/Index?id=<strong>appstore.exchange.autodesk.com%3avoronoisketchgenerator_macos%3aen</strong>&#0160;</a></p>
<p><a class="asset-img-link" href="http://modthemachine.typepad.com/.a/6a00e553fcbfc6883401b7c7e3bdaa970b-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="Voronoionappstore" border="0" class="asset  asset-image at-xid-6a00e553fcbfc6883401b7c7e3bdaa970b image-full img-responsive" src="/assets/image_91875.jpg" title="Voronoionappstore" /></a></p>
<p>Worth noting that the <strong>Entitlement API</strong> only provides information on if the user paid for your app or not, it does not tell you in case of a <strong>Free</strong> app if it has been downloaded by the given user from the store or not.&#0160;</p>
<p>So when I use this functionality with a free app then <strong>IsValid</strong> will always return <strong>false</strong>:</p>
<p><a class="asset-img-link" href="http://a2.typepad.com/6a0112791b8fe628a401b7c7e3afea970b-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="Isvalidfalse" class="asset  asset-image at-xid-6a0112791b8fe628a401b7c7e3afea970b img-responsive" src="/assets/image_797952.jpg" title="Isvalidfalse" /></a></p>
<p>But if I test it with a priced app which I paid for then I get <strong>true</strong>:</p>
<p><a class="asset-img-link" href="http://a5.typepad.com/6a0112791b8fe628a401bb088781fd970d-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="Isvalidtrue" class="asset  asset-image at-xid-6a0112791b8fe628a401bb088781fd970d img-responsive" src="/assets/image_516122.jpg" title="Isvalidtrue" /></a></p>
<p>Since the <strong>Entitlement</strong> <strong>API</strong> is based on simple <strong>HTTP</strong> requests, therefore you can easily test it in utilities like the <a href="https://addons.mozilla.org/en-GB/firefox/addon/restclient/" target="_self"><strong>RESTClient</strong></a> for <strong>Firefox</strong>.</p>
<p><a class="asset-img-link" href="http://modthemachine.typepad.com/.a/6a00e553fcbfc6883401bb08879026970d-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="Restclient" border="0" class="asset  asset-image at-xid-6a00e553fcbfc6883401bb08879026970d image-full img-responsive" src="/assets/image_284655.jpg" title="Restclient" /></a></p>
<p>You can find the full source code here:&#0160;<a href="https://github.com/AutodeskFusion360/EntitlementAPI" target="_self" title="">https://github.com/AutodeskFusion360/EntitlementAPI</a><br />At the moment the sample only contains code for <strong>MacOS</strong>, but will likely add a <strong>Windows</strong> version as well later.</p>
<p>-Adam</p>
