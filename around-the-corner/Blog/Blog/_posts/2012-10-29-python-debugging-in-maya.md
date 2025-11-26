---
layout: "post"
title: "Python debugging in Maya"
date: "2012-10-29 01:13:03"
author: "Cyrille Fauvel"
categories:
  - "Cyrille Fauvel"
  - "Debugging"
  - "Linux"
  - "Mac"
  - "Maya"
  - "Python"
  - "Test"
  - "Tools"
  - "Visual Studio"
  - "Windows"
original_url: "https://around-the-corner.typepad.com/adn/2012/10/python-debugging-in-maya.html "
typepad_basename: "python-debugging-in-maya"
typepad_status: "Publish"
---

<p>With the power of the Maya API and the scriptability of MEL, Maya Python hits a sweet spot in Maya&#39;s suite of development tools. Being an interpreted language it allows for quick turnaround, making it perfect for rapid prototyping and with thousands or dedicated users around the world contributing modules to Python, Maya inherits a large and growing array of new capabilities.</p>
<p>
<a class="asset-img-link" href="http://around-the-corner.typepad.com/.a/6a0163057a21c8970d017c32e657b1970b-pi" style="display: inline;"><img alt="Debug" border="0" class="asset  asset-image at-xid-6a0163057a21c8970d017c32e657b1970b" src="/assets/image_857465.jpg" style="display: block; margin-left: auto; margin-right: auto;" title="Debug" /></a></p>
<p>However, debugging Python code in Maya has not made to be easy like it is with C++ and Visual Studio on Windows for example. We have seen Maya Script editor improvements in the Maya 2011 release with the integration of Qt, and the syntax color highlighting, but still no tools to help developers to test and debug their scripts. All you can do at this time, is really add some print() statement with meaningful message to tell you what’s happening in your code, but you have no control on the execution, nor it is easy and/or productive to create hundreds on print() messages and remove them at the end when your code is completed. While this is still a valid technique, it is a pain and counterproductive to have to do this when you know there is debugging tools around for doing this in a professional manner.</p>
<p>The idea, here, is to modify Maya in a way Python debugging capability would be nicely integrated into Maya to bring more power to Maya customers and make Python the first scripting choice for the customer and developer community. This will also bring to Maya developers a more robust and productive approach in developing scripts for Maya to automate their pipelines. The 2 advantages with this tool is that not only it make the production of scripts more easy and professional, but it makes the effort of learning Python code into Maya more easy. That means, non professional developers can easily learn and test their Python skills within Maya and later make Maya more powerful in their business.</p>
<p>Is it something completely new? No, there are Python debuggers around already, but none of them are integrated with Maya at all. They are generic Python debugger, for debugging generic Python code outside Maya. This post is to tightly integrate a Python debugger inside Maya to debug and test Maya Python scripts and Maya Python plug-ins inside Maya. The main difficulty apart integrating a debugger into Maya was to counter the fact Maya and Python store previously executed code into memory. This solution controls and teaches Maya to work with a debugger and let the debugger to control what code should be executed when and in which context and to avoid any interference with a scene.</p>
<p>At the end, after installing couple of files on the system, you will see a simple button next to the Execute button to start Executing your code, but in debug mode this time. From there, you can choose to step through your code and see intermediate results before executing the next instruction. This is clearly a plus for anyone who wants to use Python in Maya and will help the Python adoption from the Maya community.</p>
<p>But before going into the details, let&#39;s review the options we have today (not an exhaustive list)</p>
<h2>Python Debugger Options</h2>
<h3>The basics - The PDB debugger</h3>
<p>The PDB debugger is a standard part of every Python implementation. You can use it to step through the execution of a Python function one statement at a time. However PDB alone is not that helpful because even if it gives you the ability to access everything and step through the code, using it in Maya make things even more complicated.</p>
<p>After each command is executed PDB prints a line giving the module and line number of the next statement, as well as the name of the function that it is in followed by the statement itself, then a prompt for the next command. A debugger GUI can react to this to have a nice integration, but when run from within Maya the behavior is slightly different. Since Maya&#39;s Script Editor does not have a mechanism for responding to prompts within the Script Editor window, it instead displays a separate input window each time PDB prompts for a new command. You would, for example, enter your &#39;s&#39; or &#39;n&#39; command into the input field, then press Enter or click on the OK button to have PDB execute it. This can become rather cumbersome because each time the window appears, the focus is on the OK button, and so you must first change the focus to the input field, then enter your command and press Enter. Another issue is that even if you change this behavior, the window will keep coming back right in the middle of the screen making it hard to step through the code.</p>
<p>Dean Edmonds, the Maya API architect presented a solution to this problem in his ‘Advanced Maya Python’ talk at the Maya Developer Conference, and has shown how to make the interface smoother to use. However, despite what we can do, with this solution we would still miss all the debugger tools one may like to have (call stack, local/global variables, modules &amp; packages, etc...).</p>
<h3>Winpdb</h3>
<p>
<a class="asset-img-link" href="http://around-the-corner.typepad.com/.a/6a0163057a21c8970d017d3d09aad7970c-pi" style="display: inline;"><img alt="Winpdb" border="0" class="asset  asset-image at-xid-6a0163057a21c8970d017d3d09aad7970c" src="/assets/image_3e53ca.jpg" style="display: block; margin-left: auto; margin-right: auto;" title="Winpdb" /></a></p>
<p><a href="http://winpdb.org/" target="_self">Winpdb </a>is a platform independent GPL Python debugger with support for multiple threads, namespace modification, embedded debugging, encrypted communication and is up to 20 times faster than pdb. It offers a user interface and all the&#0160;basics debug features. However the IDE is not that great to write code compared to some other tools available.</p>
<p>What is interesting and What we need for Maya is its ability to debug Python code running in another application. There is a good documentation <a href="http://winpdb.org/docs/embedded-debugging/" target="_self">here</a>&#0160;which explains how to do this.</p>
<h2>Python Debugger Options -&#0160;A true debugger</h2>
<h3>Eclipse and Pydev</h3>
<p>
<a class="asset-img-link" href="http://around-the-corner.typepad.com/.a/6a0163057a21c8970d017c32db20a5970b-pi" style="display: inline;"><img alt="Eclipse" border="0" class="asset  asset-image at-xid-6a0163057a21c8970d017c32db20a5970b" src="/assets/image_d5677c.jpg" style="display: block; margin-left: auto; margin-right: auto;" title="Eclipse" /></a>One
solution is to use an existing developer environment such as <a href="http://www.eclipse.org/">Eclipse</a>. Eclipse
is a collection of open source projects built on the Equinox OSGi run-time.
Eclipse started as a Java IDE, but has since grown to be much, much more.
Eclipse projects now cover static and dynamic languages; thick-client,
thin-client, and server-side frameworks; modeling and business reporting;
embedded and mobile. Eclipse is a versatile IDE which supports many programming
languages and can be extended by plug-ins. Eclipse also has a framework which
includes the ability to support a debugger environment. Why choose Eclipse?
This is because it is free and supports all the platforms Maya is running on.</p>
<p>Once
you have Eclipse installed, you still need to connect to PDB to be able to step
through Maya Python code. You may write your own Eclipse plug-in to establish
the connection with the Python kernel and debug core, but there is already a
free solution for you. Checkout <a href="http://pydev.org/">http://pydev.org/</a>&#0160;&#0160;</p>
<h4>PyDev
</h4>
<p><a class="asset-img-link" href="http://around-the-corner.typepad.com/.a/6a0163057a21c8970d017c32db1ee3970b-pi" style="display: inline;"><img alt="Pydev_banner2" border="0" class="asset  asset-image at-xid-6a0163057a21c8970d017c32db1ee3970b" src="/assets/image_795b9a.jpg" style="display: block; margin-left: auto; margin-right: auto;" title="Pydev_banner2" /></a></p>
<p>PyDev
is a Python IDE for Eclipse, which may be used in Python, Jython and IronPython
development. It has Pylint and Mylyn integration, syntax highlighting, code
analysis and many other features. Most importantly, it has the capability to
debug Python code which is what we are interested in today.</p>
<h3>Wing from Wingware</h3>
<p>
<a class="asset-img-link" href="http://around-the-corner.typepad.com/.a/6a0163057a21c8970d017c32db24ac970b-pi" style="display: inline;"><img alt="Wing" border="0" class="asset  asset-image at-xid-6a0163057a21c8970d017c32db24ac970b" src="/assets/image_e90e97.jpg" style="display: block; margin-left: auto; margin-right: auto;" title="Wing" /></a><br />Definitely my favorite with Eclipse/PyDev. <a href="http://wingware.com/" target="_self">Wing</a>&#0160;is the most complete package for writing and debugging Python code and has a nice <a href="http://wingware.com/doc/howtos/maya" target="_self">Maya integration</a> very easy to install. Not only the integration with Maya is simple and already there, but it has almost everything you want to write &amp; debug Python code in Maya. Definitely an option to consider when writing/debuging Python code for Maya.</p>
<h3>PTVS - Python integration for Visual Studio</h3>
<p>This is the last one I have been using and it comes with the same feature set Eclipse/Pydev and Wing have. When I first tried it, it was very new but had everything there. Since it evolves a lot and a good choice. But you may be surprised how the connection between Maya/Python and the tool in hapening. Where Eclipse/PyDev &amp; Wing run like a server and wait for a signal from the code. With PTVS you&#39;ll attach Visual Studio to a process to debug. Just different! The main issue with PTVS is that it is a Windows only solution, so it won&#39;t apply to those of you using MacOS or Linux.</p>
<h2>To be continued...</h2>
<p>In the next post we will see in details how to plug Eclipse/Pydev into Maya to debug plug-ins and Maya Script editor code.</p>
