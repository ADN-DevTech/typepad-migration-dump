---
layout: "post"
title: "Check saved viewpoints of Navisworks model on-site &ndash;Part II"
date: "2012-11-24 18:59:27"
author: "Xiaodong Liang"
categories:
  - "Android"
  - "Cloud"
  - "Xiaodong Liang"
original_url: "https://adndevblog.typepad.com/cloud_and_mobile/2012/11/check-saved-viewpoints-of-navisworks-model-on-site-part-ii.html "
typepad_basename: "check-saved-viewpoints-of-navisworks-model-on-site-part-ii"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/aec/xiaodong-liang.html" target="_self">Xiaodong Liang</a></p>
<p>In the <a href="http://adndevblog.typepad.com/cloud_and_mobile/2012/11/check-saved-viewpoints-of-navisworks-model-on-site-part-i.html" target="_self">last post</a>, we completed the application at desktop, by which you can switch to each saved viewpoint, export to an image and upload it to the cloud. In this post, we will introduce the application at mobile end. The source code is available with&#0160;<span class="asset  asset-generic at-xid-6a0167607c2431970b017ee59644bd970d"><a href="http://adndevblog.typepad.com/files/nw-cm---android.zip">Download Nw-C&amp;M - Android</a></span></p>
<p>Firstly, this application needs to have the access to the cloud. Several necessary steps:</p>
<p>- Add Amazon cloud library.    <br />- Permit the application to use Internet in AndroidManifest.xml</p>
<blockquote>
<p><em>&lt;uses-permission android:name=&quot;android.permission.INTERNET&quot;/&gt;</em></p>
</blockquote>
<p>- Same to desktop application, create the client with confidential keys. Remember to remove access key or any confidential information of your cloud account if you want to share the code in public</p>
<div style="font-family: courier new; background: white; color: black; font-size: 9pt;">
<p style="margin: 0px;"><span style="line-height: 140%; color: green;">// create client of S3 to connect to the cloud&#0160;&#0160;&#0160; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">private</span><span style="line-height: 140%;"> AmazonS3Client s3Client = </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">new</span><span style="line-height: 140%;"> AmazonS3Client(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">new</span><span style="line-height: 140%;"> BasicAWSCredentials </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; ( Constants.ACCESS_KEY_ID, </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; Constants.SECRET_KEY ) ); </span></p>
</div>
<p>The Amazon SDK for Eclipse provides the libraries of the abilities of Amazon cloud such as S3, SimpleDB, EC2 etc, and also provides some samples. For experiment, you could use the SDK sample as the skeleton to save time to configure the application for cloud. I used S3_UploaderActivity. You just need to change the application name in the manifest file.</p>
<p>As we know, at cloud end, each model has one folder withn which there are saved viewpoints images. So we need to have a list view that to display the folder as group and each group has child items. The will need to use ExpandableListView. I have to say this is the most tricky in this demo :S The Item View is totally different to the ComBox, List View or Tree View of the languages of desktop. Fortunately, there are amount of posts in internet which have discussed a lot on this topic. I accommodated one of them.</p>
<p><a href="http://www.dreamincode.net/forums/topic/270612-how-to-get-started-with-expandablelistview/">http://www.dreamincode.net/forums/topic/270612-how-to-get-started-with-expandablelistview/</a></p>
<p>After connecting the cloud, we could check the bucket and dump the items information. Following are some pieces of the main code. For details, please check the source project <a href="http://adndevblog.typepad.com/files/nw-cm---android.zip">Download Nw-C&amp;M - Android</a>..</p>
<p>1. dump folder</p>
<div style="font-family: courier new; background: white; color: black; font-size: 9pt;">
<p style="margin: 0px;"><span style="line-height: 140%; color: green;">// name list of folders</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: green;">// each folder represents the name of model.</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">ListObjectsRequest request = </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;</span><span style="line-height: 140%; color: blue;">new</span><span style="line-height: 140%;"> ListObjectsRequest();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">request.setBucketName(Constants.PICTURE_BUCKET);&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: green;">// object listing </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">ObjectListing current = </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; s3Client.listObjects( request);&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: green;">//enumerate the list</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">for</span><span style="line-height: 140%;">(S3ObjectSummary objectSummary : current.getObjectSummaries()) </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// store the folder names to the list of expandable list view as</span><span style="line-height: 140%; color: green;"> group item.</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #2b91af;">String</span><span style="line-height: 140%;"> eachkey&#0160; = objectSummary.getKey();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">if</span><span style="line-height: 140%;">(eachkey.endsWith(</span><span style="line-height: 140%; color: #a31515;">&quot;/&quot;</span><span style="line-height: 140%;">))</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; folderdata.add(eachkey);&#0160;&#0160; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160; <span style="line-height: 140%; color: green;">// folderdata is used to build the structure of expandable list view </span>&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">}&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;</span></p>
</div>
2. dump images within each folder
<div style="font-family: courier new; background: white; color: black; font-size: 9pt;">
<div style="font-family: courier new; background: white; color: black; font-size: 9pt;">
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">for</span><span style="line-height: 140%;">(</span><span style="line-height: 140%; color: #2b91af;">String</span><span style="line-height: 140%;"> eachFolder : folderdata)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: #2b91af;">&#0160;&#0160; String</span><span style="line-height: 140%;"> groupName = eachFolder;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="line-height: 140%; color: green;">// connect to cloud to</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: green;">&#0160;&#0160;&#0160; // get the items within the folder</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; ListObjectsRequest request = </span><span style="line-height: 140%; color: blue;">new</span><span style="line-height: 140%;"> ListObjectsRequest();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; request.setBucketName(Constants.PICTURE_BUCKET);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; request.withPrefix(eachFolder);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; ObjectListing current = s3Client.listObjects( request); </span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="line-height: 140%; color: green;">// iterate each item to get its name </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">for</span><span style="line-height: 140%;">(S3ObjectSummary objectSummary : current.getObjectSummaries()) </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; {&#0160;&#0160; </span>&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #2b91af;">String</span><span style="line-height: 140%;"> childName = objectSummary.getKey();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: green;">&#0160;&#0160;&#0160; // exclude the folder name</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: green;">&#0160;</span><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">if</span><span style="line-height: 140%;">(childName.equals(eachFolder))</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">continue</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="line-height: 140%; color: green;">// for build child item of expandable list view</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; ExpandListChild child = </span><span style="line-height: 140%; color: blue;">new</span><span style="line-height: 140%;"> ExpandListChild();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; child.setName(childName.substring(childName.indexOf(</span><span style="line-height: 140%; color: #a31515;">&quot;/&quot;</span><span style="line-height: 140%;">) + 1,childName.length() )); </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; child.setTag(objectSummary.getKey());</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; list2.add(child);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; }&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; gru.setItems(list2);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; list.add(gru);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">}&#0160;&#0160;&#0160; </span>&#0160;</p>
</div>
</div>
<p>Finally, in the event of clicking child item of expandable list view, we chose the simplest way to view the image, by using Image View.&#0160; </p>
<div style="font-family: courier new; background: white; color: black; font-size: 9pt;">
<p style="margin: 0px;"><span style="line-height: 140%; color: green;">// get the value (image name) of the selected item</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: #2b91af;">String</span><span style="line-height: 140%;"> selectedValue = ((ExpandListChild)childItem).getTag();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: green;">&#0160;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: green;">// find download the image in the cloud bucket&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">GetObjectRequest s3Request = </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;</span><span style="line-height: 140%; color: blue;">new</span><span style="line-height: 140%;"> GetObjectRequest(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Constants.PICTURE_BUCKET, </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">selectedValue);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">S3Object s3Response = s3Client.getObject(s3Request);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">if</span><span style="line-height: 140%;"> (s3Response != </span><span style="line-height: 140%; color: blue;">null</span><span style="line-height: 140%;">)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">//display it in the image view</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; InputStream reader = </span><span style="line-height: 140%; color: blue;">new</span><span style="line-height: 140%;"> BufferedInputStream(s3Response.getObjectContent());</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; ImageView imgView = (ImageView)findViewById(R.id.imageView1);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; imgView.setImageBitmap(BitmapFactory.decodeStream(reader));</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">}</span></p>
</div>
<p>Run the application, click [Receive Data]. The items in the cloud bucket will be listed.</p>
<p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b017ee5962d2d970d-pi"><img alt="image" border="0" height="311" src="/assets/image_a2b818.jpg" style="display: block; float: none; margin-left: auto; margin-right: auto; border: 0px;" title="image" width="238" /></a> </p>
<p>Expand any of them and click the child item, the corresponding image of saved viewpoint will be displayed.</p>
<p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b017d3e215e6b970c-pi"><img alt="image" border="0" height="327" src="/assets/image_3ffdb4.jpg" style="display: block; float: none; margin-left: auto; margin-right: auto; border: 0px;" title="image" width="231" /></a> </p>
<p>&#0160;</p>
<p>&#0160;</p>
<p>&#0160;</p>
<p><span style="line-height: 120%; font-family: &#39;courier new&#39;, courier; font-size: 8pt;">&#0160; <br /></span></p>
