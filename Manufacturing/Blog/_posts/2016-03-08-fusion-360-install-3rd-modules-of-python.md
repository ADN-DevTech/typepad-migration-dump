---
layout: "post"
title: "Fusion 360: Install 3rd Modules of Python"
date: "2016-03-08 02:34:11"
author: "Xiaodong Liang"
categories:
  - "Fusion 360"
  - "Xiaodong Liang"
original_url: "https://adndevblog.typepad.com/manufacturing/2016/03/fusion-360-install-3rd-modules-of-python.html "
typepad_basename: "fusion-360-install-3rd-modules-of-python"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/manufacturing/xiaodong-liang.html" target="_self">Xiaodong Liang</a></p>
<p>With JavaScript, you can easily load 3<sup>rd</sup> libraries in relative path or even web repository. We have an article on that:</p>
<p><a href="http://adndevblog.typepad.com/manufacturing/2015/04/fusion-360-api-load-3rd-library-in-javascript.html">http://adndevblog.typepad.com/manufacturing/2015/04/fusion-360-api-load-3rd-library-in-javascript.html</a></p>
<p>With Python, typically we run a setup to install the modules and the dependent modules this module relies on. As we have known, Fusion 360 builds a Python environment at <br />%AppData%/Local/webdeploy\production\&lt;GUID of one main release&gt;\Python.</p>
<p>In theory, you could setup your modules there. They will be deployed to \Python\Lib\site-packages\. The Python/lib folder is set to the system path on Fusion startup, so any packages located there should be found when imported from any script.</p>
<p>However, when Fusion is updated, it might update something of Python environment, particularly when it is a major release or even Fusion upgrades to a new version of Python. So there is a possibility that your script/add-in would not work because of the update. If you prefer to setup with the typical way, you will have to be at your own risk. The below is a forum post where our expert shared more detail comments:</p>
<p><a href="https://forums.autodesk.com/t5/api-and-scripts/to-install-python-modules/td-p/5777176">https://forums.autodesk.com/t5/api-and-scripts/to-install-python-modules/td-p/5777176</a></p>
<p>Then, what you can do without typical way?</p>
<p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c8208b8a970b-pi"><img alt="clip_image001" border="0" height="210" src="/assets/image_119e1a.jpg" style="background-image: none; margin: 0px; padding-left: 0px; padding-right: 0px; display: inline; padding-top: 0px; border-width: 0px;" title="clip_image001" width="244" /></a></p>
<p>Since Fusion builds the Python environment, you can actually also use relative path to import the modules dynamically. So, put your modules in the folder of your script /add-in and write some lines at the top of the codes.</p>
<p>e.g. I am practicing Socket.IO with Fusion, so I firstly downloaded the <a href="https://github.com/invisibleroads/socketIO-client">package</a> from&#0160; and copied the folder socketIO_client in my add-in path. Then, in my script, I tried to import it</p>
<pre>#import Fusion modules
import adsk.core, adsk.fusion, traceback

#import system modules
import os, sys 

#get the path of add-in
my_addin_path = os.path.dirname(os.path.realpath(__file__)) 
print(my_addin_path)

#add the path to the searchable path collection
if not my_addin_path in sys.path:
   sys.path.append(my_addin_path) 

#import socketIO according to the help of https://github.com/invisibleroads/socketIO-client
from socketIO_client import SocketIO, LoggingNamespace</pre>
<p>&#0160;</p>
<p>you might agree, life is so beautiful exactly because it is not so smoothly sometimes and we enjoy how to overcome the frustration <img alt="微笑" class="wlEmoticon wlEmoticon-smile" src="/assets/image_93b528.jpg" style="border-style: none;" />&#0160; with the code above, I got errors. It said, other modules are missing. Checking the <a href="https://github.com/invisibleroads/socketIO-client/blob/master/setup.py">setup.py</a>,&#0160; I found it indicates the required modules which are not available with Python Libs of Fusion.</p>
<pre>REQUIREMENTS = [
    &#39;requests&#39;,
    &#39;six&#39;,
    &#39;websocket-client&#39;,
]</pre>
<p>So I need to download them and copy to my add-in path. Of course, I can run setup.py to let the code to download automatically and take the modules. but, even though I copied them, it still threw an error below. I scratched my head for a bit long time. In the meantime, I started to realize it is not an elegant way to debug in Fusion to check the issue of importing only. This is because Fusion does not provide detail information about an error. It is just a superficial information which would not be helpful.</p>
<p>&#0160;</p>
<p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb08c50dcf970d-pi"><img alt="clip_image002" border="0" height="52" src="/assets/image_ab3fa3.jpg" style="background-image: none; padding-left: 0px; padding-right: 0px; display: inline; padding-top: 0px; border-width: 0px;" title="clip_image002" width="412" /></a></p>
<p>So, I thought of testing importing by Python environment. I created a separate Python file and copied it to C:\temp. In this file, some lines are like:</p>
<pre>#import system modules
import os, sys 

#get the path of add-in
#my_addin_path = os.path.dirname(os.path.realpath(__file__)) 
#hard coded the absolute path of my add-in
my_addin_path = &#39;C:\\Users\\liangx\\AppData\\Roaming\\Autodesk\\Autodesk Fusion 360\\API\\AddIns\\RemoteDriveParam&#39;
print(my_addin_path)

#add the path to the searchable path collection
if not my_addin_path in sys.path:
	sys.path.append(my_addin_path) 

#import socketIO according to the help of https://github.com/invisibleroads/socketIO-client
from socketIO_client import SocketIO, LoggingNamespace </pre>
<p>then, in command line of OS, switch to Python environment at</p>
<p>%AppData%/Local/webdeploy\production\&lt;GUID of one main release&gt;\Python, run the script:</p>
<p>python &quot;c:\temp\testimport.py&quot;</p>
<p>Now, it shows more detail about the error, which clearly tells which file popped out the error. By this clue, I finally addressed the dependent modules and the importing works now. In fact, it was just a mistake how I put the module of six.</p>
<p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c8208b98970b-pi"><img alt="clip_image003" border="0" height="228" src="/assets/image_c6b855.jpg" style="background-image: none; padding-left: 0px; padding-right: 0px; display: inline; padding-top: 0px; border: 0px;" title="clip_image003" width="348" /></a></p>
