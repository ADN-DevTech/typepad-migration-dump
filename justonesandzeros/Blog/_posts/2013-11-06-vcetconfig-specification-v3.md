---
layout: "post"
title: ".vcet.config Specification (v3)"
date: "2013-11-06 08:06:23"
author: "Doug Redmond"
categories:
  - "Tips and Tricks"
  - "Vault"
original_url: "https://justonesandzeros.typepad.com/blog/2013/11/vcetconfig-specification-v3.html "
typepad_basename: "vcetconfig-specification-v3"
typepad_status: "Publish"
---

<p><img alt="" src="/assets/Vault4.png" />    <br /><img alt="" src="/assets/TipsAndTricks4.png" /></p>
<p>A couple weeks ago, I blogged about the <a href="http://justonesandzeros.typepad.com/blog/2013/10/the-loader-framework.html" target="_blank">Loader Framework</a>.&#0160; One of the key components of that framework is the .vcet.config.&#0160; It’s basically the file that tells the Loader what to load and how to load it.&#0160; In the knowledgebase of the SDK is the specification for the XML syntax. It’s the v3 version of schema which was introduced in Vault 2014.</p>
<p>Features include:</p>
<ul>
<li>Ability to provide name/value pairs for your extension. </li>
<li>Different ways to locate referenced assemblies. </li>
<li>Ability to load an extension only in an x86 and x64 environment. </li>
<li>Different formats for identifying a .NET type. </li>
</ul>
<p>&#0160;</p>
<p>Below is the full spec, copied from the SDK documentation.</p>
<hr noshade="noshade" style="color: #d09219;" />
<h2>XML Tags:</h2>
<p><strong>&#0160;</strong></p>
<p><strong>&lt;configuration&gt;      <br /></strong>This is the top-level tag.&#0160; There should be one and only one of these tags.     <br />Required element:&#0160; 1 &lt;connectivity.ExtensionSettings3&gt;</p>
<p>&#0160;</p>
<p><strong>&lt;connectivity.ExtensionSettings3&gt;      <br /></strong>This tag is a collection of extensions.&#0160; It is directly below &lt;configuration&gt;.&#0160; There should be one and only one of these tags.     <br />Required elements: 1 to many &lt;extension&gt;</p>
<p>&#0160;</p>
<p><strong>&lt;extension&gt;      <br /></strong>This tag defines a component to be loaded at runtime.     <br />Required attributes:     <br />&#0160; - <strong>interface</strong> - The &quot;type name&quot; of the interface implemented by the extension.     <br />&#0160; - <strong>type</strong> - The &quot;type name&quot; of the class that implements the extension interface.&#0160; The assembly must be in the same folder as the .vcet.config file.     </p>
<p>Optional attribute:     <br />- <strong>runtime</strong> - The &quot;runtime requirement&quot; of the extension.&#0160; The default value is &quot;Any&quot;     <br />Optional elements: 0 to many &lt;setting&gt;, 0 to many &lt;resolveFolder&gt;</p>
<p>&#0160;</p>
<p><strong>&lt;setting&gt;      <br /></strong>This tag provides meta-data for the extension in the form of a key/value pair.     <br />Required attributes:     <br />&#0160; - <strong>key</strong> - The name of the meta data.&#0160; The value must be unique within an &lt;extension&gt;     <br />&#0160; - <strong>value</strong> - The value of the meta data.</p>
<p>&#0160;</p>
<p><strong>&lt;resolveFolder&gt;      <br /></strong>This tag provides locations to look for referenced assemblies.&#0160; This is useful if an extension references other assemblies that are not in the same folder as the extension.     <br />Required attribute:&#0160; <br />&#0160; - <strong>lookup</strong> - This is a &quot;lookup type&quot;, which determines how folders are located.     <br />Optional attribute:     <br />&#0160; - <strong>appendToPath</strong> - This is a Boolean value that determines of the resolve folder should be appended to the PATH environment variable.&#0160; This is useful when loading Win32 and COM components.&#0160; The default value is &quot;false&quot;.     </p>
<p>Lookup-specific attributes:&#0160; These attributes may be required depending on the lookup type.     <br />&#0160; - <strong>path</strong> - The path relative to the extension assembly.&#0160; Used only if the lookup value is &quot;RelativeToExtension&quot;.&#0160; <br />&#0160; - <strong>regkey</strong> - The registry key name.&#0160; Used only if the lookup value is &quot;Registry&quot;.     <br />&#0160; - <strong>regname</strong> - The name of the registry key.&#0160; Used only if the lookup value is &quot;Registry&quot;.</p>
<hr noshade="noshade" style="color: #d09219;" />
<h2>Value types:</h2>
<p><strong>     <br />Lookup Type       <br /></strong>This value specifies the algorithm to use when locating paths on disk.&#0160; <br />There are 3 possible values:     <br />&#0160; - <strong>ExplorerClient</strong> - The resolve path is the directory where the Vault client EXE lives.&#0160; If multiple clients are installed, it will the the highest tier product.&#0160; For example, the Vault Professional path will be used over the Vault Basic path.&#0160; If multiple client versions are installed, only the one matching the Vault framework are used.&#0160; For example the Vault 2014 framework will only resolve paths for Vault 2014 clients.     <br />&#0160; - <strong>Registry</strong> - The resolve path is located by looking up a registry key.&#0160; The registry value can be a folder path or a file path.&#0160; In the case of a file path, the parent folder becomes the resolve path.&#0160; The &#39;regkey&#39; and &#39;regname&#39; attributes are required for this lookup type.&#0160;&#0160; <br />- <strong>RelativeToExtension</strong> - The resolve path is defined relative to the assembly path.</p>
<p>&#0160;</p>
<p><strong>Runtime Requirement      <br /></strong>This value specifies the runtime environment required for the extension to run.&#0160; <br />There are 3 possible values:     <br />&#0160; - <strong>x86</strong> - The extension can only be loaded if the running app is 32 bit.     <br />&#0160; - <strong>x64</strong> - The extension can only be loaded if the running app is 64 bit.     <br />&#0160; - <strong>Any</strong> - The extension can be loaded regardless of whether the running app is 32 or 64 bit.&#0160; This is the default option.</p>
<p>&#0160;</p>
<p><strong>Type Name      <br /></strong>This is the identifier for a .NET type.&#0160; It is based off of the .NET syntax.&#0160; There are two possible formats. </p>
<ul>
<li><strong>Simple format</strong>:&#0160; <em>FullClassName</em>, <em>AssemblyName </em></li>
<li><strong>Complex format</strong>:&#0160; <em>FullClassName</em>, <em>AssemblyName</em>, Version=<em>VersionNumber</em>, Culture=<em>CultureName</em>, PublicKeyToken=<em>PublicKeyTokenValue</em> </li>
</ul>
<p>If the complex format is used, then the version, culture and public key token must match the assembly in order for the extension to load.</p>
<hr noshade="noshade" style="color: #d09219;" />
