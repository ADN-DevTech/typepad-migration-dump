---
layout: "post"
title: "How to import/export Render Setup with Python"
date: "2017-01-13 05:55:18"
author: "Cheng Xi Li"
categories:
  - "Cheng Xi Li"
  - "Maya"
  - "Python"
  - "Rendering"
original_url: "https://around-the-corner.typepad.com/adn/2017/01/how-to-importexport-render-setup-with-python.html "
typepad_basename: "how-to-importexport-render-setup-with-python"
typepad_status: "Publish"
---

<style>
code, pre {
    font-family: Consolas, "Liberation Mono", Menlo, "Courier Prime Web", Courier, monospace;
    background: #f3f3f3;
}

code {
    padding: 1px;
    margin: 0 -1px;
    border-radius: 3px;
}

pre {
    display: block;
    line-height: 20px;
    text-shadow: 0 1px white;
    padding: 5px 5px 5px 30px;
    white-space: nowrap;
    position: relative;
    margin: 1em 0;
}

pre:before {
    content: "";
    position: absolute;
    top: 0;
    bottom: 0;
    left: 15px;
    border-left: solid 1px #dadada;
}

</style>
<p>Render setup is very useful for managing render layers and it can import and export render layers for later usage. However, the information on how to&nbsp;import/export it in Python is quite sparse. After doing some research&nbsp;on our testing code, I found that it is quite easy.<br /><br /> There is renderSetup.decode and renderSetup.encode for import and export, respectively.<br /><br /> The prototype&nbsp;for decode is &nbsp;<span style="font-family: courier new,courier;">decode(jsonObject, importOption, optionalPrefix)</span>, where <br /> &nbsp;- the <span style="font-family: courier new,courier;">jsonObject</span> is the json string you'll need to import; <br /> &nbsp;- the <span style="font-family: courier new,courier;">importOption</span> could be one of the DECODE_AND_RENAME, DECODE_AND_MERGE, DECODE_AND_OVERWRITE value which you could find in the import dialogue;<br /> &nbsp;- the <span style="font-family: courier new,courier;">optionalPrefix</span> is the prepend string and it could be None (if you don't want a prefix) and it is only working with DECODE_AND_RENAME option.<br /><br /> The export command is easier, as it has only one parameter called <span style="font-family: courier new,courier;">Note</span>. This valie can be&nbsp;“None” if you do not want to provide a Note.</p>
<pre id="bMDACAwljFC">import maya.app.renderSetup.model.renderSetup as renderSetup<br /><br />import json<br /><br />def importRenderSetup(filename):<br />&nbsp;&nbsp; &nbsp;with open(filename, "r") as file:<br />&nbsp;&nbsp; &nbsp; &nbsp; &nbsp;renderSetup.instance().decode(json.load(file), renderSetup.DECODE_AND_OVERWRITE, None)<br />&nbsp;<br />def exportRenderSetup(filename, note = None):<br />&nbsp;&nbsp; &nbsp;with open(filename, "w+") as file:<br />&nbsp;&nbsp; &nbsp; &nbsp; &nbsp;json.dump(renderSetup.instance().encode(note), fp=file, indent=2, sort_keys=True)</pre>
<p>You can use above code snippet to import/export your render setup in Maya.</p>
