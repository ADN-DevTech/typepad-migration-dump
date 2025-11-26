---
layout: "post"
title: "Sign Title Block of DWG file with AutoCAD IO + View &amp; Data API"
date: "2016-02-12 05:36:50"
author: "Xiaodong Liang"
categories:
  - "AutoCAD I/O"
  - "Browser"
  - "HTML5"
  - "Javascript"
  - "View and Data API"
  - "Xiaodong Liang"
original_url: "https://adndevblog.typepad.com/cloud_and_mobile/2016/02/sign-title-block-of-dwg-file-with-autocad-io-view-data-api.html "
typepad_basename: "sign-title-block-of-dwg-file-with-autocad-io-view-data-api"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/aec/xiaodong-liang.html" target="_self">Xiaodong Liang</a></p>
<p>In our accelerator event, some customers talked about one scenario that the DWG drawing will need to have the signing of relevant engineers, assessors, examiners and executives etc. Typically, the signs are filled in within the title block. As we know, the text fields of a title block is the entity <strong>Attribute</strong>. The user can fill in with the final text content. In reality, this can be a signing on the hard copy of the drawing.</p>
<p>It sounds like an idea if we could make this process digitally, especially on the client that has no AutoCAD installed. So based on this idea, I made a small porotype. It is an Node.js application that allows the user to fill in the title block fields with text or signing in an AutoCAD drawing in the browser. The signing will be merged to the drawing. The application also provides the user to download the updated drawing. Behind the scene, the client is a web browser on View &amp; Data, while the signing process is by an AutoCAD.NET package which will run by <a href="https://developer.autodesk.com/api/autocadio/v2/">AutoCAD I/O</a>.</p>
<p>This is the Demo site: <a href="http://adnxddwgsig.herokuapp.com/viewer.html">http://adnxddwgsig.herokuapp.com/viewer.html</a> .</p>
<p>The source code and README is available at <a href="https://github.com/xiaodongliang/DWG-TitleBlock-Signature">Github</a>.&#0160;The video illustrates the steps.</p>
<p><iframe allowfullscreen="" frameborder="0" height="720" src="https://www.youtube.com/embed/1lil_EyKLf8" width="1280"></iframe> &#0160;</p>
<p>&#0160;</p>
<p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb08b9687c970d-pi"><img alt="clip_image001" border="0" height="244" src="/assets/image_db920b.jpg" style="background-image: none; margin: 0px; padding-left: 0px; padding-right: 0px; display: inline; padding-top: 0px; border: 0px;" title="clip_image001" width="216" /></a><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d19ec0d1970c-pi"><img alt="clip_image002" border="0" height="81" src="/assets/image_b37445.jpg" style="background-image: none; padding-left: 0px; padding-right: 0px; display: inline; padding-top: 0px; border: 0px;" title="clip_image002" width="244" /></a></p>
<p>In this blog, I’d like to highlight a couple of technical details:</p>
<p><em>1. How to make a signing on the browser</em></p>
<p>I got a <a href="https://github.com/szimek/signature_pad">Javascript library of signature</a> which can draw smooth and beautiful signatures. It&#39;s HTML5 canvas based and uses variable width Bézier curve interpolation. It works in all modern desktop and mobile browsers and doesn&#39;t depend on any external libraries. I placed a canvas with View &amp; Data. When user signs, the code gets the streaming of the signature and sends it to the server.</p>
<p><em>2. How to make the signing</em></p>
<p>If it is a pure text, it is very straightforward. Update AttributeReference.TextString. While as to a signing, I tried a couple of ways:</p>
<p>Raster image: this requires the corresponding separate image files are available within the same folder of the DWG. That means it will take more steps such as SetReference when translating the DWG by View &amp; Data. In addition, it is hard to adjust its size to fit to the width &amp; height of the range of title block.</p>
<p>OLE file: this also requires the corresponding separate image files are available. Although there is option that can merge the image file with the drawing, it is still hard to make a nice signing on the DWG drawing.</p>
<p>Finally, I got an inspiration to analyze the pixel color of the image, make an AutoCAD Solid on the location of each colored (signing) pixel. By this way, it is flexible to adjust the Solids to fit the attribute range and does not need separate images.</p>
<p><em>3. How to locate the field of the title blocks </em></p>
<p>Obviously, View &amp; Data does not translate the information of title block and attributes. So we will need to do something. The package(AutoCAD.NET) has two commands:</p>
<p>GenerateTBJson: it analyzes the title block, gets out each attributes properties, calculates its reasonable range (width &amp; height), and makes a json array. This array will be sent to the client. When the user clicks the title block on the browser, the code will detect which attribute range the mouse hits. Then if the user inputs the new text string, or makes a signing, the corresponding json content will be updated and sent to the server.</p>
<p>updateTBFromJson: it will read the updated json array, analyze each json. If it is a signing (image), it produces the Solids and put them within the range of the attribute, and put them on one layer.</p>
<p><em>4. Best practice</em></p>
<p>To work with AutoCAD I/O, it is always a best practice to test the package with a client program firstly, say in this case, I used an .NET program, before you apply the workflow to your web application.</p>
<p>If the package needs the dynamic parameters, you can specify the URL that provides the data, or use <a href="https://github.com/szilvaa/variadic">variadic</a> which can call HTTP request within AutoCAD package.</p>
<p>Last, please note, AutoCAD I/O <strong>V1</strong> has been removed. If you have any code, please migrate to <strong>V2</strong>.</p>
