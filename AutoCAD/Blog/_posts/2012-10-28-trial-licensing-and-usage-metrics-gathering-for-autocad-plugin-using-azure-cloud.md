---
layout: "post"
title: "Trial licensing and Usage metrics gathering for AutoCAD Plugin using Azure Cloud"
date: "2012-10-28 08:43:18"
author: "Balaji"
categories:
  - ".NET"
  - "AutoCAD"
  - "Balaji Ramamoorthy"
original_url: "https://adndevblog.typepad.com/autocad/2012/10/trial-licensing-and-usage-metrics-gathering-for-autocad-plugin-using-azure-cloud.html "
typepad_basename: "trial-licensing-and-usage-metrics-gathering-for-autocad-plugin-using-azure-cloud"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/balaji-ramamoorthy.html" target="_self">Balaji Ramamoorthy</a></p>
<p>Here is an application that <a href="http://adndevblog.typepad.com/autocad/virupaksha-aithal.html">Viru</a> and I have been working intermittently for a few months now.</p>
<p>It is an application for the trial licensing of AutoCAD plugins and more importantly, to track the usage of the commands by the end-user using a cloud based service. This will help the plugin developers by providing an insight into the features of their plugins that are frequently used by the end-users.</p>
<p>This sample application is to provide a simple example on using Windows Azure Cloud in an AutoCAD plugin and is not meant to be a robust trial licensing or application metrics gathering utility.</p>
<p>Here is a diagram and an explanation of its working at a broad level.</p>
<p><a class="asset-img-link" style="display: inline;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b017ee484c484970d-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b017ee484c484970d image-full" title="HowItWorks" src="/assets/image_757258.jpg" border="0" alt="HowItWorks" /></a></p>
<p>The “AppMetrics Manager” is a module that the AutoCAD plugins use to check for the availability of the trial license and to report the usage of their commands.</p>
<p>The “AppMetrics Manager” passes on the request for a trial license and the command usage information to the Azure worker role.</p>
<p>An Azure worker role manages the details of the trial licensing based on the records stored in the Azure table storage and lets the AppMetrics Manager know about the remaining duration of the trial license. The Worker role also records the command usage statistics in its Azure Table storage.</p>
<p>An Azure web role retrieves the details from the Azure Table Storage, which is then displayed in a web browser.</p>
<p>The sample projects for these modules and an AutoCAD plugin bundle to try out its working can be downloaded from the link below. </p>
<p>To try it, you will need the Windows Azure SDK to be installed in your system and if haven’t installed it already, you can install it from <a href=”http://www.windowsazure.com/en-us/">here</a></p>
<p>The Visual Studio solutions for the various modules have been kept separate for ease of debugging and can be built in the following order to resolve its dependencies:</p>
<p>1) “AppMetricsDatabaseModel”: This implements the common methods required for accessing the Azure Table storage. Before building this solution, ensure that the references to “Microsoft.WindowsAzure.ServiceRuntime” and “Microsoft.WindowsAzure.StorageClient ” are correctly resolved. </p>
<p>2)&nbsp;“AppMetricsInterface”: This defines the common interface that will be used by the Worker role and the “AppMetricsManager” to communicate. There are no additional references for this solution and building it should be straight-forward.</p>
<p>&nbsp;3)&nbsp;“AppMetricsManager” :&nbsp; This solution has dependencies on the usual AutoCAD references such as “AcDbMgd.dll”, “AcMgd.dll” and “AcCoreMgd.dll”. Ensure that these are referenced from the ObjectARX 2013 folder and it should then build ok.</p>
<p>&nbsp;4)&nbsp;“AppMetricsWorkerRole”: The Azure Worker role solution needs references to the “AppMetricsDatabaseModel” and “AppMetricsInterface” assemblies that have been built. Also, ensure that the other references to “Microsoft.WindowsAzure.XXXX” are correctly resolved. </p>
<p>&nbsp;5)&nbsp;“AppMetricsWebRole”: The Azure Worker role solution needs reference to the “AppMetricsDatabaseModel” assembly that has been built. Also, ensure that the other references to “Microsoft.WindowsAzure.XXXXXX” are correctly resolved. </p>
<p>&nbsp;6)&nbsp;“AppMetricsTestPlugin.bundle”: This is a bundle of a AutoCAD plugin in the Autoloader bundle format. Copy the entire bundle to folder from which AutoCAD can load it at startup. The folder to copy the bundle is “%appdata%\Autodesk\ApplicationPlugins”. The bundle folder has the source for a test<br />plugin which can be built and debugged. The Visual Studio solution can be found under the “Contents\Source” folder inside the bundle folder. Build this solution just as you would build a regular AutoCAD .Net plugin. After the build has succeeded, copy the “AppMetricsManager.dll” that has already been built and place it in the “Contents\Windows” folder inside the bundle folder.</p>
<p>Now, after all the solutions have been built, you are all set to try it using the Azure compute/storage emulator.</p>
<p>Here are the steps to try it out:</p>
<p>1)&nbsp;<strong>Start the Worker Role :</strong> Open the “AppMetricsWorkerRole“solution and hit F5. This should start the Azure compute emulator and start the worker role. It might take a few minutes to start it in the emulator and can be verified by clicking on the small icon in the tray and starting the compute emulator UI as shown here :</p>
<p>
<a class="asset-img-link" style="display: inline;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b017ee484c63e970d-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b017ee484c63e970d" title="StartEmulator" src="/assets/image_31199.jpg" border="0" alt="StartEmulator" /></a></p>
</p>
<p>2)<strong> Start the Web Role :</strong> Open the “AppMetricsWebRole“solution and hit F5. This should start the Azure compute emulator and start the web role. It might take a few minutes to start it in the emulator and can be verified by clicking on the small icon in the tray and starting the compute emulator UI just as we did<br />for the worker role. After the Web role starts, it should open the web browser and display the app details which is now empty as shown here :</p>
<p>
<a class="asset-img-link" style="display: inline;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b017ee484c6f7970d-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b017ee484c6f7970d image-full" title="Result_Before" src="/assets/image_76585.jpg" border="0" alt="Result_Before" /></a></p>
<p>3)<strong> Start AutoCAD 2013 : </strong>Navigate to the “%appdata%\Autodesk\ApplicationPlugins” folder and open the Visual Studio solution under “AppMetricsTestPlugin1.bundle\Contents\Source” sub folder. Set the path to AutoCAD 2013 as the external program to launch, by changing the project settings just as you would do for any AutoCAD .Net plugin. Hit F5 key to launch AutoCAD 2013 and to load the bundle.</p>
<p>4)&nbsp;<strong>Provide Contact Details:</strong> On startup, the test plugin displays a form prompting the end-user to provide the contact details. This information is requested only if the plugin is being installed<br />for the first time and not when AutoCAD is restarted. The contact details are used for display in the Web browser as we will see a little later.</p>
<p>
<a class="asset-img-link" style="display: inline;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b017d3d0f75d6970c-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b017d3d0f75d6970c image-full" title="ContactDetails" src="/assets/image_482337.jpg" border="0" alt="ContactDetails" /></a></p>
<p>After you provide the contact details, the trial duration of the plugin is displayed in the command prompt as shown here:</p>
<p>
<a class="asset-img-link" style="display: inline;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b017ee484c894970d-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b017ee484c894970d image-full" title="InitialCommandPrompt" src="/assets/image_408178.jpg" border="0" alt="InitialCommandPrompt" /></a></p>
</p>
<p>5)&nbsp;<strong>Using the AppMetricsTest plugin : </strong>The<strong> </strong>ribbon panel for the test plugin can be found under the “Plugins” tab as is the case with most of the plugins available in the AutoCAD Exchange store. While using the commands to insert smileys in the drawing, the remaining trial duration is displayed in the command prompt as shown in the screenshot.</p>
<p>
<a class="asset-img-link" style="display: inline;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b017ee484c954970d-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b017ee484c954970d image-full" title="CommandPrompt2" src="/assets/image_212164.jpg" border="0" alt="CommandPrompt2" /></a></p>
</p>
<p><strong>6) Viewing the usage statistics in a web browser: </strong>The web browser which is already open should now display the usage metrics and the trial duration available for the application as shown in this screenshot.</p>
<p>
<a class="asset-img-link" style="display: inline;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b017ee484ca51970d-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b017ee484ca51970d image-full" title="Result" src="/assets/image_502746.jpg" border="0" alt="Result" /></a></p>
<p><strong>&nbsp;</strong></p>
</p>
<span class="asset  asset-generic at-xid-6a0167607c2431970b017d3d0f93eb970c"><a href="http://adndevblog.typepad.com/files/appmetricssample.zip">Download AppMetricsSample</a></span>
