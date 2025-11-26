---
layout: "post"
title: "Tips for the &ldquo;name&rdquo; attribute of connect element in the Fragment graph"
date: "2016-07-22 04:44:00"
author: "Cheng Xi Li"
categories:
  - "Cheng Xi Li"
  - "Maya"
  - "Shader"
original_url: "https://around-the-corner.typepad.com/adn/2016/07/tips-for-the-name-attribute-of-connect-element-in-the-fragment-graph.html "
typepad_basename: "tips-for-the-name-attribute-of-connect-element-in-the-fragment-graph"
typepad_status: "Publish"
---

<p>Today, we are going to look at a little bit inside of fragment graph&#39;s connect element. For the first two attributes, <strong>from</strong> and <strong>to</strong> are self-explanatory. Essentially this allows connecting from one fragment&#39;s output to another fragment&#39;s input. However, the “name” attribute is not well explained.</p>
<p>The fragment graph is designed like the dependency graph in Maya. Connecting different nodes and having outputs after running through a series of fragments you&#39;ll get the result. But unlike the dependency graph, the fragment graph cannot accept inputs having the same name. For example, if you have a fragment <strong>f1</strong> that you would like to create multiple instances and then connect to different fragments.</p>
<p>&#0160;</p>
<pre><code>&lt;connect from=&quot;f1.output&quot; to=&quot;f2.input&quot;/&gt;
&lt;connect from=&quot;f1.output&quot; to=&quot;f3.input&quot;/&gt;
</code></pre>
<p>&#0160;</p>
<p>In this case, you will have to rename the output of f1 with the “name” attribute so Maya will create multiple instances of f1 to connect them properly. Otherwise, only the first one will be connected.</p>
<p>&#0160;</p>
<pre><code>&lt;connect from=&quot;f1.output&quot; to=&quot;f2.input&quot; name=&quot;f2Input&quot;/&gt;
&lt;connect from=&quot;f1.output&quot; to=&quot;f3.input&quot; name=&quot;f3Input&quot;/&gt;
</code></pre>
<p>&#0160;</p>
<p>But there is also an exception here. When you are declaring an input struct for the <strong>alias</strong> output like this:</p>
<p>&#0160;</p>
<pre><code>&lt;properties&gt;
   &lt;struct name=\&quot;fileTexturePluginFragmentOutput\&quot; struct_name=\&quot;fileTexturePluginFragmentOutput\&quot; /&gt;
&lt;/properties&gt;

&lt;outputs&gt;
    &lt;alias name=\&quot;fileTexturePluginFragmentOutput\&quot; struct_name=\&quot;fileTexturePluginFragmentOutput\&quot; /&gt;
    &lt;float3 name=\&quot;outColor\&quot; /&gt;
    &lt;float name=\&quot;outAlpha\&quot; /&gt;
&lt;/outputs&gt;
</code></pre>
<p>&#0160;</p>
<p>The input(properties) “name” shouldn&#39;t be changed if you want to connect its output in your graph. After you&#39;ve changed its name, Maya will try to create another instance of the renamed structure. After the input name is changed, Maya can&#39;t find the alias with original name and so the fragment graph can&#39;t be created.</p>
<p>The conclusion for correctly using the “name” attribute of the connect element:</p>
<ol>
<li>Rename the input attribute when reusing the same fragment&#39;s input for multiple outputs.</li>
<li>Don&#39;t rename an alias output.</li>
</ol>
