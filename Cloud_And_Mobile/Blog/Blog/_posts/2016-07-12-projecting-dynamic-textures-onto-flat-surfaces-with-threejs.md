---
layout: "post"
title: "Projecting Dynamic Textures onto Flat Surfaces with Three.js"
date: "2016-07-12 16:37:55"
author: "Michael Ge"
categories:
  - "Forge"
  - "HTML5"
  - "IoT"
  - "Javascript"
  - "Michael Ge"
  - "THREE.js"
  - "Viewer"
original_url: "https://adndevblog.typepad.com/cloud_and_mobile/2016/07/projecting-dynamic-textures-onto-flat-surfaces-with-threejs.html "
typepad_basename: "projecting-dynamic-textures-onto-flat-surfaces-with-threejs"
typepad_status: "Publish"
---

<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d2004ef5970c-pi" style="display: inline;"><img alt="Thumb" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b8d2004ef5970c img-responsive" src="/assets/image_95e9e2.jpg" style="display: block; margin-left: auto; margin-right: auto;" title="Thumb" /></a></p>
<p>By Michael Ge (<a href="https://twitter.com/hahakumquat">@hahakumquat</a>)</p>
<p>First and foremost, the 3D Viewer is designed to display static 3D objects, but this is not to say that applications of the Viewer cannot have dynamic elements within it!</p>
<p>So far, most of the extensions on our blog have dealt with manipulating elements in the viewer to return static changes, but with relatively simple injections of Three.js code, it&#39;s possible to quickly turn a single-state model into a data-driven, dynamic 3D simulation.</p>
<p>The extension I&#39;ve developed is a heatmap projection onto a 3D model of a building. While the input &quot;data&quot; is currently random, the modular code makes it easy to substitute these randomly generated data points with real sensors in an actual building.</p>
<p>To visualize this data, I used <a href="https://github.com/mourner/simpleheat">mourner&#39;s simpleheat.js</a> library to create a heatmap on a canvas element. From there, I created a Three.js plane and projected the texture onto the plane, masking out the shape of my desired rooftop. Several code snippets are taken&#0160;from <a href="http://adndevblog.typepad.com/cloud_and_mobile/philippe-leefsma.html">Philippe&#39;s</a> examples, including <a href="http://adndevblog.typepad.com/cloud_and_mobile/2015/08/getting-bounding-boxes-of-each-component-in-the-viewer.html">getting the bounding box of a fragment</a>&#0160;and&#0160;<a href="http://adndevblog.typepad.com/cloud_and_mobile/2015/08/moving-visually-your-components-in-the-viewer-using-the-transformtool.html">adjusting fragment positions</a>.&#0160;Overall, it&#39;s a simple process with plenty of documentation and resources, and with a few minor changes&#0160;here and there, you should be able to achieve the same effect in any scene.</p>
<p>With that said, let&#39;s take a look at the actual code. The extension can be broken up into four main sections: Viewer&#0160;functions, heatmap initialization, material initialization, and application of the material onto a mesh.</p>
<p><strong>Viewer&#0160;Functions</strong></p>
<script src="https://adndevblog.typepad.com/files/run_prettify-3.js" type="mce-text/javascript"></script>
<pre class="prettyprint">////////////////////////////////////////////////////////////////////////////

    /* Private Variables and constructor */ 
    
    // Heat Map Floor constructor
    Autodesk.Viewing.Extension.call(this, viewer, options);
    var _self = this;
    var _viewer = viewer;<br />
    // Find fragmentId of a desired mesh by selection_changed_event listener<br />    var roofFrag = 1;<br /> 
    // Settings configuration flags
    var progressiveRenderingWasOn = false, ambientShadowsWasOn = false; 

    // simpleheat private variables
    var _heat, _data = [];
    // Configurable heatmap variables:
    // MAX-the maximum amplitude of data input
    // VAL-the value of a data input, in this case, it&#39;s constant
    // RESOLUTION-the size of the circles, high res -&gt; smaller circles
    // FALLOFF-the rate a datapoint disappears
    var MAX = 2000, VAL = 1500, RESOLUTION = 10, FALLOFF = 30;

    // THREE.js private variables
    var _material, _texture, _bounds, _plane, Z_POS = -0.1; //3.44 for floor;
    
    ////////////////////////////////////////////////////////////////////////////
    
    /* Load, main, and unload functions */

    _self.load = function() {

        // Turn off progressive rendering and ambient shadows for nice look
        if (_viewer.prefs.progressiveRendering) {
            progressiveRenderingWasOn = true;
            _viewer.setProgressiveRendering(false);
        }
        if (_viewer.prefs.ambientShadows) {
            ambientShadowsWasOn = true;
            _viewer.prefs.set(&quot;ambientShadows&quot;, false);
        }
        
        _bounds = genBounds(roofFrag);        
        _heat = genHeatMap();
        _texture = genTexture();
        _material = genMaterial();
        
        _plane = clonePlane();
        setMaterial(roofFrag, _material);
        
        animate();
        console.log(&quot;Heat Map Floor loaded.&quot;);
        return true;
    }
    
    _self.unload = function() {

        if (progressiveRenderingWasOn)
            _viewer.setProgressiveRendering(true);
        if (ambientShadowsWasOn) {
            _viewer.prefs.set(&quot;ambientShadows&quot;, true);
        }
        progressiveRenderingWasOn = ambientShadowsWasOn = false;
        
        delete _viewer.impl.matman().materials.heatmap;
        _viewer.impl.scene.remove(_plane);
        
        console.log(&quot;Heat Map Floor unloaded.&quot;);
        return true;
    }
    
    // Animation loop for checking for new points and drawing them on texture
    function animate() {
        requestAnimationFrame(animate);
        _heat.add(receivedData());            
        _heat._data = decay(_heat._data);
        _heat.draw();

        _texture.needsUpdate = true;
        // setting var 3 to true enables invalidation even without changing scene
        _viewer.impl.invalidate(true, false, true);
    }
    
    ////////////////////////////////////////////////////////////////////////////

    /* Geometry/Fragment/Location functions */

    // Gets bounds of a fragment
    function genBounds(fragId) {
        var bBox = new THREE.Box3();        
        _viewer.model.getFragmentList()
            .getWorldBounds(fragId, bBox);
        
        var width = Math.abs(bBox.max.x - bBox.min.x);
        var height = Math.abs(bBox.max.y - bBox.min.y);
        var depth = Math.abs(bBox.max.z - bBox.min.z);<br />
        return {width: width, height: height, depth: depth, min: bBox.min};
    }
</pre>
<p>The scene will constantly be updating with new heatmap data, so we&#39;ll need to keep track of several private variables for the heatmap and plane. In addition, the constant updates yield rather ugly flickering with progressive rendering and grainy ambient shadows, so we&#39;ll also keep track of the settings&#39; states before the extension is enabled so that we can revert to the original state.</p>
<p>The&#0160;<em>load</em> function, when run on initialization, disables these settings, creates the material with the canvas, and projects the material onto the rooftop. As expected, <em>unload&#0160;</em>undoes such modifications.&#0160;</p>
<p><em>animate&#0160;</em>begins a requestAnimationFrame() loop that adds any received data from sensors (or in my implementation, random data), creates a data falloff effect over time, and tells the viewer to update itself with an updated texture. It&#39;s important here to note that the&#0160;invalidate() call has a third parameter which, when set to true, updates overlays. This is critical to getting the texture overlay to update correctly.</p>
<p><strong>Heatmap Initialization</strong></p>
<pre class="prettyprint">///////////////////////////////////////////////////////////////////////

    /* Heatmap functions */
    
    // Starts a heatmap
    function genHeatMap() {

        var canvas = document.getElementById(&quot;texture&quot;);
        canvas.width = _bounds.width * RESOLUTION;
        canvas.height = _bounds.height * RESOLUTION;

        return simpleheat(&quot;texture&quot;).max(MAX);
    }

    // TODO: Replace with actually received data
    // returns an array of data received by sensors
    function receivedData() {

        return [Math.random() * $(&quot;#texture&quot;).width(),
                Math.random() * $(&quot;#texture&quot;).height(),
                Math.random() * VAL];
    }

    // decrements the amplitude of a signal by FALLOFF for decay over time
    function decay(data) {

        // removes elements whose amlitude is &lt; 1
        return data.filter(function(d) {
            d[2] -= FALLOFF;
            return d[2] &gt; 1;
        });
    }
</pre>
<p><a href="https://github.com/mourner/simpleheat">mourner&#39;s simpleheat.js</a>&#0160;makes it easy to create a heatmap, taking in data with a Cartesian coordinate and amplitude, and outputting a circle with the designated location and color intensity.&#0160;</p>
<p><em>genHeatMap&#0160;</em>simply creates a canvas to store the heatmap data, returning the generated simpleheat object.</p>
<p>Every time an animate function is called, the heatmap will add data from&#0160;<em>receivedData</em>. Sensory input data in the format of a size-3 array could replace the currently random generation of data points.</p>
<p>For this experiment, I wanted to remove old datapoints. As a result, I created a&#0160;<em>decay&#0160;</em>function that lowers the amplitude values of all elements in the dataset over time, removing them once they become negligible, however it&#39;s possible to aggregate data over time, porting that information over from a database.</p>
<p><strong>Material Initialization</strong></p>
<p>Now that we have a dynamically updating canvas, we can apply it as a texture to a created Three.js plane:</p>
<pre class="prettyprint">    ///////////////////////////////////////////////////////////////////////<br /><br />    /* Texture and material functions */<br /> <br />    // Creates a texture<br />    function genTexture() {<br /> <br />        var canvas = document.getElementById(&quot;texture&quot;);<br />        var texture = new THREE.Texture(canvas);<br />        return texture;<br />    }<br /><br />    // generates a material<br />     function genMaterial() {<br /><br />     var material = new THREE.MeshBasicMaterial({<br />         map: _texture,<br />         side: THREE.DoubleSide,<br />         alphaMap: THREE.ImageUtils.loadTexture(&quot;mask_crop.png&quot;)<br />     });<br />     material.transparent = true;<br /><br />     // register the material under the name &quot;heatmap&quot;<br />     _viewer.impl.matman().addMaterial(&quot;heatmap&quot;, material, true);<br /> <br />     return material;<br /> }
</pre>
<p><em>genTexture</em>&#0160;creates a Three.Texture from the canvas that is mapped to the basic material. In order to exclude parts of the canvas that are outside the roof area, we also use an alpha mask like the following:</p>
<p><a class="asset-img-link" href="http://a5.typepad.com/6a01b8d1fcaed5970c01b8d204884d970c-pi" style="display: inline;"><img alt="Mask" class="asset  asset-image at-xid-6a01b8d1fcaed5970c01b8d204884d970c img-responsive" height="191" src="/assets/image_b18d95.jpg" style="display: block; margin-left: auto; margin-right: auto;" title="Mask" width="327" /></a></p>
<p><strong>Applying the Material to a Mesh</strong></p>
<p>We have the material ready to go, so now it&#39;s just a matter of applying it to the proper face.&#0160;</p>
<pre class="prettyprint"> ///////////////////////////////////////////////////////////////////////<br /><br /> /* Rendering the heatmap in the Viewer */<br /><br /> function clonePlane() {<br /><br />     // To use native three.js plane, use the following mesh constructor    <br />     geom = new THREE.PlaneGeometry(_bounds.width, _bounds.height);<br />     plane = new THREE.Mesh(geom, _material);<br />     plane.position.set(0, 0, _bounds.min.z - Z_POS);<br /><br />     _viewer.impl.addOverlay(&quot;pivot&quot;, plane);<br /> <br />     return plane;<br /> }</pre>
<p>We create a geometry of the roof&#39;s width and size and apply the material. Finally, we move the original plane to the desired location and insert it into the scene as an overlay.</p>
<p>&#0160;I also noticed that simply calling the&#0160;<em>addOverlay&#0160;</em>function without modifying the simpleheat.js code led to circular artifacts around the data points. This is an easy fix, however. Simply go into the&#0160;<em>draw&#0160;</em>function in simpleheat.js, and after the clearRect() function is called, draw a nearly completely transparent rectangle over the entire canvas:</p>
<pre class="prettyprint">        ...
        ctx.clearRect(0, 0, this._width, this._height); 

        // include the following section
        ctx.fillStyle = &quot;#FFFFFF&quot;;
        ctx.globalAlpha = 0.01;
        ctx.fillRect(0, 0, this._width, this._height);
        ctx.globalAlpha = 1;
</pre>
<p>&#0160;</p>
<p>Here&#39;s the full source code for the extension:</p>
<script src="https://gist.github.com/hahakumquat/9d438fab3773e67e474590a877755e2b.js"></script>
<p>And here is a link to the <a href="http://heatmap2d.herokuapp.com/">project demo</a>.</p>
<p>Hope you like it!</p>
<p>&#0160;</p>
