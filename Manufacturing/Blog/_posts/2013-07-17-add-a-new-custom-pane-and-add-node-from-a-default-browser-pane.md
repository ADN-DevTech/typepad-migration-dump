---
layout: "post"
title: "Add a new custom pane and add node from a default browser pane"
date: "2013-07-17 17:56:52"
author: "Wayne Brill"
categories:
  - "Inventor"
  - "Wayne Brill"
original_url: "https://adndevblog.typepad.com/manufacturing/2013/07/add-a-new-custom-pane-and-add-node-from-a-default-browser-pane.html "
typepad_basename: "add-a-new-custom-pane-and-add-node-from-a-default-browser-pane"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/manufacturing/wayne-brill.html" target="_self">Wayne Brill</a></p>
<p>It is not possible to add a node to the default Inventor browser panes. One idea is to create a new browser pane and add a default node to the custom browser pane instead. This example adds a new browser pane and adds the top node of the active pane of the active document. </p>
<p>&#0160; 
<span class="asset  asset-generic at-xid-6a0167607c2431970b01901e50a96f970b"><a href="http://adndevblog.typepad.com/files/inventor_api_add_pane_with_default_node.zip">Download Inventor_API_Add_Pane_With_Default_Node</a></span></p>
<p>   <br />This project creates an AddIn. To test it follow the steps in this section of the Inventor API help. </p>
<p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01901e50a58d970b-pi"><img alt="image" border="0" height="230" src="/assets/image_61a30a.jpg" style="background-image: none; padding-left: 0px; padding-right: 0px; display: inline; padding-top: 0px; border-width: 0px;" title="image" width="427" /></a>&#0160;</p>
<p>Once the AddIn is loaded open an assembly and run the “Assembly Cmd” on the Assemble Tab. It should add a new browser node named myNewPane.</p>
<p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b0192ac0ff38f970d-pi"><img alt="image" border="0" height="172" src="/assets/image_bc2d62.jpg" style="background-image: none; padding-left: 0px; padding-right: 0px; display: inline; padding-top: 0px; border-width: 0px;" title="image" width="404" /></a>&#0160; </p>
<p>Here is the pertinent code from the project:</p>
<div style="font-family: consolas; background: #eeeeee; color: black; font-size: 10pt;">
<p style="margin: 0px;"><span style="color: blue;">Sub</span> addMyBrowserPane()</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">&#39;Get the active document</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">Dim</span> actDoc <span style="color: blue;">As</span> Inventor.<span style="color: #2b91af;">Document</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; actDoc = m_inventorApplication.ActiveDocument</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">&#39;Get the top node of the active pane</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">Dim</span> actDocBrowserNode <span style="color: blue;">As</span> Inventor.<span style="color: #2b91af;">BrowserNode</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; actDocBrowserNode = actDoc.BrowserPanes. _</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; ActivePane.TopNode</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">&#39; Get a picture to use as the icon for </span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">&#39; the new browser pane</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">Dim</span> mIPictureDisp1 <span style="color: blue;">As</span> <span style="color: #2b91af;">IPictureDisp</span> = _</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: #2b91af;">PictureDispConverter</span>.ToIPictureDisp _</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; (<span style="color: blue;">My</span>.Resources.Image_For_Browser_Pane)</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">&#39; Create a client node resource</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">Dim</span> clientNodeRsc <span style="color: blue;">As</span> <span style="color: #2b91af;">ClientNodeResource</span> = _</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; actDoc.BrowserPanes.ClientNodeResources.Add _</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; (<span style="color: #a31515;">&quot;MyNodeResorce&quot;</span>, 2211, mIPictureDisp1)</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">&#39; Create a browser node definition</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">Dim</span> clientBrowserNodeDef <span style="color: blue;">As</span>&#0160; _</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: #2b91af;">ClientBrowserNodeDefinition</span> = _</p>
<p style="margin: 0px;">actDoc.BrowserPanes.CreateBrowserNodeDefinition _</p>
<p style="margin: 0px;">&#0160;&#0160; (<span style="color: #a31515;">&quot;TopNodeForNewBrowser&quot;</span>, 11344, clientNodeRsc)</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">&#39; Create the new browser pane</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">Dim</span> newBrowserPane <span style="color: blue;">As</span> <span style="color: #2b91af;">BrowserPane</span> = _</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; actDoc.BrowserPanes.AddTreeBrowserPane _</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; (<span style="color: #a31515;">&quot;myNewPane&quot;</span>, <span style="color: #a31515;">&quot;InternalNameForNewPane&quot;</span>, _</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; clientBrowserNodeDef)</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">&#39; Add the existing browser node to the </span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">&#39; new browser pane</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; newBrowserPane.TopNode.AddChild _</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; (actDocBrowserNode.BrowserNodeDefinition)</p>
<p style="margin: 0px;"><span style="color: blue;">End</span> <span style="color: blue;">Sub</span></p>
</div>
