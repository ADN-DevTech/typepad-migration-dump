---
layout: "post"
title: "Fusion Meetups and Melting Butter"
date: "2016-04-21 22:58:00"
author: "Adam Nagy"
categories:
  - "Brian"
  - "Fusion 360"
  - "Utilities"
original_url: "https://modthemachine.typepad.com/my_weblog/2016/04/fusion-meetups-and-melting-butter.html "
typepad_basename: "fusion-meetups-and-melting-butter"
typepad_status: "Publish"
---

<p>I had <a href="http://modthemachine.typepad.com/my_weblog/2016/02/fusion-360-meetups.html">posted</a> a few weeks about some upcoming meetups discussing the Fusion API. To generate some interest for the meetups I created a small animation of some butter melting and forming a puddle in a butter dish. Here’s the original animation.&nbsp; At the meetups I presented an introduction to Fusion’s API and shared how the animation was created. The meetup in Seattle was hosted by Microsoft and <a href="http://codefoster.com/">Jeremy Foster</a>, from Microsoft, was kind enough to record it and post it on <a href="https://channel9.msdn.com/Shows/themakershow/f360api">Microsoft’s Channel 9</a>.</p> <p align="center"><a class="asset-img-link" style="display: inline" href="http://modthemachine.typepad.com/.a/6a00e553fcbfc6883401b8d19b7517970c-pi"><img title="Butter" class="asset  asset-image at-xid-6a00e553fcbfc6883401b8d19b7517970c image-full img-responsive" border="0" alt="Butter" src="/assets/image_233310.jpg"></a> </p> <p align="center">&nbsp;</p> <p>If you’re wondering how I created the animation and are imagining something elaborate you’re going to be a bit disappointed. The good news is that it’s probably simpler to do than you’re expecting. First, there are not any elaborate volume calculations to make sure the volume of the puddle matches the volume that has melted from the cube. I just tried to do what looked good. The melting of the cube and the formation of the puddle is done using parameters so the majority of the work is building the model, not writing the program. Although I’m using Fusion here, this is all possible with Inventor using the same concepts. What the program is doing is actually very simple; there’s a loop where in each iteration it changes the value of one or more parameters, updates the model, and captures the screen as an image. That’s it. The challenge is building the model so you get the behavior you want by editing parameters.</p> <p>Let’s look in more detail how the butter model works. There are three components that make up the model; the butter dish, the block of butter, and the puddle. Below, three sketches are shown that are the key to making it all work. The sketch at the bottom defines the shape of the puddle. The other two sketches are used to create a loft feature which is subtracted from the block of butter.</p> <p><a href="http://modthemachine.typepad.com/.a/6a00e553fcbfc6883401b7c841f7ef970b-pi"><img title="Butter2_thumb5" style="border-left-width: 0px; border-right-width: 0px; background-image: none; border-bottom-width: 0px; float: none; padding-top: 0px; padding-left: 0px; margin-left: auto; display: block; padding-right: 0px; border-top-width: 0px; margin-right: auto" border="0" alt="Butter2_thumb5" src="/assets/image_911406.jpg" width="800" height="478"></a></p><br> <p>Below is a detailed look at one of the sketches used to define the loft that cuts away the block of butter. The sketch consists of three lines and a spline. The vertical position of the points on the spline is being controlled through the use of dimension constraints. When you place a dimension in a sketch, a parameter is automatically created that controls the value of the dimension. By editing the parameters the points on the spline will move up and down. The program modifies the parameter values so that the points slowly move down, causing the block of butter to disappear.</p> <p><a href="http://modthemachine.typepad.com/.a/6a00e553fcbfc6883401b8d1cc3e89970c-pi"><img title="Butter3_thumb2" style="border-left-width: 0px; border-right-width: 0px; background-image: none; border-bottom-width: 0px; float: none; padding-top: 0px; padding-left: 0px; margin-left: auto; display: block; padding-right: 0px; border-top-width: 0px; margin-right: auto" border="0" alt="Butter3_thumb2" src="/assets/image_875720.jpg" width="800" height="400"></a></p><br> <p>The puddle uses the same principal but is slightly more complicated because the points don’t just move down but move in two directions. Below is the puddle sketch. To move the points in two directions there are two dimensions to each spline point, one controlling the horizontal (x) direction and another controlling the vertical (y) direction. To make the puddle grow, the parameters are edited to slowly move the points “out” away from the cube. I didn’t worry about how the puddle intersects the butter dish. As it grows it will end up extending into the dish, but it looks ok because you can’t see the overlap.</p> <p><a href="http://modthemachine.typepad.com/.a/6a00e553fcbfc6883401b8d1cc3e98970c-pi"><img title="Butter4_thumb2" style="border-left-width: 0px; border-right-width: 0px; background-image: none; border-bottom-width: 0px; float: none; padding-top: 0px; padding-left: 0px; margin-left: auto; display: block; padding-right: 0px; border-top-width: 0px; margin-right: auto" border="0" alt="Butter4_thumb2" src="/assets/image_303937.jpg" width="800" height="433"></a></p><br> <p>Below is a snapshot of most of the parameters that are used to drive the model. Sketch7 is the puddle sketch and Sketch8 is one of the loft sketches. I named each of the parameters a name that made sense to me so I knew which parameter to edit to get the corresponding point to move in the way I wanted.</p> <p><a href="http://modthemachine.typepad.com/.a/6a00e553fcbfc6883401bb08e5c867970d-pi"><img title="Butter5_thumb3" style="border-left-width: 0px; border-right-width: 0px; background-image: none; border-bottom-width: 0px; float: none; padding-top: 0px; padding-left: 0px; margin-left: auto; display: block; padding-right: 0px; border-top-width: 0px; margin-right: auto" border="0" alt="Butter5_thumb3" src="/assets/image_145104.jpg" width="554" height="635"></a></p><br> <p>Below is a sample program that demonstrates the full basic workflow where it changes a parameter through a range of values and captures and image at each change. It has a loop where it continually changes the parameter named “ToChange”. In the example below, the loop goes until the parameter reaches a specified value but it could also go a pre-defined number of steps. It depends on what you need in your specific case.<br><br></p> <div style="font-size: 9pt; font-family: courier new; background: #eeeeee; color: black; line-height: 140%"><pre>import adsk.core, adsk.fusion, adsk.cam, traceback

<p>def run(context):<br />
    ui = None<br />
    try:<br />
        app = adsk.core.Application.get()<br />
        ui  = app.userInterface</p>

<p>        des = adsk.fusion.Design.cast(app.activeProduct)</p>

<p>        <font color="#0000ff"><strong># Define the values that control the stepping.</strong></font><br />
        startValue = 5<br />
        endValue = 10<br />
        increment = 0.1</p>

<p>        <font color="#0000ff"><strong># Get the parameter to change.</strong></font><br />
        param = des.allParameters.itemByName('ToChange')<br />
        <br />
        <font color="#0000ff"><strong># Iterate through the parameter changes.</strong></font><br />
        currentValue = startValue<br />
        frameCount = 0<br />
        while currentValue &lt; endValue:<br />
            param.value = currentValue<br />
            currentValue += increment<br />
            <br />
            <font color="#0000ff"><strong># Allow Fusion a chance to update.</strong></font><br />
            adsk.doEvents()</p>

<p>            <font color="#0000ff"><strong># Save the current viewport as an image.</strong></font><br />
            frameCount = frameCount + 1<br />
            filename = 'C:/Temp/Animate/Test' + str(frameCount).zfill(4)<br />
            app.activeViewport.saveAsImageFile(filename, 0, 0)<br />
        <br />
        ui.messageBox('Finished.')<br />
    except:<br />
        ui.messageBox('Failed:\n{}'.format(traceback.format_exc()))<br />
</pre></div><br />
<p>The code is really fairly simple. It's all a matter of how creative you get with your parametric model. You can use anything to determine the parameter value and you don't need to be limited to changing a single value. In the melting butter example, I change several values and use a random number so the butter melted and the puddle formed in a slightly random way.</p><br />
<p>The result is a directory containing a bunch of image files. There are many products that will let you combine the images into a video. For the gif file at the top of this post, I used <a href="http://www.gimp.org/">GIMP</a> (Gnu Image Manipulation Program).&nbsp; For a higher resolution, full color animation I used another free tool called <a href="https://www.ffmpeg.org/">FFmpeg</a> where I used the command line below to create the video:<br><br><code>ffmpeg.exe -i C:\Temp\Butter\Images\Butter%04d.png -b 5000k -r 30 output.avi<br><br></code>Here is the <a href="http://modthemachine.typepad.com/ButterMelt.zip">Fusion model and the Python program</a> for the melting butter example. When running the code it begins by displaying a message box to allow you to reset the parameter values back to the original starting values and then an option to run through the parameter changes and generate the images. If you look at the code you’ll see that it does some work to reposition the history marker in the timeline to reduce the re-compute time because Fusion re-computes with every parameter change without an option to delay the re-compute. It moves the marker to just before the features that will re-compute, changes the parameters and then moves it to the end of the timeline so there is a single re-compute of the model with each set of parameter changes.</p><br />
<p>-Brian</p></p>
