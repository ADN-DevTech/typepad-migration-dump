---
layout: "post"
title: "Managing local storage with js-data, the Angular way"
date: "2015-03-26 00:45:33"
author: "Philippe Leefsma"
categories:
  - "HTML5"
  - "Javascript"
  - "Philippe Leefsma"
  - "View and Data API"
original_url: "https://adndevblog.typepad.com/cloud_and_mobile/2015/03/managing-local-storage-with-js-data-the-angular-way.html "
typepad_basename: "managing-local-storage-with-js-data-the-angular-way"
typepad_status: "Publish"
---

<p style="text-align: left;">By&nbsp;<a href="http://adndevblog.typepad.com/cloud_and_mobile/philippe-leefsma.html">Philippe Leefsma</a></p>
<p><a href="http://www.js-data.io/v1.5.12/docs/home" target="_self"><strong>JSData</strong></a>&nbsp;consists of a convenient&nbsp;framework-agnostic,&nbsp;in-memory cache&nbsp;for managing your data, which then uses&nbsp;adapters&nbsp;to communicate with various&nbsp;persistence layers.The most commonly used adapters are the&nbsp;<a href="http://www.js-data.io/v1.5.12/docs/dshttpadapter" target="_blank">http adapter</a>, for communicating with a RESTful backend, the&nbsp;<a href="http://www.js-data.io/js-data-localstorage" target="_blank">localStorage</a>,&nbsp;<a href="http://www.js-data.io/js-data.localforage" target="_blank">localForage</a>, and&nbsp;<a href="http://www.js-data.io/js-data-firebase" target="_blank">firebase</a>. <a href="http://www.js-data.io/docs/working-with-adapters" target="_blank">Other adapters</a>&nbsp;are also available.&nbsp;</p>
<p>In that post I will focus on the <a href="http://en.wikipedia.org/wiki/Web_storage" target="_self">localStorage</a>, an html5 feature that allows to store data on the client side. In order to make it a little more exciting, I will let the user store and clear <a href="http://adndevblog.typepad.com/cloud_and_mobile/2015/02/managing-viewer-states-from-the-api.html" target="_self">viewer states</a> through a simple demo.&nbsp;</p>
<p>Although JSData is&nbsp;framework-agnostic, it has an <a href="http://www.js-data.io/v1.5.12/docs/js-data-angular" target="_self">AngularJs extension</a> which makes it very convenient to use within an Angular app.</p>
<p>Here is the <a href="http://adndevblog.typepad.com/cloud_and_mobile/2015/03/managing-local-storage-with-js-data-the-angular-way.html">live sample</a> and the code that goes along. You can play with saving and restoring the viewer states, which will be persisted across sessions. A state includes camera properties, selected/isolated components and rendering settings.</p>

<div id="storage-demo-img">
<a class="asset-img-link"  style="display: inline;" href="http://adndevblog.typepad.com/cloud_and_mobile/2015/03/managing-local-storage-with-js-data-the-angular-way.html"><img class="asset  asset-image at-xid-6a0167607c2431970b01b7c76b96c1970b image-full img-responsive" alt="Storage-demo" title="Storage-demo" src="/assets/image_79106c.jpg" border="0" /></a><br />
</div>



	<link rel="stylesheet" href="https://developer.api.autodesk.com/viewingservice/v1/viewers/style.css?v=1.2.8"/>
	<link rel="stylesheet" href="http://netdna.bootstrapcdn.com/bootstrap/3.1.1/css/bootstrap.css">
	<link rel="stylesheet" href="https://rawgit.com/angular-ui/ui-select/master/dist/select.min.css">
	<link rel="stylesheet" href="http://cdnjs.cloudflare.com/ajax/libs/select2/3.4.5/select2.css">
	<link rel="stylesheet" href="http://cdnjs.cloudflare.com/ajax/libs/selectize.js/0.8.5/css/selectize.default.css">

<div ng-app="Autodesk.ADN.Demo.Storage.App" id="storage-demo-app-div">

	<div ng-controller="Autodesk.ADN.Demo.Storage.Controller">

		<div id="viewerStorageDemoDiv"
			 style="height: 480px; width: 480px; overflow: hidden; position: relative">
		</div>
		<br>
		<div>
			<div class="row">
				<div class="col-md-3" style="width: 335px;">
					<ui-select ng-model="item.selected" theme="selectize" ng-disabled="disabled" style="width: 310px;" title="Choose a state">
						<ui-select-match placeholder="Select or search an item...">{{$select.selected.label}}</ui-select-match>
						<ui-select-choices repeat="item in items | filter: $select.search">
							<span ng-bind-html="item.label "></span>
						</ui-select-choices>
					</ui-select>
				</div>

				
					<button type="button" class="btn btn-default" ng-click="onAddState()">Store state</button>
					<button type="button" class="btn btn-default" ng-click="onClearStates()">Clear</button>
				
			</div>
		</div>
	</div>
</div>


<script src="http://code.jquery.com/jquery-2.1.3.min.js"></script>
<script src="http://ajax.googleapis.com/ajax/libs/angularjs/1.2.18/angular.js"></script>
<script src="http://ajax.googleapis.com/ajax/libs/angularjs/1.2.18/angular-sanitize.js"></script>
<script src="https://rawgit.com/angular-ui/ui-select/master/dist/select.min.js"></script>

<script src="https://rawgit.com/js-data/js-data/master/dist/js-data.min.js"></script>
<script src="https://rawgit.com/js-data/js-data-angular/master/dist/js-data-angular.min.js"></script>
<script src="https://rawgit.com/js-data/js-data-localstorage/master/dist/js-data-localstorage.min.js"></script>

<script src="https://developer.api.autodesk.com/viewingservice/v1/viewers/viewer3D.min.js?v=1.2.8"></script>
<script src="https://rawgit.com/Developer-Autodesk/library-javascript-view.and.data.api/master/js/Autodesk.ADN.Toolkit.Viewer.js"></script>


<script>

if(window.location.toString().indexOf('managing-local-storage-with-js-data-the-angular-way.html') > 0) {

$('#storage-demo-img').remove();

////////////////////////////////////////////////////////
// The Angular App
//
////////////////////////////////////////////////////////
var app = angular.module(
		'Autodesk.ADN.Demo.Storage.App',
	[
		'js-data',
		'ngSanitize',
		'ui.select'
	]);

////////////////////////////////////////////////////////
// js-data store factory
//
////////////////////////////////////////////////////////
app.factory('store', function () {

		var store = new JSData.DS();

		store.registerAdapter(
				'localstorage',
				new DSLocalStorageAdapter(),
				{ default: true });

		return store;
	});

app.factory('StateStore', function (store) {
		return store.defineResource('state');
	});

////////////////////////////////////////////////////////
// the App controller
//
////////////////////////////////////////////////////////
app.controller('Autodesk.ADN.Demo.Storage.Controller',

	function ($scope, $http, $sce, StateStore) {

		////////////////////////////////////////////////
		// $scope members
		//
		////////////////////////////////////////////////
		$scope.items = [];

		$scope.item = {};

		////////////////////////////////////////////////
		// retrieve all stored states
		//
		////////////////////////////////////////////////
		StateStore.findAll().then(function (states) {

			states.forEach(function(state) {

				$scope.items.push({
					value: state.id,
					label: $sce.trustAsHtml(state.name)
				});
			});

			console.log(states);
		});

		////////////////////////////////////////////////
		// AddState callback
		//
		////////////////////////////////////////////////
		$scope.onAddState = function() {

			var data = $scope.viewer.getState();

			var name = new Date().toString(
				'd/M/yyyy H:mm:ss');

			var stateData = {
				name: name,
				data: data
			}

			StateStore.create(stateData)

				.then(function(state) {

					$scope.items.push({
						value: state.id,
						label: $sce.trustAsHtml(name)
					});
				});
		}

		////////////////////////////////////////////////
		// ClearStates callback
		//
		////////////////////////////////////////////////
		$scope.onClearStates = function() {

                        $scope.item = {};
			$scope.items = [];

			StateStore.destroyAll();
		}

		////////////////////////////////////////////////
		// watch selectedItem
		//
		////////////////////////////////////////////////
		$scope.$watch('item.selected', function() {

			if($scope.item.selected) {

				var state = StateStore.get(
					$scope.item.selected.value);

				$scope.viewer.restoreState(state.data);
			}
		});

		////////////////////////////////////////////////
		// Load urn with token
		//
		////////////////////////////////////////////////
		$scope.loadURN = function(token, urn) {

			var config = {
				environment : 'AutodeskProduction'
			}

			var viewerFactory =
				new Autodesk.ADN.Toolkit.Viewer.
						AdnViewerFactory(
							token,
							config);

			viewerFactory.getViewablePath (urn,

				function(pathInfoCollection) {

					var viewerConfig = {
						qualityLevel: [false, false],
						viewerType: 'GuiViewer3D',
						lightPreset: 0
					};

					$scope.viewer = viewerFactory.
						createViewer(
						  $('#viewerStorageDemoDiv')[0],
						  viewerConfig);

					$scope.viewer.load(
					 pathInfoCollection.path3d[0].path);
				},
				function (error) {

					console.log('Error: ' + error);
				});
		}

		////////////////////////////////////////////////
		// Get token from the gallery (cors request)
		// enabled only for that origin:
		// http://adndevblog.typepad.com
		////////////////////////////////////////////////
		$scope.getGalleryToken = function(onSuccess) {

			var url =
				'http://viewer.autodesk.io/node/gallery/api/token/cors';

			$.ajax({
				url: url,
				method: 'GET',
				success: function(data, state, res){
					onSuccess(data.access_token);
				},
				error: function(data, state){

				  alert('CORS request only allowed for http://adndevblog.typepad.com.' + '\n' +
				  	'Use your own token to test that sample locally...');

				  console.log(
				    'error performing CORS request ...');
				  console.log(data);
				}
			});
		}

		////////////////////////////////////////////////
		//
		//
		////////////////////////////////////////////////
		$scope.getGalleryToken(function(token) {
			$scope.loadURN(
				token,
				"dXJuOmFkc2sub2JqZWN0czpvcy5vYmplY3Q6YWRuLXZpZXdlci1nYWxsZXJ5L2EzZDgtYzgwZS0zMDY3LWI0ZDktM2ZmOC5kd2Z4");
		});
	});
}
else {

 $("#storage-demo-app-div").remove();
}

</script>

<p>&nbsp;</p>


<pre style="line-height: 100%;font-family:monospace;background-color:#ffffff; border-width:0.01mm; border-color:#000000; border-style:solid;padding:4px;font-size:10pt;"><span style="color:#800000;background-color:#f0f0f0;">  1 </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">////////////////////////////////////////////////////////
</span><span style="color:#800000;background-color:#f0f0f0;">  2 </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">// The Angular App
</span><span style="color:#800000;background-color:#f0f0f0;">  3 </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">//
</span><span style="color:#800000;background-color:#f0f0f0;">  4 </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">////////////////////////////////////////////////////////
</span><span style="color:#800000;background-color:#f0f0f0;">  5 </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">var</span><span style="background-color:#ffffff;"> app = angular.module(
</span><span style="color:#800000;background-color:#f0f0f0;">  6 </span><span style="background-color:#ffffff;">        </span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">'Autodesk.ADN.Demo.Storage.App'</span><span style="background-color:#ffffff;">,
</span><span style="color:#800000;background-color:#f0f0f0;">  7 </span><span style="background-color:#ffffff;">    [
</span><span style="color:#800000;background-color:#f0f0f0;">  8 </span><span style="background-color:#ffffff;">        </span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">'js-data'</span><span style="background-color:#ffffff;">,
</span><span style="color:#800000;background-color:#f0f0f0;">  9 </span><span style="background-color:#ffffff;">        </span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">'mgcrea.ngStrap'</span><span style="background-color:#ffffff;">,
</span><span style="color:#800000;background-color:#f0f0f0;"> 10 </span><span style="background-color:#ffffff;">        </span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">'mgcrea.ngStrap.tooltip'</span><span style="background-color:#ffffff;">,
</span><span style="color:#800000;background-color:#f0f0f0;"> 11 </span><span style="background-color:#ffffff;">        </span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">'mgcrea.ngStrap.helpers.parseOptions'
</span><span style="color:#800000;background-color:#f0f0f0;"> 12 </span><span style="background-color:#ffffff;">    ]);
</span><span style="color:#800000;background-color:#f0f0f0;"> 13 
 14 </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">////////////////////////////////////////////////////////
</span><span style="color:#800000;background-color:#f0f0f0;"> 15 </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">// js-data store factory
</span><span style="color:#800000;background-color:#f0f0f0;"> 16 </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">//
</span><span style="color:#800000;background-color:#f0f0f0;"> 17 </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">////////////////////////////////////////////////////////
</span><span style="color:#800000;background-color:#f0f0f0;"> 18 </span><span style="background-color:#ffffff;">app.factory(</span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">'store'</span><span style="background-color:#ffffff;">, </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">function</span><span style="background-color:#ffffff;"> () {
</span><span style="color:#800000;background-color:#f0f0f0;"> 19 
 20 </span><span style="background-color:#ffffff;">        </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">var</span><span style="background-color:#ffffff;"> store = </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">new</span><span style="background-color:#ffffff;"> JSData.DS();
</span><span style="color:#800000;background-color:#f0f0f0;"> 21 
 22 </span><span style="background-color:#ffffff;">        store.registerAdapter(
</span><span style="color:#800000;background-color:#f0f0f0;"> 23 </span><span style="background-color:#ffffff;">                </span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">'localstorage'</span><span style="background-color:#ffffff;">,
</span><span style="color:#800000;background-color:#f0f0f0;"> 24 </span><span style="background-color:#ffffff;">                </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">new</span><span style="background-color:#ffffff;"> DSLocalStorageAdapter(),
</span><span style="color:#800000;background-color:#f0f0f0;"> 25 </span><span style="background-color:#ffffff;">                { </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">default</span><span style="background-color:#ffffff;">: </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">true</span><span style="background-color:#ffffff;"> });
</span><span style="color:#800000;background-color:#f0f0f0;"> 26 
 27 </span><span style="background-color:#ffffff;">        </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">return</span><span style="background-color:#ffffff;"> store;
</span><span style="color:#800000;background-color:#f0f0f0;"> 28 </span><span style="background-color:#ffffff;">    });
</span><span style="color:#800000;background-color:#f0f0f0;"> 29 
 30 </span><span style="background-color:#ffffff;">app.factory(</span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">'StateStore'</span><span style="background-color:#ffffff;">, </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">function</span><span style="background-color:#ffffff;"> (store) {
</span><span style="color:#800000;background-color:#f0f0f0;"> 31 </span><span style="background-color:#ffffff;">        </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">return</span><span style="background-color:#ffffff;"> store.defineResource(</span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">'state'</span><span style="background-color:#ffffff;">);
</span><span style="color:#800000;background-color:#f0f0f0;"> 32 </span><span style="background-color:#ffffff;">    });
</span><span style="color:#800000;background-color:#f0f0f0;"> 33 
 34 </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">////////////////////////////////////////////////////////
</span><span style="color:#800000;background-color:#f0f0f0;"> 35 </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">// the App controller
</span><span style="color:#800000;background-color:#f0f0f0;"> 36 </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">//
</span><span style="color:#800000;background-color:#f0f0f0;"> 37 </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">////////////////////////////////////////////////////////
</span><span style="color:#800000;background-color:#f0f0f0;"> 38 </span><span style="background-color:#ffffff;">app.controller(</span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">'Autodesk.ADN.Demo.Storage.Controller'</span><span style="background-color:#ffffff;">,
</span><span style="color:#800000;background-color:#f0f0f0;"> 39 
 40 </span><span style="background-color:#ffffff;">    </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">function</span><span style="background-color:#ffffff;"> ($scope, $http, $sce, StateStore) {
</span><span style="color:#800000;background-color:#f0f0f0;"> 41 
 42 </span><span style="background-color:#ffffff;">        </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">////////////////////////////////////////////////
</span><span style="color:#800000;background-color:#f0f0f0;"> 43 </span><span style="background-color:#ffffff;">        </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">// $scope members
</span><span style="color:#800000;background-color:#f0f0f0;"> 44 </span><span style="background-color:#ffffff;">        </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">//
</span><span style="color:#800000;background-color:#f0f0f0;"> 45 </span><span style="background-color:#ffffff;">        </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">////////////////////////////////////////////////
</span><span style="color:#800000;background-color:#f0f0f0;"> 46 </span><span style="background-color:#ffffff;">        $scope.items = [];
</span><span style="color:#800000;background-color:#f0f0f0;"> 47 
 48 </span><span style="background-color:#ffffff;">        $scope.selectedItem = </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">null</span><span style="background-color:#ffffff;">;
</span><span style="color:#800000;background-color:#f0f0f0;"> 49 
 50 </span><span style="background-color:#ffffff;">        </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">////////////////////////////////////////////////
</span><span style="color:#800000;background-color:#f0f0f0;"> 51 </span><span style="background-color:#ffffff;">        </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">// retrieve all stored states
</span><span style="color:#800000;background-color:#f0f0f0;"> 52 </span><span style="background-color:#ffffff;">        </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">//
</span><span style="color:#800000;background-color:#f0f0f0;"> 53 </span><span style="background-color:#ffffff;">        </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">////////////////////////////////////////////////
</span><span style="color:#800000;background-color:#f0f0f0;"> 54 </span><span style="background-color:#ffffff;">        StateStore.findAll().then(</span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">function</span><span style="background-color:#ffffff;"> (states) {
</span><span style="color:#800000;background-color:#f0f0f0;"> 55 
 56 </span><span style="background-color:#ffffff;">            states.forEach(</span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">function</span><span style="background-color:#ffffff;">(state) {
</span><span style="color:#800000;background-color:#f0f0f0;"> 57 
 58 </span><span style="background-color:#ffffff;">                $scope.items.push({
</span><span style="color:#800000;background-color:#f0f0f0;"> 59 </span><span style="background-color:#ffffff;">                    value: state.id,
</span><span style="color:#800000;background-color:#f0f0f0;"> 60 </span><span style="background-color:#ffffff;">                    label: $sce.trustAsHtml(state.name)
</span><span style="color:#800000;background-color:#f0f0f0;"> 61 </span><span style="background-color:#ffffff;">                });
</span><span style="color:#800000;background-color:#f0f0f0;"> 62 </span><span style="background-color:#ffffff;">            });
</span><span style="color:#800000;background-color:#f0f0f0;"> 63 
 64 </span><span style="background-color:#ffffff;">            console.log(states);
</span><span style="color:#800000;background-color:#f0f0f0;"> 65 </span><span style="background-color:#ffffff;">        });
</span><span style="color:#800000;background-color:#f0f0f0;"> 66 
 67 </span><span style="background-color:#ffffff;">        </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">////////////////////////////////////////////////
</span><span style="color:#800000;background-color:#f0f0f0;"> 68 </span><span style="background-color:#ffffff;">        </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">// AddState callback
</span><span style="color:#800000;background-color:#f0f0f0;"> 69 </span><span style="background-color:#ffffff;">        </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">//
</span><span style="color:#800000;background-color:#f0f0f0;"> 70 </span><span style="background-color:#ffffff;">        </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">////////////////////////////////////////////////
</span><span style="color:#800000;background-color:#f0f0f0;"> 71 </span><span style="background-color:#ffffff;">        $scope.onAddState = </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">function</span><span style="background-color:#ffffff;">() {
</span><span style="color:#800000;background-color:#f0f0f0;"> 72 
 73 </span><span style="background-color:#ffffff;">            </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">var</span><span style="background-color:#ffffff;"> data = $scope.viewer.getState();
</span><span style="color:#800000;background-color:#f0f0f0;"> 74 
 75 </span><span style="background-color:#ffffff;">            </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">var</span><span style="background-color:#ffffff;"> name = </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">new</span><span style="background-color:#ffffff;"> Date().toString(
</span><span style="color:#800000;background-color:#f0f0f0;"> 76 </span><span style="background-color:#ffffff;">                </span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">'d/M/yyyy H:mm:ss'</span><span style="background-color:#ffffff;">);
</span><span style="color:#800000;background-color:#f0f0f0;"> 77 
 78 </span><span style="background-color:#ffffff;">            </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">var</span><span style="background-color:#ffffff;"> stateData = {
</span><span style="color:#800000;background-color:#f0f0f0;"> 79 </span><span style="background-color:#ffffff;">                name: name,
</span><span style="color:#800000;background-color:#f0f0f0;"> 80 </span><span style="background-color:#ffffff;">                data: data
</span><span style="color:#800000;background-color:#f0f0f0;"> 81 </span><span style="background-color:#ffffff;">            }
</span><span style="color:#800000;background-color:#f0f0f0;"> 82 
 83 </span><span style="background-color:#ffffff;">            StateStore.create(stateData)
</span><span style="color:#800000;background-color:#f0f0f0;"> 84 
 85 </span><span style="background-color:#ffffff;">                .then(</span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">function</span><span style="background-color:#ffffff;">(state) {
</span><span style="color:#800000;background-color:#f0f0f0;"> 86 
 87 </span><span style="background-color:#ffffff;">                    $scope.items.push({
</span><span style="color:#800000;background-color:#f0f0f0;"> 88 </span><span style="background-color:#ffffff;">                        value: state.id,
</span><span style="color:#800000;background-color:#f0f0f0;"> 89 </span><span style="background-color:#ffffff;">                        label: $sce.trustAsHtml(name)
</span><span style="color:#800000;background-color:#f0f0f0;"> 90 </span><span style="background-color:#ffffff;">                    });
</span><span style="color:#800000;background-color:#f0f0f0;"> 91 </span><span style="background-color:#ffffff;">                });
</span><span style="color:#800000;background-color:#f0f0f0;"> 92 </span><span style="background-color:#ffffff;">        }
</span><span style="color:#800000;background-color:#f0f0f0;"> 93 
 94 </span><span style="background-color:#ffffff;">        </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">////////////////////////////////////////////////
</span><span style="color:#800000;background-color:#f0f0f0;"> 95 </span><span style="background-color:#ffffff;">        </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">// ClearStates callback
</span><span style="color:#800000;background-color:#f0f0f0;"> 96 </span><span style="background-color:#ffffff;">        </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">//
</span><span style="color:#800000;background-color:#f0f0f0;"> 97 </span><span style="background-color:#ffffff;">        </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">////////////////////////////////////////////////
</span><span style="color:#800000;background-color:#f0f0f0;"> 98 </span><span style="background-color:#ffffff;">        $scope.onClearStates = </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">function</span><span style="background-color:#ffffff;">() {
</span><span style="color:#800000;background-color:#f0f0f0;"> 99 
100 </span><span style="background-color:#ffffff;">            $scope.items = [];
</span><span style="color:#800000;background-color:#f0f0f0;">101 
102 </span><span style="background-color:#ffffff;">            StateStore.destroyAll();
</span><span style="color:#800000;background-color:#f0f0f0;">103 </span><span style="background-color:#ffffff;">        }
</span><span style="color:#800000;background-color:#f0f0f0;">104 
105 </span><span style="background-color:#ffffff;">        </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">////////////////////////////////////////////////
</span><span style="color:#800000;background-color:#f0f0f0;">106 </span><span style="background-color:#ffffff;">        </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">// watch selectedItem
</span><span style="color:#800000;background-color:#f0f0f0;">107 </span><span style="background-color:#ffffff;">        </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">//
</span><span style="color:#800000;background-color:#f0f0f0;">108 </span><span style="background-color:#ffffff;">        </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">////////////////////////////////////////////////
</span><span style="color:#800000;background-color:#f0f0f0;">109 </span><span style="background-color:#ffffff;">        $scope.$watch(</span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">'selectedItem'</span><span style="background-color:#ffffff;">, </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">function</span><span style="background-color:#ffffff;">() {
</span><span style="color:#800000;background-color:#f0f0f0;">110 
111 </span><span style="background-color:#ffffff;">            </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">if</span><span style="background-color:#ffffff;">($scope.selectedItem !== </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">null</span><span style="background-color:#ffffff;">) {
</span><span style="color:#800000;background-color:#f0f0f0;">112 
113 </span><span style="background-color:#ffffff;">                </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">var</span><span style="background-color:#ffffff;"> state = StateStore.get(
</span><span style="color:#800000;background-color:#f0f0f0;">114 </span><span style="background-color:#ffffff;">                    $scope.selectedItem);
</span><span style="color:#800000;background-color:#f0f0f0;">115 
116 </span><span style="background-color:#ffffff;">                $scope.viewer.restoreState(state.data);
</span><span style="color:#800000;background-color:#f0f0f0;">117 </span><span style="background-color:#ffffff;">            }
</span><span style="color:#800000;background-color:#f0f0f0;">118 </span><span style="background-color:#ffffff;">        });
</span><span style="color:#800000;background-color:#f0f0f0;">119 
120 </span><span style="background-color:#ffffff;">        </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">////////////////////////////////////////////////
</span><span style="color:#800000;background-color:#f0f0f0;">121 </span><span style="background-color:#ffffff;">        </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">// Load urn with token
</span><span style="color:#800000;background-color:#f0f0f0;">122 </span><span style="background-color:#ffffff;">        </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">//
</span><span style="color:#800000;background-color:#f0f0f0;">123 </span><span style="background-color:#ffffff;">        </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">////////////////////////////////////////////////
</span><span style="color:#800000;background-color:#f0f0f0;">124 </span><span style="background-color:#ffffff;">        $scope.loadURN = </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">function</span><span style="background-color:#ffffff;">(token, urn) {
</span><span style="color:#800000;background-color:#f0f0f0;">125 
126 </span><span style="background-color:#ffffff;">            </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">var</span><span style="background-color:#ffffff;"> config = {
</span><span style="color:#800000;background-color:#f0f0f0;">127 </span><span style="background-color:#ffffff;">                environment : </span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">'AutodeskProduction'
</span><span style="color:#800000;background-color:#f0f0f0;">128 </span><span style="background-color:#ffffff;">            }
</span><span style="color:#800000;background-color:#f0f0f0;">129 
130 </span><span style="background-color:#ffffff;">            </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">var</span><span style="background-color:#ffffff;"> viewerFactory =
</span><span style="color:#800000;background-color:#f0f0f0;">131 </span><span style="background-color:#ffffff;">                </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">new</span><span style="background-color:#ffffff;"> Autodesk.ADN.Toolkit.Viewer.
</span><span style="color:#800000;background-color:#f0f0f0;">132 </span><span style="background-color:#ffffff;">                        AdnViewerFactory(
</span><span style="color:#800000;background-color:#f0f0f0;">133 </span><span style="background-color:#ffffff;">                            token,
</span><span style="color:#800000;background-color:#f0f0f0;">134 </span><span style="background-color:#ffffff;">                            config);
</span><span style="color:#800000;background-color:#f0f0f0;">135 
136 </span><span style="background-color:#ffffff;">            viewerFactory.getViewablePath (urn,
</span><span style="color:#800000;background-color:#f0f0f0;">137 
138 </span><span style="background-color:#ffffff;">                </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">function</span><span style="background-color:#ffffff;">(pathInfoCollection) {
</span><span style="color:#800000;background-color:#f0f0f0;">139 
140 </span><span style="background-color:#ffffff;">                    </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">var</span><span style="background-color:#ffffff;"> viewerConfig = {
</span><span style="color:#800000;background-color:#f0f0f0;">141 </span><span style="background-color:#ffffff;">                        qualityLevel: [</span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">false</span><span style="background-color:#ffffff;">, </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">false</span><span style="background-color:#ffffff;">],
</span><span style="color:#800000;background-color:#f0f0f0;">142 </span><span style="background-color:#ffffff;">                        viewerType: </span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">'GuiViewer3D'</span><span style="background-color:#ffffff;">,
</span><span style="color:#800000;background-color:#f0f0f0;">143 </span><span style="background-color:#ffffff;">                        lightPreset: </span><span style="color:#0000ff;background-color:#ffffff;">0
</span><span style="color:#800000;background-color:#f0f0f0;">144 </span><span style="background-color:#ffffff;">                    };
</span><span style="color:#800000;background-color:#f0f0f0;">145 
146 </span><span style="background-color:#ffffff;">                    $scope.viewer = viewerFactory.
</span><span style="color:#800000;background-color:#f0f0f0;">147 </span><span style="background-color:#ffffff;">                        createViewer(
</span><span style="color:#800000;background-color:#f0f0f0;">148 </span><span style="background-color:#ffffff;">                          $(</span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">'#viewerStorageDemoDiv'</span><span style="background-color:#ffffff;">)[</span><span style="color:#0000ff;background-color:#ffffff;">0</span><span style="background-color:#ffffff;">],
</span><span style="color:#800000;background-color:#f0f0f0;">149 </span><span style="background-color:#ffffff;">                          viewerConfig);
</span><span style="color:#800000;background-color:#f0f0f0;">150 
151 </span><span style="background-color:#ffffff;">                    $scope.viewer.load(
</span><span style="color:#800000;background-color:#f0f0f0;">152 </span><span style="background-color:#ffffff;">                     pathInfoCollection.path3d[</span><span style="color:#0000ff;background-color:#ffffff;">0</span><span style="background-color:#ffffff;">].path);
</span><span style="color:#800000;background-color:#f0f0f0;">153 </span><span style="background-color:#ffffff;">                },
</span><span style="color:#800000;background-color:#f0f0f0;">154 </span><span style="background-color:#ffffff;">                </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">function</span><span style="background-color:#ffffff;"> (error) {
</span><span style="color:#800000;background-color:#f0f0f0;">155 
156 </span><span style="background-color:#ffffff;">                    console.log(</span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">'Error: '</span><span style="background-color:#ffffff;"> + error);
</span><span style="color:#800000;background-color:#f0f0f0;">157 </span><span style="background-color:#ffffff;">                });
</span><span style="color:#800000;background-color:#f0f0f0;">158 </span><span style="background-color:#ffffff;">        }
</span><span style="color:#800000;background-color:#f0f0f0;">159 
160 </span><span style="background-color:#ffffff;">        </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">////////////////////////////////////////////////
</span><span style="color:#800000;background-color:#f0f0f0;">161 </span><span style="background-color:#ffffff;">        </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">// Get token from the gallery (cors request)
</span><span style="color:#800000;background-color:#f0f0f0;">162 </span><span style="background-color:#ffffff;">        </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">// enabled only for that origin:
</span><span style="color:#800000;background-color:#f0f0f0;">163 </span><span style="background-color:#ffffff;">        </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">// http://adndevblog.typepad.com
</span><span style="color:#800000;background-color:#f0f0f0;">164 </span><span style="background-color:#ffffff;">        </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">////////////////////////////////////////////////
</span><span style="color:#800000;background-color:#f0f0f0;">165 </span><span style="background-color:#ffffff;">        $scope.getGalleryToken = </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">function</span><span style="background-color:#ffffff;">(onSuccess) {
</span><span style="color:#800000;background-color:#f0f0f0;">166 
167 </span><span style="background-color:#ffffff;">            </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">var</span><span style="background-color:#ffffff;"> url =
</span><span style="color:#800000;background-color:#f0f0f0;">168 </span><span style="background-color:#ffffff;">                </span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">'http://gallery.autodesk.io/api/tokenx'</span><span style="background-color:#ffffff;">;
</span><span style="color:#800000;background-color:#f0f0f0;">169 
170 </span><span style="background-color:#ffffff;">            $.ajax({
</span><span style="color:#800000;background-color:#f0f0f0;">171 </span><span style="background-color:#ffffff;">                url: url,
</span><span style="color:#800000;background-color:#f0f0f0;">172 </span><span style="background-color:#ffffff;">                method: </span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">'GET'</span><span style="background-color:#ffffff;">,
</span><span style="color:#800000;background-color:#f0f0f0;">173 </span><span style="background-color:#ffffff;">                success: </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">function</span><span style="background-color:#ffffff;">(data, state, res){
</span><span style="color:#800000;background-color:#f0f0f0;">174 </span><span style="background-color:#ffffff;">                    onSuccess(data.access_token);
</span><span style="color:#800000;background-color:#f0f0f0;">175 </span><span style="background-color:#ffffff;">                },
</span><span style="color:#800000;background-color:#f0f0f0;">176 </span><span style="background-color:#ffffff;">                error: </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">function</span><span style="background-color:#ffffff;">(data, state){
</span><span style="color:#800000;background-color:#f0f0f0;">177 </span><span style="background-color:#ffffff;">                  console.log(
</span><span style="color:#800000;background-color:#f0f0f0;">178 </span><span style="background-color:#ffffff;">                    </span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">'error performing CORS request...'</span><span style="background-color:#ffffff;">);
</span><span style="color:#800000;background-color:#f0f0f0;">179 </span><span style="background-color:#ffffff;">                  console.log(data);
</span><span style="color:#800000;background-color:#f0f0f0;">180 </span><span style="background-color:#ffffff;">                }
</span><span style="color:#800000;background-color:#f0f0f0;">181 </span><span style="background-color:#ffffff;">            });
</span><span style="color:#800000;background-color:#f0f0f0;">182 </span><span style="background-color:#ffffff;">        }
</span><span style="color:#800000;background-color:#f0f0f0;">183 
184 </span><span style="background-color:#ffffff;">        </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">////////////////////////////////////////////////
</span><span style="color:#800000;background-color:#f0f0f0;">185 </span><span style="background-color:#ffffff;">        </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">//
</span><span style="color:#800000;background-color:#f0f0f0;">186 </span><span style="background-color:#ffffff;">        </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">//
</span><span style="color:#800000;background-color:#f0f0f0;">187 </span><span style="background-color:#ffffff;">        </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">////////////////////////////////////////////////
</span><span style="color:#800000;background-color:#f0f0f0;">188 </span><span style="background-color:#ffffff;">        $scope.getGalleryToken(</span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">function</span><span style="background-color:#ffffff;">(token) {
</span><span style="color:#800000;background-color:#f0f0f0;">189 </span><span style="background-color:#ffffff;">            $scope.loadURN(
</span><span style="color:#800000;background-color:#f0f0f0;">190 </span><span style="background-color:#ffffff;">                token,
</span><span style="color:#800000;background-color:#f0f0f0;">191 </span><span style="background-color:#ffffff;">                </span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">"... urn ..."</span><span style="background-color:#ffffff;">);
</span><span style="color:#800000;background-color:#f0f0f0;">192 </span><span style="background-color:#ffffff;">        });
</span><span style="color:#800000;background-color:#f0f0f0;">193 </span><span style="background-color:#ffffff;">    });</span></pre>
<br>
You can download the complete example from the attachment below:
<br>


<span class="asset  asset-generic at-xid-6a0167607c2431970b01b7c7752e55970b img-responsive"><a href="http://adndevblog.typepad.com/files/storage-demo-2.zip">Storage-demo</a></span>
