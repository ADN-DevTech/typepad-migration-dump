---
layout: "post"
title: "Monitor environment temperature with Arduino and LM35 sensor"
date: "2015-09-14 01:45:54"
author: "Daniel Du"
categories: []
original_url: "https://adndevblog.typepad.com/cloud_and_mobile/2015/09/monitor-environment-temperature-with-arduino-and-lm35-sensor.html "
typepad_basename: "monitor-environment-temperature-with-arduino-and-lm35-sensor"
typepad_status: "Draft"
---

<p>By <a href="http://adndevblog.typepad.com/cloud_and_mobile/daniel-du.html">Daniel Du</a></p>  <p>In last post I get my first Arduino experience and tried 123D circuits, I will keep moving to try some sensors. I will start from LM35 temperature sensor, which can be used to monitor current temperature. LM35 sensor is included in Arduino Starter Kit.</p>  <p>Let’s connect the hardware first, put the LM35 sensor on breadboard, and connect it with Arduino as below. </p>  <p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb0871d0b0970d-pi"><img title="Screen Shot 2015-09-14 at 3.47.34 PM" style="border-top: 0px; border-right: 0px; background-image: none; border-bottom: 0px; padding-top: 0px; padding-left: 0px; border-left: 0px; display: inline; padding-right: 0px" border="0" alt="Screen Shot 2015-09-14 at 3.47.34 PM" src="/assets/image_3a0f3a.jpg" width="468" height="323" /></a></p>  <p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d15763ad970c-pi"><img title="992012145" style="border-top: 0px; border-right: 0px; background-image: none; border-bottom: 0px; padding-top: 0px; padding-left: 0px; border-left: 0px; display: inline; padding-right: 0px" border="0" alt="992012145" src="/assets/image_0b44a1.jpg" width="342" height="454" /></a></p>  <p>(You may notice the CC3000 WIFI shield extension on top of Arduino, I will use it to connect to internet later, let’s ignore it first in this post)</p>  <p>The left leg of LM35 is power, we connect it to the 5v pin on Arduino with a red wire, the right leg is ground end, we connect it to GND pin on Arduino with a black wire, the middle leg is value out, let’s connect it to A0 on Arduino with a blue wire.( if it does not work for you, you probably reverse the left and right side of legs of sensor)</p>  <p>Now, let’s write some code in Arduino IDE, </p>  <pre class="csharpcode"><span class="kwrd">float</span> temp = 0;


<span class="rem">// the setup routine runs once when you press reset:</span>
<span class="kwrd">void</span> setup() {</pre>

<p>Serial.begin(115200);
  <br />Serial.println(F(&quot;reading temperature begin. \n&quot;));

  <br /></p>

<pre class="csharpcode">}

<span class="rem">// the loop routine runs over and over again forever:</span>
<span class="kwrd">void</span> loop() {
 
  <span class="kwrd">static</span> unsigned <span class="kwrd">long</span> sensortStamp = 0;
  
  <span class="kwrd">if</span>(millis() - sensortStamp &gt; 100){
    sensortStamp = millis();
    <span class="rem">// read the LM35 sensor value and convert to the degrees every 100ms.</span>

    <span class="kwrd">int</span> reading = analogRead(0); //note that we connect the value end of LM35 to A0 pin
    temp = reading *0.0048828125*100;
    Serial.print(F(<span class="str">&quot;Real Time Temp: &quot;</span>)); 
    Serial.println(temp); 
  }
  
}</pre>
<style type="text/css">
.csharpcode, .csharpcode pre
{
	font-size: small;
	color: black;
	font-family: consolas, "Courier New", courier, monospace;
	background-color: #ffffff;
	/*white-space: pre;*/
}
.csharpcode pre { margin: 0em; }
.csharpcode .rem { color: #008000; }
.csharpcode .kwrd { color: #0000ff; }
.csharpcode .str { color: #006080; }
.csharpcode .op { color: #0000c0; }
.csharpcode .preproc { color: #cc6633; }
.csharpcode .asp { background-color: #ffff00; }
.csharpcode .html { color: #800000; }
.csharpcode .attr { color: #ff0000; }
.csharpcode .alt 
{
	background-color: #f4f4f4;
	width: 100%;
	margin: 0em;
}
.csharpcode .lnum { color: #606060; }</style>

<p>&#160;</p>

<p>Now go ahead to upload the code to Arduino and open serial monitor from “Tool” menu of Arduino IDE. This code snippet read current temperature value and display it to serial port every 100 milliseconds. If everything goes OK, you will see the temperature is output. If you heat up the sensor with a hair dryer, you will see the temperature is raising too. <img class="wlEmoticon wlEmoticon-smile" style="border-top-style: none; border-bottom-style: none; border-right-style: none; border-left-style: none" alt="Smile" src="/assets/image_1f92e0.jpg" /></p>

<p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d15763b7970c-pi"><img title="Screen Shot 2015-09-14 at 4.26.36 PM" style="border-top: 0px; border-right: 0px; background-image: none; border-bottom: 0px; padding-top: 0px; padding-left: 0px; border-left: 0px; display: inline; padding-right: 0px" border="0" alt="Screen Shot 2015-09-14 at 4.26.36 PM" src="/assets/image_eb15db.jpg" width="463" height="436" /></a></p>

<p>&#160;</p>

<p>Have fun!</p>
