---
layout: "post"
title: "Simplest web server on Mac"
date: "2014-04-08 08:47:10"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "Web Server"
original_url: "https://adndevblog.typepad.com/cloud_and_mobile/2014/04/simplest-web-server-on-mac.html "
typepad_basename: "simplest-web-server-on-mac"
typepad_status: "Publish"
---

<p>By&#0160;<a href="http://adndevblog.typepad.com/cloud_and_mobile/adam-nagy.html" target="_self">Adam Nagy</a></p>
<p>Just ran into <a href="http://vimeo.com/40929961" target="_self">this video</a> from <strong>jQuery UK</strong> and found out how easy it is to use the <a href="https://docs.python.org/2/library/simplehttpserver.html" target="_self">Python web server</a> which is installed on all Mac&#39;s. You just run this from the <strong>Terminal</strong> and you&#39;ll have access to all the files on your system through the web server in your browser, so you can test your own webpages:</p>
<pre>python -m SimpleHTTPServer 8000</pre>
