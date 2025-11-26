---
layout: "post"
title: "Become a Java EE developer - Part II: Basic RESTful API from a servlet"
date: "2015-08-21 02:59:24"
author: "Philippe Leefsma"
categories:
  - "Application Server"
  - "Cloud"
  - "HTML5"
  - "Other"
  - "Philippe Leefsma"
  - "Server"
original_url: "https://adndevblog.typepad.com/cloud_and_mobile/2015/08/become-a-java-ee-developer-part-ii-basic-restful-api-from-a-servlet.html "
typepad_basename: "become-a-java-ee-developer-part-ii-basic-restful-api-from-a-servlet"
typepad_status: "Publish"
---

<p>By&nbsp;<a href="http://adndevblog.typepad.com/cloud_and_mobile/philippe-leefsma.html">Philippe Leefsma</a></p>
<p>Here is the second part of my Java EE tutorial series. Last week I took a look at setting up a Tomcat server and creating my first project with Eclipse. If you missed it, take a look <a href="http://adndevblog.typepad.com/cloud_and_mobile/2015/08/become-a-java-ee-developer-part-i-tomcat-setup-and-my-first-servlet.html" target="_self">here</a>&nbsp;and make sure you are familiar with the basics.</p>
<p>The next step would be to create a RESTful API so our future web application will be able to easily talk to our backend. I want to start from the scratch to understand the challenges, so instead of using one of the many existing <a href="http://www.quora.com/Whats-the-best-RESTful-web-framework-to-use-with-Java" target="_self">REST frameworks</a>, I decided to implement myself a REST API using a simple servlet like the one I created last week.</p>
<p>I'm going to create an API that will perform the following tasks and look as follow:</p>
<p>- Get the list of models from our database or get a specific model based on its id:</p>
<p>&nbsp; &nbsp;<em><strong> GET /models</strong></em></p>
<p><em><strong>&nbsp; &nbsp; GET /models/{id}</strong></em></p>
<p>- Add a new model to the database:</p>
<p>&nbsp; &nbsp; <em><strong>POST /models</strong></em></p>
<p>- Update an existing model in database:</p>
<p>&nbsp; &nbsp; <em><strong>PUT /model/{id}</strong></em></p>
<p>- Remove an existing model from the database:</p>
<p>&nbsp; &nbsp; <em><strong>DELETE /models/{id}</strong></em></p>
<p>A model will be a very basic structure containing info to load into the viewer:</p>
<p><a class="asset-img-link" style="display: inline;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d14c2c48970c-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b01b8d14c2c48970c image-full img-responsive" title="Screen Shot 2015-08-21 at 11.28.24 AM" src="/assets/image_59845d.jpg" alt="Screen Shot 2015-08-21 at 11.28.24 AM" border="0"></a></p>
<p>So let's get started...</p>
<p>1/ First step is to add a new servlet to our project: <em>right-click your project &gt; New &gt; Servlet</em>. I name it "Models".&nbsp;By default the servlet will be mapped to the following url: <em>/AppName/Models</em>. We need to allow more routes to be redirected to that servlet, so edit the <em>@WebServlet</em> decorator as follow, so every request starting with <em>/model</em> will be redirected to that servlet:</p>
<p><a class="asset-img-link" style="display: inline;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb086699a0970d-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b01bb086699a0970d image-full img-responsive" title="Screen Shot 2015-08-21 at 11.32.57 AM" src="/assets/image_50e55d.jpg" alt="Screen Shot 2015-08-21 at 11.32.57 AM" border="0"></a></p>
<p>2/ My REST API is going to use JSON as data format, there are some built-in classes to handle serialization and deserialization, however I'm familiar with a powerful library from Google: <a href="https://github.com/google/gson" target="_self">GSON</a>&nbsp;which I used previously in some Android projects. So download the .jar from <a href="http://search.maven.org/#artifactdetails%7Ccom.google.code.gson%7Cgson%7C2.3.1%7Cjar" target="_self">there</a> and add it to your project: <em>Right-click &gt; Properties &gt; Java Build Path &gt; Add External Jars &gt; browser to gson.jar</em>.</p>
<p>Once you've done that, there is another trick: as your project now depends on a third party library, you need to include that in your deployment package, so it will be available when it runs on the server. No worries: Still from project's Properties &gt; <em>Deployment Assembly &gt; Add &gt; Java Build Path Entries &gt; select gson.jar &gt; done!</em></p>
<p><a class="asset-img-link" style="display: inline;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c7c2592b970b-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b01b7c7c2592b970b image-full img-responsive" title="Screen Shot 2015-08-21 at 11.41.45 AM" src="/assets/image_c470ee.jpg" alt="Screen Shot 2015-08-21 at 11.41.45 AM" border="0"></a>&nbsp;</p>
3/ Implement <em><strong>doGet, doPut, doPost</strong></em> and <strong><em>doDelete</em></strong> methods in your servlet. In the example below I'm using a simple hashmap to "simulate" a backend database, so it can keep track of my records, this is obviously not a real world scenario but it serves the purpose of that tutorial.
<p>Here is a very naive implementation of my API:</p>
<script type="text/javascript" src="https://gist.github.com/leefsmp/b4c089734852c793cf85.js"></script>
4/ Finally run the servlet from Eclipse and you can use a tool like <a href="https://www.getpostman.com/">Postman</a> to test your API:
<br><br>
<a class="asset-img-link" style="display: inline;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d14c2dbe970c-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b01b8d14c2dbe970c image-full img-responsive" alt="Screen Shot 2015-08-21 at 11.53.26 AM" title="Screen Shot 2015-08-21 at 11.53.26 AM" src="/assets/image_b9a9fc.jpg" border="0"></a><br>

To wrap it up: we have a working RESTful API run by a servlet, however I found it pretty laborious to parse the url to extract parameters like the model Id although it's very basic. 
<br><br>
If we have to add some more complexity to it, let's say for example <strong><em>GET /models/{id}/components/{nodeId}/material</em></strong> to find material attached to a specific component of a specific model, then it will quickly become very messy.
<br><br>
Next week I will pick up a popular REST framework and take a look how much easier it can be to achieve the same, stay tuned ...



<fieldset class="zemanta-related"><legend class="zemanta-related-title">Related articles</legend><div style="margin: 0; padding: 0; overflow: hidden;" class="zemanta-article-ul zemanta-article-ul-image"><div style="padding: 0; background: none; list-style: none; display: block; float: left; vertical-align: top; text-align: left; width: 84px; font-size:11px; margin: 2px 10px 10px 2px;" class="zemanta-article-ul-li-image zemanta-article-ul-li"><a target="_blank" href="http://adndevblog.typepad.com/cloud_and_mobile/2015/08/become-a-java-ee-developer-part-ii-basic-restful-api-from-a-servlet.html" style="box-shadow: 0px 0px 4px #999; padding: 2px; display: block; border-radius: 2px; text-decoration: none;"><img style="padding: 0; margin: 0; border: 0; display: block; width: 80px; max-width: 100%;" src="/assets/noimg_120_80_80.jpg"></a><a target="_blank" href="http://adndevblog.typepad.com/cloud_and_mobile/2015/08/become-a-java-ee-developer-part-ii-basic-restful-api-from-a-servlet.html" style="display: block; overflow:hidden; text-decoration: none; line-height: 12pt; height: 80px; padding: 5px 2px 0 2px;">Become a Java EE developer - Part II: Basic RESTful API from a servlet</a></div></div></fieldset>
