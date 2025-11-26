---
layout: "post"
title: "On Spaces in Help and Renaming a Parameter"
date: "2020-09-21 05:00:00"
author: "Jeremy Tammik"
categories:
  - "AI"
  - "Parameters"
  - "Ribbon"
  - "User Interface"
original_url: "https://thebuildingcoder.typepad.com/blog/2020/09/on-spaces-in-help-and-renaming-a-parameter.html "
typepad_basename: "on-spaces-in-help-and-renaming-a-parameter"
typepad_status: "Publish"
---

<p>I am busy as ever in
the <a href="http://forums.autodesk.com/t5/revit-api-forum/bd-p/160">Revit API discussion forum</a>.
Today, let's highlight a couple of items that were not discussed there:</p>

<ul>
<li><a href="#2">ContextualHelp with space</a></li>
<li><a href="#3">On renaming a shared parameter</a></li>
<li><a href="#4">Build a minimal neural network from scratch</a></li>
</ul>

<h4><a name="2"></a> ContextualHelp With Space</h4>

<p>A colleague looked into an issue with <code>ContextualHelp</code> and wrote the following based on own tests and the info in the development ticket:</p>

<p>Using <code>ContextualHelp</code> you can provide a URL for any button that should be shown when the user clicks <code>F1</code> while a ribbon button tooltip is displayed.</p>

<p>If the URL has a space character in the path, you usually encode it either as <code>%20</code> or <code>+</code>.</p>

<p>That may cause an issue for <code>ContextualHelp</code>.</p>

<p>Let’s use this URL as an example:</p>

<ul>
<li><code>postman-echo.com/get?text%20with%20space</code></li>
</ul>

<p><center></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e168978833026bde94e984200c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e168978833026bde94e984200c img-responsive" style="width: 500px; display: block; margin-left: auto; margin-right: auto;" alt="Contextual help test redirect" title="Contextual help test redirect" src="/assets/image_ecaade.jpg" /></a><br /></p>

<p></center></p>

<p>First problem: if you use <code>https</code>, then <em>https://accounts.autodesk.com/oAuth/OAuthRedirect</em> will be called with a redirect to the actual URL you specified for <code>F1</code> using the <code>ContextualHelp</code> object.</p>

<p>This will confuse and stall users.</p>

<p>Secondly: if, on top of that, the URL also contains a space (<code>%20</code> or <code>+</code>), then you’ll get stuck on that webpage:</p>

<pre>https://accounts.autodesk.com/oAuth/OAuthRedirect?oauth_consumer_key=1c27193f-af5e-4e7c-9847-06cd5c3c30ae&oauth_nonce=cd819e65f0ac476099e9c795a22c05a7&oauth_redirect_url=https%3A%2F%2Fpostman-echo.com%2Fget%3Ftext%2520with%2520space&oauth_signature=xl7aBEcj5lI%2FX28ozkvQ%2Ba163qg%3D&oauth_signature_method=HMAC-SHA1&oauth_timestamp=1600289858&oauth_token=bskZ8nJbcvBt%2FTyQvS%2FeImjP6pc%3D&oauth_version=1.0</pre>

<p>It displays the following error message:</p>

<pre>xoauth_problem=parameter_rejected&xoauth_parameters_absent=oauth_redirect_url&oauth_error_message=Invalid%20value%20for%20parameter%3Aoauth_redirect_url</pre>

<p><center></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e1689788330263e9678e15200b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e1689788330263e9678e15200b image-full img-responsive" alt="Contextual help test redirect" title="Contextual help test redirect" src="/assets/image_6dcd05.jpg" border="0" style="display: block; margin-left: auto; margin-right: auto;" /></a><br /></p>

<p></center></p>

<p>You can reproduce this issue with any sample using the following code:</p>

<pre class="code">
&nbsp;&nbsp;<span style="color:#2b91af;">ContextualHelp</span>&nbsp;contextualHelp&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">ContextualHelp</span>(
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">ContextualHelpType</span>.Url,
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#a31515;">&quot;https://postman-echo.com/get?text%20with%20space&quot;</span>&nbsp;);

&nbsp;&nbsp;pushButton.SetContextualHelp(contextualHelp);
</pre>

<p>Both of the above-mentioned problems can be avoided by passing the link as <code>http</code> instead of <code>https</code>.</p>

<p>If the given website redirects from <code>http</code> to <code>https</code>, that won’t cause a problem:</p>

<pre class="code">
  <span style="color:#2b91af;">ContextualHelp</span>&nbsp;contextualHelp&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">ContextualHelp</span>(
  &nbsp;&nbsp;<span style="color:#2b91af;">ContextualHelpType</span>.Url,
  &nbsp;&nbsp;<span style="color:#a31515;">&quot;http://postman-echo.com/get?text%20with%20space&quot;</span>&nbsp;);

  pushButton.SetContextualHelp(contextualHelp);
</pre>

<p>I hope you find this useful as well.</p>

<h4><a name="3"></a> On Renaming a Shared Parameter</h4>

<p>Jay Merlan very kindly shared some insights on renaming shared parameters in his recent article
on <a href="https://opendefinery.com/blog/revit-shared-parameters-can-be-renamed">True or False: Shared Parameters Can Be Renamed</a>.</p>

<p>As I pointed out in <a href="https://opendefinery.com/blog/revit-shared-parameters-can-be-renamed/#comment-5072357426">my comment</a> on that post, the Revit development team discussed Jay's suggestions internally and say:</p>

<p>My perception is that we don't support renaming a shared parameter after loading into a project or a family.</p>

<p>We are discussing support for such a feature of 'unified parameters' in a future release of Revit.</p>

<p>Jay's article above explains very well why Revit customers want to rename them, and that is really helpful.</p>

<p>The steps above work well to generate a new family with the new name, and the references, like formulas within the family, are updated to the new name. But, when I try to load it to an existing project with old name parameters, it will be renamed back. That makes sense and that is how this workaround works. So, it only works for the projects and models without using the old parameters.</p>

<p>The fundamental problem with the current Revit implementation of shared params is the fundamental question: what does it mean to 'rename' a parameter?</p>

<p>Once the parameter definition <code>.txt</code> file has been consumed by Revit, you can rename a parameter in the <code>.txt</code> file all you like, regardless of what Revit considers correct. But, all docs where this file has already been consumed will have no idea of the new name. Which will clearly result in a mess, as the same parameter will appear under different names spread all over various projects and families.</p>

<p>In cases when two incarnations of the same parameter need to be merged (because it is the same parameter, after all), one incarnation will magically change its name &ndash; like in the Load Family case.</p>

<p>As far as we can tell, a problem like this can only be solved via some 'central source of truth', like a cloud-something.</p>

<h4><a name="4"></a> Build a Minimal Neural Network from Scratch</h4>

<p>I browsed through this tutorial
on <a href="https://www.freecodecamp.org/news/how-to-build-a-neural-network-with-pytorch">how to build a neural network from scratch with PyTorch</a> and
found it quite illuminating and inspiring.
I wish I had more time to dabble with this kind of stuff!</p>

<blockquote>
  <p>Going under the hood of neural networks to learn how to build one from the ground up...
  The MNIST data set contains handwritten digits from zero to nine with their corresponding labels...
  Simply feed the neural network the images of the digits and their corresponding labels which tell the neural network that this is a three or seven...
  We just write the code to index out only the images with a label of three or seven. Thus, we get a data set of threes and sevens...</p>
  
  <ul>
  <li><a href="https://github.com/bipinKrishnan/ML_from_scratch/blob/master/neural_network_pytorch.ipynb">Code on GitHub</a></li>
  <li><a href="https://colab.research.google.com/github/bipinKrishnan/ML_from_scratch/blob/master/neural_network_pytorch.ipynb">Play with it on Colab</a></li>
  </ul>
</blockquote>
