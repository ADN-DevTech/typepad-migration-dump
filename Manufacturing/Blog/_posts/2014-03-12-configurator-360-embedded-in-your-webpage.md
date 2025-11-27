---
layout: "post"
title: "Configurator 360 embedded in your webpage"
date: "2014-03-12 10:31:58"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "Configurator 360"
original_url: "https://adndevblog.typepad.com/manufacturing/2014/03/configurator-360-embedded-in-your-webpage.html "
typepad_basename: "configurator-360-embedded-in-your-webpage"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/manufacturing/adam-nagy.html" target="_self">Adam Nagy</a></p>
<p><strong>Configurator 360</strong> enables you to upload your design and then let others configure it through a web or mobile interface. Now you can even embed the web component in your own webpage. To test this feature you'll need at least a trial account, which you can get through here: <a title="" href="http://www.autodesk.com/products/autodesk-configurator-360/free-trial" target="_self">http://www.autodesk.com/products/autodesk-configurator-360/free-trial</a></p>
<p>And here you can find the information on embedding the web component which also includes a link to a sample page: <a href="http://embedding.configurator360.autodesk.com/doc.html">http://embedding.configurator360.autodesk.com/doc.html</a></p>
<p>One thing to note is that the URL of the webpage embedding the&nbsp;configurator needs to be exact - e.g. simply adding <strong>http://www.typepad.com</strong> is not enough. When&nbsp;I was writing this article and wanted to preview it, the temporary URL was different from the final URL of the blog post and&nbsp;so I&nbsp;needed to add that too:</p>
<p><a class="asset-img-link" style="display: inline;" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01a73d8e4d20970d-popup"><img class="asset  asset-image at-xid-6a0167607c2431970b01a73d8e4d20970d img-responsive" style="width: 450px;" title="C360_config" src="/assets/image_b37a46.jpg" alt="C360_config" /></a></p>
<p>I also needed to set <strong>Unrestricted viewing</strong> access for the model so that anyone looking at this webpage can access it:<br /> <a class="asset-img-link" style="display: inline;" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01a73d9a5980970d-popup"><img class="asset  asset-image at-xid-6a0167607c2431970b01a73d9a5980970d img-responsive" style="width: 450px;" title="ConfigSettings" src="/assets/image_1da972.jpg" alt="ConfigSettings" /></a></p>
<p><strong>Configurator 360</strong> web component is using <strong>WebGL</strong> and so can be used on any <strong>WebGL</strong> enabled device. If the web control does not appear below then it is not supported on your system.</p>
<script type="text/javascript" src="https://configurator360.autodesk.com/Script/EmbeddedViewer"></script>
<div id="myViewer" style="position: relative; width: 480px; height: 400px;">&nbsp;</div>
<script type="text/javascript">
(function (window) {
        "use strict";
        var C360 = window.ADSK && window.ADSK.C360;

        // callback for getPropertyValues.
        function listProperties(result) {
            window.console.log(window.JSON.stringify(result, null, " "));
        }

        // success handler
        function viewerLoaded(viewer) {
            // The C360Viewer is loaded, do something with it.
            viewer.getPropertyValues(null, listProperties);
        }

        // error handler
        function failedToLoad(viewer) {
            window.alert("The viewer failed to load for some reason.");
            viewer.unload(); // Unload the C360Viewer
        }

        // Check if the API was loaded.
        if (C360 && !C360.isLoaded) { // Not supported on mobile devices currently.
            C360.isLoaded = true;

            // Initialize the viewer
            C360.initViewer({
                container: "myViewer",
                design: "520905552843171165/rlmjpx00pvv7",
                panes: {    // Panes to show
                    parameters: true
                 },
                success: viewerLoaded, // Set success handler
                error: failedToLoad // Set error handler
            });

        }
    }(this));
</script>
<p><br />For some reason on typepad the content gets inserted twice (one of which is hidden) which means the script is inserted and run twice too, creating two controls one under the other. To avoid that I modified the code to store in the <strong>C360.isLoaded</strong> variable if the component is loaded already, and if not, only then run the usual code:</p>
<pre style="line-height: 120%;">// Check if the API was loaded.
if (C360 &amp;&amp; !C360.isLoaded) { 
  C360.isLoaded = true;

  // Initialize the viewer
  C360.initViewer({
    container: "myViewer",
    design: "520905552843171165/rlmjpx00pvv7",
    panes: { // Panes to show
      parameters: true
    },
    success: viewerLoaded, // Set success handler
    error: failedToLoad // Set error handler
  });
}</pre>
<p>If you want to keep up-to-date with the latest features of <strong>Configurator 360</strong> then keep an eye on this forum post: <a href="http://forums.autodesk.com/t5/Configurator-360-General/What-s-New-in-Configurator-360/m-p/4378949">http://forums.autodesk.com/t5/Configurator-360-General/What-s-New-in-Configurator-360/m-p/4378949</a></p>
