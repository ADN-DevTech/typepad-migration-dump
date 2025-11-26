---
layout: "post"
title: "Non-DWG window in AutoCAD 2015"
date: "2014-05-01 03:15:45"
author: "Balaji"
categories:
  - "2015"
  - "AutoCAD"
  - "Balaji Ramamoorthy"
  - "ObjectARX"
  - "UI"
original_url: "https://adndevblog.typepad.com/autocad/2014/05/non-dwg-window-in-autocad-2015.html "
typepad_basename: "non-dwg-window-in-autocad-2015"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/balaji-ramamoorthy.html" target="_self">Balaji Ramamoorthy</a></p>
<p>AutoCAD 2015 enables the creation of Non-DWG document window. The Non-DWG document window appears as a tab just as any other drawing window.</p>
<p>In this blog post, I have attached a C++ project to create a Non-DWG document window and demonstrate its usage.</p>
<p>To try it :</p>
<p>1. Build the sample project using Visual Studio 2012 with Platform Toolset v110.</p>
<p>2. Start AutoCAD 2015 and load the arx module.</p>
<p>3. Run "ShowMyWnd" command. This command creates a Non-DWG document window that accepts user input for the dimensions of a chain link as shown here :</p>
<p><a class="asset-img-link" style="float: left;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01a73db938a1970d-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b01a73db938a1970d img-responsive" style="margin: 0px 5px 5px 0px;" title="Non-DWG Window" src="/assets/image_334318.jpg" alt="Non-DWG Window" /></a></p>
<p>&nbsp;</p>
<p>&nbsp;</p>
<p>&nbsp;</p>
<p>&nbsp;</p>
<p>&nbsp;</p>
<p>&nbsp;</p>
<p>&nbsp;</p>
<p>&nbsp;</p>
<p>4. Create another drawing and run the "InsertLink" command.&nbsp;This command creates a chain link based on the values provided in the Non-DWG document window.</p>
<p>Now, here is a brief description of the steps to create a Non-DWG document window using C++ :</p>
<p>Step-1. Create a dialog class derived from CDialog.&nbsp;An MFC dialog can be created using the resource view of Visual Studio as usual.</p>
<p>Step-2. Create a custom document class derived from AcRxObject.&nbsp;This class will be used to hold the data that are specific to this document window.&nbsp;<br /> <br />Step-3. Create a custom document window class derived from AcApDocWindow</p>
<p>- Override the "onCreate" method to instantiate the dialog that is to be shown.</p>
<p>- Override the "onLoad" method to associate the custom document created in step-2 with our document window class</p>
<p>- Override the "onDestroy" method to perform cleanup such as deleting the dialog instance.</p>
<p>- Override the "subRouteMessage" to perform resizing of the dialog when the document window size changes.</p>
<p>Step-4. Create a custom document window manager reactor derived from AcApDocWindowManagerReactor</p>
<p>- Override the "documentWindowActivated" method to keep our custom document updated with the values provided in the dialog.</p>
<p>- Override the "documentWindowCreated" method to get a pointer to our custom document window after its gets created</p>
<p>- Override the "documentWindowDestroyed" method so we know when our custom document window is no longer valid.</p>
<p>In the attached sample, the steps to associate a custom document have been commented. AutoCAD 2015 at present becomes unstable if a custom document is associated with the custom document window. This behavior has been logged in our internal database for our engineering team to analyze. So the attached sample project now stores the document data in a helper class as static variables for the "InsertLink" command to access.</p>
<p>Here is the sample project :</p>
<p><span class="asset  asset-generic at-xid-6a0167607c2431970b01a511ae0e5b970c img-responsive"><a href="http://adndevblog.typepad.com/files/nondwgwindowsample.zip">Download NonDwgWindowSample</a></span></p>
