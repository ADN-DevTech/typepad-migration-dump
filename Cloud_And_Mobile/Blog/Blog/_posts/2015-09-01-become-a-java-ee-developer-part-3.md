---
layout: "post"
title: "Become a Java EE developer - Part III: : RESTful API with Jersey"
date: "2015-09-01 09:52:23"
author: "Philippe Leefsma"
categories:
  - "Application Server"
  - "Philippe Leefsma"
  - "Web Server"
original_url: "https://adndevblog.typepad.com/cloud_and_mobile/2015/09/become-a-java-ee-developer-part-3.html "
typepad_basename: "become-a-java-ee-developer-part-3"
typepad_status: "Publish"
---

<p>By&nbsp;<a href="http://adndevblog.typepad.com/cloud_and_mobile/philippe-leefsma.html">Philippe Leefsma</a></p>
<p>Here is the third part of my Java EE series and this week it's time to tackle how to create a REST API using a proper framework. There are many existing Java frameworks that would help you achieve that, some of the most popular are&nbsp;<a title="" href="http://sparkjava.com/" target="_self">Spark</a>,&nbsp;<a title="" href="http://resteasy.jboss.org/" target="_self">RESTEasy</a>,&nbsp;<a title="" href="http://restx.io/" target="_self">RESTX</a>&nbsp;or&nbsp;<a title="" href="https://jersey.java.net/" target="_self">Jersey</a>. As I have no experience with any of them and no specific constraint, I picked up Jersey which seems to have a good API, descent support from the community and it's an implementation of the&nbsp;JAX-RS reference. So let's get started!</p>
<p>If you missed the two previous chapters, here are the links as I'm reusing the same project rather than starting one from scratch:&nbsp;</p>
<p><a title="" href="http://adndevblog.typepad.com/cloud_and_mobile/2015/08/become-a-java-ee-developer-part-i-tomcat-setup-and-my-first-servlet.html" target="_self">Become a Java EE developer - Part I: Tomcat setup and my first servlet</a></p>
<p><a title="" href="http://adndevblog.typepad.com/cloud_and_mobile/2015/08/become-a-java-ee-developer-part-ii-basic-restful-api-from-a-servlet.html" target="_self">Become a Java EE developer - Part II: Basic RESTful API from a servlet</a></p>
<p>The first thing I'm going to do is to convert my project to a Maven project (obviously a prerequisite is to have <a href="https://maven.apache.org/install.html" target="_self">Maven</a> installed): <em>Right-click your Eclipse project &gt; Configure &gt; Convert to Maven Project</em></p>
<p>This will generate a <em>pom.xml</em>&nbsp;file at the project root. Just copy all the Jersey dependencies (<em>&lt;dependencies&gt;</em> section) similarly to my pom.xml:</p>

<script src="https://gist.github.com/leefsmp/15e85dc5d5fc60af99a9.js"></script>


<p>Then <em>Right-click your Eclipse project &gt; Maven &gt; Update Project ...&nbsp;</em>This should download locally all the Jersey dependencies defined in the pom.xml, sweet! ...</p>
<p><a class="asset-img-link" style="display: inline;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb086bbe57970d-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b01bb086bbe57970d image-full img-responsive" title="Screen Shot 2015-09-01 at 6.30.08 PM" src="/assets/image_40b1c4.jpg" alt="Screen Shot 2015-09-01 at 6.30.08 PM" border="0" /></a></p>
<p>Make also sure those dependencies are part of the deployment package:</p>
<p><em>Right-click project &gt; Properties &gt; Java Build Path &gt; Order and Export &gt; Check Maven Dependencies</em></p>
<p>Then still from <em>Properties &gt; Deployment Assembly &gt; Java Build Path Entries &gt; Maven Dependencies</em></p>
<p>My Deployment package looks as follow:</p>
<p><a class="asset-img-link" style="display: inline;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c7c76f5e970b-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b01b7c7c76f5e970b image-full img-responsive" title="Screen Shot 2015-09-01 at 6.38.06 PM" src="/assets/image_f63879.jpg" alt="Screen Shot 2015-09-01 at 6.38.06 PM" border="0" /></a></p>
<p>Next we need to define a Servlet mapping, this is done by editing the web.xml. If you created a dynamic web project with module version 3.0, there are chances you don't have a web.xml, so <em>Right-click project &gt; Java EE Tools &gt; Generate Deployment Descriptor Stub</em></p>
<p><a class="asset-img-link" style="display: inline;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c7c76fe3970b-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b01b7c7c76fe3970b image-full img-responsive" title="Screen Shot 2015-08-31 at 12.04.25 PM" src="/assets/image_c76ef9.jpg" alt="Screen Shot 2015-08-31 at 12.04.25 PM" border="0" /></a></p>
<p>From there we define a url mapping that will be redirected to the Jersey server and also a provider package: in my case all the classes part of "com.autodesk.adn.viewanddata.api" package will be considered as resource for the Jersey server.</p>

<script src="https://gist.github.com/leefsmp/c055b68aecfd0d845d9c.js"></script>



<p>The last step is the implementation of the API itself and if you take a look at Part II, you will notice that it is significantly more straightforward when using Jersey as it handles parsing of the url and parameters nicely:</p>

<script src="https://gist.github.com/leefsmp/013f833d1f9538677423.js"></script>

<p>You can then run your project from Eclipse which should launch the Tomcat server and let you test the API using a Rest client as described in Part II.</p>
<p>The implementation still relies on a clunky hashmap, so next time I will take a look at how to use a mongoDB connector so it will fetch the records from a real database... stay tuned!</p>

<fieldset class="zemanta-related"><legend class="zemanta-related-title">Related articles</legend>
<div class="zemanta-article-ul zemanta-article-ul-image" style="margin: 0; padding: 0; overflow: hidden;">
<div class="zemanta-article-ul-li-image zemanta-article-ul-li" style="padding: 0; background: none; list-style: none; display: block; float: left; vertical-align: top; text-align: left; width: 84px; font-size: 11px; margin: 2px 10px 10px 2px;"><a style="box-shadow: 0px 0px 4px #999; padding: 2px; display: block; border-radius: 2px; text-decoration: none;" href="http://adndevblog.typepad.com/cloud_and_mobile/2015/08/become-a-java-ee-developer-part-ii-basic-restful-api-from-a-servlet.html" target="_blank"><img style="padding: 0; margin: 0; border: 0; display: block; width: 80px; max-width: 100%;" src="/assets/noimg_106_80_80.jpg" alt="" /></a><a style="display: block; overflow: hidden; text-decoration: none; line-height: 12pt; height: 80px; padding: 5px 2px 0 2px;" href="http://adndevblog.typepad.com/cloud_and_mobile/2015/08/become-a-java-ee-developer-part-ii-basic-restful-api-from-a-servlet.html" target="_blank">Become a Java EE developer - Part II: Basic RESTful API from a servlet</a></div>
</div>
</fieldset>
