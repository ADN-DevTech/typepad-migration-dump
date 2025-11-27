---
layout: "post"
title: "Displaying a round-the-world itinerary using Google Maps &ndash; Part 5"
date: "2017-08-21 15:21:53"
author: "Kean Walmsley"
categories:
  - "HTML"
  - "JavaScript"
  - "Personal"
  - "Round the world"
original_url: "https://www.keanw.com/2017/08/displaying-a-round-the-world-itinerary-using-google-maps-part-5.html "
typepad_basename: "displaying-a-round-the-world-itinerary-using-google-maps-part-5"
typepad_status: "Publish"
---

<p>I really thought this series was finished, but there you go. In the first part we saw <a href="http://through-the-interface.typepad.com/through_the_interface/2017/06/displaying-a-round-the-world-itinerary-using-google-maps-part-1.html">a basic, embedded map</a> and then added <a href="http://through-the-interface.typepad.com/through_the_interface/2017/06/displaying-a-round-the-world-itinerary-using-google-maps-part-2.html">information windows</a> and <a href="http://through-the-interface.typepad.com/through_the_interface/2017/07/displaying-a-round-the-world-itinerary-using-google-maps-part-3.html">labels</a>. In the most recent part we added <a href="http://through-the-interface.typepad.com/through_the_interface/2017/07/displaying-a-round-the-world-itinerary-using-google-maps-part-4.html" rel="noopener noreferrer" target="_blank">overlay data being recorded by a tracking device to show our progress in real-time</a>.</p>
<p>Here’s the thing: a few days ago I realised the tracking information wasn’t being displayed. Initially I thought it might be due to limited connectivity or bandwidth – I was in Peru, at the time – but I then realised it was probably due to the volume of points being queried and overlaid… we’ve come far enough now that this has become a problem. I haven’t checked to see whether the problem was on Garmin’s side or on Google’s – whether the issue was querying the points or processing/displaying the overlay – but the below solution worked well enough for my immediate needs.</p>
<p>To fix the situation, I simply went and split the queries into 6: one for each month of our journey. I might have done this automatically (and I may yet do this in a future post, we’ll see) but the basic manual implementation works well.&#0160; Basically this means that instead of 2 overlays – one for the planned journey, one for the “actual” one – we now have 7 (i.e. 1 + 6).</p>
<p>Here’s the updated source:</p>
<div style="background: white; color: black; line-height: 140%; font-family: courier new; font-size: 8pt;">
<p style="margin: 0px;"><span style="color: blue;">&lt;</span><span style="color: maroon;">html</span><span style="color: blue;">&gt;</span></p>
<p style="margin: 0px;">&#0160; <span style="color: blue;">&lt;</span><span style="color: maroon;">head</span><span style="color: blue;">&gt;</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">&lt;</span><span style="color: maroon;">style</span><span style="color: blue;">&gt;</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: maroon;">html</span>,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: maroon;">body</span> {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: red;">padding</span>: <span style="color: blue;">0</span>;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: red;">margin</span>: <span style="color: blue;">0</span>;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: maroon;">#map</span> {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: red;">height</span>: <span style="color: blue;">100%</span>;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: red;">width</span>: <span style="color: blue;">100%</span>;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: red;">overflow</span>: <span style="color: blue;">hidden</span>;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: red;">float</span>: <span style="color: blue;">left</span>;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: red;">border</span>: <span style="color: blue;">thin</span> <span style="color: blue;">solid</span> <span style="color: blue;">#333</span>;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: maroon;">h3</span> {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: red;">margin</span>: <span style="color: blue;">0</span> <span style="color: blue;">0</span> <span style="color: blue;">5px</span> <span style="color: blue;">0</span>;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: red;">white-space</span>: <span style="color: blue;">nowrap</span>;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: red;">overflow</span>: <span style="color: blue;">hidden</span>;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: red;">text-overflow</span>: <span style="color: blue;">ellipsis</span>;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: maroon;">p</span> {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: red;">margin</span>: <span style="color: blue;">0</span> <span style="color: blue;">0</span> <span style="color: blue;">10px</span> <span style="color: blue;">0</span>;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: red;">white-space</span>: <span style="color: blue;">nowrap</span>;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: red;">overflow</span>: <span style="color: blue;">hidden</span>;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: red;">text-overflow</span>: <span style="color: blue;">ellipsis</span>;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">&lt;/</span><span style="color: maroon;">style</span><span style="color: blue;">&gt;</span></p>
<p style="margin: 0px;">&#0160; <span style="color: blue;">&lt;/</span><span style="color: maroon;">head</span><span style="color: blue;">&gt;</span></p>
<p style="margin: 0px;">&#0160; <span style="color: blue;">&lt;</span><span style="color: maroon;">body</span><span style="color: blue;">&gt;</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">&lt;</span><span style="color: maroon;">div</span> <span style="color: red;">id</span><span style="color: blue;">=&quot;map&quot;&gt;&lt;/</span><span style="color: maroon;">div</span><span style="color: blue;">&gt;</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">&lt;</span><span style="color: maroon;">script</span> <span style="color: red;">async</span> <span style="color: red;">defer</span> <span style="color: red;">src</span><span style="color: blue;">=&quot;https://maps.googleapis.com/maps/api/js?key=[ENTER_YOUR_KEY_HERE]&amp;libraries=places&amp;callback=initMap&quot;&gt;&lt;/</span><span style="color: maroon;">script</span><span style="color: blue;">&gt;</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">&lt;</span><span style="color: maroon;">script</span> <span style="color: red;">type</span><span style="color: blue;">=&#39;text/javascript&#39;&gt;</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">var</span> map;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">var</span> infowindow;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">var</span> service;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">var</span> overlayBase = <span style="color: #a31515;">&#39;https://inreach.garmin.com/feed/Share/mondeEnPoche?&#39;</span>;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">var</span> overlays = [</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: #a31515;">&#39;d1=2017-07-01T00:00Z&amp;d2=2017-08-01T00:00Z&#39;</span>,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: #a31515;">&#39;d1=2017-08-01T00:00Z&amp;d2=2017-09-01T00:00Z&#39;</span>,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: #a31515;">&#39;d1=2017-09-01T00:00Z&amp;d2=2017-10-01T00:00Z&#39;</span>,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: #a31515;">&#39;d1=2017-10-01T00:00Z&amp;d2=2017-11-01T00:00Z&#39;</span>,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: #a31515;">&#39;d1=2017-11-01T00:00Z&amp;d2=2017-12-01T00:00Z&#39;</span>,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: #a31515;">&#39;d1=2017-12-01T00:00Z&amp;d2=2018-01-05T00:00Z&#39;</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; ];</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">// Data for the markers consisting of a name, a LatLng and a zIndex for the</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">// order in which these markers should display on top of each other.</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">var</span> stops = [</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; [<span style="color: #a31515;">&#39;Marin-Epagnier&#39;</span>, <span style="color: #a31515;">&#39;ChIJt4RhN0UJjkcR9dxtPHTvIRY&#39;</span>, 47.0091808, 7.0015896, 1, <span style="color: #a31515;">&#39;Start &amp; End&#39;</span>],</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; [<span style="color: #a31515;">&#39;Washington&#39;</span>, <span style="color: #a31515;">&#39;ChIJW-T2Wt7Gt4kRKl2I1CJFUsI&#39;</span>, 38.9071923, -77.0368707, 2],</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; [<span style="color: #a31515;">&#39;New York&#39;</span>, <span style="color: #a31515;">&#39;ChIJOwg_06VPwokRYv534QaPC8g&#39;</span>, 40.7127837, -74.0059413, 3],</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; [<span style="color: #a31515;">&#39;West Hartford&#39;</span>, <span style="color: #a31515;">&#39;ChIJ_RQEifWs54kRfDtRDlPX-Wc&#39;</span>, 41.7620842, -72.7420151, 4],</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; [<span style="color: #a31515;">&#39;Boston&#39;</span>, <span style="color: #a31515;">&#39;ChIJGzE9DS1l44kRoOhiASS_fHg&#39;</span>, 42.3600825, -71.0588801, 5, <span style="color: #a31515;">&#39;July&#39;</span>],</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; [<span style="color: #a31515;">&#39;Toronto&#39;</span>, <span style="color: #a31515;">&#39;ChIJpTvG15DL1IkRd8S0KlBVNTI&#39;</span>, 43.653226, -79.3831843, 6],</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; [<span style="color: #a31515;">&#39;Bozeman&#39;</span>, <span style="color: #a31515;">&#39;ChIJE4i6T0xERVMRqmA792TQ9WM&#39;</span>, 45.6769979, -111.0429339, 7],</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; [<span style="color: #a31515;">&#39;Yellowstone National Park&#39;</span>, <span style="color: #a31515;">&#39;ChIJVVVVVVXlUVMRu-GPNDD5qKw&#39;</span>, 44.427963, -110.588455, 8],</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; [<span style="color: #a31515;">&#39;Grand Teton National Park&#39;</span>, <span style="color: #a31515;">&#39;ChIJqRtdyZ5RUlMRN6ORzI64oKU&#39;</span>, 43.7904282, -110.6817627, 9],</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; [<span style="color: #a31515;">&#39;Salt Lake City&#39;</span>, <span style="color: #a31515;">&#39;ChIJ7THRiJQ9UocRyjFNSKC3U1s&#39;</span>, 40.7607793, -111.8910474, 10],</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; [<span style="color: #a31515;">&#39;Bryce Canyon&#39;</span>, <span style="color: #a31515;">&#39;ChIJbUw47h9pNYcRYv1Jemw3nHU&#39;</span>, 37.6283161, -112.1676947, 11],</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; [<span style="color: #a31515;">&#39;Zion National Park&#39;</span>, <span style="color: #a31515;">&#39;ChIJ2fhEiNDqyoAR9VY2qhU6Lnw&#39;</span>, 37.2982022, -113.0263005, 12],</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; [<span style="color: #a31515;">&#39;Las Vegas&#39;</span>, <span style="color: #a31515;">&#39;ChIJ0X31pIK3voARo3mz1ebVzDo&#39;</span>, 36.1699412, -115.1398296, 13],</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; [<span style="color: #a31515;">&#39;Death Valley&#39;</span>, <span style="color: #a31515;">&#39;ChIJsf-PHqI5x4ARJd0j14NziRw&#39;</span>, 36.5322649, -116.9325408, 14],</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; [<span style="color: #a31515;">&#39;Sequoia National Park&#39;</span>, <span style="color: #a31515;">&#39;ChIJeWUZLX37v4ARZPQen_nfCkQ&#39;</span>, 36.4863668, -118.5657516, 15],</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; [<span style="color: #a31515;">&#39;Big Sur&#39;</span>, <span style="color: #a31515;">&#39;ChIJVVikTfuPjYARYuO38cfXpRY&#39;</span>, 36.2704212, -121.807976, 16],</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; [<span style="color: #a31515;">&#39;Monterey&#39;</span>, <span style="color: #a31515;">&#39;ChIJkfu1cFLkjYARXj1K2AlJSO4&#39;</span>, 36.6002378, -121.8946761, 17],</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; [<span style="color: #a31515;">&#39;San Francisco&#39;</span>, <span style="color: #a31515;">&#39;ChIJIQBpAG2ahYAR_6128GcTUEo&#39;</span>, 37.7749295, -122.4194155, 18, <span style="color: #a31515;">&#39;August&#39;</span>],</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; [<span style="color: #a31515;">&#39;Lima&#39;</span>, <span style="color: #a31515;">&#39;ChIJ3-EpLOzDBZERRBEzku1Ooak&#39;</span>, -12.0463667, -77.0427891, 19],</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; [<span style="color: #a31515;">&#39;Machu Picchu&#39;</span>, <span style="color: #a31515;">&#39;ChIJVVVViV-abZERJxqgpA43EDo&#39;</span>, -13.1631412, -72.5449629, 20],</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; [<span style="color: #a31515;">&#39;Cusco&#39;</span>, <span style="color: #a31515;">&#39;ChIJMYRZJtjVbZERXTEYI8yWqSo&#39;</span>, -13.53195, -71.9674626, 21],</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; [<span style="color: #a31515;">&#39;São Paulo&#39;</span>, <span style="color: #a31515;">&#39;ChIJ0WGkg4FEzpQRrlsz_whLqZs&#39;</span>, -23.5505199, -46.6333094, 22],</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; [<span style="color: #a31515;">&#39;Rio de Janeiro&#39;</span>, <span style="color: #a31515;">&#39;ChIJW6AIkVXemwARTtIvZ2xC3FA&#39;</span>, -22.9068467, -43.1728965, 23, <span style="color: #a31515;">&#39;September&#39;</span>],</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; [<span style="color: #a31515;">&#39;Iguazu Falls&#39;</span>, <span style="color: #a31515;">&#39;ChIJbRuqowzq9pQRfphenBd1e5E&#39;</span>, -25.695259, -54.4388549, 24],</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; [<span style="color: #a31515;">&#39;Córdoba&#39;</span>, <span style="color: #a31515;">&#39;ChIJaVuPR1-YMpQRkrBmU5pPorA&#39;</span>, -31.4200833, -64.1887761, 25],</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; [<span style="color: #a31515;">&#39;Parque Provincial Ischigualasto&#39;</span>, <span style="color: #a31515;">&#39;ChIJwynmBT3sgpYR0J11F_1O5cw&#39;</span>, -30.167266,-67.9860327, 26],</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; [<span style="color: #a31515;">&#39;Parque Nacional Talampaya&#39;</span>, <span style="color: #a31515;">&#39;ChIJUUxbf6rPgpYRaEkBxpGDANQ&#39;</span>, -29.8906226, -67.853468, 27],</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; [<span style="color: #a31515;">&#39;Catamarca&#39;</span>, <span style="color: #a31515;">&#39;ChIJzZ8PHb8oJJQRGoYJFkvdHn4&#39;</span>, -28.469581, -65.7795441, 28],</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; [<span style="color: #a31515;">&#39;San Miguel de Tucumán&#39;</span>, <span style="color: #a31515;">&#39;ChIJA2nF1pI3IpQRJ2XFtZJbjfg&#39;</span>, -26.8082848, -65.2175903, 29],</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; [<span style="color: #a31515;">&#39;Salta&#39;</span>, <span style="color: #a31515;">&#39;ChIJ-bdRUaPDG5QRBvKH1SyZzaU&#39;</span>, -24.7821269, -65.4231976, 30],</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; [<span style="color: #a31515;">&#39;Salar de Uyuni&#39;</span>, <span style="color: #a31515;">&#39;ChIJh9rdHuC6_5MRkFuFng0T5RI&#39;</span>, -20.1595348, -67.4054025, 31],</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; [<span style="color: #a31515;">&#39;San Pedro de Atacama&#39;</span>, <span style="color: #a31515;">&#39;ChIJP78qqXpMqJYR0Zf5rExh9Ho&#39;</span>, -22.9087073, -68.1997156, 32],</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; [<span style="color: #a31515;">&#39;Pan de Azúcar National Park&#39;</span>, <span style="color: #a31515;">&#39;ChIJM6BM4cewvJYRbC7GcVat_6U&#39;</span>, -26.177565, -70.5495396, 33],</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; [<span style="color: #a31515;">&#39;Raúl Marine Balmaceda&#39;</span>, <span style="color: #a31515;">&#39;ChIJ4V-JqObIkZYRiGptmZGVUn8&#39;</span>, -29.9695076, -71.3416309, 34],</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; [<span style="color: #a31515;">&#39;Santiago&#39;</span>, <span style="color: #a31515;">&#39;ChIJuzrymgbQYpYRl0jtCfRZnYc&#39;</span>, -33.4378305, -70.6504492, 35],</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; [<span style="color: #a31515;">&#39;Easter Island&#39;</span>, <span style="color: #a31515;">&#39;ChIJK67UqBfwR5kRti0qwO2z5bs&#39;</span>, -27.112723, -109.3496865, 36],</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; [<span style="color: #a31515;">&#39;Tahiti&#39;</span>, <span style="color: #a31515;">&#39;ChIJTddtfNB1GHQREVfDCXp6wJs&#39;</span>, -17.6509195, -149.4260421, 37],</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; [<span style="color: #a31515;">&#39;Auckland&#39;</span>, <span style="color: #a31515;">&#39;ChIJ--acWvtHDW0RF5miQ2HvAAU&#39;</span>, -36.8484597, 174.7633315, 38],</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; [<span style="color: #a31515;">&#39;Rotorua&#39;</span>, <span style="color: #a31515;">&#39;ChIJK7L2gj2Ybm0RMZmjQ2HvAAU&#39;</span>, -38.1368478, 176.2497461, 39, <span style="color: #a31515;">&#39;October&#39;</span>],</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; [<span style="color: #a31515;">&#39;Wellington&#39;</span>, <span style="color: #a31515;">&#39;ChIJy3TpSfyxOG0RcLQTomPvAAo&#39;</span>, -41.2864603, 174.776236, 40],</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; [<span style="color: #a31515;">&#39;Paparoa National Park&#39;</span>, <span style="color: #a31515;">&#39;ChIJbZoxICBxJW0RIPF5hIbvAAU&#39;</span>, -42.1632433, 171.366731, 41],</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; [<span style="color: #a31515;">&#39;Queenstown&#39;</span>, <span style="color: #a31515;">&#39;ChIJX96o1_Ed1akRAKZ5hIbvAAU&#39;</span>, -45.0311622, 168.6626435, 42],</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; [<span style="color: #a31515;">&#39;Sydney&#39;</span>, <span style="color: #a31515;">&#39;ChIJP5iLHkCuEmsRwMwyFmh9AQU&#39;</span>, -33.8688197, 151.2092955, 43],</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; [<span style="color: #a31515;">&#39;Brisbane&#39;</span>, <span style="color: #a31515;">&#39;ChIJM9KTrJpXkWsRQK_e81qjAgQ&#39;</span>, -27.4697707, 153.0251235, 44],</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; [<span style="color: #a31515;">&#39;Cairns&#39;</span>, <span style="color: #a31515;">&#39;ChIJEySiW1VieGkRYHggf_HuAAQ&#39;</span>, -16.9185514, 145.7780548, 45],</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; [<span style="color: #a31515;">&#39;Kuala Lumpur&#39;</span>, <span style="color: #a31515;">&#39;ChIJ5-rvAcdJzDERfSgcL1uO2fQ&#39;</span>, 3.139003, 101.686855, 46],</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; [<span style="color: #a31515;">&#39;Singapore&#39;</span>, <span style="color: #a31515;">&#39;ChIJdZOLiiMR2jERxPWrUs9peIg&#39;</span>, 1.352083, 103.819836, 47],</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; [<span style="color: #a31515;">&#39;Coimbatore&#39;</span>, <span style="color: #a31515;">&#39;ChIJtRyXL69ZqDsRgtI-GB7IwS8&#39;</span>, 11.0168445, 76.9558321, 48],</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; [<span style="color: #a31515;">&#39;Kodaikanal&#39;</span>, <span style="color: #a31515;">&#39;ChIJhwMKf2NmBzsRPMFYNzfp-p8&#39;</span>, 10.2381136, 77.4891822, 49],</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; [<span style="color: #a31515;">&#39;Bangalore&#39;</span>, <span style="color: #a31515;">&#39;ChIJbU60yXAWrjsR4E9-UejD3_g&#39;</span>, 12.9715987, 77.5945627, 50, <span style="color: #a31515;">&#39;November&#39;</span>],</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; [<span style="color: #a31515;">&#39;Durban&#39;</span>, <span style="color: #a31515;">&#39;ChIJt2G8AQCq9x4RgW6qxEZVp8w&#39;</span>, -29.8586804, 31.0218404, 51],</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; [<span style="color: #a31515;">&#39;Lesotho&#39;</span>, <span style="color: #a31515;">&#39;ChIJ64xf1idIjB4Rsx7ReLhXLSM&#39;</span>, -29.609988, 28.233608, 52, <span style="color: #a31515;">&#39;December&#39;</span>],</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; [<span style="color: #a31515;">&#39;Addo Elephant National Park&#39;</span>, <span style="color: #a31515;">&#39;ChIJY2nuzYRPex4RCsT--8cm454&#39;</span>, -33.4833333, 25.75, 53],</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; [<span style="color: #a31515;">&#39;Tsitsikamma&#39;</span>, <span style="color: #a31515;">&#39;ChIJaTwmTQ5ueR4R5_kNGLX6RBs&#39;</span>, -32.2178721, 26.5772048, 54],</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; [<span style="color: #a31515;">&#39;Knysna&#39;</span>, <span style="color: #a31515;">&#39;ChIJ2QwBlkDqeB4Rzc5QdeG5Kr4&#39;</span>, -34.0350856, 23.0464693, 55],</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; [<span style="color: #a31515;">&#39;Oudtshoorn&#39;</span>, <span style="color: #a31515;">&#39;ChIJtRO16obB1R0RYesIjnRHQ40&#39;</span>, -33.6007225, 22.2026347, 56],</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; [<span style="color: #a31515;">&#39;Franschhoek&#39;</span>, <span style="color: #a31515;">&#39;ChIJz7IFaAe9zR0R-bJW01SGtDw&#39;</span>, -33.8974833, 19.1523292, 57],</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; [<span style="color: #a31515;">&#39;Stellenbosch&#39;</span>, <span style="color: #a31515;">&#39;ChIJpeKIUfeyzR0R4mvj3gCqCXA&#39;</span>, -33.9321045, 18.860152, 58],</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; [<span style="color: #a31515;">&#39;Cape Town&#39;</span>, <span style="color: #a31515;">&#39;ChIJ1-4miA9QzB0Rh6ooKPzhf2g&#39;</span>, -33.9248685, 18.4240553, 59]</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; ];</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">var</span> labels = [</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; [<span style="color: #a31515;">&#39;Start &amp; End&#39;</span>, 47.0091808, 7.0015896, 1],</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; [<span style="color: #a31515;">&#39;July&#39;</span>, 42.409143, -102.280372, 2],</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; [<span style="color: #a31515;">&#39;August&#39;</span>, 5.247246, -73.979869, 3],</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; [<span style="color: #a31515;">&#39;September&#39;</span>, -36.753594, -65.018287, 4],</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; [<span style="color: #a31515;">&#39;October&#39;</span>, -33.622306, 160.985311, 5],</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; [<span style="color: #a31515;">&#39;November&#39;</span>, 16.921484, 91.724302, 6],</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; [<span style="color: #a31515;">&#39;December&#39;</span>, -16.011953, 23.167125, 7]</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; ];</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">function</span> initMap() {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; map = <span style="color: blue;">new</span> google.maps.Map(document.getElementById(<span style="color: #a31515;">&#39;map&#39;</span>), {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; center: <span style="color: blue;">new</span> google.maps.LatLng(15, -30),</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; zoom: 2,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; mapTypeId: <span style="color: #a31515;">&#39;satellite&#39;</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; });</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; infowindow = <span style="color: blue;">new</span> google.maps.InfoWindow();</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; service = <span style="color: blue;">new</span> google.maps.places.PlacesService(map);</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; loadJS(<span style="color: #a31515;">&#39;./maplabel-compiled.js&#39;</span>, onInit, document.body);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">function</span> loadJS(url, implementationCode, location){</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">var</span> scriptTag = document.createElement(<span style="color: #a31515;">&#39;script&#39;</span>);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; scriptTag.src = url;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; scriptTag.onload = implementationCode;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; scriptTag.onreadystatechange = implementationCode;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; location.appendChild(scriptTag);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; };</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">function</span> onInit(){</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; setMarkers(map);</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">for</span> (<span style="color: blue;">var</span> i = 0; i &lt;= overlays.length; i++) {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">var</span> kmlOverlayer = <span style="color: blue;">new</span> google.maps.KmlLayer(overlayBase + overlays[i], {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; suppressInfoWindows: <span style="color: blue;">true</span>,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; preserveViewport: <span style="color: blue;">true</span>,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; map: map</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; });</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">function</span> setMarkers(map) {</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">// Adds markers to the map with a delay</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">var</span> delay = 100;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">for</span> (<span style="color: blue;">var</span> i = 0; i &lt;= stops.length; i++) {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">var</span> timeout = i * delay;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">// If this is the last segment, just add the line</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">if</span> (i === stops.length) {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; addConnectingLineWithTimeout(stops[i - 1], stops[0], timeout + delay);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; } <span style="color: blue;">else</span> <span style="color: blue;">if</span> (i &gt;= 0) {</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">// Otherwise add a marker after a delay, followed by the</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">// connecting line to the previous marker, if there is one</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; addMarkerWithTimeout(stops[i], timeout);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">if</span> (i &gt; 0) {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; addConnectingLineWithTimeout(stops[i], stops[i - 1], timeout + delay);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">function</span> addMarkerWithTimeout(stop, timeout) {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; setTimeout(<span style="color: blue;">function</span>() {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">var</span> marker = <span style="color: blue;">new</span> google.maps.Marker({</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; map: map,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; title: stop[0],</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; placeId: stop[1],</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; position: {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; lat: stop[2],</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; lng: stop[3]</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; },</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; label: stop[4].toString(),</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; zIndex: stop[4]</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">//animation: google.maps.Animation.DROP, // Cool but too much</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; });</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">// If we have a label listed, find out which and add it to the map</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">if</span> (stop.length &gt; 5) {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">var</span> idx = labels.findIndex(<span style="color: blue;">function</span>(val) {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">return</span> val[0] === stop[5];</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; });</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">if</span> (idx &gt;= 0) {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">var</span> label = labels[idx];</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; addLabelWithTimeout(label[1], label[2], label[0], 0);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">// Register the callback for when the marker is clicked</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; google.maps.event.addListener(marker, <span style="color: #a31515;">&#39;click&#39;</span>, <span style="color: blue;">function</span>() {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; onItemClick(event, marker);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; });</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; }, timeout);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">function</span> addLabelWithTimeout(lat, long, text, timeout) {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; setTimeout(<span style="color: blue;">function</span>() {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">var</span> pos = <span style="color: blue;">new</span> google.maps.LatLng(lat, long);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">var</span> mapLabel = <span style="color: blue;">new</span> MapLabel({</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; text: text,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; position: pos,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; map: map,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; fontSize: 14</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; });</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; mapLabel.set(<span style="color: #a31515;">&#39;position&#39;</span>, <span style="color: blue;">new</span> google.maps.LatLng(lat, long));</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; }, timeout);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">function</span> addConnectingLineWithTimeout(stop1, stop2, timeout) {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; setTimeout(<span style="color: blue;">function</span>() {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">var</span> flightPath = <span style="color: blue;">new</span> google.maps.Polyline({</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; path: [{</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; lat: stop1[2],</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; lng: stop1[3]</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; }, {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; lat: stop2[2],</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; lng: stop2[3]</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; }],</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; geodesic: <span style="color: blue;">true</span>,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; strokeColor: <span style="color: #a31515;">&#39;#D34038&#39;</span>,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; strokeOpacity: 1.0,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; strokeWeight: 4</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; });</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; flightPath.setMap(map);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; }, timeout);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">// Info window trigger function</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">function</span> onItemClick(event, pin) {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; service.getDetails({</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; placeId: pin.placeId</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; }, <span style="color: blue;">function</span>(place, status) {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">var</span> cont =</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: #a31515;">&#39;&lt;div&gt;&lt;h3&gt;&#39;</span> + place.name + <span style="color: #a31515;">&#39;&lt;/h3&gt;&lt;p&gt;&#39;</span> + place.formatted_address + <span style="color: #a31515;">&#39;&lt;/p&gt;&#39;</span> +</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; (place.photos &amp;&amp; place.photos.length &gt; 0 ?</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; (<span style="color: #a31515;">&#39;&lt;img src=&quot;&#39;</span> +</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; place.photos[0].getUrl({</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: #a31515;">&#39;maxWidth&#39;</span>: 300,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: #a31515;">&#39;maxHeight&#39;</span>: 200</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; }) + <span style="color: #a31515;">&#39;&quot; /&gt;&#39;</span>) : <span style="color: #a31515;">&#39;&#39;</span>) +</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: #a31515;">&#39;&lt;/div&gt;&#39;</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; infowindow.setContent(cont);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; infowindow.open(map, pin);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; });</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">&lt;/</span><span style="color: maroon;">script</span><span style="color: blue;">&gt;</span></p>
<p style="margin: 0px;">&#0160; <span style="color: blue;">&lt;/</span><span style="color: maroon;">body</span><span style="color: blue;">&gt;</span></p>
<p style="margin: 0px;"><span style="color: blue;">&lt;/</span><span style="color: maroon;">html</span><span style="color: blue;">&gt;</span></p>
<p style="margin: 0px;"><span style="color: blue;">&#0160;</span></p>
</div>
<p>Here’s the area of the map we’re in at the moment – we arrived yesterday in Paraty, a lovely town along the coast between Sao Paolo and Rio de Janeiro. I’ll talk more about this in the next post.</p>
<p><a href="http://through-the-interface.typepad.com/.a/6a00d83452464869e201b8d2a22ca1970c-pi" rel="noopener noreferrer" target="_blank"><img alt="Our trip along the Brazilian green coast" border="0" height="282" src="/assets/image_479960.jpg" style="margin: 30px auto; float: none; display: block; background-image: none;" title="Our trip along the Brazilian green coast" width="500" /></a></p>
<p>Here’s the embedded map (<a href="http://walmsley.ch" rel="noopener noreferrer" target="_blank">which you can also find on our trip’s website</a>).</p>
<p style="text-align: center"><iframe height="300" src="https://through-the-interface.typepad.com/files/maps/map5.html" width="500"></iframe></p>
<p>Here’s an interesting side-note – well, a cautionary tale, really – for people considering doing something similar for their own trips. If you want to upload tracking information wherever you are in the world, <strong>make sure you have the right plan with Garmin</strong>. I did not. I signed up for the basic, “safety” plan at $20 a month (at least for the “freedom” version, which allows you to modify your choice month-by-month rarher than being locked in for a year at a time). It turns out that this basic plan does <strong>not</strong> include unlimited tracking uploads: these get charged at $0.10 each. I only realised this when I received my bill for the month of July, which amounted to $160! Yeesh.</p>
<p>It turns out you need the $35 a month “recreation” plan to avoid these charges. I promptly upgraded and contacted Garmin to see what could be done about the bill. After a few emails they agreed to credit some of the $160 cost (not all of it, but then ultimately it was my own fault for not reading the fine – or even standard-sized – print carefully enough). So if you’re planning on getting a Garmin inReach device to track your location live, in the way we have, please make sure you get the right plan!</p>
