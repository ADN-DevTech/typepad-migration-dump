---
layout: "post"
title: "An AngularJs directive for the View & Data API Viewer"
date: "2015-02-15 09:47:33"
author: "Philippe Leefsma"
categories:
  - "HTML5"
  - "Javascript"
  - "Philippe Leefsma"
  - "View and Data API"
original_url: "https://adndevblog.typepad.com/cloud_and_mobile/2015/02/an-angularjs-directive-for-the-view-data-api-viewer.html "
typepad_basename: "an-angularjs-directive-for-the-view-data-api-viewer"
typepad_status: "Publish"
---

<script src="http://code.jquery.com/jquery-2.1.3.min.js"></script>
<script src="https://ajax.googleapis.com/ajax/libs/angularjs/1.2.10/angular.min.js"></script>
<script src="https://developer.api.autodesk.com/viewingservice/v1/viewers/viewer3D.min.js"></script>
<script src="https://rawgit.com/Developer-Autodesk/library-javascript-view.and.data.api/master/js/Autodesk.ADN.Toolkit.Viewer.js"></script>
<p>By&nbsp;<a href="http://adndevblog.typepad.com/cloud_and_mobile/philippe-leefsma.html">Philippe Leefsma</a></p>
<p>Far from being an expert in the topic, I've been playing with&nbsp;<a title="" href="https://angularjs.org/" target="_self">AngularJs</a>&nbsp;for a while and I'm pretty much a huge fan of it... Today's post focuses on creating an angular directive to easily use the View &amp; Data viewer in your angular-powered web pages.</p>
<p>Let's start by defining what is an angular directive:</p>
<p><em>"At a high level, directives are markers on a DOM element (such as an attribute, element name, comment or CSS class) that tell AngularJS's&nbsp;HTML compiler&nbsp;(<a href="https://docs.angularjs.org/api/ng/service/$compile"><code>$compile</code></a>) to attach a specified behavior to that DOM element or even transform the DOM element and its children.</em></p>
<p><em>Angular comes with a set of these directives built-in, like&nbsp;<code>ngBind</code>,&nbsp;<code>ngModel</code>, and&nbsp;<code>ngClass</code>. Much like you create controllers and services, you can create your own directives for Angular to use. When Angular&nbsp;<a href="https://docs.angularjs.org/guide/bootstrap">bootstraps</a>&nbsp;your application, the&nbsp;<a href="https://docs.angularjs.org/guide/compiler">HTML compiler</a>&nbsp;traverses the DOM matching directives against the DOM elements."</em></p>
<p>Basically a directive is a piece of JavaScript code loaded by your angular application that allows to create new html tags or extend the behavior of existing ones. I am not going to expose directive concepts in details, there is already a lot of very documentation on the web, so if you are not familiar with it I strongly recommend you take a look at the following reference before reading any further:</p>
<p><a title="" href="https://docs.angularjs.org/guide/directive" target="_self">Creating Custom Directives</a></p>
<p><a title="" href="https://github.com/angular/angular.js/wiki/Understanding-Directives" target="_self">Understanding Directives</a></p>

<p>Let's start with a simple yet cool directive: let's suppose I want to insert in my webpage a spinning image animated using a css transform. I could use an &lt;img&gt; tag and add some JavaScript code to access the img element which will update the rotation value. That will of course work but if I want to insert 2 or more spinning images, I have to write code accordingly. Applying the same concept to multiple elements may end up in oa lot of fuzzy code all over the place.</p>
<p>A much slicker approach is to write a directive that will handle the spinning logic, so with a simple html tag like below:
<pre style="line-height: 100%;font-family:monospace;background-color:#ffffff; border-width:0.01mm; border-color:#000000; border-style:solid;padding:4px;font-size:10pt;"><span style="color:#800000;background-color:#f0f0f0;"> 1 </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">&lt;!-- A spinning image using adn-spinning-img directive--&gt;
</span><span style="color:#800000;background-color:#f0f0f0;"> 2 </span><span style="background-color:#efefef;">&lt;</span><span style="color:#000080;background-color:#efefef;font-weight:bold;">a</span><span style="background-color:#efefef;"> </span><span style="color:#0000ff;background-color:#efefef;font-weight:bold;">class=</span><span style="color:#008000;background-color:#efefef;font-weight:bold;">"navbar-brand"</span><span style="background-color:#efefef;"> 
</span><span style="color:#800000;background-color:#f0f0f0;"> 3 </span><span style="background-color:#efefef;">   </span><span style="color:#0000ff;background-color:#efefef;font-weight:bold;">href=</span><span style="color:#008000;background-color:#efefef;font-weight:bold;">"http://www.autodesk.com"
</span><span style="color:#800000;background-color:#f0f0f0;"> 4 </span><span style="background-color:#efefef;">    </span><span style="color:#0000ff;background-color:#efefef;font-weight:bold;">target=</span><span style="color:#008000;background-color:#efefef;font-weight:bold;">"_blank"</span><span style="background-color:#efefef;">&gt;
</span><span style="color:#800000;background-color:#f0f0f0;"> 5 </span><span style="background-color:#ffffff;">    </span><span style="background-color:#efefef;">&lt;</span><span style="color:#000080;background-color:#efefef;font-weight:bold;">adn-spinning-img
</span><span style="color:#800000;background-color:#f0f0f0;"> 6 </span><span style="background-color:#efefef;">        </span><span style="color:#0000ff;background-color:#efefef;font-weight:bold;">step=</span><span style="color:#008000;background-color:#efefef;font-weight:bold;">"5.0"
</span><span style="color:#800000;background-color:#f0f0f0;"> 7 </span><span style="background-color:#efefef;">        </span><span style="color:#0000ff;background-color:#efefef;font-weight:bold;">period=</span><span style="color:#008000;background-color:#efefef;font-weight:bold;">"100"
</span><span style="color:#800000;background-color:#f0f0f0;"> 8 </span><span style="background-color:#efefef;">        </span><span style="color:#0000ff;background-color:#efefef;font-weight:bold;">height=</span><span style="color:#008000;background-color:#efefef;font-weight:bold;">"256"
</span><span style="color:#800000;background-color:#f0f0f0;"> 9 </span><span style="background-color:#efefef;">        </span><span style="color:#0000ff;background-color:#efefef;font-weight:bold;">width=</span><span style="color:#008000;background-color:#efefef;font-weight:bold;">"256"
</span><span style="color:#800000;background-color:#f0f0f0;">10 </span><span style="background-color:#efefef;">        </span><span style="color:#0000ff;background-color:#efefef;font-weight:bold;">src=</span><span style="color:#008000;background-color:#efefef;font-weight:bold;">"img.jpg"</span><span style="background-color:#efefef;">/&gt;
</span><span style="color:#800000;background-color:#f0f0f0;">11 </span><span style="background-color:#efefef;">&lt;/</span><span style="color:#000080;background-color:#efefef;font-weight:bold;">a</span><span style="background-color:#efefef;">&gt;</span></pre>
You end up with that result:



<div id="spinningImgAppId">

	<div ng-controller="Autodesk.ADN.Demo.Controller">

<!-- A spinning image using adn-spinning-img directive-->
<a class="navbar-brand"
   href="http://www.autodesk.com"
	target="_blank">
    <adn-spinning-img
		step="5.0"
		period="100"
		height="256"
		width="256"
		src="/assets/image_18af42.jpg"/>
</a>

	</div>
</div>

<div style="height: 270px">
<div id="angularImgDivId">
<a class="asset-img-link"  style="display: inline;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d0d6e2fc970c-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b01b8d0d6e2fc970c image-full img-responsive" alt="Img" title="Img" src="/assets/image_18af42.jpg" border="0" /></a><br/>
</div>
</div>


<script>

	var app = angular.module('Autodesk.ADN.Demo.App', []);

	app.controller('Autodesk.ADN.Demo.Controller', ['$scope', function ($scope) {

		$("#angularImgDivId").remove();

	}]);

	app.directive('adnSpinningImg', ['$interval', function($interval) {

		function link($scope, $element, $attributes) {

			var angle = 0.0;

			function update() {

				angle += parseFloat($attributes.step);

				angle = angle % 360;

				var value = "rotateY(" + angle + "deg)";

				jQuery($element).css({
					"transform": value,
					"-moz-transform": value,
					"-webkit-transform": value,
					"-ms-transform": value,
					"-o-transform": value
				});
			}

			var timerId = $interval(function() {
				update();
			}, parseInt($attributes.period));
		}

		return {
			restrict: 'E',
			replace: true,
			template: '<img height={{height}}width={{width}}src={{src}}style={{style}}>',
			link: link
		}
	}]);

var imgApp = document.getElementById("spinningImgAppId");

angular.element(imgApp).ready(function() {

	angular.bootstrap(
		imgApp,
		['Autodesk.ADN.Demo.App']);
});

</script>

It's actually pretty easy to write such a directive: all you need is setting up a timer in the "link" function that will update the element (ie our image) transform property:
<pre style="line-height: 100%;font-family:monospace;background-color:#ffffff; border-width:0.01mm; border-color:#000000; border-style:solid;padding:4px;font-size:10pt;"><span style="color:#800000;background-color:#f0f0f0;"> 1 </span><span style="background-color:#ffffff;">app.directive(</span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">'adnSpinningImg'</span><span style="background-color:#ffffff;">,
</span><span style="color:#800000;background-color:#f0f0f0;"> 2 </span><span style="background-color:#ffffff;">        [</span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">'$interval'</span><span style="background-color:#ffffff;">, </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">function</span><span style="background-color:#ffffff;">($interval) {
</span><span style="color:#800000;background-color:#f0f0f0;"> 3 
 4 </span><span style="background-color:#ffffff;">    </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">function</span><span style="background-color:#ffffff;"> link($scope, $element, $attributes) {
</span><span style="color:#800000;background-color:#f0f0f0;"> 5 
 6 </span><span style="background-color:#ffffff;">        </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">var</span><span style="background-color:#ffffff;"> angle = </span><span style="color:#0000ff;background-color:#ffffff;">0.0</span><span style="background-color:#ffffff;">;
</span><span style="color:#800000;background-color:#f0f0f0;"> 7 
 8 </span><span style="background-color:#ffffff;">        </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">function</span><span style="background-color:#ffffff;"> update() {
</span><span style="color:#800000;background-color:#f0f0f0;"> 9 
10 </span><span style="background-color:#ffffff;">            angle += parseFloat($attributes.step);
</span><span style="color:#800000;background-color:#f0f0f0;">11 
12 </span><span style="background-color:#ffffff;">            angle = angle % </span><span style="color:#0000ff;background-color:#ffffff;">360</span><span style="background-color:#ffffff;">;
</span><span style="color:#800000;background-color:#f0f0f0;">13 
14 </span><span style="background-color:#ffffff;">            </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">var</span><span style="background-color:#ffffff;"> value = </span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">"rotateY("</span><span style="background-color:#ffffff;"> + angle + </span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">"deg)"</span><span style="background-color:#ffffff;">;
</span><span style="color:#800000;background-color:#f0f0f0;">15 
16 </span><span style="background-color:#ffffff;">            jQuery($element).css({
</span><span style="color:#800000;background-color:#f0f0f0;">17 </span><span style="background-color:#ffffff;">                </span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">"transform"</span><span style="background-color:#ffffff;">: value,
</span><span style="color:#800000;background-color:#f0f0f0;">18 </span><span style="background-color:#ffffff;">                </span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">"-moz-transform"</span><span style="background-color:#ffffff;">: value,
</span><span style="color:#800000;background-color:#f0f0f0;">19 </span><span style="background-color:#ffffff;">                </span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">"-webkit-transform"</span><span style="background-color:#ffffff;">: value,
</span><span style="color:#800000;background-color:#f0f0f0;">20 </span><span style="background-color:#ffffff;">                </span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">"-ms-transform"</span><span style="background-color:#ffffff;">: value,
</span><span style="color:#800000;background-color:#f0f0f0;">21 </span><span style="background-color:#ffffff;">                </span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">"-o-transform"</span><span style="background-color:#ffffff;">: value
</span><span style="color:#800000;background-color:#f0f0f0;">22 </span><span style="background-color:#ffffff;">            });
</span><span style="color:#800000;background-color:#f0f0f0;">23 </span><span style="background-color:#ffffff;">        }
</span><span style="color:#800000;background-color:#f0f0f0;">24 
25 </span><span style="background-color:#ffffff;">        </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">var</span><span style="background-color:#ffffff;"> timerId = $interval(</span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">function</span><span style="background-color:#ffffff;">() {
</span><span style="color:#800000;background-color:#f0f0f0;">26 </span><span style="background-color:#ffffff;">            update();
</span><span style="color:#800000;background-color:#f0f0f0;">27 </span><span style="background-color:#ffffff;">        }, parseInt($attributes.period));
</span><span style="color:#800000;background-color:#f0f0f0;">28 </span><span style="background-color:#ffffff;">    }
</span><span style="color:#800000;background-color:#f0f0f0;">29 
30 </span><span style="background-color:#ffffff;">    </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">return</span><span style="background-color:#ffffff;"> {
</span><span style="color:#800000;background-color:#f0f0f0;">31 </span><span style="background-color:#ffffff;">        restrict: </span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">'E'</span><span style="background-color:#ffffff;">,
</span><span style="color:#800000;background-color:#f0f0f0;">32 </span><span style="background-color:#ffffff;">        replace: </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">true</span><span style="background-color:#ffffff;">,
</span><span style="color:#800000;background-color:#f0f0f0;">33 </span><span style="background-color:#ffffff;">        template: </span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">'&lt;img height={{height}}width={{width}}'
</span><span style="color:#800000;background-color:#f0f0f0;">34 </span><span style="background-color:#ffffff;">            + </span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">'src={{src}}style={{style}}&gt;'</span><span style="background-color:#ffffff;">,
</span><span style="color:#800000;background-color:#f0f0f0;">35 </span><span style="background-color:#ffffff;">        link: link
</span><span style="color:#800000;background-color:#f0f0f0;">36 </span><span style="background-color:#ffffff;">    }
</span><span style="color:#800000;background-color:#f0f0f0;">37 </span><span style="background-color:#ffffff;">}]);</span></pre>

If you find that cool, hang on because the best is yet to come...

How about writing a directive that would allow to easily embed the View & Data viewer in an html page: instead of using a simple <em><strong>div</strong></em> tag and writing a bunch of code that will access the <em>div</em> and render the graphics in it, we could write the following html and have the viewer render the specified model in urn in that area, without worrying about writing any code:
<pre style="line-height: 100%;font-family:monospace;background-color:#ffffff; border-width:0.01mm; border-color:#000000; border-style:solid;padding:4px;font-size:10pt;"><span style="color:#800000;background-color:#f0f0f0;">1 </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">&lt;!-- basic adn-viewer-div --&gt;
</span><span style="color:#800000;background-color:#f0f0f0;">2 </span><span style="background-color:#efefef;">&lt;</span><span style="color:#000080;background-color:#efefef;font-weight:bold;">adn-viewer-div</span><span style="background-color:#efefef;"> </span><span style="color:#0000ff;background-color:#efefef;font-weight:bold;">style=</span><span style="color:#008000;background-color:#efefef;font-weight:bold;">"</span><span style="color:#0000ff;background-color:#ffffff;font-weight:bold;">width</span><span style="background-color:#ffffff;">: </span><span style="color:#0000ff;background-color:#ffffff;">300</span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">px</span><span style="background-color:#ffffff;">; </span><span style="color:#0000ff;background-color:#ffffff;font-weight:bold;">height</span><span style="background-color:#ffffff;">: </span><span style="color:#0000ff;background-color:#ffffff;">300</span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">px</span><span style="background-color:#ffffff;">;</span><span style="color:#008000;background-color:#efefef;font-weight:bold;">"
</span><span style="color:#800000;background-color:#f0f0f0;">3 </span><span style="background-color:#efefef;">    </span><span style="color:#0000ff;background-color:#efefef;font-weight:bold;">url=</span><span style="color:#008000;background-color:#efefef;font-weight:bold;">"&lt;replace with generated token or token url&gt;"
</span><span style="color:#800000;background-color:#f0f0f0;">4 </span><span style="background-color:#efefef;">    </span><span style="color:#0000ff;background-color:#efefef;font-weight:bold;">urn=</span><span style="color:#008000;background-color:#efefef;font-weight:bold;">"&lt;replace with document URN&gt;"</span><span style="background-color:#efefef;">&gt;
</span><span style="color:#800000;background-color:#f0f0f0;">5 </span><span style="background-color:#efefef;">&lt;/</span><span style="color:#000080;background-color:#efefef;font-weight:bold;">adn-viewer-div</span><span style="background-color:#efefef;">&gt;</span></pre>

Yes that would be great! ... but we are developers so we want to customise the behavior of the viewer using it's JavaScript API. Since there is just an html tag, how do I access the viewer object? Well, one elegant solution would be to use on of the tag attribute to specify a callback. That way you could write a method in your controller that will be called once the viewer has been initialized.

Here is how your html could look like:

<pre style="line-height: 100%;font-family:monospace;background-color:#ffffff; border-width:0.01mm; border-color:#000000; border-style:solid;padding:4px;font-size:10pt;"><span style="color:#800000;background-color:#f0f0f0;">1 </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">&lt;!-- adn-viewer-div with initialized callback--&gt;
</span><span style="color:#800000;background-color:#f0f0f0;">2 </span><span style="background-color:#efefef;">&lt;</span><span style="color:#000080;background-color:#efefef;font-weight:bold;">adn-viewer-div</span><span style="background-color:#efefef;"> </span><span style="color:#0000ff;background-color:#efefef;font-weight:bold;">style=</span><span style="color:#008000;background-color:#efefef;font-weight:bold;">"</span><span style="color:#0000ff;background-color:#ffffff;font-weight:bold;">width</span><span style="background-color:#ffffff;">: </span><span style="color:#0000ff;background-color:#ffffff;">300</span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">px</span><span style="background-color:#ffffff;">; </span><span style="color:#0000ff;background-color:#ffffff;font-weight:bold;">height</span><span style="background-color:#ffffff;">: </span><span style="color:#0000ff;background-color:#ffffff;">300</span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">px</span><span style="background-color:#ffffff;">;</span><span style="color:#008000;background-color:#efefef;font-weight:bold;">"
</span><span style="color:#800000;background-color:#f0f0f0;">3 </span><span style="background-color:#efefef;">    </span><span style="color:#0000ff;background-color:#efefef;font-weight:bold;">id=</span><span style="color:#008000;background-color:#efefef;font-weight:bold;">"viewer"
</span><span style="color:#800000;background-color:#f0f0f0;">4 </span><span style="background-color:#efefef;">    </span><span style="color:#0000ff;background-color:#efefef;font-weight:bold;">url=</span><span style="color:#008000;background-color:#efefef;font-weight:bold;">"{{tokenUrl}}"
</span><span style="color:#800000;background-color:#f0f0f0;">5 </span><span style="background-color:#efefef;">    </span><span style="color:#0000ff;background-color:#efefef;font-weight:bold;">urn=</span><span style="color:#008000;background-color:#efefef;font-weight:bold;">"{{docUrn}}"
</span><span style="color:#800000;background-color:#f0f0f0;">6 </span><span style="background-color:#efefef;">    </span><span style="color:#0000ff;background-color:#efefef;font-weight:bold;">viewer-initialized=</span><span style="color:#008000;background-color:#efefef;font-weight:bold;">"onViewerInitialized(viewer)"</span><span style="background-color:#efefef;">&gt;
</span><span style="color:#800000;background-color:#f0f0f0;">7 
8 </span><span style="background-color:#efefef;">&lt;/</span><span style="color:#000080;background-color:#efefef;font-weight:bold;">adn-viewer-div</span><span style="background-color:#efefef;">&gt;</span></pre>
And the controller implementation as follow:
<pre style="line-height: 100%;font-family:monospace;background-color:#ffffff; border-width:0.01mm; border-color:#000000; border-style:solid;padding:4px;font-size:10pt;"><span style="color:#800000;background-color:#f0f0f0;"> 1 </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">var</span><span style="background-color:#ffffff;"> app = angular.module(</span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">'Autodesk.ADN.Demo.App'</span><span style="background-color:#ffffff;">, []);
</span><span style="color:#800000;background-color:#f0f0f0;"> 2 
 3 </span><span style="background-color:#ffffff;">app.controller(</span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">'Autodesk.ADN.Demo.Controller'</span><span style="background-color:#ffffff;">, 
</span><span style="color:#800000;background-color:#f0f0f0;"> 4 </span><span style="background-color:#ffffff;">        [</span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">'$scope'</span><span style="background-color:#ffffff;">, </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">function</span><span style="background-color:#ffffff;"> ($scope) {
</span><span style="color:#800000;background-color:#f0f0f0;"> 5 
 6 </span><span style="background-color:#ffffff;">    $scope.tokenUrl = </span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">'&lt;replace with token or token url&gt;'</span><span style="background-color:#ffffff;">;
</span><span style="color:#800000;background-color:#f0f0f0;"> 7 
 8 </span><span style="background-color:#ffffff;">    $scope.onViewerInitialized = </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">function</span><span style="background-color:#ffffff;">(viewer) {
</span><span style="color:#800000;background-color:#f0f0f0;"> 9 
10 </span><span style="background-color:#ffffff;">        viewer.addEventListener(
</span><span style="color:#800000;background-color:#f0f0f0;">11 </span><span style="background-color:#ffffff;">            Autodesk.Viewing.GEOMETRY_LOADED_EVENT,
</span><span style="color:#800000;background-color:#f0f0f0;">12 </span><span style="background-color:#ffffff;">            </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">function</span><span style="background-color:#ffffff;"> (event) {
</span><span style="color:#800000;background-color:#f0f0f0;">13 </span><span style="background-color:#ffffff;">                    console.log(</span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">'Geometry Loaded...'</span><span style="background-color:#ffffff;">);
</span><span style="color:#800000;background-color:#f0f0f0;">14 </span><span style="background-color:#ffffff;">            });
</span><span style="color:#800000;background-color:#f0f0f0;">15 </span><span style="background-color:#ffffff;">    }
</span><span style="color:#800000;background-color:#f0f0f0;">16 
17 </span><span style="background-color:#ffffff;">    $scope.load = </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">function</span><span style="background-color:#ffffff;">() {
</span><span style="color:#800000;background-color:#f0f0f0;">18 </span><span style="background-color:#ffffff;">        $scope.docUrn =
</span><span style="color:#800000;background-color:#f0f0f0;">19 </span><span style="background-color:#ffffff;">            </span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">"&lt;replace with document URN&gt;"</span><span style="background-color:#ffffff;">;
</span><span style="color:#800000;background-color:#f0f0f0;">20 </span><span style="background-color:#ffffff;">    }
</span><span style="color:#800000;background-color:#f0f0f0;">21 
22 </span><span style="background-color:#ffffff;">    $scope.close = </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">function</span><span style="background-color:#ffffff;">() {
</span><span style="color:#800000;background-color:#f0f0f0;">23 </span><span style="background-color:#ffffff;">        $scope.docUrn = </span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">""</span><span style="background-color:#ffffff;">;
</span><span style="color:#800000;background-color:#f0f0f0;">24 </span><span style="background-color:#ffffff;">    }
</span><span style="color:#800000;background-color:#f0f0f0;">25 
26 </span><span style="background-color:#ffffff;">}]);</span></pre>

Pretty slick! And very angular-ish :)

Below is the implementation of my <em><strong>adn-viewer-div</strong></em> directive. It's not very complicated, however there are several directive concepts that you need to know in order to understand the details. I would suggest first that you take a look at that very concise <a href="http://weblogs.asp.net/dwahlin/creating-custom-angularjs-directives-part-2-isolate-scope">article</a> if you are not an angular directive expert. Also to shorten the code, the directive is using my wrapper API available there: <a href="https://github.com/Developer-Autodesk/library-javascript-view.and.data.api">View and Data API JavaScript Wrapper Library</a>

<pre style="line-height: 100%;font-family:monospace;background-color:#ffffff; border-width:0.01mm; border-color:#000000; border-style:solid;padding:4px;font-size:10pt;"><span style="color:#800000;background-color:#f0f0f0;"> 1 </span><span style="background-color:#ffffff;">app.directive(</span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">'adnViewerDiv'</span><span style="background-color:#ffffff;">, </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">function</span><span style="background-color:#ffffff;"> () {
</span><span style="color:#800000;background-color:#f0f0f0;"> 2 
 3 </span><span style="background-color:#ffffff;">    </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">function</span><span style="background-color:#ffffff;"> link($scope, $element, $attributes) {
</span><span style="color:#800000;background-color:#f0f0f0;"> 4 
 5 </span><span style="background-color:#ffffff;">        </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">// instanciate viewer manager in directive scope
</span><span style="color:#800000;background-color:#f0f0f0;"> 6 
 7 </span><span style="background-color:#ffffff;">        $scope.adnViewerMng =
</span><span style="color:#800000;background-color:#f0f0f0;"> 8 </span><span style="background-color:#ffffff;">         </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">new</span><span style="background-color:#ffffff;"> Autodesk.ADN.Toolkit.Viewer.AdnViewerManager(
</span><span style="color:#800000;background-color:#f0f0f0;"> 9 </span><span style="background-color:#ffffff;">            $attributes.url,
</span><span style="color:#800000;background-color:#f0f0f0;">10 </span><span style="background-color:#ffffff;">            $element[</span><span style="color:#0000ff;background-color:#ffffff;">0</span><span style="background-color:#ffffff;">],
</span><span style="color:#800000;background-color:#f0f0f0;">11 </span><span style="background-color:#ffffff;">            ($attributes.hasOwnProperty(</span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">'environment'</span><span style="background-color:#ffffff;">) ?
</span><span style="color:#800000;background-color:#f0f0f0;">12 </span><span style="background-color:#ffffff;">                $attributes.environment :
</span><span style="color:#800000;background-color:#f0f0f0;">13 </span><span style="background-color:#ffffff;">                </span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">'AutodeskProduction'</span><span style="background-color:#ffffff;">));
</span><span style="color:#800000;background-color:#f0f0f0;">14 
15 </span><span style="background-color:#ffffff;">        $attributes.$observe(</span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">'urn'</span><span style="background-color:#ffffff;">, </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">function</span><span style="background-color:#ffffff;">(urn) {
</span><span style="color:#800000;background-color:#f0f0f0;">16 
17 </span><span style="background-color:#ffffff;">            </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">// check if urn is not empty
</span><span style="color:#800000;background-color:#f0f0f0;">18 </span><span style="background-color:#ffffff;">            </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">// if empty close doc
</span><span style="color:#800000;background-color:#f0f0f0;">19 </span><span style="background-color:#ffffff;">            </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">if</span><span style="background-color:#ffffff;">(urn.length) {
</span><span style="color:#800000;background-color:#f0f0f0;">20 
21 </span><span style="background-color:#ffffff;">                </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">// loads document from urn
</span><span style="color:#800000;background-color:#f0f0f0;">22 </span><span style="background-color:#ffffff;">                $scope.adnViewerMng.loadDocument(
</span><span style="color:#800000;background-color:#f0f0f0;">23 </span><span style="background-color:#ffffff;">                        urn,
</span><span style="color:#800000;background-color:#f0f0f0;">24 </span><span style="background-color:#ffffff;">                        </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">function</span><span style="background-color:#ffffff;"> (viewer) {
</span><span style="color:#800000;background-color:#f0f0f0;">25 </span><span style="background-color:#ffffff;">                            $scope.viewerInitialized({
</span><span style="color:#800000;background-color:#f0f0f0;">26 </span><span style="background-color:#ffffff;">                                viewer: viewer
</span><span style="color:#800000;background-color:#f0f0f0;">27 </span><span style="background-color:#ffffff;">                            })
</span><span style="color:#800000;background-color:#f0f0f0;">28 </span><span style="background-color:#ffffff;">                        });
</span><span style="color:#800000;background-color:#f0f0f0;">29 </span><span style="background-color:#ffffff;">            }
</span><span style="color:#800000;background-color:#f0f0f0;">30 </span><span style="background-color:#ffffff;">            </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">else</span><span style="background-color:#ffffff;"> {
</span><span style="color:#800000;background-color:#f0f0f0;">31 </span><span style="background-color:#ffffff;">                $scope.adnViewerMng.closeDocument();
</span><span style="color:#800000;background-color:#f0f0f0;">32 </span><span style="background-color:#ffffff;">            }
</span><span style="color:#800000;background-color:#f0f0f0;">33 </span><span style="background-color:#ffffff;">        });
</span><span style="color:#800000;background-color:#f0f0f0;">34 </span><span style="background-color:#ffffff;">    }
</span><span style="color:#800000;background-color:#f0f0f0;">35 
36 </span><span style="background-color:#ffffff;">    </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">return</span><span style="background-color:#ffffff;"> {
</span><span style="color:#800000;background-color:#f0f0f0;">37 </span><span style="background-color:#ffffff;">        scope: {
</span><span style="color:#800000;background-color:#f0f0f0;">38 </span><span style="background-color:#ffffff;">            url: </span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">'@'</span><span style="background-color:#ffffff;">,
</span><span style="color:#800000;background-color:#f0f0f0;">39 </span><span style="background-color:#ffffff;">            urn: </span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">'@'</span><span style="background-color:#ffffff;">,
</span><span style="color:#800000;background-color:#f0f0f0;">40 </span><span style="background-color:#ffffff;">            viewerInitialized: </span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">'&'
</span><span style="color:#800000;background-color:#f0f0f0;">41 </span><span style="background-color:#ffffff;">        },
</span><span style="color:#800000;background-color:#f0f0f0;">42 </span><span style="background-color:#ffffff;">        restrict: </span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">'E'</span><span style="background-color:#ffffff;">,
</span><span style="color:#800000;background-color:#f0f0f0;">43 </span><span style="background-color:#ffffff;">        replace: </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">true</span><span style="background-color:#ffffff;">,
</span><span style="color:#800000;background-color:#f0f0f0;">44 </span><span style="background-color:#ffffff;">        template: </span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">'&lt;div style="overflow: hidden;'</span><span style="background-color:#ffffff;"> +
</span><span style="color:#800000;background-color:#f0f0f0;">45 </span><span style="background-color:#ffffff;">         </span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">' position: relative; {{style}}"&gt;&lt;div/&gt;'</span><span style="background-color:#ffffff;">,
</span><span style="color:#800000;background-color:#f0f0f0;">46 </span><span style="background-color:#ffffff;">        link: link
</span><span style="color:#800000;background-color:#f0f0f0;">47 </span><span style="background-color:#ffffff;">    };
</span><span style="color:#800000;background-color:#f0f0f0;">48 </span><span style="background-color:#ffffff;">});</span></pre>
 
Here is the complete code for that sample, also attached as a zip:

<script src="https://gist.github.com/leefsmp/993743203edb540f1bd3.js"></script>

Finally a side note: the url expected by the directive is either a token string generated using your API credentials or more useful the url of your token server which should return a json response formatted as follow:

<pre style="line-height: 100%;font-family:monospace;background-color:#ffffff; border-width:0.01mm; border-color:#000000; border-style:solid;padding:4px;font-size:10pt;"><span style="color:#800000;background-color:#f0f0f0;">1 </span><span style="background-color:#ffffff;">{
</span><span style="color:#800000;background-color:#f0f0f0;">2 </span><span style="background-color:#ffffff;">    token_type: "Bearer",
</span><span style="color:#800000;background-color:#f0f0f0;">3 </span><span style="background-color:#ffffff;">    expires_in: 1799,
</span><span style="color:#800000;background-color:#f0f0f0;">4 </span><span style="background-color:#ffffff;">    access_token: "9PWqbIZyAXh5DIVKKCj9XaCdYiy4"
</span><span style="color:#800000;background-color:#f0f0f0;">5 </span><span style="background-color:#ffffff;">}</span></pre>
<span class="asset  asset-generic at-xid-6a0167607c2431970b01bb07f13cf8970d img-responsive"><a href="http://adndevblog.typepad.com/files/directive-demo.zip">Download Directive-demo</a></span>
