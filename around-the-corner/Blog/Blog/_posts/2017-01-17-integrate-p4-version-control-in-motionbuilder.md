---
layout: "post"
title: "Integrate p4 Version Control in Motionbuilder"
date: "2017-01-17 15:21:00"
author: "Zhong Wu"
categories:
  - "Autodesk"
  - "MotionBuilder"
  - "Python"
  - "Zhong Wu"
original_url: "https://around-the-corner.typepad.com/adn/2017/01/integrate-p4-version-control-in-motionbuilder.html "
typepad_basename: "integrate-p4-version-control-in-motionbuilder"
typepad_status: "Publish"
---

<p>Version Control is an important feature for a team to work together, and Motionbuilder has its own version control manager that uses simplified versions of the Perforce and Microsoft Visual SourceSafe version control software products. </p>  <p>But, how to set it up? How to use the provided API to operate with version control? There are very few information about this, but recently, I spent some time on this, and in what follows I will talk about how to set it up and how to use these API to help you get started. </p>  <p>How to set it up? Let’s use Perforce as example, here are 4 steps: </p>  <ul>   <li>Install Perforce if you do not have, and open it, connect to a server with a specified workspace.      <br /><a href="http://around-the-corner.typepad.com/.a/6a0163057a21c8970d01bb096df0f0970d-pi"><img title="image" style="border-top: 0px; border-right: 0px; background-image: none; border-bottom: 0px; float: none; padding-top: 0px; padding-left: 0px; margin-left: auto; border-left: 0px; display: block; padding-right: 0px; margin-right: auto" border="0" alt="image" src="/assets/image_e293e5.jpg" width="386" height="222" /></a> </li>    <li>Open the file \\MyDocuments\MB\2017-x64\config\*.Application.txt, and add following:      <pre class="brush:python;toolbar: false;">[Version Control]
AskOnStartup = Yes
CurrentManager = Perforce
Enabled = Yes</pre>
  </li>

  <li>Open the file \\MyDocuments\MB\2017-x64\config\*.Perforce.txt, and add following, please make sure to change the User and Workspace to the one you used in P4: 
    <pre class="brush:python;toolbar: false;">[Perforce]
CheckOutOnLoad = ASK
UploadOnSave = ASK
AddOnNewSave = ASK
CheckInOnClose = ASK
User = MY_USERNAME
Workspace = MY_WORKSPACE</pre>
  </li>

  <li>Run Motionbuilder, you should see a “Version Control” Menu as following picture. <a href="http://around-the-corner.typepad.com/.a/6a0163057a21c8970d01b7c8cad65c970b-pi"><img title="image" style="border-top: 0px; border-right: 0px; background-image: none; border-bottom: 0px; float: none; padding-top: 0px; padding-left: 0px; margin-left: auto; border-left: 0px; display: block; padding-right: 0px; margin-right: auto" border="0" alt="image" src="/assets/image_a21a9f.jpg" width="387" height="144" /></a> <a href="http://around-the-corner.typepad.com/.a/6a0163057a21c8970d01b8d254ff74970c-pi"><img title="image" style="border-top: 0px; border-right: 0px; background-image: none; border-bottom: 0px; float: none; padding-top: 0px; padding-left: 0px; margin-left: auto; border-left: 0px; display: block; padding-right: 0px; margin-right: auto" border="0" alt="image" src="/assets/image_1fd1a1.jpg" width="387" height="202" /></a> </li>
</ul>

<p>Until now, everything should be all set, and you can use the integrated version control menu to work on FBX file with Perforce. </p>

<p>But, how I can use the API to work with version control tools? Within Motionbuilder, it provides 4 classes: 
  <br /><b><b><code><i>- FBAssetMng</i></code></b></b> 

  <br /><b><b><code><i>- FBAssetItem</i></code></b></b> 

  <br /><b><b><code><i>- FBAssetFile</i></code></b></b> 

  <br /><b><b><code><i>- FBAssetFolder</i></code></b></b> </p>

<p>FBAssetMng is used to access asset manager functionity to get files locally or from a server. You cannot instance this class because it’s a virtual class, instead, you can get it from FBSystem().AssetManager if there is already an asset manager set up. When you get the asset manager, you can use the following method to get the folder or file: </p>

<ul>
  <li><i><i><code>GetAssetFileFromLocalPath() </code></i></i></li>

  <li><i><i><code>GetAssetFile() </code></i></i></li>

  <li><i><i><code>GetAssetFolderFromLocalPath() </code></i></i></li>

  <li><i><i><code>GetAssetFolder() </code></i></i></li>
</ul>

<p>Here is a simple example to use these methods, please make sure using “/” instead of “\”: </p>

<pre class="brush:python;toolbar: false;">srvPath = &quot;//depot/mytest/ test.fbx&quot;
localPath = &quot;C:/mytest/test.fbx&quot;
asset_mgn = FBSystem().AssetManager
asset_file1 = asset_mgn.GetAssetFileFromLocalPath(localPath)
asset_file2 = asset_mgn.GetAssetFile(srvPath)</pre>

<p>Or you can use BrowseForFile() to browser a FBX file. </p>

<p>When you got a folder or file, you can do a couple of operations per your request, usually, the following methods are often used: </p>

<ul>
  <li><i><code><i>CheckIn</i>()</code></i> </li>

  <li><i><code><i>CheckOut</i>()</code></i> </li>

  <li><i><code><i>GetLatest</i>()</code></i> </li>

  <li><i><code><i>UndoCheckOut</i>()</code></i> </li>
</ul>

<p>These methods are pretty straightforward, please refer the <a href="http://docs.autodesk.com/MOBPRO/2017/ENU/MotionBuilder-Developer-Help/index.html#!/url=./cpp_ref/class_o_r_s_d_k2017_1_1_f_b_asset_item.html">API help doc</a> for each of these methods if you are interested. </p>
