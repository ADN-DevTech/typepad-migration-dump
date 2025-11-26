---
layout: "post"
title: "Navisworks 2024 SDK is posted"
date: "2023-06-13 02:01:39"
author: "Naveen Kumar"
categories:
  - ".NET"
  - "Naveen Kumar"
  - "Navisworks"
original_url: "https://adndevblog.typepad.com/aec/2023/06/navisworks-2024-sdk-is-posted.html "
typepad_basename: "navisworks-2024-sdk-is-posted"
typepad_status: "Publish"
---

<p>By <a href="https://adndevblog.typepad.com/aec/Naveen-Kumar.html" target="_self">Naveen Kumar</a></p>
<p>Navisworks 2024 SDK has been posted on ADNOpen:&nbsp;</p>
<p><a href="https://www.autodesk.com/developer-network/platform-technologies/navisworks">https://www.autodesk.com/developer-network/platform-technologies/navisworks</a></p>
<p>Navisworks 2024 is built against .NET Framework 4.8, which means the application with NET API supports .NET Framework 4.8 and above. The program will need to be compiled by Visual Studio 2022 and .NET Framework 4.8 and above.</p>
<p>In Navisworks 2024 API, We have found an issue of .NET control application.</p>
<p><strong>Issue Description:</strong> Autodesk.Navisworks.Api.Controls.ApplicationControl.Initialize() will throw an exception: "External Component has thrown an exception".</p>
<p><a href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c1aa76a9de200d-popup"> </a><a class="asset-img-link" style="display: inline;" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c1aa76ab90200d-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a0167607c2431970b02c1aa76ab90200d img-responsive" alt="GIF" title="GIF" src="/assets/image_748196.jpg" border="0" /></a><a href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c1aa76a9de200d-popup"><br /><br /></a></p>
<p><strong>Solution:</strong> This issue is scheduled to be fixed in future update.</p>
<p>For now, the workaround is to add the installation directory of Navisworks to the path before calling Autodesk.Navisworks.Api.Controls.ApplicationControl.Initialize();&nbsp;</p>
<p><span style="font-size: 10pt;"><strong><span style="font-family: georgia, palatino;">(i.e) Environment.SetEnvironmentVariable("PATH",</span></strong></span><span style="font-size: 10pt;"><strong><span style="font-family: georgia, palatino;">Environment.GetEnvironmentVariable("PATH")+ ";"+nwInstallDir);&nbsp;</span></strong></span></p>
<p>where&nbsp;nwInstallDir&nbsp;has a value corresponding to where NW is installed.</p>
<p>Your sample code will look like this</p>
<pre>
 static void XMain()
 {
   //Set Environment variable
   string nwInstallDir = @"C:\Program Files\Autodesk\Navisworks Manage 2024";
   Environment.SetEnvironmentVariable("PATH",
                 Environment.GetEnvironmentVariable("PATH")+";"+nwInstallDir);
        
   //Set to single document mode
   Autodesk.Navisworks.Api.Controls.ApplicationControl.ApplicationType=ApplicationType.SingleDocument;

   //Initialise the api
   Autodesk.Navisworks.Api.Controls.ApplicationControl.Initialize();

   Application.EnableVisualStyles();
   Application.SetCompatibleTextRenderingDefault(false);
   Application.Run(new Viewer());

   //Finish use of the API.
   Autodesk.Navisworks.Api.Controls.ApplicationControl.Terminate();
 }
</pre>
