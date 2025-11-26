---
layout: "post"
title: "Using LXML in Maya"
date: "2012-10-17 06:13:00"
author: "Cyrille Fauvel"
categories:
  - "C++"
  - "Cyrille Fauvel"
  - "Linux"
  - "Maya"
  - "Python"
original_url: "https://around-the-corner.typepad.com/adn/2012/10/using-lxml-in-maya.html "
typepad_basename: "using-lxml-in-maya"
typepad_status: "Publish"
---

<p>
<a class="asset-img-link" href="http://around-the-corner.typepad.com/.a/6a0163057a21c8970d017d3ca386dd970c-pi" style="display: inline;"><img alt="Python-xml-title" border="0" class="asset  asset-image at-xid-6a0163057a21c8970d017d3ca386dd970c" src="/assets/image_e6b191.jpg" style="display: block; margin-left: auto; margin-right: auto;" title="Python-xml-title" /></a></p>
<p>Recently someone asked me if we could use <a href="http://lxml.de/" target="_self">lxml </a>inside Maya and it turns out that lxml can be used in Maya excepted the Linux version. If you wonder what is lxml, here is a quick description:&#0160;The lxml XML toolkit is a Pythonic binding for the C libraries&#0160;<a href="http://xmlsoft.org/">libxml2</a>&#0160;and&#0160;<a href="http://xmlsoft.org/XSLT/">libxslt</a>. It is unique in that it combines the speed and XML feature completeness of these libraries with the simplicity of a native Python API, mostly compatible but superior to the well-known&#0160;<a href="http://effbot.org/zone/element-index.htm">ElementTree</a>&#0160;API.</p>
<p>Libxml2 is the XML C parser and toolkit developed for the Gnome project (but usable outside of the Gnome platform).&#0160;</p>
<p>
<a class="asset-img-link" href="http://around-the-corner.typepad.com/.a/6a0163057a21c8970d017c32750a04970b-pi" style="display: inline;"><img alt="Libxml" border="0" class="asset  asset-image at-xid-6a0163057a21c8970d017c32750a04970b image-full" src="/assets/image_4e1542.jpg" title="Libxml" /></a><br />Because the Linux version of Maya uses a very old version of libxml2 (i.e. version 2.6.4) and that the least version lxml can be built with is version 2.6.14, it is impossible to make it working. And when I say least lxml version, it is an old one :(</p>
<p>lxml web site mentions that the workaround is to build lxml in static mode. But it turns out that Maya will continue to crash (I also tried the egg way with no luck).</p>
<p>Anyway, after debuging into Maya, the issue was showing when Python was referencing a dictionary libxml2 upon loading lxml.etree =&gt; dict.c - xmlDictReference(). And naturally no way to fix the issue from the lxml side.</p>
<p>A workaround, but cannot guarante you would not break any other Maya features is to replace the libawxml2.so from Maya with a newer version. I tried with lxml-2.3.5 and libxml2-2.7.8 and it worked great.</p>
<p>Here is my scripts in case you want to try</p>
<pre class="brush: bash; toolbar: false;">#!/bin/bash
export MAYA_LOCATION=/usr/autodesk/maya2013.5-x64
export MYHOME=/home/cyrille
export DEST=$HOME/output

# assuming libxml2 source was downloaded and untar in ~/libxml2
# and that libxslt is already on the machine or was built as well
cd ~/libxml2
./configure --prefix=$DEST --without-python --with-zlib
make clean &amp;&amp; make
make install

# assuming lxml was downloaded and untar in ~/lxml
cd ~/lxml
$MAYA_LOCATION/bin/mayapy ./setup.py build
# log as su
$MAYA_LOCATION/bin/mayapy ./setup.py install

mv $MAYA_LOCATION/lib/libawxml2.so $MAYA_LOCATION/lib/libawxml2.so.old
cp $DEST/lib/libxml2.so $MAYA_LOCATION/lib/libawxml2.so</pre>
<p>That should be it,</p>
<p>but do not forget you need GCC 4.1.2 as it always been for Maya 2013 - 2013.5</p>
