---
layout: "post"
title: "Understand the Coordinate System of View and Data API with AxisHelper Extension"
date: "2015-12-09 22:43:47"
author: "Daniel Du"
categories:
  - "Browser"
  - "Daniel Du"
  - "HTML5"
  - "Javascript"
  - "Script"
  - "View and Data API"
  - "WebGL"
original_url: "https://adndevblog.typepad.com/cloud_and_mobile/2015/12/understand-the-coordinate-system-of-view-and-data-api-with-axishelper-extension.html "
typepad_basename: "understand-the-coordinate-system-of-view-and-data-api-with-axishelper-extension"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/cloud_and_mobile/daniel-du.html">Daniel Du</a></p>  <p>To work with Autodesk View and Data API, you will need understand the coordinate system definition of 3D model space. As you know, View and Data API is build on top of Three.js, while there is an AxisHelper in three.js to show the axes. As asked <a href="http://forums.autodesk.com/t5/view-and-data-api/show-axes/m-p/5610748" target="_blank">in this post</a>, if you just use it directly in viewer, it does not work, you will get an error message :&quot;Only THREE.Mesh can be rendered by the Firefly renderer. Use THREE.Mesh to draw lines.&quot;&#160; The reason is that we have a custom rendered on top of three.js, so this renderer cannot render that AxisHelper. You would need to handle creation of the lines by yourself. </p>  <p>I created an extension so that you can use it directly: </p>  <pre class="csharpcode"><span class="rem">// It is recommended to load the extension when geometry is loaded</span>
viewer.addEventListener(Autodesk.Viewing.GEOMETRY_LOADED_EVENT, 
   <span class="kwrd">function</span>(){

   viewer.loadExtension(<span class="str">'Autodesk.ADN.Viewing.Extension.AxisHelper'</span>);

   });</pre>
<style type="text/css">
.csharpcode, .csharpcode pre
{
	font-size: small;
	color: black;
	font-family: consolas, "Courier New", courier, monospace;
	background-color: #ffffff;
	/*white-space: pre;*/
}
.csharpcode pre { margin: 0em; }
.csharpcode .rem { color: #008000; }
.csharpcode .kwrd { color: #0000ff; }
.csharpcode .str { color: #006080; }
.csharpcode .op { color: #0000c0; }
.csharpcode .preproc { color: #cc6633; }
.csharpcode .asp { background-color: #ffff00; }
.csharpcode .html { color: #800000; }
.csharpcode .attr { color: #ff0000; }
.csharpcode .alt 
{
	background-color: #f4f4f4;
	width: 100%;
	margin: 0em;
}
.csharpcode .lnum { color: #606060; }</style>

<p>Here is a screen-shot, the red line is X-axis, green line is Y-axis and blue line is Z-axis:</p>

<p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c7f7e5fa970b-pi"><img title="Screen Shot 2015-12-10 at 2.38.34 PM" style="border-top: 0px; border-right: 0px; background-image: none; border-bottom: 0px; padding-top: 0px; padding-left: 0px; border-left: 0px; display: inline; padding-right: 0px" border="0" alt="Screen Shot 2015-12-10 at 2.38.34 PM" src="/assets/image_6ad519.jpg" width="471" height="325" /></a></p>

<p>Here is the source code of this extension, but you may need to check it from <a href="https://github.com/Developer-Autodesk/library-javascript-viewer-extensions/tree/master/Autodesk.ADN.Viewing.Extension.AxisHelper" target="_blank">github</a> to get the latest version as it may be updated from time to time.</p>

<pre class="csharpcode"><span class="rem">///////////////////////////////////////////////////////////////////////////////</span>
AutodeskNamespace(<span class="str">&quot;Autodesk.ADN.Viewing.Extension&quot;</span>);

Autodesk.ADN.Viewing.Extension.AxisHelper = <span class="kwrd">function</span> (viewer, options) {

    Autodesk.Viewing.Extension.call(<span class="kwrd">this</span>, viewer, options);

    <span class="kwrd">var</span> _self = <span class="kwrd">this</span>;

    <span class="kwrd">var</span> _axisLines = [];

    _self.load = <span class="kwrd">function</span> () {

        console.log(<span class="str">'Autodesk.ADN.Viewing.Extension.AxisHelper loaded'</span>);

        addAixsHelper();

        <span class="rem">//workaround</span>
        <span class="rem">//have to call this to show up the axis</span>
        viewer.restoreState(viewer.getState());

        <span class="kwrd">return</span> <span class="kwrd">true</span>;
    };



    _self.unload = <span class="kwrd">function</span> () {

        removeAixsHelper();

        console.log(<span class="str">'Autodesk.ADN.Viewing.Extension.AxisHelper unloaded'</span>);
        <span class="kwrd">return</span> <span class="kwrd">true</span>;
    };


    <span class="kwrd">var</span> addAixsHelper = <span class="kwrd">function</span>() {

        _axisLines = [];

        <span class="rem">//get bounding box of the model</span>
        <span class="kwrd">var</span> boundingBox = viewer.model.getBoundingBox();
        <span class="kwrd">var</span> maxpt = boundingBox.max;
        <span class="kwrd">var</span> minpt = boundingBox.min;
     
        <span class="kwrd">var</span> xdiff =    maxpt.x - minpt.x;
        <span class="kwrd">var</span> ydiff =    maxpt.y - minpt.y;
        <span class="kwrd">var</span> zdiff =    maxpt.z - minpt.z;

        <span class="rem">//make the size is bigger than the max bounding box </span>
        <span class="rem">//so that it is visible </span>
        <span class="kwrd">var</span> size = Math.max(xdiff,ydiff,zdiff) * 1.2; 
        <span class="rem">//console.log('axix size :' + size);</span>


        <span class="rem">// x-axis is red</span>
        <span class="kwrd">var</span> material_X_Axis = <span class="kwrd">new</span> THREE.LineBasicMaterial({
            color: 0xff0000,  <span class="rem">//red </span>
            linewidth: 2
        });
        viewer.impl.matman().addMaterial(<span class="str">'material_X_Axis'</span>,material_X_Axis,<span class="kwrd">true</span>);
        <span class="rem">//draw the x-axix line</span>
        <span class="kwrd">var</span> xLine = drawLine(
            {x : 0, y : 0, z : 0} ,
            {x : size, y : 0, z : 0} , 
            material_X_Axis);
       
        _axisLines.push(xLine);


        <span class="rem">// y-axis is green</span>
        <span class="kwrd">var</span> material_Y_Axis = <span class="kwrd">new</span> THREE.LineBasicMaterial({
            color: 0x00ff00,  <span class="rem">//green </span>
            linewidth: 2
        });
        viewer.impl.matman().addMaterial(<span class="str">'material_Y_Axis'</span>,material_Y_Axis,<span class="kwrd">true</span>);
        <span class="rem">//draw the y-axix line</span>
        <span class="kwrd">var</span> yLine = drawLine(
            {x : 0, y : 0, z : 0} ,
            {x : 0, y : size, z : 0} , 
            material_Y_Axis);
        
        _axisLines.push(yLine);


        <span class="rem">// z-axis is blue</span>
        <span class="kwrd">var</span> material_Z_Axis = <span class="kwrd">new</span> THREE.LineBasicMaterial({
            color: 0x0000ff,  <span class="rem">//blue </span>
            linewidth: 2
        });
        viewer.impl.matman().addMaterial(<span class="str">'material_Z_Axis'</span>,material_Z_Axis,<span class="kwrd">true</span>);
        <span class="rem">//draw the z-axix line</span>
        <span class="kwrd">var</span> zLine = drawLine(
            {x : 0, y : 0, z : 0} ,
            {x : 0, y : 0, z : size} , 
            material_Z_Axis);
      
        _axisLines.push(zLine);


    }


    <span class="kwrd">var</span> drawLine = <span class="kwrd">function</span>(start, end, material) {

            <span class="kwrd">var</span> geometry = <span class="kwrd">new</span> THREE.Geometry();

            geometry.vertices.push(<span class="kwrd">new</span> THREE.Vector3(
                start.x, start.y, start.z));

            geometry.vertices.push(<span class="kwrd">new</span> THREE.Vector3(
                end.x, end.y, end.z));

            <span class="kwrd">var</span> line = <span class="kwrd">new</span> THREE.Line(geometry, material);

            viewer.impl.scene.add(line);
            <span class="rem">//refresh viewer</span>
            viewer.impl.invalidate(<span class="kwrd">true</span>);

            <span class="kwrd">return</span> line;
    }

    <span class="kwrd">var</span> removeAixsHelper = <span class="kwrd">function</span>() {

        _axisLines = [];

        _axisLines.forEach(<span class="kwrd">function</span>(line){

            viewer.impl.scene.remove(line);
        });

        <span class="rem">//remove materials</span>
        delete viewer.impl.matman().materials.material_X_Axis;
        delete viewer.impl.matman().materials.material_Y_Axis;
        delete viewer.impl.matman().materials.material_Z_Axis;

        
    }


};

Autodesk.ADN.Viewing.Extension.AxisHelper.prototype =
    Object.create(Autodesk.Viewing.Extension.prototype);

Autodesk.ADN.Viewing.Extension.AxisHelper.prototype.constructor =
    Autodesk.ADN.Viewing.Extension.AxisHelper;

Autodesk.Viewing.theExtensionManager.registerExtension(
    <span class="str">'Autodesk.ADN.Viewing.Extension.AxisHelper'</span>,
    Autodesk.ADN.Viewing.Extension.AxisHelper);</pre>

<p>Hope it helps.</p>
