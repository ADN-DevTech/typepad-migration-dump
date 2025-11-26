---
layout: "post"
title: "Maya and Python a love story?"
date: "2012-06-03 11:25:48"
author: "Cyrille Fauvel"
categories:
  - "C++"
  - "Cyrille Fauvel"
  - "Maya"
  - "MEL"
  - "Python"
original_url: "https://around-the-corner.typepad.com/adn/2012/06/maya-and-python-a-love-story.html "
typepad_basename: "maya-and-python-a-love-story"
typepad_status: "Publish"
---

<p>With version 8.5, Maya introduced Python as a complementary language to MEL and C++. I have to say, Python isn’t my preference (sorry Kristine – she is a big fan of Python ;). I said it isn’t my preference but not because of what Python is or can do, but because of the indentation requirements and syntax scheme. I feel returning to my Lisp experience with AutoCAD 25 years ago where if you had forgotten a parenthesis somewhere, your code would not execute properly, and you don’t know why up front&nbsp;%\. I would have preferred myself a more elegant syntax wise language such as PHP or Javascript. Javascript being more popular than Python as well.</p>
<p><a class="asset-img-link" style="display: inline;" href="http://around-the-corner.typepad.com/.a/6a0163057a21c8970d01761534fd82970c-pi"><img class="asset  asset-image at-xid-6a0163057a21c8970d01761534fd82970c" style="display: block; margin-left: auto; margin-right: auto;" title="Love-story" src="/assets/image_59ad02.jpg" border="0" alt="Love-story" /></a></p>
<p>Anyway, Python is very popular in WEB programing and is now as well in the Maya scripting ecosystem. Every year I am running an API survey in the developer community and I can see a trend on the Python adoption. Python is there and to stay.</p>
<p>Back to the technical side. Python is very powerful like all the other scripting languages, but before anything I need to clarify one thing which did confuse people a lot in the past. There is 2 implementation of Python in Maya.</p>
<ol>
<li>the command interpreter</li>
<li>the API</li>
</ol>
<p>The command interpreter is an attempt to replace MEL as the scripting language of Maya while Maya still heavily use MEL for all its UI paradigm. Also the command interpreter implementation wasn’t really design like it should have been. For example the following code isn’t taking any advantages of the Python language. If you wonder why that is, the answer is that it was generated automatically using some strange rules to simplify the port at that time. But once it is there, you can’t change it :(</p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; <span style="color: blue;">import</span> maya<span style="color: blue;">.</span>cmds <span style="color: blue;">as</span> cmds</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; cmds<span style="color: blue;">.</span>polySphere <span style="color: teal;">(</span>r<span style="color: teal;">=</span>’5’<span style="color: teal;">)</span></p>
</div>
<p>Chad Dombrova from ‘Luma Pictures’ made an attempt with PyMel to solve that issue (Note that PyMel is now part of the Maya distribution even if PyMel isn’t maintained by Autodesk). PyMel solves a lot of syntax issue for the please of Python developers.</p>
<p>The API implementation was also generated automatically using SWIG. SWIG is very powerful, but as any automatic instrumentation, it does only was it was told to do. This is why you can find a 1 to 1 match between the C++ and Python API with very few additions on the Python side to convert C++ types to Python and vice et versa. One of the is that since Python do not have notion of pointers and that the Maya C++ API uses pointers in some places, the Maya team had to adjust the API accordingly. See the MScriptUtil Python class for example.</p>
<p>Another thing to note is that the Python API was not accessible in the Script editor at the beginning, but since few releases it is. That means you can mix commands and API calls all together in your Python code – a nice plus for Python.</p>
<p>But the real question is why using Python versus MEL / C++?</p>
<p>The question isn’t really easy to answer, but I’ll try to give some hints for you to decide what is best for you.</p>
<p>First, be aware that Python is an interpreted language and that means it can have a performance hit vs C++. Someone may tell, yes interpreted but you can compile it. Don’t trust that guy – unfortunately, that guy should have said precompiled, because Python is never compiled, it remains an interpreted language. All precompile means is that the ASCII formatted is precompiled is a simpler / lighter component that the Python interpreter would not need to parse and verify syntax. Performance wise this has almost no impact.</p>
<p>In practice, using Python to call a Maya command or a Maya API would not have any significant performance hit. That is because the Maya Python implementation is a thin wrapper of its MEL or C++ equivalent. Where Python is bad at for Maya is when you do either compute any non Maya API or call a limited set of the Maya API where Maya needs to convert Python types to Maya types and vice et versa. On this last item, the performance hit will show up only if you do this in a loop for a big number of iteration.</p>
<p>Second, I mentioned in the previous paragraph the notion of precompile Python module. Someone can decide to not send its .py file, but the .pyo/.pyc file instead. The code will execute fine on the machine and Python would be happy with that as it creates these anyway from the .py file before executing. But do not think it is a way for you to protect your IP – it is fairly easy to decompile and retrieve the original source code (with losing the function, variable names). But it is a good way to redistribute modules and prevent end users to make any changes in your code easily.</p>
<p>Third, Python is a great tool at prototyping, the Maya API architect does this a lot – prototyping in Python and do the final implementation in C++. Because you do not need to compile the workflow for development/testing is a lot simpler.</p>
<p>Finally, like C++ (but unlike MEL) you can debug Python code using WinG, Eclipse, Visual Studio, etc… I'll talk more specifically about this in a future post.&nbsp;</p>
