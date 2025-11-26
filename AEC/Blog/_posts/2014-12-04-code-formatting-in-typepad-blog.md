---
layout: "post"
title: "Code formatting in TypePad blog"
date: "2014-12-04 22:57:04"
author: "Aaron Lu"
categories:
  - "Aaron Lu"
original_url: "https://adndevblog.typepad.com/aec/2014/12/code-formatting-in-typepad-blog.html "
typepad_basename: "code-formatting-in-typepad-blog"
typepad_status: "Publish"
---

<p><small>By <a href="http://adndevblog.typepad.com/aec/aaron-lu.html" target="_self">Aaron Lu</a></small></p>
<p>It is a pain to insert code block in typepad blog, so I yahoo!ed (can&#39;t google in China :(), did not find much useful informaiton, fortunately the next day I searched again, yahoo! brought me to Jeremy&#39;s blog! There is a nice <a href="http://thebuildingcoder.typepad.com/blog/2013/05/source-code-formatting-and-google-prettyfier.html" target="_self" title="Source Code Formatting and Google Prettifier">article </a>describing how he handles it using VS plugin <a href="https://visualstudiogallery.msdn.microsoft.com/98fef791-eb65-4cdf-bf84-077b98c234cf?SRC=VSIDE" target="_self" title="Copy As HTML">Copy As HTML</a> for .net code and <a href="https://code.google.com/p/google-code-prettify/" target="_self" title="Google code prettify">Google code prettify</a> for Javascript.</p>
<p>Thanks, Jeremy!</p>
<p><strong>&quot;Copy As HTML&quot;</strong></p>
<p><strong><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d0a1f559970c-pi" style="display: inline;"> <img alt="VS CopyAsHtml" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b8d0a1f559970c image-full img-responsive" src="/assets/image_889295.jpg" title="VS CopyAsHtml" /></a><br /> </strong></p>
<p><strong>Result:</strong></p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c717f432970b-pi" style="display: inline;"> <img alt="VS CopyAsHtml Result" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b7c717f432970b image-full img-responsive" src="/assets/image_390842.jpg" title="VS CopyAsHtml Result" /></a></p>
<p>Since typepad&#39;s post display width is fixed with 520px, we have to wrap code text if it is too long (more than 70 characters). But how should we know a line of code exceeds 70 characters quickly? I found a VS plugin for that:</p>
<p><a href="https://visualstudiogallery.msdn.microsoft.com/d0d33361-18e2-46c0-8ff2-4adea1e34fef" target="_self" title="Productivity Power Tools">Productivity Power Tools</a> helps to add a column guide mark at any specific column:</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c717e83a970b-pi" style="display: inline;"> <img alt="ProductivityTool" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b7c717e83a970b image-full img-responsive" src="/assets/image_789745.jpg" title="ProductivityTool" /></a></p>
<p>If you think this tool is too heavy, you can modify VS&#39;s registry, <a href="stackoverflow.com/questions/84209/adding-a-guideline-to-the-editor-in-visual-studio" target="_self" title="Adding a guideline to the editor in Visual Studio">this </a>is how.</p>
<p>&#0160;</p>
<p><strong>&quot;Google code prettify&quot; </strong></p>
<p>Jeremy used the js file in typepad rather than from google site, but I found it still connects to googlecode.com to retrieve some .css files which is extremely slow in China, so I replaced most of google related URLs in &quot;run_prettify.js&quot; and uploaded it with some .css files to typepad,</p>
<p>I also changed the comment color in the prettify.css file from <span style="color: #800;">.com{color:#800}</span> to <span style="color: #008000;">.com{color:#008000}</span> because it is more consistent with visual studio.</p>
<p>now the new script is here: http://adndevblog.typepad.com/files/run_prettify-3.js.</p>
<p>(I wonder whether there is any better free CDN site of which the speed is also good for China.)</p>
<p>so the code html node is this:</p>
<script src="https://adndevblog.typepad.com/files/run_prettify-3.js" type="text/javascript"></script>
<pre class="prettyprint">&lt;script type=&quot;text/javascript&quot; 
src=&quot;http://adndevblog.typepad.com/files/run_prettify-3.js&quot;/&gt;&lt;/script&gt;
&lt;pre class=&quot;prettyprint&quot;&gt;
    Your code here
&lt;/pre&gt;
</pre>
<p><strong>Result:</strong></p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb07bce842970d-pi" style="display: inline;"> <img alt="Google Code Prettify Result" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01bb07bce842970d image-full img-responsive" src="/assets/image_992452.jpg" title="Google Code Prettify Result" /></a></p>
<p><a href="https://code.google.com/p/google-code-prettify/downloads/list" target="_self" title="Google Code Prettify">&#0160;google code prettify source download link</a></p>
