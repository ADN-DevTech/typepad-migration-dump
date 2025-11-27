---
layout: "post"
title: "Vault API: Users and Roles, part 1"
date: "2012-07-31 11:37:23"
author: "Marat Mirgaleev"
categories:
  - "Marat Mirgaleev"
  - "Vault"
original_url: "https://adndevblog.typepad.com/manufacturing/2012/07/vault-api-users-and-roles-part-1.html "
typepad_basename: "vault-api-users-and-roles-part-1"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/manufacturing/marat-mirgaleev.html" target="_self">Marat Mirgaleev</a></p>  <p><strong>Issue</strong></p>  <p><em>I would like to get a list of the users registered at the Vault server. Could you provide a code sample for this task, please?</em></p>  <p><strong>Solution</strong></p>  <p>Autodesk.Connectivity.WebServices.AdminService is the class which manipulates users, groups, roles etc.</p>  <p>To access the list of the users, we will need to connect to the Vault server and read the WebServiceManager.AdminService property first. I will create a utility class to maintain the connection (see the MyVaultServiceManager class in the code below).</p>  <p>Next, there is the AdminService.GetAllUsers() method that returns an array of objects of the User class. From the User class we can get information about the user like his/her name, ID, email address etc.</p>  <p>Here is a sample. The AdminSample.PrintUserInfo() method is what you need to call from your program, for example, when the user presses a button:</p>  <div style="font-family: courier new; background: white; color: black; font-size: 8pt">   <pre style="margin: 0px"><span style="line-height: 140%; color: blue">private</span><span style="line-height: 140%"> </span><span style="line-height: 140%; color: blue">void</span><span style="line-height: 140%"> printUserInfo_button_Click(</span><span style="line-height: 140%; color: blue">object</span><span style="line-height: 140%"> sender, </span><span style="line-height: 140%; color: #2b91af">EventArgs</span><span style="line-height: 140%"> e)</span></pre>

  <pre style="margin: 0px"><span style="line-height: 140%">{</span></pre>

  <pre style="margin: 0px"><span style="line-height: 140%">&#160; MyVault.</span><span style="line-height: 140%; color: #2b91af">AdminSample</span><span style="line-height: 140%">.PrintUserInfo();</span></pre>

  <pre style="margin: 0px"><span style="line-height: 140%">}</span></pre>
</div>

<p>This is the program output:</p>

<p align="center">&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; <a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b017743d02820970d-pi"><img style="border-right-width: 0px; padding-left: 0px; padding-right: 0px; display: inline; border-top-width: 0px; border-bottom-width: 0px; border-left-width: 0px; padding-top: 0px" title="image" border="0" alt="image" src="/assets/image_ed6b9a.jpg" width="323" height="360" /></a></p>

<p>And this is the code itself:</p>

<div style="font-family: courier new; background: white; color: black; font-size: 8pt">
  <pre style="margin: 0px"><span style="line-height: 140%; color: blue">using</span><span style="line-height: 140%"> System.IO;</span></pre>

  <pre style="margin: 0px"><span style="line-height: 140%; color: blue">using</span><span style="line-height: 140%"> System.Windows.Forms;</span></pre>

  <pre style="margin: 0px">&#160;</pre>

  <pre style="margin: 0px"><span style="line-height: 140%; color: blue">using</span><span style="line-height: 140%"> Autodesk.Connectivity.WebServices;</span></pre>

  <pre style="margin: 0px"><span style="line-height: 140%; color: blue">using</span><span style="line-height: 140%"> Autodesk.Connectivity.WebServicesTools;</span></pre>

  <pre style="margin: 0px">&#160;</pre>

  <pre style="margin: 0px"><span style="line-height: 140%; color: blue">namespace</span><span style="line-height: 140%"> MyVault</span></pre>

  <pre style="margin: 0px"><span style="line-height: 140%">{</span></pre>

  <pre style="margin: 0px">&#160;</pre>

  <pre style="margin: 0px"><span style="line-height: 140%">&#160; </span><span style="line-height: 140%; color: green">// A wrapper for the Vault server connection.</span></pre>

  <pre style="margin: 0px"><span style="line-height: 140%">&#160; </span><span style="line-height: 140%; color: green">// Place it in a 'using' block for automatic call of Dispose(),</span></pre>

  <pre style="margin: 0px"><span style="line-height: 140%">&#160; </span><span style="line-height: 140%; color: green">//&#160;&#160; which insures that it logs out when we are done.</span></pre>

  <pre style="margin: 0px"><span style="line-height: 140%">&#160; </span><span style="line-height: 140%; color: green">//=================================================================</span></pre>

  <pre style="margin: 0px"><span style="line-height: 140%">&#160; </span><span style="line-height: 140%; color: blue">class</span><span style="line-height: 140%"> </span><span style="line-height: 140%; color: #2b91af">MyVaultServiceManager</span><span style="line-height: 140%"> : System.</span><span style="line-height: 140%; color: #2b91af">IDisposable</span></pre>

  <pre style="margin: 0px"><span style="line-height: 140%">&#160; {</span></pre>

  <pre style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160; </span><span style="line-height: 140%; color: green">// We will incapsulate the WebServiceManager here.</span></pre>

  <pre style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160; </span><span style="line-height: 140%; color: green">// The WebServiceManager will be used for our Vault server calls.</span></pre>

  <pre style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160; </span><span style="line-height: 140%; color: blue">private</span><span style="line-height: 140%"> </span><span style="line-height: 140%; color: #2b91af">WebServiceManager</span><span style="line-height: 140%"> _svcManager = </span><span style="line-height: 140%; color: blue">null</span><span style="line-height: 140%">;</span></pre>

  <pre style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160; </span><span style="line-height: 140%; color: blue">public</span><span style="line-height: 140%"> </span><span style="line-height: 140%; color: #2b91af">WebServiceManager</span><span style="line-height: 140%"> Services</span></pre>

  <pre style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160;&#160;&#160; { </span><span style="line-height: 140%; color: blue">get</span><span style="line-height: 140%"> { </span><span style="line-height: 140%; color: blue">return</span><span style="line-height: 140%"> _svcManager; } }</span></pre>

  <pre style="margin: 0px">&#160;</pre>

  <pre style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160; </span><span style="line-height: 140%; color: blue">public</span><span style="line-height: 140%"> </span><span style="line-height: 140%; color: blue">enum</span><span style="line-height: 140%"> </span><span style="line-height: 140%; color: #2b91af">Mode</span><span style="line-height: 140%"> { ReadOnly, ReadWrite };</span></pre>

  <pre style="margin: 0px">&#160;</pre>

  <pre style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160; </span><span style="line-height: 140%; color: green">// Preventing usage of the default constructor - made it private</span></pre>

  <pre style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160; </span><span style="line-height: 140%; color: blue">private</span><span style="line-height: 140%"> MyVaultServiceManager() { }</span></pre>

  <pre style="margin: 0px">&#160;</pre>

  <pre style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160; </span><span style="line-height: 140%; color: green">// Constructor.</span></pre>

  <pre style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160; </span><span style="line-height: 140%; color: green">// Parameter: - Log in as read-only, which doesn't consume </span></pre>

  <pre style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160; </span><span style="line-height: 140%; color: green">//&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; a license.</span></pre>

  <pre style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160; </span><span style="line-height: 140%; color: green">//===============================================================</span></pre>

  <pre style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160; </span><span style="line-height: 140%; color: blue">public</span><span style="line-height: 140%"> MyVaultServiceManager(</span><span style="line-height: 140%; color: #2b91af">Mode</span><span style="line-height: 140%"> i_ReadWriteMode)</span></pre>

  <pre style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160; {</span></pre>

  <pre style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160;&#160;&#160; </span><span style="line-height: 140%; color: #2b91af">UserPasswordCredentials</span><span style="line-height: 140%"> login = </span><span style="line-height: 140%; color: blue">new</span><span style="line-height: 140%"> </span><span style="line-height: 140%; color: #2b91af">UserPasswordCredentials</span><span style="line-height: 140%">(</span></pre>

  <pre style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; </span><span style="line-height: 140%; color: #a31515">&quot;localhost&quot;</span><span style="line-height: 140%">, </span><span style="line-height: 140%; color: #a31515">&quot;Vault&quot;</span><span style="line-height: 140%">, </span><span style="line-height: 140%; color: #a31515">&quot;Administrator&quot;</span><span style="line-height: 140%">, </span><span style="line-height: 140%; color: #a31515">&quot;&quot;</span><span style="line-height: 140%">,</span></pre>

  <pre style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; (i_ReadWriteMode == </span><span style="line-height: 140%; color: #2b91af">Mode</span><span style="line-height: 140%">.ReadOnly) );</span></pre>

  <pre style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; </span><span style="line-height: 140%; color: green">// Yeah, we shouldn't hardcode the credentials here,</span></pre>

  <pre style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; </span><span style="line-height: 140%; color: green">// but this is just a sample</span></pre>

  <pre style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160;&#160;&#160; _svcManager = </span><span style="line-height: 140%; color: blue">new</span><span style="line-height: 140%"> </span><span style="line-height: 140%; color: #2b91af">WebServiceManager</span><span style="line-height: 140%">(login);</span></pre>

  <pre style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160; }</span></pre>

  <pre style="margin: 0px">&#160;</pre>

  <pre style="margin: 0px">&#160;</pre>

  <pre style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160; </span><span style="line-height: 140%; color: blue">void</span><span style="line-height: 140%"> System.</span><span style="line-height: 140%; color: #2b91af">IDisposable</span><span style="line-height: 140%">.Dispose()</span></pre>

  <pre style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160; {</span></pre>

  <pre style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160;&#160;&#160; _svcManager.Dispose();</span></pre>

  <pre style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160; }</span></pre>

  <pre style="margin: 0px">&#160;</pre>

  <pre style="margin: 0px"><span style="line-height: 140%">&#160; } </span><span style="line-height: 140%; color: green">// class MyVaultServiceManager</span></pre>

  <pre style="margin: 0px">&#160;</pre>

  <pre style="margin: 0px">&#160;</pre>

  <pre style="margin: 0px"><span style="line-height: 140%">&#160; </span><span style="line-height: 140%; color: green">// In this sample we will try different things related to the</span></pre>

  <pre style="margin: 0px"><span style="line-height: 140%">&#160; </span><span style="line-height: 140%; color: green">//&#160;&#160;&#160; Vault administration.</span></pre>

  <pre style="margin: 0px"><span style="line-height: 140%">&#160; </span><span style="line-height: 140%; color: green">//=================================================================</span></pre>

  <pre style="margin: 0px"><span style="line-height: 140%">&#160; </span><span style="line-height: 140%; color: blue">class</span><span style="line-height: 140%"> </span><span style="line-height: 140%; color: #2b91af">AdminSample</span></pre>

  <pre style="margin: 0px"><span style="line-height: 140%">&#160; {</span></pre>

  <pre style="margin: 0px">&#160;</pre>

  <pre style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160; </span><span style="line-height: 140%; color: green">// Lists all the users along with their roles and the vaults they </span></pre>

  <pre style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160; </span><span style="line-height: 140%; color: green">//&#160;&#160; have access to.</span></pre>

  <pre style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160; </span><span style="line-height: 140%; color: green">//===============================================================</span></pre>

  <pre style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160; </span><span style="line-height: 140%; color: blue">public</span><span style="line-height: 140%"> </span><span style="line-height: 140%; color: blue">static</span><span style="line-height: 140%"> </span><span style="line-height: 140%; color: blue">void</span><span style="line-height: 140%"> PrintUserInfo()</span></pre>

  <pre style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160; {</span></pre>

  <pre style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160;&#160;&#160; </span><span style="line-height: 140%; color: blue">try</span></pre>

  <pre style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160;&#160;&#160; {</span></pre>

  <pre style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160;&#160;&#160;&#160;&#160; </span><span style="line-height: 140%; color: blue">using</span><span style="line-height: 140%"> (</span><span style="line-height: 140%; color: #2b91af">MyVaultServiceManager</span><span style="line-height: 140%"> mgr = </span><span style="line-height: 140%; color: blue">new</span><span style="line-height: 140%"> </span><span style="line-height: 140%; color: #2b91af">MyVaultServiceManager</span><span style="line-height: 140%">(</span></pre>

  <pre style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; </span><span style="line-height: 140%; color: #2b91af">MyVaultServiceManager</span><span style="line-height: 140%">.</span><span style="line-height: 140%; color: #2b91af">Mode</span><span style="line-height: 140%">.ReadOnly))</span></pre>

  <pre style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160;&#160;&#160;&#160;&#160; {</span></pre>

  <pre style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; </span><span style="line-height: 140%; color: green">// The GetAllUsers method provides all the users' info</span></pre>

  <pre style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; </span><span style="line-height: 140%; color: green">//-----------------------------------------------------</span></pre>

  <pre style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; </span><span style="line-height: 140%; color: #2b91af">User</span><span style="line-height: 140%">[] users = mgr.Services.AdminService.GetAllUsers();</span></pre>

  <pre style="margin: 0px">&#160;</pre>

  <pre style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; </span><span style="line-height: 140%; color: green">// We will show the information in a simple message box</span></pre>

  <pre style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; </span><span style="line-height: 140%; color: blue">string</span><span style="line-height: 140%"> msg = </span><span style="line-height: 140%; color: #a31515">&quot;&quot;</span><span style="line-height: 140%">;</span></pre>

  <pre style="margin: 0px">&#160;</pre>

  <pre style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; </span><span style="line-height: 140%; color: blue">foreach</span><span style="line-height: 140%"> (</span><span style="line-height: 140%; color: #2b91af">User</span><span style="line-height: 140%"> user </span><span style="line-height: 140%; color: blue">in</span><span style="line-height: 140%"> users)</span></pre>

  <pre style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; {</span></pre>

  <pre style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; </span><span style="line-height: 140%; color: #2b91af">UserInfo</span><span style="line-height: 140%"> userInfo = </span></pre>

  <pre style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; mgr.Services.AdminService.GetUserInfoByUserId(user.Id);</span></pre>

  <pre style="margin: 0px">&#160;</pre>

  <pre style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; msg += user.Name + </span><span style="line-height: 140%; color: #a31515">&quot;\n----------------&quot;</span><span style="line-height: 140%">;</span></pre>

  <pre style="margin: 0px">&#160;</pre>

  <pre style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; </span><span style="line-height: 140%; color: blue">if</span><span style="line-height: 140%"> (userInfo.Roles != </span><span style="line-height: 140%; color: blue">null</span><span style="line-height: 140%"> &amp;&amp; userInfo.Roles.Length &gt; 0)</span></pre>

  <pre style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; {</span></pre>

  <pre style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; msg += </span><span style="line-height: 140%; color: #a31515">&quot;\n&#160;&#160; Roles:&quot;</span><span style="line-height: 140%">;</span></pre>

  <pre style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; </span><span style="line-height: 140%; color: blue">foreach</span><span style="line-height: 140%"> (</span><span style="line-height: 140%; color: #2b91af">Role</span><span style="line-height: 140%"> role </span><span style="line-height: 140%; color: blue">in</span><span style="line-height: 140%"> userInfo.Roles)</span></pre>

  <pre style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; {</span></pre>

  <pre style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; msg += </span><span style="line-height: 140%; color: #a31515">&quot;\n\tId:&#160;&#160; &quot;</span><span style="line-height: 140%"> + role.Id </span></pre>

  <pre style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; + </span><span style="line-height: 140%; color: #a31515">&quot;.\tName: &quot;</span><span style="line-height: 140%"> + role.Name;</span></pre>

  <pre style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; }</span></pre>

  <pre style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; }</span></pre>

  <pre style="margin: 0px">&#160;</pre>

  <pre style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; </span><span style="line-height: 140%; color: blue">if</span><span style="line-height: 140%"> (userInfo.Vaults != </span><span style="line-height: 140%; color: blue">null</span><span style="line-height: 140%"> &amp;&amp; userInfo.Vaults.Length &gt;0)</span></pre>

  <pre style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; {</span></pre>

  <pre style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; msg += </span><span style="line-height: 140%; color: #a31515">&quot;\n&#160;&#160; Vaults:&quot;</span><span style="line-height: 140%">;</span></pre>

  <pre style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; </span><span style="line-height: 140%; color: blue">foreach</span><span style="line-height: 140%"> (</span><span style="line-height: 140%; color: #2b91af">KnowledgeVault</span><span style="line-height: 140%"> vault </span><span style="line-height: 140%; color: blue">in</span><span style="line-height: 140%"> userInfo.Vaults)</span></pre>

  <pre style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; {</span></pre>

  <pre style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; msg += </span><span style="line-height: 140%; color: #a31515">&quot;\n\tId:&#160;&#160; &quot;</span><span style="line-height: 140%"> + vault.Id</span></pre>

  <pre style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; + </span><span style="line-height: 140%; color: #a31515">&quot;.\tName: &quot;</span><span style="line-height: 140%"> + vault.Name;</span></pre>

  <pre style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; }</span></pre>

  <pre style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; }</span></pre>

  <pre style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; msg += </span><span style="line-height: 140%; color: #a31515">&quot;\n================================\n&quot;</span><span style="line-height: 140%">;</span></pre>

  <pre style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; }</span></pre>

  <pre style="margin: 0px">&#160;</pre>

  <pre style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; </span><span style="line-height: 140%; color: #2b91af">MessageBox</span><span style="line-height: 140%">.Show( msg, </span><span style="line-height: 140%; color: #a31515">&quot;Completed!&quot;</span><span style="line-height: 140%">);</span></pre>

  <pre style="margin: 0px">&#160;</pre>

  <pre style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160;&#160;&#160;&#160;&#160; } </span><span style="line-height: 140%; color: green">// using</span></pre>

  <pre style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160;&#160;&#160; }</span></pre>

  <pre style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160;&#160;&#160; </span><span style="line-height: 140%; color: blue">catch</span><span style="line-height: 140%"> (System.</span><span style="line-height: 140%; color: #2b91af">Exception</span><span style="line-height: 140%"> err)</span></pre>

  <pre style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160;&#160;&#160; {</span></pre>

  <pre style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160;&#160;&#160;&#160;&#160; </span><span style="line-height: 140%; color: #2b91af">MessageBox</span><span style="line-height: 140%">.Show(err.Message);</span></pre>

  <pre style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160;&#160;&#160; }</span></pre>

  <pre style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160; } </span><span style="line-height: 140%; color: green">// PrintUserInfo()</span></pre>

  <pre style="margin: 0px">&#160;</pre>

  <pre style="margin: 0px"><span style="line-height: 140%">&#160; } </span><span style="line-height: 140%; color: green">// class AdminSample</span></pre>

  <pre style="margin: 0px">&#160;</pre>

  <pre style="margin: 0px"><span style="line-height: 140%">} </span><span style="line-height: 140%; color: green">// namespace AdminExample</span></pre>
</div>
