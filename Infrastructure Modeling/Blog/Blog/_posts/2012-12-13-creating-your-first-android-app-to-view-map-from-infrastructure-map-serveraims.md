---
layout: "post"
title: "Creating your first Android App to view Map from Autodesk Infrastructure Map Server(AIMS)"
date: "2012-12-13 02:26:51"
author: "Partha Sarkar"
categories:
  - "AIMS 2012"
  - "AIMS 2013"
  - "Android"
  - "Cloud"
  - "Mobile"
  - "Partha Sarkar"
  - "Web"
original_url: "https://adndevblog.typepad.com/infrastructure/2012/12/creating-your-first-android-app-to-view-map-from-infrastructure-map-serveraims.html "
typepad_basename: "creating-your-first-android-app-to-view-map-from-infrastructure-map-serveraims"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/infrastructure/partha-sarkar.html" target="_self">Partha Sarkar</a></p>
<p>The very
first step is setting up the development environment with SDK, Tools and
configuring them correctly. I think, <a href="http://developer.android.com/training/basics/firstapp/index.html">Building
Your First App</a> is the first place you need to go to understand what SDK,
Tools are required and how to configure them. </p>
<p>&#0160;</p>
<p>Once you have
completed your &quot;Hello World&quot; app and some of the tutorial android
apps, you are almost ready to see how you can create a simple mapping
application to view Map served from Autodesk Infrastructure Map Server.</p>
<p>&#0160;</p>
<p>In this
example I used <a href="http://developer.android.com/reference/android/webkit/WebView.html">WebView</a>
class from Android <a class="zem_slink" href="http://en.wikipedia.org/wiki/Application_programming_interface" rel="wikipedia" target="_blank" title="Application programming interface">APIs</a>. Here is the code snippet :</p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="line-height: 140%;">import android.app.Activity;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">import android.os.Bundle;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">import android.webkit.WebView;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">import android.webkit.WebViewClient;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">public</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">class</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">AIMSWebViewActivity</span><span style="line-height: 140%;"> extends Activity {</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;WebView mWebView;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;@Override</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;</span><span style="color: blue; line-height: 140%;">public</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">void</span><span style="line-height: 140%;"> onCreate(Bundle savedInstanceState) {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; super.onCreate(savedInstanceState);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; setContentView(R.layout.main);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: green; line-height: 140%;">// This initializes the member WebView</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: green; line-height: 140%;">// with the one from the Activity layout;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: green; line-height: 140%;">// requests a WebSettings object with getSettings();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: green; line-height: 140%;">// and enables JavaScript for the WebView with</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: green; line-height: 140%;">// setJavaScriptEnabled(boolean).</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: green; line-height: 140%;">// Finally, an initial web page is loaded with loadUrl(String).</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; mWebView = (WebView) findViewById(R.id.webview);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; mWebView.getSettings().setJavaScriptEnabled(</span><span style="color: blue; line-height: 140%;">true</span><span style="line-height: 140%;">);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; mWebView.getSettings().setBuiltInZoomControls(</span><span style="color: blue; line-height: 140%;">true</span><span style="line-height: 140%;">);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; mWebView.loadUrl(</span><span style="color: #a31515; line-height: 140%;">&quot;http://www.MyAIMSSite.com/MapView/&quot;</span><span style="line-height: 140%;">);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: green; line-height: 140%;">// setContentView(mWebView); // this works too</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; mWebView.setWebViewClient(</span><span style="color: blue; line-height: 140%;">new</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">HelloWebViewClient</span><span style="line-height: 140%;">());</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;}</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;</span><span style="color: blue; line-height: 140%;">private</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">class</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">HelloWebViewClient</span><span style="line-height: 140%;"> extends WebViewClient {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; @Override</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: blue; line-height: 140%;">public</span><span style="line-height: 140%;"> boolean shouldOverrideUrlLoading(WebView view, String url) {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160; view.loadUrl(url);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160; </span><span style="color: blue; line-height: 140%;">return</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">true</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;}</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">}</span></p>
</div>
<p>&#0160;</p>
<p>Screenshot of
Android AIMS Map View application :</p>
<p>&#0160;
<a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b017d3ec00d15970c-pi" style="display: inline;"><img alt="AIMS_Android_webview" class="asset  asset-image at-xid-6a0167607c2431970b017d3ec00d15970c" src="/assets/image_ce6d15.jpg" title="AIMS_Android_webview" /></a>&#0160;</p>
<p>&#0160;</p>
<p>And here is a
short video of the same running in Emulator.</p>
<p><iframe frameborder="0" height="344" src="http://www.youtube.com/embed/hbbiBKAvfc8?fs=1&amp;feature=oembed" width="459"></iframe>&#0160;</p>
<p>I hope this
is useful to you!</p>
