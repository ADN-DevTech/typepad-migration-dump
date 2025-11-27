---
layout: "post"
title: "Displaying a round-the-world itinerary using Google Maps &ndash; Part 2"
date: "2017-06-30 10:50:47"
author: "Kean Walmsley"
categories:
  - "HTML"
  - "JavaScript"
  - "Round the world"
original_url: "https://www.keanw.com/2017/06/displaying-a-round-the-world-itinerary-using-google-maps-part-2.html "
typepad_basename: "displaying-a-round-the-world-itinerary-using-google-maps-part-2"
typepad_status: "Publish"
---

<p>In <a href="http://through-the-interface.typepad.com/through_the_interface/2017/06/displaying-a-round-the-world-itinerary-using-google-maps-part-1.html" target="_blank">the last post</a> we looked at a basic implementation of a round-the-world itinerary in Google Maps. In today’s post we’re going to add the ability to see the first photo associated with a location when you click on it.</p><p>The Google Maps API makes it fairly easy to get the photos associated with a particular place, but it helps to identify that place uniquely: while you can search for a place based on its name, it’s more efficient and reliable to get its unique “place ID” and use that instead. I used <a href="https://developers.google.com/maps/documentation/javascript/examples/places-placeid-finder" target="_blank">this handy PlaceD Finder tool</a> to get the IDs associated with our various destinations and went and added them to our array.</p><p>Then it was a fairly simple matter of using the PlacesService to get the place description which includes an array of any photos associated with the place. We can then have an img element in our “info window” point at the first photo in the list.</p><p>Here’s the updated code:</p><p><div style="background: white; color: black; line-height: 140%; font-family: courier new; font-size: 8pt;">
<p style="margin: 0px;"><span style="color: blue;">&lt;</span><span style="color: maroon;">html</span><span style="color: blue;">&gt;</span></p>
<p style="margin: 0px;">&nbsp; <span style="color: blue;">&lt;</span><span style="color: maroon;">head</span><span style="color: blue;">&gt;</span></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; <span style="color: blue;">&lt;</span><span style="color: maroon;">style</span><span style="color: blue;">&gt;</span></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: maroon;">html</span>,</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: maroon;">body</span> {</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: red;">padding</span>: <span style="color: blue;">0</span>;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: red;">margin</span>: <span style="color: blue;">0</span>;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; }</p>
<p style="margin: 0px;"><br></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: maroon;">#map</span> {</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: red;">height</span>: <span style="color: blue;">100%</span>;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: red;">width</span>: <span style="color: blue;">100%</span>;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: red;">overflow</span>: <span style="color: blue;">hidden</span>;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: red;">float</span>: <span style="color: blue;">left</span>;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: red;">border</span>: <span style="color: blue;">thin</span> <span style="color: blue;">solid</span> <span style="color: blue;">#333</span>;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; }</p>
<p style="margin: 0px;"><br></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: maroon;">h3</span> {</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: red;">margin</span>: <span style="color: blue;">0</span> <span style="color: blue;">0</span> <span style="color: blue;">5px</span> <span style="color: blue;">0</span>;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: red;">white-space</span>: <span style="color: blue;">nowrap</span>;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: red;">overflow</span>: <span style="color: blue;">hidden</span>;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: red;">text-overflow</span>: <span style="color: blue;">ellipsis</span>;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; }</p>
<p style="margin: 0px;"><br></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: maroon;">p</span> {</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: red;">margin</span>: <span style="color: blue;">0</span> <span style="color: blue;">0</span> <span style="color: blue;">10px</span> <span style="color: blue;">0</span>;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: red;">white-space</span>: <span style="color: blue;">nowrap</span>;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: red;">overflow</span>: <span style="color: blue;">hidden</span>;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: red;">text-overflow</span>: <span style="color: blue;">ellipsis</span>;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; }</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; <span style="color: blue;">&lt;/</span><span style="color: maroon;">style</span><span style="color: blue;">&gt;</span></p>
<p style="margin: 0px;">&nbsp; <span style="color: blue;">&lt;/</span><span style="color: maroon;">head</span><span style="color: blue;">&gt;</span></p>
<p style="margin: 0px;">&nbsp; <span style="color: blue;">&lt;</span><span style="color: maroon;">body</span><span style="color: blue;">&gt;</span></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; <span style="color: blue;">&lt;</span><span style="color: maroon;">div</span> <span style="color: red;">id</span><span style="color: blue;">="map"&gt;&lt;/</span><span style="color: maroon;">div</span><span style="color: blue;">&gt;</span></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; <span style="color: blue;">&lt;</span><span style="color: maroon;">script</span> <span style="color: red;">async</span> <span style="color: red;">defer</span> <span style="color: red;">src</span><span style="color: blue;">="https://maps.googleapis.com/maps/api/js?key=[ENTER_YOUR_KEY_HERE]&amp;libraries=places&amp;callback=initMap"&gt;&lt;/</span><span style="color: maroon;">script</span><span style="color: blue;">&gt;</span></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; <span style="color: blue;">&lt;</span><span style="color: maroon;">script</span> <span style="color: red;">type</span><span style="color: blue;">='text/javascript'&gt;</span></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">var</span> map;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">var</span> infowindow;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">var</span> service;</p>
<p style="margin: 0px;"><br></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: green;">// Data for the markers consisting of a name, a LatLng and a zIndex for the</span></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: green;">// order in which these markers should display on top of each other.</span></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">var</span> stops = [</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; [<span style="color: rgb(163, 21, 21);">'Marin-Epagnier'</span>, <span style="color: rgb(163, 21, 21);">'ChIJt4RhN0UJjkcR9dxtPHTvIRY'</span>, 47.0091808, 7.0015896, 1],</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; [<span style="color: rgb(163, 21, 21);">'Washington'</span>, <span style="color: rgb(163, 21, 21);">'ChIJW-T2Wt7Gt4kRKl2I1CJFUsI'</span>, 38.9071923, -77.0368707, 2],</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; [<span style="color: rgb(163, 21, 21);">'New York'</span>, <span style="color: rgb(163, 21, 21);">'ChIJOwg_06VPwokRYv534QaPC8g'</span>, 40.7127837, -74.0059413, 3],</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; [<span style="color: rgb(163, 21, 21);">'West Hartford'</span>, <span style="color: rgb(163, 21, 21);">'ChIJ_RQEifWs54kRfDtRDlPX-Wc'</span>, 41.7620842, -72.7420151, 4],</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; [<span style="color: rgb(163, 21, 21);">'Boston'</span>, <span style="color: rgb(163, 21, 21);">'ChIJGzE9DS1l44kRoOhiASS_fHg'</span>, 42.3600825, -71.0588801, 5],</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; [<span style="color: rgb(163, 21, 21);">'Toronto'</span>, <span style="color: rgb(163, 21, 21);">'ChIJpTvG15DL1IkRd8S0KlBVNTI'</span>, 43.653226, -79.3831843, 6],</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; [<span style="color: rgb(163, 21, 21);">'Bozeman'</span>, <span style="color: rgb(163, 21, 21);">'ChIJE4i6T0xERVMRqmA792TQ9WM'</span>, 45.6769979, -111.0429339, 7],</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; [<span style="color: rgb(163, 21, 21);">'Yellowstone National Park'</span>, <span style="color: rgb(163, 21, 21);">'ChIJVVVVVVXlUVMRu-GPNDD5qKw'</span>, 44.427963, -110.588455, 8],</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; [<span style="color: rgb(163, 21, 21);">'Grand Teton National Park'</span>, <span style="color: rgb(163, 21, 21);">'ChIJqRtdyZ5RUlMRN6ORzI64oKU'</span>, 43.7904282, -110.6817627, 9],</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; [<span style="color: rgb(163, 21, 21);">'Salt Lake City'</span>, <span style="color: rgb(163, 21, 21);">'ChIJ7THRiJQ9UocRyjFNSKC3U1s'</span>, 40.7607793, -111.8910474, 10],</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; [<span style="color: rgb(163, 21, 21);">'Bryce Canyon'</span>, <span style="color: rgb(163, 21, 21);">'ChIJbUw47h9pNYcRYv1Jemw3nHU'</span>, 37.6283161, -112.1676947, 11],</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; [<span style="color: rgb(163, 21, 21);">'Zion National Park'</span>, <span style="color: rgb(163, 21, 21);">'ChIJ2fhEiNDqyoAR9VY2qhU6Lnw'</span>, 37.2982022, -113.0263005, 12],</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; [<span style="color: rgb(163, 21, 21);">'Las Vegas'</span>, <span style="color: rgb(163, 21, 21);">'ChIJ0X31pIK3voARo3mz1ebVzDo'</span>, 36.1699412, -115.1398296, 13],</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; [<span style="color: rgb(163, 21, 21);">'Death Valley'</span>, <span style="color: rgb(163, 21, 21);">'ChIJsf-PHqI5x4ARJd0j14NziRw'</span>, 36.5322649, -116.9325408, 14],</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; [<span style="color: rgb(163, 21, 21);">'Sequoia National Park'</span>, <span style="color: rgb(163, 21, 21);">'ChIJeWUZLX37v4ARZPQen_nfCkQ'</span>, 36.4863668, -118.5657516, 15],</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; [<span style="color: rgb(163, 21, 21);">'Big Sur'</span>, <span style="color: rgb(163, 21, 21);">'ChIJVVikTfuPjYARYuO38cfXpRY'</span>, 36.2704212, -121.807976, 16],</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; [<span style="color: rgb(163, 21, 21);">'Monterey'</span>, <span style="color: rgb(163, 21, 21);">'ChIJkfu1cFLkjYARXj1K2AlJSO4'</span>, 36.6002378, -121.8946761, 17],</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; [<span style="color: rgb(163, 21, 21);">'San Francisco'</span>, <span style="color: rgb(163, 21, 21);">'ChIJIQBpAG2ahYAR_6128GcTUEo'</span>, 37.7749295, -122.4194155, 18],</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; [<span style="color: rgb(163, 21, 21);">'Lima'</span>, <span style="color: rgb(163, 21, 21);">'ChIJ3-EpLOzDBZERRBEzku1Ooak'</span>, -12.0463667, -77.0427891, 19],</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; [<span style="color: rgb(163, 21, 21);">'Machu Picchu'</span>, <span style="color: rgb(163, 21, 21);">'ChIJVVVViV-abZERJxqgpA43EDo'</span>, -13.1631412, -72.5449629, 20],</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; [<span style="color: rgb(163, 21, 21);">'Cusco'</span>, <span style="color: rgb(163, 21, 21);">'ChIJMYRZJtjVbZERXTEYI8yWqSo'</span>, -13.53195, -71.9674626, 21],</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; [<span style="color: rgb(163, 21, 21);">'São Paulo'</span>, <span style="color: rgb(163, 21, 21);">'ChIJ0WGkg4FEzpQRrlsz_whLqZs'</span>, -23.5505199, -46.6333094, 22],</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; [<span style="color: rgb(163, 21, 21);">'Rio de Janeiro'</span>, <span style="color: rgb(163, 21, 21);">'ChIJW6AIkVXemwARTtIvZ2xC3FA'</span>, -22.9068467, -43.1728965, 23],</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; [<span style="color: rgb(163, 21, 21);">'Iguazu Falls'</span>, <span style="color: rgb(163, 21, 21);">'ChIJbRuqowzq9pQRfphenBd1e5E'</span>, -25.695259, -54.4388549, 24],</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; [<span style="color: rgb(163, 21, 21);">'Córdoba'</span>, <span style="color: rgb(163, 21, 21);">'ChIJaVuPR1-YMpQRkrBmU5pPorA'</span>, -31.4200833, -64.1887761, 25],</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; [<span style="color: rgb(163, 21, 21);">'Parque Nacional Talampaya'</span>, <span style="color: rgb(163, 21, 21);">'ChIJUUxbf6rPgpYRaEkBxpGDANQ'</span>, -29.8906226, -67.853468, 26],</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; [<span style="color: rgb(163, 21, 21);">'Valle de la Luna'</span>, <span style="color: rgb(163, 21, 21);">'ChIJ33tfOKhoLZQRTn9HuPJlE5g'</span>, -22.9257639, -68.2879926, 27],</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; [<span style="color: rgb(163, 21, 21);">'Catamarca'</span>, <span style="color: rgb(163, 21, 21);">'ChIJzZ8PHb8oJJQRGoYJFkvdHn4'</span>, -28.469581, -65.7795441, 28],</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; [<span style="color: rgb(163, 21, 21);">'San Miguel de Tucumán'</span>, <span style="color: rgb(163, 21, 21);">'ChIJA2nF1pI3IpQRJ2XFtZJbjfg'</span>, -26.8082848, -65.2175903, 29],</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; [<span style="color: rgb(163, 21, 21);">'Salta'</span>, <span style="color: rgb(163, 21, 21);">'ChIJ-bdRUaPDG5QRBvKH1SyZzaU'</span>, -24.7821269, -65.4231976, 30],</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; [<span style="color: rgb(163, 21, 21);">'Salar de Uyuni'</span>, <span style="color: rgb(163, 21, 21);">'ChIJh9rdHuC6_5MRkFuFng0T5RI'</span>, -20.1595348, -67.4054025, 31],</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; [<span style="color: rgb(163, 21, 21);">'San Pedro de Atacama'</span>, <span style="color: rgb(163, 21, 21);">'ChIJP78qqXpMqJYR0Zf5rExh9Ho'</span>, -22.9087073, -68.1997156, 32],</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; [<span style="color: rgb(163, 21, 21);">'Pan de Azúcar National Park'</span>, <span style="color: rgb(163, 21, 21);">'ChIJM6BM4cewvJYRbC7GcVat_6U'</span>, -26.177565, -70.5495396, 33],</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; [<span style="color: rgb(163, 21, 21);">'Raúl Marine Balmaceda'</span>, <span style="color: rgb(163, 21, 21);">'ChIJ4V-JqObIkZYRiGptmZGVUn8'</span>, -29.9695076, -71.3416309, 34],</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; [<span style="color: rgb(163, 21, 21);">'Santiago'</span>, <span style="color: rgb(163, 21, 21);">'ChIJuzrymgbQYpYRl0jtCfRZnYc'</span>, -33.4378305, -70.6504492, 35],</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; [<span style="color: rgb(163, 21, 21);">'Easter Island'</span>, <span style="color: rgb(163, 21, 21);">'ChIJK67UqBfwR5kRti0qwO2z5bs'</span>, -27.112723, -109.3496865, 36],</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; [<span style="color: rgb(163, 21, 21);">'Tahiti'</span>, <span style="color: rgb(163, 21, 21);">'ChIJTddtfNB1GHQREVfDCXp6wJs'</span>, -17.6509195, -149.4260421, 37],</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; [<span style="color: rgb(163, 21, 21);">'Auckland'</span>, <span style="color: rgb(163, 21, 21);">'ChIJ--acWvtHDW0RF5miQ2HvAAU'</span>, -36.8484597, 174.7633315, 38],</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; [<span style="color: rgb(163, 21, 21);">'Rotorua'</span>, <span style="color: rgb(163, 21, 21);">'ChIJK7L2gj2Ybm0RMZmjQ2HvAAU'</span>, -38.1368478, 176.2497461, 39],</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; [<span style="color: rgb(163, 21, 21);">'Wellington'</span>, <span style="color: rgb(163, 21, 21);">'ChIJy3TpSfyxOG0RcLQTomPvAAo'</span>, -41.2864603, 174.776236, 40],</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; [<span style="color: rgb(163, 21, 21);">'Paparoa National Park'</span>, <span style="color: rgb(163, 21, 21);">'ChIJbZoxICBxJW0RIPF5hIbvAAU'</span>, -42.1632433, 171.366731, 41],</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; [<span style="color: rgb(163, 21, 21);">'Queenstown'</span>, <span style="color: rgb(163, 21, 21);">'ChIJX96o1_Ed1akRAKZ5hIbvAAU'</span>, -45.0311622, 168.6626435, 42],</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; [<span style="color: rgb(163, 21, 21);">'Sydney'</span>, <span style="color: rgb(163, 21, 21);">'ChIJP5iLHkCuEmsRwMwyFmh9AQU'</span>, -33.8688197, 151.2092955, 43],</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; [<span style="color: rgb(163, 21, 21);">'Brisbane'</span>, <span style="color: rgb(163, 21, 21);">'ChIJM9KTrJpXkWsRQK_e81qjAgQ'</span>, -27.4697707, 153.0251235, 44],</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; [<span style="color: rgb(163, 21, 21);">'Cairns'</span>, <span style="color: rgb(163, 21, 21);">'ChIJEySiW1VieGkRYHggf_HuAAQ'</span>, -16.9185514, 145.7780548, 45],</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; [<span style="color: rgb(163, 21, 21);">'Kuala Lumpur'</span>, <span style="color: rgb(163, 21, 21);">'ChIJ5-rvAcdJzDERfSgcL1uO2fQ'</span>, 3.139003, 101.686855, 46],</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; [<span style="color: rgb(163, 21, 21);">'Singapore'</span>, <span style="color: rgb(163, 21, 21);">'ChIJdZOLiiMR2jERxPWrUs9peIg'</span>, 1.352083, 103.819836, 47],</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; [<span style="color: rgb(163, 21, 21);">'Coimbatore'</span>, <span style="color: rgb(163, 21, 21);">'ChIJtRyXL69ZqDsRgtI-GB7IwS8'</span>, 11.0168445, 76.9558321, 48],</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; [<span style="color: rgb(163, 21, 21);">'Kodaikanal'</span>, <span style="color: rgb(163, 21, 21);">'ChIJhwMKf2NmBzsRPMFYNzfp-p8'</span>, 10.2381136, 77.4891822, 49],</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; [<span style="color: rgb(163, 21, 21);">'Bangalore'</span>, <span style="color: rgb(163, 21, 21);">'ChIJbU60yXAWrjsR4E9-UejD3_g'</span>, 12.9715987, 77.5945627, 50],</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; [<span style="color: rgb(163, 21, 21);">'Durban'</span>, <span style="color: rgb(163, 21, 21);">'ChIJt2G8AQCq9x4RgW6qxEZVp8w'</span>, -29.8586804, 31.0218404, 51],</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; [<span style="color: rgb(163, 21, 21);">'Lesotho'</span>, <span style="color: rgb(163, 21, 21);">'ChIJ64xf1idIjB4Rsx7ReLhXLSM'</span>, -29.609988, 28.233608, 52],</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; [<span style="color: rgb(163, 21, 21);">'Addo Elephant National Park'</span>, <span style="color: rgb(163, 21, 21);">'ChIJY2nuzYRPex4RCsT--8cm454'</span>, -33.4833333, 25.75, 53],</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; [<span style="color: rgb(163, 21, 21);">'Tsitsikamma'</span>, <span style="color: rgb(163, 21, 21);">'ChIJaTwmTQ5ueR4R5_kNGLX6RBs'</span>, -32.2178721, 26.5772048, 54],</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; [<span style="color: rgb(163, 21, 21);">'Knysna'</span>, <span style="color: rgb(163, 21, 21);">'ChIJ2QwBlkDqeB4Rzc5QdeG5Kr4'</span>, -34.0350856, 23.0464693, 55],</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; [<span style="color: rgb(163, 21, 21);">'Oudtshoorn'</span>, <span style="color: rgb(163, 21, 21);">'ChIJtRO16obB1R0RYesIjnRHQ40'</span>, -33.6007225, 22.2026347, 56],</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; [<span style="color: rgb(163, 21, 21);">'Franschhoek'</span>, <span style="color: rgb(163, 21, 21);">'ChIJz7IFaAe9zR0R-bJW01SGtDw'</span>, -33.8974833, 19.1523292, 57],</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; [<span style="color: rgb(163, 21, 21);">'Stellenbosch'</span>, <span style="color: rgb(163, 21, 21);">'ChIJpeKIUfeyzR0R4mvj3gCqCXA'</span>, -33.9321045, 18.860152, 58],</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; [<span style="color: rgb(163, 21, 21);">'Cape Town'</span>, <span style="color: rgb(163, 21, 21);">'ChIJ1-4miA9QzB0Rh6ooKPzhf2g'</span>, -33.9248685, 18.4240553, 59]</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; ];</p>
<p style="margin: 0px;"><br></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">function</span> initMap() {</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; map = <span style="color: blue;">new</span> google.maps.Map(document.getElementById(<span style="color: rgb(163, 21, 21);">'map'</span>), {</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; center: <span style="color: blue;">new</span> google.maps.LatLng(15, -30),</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; zoom: 2,</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; mapTypeId: <span style="color: rgb(163, 21, 21);">'satellite'</span></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; });</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; infowindow = <span style="color: blue;">new</span> google.maps.InfoWindow();</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; service = <span style="color: blue;">new</span> google.maps.places.PlacesService(map);</p>
<p style="margin: 0px;"><br></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; setMarkers(map);</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; }</p>
<p style="margin: 0px;"><br></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">function</span> setMarkers(map) {</p>
<p style="margin: 0px;"><br></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: green;">// Adds markers to the map with a delay</span></p>
<p style="margin: 0px;"><br></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">var</span> delay = 100;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">for</span> (<span style="color: blue;">var</span> i = 0; i &lt;= stops.length; i++) {</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">var</span> timeout = i * delay;</p>
<p style="margin: 0px;"><br></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: green;">// If this is the last segment, just add the line</span></p>
<p style="margin: 0px;"><br></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">if</span> (i === stops.length) {</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; addConnectingLineWithTimeout(stops[i - 1], stops[0], timeout + delay);</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; } <span style="color: blue;">else</span> <span style="color: blue;">if</span> (i &gt;= 0) {</p>
<p style="margin: 0px;"><br></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: green;">// Otherwise add a marker after a delay, followed by the</span></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: green;">// connecting line to the previous marker, if there is one</span></p>
<p style="margin: 0px;"><br></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; addMarkerWithTimeout(stops[i], timeout);</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">if</span> (i &gt; 0) {</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; addConnectingLineWithTimeout(stops[i], stops[i - 1], timeout + delay);</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; }</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; }</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; }</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; }</p>
<p style="margin: 0px;"><br></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">function</span> addMarkerWithTimeout(stop, timeout) {</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; setTimeout(<span style="color: blue;">function</span>() {</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">var</span> marker = <span style="color: blue;">new</span> google.maps.Marker({</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; map: map,</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; title: stop[0],</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; placeId: stop[1],</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; position: {</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; lat: stop[2],</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; lng: stop[3]</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; },</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; label: stop[4].toString(),</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; zIndex: stop[4]</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: green;">//animation: google.maps.Animation.DROP, // Cool but too much</span></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; });</p>
<p style="margin: 0px;"><br></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: green;">// Register the callback for when the marker is clicked</span></p>
<p style="margin: 0px;"><br></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; google.maps.event.addListener(marker, <span style="color: rgb(163, 21, 21);">'click'</span>, <span style="color: blue;">function</span>() {</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; onItemClick(event, marker);</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; });</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; }, timeout);</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; }</p>
<p style="margin: 0px;"><br></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">function</span> addLabelWithTimeout(lat, long, text, timeout) {</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; setTimeout(<span style="color: blue;">function</span>() {</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">var</span> pos = <span style="color: blue;">new</span> google.maps.LatLng(lat, long);</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">var</span> mapLabel = <span style="color: blue;">new</span> MapLabel({</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; text: text,</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; position: pos,</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; map: map,</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; fontSize: 14</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; });</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; mapLabel.set(<span style="color: rgb(163, 21, 21);">'position'</span>, <span style="color: blue;">new</span> google.maps.LatLng(lat, long));</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; }, timeout);</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; }</p>
<p style="margin: 0px;"><br></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">function</span> addConnectingLineWithTimeout(stop1, stop2, timeout) {</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; setTimeout(<span style="color: blue;">function</span>() {</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">var</span> flightPath = <span style="color: blue;">new</span> google.maps.Polyline({</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; path: [{</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; lat: stop1[2],</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; lng: stop1[3]</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; }, {</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; lat: stop2[2],</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; lng: stop2[3]</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; }],</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; geodesic: <span style="color: blue;">true</span>,</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; strokeColor: <span style="color: rgb(163, 21, 21);">'#D34038'</span>,</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; strokeOpacity: 1.0,</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; strokeWeight: 4</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; });</p>
<p style="margin: 0px;"><br></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; flightPath.setMap(map);</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; }, timeout);</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; }</p>
<p style="margin: 0px;"><br></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: green;">// Info window trigger function</span></p>
<p style="margin: 0px;"><br></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">function</span> onItemClick(event, pin) {</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; service.getDetails({</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; placeId: pin.placeId</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; }, <span style="color: blue;">function</span>(place, status) {</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">var</span> cont =</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: rgb(163, 21, 21);">'&lt;div&gt;&lt;h3&gt;'</span> + place.name + <span style="color: rgb(163, 21, 21);">'&lt;/h3&gt;&lt;p&gt;'</span> + place.formatted_address + <span style="color: rgb(163, 21, 21);">'&lt;/p&gt;'</span> +</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; (place.photos &amp;&amp; place.photos.length &gt; 0 ?</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; (<span style="color: rgb(163, 21, 21);">'&lt;img src="'</span> +</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; place.photos[0].getUrl({</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: rgb(163, 21, 21);">'maxWidth'</span>: 300,</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: rgb(163, 21, 21);">'maxHeight'</span>: 200</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; }) + <span style="color: rgb(163, 21, 21);">'" /&gt;'</span>) : <span style="color: rgb(163, 21, 21);">''</span>) +</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: rgb(163, 21, 21);">'&lt;/div&gt;'</span></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; infowindow.setContent(cont);</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; infowindow.open(map, pin);</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; });</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; }</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; <span style="color: blue;">&lt;/</span><span style="color: maroon;">script</span><span style="color: blue;">&gt;</span></p>
<p style="margin: 0px;">&nbsp; <span style="color: blue;">&lt;/</span><span style="color: maroon;">body</span><span style="color: blue;">&gt;</span></p>
<p style="margin: 0px;"><span style="color: blue;">&lt;/</span><span style="color: maroon;">html</span><span style="color: blue;">&gt;</span></p><p style="margin: 0px;"><span style="color: blue;"><br></span></p>
</div>
<p>Here’s a picture of what our map now looks like once one of our placemarkers has been clicked:</p><a href="http://through-the-interface.typepad.com/.a/6a00d83452464869e201b7c90753c3970b-pi" target="_blank"><img width="500" height="313" title="Information about the Salar de Uyuni in Bolivia" style="margin: 30px auto; border: 0px currentcolor; border-image: none; float: none; display: block; background-image: none;" alt="Information about the Salar de Uyuni in Bolivia" src="/assets/image_16236.jpg" border="0"></a><p>Try it for yourself here:</p><p align="center"><br></p><p align="center">
<iframe width="500" height="300" src="https://through-the-interface.typepad.com/files/maps/map1.html"></iframe></p><p><br></p><p>Next time we’ll look at placing labels on the map to indicate the month of travel. I’ll be posting from Washington D.C. which is the first official stop on our trip, if you don’t include a pub lunch in London between flights on Sunday. Here we go!</p>
