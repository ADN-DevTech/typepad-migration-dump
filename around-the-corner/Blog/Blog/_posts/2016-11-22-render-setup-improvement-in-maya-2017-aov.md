---
layout: "post"
title: "Render Setup improvement in Maya 2017: Support for AOV setups per layer and inclusion of AOV setups in exported templates"
date: "2016-11-22 20:17:00"
author: "Zhong Wu"
categories:
  - "Autodesk"
  - "C++"
  - "Maya"
  - "Plug-in"
  - "Python"
  - "Rendering"
  - "Visual Studio"
  - "Zhong Wu"
original_url: "https://around-the-corner.typepad.com/adn/2016/11/render-setup-improvement-in-maya-2017-aov.html "
typepad_basename: "render-setup-improvement-in-maya-2017-aov"
typepad_status: "Publish"
---

<p>As you know in Maya 2016 Extension 2 we introduced the replacement for Maya render layers called “Render Setup”, during that period we introduced this feature to several 3rd party renderers in person.&#160; For Maya 2017 we have been making improvements to this feature that will allow for improved support for 3rd party renderers, and other recent changes have the potential to impact 3rd party renderers as well, will talk about the 3 improvements in the following separate blogs, and the information is provided by our engineering team, thanks to them for sharing.</p>  <p>The first improvement is supporting for AOV setups per layer and inclusion of AOV setups in exported templates.</p>  <h3><font style="font-weight: bold" size="3">Introduction</font></h3>  <p>In Maya 2016 extension 2 and later, Render Setup has had the ability to import and export Render Settings information. This information however did not include AOV (Arbitrary Output Variable) data. With Maya 2017 however, an interface has been created to enable renderers to register a set of callbacks to import and export this AOV information. Not only that, but other AOV related callbacks can also be registered that do a variety of things. One such callback allows renderers to supply a means of opening the Render Settings to the renderer’s AOV section when double clicking on the AOV portion of the Render Setup window. Another set of callbacks provides a means of organizing AOV overrides into a more readable set of AOV override menus. The purpose of this document is to outline the various callbacks that are available for AOVs, as well as how to go about registering and implementing them.</p>  <h3><font style="font-weight: bold" size="3">AOV Callbacks</font></h3>  <p>In Maya 2017, there is a new callback mechanism for registering AOV callbacks with the Render Setup system. Essentially there is a single class defined in renderSetup/model/rendererCallbacks.py called AOVCallbacks that one needs to derive from and implement:</p>  <pre class="brush:python;toolbar: false;">import maya.app.renderSetup.model.rendererCallbacks as rc 
class RendererAOVCallbacks(rc.AOVCallbacks):
    def encode(self):
        ...    
    def decode(self, aovsData, decodeType):
        ...
    def displayMenu(self):
        ...
    def getAOVName(self, aovNode):
        ...
    def getCollectionSelector(self, selectorName):
        ...
    def getChildCollectionSelector(self, selectorName, aovName):
        ...</pre>

<p>Subsequent sections will take a look at each of these callback functions, and discuss both their purpose as well as give some advice on how to implement them.</p>

<p>After deriving from AOVCallbacks one still needs to register this set of AOV callbacks with the Render Setup system. This is achieved as follows:</p>

<pre class="brush:python;toolbar: false;">import maya.app.renderSetup.model.rendererCallbacks as rc
class RendererAOVCallbacks(rc.AOVCallbacks):
	...

rendererCallbacks = RendererAOVCallbacks()
rc.registerCallbacks(“rendererName”, 
                     rc.CALLBACKS_TYPE_AOVS,
                     rendererAOVCallbacks)</pre>

<h3><font style="font-weight: bold" size="3">Encode/Decode</font></h3>

<p>The most important set of AOV callbacks are the encode/decode callbacks which are used for importing and exporting a renderer’s AOV information. The encode function takes no parameters and must return a JSON string with the renderer specific AOV information encoded. To help simplify the process of exporting this information to JSON, the Render Setup plugin comes with a BasicExporter also located in renderSetup/model/rendererCallbacks.py that can be used to encode and decode Maya node information to and from JSON. The BasicNodeExporter derives from the NodeExporter class which has some additional useful methods. The signature of these classes is as follows:</p>

<pre class="brush:python;toolbar: false;">class NodeExporter(object):
    def encode(self):
        ...
    def decode(self, encodedData):
        ...
    def setPlugsToIgnore(self, plugsToIgnore):
        ...

class BasicNodeExporter(NodeExporter):
    def setNodes(self, nodes):
        ...
    def getNodes(self):
        ...</pre>

<p>To use the BasicNodeExporter, simply create one and call setNodes with a list of nodes that you wish to export. You can also use the setPlugsToIgnore function to specify a list of attributes that you want the exporter to not export. So for example if you used basicNodeExporter.setNodes([pSphere1]), you could call basicNodeExporter.setPlugsToIgnore([pSphere1.translate]) to avoid exporting the translate attribute when calling encode.</p>

<p>Unlike encode, the AOV callback decode function takes 2 parameters: the AOV JSON data and the decode type. The decode type has one of two possible values: DECODE_TYPE_OVERWRITE and DECODE_TYPE_MERGE. The overwrite mode specifies that the import should delete all existing AOV information before doing the import. The merge mode on the other hand overwrites any AOVs with the same name, imports AOVs with different names, and leaves alone any other pre-existing AOVs in the scene. The BasicNodeExporter can also be used to decode the data that you encoded previously with it. It doesn’t take care of the overwrite mode for you, but it can be used to do a basic merge style import.</p>

<h3><font style="font-weight: bold" size="3">Display Menu</font></h3>

<p>The next piece of functionality provided by the AOV callbacks is the ability to specify a way to open the Render Settings to a particular AOV related tab for the respective renderer. A sample implementation of such a callback is as follows:</p>

<pre class="brush:python;toolbar: false;">import maya.mel as mel
mel.eval(‘unifiedRenderGlobalsWindow’)
mel.eval(‘setCurrentTabInRenderGlobalsWindow(\”AOVs\”)’)
mel.eval(‘fillSelectedTabForCurrentRenderer’)</pre>

<p>The code fragment above attempts to load an AOVs tab in the Render Settings window. The tab in question to open is renderer specific.</p>

<h3><font style="font-weight: bold" size="3">AOV Overrides</font></h3>

<p>By default all Render Settings overrides for a layer are placed under the layer in a collection called Render Settings. The problem with placing AOV overrides here is that they can easily become lost amongst the various other Render Settings overrides. That’s where 3 callbacks come into play: getAOVName, getCollectionSelector and getChildCollectionSelector. These callbacks make it possible for an AOV collection to be created under the layer to help organize the AOV overrides. This AOV collection in turn has a child collection for each AOV which is used to house each AOV specific override.</p>

<p>As was mentioned, in order improve the organization of the AOV overrides, there are three callback functions that need to be implemented. The first, getAOVName, is used to get the AOV name for a particular AOV node. For example, for the Autodesk Arnold renderer, AOVs are broken into 3 nodes, each associated with a particular AOV (aiAOV, aiAOVDriver, aiAOVFilter). This function is used to return the AOV node name given a node of any of these three types.</p>

<p>The second function, called getCollectionSelector is used to select all AOV related nodes. In the case of Autodesk’s Arnold renderer this was once again the aiAOV, aiAOVDriver and aiAOVFilter nodes. In order to do so, an instance of the render setup SimpleSelector class was created to select all pertinent nodes. In Autodesk’s Arnold implementation this was simply done by the following code:</p>

<pre class="brush:python;toolbar: false;">import maya.app.renderSetup.model.selector as sel
import maya.app.renderSetup.model.rendererCallbacks as rc
import maya.app.renderSetup.model.utils as utils
...
 
class RendererAOVCallbacks(rc.AOVCallbacks):
    ...
	
    def getCollectionSelector(self, selectorName):
        selectorName = cmds.createNode(sel.SimpleSelector.kTypeName, 
                                   name=selectorName,            
                                   skipSelect=True)
        selector = utils.nameToUserNode(selectorName)
        selector.setPattern(&quot;*&quot;)
        selector.setFilterType(sel.Filters.kCustom)
        selector.setCustomFilterValue(
            &quot;aiAOV aiAOVDriver aiAOVFilter&quot;)
        return selectorName</pre>

<p>This creates a SimpleSelector that selects all nodes that are of the types: aiAOV, aiAOVDriver and aiAOVFilter.</p>

<p>The third function that needs to be implemented is getChildCollectionSelector which is used to return a selector that selects all of the nodes for a particular AOV. The implementation of this function will vary greatly based upon the way a renderer organizes its AOVs. At its simplest this function will simply look up the aovNode names and statically select them as follows:</p>

<pre class="brush:python;toolbar: false;">import maya.app.renderSetup.model.rendererCallbacks as rc
...

class RendererAOVCallbacks(rc.AOVCallbacks):
    ...

    def _getListOfAOVNodesFromAOVName(self, aovName):
        ...

    def getChildCollectionSelector(self, selectorName, aovName):
        selectorName = cmds.createNode(sel.SimpleSelector.kTypeName, 
                                   name=selectorName,            
                                   skipSelect=True)
        selector = utils.nameToUserNode(selectorName)
        aovNodes = self._getListOfAOVNodesFromAOVName(aovName)
        selector.staticSelection.setWithoutExistenceCheck(aovNodes)
        return selectorName</pre>

<p>In the above example, the function _getListOfAOVNodesFromAOVName would be a renderer specific function for getting the AOV node list from the AOV name.</p>

<p>It is possible that a SimpleSelector will be too simplistic for a particular renderer’s needs. In that case it is possible to derive from the base Selector class to create a custom selector. This selector can be registered using registerNode() located in renderSetup/model/renderSetup.py. Details on how to implement such a Selector are beyond the scope of this document. Please contact the Render Setup team if you believe you need to implement such a custom selector.</p>
