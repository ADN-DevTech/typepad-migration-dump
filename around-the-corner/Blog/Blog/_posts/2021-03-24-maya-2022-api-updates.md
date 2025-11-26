---
layout: "post"
title: "Maya 2022 API Updates"
date: "2021-03-24 17:17:53"
author: "Lanh Hong"
categories:
  - "Maya"
original_url: "https://around-the-corner.typepad.com/adn/2021/03/maya-2022-api-updates.html "
typepad_basename: "maya-2022-api-updates"
typepad_status: "Publish"
---

<h1 class="code-line" data-line="0" id="maya-2022-api-update-guide">Maya 2022 API Update guide</h1>
<p class="code-line" data-line="2">The guide is based on What&#39;s New in the Maya Devkit in Maya 2022 with some extra info. Check the Maya documentation for the most up to date information.</p>
<h2 class="code-line" data-line="4" id="build-environment">Build environment:</h2>
<p class="code-line" data-line="6">The build environments have been upgraded. Mac still uses Mojave 10.14.x with Xcode 10.x, and Windows remains Windows 10 with an upgrade to VS2019.&#0160;<strong>Linux has been updated to CentOS/RHEL 7.6 or above and DTS 9.1</strong>.</p>
<table border="1" style="border-collapse: collapse; width: 100%; height: 72px;">
<tbody>
<tr style="height: 18px;">
<td style="width: 33.3333%; height: 18px; text-align: center;"><span style="font-family: verdana, geneva; font-size: 11pt;"><strong>Platform</strong></span></td>
<td style="width: 33.3333%; height: 18px; text-align: center;"><span style="font-family: verdana, geneva; font-size: 11pt;"><strong>OS</strong></span></td>
<td style="width: 33.3333%; height: 18px; text-align: center;"><span style="font-family: verdana, geneva; font-size: 11pt;"><strong>Compiler</strong></span></td>
</tr>
<tr style="height: 18px;">
<td style="width: 33.3333%; height: 18px; text-align: center;"><span style="font-family: verdana, geneva; font-size: 11pt;">Windows</span></td>
<td style="width: 33.3333%; height: 18px; text-align: center;"><span style="font-family: verdana, geneva; font-size: 11pt;">Windows 10 x64</span></td>
<td style="width: 33.3333%; height: 18px; text-align: center;"><span style="font-family: verdana, geneva; font-size: 11pt;">Visual Studio 2019</span></td>
</tr>
<tr style="height: 18px;">
<td style="width: 33.3333%; height: 18px; text-align: center;"><span style="font-family: verdana, geneva; font-size: 11pt;">Mac</span></td>
<td style="width: 33.3333%; height: 18px; text-align: center;"><span style="font-family: verdana, geneva; font-size: 11pt;">Mojave 10.14.x</span></td>
<td style="width: 33.3333%; height: 18px; text-align: center;"><span style="font-family: verdana, geneva; font-size: 11pt;">XCode 10.2.1</span></td>
</tr>
<tr style="height: 18px;">
<td style="width: 33.3333%; height: 18px; text-align: center;"><span style="font-family: verdana, geneva; font-size: 11pt;">Linux</span></td>
<td style="width: 33.3333%; height: 18px; text-align: center;"><span style="font-family: verdana, geneva; font-size: 11pt;">CentOS/RHEL 7.6+ and 8.2+</span><br /><span style="font-family: verdana, geneva; font-size: 11pt;">(8.0 and 8.1 are missing some system packages)</span></td>
<td style="width: 33.3333%; height: 18px; text-align: center;"><span style="font-family: verdana, geneva; font-size: 11pt;">gcc 9.3.1 (DTS 9.1)</span></td>
</tr>
</tbody>
</table>
<h2 class="code-line" data-line="14" id="developer-resources">Developer resources</h2>
<ul>
<li class="code-line" data-line="16">
<p class="code-line" data-line="16">The Maya docs are being updated with API changes. You can find the most up to date information for the API in the&#0160;<a data-href="https://help.autodesk.com/view/MAYAUL/2022/ENU/?guid=Maya_SDK_What_s_New_What_s_Changed_html" href="https://help.autodesk.com/view/MAYAUL/2022/ENU/?guid=Maya_SDK_What_s_New_What_s_Changed_html" title="https://help.autodesk.com/view/MAYAUL/2022/ENU/?guid=Maya_SDK_What_s_New_What_s_Changed_html">What&#39;s New / What&#39;s Changed</a>&#0160;section.</p>
</li>
<li class="code-line" data-line="18">
<p class="code-line" data-line="18">For those who are part of the Feedback community, there is an additional resource to help determine if your plug-in needs to be recompiled for Maya 2022. The product team is posting&#0160;<a data-href="https://feedback.autodesk.com/project/forum/thread.html?cap=037743fd817049f18d3a9e014e771fd6&amp;forid=%7Bf93ee204-9670-4c0e-ae43-c67b5008e28f%7D&amp;topid=%7BD7D47D19-B419-4294-B524-9B215517BCD5%7D" href="https://feedback.autodesk.com/project/forum/thread.html?cap=037743fd817049f18d3a9e014e771fd6&amp;forid=%7Bf93ee204-9670-4c0e-ae43-c67b5008e28f%7D&amp;topid=%7BD7D47D19-B419-4294-B524-9B215517BCD5%7D" title="https://feedback.autodesk.com/project/forum/thread.html?cap=037743fd817049f18d3a9e014e771fd6&amp;forid=%7Bf93ee204-9670-4c0e-ae43-c67b5008e28f%7D&amp;topid=%7BD7D47D19-B419-4294-B524-9B215517BCD5%7D">API compatibility reports</a>&#0160;in the Feedback forum for every PR release so check it out.</p>
</li>
</ul>
<h2 class="code-line" data-line="20" id="changes-in-maya-2022-at-a-glance">Changes in Maya 2022 at a glance</h2>
<ul>
<li class="code-line" data-line="21">
<p class="code-line" data-line="21"><a data-href="https://help.autodesk.com/view/MAYAUL/2022/ENU/?guid=Maya_SDK_What_s_New_What_s_Changed_2022_Whats_New_in_API_html#python-3-support" href="https://help.autodesk.com/view/MAYAUL/2022/ENU/?guid=Maya_SDK_What_s_New_What_s_Changed_2022_Whats_New_in_API_html#python-3-support" title="https://help.autodesk.com/view/MAYAUL/2022/ENU/?guid=Maya_SDK_What_s_New_What_s_Changed_2022_Whats_New_in_API_html#python-3-support">Python 3 support</a>&#0160;- Python 3 support is now available for all OS. Python 2 is still supported on Linux and Windows only.</p>
</li>
<li class="code-line" data-line="23">
<p class="code-line" data-line="23"><a data-href="https://help.autodesk.com/view/MAYAUL/2022/ENU/?guid=Maya_SDK_What_s_New_What_s_Changed_2022_Whats_New_in_API_html#notable-changes-to-third-party-components" href="https://help.autodesk.com/view/MAYAUL/2022/ENU/?guid=Maya_SDK_What_s_New_What_s_Changed_2022_Whats_New_in_API_html#notable-changes-to-third-party-components" title="https://help.autodesk.com/view/MAYAUL/2022/ENU/?guid=Maya_SDK_What_s_New_What_s_Changed_2022_Whats_New_in_API_html#notable-changes-to-third-party-components">Changes to third party components</a>&#0160;- Qt and PySide updated to version 5.15.2. Updates made to Alembic and OpenEXR libraries. Qt Creator Tools will not be distributed in their entirety. The pysideuic module has been deprecated by QtC and replaced by the pyside2-uic script.</p>
</li>
<li class="code-line" data-line="25">
<p class="code-line" data-line="25"><a data-href="https://help.autodesk.com/view/MAYAUL/2022/ENU/?guid=Maya_SDK_What_s_New_What_s_Changed_2022_Whats_New_in_API_html#the-maya-script-editor-now-fully-supports-unicode-characters" href="https://help.autodesk.com/view/MAYAUL/2022/ENU/?guid=Maya_SDK_What_s_New_What_s_Changed_2022_Whats_New_in_API_html#the-maya-script-editor-now-fully-supports-unicode-characters" title="https://help.autodesk.com/view/MAYAUL/2022/ENU/?guid=Maya_SDK_What_s_New_What_s_Changed_2022_Whats_New_in_API_html#the-maya-script-editor-now-fully-supports-unicode-characters">Unicode characters support in Script Editor</a>&#0160;- Scripts are now saved using UTF-8 encoding as prescibed by PEP-263.</p>
</li>
<li class="code-line" data-line="27">
<p class="code-line" data-line="27"><a data-href="https://help.autodesk.com/view/MAYAUL/2022/ENU/?guid=Maya_SDK_What_s_New_What_s_Changed_2022_Whats_New_in_API_html#new-environment-variables" href="https://help.autodesk.com/view/MAYAUL/2022/ENU/?guid=Maya_SDK_What_s_New_What_s_Changed_2022_Whats_New_in_API_html#new-environment-variables" title="https://help.autodesk.com/view/MAYAUL/2022/ENU/?guid=Maya_SDK_What_s_New_What_s_Changed_2022_Whats_New_in_API_html#new-environment-variables">New environment variables</a>&#0160;- Two new environment variables added to control what is the output to stderr and stdout:&#0160;<code>MAYA_BATCH_STDOUT_LOGGING_LEVEL</code>&#0160;and&#0160;<code>MAYA_BATCH_STDERR_LOGGING_LEVEL.</code></p>
</li>
<li class="code-line" data-line="29">
<p class="code-line" data-line="29"><a data-href="https://help.autodesk.com/view/MAYAUL/2022/ENU/?guid=Maya_SDK_What_s_New_What_s_Changed_2022_Whats_New_in_API_html#opting-out-of-analytics-when-starting-maya-in-batch-mode" href="https://help.autodesk.com/view/MAYAUL/2022/ENU/?guid=Maya_SDK_What_s_New_What_s_Changed_2022_Whats_New_in_API_html#opting-out-of-analytics-when-starting-maya-in-batch-mode" title="https://help.autodesk.com/view/MAYAUL/2022/ENU/?guid=Maya_SDK_What_s_New_What_s_Changed_2022_Whats_New_in_API_html#opting-out-of-analytics-when-starting-maya-in-batch-mode">Opting out of analytics in batch mode</a>&#0160;- Analytics will run by default when Maya starts in batch mode. Set&#0160;<code>MAYA_DISABLE_ADP</code>&#0160;to 1 to opt out of analytics.</p>
</li>
<li class="code-line" data-line="31">
<p class="code-line" data-line="31"><a data-href="https://help.autodesk.com/view/MAYAUL/2022/ENU/?guid=Maya_SDK_What_s_New_What_s_Changed_2022_Whats_New_in_API_html#autoloadonce-option-added-to-packagecontent-xml" href="https://help.autodesk.com/view/MAYAUL/2022/ENU/?guid=Maya_SDK_What_s_New_What_s_Changed_2022_Whats_New_in_API_html#autoloadonce-option-added-to-packagecontent-xml" title="https://help.autodesk.com/view/MAYAUL/2022/ENU/?guid=Maya_SDK_What_s_New_What_s_Changed_2022_Whats_New_in_API_html#autoloadonce-option-added-to-packagecontent-xml">AutoLoadOnce added to PackageContent.xml</a>&#0160;- A new option,&#0160;<code>AutoLoadOnce</code>&#0160;has been added to&#0160;<code>PackageContents.xml</code>&#0160;that is used when distributing plug-ins using the App Store format.</p>
</li>
<li class="code-line" data-line="33">
<p class="code-line" data-line="33"><a data-href="https://help.autodesk.com/view/MAYAUL/2022/ENU/?guid=Maya_SDK_What_s_New_What_s_Changed_2022_Whats_New_in_API_html#api-changes" href="https://help.autodesk.com/view/MAYAUL/2022/ENU/?guid=Maya_SDK_What_s_New_What_s_Changed_2022_Whats_New_in_API_html#api-changes" title="https://help.autodesk.com/view/MAYAUL/2022/ENU/?guid=Maya_SDK_What_s_New_What_s_Changed_2022_Whats_New_in_API_html#api-changes">API changes</a>&#0160;- New smart pointer, classes, methods, and attributes have been added along with some changes made to the API.</p>
</li>
<li class="code-line" data-line="35">
<p class="code-line" data-line="35"><a data-href="https://help.autodesk.com/view/MAYAUL/2022/ENU/?guid=Maya_SDK_What_s_New_What_s_Changed_2022_Whats_New_in_API_html#changes-to-devkit-examples" href="https://help.autodesk.com/view/MAYAUL/2022/ENU/?guid=Maya_SDK_What_s_New_What_s_Changed_2022_Whats_New_in_API_html#changes-to-devkit-examples" title="https://help.autodesk.com/view/MAYAUL/2022/ENU/?guid=Maya_SDK_What_s_New_What_s_Changed_2022_Whats_New_in_API_html#changes-to-devkit-examples">Changes to Devkit examples</a>&#0160;- Several examples have new node IDs. Python examples have been renamed and moved. New examples added to the Devkit and some have been updated.</p>
</li>
<li class="code-line" data-line="37">
<p class="code-line" data-line="37"><a data-href="https://help.autodesk.com/view/MAYAUL/2022/ENU/?guid=Maya_SDK_What_s_New_What_s_Changed_2022_Whats_New_in_API_html#scripting-changes" href="https://help.autodesk.com/view/MAYAUL/2022/ENU/?guid=Maya_SDK_What_s_New_What_s_Changed_2022_Whats_New_in_API_html#scripting-changes" title="https://help.autodesk.com/view/MAYAUL/2022/ENU/?guid=Maya_SDK_What_s_New_What_s_Changed_2022_Whats_New_in_API_html#scripting-changes">Scripting changes</a>&#0160;- Some commands now accept UFE path strings. Two new commands:&#0160;<code>FBXExtPlugin</code>&#0160;and&#0160;<code>mimicManipulation</code>. Several new flags and options were added along with changes to some.</p>
</li>
</ul>
<h2 class="code-line" data-line="41" id="python-updates">Python Updates</h2>
<h3 class="code-line" data-line="43" id="maya-starts-up-in-python-3-by-default">Maya starts up in Python 3 by default</h3>
<p class="code-line" data-line="45"><strong>Note: Maya on Windows and Linux supports both Python 2 and Python 3. Maya on macOS only supports Python 3.</strong></p>
<p class="code-line" data-line="47">To ways to start Maya in Python 2 mode:</p>
<ol>
<li class="code-line" data-line="49">Start Maya from the command line with&#0160;<code>-pythonver 2</code>. For example, run&#0160;<code>maya.exe -pythonver 2</code>&#0160;on command window.</li>
<li class="code-line" data-line="50">Set the environment variable&#0160;<code>MAYA_PYTHON_VERSION</code>&#0160;to 2. For example, set&#0160;<code>MAYA_PYTHON_VERSION</code>&#0160;to 2 in the command window, then start Maya from the same command window.</li>
</ol>
<h3 class="code-line" data-line="53" id="renderexe-and-rendersh-can-now-run-in-either-python-2-or-python-3-mode">Render.exe and Render.sh can now run in either Python 2 or Python 3 mode</h3>
<ul>
<li class="code-line" data-line="55">Python 2: Run&#0160;<code>Render.exe -pythonver 2</code>&#0160;on Windows or&#0160;<code>./Render -pythonver 2</code></li>
<li class="code-line" data-line="56">Python 3: Run&#0160;<code>Render.exe -pythonver 3</code>&#0160;on Windows or&#0160;<code>./Render -pythonver 3</code>.</li>
</ul>
<h3 class="code-line" data-line="59" id="new-pip-support">New pip support</h3>
<p class="code-line" data-line="61"><code>mayapy</code>&#0160;now supports installing Python 3 modules with pip. To use pip with&#0160;<code>mayapy</code>, use the follow command:</p>
<ul>
<li class="code-line" data-line="63">Linux and macOS:&#0160;<code>./mayapy -m pip &lt;command&gt;</code></li>
<li class="code-line" data-line="64">Windows:&#0160;<code>mayapy -m pip &lt;command&gt;</code></li>
</ul>
<h2 class="code-line" data-line="67" id="api-updates">API Updates</h2>
<ul>
<li class="code-line" data-line="69">
<p class="code-line" data-line="69"><a data-href="https://help.autodesk.com/view/MAYAUL/2022/ENU/?guid=Maya_SDK_What_s_New_What_s_Changed_2022_Whats_New_in_API_html#new-smart-pointer" href="https://help.autodesk.com/view/MAYAUL/2022/ENU/?guid=Maya_SDK_What_s_New_What_s_Changed_2022_Whats_New_in_API_html#new-smart-pointer" title="https://help.autodesk.com/view/MAYAUL/2022/ENU/?guid=Maya_SDK_What_s_New_What_s_Changed_2022_Whats_New_in_API_html#new-smart-pointer">New smart pointer</a>&#0160;-&#0160;<code>MSharedPtr&lt;&gt;</code>&#0160;has been added to Maya. They are used in the Maya API to make object ownership explicit.</p>
</li>
<li class="code-line" data-line="71">
<p class="code-line" data-line="71"><a data-href="https://help.autodesk.com/view/MAYAUL/2022/ENU/?guid=Maya_SDK_What_s_New_What_s_Changed_2022_Whats_New_in_API_html#new-validator-for-decimal-indicators" href="https://help.autodesk.com/view/MAYAUL/2022/ENU/?guid=Maya_SDK_What_s_New_What_s_Changed_2022_Whats_New_in_API_html#new-validator-for-decimal-indicators" title="https://help.autodesk.com/view/MAYAUL/2022/ENU/?guid=Maya_SDK_What_s_New_What_s_Changed_2022_Whats_New_in_API_html#new-validator-for-decimal-indicators">New validator for decimal indicators</a>&#0160;-&#0160;<code>MayaQclocaleDoubleValidator</code>&#0160;was added to&#0160;<code>maya.internal.common.qt.doubleValidator</code>&#0160;to accomodate locales where decimal values use commas instead of periods.</p>
</li>
<li class="code-line" data-line="73">
<p class="code-line" data-line="73"><a data-href="https://help.autodesk.com/view/MAYAUL/2022/ENU/?guid=Maya_SDK_What_s_New_What_s_Changed_2022_Whats_New_in_API_html#new-class-in-openmayaanim-module" href="https://help.autodesk.com/view/MAYAUL/2022/ENU/?guid=Maya_SDK_What_s_New_What_s_Changed_2022_Whats_New_in_API_html#new-class-in-openmayaanim-module" title="https://help.autodesk.com/view/MAYAUL/2022/ENU/?guid=Maya_SDK_What_s_New_What_s_Changed_2022_Whats_New_in_API_html#new-class-in-openmayaanim-module">New class in OpenMayaAnim module</a>&#0160;-&#0160;<code>MFalloffContext</code></p>
</li>
<li class="code-line" data-line="75">
<p class="code-line" data-line="75"><a data-href="https://help.autodesk.com/view/MAYAUL/2022/ENU/?guid=Maya_SDK_What_s_New_What_s_Changed_2022_Whats_New_in_API_html#new-class-added-as-part-of-the-gpu-deformer-upgrades" href="https://help.autodesk.com/view/MAYAUL/2022/ENU/?guid=Maya_SDK_What_s_New_What_s_Changed_2022_Whats_New_in_API_html#new-class-added-as-part-of-the-gpu-deformer-upgrades" title="https://help.autodesk.com/view/MAYAUL/2022/ENU/?guid=Maya_SDK_What_s_New_What_s_Changed_2022_Whats_New_in_API_html#new-class-added-as-part-of-the-gpu-deformer-upgrades">New class added as part of the GPU deformer upgrades</a>&#0160;-&#0160;<code>MFnFloatVectorArrayData</code>&#0160;has been added to provide access to&#0160;<code>MFloatVectorArray</code>.</p>
</li>
<li class="code-line" data-line="77">
<p class="code-line" data-line="77"><a data-href="https://help.autodesk.com/view/MAYAUL/2022/ENU/?guid=Maya_SDK_What_s_New_What_s_Changed_2022_Whats_New_in_API_html#new-class-and-methods-to-perform-mrenderitem-specific-evaluations" href="https://help.autodesk.com/view/MAYAUL/2022/ENU/?guid=Maya_SDK_What_s_New_What_s_Changed_2022_Whats_New_in_API_html#new-class-and-methods-to-perform-mrenderitem-specific-evaluations" title="https://help.autodesk.com/view/MAYAUL/2022/ENU/?guid=Maya_SDK_What_s_New_What_s_Changed_2022_Whats_New_in_API_html#new-class-and-methods-to-perform-mrenderitem-specific-evaluations">New class and methods to perform MRenderItem-specific evaluations</a>&#0160;-&#0160;<code>MPxViewportComputeItem</code>,&#0160;<code>MRenderItem::addViewportComputeItem()</code>, and&#0160;<code>MRenderItem::viewportComputeItem()</code>.</p>
</li>
<li class="code-line" data-line="79">
<p class="code-line" data-line="79"><a data-href="https://help.autodesk.com/view/MAYAUL/2022/ENU/?guid=Maya_SDK_What_s_New_What_s_Changed_2022_Whats_New_in_API_html#new-syntax-for-python-iterators" href="https://help.autodesk.com/view/MAYAUL/2022/ENU/?guid=Maya_SDK_What_s_New_What_s_Changed_2022_Whats_New_in_API_html#new-syntax-for-python-iterators" title="https://help.autodesk.com/view/MAYAUL/2022/ENU/?guid=Maya_SDK_What_s_New_What_s_Changed_2022_Whats_New_in_API_html#new-syntax-for-python-iterators">New syntax for Python iterators</a>&#0160;- The newly-added syntax calls&#0160;<code>next</code>&#0160;with the iterator as the parameter:&#0160;<code>next(iterator)</code>.</p>
</li>
<li class="code-line" data-line="81">
<p class="code-line" data-line="81"><a data-href="https://help.autodesk.com/view/MAYAUL/2022/ENU/?guid=Maya_SDK_What_s_New_What_s_Changed_2022_Whats_New_in_API_html#changed-behavior-for-mglobal-setactiveselectionlist-" href="https://help.autodesk.com/view/MAYAUL/2022/ENU/?guid=Maya_SDK_What_s_New_What_s_Changed_2022_Whats_New_in_API_html#changed-behavior-for-mglobal-setactiveselectionlist-" title="https://help.autodesk.com/view/MAYAUL/2022/ENU/?guid=Maya_SDK_What_s_New_What_s_Changed_2022_Whats_New_in_API_html#changed-behavior-for-mglobal-setactiveselectionlist-">Changed behavior for MGlobal::setActiveSelectionList()</a>&#0160;-&#0160;<code>MGlobal::setActiveSelectionList()</code>&#0160;will behave as expected: when it is called with an empty selection list, it will delete the list if&#0160;<code>ListAdjustment</code>&#0160;is set to&#0160;<code>kReplaceList</code>, otherwise it will do nothing.</p>
</li>
<li class="code-line" data-line="83">
<p class="code-line" data-line="83"><a data-href="https://help.autodesk.com/view/MAYAUL/2022/ENU/?guid=Maya_SDK_What_s_New_What_s_Changed_2022_Whats_New_in_API_html#deprecated-method-and-changed-behavior-in-mcacheschema" href="https://help.autodesk.com/view/MAYAUL/2022/ENU/?guid=Maya_SDK_What_s_New_What_s_Changed_2022_Whats_New_in_API_html#deprecated-method-and-changed-behavior-in-mcacheschema" title="https://help.autodesk.com/view/MAYAUL/2022/ENU/?guid=Maya_SDK_What_s_New_What_s_Changed_2022_Whats_New_in_API_html#deprecated-method-and-changed-behavior-in-mcacheschema">Deprecated method and changed behavior in MCacheSchema</a>&#0160;- The method&#0160;<code>reset()</code>&#0160;has been deprecated and&#0160;<code>attributes()&#0160;</code>is now a range over&#0160;<code>MObject</code>&#0160;rather than&#0160;<code>MPlug</code>.</p>
</li>
<li class="code-line" data-line="85">
<p class="code-line" data-line="85"><a data-href="https://help.autodesk.com/view/MAYAUL/2022/ENU/?guid=Maya_SDK_What_s_New_What_s_Changed_2022_Whats_New_in_API_html#changes-to-mitgeometry-constructors" href="https://help.autodesk.com/view/MAYAUL/2022/ENU/?guid=Maya_SDK_What_s_New_What_s_Changed_2022_Whats_New_in_API_html#changes-to-mitgeometry-constructors" title="https://help.autodesk.com/view/MAYAUL/2022/ENU/?guid=Maya_SDK_What_s_New_What_s_Changed_2022_Whats_New_in_API_html#changes-to-mitgeometry-constructors">Changes to MItGeometry constructors</a>&#0160;- Two new constructors have been added for&#0160;<code>MItGeometry</code>.</p>
</li>
<li class="code-line" data-line="87">
<p class="code-line" data-line="87"><a data-href="https://help.autodesk.com/view/MAYAUL/2022/ENU/?guid=Maya_SDK_What_s_New_What_s_Changed_2022_Whats_New_in_API_html#changes-to-mrenderitem" href="https://help.autodesk.com/view/MAYAUL/2022/ENU/?guid=Maya_SDK_What_s_New_What_s_Changed_2022_Whats_New_in_API_html#changes-to-mrenderitem" title="https://help.autodesk.com/view/MAYAUL/2022/ENU/?guid=Maya_SDK_What_s_New_What_s_Changed_2022_Whats_New_in_API_html#changes-to-mrenderitem">Changes to MRenderItem</a>&#0160;- Six new methods have been added to&#0160;<code>MRenderItem</code>. Three methods have been deprecated.</p>
</li>
<li class="code-line" data-line="89">
<p class="code-line" data-line="89"><a data-href="https://help.autodesk.com/view/MAYAUL/2022/ENU/?guid=Maya_SDK_What_s_New_What_s_Changed_2022_Whats_New_in_API_html#new-attribute-and-method-added-to-mpxgeometryfilter" href="https://help.autodesk.com/view/MAYAUL/2022/ENU/?guid=Maya_SDK_What_s_New_What_s_Changed_2022_Whats_New_in_API_html#new-attribute-and-method-added-to-mpxgeometryfilter" title="https://help.autodesk.com/view/MAYAUL/2022/ENU/?guid=Maya_SDK_What_s_New_What_s_Changed_2022_Whats_New_in_API_html#new-attribute-and-method-added-to-mpxgeometryfilter">New attribute and method added to MPxGeometryFilter</a>&#0160;- The&#0160;<code>componentTagExpression</code>&#0160;attribute has been added to&#0160;<code>MPxGeometryFilter</code>&#0160;in the&#0160;<code>OpenMayaAnim</code>&#0160;module.</p>
</li>
<li class="code-line" data-line="91">
<p class="code-line" data-line="91"><a data-href="https://help.autodesk.com/view/MAYAUL/2022/ENU/?guid=Maya_SDK_What_s_New_What_s_Changed_2022_Whats_New_in_API_html#new-interface-added-to-mrenderoverride-in-the-openmayarender-module" href="https://help.autodesk.com/view/MAYAUL/2022/ENU/?guid=Maya_SDK_What_s_New_What_s_Changed_2022_Whats_New_in_API_html#new-interface-added-to-mrenderoverride-in-the-openmayarender-module" title="https://help.autodesk.com/view/MAYAUL/2022/ENU/?guid=Maya_SDK_What_s_New_What_s_Changed_2022_Whats_New_in_API_html#new-interface-added-to-mrenderoverride-in-the-openmayarender-module">New interface added to MRenderOverride in the OpenMayaRender module</a>&#0160;- New interface added to&#0160;<code>MRenderOverride</code>&#0160;to override the default Viewport 2.0 selection</p>
</li>
<li class="code-line" data-line="93">
<p class="code-line" data-line="93"><a data-href="https://help.autodesk.com/view/MAYAUL/2022/ENU/?guid=Maya_SDK_What_s_New_What_s_Changed_2022_Whats_New_in_API_html#new-methods-added-to-mselectioninfo-in-the-openmayarender-module" href="https://help.autodesk.com/view/MAYAUL/2022/ENU/?guid=Maya_SDK_What_s_New_What_s_Changed_2022_Whats_New_in_API_html#new-methods-added-to-mselectioninfo-in-the-openmayarender-module" title="https://help.autodesk.com/view/MAYAUL/2022/ENU/?guid=Maya_SDK_What_s_New_What_s_Changed_2022_Whats_New_in_API_html#new-methods-added-to-mselectioninfo-in-the-openmayarender-module">New methods added to MSelectionInfo in the OpenMayaRender module</a>&#0160;- Two new methods were added to&#0160;<code>MSelectionInfo</code>.</p>
</li>
<li class="code-line" data-line="95">
<p class="code-line" data-line="95"><a data-href="https://help.autodesk.com/view/MAYAUL/2022/ENU/?guid=Maya_SDK_What_s_New_What_s_Changed_2022_Whats_New_in_API_html#new-methods-and-types-added-to-mfngeometrydata-to-support-component-tags" href="https://help.autodesk.com/view/MAYAUL/2022/ENU/?guid=Maya_SDK_What_s_New_What_s_Changed_2022_Whats_New_in_API_html#new-methods-and-types-added-to-mfngeometrydata-to-support-component-tags" title="https://help.autodesk.com/view/MAYAUL/2022/ENU/?guid=Maya_SDK_What_s_New_What_s_Changed_2022_Whats_New_in_API_html#new-methods-and-types-added-to-mfngeometrydata-to-support-component-tags">New methods and types added to MFnGeometryData to support component tags</a>&#0160;- Several new methods and types added to&#0160;<code>MFnGeometryData</code>&#0160;to support component tags</p>
</li>
<li class="code-line" data-line="97">
<p class="code-line" data-line="97"><a data-href="https://help.autodesk.com/view/MAYAUL/2022/ENU/?guid=Maya_SDK_What_s_New_What_s_Changed_2022_Whats_New_in_API_html#new-methods-and-types-added-to-mitdependencygraph" href="https://help.autodesk.com/view/MAYAUL/2022/ENU/?guid=Maya_SDK_What_s_New_What_s_Changed_2022_Whats_New_in_API_html#new-methods-and-types-added-to-mitdependencygraph" title="https://help.autodesk.com/view/MAYAUL/2022/ENU/?guid=Maya_SDK_What_s_New_What_s_Changed_2022_Whats_New_in_API_html#new-methods-and-types-added-to-mitdependencygraph">New methods and types added to MItDependencyGraph</a>&#0160;- Several new methods and types have been added to&#0160;<code>MItDependencyGraph</code>&#0160;to allow for traversing on graphs based on different relations.</p>
</li>
<li class="code-line" data-line="99">
<p class="code-line" data-line="99"><a data-href="https://help.autodesk.com/view/MAYAUL/2022/ENU/?guid=Maya_SDK_What_s_New_What_s_Changed_2022_Whats_New_in_API_html#new-methods-added-to-mfragmentmanager" href="https://help.autodesk.com/view/MAYAUL/2022/ENU/?guid=Maya_SDK_What_s_New_What_s_Changed_2022_Whats_New_in_API_html#new-methods-added-to-mfragmentmanager" title="https://help.autodesk.com/view/MAYAUL/2022/ENU/?guid=Maya_SDK_What_s_New_What_s_Changed_2022_Whats_New_in_API_html#new-methods-added-to-mfragmentmanager">New methods added to MFragmentManager</a>&#0160;- Two new methods were added to&#0160;<code>MFragmentManager</code>&#0160;to add and remove, respectively, a specific shader stage&#39;s input parameters. Three new methods were added to manage the mapping between parameter names and transient names.</p>
</li>
<li class="code-line" data-line="101">
<p class="code-line" data-line="101"><a data-href="https://help.autodesk.com/view/MAYAUL/2022/ENU/?guid=Maya_SDK_What_s_New_What_s_Changed_2022_Whats_New_in_API_html#new-method-added-to-mselectionlist" href="https://help.autodesk.com/view/MAYAUL/2022/ENU/?guid=Maya_SDK_What_s_New_What_s_Changed_2022_Whats_New_in_API_html#new-method-added-to-mselectionlist" title="https://help.autodesk.com/view/MAYAUL/2022/ENU/?guid=Maya_SDK_What_s_New_What_s_Changed_2022_Whats_New_in_API_html#new-method-added-to-mselectionlist">New method added to MSelectionList</a>&#0160;-&#0160;<code>getPlug()</code>&#0160;have been added to&#0160;<code>MSelectionList</code>.</p>
</li>
<li class="code-line" data-line="103">
<p class="code-line" data-line="103"><a data-href="https://help.autodesk.com/view/MAYAUL/2022/ENU/?guid=Maya_SDK_What_s_New_What_s_Changed_2022_Whats_New_in_API_html#new-method-added-to-mdgmodifier" href="https://help.autodesk.com/view/MAYAUL/2022/ENU/?guid=Maya_SDK_What_s_New_What_s_Changed_2022_Whats_New_in_API_html#new-method-added-to-mdgmodifier" title="https://help.autodesk.com/view/MAYAUL/2022/ENU/?guid=Maya_SDK_What_s_New_What_s_Changed_2022_Whats_New_in_API_html#new-method-added-to-mdgmodifier">New method added to MDGModifier</a>&#0160;-&#0160;<code>deleteNode()</code>&#0160;has been added to&#0160;<code>MDGModifier</code>.</p>
</li>
<li class="code-line" data-line="105">
<p class="code-line" data-line="105"><a data-href="https://help.autodesk.com/view/MAYAUL/2022/ENU/?guid=Maya_SDK_What_s_New_What_s_Changed_2022_Whats_New_in_API_html#new-methods-added-to-mpxgpudeformer" href="https://help.autodesk.com/view/MAYAUL/2022/ENU/?guid=Maya_SDK_What_s_New_What_s_Changed_2022_Whats_New_in_API_html#new-methods-added-to-mpxgpudeformer" title="https://help.autodesk.com/view/MAYAUL/2022/ENU/?guid=Maya_SDK_What_s_New_What_s_Changed_2022_Whats_New_in_API_html#new-methods-added-to-mpxgpudeformer">New methods added to MPxGPUDeformer</a>&#0160;- Two new methods have been added to&#0160;<code>MPxGPUDeformer</code>.</p>
</li>
<li class="code-line" data-line="107">
<p class="code-line" data-line="107"><a data-href="https://help.autodesk.com/view/MAYAUL/2022/ENU/?guid=Maya_SDK_What_s_New_What_s_Changed_2022_Whats_New_in_API_html#new-method-added-to-mfnnurbscurve" href="https://help.autodesk.com/view/MAYAUL/2022/ENU/?guid=Maya_SDK_What_s_New_What_s_Changed_2022_Whats_New_in_API_html#new-method-added-to-mfnnurbscurve" title="https://help.autodesk.com/view/MAYAUL/2022/ENU/?guid=Maya_SDK_What_s_New_What_s_Changed_2022_Whats_New_in_API_html#new-method-added-to-mfnnurbscurve">New method added to MFnNurbsCurve</a>&#0160;-&#0160;<code>findParamFromLength()</code>&#0160;has been added to&#0160;<code>MFnNurbsCurve</code>.</p>
</li>
<li class="code-line" data-line="109">
<p class="code-line" data-line="109"><a data-href="https://help.autodesk.com/view/MAYAUL/2022/ENU/?guid=Maya_SDK_What_s_New_What_s_Changed_2022_Whats_New_in_API_html#new-methods-added-to-mevaluationnode" href="https://help.autodesk.com/view/MAYAUL/2022/ENU/?guid=Maya_SDK_What_s_New_What_s_Changed_2022_Whats_New_in_API_html#new-methods-added-to-mevaluationnode" title="https://help.autodesk.com/view/MAYAUL/2022/ENU/?guid=Maya_SDK_What_s_New_What_s_Changed_2022_Whats_New_in_API_html#new-methods-added-to-mevaluationnode">New methods added to MEvaluationNode</a>&#0160;- Two new methods have been added to&#0160;<code>MEvaluationNode</code>. A new example,&#0160;<code>simpleSkipNode</code>, has been added to demonstrate the use of these new methods.</p>
</li>
<li class="code-line" data-line="111">
<p class="code-line" data-line="111"><a data-href="https://help.autodesk.com/view/MAYAUL/2022/ENU/?guid=Maya_SDK_What_s_New_What_s_Changed_2022_Whats_New_in_API_html#new-methods-added-to-mpxcustomevaluator" href="https://help.autodesk.com/view/MAYAUL/2022/ENU/?guid=Maya_SDK_What_s_New_What_s_Changed_2022_Whats_New_in_API_html#new-methods-added-to-mpxcustomevaluator" title="https://help.autodesk.com/view/MAYAUL/2022/ENU/?guid=Maya_SDK_What_s_New_What_s_Changed_2022_Whats_New_in_API_html#new-methods-added-to-mpxcustomevaluator">New methods added to MPxCustomEvaluator</a>&#0160;- Two new methods have been added to&#0160;<code>MPxCustomEvaluator</code>. A new example, evaluationPruningEvaluator, has been added to demonstrate the use of these new methods.</p>
</li>
<li class="code-line" data-line="113">
<p class="code-line" data-line="113"><a data-href="https://help.autodesk.com/view/MAYAUL/2022/ENU/?guid=Maya_SDK_What_s_New_What_s_Changed_2022_Whats_New_in_API_html#new-methods-added-to-mgpudeformerregistrationinfo" href="https://help.autodesk.com/view/MAYAUL/2022/ENU/?guid=Maya_SDK_What_s_New_What_s_Changed_2022_Whats_New_in_API_html#new-methods-added-to-mgpudeformerregistrationinfo" title="https://help.autodesk.com/view/MAYAUL/2022/ENU/?guid=Maya_SDK_What_s_New_What_s_Changed_2022_Whats_New_in_API_html#new-methods-added-to-mgpudeformerregistrationinfo">New methods added to MGPUDeformerRegistrationInfo</a>&#0160;- Four new methods have been added to&#0160;<code>MGPUDeformerRegistrationInfo</code>.</p>
</li>
<li class="code-line" data-line="115">
<p class="code-line" data-line="115"><a data-href="https://help.autodesk.com/view/MAYAUL/2022/ENU/?guid=Maya_SDK_What_s_New_What_s_Changed_2022_Whats_New_in_API_html#changes-to-mgpudeformerbuffer" href="https://help.autodesk.com/view/MAYAUL/2022/ENU/?guid=Maya_SDK_What_s_New_What_s_Changed_2022_Whats_New_in_API_html#changes-to-mgpudeformerbuffer" title="https://help.autodesk.com/view/MAYAUL/2022/ENU/?guid=Maya_SDK_What_s_New_What_s_Changed_2022_Whats_New_in_API_html#changes-to-mgpudeformerbuffer">Changes to MGPUDeformerBuffer</a>&#0160;- Two new methods added to&#0160;<code>MGPUDeformerBuffer</code>, and the signature for&#0160;<code>MGPUDeformerBuffer()</code>&#0160;has been changed.</p>
</li>
<li class="code-line" data-line="117">
<p class="code-line" data-line="117"><a data-href="https://help.autodesk.com/view/MAYAUL/2022/ENU/?guid=Maya_SDK_What_s_New_What_s_Changed_2022_Whats_New_in_API_html#signature-changes-in-mgpudeformerdata-and-mpxgpudeformer-classes" href="https://help.autodesk.com/view/MAYAUL/2022/ENU/?guid=Maya_SDK_What_s_New_What_s_Changed_2022_Whats_New_in_API_html#signature-changes-in-mgpudeformerdata-and-mpxgpudeformer-classes" title="https://help.autodesk.com/view/MAYAUL/2022/ENU/?guid=Maya_SDK_What_s_New_What_s_Changed_2022_Whats_New_in_API_html#signature-changes-in-mgpudeformerdata-and-mpxgpudeformer-classes">Signature changes in MGPUDeformerData and MPxGPUDeformer classes</a>&#0160;- The signature for&#0160;<code>MGPUDeformerData::getBuffer()</code>&#0160;and&#0160;<code>MPxGPUDeformer::evaluate()</code>&#0160;has changed.</p>
</li>
<li class="code-line" data-line="119">
<p class="code-line" data-line="119"><a data-href="https://help.autodesk.com/view/MAYAUL/2022/ENU/?guid=Maya_SDK_What_s_New_What_s_Changed_2022_Whats_New_in_API_html#new-argument-added-to-mpxcommand-setresult" href="https://help.autodesk.com/view/MAYAUL/2022/ENU/?guid=Maya_SDK_What_s_New_What_s_Changed_2022_Whats_New_in_API_html#new-argument-added-to-mpxcommand-setresult" title="https://help.autodesk.com/view/MAYAUL/2022/ENU/?guid=Maya_SDK_What_s_New_What_s_Changed_2022_Whats_New_in_API_html#new-argument-added-to-mpxcommand-setresult">New argument added to MPxCommand.setResult</a>&#0160;- A new argument,&#0160;<code>append</code>, has been added to&#0160;<code>MPxCommand.setResult</code>&#0160;in Python API 2.0</p>
</li>
<li class="code-line" data-line="121">
<p class="code-line" data-line="121"><a data-href="https://help.autodesk.com/view/MAYAUL/2022/ENU/?guid=Maya_SDK_What_s_New_What_s_Changed_2022_Whats_New_in_API_html#other-new-methods" href="https://help.autodesk.com/view/MAYAUL/2022/ENU/?guid=Maya_SDK_What_s_New_What_s_Changed_2022_Whats_New_in_API_html#other-new-methods" title="https://help.autodesk.com/view/MAYAUL/2022/ENU/?guid=Maya_SDK_What_s_New_What_s_Changed_2022_Whats_New_in_API_html#other-new-methods">Other new methods</a></p>
</li>
<li class="code-line" data-line="123">
<p class="code-line" data-line="123"><a data-href="https://help.autodesk.com/view/MAYAUL/2022/ENU/?guid=Maya_SDK_What_s_New_What_s_Changed_2022_Whats_New_in_API_html#other-deprecated-methods" href="https://help.autodesk.com/view/MAYAUL/2022/ENU/?guid=Maya_SDK_What_s_New_What_s_Changed_2022_Whats_New_in_API_html#other-deprecated-methods" title="https://help.autodesk.com/view/MAYAUL/2022/ENU/?guid=Maya_SDK_What_s_New_What_s_Changed_2022_Whats_New_in_API_html#other-deprecated-methods">Other deprecated methods</a></p>
</li>
</ul>
<h2 class="code-line" data-line="126" id="devkit-updates">Devkit Updates</h2>
<ul>
<li class="code-line" data-line="128">
<p class="code-line" data-line="128"><a data-href="https://help.autodesk.com/view/MAYAUL/2022/ENU/?guid=Maya_SDK_What_s_New_What_s_Changed_2022_Whats_New_in_API_html#new-node-ids-for-several-examples" href="https://help.autodesk.com/view/MAYAUL/2022/ENU/?guid=Maya_SDK_What_s_New_What_s_Changed_2022_Whats_New_in_API_html#new-node-ids-for-several-examples" title="https://help.autodesk.com/view/MAYAUL/2022/ENU/?guid=Maya_SDK_What_s_New_What_s_Changed_2022_Whats_New_in_API_html#new-node-ids-for-several-examples">New node IDs for several examples</a>&#0160;- Changes were made to ensure that the node IDs used in all examples were unique. Examples with node ID changes will need to be recompiled.</p>
</li>
<li class="code-line" data-line="130">
<p class="code-line" data-line="130"><a data-href="https://help.autodesk.com/view/MAYAUL/2022/ENU/?guid=Maya_SDK_What_s_New_What_s_Changed_2022_Whats_New_in_API_html#python-examples-have-been-moved-and-renamed" href="https://help.autodesk.com/view/MAYAUL/2022/ENU/?guid=Maya_SDK_What_s_New_What_s_Changed_2022_Whats_New_in_API_html#python-examples-have-been-moved-and-renamed" title="https://help.autodesk.com/view/MAYAUL/2022/ENU/?guid=Maya_SDK_What_s_New_What_s_Changed_2022_Whats_New_in_API_html#python-examples-have-been-moved-and-renamed">Python examples have been moved and renamed</a>&#0160;- Python 1.0 examples have been renamed to have&#0160;<code>py1</code>&#0160;prefix and Python 2.0 examples have&#0160;<code>py2</code>&#0160;prefix. Python examples have been moved from&#0160;<code>scripting</code>&#0160;directory to&#0160;<code>python</code>&#0160;directory.</p>
</li>
<li class="code-line" data-line="132">
<p class="code-line" data-line="132"><a data-href="https://help.autodesk.com/view/MAYAUL/2022/ENU/?guid=Maya_SDK_What_s_New_What_s_Changed_2022_Whats_New_in_API_html#new-examples" href="https://help.autodesk.com/view/MAYAUL/2022/ENU/?guid=Maya_SDK_What_s_New_What_s_Changed_2022_Whats_New_in_API_html#new-examples" title="https://help.autodesk.com/view/MAYAUL/2022/ENU/?guid=Maya_SDK_What_s_New_What_s_Changed_2022_Whats_New_in_API_html#new-examples">New examples</a>&#0160;- Six new examples added to the devkit:&#0160;<code>customComponentTagNode</code>,&#0160;<code>exampleFalloff</code>,&#0160;<code>basicMorphNode</code>,&#0160;<code>simpleSkipNode</code>,&#0160;<code>evaluationPruningEvaluator</code>, and&#0160;<code>narrowPolyRenderOverride</code></p>
</li>
<li class="code-line" data-line="134">
<p class="code-line" data-line="134"><a data-href="https://help.autodesk.com/view/MAYAUL/2022/ENU/?guid=Maya_SDK_What_s_New_What_s_Changed_2022_Whats_New_in_API_html#updated-examples" href="https://help.autodesk.com/view/MAYAUL/2022/ENU/?guid=Maya_SDK_What_s_New_What_s_Changed_2022_Whats_New_in_API_html#updated-examples" title="https://help.autodesk.com/view/MAYAUL/2022/ENU/?guid=Maya_SDK_What_s_New_What_s_Changed_2022_Whats_New_in_API_html#updated-examples">Updated examples</a>&#0160;- Six examples have been updated:&#0160;<code>offsetNode</code>,&#0160;<code>threadingLockTests</code>,&#0160;<code>moveManip.py</code>,&#0160;<code>blindDoubleDataCmd.py</code>,&#0160;<code>glslShader</code>, and&#0160;<code>manipOverride</code>.</p>
</li>
<li class="code-line" data-line="136">
<p class="code-line" data-line="136"><a data-href="https://help.autodesk.com/view/MAYAUL/2022/ENU/?guid=Maya_SDK_What_s_New_What_s_Changed_2022_Whats_New_in_API_html#alembic-examples-updated-to-only-support-ogawa-alembic-files" href="https://help.autodesk.com/view/MAYAUL/2022/ENU/?guid=Maya_SDK_What_s_New_What_s_Changed_2022_Whats_New_in_API_html#alembic-examples-updated-to-only-support-ogawa-alembic-files" title="https://help.autodesk.com/view/MAYAUL/2022/ENU/?guid=Maya_SDK_What_s_New_What_s_Changed_2022_Whats_New_in_API_html#alembic-examples-updated-to-only-support-ogawa-alembic-files">Alembic examples updated to only support Ogawa Alembic files</a>&#0160;- Maya no longer supports HDF5 variants of Alembic. These examples now only support Ogawa Alembic files:&#0160;<code>gpuCache</code>,&#0160;<code>AbcBullet</code>,&#0160;<code>AbcImport</code>,&#0160;<code>AbcExport</code></p>
</li>
<li class="code-line" data-line="138">
<p class="code-line" data-line="138"><a data-href="https://help.autodesk.com/view/MAYAUL/2022/ENU/?guid=Maya_SDK_What_s_New_What_s_Changed_2022_Whats_New_in_API_html#removed-examples" href="https://help.autodesk.com/view/MAYAUL/2022/ENU/?guid=Maya_SDK_What_s_New_What_s_Changed_2022_Whats_New_in_API_html#removed-examples" title="https://help.autodesk.com/view/MAYAUL/2022/ENU/?guid=Maya_SDK_What_s_New_What_s_Changed_2022_Whats_New_in_API_html#removed-examples">Removed examples</a>&#0160;- Two examples removed from the devkit:&#0160;<code>exampleCameraSetView</code>,&#0160;<code>narrowPolyViewer</code></p>
</li>
</ul>
<h2 class="code-line" data-line="141" id="scripting-updates">Scripting Updates</h2>
<ul>
<li class="code-line" data-line="143">
<p class="code-line" data-line="143"><a data-href="https://help.autodesk.com/view/MAYAUL/2022/ENU/?guid=Maya_SDK_What_s_New_What_s_Changed_2022_Whats_New_in_API_html#ufe-aware-commands" href="https://help.autodesk.com/view/MAYAUL/2022/ENU/?guid=Maya_SDK_What_s_New_What_s_Changed_2022_Whats_New_in_API_html#ufe-aware-commands" title="https://help.autodesk.com/view/MAYAUL/2022/ENU/?guid=Maya_SDK_What_s_New_What_s_Changed_2022_Whats_New_in_API_html#ufe-aware-commands">UFE-aware commands</a>&#0160;- Some commands now accept UFE path strings. Commands that are UFE-aware will have different syntax than non-UFE-aware commands.</p>
</li>
<li class="code-line" data-line="145">
<p class="code-line" data-line="145"><a data-href="https://help.autodesk.com/view/MAYAUL/2022/ENU/?guid=Maya_SDK_What_s_New_What_s_Changed_2022_Whats_New_in_API_html#new-plug-ins" href="https://help.autodesk.com/view/MAYAUL/2022/ENU/?guid=Maya_SDK_What_s_New_What_s_Changed_2022_Whats_New_in_API_html#new-plug-ins" title="https://help.autodesk.com/view/MAYAUL/2022/ENU/?guid=Maya_SDK_What_s_New_What_s_Changed_2022_Whats_New_in_API_html#new-plug-ins">New plug-ins</a>&#0160;- Three new plug-ins:&#0160;<code>convertToComponentTags.py</code>,&#0160;<code>snapTransform.py</code>, and&#0160;<code>gameVertexCount</code></p>
</li>
<li class="code-line" data-line="147">
<p class="code-line" data-line="147"><a data-href="https://help.autodesk.com/view/MAYAUL/2022/ENU/?guid=Maya_SDK_What_s_New_What_s_Changed_2022_Whats_New_in_API_html#new-commands" href="https://help.autodesk.com/view/MAYAUL/2022/ENU/?guid=Maya_SDK_What_s_New_What_s_Changed_2022_Whats_New_in_API_html#new-commands" title="https://help.autodesk.com/view/MAYAUL/2022/ENU/?guid=Maya_SDK_What_s_New_What_s_Changed_2022_Whats_New_in_API_html#new-commands">New commands</a>&#0160;- Two new commands:&#0160;<code>FBXExtPlugin</code>,&#0160;<code>mimicManipulation</code></p>
</li>
<li class="code-line" data-line="149">
<p class="code-line" data-line="149"><a data-href="https://help.autodesk.com/view/MAYAUL/2022/ENU/?guid=Maya_SDK_What_s_New_What_s_Changed_2022_Whats_New_in_API_html#new-flags-and-options" href="https://help.autodesk.com/view/MAYAUL/2022/ENU/?guid=Maya_SDK_What_s_New_What_s_Changed_2022_Whats_New_in_API_html#new-flags-and-options" title="https://help.autodesk.com/view/MAYAUL/2022/ENU/?guid=Maya_SDK_What_s_New_What_s_Changed_2022_Whats_New_in_API_html#new-flags-and-options">New flags and options</a>&#0160;New flags and options added to these commands:&#0160;<code>file</code>,&#0160;<code>outlinerEditor</code>,&#0160;<code>sets</code>,&#0160;<code>deformerEvaluator</code>,&#0160;<code>filterCurve</code>,&#0160;<code>optVar</code>,&#0160;<code>geometryAttrInfo</code>,&#0160;<code>deformableShape</code>, and&#0160;<code>ghosting</code></p>
</li>
<li class="code-line" data-line="151">
<p class="code-line" data-line="151"><a data-href="https://help.autodesk.com/view/MAYAUL/2022/ENU/?guid=Maya_SDK_What_s_New_What_s_Changed_2022_Whats_New_in_API_html#changed-flags-and-options" href="https://help.autodesk.com/view/MAYAUL/2022/ENU/?guid=Maya_SDK_What_s_New_What_s_Changed_2022_Whats_New_in_API_html#changed-flags-and-options" title="https://help.autodesk.com/view/MAYAUL/2022/ENU/?guid=Maya_SDK_What_s_New_What_s_Changed_2022_Whats_New_in_API_html#changed-flags-and-options">Changed flags and options</a>&#0160;- Changes made to:&#0160;<code>deformableShape</code>,&#0160;<code>deleteAttr</code>,&#0160;<code>affectedNet</code>,&#0160;<code>dynExpression</code>, and&#0160;<code>cutKey</code></p>
</li>
<li class="code-line" data-line="153">
<p class="code-line" data-line="153"><a data-href="https://help.autodesk.com/view/MAYAUL/2022/ENU/?guid=Maya_SDK_What_s_New_What_s_Changed_2022_Whats_New_in_API_html#removed-script" href="https://help.autodesk.com/view/MAYAUL/2022/ENU/?guid=Maya_SDK_What_s_New_What_s_Changed_2022_Whats_New_in_API_html#removed-script" title="https://help.autodesk.com/view/MAYAUL/2022/ENU/?guid=Maya_SDK_What_s_New_What_s_Changed_2022_Whats_New_in_API_html#removed-script">Removed script</a>&#0160;-&#0160;<code>performSkinCluster.mel</code>&#0160;have been removed.</p>
</li>
</ul>
<p class="code-line code-active-line" data-line="157">Please refer to&#0160;<a data-href="https://help.autodesk.com/view/MAYAUL/2022/ENU/?guid=Maya_SDK_What_s_New_What_s_Changed_2022_Whats_New_in_API_html" href="https://help.autodesk.com/view/MAYAUL/2022/ENU/?guid=Maya_SDK_What_s_New_What_s_Changed_2022_Whats_New_in_API_html" title="https://help.autodesk.com/view/MAYAUL/2022/ENU/?guid=Maya_SDK_What_s_New_What_s_Changed_2022_Whats_New_in_API_html">What&#39;s New / What&#39;s Changed</a>&#0160;in the Developer Help documentation for details.</p>
