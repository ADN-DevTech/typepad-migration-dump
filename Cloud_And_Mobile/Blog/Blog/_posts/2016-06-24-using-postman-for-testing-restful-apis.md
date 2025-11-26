---
layout: "post"
title: "Using Postman for testing RESTful API's"
date: "2016-06-24 10:32:46"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "Web Development"
original_url: "https://adndevblog.typepad.com/cloud_and_mobile/2016/06/using-postman-for-testing-restful-apis.html "
typepad_basename: "using-postman-for-testing-restful-apis"
typepad_status: "Publish"
---

<p>By&nbsp;<a href="http://adndevblog.typepad.com/cloud_and_mobile/adam-nagy.html" target="_self">Adam Nagy</a></p>
<p><strong>Postman</strong> is a really nice free tool to test <strong>RESTful</strong> <strong>API</strong>'s with. You can find it here: <a href="https://www.getpostman.com/">https://www.getpostman.com/</a></p>
<p>Many times you need to use the result of a request as the input parameter of the next request. E.g. I authenticate myself using a 2 legged <strong>OAuth</strong> call and then I would need to keep using the received <strong>access token</strong> in&nbsp;further&nbsp;calls.</p>
<p>In <strong>Postman</strong> you can create <strong>Environments</strong> and can also add <a href="https://www.getpostman.com/docs/environments">Environment Variables</a> to them which then can be used as the parameters of your queries.</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb09164c7c970d-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false" style="display: inline;"><img class="asset  asset-image at-xid-6a0167607c2431970b01bb09164c7c970d img-responsive" title="EnvironmentVariables" src="/assets/image_267a42.jpg" alt="EnvironmentVariables" /></a></p>
<p>You can reference those variables from inside your queries using double curly braces: <strong>{{</strong>MyEnvironmentVariable<strong>}}</strong></p>
<p>Inside your queries you can also add some scripts to modify these variables: <a href="https://www.getpostman.com/docs/writing_tests">https://www.getpostman.com/docs/writing_tests</a><br />This way you can store the result of a query and then reuse that in your next query.</p>
<p>My query is already using some environment variables:</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d1fca645970c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false" style="display: inline;"><img class="asset  asset-image at-xid-6a0167607c2431970b01b8d1fca645970c img-responsive" title="Authenticate" src="/assets/image_84cca1.jpg" alt="Authenticate" /></a></p>
<p>Then I also added a script to store the result:</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d1fca664970c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false" style="display: inline;"><img class="asset  asset-image at-xid-6a0167607c2431970b01b8d1fca664970c img-responsive" title="Authenticatescript" src="/assets/image_d0a276.jpg" alt="Authenticatescript" /></a></p>
<p>Now I can use the "<strong>AccessToken</strong>" variable in my further requests, like bucket creation:</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c872e28f970b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false" style="display: inline;"><img class="asset  asset-image at-xid-6a0167607c2431970b01b7c872e28f970b img-responsive" title="BucketCreation" src="/assets/image_ba724e.jpg" alt="BucketCreation" /></a></p>
<p>Nice, ey? :)</p>
