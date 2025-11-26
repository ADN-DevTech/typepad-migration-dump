---
layout: "post"
title: "Retrieving and Broadcasting the Roomedit3dv3 Translation"
date: "2016-10-03 05:00:00"
author: "Jeremy Tammik"
categories:
  - "Cloud"
  - "Forge"
  - "JavaScript"
  - "MongoDB"
  - "RTC"
  - "Viewer"
original_url: "https://thebuildingcoder.typepad.com/blog/2016/10/retrieving-and-broadcasting-the-roomedit3dv3-translation.html "
typepad_basename: "retrieving-and-broadcasting-the-roomedit3dv3-translation"
typepad_status: "Publish"
---

<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js?autoload=true" defer="defer"></script>

<p>As I discussed last week,
<a href="http://thebuildingcoder.typepad.com/blog/2016/09/warning-swallower-and-roomedit3d-viewer-extension.html#3">the translation tool emits info on its activity</a> via
<code>socket.io</code>.</p>

<p>Now I want to pick up that information higher up, in the viewer transform extension, and relay it to the node.js web server or broadcast it to the rest of the world.</p>

<p>The existing sample call to <code>emit</code> is inside <code>onTxChange</code>, which is called on each mouse move.</p>

<p>We do not need to track each mouse movement in such fine detail, just the beginning and end of the translation.</p>

<p><center></p>

<p><a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e16897883301b8d2249fe3970c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883301b8d2249fe3970c image-full img-responsive" alt="Roomedit3dv3 Forge extension in action" title="Roomedit3dv3 Forge extension in action" src="/assets/image_dbbcaa.jpg" border="0" /></a><br /></p>

<p></center></p>

<p>Luckily, I can take a look back at the previous incarnation <a href="https://github.com/jeremytammik/roomedit3d">roomedit3d</a>,
in the module <a href="https://github.com/jeremytammik/roomedit3d/blob/master/www/js/extensions/Roomedit3dTranslationTool.js#L333-L366">Roomedit3dTranslationTool.js</a>,
where this is handled by the <code>handleButtonUp</code> function:</p>

<pre class="prettyprint">
  this.handleButtonUp = function(event, button) {

    if( _isDirty && _externalId && _initialHitPoint ) {
      var offset = subtract_point(
        _transformControlTx.position,
        _initialHitPoint );

      _initialHitPoint = new THREE.Vector3(
        _transformControlTx.position.x,
        _transformControlTx.position.y,
        _transformControlTx.position.z );

      console.log( 'button up: external id '
        + _externalId + ' offset by '
        + pointString( offset ) );

      var data = {
        externalId : _externalId,
        offset : offset
      }

      options.roomedit3dApi.postTransform(data);

      _isDirty = false;
    }

    _isDragging = false;

    if (_transformControlTx.onPointerUp(event))
      return true;

    return false;
  };
</pre>

<p>Using that as a starting point for my exploration how to reproduce the same functionality in the new version, I found that this is all I need for the internal <code>emit</code> call from the translate tool to the transform extension:</p>

<pre class="prettyprint">
  handleButtonUp(event, button) {

    console.log( 'transform.translate complete' );

    if (this._selection) {
      if (this._selection.dbIdArray) {
        var dbId = this._selection.dbIdArray[0]

        if(dbId) {

          var translation = new THREE.Vector3(
            this._transformMesh.position.x - this._selection.model.offset.x,
            this._transformMesh.position.y - this._selection.model.offset.y,
            this._transformMesh.position.z - this._selection.model.offset.z)

          this._viewer.getProperties(dbId, (result) =&gt; {

            var externalId = result.externalId;

            console.log( 'transform.translate complete for '
              + externalId
              + ': ' + translation.x.toFixed( 2 )
              + ','+ translation.y.toFixed( 2 )
              + ','+ translation.z.toFixed( 2 ) );

            this.emit('transform.translate.complete', {
              externalId: externalId,
              translation: translation
            })
          });
        }    
      }
    }

    this._isDragging = false

    if (this._transformControlTx.onPointerUp(event))
      return true

    return false
  }
</pre>

<p>This is much nicer and cleaner than my previous implementation, because I have not touched any of the other functionality or functions of the translation tool at all.</p>

<p>To pass on the information from the tool to the wide outside world via a <code>socket.io</code> broadcast in the viewer extension, I just added a couple of lines to its constructor:</p>

<pre class="prettyprint">
class TransformExtension extends ExtensionBase {

  //////////////////////////////////////////////
  // Class constructor
  //
  //////////////////////////////////////////////
  constructor (viewer, options) {

    super (viewer, options)

    this.translateTool = new TranslateTool(viewer)

    this._viewer.toolController.registerTool(
      this.translateTool)

    this.rotateTool = new RotateTool(viewer)

    this._viewer.toolController.registerTool(
      this.rotateTool)

    this.translateTool.on('transform.translate.complete', (data) =&gt; {

      console.log( 'broadcast transform of '
        + data.externalId
        + ': ' + data.translation.x.toFixed( 2 )
        + ','+ data.translation.y.toFixed( 2 )
        + ','+ data.translation.z.toFixed( 2 ) );

      var socketSvc = ServiceManager.getService('SocketSvc')

      // external id == Revit UniqueId
      // THREE.Vector3 offset x y z

      socketSvc.broadcast('transform', data)

    })    
  }

  // . . .
</pre>

<p>So things are going swimmingly on this front.</p>

<p>I still have a bunch of other stuff to implement, test and document, though, before I can go off on my vacation next week feeling well prepared for 
the <a href="http://www.rtcevents.com/rtc2016eur">RTC Revit Technology Conference Europe</a> in Porto the week after that:</p>

<p>In fact, I have to go over my whole suite of samples connecting the desktop and the cloud, each consisting of a C# .NET Revit API desktop add-in and a web server:</p>

<ul>
<li><a href="https://github.com/jeremytammik/RoomEditorApp">RoomEditorApp</a> and  the <a href="https://github.com/jeremytammik/roomedit">roomeditdb</a> CouchDB
database and web server demonstrating real-time round-trip graphical editing of furniture family instance location and rotation plus textual editing of element properties in a simplified 2D representation of the 3D BIM.</li>
<li><a href="https://github.com/jeremytammik/FireRatingCloud">FireRatingCloud</a> and
the <a href="https://github.com/jeremytammik/firerating">fireratingdb</a> node.js
MongoDB web server demonstrating real-time round-trip editing of Revit element shared parameter values
&ndash; <a href="https://heroku.com">Heroku</a> requires an update to MongoDB 3.2.</li>
<li>Update the <a href="https://github.com/jeremytammik/Roomedit3dApp">Roomedit3dApp</a> Revit add-in to work with the 
new <a href="https://github.com/Autodesk-Forge/forge-boilers.nodejs/tree/roomedit3d">roomedit3dv3</a> Forge Viewer extension demonstrating translation of BIM element instances in the viewer and updating the Revit model in real time via a <code>socket.io</code> broadcast.</li>
</ul>

<p>For the latter, I need to set up a production environment and deploy to Heroku.</p>

<p>So far, I have just been developing and testing in a <code>DEV</code> environment on the local machine.</p>

<p>It is rather complicated to connect to that from the virtual Windows machine running Revit and the add-in, though, so better to move to the real Internet and <code>PROD</code> first.</p>

<p>Wish me luck with the next steps.</p>
