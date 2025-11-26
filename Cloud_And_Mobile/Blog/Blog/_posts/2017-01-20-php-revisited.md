---
layout: "post"
title: "PHP revisited"
date: "2017-01-20 16:12:54"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "Forge"
  - "Viewer"
original_url: "https://adndevblog.typepad.com/cloud_and_mobile/2017/01/php-revisited.html "
typepad_basename: "php-revisited"
typepad_status: "Publish"
---

<p>By&nbsp;<a href="http://adndevblog.typepad.com/cloud_and_mobile/adam-nagy.html" target="_self">Adam Nagy</a>&nbsp;(<a href="https://twitter.com/adamthenagy" target="_self">@AdamTheNagy</a>)</p>
<img alt="" src="/assets/Viewer-v2.12-green.svg">&nbsp;<img alt="" src="/assets/Authentication-v1-green.svg">
<p>Someone contacted me about using the viewer on his website to show a couple of his models. As you may know by now, the viewer requires a valid <strong>access&nbsp;token</strong> to display a file.&nbsp;In theory you could try to call the <a href="https://developer.autodesk.com/en/docs/oauth/v2/reference/http/authenticate-POST/">authentication</a> endpoint from a client side <strong>JavaScript</strong> code as well. However, that way you would expose the <strong>Client Secret</strong> of your <strong>app</strong>. <br />The best thing is to use some <strong>server side</strong> processing to request&nbsp;the <strong>access token</strong>, which also helps you hide your <strong>app</strong>'s <strong>Client Secret</strong>. &nbsp;&nbsp;</p>
<p>If you already have a website hosted somewhere, then you might not want to migrate all that to another server just so that&nbsp;you could also run e.g.&nbsp;<a href="https://nodejs.org/en/">Node.js</a> code. You could either just create another web server only&nbsp;for the authentication part, or simply add some server-side code to your existing webpage. Most servers (even the free hosting ones) support <a href="http://php.net/manual/en/intro-whatis.php">PHP</a>, so you could probably go with that.</p>
<p>As a test I got a free website, <a href="http://adamenagy.com">http://adamenagy.com</a>. After that I could simply follow what <strong>Augusto</strong> already talked about in <a href="http://adndevblog.typepad.com/cloud_and_mobile/2016/01/quick-and-simple-php-view-and-data-sample.html">this blog post</a>.<br />I just needed to create and upload <strong>three files</strong> to the main folder of my webpage, where the <strong>index.html</strong> or <strong>index.php</strong> file is: <strong>viewer.html</strong>, <strong>auth.php</strong>,&nbsp;<strong><span class="s1">httpful.phar</span></strong></p>
<p>The only thing I needed to add to the original code was the <a href="https://developer.autodesk.com/en/docs/oauth/v2/overview/scopes/">scoping</a> for the <strong>access token</strong> request: <strong>scope=data:read </strong>(line 39 in auth.php)<strong><br /></strong>And of course I had to add my <strong>app</strong>'s <strong>Client ID</strong> and <strong>Client Secret</strong> to the code (line 11 &amp; 12 in auth.php)<strong><br /></strong>My server's <strong>PHP</strong> did not like the "<strong>define()</strong>" parts, but adding what <strong>Maxence</strong> suggested in the comments of <strong>Augusto</strong>'s post helped: use double quotes in them for the constant names (line 11-13 in auth.php)</p>
<p>Now I can just go on&nbsp;<a href="http://adamenagy.com/viewer.html">http://adamenagy.com/viewer.html</a>&nbsp;and the viewer shows the model whose <strong>URN</strong> I insert in the text&nbsp;box.</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d2564767970c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false" style="display: inline;"><img class="asset  asset-image at-xid-6a0167607c2431970b01b8d2564767970c img-responsive" title="AuthenticateInPHP" src="/assets/image_43a98a.jpg" alt="AuthenticateInPHP" /></a></p>
<p>The three files (<strong>viewer.html</strong>, <strong>auth.php</strong>,&nbsp;<strong><span class="s1">httpful.phar</span></strong>): <a href="https://github.com/adamenagy/AuthenticateInPHP">https://github.com/adamenagy/AuthenticateInPHP</a>&nbsp;&nbsp;</p>
