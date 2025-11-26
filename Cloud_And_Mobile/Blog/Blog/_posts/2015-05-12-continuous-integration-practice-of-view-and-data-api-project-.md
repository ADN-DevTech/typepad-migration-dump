---
layout: "post"
title: "Continuous Integration Practice of View and Data API project "
date: "2015-05-12 03:18:19"
author: "Daniel Du"
categories:
  - "Cloud"
  - "Daniel Du"
  - "Javascript"
  - "View and Data API"
original_url: "https://adndevblog.typepad.com/cloud_and_mobile/2015/05/continuous-integration-practice-of-view-and-data-api-project-.html "
typepad_basename: "continuous-integration-practice-of-view-and-data-api-project-"
typepad_status: "Publish"
---

<p><a href="http://adndevblog.typepad.com/cloud_and_mobile/daniel-du.html">By Daniel Du</a></p>

<p>As you know we posted quite a few <a href="http://developer-autodesk.github.io">sample projects</a> to show off the power of View and Data API, and we also keep polishing them to make them better. I am investigating the automatic testing of our sample project, in this post, I will share what I learned with the continuous integration tool - Travis-CI.</p>

<p><a href="https://travis-ci.org/">Travis-CI</a> is a hosted continuous integration service. It is integrated with GitHub and offers first class support for variant languages. I tried it with JavaScript(node.js), it seems promising. </p>

<p>Firstly, I need to signup the Travis-CI service, since my source code is hosted on github, and Travic-CI allows us to login with github.com account. Once I logged in with github.com account, it asks me for authentication which orgranizations I authorize Travis-CI to access. </p>

<p>Now I have logged in Travis-CI, and my repository on github.com are listed.  Just click the checkbox ahead of my repo to enable integration with Travis-CI. 
<img src="/assets/image_a5b975.jpg" alt="Switch on repo" /></p>

<p>Next step, I need to add <code>.travis.yml</code> file into the root folder of project, and push it to github. The most important part in the content is the language, it tells Travis CI which language environment to select for your project. For my sample repo, it is a node.js repository, and as a start, I would like to test on different node.js versions:
    language: node_js
    node_js:
      + "0.12"
      + "0.11"
      + "0.10"</p>

<p>Now push the changes with <code>git push</code> to github repository, it will trigger the building and testing process. If everything is OK, I will get a green button, saying that my build is passed. And you can also see the build result:
<img src="/assets/image_7b2e10.jpg" alt="Build Result" /></p>

<p>Now continuous integration has been set up, pretty easy. Whenever a new push to github, the building and testing will be executed automatically by Travis-CI. To nortify users, I put a status icon to the readme of reposity, just editing readme.md of my repository as bellow:</p>

<pre><code>[![build status](https://api.travis-ci.org/duchangyu/workflow-node.js-view.and.data.api.png)](https://api.travis-ci.org/duchangyu/workflow-node.js-view.and.data.api)
</code></pre>

<p>Here is how it looks:
<img src="/assets/image_558cd6.jpg" alt="Switch on repo" /></p>

<p>But as you wonder, what kind of testing did Travis-CI do? Actually nothing so far. I did not put any test code to my repo yet. Currently Travis-CI only check the <code>npm install</code> on different node.js versions. When it goes to <code>npm test</code>, no test case is found. 
<img src="/assets/image_7df565.jpg" alt="" /></p>

<p>So next step is to create some test cases to verify whether the repository works or not, a lot stuffs to learn :)</p>
