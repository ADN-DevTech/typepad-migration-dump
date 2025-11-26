---
layout: "post"
title: "First steps with React & Flux"
date: "2015-04-07 14:36:34"
author: "Philippe Leefsma"
categories:
  - "Cloud"
  - "HTML5"
  - "Javascript"
  - "Philippe Leefsma"
  - "View and Data API"
original_url: "https://adndevblog.typepad.com/cloud_and_mobile/2015/04/first-steps-with-react-flux.html "
typepad_basename: "first-steps-with-react-flux"
typepad_status: "Publish"
---

<p>By&nbsp;<a href="http://adndevblog.typepad.com/cloud_and_mobile/philippe-leefsma.html">Philippe Leefsma</a></p>
<p>Let's face it, <a href="https://facebook.github.io/react/" target="_self">React</a> is all over the web by now! The UI library from Facebook seems to be pretty popular among web developers, so I thought I would give it a try rather sooner than later...</p>
<p>The React library in itself lets you write pure UI components, but order to play with them you need a web app. Those components can be efficiently leveraged by an architecture called <a href="https://facebook.github.io/flux/" target="_self">Flux</a>, a concept based on the idea that, in a web app, data flows in a unique direction, hence introducing the notion of a central dispatcher, stores keeping the application states and views displaying those states.</p>
<p>Well, I'm not intending to give you an introduction to React and Flux, you will easily find plenty on the web! I rather wanted to share a sample I've put together using <a href="https://reactjsnews.com/the-state-of-flux/" target="_self">one of the Flux implementation</a>: <a href="http://deloreanjs.com/" target="_self">DeLorean.js</a></p>
<p>The sample is slightly derived from DeLorean basic overview sample and tweaked to use a custom React component that will fetch random models from our View &amp; Data <a href="http://gallery.autodesk.io" target="_self">Gallery</a>. You can add some more models using the "Add Model" button and clicking on an item will&nbsp; open that model in a new tab. Full sample code embedded at the bottom ...</p>

Want to know more about React? This resource might come in handy: 

<a href="https://github.com/enaqx/awesome-react">https://github.com/enaqx/awesome-react</a>

<p>&nbsp;</p>

<link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.0/css/bootstrap.min.css">

<style>

    .panel {
        width: 480px;
    }

    .panel-heading.adn-panel {
        background-color: #337ab7;
    }

</style>

<div class="panel panel-default">
    <div class="panel-heading adn-panel">
        <h4 class="panel-title">Gallery Models</h4>
    </div>

    <div class="panel-body">
        <div id="modelList"></div>
    </div>
</div>

<div style="margin-left: 200px">
    <button type="button" id="addItem" class="btn btn-default">
        <span class="glyphicon glyphicon-plus" aria-hidden="true">
        </span>
        Add Model
    </button>
</div>

<script src="http://fb.me/react-0.13.1.js"></script>
<script src="http://fb.me/JSXTransformer-0.13.1.js"></script>
<script src="http://code.jquery.com/jquery-2.1.3.min.js"></script>
<script src="http://rawgit.com/f/delorean/master/dist/delorean.min.js"></script>

<script type="text/jsx">

    ///////////////////////////////////////////////////////
    // DeLorean Flux Instance
    //
    ///////////////////////////////////////////////////////
    var Flux = DeLorean.Flux;

    ///////////////////////////////////////////////////////
    // Creates a store
    //
    ///////////////////////////////////////////////////////
    var store = DeLorean.Flux.createStore({

        items: [],

        actions: {
            'addItem': 'addItemMethod'
        },

        addItemMethod: function (item) {

            this.items.push(item);

            // You need to say your store has changed.
            this.emit('change');
        },

        getState: function () {
            return {
                items: this.items
            };
        }
    });

    ///////////////////////////////////////////////////////
    // The App Dispatcher
    //
    ///////////////////////////////////////////////////////
    var AppDispatcher = DeLorean.Flux.createDispatcher({

        addItem: function (data) {
            this.dispatch('addItem', data);
        },

        getStores: function () {
            return {
                ItemsStore: store
            }
        }
    });

    ///////////////////////////////////////////////////////
    // The Action Creator
    //
    ///////////////////////////////////////////////////////
    var ActionCreator = {

        addItem: function () {

            getGalleryModelCount(function(count) {

                var idx = randomInt(0, count-1);

                getGalleryModel(idx, function(model) {

                    getModelThumbnail(model._id, function(thumbnail) {

                        AppDispatcher.addItem({
                            key: newGUID(),
                            model: model,
                            thumbnail: 'data:image/png;base64,' + thumbnail
                        });
                    });
                });
            });
        }
    };

    ///////////////////////////////////////////////////////
    // The React Component: a Bootstrap list of models
    //
    ///////////////////////////////////////////////////////
    var ModelList = React.createClass({

        getInitialState: function () {
            return {
                items: []
            };
        },

        componentDidMount: function() {
            store.onChange(this._onChange);
        },

        componentWillUnmount: function() {

        },

        render: function() {

            return (

                <div className="list-group">
                    {
                        //Maps each item to a Boostrap list-group-item
                        this.state.items.map(function(item) {

                        // clicking on a model will open the model in a new tab
                        // using Gallery embed feature
                        var embedUrl =
                            'http://viewer.autodesk.io/node/gallery/embed/' +
                            item.model._id;

                        return (
                            <a key={item.key} href={embedUrl} target="_blank" className="list-group-item">
                                <div className='row'>
                                    <img src={item.thumbnail} width='100' height='100' className='col-md-4'></img>
                                    <p className='list-group-item-text col-md-8'>
                                        {item.model.name}
                                    </p>
                                </div>
                            </a>
                           );
                    })}
                </div>);
        },

        _onChange: function () {

            var storeState = store.getState();

            this.setState({

                items: storeState.items
            });
        }
    });

    ///////////////////////////////////////////////////////
    // Utils function: returns int between [min, max]
    //
    ///////////////////////////////////////////////////////
    function randomInt(min, max) {
        return Math.floor(Math.random() * (max - min)) + min;
    }

    ///////////////////////////////////////////////////////
    //  Utils function: returns number of Gallery models
    //
    ///////////////////////////////////////////////////////
    function getGalleryModelCount(onSuccess) {

        var url = 'http://gallery.autodesk.io/api/modelcount';

        $.ajax({
            url: url,
            dataType: 'jsonp',
            success: function(response){
                onSuccess(response.count);
            },
            error: function(error){
                console.log('error performing request...');
                console.log(error);
            }
        });
    }

    ///////////////////////////////////////////////////////
    //  Utils function: returns Gallery model from index
    //
    ///////////////////////////////////////////////////////
    function getGalleryModel(index, onSuccess) {

        // use Gallery REST API to pick up one model
        var url = 'http://gallery.autodesk.io/api/models?skip='
                + index + '&limit=1';

        $.ajax({
            url: url,
            dataType: 'jsonp',
            success: function(response){
                onSuccess(response.models[0]);
            },
            error: function(error){
                console.log('error performing request...');
                console.log(error);
            }
        });
    }

    ///////////////////////////////////////////////////////
    //  Utils function: returns model thumbnail base64
    //
    ///////////////////////////////////////////////////////
    function getModelThumbnail(modelId, onSuccess) {

        var url = 'http://gallery.autodesk.io/api/thumbnail/'
            + modelId;

        $.ajax({
            url: url,
            dataType: 'jsonp',
            success: function(response){
                onSuccess(response.thumbnail.data);
            },
            error: function(error){
                console.log('error performing request...');
                console.log(error);
            }
        });
    }

    ///////////////////////////////////////////////////////
    //  Utils function: returns a random guid
    //
    ///////////////////////////////////////////////////////
    function newGUID() {

        var d = new Date().getTime();

        var guid = 'xxxx-xxxx-xxxx-xxxx-xxxx'.replace(
                /[xy]/g,
                function (c) {
                    var r = (d + Math.random() * 16) % 16 | 0;
                    d = Math.floor(d / 16);
                    return (c == 'x' ? r : (r & 0x7 | 0x8)).toString(16);
                });

        return guid;
    };

    ///////////////////////////////////////////////////////
    // OnClick handler
    //
    ///////////////////////////////////////////////////////
    document.getElementById('addItem').onclick =
        function () {
            ActionCreator.addItem();
        };

    ///////////////////////////////////////////////////////
    // Render our React component
    //
    ///////////////////////////////////////////////////////
    React.render(<ModelList dispatcher={AppDispatcher} />,
        document.getElementById('modelList'));

    ///////////////////////////////////////////////////////
    // Fetches 3 items by default ...
    //
    ///////////////////////////////////////////////////////
    ActionCreator.addItem();
    ActionCreator.addItem();
    ActionCreator.addItem();

</script>

<p>&nbsp;</p>

<script src="https://gist.github.com/leefsmp/ade140b11ed74401d812.js"></script>
