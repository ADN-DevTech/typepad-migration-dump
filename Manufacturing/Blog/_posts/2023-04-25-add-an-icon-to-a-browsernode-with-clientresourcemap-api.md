---
layout: "post"
title: "Add an Icon to a Browsernode with ClientResourceMap  API"
date: "2023-04-25 01:04:09"
author: "Fidel Makatia"
categories:
  - "Fidel Makatia"
original_url: "https://adndevblog.typepad.com/manufacturing/2023/04/add-an-icon-to-a-browsernode-with-clientresourcemap-api.html "
typepad_basename: "add-an-icon-to-a-browsernode-with-clientresourcemap-api"
typepad_status: "Publish"
---

<p>ClientResourceMap was introduced in Inventor 2022. This API allows users to set a list of client resources (like icons) for the browser pane for different themes.</p>
<p><strong>STEP1</strong></p>
<p>Make sure the Autodesk.inventor.interop.dll reference in your add in is the latest dll. You can do this by copying from the <em>C:\Program Files\Autodesk\Inventor 2022\Bin\Public Assemblies </em>to the <em>/bin/debug/</em> directory of your Add in</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02b685356b36200d-pi" style="display: inline;"><img alt="Picture1" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02b685356b36200d image-full img-responsive" src="/assets/image_9894e9.jpg" title="Picture1" /></a></p>
<p><strong>STEP 2</strong></p>
<p>Create&#0160; a NameValueMap variable and a new Icon to it.</p>
<p>&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;<span style="font-family: &#39;courier new&#39;, courier;">&#0160;&#0160;&#0160; NameValueMap oNameValueMap;</span></p>
<p><span style="font-family: &#39;courier new&#39;, courier;">&#0160; &#0160; &#0160; &#0160;oNameValueMap = app.TransientObjects.CreateNameValueMap();</span></p>
<p><span style="font-family: &#39;courier new&#39;, courier;">&#0160; &#0160; &#0160; &#0160;oNameValueMap.Add(&quot;Icon&quot;, oIcon);</span></p>
<p><strong>STEP 3</strong></p>
<p>Create a ClientResourceMaps instance and initialize it as below</p>
<p>&#0160;&#0160;&#0160;&#0160;&#0160;<span style="font-family: &#39;courier new&#39;, courier;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; ClientResourceMaps clientResourceMaps;</span></p>
<p><span style="font-family: &#39;courier new&#39;, courier;">&#0160; &#0160; &#0160; &#0160; &#0160;clientResourceMaps = app.ClientResourceMaps;</span></p>
<p>NB: app is of type Inventor. Application</p>
<p><strong>STEP 4</strong></p>
<p>Add a new Client resource map through the ClientResourceMap instance and using the add() method</p>
<p><span style="font-family: &#39;courier new&#39;, courier;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; ClientResourceMap clientResourceMap;</span></p>
<p><span style="font-family: &#39;courier new&#39;, courier;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; clientResourceMap = clientResourceMaps.Add(clintId, 1);</span></p>
<p>NB: ClientID is a static string variable. In this case, is the same as the class ID of the Add in. However, It can be any unique string. The next parameter is an integer that will be the ID of the added resource</p>
<p><strong>STEP 5</strong></p>
<p>Use the<span style="font-family: &#39;courier new&#39;, courier;"> <em>setBrowserIconData() </em></span>method to set the browser icon information and theme. Call this method for each Theme to set the icon data for that Theme, and in each browser icon data there should be an item that has the same name but a different icon for that Theme. The first argument is the <em>NameValueMap </em>variable you declared in Step 2, the next is the theme name as a string. The last variable is optional and is Boolean in nature. It specifies if the icon will be used as the default for the other themes if no icon data is provided for that theme. It is null by default.</p>
<p>&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;<span style="font-family: &#39;courier new&#39;, courier;"> clientResourceMap.SetBrowserIconData(oNameValueMap, &quot;Dark&quot;);</span></p>
<p><strong>Step 6</strong></p>
<p>Create a <em><span style="font-family: &#39;courier new&#39;, courier;">clientNodeResource</span> </em>object and use the <span style="font-family: &#39;courier new&#39;, courier;"><em>AddNodeResource()</em> </span>method to add a node resource with icon data from your clientResourceMap</p>
<p><span style="font-family: &#39;courier new&#39;, courier;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; ClientNodeResources oCnrs;</span></p>
<p><span style="font-family: &#39;courier new&#39;, courier;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; oCnrs = oDoc.BrowserPanes.ClientNodeResources;</span></p>
<p><span style="font-family: &#39;courier new&#39;, courier;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; ClientNodeResource oCnr;</span></p>
<p><span style="font-family: &#39;courier new&#39;, courier;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; oCnr = oCnrs.AddNodeResource(clientResourceMap.ClientId, 1, &quot;Icon&quot;);</span></p>
<p>The<span style="font-family: &#39;courier new&#39;, courier;"> AddNodeResource()</span> method takes in 3 arguments. First is the ClientID as a string. In this case, I passed the same client ID I used in creating the clientResourceMap, the second is an integer which is the ID of the resource. The third argument is the string value which is the name of the Icon you want to use from your ClientMapResource list.</p>
<p><strong>Step 7</strong></p>
<p>Override the icon for the client feature and update the view</p>
<p><span style="font-family: &#39;courier new&#39;, courier;">&#0160;NativeBrowserNodeDefinition nativeNodeDef = oNode.BrowserNodeDefinition as NativeBrowserNodeDefinition;</span></p>
<p><span style="font-family: &#39;courier new&#39;, courier;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; if (nativeNodeDef != null)</span></p>
<p><span style="font-family: &#39;courier new&#39;, courier;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; nativeNodeDef.OverrideIcon = oCnr;</span></p>
<p><span style="font-family: &#39;courier new&#39;, courier;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; app.ActiveView.Update();</span></p>
<p><span style="font-family: &#39;courier new&#39;, courier;"> <a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02b751a2bb0f200c-pi" style="display: inline;"><img alt="Picture2" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02b751a2bb0f200c image-full img-responsive" src="/assets/image_1f7a2f.jpg" title="Picture2" /></a><br /></span></p>
<p>Before running the attached code sample make sure you change the following code to look for the .bmp image file in the right directory.</p>
<p><span style="font-family: &#39;courier new&#39;, courier;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; var image = Image.FromFile(@&quot;C:\temp\clock.bmp&quot;);</span></p>
<p><span style="font-family: &#39;courier new&#39;, courier;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; stdole.IPictureDisp oIcon;</span></p>
<p><span style="font-family: &#39;courier new&#39;, courier;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; oIcon = PictureDispConverter.ToIPictureDisp(image);</span></p>
<p><span style="font-family: &#39;courier new&#39;, courier;"> <span class="asset  asset-generic at-xid-6a0167607c2431970b02b685356bd0200d img-responsive"><a href="https://adndevblog.typepad.com/files/clientfeatureicon355.zip">Download ClientFeatureIcon355</a></span></span></p>
