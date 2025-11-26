---
layout: "post"
title: "Writing viewer extensions in ES6"
date: "2015-08-06 05:09:43"
author: "Philippe Leefsma"
categories:
  - "HTML5"
  - "Javascript"
  - "Philippe Leefsma"
  - "View and Data API"
  - "WebGL"
original_url: "https://adndevblog.typepad.com/cloud_and_mobile/2015/08/writing-viewer-extensions-in-es6.html "
typepad_basename: "writing-viewer-extensions-in-es6"
typepad_status: "Publish"
---

<p>By&nbsp;<a href="http://adndevblog.typepad.com/cloud_and_mobile/philippe-leefsma.html">Philippe Leefsma</a></p>
<p><strong>ECMAScript 6&nbsp;</strong>(a.k.a. ECMAScript 2015) is the next standard for JavaScript as it has been recently approved by the <a href="http://www.ecma-international.org/ecma-262/6.0/" target="_self">ECMA organisation</a>. While its features get more and more support in modern browsers and runtimes (see the&nbsp;<a href="http://kangax.github.io/compat-table/es6/">Kangax compatibility table</a>), to deploy your ES6 code you still need to compile - more accurately transpile - it to ES5.1, the current version of JavaScript.</p>
<p>As far as transpilers are concerned, there are two names that pop up on top the pile: <a href="https://babeljs.io/docs/learn-es2015/" target="_self">Babel</a>&nbsp;and <a href="https://github.com/google/traceur-compiler" target="_self">Traceur</a> from Google. However Babel seems to be the one getting the most focus from the web community.&nbsp;</p>
<p>Basically you don't use a transpiler manually but you invoke it through your web development tools as you build your website from your source. Once configured, which is a pretty easy thing to do, you can start writing ES6 code and leverage its features without the need to worry about browser compatibility. Here is neat a blog post that takes you through the steps in order to use ES6 code in your workflow using Webpack:&nbsp;<a href="http://www.2ality.com/2015/04/webpack-es6.html" target="_self">Writing client-side ES6 with webpack</a>. And here is the github page of the <a href="https://github.com/babel/babel-loader" target="_self">babel-loader</a> that the post refers to.</p>
<p>If you are using a modern web IDE, there are good chances that it also supports ES6 syntax and can integrate tools to transpile your code on the fly, this is the case of WebStorm and <a href="http://blog.jetbrains.com/webstorm/2015/05/ecmascript-6-in-webstorm-transpiling/" target="_self">here</a> are more details on how to set it up.</p>
<p>As far as I'm concerned, I'm using the babel-loader + webpack as part of the build process, but I also added a feature that allows my server logic to transpile script files or code on the fly using the <a href="https://babeljs.io/docs/usage/api/">babel API</a>. Here is how my REST API looks like to give you an idea, pretty easy:</p>


<pre style="line-height: 100%;font-family:monospace;background-color:#ffffff; border-width:0.01mm; border-color:#000000; border-style:solid;padding:4px;font-size:9pt;"><span style="color:#800000;background-color:#f0f0f0;"> 1 </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">/////////////////////////////////////////////////////////////////////
</span><span style="color:#800000;background-color:#f0f0f0;"> 2 </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">// transpiles extensions script files
</span><span style="color:#800000;background-color:#f0f0f0;"> 3 </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">//
</span><span style="color:#800000;background-color:#f0f0f0;"> 4 </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">/////////////////////////////////////////////////////////////////////
</span><span style="color:#800000;background-color:#f0f0f0;"> 5 </span><span style="background-color:#ffffff;">router.get(</span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">'/transpile/:extensionId/:fileId'</span><span style="background-color:#ffffff;">,
</span><span style="color:#800000;background-color:#f0f0f0;"> 6 </span><span style="background-color:#ffffff;">  </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">function</span><span style="background-color:#ffffff;"> (req, res) {
</span><span style="color:#800000;background-color:#f0f0f0;"> 7 
 8 </span><span style="background-color:#ffffff;">    </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">var</span><span style="background-color:#ffffff;"> filepath = path.join(__dirname,
</span><span style="color:#800000;background-color:#f0f0f0;"> 9 </span><span style="background-color:#ffffff;">      </span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">'../../www/uploads/extensions/'</span><span style="background-color:#ffffff;"> +
</span><span style="color:#800000;background-color:#f0f0f0;">10 </span><span style="background-color:#ffffff;">      req.params.extensionId + </span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">'/'</span><span style="background-color:#ffffff;"> +
</span><span style="color:#800000;background-color:#f0f0f0;">11 </span><span style="background-color:#ffffff;">      req.params.fileId);
</span><span style="color:#800000;background-color:#f0f0f0;">12 
13 </span><span style="background-color:#ffffff;">    </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">var</span><span style="background-color:#ffffff;"> options = {};
</span><span style="color:#800000;background-color:#f0f0f0;">14 
15 </span><span style="background-color:#ffffff;">    </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">//result; // =&gt; { code, map, ast }
</span><span style="color:#800000;background-color:#f0f0f0;">16 </span><span style="background-color:#ffffff;">    babel.transformFile(filepath,
</span><span style="color:#800000;background-color:#f0f0f0;">17 </span><span style="background-color:#ffffff;">      options, </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">function</span><span style="background-color:#ffffff;"> (err, result) {
</span><span style="color:#800000;background-color:#f0f0f0;">18 
19 </span><span style="background-color:#ffffff;">        </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">if</span><span style="background-color:#ffffff;"> (err) {
</span><span style="color:#800000;background-color:#f0f0f0;">20 </span><span style="background-color:#ffffff;">          console.log(err);
</span><span style="color:#800000;background-color:#f0f0f0;">21 </span><span style="background-color:#ffffff;">          res.status(</span><span style="color:#0000ff;background-color:#ffffff;">404</span><span style="background-color:#ffffff;">);
</span><span style="color:#800000;background-color:#f0f0f0;">22 </span><span style="background-color:#ffffff;">        }
</span><span style="color:#800000;background-color:#f0f0f0;">23 </span><span style="background-color:#ffffff;">        </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">else</span><span style="background-color:#ffffff;"> {
</span><span style="color:#800000;background-color:#f0f0f0;">24 
25 </span><span style="background-color:#ffffff;">          res.send(result.code);
</span><span style="color:#800000;background-color:#f0f0f0;">26 </span><span style="background-color:#ffffff;">        }
</span><span style="color:#800000;background-color:#f0f0f0;">27 </span><span style="background-color:#ffffff;">    });
</span><span style="color:#800000;background-color:#f0f0f0;">28 </span><span style="background-color:#ffffff;">  });
</span><span style="color:#800000;background-color:#f0f0f0;">29 
30 </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">/////////////////////////////////////////////////////////////////////
</span><span style="color:#800000;background-color:#f0f0f0;">31 </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">// transpiles code on the fly
</span><span style="color:#800000;background-color:#f0f0f0;">32 </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">//
</span><span style="color:#800000;background-color:#f0f0f0;">33 </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">/////////////////////////////////////////////////////////////////////
</span><span style="color:#800000;background-color:#f0f0f0;">34 </span><span style="background-color:#ffffff;">router.post(</span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">'/transpile'</span><span style="background-color:#ffffff;">,
</span><span style="color:#800000;background-color:#f0f0f0;">35 </span><span style="background-color:#ffffff;">  </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">function</span><span style="background-color:#ffffff;"> (req, res) {
</span><span style="color:#800000;background-color:#f0f0f0;">36 
37 </span><span style="background-color:#ffffff;">    </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">var</span><span style="background-color:#ffffff;"> payload = req.body;
</span><span style="color:#800000;background-color:#f0f0f0;">38 
39 </span><span style="background-color:#ffffff;">    </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">var</span><span style="background-color:#ffffff;"> options = payload.options || {};
</span><span style="color:#800000;background-color:#f0f0f0;">40 
41 </span><span style="background-color:#ffffff;">    </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">// =&gt; { code, map, ast }
</span><span style="color:#800000;background-color:#f0f0f0;">42 </span><span style="background-color:#ffffff;">    </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">var</span><span style="background-color:#ffffff;"> result = babel.transform(
</span><span style="color:#800000;background-color:#f0f0f0;">43 </span><span style="background-color:#ffffff;">      payload.code,
</span><span style="color:#800000;background-color:#f0f0f0;">44 </span><span style="background-color:#ffffff;">      options);
</span><span style="color:#800000;background-color:#f0f0f0;">45 
46 </span><span style="background-color:#ffffff;">    </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">var</span><span style="background-color:#ffffff;"> response = {
</span><span style="color:#800000;background-color:#f0f0f0;">47 </span><span style="background-color:#ffffff;">      code: result.code
</span><span style="color:#800000;background-color:#f0f0f0;">48 </span><span style="background-color:#ffffff;">    }
</span><span style="color:#800000;background-color:#f0f0f0;">49 
50 </span><span style="background-color:#ffffff;">    res.send(response);
</span><span style="color:#800000;background-color:#f0f0f0;">51 </span><span style="background-color:#ffffff;">  });</span></pre>


One of the nice feature of ES6 is the ability to use the <a href="http://javascriptplayground.com/blog/2014/07/introduction-to-es6-classes-tutorial/"><em>class</em></a> keyword with which object oriented programmers are familiar with and <em>"extends"</em> to derive your class from a base class. It's obviously interesting to take a look at how a viewer extension may look like when written using a class, so here is a basic extension that will show an alert message and change the background:
<br>
<br>
<script src="https://gist.github.com/leefsmp/71f6bf6e9b20678ee7f4.js"></script>
  
Finally, if you are curious, you can cut and paste that ES6 code into the <a href="https://babeljs.io/repl/">babel online tool</a> to see how this code gets transpiled...
