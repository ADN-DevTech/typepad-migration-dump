---
layout: "post"
title: "Exploring Boostrap Angular UI components"
date: "2015-02-19 03:04:53"
author: "Philippe Leefsma"
categories:
  - "HTML5"
  - "Javascript"
  - "Philippe Leefsma"
  - "View and Data API"
  - "WebGL"
original_url: "https://adndevblog.typepad.com/cloud_and_mobile/2015/02/exploring-boostrap-angular-ui-components.html "
typepad_basename: "exploring-boostrap-angular-ui-components"
typepad_status: "Publish"
---

<p style="text-align: left;">By&nbsp;<a href="http://adndevblog.typepad.com/cloud_and_mobile/philippe-leefsma.html">Philippe Leefsma</a></p>
<p style="text-align: left;"><a href="https://angularjs.org/" target="_self">AngularJs</a>&nbsp;is starting to impose itself as the reference JavaScript framework to develop <a href="http://en.wikipedia.org/wiki/Single-page_application" target="_self">SPA</a>&nbsp;- Single Page (Web) Applications. And probably for a reason, that stuff acts like magic :)</p>
<p style="text-align: left;"><a href="http://getbootstrap.com/" target="_self">Boostrap</a>, another world famous framework for building websites, allows you to quickly create nice looking UI components.</p>
<p style="text-align: left;">I'm working on building a demo website featuring our viewing technology entirely relying on Angular-friendly components, so today I'm taking a look at two Bootstrap libraries offering Angular UI directives:</p>
<p style="text-align: left;">&nbsp; &nbsp; - <a href="http://angular-ui.github.io/bootstrap/" target="_self">UI Boostrap</a></p>
<p style="text-align: left;">&nbsp; &nbsp; - <a href="http://mgcrea.github.io/angular-strap/" target="_self">AngularStrap</a></p>
<p style="text-align: left;">Both offer some pretty cool UI components and you can directly test them on their webpage, which makes it very explicit to see what you can achieve.</p>
<p style="text-align: left;">I decided to pick up one and integrate our viewer in it to see how they can get along: a nice one is the Bootstrap UI Carousel, a component that displays a collection of slides rotating on a timer. You can see a plunker demo of the carousel <a href="http://plnkr.co/edit/?p=preview" target="_self">there</a>. Not that I don't like kittens, but I think 3D models on the web are cooler ;)</p>
<p style="text-align: left;">The integration ended up being more challenging and instructive than I initially thought. Here are the highlights of the sample:
<br><br>
Following code illustrates how to perform a <a href="http://en.wikipedia.org/wiki/JSONP">jsonP</a> call from the angular controller:
<pre style="line-height: 100%;font-family:monospace;background-color:#ffffff; border-width:0.01mm; border-color:#000000; border-style:solid;padding:4px;font-size:10pt;"><span style="color:#800000;background-color:#f0f0f0;"> 1 </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">//jsonP call to get total number of models in the Gallery
</span><span style="color:#800000;background-color:#f0f0f0;"> 2 </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">function</span><span style="background-color:#ffffff;"> getGalleryModelCount(onSuccess) {
</span><span style="color:#800000;background-color:#f0f0f0;"> 3 </span><span style="background-color:#ffffff;">    
</span><span style="color:#800000;background-color:#f0f0f0;"> 4 </span><span style="background-color:#ffffff;">  </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">var</span><span style="background-color:#ffffff;"> url = </span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">'http://gallery.autodesk.io/api/models/count'</span><span style="background-color:#ffffff;">;
</span><span style="color:#800000;background-color:#f0f0f0;"> 5 
 6 </span><span style="background-color:#ffffff;">  $http.jsonp(url + </span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">"?callback=JSON_CALLBACK"</span><span style="background-color:#ffffff;">).
</span><span style="color:#800000;background-color:#f0f0f0;"> 7 </span><span style="background-color:#ffffff;">        success(</span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">function</span><span style="background-color:#ffffff;">(data, status, headers, config) {
</span><span style="color:#800000;background-color:#f0f0f0;"> 8 </span><span style="background-color:#ffffff;">            onSuccess(data.count);
</span><span style="color:#800000;background-color:#f0f0f0;"> 9 </span><span style="background-color:#ffffff;">        }).
</span><span style="color:#800000;background-color:#f0f0f0;">10 </span><span style="background-color:#ffffff;">        error(</span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">function</span><span style="background-color:#ffffff;">(data, status, headers, config) {
</span><span style="color:#800000;background-color:#f0f0f0;">11 </span><span style="background-color:#ffffff;">            console.log(</span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">'Error: '</span><span style="background-color:#ffffff;"> + status);
</span><span style="color:#800000;background-color:#f0f0f0;">12 </span><span style="background-color:#ffffff;">        });
</span><span style="color:#800000;background-color:#f0f0f0;">13 </span><span style="background-color:#ffffff;">}</span></pre>

This is needed because invoking the Gallery REST API from a different domain requires a cross domain call. Also needed, the activation of cors and jsonp on my node.js server:
<br>
<br>
<pre style="line-height: 100%;font-family:monospace;background-color:#ffffff; border-width:0.01mm; border-color:#000000; border-style:solid;padding:4px;font-size:10pt;"><span style="color:#800000;background-color:#f0f0f0;"> 1 </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">var</span><span style="background-color:#ffffff;"> app = express();
</span><span style="color:#800000;background-color:#f0f0f0;"> 2 
 3 </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">//CORS middleware
</span><span style="color:#800000;background-color:#f0f0f0;"> 4 </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">var</span><span style="background-color:#ffffff;"> cors = </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">function</span><span style="background-color:#ffffff;"> (req, res, next) {
</span><span style="color:#800000;background-color:#f0f0f0;"> 5 
 6 </span><span style="background-color:#ffffff;">    res.header(</span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">"Access-Control-Allow-Origin"</span><span style="background-color:#ffffff;">, </span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">"*"</span><span style="background-color:#ffffff;">);
</span><span style="color:#800000;background-color:#f0f0f0;"> 7 
 8 </span><span style="background-color:#ffffff;">    res.header(</span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">"Access-Control-Allow-Headers"</span><span style="background-color:#ffffff;">, 
</span><span style="color:#800000;background-color:#f0f0f0;"> 9 </span><span style="background-color:#ffffff;">      </span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">"Origin, X-Requested-With, Content-Type, Accept"</span><span style="background-color:#ffffff;">);
</span><span style="color:#800000;background-color:#f0f0f0;">10 
11 </span><span style="background-color:#ffffff;">    res.header(</span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">'Access-Control-Allow-Methods'</span><span style="background-color:#ffffff;">,
</span><span style="color:#800000;background-color:#f0f0f0;">12 </span><span style="background-color:#ffffff;">        </span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">'GET,PUT,POST,DELETE'</span><span style="background-color:#ffffff;">);
</span><span style="color:#800000;background-color:#f0f0f0;">13 
14 </span><span style="background-color:#ffffff;">    next();
</span><span style="color:#800000;background-color:#f0f0f0;">15 </span><span style="background-color:#ffffff;">}
</span><span style="color:#800000;background-color:#f0f0f0;">16 
17 </span><span style="background-color:#ffffff;">app.use(cors);
</span><span style="color:#800000;background-color:#f0f0f0;">18 
19 </span><span style="background-color:#ffffff;">app.set(</span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">"jsonp callback"</span><span style="background-color:#ffffff;">, </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">true</span><span style="background-color:#ffffff;">);</span></pre>
An angular filter is also required because I bind the iframe ng-source to a scope.member, so the url as to be trusted...
<br>
<br>

<pre style="line-height: 100%;font-family:monospace;background-color:#ffffff; border-width:0.01mm; border-color:#000000; border-style:solid;padding:4px;font-size:10pt;"><span style="color:#800000;background-color:#f0f0f0;">1 </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">// needs that filter to bind iframe ng-src to scope member
</span><span style="color:#800000;background-color:#f0f0f0;">2 </span><span style="background-color:#ffffff;">app.filter(</span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">'trustAsResourceUrl'</span><span style="background-color:#ffffff;">, [</span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">'$sce'</span><span style="background-color:#ffffff;">, </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">function</span><span style="background-color:#ffffff;">($sce) {
</span><span style="color:#800000;background-color:#f0f0f0;">3 </span><span style="background-color:#ffffff;">    </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">return</span><span style="background-color:#ffffff;"> </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">function</span><span style="background-color:#ffffff;">(url) {
</span><span style="color:#800000;background-color:#f0f0f0;">4 </span><span style="background-color:#ffffff;">        </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">return</span><span style="background-color:#ffffff;"> $sce.trustAsResourceUrl(url);
</span><span style="color:#800000;background-color:#f0f0f0;">5 </span><span style="background-color:#ffffff;">}}]);</span></pre>


Just for fun, I wanted to set up a listener for the carousel slide changed event, that <a href="http://stackoverflow.com/questions/24686119/how-do-you-bind-to-angular-uis-carousel-slide-events">thread</a> gives a pretty exhaustive solution:
<br>
<br>
<pre style="line-height: 100%;font-family:monospace;background-color:#ffffff; border-width:0.01mm; border-color:#000000; border-style:solid;padding:4px;font-size:10pt;"><span style="color:#800000;background-color:#f0f0f0;"> 1 </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">// a directive to watch carousel slide changed event
</span><span style="color:#800000;background-color:#f0f0f0;"> 2 </span><span style="background-color:#ffffff;">app.directive(</span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">'onCarouselChange'</span><span style="background-color:#ffffff;">, </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">function</span><span style="background-color:#ffffff;"> ($parse) {
</span><span style="color:#800000;background-color:#f0f0f0;"> 3 </span><span style="background-color:#ffffff;">    </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">return</span><span style="background-color:#ffffff;"> {
</span><span style="color:#800000;background-color:#f0f0f0;"> 4 </span><span style="background-color:#ffffff;">        require: </span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">'carousel'</span><span style="background-color:#ffffff;">,
</span><span style="color:#800000;background-color:#f0f0f0;"> 5 </span><span style="background-color:#ffffff;">        link: </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">function</span><span style="background-color:#ffffff;"> (scope,
</span><span style="color:#800000;background-color:#f0f0f0;"> 6 </span><span style="background-color:#ffffff;">                element,
</span><span style="color:#800000;background-color:#f0f0f0;"> 7 </span><span style="background-color:#ffffff;">                attrs,
</span><span style="color:#800000;background-color:#f0f0f0;"> 8 </span><span style="background-color:#ffffff;">                carouselCtrl) {
</span><span style="color:#800000;background-color:#f0f0f0;"> 9 </span><span style="background-color:#ffffff;">            </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">var</span><span style="background-color:#ffffff;"> fn = $parse(attrs.onCarouselChange);
</span><span style="color:#800000;background-color:#f0f0f0;">10 </span><span style="background-color:#ffffff;">            </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">var</span><span style="background-color:#ffffff;"> origSelect = carouselCtrl.select;
</span><span style="color:#800000;background-color:#f0f0f0;">11 </span><span style="background-color:#ffffff;">            carouselCtrl.select =
</span><span style="color:#800000;background-color:#f0f0f0;">12 </span><span style="background-color:#ffffff;">                </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">function</span><span style="background-color:#ffffff;"> (nextSlide, direction) {
</span><span style="color:#800000;background-color:#f0f0f0;">13 </span><span style="background-color:#ffffff;">                  </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">if</span><span style="background-color:#ffffff;"> (nextSlide !== </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">this</span><span style="background-color:#ffffff;">.currentSlide) {
</span><span style="color:#800000;background-color:#f0f0f0;">14 </span><span style="background-color:#ffffff;">                        fn(scope, {
</span><span style="color:#800000;background-color:#f0f0f0;">15 </span><span style="background-color:#ffffff;">                            nextSlide: nextSlide,
</span><span style="color:#800000;background-color:#f0f0f0;">16 </span><span style="background-color:#ffffff;">                            direction: direction
</span><span style="color:#800000;background-color:#f0f0f0;">17 </span><span style="background-color:#ffffff;">                        });
</span><span style="color:#800000;background-color:#f0f0f0;">18 </span><span style="background-color:#ffffff;">                }
</span><span style="color:#800000;background-color:#f0f0f0;">19 </span><span style="background-color:#ffffff;">                </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">return</span><span style="background-color:#ffffff;"> origSelect.apply(</span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">this</span><span style="background-color:#ffffff;">, arguments);
</span><span style="color:#800000;background-color:#f0f0f0;">20 </span><span style="background-color:#ffffff;">            };
</span><span style="color:#800000;background-color:#f0f0f0;">21 </span><span style="background-color:#ffffff;">        }
</span><span style="color:#800000;background-color:#f0f0f0;">22 </span><span style="background-color:#ffffff;">    };
</span><span style="color:#800000;background-color:#f0f0f0;">23 </span><span style="background-color:#ffffff;">});</span></pre>



Here is the full code of the final result and the live demo: that carousel will fetch iframe slides of models from my <a href="http://gallery.autodesk.io/" target="_self">Gallery</a>, you can hit the "Add Slide" button to randomly add a new model slide.</p>

<script src="https://gist.github.com/leefsmp/05d62aefa165c85e47ae.js"></script>


	<link type="text/css" rel="stylesheet" href="https://developer.api.autodesk.com/viewingservice/v1/viewers/style.css"/>
	<link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.0/css/bootstrap.min.css">
	<link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.0/css/bootstrap-theme.min.css">

<body>

	<!--div ng-app="Autodesk.ADN.Demo.Bootstrap.App"-->

	<div id="carouselAppId">
		<div ng-controller="Autodesk.ADN.Demo.Bootstrap.Controller"
			 style="height: 490px; width: 490px">

			<div >
				<!-- a bootstrap ui carousel with custom directive for onSildeChanged event -->
				<carousel interval="myInterval"
						  on-carousel-change="onSlideChanged(nextSlide, direction)">

					<slide ng-repeat="slide in slides" active="slide.active">

						<iframe
							width='100%' height='490px' frameborder='0'
							allowFullScreen webkitallowfullscreen mozallowfullscreen
							ng-src='{{slide.url | trustAsResourceUrl}}'>
						</iframe>

		<div class="carousel-caption" style="text-align:center">
		<h4>Model {{$index+1}}</h4><h5>{{slide.name}}</h5>
						</div>

					</slide>
				</carousel>
			</div>

			<div class="row">
				<br>
				<div class="col-md-6">
					<button type="button" class="btn btn-info" ng-click="addSlide()">Add Slide</button>
				</div>
				<div class="col-md-6">
					Interval, in milliseconds: <input type="number" class="form-control" ng-model="myInterval">
					<br />Enter a negative number or 0 to stop the interval.
				</div>
			</div>

		</div>

	</div>

<script src="http://code.jquery.com/jquery-2.1.3.min.js"></script>
<script src="https://ajax.googleapis.com/ajax/libs/angularjs/1.2.10/angular.min.js"></script>
<script src="http://angular-ui.github.io/bootstrap/ui-bootstrap-tpls-0.12.0.min.js"></script>

<script>

var app = angular.module(
	'Autodesk.ADN.Demo.Bootstrap.App',
		['ui.bootstrap']);

app.controller('Autodesk.ADN.Demo.Bootstrap.Controller',
		['$scope', '$http', function ($scope, $http) {

			$scope.myInterval = 8000;

			$scope.slides = [];

			$scope.count = 1;

			// add slide from modelId - internal method
			function addSlide(index) {

				getGalleryModel(index, function(model){

					var url = 'http://viewer.autodesk.io/node/gallery/embed?id=';

					$scope.slides.push({
						url: url + model._id,
						name: model.name
					});

					$scope.slides[$scope.slides.length - 1].active = true;
				});
			};

			// on slide changed event - just for testing
			$scope.onSlideChanged = function (nextSlide, direction) {

				//console.log('onSlideChanged:', direction, nextSlide);
			};

			// returns random int in [min, max]
			function randomInt(min, max) {

				 return Math.floor(Math.random() * (max - min)) + min;
			 }

			// jsonP call to get total number of models in the Gallery
			function getGalleryModelCount(onSuccess) {

				var url =  'http://viewer.autodesk.io/node/gallery/api/models/count';

				$http.jsonp(url + "?callback=JSON_CALLBACK").
					success(function(data, status, headers, config) {
						onSuccess(data.count);
					}).
					error(function(data, status, headers, config) {
						console.log('Error: ' + status);
					});
			}

			// get model data from index
			function getGalleryModel(index, onSuccess) {

				// use Gallery REST API to pick up one model
				var url =  'http://viewer.autodesk.io/node/gallery/api/models?skip='
						+ index + '&limit=1';

				$http.jsonp(url + "&callback=JSON_CALLBACK").
					success(function(data, status, headers, config) {
						onSuccess(data.models[0]);
					}).
					error(function(data, status, headers, config) {
						console.log('Error: ' + status);
					});
			}

			// add slide - scope method
			$scope.addSlide = function() {

				// picks a random model from the gallery
				var index = randomInt(0, $scope.count-1);

				addSlide(index);
			}

			// stores model count
			getGalleryModelCount(function(count) {

				$scope.count = count;
			});

			// Adds first slide
			addSlide(0);
}]);

// a directive to watch carousel slide changed event
app.directive('onCarouselChange', function ($parse) {
	return {
		require: 'carousel',
		link: function (scope, element, attrs, carouselCtrl) {
			var fn = $parse(attrs.onCarouselChange);
			var origSelect = carouselCtrl.select;
			carouselCtrl.select = function (nextSlide, direction) {
				if (nextSlide !== this.currentSlide) {
					fn(scope, {
						nextSlide: nextSlide,
						direction: direction
					});
				}
				return origSelect.apply(this, arguments);
			};
		}
	};
});

// needs that filter to bind iframe ng-src to scope member
app.filter('trustAsResourceUrl', ['$sce', function($sce) {
	return function(url) {
		return $sce.trustAsResourceUrl(url);
}}]);

var carouselApp = document.getElementById("carouselAppId");

angular.element(carouselApp).ready(function() {

	angular.bootstrap(
		carouselApp,
		['Autodesk.ADN.Demo.Bootstrap.App']);
});

</script>
