---
layout: "post"
title: "Cached Playback issues and their workarounds"
date: "2019-04-25 10:04:15"
author: "Lanh Hong"
categories:
  - "Maya"
original_url: "https://around-the-corner.typepad.com/adn/2019/04/cached-playback-issues-and-their-workarounds.html "
typepad_basename: "cached-playback-issues-and-their-workarounds"
typepad_status: "Publish"
---

<p><a href="http://help.autodesk.com/view/MAYAUL/2019/ENU/?guid=GUID-C9DB1549-B9C0-4FC2-B984-91B7DCE3141B">Cached playback</a> is a new feature that is available in Maya 2019, and it’s a major performance upgrade from previous releases. However, sometimes with new features, you may run into some issues while developing your plugins.</p>
<p>Our engineers are actively working on building the Cached Playback API in C++ and Python which will make it easier to deal with the common issues that plugin developers are running into, and you will see those improvements in the future.</p>
<p>Here are a couple of common problems and how to handle them.</p>
<p>&#0160;</p>
<h3>Unsupported Nodes</h3>
<p>One of the limitations to the cached playback is the list of unsupported nodes. If your scene contains any unsupported nodes, it will trigger Safe Mode which will disable caching for you and notifies you automatically. Unfortunately, this only applies to built-in nodes in Maya. For custom node plugins that are unsupported and does not interact well with cached playback, you have to do the inconvenient work by manually disabling the cache and printing your own message.</p>
<p>The workaround for this issue is to have your plugin disable caching by executing the following command when the plugin gets loaded or when plugin node gets created. Then print your own warning message.</p>
<p>&#0160;</p>
<p><code>cmds.evaluator(name=’cache’, enable=0)</code></p>
<p>&#0160;</p>
<p><span style="text-decoration: underline;">NOTE:</span> Unlike Safe Mode, this workaround does not prevent the user from enabling cached playback again. So be aware that the user can still manually turn on cached playback and possibly corrupting the entire scene.</p>
<p>&#0160;</p>
<h3>Shape Nodes Not Caching</h3>
<p>If you are developing a custom shape node, you may encounter an issue where your node isn’t caching by default when cached playback is enabled. This is an expected behavior for now.</p>
<p>The workaround requires you to use the <a href="https://help.autodesk.com/view/MAYAUL/2019/ENU/?guid=__CommandsPython_cacheEvaluator_html">cacheEvaluator MEL/Python command</a> to control caching configuration. Even though the C++/Python API is not out yet, the cacheEvaluator command encapsulates the API for cached playback. Using this command, you can configure the caching rules and add your custom shape node to support caching.</p>
<p>&#0160;</p>
<p><code>cmds.cacheEvaluator(</code><br /><code>&#0160;&#0160;&#0160;&#0160;newFiler=’nodeTypes’,</code><br /><code>&#0160;&#0160;&#0160;&#0160;newFilterParam=’types=+myCustomType’,</code><br /><code>&#0160;&#0160;&#0160;&#0160;newAction=’enableEvaluationCache’</code><br /><code>)</code></p>
<p>&#0160;</p>
<p><span style="text-decoration: underline;">NOTE:</span> This command must be re-executed every time the scene reopens, caching type changes, caching turns on and off, and caching rule resets.</p>
<p>To make it easier for your users, there is a hack that can make it more permanent so that the user does not have to configure the caching rules themselves. Have your plugin edit the caching rules using the follow code when the plugin gets loaded. This way, your plugin will support caching right out of the box.</p>
<p>&#0160;</p>
<p><code>import maya.plugin.evaluator.CacheEvaluatorManager</code><br /><code># Define a new rule to enable Evaluation Cache on a custom node type.</code><br /><code>new_rule_to_add = {</code><br /><code>&#0160;&#0160;&#0160;&#0160;&quot;newFilter&quot;: &quot;nodeTypes&quot;,</code><br /><code>&#0160;&#0160;&#0160;&#0160;&quot;newFilterParam&quot;: &quot;types=+myCustomType&quot;,</code><br /><code>&#0160;&#0160;&#0160;&#0160;&quot;newAction&quot;: &quot;enableEvaluationCache&quot;</code><br /><code>}</code><br /><code># Add this rule to the built-in caching mode.</code><br /><code>maya.plugin.evaluator.CacheEvaluatorManager.CACHE_STANDARD_MODE_EVAL.insert(0, new_rule_to_add)</code></p>
<p>&#0160;</p>
<p>The default built-in caching modes used by Maya are defined in <em>Python\Lib\site-packages\maya\plugin\evaluator\CacheEvaluatorManager.py</em>. They are:</p>
<p>&#0160;</p>
<p><code>- CACHE_STANDARD_MODE_VP2_HW</code><br /><code>- CACHE_STANDARD_MODE_VP2_SW</code><br /><code>- CACHE_STANDARD_MODE_EVAL</code></p>
<p><br />Hopefully these common issues can be resolved and improved when the Cached Playback API becomes available. For more information and details, you can check out the <a href="http://download.autodesk.com/us/company/files/MayaCachedPlayback/2019/MayaCachedPlaybackWhitePaper.html">Cached Playback whitepaper</a>.</p>
