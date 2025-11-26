---
layout: "post"
title: "Top 10 Autodesk Maya Python API Gotchas When Getting Started"
date: "2012-07-05 23:14:00"
author: "Kristine Middlemiss"
categories:
  - "Autodesk"
  - "Kristine Middlemiss"
  - "Linux"
  - "Mac"
  - "Maya"
  - "Python"
  - "Windows"
original_url: "https://around-the-corner.typepad.com/adn/2012/07/top-10-autodesk-maya-python-api-gotchas-when-getting-started.html "
typepad_basename: "top-10-autodesk-maya-python-api-gotchas-when-getting-started"
typepad_status: "Publish"
---

<ol start="10">
  <li><strong>Not Understanding the Difference between Python Script and  Python API</strong> <br />
  Maya  has four programming interfaces, two of which are using the  Python language. It&rsquo;s important to know the distinction between the terminology  of Python Script and the Python API, because they each access different  functionality within Maya (with very little overlap). The other two interfaces are  Maya Embedded Language (MEL) and the C++ API. </li>
</ol>
<ol start="9">
  <li><strong>Python Plug-in  Disclosure </strong><br />
    You cannot hide your Python API code  from users! If you&rsquo;re a games developer looking to create Python plug-ins  in-house and not worried about protecting your code, then feel free to code  away. But it you want to protect, or commercialize and sell your hard work, you  should consider using C++ to create your plug-ins.</li>
</ol>
<ol start="8">
  <li><strong>Must use Autodesk  Specific Version of Qt for Building PyQt</strong><br />
  Building PyQt to work with Maya  is a three step process. First you need to compile Qt using the Autodesk's  Modified Qt Source Code (new for 2012) that is located on the Autodesk.com  site. Then you need to compile SIP (generates Python bindings for C++) against  your built Qt libraries. Last, you need to compile PyQt against your built SIP  libraries. Check the website for the correct versions or you could have some  difficulties down the road in your tools.</li>
</ol>
<ol start="7">
  <li><strong>Awareness of Potential  Python API Performance Penalties</strong><br />
  In certain situations when  computation involves complex or many API calls, the Python API may take a  performance penalty compared to the C++ API. This is because the Python API  sits on top of the C++ API. This results in an extra level of conversion  through C++ and as well as Python is an interpreted language which is slower by  nature. For example if you are thinking of doing a Python API shader, C++ will  be the better choice, but for most other tasks Python is perfect!</li>
</ol>
<ol start="6">
  <li><strong>Working with Multiple Versions of Maya Equals Multiple Versions  of Python</strong><br />
  If you are working on multiple  versions of Maya, then you need to keep in mind that you will need to have multiple  version of Python. Also if you are using PyQt, multiple versions of PyQt will  need to be installed on your computer. As nice as it would be to use the same  version of Python and PyQt for every version of Maya, it&rsquo;s just not realistic  as things are always improving in the Python and PyQt releases. Keep yourself  educated in the Maya documentation for the versions that are used in each major  release, and on which platform.</li>
</ol>
<ol start="5">
  <li><strong>Your Python API Code  is not Working, and you cannot find the Problem </strong><br />
  When working with the Python  API, like all humans, you will have errors or code you want to debug; but how  the heck do you debug it? Unfortunately, there are no out-of-the-box tools in  Maya, but there are some good solutions available. Cyrille Fauvel from Autodesk  has integrated a Maya Python debugger into the Eclipse IDE. The details are  available on Autodesk.com, at the Maya Developer Center page. Dean Edmonds also  from Autodesk has integrated the PDB debugger into the Script Editor within  Maya by overriding the stdin and stout functions. Also, if you Google &ldquo;Python  in Maya&rdquo; group you will see other solutions.</li>
</ol>
<ol start="4">
  <li><strong>Knowing when and how  to Use MScriptUtil Class</strong><br />
  Many of the API methods in Maya  require that one or more of their parameters be passed as pointers or  references (return values can be pointers or references as well). Because  Python does not have the concept of references or pointers, you will need the  utility class called MScriptUtil for working with those pointers and references  in Python. MScriptUtil bridges this gap between Python and its underlying C++  API. When you see the characters * or &amp; in the documentation for simple  data types like integers and floats, think MScriptUtil!</li>
</ol>
<ol start="3">
  <li><strong>The Reference  Documentation is written for C++, but you are Using Python</strong><br />
  If you do not know C++, at  first glance the reference documentation will be very confusing!&nbsp; But don&rsquo;t worry; just put your eye blinders  on to certain things when reading it. Because Python is a more simplified  language than C++ (but none the less powerful), there are certain components in  the documentation you can ignore. For example the class MString and  MStringArray are not available in Python and instead you will work with strings  as they are in native Python. Another example is that there is no MStatus class  in Python and instead you will use standard Python error checking (try, except,  catch). Try to think of this as having a mini translator going on in your  brain, you see MStatus, so you just ignore it!
  </li>
  </ol>
<ol start="2">
  <li><strong>Trying to live in  a Bubble and Teach Yourself Everything </strong><br />
    There is no possible way to memorize  or learn every single Maya API class, so don&rsquo;t be shy to use the resources  available. As they say &ldquo;Be Resourceful&rdquo; and learn from others successes,  mistakes and questions. Maya has been around for over ten years, and there is  lots of API information out there (www.autodesk.com/developmaya). Also, you can  check out the new book &ldquo;Maya Python for Games and Film: A Complete Reference  for Maya Python and the Maya Python API&rdquo; by Adam Mechtley and Ryan Trowbridge.</li>
</ol>
<ol start="1">
  <li><strong>Not Fully  Grasping the Maya Architecture and How the Python API Leverages It</strong><br />
  If you don&rsquo;t completely understanding  the Maya architecture, command engine and the Dependency Graph, you will run  into trouble as your tools get more complex. Take the time to learn these  components as they are very different than any other animation product. Because  of this complexity, it makes Maya more flexible and powerful than any other  product. Check out the free Maya Python API webcast recording located here (www.autodesk.com/developmaya)</li>
</ol>
