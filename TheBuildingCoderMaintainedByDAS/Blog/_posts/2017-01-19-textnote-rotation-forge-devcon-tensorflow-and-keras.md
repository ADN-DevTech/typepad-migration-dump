---
layout: "post"
title: "TextNote Rotation, DevCon, TensorFlow and Keras"
date: "2017-01-19 05:00:00"
author: "Jeremy Tammik"
categories:
  - "AI"
  - "AU"
  - "Deep Learning"
  - "Element Creation"
  - "Forge"
  - "Installation"
  - "Mac"
  - "News"
  - "Python"
  - "Security"
original_url: "https://thebuildingcoder.typepad.com/blog/2017/01/textnote-rotation-forge-devcon-tensorflow-and-keras.html "
typepad_basename: "textnote-rotation-forge-devcon-tensorflow-and-keras"
typepad_status: "Publish"
---

<p>The Forge DevCon developer conference has been happily united with Autodesk University, text note rotation is easy, and I continued my deep learning exploration for implementing a Revit API question answering system:</p>

<ul>
<li><a href="#2">Forge DevCon at AU</a></li>
<li><a href="#3">Setting <code>TextNote</code> rotation</a></li>
<li><a href="#4">TensorFlow and Keras</a></li>
<li><a href="#5">Updating restricted Python packages</a></li>
<li><a href="#6">Rules of machine learning</a></li>
</ul>

<h4><a name="2"></a>Forge DevCon at AU</h4>

<p>A match made in heaven:
the <a href="https://forge.autodesk.com/DevCon">Forge DevCon</a> developer
conference has been happily united
with <a href="http://au.autodesk.com/las-vegas">Autodesk University</a></p>

<p>A lot of synergy is gained connecting the two.</p>

<p><center></p>

<p><a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e16897883301bb096ebd8b970d-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883301bb096ebd8b970d img-responsive" alt="Forge DevCon at AU" title="Forge DevCon at AU" src="/assets/image_9a29b5.jpg" border="0" /></a><br /></p>

<p></center></p>

<p>The last DevCon in 2016 was held in San Francisco and was a great success.</p>

<p>We plan to make it bigger and better still this year, and moving it to Las Vegas for 2017 will help.</p>

<p>It will take place on November 13-14,
i.e., <a href="http://adndevblog.typepad.com/cloud_and_mobile/2017/01/forge-devcon-change-of-date-and-venue.html">Monday and Tuesday of the Autodesk University week</a>.</p>

<p>In previous years, these were the days on which we hosted the DevDay and DevLab conference and open house support hackathon for application developers.</p>

<p>This is a good thing.</p>

<p>Somewhat selfishly, it means less travel for those of us coming across from Europe.</p>

<p>It will also make AU a more compelling, developer-centric event.</p>

<p>Now nobody has to choose between DevCon and DevDays + AU. It’s all together in one place.</p>

<h4><a name="3"></a>Setting TextNote Rotation</h4>

<p>Rotating a text note is very easy, both during creation and when modifying existing elements.</p>

<p>One little complication for existing add-ins is introduced by fact that the approach changed with
the <a href="http://thebuildingcoder.typepad.com/blog/2015/04/whats-new-in-the-revit-2016-api.html#4.02"><code>TextNote</code> API overhaul in Revit 2016</a>:</p>

<p><strong>Question:</strong> In Revit 2015, I used the method:</p>

<pre class="code">
  <span style="color:#2b91af;">TextNote</span>&nbsp;text&nbsp;=&nbsp;doc.Create.NewTextNote(&nbsp;
  &nbsp;&nbsp;view,&nbsp;origin,&nbsp;baseVec,&nbsp;upVec,&nbsp;lineWidth,
  &nbsp;&nbsp;<span style="color:#2b91af;">TextAlignFlags</span>.TEF_ALIGN_CENTER&nbsp;
  &nbsp;&nbsp;|&nbsp;<span style="color:#2b91af;">TextAlignFlags</span>.TEF_ALIGN_MIDDLE,
  &nbsp;&nbsp;strText&nbsp;);
</pre>

<p>for creating a text note.</p>

<p>As this method has been replaced with <code>TextNote.Create</code>, I can create a text note using:</p>

<pre class="code">
  <span style="color:#2b91af;">TextNote</span>&nbsp;text&nbsp;=&nbsp;<span style="color:#2b91af;">TextNote</span>.Create(&nbsp;doc,&nbsp;
  &nbsp;&nbsp;view.Id,&nbsp;&nbsp;origin,&nbsp;strText,&nbsp;texttypeID&nbsp;);
</pre>

<p>But how do I set its rotation when creating it?</p>

<p>Also, how do I change the rotation of existing text notes?</p>

<p><strong>Answer:</strong> To rotate the <code>TextNote</code> as it is placed, one of the overloads for <code>TextNote.Create</code> uses <code>TextNoteOptions</code> and its <code>Rotation</code> option:</p>

<pre class="code">
  <span style="color:#2b91af;">TextNoteOptions</span>&nbsp;tno&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">TextNoteOptions</span>();
  tno.Rotation&nbsp;=&nbsp;0.5&nbsp;*&nbsp;<span style="color:#2b91af;">Math</span>.PI;&nbsp;<span style="color:green;">//&nbsp;in&nbsp;Radians</span>
  tno.TypeId&nbsp;=&nbsp;texttypeID;&nbsp;<span style="color:green;">//&nbsp;need&nbsp;to&nbsp;include&nbsp;text&nbsp;type</span>

  <span style="color:#2b91af;">TextNote</span>&nbsp;text&nbsp;=&nbsp;<span style="color:#2b91af;">TextNote</span>.Create(&nbsp;doc,&nbsp;
  &nbsp;&nbsp;view.Id,&nbsp;origin,&nbsp;strText,&nbsp;tno&nbsp;);
</pre>

<p><strong>Response:</strong> This is how I managed to create the rotated text aligned with existing curve based elements:</p>

<pre class="code">
  <span style="color:green;">//&nbsp;get&nbsp;the&nbsp;curve&nbsp;of&nbsp;the&nbsp;element</span>
  <span style="color:#2b91af;">Curve</span>&nbsp;curve&nbsp;=&nbsp;(&nbsp;(<span style="color:#2b91af;">LocationCurve</span>)&nbsp;elem.Location&nbsp;)
  &nbsp;&nbsp;.Curve;

  <span style="color:green;">//&nbsp;get&nbsp;start&nbsp;and&nbsp;end&nbsp;point&nbsp;of&nbsp;pipe</span>
  <span style="color:#2b91af;">XYZ</span>&nbsp;PipeEnd&nbsp;=&nbsp;curve.GetEndPoint(&nbsp;1&nbsp;);
  <span style="color:#2b91af;">XYZ</span>&nbsp;PipeStart&nbsp;=&nbsp;curve.GetEndPoint(&nbsp;0&nbsp;);

  <span style="color:green;">//&nbsp;get&nbsp;vector&nbsp;representing&nbsp;the&nbsp;pipe</span>
  <span style="color:#2b91af;">XYZ</span>&nbsp;v&nbsp;=&nbsp;PipeEnd&nbsp;-&nbsp;PipeStart;

  <span style="color:blue;">double</span>&nbsp;angle&nbsp;=&nbsp;<span style="color:#2b91af;">Math</span>.Atan2(&nbsp;v.Y,&nbsp;v.X&nbsp;);

  <span style="color:blue;">var</span>&nbsp;testOptions&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">TextNoteOptions</span>()
  {
  &nbsp;&nbsp;HorizontalAlignment&nbsp;=&nbsp;<span style="color:#2b91af;">HorizontalTextAlignment</span>.Center,
  &nbsp;&nbsp;Rotation&nbsp;=&nbsp;angle,
  &nbsp;&nbsp;TypeId&nbsp;=&nbsp;texttypeID
  };

  <span style="color:blue;">var</span>&nbsp;text&nbsp;=&nbsp;<span style="color:#2b91af;">TextNote</span>.Create(&nbsp;doc,&nbsp;view.Id,
  &nbsp;&nbsp;origin,&nbsp;strText,&nbsp;testOptions&nbsp;);
</pre>

<p><center></p>

<p><a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e16897883301b7c8cb9ada970b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883301b7c8cb9ada970b image-full img-responsive" alt="Rotate text" title="Rotate text" src="/assets/image_cb51eb.jpg" border="0" /></a><br /></p>

<p></center></p>

<h4><a name="4"></a>TensorFlow and Keras</h4>

<p>Continuing my superficial browsing of various open source possibilities on which to base a question answering system for the Revit API add-in programming, I now bumped
into <a href="https://en.wikipedia.org/wiki/TensorFlow">TensorFlow</a>
(<a href="https://www.tensorflow.org/">web site</a>,
<a href="https://github.com/tensorflow/tensorflow">GitHub repo</a>).</p>

<p>It converts the learning matter to vectors and computes similarities, learning best matches by assigning weight using various algorithms.</p>

<p>Here are a bunch of interesting TensorFlow tutorial and other related texts that I would all like to work through in greater depth anon:</p>

<ul>
<li><a href="https://www.tensorflow.org/tutorials/mnist/beginners/index">MNIST tutorial for beginners</a> and <a href="https://www.tensorflow.org/tutorials/mnist/pros/index">advanced</a></li>
<li><a href="https://www.tensorflow.org/tutorials/word2vec/">Vector Representations of Words</a></li>
<li><a href="https://www.tensorflow.org/tutorials/recurrent/">Recurrent Neural Networks</a></li>
<li><a href="https://www.tensorflow.org/tutorials/seq2seq/">Sequence-to-Sequence Models</a></li>
<li><a href="http://kevinhughes.ca/blog/tensor-kart">TensorKart: self-driving MarioKart with TensorFlow</a></li>
</ul>

<p>They also led to further papers and the <a href="https://keras.io">Keras</a> system built on top of it:</p>

<ul>
<li><a href="https://www.tensorflow.org/tutorials/syntaxnet/">SyntaxNet</a> is a neural-network Natural Language Processing framework for TensorFlow.</li>
<li><a href="https://cs224d.stanford.edu/reports/StrohMathur.pdf">Question Answering Using Deep Learning</a> &ndash; neural network variants are becoming the dominant architecture for many NLP tasks. This project applies several deep learning approaches to question answering, with a focus on the bAbI dataset, including TensorFlow.</li>
<li><a href="https://keras.io/">Keras: Deep Learning library for Theano and TensorFlow</a> &ndash; paper on <a href="http://benjaminbolte.com/blog/2016/keras-language-modeling.html">Deep Language Modelling for Question Answering using Keras</a> with <a href="https://github.com/fchollet/keras/blob/master/examples/babi_rnn.py">GitHub repo</a></li>
<li><a href="http://smerity.com/articles/2015/keras_qa.html">Question answering on the Facebook bAbi dataset using recurrent neural networks and 175 lines of Python + Keras</a></li>
<li><a href="https://research.fb.com/downloads/babi/">The Facebook bAbI tasks</a></li>
</ul>

<p>I decided to install these packages, which led to an interesting problem that took a while to sort out and seems worthwhile documenting for future reference.</p>

<h4><a name="5"></a>Updating Restricted Python Packages</h4>

<p>TensorFlow and Keras are Python based.</p>

<p>Installation is very straightforward and easy.</p>

<p>TensorFlow requires several other base packages: <code>six</code>, <code>funcsigs</code>, <code>pbr</code>, <code>mock</code>, <code>numpy</code>, <code>protobuf</code>.</p>

<p>Unfortunately, on my system, some of these were out of date and the system refused to allow me to update them.</p>

<p>For instance, calling <code>$ pip install tensorflow</code> led to a conflict due to the existence of prior versions of <code>six</code> and <code>numpy</code>.</p>

<p>After some research, I determined that I could resolve the issues by simply deleting the associated files.</p>

<p>Unfortunately, this is not permitted by the system, as described in the question
on <a href="http://superuser.com/questions/279235/why-does-chown-report-operation-not-permitted-on-os-x">why <code>chown</code> reports <code>operation not permitted</code> on OS X</a>.</p>

<p>The short description of the solution for this is given there:</p>

<blockquote>
  <p>System Integrity Protection (rootless) ... boot into Recovery Mode (Cmd-R), run <code>csrutil disable</code>, then restart; to reenable, use <code>csrutil enable</code>.</p>
</blockquote>

<p>It is explained in greater detail in the StackOverflow question
on <a href="http://stackoverflow.com/questions/30768087/restricted-folder-files-in-os-x-el-capitan">restricted folder and files in OS X El Capitan</a>:</p>

<p>To temporarily disable SIP (System Integrity Protection):</p>

<ul>
<li>Reboot</li>
<li>As soon as you hear the "Mac sound" on the grey screen, press Cmd+R to enter Recovery mode</li>
<li>Open Utilities &gt; Terminal</li>
<li>Run the command <code>csrutil disable</code></li>
<li>Reboot, you'll land in the normal OS with SIP disabled</li>
<li>Do all the changes you'd like to do</li>
<li>Reboot again</li>
<li>As soon as you hear the "Mac sound" on the grey screen, press Cmd+R to enter Recovery mode</li>
<li>Enable SIP with <code>csrutil enable</code></li>
<li>Reboot again</li>
<li>Done</li>
</ul>

<p>On my system, the problematic packages live in the folder</p>

<ul>
<li><code>System/Library/Frameworks/Python.framework/Versions/2.7/Extras/lib/python</code></li>
</ul>

<p>You can examine their restricted status using <code>ls</code>:</p>

<pre>
&#36; &#108;s -lhdO num*
drwxr-xr-x  36 root  wheel  restricted             1.2K May 25  2016 numpy
-rw-r--r--   1 root  wheel  restricted,compressed  1.6K Aug  1  2015 numpy-1.8.0rc1-py2.7.egg-info
</pre>

<p>By temporarily disabling SIP, I was finally able to move them out of the way and continue normal installation:</p>

<pre>
&#36; sudo -H pip install tensorflow
</pre>

<p>With TensorFlow in place, I successfully ran the standard Python install in the <code>keras</code> folder and immediately executed one of its interesting examples:</p>

<pre>
/a/src/deep_learning &#36; cd kears
/a/src/deep_learning/keras &#36; sudo python setup.py install
/a/src/deep_learning/keras &#36; cd examples
/a/src/deep_learning/keras/examples &#36; python babi_rnn.py
</pre>

<p>Keras is up and running now.</p>

<p>What shall I do next?</p>

<p>Here are two descriptions of successful experiments implementing minimal question answering systems with it that I am taking a deeper look at:</p>

<ul>
<li><a href="http://benjaminbolte.com/blog/2016/keras-language-modeling.html">Deep Language Modelling for Question Answering using Keras</a>
(<a href="/a/doc/deep_learning/keras/Deep_Language_Modeling_for_Question_Answering_using_Keras.pdf">^</a>),
<a href="https://github.com/codekansas/keras-language-modeling">GitHub repo</a></li>
<li><a href="https://cs224d.stanford.edu/reports/StrohMathur.pdf">Question Answering Using Deep Learning</a>
(<a href="/a/doc/deep_learning/keras/Question_Answering_Using_Deep_Learning_by_Stroh_Mathur.pdf">^</a>),
<a href="https://github.com/priyank87/memn2n">GitHub repo</a></li>
</ul>

<p>Time for me to get going putting together some Revit API related tests?</p>

<h4><a name="6"></a>Rules of Machine Learning</h4>

<p>By the way, before doing anything else, I and everybody else interested in machine learning ought to read and ponder
Martin Zinkevich's 43 <a href="http://martin.zinkevich.org/rules_of_ml/rules_of_ml.pdf">Rules of Machine Learning</a>,
split up into useful consecutive phases:</p>

<ul>
<li>Before Machine Learning</li>
<li>Phase I: Your First Pipeline
<ul>
<li>Monitoring</li>
<li>Your First Objective</li>
</ul></li>
<li>Phase II: Feature Engineering
<ul>
<li>Human Analysis of the System</li>
<li>Training-­Serving Skew</li>
</ul></li>
<li>Phase III: Slowed Growth, Optimization Refinement, and Complex Models</li>
</ul>

<p>To quote Martin:</p>

<p>The document is arranged in four parts:</p>

<ul>
<li>The first part should help you understand whether the time is right for building a machine learning system.</li>
<li>The second part is about deploying your first pipeline.</li>
<li>The third part is about launching and iterating while adding new features to your pipeline, how to evaluate models and training-­serving skew.</li>
<li>The final part is about what to do when you reach a plateau.</li>
<li>Afterwards, there is a list of related work and an appendix with some background on the systems commonly used as examples in this document.</li>
</ul>

<p>It is strongly geared towards ranking.</p>
