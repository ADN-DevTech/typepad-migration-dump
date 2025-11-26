---
layout: "post"
title: "Giving a try at HTML5 canvas and JavaScript Part - II"
date: "2013-01-04 01:51:11"
author: "Partha Sarkar"
categories:
  - "HTML"
  - "HTML5"
  - "Javascript"
original_url: "https://adndevblog.typepad.com/cloud_and_mobile/2013/01/giving-a-try-at-html5-canvas-and-javascript-part-ii.html "
typepad_basename: "giving-a-try-at-html5-canvas-and-javascript-part-ii"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/infrastructure/partha-sarkar.html" target="_self">Partha Sarkar</a></p>
<p>&nbsp;</p>
<p>
<strong>Welcome</strong> back after the holiday season and a very <strong>Happy New Year</strong> to you all !</p>
<p>Philippe started a new topic with <a href="http://adndevblog.typepad.com/cloud_and_mobile/2012/12/giving-a-try-at-html5-canvas-and-javascript.html" target="_self">Giving a try at HTML5 canvas and Javascript</a> before the Christmas and year-end holidays started. In the above post we saw how quickly we can build simple HTML5 application to draw planer geometries like Line and Circle using mouse click. Let's add some more fun! </p>
<p>Let's see how we can draw these types of geometric objects from user input and save the resulting geometries as image.</p>
<p>&nbsp;</p>

<!DOCTYPE html>
<html>
<head>
<style type="text/css">
#myCanvas {
	border: thin solid;
}
button {
	display: block;
	width: 100px; 
}
#myButtons {
	float: down;
}
</style>
<script type="text/javascript">


  //
  // function
  //
  function myDrawing(x1, y1, x2, y2) {
    // setup the canvas and context
    var canvas = document.getElementById("myCanvas");
    var context = canvas.getContext("2d");
    context.clearRect(0, 0, canvas.width, canvas.height);
    context.beginPath();
    context.moveTo(x1.value, y1.value);
    context.lineTo(x2.value, y2.value);

    // Set Line width
    context.lineWidth = 5;

    // Set Line color
    context.strokeStyle = '#fa00ff';
    context.stroke();
  }

  //
  // function
  //
  function saveImage() {
    var canvas = document.getElementById("myCanvas");
    var context = canvas.getContext('2d');
    // save canvas image as Data URL	
    var imageDataURL = canvas.toDataURL("image/png");

    // set myCanvasImage image src to dataURL to save it as an Image
    document.getElementById('myCanvasImage').src = imageDataURL;

    // if you want to show the resulting image 
    // in the current window replacing everyting then 
    // use the following code

    //window.location = imageDataURL;
  }

  //
</script>
</head>
<body>
<canvas id="myCanvas" width="450" height="300"
style="border:5px solid #000000;">
Your browser does not support the new HTML5 canvas element.
</canvas>
<img id="myCanvasImage">

<form>
X1: <input type="text" name="x1"> Y1: <input type="text" name="y1"><br>
X2: <input type="text" name="x2"> Y2: <input type="text" name="y2"><br>
<input type="button" onclick="myDrawing(x1,y1,x2,y2)" value="Draw Line">
<input type="button" onclick="saveImage()" value="Save Image">
</form>

</body>
</html>



<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="color: blue; line-height:140%;">&lt;!</span><span style="color: maroon; line-height:140%;">DOCTYPE</span><span style="line-height:140%;"> </span><span style="color: red; line-height:140%;">html</span><span style="color: blue; line-height:140%;">&gt;</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height:140%;">&lt;</span><span style="color: maroon; line-height:140%;">html</span><span style="color: blue; line-height:140%;">&gt;</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height:140%;">&lt;</span><span style="color: maroon; line-height:140%;">head</span><span style="color: blue; line-height:140%;">&gt;</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height:140%;">&lt;</span><span style="color: maroon; line-height:140%;">style</span><span style="line-height:140%;"> </span><span style="color: red; line-height:140%;">type</span><span style="color: blue; line-height:140%;">=&quot;text/css&quot;&gt;</span></p>
<p style="margin: 0px;"><span style="color: maroon; line-height:140%;">#myCanvas</span><span style="line-height:140%;"> {</span></p>
<p style="margin: 0px;"><span style="line-height:140%;">&nbsp;</span><span style="color: red; line-height:140%;">border</span><span style="line-height:140%;">: </span><span style="color: blue; line-height:140%;">thin</span><span style="line-height:140%;"> </span><span style="color: blue; line-height:140%;">solid</span><span style="line-height:140%;">;</span></p>
<p style="margin: 0px;"><span style="line-height:140%;">}</span></p>
<p style="margin: 0px;"><span style="color: maroon; line-height:140%;">button</span><span style="line-height:140%;"> {</span></p>
<p style="margin: 0px;"><span style="line-height:140%;">&nbsp;</span><span style="color: red; line-height:140%;">display</span><span style="line-height:140%;">: </span><span style="color: blue; line-height:140%;">block</span><span style="line-height:140%;">;</span></p>
<p style="margin: 0px;"><span style="line-height:140%;">&nbsp;</span><span style="color: red; line-height:140%;">width</span><span style="line-height:140%;">: </span><span style="color: blue; line-height:140%;">100px</span><span style="line-height:140%;">; </span></p>
<p style="margin: 0px;"><span style="line-height:140%;">}</span></p>
<p style="margin: 0px;"><span style="color: maroon; line-height:140%;">#myButtons</span><span style="line-height:140%;"> {</span></p>
<p style="margin: 0px;"><span style="line-height:140%;">&nbsp;</span><span style="color: red; line-height:140%;">float</span><span style="line-height:140%;">: </span><span style="color: blue; line-height:140%;">down</span><span style="line-height:140%;">;</span></p>
<p style="margin: 0px;"><span style="line-height:140%;">}</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height:140%;">&lt;/</span><span style="color: maroon; line-height:140%;">style</span><span style="color: blue; line-height:140%;">&gt;</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height:140%;">&lt;</span><span style="color: maroon; line-height:140%;">script</span><span style="line-height:140%;"> </span><span style="color: red; line-height:140%;">type</span><span style="color: blue; line-height:140%;">=&quot;text/javascript&quot;&gt;</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height:140%;">&nbsp; </span><span style="color: #006400; line-height:140%;">//</span></p>
<p style="margin: 0px;"><span style="line-height:140%;">&nbsp; </span><span style="color: #006400; line-height:140%;">// function</span></p>
<p style="margin: 0px;"><span style="line-height:140%;">&nbsp; </span><span style="color: #006400; line-height:140%;">//</span></p>
<p style="margin: 0px;"><span style="line-height:140%;">&nbsp; </span><span style="color: blue; line-height:140%;">function</span><span style="line-height:140%;"> myDrawing(x1, y1, x2, y2) {</span></p>
<p style="margin: 0px;"><span style="line-height:140%;">&nbsp; &nbsp; </span><span style="color: #006400; line-height:140%;">// setup the canvas and context</span></p>
<p style="margin: 0px;"><span style="line-height:140%;">&nbsp; &nbsp; </span><span style="color: blue; line-height:140%;">var</span><span style="line-height:140%;"> canvas = document.getElementById(</span><span style="color: maroon; line-height:140%;">&quot;myCanvas&quot;</span><span style="line-height:140%;">);</span></p>
<p style="margin: 0px;"><span style="line-height:140%;">&nbsp; &nbsp; </span><span style="color: blue; line-height:140%;">var</span><span style="line-height:140%;"> context = canvas.getContext(</span><span style="color: maroon; line-height:140%;">&quot;2d&quot;</span><span style="line-height:140%;">);</span></p>
<p style="margin: 0px;"><span style="line-height:140%;">&nbsp; &nbsp; context.clearRect(0, 0, canvas.width, canvas.height);</span></p>
<p style="margin: 0px;"><span style="line-height:140%;">&nbsp; &nbsp; context.beginPath();</span></p>
<p style="margin: 0px;"><span style="line-height:140%;">&nbsp; &nbsp; context.moveTo(x1.value, y1.value);</span></p>
<p style="margin: 0px;"><span style="line-height:140%;">&nbsp; &nbsp; context.lineTo(x2.value, y2.value);</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height:140%;">&nbsp; &nbsp; </span><span style="color: #006400; line-height:140%;">// Set Line width</span></p>
<p style="margin: 0px;"><span style="line-height:140%;">&nbsp; &nbsp; context.lineWidth = 5;</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height:140%;">&nbsp; &nbsp; </span><span style="color: #006400; line-height:140%;">// Set Line color</span></p>
<p style="margin: 0px;"><span style="line-height:140%;">&nbsp; &nbsp; context.strokeStyle = </span><span style="color: maroon; line-height:140%;">'#fa00ff'</span><span style="line-height:140%;">;</span></p>
<p style="margin: 0px;"><span style="line-height:140%;">&nbsp; &nbsp; context.stroke();</span></p>
<p style="margin: 0px;"><span style="line-height:140%;">&nbsp; }</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height:140%;">&nbsp; </span><span style="color: #006400; line-height:140%;">//</span></p>
<p style="margin: 0px;"><span style="line-height:140%;">&nbsp; </span><span style="color: #006400; line-height:140%;">// function</span></p>
<p style="margin: 0px;"><span style="line-height:140%;">&nbsp; </span><span style="color: #006400; line-height:140%;">//</span></p>
<p style="margin: 0px;"><span style="line-height:140%;">&nbsp; </span><span style="color: blue; line-height:140%;">function</span><span style="line-height:140%;"> saveImage() {</span></p>
<p style="margin: 0px;"><span style="line-height:140%;">&nbsp; &nbsp; </span><span style="color: blue; line-height:140%;">var</span><span style="line-height:140%;"> canvas = document.getElementById(</span><span style="color: maroon; line-height:140%;">&quot;myCanvas&quot;</span><span style="line-height:140%;">);</span></p>
<p style="margin: 0px;"><span style="line-height:140%;">&nbsp; &nbsp; </span><span style="color: blue; line-height:140%;">var</span><span style="line-height:140%;"> context = canvas.getContext(</span><span style="color: maroon; line-height:140%;">'2d'</span><span style="line-height:140%;">);</span></p>
<p style="margin: 0px;"><span style="line-height:140%;">&nbsp; &nbsp; </span><span style="color: #006400; line-height:140%;">// save canvas image as Data URL </span></p>
<p style="margin: 0px;"><span style="line-height:140%;">&nbsp; &nbsp; </span><span style="color: blue; line-height:140%;">var</span><span style="line-height:140%;"> imageDataURL = canvas.toDataURL(</span><span style="color: maroon; line-height:140%;">&quot;image/png&quot;</span><span style="line-height:140%;">);</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height:140%;">&nbsp; &nbsp; </span><span style="color: #006400; line-height:140%;">// set myCanvasImage image src to dataURL to save it as an Image</span></p>
<p style="margin: 0px;"><span style="line-height:140%;">&nbsp; &nbsp; document.getElementById(</span><span style="color: maroon; line-height:140%;">'myCanvasImage'</span><span style="line-height:140%;">).src = imageDataURL;</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height:140%;">&nbsp; &nbsp; </span><span style="color: #006400; line-height:140%;">// if you want to show the resulting image </span></p>
<p style="margin: 0px;"><span style="line-height:140%;">&nbsp; &nbsp; </span><span style="color: #006400; line-height:140%;">// in the current window replacing everyting then </span></p>
<p style="margin: 0px;"><span style="line-height:140%;">&nbsp; &nbsp; </span><span style="color: #006400; line-height:140%;">// use the following code</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height:140%;">&nbsp; &nbsp; </span><span style="color: #006400; line-height:140%;">//window.location = imageDataURL;</span></p>
<p style="margin: 0px;"><span style="line-height:140%;">&nbsp; }</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height:140%;">&nbsp; </span><span style="color: #006400; line-height:140%;">//</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height:140%;">&lt;/</span><span style="color: maroon; line-height:140%;">script</span><span style="color: blue; line-height:140%;">&gt;</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height:140%;">&lt;/</span><span style="color: maroon; line-height:140%;">head</span><span style="color: blue; line-height:140%;">&gt;</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height:140%;">&lt;</span><span style="color: maroon; line-height:140%;">body</span><span style="color: blue; line-height:140%;">&gt;</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height:140%;">&lt;</span><span style="color: maroon; line-height:140%;">canvas</span><span style="line-height:140%;"> </span><span style="color: red; line-height:140%;">id</span><span style="color: blue; line-height:140%;">=&quot;myCanvas&quot;</span><span style="line-height:140%;"> </span><span style="color: red; line-height:140%;">width</span><span style="color: blue; line-height:140%;">=&quot;450&quot;</span><span style="line-height:140%;"> </span><span style="color: red; line-height:140%;">height</span><span style="color: blue; line-height:140%;">=&quot;300&quot;</span></p>
<p style="margin: 0px;"><span style="color: red; line-height:140%;">style</span><span style="color: blue; line-height:140%;">=&quot;</span><span style="color: red; line-height:140%;">border</span><span style="color: blue; line-height:140%;">:5px solid #000000;&quot;&gt;</span></p>
<p style="margin: 0px;"><span style="line-height:140%;">Your browser does not support the new HTML5 canvas element.</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height:140%;">&lt;/</span><span style="color: maroon; line-height:140%;">canvas</span><span style="color: blue; line-height:140%;">&gt;</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height:140%;">&lt;</span><span style="color: maroon; line-height:140%;">img</span><span style="line-height:140%;"> </span><span style="color: red; line-height:140%;">id</span><span style="color: blue; line-height:140%;">=&quot;myCanvasImage&quot;&gt;</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="color: blue; line-height:140%;">&lt;</span><span style="color: maroon; line-height:140%;">form</span><span style="color: blue; line-height:140%;">&gt;</span></p>
<p style="margin: 0px;"><span style="line-height:140%;">X1: </span><span style="color: blue; line-height:140%;">&lt;</span><span style="color: maroon; line-height:140%;">input</span><span style="line-height:140%;"> </span><span style="color: red; line-height:140%;">type</span><span style="color: blue; line-height:140%;">=&quot;text&quot;</span><span style="line-height:140%;"> </span><span style="color: red; line-height:140%;">name</span><span style="color: blue; line-height:140%;">=&quot;x1&quot;&gt;</span><span style="line-height:140%;"> Y1: </span><span style="color: blue; line-height:140%;">&lt;</span><span style="color: maroon; line-height:140%;">input</span><span style="line-height:140%;"> </span><span style="color: red; line-height:140%;">type</span><span style="color: blue; line-height:140%;">=&quot;text&quot;</span><span style="line-height:140%;"> </span><span style="color: red; line-height:140%;">name</span><span style="color: blue; line-height:140%;">=&quot;y1&quot;&gt;&lt;</span><span style="color: maroon; line-height:140%;">br</span><span style="color: blue; line-height:140%;">&gt;</span></p>
<p style="margin: 0px;"><span style="line-height:140%;">X2: </span><span style="color: blue; line-height:140%;">&lt;</span><span style="color: maroon; line-height:140%;">input</span><span style="line-height:140%;"> </span><span style="color: red; line-height:140%;">type</span><span style="color: blue; line-height:140%;">=&quot;text&quot;</span><span style="line-height:140%;"> </span><span style="color: red; line-height:140%;">name</span><span style="color: blue; line-height:140%;">=&quot;x2&quot;&gt;</span><span style="line-height:140%;"> Y2: </span><span style="color: blue; line-height:140%;">&lt;</span><span style="color: maroon; line-height:140%;">input</span><span style="line-height:140%;"> </span><span style="color: red; line-height:140%;">type</span><span style="color: blue; line-height:140%;">=&quot;text&quot;</span><span style="line-height:140%;"> </span><span style="color: red; line-height:140%;">name</span><span style="color: blue; line-height:140%;">=&quot;y2&quot;&gt;&lt;</span><span style="color: maroon; line-height:140%;">br</span><span style="color: blue; line-height:140%;">&gt;</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height:140%;">&lt;</span><span style="color: maroon; line-height:140%;">input</span><span style="line-height:140%;"> </span><span style="color: red; line-height:140%;">type</span><span style="color: blue; line-height:140%;">=&quot;button&quot;</span><span style="line-height:140%;"> </span><span style="color: red; line-height:140%;">onclick</span><span style="color: blue; line-height:140%;">=&quot;myDrawing(x1,y1,x2,y2)&quot;</span><span style="line-height:140%;"> </span><span style="color: red; line-height:140%;">value</span><span style="color: blue; line-height:140%;">=&quot;Draw Line&quot;&gt;</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height:140%;">&lt;</span><span style="color: maroon; line-height:140%;">input</span><span style="line-height:140%;"> </span><span style="color: red; line-height:140%;">type</span><span style="color: blue; line-height:140%;">=&quot;button&quot;</span><span style="line-height:140%;"> </span><span style="color: red; line-height:140%;">onclick</span><span style="color: blue; line-height:140%;">=&quot;saveImage()&quot;</span><span style="line-height:140%;"> </span><span style="color: red; line-height:140%;">value</span><span style="color: blue; line-height:140%;">=&quot;Save Image&quot;&gt;</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height:140%;">&lt;/</span><span style="color: maroon; line-height:140%;">form</span><span style="color: blue; line-height:140%;">&gt;</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="color: blue; line-height:140%;">&lt;/</span><span style="color: maroon; line-height:140%;">body</span><span style="color: blue; line-height:140%;">&gt;</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height:140%;">&lt;/</span><span style="color: maroon; line-height:140%;">html</span><span style="color: blue; line-height:140%;">&gt;</span></p>
</div>
