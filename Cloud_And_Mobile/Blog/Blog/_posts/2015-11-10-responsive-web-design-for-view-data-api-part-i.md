---
layout: "post"
title: "Responsive Web Design for View & Data API - Part I"
date: "2015-11-10 12:10:53"
author: "Philippe Leefsma"
categories:
  - "HTML5"
  - "Philippe Leefsma"
  - "View and Data API"
original_url: "https://adndevblog.typepad.com/cloud_and_mobile/2015/11/responsive-web-design-for-view-data-api-part-i.html "
typepad_basename: "responsive-web-design-for-view-data-api-part-i"
typepad_status: "Publish"
---

<p>By&#0160;<a href="http://adndevblog.typepad.com/cloud_and_mobile/philippe-leefsma.html">Philippe Leefsma</a></p>
<p><a href="https://en.wikipedia.org/wiki/Responsive_web_design" target="_blank">Responsive Web Design</a> defines any approach or technique that makes your web page mobile friendly. It&#39;s actually more a concept rather than a well defined set of techniques.&#0160;</p>
<p>A responsive website should look good on virtually any screen size: desktops, tablets and smartphones. So should your website be responsive? When we know that over <a href="http://www.smartinsights.com/mobile-marketing/mobile-marketing-analytics/mobile-marketing-statistics/" target="_blank">50% of web browsing</a> is done through mobile nowadays, the answer is definitely yes. Let&#39;s put it another way: if your site isn&#39;t responsive, you&#39;re likely to either lose a lot of traffic or look technologically&#0160;backward!</p>
<p>I&#39;m travelling to New York next week in order to present at a <a href="http://www.meetup.com/ResponsiveWebDesign/events/226005779/" target="_blank">meetup</a>, so I spent the last couple of weeks catching up with RWD all over the web. Even if I&#39;m still scratching the surface, I feel a bit better about my web design skills and I&#39;m going to share my experience so far...</p>
<p>Before we start, here is a <a href="https://developers.google.com/web/fundamentals/design-and-ui/responsive/fundamentals/?hl=en" target="_blank">tutorial</a> that should make you familiar with the basics of RWD.</p>
<p>What are the tools which are available to help a novice creating a responsive web page?</p>
<p><strong>1/</strong> Let&#39;s start with the easiest! Grab an existing template and tweak it to your needs:&#0160;</p>
<p>A lot of existing responsive templates are available on the web, some are for sale and a lot are for free, so you can just browse for something close to what you are looking and start customising it. This obviously requires some html, css and a bit of JavaScript skills unless the template looks and does exactly what you need but it&#39;s unlikely.&#0160;</p>
<p>Here is a descent&#0160;<a href="http://www.w3schools.com/w3css/default.asp" target="_blank">tutorial</a>&#0160;on RWD from&#0160;w3schools. I used one of their sample to create my first responsive demo:</p>
<p><a href="http://viewer.autodesk.io/node/responsive/demo1/" target="_blank">http://viewer.autodesk.io/node/responsive/demo1</a></p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d173ff79970c-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="Screen Shot 2015-11-10 at 16.09.42" class="asset  asset-image at-xid-6a0167607c2431970b01b8d173ff79970c img-responsive" src="/assets/image_4ababb.jpg" title="Screen Shot 2015-11-10 at 16.09.42" /></a></p>
<p>And here a large collection of <a href="http://www.freewebtutorials.info/250-free-responsive-html5-css3-website-templates/?utm_source=ReviveOldPost&amp;utm_medium=social&amp;utm_campaign=ReviveOldPostSent" target="_blank">free responsive templates</a>. I picked two of them and customised it to my needs, integrating them with View &amp; Data API:</p>
<p><a href="http://viewer.autodesk.io/node/responsive/demo2" target="_blank">http://viewer.autodesk.io/node/responsive/demo2</a></p>
<p><a href="http://viewer.autodesk.io/node/responsive/demo3" target="_blank">http://viewer.autodesk.io/node/responsive/demo3</a></p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c7ea2d67970b-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="Screen Shot 2015-11-10 at 16.12.36" class="asset  asset-image at-xid-6a0167607c2431970b01b7c7ea2d67970b img-responsive" src="/assets/image_a49b04.jpg" title="Screen Shot 2015-11-10 at 16.12.36" /></a></p>
<p>The full source code for those project is on <a href="https://github.com/Developer-Autodesk/view-and-data-responsive" target="_blank">Github</a>.</p>
<p>&#0160;</p>
<p><strong>2/ </strong>The good old way: Do it yourself!</p>
<p>Templates are ok but at some point you may want to go a step further and either create your own responsive page or upgrade an existing page to be responsive. If your page was not designed to be responsive from the scratch however, it&#39;s likely you will have to completely rethink it. But writing a responsive page is not that hard after all...</p>
<p>HTML5 and CSS3 provide a great deal of features to handle responsiveness: <a href="https://css-tricks.com/snippets/css/a-guide-to-flexbox/" target="_blank">Flexbox</a>, <a href="https://developer.mozilla.org/en-US/docs/Web/CSS/Media_Queries/Using_media_queries" target="_blank">Media Queries</a>&#0160;or <a href="http://getbootstrap.com/" target="_blank">Bootstrap</a> will let you create cutting edge webpages. &#0160;</p>
<div>&#0160;</div>
<div><strong>3/</strong> Use modern user interface design tools</div>
<div>&#0160;</div>
<div>There might be plenty of them already on the web and this is an area which should be rapidly expending in the future. I&#39;ve picked two of them just because they are popular and expose a different experience: <a href="https://webflow.com%20" target="_blank">Webflow</a> and <a href="https://bootstrapstudio.io/" target="_blank">Bootstrap Studio</a>.</div>
<div>&#0160;</div>
<div><strong>Webflow</strong> is a pure cloud product and also offers more than just interface design: it&#39;s a content management tool and can host your website at the same time, providing an end-to-end experience to the developers. It&#39;s UI is pretty slick with lot of features already. The approach is to shield the user from having to write any code at all. I found it pretty intuitive and they have a lot of video tutorials on their site so you can get up to speed quickly. It&#39;s free to try and to use to some extent, you can even publish websites. The paid plans start at $20/month and allow you to export your pages to use html/css/js in your own app. There is no way back: you cannot re-import into Webflow a modified project.</div>
<div>&#0160;</div>
<div><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c7ea2da6970b-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="Screen Shot 2015-11-10 at 16.46.47" class="asset  asset-image at-xid-6a0167607c2431970b01b7c7ea2da6970b img-responsive" src="/assets/image_dfbf5c.jpg" title="Screen Shot 2015-11-10 at 16.46.47" /></a></div>
<div>&#0160;</div>
<div>Here is my fancy test webpage, it&#39;s just a UI which isn&#39;t connected to anything in the background:</div>
<div><a href="http://view-and-data.webflow.io/" target="_blank">http://view-and-data.webflow.io&#0160;<br /></a></div>
<div>&#0160;</div>
<div><strong>Bootstrap Studio</strong> is a very recent product. As the name gives you a clue, it&#39;s based on Bootstrap and the approach is different: this is a desktop app that runs on Windows, Mac and Linux. It&#39;s just the first version so it offers much less control than Webflow however it looks promising. &#0160;You can create components which are higher level gathering of base html elements, save and share them, so several components are already available from their website: for example responsive navbar, pretty footer, login form and so on... It lets you drag and drop elements to your design and tweak their properties through the UI, however it also lets you directly edit the css, which gives a great share of control over what you can do even if it&#39;s not doable from the product directly. As a developer I particularly enjoy that feature. As a desktop product, you buy it once and you own it. It&#39;s market at $50 and but discounted at $25 for early adopters. I find it well worths the money.</div>
<div>&#0160;</div>
<div>Here is a picture of a test web page I created to play with the tool:</div>
<div>&#0160;</div>
<div><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb088e0037970d-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="Screen Shot 2015-11-10 at 16.38.45" class="asset  asset-image at-xid-6a0167607c2431970b01bb088e0037970d img-responsive" src="/assets/image_55a4d2.jpg" title="Screen Shot 2015-11-10 at 16.38.45" /></a></div>
<div>&#0160;</div>
<div>The test would not be complete without applying a concrete example to the View &amp; Data API. So I experimented creating a responsive dialog interface I could inject in a viewer docking panel and the result is pretty satisfying:</div>
<div>&#0160;</div>
<div><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c7ea2dde970b-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="Screen Shot 2015-11-10 at 21.06.34" class="asset  asset-image at-xid-6a0167607c2431970b01b7c7ea2dde970b img-responsive" src="/assets/image_74fd17.jpg" title="Screen Shot 2015-11-10 at 21.06.34" /></a></div>
<div>&#0160;</div>
<div>And this is how the same dialog looks on the narrow screen of my phone, I had to use a scroll container because as the width is shrinking, the content gets shifted vertically, but I feel this is definitely acceptable:</div>
<div>&#0160;</div>
<div><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d1740185970c-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="Screenshot_2015-11-10-21-31-37" class="asset  asset-image at-xid-6a0167607c2431970b01b8d1740185970c img-responsive" src="/assets/image_31a940.jpg" title="Screenshot_2015-11-10-21-31-37" /></a></div>
<div>&#0160;</div>
<p>Live demo is testable <a href="http://viewer.autodesk.io/node/gallery/embed?id=54464d43af600b5c0a87254a&amp;extIds=Autodesk.ADN.Viewing.Extension.BootstrapPanel" target="_blank">here</a> and source code of the extension (es6) <a href="http://viewer.autodesk.io/node/gallery/uploads/extensions/Autodesk.ADN.Viewing.Extension.BootstrapPanel/Autodesk.ADN.Viewing.Extension.BootstrapPanel.js" target="_blank">here</a>.</p>
<p>There is still a lot to explore about of responsive design with and without View &amp; Data, so stay tuned!</p>
