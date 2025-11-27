---
layout: "post"
title: "Inventor 2015 API Enhancements"
date: "2014-04-08 20:32:40"
author: "Wayne Brill"
categories:
  - "Assemblies"
  - "Drawings"
  - "Inventor"
  - "Parts"
  - "Sheet Metal"
  - "Wayne"
original_url: "https://modthemachine.typepad.com/my_weblog/2014/04/inventor-2015-api-enhancements.html "
typepad_basename: "inventor-2015-api-enhancements"
typepad_status: "Publish"
---

<p>Inventor 2015 will soon be shipping. Most of the API enhancements are keeping up with the new Inventor 2015 functionality and some of the enhancements are catching up to things that have been&#0160; available from the Inventor user interface in previous releases.&#0160;</p>
<p><strong>Enhancements in the API for Parts</strong></p>
<p><em>Sweep Feature.</em></p>
<p>The <em>SweepFeature</em> has a new option for twist angle and now&#0160; instead of having a method call that takes all the arguments to create a sweep you create a definition object and set all the properties on that object and provide definition as input to create the sweep. In the UI when you click the sweep command the dialog comes up it gathers all of the inputs needed to create the sweep but the sweep is not actually created until you click ok. You can consider the definition object as similar to the dialog.</p>
<p>&#0160;<a href="http://modthemachine.typepad.com/.a/6a00e553fcbfc6883401a3fcea4c9a970b-pi"><img alt="image" border="0" height="221" src="/assets/image_787757.jpg" style="background-image: none; padding-left: 0px; padding-right: 0px; display: inline; padding-top: 0px; border-width: 0px;" title="image" width="490" /></a></p>
<p>&#0160;</p>
<p><em>Work plane.</em></p>
<p>Previous to Inventor 2015 you could create a mid plane between two parallel planes. Inventor now supports the creation of a plane between any two planes and they do not have to be parallel. An example of this is bisecting two perpendicular planes. A new point argument that defines which quadrant to use needs to be provided because if the planes are not parallel there are two possible solutions.</p>
<p><a href="http://modthemachine.typepad.com/.a/6a00e553fcbfc6883401a73da50104970d-pi"><img alt="image" border="0" height="459" src="/assets/image_921792.jpg" style="background-image: none; padding-left: 0px; padding-right: 0px; display: inline; padding-top: 0px; border-width: 0px;" title="image" width="481" /></a></p>
<p>&#0160;</p>
<p>&#0160;</p>
<p>&#0160;</p>
<p>&#0160;</p>
<p>Work plane created using WorkPlanes.AddByTwoPlanes()</p>
<p>&#0160;</p>
<p><em>DirectEditFeature</em></p>
<p>In the UI the direct edit feature lets you push and pull features. The API has read support and allows you to find information such as&#0160; which operations were defined.</p>
<p><em>FreeformFeature</em></p>
<p>Another enhancement in the user interface is T-Spline capabilities. In the product it is referred to as a Freeform. The 2015 API has minimal support for this but should change in the future. In this release you can find the <em>FreeformFeature</em> and do basic feature operations such as getting its name, suppressing it or reordering it in the tree.</p>
<p><em>Sketch</em></p>
<p>Inventor 2015 has changes to sketching that makes it more intuitive to use. The API has been updated because there are quite a few new settings.</p>
<p><a href="http://modthemachine.typepad.com/.a/6a00e553fcbfc6883401a73da50109970d-pi"><img alt="image" border="0" height="248" src="/assets/image_348414.jpg" style="background-image: none; padding-left: 0px; padding-right: 0px; display: inline; padding-top: 0px; border-width: 0px;" title="image" width="547" /></a></p>
<p>These settings are exposed through the API.</p>
<p>&#0160;</p>
<p><strong>Enhancements in the API for Sheet metal</strong></p>
<p><em>Punch – cut across bend</em></p>
<p>Now when you do a punch you have an option to have the punch cut across a bend.</p>
<p style="text-align: left;"><a href="http://modthemachine.typepad.com/.a/6a00e553fcbfc6883401a73da5010d970d-pi"><img alt="image" border="0" height="313" src="/assets/image_448553.jpg" style="background-image: none; padding-left: 0px; padding-right: 0px; display: inline; padding-top: 0px; border-width: 0px;" title="image" width="443" /></a></p>
<p style="text-align: left;">Punch on the right used cut across bend</p>
<p style="text-align: left;"><a href="http://modthemachine.typepad.com/.a/6a00e553fcbfc6883401a73da50117970d-pi"><img alt="image" border="0" height="381" src="/assets/image_388089.jpg" style="background-image: none; padding-left: 0px; padding-right: 0px; display: inline; padding-top: 0px; border-width: 0px;" title="image" width="441" /></a></p>
<p style="text-align: left;">You can think of it as unfolding the part and doing the punch and then folding it back. (the edges are normal to the flat)</p>
<p><em>Punch – cut normal to the flat pattern</em></p>
<p>In this case the edges will be perpendicular to the flat. Instead of being perpendicular to the sketch. This will make it easier to fabricate the part.</p>
<p><a href="http://modthemachine.typepad.com/.a/6a00e553fcbfc6883401a51199f42f970c-pi"><img alt="image" border="0" height="384" src="/assets/image_319479.jpg" style="background-image: none; padding-left: 0px; padding-right: 0px; display: inline; padding-top: 0px; border-width: 0px;" title="image" width="418" /></a></p>
<p>Cut on the left is normal to the flat</p>
<p><a href="http://modthemachine.typepad.com/.a/6a00e553fcbfc6883401a51199f438970c-pi"><img alt="image" border="0" height="299" src="/assets/image_679629.jpg" style="background-image: none; padding-left: 0px; padding-right: 0px; display: inline; padding-top: 0px; border-width: 0px;" title="image" width="381" /></a></p>
<p>How it looks in the flat pattern.</p>
<p><em>Flat pattern orientation</em></p>
<p>You now have more control over the flat pattern orientation.&#0160; In the UI there is a new dialog where you can pre define multiple orientations and switch between those and the API provides full access.</p>
<p><a href="http://modthemachine.typepad.com/.a/6a00e553fcbfc6883401a51199f448970c-pi"><img alt="image" border="0" height="338" src="/assets/image_225592.jpg" style="background-image: none; padding-left: 0px; padding-right: 0px; display: inline; padding-top: 0px; border-width: 0px;" title="image" width="435" /></a></p>
<p>&#0160;</p>
<p><strong>Enhancements in API for Assemblies</strong></p>
<p><em>BOM Structure</em></p>
<p>There are new methods to import and export the BOM customization file. The API has some support for the BOM but it does not support defining the BOM structure. A solution is to interactively create the BOM structure that you want and export it. Using the API you can import that to have a custom BOM structure.</p>
<p><em>AssemblySymmetryConstraint</em></p>
<p>The assembly symmetry constraint has a new option to control the orientation and the API was enhanced to provide that capability.</p>
<p><strong>Enhancements in API for Drawings</strong></p>
<p><em>Hole table title</em></p>
<p>In this release you have control over hole table title.</p>
<p><em>Foreshortened dimensions</em></p>
<p>Foreshortened drawing dimensions are linear, angular, and arc length dimensions.</p>
<p><a href="http://modthemachine.typepad.com/.a/6a00e553fcbfc6883401a73da5012d970d-pi"><img alt="image" border="0" height="259" src="/assets/image_246944.jpg" style="background-image: none; padding-left: 0px; padding-right: 0px; display: inline; padding-top: 0px; border-width: 0px;" title="image" width="450" /></a></p>
<p>&#0160;</p>
<p><strong>Miscellaneous Enhancements</strong></p>
<p><em>FileManager</em></p>
<p>The FileManager object now has a method <em>ReferencedDocumentCount</em> that will allow you to get the number of referenced documents. The WillOpenExpressDefault method will allow you to find out whether the input assembly will open in Express mode by default. To use these methods you just pass in a file name and it returns the information.</p>
<p>&#0160;</p>
<p>-Wayne</p>
