---
layout: "post"
title: "Vault API: Users and Roles, part 3"
date: "2012-07-31 12:26:14"
author: "Marat Mirgaleev"
categories:
  - "Marat Mirgaleev"
  - "Vault"
original_url: "https://adndevblog.typepad.com/manufacturing/2012/07/vault-api-users-and-roles-part-3.html "
typepad_basename: "vault-api-users-and-roles-part-3"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/manufacturing/marat-mirgaleev.html">Marat Mirgaleev</a></p>  <p><strong>Issue</strong></p>  <p><em>Is there any programmatic way to add a user to the Vault server?</em></p>  <p><strong>Solution</strong></p>  <p>Yes, in Vault API we can easily create a new user with usage of the Autodesk.Connectivity.WebServices.AdminService class.</p>  <p>I will add a new method to the AdminSample class which we were working on in these posts: <a href="http://adndevblog.typepad.com/manufacturing/2012/07/vault-api-users-and-roles-part-1.html" target="_blank">Vault API: Users and Roles, part 1</a> and <a href="http://adndevblog.typepad.com/manufacturing/2012/07/vault-api-users-and-roles-part-2.html" target="_blank">Vault API: Users and Roles, part 2</a>.</p>  <p>Sorry that I hardcoded the user’s data, I hope it is Ok for the purpose of a sample showing the process. Also, we will need to assign some role and some vault for the new user, this will require to find the IDs of the respective objects.</p>  <p>Here is our new method called AddUser():</p>  <div style="font-family: courier new; background: white; color: black; font-size: 8pt">   <pre style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160; </span><span style="line-height: 140%; color: green">// Add a new user.</span></pre>

  <pre style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160; </span><span style="line-height: 140%; color: green">// Let it be me with the 'Adminstrator' role and access to</span></pre>

  <pre style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160; </span><span style="line-height: 140%; color: green">//&#160;&#160; the vault called 'MaratVault'.</span></pre>

  <pre style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160; </span><span style="line-height: 140%; color: green">//===============================================================</span></pre>

  <pre style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160; </span><span style="line-height: 140%; color: blue">public</span><span style="line-height: 140%"> </span><span style="line-height: 140%; color: blue">static</span><span style="line-height: 140%"> </span><span style="line-height: 140%; color: blue">void</span><span style="line-height: 140%"> AddUser()</span></pre>

  <pre style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160; {</span></pre>

  <pre style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160;&#160;&#160; </span><span style="line-height: 140%; color: blue">using</span><span style="line-height: 140%"> (</span><span style="line-height: 140%; color: #2b91af">MyVaultServiceManager</span><span style="line-height: 140%"> mgr = </span><span style="line-height: 140%; color: blue">new</span><span style="line-height: 140%"> </span><span style="line-height: 140%; color: #2b91af">MyVaultServiceManager</span><span style="line-height: 140%">(</span></pre>

  <pre style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; </span><span style="line-height: 140%; color: #2b91af">MyVaultServiceManager</span><span style="line-height: 140%">.</span><span style="line-height: 140%; color: #2b91af">Mode</span><span style="line-height: 140%">.ReadWrite))</span></pre>

  <pre style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160;&#160;&#160; {</span></pre>

  <pre style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160;&#160;&#160;&#160;&#160; </span><span style="line-height: 140%; color: blue">try</span></pre>

  <pre style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160;&#160;&#160;&#160;&#160; {</span></pre>

  <pre style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; </span><span style="line-height: 140%; color: green">// Let's find the 'Administrator' role</span></pre>

  <pre style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; </span><span style="line-height: 140%; color: green">//------------------------------------------------------</span></pre>

  <pre style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; </span><span style="line-height: 140%; color: #2b91af">Role</span><span style="line-height: 140%">[] roles = mgr.Services.AdminService.GetAllRoles();</span></pre>

  <pre style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; </span><span style="line-height: 140%; color: #2b91af">Role</span><span style="line-height: 140%"> admin = FindRole(roles, </span><span style="line-height: 140%; color: #a31515">&quot;Administrator&quot;</span><span style="line-height: 140%">);</span></pre>

  <pre style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; </span><span style="line-height: 140%; color: blue">if</span><span style="line-height: 140%">( admin == </span><span style="line-height: 140%; color: blue">null</span><span style="line-height: 140%"> )</span></pre>

  <pre style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; {</span></pre>

  <pre style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; </span><span style="line-height: 140%; color: #2b91af">MessageBox</span><span style="line-height: 140%">.Show(</span><span style="line-height: 140%; color: #a31515">&quot;FindRole() failed.&quot;</span><span style="line-height: 140%">);</span></pre>

  <pre style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; </span><span style="line-height: 140%; color: blue">return</span><span style="line-height: 140%">;</span></pre>

  <pre style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; }</span></pre>

  <pre style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; </span><span style="line-height: 140%; color: blue">long</span><span style="line-height: 140%">[] roleIdArray = </span><span style="line-height: 140%; color: blue">new</span><span style="line-height: 140%"> </span><span style="line-height: 140%; color: blue">long</span><span style="line-height: 140%">[] { admin.Id };</span></pre>

  <pre style="margin: 0px">&#160;</pre>

  <pre style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; </span><span style="line-height: 140%; color: green">// I want to provide the user access to the vault called</span></pre>

  <pre style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; </span><span style="line-height: 140%; color: green">//&#160;&#160; &quot;MaratVault&quot;. So, let's find that vault Id:</span></pre>

  <pre style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; </span><span style="line-height: 140%; color: green">//---------------------------------------------------------</span></pre>

  <pre style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; </span><span style="line-height: 140%; color: #2b91af">KnowledgeVault</span><span style="line-height: 140%"> vault = mgr.Services.KnowledgeVaultService.</span></pre>

  <pre style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; GetKnowledgeVaultByName(</span><span style="line-height: 140%; color: #a31515">&quot;MaratVault&quot;</span><span style="line-height: 140%">);</span></pre>

  <pre style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; </span><span style="line-height: 140%; color: blue">long</span><span style="line-height: 140%">[] vaultIdArray = </span><span style="line-height: 140%; color: blue">new</span><span style="line-height: 140%"> </span><span style="line-height: 140%; color: blue">long</span><span style="line-height: 140%">[] { vault.Id };</span></pre>

  <pre style="margin: 0px">&#160;</pre>

  <pre style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; </span><span style="line-height: 140%; color: green">// Now create the user</span></pre>

  <pre style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; </span><span style="line-height: 140%; color: green">//---------------------------------------------------------</span></pre>

  <pre style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; </span><span style="line-height: 140%; color: #2b91af">User</span><span style="line-height: 140%"> newUser = mgr.Services.AdminService.AddUser(</span></pre>

  <pre style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; </span><span style="line-height: 140%; color: #a31515">&quot;MaratM&quot;</span><span style="line-height: 140%">,&#160;&#160;&#160;&#160;&#160; </span><span style="line-height: 140%; color: green">// user name</span></pre>

  <pre style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; </span><span style="line-height: 140%; color: #a31515">&quot;123456&quot;</span><span style="line-height: 140%">,&#160;&#160;&#160;&#160;&#160; </span><span style="line-height: 140%; color: green">// password</span></pre>

  <pre style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; </span><span style="line-height: 140%; color: #2b91af">AuthTyp</span><span style="line-height: 140%">.Vault, </span><span style="line-height: 140%; color: green">// authentication - not Active Directory</span></pre>

  <pre style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; </span><span style="line-height: 140%; color: #a31515">&quot;Marat&quot;</span><span style="line-height: 140%">,&#160;&#160;&#160;&#160;&#160;&#160; </span><span style="line-height: 140%; color: green">// first name</span></pre>

  <pre style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; </span><span style="line-height: 140%; color: #a31515">&quot;Mirgaleev&quot;</span><span style="line-height: 140%">,&#160;&#160; </span><span style="line-height: 140%; color: green">// last name</span></pre>

  <pre style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; </span><span style="line-height: 140%; color: #a31515">&quot;email&quot;</span><span style="line-height: 140%">,&#160;&#160;&#160;&#160;&#160;&#160; </span><span style="line-height: 140%; color: green">// email address</span></pre>

  <pre style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; </span><span style="line-height: 140%; color: blue">true</span><span style="line-height: 140%">,&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; </span><span style="line-height: 140%; color: green">// is active</span></pre>

  <pre style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; roleIdArray,&#160;&#160; </span><span style="line-height: 140%; color: green">// which roles</span></pre>

  <pre style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; vaultIdArray); </span><span style="line-height: 140%; color: green">// which vaults</span></pre>

  <pre style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160;&#160;&#160;&#160;&#160; }</span></pre>

  <pre style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160;&#160;&#160;&#160;&#160; </span><span style="line-height: 140%; color: blue">catch</span><span style="line-height: 140%"> (System.</span><span style="line-height: 140%; color: #2b91af">Exception</span><span style="line-height: 140%"> err)</span></pre>

  <pre style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160;&#160;&#160;&#160;&#160; {</span></pre>

  <pre style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; </span><span style="line-height: 140%; color: #2b91af">MessageBox</span><span style="line-height: 140%">.Show(</span><span style="line-height: 140%; color: #a31515">&quot;Probably, a user name conflict.\n&quot;</span></pre>

  <pre style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; + </span><span style="line-height: 140%; color: #a31515">&quot;The user name may already exist.\n\n&quot;</span></pre>

  <pre style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; + err.ToString(), </span><span style="line-height: 140%; color: #a31515">&quot;Error&quot;</span><span style="line-height: 140%">);</span></pre>

  <pre style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160;&#160;&#160;&#160;&#160; } </span><span style="line-height: 140%; color: green">// try</span></pre>

  <pre style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160;&#160;&#160; } </span><span style="line-height: 140%; color: green">// using</span></pre>

  <pre style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160; } </span><span style="line-height: 140%; color: green">// AddUser()</span>&#160;</pre>
</div>

<p>You may have noticed that we connect to Vault in the ‘Read-and-Write’ mode only if we need to change or add some information.</p>

<p>Let’s check whether is really works:</p>

<p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b017616e9ff23970c-pi"><img style="background-image: none; border-right-width: 0px; padding-left: 0px; padding-right: 0px; display: inline; border-top-width: 0px; border-bottom-width: 0px; border-left-width: 0px; padding-top: 0px" title="image" border="0" alt="image" src="/assets/image_671e99.jpg" width="483" height="403" /></a></p>

<p>Nice, we did it! And it wasn't very difficult.</p>
