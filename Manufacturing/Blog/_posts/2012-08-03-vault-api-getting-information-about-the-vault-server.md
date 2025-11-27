---
layout: "post"
title: "Vault API: Getting information about the Vault server"
date: "2012-08-03 04:16:33"
author: "Marat Mirgaleev"
categories:
  - "Marat Mirgaleev"
  - "Vault"
original_url: "https://adndevblog.typepad.com/manufacturing/2012/08/vault-api-getting-information-about-the-vault-server.html "
typepad_basename: "vault-api-getting-information-about-the-vault-server"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/manufacturing/marat-mirgaleev.html" target="_self">Marat Mirgaleev</a></p>  <p><strong>Issue</strong></p>  <p><em>Please, advise how I can get the Vault version and some other information from my code.</em></p>  <p><strong>Solution</strong></p>  <p>The Autodesk.Connectivity.WebServices.InformationService class is designed for this purpose. You may find useful this post about it in the <a href="http://justonesandzeros.typepad.com/blog/2010/11/the-information-service.html">Doug Redmond’s blog</a>.</p>  <p>Ok, let’s show how to work with it in a short sample.</p>  <p>I will use my MyVaultServiceManager class from <a href="http://adndevblog.typepad.com/manufacturing/2012/07/vault-api-users-and-roles-part-1.html">this post</a> to connect to the server. It will let me to write the sample as simple like this:</p>  <div style="font-family: courier new; background: white; color: black; font-size: 8pt">   <pre style="margin: 0px"><span style="line-height: 140%">&#160; </span><span style="line-height: 140%; color: green">// A sample how to work with the Vault InformationService</span></pre>

  <pre style="margin: 0px"><span style="line-height: 140%">&#160; </span><span style="line-height: 140%; color: green">//=================================================================</span></pre>

  <pre style="margin: 0px"><span style="line-height: 140%">&#160; </span><span style="line-height: 140%; color: blue">class</span><span style="line-height: 140%"> </span><span style="line-height: 140%; color: #2b91af">InfoServiceSample</span></pre>

  <pre style="margin: 0px"><span style="line-height: 140%">&#160; {</span></pre>

  <pre style="margin: 0px">&#160;</pre>

  <pre style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160; </span><span style="line-height: 140%; color: green">// Show some information about the current Vault server</span></pre>

  <pre style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160; </span><span style="line-height: 140%; color: green">//===============================================================</span></pre>

  <pre style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160; </span><span style="line-height: 140%; color: blue">public</span><span style="line-height: 140%"> </span><span style="line-height: 140%; color: blue">static</span><span style="line-height: 140%"> </span><span style="line-height: 140%; color: blue">void</span><span style="line-height: 140%"> ShowServerInfo()</span></pre>

  <pre style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160; {</span></pre>

  <pre style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160;&#160;&#160; </span><span style="line-height: 140%; color: green">// Establish a connection to the server</span></pre>

  <pre style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160;&#160;&#160; </span><span style="line-height: 140%; color: blue">using</span><span style="line-height: 140%"> (</span><span style="line-height: 140%; color: #2b91af">MyVaultServiceManager</span><span style="line-height: 140%"> mgr = </span><span style="line-height: 140%; color: blue">new</span><span style="line-height: 140%"> </span><span style="line-height: 140%; color: #2b91af">MyVaultServiceManager</span><span style="line-height: 140%">(</span></pre>

  <pre style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; </span><span style="line-height: 140%; color: #2b91af">MyVaultServiceManager</span><span style="line-height: 140%">.</span><span style="line-height: 140%; color: #2b91af">Mode</span><span style="line-height: 140%">.ReadOnly))</span></pre>

  <pre style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160;&#160;&#160; {</span></pre>

  <pre style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160;&#160;&#160;&#160;&#160; </span><span style="line-height: 140%; color: blue">try</span></pre>

  <pre style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160;&#160;&#160;&#160;&#160; {</span></pre>

  <pre style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; </span><span style="line-height: 140%; color: green">// Print the server name</span></pre>

  <pre style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; </span><span style="line-height: 140%; color: blue">string</span><span style="line-height: 140%"> svrInfo = </span><span style="line-height: 140%; color: #a31515">&quot;Server:\t&quot;</span><span style="line-height: 140%"> </span></pre>

  <pre style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; + mgr.Services.InformationService.GetServerName();</span></pre>

  <pre style="margin: 0px">&#160;</pre>

  <pre style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; </span><span style="line-height: 140%; color: green">// Print all products available</span></pre>

  <pre style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; </span><span style="line-height: 140%; color: #2b91af">Product</span><span style="line-height: 140%">[] PdSupported = </span></pre>

  <pre style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; mgr.Services.InformationService.GetSupportedProducts();</span></pre>

  <pre style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; svrInfo += </span><span style="line-height: 140%; color: #a31515">&quot;\nProduct List:&quot;</span></pre>

  <pre style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; + </span><span style="line-height: 140%; color: #a31515">&quot;\n-------------------------------------------&quot;</span><span style="line-height: 140%">;</span></pre>

  <pre style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; </span><span style="line-height: 140%; color: blue">foreach</span><span style="line-height: 140%"> (</span><span style="line-height: 140%; color: #2b91af">Product</span><span style="line-height: 140%"> Pd </span><span style="line-height: 140%; color: blue">in</span><span style="line-height: 140%"> PdSupported)</span></pre>

  <pre style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; {</span></pre>

  <pre style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; svrInfo += </span><span style="line-height: 140%; color: #a31515">&quot;\n&#160;&#160; Product Name:\t\t &quot;</span><span style="line-height: 140%"> + Pd.ProductName</span></pre>

  <pre style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; + </span><span style="line-height: 140%; color: #a31515">&quot;\n&#160;&#160; Product Version:\t\t &quot;</span><span style="line-height: 140%"> + Pd.ProductVersion</span></pre>

  <pre style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; + </span><span style="line-height: 140%; color: #a31515">&quot;\n&#160;&#160; Product Display Name:\t &quot;</span><span style="line-height: 140%"> + Pd.DisplayName</span></pre>

  <pre style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; + </span><span style="line-height: 140%; color: #a31515">&quot;\n&#160;&#160; - - - - - - - - - - - - - - - - - - - - - - -&quot;</span><span style="line-height: 140%">;</span></pre>

  <pre style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; }</span></pre>

  <pre style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; </span><span style="line-height: 140%; color: #2b91af">MessageBox</span><span style="line-height: 140%">.Show(svrInfo, </span><span style="line-height: 140%; color: #a31515">&quot;Server Info&quot;</span><span style="line-height: 140%">);</span></pre>

  <pre style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160;&#160;&#160;&#160;&#160; }</span></pre>

  <pre style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160;&#160;&#160;&#160;&#160; </span><span style="line-height: 140%; color: blue">catch</span><span style="line-height: 140%"> (System.</span><span style="line-height: 140%; color: #2b91af">Exception</span><span style="line-height: 140%"> err)</span></pre>

  <pre style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160;&#160;&#160;&#160;&#160; {</span></pre>

  <pre style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; </span><span style="line-height: 140%; color: #2b91af">MessageBox</span><span style="line-height: 140%">.Show(err.ToString());</span></pre>

  <pre style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160;&#160;&#160;&#160;&#160; }</span></pre>

  <pre style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160;&#160;&#160; }</span></pre>

  <pre style="margin: 0px">&#160;</pre>

  <pre style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160; } </span><span style="line-height: 140%; color: green">// ShowServerInfo()</span></pre>

  <pre style="margin: 0px"><span style="line-height: 140%">&#160; } </span><span style="line-height: 140%; color: green">// class InfoServiceSample</span></pre>
</div>

<p>On my system this code produces the following result:</p>

<p>&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; <a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01676905bc8b970b-pi"><img style="background-image: none; border-bottom: 0px; border-left: 0px; padding-left: 0px; padding-right: 0px; display: inline; border-top: 0px; border-right: 0px; padding-top: 0px" title="image" border="0" alt="image" src="/assets/image_b68393.jpg" width="383" height="418" /></a></p>
