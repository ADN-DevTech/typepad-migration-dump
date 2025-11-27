---
layout: "post"
title: "Displaying a round-the-world itinerary using Google Maps &ndash; Part 1"
date: "2017-06-28 20:01:30"
author: "Kean Walmsley"
categories:
  - "HTML"
  - "JavaScript"
  - "Round the world"
original_url: "https://www.keanw.com/2017/06/displaying-a-round-the-world-itinerary-using-google-maps-part-1.html "
typepad_basename: "displaying-a-round-the-world-itinerary-using-google-maps-part-1"
typepad_status: "Publish"
---

<p>Over the next few posts I’m going to talk about the process I went through to integrate the itinerary for <a href="http://keanw.com/2017/06/meet-the-walmsleys.html" target="_blank">our imminent round-the-world trip</a> into <a href="http://walmsley.ch" target="_blank">the awesome web-site my wife has set up</a>.</p><p>The initial approach I took was to use <a href="https://www.google.com/mymaps" target="_blank">Google My Maps</a>: this is a really cool tool for creating a custom map without any coding needed (yes, I know – that doesn’t sound like much fun :-). For instance, you can set up your placemarks and link them with a route, and when individual places are clicked it shows a nice sliding info panel.</p><p>I started out with creating individual routes between places, but then ended up creating a single one for the whole tour. As much as anything to see the total length: it showed we’ll be travelling upwards of 75,000 km! Crazy.</p><p><a href="http://through-the-interface.typepad.com/.a/6a00d83452464869e201b8d291113b970c-pi" target="_blank"><img width="500" height="319" title="RTW itinerary in My Maps" style="margin: 30px auto; border: 0px currentcolor; border-image: none; float: none; display: block; background-image: none;" alt="RTW itinerary in My Maps" src="/assets/image_517446.jpg" border="0"></a></p><p>You can, of course, embed the created map into your website:</p><p align="center"><br></p><p align="center"><iframe width="500" height="300" src="https://www.google.com/maps/d/u/0/embed?mid=1vq1hwZs5xZP5tBGE6GoKzXV3jz0"></iframe></p><p align="center"><br></p><p>I like many of the aspects of My Maps, but ultimately wanted more control for this particular project. Part of this is about integrating live tracking data, which we’ll see in an upcoming post. So I exported the map to KML (there’s a layer of indirection you may need to overcome by then loading the embedded KML reference) and then renamed this to XML so that I could open it in Excel and easily copy &amp; paste the placemark information.</p><p>I put the location data into an HTML page that includes some simple code to animate the addition of placemarkers and the links between them. Here’s the JavaScript code:</p><p><div style="background: white; color: black; line-height: 140%; font-family: courier new; font-size: 8pt;">
<p style="margin: 0px;"><span style="color: blue;">&lt;</span><span style="color: maroon;">html</span><span style="color: blue;">&gt;</span></p>
<p style="margin: 0px;">&nbsp; <span style="color: blue;">&lt;</span><span style="color: maroon;">head</span><span style="color: blue;">&gt;</span></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; <span style="color: blue;">&lt;</span><span style="color: maroon;">style</span><span style="color: blue;">&gt;</span></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; <span style="color: maroon;">html</span>,</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; <span style="color: maroon;">body</span> {</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: red;">padding</span>: <span style="color: blue;">0</span>;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: red;">margin</span>: <span style="color: blue;">0</span>;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; }</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; <span style="color: maroon;">#map</span> {</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: red;">height</span>: <span style="color: blue;">100%</span>;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: red;">width</span>: <span style="color: blue;">100%</span>;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: red;">overflow</span>: <span style="color: blue;">hidden</span>;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: red;">float</span>: <span style="color: blue;">left</span>;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: red;">border</span>: <span style="color: blue;">thin</span> <span style="color: blue;">solid</span> <span style="color: blue;">#333</span>;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; }</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; <span style="color: blue;">&lt;/</span><span style="color: maroon;">style</span><span style="color: blue;">&gt;</span></p>
<p style="margin: 0px;">&nbsp; <span style="color: blue;">&lt;/</span><span style="color: maroon;">head</span><span style="color: blue;">&gt;</span></p>
<p style="margin: 0px;">&nbsp; <span style="color: blue;">&lt;</span><span style="color: maroon;">body</span><span style="color: blue;">&gt;</span></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; <span style="color: blue;">&lt;</span><span style="color: maroon;">div</span> <span style="color: red;">id</span><span style="color: blue;">="map"&gt;&lt;/</span><span style="color: maroon;">div</span><span style="color: blue;">&gt;</span></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; <span style="color: blue;">&lt;</span><span style="color: maroon;">script</span> <span style="color: red;">async</span> <span style="color: red;">defer</span> <span style="color: red;">src</span><span style="color: blue;">="https://maps.googleapis.com/maps/api/js?key=[ENTER_YOUR_KEY_HERE]&amp;libraries=places&amp;callback=initMap"&gt;&lt;/</span><span style="color: maroon;">script</span><span style="color: blue;">&gt;</span></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; <span style="color: blue;">&lt;</span><span style="color: maroon;">script</span> <span style="color: red;">type</span><span style="color: blue;">='text/javascript'&gt;</span></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; <span style="color: blue;">var</span> map;</p>
<p style="margin: 0px;"><br></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; <span style="color: green;">// Data for the markers consisting of a name, a LatLng and a zIndex for the</span></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; <span style="color: green;">// order in which these markers should display on top of each other.</span></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; <span style="color: blue;">var</span> stops = [</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; [<span style="color: rgb(163, 21, 21);">'Marin-Epagnier'</span>, 47.0091808, 7.0015896, 1],</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; [<span style="color: rgb(163, 21, 21);">'Washington'</span>, 38.9071923, -77.0368707, 2],</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; [<span style="color: rgb(163, 21, 21);">'New York'</span>, 40.7127837, -74.0059413, 3],</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; [<span style="color: rgb(163, 21, 21);">'West Hartford'</span>, 41.7620842, -72.7420151, 4],</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; [<span style="color: rgb(163, 21, 21);">'Boston'</span>, 42.3600825, -71.0588801, 5],</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; [<span style="color: rgb(163, 21, 21);">'Toronto'</span>, 43.653226, -79.3831843, 6],</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; [<span style="color: rgb(163, 21, 21);">'Bozeman'</span>, 45.6769979, -111.0429339, 7],</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; [<span style="color: rgb(163, 21, 21);">'Yellowstone National Park'</span>, 44.427963, -110.588455, 8],</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; [<span style="color: rgb(163, 21, 21);">'Grand Teton National Park'</span>, 43.7904282, -110.6817627, 9],</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; [<span style="color: rgb(163, 21, 21);">'Salt Lake City'</span>, 40.7607793, -111.8910474, 10],</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; [<span style="color: rgb(163, 21, 21);">'Bryce Canyon'</span>, 37.6283161, -112.1676947, 11],</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; [<span style="color: rgb(163, 21, 21);">'Zion National Park'</span>, 37.2982022, -113.0263005, 12],</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; [<span style="color: rgb(163, 21, 21);">'Las Vegas'</span>, 36.1699412, -115.1398296, 13],</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; [<span style="color: rgb(163, 21, 21);">'Death Valley'</span>, 36.5322649, -116.9325408, 14],</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; [<span style="color: rgb(163, 21, 21);">'Sequoia National Park'</span>, 36.4863668, -118.5657516, 15],</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; [<span style="color: rgb(163, 21, 21);">'Big Sur'</span>, 36.2704212, -121.807976, 16],</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; [<span style="color: rgb(163, 21, 21);">'Monterey'</span>, 36.6002378, -121.8946761, 17],</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; [<span style="color: rgb(163, 21, 21);">'San Francisco'</span>, 37.7749295, -122.4194155, 18],</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; [<span style="color: rgb(163, 21, 21);">'Lima'</span>, -12.0463667, -77.0427891, 19],</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; [<span style="color: rgb(163, 21, 21);">'Machu Picchu'</span>, -13.1631412, -72.5449629, 20],</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; [<span style="color: rgb(163, 21, 21);">'Cusco'</span>, -13.53195, -71.9674626, 21],</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; [<span style="color: rgb(163, 21, 21);">'São Paulo'</span>, -23.5505199, -46.6333094, 22],</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; [<span style="color: rgb(163, 21, 21);">'Rio de Janeiro'</span>, -22.9068467, -43.1728965, 23],</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; [<span style="color: rgb(163, 21, 21);">'Iguazu Falls'</span>, -25.695259, -54.4388549, 24],</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; [<span style="color: rgb(163, 21, 21);">'Córdoba'</span>, -31.4200833, -64.1887761, 25],</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; [<span style="color: rgb(163, 21, 21);">'Parque Nacional Talampaya'</span>, -29.8906226, -67.853468, 26],</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; [<span style="color: rgb(163, 21, 21);">'Valle de la Luna'</span>, -22.9257639, -68.2879926, 27],</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; [<span style="color: rgb(163, 21, 21);">'Catamarca'</span>, -28.469581, -65.7795441, 28],</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; [<span style="color: rgb(163, 21, 21);">'San Miguel de Tucumán'</span>, -26.8082848, -65.2175903, 29],</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; [<span style="color: rgb(163, 21, 21);">'Salta'</span>, -24.7821269, -65.4231976, 30],</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; [<span style="color: rgb(163, 21, 21);">'Salar de Uyuni'</span>, -20.1595348, -67.4054025, 31],</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; [<span style="color: rgb(163, 21, 21);">'San Pedro de Atacama'</span>, -22.9087073, -68.1997156, 32],</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; [<span style="color: rgb(163, 21, 21);">'Pan de Azúcar National Park'</span>, -26.177565, -70.5495396, 33],</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; [<span style="color: rgb(163, 21, 21);">'Raúl Marine Balmaceda'</span>, -29.9695076, -71.3416309, 34],</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; [<span style="color: rgb(163, 21, 21);">'Santiago'</span>, -33.4378305, -70.6504492, 35],</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; [<span style="color: rgb(163, 21, 21);">'Easter Island'</span>, -27.112723, -109.3496865, 36],</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; [<span style="color: rgb(163, 21, 21);">'Tahiti'</span>, -17.6509195, -149.4260421, 37],</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; [<span style="color: rgb(163, 21, 21);">'Auckland'</span>, -36.8484597, 174.7633315, 38],</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; [<span style="color: rgb(163, 21, 21);">'Rotorua'</span>, -38.1368478, 176.2497461, 39],</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; [<span style="color: rgb(163, 21, 21);">'Wellington'</span>, -41.2864603, 174.776236, 40],</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; [<span style="color: rgb(163, 21, 21);">'Paparoa National Park'</span>, -42.1632433, 171.366731, 41],</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; [<span style="color: rgb(163, 21, 21);">'Queenstown'</span>, -45.0311622, 168.6626435, 42],</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; [<span style="color: rgb(163, 21, 21);">'Sydney'</span>, -33.8688197, 151.2092955, 43],</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; [<span style="color: rgb(163, 21, 21);">'Brisbane'</span>, -27.4697707, 153.0251235, 44],</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; [<span style="color: rgb(163, 21, 21);">'Cairns'</span>, -16.9185514, 145.7780548, 45],</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; [<span style="color: rgb(163, 21, 21);">'Kuala Lumpur'</span>, 3.139003, 101.686855, 46],</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; [<span style="color: rgb(163, 21, 21);">'Singapore'</span>, 1.352083, 103.819836, 47],</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; [<span style="color: rgb(163, 21, 21);">'Coimbatore'</span>, 11.0168445, 76.9558321, 48],</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; [<span style="color: rgb(163, 21, 21);">'Kodaikanal'</span>, 10.2381136, 77.4891822, 49],</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; [<span style="color: rgb(163, 21, 21);">'Bangalore'</span>, 12.9715987, 77.5945627, 50],</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; [<span style="color: rgb(163, 21, 21);">'Durban'</span>, -29.8586804, 31.0218404, 51],</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; [<span style="color: rgb(163, 21, 21);">'Lesotho'</span>, -29.609988, 28.233608, 52],</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; [<span style="color: rgb(163, 21, 21);">'Addo Elephant National Park'</span>, -33.4833333, 25.75, 53],</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; [<span style="color: rgb(163, 21, 21);">'Tsitsikamma'</span>, -32.2178721, 26.5772048, 54],</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; [<span style="color: rgb(163, 21, 21);">'Knysna'</span>, -34.0350856, 23.0464693, 55],</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; [<span style="color: rgb(163, 21, 21);">'Oudtshoorn'</span>, -33.6007225, 22.2026347, 56],</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; [<span style="color: rgb(163, 21, 21);">'Franschhoek'</span>, -33.8974833, 19.1523292, 57],</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; [<span style="color: rgb(163, 21, 21);">'Stellenbosch'</span>, -33.9321045, 18.860152, 58],</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; [<span style="color: rgb(163, 21, 21);">'Cape Town'</span>, -33.9248685, 18.4240553, 59]</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; ];</p>
<p style="margin: 0px;"><br></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; <span style="color: blue;">function</span> initMap() {</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; map = <span style="color: blue;">new</span> google.maps.Map(document.getElementById(<span style="color: rgb(163, 21, 21);">'map'</span>), {</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; center: <span style="color: blue;">new</span> google.maps.LatLng(15, -30),</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; zoom: 2,</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; mapTypeId: <span style="color: rgb(163, 21, 21);">'satellite'</span></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; });</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; setMarkers(map);</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; }</p>
<p style="margin: 0px;"><br></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; <span style="color: blue;">function</span> setMarkers(map) {</p>
<p style="margin: 0px;"><br></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: green;">// Adds markers to the map with a delay</span></p>
<p style="margin: 0px;"><br></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">var</span> delay = 100;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">for</span> (<span style="color: blue;">var</span> i = 0; i &lt;= stops.length; i++) {</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">var</span> timeout = i * delay;</p>
<p style="margin: 0px;"><br></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: green;">// If this is the last segment, just add the line</span></p>
<p style="margin: 0px;"><br></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">if</span> (i === stops.length) {</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; addConnectingLineWithTimeout(stops[i - 1], stops[0], timeout + delay);</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; } <span style="color: blue;">else</span> <span style="color: blue;">if</span> (i &gt;= 0) {</p>
<p style="margin: 0px;"><br></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: green;">// Otherwise add a marker after a delay, followed by the</span></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: green;">// connecting line to the previous marker, if there is one</span></p>
<p style="margin: 0px;"><br></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; addMarkerWithTimeout(stops[i], timeout);</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">if</span> (i &gt; 0) {</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; addConnectingLineWithTimeout(stops[i], stops[i - 1], timeout + delay);</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; }</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; }</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; }</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; }</p>
<p style="margin: 0px;"><br></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; <span style="color: blue;">function</span> addMarkerWithTimeout(stop, timeout) {</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; setTimeout(<span style="color: blue;">function</span>() {</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">var</span> marker = <span style="color: blue;">new</span> google.maps.Marker({</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; map: map,</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; title: stop[0],</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; placeId: stop[1],</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; position: {</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; lat: stop[1],</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; lng: stop[2]</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; },</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; label: stop[3].toString(),</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; zIndex: stop[3]</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: green;">//animation: google.maps.Animation.DROP, // Cool but too much</span></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; });</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; }, timeout);</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; }</p>
<p style="margin: 0px;"><br></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; <span style="color: blue;">function</span> addConnectingLineWithTimeout(stop1, stop2, timeout) {</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; setTimeout(<span style="color: blue;">function</span>() {</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">var</span> flightPath = <span style="color: blue;">new</span> google.maps.Polyline({</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; path: [{</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; lat: stop1[1],</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; lng: stop1[2]</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; }, {</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; lat: stop2[1],</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; lng: stop2[2]</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; }],</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; geodesic: <span style="color: blue;">true</span>,</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; strokeColor: <span style="color: rgb(163, 21, 21);">'#D34038'</span>,</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; strokeOpacity: 1.0,</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; strokeWeight: 4</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; });</p>
<p style="margin: 0px;"><br></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; flightPath.setMap(map);</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; }, timeout);</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; }</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; <span style="color: blue;">&lt;/</span><span style="color: maroon;">script</span><span style="color: blue;">&gt;</span></p>
<p style="margin: 0px;">&nbsp; <span style="color: blue;">&lt;/</span><span style="color: maroon;">body</span><span style="color: blue;">&gt;</span></p>
<p style="margin: 0px;"><span style="color: blue;">&lt;/</span><span style="color: maroon;">html</span><span style="color: blue;">&gt;</span></p><p style="margin: 0px;"><span style="color: blue;"><br></span></p>
</div>
<p>Here’s the embedded map:</p><p align="center"><br></p><p align="center">
<iframe width="500" height="300" src="https://through-the-interface.typepad.com/files/maps/map0.html"></iframe>
</p><p><br></p><p>In case you missed it, as this page loaded, here’s the animation of the route creation:</p><p><a href="http://through-the-interface.typepad.com/.a/6a00d83452464869e201b7c906be5b970b-pi"><img width="500" height="218" title="Animated itinerary" style="margin: 30px auto; float: none; display: block;" alt="Animated itinerary" src="/assets/image_823432.jpg"></a></p><p>For fun, here’s one last (ultimately aborted) attempt, using a bounce animation to add the various markers. I decided it was just a little too much.</p><p><a href="http://through-the-interface.typepad.com/.a/6a00d83452464869e201bb09a9f1fb970d-pi"><img width="500" height="218" title="Bouncy animated itinerary" style="margin: 30px auto; float: none; display: block;" alt="Bouncy animated itinerary" src="/assets/image_410203.jpg"></a></p><p>Next time we’re going to add some code that shows a picture in an information window when a placemark is clicked.</p>
