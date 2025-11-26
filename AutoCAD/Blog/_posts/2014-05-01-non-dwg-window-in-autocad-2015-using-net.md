---
layout: "post"
title: "Non-DWG window in AutoCAD 2015 using .Net"
date: "2014-05-01 08:40:55"
author: "Balaji"
categories:
  - ".NET"
  - "2015"
  - "AutoCAD"
  - "Balaji Ramamoorthy"
original_url: "https://adndevblog.typepad.com/autocad/2014/05/non-dwg-window-in-autocad-2015-using-net.html "
typepad_basename: "non-dwg-window-in-autocad-2015-using-net"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/balaji-ramamoorthy.html" target="_self">Balaji Ramamoorthy</a></p>
<p>AutoCAD 2015 enables the creation of Non-DWG document window. The Non-DWG document window appears as a tab just as any other drawing window.</p>
<p>In this blog post, I have attached a C# project to create a Non-DWG document window and demonstrate its usage.</p>
<p>To try it :</p>
<p>1. Build the sample project using Visual Studio 2012 with .Net framework set to 4.5.</p>
<p>2. Start AutoCAD 2015 and netload the dll.</p>
<p>3. Run "MyWnd" command. This command creates a Non-DWG document window that accepts user input for the radius of a smiley.</p>
<p><a class="asset-img-link" style="float: left;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01a3fcfe9dd4970b-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b01a3fcfe9dd4970b img-responsive" style="margin: 0px 5px 5px 0px;" title="NonDwgWindow_Net" src="/assets/image_578117.jpg" alt="NonDwgWindow_Net" /></a></p>
<p>&nbsp;</p>
<p>&nbsp;</p>
<p>&nbsp;</p>
<p>&nbsp;</p>
<p>&nbsp;</p>
<p>&nbsp;</p>
<p>&nbsp;</p>
<p>&nbsp;</p>
<p>&nbsp;</p>
<p>4. Create another drawing and run the "InsertSmiley" command. This command creates a smiley based on the radius value provided in the Non-DWG document window.</p>
<p>Now, here is a brief description of the steps to create a Non-DWG document window using .Net :</p>
<p>Step-1. Create a WPF usercontrol and customize it as you would create it usually.</p>
<p>Step-2. Create a custom document class. This class will be used to hold the data that are specific to this document window.</p>
<p>Step-3. Create a custom document window class derived from WPFDocumentWindow.</p>
<p>- Override the "OnCreate" method to know when the document window is created.</p>
<p>- Override the "OnLoad" method to associate the custom document with our document window class</p>
<p>- Override the "OnActivate" method to know when the document window gets activated.</p>
<p>Step-4. Create an instance of the custom document window class and add it to the DocumentWindowCollection using Application.DocumentWindowCollection.AddDocumentWindow.</p>
<p>In the attached sample, the steps to associate a custom document have been commented. AutoCAD 2015 at present becomes unstable if a custom document is associated with the custom document window. This behavior has been logged in our internal database for our engineering team to analyze.</p>
<p>So the attached sample project retrieves the document data by directly accessing the usercontrol for the "InsertSmiley" command to access.</p>
<p>Here is the sample project :</p>
<p><span class="asset  asset-generic at-xid-6a0167607c2431970b01a73db9722c970d img-responsive"><a href="http://adndevblog.typepad.com/files/nondwgdocwindow_net.zip">Download NonDwgDocWindow_Net</a></span></p>
<p>&nbsp;</p>
