---
layout: "post"
title: "Room Editor with Handlebars and Refresh"
date: "2014-05-31 05:00:00"
author: "Jeremy Tammik"
categories:
  - "Cloud"
  - "Git"
  - "HTML"
  - "JavaScript"
original_url: "https://thebuildingcoder.typepad.com/blog/2014/05/room-editor-with-handlebars-and-refresh.html "
typepad_basename: "room-editor-with-handlebars-and-refresh"
typepad_status: "Publish"
---

<script src="https://thebuildingcoder.typepad.com/google-code-prettify/run_prettify.js"></script>

<p>Somehow, I have a much harder time documenting my JavaScript exploits than my Revit API ones.</p>

<p>The Autodesk Tech Summit is taking place in Toronto next week, and I am making those potentially disastrous last minute changes that every sane person avoids at all costs.</p>

<a name="2"></a>

<h4>Automatically Refresh on Save</h4>

<p>One really important thing that I fixed now required just one single line of code:</p>

<p>The browser display of the model is automatically refreshed when you press 'Save'.</p>

<p>I remember wondering why this did not happen when I originally implemented it last year, and finally giving up, simply resorting to an additional manual button click to trigger the refresh.</p>

<p>Pondering my simple solution to the

<a href="http://thebuildingcoder.typepad.com/blog/2014/05/room-editor-element-properties-and-the-async-trap.html#2">async trap</a> that

I fell into saving element properties, I discovered that the issue here is the exact same thing.</p>

<p>I need to do whatever needs to be done inside the database callback function and nowhere else.</p>

<p>If I add other code after initiating the database call, that code will interfere with and corrupt the database interaction.</p>

<p>So now my save method looks like this:</p>

<pre class="prettyprint">
function save(a) {
  for( var i = 0; i < a.length; ++i ) {
    var f = a[i];
    var id = f.data("jid");

    var txy = f.transform().toString()
      .substring(1).split(/[,r]/);

    var trxy = 'R' + f.data('angle')
      + 'T' + txy[0] + "," + txy[1];

    var fdoc = f.data("doc");

    fdoc.transform = trxy;

    if( fdoc.hasOwnProperty('loop') ) {
      delete fdoc.loop;
    }
    db.saveDoc( fdoc,
      function (err, data) {
        if (err) {
          console.log(err);
          alert(JSON.stringify(err));
        }
        document.location.reload( true );
      }
    );
  }
}
</pre>

<p>Note the call to set the document location?</p>

<p>Last year, I tried to make that call on the last line of the save function instead of inside the anonymous database callback.</p>

<p>No-no.</p>


<a name="3"></a>

<h4>Adding Handlebars</h4>

<p>Today, I added a new package to my bundle: <a href="http://handlebarsjs.com">handlebars</a>.</p>

<p>It comes packaged for use with <a href="http://kan.so">kanso</a>, and yet I had to struggle a little bit to understand how to add it to my installation.</p>

<p>In the end, it was simple, just copying the package contents into my packages subfolder and adding a reference to it in my main kanso.json file, which now looks like this:</p>

<pre class="prettyprint">
{
  "name": "roomedit",
  "version": "0.0.2",
  "description": "display and edit 2D room, furniture and equipment layout",
  "attachments": ["index.html","raphael-min-jt.js","roomedit.js","index2.html"],
  "modules": ["lib"],
  "load": "lib/app",
  "dependencies": {
    "attachments": null,
    "db": null,
    "handlebars":null,
    "modules": null,
    "jquery": null,
    "properties": null
  }
}
</pre>

<p>So far, though, I am only using it as a pretty stupid formatting tool by defining a template on the fly and stuffing it with values like this:</p>

<pre class="prettyprint">
  var handlebars = require('handlebars');

  var htemplate = handlebars.compile(
    '&lt;p&gt;{{levels}} and '
    + '{{sheets}} in '
    + 'model &lt;i&gt;{{model}}&lt;/i&gt;.&lt;/p&gt;'
    + '&lt;p&gt;Please select a level or sheet:&lt;/p&gt;' );

  var hresult = htemplate({
    levels: thingies( nLevel, 'level' ),
    sheets: thingies( nSheet, 'sheet' ),
    model: modeldoc.name});

  $('#content').append( hresult );
</pre>

<p>That generates two paragraph nodes that I append to the DOM, and nothing more.</p>

<p>I was previously achieving the exact same result using JavaScript and jquery like this:</p>

<pre class="prettyprint">
  var prompt =
    nLevel.toString() + ' level' + pluralSuffix( nLevel ) + ' and ' +
    nSheet.toString() + ' sheet' + pluralSuffix( nSheet ) + ' in model ';

  $('#content')
    .append($('&lt;p/&gt;')
      .text( prompt )
      .append( $('&lt;i/&gt;').text( modeldoc.name ) )
      .append( document.createTextNode( '.' ) ) )
    .append($('&lt;p/&gt;')
      .text( 'Please select a level or sheet:' ) );
</pre>

<p>It's not really shorter.</p>

<p>I find it more readable, though.</p>

<p>And a useful exercise.</p>

<p>The rest of my efforts today went into refactoring my JavaScript, eliminating code duplication and ludicrously deep indenting.</p>



<a name="4"></a>

<h4>Download</h4>

<p>As always, the updated CouchDB database design and Kanso package definition is available from the

<a href="https://github.com/jeremytammik/roomedit">roomedit GitHub repository</a>,

and the version described above is

<a href="https://github.com/jeremytammik/roomedit/releases/tag/2.0.0.12">release 2.0.0.12</a>.</p>

<p>Its playmate, the RoomEditorApp Revit add-in, with its Visual Studio solution and add-in manifest remains virtually unchanged in its

<a href="https://github.com/jeremytammik/RoomEditorApp">RoomEditorApp GitHub repository</a>,

and the current version is now

<a href="https://github.com/jeremytammik/RoomEditorApp/releases/tag/2015.0.2.16">2015.0.2.16</a>.</p>
