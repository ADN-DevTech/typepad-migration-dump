---
layout: "post"
title: "Custom Python Scripts for AutoCAD Plant 3D &ndash; Part 5"
date: "2016-01-21 09:00:21"
author: "Augusto Goncalves"
categories:
  - "Plant3D"
original_url: "https://adndevblog.typepad.com/autocad/2016/01/custom-python-scripts-for-autocad-plant-3d-part-5.html "
typepad_basename: "custom-python-scripts-for-autocad-plant-3d-part-5"
typepad_status: "Publish"
---

<p>By <a href="http://www.autodesk.com/expert-elite/featured-members/david-wolfe">David Wolfe</a> (Contributor)</p>
<p>Get start with <a href="http://adndevblog.typepad.com/autocad/2015/06/custom-python-scripts-for-autocad-plant-3d-part-1.html">Part 1</a>, <a href="http://adndevblog.typepad.com/autocad/2015/06/custom-python-scripts-for-autocad-plant-3d-part-2.html">Part 2</a>, <a href="http://adndevblog.typepad.com/autocad/2015/06/custom-python-scripts-for-autocad-plant-3d-part-3.html">Part 3</a> and <a href="http://adndevblog.typepad.com/autocad/2015/07/custom-python-scripts-for-autocad-plant-3d-part-4.html">Part 4</a> of this series.</p>
<p><strong>Python Script Nozzles in AutoCAD Plant 3D</strong></p>
<p>This article will look at creating a nozzle to use in the nozzles catalog which AutoCAD Plant 3D equipment loads when putting nozzles on equipment.</p>
<p>The full sample is available <a href="http://adndevblog.typepad.com/files/nozflange-script.zip">at this link</a>. The script should look like this:</p>
<div>
<pre><span style="color: #408080; font-style: italic;"># Embedded file name: varmain\pipesub\cpp_f_f_.pyc</span>
<span style="font-weight: bold; color: #008000;">from</span> <span style="font-weight: bold; color: #0000ff;">aqa.math</span> <span style="font-weight: bold; color: #008000;">import</span> <span style="color: #666666;">*</span>
<span style="font-weight: bold; color: #008000;">from</span> <span style="font-weight: bold; color: #0000ff;">varmain.flangesub.cpfwo</span> <span style="font-weight: bold; color: #008000;">import</span> <span style="color: #666666;">*</span>
<span style="font-weight: bold; color: #008000;">from</span> <span style="font-weight: bold; color: #0000ff;">varmain.pipesub.cpp_util</span> <span style="font-weight: bold; color: #008000;">import</span> <span style="color: #666666;">*</span>
<span style="font-weight: bold; color: #008000;">from</span> <span style="font-weight: bold; color: #0000ff;">varmain.primitiv</span> <span style="font-weight: bold; color: #008000;">import</span> <span style="color: #666666;">*</span>
<span style="font-weight: bold; color: #008000;">from</span> <span style="font-weight: bold; color: #0000ff;">varmain.var_basic</span> <span style="font-weight: bold; color: #008000;">import</span> <span style="color: #666666;">*</span>
<span style="font-weight: bold; color: #008000;">from</span> <span style="font-weight: bold; color: #0000ff;">varmain.custom</span> <span style="font-weight: bold; color: #008000;">import</span> <span style="color: #666666;">*</span>

<span style="color: #aa22ff;">@activate</span>(Group<span style="color: #666666;">=</span><span style="color: #ba2121;">"StraightNozzle,Nozzle"</span>, TooltipShort<span style="color: #666666;">=</span><span style="color: #ba2121;">"Long Weld Neck"</span>, TooltipLong<span style="color: #666666;">=</span><span style="color: #ba2121;">"Long Weld Neck"</span>, LengthUnit<span style="color: #666666;">=</span><span style="color: #ba2121;">"in"</span>, Ports<span style="color: #666666;">=</span><span style="color: #ba2121;">"2"</span>)
<span style="color: #aa22ff;">@group</span>(<span style="color: #ba2121;">"MainDimensions"</span>)
<span style="color: #aa22ff;">@param</span>(D10<span style="color: #666666;">=</span>LENGTH, TooltipShort<span style="color: #666666;">=</span><span style="color: #ba2121;">"Nominal Outside Diameter"</span>,TooltipLong<span style="color: #666666;">=</span><span style="color: #ba2121;">"Nominal Outside Diameter"</span>)
<span style="color: #aa22ff;">@param</span>(L1<span style="color: #666666;">=</span>LENGTH, TooltipShort<span style="color: #666666;">=</span><span style="color: #ba2121;">"Overall Length."</span>, TooltipLong<span style="color: #666666;">=</span><span style="color: #ba2121;">"Overall Length"</span>)
<span style="color: #aa22ff;">@param</span>(L2<span style="color: #666666;">=</span>LENGTH, TooltipShort<span style="color: #666666;">=</span><span style="color: #ba2121;">"Hub Length"</span>, TooltipLong<span style="color: #666666;">=</span><span style="color: #ba2121;">"Length of flange with weld neck."</span>)
<span style="color: #aa22ff;">@param</span>(OF<span style="color: #666666;">=</span>LENGTH, TooltipLong<span style="color: #666666;">=</span><span style="color: #ba2121;">"Length between connection points."</span>)
<span style="color: #aa22ff;">@param</span>(B1<span style="color: #666666;">=</span>LENGTH, TooltipLong<span style="color: #666666;">=</span><span style="color: #ba2121;">"Flange Thickness"</span>)
<span style="color: #aa22ff;">@param</span>(D12<span style="color: #666666;">=</span>LENGTH, TooltipLong<span style="color: #666666;">=</span><span style="color: #ba2121;">"Flange OD Width"</span>)
<span style="color: #aa22ff;">@param</span>(D13<span style="color: #666666;">=</span>LENGTH, TooltipLong<span style="color: #666666;">=</span><span style="color: #ba2121;">"OD of Weld neck"</span>)

<span style="font-weight: bold; color: #008000;">def</span> <span style="color: #0000ff;">nozflange</span>(s, D10 <span style="color: #666666;">=</span> <span style="color: #666666;">4.5</span>, L1 <span style="color: #666666;">=</span> <span style="color: #666666;">9</span>, L2 <span style="color: #666666;">=</span> <span style="color: #666666;">3</span>, B1 <span style="color: #666666;">=</span> <span style="color: #666666;">0.94</span>, D12 <span style="color: #666666;">=</span> <span style="color: #666666;">9</span>, D13 <span style="color: #666666;">=</span> <span style="color: #666666;">5.31</span>, OF <span style="color: #666666;">=</span> <span style="color: #666666;">-1.0</span>,<span style="color: #666666;">**</span>kw):
    xH1 <span style="color: #666666;">=</span> D12 <span style="color: #666666;">+</span> OF <span style="color: #666666;">*</span> <span style="color: #666666;">2.0</span>
    <span style="font-weight: bold; color: #008000;">if</span> D12 <span style="color: #666666;">&lt;</span> xH1:
        D12 <span style="color: #666666;">=</span> xH1
    W1 <span style="color: #666666;">=</span> D12
    DI <span style="color: #666666;">=</span> <span style="color: #666666;">.25</span>
    DO <span style="color: #666666;">=</span> <span style="color: #666666;">.25</span>
    <span style="color: #408080; font-style: italic;">#create the nozzle extension</span>
    <span style="font-weight: bold; color: #008000;">if</span> L1 <span style="color: #666666;">&gt;</span> <span style="color: #666666;">0</span>:
        oTH <span style="color: #666666;">=</span> CYLINDER(s, R<span style="color: #666666;">=</span>D10 <span style="color: #666666;">/</span> <span style="color: #666666;">2.0</span>, H<span style="color: #666666;">=</span>L1, O<span style="color: #666666;">=</span>D10 <span style="color: #666666;">-</span> (DO <span style="color: #666666;">/</span> <span style="color: #666666;">2.0</span>))<span style="color: #666666;">.</span>rotateY(<span style="color: #666666;">90.0</span>)
    <span style="font-weight: bold; color: #008000;">else</span>:
        oTH <span style="color: #666666;">=</span> <span style="color: #008000;">None</span>
        L1 <span style="color: #666666;">=</span> <span style="color: #666666;">-</span>L1
    oF1 <span style="color: #666666;">=</span> CPFWO(s, L<span style="color: #666666;">=</span>L2, B<span style="color: #666666;">=</span>B1, D1<span style="color: #666666;">=</span>D12, D2<span style="color: #666666;">=</span>D10, D3<span style="color: #666666;">=</span>D13, W<span style="color: #666666;">=</span>W1, OF<span style="color: #666666;">=</span>OF)
    <span style="font-weight: bold; color: #008000;">if</span> oTH:
        oTH<span style="color: #666666;">.</span>uniteWith(oF1)
        oF1<span style="color: #666666;">.</span>erase()
    <span style="font-weight: bold; color: #008000;">if</span> W1 <span style="color: #666666;">&lt;=</span> D12:
        WA1 <span style="color: #666666;">=</span> <span style="color: #666666;">0.0</span>
    <span style="font-weight: bold; color: #008000;">else</span>:
        WA1 <span style="color: #666666;">=</span> <span style="color: #666666;">90.0</span>
<span style="color: #408080; font-style: italic;">#set port points</span>
    s<span style="color: #666666;">.</span>setPoint((<span style="color: #666666;">0.0</span>, <span style="color: #666666;">0.0</span>, <span style="color: #666666;">0.0</span>), (<span style="color: #666666;">-1.0</span>, <span style="color: #666666;">0.0</span>, <span style="color: #666666;">0.0</span>), WA1, <span style="color: #666666;">0.0</span>, L2)
    s<span style="color: #666666;">.</span>setPoint((L1, <span style="color: #666666;">0.0</span>, <span style="color: #666666;">0.0</span>), (<span style="color: #666666;">1.0</span>, <span style="color: #666666;">0.0</span>, <span style="color: #666666;">0.0</span>), <span style="color: #666666;">0</span>, <span style="color: #666666;">0.0</span>, L2)
    <span style="font-weight: bold; color: #008000;">return</span></pre>
</div>
<p><strong>Metadata</strong></p>
<p>First we have the includes, this script the includes section is particular important because we are using some of the stock flange scripts (flangesub, pipesub).</p>
<div>
<pre style="margin: 0px; line-height: 125%;"><span style="color: #408080; font-style: italic;"># Embedded file name: varmain\pipesub\cpp_f_f_.pyc</span>
<span style="font-weight: bold; color: #008000;">from</span> <span style="font-weight: bold; color: #0000ff;">aqa.math</span> <span style="font-weight: bold; color: #008000;">import</span> <span style="color: #666666;">*</span>
<span style="font-weight: bold; color: #008000;">from</span> <span style="font-weight: bold; color: #0000ff;">varmain.flangesub.cpfwo</span> <span style="font-weight: bold; color: #008000;">import</span> <span style="color: #666666;">*</span>
<span style="font-weight: bold; color: #008000;">from</span> <span style="font-weight: bold; color: #0000ff;">varmain.pipesub.cpp_util</span> <span style="font-weight: bold; color: #008000;">import</span> <span style="color: #666666;">*</span>
<span style="font-weight: bold; color: #008000;">from</span> <span style="font-weight: bold; color: #0000ff;">varmain.primitiv</span> <span style="font-weight: bold; color: #008000;">import</span> <span style="color: #666666;">*</span>
<span style="font-weight: bold; color: #008000;">from</span> <span style="font-weight: bold; color: #0000ff;">varmain.var_basic</span> <span style="font-weight: bold; color: #008000;">import</span> <span style="color: #666666;">*</span>
<span style="font-weight: bold; color: #008000;">from</span> <span style="font-weight: bold; color: #0000ff;">varmain.custom</span> <span style="font-weight: bold; color: #008000;">import</span> <span style="color: #666666;">*</span></pre>
</div>
<p>Second is the section declaring our tooltips and parameters.&nbsp; One the first line here, because we want the script listed under the StraightNozzle and Nozzle categories, we include both.</p>
<div>
<pre style="margin: 0px; line-height: 125%;"><span style="color: #aa22ff;">@activate</span>(Group<span style="color: #666666;">=</span><span style="color: #ba2121;">"StraightNozzle,Nozzle"</span>, TooltipShort<span style="color: #666666;">=</span><span style="color: #ba2121;">"Long Weld Neck"</span>, TooltipLong<span style="color: #666666;">=</span><span style="color: #ba2121;">"Long Weld Neck"</span>, LengthUnit<span style="color: #666666;">=</span><span style="color: #ba2121;">"in"</span>, Ports<span style="color: #666666;">=</span><span style="color: #ba2121;">"2"</span>)
<span style="color: #aa22ff;">@group</span>(<span style="color: #ba2121;">"MainDimensions"</span>)
<span style="color: #aa22ff;">@param</span>(D10<span style="color: #666666;">=</span>LENGTH, TooltipShort<span style="color: #666666;">=</span><span style="color: #ba2121;">"Nominal Outside Diameter"</span>,TooltipLong<span style="color: #666666;">=</span><span style="color: #ba2121;">"Nominal Outside Diameter"</span>)
<span style="color: #aa22ff;">@param</span>(L1<span style="color: #666666;">=</span>LENGTH, TooltipShort<span style="color: #666666;">=</span><span style="color: #ba2121;">"Overall Length."</span>, TooltipLong<span style="color: #666666;">=</span><span style="color: #ba2121;">"Overall Length"</span>)
<span style="color: #aa22ff;">@param</span>(L2<span style="color: #666666;">=</span>LENGTH, TooltipShort<span style="color: #666666;">=</span><span style="color: #ba2121;">"Hub Length"</span>, TooltipLong<span style="color: #666666;">=</span><span style="color: #ba2121;">"Length of flange with weld neck."</span>)
<span style="color: #aa22ff;">@param</span>(OF<span style="color: #666666;">=</span>LENGTH, TooltipLong<span style="color: #666666;">=</span><span style="color: #ba2121;">"Length between connection points."</span>)
<span style="color: #aa22ff;">@param</span>(B1<span style="color: #666666;">=</span>LENGTH, TooltipLong<span style="color: #666666;">=</span><span style="color: #ba2121;">"Flange Thickness"</span>)
<span style="color: #aa22ff;">@param</span>(D12<span style="color: #666666;">=</span>LENGTH, TooltipLong<span style="color: #666666;">=</span><span style="color: #ba2121;">"Flange OD Width"</span>)
<span style="color: #aa22ff;">@param</span>(D13<span style="color: #666666;">=</span>LENGTH, TooltipLong<span style="color: #666666;">=</span><span style="color: #ba2121;">"OD of Weld neck"</span>)</pre>
</div>
<p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c80ac6e9970b-pi"><img style="display: inline; border: 0px;" title="image1" src="/assets/image_21895.jpg" alt="image1" width="361" height="264" border="0" /></a>&nbsp; <br />StraightNozzles</p>
<p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c80ac6f3970b-pi"><img style="display: inline; border: 0px;" title="image2" src="/assets/image_447195.jpg" alt="image2" width="360" height="264" border="0" /></a>&nbsp; <br />Nozzle class shown by clicking advanced shape options and selecting nozzle.</p>
<p><strong>Testing</strong></p>
<p>To test the script, I made a tool palette icon with this in the macro portion: <br /><em>^C^CPLANTREGISTERCUSTOMSCRIPTS;^C^C(command "arx" "l" "Pnp3dacpadapter");(testacpscript "nozflange");</em></p>
<p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d194d5c3970c-pi"><img style="display: inline; border: 0px;" title="image3" src="/assets/image_445291.jpg" alt="image3" width="363" height="397" border="0" /></a></p>
<p>Once the script is tested, we need to be able to test it in a catalog.&nbsp; For Plant 3D, all of the nozzles are read from a shared content folder path. You can find the path by running the spec editor as administrator, and then going to Tools &gt; Modify Shared Content folder.</p>
<p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c80ac703970b-pi"><img style="display: inline; border: 0px;" title="image4" src="/assets/image_770641.jpg" alt="image4" width="357" height="125" border="0" /></a></p>
<p>The default path is C:\AutoCAD Plant 3D 2016 Content\.</p>
<p>The default path for the nozzle catalog is "C:\AutoCAD Plant 3D 2016 Content\CPak Common\NOZZLE Catalog.acat"</p>
<p>To open the nozzle catalog, switch to the Catalogs tab, and go to open, and then browse to it.&nbsp; Once in the catalog you can click create new component and select your script. Fill in some sample data and save your catalog component and the catalog. Note that for flanged nozzles, your end type and pressure class must be set.</p>
<p>In a project drawing, create a piece of equipment, and then pick your nozzle.</p>
<p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb08af933d970d-pi"><img style="display: inline; border: 0px;" title="image5" src="/assets/image_928242.jpg" alt="image5" width="361" height="194" border="0" /></a></p>
<p>You can change the nozzle orientation in the Change location tab.</p>
<p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c80ac712970b-pi"><img style="display: inline; border: 0px;" title="image6" src="/assets/image_821269.jpg" alt="image6" width="360" height="216" border="0" /></a></p>
<p>Note that if you want the user to override the nozzle length, you should use the L parameter in your script. In this case (using L1), for a long weld neck, the catalog data will drive the length, and the user won’t be able to change it.</p>
<p><strong>Deployment</strong></p>
<p>Because you can’t directly modify the nozzle catalog on a user’s computer without overwriting it, you should deploy a folder (eg CPak Custom Nozzles) with your nozzle catalog. The user then will be responsible to add the nozzles. Another option would be to deploy two catalogs, one with the stock nozzles + your custom nozzles, and one with just your custom nozzles.&nbsp; You could write an installer that lets the user pick which of the two installs they want.&nbsp; In either case, you should use a property like Design Pressure Factor to include unique information so that they can quickly filter to locate your nozzles.</p>
