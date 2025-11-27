---
layout: "post"
title: "Creating a face-recognising security cam with a Raspberry Pi &ndash; Part 4"
date: "2012-10-10 16:26:16"
author: "Kean Walmsley"
categories:
  - "Raspberry Pi"
original_url: "https://www.keanw.com/2012/10/creating-a-face-recognising-security-cam-with-a-raspberry-pi-part-4.html "
typepad_basename: "creating-a-face-recognising-security-cam-with-a-raspberry-pi-part-4"
typepad_status: "Publish"
---

<p>This is the final part of the series on creating a face-recognising security cam. We started by showing how to get <a href="http://through-the-interface.typepad.com/through_the_interface/2012/08/creating-a-motion-detecting-security-cam-with-a-raspberry-pi-part-1.html" target="_blank">motion</a> <a href="http://through-the-interface.typepad.com/through_the_interface/2012/09/creating-a-motion-detecting-security-cam-with-a-raspberry-pi-part-2.html" target="_blank">detection</a> working, and then followed with <a href="http://through-the-interface.typepad.com/through_the_interface/2012/09/creating-a-face-recognising-security-cam-with-a-raspberry-pi-part-1.html" target="_blank">an initial overview</a> and then posts on <a href="http://through-the-interface.typepad.com/through_the_interface/2012/09/creating-a-face-recognising-security-cam-with-a-raspberry-pi-part-2.html" target="_blank">the separate Facebook-downloader tool</a> and <a href="http://through-the-interface.typepad.com/through_the_interface/2012/09/creating-a-face-recognising-security-cam-with-a-raspberry-pi-part-3.html" target="_blank">the onboard face detection component</a>. In this post, we’ll see how we managed to connect up <a href="http://www.dreamcheeky.com/led-message-board" target="_blank">a USB-powered LED message-board from DreamCheeky</a>.</p>
<p>I was originally inspired to use this device to present the results of the Facecam while having dinner at a friend’s place. He’s a fellow geek – even his <a href="http://www.geekologie.com/2007/04/theres-no-place-like-127001.php" target="_blank">welcome mat</a> says so – so we Googled around for a solution and came up with the DreamCheeky, mainly because <a href="http://www.last-outpost.com/~malakai/dcled" target="_blank">someone had already created a Linux driver for it</a>.</p>
<p>The device is USB-powered and I’m thankfully able to power both this and the Logitech webcam directly from the Raspberry Pi: I don’t need to resort to a powered USB hub, which is sometimes needed if your webcam needs more juice than the average.</p>
<p>Here are the steps I followed to install and build the necessary components on the Raspberry Pi:</p>
<div style="font-family: courier new; background: white; color: black; font-size: 8pt;">
<p style="margin: 0px;"><span style="line-height: 140%;"># Create our main folder</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">cd</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">mkdir led</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">cd led</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;"># Get the libhid source and build it</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">wget &quot;http://alioth.debian.org/frs/download.php/1958/libhid-0.2.16.tar.gz&quot;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">tar -xzf *.tar.gz</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">rm *.gz</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">cd lib*</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">./configure &amp;&amp; make</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">sudo make install</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;"># We need a symbolic link to the libhid output for later</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">sudo ln -s /usr/local/lib/libhid.so.0 /usr/lib/libhid.so.0</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">sudo ldconfig</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;"># Get the source code for the dcled component</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">wget &quot;http://www.last-outpost.com/~malakai/dcled/dcled-2.0.tgz&quot;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">tar -xzf *.tgz</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">rm *.tgz</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">cd dc*</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;"># Make the dcled component</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">sudo pacman -S make libusb</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">make</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">sudo make install</span></p>
</div>
<p>I’m not 100% happy with the above process: for home use it’s OK, but as the required library, <a href="http://libhid.alioth.debian.org" target="_blank">libhid</a> is licensed under the GPL license there are serious limitations on how you might want to release this as </p>
<blockquote>
<p><em>We realise that this is a serious impediment. The GPL is a &quot;viral&quot; licence, and you will only be able to use libhid in other GPL projects. We would like to change the licence, but libhid uses the </em><a href="http://www.mgeups.com/opensource/projects/hidparser.htm"><em>MGE UPS SYSTEMS HID Parser</em></a><em>, which is GPL, and thus we cannot. Our solution is to rewrite the HID parser. One of these days. We are also in contact with MGE, trying to convince them to loosen their licence. If the licencing issues are solved, we are likely to re-release libhid under the </em><a href="http://www.opensource.org/licenses/artistic-license.php"><em>Artistic Licence</em></a><em>.</em></p>
</blockquote>
<p>That said, there is apparently hope that it’ll be possible at some point to swap this module out for <a href="https://twitter.com/asbradbury/status/242317798750187520" target="_blank">an alternative</a>.</p>
<p>Once the needed components are built, it should be a simple matter of calling the dcled command to test whether it displays text properly or not. Here are the usage instructions for dcled:</p>
<div style="font-family: courier new; background: white; color: black; font-size: 8pt;">
<p style="margin: 0px;"><span style="line-height: 140%;">[pi@alarmpi ~]$ <span style="color: #ff0000;">dcled --help</span></span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Usage- dcled [opts] [files]</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; --brightness&#0160; -b&#0160;&#0160; How bright, 0-2</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; --clock&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; -c&#0160;&#0160; Show the time</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; --clock24h&#0160;&#0160;&#0160; -C&#0160;&#0160; Show the 24h time</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; --bcdclock&#0160;&#0160;&#0160; -B&#0160;&#0160; Show the time in binary</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; --debug&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; -d&#0160;&#0160; Mostly useless</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; --echo&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; -e&#0160;&#0160; Send copy to stdout</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; --help&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; -h&#0160;&#0160; Show this message</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; --message&#0160;&#0160;&#0160;&#0160; -m&#0160;&#0160; A single line message to scroll</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; --nodev&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; -n&#0160;&#0160; Don&#39;t use the device</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; --preamble&#0160;&#0160;&#0160; -p&#0160;&#0160; Send a graphic before the text.</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; --repeat&#0160;&#0160;&#0160;&#0160;&#0160; -r&#0160;&#0160; Keep scrolling forever</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; --fastprint&#0160;&#0160; -f&#0160;&#0160; Jump to end of message.</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; --speed&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; -s&#0160;&#0160; General delay in ms</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; --test&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; -t&#0160;&#0160; Output a test pattern</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; --font&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; -g&#0160;&#0160; Select a font</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; --fontdir&#0160;&#0160;&#0160;&#0160; -G&#0160;&#0160; Select a font directory</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">Available preamble graphics:</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160; 1 - dots&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; - A string of random dots</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160; 2 - static&#0160;&#0160;&#0160;&#0160; - Warms up like an old TV</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160; 3 - squiggle&#0160;&#0160; - A squiggly line</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160; 4 - clock24&#0160;&#0160;&#0160; - Shows the 24 hour time</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160; 5 - clock&#0160;&#0160;&#0160;&#0160;&#0160; - Shows the time</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160; 6 - spiral&#0160;&#0160;&#0160;&#0160; - Draws a spiral</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160; 7 - fire&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; - A nice warm hearth</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160; 8 - bcdclock&#0160;&#0160; - Shows the time in binary</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">Optional fonts:</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160; 1 - small&#0160;&#0160;&#0160;&#0160;&#0160; - Very small characters</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160; 2 - sga&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; - Standard galactic alphabet</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160; 3 - small_inv&#0160; - Very small inverted characters</span></p>
</div>
<p>You can easily test the unit, then, by using this command to send a repeatedly scrolling message to the screen:</p>
<div style="font-family: courier new; background: white; color: black; font-size: 8pt;">
<p style="margin: 0px;"><span style="line-height: 140%;">sudo dcled -r &quot;This is a test&quot;</span></p>
</div>
<p>If you see the following message, then you’ve probably forgotten to run as root via sudo:</p>
<div style="font-family: courier new; background: white; color: black; font-size: 8pt;">
<p style="margin: 0px;"><span style="line-height: 140%;">hid_force_open failed with return code 6</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Couldn&#39;t find the device.&#0160; Was expecting to find a readable</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">device that matched vendor 1d34 and product 13.&#0160; Is the</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">device plugged in? Do you have permission?</span></p>
</div>
<p>You can also use the lsusb command to check whether the device has been recognised by the Pi.</p>
<p>Now we have the driver working, it’s a fairly simple matter to create another daemon to look for files in a certain folder and then send the messages they contain to the message-board before deleting them. We’ll use the &quot;-p 7” option to cause the message to be posted with a flame-like pre- &amp; post-amble.</p>
<p>Here’s the C/C++ code I used to implement this:</p>
<div style="font-family: courier new; background: white; color: black; font-size: 8pt;">
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">#include</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #a31515;">&lt;sys/types.h&gt;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">#include</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #a31515;">&lt;sys/stat.h&gt;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">#include</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #a31515;">&lt;stdio.h&gt;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">#include</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #a31515;">&lt;stdlib.h&gt;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">#include</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #a31515;">&lt;fcntl.h&gt;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">#include</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #a31515;">&lt;errno.h&gt;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">#include</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #a31515;">&lt;unistd.h&gt;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">#include</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #a31515;">&lt;syslog.h&gt;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">#include</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #a31515;">&lt;dirent.h&gt;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">#include</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #a31515;">&lt;string.h&gt;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">#include</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #a31515;">&lt;string&gt;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">#include</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #a31515;">&lt;vector&gt;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">#include</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #a31515;">&lt;iostream&gt;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">#include</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #a31515;">&lt;fstream&gt;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">#include</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #a31515;">&lt;algorithm&gt;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">#include</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #a31515;">&lt;syslog.h&gt;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">using</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">namespace</span><span style="line-height: 140%;"> std;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%; color: green;">// Input and output folder locations</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">const</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">char</span><span style="line-height: 140%;"> * inDir = </span><span style="line-height: 140%; color: #a31515;">&quot;/home/pi/faces/out&quot;</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%; color: green;">// Get the list of files in a directory</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">int</span><span style="line-height: 140%;"> getdir(string dir, vector&lt;string&gt; &amp;files)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; DIR *dp;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: blue;">struct</span><span style="line-height: 140%;"> dirent *dirp;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: blue;">if</span><span style="line-height: 140%;">((dp = opendir(dir.c_str())) == NULL)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">char</span><span style="line-height: 140%;"> msg[200];</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; snprintf(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; msg, </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">sizeof</span><span style="line-height: 140%;">(msg)-1,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #a31515;">&quot;Error(%d) opening %s&quot;</span><span style="line-height: 140%;">,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; errno,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; dir.c_str()</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; );</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; syslog(LOG_INFO, msg);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">return</span><span style="line-height: 140%;"> errno;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; }</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: blue;">while</span><span style="line-height: 140%;"> ((dirp = readdir(dp)) != NULL)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; files.push_back(string(dirp-&gt;d_name));</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; }</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; closedir(dp);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; sort(files.begin(), files.end());</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: blue;">return</span><span style="line-height: 140%;"> 0;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">}</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">int</span><span style="line-height: 140%;"> main(</span><span style="line-height: 140%; color: blue;">void</span><span style="line-height: 140%;">)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: green;">// Our process ID and Session ID</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; pid_t pid, sid;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: green;">// Fork off the parent process</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; pid = fork();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: blue;">if</span><span style="line-height: 140%;"> (pid &lt; 0)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; exit(EXIT_FAILURE);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; }</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: green;">// If we got a good PID, then we can exit the parent process</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: blue;">if</span><span style="line-height: 140%;"> (pid &gt; 0)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; exit(EXIT_SUCCESS);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; }</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: green;">// Change the file mode mask</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; umask(0);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: green;">// Open any logs here</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; openlog(</span><span style="line-height: 140%; color: #a31515;">&quot;ledmsgd&quot;</span><span style="line-height: 140%;">, LOG_PID|LOG_CONS, LOG_USER);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: green;">// Create a new SID for the child process</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; sid = setsid();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: blue;">if</span><span style="line-height: 140%;"> (sid &lt; 0)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// Log the failure</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; syslog(LOG_INFO, </span><span style="line-height: 140%; color: #a31515;">&quot;Unable to get SID.&quot;</span><span style="line-height: 140%;">);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; closelog();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; exit(EXIT_FAILURE);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; }</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: green;">// Change the current working directory</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: blue;">if</span><span style="line-height: 140%;"> (chdir(</span><span style="line-height: 140%; color: #a31515;">&quot;/&quot;</span><span style="line-height: 140%;">) &lt; 0)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// Log the failure</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; syslog(LOG_INFO, </span><span style="line-height: 140%; color: #a31515;">&quot;Unable to change working directory.&quot;</span><span style="line-height: 140%;">);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; closelog();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; exit(EXIT_FAILURE);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; }</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: green;">// Close out the standard file descriptors</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; close(STDIN_FILENO);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; close(STDOUT_FILENO);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; close(STDERR_FILENO);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: green;">// Daemon-specific initialization goes here</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: blue;">struct</span><span style="line-height: 140%;"> stat st = {0};</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: blue;">if</span><span style="line-height: 140%;"> (stat(inDir, &amp;st) == -1)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; mkdir(inDir, 0700);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; }</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; vector&lt;string&gt; files = vector&lt;string&gt;();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: blue;">const</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">char</span><span style="line-height: 140%;"> *contents = NULL;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: green;">/* The Big Loop */</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; syslog(LOG_INFO, </span><span style="line-height: 140%; color: #a31515;">&quot;main loop begins&quot;</span><span style="line-height: 140%;">);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: blue;">while</span><span style="line-height: 140%;"> (1)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// Get the files in our &quot;in&quot; directory</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; files.clear();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; getdir(inDir, files);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">if</span><span style="line-height: 140%;"> ((</span><span style="line-height: 140%; color: blue;">int</span><span style="line-height: 140%;">)files.size() &gt;= 3)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; contents = files[2].c_str();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">else</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; contents = NULL;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">if</span><span style="line-height: 140%;"> (contents != NULL)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; syslog(LOG_INFO, </span><span style="line-height: 140%; color: #a31515;">&quot;found a file&quot;</span><span style="line-height: 140%;">);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; syslog(LOG_INFO, contents);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">char</span><span style="line-height: 140%;"> input[256];</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; input[0] = 0;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; strcat(input, inDir);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; strcat(input, </span><span style="line-height: 140%; color: #a31515;">&quot;/&quot;</span><span style="line-height: 140%;">);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; strcat(input, contents);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">char</span><span style="line-height: 140%;"> cmd[200];</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; snprintf(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; cmd,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">sizeof</span><span style="line-height: 140%;">(cmd)-1,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #a31515;">&quot;dcled -p 7 %s&quot;</span><span style="line-height: 140%;">,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; input</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; );</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; system(cmd);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; remove(input);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; sleep(0.5); </span><span style="line-height: 140%; color: green;">/* wait half a second */</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; }</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; closelog();</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; exit(EXIT_SUCCESS);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">}</span></p>
</div>
<p>The simplest way to get this source onto the device is to wget it from this blog. Then it’s a simple matter of building it (again, apologies for the lack of makefile):</p>
<div style="font-family: courier new; background: white; color: black; font-size: 8pt;">
<p style="margin: 0px;"><span style="line-height: 140%;">wget &quot;http://through-the-interface.typepad.com/files/LedMsgDaemon.cpp&quot;</span> </p>
<p style="margin: 0px;"><span style="line-height: 140%;">g++ LedMsgDaemon.cpp -o ledmsgd</span></p>
</div>
<p>Once built you should copy &amp; paste the executable to the appropriate folder:</p>
<div style="font-family: courier new; background: white; color: black; font-size: 8pt;">
<p style="margin: 0px;"><span style="line-height: 140%;">cp ledmsgd /etc/rc.d</span></p>
</div>
<p>And edit the last line of the /etc/rc.conf file to make sure ledmsgd gets launched along with facerecd on boot:</p>
<div style="font-family: courier new; background: white; color: black; font-size: 8pt;">
<p style="margin: 0px;"><span style="line-height: 140%;">DAEMONS=(!hwclock syslog-ng network openntpd @netfs @crond @sshd @motion ledmsgd facerecd)</span></p>
</div>
<p>That should be it for getting the last of our components in place. At this stage, you should now have an at least partially functional security webcam, assuming you’ve been able to train and copy across a face database in the form of a <em>facedata.xml</em> file.</p>
<p>For fun, here’s a quick test of my own device at my home’s front door. I hadn’t realised that the human eye (and brain) manages to create the effect of scrolling text from an LED message-board better than when not filtered through a video recording (even when in HD), but then I suppose that saves me the trouble of attempting to protect the innocent (i.e. my Facebook friends, if you can call them that. ;-)</p>
<iframe allowfullscreen="allowfullscreen" frameborder="0" height="352" src="http://www.youtube.com/embed/FuyS8TEeN4g" width="470"></iframe>
<p>I looked at the debug images that were stored in the <em>~/faces/debug folder</em>, and saw that in general my face was getting detected appropriately, even if the recognition process clearly still needs tweaking to reduce (eliminate?) the false positives:</p>
<p><a href="http://through-the-interface.typepad.com/.a/6a00d83452464869e2017d3c9e5b34970c-pi" target="_blank"><img alt="Captured by the Facecam" border="0" height="201" src="/assets/image_257406.jpg" style="background-image: none; margin: 20px auto; padding-left: 0px; padding-right: 0px; display: block; float: none; padding-top: 0px; border-width: 0px;" title="Captured by the Facecam" width="201" /></a></p>
<p>Part of the issue clearly stems that we don’t look the same when captured via a security camera (especially when recording a video about the experience :-) as we do when we get tagged in photos on Facebook. It’s very possible the issue goes deeper than that, but I’m going to leave my investigations there, for now.</p>
