---
layout: "post"
title: "Automated browser testing with Node.js + Selenium"
date: "2015-11-27 19:26:15"
author: "Philippe Leefsma"
categories:
  - "Browser"
  - "HTML5"
  - "Javascript"
  - "Philippe Leefsma"
  - "View and Data API"
  - "Web Server"
original_url: "https://adndevblog.typepad.com/cloud_and_mobile/2015/11/automated-browser-testing-with-nodejs-selenium.html "
typepad_basename: "automated-browser-testing-with-nodejs-selenium"
typepad_status: "Publish"
---

<p>By&nbsp;<a href="http://adndevblog.typepad.com/cloud_and_mobile/philippe-leefsma.html">Philippe Leefsma</a></p>
<p><a href="http://www.seleniumhq.org/" target="_self">SeleniumHQ</a>&nbsp;is a free set of tools and APIs that allow you to simulate user actions in a browser, locate html elements such as buttons, inputs, send clicks and so on... This is what our development team is using, among other tools, to automate tests with the viewer.</p>
<p>I gave a quick shot at it and it looks promising: Selenium API can be invoked with no less than seven programming languages! Java, C#, Python, Ruby, PHP, Perl, JavaScript (node.js).</p>
<p>I'm a big Node.js addict, so I tested again Node. The steps involved to set it up on your machine are very easy:</p>
<p>1/ Download the Selenium server from their <a href="http://www.seleniumhq.org/download/" target="_self">download</a> section</p>
<p>2/ The version at the time of this writing (2.48.2) requires Java SDK7 to be installed, so you may need to update your Java setup</p>
<p>3/ Run the Selenium server: <em><strong>Java -jar path-to-selenium-srv.jar</strong></em></p>
<p>4/ Write your test and run it based on the framework you selected. If you are using Node, an npm package is available: <a href="https://www.npmjs.com/package/selenium-webdriver" target="_self">selenium-webdriver</a></p>

<p>I created a basic test which already does a bit of work:</p>
<p>- It will start a web browser using the url of my gallery at <a href="http://gallery.autodesk.io" target="_self">http://gallery.autodesk.io</a>. &nbsp;My test supports firefox and chrome (an additional <a href="https://www.npmjs.com/package/chromedriver" target="_self">chromedriver</a> package is required for chrome and the setup is slightly different).</p>
<p>- It will then wait for the model buttons to appear and will open a specific one: <em>Engine</em></p>
<p>- Once the model is loaded, it will open the extension manager dialog and successively toggle each extension On and OFF. Some extensions are displaying an alert dialog, so a specific handling in that case is needed</p>
<p>That's it. Obviously each extension would require to write a specific test in order to be tested properly and some maybe tricky, but this is already a decent start. I'm also attempting to access the browser console output but I couldn't get it to work properly so far... Work in progress.</p>
<p>Here is the full node.js script of my selenium test:</p>

<script src="https://gist.github.com/leefsmp/3e4385e08ea27e30ba96.js"></script>
