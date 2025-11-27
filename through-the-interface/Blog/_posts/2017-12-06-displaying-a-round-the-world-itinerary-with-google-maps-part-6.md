---
layout: "post"
title: "Displaying a round-the-world itinerary with Google Maps &ndash; Part 6"
date: "2017-12-06 10:33:00"
author: "Kean Walmsley"
categories:
  - "HTML"
  - "JavaScript"
  - "Personal"
  - "Round the world"
original_url: "https://www.keanw.com/2017/12/displaying-a-round-the-world-itinerary-with-google-maps-part-6.html "
typepad_basename: "displaying-a-round-the-world-itinerary-with-google-maps-part-6"
typepad_status: "Publish"
---

<p>Here’s a quick recap of the series, so far:</p>
<ol>
<li><a href="http://through-the-interface.typepad.com/through_the_interface/2017/06/displaying-a-round-the-world-itinerary-using-google-maps-part-1.html">Creating a basic map for our round-the-world itinerary</a></li>
<li><a href="http://through-the-interface.typepad.com/through_the_interface/2017/06/displaying-a-round-the-world-itinerary-using-google-maps-part-2.html" rel="noopener noreferrer" target="_blank">Adding a photo for each placemark</a></li>
<li><a href="http://through-the-interface.typepad.com/through_the_interface/2017/07/displaying-a-round-the-world-itinerary-using-google-maps-part-3.html" rel="noopener noreferrer" target="_blank">Adding labels for the approximate timing of the journey</a></li>
<li><a href="http://through-the-interface.typepad.com/through_the_interface/2017/07/displaying-a-round-the-world-itinerary-using-google-maps-part-4.html" rel="noopener noreferrer" target="_blank">Overlaying the actual path visited from a tracking device</a></li>
<li><a href="http://through-the-interface.typepad.com/through_the_interface/2017/08/displaying-a-round-the-world-itinerary-using-google-maps-part-5.html" rel="noopener noreferrer" target="_blank">Downloading the actual path in multiple queries</a></li>
</ol>
<p>A few weeks ago – while we were in Tahiti, but again in New Zealand – I noticed that the “visited” path from the Garmin site wasn’t being integrated properly into the Google Map integrated into <a href="http://walmsley.ch" rel="noopener noreferrer" target="_blank">our family blog</a>. After a little bit of analysis, I realised that the amount of tracking data being pulled down each time the map was being built was large (upwards of 10MB). I figured it was probably related to the connection speed – the page had trouble getting all that data in time – but of course having a page query 10MB+ plus of data (that will result in the same data for every page loaded in every browser) isn’t the best way to build a site.</p>
<p><a href="http://through-the-interface.typepad.com/.a/6a00d83452464869e201bb09dd34c9970d-pi" rel="noopener noreferrer" target="_blank"><img alt="The itinerary, to date" border="0" height="242" src="/assets/image_431040.jpg" style="margin: 30px auto; border: 0px currentcolor; float: none; display: block; background-image: none;" title="The itinerary, to date" width="500" /></a></p>
<p>Attempting to address the problem, I thought about ways to reduce this payload. The obvious one was to take the KML data from previous months of the trip and compress it into a single KMZ. I looked at ways to do this, and found you can load the respective KMLs – which I downloaded via the URLs contained in the source of Part 5, above – into Google Earth using File –&gt; Open:</p>
<p><a href="http://through-the-interface.typepad.com/.a/6a00d83452464869e201bb09dd34ce970d-pi" rel="noopener noreferrer" target="_blank"><img alt="Paths loaded in Google Earth" border="0" height="322" src="/assets/image_237757.jpg" style="margin: 30px auto; border: 0px currentcolor; float: none; display: block; background-image: none;" title="Paths loaded in Google Earth" width="500" /></a></p>
<p>From here you can right-click Temporary Places (or whichever place you’ve moved the imported data to) and select Save Place As… to export the data to a KMZ file.<a href="http://through-the-interface.typepad.com/.a/6a00d83452464869e201b8d2c46c56970c-pi" rel="noopener noreferrer" target="_blank"><img alt="Saving to KMZ" border="0" height="169" src="/assets/image_452413.jpg" style="margin: 30px auto; border: 0px currentcolor; float: none; display: block; background-image: none;" title="Saving to KMZ" width="300" /></a>The KML files I selected – from July to November – weighed in at a whopping 11.9MB and resulted in a KMZ file of just 380KB: a very respectable compression ratio.</p>
<p>One other thing I decided to fix, while I was at it, was to add some points in for the early part of the trip: I’d only picked up the Garmin inReach Explorer+ in West Hartford, so nothing was tracked prior to that point. I started trying to add these points inside Google Earth, but found that really hard: I ended up handrolling a KML with the points I wanted to add and then included these as a separate overlay. There weren’t many, so adding the overhead of an additional 6KB KML file seemed the simplest way to go.</p>
<p>Here’s the KML file, placed in the same folder as the HTML and called <em>pre-tracking.kml</em>:</p>
<div style="background: white; color: black; line-height: 140%; font-family: courier new; font-size: 8pt;">
<p style="margin: 0px;"><span style="color: blue;">&lt;?</span><span style="color: #a31515;">xml</span> <span style="color: red;">version</span><span style="color: blue;">=</span>&quot;<span style="color: blue;">1.0</span>&quot; <span style="color: red;">encoding</span><span style="color: blue;">=</span>&quot;<span style="color: blue;">UTF-8</span>&quot;<span style="color: blue;">?&gt;</span></p>
<p style="margin: 0px;"><span style="color: blue;">&lt;</span><span style="color: #a31515;">kml</span> <span style="color: red;">xmlns</span><span style="color: blue;">=</span>&quot;<span style="color: blue;">http://www.opengis.net/kml/2.2</span>&quot; <span style="color: red;">xmlns:gx</span><span style="color: blue;">=</span>&quot;<span style="color: blue;">http://www.google.com/kml/ext/2.2</span>&quot; <span style="color: red;">xmlns:kml</span><span style="color: blue;">=</span>&quot;<span style="color: blue;">http://www.opengis.net/kml/2.2</span>&quot; <span style="color: red;">xmlns:atom</span><span style="color: blue;">=</span>&quot;<span style="color: blue;">http://www.w3.org/2005/Atom</span>&quot;<span style="color: blue;">&gt;</span></p>
<p style="margin: 0px;"><span style="color: blue;">&lt;</span><span style="color: #a31515;">Folder</span><span style="color: blue;">&gt;</span></p>
<p style="margin: 0px;"><span style="color: blue;">&#0160; &lt;</span><span style="color: #a31515;">name</span><span style="color: blue;">&gt;</span>Temporary Places<span style="color: blue;">&lt;/</span><span style="color: #a31515;">name</span><span style="color: blue;">&gt;</span></p>
<p style="margin: 0px;"><span style="color: blue;">&#0160; &lt;</span><span style="color: #a31515;">open</span><span style="color: blue;">&gt;</span>1<span style="color: blue;">&lt;/</span><span style="color: #a31515;">open</span><span style="color: blue;">&gt;</span></p>
<p style="margin: 0px;"><span style="color: blue;">&#0160; &lt;</span><span style="color: #a31515;">Folder</span><span style="color: blue;">&gt;</span></p>
<p style="margin: 0px;"><span style="color: blue;">&#0160;&#0160;&#0160; &lt;</span><span style="color: #a31515;">name</span><span style="color: blue;">&gt;</span>Temporary Places<span style="color: blue;">&lt;/</span><span style="color: #a31515;">name</span><span style="color: blue;">&gt;</span></p>
<p style="margin: 0px;"><span style="color: blue;">&#0160;&#0160;&#0160; &lt;</span><span style="color: #a31515;">open</span><span style="color: blue;">&gt;</span>1<span style="color: blue;">&lt;/</span><span style="color: #a31515;">open</span><span style="color: blue;">&gt;</span></p>
<p style="margin: 0px;"><span style="color: blue;">&#0160;&#0160;&#0160; &lt;</span><span style="color: #a31515;">Document</span><span style="color: blue;">&gt;</span></p>
<p style="margin: 0px;"><span style="color: blue;">&#0160;&#0160;&#0160;&#0160;&#0160; &lt;</span><span style="color: #a31515;">name</span><span style="color: blue;">&gt;</span>Pre-Garmin Tracking<span style="color: blue;">&lt;/</span><span style="color: #a31515;">name</span><span style="color: blue;">&gt;</span></p>
<p style="margin: 0px;"><span style="color: blue;">&#0160;&#0160;&#0160;&#0160;&#0160; &lt;</span><span style="color: #a31515;">open</span><span style="color: blue;">&gt;</span>1<span style="color: blue;">&lt;/</span><span style="color: #a31515;">open</span><span style="color: blue;">&gt;</span></p>
<p style="margin: 0px;"><span style="color: blue;">&#0160;&#0160;&#0160;&#0160;&#0160; &lt;</span><span style="color: #a31515;">Style</span> <span style="color: red;">id</span><span style="color: blue;">=</span>&quot;<span style="color: blue;">linestyle_857985</span>&quot;<span style="color: blue;">&gt;</span></p>
<p style="margin: 0px;"><span style="color: blue;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; &lt;</span><span style="color: #a31515;">LineStyle</span><span style="color: blue;">&gt;</span></p>
<p style="margin: 0px;"><span style="color: blue;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; &lt;</span><span style="color: #a31515;">color</span><span style="color: blue;">&gt;</span>ffff5500<span style="color: blue;">&lt;/</span><span style="color: #a31515;">color</span><span style="color: blue;">&gt;</span></p>
<p style="margin: 0px;"><span style="color: blue;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; &lt;/</span><span style="color: #a31515;">LineStyle</span><span style="color: blue;">&gt;</span></p>
<p style="margin: 0px;"><span style="color: blue;">&#0160;&#0160;&#0160;&#0160;&#0160; &lt;/</span><span style="color: #a31515;">Style</span><span style="color: blue;">&gt;</span></p>
<p style="margin: 0px;"><span style="color: blue;">&#0160;&#0160;&#0160;&#0160;&#0160; &lt;</span><span style="color: #a31515;">Style</span> <span style="color: red;">id</span><span style="color: blue;">=</span>&quot;<span style="color: blue;">style_8579852</span>&quot;<span style="color: blue;">&gt;</span></p>
<p style="margin: 0px;"><span style="color: blue;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; &lt;</span><span style="color: #a31515;">IconStyle</span><span style="color: blue;">&gt;</span></p>
<p style="margin: 0px;"><span style="color: blue;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; &lt;</span><span style="color: #a31515;">color</span><span style="color: blue;">&gt;</span>ffff5500<span style="color: blue;">&lt;/</span><span style="color: #a31515;">color</span><span style="color: blue;">&gt;</span></p>
<p style="margin: 0px;"><span style="color: blue;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; &lt;</span><span style="color: #a31515;">Icon</span><span style="color: blue;">&gt;</span></p>
<p style="margin: 0px;"><span style="color: blue;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; &lt;</span><span style="color: #a31515;">href</span><span style="color: blue;">&gt;</span>http://maps.google.com/mapfiles/kml/paddle/wht-blank.png<span style="color: blue;">&lt;/</span><span style="color: #a31515;">href</span><span style="color: blue;">&gt;</span></p>
<p style="margin: 0px;"><span style="color: blue;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; &lt;/</span><span style="color: #a31515;">Icon</span><span style="color: blue;">&gt;</span></p>
<p style="margin: 0px;"><span style="color: blue;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; &lt;/</span><span style="color: #a31515;">IconStyle</span><span style="color: blue;">&gt;</span></p>
<p style="margin: 0px;"><span style="color: blue;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; &lt;</span><span style="color: #a31515;">BalloonStyle</span><span style="color: blue;">&gt;</span></p>
<p style="margin: 0px;"><span style="color: blue;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; &lt;</span><span style="color: #a31515;">text</span><span style="color: blue;">&gt;&lt;![CDATA[</span><span style="color: gray;">&lt;table&gt;&lt;tr&gt;&lt;td&gt;Id&lt;/td&gt;&lt;td&gt; $[Id] &lt;/td&gt;&lt;/tr&gt;&lt;tr&gt;&lt;td&gt;Time&lt;/td&gt;&lt;td&gt; $[Time] &lt;/td&gt;&lt;/tr&gt;&lt;tr&gt;&lt;td&gt;Time UTC&lt;/td&gt;&lt;td&gt; $[Time UTC] &lt;/td&gt;&lt;/tr&gt;&lt;tr&gt;&lt;td&gt;Name&lt;/td&gt;&lt;td&gt; $[Name] &lt;/td&gt;&lt;/tr&gt;&lt;tr&gt;&lt;td&gt;Map Display Name&lt;/td&gt;&lt;td&gt; $[Map Display Name] &lt;/td&gt;&lt;/tr&gt;&lt;tr&gt;&lt;td&gt;Device Type&lt;/td&gt;&lt;td&gt; $[Device Type] &lt;/td&gt;&lt;/tr&gt;&lt;tr&gt;&lt;td&gt;IMEI&lt;/td&gt;&lt;td&gt; $[IMEI] &lt;/td&gt;&lt;/tr&gt;&lt;tr&gt;&lt;td&gt;Incident Id&lt;/td&gt;&lt;td&gt; $[Incident Id] &lt;/td&gt;&lt;/tr&gt;&lt;tr&gt;&lt;td&gt;Latitude&lt;/td&gt;&lt;td&gt; $[Latitude] &lt;/td&gt;&lt;/tr&gt;&lt;tr&gt;&lt;td&gt;Longitude&lt;/td&gt;&lt;td&gt; $[Longitude] &lt;/td&gt;&lt;/tr&gt;&lt;tr&gt;&lt;td&gt;Elevation&lt;/td&gt;&lt;td&gt; $[Elevation] &lt;/td&gt;&lt;/tr&gt;&lt;tr&gt;&lt;td&gt;Velocity&lt;/td&gt;&lt;td&gt; $[Velocity] &lt;/td&gt;&lt;/tr&gt;&lt;tr&gt;&lt;td&gt;Course&lt;/td&gt;&lt;td&gt; $[Course] &lt;/td&gt;&lt;/tr&gt;&lt;tr&gt;&lt;td&gt;Valid GPS Fix&lt;/td&gt;&lt;td&gt; $[Valid GPS Fix] &lt;/td&gt;&lt;/tr&gt;&lt;tr&gt;&lt;td&gt;In Emergency&lt;/td&gt;&lt;td&gt; $[In Emergency] &lt;/td&gt;&lt;/tr&gt;&lt;tr&gt;&lt;td&gt;Text&lt;/td&gt;&lt;td&gt; $[Text] &lt;/td&gt;&lt;/tr&gt;&lt;tr&gt;&lt;td&gt;Event&lt;/td&gt;&lt;td&gt; $[Event] &lt;/td&gt;&lt;/tr&gt;&lt;/table&gt;</span><span style="color: blue;">]]&gt;&lt;/</span><span style="color: #a31515;">text</span><span style="color: blue;">&gt;</span></p>
<p style="margin: 0px;"><span style="color: blue;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; &lt;/</span><span style="color: #a31515;">BalloonStyle</span><span style="color: blue;">&gt;</span></p>
<p style="margin: 0px;"><span style="color: blue;">&#0160;&#0160;&#0160;&#0160;&#0160; &lt;/</span><span style="color: #a31515;">Style</span><span style="color: blue;">&gt;</span></p>
<p style="margin: 0px;"><span style="color: blue;">&#0160;&#0160;&#0160;&#0160;&#0160; &lt;</span><span style="color: #a31515;">Folder</span><span style="color: blue;">&gt;</span></p>
<p style="margin: 0px;"><span style="color: blue;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; &lt;</span><span style="color: #a31515;">name</span><span style="color: blue;">&gt;</span>Kean Walmsley<span style="color: blue;">&lt;/</span><span style="color: #a31515;">name</span><span style="color: blue;">&gt;</span></p>
<p style="margin: 0px;"><span style="color: blue;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; &lt;</span><span style="color: #a31515;">open</span><span style="color: blue;">&gt;</span>1<span style="color: blue;">&lt;/</span><span style="color: #a31515;">open</span><span style="color: blue;">&gt;</span></p>
<p style="margin: 0px;"><span style="color: blue;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; &lt;</span><span style="color: #a31515;">Placemark</span><span style="color: blue;">&gt;</span></p>
<p style="margin: 0px;"><span style="color: blue;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; &lt;</span><span style="color: #a31515;">name</span><span style="color: blue;">&gt;</span>Geneva Airport<span style="color: blue;">&lt;/</span><span style="color: #a31515;">name</span><span style="color: blue;">&gt;</span></p>
<p style="margin: 0px;"><span style="color: blue;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; &lt;</span><span style="color: #a31515;">TimeStamp</span><span style="color: blue;">&gt;&lt;</span><span style="color: #a31515;">when</span><span style="color: blue;">&gt;</span>2017-07-07T12:42:45Z<span style="color: blue;">&lt;/</span><span style="color: #a31515;">when</span><span style="color: blue;">&gt;&lt;/</span><span style="color: #a31515;">TimeStamp</span><span style="color: blue;">&gt;</span></p>
<p style="margin: 0px;"><span style="color: blue;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; &lt;</span><span style="color: #a31515;">styleUrl</span><span style="color: blue;">&gt;</span>#style_8579852<span style="color: blue;">&lt;/</span><span style="color: #a31515;">styleUrl</span><span style="color: blue;">&gt;</span></p>
<p style="margin: 0px;"><span style="color: blue;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; &lt;</span><span style="color: #a31515;">Point</span><span style="color: blue;">&gt;</span></p>
<p style="margin: 0px;"><span style="color: blue;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; &lt;</span><span style="color: #a31515;">altitudeMode</span><span style="color: blue;">&gt;</span>absolute<span style="color: blue;">&lt;/</span><span style="color: #a31515;">altitudeMode</span><span style="color: blue;">&gt;</span></p>
<p style="margin: 0px;"><span style="color: blue;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; &lt;</span><span style="color: #a31515;">coordinates</span><span style="color: blue;">&gt;</span>6.109156399971248,46.23700969987675,0<span style="color: blue;">&lt;/</span><span style="color: #a31515;">coordinates</span><span style="color: blue;">&gt;</span></p>
<p style="margin: 0px;"><span style="color: blue;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; &lt;/</span><span style="color: #a31515;">Point</span><span style="color: blue;">&gt;</span></p>
<p style="margin: 0px;"><span style="color: blue;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; &lt;/</span><span style="color: #a31515;">Placemark</span><span style="color: blue;">&gt;&#0160;&#0160;&#0160; </span></p>
<p style="margin: 0px;"><span style="color: blue;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; &lt;</span><span style="color: #a31515;">Placemark</span><span style="color: blue;">&gt;</span></p>
<p style="margin: 0px;"><span style="color: blue;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; &lt;</span><span style="color: #a31515;">name</span><span style="color: blue;">&gt;</span>Marin-Epagnier<span style="color: blue;">&lt;/</span><span style="color: #a31515;">name</span><span style="color: blue;">&gt;</span></p>
<p style="margin: 0px;"><span style="color: blue;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; &lt;</span><span style="color: #a31515;">TimeStamp</span><span style="color: blue;">&gt;&lt;</span><span style="color: #a31515;">when</span><span style="color: blue;">&gt;</span>2017-07-02T12:00:00Z<span style="color: blue;">&lt;/</span><span style="color: #a31515;">when</span><span style="color: blue;">&gt;&lt;/</span><span style="color: #a31515;">TimeStamp</span><span style="color: blue;">&gt;</span></p>
<p style="margin: 0px;"><span style="color: blue;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; &lt;</span><span style="color: #a31515;">styleUrl</span><span style="color: blue;">&gt;</span>#style_8579852<span style="color: blue;">&lt;/</span><span style="color: #a31515;">styleUrl</span><span style="color: blue;">&gt;</span></p>
<p style="margin: 0px;"><span style="color: blue;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; &lt;</span><span style="color: #a31515;">Point</span><span style="color: blue;">&gt;</span></p>
<p style="margin: 0px;"><span style="color: blue;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; &lt;</span><span style="color: #a31515;">altitudeMode</span><span style="color: blue;">&gt;</span>absolute<span style="color: blue;">&lt;/</span><span style="color: #a31515;">altitudeMode</span><span style="color: blue;">&gt;</span></p>
<p style="margin: 0px;"><span style="color: blue;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; &lt;</span><span style="color: #a31515;">coordinates</span><span style="color: blue;">&gt;</span>7.001589598912035,47.00918080225755,0<span style="color: blue;">&lt;/</span><span style="color: #a31515;">coordinates</span><span style="color: blue;">&gt;</span></p>
<p style="margin: 0px;"><span style="color: blue;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; &lt;/</span><span style="color: #a31515;">Point</span><span style="color: blue;">&gt;</span></p>
<p style="margin: 0px;"><span style="color: blue;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; &lt;/</span><span style="color: #a31515;">Placemark</span><span style="color: blue;">&gt;</span></p>
<p style="margin: 0px;"><span style="color: blue;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; &lt;</span><span style="color: #a31515;">Placemark</span><span style="color: blue;">&gt;</span></p>
<p style="margin: 0px;"><span style="color: blue;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; &lt;</span><span style="color: #a31515;">name</span><span style="color: blue;">&gt;</span>Heathrow Terminal 5<span style="color: blue;">&lt;/</span><span style="color: #a31515;">name</span><span style="color: blue;">&gt;</span></p>
<p style="margin: 0px;"><span style="color: blue;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; &lt;</span><span style="color: #a31515;">TimeStamp</span><span style="color: blue;">&gt;&lt;</span><span style="color: #a31515;">when</span><span style="color: blue;">&gt;</span>2017-07-02T16:00:00Z<span style="color: blue;">&lt;/</span><span style="color: #a31515;">when</span><span style="color: blue;">&gt;&lt;/</span><span style="color: #a31515;">TimeStamp</span><span style="color: blue;">&gt;</span></p>
<p style="margin: 0px;"><span style="color: blue;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; &lt;</span><span style="color: #a31515;">styleUrl</span><span style="color: blue;">&gt;</span>#style_8579852<span style="color: blue;">&lt;/</span><span style="color: #a31515;">styleUrl</span><span style="color: blue;">&gt;</span></p>
<p style="margin: 0px;"><span style="color: blue;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; &lt;</span><span style="color: #a31515;">Point</span><span style="color: blue;">&gt;</span></p>
<p style="margin: 0px;"><span style="color: blue;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; &lt;</span><span style="color: #a31515;">altitudeMode</span><span style="color: blue;">&gt;</span>absolute<span style="color: blue;">&lt;/</span><span style="color: #a31515;">altitudeMode</span><span style="color: blue;">&gt;</span></p>
<p style="margin: 0px;"><span style="color: blue;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; &lt;</span><span style="color: #a31515;">coordinates</span><span style="color: blue;">&gt;</span>-0.4879806004656406,51.47146600045286,0<span style="color: blue;">&lt;/</span><span style="color: #a31515;">coordinates</span><span style="color: blue;">&gt;</span></p>
<p style="margin: 0px;"><span style="color: blue;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; &lt;/</span><span style="color: #a31515;">Point</span><span style="color: blue;">&gt;</span></p>
<p style="margin: 0px;"><span style="color: blue;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; &lt;/</span><span style="color: #a31515;">Placemark</span><span style="color: blue;">&gt;</span></p>
<p style="margin: 0px;"><span style="color: blue;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; &lt;</span><span style="color: #a31515;">Placemark</span><span style="color: blue;">&gt;</span></p>
<p style="margin: 0px;"><span style="color: blue;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; &lt;</span><span style="color: #a31515;">name</span><span style="color: blue;">&gt;</span>Washington Dulles<span style="color: blue;">&lt;/</span><span style="color: #a31515;">name</span><span style="color: blue;">&gt;</span></p>
<p style="margin: 0px;"><span style="color: blue;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; &lt;</span><span style="color: #a31515;">TimeStamp</span><span style="color: blue;">&gt;&lt;</span><span style="color: #a31515;">when</span><span style="color: blue;">&gt;</span>2017-07-02T23:00:00Z<span style="color: blue;">&lt;/</span><span style="color: #a31515;">when</span><span style="color: blue;">&gt;&lt;/</span><span style="color: #a31515;">TimeStamp</span><span style="color: blue;">&gt;</span></p>
<p style="margin: 0px;"><span style="color: blue;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; &lt;</span><span style="color: #a31515;">styleUrl</span><span style="color: blue;">&gt;</span>#style_8579852<span style="color: blue;">&lt;/</span><span style="color: #a31515;">styleUrl</span><span style="color: blue;">&gt;</span></p>
<p style="margin: 0px;"><span style="color: blue;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; &lt;</span><span style="color: #a31515;">Point</span><span style="color: blue;">&gt;</span></p>
<p style="margin: 0px;"><span style="color: blue;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; &lt;</span><span style="color: #a31515;">altitudeMode</span><span style="color: blue;">&gt;</span>absolute<span style="color: blue;">&lt;/</span><span style="color: #a31515;">altitudeMode</span><span style="color: blue;">&gt;</span></p>
<p style="margin: 0px;"><span style="color: blue;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; &lt;</span><span style="color: #a31515;">coordinates</span><span style="color: blue;">&gt;</span>-77.45653879988311,38.95311619998505,0<span style="color: blue;">&lt;/</span><span style="color: #a31515;">coordinates</span><span style="color: blue;">&gt;</span></p>
<p style="margin: 0px;"><span style="color: blue;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; &lt;/</span><span style="color: #a31515;">Point</span><span style="color: blue;">&gt;</span></p>
<p style="margin: 0px;"><span style="color: blue;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; &lt;/</span><span style="color: #a31515;">Placemark</span><span style="color: blue;">&gt;</span></p>
<p style="margin: 0px;"><span style="color: blue;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; &lt;</span><span style="color: #a31515;">Placemark</span><span style="color: blue;">&gt;</span></p>
<p style="margin: 0px;"><span style="color: blue;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; &lt;</span><span style="color: #a31515;">name</span><span style="color: blue;">&gt;</span>University Inn, Washington DC<span style="color: blue;">&lt;/</span><span style="color: #a31515;">name</span><span style="color: blue;">&gt;</span></p>
<p style="margin: 0px;"><span style="color: blue;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; &lt;</span><span style="color: #a31515;">TimeStamp</span><span style="color: blue;">&gt;&lt;</span><span style="color: #a31515;">when</span><span style="color: blue;">&gt;</span>2017-07-03T00:00:00Z<span style="color: blue;">&lt;/</span><span style="color: #a31515;">when</span><span style="color: blue;">&gt;&lt;/</span><span style="color: #a31515;">TimeStamp</span><span style="color: blue;">&gt;</span></p>
<p style="margin: 0px;"><span style="color: blue;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; &lt;</span><span style="color: #a31515;">styleUrl</span><span style="color: blue;">&gt;</span>#style_8579852<span style="color: blue;">&lt;/</span><span style="color: #a31515;">styleUrl</span><span style="color: blue;">&gt;</span></p>
<p style="margin: 0px;"><span style="color: blue;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; &lt;</span><span style="color: #a31515;">Point</span><span style="color: blue;">&gt;</span></p>
<p style="margin: 0px;"><span style="color: blue;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; &lt;</span><span style="color: #a31515;">altitudeMode</span><span style="color: blue;">&gt;</span>absolute<span style="color: blue;">&lt;/</span><span style="color: #a31515;">altitudeMode</span><span style="color: blue;">&gt;</span></p>
<p style="margin: 0px;"><span style="color: blue;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; &lt;</span><span style="color: #a31515;">coordinates</span><span style="color: blue;">&gt;</span>-77.05284809999405,38.90030650017117,0<span style="color: blue;">&lt;/</span><span style="color: #a31515;">coordinates</span><span style="color: blue;">&gt;</span></p>
<p style="margin: 0px;"><span style="color: blue;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; &lt;/</span><span style="color: #a31515;">Point</span><span style="color: blue;">&gt;</span></p>
<p style="margin: 0px;"><span style="color: blue;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; &lt;/</span><span style="color: #a31515;">Placemark</span><span style="color: blue;">&gt;</span></p>
<p style="margin: 0px;"><span style="color: blue;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; &lt;</span><span style="color: #a31515;">Placemark</span><span style="color: blue;">&gt;</span></p>
<p style="margin: 0px;"><span style="color: blue;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; &lt;</span><span style="color: #a31515;">name</span><span style="color: blue;">&gt;</span>Union Station<span style="color: blue;">&lt;/</span><span style="color: #a31515;">name</span><span style="color: blue;">&gt;</span></p>
<p style="margin: 0px;"><span style="color: blue;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; &lt;</span><span style="color: #a31515;">TimeStamp</span><span style="color: blue;">&gt;&lt;</span><span style="color: #a31515;">when</span><span style="color: blue;">&gt;</span>2017-07-04T12:00:00Z<span style="color: blue;">&lt;/</span><span style="color: #a31515;">when</span><span style="color: blue;">&gt;&lt;/</span><span style="color: #a31515;">TimeStamp</span><span style="color: blue;">&gt;</span></p>
<p style="margin: 0px;"><span style="color: blue;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; &lt;</span><span style="color: #a31515;">styleUrl</span><span style="color: blue;">&gt;</span>#style_8579852<span style="color: blue;">&lt;/</span><span style="color: #a31515;">styleUrl</span><span style="color: blue;">&gt;</span></p>
<p style="margin: 0px;"><span style="color: blue;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; &lt;</span><span style="color: #a31515;">Point</span><span style="color: blue;">&gt;</span></p>
<p style="margin: 0px;"><span style="color: blue;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; &lt;</span><span style="color: #a31515;">altitudeMode</span><span style="color: blue;">&gt;</span>absolute<span style="color: blue;">&lt;/</span><span style="color: #a31515;">altitudeMode</span><span style="color: blue;">&gt;</span></p>
<p style="margin: 0px;"><span style="color: blue;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; &lt;</span><span style="color: #a31515;">coordinates</span><span style="color: blue;">&gt;</span>-77.00250794999864,38.90428664917936,0<span style="color: blue;">&lt;/</span><span style="color: #a31515;">coordinates</span><span style="color: blue;">&gt;</span></p>
<p style="margin: 0px;"><span style="color: blue;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; &lt;/</span><span style="color: #a31515;">Point</span><span style="color: blue;">&gt;</span></p>
<p style="margin: 0px;"><span style="color: blue;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; &lt;/</span><span style="color: #a31515;">Placemark</span><span style="color: blue;">&gt;</span></p>
<p style="margin: 0px;"><span style="color: blue;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; &lt;</span><span style="color: #a31515;">Placemark</span><span style="color: blue;">&gt;</span></p>
<p style="margin: 0px;"><span style="color: blue;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; &lt;</span><span style="color: #a31515;">name</span><span style="color: blue;">&gt;</span>Penn Station, NYC<span style="color: blue;">&lt;/</span><span style="color: #a31515;">name</span><span style="color: blue;">&gt;</span></p>
<p style="margin: 0px;"><span style="color: blue;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; &lt;</span><span style="color: #a31515;">TimeStamp</span><span style="color: blue;">&gt;&lt;</span><span style="color: #a31515;">when</span><span style="color: blue;">&gt;</span>2017-07-04T16:00:00Z<span style="color: blue;">&lt;/</span><span style="color: #a31515;">when</span><span style="color: blue;">&gt;&lt;/</span><span style="color: #a31515;">TimeStamp</span><span style="color: blue;">&gt;</span></p>
<p style="margin: 0px;"><span style="color: blue;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; &lt;</span><span style="color: #a31515;">styleUrl</span><span style="color: blue;">&gt;</span>#style_8579852<span style="color: blue;">&lt;/</span><span style="color: #a31515;">styleUrl</span><span style="color: blue;">&gt;</span></p>
<p style="margin: 0px;"><span style="color: blue;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; &lt;</span><span style="color: #a31515;">Point</span><span style="color: blue;">&gt;</span></p>
<p style="margin: 0px;"><span style="color: blue;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; &lt;</span><span style="color: #a31515;">altitudeMode</span><span style="color: blue;">&gt;</span>absolute<span style="color: blue;">&lt;/</span><span style="color: #a31515;">altitudeMode</span><span style="color: blue;">&gt;</span></p>
<p style="margin: 0px;"><span style="color: blue;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; &lt;</span><span style="color: #a31515;">coordinates</span><span style="color: blue;">&gt;</span>-73.99352333750556,40.75056537490029,0<span style="color: blue;">&lt;/</span><span style="color: #a31515;">coordinates</span><span style="color: blue;">&gt;</span></p>
<p style="margin: 0px;"><span style="color: blue;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; &lt;/</span><span style="color: #a31515;">Point</span><span style="color: blue;">&gt;</span></p>
<p style="margin: 0px;"><span style="color: blue;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; &lt;/</span><span style="color: #a31515;">Placemark</span><span style="color: blue;">&gt;</span></p>
<p style="margin: 0px;"><span style="color: blue;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; &lt;</span><span style="color: #a31515;">Placemark</span><span style="color: blue;">&gt;</span></p>
<p style="margin: 0px;"><span style="color: blue;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; &lt;</span><span style="color: #a31515;">name</span><span style="color: blue;">&gt;</span>Sunnyside<span style="color: blue;">&lt;/</span><span style="color: #a31515;">name</span><span style="color: blue;">&gt;</span></p>
<p style="margin: 0px;"><span style="color: blue;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; &lt;</span><span style="color: #a31515;">TimeStamp</span><span style="color: blue;">&gt;&lt;</span><span style="color: #a31515;">when</span><span style="color: blue;">&gt;</span>2017-07-04T20:00:00Z<span style="color: blue;">&lt;/</span><span style="color: #a31515;">when</span><span style="color: blue;">&gt;&lt;/</span><span style="color: #a31515;">TimeStamp</span><span style="color: blue;">&gt;</span></p>
<p style="margin: 0px;"><span style="color: blue;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; &lt;</span><span style="color: #a31515;">styleUrl</span><span style="color: blue;">&gt;</span>#style_8579852<span style="color: blue;">&lt;/</span><span style="color: #a31515;">styleUrl</span><span style="color: blue;">&gt;</span></p>
<p style="margin: 0px;"><span style="color: blue;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; &lt;</span><span style="color: #a31515;">Point</span><span style="color: blue;">&gt;</span></p>
<p style="margin: 0px;"><span style="color: blue;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; &lt;</span><span style="color: #a31515;">altitudeMode</span><span style="color: blue;">&gt;</span>absolute<span style="color: blue;">&lt;/</span><span style="color: #a31515;">altitudeMode</span><span style="color: blue;">&gt;</span></p>
<p style="margin: 0px;"><span style="color: blue;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; &lt;</span><span style="color: #a31515;">coordinates</span><span style="color: blue;">&gt;</span>-73.91963239878011,40.74327590022647,0<span style="color: blue;">&lt;/</span><span style="color: #a31515;">coordinates</span><span style="color: blue;">&gt;</span></p>
<p style="margin: 0px;"><span style="color: blue;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; &lt;/</span><span style="color: #a31515;">Point</span><span style="color: blue;">&gt;</span></p>
<p style="margin: 0px;"><span style="color: blue;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; &lt;/</span><span style="color: #a31515;">Placemark</span><span style="color: blue;">&gt;</span></p>
<p style="margin: 0px;"><span style="color: blue;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; &lt;</span><span style="color: #a31515;">Placemark</span><span style="color: blue;">&gt;</span></p>
<p style="margin: 0px;"><span style="color: blue;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; &lt;</span><span style="color: #a31515;">name</span><span style="color: blue;">&gt;</span>Times Square<span style="color: blue;">&lt;/</span><span style="color: #a31515;">name</span><span style="color: blue;">&gt;</span></p>
<p style="margin: 0px;"><span style="color: blue;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; &lt;</span><span style="color: #a31515;">TimeStamp</span><span style="color: blue;">&gt;&lt;</span><span style="color: #a31515;">when</span><span style="color: blue;">&gt;</span>2017-07-04T22:00:00Z<span style="color: blue;">&lt;/</span><span style="color: #a31515;">when</span><span style="color: blue;">&gt;&lt;/</span><span style="color: #a31515;">TimeStamp</span><span style="color: blue;">&gt;</span></p>
<p style="margin: 0px;"><span style="color: blue;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; &lt;</span><span style="color: #a31515;">styleUrl</span><span style="color: blue;">&gt;</span>#style_8579852<span style="color: blue;">&lt;/</span><span style="color: #a31515;">styleUrl</span><span style="color: blue;">&gt;</span></p>
<p style="margin: 0px;"><span style="color: blue;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; &lt;</span><span style="color: #a31515;">Point</span><span style="color: blue;">&gt;</span></p>
<p style="margin: 0px;"><span style="color: blue;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; &lt;</span><span style="color: #a31515;">altitudeMode</span><span style="color: blue;">&gt;</span>absolute<span style="color: blue;">&lt;/</span><span style="color: #a31515;">altitudeMode</span><span style="color: blue;">&gt;</span></p>
<p style="margin: 0px;"><span style="color: blue;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; &lt;</span><span style="color: #a31515;">coordinates</span><span style="color: blue;">&gt;</span>-73.98512727848284,40.75889951208065,0<span style="color: blue;">&lt;/</span><span style="color: #a31515;">coordinates</span><span style="color: blue;">&gt;</span></p>
<p style="margin: 0px;"><span style="color: blue;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; &lt;/</span><span style="color: #a31515;">Point</span><span style="color: blue;">&gt;</span></p>
<p style="margin: 0px;"><span style="color: blue;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; &lt;/</span><span style="color: #a31515;">Placemark</span><span style="color: blue;">&gt;</span></p>
<p style="margin: 0px;"><span style="color: blue;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; &lt;</span><span style="color: #a31515;">Placemark</span><span style="color: blue;">&gt;</span></p>
<p style="margin: 0px;"><span style="color: blue;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; &lt;</span><span style="color: #a31515;">name</span><span style="color: blue;">&gt;</span>Central Park, NYC<span style="color: blue;">&lt;/</span><span style="color: #a31515;">name</span><span style="color: blue;">&gt;</span></p>
<p style="margin: 0px;"><span style="color: blue;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; &lt;</span><span style="color: #a31515;">TimeStamp</span><span style="color: blue;">&gt;&lt;</span><span style="color: #a31515;">when</span><span style="color: blue;">&gt;</span>2017-07-05T00:00:00Z<span style="color: blue;">&lt;/</span><span style="color: #a31515;">when</span><span style="color: blue;">&gt;&lt;/</span><span style="color: #a31515;">TimeStamp</span><span style="color: blue;">&gt;</span></p>
<p style="margin: 0px;"><span style="color: blue;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; &lt;</span><span style="color: #a31515;">styleUrl</span><span style="color: blue;">&gt;</span>#style_8579852<span style="color: blue;">&lt;/</span><span style="color: #a31515;">styleUrl</span><span style="color: blue;">&gt;</span></p>
<p style="margin: 0px;"><span style="color: blue;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; &lt;</span><span style="color: #a31515;">Point</span><span style="color: blue;">&gt;</span></p>
<p style="margin: 0px;"><span style="color: blue;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; &lt;</span><span style="color: #a31515;">altitudeMode</span><span style="color: blue;">&gt;</span>absolute<span style="color: blue;">&lt;/</span><span style="color: #a31515;">altitudeMode</span><span style="color: blue;">&gt;</span></p>
<p style="margin: 0px;"><span style="color: blue;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; &lt;</span><span style="color: #a31515;">coordinates</span><span style="color: blue;">&gt;</span>-73.96535509973162,40.78286469982217,0<span style="color: blue;">&lt;/</span><span style="color: #a31515;">coordinates</span><span style="color: blue;">&gt;</span></p>
<p style="margin: 0px;"><span style="color: blue;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; &lt;/</span><span style="color: #a31515;">Point</span><span style="color: blue;">&gt;</span></p>
<p style="margin: 0px;"><span style="color: blue;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; &lt;/</span><span style="color: #a31515;">Placemark</span><span style="color: blue;">&gt;</span></p>
<p style="margin: 0px;"><span style="color: blue;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; &lt;</span><span style="color: #a31515;">Placemark</span><span style="color: blue;">&gt;</span></p>
<p style="margin: 0px;"><span style="color: blue;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; &lt;</span><span style="color: #a31515;">name</span><span style="color: blue;">&gt;</span>Hartford<span style="color: blue;">&lt;/</span><span style="color: #a31515;">name</span><span style="color: blue;">&gt;</span></p>
<p style="margin: 0px;"><span style="color: blue;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; &lt;</span><span style="color: #a31515;">TimeStamp</span><span style="color: blue;">&gt;&lt;</span><span style="color: #a31515;">when</span><span style="color: blue;">&gt;</span>2017-07-06T12:00:00Z<span style="color: blue;">&lt;/</span><span style="color: #a31515;">when</span><span style="color: blue;">&gt;&lt;/</span><span style="color: #a31515;">TimeStamp</span><span style="color: blue;">&gt;</span></p>
<p style="margin: 0px;"><span style="color: blue;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; &lt;</span><span style="color: #a31515;">styleUrl</span><span style="color: blue;">&gt;</span>#style_8579852<span style="color: blue;">&lt;/</span><span style="color: #a31515;">styleUrl</span><span style="color: blue;">&gt;</span></p>
<p style="margin: 0px;"><span style="color: blue;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; &lt;</span><span style="color: #a31515;">Point</span><span style="color: blue;">&gt;</span></p>
<p style="margin: 0px;"><span style="color: blue;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; &lt;</span><span style="color: #a31515;">altitudeMode</span><span style="color: blue;">&gt;</span>absolute<span style="color: blue;">&lt;/</span><span style="color: #a31515;">altitudeMode</span><span style="color: blue;">&gt;</span></p>
<p style="margin: 0px;"><span style="color: blue;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; &lt;</span><span style="color: #a31515;">coordinates</span><span style="color: blue;">&gt;</span>-72.68740885096805,41.76193520621887,0<span style="color: blue;">&lt;/</span><span style="color: #a31515;">coordinates</span><span style="color: blue;">&gt;</span></p>
<p style="margin: 0px;"><span style="color: blue;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; &lt;/</span><span style="color: #a31515;">Point</span><span style="color: blue;">&gt;</span></p>
<p style="margin: 0px;"><span style="color: blue;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; &lt;/</span><span style="color: #a31515;">Placemark</span><span style="color: blue;">&gt;</span></p>
<p style="margin: 0px;"><span style="color: blue;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; &lt;</span><span style="color: #a31515;">Placemark</span><span style="color: blue;">&gt;</span></p>
<p style="margin: 0px;"><span style="color: blue;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; &lt;</span><span style="color: #a31515;">name</span><span style="color: blue;">&gt;</span>Kean Walmsley<span style="color: blue;">&lt;/</span><span style="color: #a31515;">name</span><span style="color: blue;">&gt;</span></p>
<p style="margin: 0px;"><span style="color: blue;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; &lt;</span><span style="color: #a31515;">description</span><span style="color: blue;">&gt;</span>Kean Walmsley<span style="color: red;">&amp;apos;</span>s track log<span style="color: blue;">&lt;/</span><span style="color: #a31515;">description</span><span style="color: blue;">&gt;</span></p>
<p style="margin: 0px;"><span style="color: blue;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; &lt;</span><span style="color: #a31515;">styleUrl</span><span style="color: blue;">&gt;</span>#linestyle_857985<span style="color: blue;">&lt;/</span><span style="color: #a31515;">styleUrl</span><span style="color: blue;">&gt;</span></p>
<p style="margin: 0px;"><span style="color: blue;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; &lt;</span><span style="color: #a31515;">LineString</span><span style="color: blue;">&gt;</span></p>
<p style="margin: 0px;"><span style="color: blue;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; &lt;</span><span style="color: #a31515;">tessellate</span><span style="color: blue;">&gt;</span>1<span style="color: blue;">&lt;/</span><span style="color: #a31515;">tessellate</span><span style="color: blue;">&gt;</span></p>
<p style="margin: 0px;"><span style="color: blue;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; &lt;</span><span style="color: #a31515;">coordinates</span><span style="color: blue;">&gt;</span>7.001589598912035,47.00918080225755,0 -0.4879806004656406,51.47146600045286,0 -77.45653879988311,38.95311619998505,0 -77.05284809999405,38.90030650017117,0 -77.00250794999864,38.90428664917936,0 -73.99352333750556,40.75056537490029,0 -73.91963239878011,40.74327590022647,0 -73.98512727848284,40.75889951208065,0 -73.96535509973162,40.78286469982217,0 -73.98512727848284,40.75889951208065,0 -72.68740885096805,41.76193520621887,0<span style="color: blue;">&lt;/</span><span style="color: #a31515;">coordinates</span><span style="color: blue;">&gt;</span></p>
<p style="margin: 0px;"><span style="color: blue;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; &lt;/</span><span style="color: #a31515;">LineString</span><span style="color: blue;">&gt;</span></p>
<p style="margin: 0px;"><span style="color: blue;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; &lt;/</span><span style="color: #a31515;">Placemark</span><span style="color: blue;">&gt;</span></p>
<p style="margin: 0px;"><span style="color: blue;">&#0160;&#0160;&#0160;&#0160;&#0160; &lt;/</span><span style="color: #a31515;">Folder</span><span style="color: blue;">&gt;</span></p>
<p style="margin: 0px;"><span style="color: blue;">&#0160;&#0160;&#0160; &lt;/</span><span style="color: #a31515;">Document</span><span style="color: blue;">&gt;</span></p>
<p style="margin: 0px;"><span style="color: blue;">&#0160; &lt;/</span><span style="color: #a31515;">Folder</span><span style="color: blue;">&gt;</span></p>
<p style="margin: 0px;"><span style="color: blue;">&lt;/</span><span style="color: #a31515;">Folder</span><span style="color: blue;">&gt;</span></p>
<p style="margin: 0px;"><span style="color: blue;">&lt;/</span><span style="color: #a31515;">kml</span><span style="color: blue;">&gt;</span></p>
</div>
<p>Here’s the updated HTML file:</p>
<div style="background: white; color: black; line-height: 140%; font-family: courier new; font-size: 8pt;">
<p style="margin: 0px;"><span style="color: blue;">&lt;</span><span style="color: maroon;">html</span><span style="color: blue;">&gt;</span></p>
<p style="margin: 0px;">&#0160; <span style="color: blue;">&lt;</span><span style="color: maroon;">head</span><span style="color: blue;">&gt;</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">&lt;</span><span style="color: maroon;">style</span><span style="color: blue;">&gt;</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: maroon;">html</span>,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: maroon;">body</span> {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: red;">padding</span>: <span style="color: blue;">0</span>;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: red;">margin</span>: <span style="color: blue;">0</span>;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: maroon;">#map</span> {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: red;">height</span>: <span style="color: blue;">100%</span>;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: red;">width</span>: <span style="color: blue;">100%</span>;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: red;">overflow</span>: <span style="color: blue;">hidden</span>;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: red;">float</span>: <span style="color: blue;">left</span>;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: red;">border</span>: <span style="color: blue;">thin</span> <span style="color: blue;">solid</span> <span style="color: blue;">#333</span>;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: maroon;">h3</span> {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: red;">margin</span>: <span style="color: blue;">0</span> <span style="color: blue;">0</span> <span style="color: blue;">5px</span> <span style="color: blue;">0</span>;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: red;">white-space</span>: <span style="color: blue;">nowrap</span>;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: red;">overflow</span>: <span style="color: blue;">hidden</span>;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: red;">text-overflow</span>: <span style="color: blue;">ellipsis</span>;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: maroon;">p</span> {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: red;">margin</span>: <span style="color: blue;">0</span> <span style="color: blue;">0</span> <span style="color: blue;">10px</span> <span style="color: blue;">0</span>;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: red;">white-space</span>: <span style="color: blue;">nowrap</span>;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: red;">overflow</span>: <span style="color: blue;">hidden</span>;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: red;">text-overflow</span>: <span style="color: blue;">ellipsis</span>;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">&lt;/</span><span style="color: maroon;">style</span><span style="color: blue;">&gt;</span></p>
<p style="margin: 0px;">&#0160; <span style="color: blue;">&lt;/</span><span style="color: maroon;">head</span><span style="color: blue;">&gt;</span></p>
<p style="margin: 0px;">&#0160; <span style="color: blue;">&lt;</span><span style="color: maroon;">body</span><span style="color: blue;">&gt;</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">&lt;</span><span style="color: maroon;">div</span> <span style="color: red;">id</span><span style="color: blue;">=&quot;map&quot;&gt;&lt;/</span><span style="color: maroon;">div</span><span style="color: blue;">&gt;</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">&lt;</span><span style="color: maroon;">script</span> <span style="color: red;">async</span> <span style="color: red;">defer</span> <span style="color: red;">src</span><span style="color: blue;">=&quot;https://maps.googleapis.com/maps/api/js?key=AIzaSyDtsYI6-syxALgcCkz3hjsAnzK8_4C_OSc&amp;libraries=places&amp;callback=initMap&quot;&gt;&lt;/</span><span style="color: maroon;">script</span><span style="color: blue;">&gt;</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">&lt;</span><span style="color: maroon;">script</span> <span style="color: red;">type</span><span style="color: blue;">=&#39;text/javascript&#39;&gt;</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">var</span> map;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">var</span> infowindow;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">var</span> service;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">var</span> overlayBase = <span style="color: #a31515;">&#39;https://inreach.garmin.com/feed/Share/mondeEnPoche?&#39;</span>;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">var</span> locals = [ <span style="color: #a31515;">&#39;pre-tracking.kml&#39;</span>, <span style="color: #a31515;">&#39;2017-07-to-12.kmz&#39;</span> ];</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">var</span> overlays = [</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">// &#39;d1=2017-07-01T00:00Z&amp;d2=2017-08-01T00:00Z&#39;,</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">// &#39;d1=2017-08-01T00:00Z&amp;d2=2017-09-01T00:00Z&#39;,</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">// &#39;d1=2017-09-01T00:00Z&amp;d2=2017-10-01T00:00Z&#39;,</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">// &#39;d1=2017-10-01T00:00Z&amp;d2=2017-11-01T00:00Z&#39;,</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">// &#39;d1=2017-11-01T00:00Z&amp;d2=2017-12-01T00:00Z&#39;,</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: #a31515;">&#39;d1=2017-12-01T00:00Z&amp;d2=2018-01-05T00:00Z&#39;</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; ];</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">// Data for the markers consisting of a name, a LatLng and a zIndex for the</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">// order in which these markers should display on top of each other.</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">var</span> stops = [</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; [<span style="color: #a31515;">&#39;Marin-Epagnier&#39;</span>, <span style="color: #a31515;">&#39;ChIJt4RhN0UJjkcR9dxtPHTvIRY&#39;</span>, 47.0091808, 7.0015896, 1, <span style="color: #a31515;">&#39;Start &amp; End&#39;</span>],</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; [<span style="color: #a31515;">&#39;Washington&#39;</span>, <span style="color: #a31515;">&#39;ChIJW-T2Wt7Gt4kRKl2I1CJFUsI&#39;</span>, 38.9071923, -77.0368707, 2],</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; [<span style="color: #a31515;">&#39;New York&#39;</span>, <span style="color: #a31515;">&#39;ChIJOwg_06VPwokRYv534QaPC8g&#39;</span>, 40.7127837, -74.0059413, 3],</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; [<span style="color: #a31515;">&#39;West Hartford&#39;</span>, <span style="color: #a31515;">&#39;ChIJ_RQEifWs54kRfDtRDlPX-Wc&#39;</span>, 41.7620842, -72.7420151, 4],</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; [<span style="color: #a31515;">&#39;Boston&#39;</span>, <span style="color: #a31515;">&#39;ChIJGzE9DS1l44kRoOhiASS_fHg&#39;</span>, 42.3600825, -71.0588801, 5, <span style="color: #a31515;">&#39;July&#39;</span>],</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; [<span style="color: #a31515;">&#39;Toronto&#39;</span>, <span style="color: #a31515;">&#39;ChIJpTvG15DL1IkRd8S0KlBVNTI&#39;</span>, 43.653226, -79.3831843, 6],</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; [<span style="color: #a31515;">&#39;Bozeman&#39;</span>, <span style="color: #a31515;">&#39;ChIJE4i6T0xERVMRqmA792TQ9WM&#39;</span>, 45.6769979, -111.0429339, 7],</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; [<span style="color: #a31515;">&#39;Yellowstone National Park&#39;</span>, <span style="color: #a31515;">&#39;ChIJVVVVVVXlUVMRu-GPNDD5qKw&#39;</span>, 44.427963, -110.588455, 8],</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; [<span style="color: #a31515;">&#39;Grand Teton National Park&#39;</span>, <span style="color: #a31515;">&#39;ChIJqRtdyZ5RUlMRN6ORzI64oKU&#39;</span>, 43.7904282, -110.6817627, 9],</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; [<span style="color: #a31515;">&#39;Salt Lake City&#39;</span>, <span style="color: #a31515;">&#39;ChIJ7THRiJQ9UocRyjFNSKC3U1s&#39;</span>, 40.7607793, -111.8910474, 10],</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; [<span style="color: #a31515;">&#39;Bryce Canyon&#39;</span>, <span style="color: #a31515;">&#39;ChIJbUw47h9pNYcRYv1Jemw3nHU&#39;</span>, 37.6283161, -112.1676947, 11],</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; [<span style="color: #a31515;">&#39;Zion National Park&#39;</span>, <span style="color: #a31515;">&#39;ChIJ2fhEiNDqyoAR9VY2qhU6Lnw&#39;</span>, 37.2982022, -113.0263005, 12],</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; [<span style="color: #a31515;">&#39;Las Vegas&#39;</span>, <span style="color: #a31515;">&#39;ChIJ0X31pIK3voARo3mz1ebVzDo&#39;</span>, 36.1699412, -115.1398296, 13],</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; [<span style="color: #a31515;">&#39;Death Valley&#39;</span>, <span style="color: #a31515;">&#39;ChIJsf-PHqI5x4ARJd0j14NziRw&#39;</span>, 36.5322649, -116.9325408, 14],</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; [<span style="color: #a31515;">&#39;Sequoia National Park&#39;</span>, <span style="color: #a31515;">&#39;ChIJeWUZLX37v4ARZPQen_nfCkQ&#39;</span>, 36.4863668, -118.5657516, 15],</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; [<span style="color: #a31515;">&#39;Big Sur&#39;</span>, <span style="color: #a31515;">&#39;ChIJVVikTfuPjYARYuO38cfXpRY&#39;</span>, 36.2704212, -121.807976, 16],</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; [<span style="color: #a31515;">&#39;Monterey&#39;</span>, <span style="color: #a31515;">&#39;ChIJkfu1cFLkjYARXj1K2AlJSO4&#39;</span>, 36.6002378, -121.8946761, 17],</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; [<span style="color: #a31515;">&#39;San Francisco&#39;</span>, <span style="color: #a31515;">&#39;ChIJIQBpAG2ahYAR_6128GcTUEo&#39;</span>, 37.7749295, -122.4194155, 18, <span style="color: #a31515;">&#39;August&#39;</span>],</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; [<span style="color: #a31515;">&#39;Lima&#39;</span>, <span style="color: #a31515;">&#39;ChIJ3-EpLOzDBZERRBEzku1Ooak&#39;</span>, -12.0463667, -77.0427891, 19],</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; [<span style="color: #a31515;">&#39;Machu Picchu&#39;</span>, <span style="color: #a31515;">&#39;ChIJVVVViV-abZERJxqgpA43EDo&#39;</span>, -13.1631412, -72.5449629, 20],</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; [<span style="color: #a31515;">&#39;Cusco&#39;</span>, <span style="color: #a31515;">&#39;ChIJMYRZJtjVbZERXTEYI8yWqSo&#39;</span>, -13.53195, -71.9674626, 21],</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; [<span style="color: #a31515;">&#39;S�o Paulo&#39;</span>, <span style="color: #a31515;">&#39;ChIJ0WGkg4FEzpQRrlsz_whLqZs&#39;</span>, -23.5505199, -46.6333094, 22],</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; [<span style="color: #a31515;">&#39;Rio de Janeiro&#39;</span>, <span style="color: #a31515;">&#39;ChIJW6AIkVXemwARTtIvZ2xC3FA&#39;</span>, -22.9068467, -43.1728965, 23, <span style="color: #a31515;">&#39;September&#39;</span>],</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; [<span style="color: #a31515;">&#39;Iguazu Falls&#39;</span>, <span style="color: #a31515;">&#39;ChIJbRuqowzq9pQRfphenBd1e5E&#39;</span>, -25.695259, -54.4388549, 24],</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; [<span style="color: #a31515;">&#39;C�rdoba&#39;</span>, <span style="color: #a31515;">&#39;ChIJaVuPR1-YMpQRkrBmU5pPorA&#39;</span>, -31.4200833, -64.1887761, 25],</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; [<span style="color: #a31515;">&#39;Parque Provincial Ischigualasto&#39;</span>, <span style="color: #a31515;">&#39;ChIJwynmBT3sgpYR0J11F_1O5cw&#39;</span>, -30.167266,-67.9860327, 26],</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; [<span style="color: #a31515;">&#39;Parque Nacional Talampaya&#39;</span>, <span style="color: #a31515;">&#39;ChIJUUxbf6rPgpYRaEkBxpGDANQ&#39;</span>, -29.8906226, -67.853468, 27],</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; [<span style="color: #a31515;">&#39;Catamarca&#39;</span>, <span style="color: #a31515;">&#39;ChIJzZ8PHb8oJJQRGoYJFkvdHn4&#39;</span>, -28.469581, -65.7795441, 28],</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; [<span style="color: #a31515;">&#39;San Miguel de Tucum�n&#39;</span>, <span style="color: #a31515;">&#39;ChIJA2nF1pI3IpQRJ2XFtZJbjfg&#39;</span>, -26.8082848, -65.2175903, 29],</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; [<span style="color: #a31515;">&#39;Salta&#39;</span>, <span style="color: #a31515;">&#39;ChIJ-bdRUaPDG5QRBvKH1SyZzaU&#39;</span>, -24.7821269, -65.4231976, 30],</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; [<span style="color: #a31515;">&#39;Salar de Uyuni&#39;</span>, <span style="color: #a31515;">&#39;ChIJh9rdHuC6_5MRkFuFng0T5RI&#39;</span>, -20.1595348, -67.4054025, 31],</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; [<span style="color: #a31515;">&#39;San Pedro de Atacama&#39;</span>, <span style="color: #a31515;">&#39;ChIJP78qqXpMqJYR0Zf5rExh9Ho&#39;</span>, -22.9087073, -68.1997156, 32],</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; [<span style="color: #a31515;">&#39;Pan de Az�car National Park&#39;</span>, <span style="color: #a31515;">&#39;ChIJM6BM4cewvJYRbC7GcVat_6U&#39;</span>, -26.177565, -70.5495396, 33],</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; [<span style="color: #a31515;">&#39;Ra�l Marine Balmaceda&#39;</span>, <span style="color: #a31515;">&#39;ChIJ4V-JqObIkZYRiGptmZGVUn8&#39;</span>, -29.9695076, -71.3416309, 34],</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; [<span style="color: #a31515;">&#39;Santiago&#39;</span>, <span style="color: #a31515;">&#39;ChIJuzrymgbQYpYRl0jtCfRZnYc&#39;</span>, -33.4378305, -70.6504492, 35],</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; [<span style="color: #a31515;">&#39;Easter Island&#39;</span>, <span style="color: #a31515;">&#39;ChIJK67UqBfwR5kRti0qwO2z5bs&#39;</span>, -27.112723, -109.3496865, 36],</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; [<span style="color: #a31515;">&#39;Tahiti&#39;</span>, <span style="color: #a31515;">&#39;ChIJTddtfNB1GHQREVfDCXp6wJs&#39;</span>, -17.6509195, -149.4260421, 37],</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; [<span style="color: #a31515;">&#39;Auckland&#39;</span>, <span style="color: #a31515;">&#39;ChIJ--acWvtHDW0RF5miQ2HvAAU&#39;</span>, -36.8484597, 174.7633315, 38],</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; [<span style="color: #a31515;">&#39;Rotorua&#39;</span>, <span style="color: #a31515;">&#39;ChIJK7L2gj2Ybm0RMZmjQ2HvAAU&#39;</span>, -38.1368478, 176.2497461, 39, <span style="color: #a31515;">&#39;October&#39;</span>],</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; [<span style="color: #a31515;">&#39;Wellington&#39;</span>, <span style="color: #a31515;">&#39;ChIJy3TpSfyxOG0RcLQTomPvAAo&#39;</span>, -41.2864603, 174.776236, 40],</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; [<span style="color: #a31515;">&#39;Paparoa National Park&#39;</span>, <span style="color: #a31515;">&#39;ChIJbZoxICBxJW0RIPF5hIbvAAU&#39;</span>, -42.1632433, 171.366731, 41],</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; [<span style="color: #a31515;">&#39;Queenstown&#39;</span>, <span style="color: #a31515;">&#39;ChIJX96o1_Ed1akRAKZ5hIbvAAU&#39;</span>, -45.0311622, 168.6626435, 42],</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; [<span style="color: #a31515;">&#39;Sydney&#39;</span>, <span style="color: #a31515;">&#39;ChIJP5iLHkCuEmsRwMwyFmh9AQU&#39;</span>, -33.8688197, 151.2092955, 43],</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; [<span style="color: #a31515;">&#39;Brisbane&#39;</span>, <span style="color: #a31515;">&#39;ChIJM9KTrJpXkWsRQK_e81qjAgQ&#39;</span>, -27.4697707, 153.0251235, 44],</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; [<span style="color: #a31515;">&#39;Cairns&#39;</span>, <span style="color: #a31515;">&#39;ChIJEySiW1VieGkRYHggf_HuAAQ&#39;</span>, -16.9185514, 145.7780548, 45],</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; [<span style="color: #a31515;">&#39;Kuala Lumpur&#39;</span>, <span style="color: #a31515;">&#39;ChIJ5-rvAcdJzDERfSgcL1uO2fQ&#39;</span>, 3.139003, 101.686855, 46],</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; [<span style="color: #a31515;">&#39;Singapore&#39;</span>, <span style="color: #a31515;">&#39;ChIJdZOLiiMR2jERxPWrUs9peIg&#39;</span>, 1.352083, 103.819836, 47],</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; [<span style="color: #a31515;">&#39;Coimbatore&#39;</span>, <span style="color: #a31515;">&#39;ChIJtRyXL69ZqDsRgtI-GB7IwS8&#39;</span>, 11.0168445, 76.9558321, 48],</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; [<span style="color: #a31515;">&#39;Kodaikanal&#39;</span>, <span style="color: #a31515;">&#39;ChIJhwMKf2NmBzsRPMFYNzfp-p8&#39;</span>, 10.2381136, 77.4891822, 49],</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; [<span style="color: #a31515;">&#39;Bangalore&#39;</span>, <span style="color: #a31515;">&#39;ChIJbU60yXAWrjsR4E9-UejD3_g&#39;</span>, 12.9715987, 77.5945627, 50, <span style="color: #a31515;">&#39;November&#39;</span>],</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; [<span style="color: #a31515;">&#39;Durban&#39;</span>, <span style="color: #a31515;">&#39;ChIJt2G8AQCq9x4RgW6qxEZVp8w&#39;</span>, -29.8586804, 31.0218404, 51],</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; [<span style="color: #a31515;">&#39;Lesotho&#39;</span>, <span style="color: #a31515;">&#39;ChIJ64xf1idIjB4Rsx7ReLhXLSM&#39;</span>, -29.609988, 28.233608, 52, <span style="color: #a31515;">&#39;December&#39;</span>],</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; [<span style="color: #a31515;">&#39;Addo Elephant National Park&#39;</span>, <span style="color: #a31515;">&#39;ChIJY2nuzYRPex4RCsT--8cm454&#39;</span>, -33.4833333, 25.75, 53],</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; [<span style="color: #a31515;">&#39;Tsitsikamma&#39;</span>, <span style="color: #a31515;">&#39;ChIJaTwmTQ5ueR4R5_kNGLX6RBs&#39;</span>, -32.2178721, 26.5772048, 54],</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; [<span style="color: #a31515;">&#39;Knysna&#39;</span>, <span style="color: #a31515;">&#39;ChIJ2QwBlkDqeB4Rzc5QdeG5Kr4&#39;</span>, -34.0350856, 23.0464693, 55],</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; [<span style="color: #a31515;">&#39;Oudtshoorn&#39;</span>, <span style="color: #a31515;">&#39;ChIJtRO16obB1R0RYesIjnRHQ40&#39;</span>, -33.6007225, 22.2026347, 56],</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; [<span style="color: #a31515;">&#39;Franschhoek&#39;</span>, <span style="color: #a31515;">&#39;ChIJz7IFaAe9zR0R-bJW01SGtDw&#39;</span>, -33.8974833, 19.1523292, 57],</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; [<span style="color: #a31515;">&#39;Stellenbosch&#39;</span>, <span style="color: #a31515;">&#39;ChIJpeKIUfeyzR0R4mvj3gCqCXA&#39;</span>, -33.9321045, 18.860152, 58],</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; [<span style="color: #a31515;">&#39;Cape Town&#39;</span>, <span style="color: #a31515;">&#39;ChIJ1-4miA9QzB0Rh6ooKPzhf2g&#39;</span>, -33.9248685, 18.4240553, 59]</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; ];</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">var</span> labels = [</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; [<span style="color: #a31515;">&#39;Start &amp; End&#39;</span>, 47.0091808, 7.0015896, 1],</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; [<span style="color: #a31515;">&#39;July&#39;</span>, 42.409143, -102.280372, 2],</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; [<span style="color: #a31515;">&#39;August&#39;</span>, 5.247246, -73.979869, 3],</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; [<span style="color: #a31515;">&#39;September&#39;</span>, -36.753594, -65.018287, 4],</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; [<span style="color: #a31515;">&#39;October&#39;</span>, -33.622306, 160.985311, 5],</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; [<span style="color: #a31515;">&#39;November&#39;</span>, 16.921484, 91.724302, 6],</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; [<span style="color: #a31515;">&#39;December&#39;</span>, -16.011953, 23.167125, 7]</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; ];</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">function</span> initMap() {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; map = <span style="color: blue;">new</span> google.maps.Map(document.getElementById(<span style="color: #a31515;">&#39;map&#39;</span>), {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; center: <span style="color: blue;">new</span> google.maps.LatLng(15, -30),</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; zoom: 2,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; mapTypeId: <span style="color: #a31515;">&#39;satellite&#39;</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; });</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; infowindow = <span style="color: blue;">new</span> google.maps.InfoWindow();</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; service = <span style="color: blue;">new</span> google.maps.places.PlacesService(map);</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; loadJS(<span style="color: #a31515;">&#39;./maplabel-compiled.js&#39;</span>, onInit, document.body);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">function</span> loadJS(url, implementationCode, location){</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">var</span> scriptTag = document.createElement(<span style="color: #a31515;">&#39;script&#39;</span>);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; scriptTag.src = url;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; scriptTag.onload = implementationCode;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; scriptTag.onreadystatechange = implementationCode;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; location.appendChild(scriptTag);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; };</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">function</span> onInit(){</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; setMarkers(map, <span style="color: blue;">function</span>() {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">var</span> href = window.location.href;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">var</span> dir = href.substring(0, href.lastIndexOf(<span style="color: #a31515;">&#39;/&#39;</span>)) + <span style="color: #a31515;">&#39;/&#39;</span>;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">for</span> (<span style="color: blue;">var</span> i = 0; i &lt; locals.length; i++) {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">var</span> kmlLocalOverlayer = <span style="color: blue;">new</span> google.maps.KmlLayer(dir + locals[i], {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; suppressInfoWindows: <span style="color: blue;">true</span>,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; preserveViewport: <span style="color: blue;">true</span>,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; map: map</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; });</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">for</span> (<span style="color: blue;">var</span> i = 0; i &lt; overlays.length; i++) {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">var</span> kmlOverlayer = <span style="color: blue;">new</span> google.maps.KmlLayer(overlayBase + overlays[i], {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; suppressInfoWindows: <span style="color: blue;">true</span>,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; preserveViewport: <span style="color: blue;">true</span>,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; map: map</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; });</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; });</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">function</span> setMarkers(map, callback) {</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">// Adds markers to the map with a delay</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">var</span> delay = 50;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">for</span> (<span style="color: blue;">var</span> i = 0; i &lt;= stops.length; i++) {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">var</span> timeout = i * delay;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">// If this is the last segment, just add the line</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">if</span> (i === stops.length) {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; addConnectingLineWithTimeout(stops[i - 1], stops[0], timeout + delay);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">if</span> (callback) {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; setTimeout(callback, timeout + delay);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; } <span style="color: blue;">else</span> <span style="color: blue;">if</span> (i &gt;= 0) {</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">// Otherwise add a marker after a delay, followed by the</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">// connecting line to the previous marker, if there is one</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; addMarkerWithTimeout(stops[i], timeout);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">if</span> (i &gt; 0) {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; addConnectingLineWithTimeout(stops[i], stops[i - 1], timeout + delay);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">function</span> addMarkerWithTimeout(stop, timeout) {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; setTimeout(<span style="color: blue;">function</span>() {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">var</span> marker = <span style="color: blue;">new</span> google.maps.Marker({</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; map: map,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; title: stop[0],</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; placeId: stop[1],</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; position: {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; lat: stop[2],</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; lng: stop[3]</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; },</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; label: stop[4].toString(),</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; zIndex: stop[4]</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">//animation: google.maps.Animation.DROP, // Cool but too much</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; });</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">// If we have a label listed, find out which and add it to the map</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">if</span> (stop.length &gt; 5) {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">var</span> idx = labels.findIndex(<span style="color: blue;">function</span>(val) {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">return</span> val[0] === stop[5];</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; });</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">if</span> (idx &gt;= 0) {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">var</span> label = labels[idx];</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; addLabelWithTimeout(label[1], label[2], label[0], 0);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">// Register the callback for when the marker is clicked</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; google.maps.event.addListener(marker, <span style="color: #a31515;">&#39;click&#39;</span>, <span style="color: blue;">function</span>() {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; onItemClick(event, marker);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; });</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; }, timeout);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">function</span> addLabelWithTimeout(lat, long, text, timeout) {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; setTimeout(<span style="color: blue;">function</span>() {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">var</span> pos = <span style="color: blue;">new</span> google.maps.LatLng(lat, long);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">var</span> mapLabel = <span style="color: blue;">new</span> MapLabel({</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; text: text,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; position: pos,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; map: map,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; fontSize: 14</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; });</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; mapLabel.set(<span style="color: #a31515;">&#39;position&#39;</span>, <span style="color: blue;">new</span> google.maps.LatLng(lat, long));</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; }, timeout);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">function</span> addConnectingLineWithTimeout(stop1, stop2, timeout) {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; setTimeout(<span style="color: blue;">function</span>() {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">var</span> flightPath = <span style="color: blue;">new</span> google.maps.Polyline({</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; path: [{</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; lat: stop1[2],</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; lng: stop1[3]</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; }, {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; lat: stop2[2],</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; lng: stop2[3]</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; }],</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; geodesic: <span style="color: blue;">true</span>,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; strokeColor: <span style="color: #a31515;">&#39;#D34038&#39;</span>,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; strokeOpacity: 1.0,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; strokeWeight: 4</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; });</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; flightPath.setMap(map);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; }, timeout);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">// Info window trigger function</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">function</span> onItemClick(event, pin) {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; service.getDetails({</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; placeId: pin.placeId</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; }, <span style="color: blue;">function</span>(place, status) {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">var</span> cont =</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: #a31515;">&#39;&lt;div&gt;&lt;h3&gt;&#39;</span> + place.name + <span style="color: #a31515;">&#39;&lt;/h3&gt;&lt;p&gt;&#39;</span> + place.formatted_address + <span style="color: #a31515;">&#39;&lt;/p&gt;&#39;</span> +</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; (place.photos &amp;&amp; place.photos.length &gt; 0 ?</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; (<span style="color: #a31515;">&#39;&lt;img src=&quot;&#39;</span> +</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; place.photos[0].getUrl({</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: #a31515;">&#39;maxWidth&#39;</span>: 300,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: #a31515;">&#39;maxHeight&#39;</span>: 200</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; }) + <span style="color: #a31515;">&#39;&quot; /&gt;&#39;</span>) : <span style="color: #a31515;">&#39;&#39;</span>) +</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: #a31515;">&#39;&lt;/div&gt;&#39;</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; infowindow.setContent(cont);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; infowindow.open(map, pin);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; });</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">&lt;/</span><span style="color: maroon;">script</span><span style="color: blue;">&gt;</span></p>
<p style="margin: 0px;">&#0160; <span style="color: blue;">&lt;/</span><span style="color: maroon;">body</span><span style="color: blue;">&gt;</span></p>
<p style="margin: 0px;"><span style="color: blue;">&lt;/</span><span style="color: maroon;">html</span><span style="color: blue;">&gt;</span></p>
</div>
<p>Here’s the embedded map (<a href="http://walmsley.ch/">which you can also find on our trip’s website</a>).</p>
<p style="text-align: center;"><iframe height="300" src="https://through-the-interface.typepad.com/files/maps/mapp.html" width="500"></iframe></p>
<p>The site seems to work well again, now: once the trip is done I’ll integrate the last month of tracking data into the KMZ, to avoid any further visits to the Garmin service.</p>
