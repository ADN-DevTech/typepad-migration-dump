---
layout: "post"
title: "Animated radial menu with css & jquery"
date: "2015-11-13 05:18:18"
author: "Philippe Leefsma"
categories:
  - "HTML5"
  - "Philippe Leefsma"
  - "View and Data API"
original_url: "https://adndevblog.typepad.com/cloud_and_mobile/2015/11/animated-radial-menu-with-css-jquery.html "
typepad_basename: "animated-radial-menu-with-css-jquery"
typepad_status: "Publish"
---

<p>By&nbsp;<a href="http://adndevblog.typepad.com/cloud_and_mobile/philippe-leefsma.html">Philippe Leefsma</a></p>
<p>Here is a quickie before the weekend. I actually stole the original version from one of Cyrille's recent project. That's an animated radial menu using css and jquery. Click the icon to toggle visibility of the menu items:&nbsp;</p>
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/font-awesome/4.4.0/css/font-awesome.min.css">

<style>

.menu-selector {
    position: absolute;
    width: 140px;
    height: 140px;
    top: 168px;
    left:168px;
}

.menu-selector,
.menu-selector button {
    font-family: 'Oswald', sans-serif;
    font-weight: 300;
}

.menu-selector button {
    position: relative;
    width: 100%;
    height: 100%;
    padding: 10px;
    background: #428bca;
    border-radius: 50%;
    border: 0;
    color: white;
    font-size: 20px;
    cursor: pointer;
    transition: all .1s;
	pointer-events: auto;
}

.menu-selector button:hover {
	background: #3071a9;
}

.menu-selector button:focus {
	outline: none;
}

.menu-selector ul {
    position: absolute;
    list-style: none;
    padding: 0;
    margin: 0;
    top: -20px;
    right: -20px;
    bottom: -20px;
    left: -20px;
    pointer-events: none;
}

.menu-selector li {
    position: absolute;
    width: 100%;
    height: 100%;
    margin: 0 50%;
    -webkit-transform: rotate(-360deg);
    transition: all 0.8s ease-in-out;
}

.menu-selector li input {
	display: none;
}

.menu-selector li input + label {
    position: absolute;
    left: 50%;
    bottom: 100%;
    width: 0;
    height: 0;
    line-height: 1px;
    margin-left: 0;
    background: #fff;
    border-radius: 50%;
    text-align: center;
    font-size: 1px;
    overflow: hidden;
    cursor: pointer;
    box-shadow: none;
    transition: all 0.8s ease-in-out, color 0.1s, background 0.1s;
    pointer-events: auto;
}

.menu-selector li input + label {
    background: #86d2ff;
}

.menu-selector li input + label:hover {
	background: #A6DA7F;
}

.menu-selector li input:checked + label {
    background: #5cb85c;
    color: white;
}

.menu-selector li input:checked + label:hover {
	background: #449d44;
}

.menu-selector.open li input + label {
    width: 80px;
    height: 80px;
    line-height: 80px;
    margin-left: -40px;
    box-shadow: 0 3px 3px rgba(0, 0, 0, 0.1);
    font-size: 14px;
}

/* For Viewer */
.menu-container .menu-selector {
    width: 32px;
    height: 32px;
    z-index: 5;
}

.menu-container .menu-selector ul {
    top: -80px;
    right: -80px;
    bottom: -80px;
    left: -80px;
    pointer-events: none ;
}

.menu-container .menu-selector li {
    margin: 0 auto;
}

.menu-container .menu-selector button {
    width: 32px;
    height: 32px;
    background: transparent;
    padding: 0px;
}

</style>

<div id="menu-container" style="height: 370px; width: 370px; background-color: rgba(24, 153, 140, 0.23)"></div>

<script src="http://code.jquery.com/jquery-2.1.3.min.js"></script>

<script>

function guid() {

    var d = new Date().getTime();

    var guid = 'xxxx-xxxx-xxxx-xxxx'.replace(
      /[xy]/g,
      function (c) {
          var r = (d + Math.random() * 16) % 16 | 0;
          d = Math.floor(d / 16);
          return (c == 'x' ? r : (r & 0x7 | 0x8)).toString(16);
      });

    return guid;
}

function createRadialMenu(menuItems, parent, imgSrc) {

    var $parent = $(parent);

    var selectorId = guid();
    var triggerId = guid();

    var itemsId = guid();

    var menuId = guid();

    var html = [

        '<div class="menu-container" id="' + menuId + '">',
            '<div id="' + selectorId + '" class="menu-selector"> ',
            '<ul id="' + itemsId + '">',
            '</ul>',
            '<button id="' + triggerId + '">' +
              '<img width="32" height="32" src="' + imgSrc + '"/>',
            '</button>',
            '</div>',
        '</div>'
    ];

    $parent.append(html.join('\n'));

    var $selector = $('#' + selectorId);

    $('#' + triggerId).click(function() {

console.log('toggle');

        var angleStart =-360 ;

        function rotate (li, d) {

            $({ d: angleStart }).animate ({ d: d }, {
                step: function (now) {
                    $(li)
                      .css ({ transform: 'rotate(' + now + 'deg)' })
                      .find ('label')
                      .css ({ transform: 'rotate(' + (-now) + 'deg)' }) ;
                },
                duration: 0
            }) ;
        }

        $selector.toggleClass('open');

        var li = $selector.find('li');

        var deg = $selector.hasClass ('half') ? 180 / (li.length - 1) : 360 / li.length;

        for ( var i =0 ; i < li.length ; i++ ) {

            var d = $selector.hasClass('half') ? (i * deg) - 90 : i * deg;

            $selector.hasClass('open') ? rotate (li[i], d) : rotate (li[i], angleStart) ;
        }
    });

    var $menu = $('#' + menuId);

    $menu.css({
        'background-color':'transparent',
        'height': $parent.outerHeight(),
        'width': $parent.outerWidth(),
        'pointer-events':'none',
        'position':'relative',
        'left': '0px',
        'top': '0px'
    });

    var $items = $('#' + itemsId);

    menuItems.forEach(function(menuItem) {

        var itemId = guid();

        var itemHtml = [

            '<li id="' + itemId + '">',
                '<input type="checkbox">',
                '<label class="' + menuItem.class + '" id="' + itemId + '"> ' +
                    menuItem.text +
                '</label>',
            '</li>'
        ];

        $items.append(itemHtml.join('\n'));

        $('#' + itemId).click(function(){

            menuItem.onClick(this);
        });
    });

    return $selector;
};

function onload() {

    var $menu = createRadialMenu(
      [
          {
              text: 'Item 1',
              class: 'fa fa-play',
              onClick: function(label) {
                 alert('Item 1!');
              }
          },
          {
              text: 'Item 2',
              class: 'fa fa-stop',
              onClick: function(label) {
                  alert('Item 2!');
              }
          },
          {
              text: 'Item 3',
              class: 'fa fa-pause',
              onClick: function(label) {
                  alert('Item 3!');
              }
          },
          {
              text: 'Item 4',
              class: 'fa fa-random',
              onClick: function(label) {
                  alert('Item 4!');
              }
          },
          {
              text: 'Item 5',
              class: 'fa fa-backward',
              onClick: function(label) {
                  alert('Item 5!');
              }
          },
          {
              text: 'Item 6',
              class: 'fa fa-forward',
              onClick: function(label) {
                  alert('Item 6!');
              }
          },
      ],
      document.getElementById('menu-container'),
      'http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d175c675970c-320wi');

var container = $('#menu-container');

/*$menu.css({
'top': container.position().top ,
'left':container.position().left
});*/


}

$( document ).ready(onload);

</script>


<a class="asset-img-link"  style="float: left;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d175c675970c-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b01b8d175c675970c img-responsive" alt="Radial menu" title="Radial menu" src="/assets/image_fb2b14.jpg" style="display:none" /></a>

<br>

You can download the source from there: 
<span class="asset  asset-generic at-xid-6a0167607c2431970b01bb0890243b970d img-responsive"><a href="http://adndevblog.typepad.com/files/radial-menu.zip">radial menu.zip</a></span>

<br />
I also turned it into a viewer extension: select a component and the menu will stick to the hit point when you move the camera. You can test a live version <a href="http://viewer.autodesk.io/node/gallery/embed?id=560c6c57611ca14810e1b2bf&extIds=Autodesk.ADN.Viewing.Extension.RadialMenu" target="_blank">here</a>.

<br />
<br />

<a class="asset-img-link"  style="display: inline;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb089029b4970d-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a0167607c2431970b01bb089029b4970d img-responsive" alt="Screen Shot 2015-11-13 at 15.14.18" title="Screen Shot 2015-11-13 at 15.14.18" src="/assets/image_eca3c9.jpg" /></a><br />

<script src="https://gist.github.com/leefsmp/8138a2df3c7a774cfdbf.js"></script>
