---
layout: "post"
title: "Maya 2024 API Update guide"
date: "2023-03-28 18:46:46"
author: "Cheng Xi Li"
categories:
  - "C++"
  - "Cheng Xi Li"
  - "Maya"
  - "Python"
original_url: "https://around-the-corner.typepad.com/adn/2023/03/maya-2024-api-update-guide.html "
typepad_basename: "maya-2024-api-update-guide"
typepad_status: "Publish"
---

<div style="font-family: &#39;Roboto Condensed&#39;, Tauri, &#39;Hiragino Sans GB&#39;, &#39;Microsoft YaHei&#39;, STHeiti, SimSun, &#39;Lucida Grande&#39;, &#39;Lucida Sans Unicode&#39;, &#39;Lucida Sans&#39;, &#39;Segoe UI&#39;, AppleSDGothicNeo-Medium, &#39;Malgun Gothic&#39;, Verdana, Tahoma, sans-serif; font-size: 15px; overflow-x: hidden; overflow-y: auto; margin: 0px !important; padding: 20px; background-color: #ffffff; color: #222222; line-height: 1.6; -webkit-font-smoothing: antialiased; background: #ffffff;">
<p style="margin: 1em 0px; word-wrap: break-word;"><a id="top" style="text-decoration: none; vertical-align: baseline; color: #3269a0;"></a>The guide is based on What’s New in the Maya Devkit in Maya 2024 with some extra info. For more details including commands and scripts changes, please checkout the <a href="http://help.autodesk.com/view/MAYAUL/2024/ENU/" style="text-decoration: none; vertical-align: baseline; color: #3269a0;">documentation</a> for more details.</p>
<h2 id="building-environments" style="clear: both; font-size: 1.8em; font-weight: bold; margin: 1.275em 0px 0.85em; border-bottom-width: 1px; border-bottom-style: solid; border-bottom-color: #e6e6e6;"><a href="#building-environments" name="building-environments" style="text-decoration: none; vertical-align: baseline; color: #3269a0;"></a>Building environments</h2>
<p style="margin-top: 0px; margin: 1em 0px; word-wrap: break-word;">In Maya 2024, we made significent changes on the building environment. The minimum supported version of CMake has been updated to 3.22.1.</p>
<table style="padding: 0px; border-collapse: collapse; border-spacing: 0px; margin-bottom: 16px; background-color: #fafafa;">
<thead>
<tr>
<th style="border: 1px solid #e6e6e6; margin: 0px; padding: 6px 13px; font-weight: bold;">Platform</th>
<th style="border: 1px solid #e6e6e6; margin: 0px; padding: 6px 13px; font-weight: bold;">OS</th>
<th style="border: 1px solid #e6e6e6; margin: 0px; padding: 6px 13px; font-weight: bold;">Building Environment</th>
</tr>
</thead>
<tbody>
<tr>
<td style="border: 1px solid #e6e6e6; margin: 0px; padding: 6px 13px;">Windows</td>
<td style="border: 1px solid #e6e6e6; margin: 0px; padding: 6px 13px;">-</td>
<td style="border: 1px solid #e6e6e6; margin: 0px; padding: 6px 13px;">Visual studio 2022, .NET 4.8.1</td>
</tr>
<tr style="background-color: #f2f2f2;">
<td style="border: 1px solid #e6e6e6; margin: 0px; padding: 6px 13px;">Linux</td>
<td style="border: 1px solid #e6e6e6; margin: 0px; padding: 6px 13px;">RHEL 8.6+</td>
<td style="border: 1px solid #e6e6e6; margin: 0px; padding: 6px 13px;">DTS-11 + gcc 11.2.1</td>
</tr>
<tr>
<td style="border: 1px solid #e6e6e6; margin: 0px; padding: 6px 13px;">Mac</td>
<td style="border: 1px solid #e6e6e6; margin: 0px; padding: 6px 13px;">-</td>
<td style="border: 1px solid #e6e6e6; margin: 0px; padding: 6px 13px;">Xcode 13.4+, macOS 11(Big Sur) and Python 3.10.6</td>
</tr>
</tbody>
</table>
<p style="margin: 1em 0px; word-wrap: break-word;">If you are working with <strong>MFnAnimCurve::evaluate()</strong>, <strong>MFnAnimCurve::addKey()</strong>, <strong>MFnAnimCurve</strong> or using <strong>glFunctionTable</strong>, you’ll need to check the update document. There are some changes for these classes may break your plugin.</p>
<p style="margin: 1em 0px; word-wrap: break-word;"><a href="#top" style="text-decoration: none; vertical-align: baseline; color: #3269a0;">Back to top</a></p>
<h2 id="python-api-changes" style="clear: both; font-size: 1.8em; font-weight: bold; margin: 1.275em 0px 0.85em; border-bottom-width: 1px; border-bottom-style: solid; border-bottom-color: #e6e6e6;"><a href="#python-api-changes" name="python-api-changes" style="text-decoration: none; vertical-align: baseline; color: #3269a0;"></a>Python API Changes</h2>
<ul>
<li>Python API 2.0 now includes <strong>MFnAssembly</strong>.</li>
<li>Python hash support has been added to <strong>MObjectHandle</strong>. Objects of type MObjectHandle can now be used as keys in Python containers that require hashing.</li>
</ul>
<h2 id="new-build-instructions-for-apple-silicon-machines-running-the-mac-ub2-build" style="clear: both; font-size: 1.8em; font-weight: bold; margin: 1.275em 0px 0.85em; border-bottom-width: 1px; border-bottom-style: solid; border-bottom-color: #e6e6e6;"><a href="#new-build-instructions-for-apple-silicon-machines-running-the-mac-ub2-build" name="new-build-instructions-for-apple-silicon-machines-running-the-mac-ub2-build" style="text-decoration: none; vertical-align: baseline; color: #3269a0;"></a>New build instructions for Apple Silicon machines running the Mac UB2 build</h2>
<p style="margin-top: 0px; margin: 1em 0px; word-wrap: break-word;">You’ll need to specify building platform on Apple Silicon. e.g., to generate Intel86_64 binaries use:</p>
<pre style="border-top-left-radius: 3px; border-top-right-radius: 3px; border-bottom-right-radius: 3px; border-bottom-left-radius: 3px; word-wrap: break-word; border: 1px solid #cccccc; overflow: auto; padding: 0.5em;"><code data-origin="&lt;pre&gt;&lt;code&gt;cmake -H. -Bbuild -G Xcode -DCMAKE_OSX_ARCHITECTURES=x86_64
&lt;/code&gt;&lt;/pre&gt;" style="border: 1px solid #cccccc; display: block; font-family: Consolas, Inconsolata, Courier, monospace; font-weight: bold; white-space: pre; margin: 0px 2px; border-top-left-radius: 3px; border-top-right-radius: 3px; border-bottom-right-radius: 3px; border-bottom-left-radius: 3px; word-wrap: break-word; padding: 0px 5px; font-size: 1em; letter-spacing: -1px;">cmake -H. -Bbuild -G Xcode -DCMAKE_OSX_ARCHITECTURES=x86_64
</code></pre>
<p style="margin: 1em 0px; word-wrap: break-word;">And to generate binaries for both Intel86_64 and arm64 use:</p>
<pre style="border-top-left-radius: 3px; border-top-right-radius: 3px; border-bottom-right-radius: 3px; border-bottom-left-radius: 3px; word-wrap: break-word; border: 1px solid #cccccc; overflow: auto; padding: 0.5em;"><code data-origin="&lt;pre&gt;&lt;code&gt;cmake -H. -Bbuild -G Xcode -DCMAKE_OSX_ARCHITECTURES=&quot;x86_64;arm64&quot;
&lt;/code&gt;&lt;/pre&gt;" style="border: 1px solid #cccccc; display: block; font-family: Consolas, Inconsolata, Courier, monospace; font-weight: bold; white-space: pre; margin: 0px 2px; border-top-left-radius: 3px; border-top-right-radius: 3px; border-bottom-right-radius: 3px; border-bottom-left-radius: 3px; word-wrap: break-word; padding: 0px 5px; font-size: 1em; letter-spacing: -1px;">cmake -H. -Bbuild -G Xcode -DCMAKE_OSX_ARCHITECTURES=&quot;x86_64;arm64&quot;
</code></pre>
<p style="margin: 1em 0px; word-wrap: break-word;"><a href="#top" style="text-decoration: none; vertical-align: baseline; color: #3269a0;">Back to top</a></p>
<h2 id="devkit-changes" style="clear: both; font-size: 1.8em; font-weight: bold; margin: 1.275em 0px 0.85em; border-bottom-width: 1px; border-bottom-style: solid; border-bottom-color: #e6e6e6;"><a href="#devkit-changes" name="devkit-changes" style="text-decoration: none; vertical-align: baseline; color: #3269a0;"></a>Devkit changes</h2>
<ul>
<li>Each Qt example is now located in its own directory under the <strong>plug-ins</strong> directory</li>
<li>Fixed a bug in cvColorNode</li>
<li>The apiMeshShape example has been updated to use OpenGL. It provides information on how to migrate your code to use OpenGL calls instead of <strong>glFunctionTable</strong>.</li>
</ul>
<p style="margin: 1em 0px; word-wrap: break-word;"><a href="#top" style="text-decoration: none; vertical-align: baseline; color: #3269a0;">Back to top</a></p>
<h2 id="api-changes" style="clear: both; font-size: 1.8em; font-weight: bold; margin: 1.275em 0px 0.85em; border-bottom-width: 1px; border-bottom-style: solid; border-bottom-color: #e6e6e6;"><a href="#api-changes" name="api-changes" style="text-decoration: none; vertical-align: baseline; color: #3269a0;"></a>API Changes</h2>
<ul>
<li>A bug in the <strong>evaluate()</strong> and <strong>addKey()</strong> methods of <strong>MFnAnimCurve</strong> caused <strong>MTime</strong> to be handled as ticks instead of time has been fixed.</li>
<li><strong>MDisplayLayerMessage</strong> registers callbacks for display layer membership changes.</li>
<li><strong>MFnDisplayLayer</strong> is the function set for querying the contents of a display layer.</li>
<li><strong>MFnDisplayLayerManager</strong> is the function set used to query which display layer a Ufe item is a member of.</li>
<li><strong>inAlternateContext()</strong> and <strong>doExitRegion()</strong> added to <strong>MPxContext</strong>.<br style="clear: both;" />inAlternateContext() returns true when an alternate context is active. doExitRegion() is called when the mouse exits the viewport.</li>
<li><strong>indexMapper()</strong> and <strong>envelopeWeights()</strong> added to <strong>MPxDeformerNode</strong>.<br style="clear: both;" />indexMapper() returns the indexMapper of the deformer, and envelopeWeights() returns the deformer’s envelope weights, which are a combination of painted weights and falloff weights.</li>
<li><strong>indexMapper()</strong> has been added to <strong>MPxGeometryFilter</strong>.</li>
<li><strong>setOverrideBlendState()</strong> added to <strong>MUIDrawManager</strong>. setOverrideBlendState() lets you specify a new blend state to override the default blend state of the UI painter. It applies to mesh-like UI objects, not text or icons.</li>
<li><strong>lastEvaluatedOnGPU()</strong> added to <strong>MPxGPUDeformer</strong>. This method returns true if the previous evaluation of this node was on the GPU.</li>
<li><strong>isExactlyEqual()</strong> added to <strong>MPlug</strong>. This method is used to compare multi-indices instead of the == and != operators.</li>
<li><strong>balanceTransformation()</strong> has been added to <strong>MFnTransform</strong>. This method balances a transformation when applying a world matrix to a joint.</li>
<li><strong>isHideOnPlayback()</strong>, <strong>setHideOnPlayback()</strong>, <strong>setDrawLast()</strong> and <strong>isDrawLast()</strong>, have been added to MRenderItem.</li>
<li><strong>getDisplayStyleOfAllViewports()</strong> has been added to <strong>MHWRender::MFrameContext</strong>. This method returns the union of the display style of all active visible 3d viewports.</li>
<li>Four const method has been added to <strong>MPxGPUStandardDeformer</strong>, <strong>multiIndex()</strong>, <strong>inputPlug()</strong>, <strong>indexMapper()</strong>, <strong>passThroughWithZeroEnvelope()</strong></li>
<li><strong>k119_88FPS</strong> has been added to the <strong>MTime::Unit</strong> enum. It corresponds to a framerate of 119fps.</li>
<li><strong>kPolyUnsubdivide</strong>, <strong>kOpaqueAttribute</strong> types are added to MFn.</li>
<li><strong>MSelectInfo</strong>, <strong>MPxSurfaceShapeUI</strong> are deprecated.</li>
</ul>
<p style="margin: 1em 0px; word-wrap: break-word;"><a href="#top" style="text-decoration: none; vertical-align: baseline; color: #3269a0;">Back to top</a></p>
</div>
