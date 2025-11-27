---
layout: "post"
title: "Does Inventor support multi-threaded applications?"
date: "2012-06-01 01:37:53"
author: "Philippe Leefsma"
categories:
  - "Inventor"
  - "Philippe Leefsma"
original_url: "https://adndevblog.typepad.com/manufacturing/2012/06/does-inventor-support-multi-threaded-applications.html "
typepad_basename: "does-inventor-support-multi-threaded-applications"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/manufacturing/philippe-leefsma.html" target="_self">Philippe Leefsma</a></p>  <p><b>Q:</b></p>  <p>Can a multi-threaded application be written that interfaces with the Inventor API, and how are the API calls synchronized?</p>  <p><a name="section2"></a></p>  <p><b>A:</b></p>  <p>Developers are free to spawn worker threads for their own purposes, but Inventor is a single threaded application. Inventor cannot perform concurrent modeling operations. Any calls made to the API from a worker thread would be marshaled back to Inventor's single threaded apartment and would run serially with any commands that Inventor may already be performing.</p>
