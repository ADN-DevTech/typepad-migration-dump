---
layout: "post"
title: "Deploy Node.Js viewer sample to Heroku"
date: "2015-05-07 01:53:49"
author: "Daniel Du"
categories:
  - "Cloud"
  - "Daniel Du"
  - "Javascript"
  - "Server"
  - "View and Data API"
  - "Web Server"
original_url: "https://adndevblog.typepad.com/cloud_and_mobile/2015/05/deploy-nodejs-viewer-sample-to-heroku.html "
typepad_basename: "deploy-nodejs-viewer-sample-to-heroku"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/cloud_and_mobile/daniel-du.html">Daniel Du</a></p>

<p>I have been evaluating different cloud enviroment to host our viewer samples, in this post I will share my experience with <a href="http://www.heroku.com">Heroku</a>, which is a Platform as a Service(PaaS) provider. The advantage of PaaS like Heroku is simplicity, we do not need to care about the infrastructure underneath, and we do not need to care about the load balance, scaling .etc. On the contrary, it gives us limited control with the infrastructure, and it only provides pre-defined hosting environments. Just in case something bad happened on the stack which our application is running, we have nothing to do it, what we can do is waiting for Heroku to fix it. Nevertheless, it is a good solution for a quick deployment and verification, and it is good enough for a simple sample application like what I did. </p>

<p>I am using the <a href="https://github.com/duchangyu/workflow-node.js-view.and.data.api">workflow-node.js-view.and.data.api</a> sample which is a forked repo and I did some minor changes.</p>

<h3>Develop and test on local machine</h3>

<p>For view and Data API, we need a consumer key and secret key pair to do the authentication, protecting your data from being peeked by unauthorized hackers. We do not want to expose our secret keys to public right? We need to keep the keys on server side. And I am not comfortable to save the keys in source code, as I am pushing the source code to public repo of github all the time, sometime I may push the keys to public accidentally and it is not easy to clear the history, so I would prefer to set the keys at run time. In node.js, I can use <b>process.env.variablename</b> and pass the values to it when running the node server.</p>

<pre><code>router.get('/token', function (req, res) {
    var params = {
        client_id:  process.env.ClientId , 
        client_secret:  process.env.ClientSecret, 
        grant_type: 'client_credentials'
    }

    request.post(
        process.env.BaseUrl + '/authentication/v1/authenticate',
        { form: params },

        function (error, response, body) {
            if (!error &amp;&amp; response.statusCode == 200) {
                res.send(body);
            }
        });
});
</code></pre>

<p>For local testing, I can pass the parameters like "key1=value1 key2=value2 node server.js" when starting the node server. Of cause it is too long to input it all them time when running, so I put it into a shell script, name it as run.sh. It can be bat file for windows.</p>

<pre><code>ClientId=replace_with_your_consumer_key \
ClientSecret=replace_with_your_secret_key \
BaseUrl=https://developer.api.autodesk.com \
node server.js
</code></pre>

<p>On Mac/Linux, I need to make it execuable by :</p>

<pre><code>chmod +x run.sh
</code></pre>

<p>When I test my node server at local side, I can simplily run the script:</p>

<pre><code>./run.sh
</code></pre>

<p>With that I can working on my node.js viewer sample and test it on my local machine.</p>

<h3>Deploy to heroku</h3>

<p>With that, I am OK to deploy my web site to Heroku. As we said, heroku is pretty easy to use to host a website, and it provides up to 5 free apps.</p>

<p>Firstly, sign up on <a href="https://www.heroku.com/">heroku.com</a> for a free account, and then download and install the <a href="https://toolbelt.heroku.com/">Heroku Toolbelt</a> , you can learn more about the <a href="https://devcenter.heroku.com/categories/command-line">Heroku Command Line Interface</a> if you want to know detailed instruction of the usage.</p>

<p>Next, log in to your Heroku account and follow the prompts to create a new SSH public key.</p>

<pre><code>$ heroku login
Enter your Heroku credentials.
Email: &lt;your heroku account here&gt;
Password (typing will be hidden): 
Authentication successful.
</code></pre>

<p>Create a new Heroku app through command line, it will create an app with random name if you do not give one in command line, it also add a git remote to Heroku so that you can deploy your code by git push.</p>

<pre><code>$ heroku create
Creating quiet-shore-6917... done, stack is cedar-14
https://quiet-shore-6917.herokuapp.com/ | https://git.heroku.com/quiet-shore-6917.git
Git remote heroku added
</code></pre>

<p>Deploy your website to Heroku using Git once your are ready. Heroku will detect your app and setup the corresponding hosting environment, and then host it for you. </p>

<pre><code>git push heroku master
</code></pre>

<p>Similarly, Heroku lets you externalise configuration - storing data such as encryption keys or external resource addresses in <a href="https://devcenter.heroku.com/articles/config-vars">config vars</a>. At runtime, config vars are exposed as environment variables to the application. Heroku will automatically setup the environment based on the contents of the .env file in your local directory, so you can create a <code>.env</code> with the key pair: </p>

<pre><code>ClientId=replace_with_your_consumer_key 
ClientSecret=replace_with_your_secret_key 
BaseUrl=https://developer.api.autodesk.com 
</code></pre>

<p>Or set the config vars manualy by command:</p>

<pre><code>heroku config:set ClientId=replace_with_your_consumer_key 
heroku config:set ClientSecret=replace_with_your_secret_key 
heroku config:set BaseUrl=https://developer.api.autodesk.com 
</code></pre>

<p>Ensure that at least one instance of the app is running(do this only once):</p>

<pre><code>$ heroku ps:scale web=1
</code></pre>

<p>Once the deployment is done, you can open the website by following command line, it launches your website in your default web browser. You can note down the URL if you'd like to switch to another browser. "heroku open" uses HTTPS by default, you need to use HTTP instead for now due to some known issue of viewer, for example, browse to <b>http://</b>quiet-shore-6917.heroku.com instead of https://quiet-shore-6917.heroku.com</p>

<pre><code>heroku open
</code></pre>

<p>If everything works fine, your website is hosted on cloud now. You can keep working on the project on your local machine, making some changes, testing on local machine and commit when the test is passed, and deploy your changes again to heroku by git push.</p>

<pre><code>git push heroku master
</code></pre>

<p>Ok, seems easy and simple, right? </p>
