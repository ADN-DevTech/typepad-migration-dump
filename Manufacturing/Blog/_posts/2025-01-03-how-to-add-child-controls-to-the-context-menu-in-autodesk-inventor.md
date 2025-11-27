---
layout: "post"
title: "How to Add Child Controls to the Context Menu in Autodesk Inventor"
date: "2025-01-03 11:14:45"
author: "Chandra Shekar Gopal"
categories:
  - "Chandra Shekar Gopal"
  - "Inventor"
original_url: "https://adndevblog.typepad.com/manufacturing/2025/01/how-to-add-child-controls-to-the-context-menu-in-autodesk-inventor.html "
typepad_basename: "how-to-add-child-controls-to-the-context-menu-in-autodesk-inventor"
typepad_status: "Publish"
---

<p>by <a href="https://adndevblog.typepad.com/manufacturing/chandra-shekar-gopal.html">Chandra shekar Gopal</a>,</p>
<p>In Autodesk Inventor, customizing the context menu (the right-click menu) can significantly enhance user experience by adding commonly used commands or custom tools. However, there is no direct method currently available for adding child controls directly to the context menu. While this functionality isn&#39;t natively supported, there is a workaround using VBA (Visual Basic for Applications) to achieve this.</p>
<p>In this blog, we will walk through the steps and code required to add child controls (such as &quot;Update&quot;, &quot;Delete&quot;, and &quot;Copy&quot;) to the context menu in Autodesk Inventor.</p>
<hr />
<h3>The Workaround: Using VBA to Add Child Controls to the Context Menu</h3>
<p>Although there isn’t a built-in option for directly adding child controls to the context menu, you can work around this by creating a custom command bar and populating it with buttons. You can then add this custom command bar as a popup to the existing context menu.</p>
<p>Here’s a simple guide to implement this workaround:</p>
<h4>Step 1: Initialize the User Input Events</h4>
<p>In your VBA code, you need to start by initializing the <code>UserInputEvents</code> object, which listens for user actions, including the context menu interaction.</p>
<pre><code>Private WithEvents UIE As UserInputEvents

Public Sub Init()
    Set UIE = ThisApplication.CommandManager.UserInputEvents

    Do
        DoEvents
    Loop
End Sub
</code></pre>
<h4>Step 2: Handling the Context Menu</h4>
<p>Next, you can use the <code>UIE_OnContextMenu</code> event to handle the right-click event and add the custom controls to the context menu. In this example, we are creating a custom command bar (called &quot;CustomBar&quot;) and populating it with buttons.</p>
<pre><code>Private Sub UIE_OnContextMenu(ByVal SelectionDevice As SelectionDeviceEnum, ByVal AdditionalInfo As NameValueMap, ByVal CommandBar As CommandBar)
    Dim oBar As CommandBar

    On Error Resume Next
    Set oBar = ThisApplication.UserInterfaceManager.CommandBars(&quot;CustomBar&quot;)
    oBar.Delete &#39; Delete any existing &quot;CustomBar&quot;

    Set oBar = ThisApplication.UserInterfaceManager.CommandBars.Add(&quot;CustomBar&quot;, &quot;CustomBar&quot;, kPopUpCommandBar)
    On Error GoTo 0

    &#39; Adding child controls (buttons)
    oBar.Controls.AddButton ThisApplication.CommandManager.ControlDefinitions(&quot;AppLocalUpdateCmd&quot;)
    oBar.Controls.AddButton ThisApplication.CommandManager.ControlDefinitions(&quot;AppDeleteCmd&quot;)
    oBar.Controls.AddButton ThisApplication.CommandManager.ControlDefinitions(&quot;AppCopyCmd&quot;)

    &#39; Add the custom bar as a popup in the context menu
    CommandBar.Controls.AddPopup oBar
End Sub
</code></pre>
<h4>Step 3: Running the Code</h4>
<p>Once the VBA code is executed, a custom command bar named <strong>&quot;CustomBar&quot;</strong> will be added to the context menu with the following child controls:</p>
<ul>
<li>Update</li>
<li>Delete</li>
<li>Copy</li>
</ul>
<p>This setup allows you to extend the context menu with additional functionality that can be customized according to your needs.</p>
<p><em>Here’s what the context menu would look like after the code runs (as shown in the image below):</em></p>

<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02e860dec338200b-pi" style="display: inline;"><img alt="Context menu" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02e860dec338200b img-responsive" height="402" src="/assets/image_525b4b.jpg" title="Context menu" width="302" /></a></p>
<h3>Conclusion</h3>
<p>While Autodesk Inventor does not currently provide a direct method to add child controls to the context menu, this workaround using VBA allows you to create custom command bars and enhance the user interface with added functionality. With this method, you can improve your workflow by providing quick access to commands like <em>Update</em>, <em>Delete</em>, and <em>Copy</em> right from the context menu.</p>
<p>As always, keep an eye on Autodesk’s updates, as future releases may provide a more native way to handle this customization, making it easier to implement in your applications.</p>
