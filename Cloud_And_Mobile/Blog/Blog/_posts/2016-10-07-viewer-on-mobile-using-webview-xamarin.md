---
layout: "post"
title: "Viewer on Mobile using WebView [Xamarin]"
date: "2016-10-07 07:56:18"
author: "Augusto Goncalves"
categories:
  - "Android"
  - "Augusto Goncalves"
  - "iOS"
  - "Mobile"
  - "Viewer"
original_url: "https://adndevblog.typepad.com/cloud_and_mobile/2016/10/viewer-on-mobile-using-webview-xamarin.html "
typepad_basename: "viewer-on-mobile-using-webview-xamarin"
typepad_status: "Publish"
---

<p>By Augusto Goncalves &#0160;(<a href="https://twitter.com/augustomaia">@augustomaia</a>)</p>
<p>Here is a recurrent question:&#0160;<em>can I use Forge Viewer on my mobile app?</em>&#0160;</p>
<p>And the direct answer is:&#0160;<em>Yes you can, use a WebView!</em></p>
<p>What about a sample? Here you have it! For this really quick demo I&#39;m using <a href="https://www.xamarin.com/">Xamarin cross-platform</a> approach. It&#39;s actually quite nice: write in C#, compile and deploy on iOS, Android and Windows phones.</p>
<p>&#0160;</p>
<p class="asset-video"><iframe allowfullscreen="" frameborder="0" height="281" src="https://www.youtube.com/embed/WlPz078J04o?feature=oembed" width="500"></iframe></p>
<p>Next I&#39;ll prepare a more complete sample (with other Forge APIs), but for now just would like to share this piece first.&#0160;</p>
<p>You&#39;ll need this for setting up iOS permissions:</p>
<p><em> &lt;key&gt;NSAppTransportSecurity&lt;/key&gt;</em><br /><em> &#0160;&#0160;&#0160;&#0160;&lt;dict&gt;</em><br /><em> &#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&lt;key&gt;NSAllowsArbitraryLoads &lt;/key&gt;</em><br /><em> &#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&lt;true/&gt;</em><br /><em> &#0160;&#0160;&#0160;&#0160;&lt;/dict&gt;</em></p>
<p>&#0160;</p>
