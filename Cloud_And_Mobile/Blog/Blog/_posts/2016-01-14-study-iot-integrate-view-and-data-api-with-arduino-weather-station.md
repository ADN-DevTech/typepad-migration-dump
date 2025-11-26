---
layout: "post"
title: "Study IoT : Integrate View and Data API with Arduino weather station"
date: "2016-01-14 04:42:48"
author: "Daniel Du"
categories:
  - "Daniel Du"
  - "HTML5"
  - "Javascript"
  - "View and Data API"
original_url: "https://adndevblog.typepad.com/cloud_and_mobile/2016/01/study-iot-integrate-view-and-data-api-with-arduino-weather-station.html "
typepad_basename: "study-iot-integrate-view-and-data-api-with-arduino-weather-station"
typepad_status: "Publish"
---

<h5>By <a href="http://adndevblog.typepad.com/cloud_and_mobile/daniel-du.html">Daniel Du</a></h5>  <p>In <a href="http://adndevblog.typepad.com/cloud_and_mobile/2015/12/study-iot-connect-arduino-based-weather-station-to-cloud.html" target="_blank">previous blog</a>, I introduced how I create a simplest weather station with Arduino and connect it to cloud. In this post, I will introduce how I consume the temperature from a client. To make it fun, I use <a href="http://developer.autodesk.com" target="_blank">View and Data API</a>. Think about following scenario, the temperature sensor is installed somewhere in a smart building, or a device in a smart factory, as an import part of facility management(FM) system, we need to see real time information of the sensor and show it in BIM model. </p>  <p>In my sample, pretend that I install the temperature sensor on the top of gate house, I need to see the template line chart of the senor. In previous blog, I created a RESTful server so that the Arduino can post temperature value to the server periodically, now I need to keep working on the server so that client can get the temperature values and show them in a line chart. In my router configuration (<a href="https://github.com/duchangyu/project-arduivew/blob/v0.1/routes/api.js" target="_blank">compete code</a>) I add following code</p>  <pre class="csharpcode">router.route(<span class="str">'/sensors/:sensorId/values/:count'</span>)
  .get(sensorController.getSensorLastNValues)</pre>
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

<p>Here is the code snippet in <a href="https://github.com/duchangyu/project-arduivew/blob/v0.1/controllers/sensorCtl.js" target="_blank">sensor controller</a> , I get the last N temperature values from MongoDb and output as JSON, so that it could be consumed by other clients. </p>

<pre class="csharpcode">exports.getSensorLastNValues = <span class="kwrd">function</span>(req,res){

   <span class="kwrd">var</span> sensorId = req.<span class="kwrd">params</span>.sensorId;
   <span class="kwrd">var</span> count = req.<span class="kwrd">params</span>.count;

    Sensor.findById(sensorId, <span class="kwrd">function</span>(err, sensor){
      <span class="kwrd">if</span>(err)
        res.json(err);

      <span class="kwrd">if</span>(sensor &amp;&amp; sensor.values){

        <span class="kwrd">var</span> total = sensor.values.length;
        <span class="kwrd">if</span>(count &gt; total) {
          res.json(sensor.values); <span class="rem">//return all values</span>
        }
        <span class="kwrd">else</span>
        {
          res.json(sensor.values.slice(sensor.values.length - count));
        }

      }
      <span class="kwrd">else</span>{
        res.json({ message : <span class="str">&quot;sensor with specified id does not exist.&quot;</span>})
      }
    })

}
<style type="text/css">.csharpcode, .csharpcode pre
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
.csharpcode .lnum { color: #606060; }
</style></pre>

<p>Now let’s go to the client side, I am using View and Data Client API with JavaScript, so my client is browser, I will create a <a href="https://github.com/duchangyu/project-arduivew/blob/v0.1/www/js/dataloader.js" target="_blank">data loader</a> in JavaScript to get the temperatures with REST. As defined in the route of server, it would be GET /api/sensors/&lt;sensorId&gt;/values/10. Here is the code snippet, the <a href="https://github.com/duchangyu/project-arduivew/blob/v0.1/www/js/dataloader.js" target="_blank">complete code</a> is on github. </p>

<pre class="csharpcode">dataloader.getLast10Temperatures = <span class="kwrd">function</span>(onSuccess) {

  <span class="rem">//get last 10 temperature items</span>
  <span class="kwrd">var</span> url = <span class="str">'/api/sensors/561083be06dd6162658ae8c8/values/10'</span>;

  $.getJSON(url, <span class="kwrd">function</span>(data){

        <span class="kwrd">if</span>(!Array.isArray(data)) <span class="kwrd">return</span>;

        <span class="rem">//sort by timestamp</span>
        data.sort(<span class="kwrd">function</span>(a,b){
            <span class="kwrd">return</span> parseInt(a.timeStamp) - parseInt(b.timeStamp);
        });
    onSuccess(data);

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

<p>To show the temperature line chart, firstly I create an docking panel with View and Data API as the container. Here is an <a href="http://viewer.autodesk.io/node/gallery/uploads/extensions/Autodesk.ADN.Viewing.Extension.DockingPanel/Autodesk.ADN.Viewing.Extension.DockingPanel.js" target="_blank">example of docking panel</a> created by Philippe, so I will skip this part. To draw the temperature line chat, I use <a href="http://www.chartjs.org/" target="_blank">Chart.js</a> which is pretty popular chart library to generate charts in various style. Here is the code snippet, the <a href="https://github.com/duchangyu/project-arduivew/blob/v0.1/www/js/Autodesk.ADN.Viewing.Extension.GenericDockingPanel.js" target="_blank">complete code</a> of this part is on github: </p>

<pre class="csharpcode">  <span class="rem">////////////////////////////////////</span>
  <span class="rem">///Generate the chart</span>
  <span class="rem">///</span>
  <span class="rem">////////////////////////////////////</span>
  <span class="kwrd">var</span> gernerateTempChart = <span class="kwrd">function</span>(){

    <span class="kwrd">var</span> lineChartData = {
          labels : [],
          datasets : [
            {
              label: <span class="str">&quot;Temperatures&quot;</span>,
              fillColor : <span class="str">&quot;rgba(220,220,220,0.2)&quot;</span>,
              strokeColor : <span class="str">&quot;rgba(220,220,220,1)&quot;</span>,
              pointColor : <span class="str">&quot;rgba(220,220,220,1)&quot;</span>,
              pointStrokeColor : <span class="str">&quot;#fff&quot;</span>,
              pointHighlightFill : <span class="str">&quot;#fff&quot;</span>,
              pointHighlightStroke : <span class="str">&quot;rgba(220,220,220,1)&quot;</span>,
              data : []
            }
          ]
        }


      dataloader.getLast10Temperatures(<span class="kwrd">function</span>(tempItems){

        <span class="rem">//clear first</span>
        lineChartData.labels.length = 0;
        lineChartData.datasets[0].data.length = 0;

        
        <span class="rem">//prepare data</span>
        tempItems.forEach(<span class="kwrd">function</span>(tempItem){
          <span class="rem">//add time as label</span>
          <span class="kwrd">var</span> timeStamp = tempItem.timestamp;
          <span class="kwrd">var</span> lbl = <span class="kwrd">new</span> Date(timeStamp).toLocaleTimeString();

          lineChartData.labels.push(lbl);

          <span class="rem">//add temperature values</span>
          <span class="kwrd">var</span> temperature = parseFloat(tempItem.value).toFixed(1);<span class="rem">// 0.1 degree precise</span>
          lineChartData.datasets[0].data.push(temperature); 
                  
        });
        
        <span class="kwrd">var</span> min = Math.min.apply(<span class="kwrd">null</span>, lineChartData.datasets[0].data) ; 
        <span class="kwrd">var</span> max = Math.max.apply(<span class="kwrd">null</span>, lineChartData.datasets[0].data) ;         

        
        <span class="kwrd">var</span> steps = lineChartData.datasets[0].data.length ;
        <span class="kwrd">var</span> stepsWidth = (max - min) / steps;
        <span class="kwrd">var</span> stepValue = min;

        <span class="rem">//with 1 degree as bottom margin on chart </span>
        <span class="rem">//so that the lowered point does not fall on the x-axis</span>
        <span class="kwrd">var</span> margin = 0.5; 

        <span class="kwrd">if</span>(max - min &lt; 1 ){ <span class="rem">//temperature almost constant, change is less than 1 degree</span>
          
          stepsWidth = 1.0 / steps;
          stepValue = min - margin;
          
        }<span class="kwrd">else</span>{

            stepsWidth = (max - min) * 1.0 / steps;
            stepValue = min - margin ;

        }

        <span class="rem">//generate the chart</span>
        <span class="kwrd">var</span> ctx = document.getElementById(<span class="str">&quot;canvasChart&quot;</span>).getContext(<span class="str">&quot;2d&quot;</span>);
        window.myLine = <span class="kwrd">new</span> Chart(ctx).Line(lineChartData, {
          responsive: <span class="kwrd">true</span> 
          ,
          scaleOverride : <span class="kwrd">true</span>,
          scaleSteps : steps,
          scaleStepWidth : stepsWidth,
          scaleStartValue : stepValue,
          scaleLabel: <span class="str">&quot;&lt;%= Number(value).toFixed(1) %&gt;&quot;</span>
        });

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

<p>&#160;</p>

<p>With that, I can draw a temperate line chart on a docking panel of viewer with View and Data API. If you are interested, please find the complete code on github: <a title="https://github.com/duchangyu/project-arduivew/tree/v0.1" href="https://github.com/duchangyu/project-arduivew/tree/v0.1">https://github.com/duchangyu/project-arduivew/tree/v0.1</a></p>
