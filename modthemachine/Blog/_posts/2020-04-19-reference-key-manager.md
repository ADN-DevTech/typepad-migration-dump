---
layout: "post"
title: "Reference Key Manager"
date: "2020-04-19 14:36:39"
author: "Adam Nagy"
categories:
  - "Adam"
  - "Inventor"
original_url: "https://modthemachine.typepad.com/my_weblog/2020/04/reference-key-manager.html "
typepad_basename: "reference-key-manager"
typepad_status: "Publish"
---

<p><a href="https://modthemachine.typepad.com/my_weblog/about-the-author.html">Brian</a> has a very detailed <a href="https://modthemachine.typepad.com/my_weblog/2015/09/understanding-reference-keys-in-inventor.html">article on Reference Keys</a> (exposed through the <a href="http://help.autodesk.com/view/INVNTOR/2020/ENU/?guid=GUID-7ACB5EC8-04F5-4E03-9DB1-B295D8563270">ReferenceKeyManager</a> object) and as I was playing with them I thought it could be useful to have a tool for that.</p>
<p>So I created an <strong>add-in</strong> that lets you create <strong>Key Contexts</strong> and <strong>Reference Keys</strong> for the various objects, and <strong>binding</strong> them back to objects in the model.<br />When switching between documents, it also stores/loads data to/from <strong>json</strong> files named based on the <strong>Document</strong>&#39;s <strong>DisplayName</strong> plus &quot;<strong>.json</strong>&quot; as extension, .e.g. &quot;Boxes.iam.json&quot;&#0160;<br />They will be saved in the same folder as the <strong>add-in</strong>&#39;s <strong>dll</strong> and their content will look like this:</p>
<pre>{
  &quot;contexts&quot;: {
    &quot;Default&quot;: {
      &quot;index&quot;: 0,
      &quot;keys&quot;: [
        &quot;AgEBAAQAAAADAAAA&quot;
      ]
    },
    &quot;AgAAAAEAAAA=&quot;: {
      &quot;index&quot;: 1,
      &quot;keys&quot;: [
        &quot;AgEBAAQAAAACAAAA&quot;,
        &quot;AgEBAAQAAAADAAAA&quot;,
        &quot;AgEBAAQAAAAGAAAA&quot;
      ]
    }
  }
}</pre>
<p>Here is the add-in&#39;s form:</p>
<p><a class="asset-img-link" href="https://modthemachine.typepad.com/.a/6a00e553fcbfc688340240a521ae04200b-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="ReferenceKeyManager" class="asset  asset-image at-xid-6a00e553fcbfc688340240a521ae04200b img-responsive" src="/assets/image_335583.jpg" title="ReferenceKeyManager" /></a></p>
<p>As I was looking for <strong>icons</strong> I ran into the <strong>Visual Studio Image Library</strong> which can be downloaded from the <strong>Microsoft</strong> website:<br /><a href="https://www.microsoft.com/en-us/download/confirmation.aspx?id=35825">https://www.microsoft.com/en-us/download/confirmation.aspx?id=35825</a></p>
<p>Here is the source code of the project: <a href="https://github.com/adamenagy/ReferenceKeyManager">https://github.com/adamenagy/ReferenceKeyManager</a></p>
<p>I modified the <strong>Project</strong> settings so that the <strong>Output</strong> folder will always be the correct location for loading the <strong>add-in</strong> in <strong>Inventor</strong>:<br /><strong>%AppData%</strong>\Autodesk\ApplicationPlugins\ReferenceKeyManager</p>
<p>The way to do that is open the <a href="https://github.com/adamenagy/ReferenceKeyManager/blob/master/ReferenceKeyManager/ReferenceKeyManager.csproj"><strong>csproj</strong></a> file in a <strong>text editor</strong> and then you can use <strong>special variables</strong> like <strong>$(AppData)</strong> in the <strong>&lt;OutputPath&gt;</strong>:</p>
<pre>&lt;OutputPath&gt;$(AppData)\Autodesk\ApplicationPlugins\ReferenceKeyManager\&lt;/OutputPath&gt;</pre>
<p>-Adam</p>
