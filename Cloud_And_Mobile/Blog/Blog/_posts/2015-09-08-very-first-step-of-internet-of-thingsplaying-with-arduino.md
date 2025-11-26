---
layout: "post"
title: "Very first step of Internet of Things&ndash;playing with Arduino"
date: "2015-09-08 20:47:10"
author: "Daniel Du"
categories:
  - "Daniel Du"
original_url: "https://adndevblog.typepad.com/cloud_and_mobile/2015/09/very-first-step-of-internet-of-thingsplaying-with-arduino.html "
typepad_basename: "very-first-step-of-internet-of-thingsplaying-with-arduino"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/cloud_and_mobile/daniel-du.html">Daniel Du</a></p>  <p>Internet of Things(IoT) is more and more popular these days, and Autodesk is also heading to IoT by acquiring the leading IoT company – SeeControl.&#160; As my very first step, I’ve been playing with Arduino and have a lot of fun. </p>  <p>knowing nothing about hardware stuff, Arduino Starter Kit is good place to start for me, it includes Arduino and some necessary commonly used sensors. </p>  <h3>Getting started with Arduino </h3>  <p>To start, I will need an Arduino board, breadboard, some sensors and some connection wires. You can buy a Arduino Starter Kit to start. </p>  <p>I also need Arduino IDE, which is an integrated development environment, enables us to write some code and upload then to Arduino to run. It has Windows version, Linux version and Mac version. I am using Mac version. Currently the latest version is 1.6.5</p>  <p><a title="https://www.arduino.cc/en/Main/Software" href="https://www.arduino.cc/en/Main/Software">https://www.arduino.cc/en/Main/Software</a></p>  <p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb086f4345970d-pi"><img title="image" style="border-top: 0px; border-right: 0px; background-image: none; border-bottom: 0px; padding-top: 0px; padding-left: 0px; border-left: 0px; display: inline; padding-right: 0px" border="0" alt="image" src="/assets/image_3ef0ab.jpg" width="440" height="130" /></a></p>  <p>To run my very first Arduino app, we can start from the Blink example, it Turns on an LED on for one second, then off for one second, repeatedly.If you want to have a try, once you installed the Arduino IDE, you can find the sample from File – Examples.</p>  <p>Before running it, we need setup the hardware. Connect Arduino and my laptop with USB cable, upload the sample from Arduino IDE by clicking the second arrow button. You will see the little LED beside 13 pin on Arduino board is splashing. This is also a test program to verify whether your Arduino board is good or not.</p>  <p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb086f4349970d-pi"><img title="Screen Shot 2015-09-09 at 11.11.35 AM" style="border-top: 0px; border-right: 0px; background-image: none; border-bottom: 0px; padding-top: 0px; padding-left: 0px; border-left: 0px; display: inline; padding-right: 0px" border="0" alt="Screen Shot 2015-09-09 at 11.11.35 AM" src="/assets/image_6eb45d.jpg" width="465" height="399" /></a></p>  <p>&#160;</p>  <h3>Do not have Arduino yet? Try 123D Circuits</h3>  <p>You may not understand what I am saying about because I did not put a picture or a video as I do not have my devices with me at the time of writing. But you want to try it yourself, right? Are you thrilled to have a try but have not get Arduino yet? Actually you do not have to, try <a href="https://123d.circuits.io" target="_blank">Autodesk 123D Circuits</a>, you can do the hardware simulation, coding online in 123D Circuits and share your work among communities. </p>  <p>Here is the simplest Blink hardware lab view. I put a LED on the breadboard, connect its leg to 13 pin on Arduino with the blue wire. And then connect its another led to 5V pin on Arduino with a red wire. It is that simple, but wait, I’d better to put a resistor between them, otherwise the LED may be exploded since the voltage is too high. </p>  <p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d154de2c970c-pi"><img title="Screen Shot 2015-09-09 at 11.23.35 AM" style="border-top: 0px; border-right: 0px; background-image: none; border-bottom: 0px; padding-top: 0px; padding-left: 0px; border-left: 0px; display: inline; padding-right: 0px" border="0" alt="Screen Shot 2015-09-09 at 11.23.35 AM" src="/assets/image_67634b.jpg" width="478" height="294" /></a></p>  <p>Once I am done, I can press “Start Simulation” from top right or “Upload &amp; Run” from the code editor on the bottom to run the test code. </p>  <p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d154de30970c-pi"><img title="Screen Shot 2015-09-09 at 11.32.57 AM" style="border-top: 0px; border-right: 0px; background-image: none; border-bottom: 0px; padding-top: 0px; padding-left: 0px; border-left: 0px; display: inline; padding-right: 0px" border="0" alt="Screen Shot 2015-09-09 at 11.32.57 AM" src="/assets/image_dbcf27.jpg" width="456" height="232" /></a></p>  <p>Please note that the Arduino code is the default blink code as below, if it is not, you need to copy and paste it into code editor before you upload and run: </p>  <pre class="csharpcode"><span class="rem">// Pin 13 has an LED connected on most Arduino boards.</span>
<span class="rem">// give it a name:</span>
<span class="kwrd">int</span> led = 13;

<span class="rem">// the setup routine runs once when you press reset:</span>
<span class="kwrd">void</span> setup() {
  <span class="rem">// initialize the digital pin as an output.</span>
  pinMode(led, OUTPUT);
}

<span class="rem">// the loop routine runs over and over again forever:</span>
<span class="kwrd">void</span> loop() {
  digitalWrite(led, HIGH);   <span class="rem">// turn the LED on (HIGH is the voltage level)</span>
  delay(1000);               <span class="rem">// wait for a second</span>
  digitalWrite(led, LOW);    <span class="rem">// turn the LED off by making the voltage LOW</span>
  delay(1000);               <span class="rem">// wait for a second</span>
}</pre>

<p>Now you should be able see the LED is blinking. <img class="wlEmoticon wlEmoticon-smile" style="border-top-style: none; border-bottom-style: none; border-right-style: none; border-left-style: none" alt="Smile" src="/assets/image_b6a831.jpg" />&#160;</p>

<p>&#160;</p>

<p>My next job is put on a LM35 temperature sensor to get current temperature and show the value out. I will blog it next time. </p>

<p>Before you get your Arduino shipped, you can play with <a href="https://123d.circuits.io" target="_blank">Autodesk 123D Circuits</a>, and there are quite a lot <a href="https://www.youtube.com/watch?v=nOCIgTMxbRE&amp;list=PLu8TYSQ5jCFho31LxXCoEBlL3x94l6mLc" target="_blank">videos on YouTube</a> to demo how to use it.</p>

<p>Have fun!</p>
