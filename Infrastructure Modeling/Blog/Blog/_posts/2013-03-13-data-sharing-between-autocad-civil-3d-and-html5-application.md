---
layout: "post"
title: "Data sharing between AutoCAD Civil 3D and HTML5 application"
date: "2013-03-13 01:33:00"
author: "Partha Sarkar"
categories:
  - ".NET"
  - "AutoCAD Civil 3D 2012"
  - "AutoCAD Civil 3D 2013"
  - "Cloud"
  - "HTML5"
  - "Mobile"
  - "Partha Sarkar"
original_url: "https://adndevblog.typepad.com/infrastructure/2013/03/data-sharing-between-autocad-civil-3d-and-html5-application.html "
typepad_basename: "data-sharing-between-autocad-civil-3d-and-html5-application"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/infrastructure/partha-sarkar.html" target="_self">Partha Sarkar</a></p>
<p>In the
process of exploring HTML5 canvas and draw geometries, I thought how about
‘creating a HTML5 application to show a Civil 3D Pipe Network geometry’.&#0160; </p>
<p>With this
goal in mind, I first approached to create a simple Civil 3D .NET application to
select a Pipe, get the Pipe Network and export the Pipe coordinates from the
selected network to an external file (TXT file). We can use the following Civil
3D .NET API to get the Pipe’s start and end points –</p>
<p><strong>Pipe.StartPoint</strong>
</p>
<p><strong>Pipe.EndPoint</strong></p>
<p>So, the first
part was easy to create a TXT file and store all the StartPoint and EndPoint of
the Pipes in a selected Pipe Network.</p>
<p>In the second
part, I wanted to read the TXT file with Pipe Geometry from Civil 3D. I found
the <a href="http://www.w3.org/TR/FileAPI/">FileReader</a>
in HTML5 which provides an API to select files and access their data.</p>
<p>After
accessing the data, I had to parse it and create an array in JavaScript. In the
next part, it was easy to get the HTML5 canvas and the context and loop through
the array storing the pipe network vertices and create line using HTML5 <strong>context.lineTo()</strong>
API</p>
<p><span style="font-family: Verdana, Arial, Helvetica, sans-serif; font-size: 10px;">&#0160;if (cleanlineVertices.length &gt; 3)</span></p>
<pre>{<br />	context.beginPath();<br />	context.moveTo(cleanlineVertices[0], cleanlineVertices[1]);<br />	for (var i = 2; i &lt; (cleanlineVertices.length - 1); i+=2)<br />	{		<br />	 context.lineTo(cleanlineVertices[i], cleanlineVertices[i+1]);	<br />	}<br />		<br />	// Set Line width<br />	context.lineWidth = 5;<br />		<br />	// Set Line color<br />	context.strokeStyle = &#39;#fa00ff&#39;;<br />	context.stroke();<br />}</pre>
<p>&#0160;</p>
<p>In this video
you will see the workflow and a quick demo of this application.</p>
<p><iframe frameborder="0" height="281" src="http://www.youtube.com/embed/Lb62LTNfXSI?feature=oembed" width="500"></iframe>&#0160;</p>
So, we can share data from AutoCAD Civil 3D and a HTML5 can show us the
geometry in the actual project site and all the stakeholders can verify if the
network is passing through the desired location &#0160;:) !
