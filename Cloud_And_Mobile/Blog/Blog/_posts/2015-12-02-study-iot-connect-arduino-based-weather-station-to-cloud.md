---
layout: "post"
title: "Study IoT: Connect Arduino based weather station to cloud"
date: "2015-12-02 23:11:56"
author: "Daniel Du"
categories:
  - "Cloud"
  - "Daniel Du"
  - "View and Data API"
original_url: "https://adndevblog.typepad.com/cloud_and_mobile/2015/12/study-iot-connect-arduino-based-weather-station-to-cloud.html "
typepad_basename: "study-iot-connect-arduino-based-weather-station-to-cloud"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/cloud_and_mobile/daniel-du.html">Daniel Du</a></p>  <p>After a long time break, I need to speed up blogging what I learned with IoT. I’ve learned <a href="http://adndevblog.typepad.com/cloud_and_mobile/2015/09/very-first-step-of-internet-of-thingsplaying-with-arduino.html" target="_blank">how to setup the environment of Arduino</a> and how to do the same in Autodesk 123D circuits without a real device, and then I learned <a href="http://adndevblog.typepad.com/cloud_and_mobile/2015/09/monitor-environment-temperature-with-arduino-and-lm35-sensor-1.html" target="_blank">how to get the temperature</a> with a LM35 temperature sensor. My next step is to send the temperature value to the cloud, so that it can be shared/used by other users or applications.</p>  <p>I created a sample with this idea, integrating the Arduino weather station with View and Data API. Here is the live demo : <a title="http://arduiview.herokuapp.com/" href="http://arduiview.herokuapp.com/">http://arduiview.herokuapp.com/</a> (this line char may be empty as the sensor is not connected all the time). The viewer shows 3D model of a building, let’s say the Arduino based weather station is setup on the roof, and keep sending temperature to the cloud periodically. The web page client get the temperature data and shows up a line chart in the viewer. When the temperature is too high( higher than 40 ℃), There will be a high temperature alarm, and the viewer zoom to the sensor. It is just a simple sample for demo, but you can applied to more practical scenario.&#160;&#160; </p>  <p>Here is the architecture, the Arduino based weather station and the viewer communicate with cloud via REST API.</p>  <p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c7f48b21970b-pi"><img title="Screen Shot 2015-12-03 at 2.50.53 PM" style="border-left-width: 0px; border-right-width: 0px; background-image: none; border-bottom-width: 0px; padding-top: 0px; padding-left: 0px; display: inline; padding-right: 0px; border-top-width: 0px" border="0" alt="Screen Shot 2015-12-03 at 2.50.53 PM" src="/assets/image_f39847.jpg" width="426" height="231" /></a></p>  <p>&#160;</p>  <p>The Arduino itself cannot connect to the network, to communicate with cloud, I need another device. I choose CC3000 WiFi Shield, which can be used with Arduino together, so that Arduino can connect to internet with WiFi. I put CC3000 WiFi shield on top of Arduino and connect to the LM35 temperature sensor, it looks like below:</p>  <p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb0898a28c970d-pi"><img title="arduino-lm35" style="border-left-width: 0px; border-right-width: 0px; background-image: none; border-bottom-width: 0px; padding-top: 0px; padding-left: 0px; display: inline; padding-right: 0px; border-top-width: 0px" border="0" alt="arduino-lm35" src="/assets/image_dcd1cc.jpg" width="420" height="406" /></a></p>  <p>&#160;</p>  <p>The software side, I use Adafruite CC3000 Library to drive the shield. In Arduino IDE, I go to “Project” –&gt; “Include Libraries” –&gt; “Manage Libraries”, and search “CC3000”, and installed it as screen-shot below:</p>  <p>&#160;<a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d17e3ab5970c-pi"><img title="Screen Shot 2015-12-03 at 1.25.34 PM" style="border-left-width: 0px; border-right-width: 0px; background-image: none; border-bottom-width: 0px; padding-top: 0px; padding-left: 0px; display: inline; padding-right: 0px; border-top-width: 0px" border="0" alt="Screen Shot 2015-12-03 at 1.25.34 PM" src="/assets/image_26a37f.jpg" width="479" height="271" /></a></p>  <p>Once the library is installed, you can find many example projects in Arduino IDE, which is lovely <img class="wlEmoticon wlEmoticon-smile" style="border-top-style: none; border-bottom-style: none; border-right-style: none; border-left-style: none" alt="Smile" src="/assets/image_3056ef.jpg" />. You can take a look at the example files to understand how the library is used. </p>  <p>Now I need to create a server which is supposed to be running on cloud as a data center. I use node.js to create a simple website which expose some REST APIs, so that the Arduino can send temperature data to the server on Cloud with REST. Here are some code snippet. </p>  <p>The server exposes a REST API to receive data, the URL is similar like :</p>  <p>PUT /sensors/somesensorId/values</p>  <p>body:</p>  <p>{</p>  <p>value : 22</p>  <p>}</p>  <p>The node.js code snippet is like below:</p>  <pre class="csharpcode">router.route(<span class="str">'/sensors/:sensorId/values'</span>)
  .get(sensorController.getSensorValues)
  .put(sensorController.appendSensorValues);</pre>
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

<p>And here is the code of sensorController. Actually I am using mongoose and MongoDb at back end to save the data into database. Just for demo, I did not save all the temperature data as it can be huge since the sensor will be uploading data to cloud serve all the time, I just keep the latest piece. For the practical system, you may want to save all the data to make it possible to do big data analysis latter. </p>

<pre class="csharpcode">exports.appendSensorValues = <span class="kwrd">function</span>(req,res){  <span class="rem">//append</span>

    <span class="rem">//we just save 50 + 1 values items to save db spaces</span>
    <span class="kwrd">var</span> MAX_VAULE_ITEM_COUNT = 50;

    <span class="kwrd">var</span> sensorId = req.<span class="kwrd">params</span>.sensorId;

    Sensor.findById(sensorId, <span class="kwrd">function</span>(err, sensor){
      <span class="kwrd">if</span>(err)
        res.json(err);

      <span class="kwrd">var</span> sensorValueItem = {};
      sensorValueItem.timestamp = <span class="kwrd">new</span> Date().getTime();
      sensorValueItem.value = req.body.value;

      <span class="rem">//console.log(sensorValueItem);</span>
      <span class="kwrd">var</span> len = sensor.values.length;
      sensor.values = sensor.values.slice(len - MAX_VAULE_ITEM_COUNT );

      sensor.values = sensor.values.concat(sensorValueItem);

      sensor.save(<span class="kwrd">function</span>(err){
        
        <span class="kwrd">if</span>(err)
          res.send(err);

        res.json(sensorValueItem);
      })
    });

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

<p>Please refer to following link to see the complete source code.<a title="https://github.com/duchangyu/project-arduivew/tree/v0.1" href="https://github.com/duchangyu/project-arduivew/tree/v0.1">https://github.com/duchangyu/project-arduivew/tree/v0.1</a>, </p>

<p>Now let’s go to the Arduino side. Since we can connect to internet with CC3000 WiFi shield, we can send the temperature value to our cloud server by REST call. The REST call is basically sending raw http content, according to our REST API defined by the server:</p>

<p>PUT /api/sensors/somesensorid/value HTTP/1.1 </p>

<p>HOST: arduiview.heroku.com </p>

<p>content-type : application/json </p>

<p>Content-Length : 19 </p>

<p>{ </p>

<p>value : 22 </p>

<p>}</p>

<p>Here is the code snippet: </p>

<pre class="csharpcode"><span class="kwrd">void</span> postTemperatureToCloudServer() {

  <span class="rem">//connectToCloudServer</span>
  Serial.println(F(<span class="str">&quot;trying to connect to cloud server.....&quot;</span>));
  <span class="rem">//client.close();</span>
  client = cc3000.connectTCP(ip, 80);

  Serial.println(F(<span class="str">&quot;connected to cloud server - &quot;</span>));
  Serial.println(WEBSITE );

  Serial.println(F(<span class="str">&quot;begin uploading...&quot;</span>));

  <span class="kwrd">float</span> temp = 0.0;
  <span class="rem">// get the current temperature from sensor</span>
  <span class="kwrd">int</span> reading = analogRead(0);
  temp = reading * 0.0048828125 * 100;
  Serial.print(F(<span class="str">&quot;Current temp&quot;</span>));
  Serial.println(temp);

  <span class="kwrd">int</span> length;
  <span class="kwrd">char</span> sTemp[5] = <span class="str">&quot;&quot;</span>;
  <span class="rem">//convert float to char*,</span>
  dtostrf(temp, 2, 2, sTemp); <span class="rem">//val, integer part width, precise, result char array</span>
  <span class="rem">//itoa(temp, sTemp,10);</span>
  Serial.println(sTemp);


  <span class="kwrd">char</span> sLength[3];


  <span class="rem">//prepare the http body</span>
  <span class="rem">//</span>
  <span class="rem">//{</span>
  <span class="rem">//  &quot;value&quot; : 55.23</span>
  <span class="rem">//}</span>
  <span class="rem">//</span>

  <span class="kwrd">char</span> httpPackage[20] = <span class="str">&quot;&quot;</span>;

  strcat(httpPackage, <span class="str">&quot;{\&quot;value\&quot;: \&quot;&quot;</span>);
  strcat(httpPackage, sTemp);
  strcat(httpPackage, <span class="str">&quot;\&quot; }&quot;</span>);

  <span class="rem">// get the length of data package</span>
  length = strlen(httpPackage);
  <span class="rem">// convert int to char array for posting</span>
  itoa(length, sLength, 10);
  Serial.print(F(<span class="str">&quot;body lenght=&quot;</span>));
  Serial.println(sLength);

  <span class="rem">//prepare the http header</span>
  Serial.println(F(<span class="str">&quot;Sending headers...&quot;</span>));

  client.fastrprint(F(<span class="str">&quot;PUT /api/sensors/&quot;</span>));
  <span class="kwrd">char</span> *sensorId = SENSOR_ID;
  client.fastrprint(sensorId);
  <span class="rem">//client.fastrprint(SENSOR_ID);</span>
  client.fastrprint(F(<span class="str">&quot;/values&quot;</span>));

  client.fastrprintln(F(<span class="str">&quot; HTTP/1.1&quot;</span>));
  Serial.print(F(<span class="str">&quot;.&quot;</span>));

  client.fastrprint(F(<span class="str">&quot;Host: &quot;</span>));
  client.fastrprintln(WEBSITE);
  Serial.print(F(<span class="str">&quot;.&quot;</span>));

  client.fastrprint(F(<span class="str">&quot;content-type: &quot;</span>));
  client.fastrprintln(F(<span class="str">&quot;application/json&quot;</span>));
  Serial.print(F(<span class="str">&quot;.&quot;</span>));

  client.fastrprint(F(<span class="str">&quot;Content-Length: &quot;</span>));
  client.fastrprintln(sLength);
  client.fastrprintln(F(<span class="str">&quot;&quot;</span>));
  Serial.print(F(<span class="str">&quot;.&quot;</span>));

  Serial.println(F(<span class="str">&quot;header done.&quot;</span>));

  <span class="rem">//send data</span>
  Serial.println(F(<span class="str">&quot;Sending data&quot;</span>));
  client.fastrprintln(httpPackage);


  Serial.println(F(<span class="str">&quot;===upload completed.&quot;</span>));

  <span class="rem">// Get the http page feedback</span>

  unsigned <span class="kwrd">long</span> rTimer = millis();
  Serial.println(F(<span class="str">&quot;Reading Cloud Response!!!\r\n&quot;</span>));
  <span class="kwrd">while</span> (millis() - rTimer &lt; 2000) {
    <span class="kwrd">while</span> (client.connected() &amp;&amp; client.available()) {
      <span class="kwrd">char</span> c = client.read();
      Serial.print(c);
    }
  }
  delay(1000);             <span class="rem">// Wait for 1s to finish posting the data stream</span>
  client.close();      <span class="rem">// Close the service connection</span>

 
  Serial.println(F(<span class="str">&quot;upload completed\n&quot;</span>));

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

<pre class="csharpcode">You can get the complete code from github : </pre>

<pre class="csharpcode"><a title="https://github.com/duchangyu/project-arduivew/blob/v0.1/arduino/arduiview-lm35-2/arduiview-lm35-2.ino" href="https://github.com/duchangyu/project-arduivew/blob/v0.1/arduino/arduiview-lm35-2/arduiview-lm35-2.ino">https://github.com/duchangyu/project-arduivew/blob/v0.1/arduino/arduiview-lm35-2/arduiview-lm35-2.ino</a> </pre>
