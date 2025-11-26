---
layout: "post"
title: "Edge detection of image using Azure Webrole + Android OpenGL ES"
date: "2014-10-15 04:33:55"
author: "Balaji"
categories:
  - ".NET"
  - "Balaji Ramamoorthy"
  - "Cloud"
  - "Madhukar Moogala"
  - "Off-topic"
  - "Virupaksha Aithal"
original_url: "https://adndevblog.typepad.com/autocad/2014/10/edge-detection-of-image-using-azure-webrole-android-opengl-es.html "
typepad_basename: "edge-detection-of-image-using-azure-webrole-android-opengl-es"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/balaji-ramamoorthy.html" target="_self">Balaji Ramamoorthy</a>, <a href="http://adndevblog.typepad.com/autocad/madhukar-moogala.html" target="madhukar">Madhukar Moogala</a>, <a href="http://adndevblog.typepad.com/autocad/virupaksha-aithal.html" target="viru">Virupaksha Aithal</a></p>
<p>In the recently concluded Apps Hackathon, DevTech teams worked on different cloud and mobile based projects of their choice. The DevTech team in India chose to work on an image edge detection web service by creating a Microsoft Azure Webrole that exposes REST API and to create an Android client for it to display the edges. In this blog post, we are sharing the work that we did. Most of the webrole had already been created as a hobby project and a significant portion of the work during the Apps Hackathon went into consuming the web service in an Android OpenGL ES project.</p>
<p>Here is a sample image that was uploaded and the edges in an Android tablet</p>

</p>
<a class="asset-img-link"  href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d07dcb79970c-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b01b8d07dcb79970c img-responsive" alt="SampleImage" title="SampleImage" src="/assets/image_195788.jpg" style="margin: 0px 5px 5px 0px;" /></a>

</p>
<a class="asset-img-link"  href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d07dcb84970c-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b01b8d07dcb84970c img-responsive" alt="Screenshot_AndroidClientOutput" title="Screenshot_AndroidClientOutput" src="/assets/image_198726.jpg" style="margin: 0px 5px 5px 0px;" /></a>

</p>
<p>As a basis for the algorithm needed for image edge detection, we converted the code from this excellent blog post into a Azure web role and provided REST access to it.</p>
<p><a href="http://www.codeproject.com/Articles/93642/Canny-Edge-Detection-in-C">Canny edge detection of images</a></p>
<p>The Azure webrole project, android client project, a sample image and a screenshot of the output in an Android tablet can be downloaded here :</p>


<span class="asset  asset-generic at-xid-6a0167607c2431970b01b8d07dcaf6970c img-responsive"><a href="http://adndevblog.typepad.com/files/edserviceazurewebrole.zip">Download EDServiceAzureWebRole</a></span>

</p>

<span class="asset  asset-generic at-xid-6a0167607c2431970b01bb0798ee1d970d img-responsive"><a href="http://adndevblog.typepad.com/files/edandroidclient.zip">Download EDAndroidClient</a></span>

</p>

<span class="asset  asset-generic at-xid-6a0167607c2431970b01bb0798ee26970d img-responsive"><a href="http://adndevblog.typepad.com/files/ds1.ogl">Download DS1</a></span>

</p>

<p>Here are some details about the files provided for download :</p>
<p>EDServiceAzureWebRole.zip includes source code for a Azure web role that does edge detection of images and provides output as lines. Canny edge detection algorithm as implemented in the blog post is used as a basis for this web service. REST API has been provided to upload an image and get access to the edge detected output. Here are some details about the webrole.</p>
<p>After the EDService Webrole is hosted in Azure, you will get a URL similar to this : http://168.63.186.125:8080/EDService.svc/</p>
<p>The REST API exposed by the web role are :</p>
<p>1) GET : http://168.63.186.125:8080/EDService.svc/Datasets</p>
<p>Provides the id of all the image datasets that have been uploaded for edge detection</p>
<p>2) GET : http://168.63.186.125:8080/EDService.svc/Dataset/{id}</p>
<p>Gets the edge information of a dataset that is identified using {id}</p>
<p>A sample edge information obtained for a dataset is provided in "DS1.ogl" file.</p>
<p>3) POST : http://168.63.186.125:8080/EDService.svc/Image/{id}</p>
<p>Uploads the image as a JSON data represented by "ImageData" class.</p>
<p>Please refer to IEDService.cs for the contents of ImageData class that represents an image</p>
<p>4) GET : http://168.63.186.125:8080/EDService.svc/ClearDataset/{id}</p>
<p>Clears the dataset identified by the id. If you no longer need the edge detected image to be stored at the server, this will help clear it.</p>
<p>DS1.ogl : A sample file that contains the edge information as obtained for the sample image from the webrole. This sample data is used in the Android application for display.</p>
<p>EDAndroidClient.zip includes an Android project that uses OpenGL ES 2.0 for displaying the edges as obtained from the webrole.</p>
<p>SampleImage.png : A sample image that was uploaded to the Webrole for edge detection. The edges were displayed in an Android application using OpenGL ES 2.0</p>
<p>Screenshot_AndroidClientOutput.png : A screenshot of the Android client as it displays the edges as obtained from the webrole.</p>
<p>&nbsp;</p>
