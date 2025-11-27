---
layout: "post"
title: "Vault API: Users and Roles, part 2"
date: "2012-07-31 11:52:08"
author: "Marat Mirgaleev"
categories:
  - "Marat Mirgaleev"
  - "Vault"
original_url: "https://adndevblog.typepad.com/manufacturing/2012/07/vault-api-users-and-roles-part-2.html "
typepad_basename: "vault-api-users-and-roles-part-2"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/manufacturing/marat-mirgaleev.html" target="_self">Marat Mirgaleev</a></p>  <p><strong>Issue</strong></p>  <p><em>How do I get the list of Roles from the Vault server?</em></p>  <p><strong>Solution</strong></p>  <p>We will need to use the Autodesk.Connectivity.WebServices.AdminService class for this.</p>  <p>In my <a href="http://adndevblog.typepad.com/manufacturing/2012/07/vault-api-users-and-roles-part-1.html" target="_blank">previous post</a> I explained how to get the list of the Vault server users. Let’s continue working with the same code sample and add a ListRoles() method to our AdminSample class:</p>  <div style="font-family: courier new; background: white; color: black; font-size: 8pt">   <pre style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160; </span><span style="line-height: 140%; color: green">// List all roles in the database</span></pre>

  <pre style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160; </span><span style="line-height: 140%; color: green">//===============================================================</span></pre>

  <pre style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160; </span><span style="line-height: 140%; color: blue">public</span><span style="line-height: 140%"> </span><span style="line-height: 140%; color: blue">static</span><span style="line-height: 140%"> </span><span style="line-height: 140%; color: blue">void</span><span style="line-height: 140%"> ListRoles()</span></pre>

  <pre style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160; {</span></pre>

  <pre style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160;&#160;&#160; </span><span style="line-height: 140%; color: blue">using</span><span style="line-height: 140%"> (</span><span style="line-height: 140%; color: #2b91af">MyVaultServiceManager</span><span style="line-height: 140%"> mgr = </span><span style="line-height: 140%; color: blue">new</span><span style="line-height: 140%"> </span><span style="line-height: 140%; color: #2b91af">MyVaultServiceManager</span><span style="line-height: 140%">(</span></pre>

  <pre style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; </span><span style="line-height: 140%; color: #2b91af">MyVaultServiceManager</span><span style="line-height: 140%">.</span><span style="line-height: 140%; color: #2b91af">Mode</span><span style="line-height: 140%">.ReadOnly))</span></pre>

  <pre style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160;&#160;&#160; {</span></pre>

  <pre style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160;&#160;&#160;&#160;&#160; </span><span style="line-height: 140%; color: blue">try</span></pre>

  <pre style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160;&#160;&#160;&#160;&#160; {</span></pre>

  <pre style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; </span><span style="line-height: 140%; color: green">// GetAllRoles() - this is the way to get all the Roles</span></pre>

  <pre style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; </span><span style="line-height: 140%; color: green">//------------------------------------------------------</span></pre>

  <pre style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; </span><span style="line-height: 140%; color: #2b91af">Role</span><span style="line-height: 140%">[] roles = mgr.Services.AdminService.GetAllRoles();</span></pre>

  <pre style="margin: 0px">&#160;</pre>

  <pre style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; </span><span style="line-height: 140%; color: green">// Prepare a string to show the Roles in a message box</span></pre>

  <pre style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; </span><span style="line-height: 140%; color: blue">string</span><span style="line-height: 140%"> msg = </span><span style="line-height: 140%; color: #a31515">&quot;Id |&#160;&#160; Name\n&quot;</span><span style="line-height: 140%">;</span></pre>

  <pre style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; msg += </span><span style="line-height: 140%; color: #a31515">&quot;-------------\n&quot;</span><span style="line-height: 140%">;</span></pre>

  <pre style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; </span><span style="line-height: 140%; color: blue">foreach</span><span style="line-height: 140%"> (</span><span style="line-height: 140%; color: #2b91af">Role</span><span style="line-height: 140%"> role </span><span style="line-height: 140%; color: blue">in</span><span style="line-height: 140%"> roles)</span></pre>

  <pre style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; msg += role.Id.ToString() + </span><span style="line-height: 140%; color: #a31515">&quot;: &quot;</span><span style="line-height: 140%"> + role.Name + </span><span style="line-height: 140%; color: #a31515">&quot;\n&quot;</span><span style="line-height: 140%">;</span></pre>

  <pre style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; </span><span style="line-height: 140%; color: #2b91af">MessageBox</span><span style="line-height: 140%">.Show(msg, </span><span style="line-height: 140%; color: #a31515">&quot;Roles found&quot;</span><span style="line-height: 140%">);</span></pre>

  <pre style="margin: 0px">&#160;</pre>

  <pre style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; </span><span style="line-height: 140%; color: green">// This is how we do find some arbitrary role</span></pre>

  <pre style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; </span><span style="line-height: 140%; color: #2b91af">Role</span><span style="line-height: 140%"> admin = FindRole(roles, </span><span style="line-height: 140%; color: #a31515">&quot;Administrator&quot;</span><span style="line-height: 140%">);</span></pre>

  <pre style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; </span><span style="line-height: 140%; color: blue">if</span><span style="line-height: 140%"> (admin != </span><span style="line-height: 140%; color: blue">null</span><span style="line-height: 140%">)</span></pre>

  <pre style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; </span><span style="line-height: 140%; color: #2b91af">MessageBox</span><span style="line-height: 140%">.Show(</span><span style="line-height: 140%; color: #a31515">&quot;Administrator role Id is &quot;</span></pre>

  <pre style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; + admin.Id.ToString());</span></pre>

  <pre style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160;&#160;&#160;&#160;&#160; }</span></pre>

  <pre style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160;&#160;&#160;&#160;&#160; </span><span style="line-height: 140%; color: blue">catch</span><span style="line-height: 140%"> (System.</span><span style="line-height: 140%; color: #2b91af">Exception</span><span style="line-height: 140%"> err)</span></pre>

  <pre style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160;&#160;&#160;&#160;&#160; {</span></pre>

  <pre style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; </span><span style="line-height: 140%; color: #2b91af">MessageBox</span><span style="line-height: 140%">.Show(err.ToString());</span></pre>

  <pre style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160;&#160;&#160;&#160;&#160; } </span><span style="line-height: 140%; color: green">// try</span></pre>

  <pre style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160;&#160;&#160; } </span><span style="line-height: 140%; color: green">// using</span></pre>

  <pre style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160; } </span><span style="line-height: 140%; color: green">// ListRoles()</span></pre>
</div>

<p>Also, in this piece of code we look for a role called &quot;Administrator&quot;, for this purpose we’ve created a very simple function which iterates through the roles array:</p>

<div style="font-family: courier new; background: white; color: black; font-size: 8pt">
  <pre style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160; </span><span style="line-height: 140%; color: green">// Find a role by its name.</span></pre>

  <pre style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160; </span><span style="line-height: 140%; color: green">// Parameters: - An array of roles;</span></pre>

  <pre style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160; </span><span style="line-height: 140%; color: green">//&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; - Role name to find.</span></pre>

  <pre style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160; </span><span style="line-height: 140%; color: green">// Returns the found Role or null, if didn't find.</span></pre>

  <pre style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160; </span><span style="line-height: 140%; color: green">//===============================================================</span></pre>

  <pre style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160; </span><span style="line-height: 140%; color: blue">private</span><span style="line-height: 140%"> </span><span style="line-height: 140%; color: blue">static</span><span style="line-height: 140%"> </span><span style="line-height: 140%; color: #2b91af">Role</span><span style="line-height: 140%"> FindRole(</span><span style="line-height: 140%; color: #2b91af">Role</span><span style="line-height: 140%">[] i_roles, </span><span style="line-height: 140%; color: blue">string</span><span style="line-height: 140%"> i_roleName)</span></pre>

  <pre style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160; {</span></pre>

  <pre style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160;&#160;&#160; </span><span style="line-height: 140%; color: #2b91af">Role</span><span style="line-height: 140%"> found = </span><span style="line-height: 140%; color: blue">null</span><span style="line-height: 140%">;</span></pre>

  <pre style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160;&#160;&#160; </span><span style="line-height: 140%; color: blue">foreach</span><span style="line-height: 140%">(</span><span style="line-height: 140%; color: #2b91af">Role</span><span style="line-height: 140%"> role </span><span style="line-height: 140%; color: blue">in</span><span style="line-height: 140%"> i_roles)</span></pre>

  <pre style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160;&#160;&#160;&#160;&#160; </span><span style="line-height: 140%; color: blue">if</span><span style="line-height: 140%"> (role.Name == i_roleName)</span></pre>

  <pre style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160;&#160;&#160;&#160;&#160; {</span></pre>

  <pre style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; found = role;</span></pre>

  <pre style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; </span><span style="line-height: 140%; color: blue">break</span><span style="line-height: 140%">;</span></pre>

  <pre style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160;&#160;&#160;&#160;&#160; }</span></pre>

  <pre style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160;&#160;&#160; </span><span style="line-height: 140%; color: blue">return</span><span style="line-height: 140%"> found;</span></pre>

  <pre style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160; }</span>&#160;</pre>
</div>

<p>This is the program output showing the list of Roles that exist in Vault by default:</p>

<p>&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; <a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b017743d03bdb970d-pi"><img style="background-image: none; border-right-width: 0px; padding-left: 0px; padding-right: 0px; display: inline; border-top-width: 0px; border-bottom-width: 0px; border-left-width: 0px; padding-top: 0px" title="image" border="0" alt="image" src="/assets/image_0fbf09.jpg" width="255" height="448" /></a></p>
