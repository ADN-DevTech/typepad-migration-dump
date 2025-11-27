---
layout: "post"
title: "Connect to Fusion Lifecycle from Fusion 360 add-in"
date: "2017-02-15 15:20:37"
author: "Adam Nagy"
categories:
  - "Adam"
  - "Fusion 360"
original_url: "https://modthemachine.typepad.com/my_weblog/2017/02/connect-to-fusion-lifecycle-from-fusion-360-add-in.html "
typepad_basename: "connect-to-fusion-lifecycle-from-fusion-360-add-in"
typepad_status: "Publish"
---

<p><a href="http://www.autodeskfusionlifecycle.com/discover/">Fusion Lifecycle</a> has a <a href="http://help.autodesk.com/view/PLM/ENU/?guid=GUID-C03A2183-DE8D-41F5-9E33-9AC4435C5051">RESTful API</a> which can be accessed through simple <strong>HTTP</strong> requests, and so can be used from a <a href="http://www.autodesk.com/products/fusion-360/overview">Fusion 360</a> <strong>add-in</strong>/<strong>script</strong> as well:<br /><a href="http://modthemachine.typepad.com/my_weblog/2015/10/call-web-services-from-fusion-add-in.html">http://modthemachine.typepad.com/my_weblog/2015/10/call-web-services-from-fusion-add-in.html</a></p>
<p>I had a bit of difficulty doing <strong>HTTP</strong> requests from a&#0160;<a href="https://www.javascript.com">JavaScript</a>&#0160;<strong>Fusion 360 script</strong>, but all is fine from <a href="https://www.python.org">Python</a>.&#0160;</p>
<p>It&#39;s very easy to create a new <strong>Python</strong>&#0160;<strong>script</strong>/<strong>add-in</strong> inside <strong>Fusion 360</strong> and open it for edit. <br />Once it&#39;s open, you can add the following code that will log into your&#0160;<strong>Fusion Lifecycle</strong> tenant.<br />Note: <strong>POST login</strong> endpoint help:&#0160;<a href="http://help.autodesk.com/view/PLM/ENU/?guid=GUID-43302513-5120-4A42-8804-EF835D97D10D">http://help.autodesk.com/view/PLM/ENU/?guid=GUID-43302513-5120-4A42-8804-EF835D97D10D</a></p>
<p>You just have to set the correct <strong>userID</strong>, <strong>password</strong>, and <strong>tenant</strong>&#0160;<strong>URL</strong> in the code:</p>
<pre>#Author-
#Description-

import adsk.core, adsk.fusion, adsk.cam, traceback, json, http.client

def login(): 
    body = {
        &quot;userID&quot;: &quot;&lt;your user id&gt;&quot;, 
        &quot;password&quot;: &quot;&lt;your password&gt;&quot;
    }
    h = http.client.HTTPSConnection(&#39;&lt;name of your tenant&gt;.autodeskplm360.net&#39;)
    headers = {
        &#39;User-Agent&#39;: &#39;Fusion360&#39;,
        &#39;Content-Type&#39;: &#39;application/json&#39;,
        &#39;Accept&#39;: &#39;application/json&#39;
    }
    h.request(&#39;POST&#39;, &#39;/rest/auth/1/login&#39;, json.dumps(body), headers)
    res = h.getresponse()
    return res.status, res.reason, res.read()

def run(context):
    ui = None
    try:
        app = adsk.core.Application.get()
        ui  = app.userInterface
        
        status, response, data = login()
        dataString = data.decode(&#39;utf-8&#39;)
        ui.messageBox(dataString)
        
        # parse the json string
        dataObject = json.loads(dataString)
        ui.messageBox(&quot;My sessionid is &quot; + dataObject[&quot;sessionid&quot;])
    except:
        if ui:
            ui.messageBox(&#39;Failed:\n{}&#39;.format(traceback.format_exc()))
</pre>
<p>This what you get when running the code (if everything goes well):</p>
<p><a class="asset-img-link" href="http://modthemachine.typepad.com/.a/6a00e553fcbfc6883401b7c8d65190970b-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="LoginSuccess" class="asset  asset-image at-xid-6a00e553fcbfc6883401b7c8d65190970b img-responsive" src="/assets/image_661870.jpg" title="LoginSuccess" /></a></p>
<p>-Adam</p>
