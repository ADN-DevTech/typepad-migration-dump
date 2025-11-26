---
layout: "post"
title: "Custom Python Scripts for AutoCAD Plant 3D &ndash; Part 2"
date: "2015-06-19 06:13:16"
author: "Augusto Goncalves"
categories:
  - "Plant3D"
original_url: "https://adndevblog.typepad.com/autocad/2015/06/custom-python-scripts-for-autocad-plant-3d-part-2.html "
typepad_basename: "custom-python-scripts-for-autocad-plant-3d-part-2"
typepad_status: "Publish"
---

<p>By <a href="http://www.autodesk.com/expert-elite/featured-members/david-wolfe">David Wolfe</a> (Contributor)</p>  <p>Check <a href="http://adndevblog.typepad.com/autocad/2015/06/custom-python-scripts-for-autocad-plant-3d-part-1.html">here the Part 1</a> of this series.</p>  <p>The first example script is taken from an older Autodesk University class (AU Python PDF). The pdf is available <a href="http://www.pdoteam.com/download/custom-python-scripts-au-1/">here</a>. Beginning on page 44, the pdf creates a sample script. The script should look like this:</p>  <pre style="border-top: rgb(204,204,204) 1px solid; border-right: rgb(204,204,204) 1px solid; background: rgb(255,255,255); word-spacing: 0px; overflow-x: auto; overflow-y: hidden; border-bottom: rgb(204,204,204) 1px solid; text-transform: none; color: rgb(0,0,0); padding-bottom: 0.5em; text-align: left; padding-top: 0.5em; font: 13px/15px consolas, &#39;DejaVu Sans Mono&#39;, &#39;Bitstream Vera Sans Mono&#39;, monospace; padding-left: 0.5em; border-left: rgb(204,204,204) 1px solid; widows: 1; letter-spacing: 0.01em; padding-right: 0.5em; text-indent: 0px; border-radius: 2px; -webkit-text-stroke-width: 0px"><span class="kn" style="font-weight: bold; color: rgb(169,13,145)">from</span> <span class="nn" style="font-weight: bold; color: rgb(0,0,0)">aqa.math</span> <span class="kn" style="font-weight: bold; color: rgb(169,13,145)">import</span> <span class="o" style="color: rgb(0,0,0)">*</span>
<span class="kn" style="font-weight: bold; color: rgb(169,13,145)">from</span> <span class="nn" style="font-weight: bold; color: rgb(0,0,0)">varmain.primitiv</span> <span class="kn" style="font-weight: bold; color: rgb(169,13,145)">import</span> <span class="o" style="color: rgb(0,0,0)">*</span>
<span class="kn" style="font-weight: bold; color: rgb(169,13,145)">from</span> <span class="nn" style="font-weight: bold; color: rgb(0,0,0)">varmain.custom</span> <span class="kn" style="font-weight: bold; color: rgb(169,13,145)">import</span> <span class="o" style="color: rgb(0,0,0)">*</span>
 
<span class="nd" style="font-weight: bold; color: rgb(0,0,0)">@activate</span><span class="p">(</span><span class="n" style="color: rgb(0,0,0)">Group</span><span class="o" style="color: rgb(0,0,0)">=</span><span class="s" style="color: rgb(196,26,22)">&quot;Support&quot;</span><span class="p">,</span>
 <span class="n" style="color: rgb(0,0,0)">TooltipShort</span><span class="o" style="color: rgb(0,0,0)">=</span><span class="s" style="color: rgb(196,26,22)">&quot;Test script&quot;</span><span class="p">,</span>
 <span class="n" style="color: rgb(0,0,0)">TooltipLong</span><span class="o" style="color: rgb(0,0,0)">=</span><span class="s" style="color: rgb(196,26,22)">&quot;This is a custom Testscript&quot;</span><span class="p">,</span> 
 <span class="n" style="color: rgb(0,0,0)">LengthUnit</span><span class="o" style="color: rgb(0,0,0)">=</span><span class="s" style="color: rgb(196,26,22)">&quot;in&quot;</span><span class="p">,</span>
 <span class="n" style="color: rgb(0,0,0)">Ports</span><span class="o" style="color: rgb(0,0,0)">=</span><span class="mi" style="color: rgb(28,1,206)">1</span><span class="p">)</span>
<span class="nd" style="font-weight: bold; color: rgb(0,0,0)">@group</span><span class="p">(</span><span class="s" style="color: rgb(196,26,22)">&quot;MainDimensions&quot;</span><span class="p">)</span>
<span class="nd" style="font-weight: bold; color: rgb(0,0,0)">@param</span><span class="p">(</span><span class="n" style="color: rgb(0,0,0)">D</span><span class="o" style="color: rgb(0,0,0)">=</span><span class="n" style="color: rgb(0,0,0)">LENGTH</span><span class="p">,</span> <span class="n" style="color: rgb(0,0,0)">TooltipShort</span><span class="o" style="color: rgb(0,0,0)">=</span><span class="s" style="color: rgb(196,26,22)">&quot;Cylinder Diameter&quot;</span><span class="p">,</span> <span class="n" style="color: rgb(0,0,0)">Ask4Dist</span><span class="o" style="color: rgb(0,0,0)">=</span><span class="bp" style="color: rgb(91,38,154)">True</span><span class="p">)</span>
<span class="nd" style="font-weight: bold; color: rgb(0,0,0)">@param</span><span class="p">(</span><span class="n" style="color: rgb(0,0,0)">L</span><span class="o" style="color: rgb(0,0,0)">=</span><span class="n" style="color: rgb(0,0,0)">LENGTH</span><span class="p">,</span> <span class="n" style="color: rgb(0,0,0)">TooltipLong</span><span class="o" style="color: rgb(0,0,0)">=</span><span class="s" style="color: rgb(196,26,22)">&quot;Length of the Cylinder&quot;</span><span class="p">)</span>
<span class="nd" style="font-weight: bold; color: rgb(0,0,0)">@param</span><span class="p">(</span><span class="n" style="color: rgb(0,0,0)">OF</span><span class="o" style="color: rgb(0,0,0)">=</span><span class="n" style="color: rgb(0,0,0)">LENGTH0</span><span class="p">)</span>
<span class="nd" style="font-weight: bold; color: rgb(0,0,0)">@group</span><span class="p">(</span><span class="n" style="color: rgb(0,0,0)">Name</span><span class="o" style="color: rgb(0,0,0)">=</span><span class="s" style="color: rgb(196,26,22)">&quot;meaningless enum&quot;</span><span class="p">)</span>
<span class="nd" style="font-weight: bold; color: rgb(0,0,0)">@param</span><span class="p">(</span><span class="n" style="color: rgb(0,0,0)">K</span><span class="o" style="color: rgb(0,0,0)">=</span><span class="n" style="color: rgb(0,0,0)">ENUM</span><span class="p">)</span>
<span class="nd" style="font-weight: bold; color: rgb(0,0,0)">@enum</span><span class="p">(</span><span class="mi" style="color: rgb(28,1,206)">1</span><span class="p">,</span> <span class="s" style="color: rgb(196,26,22)">&quot;align X&quot;</span><span class="p">)</span>
<span class="nd" style="font-weight: bold; color: rgb(0,0,0)">@enum</span><span class="p">(</span><span class="mi" style="color: rgb(28,1,206)">2</span><span class="p">,</span> <span class="s" style="color: rgb(196,26,22)">&quot;align Y&quot;</span><span class="p">)</span>
<span class="nd" style="font-weight: bold; color: rgb(0,0,0)">@enum</span><span class="p">(</span><span class="mi" style="color: rgb(28,1,206)">3</span><span class="p">,</span> <span class="s" style="color: rgb(196,26,22)">&quot;align Z&quot;</span><span class="p">)</span>
<span class="c" style="color: rgb(23,117,0); font-style: italic">#--------------------------------------------------------</span>
<span class="c" style="color: rgb(23,117,0); font-style: italic">#(arxload &quot;PnP3dACPAdapter&quot;)</span>
<span class="c" style="color: rgb(23,117,0); font-style: italic">#(testacpscript &quot;TESTSCRIPT&quot; &quot;D&quot; &quot;4.5&quot; &quot;L&quot; &quot;12&quot;)</span>
<span class="k" style="font-weight: bold; color: rgb(169,13,145)">def</span> <span class="nf" style="color: rgb(0,0,0)">TESTSCRIPT</span><span class="p">(</span><span class="n" style="color: rgb(0,0,0)">s</span><span class="p">,</span> <span class="n" style="color: rgb(0,0,0)">D</span><span class="o" style="color: rgb(0,0,0)">=</span><span class="mf" style="color: rgb(28,1,206)">80.0</span><span class="p">,</span> <span class="n" style="color: rgb(0,0,0)">L</span><span class="o" style="color: rgb(0,0,0)">=</span><span class="mf" style="color: rgb(28,1,206)">150.0</span><span class="p">,</span> <span class="n" style="color: rgb(0,0,0)">OF</span><span class="o" style="color: rgb(0,0,0)">=-</span><span class="mi" style="color: rgb(28,1,206)">1</span><span class="p">,</span> <span class="n" style="color: rgb(0,0,0)">K</span><span class="o" style="color: rgb(0,0,0)">=</span><span class="mi" style="color: rgb(28,1,206)">1</span><span class="p">,</span> <span class="o" style="color: rgb(0,0,0)">**</span><span class="n" style="color: rgb(0,0,0)">kw</span><span class="p">):</span>
    <span class="n" style="color: rgb(0,0,0)">CYLINDER</span><span class="p">(</span><span class="n" style="color: rgb(0,0,0)">s</span><span class="p">,</span> <span class="n" style="color: rgb(0,0,0)">R</span><span class="o" style="color: rgb(0,0,0)">=</span><span class="n" style="color: rgb(0,0,0)">D</span><span class="o" style="color: rgb(0,0,0)">/</span><span class="mi" style="color: rgb(28,1,206)">2</span><span class="p">,</span> <span class="n" style="color: rgb(0,0,0)">H</span><span class="o" style="color: rgb(0,0,0)">=</span><span class="n" style="color: rgb(0,0,0)">L</span><span class="p">,</span> <span class="n" style="color: rgb(0,0,0)">O</span><span class="o" style="color: rgb(0,0,0)">=</span><span class="mf" style="color: rgb(28,1,206)">0.0</span><span class="p">)</span><span class="o" style="color: rgb(0,0,0)">.</span><span class="n" style="color: rgb(0,0,0)">rotateY</span><span class="p">(</span><span class="mi" style="color: rgb(28,1,206)">90</span><span class="p">)</span></pre>

<p><strong>Scripts Parts</strong></p>

<p>There are three sections to this script, the imports section, the metadata section, and the actual shape script.&#160; In addition, you may create functions as needed for your shape generation.</p>

<p><strong>Imports</strong></p>

<p>The imports section lists other python classes and functions that may be used by the current script. In order to avoid having to tell Plant 3D what a cylinder is and how to draw it, we can simpler reference a function (CYLINDER) and use that in our scripts.&#160; The imports section is resolved by the AutoCAD Plant 3D Python interpreter, and as such, doesn’t load into other IDE’s.&#160; The imports shown here are basic ones, but others available in the actual product scripts. Some of the scripts used by Plant 3D are available upon request through ADN.</p>

<p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c7a13509970b-pi"><img title="01" style="border-left-width: 0px; border-right-width: 0px; border-bottom-width: 0px; display: inline; border-top-width: 0px" border="0" alt="01" src="/assets/image_149573.jpg" width="489" height="158" /></a> </p>

<p><strong>Metadata</strong></p>

<p>The metadata section instructs the PLANTREGISTERCUSTOMSCRIPTS command how to treat the script being registered. One of the key pieces not shown in the original Python script sample is the number of Ports.&#160; In order for the script to be used in the Spec Editor, the number of ports must be included in the metadata section.</p>

<p>Some of the other parameters referenced in the AU python documentation aren’t used. The image options will be explained in future posts.</p>

<p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb08454dda970d-pi"><img title="02" style="border-left-width: 0px; border-right-width: 0px; border-bottom-width: 0px; display: inline; border-top-width: 0px" border="0" alt="02" src="/assets/image_242022.jpg" width="491" height="158" /></a> </p>

<p><strong>Shape Scripts</strong></p>

<p>Before getting into the script portion, notice the comments above the line that starts with def.&#160; In Python, comment rows start with pound signs (#). It’s easier to test your script if you can copy the lines minus the comments to the command line to load the testing adapter, and then call the script with values.</p>

<p>In any case, the def line defines our shape function that will be called to create a script. The registration command looks for the function that matches the script file name.&#160; If you create additional functions to use within your shape script, you must locate them above the script so the script will compile. For example, to use a function called FlangeSizeCalc, you would have to define it first and then reference it lower in the script file.</p>

<p></p>

<p></p>

<p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d12a9b01970c-pi"><img title="03" style="border-left-width: 0px; border-right-width: 0px; border-bottom-width: 0px; display: inline; border-top-width: 0px" border="0" alt="03" src="/assets/image_164703.jpg" width="487" height="330" /></a> </p>

<p>Shape scripts can get complicated, so you should contact ADN to view samples that work.&#160; In addition the AU Python PDF gives more details on how to move objects and use Boolean operations to get the shape needed.</p>

<p><strong>Finished Script</strong></p>

<p>At the beginning you saw the finished script.&#160; Here is a screenshot of the final product with images being used in the spec editor.</p>

<p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c7a1353d970b-pi"><img title="fig01" style="border-left-width: 0px; border-right-width: 0px; border-bottom-width: 0px; display: inline; border-top-width: 0px" border="0" alt="fig01" src="/assets/image_138002.jpg" width="488" height="492" /></a> </p>

<p>Figure 1: script thumbnail</p>

<p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c7a13544970b-pi"><img title="fig02" style="border-left-width: 0px; border-right-width: 0px; border-bottom-width: 0px; display: inline; border-top-width: 0px" border="0" alt="fig02" src="/assets/image_957813.jpg" width="488" height="317" /></a> </p>

<p>Figure 2: dimension images and tool tips</p>

<p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb08454e09970d-pi"><img title="fig03" style="border-left-width: 0px; border-right-width: 0px; border-bottom-width: 0px; display: inline; border-top-width: 0px" border="0" alt="fig03" src="/assets/image_59105.jpg" width="493" height="271" /></a> 

  <br />Figure 3: register custom script files</p>
