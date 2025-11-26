---
layout: "post"
title: "Using Maya template for creating deformer UI"
date: "2023-06-29 01:33:22"
author: "Cheng Xi Li"
categories:
  - "Cheng Xi Li"
  - "Maya"
  - "Python"
  - "UI"
original_url: "https://around-the-corner.typepad.com/adn/2023/06/using-maya-template-for-creating-deformer-ui.html "
typepad_basename: "using-maya-template-for-creating-deformer-ui"
typepad_status: "Publish"
---

<p>Recently, a customer asked a question about creating UI for deformers. The deformer nodes in Maya are created with a table for deformer attributes e.g.,</p>
<p><a class="asset-img-link" href="https://around-the-corner.typepad.com/.a/6a0163057a21c8970d02c1b25803c4200d-pi" style="display: inline;"><img alt="MayaMorph" border="0" class="asset  asset-image at-xid-6a0163057a21c8970d02c1b25803c4200d image-full img-responsive" src="/assets/image_d2d900.jpg" title="MayaMorph" /></a></p>
<p>But for deformer plugins, if you don’t have an AETemplate assigned for it, it looks like this, which is not as easy to understand and has extra, but unnecessary details…</p>
<p><a class="asset-img-link" href="https://around-the-corner.typepad.com/.a/6a0163057a21c8970d02b751a99071200c-pi" style="display: inline;"><img alt="NaiveBasicMorph" border="0" class="asset  asset-image at-xid-6a0163057a21c8970d02b751a99071200c image-full img-responsive" src="/assets/image_dc9646.jpg" title="NaiveBasicMorph" /></a></p>
<p>The first thought is about reusing the AETemplates in scripts. Unfortunately, it wasn’t created with MEL and you can’t find it inside of scripts/AETemplates. AETemplate of some nodes in Maya were created with Python and Qt and stored inside Python/Lib/site-packages/maya/internal/nodes/morph.</p>
<pre><code data-origin="&lt;pre&gt;&lt;code&gt;import maya
maya.utils.loadStringResourcesForModule(__name__)

import maya.internal.common.ae.template as aetemplate
import maya.internal.nodes.weightgeometryfilter.ae_template as ae
import maya.cmds as cmds

class AETemplate(ae.AETemplate):
    def buildUI(self, nodeName):
    ...
&lt;/code&gt;&lt;/pre&gt;">import maya
maya.utils.loadStringResourcesForModule(__name__)

import maya.internal.common.ae.template as aetemplate
import maya.internal.nodes.weightgeometryfilter.ae_template as ae
import maya.cmds as cmds

class AETemplate(ae.AETemplate):
    def buildUI(self, nodeName):
    ...
</code></pre>
<p>As you can see, it inherits from another AETemplate from weightgeometryfilter and the latter geometryfilter. All we want to do is to create our AETemplate based on geometryfilter template, like below:</p>
<pre><code data-origin="&lt;pre&gt;&lt;code&gt;import maya.internal.common.ae.template as aetemplate
import maya.internal.nodes.geometryfilter.ae_template as ae

class AETemplate(ae.AETemplate):
    def buildUI(self, nodeName):
        self.suppress(&#39;weightList&#39;)
        super(AETemplate, self).buildUI(nodeName)
&lt;/code&gt;&lt;/pre&gt;">import maya.internal.common.ae.template as aetemplate
import maya.internal.nodes.geometryfilter.ae_template as ae

class AETemplate(ae.AETemplate):
    def buildUI(self, nodeName):
        self.suppress(&#39;weightList&#39;)
        super(AETemplate, self).buildUI(nodeName)
</code></pre>
<p>Save it to a place where Maya Python can load it as a package(e.g., site-packages), and create a __init__.py for it like below. My package name is myaetemplates and the module name is AEbasicMorphTemplate.</p>
<pre><code data-origin="&lt;pre&gt;&lt;code&gt;from . import AEbasicMorphTemplate
&lt;/code&gt;&lt;/pre&gt;">from . import AEbasicMorphTemplate
</code></pre>
<p>At last, we want to register for basicMorph node in Maya. To make things easier, we can use Flux, the UI library for Mash to register it.</p>
<pre><code data-origin="&lt;pre&gt;&lt;code&gt;import maya.app.flux.ae.api as loader

loader.registerTemplate(&#39;myaetemplates.AEbasicMorphTemplate.AETemplate&#39;, &#39;basicMorph&#39;)
&lt;/code&gt;&lt;/pre&gt;">import maya.app.flux.ae.api as loader

loader.registerTemplate(&#39;myaetemplates.AEbasicMorphTemplate.AETemplate&#39;, &#39;basicMorph&#39;)
</code></pre>
<p>Now, we have a much better interface to start with.<br /><a class="asset-img-link" href="https://around-the-corner.typepad.com/.a/6a0163057a21c8970d02c1a6cc2227200b-pi" style="display: inline;"><img alt="AEBasicMorph" border="0" class="asset  asset-image at-xid-6a0163057a21c8970d02c1a6cc2227200b image-full img-responsive" src="/assets/image_16aa84.jpg" title="AEBasicMorph" /></a></p>
