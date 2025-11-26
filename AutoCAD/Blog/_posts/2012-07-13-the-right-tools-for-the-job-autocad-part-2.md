---
layout: "post"
title: "The Right Tools for the Job &ndash; AutoCAD Part 2"
date: "2012-07-13 16:12:30"
author: "Fenton Webb"
categories:
  - ".NET"
  - "2010"
  - "2011"
  - "2012"
  - "2013"
  - "AutoCAD"
  - "Fenton Webb"
  - "LISP"
  - "ObjectARX"
original_url: "https://adndevblog.typepad.com/autocad/2012/07/the-right-tools-for-the-job-autocad-part-2.html "
typepad_basename: "the-right-tools-for-the-job-autocad-part-2"
typepad_status: "Publish"
---

<p>by <a href="http://adndevblog.typepad.com/autocad/fenton-webb.html">Fenton Webb</a></p>
<p>Leading on from <a href="http://adndevblog.typepad.com/autocad/2012/07/the-right-tools-for-the-job-autocad-part-1.html">Part 1</a> - what else should you also think about before you get started with programming AutoCAD…?</p>
<p>Your specific performance requirements is one thing (and I’ll get to those details soon, I promise), but I there are other considerations too when deciding on the right AutoCAD programming language to adopt.</p>
<p>One thing that springs to mind, especially for beginners, is the learning curve and also the cost of start up. If you are just starting out, then you will find that the time it takes you to learn the API is actually a substantial investment for any customization programming work. I think you have to carefully consider each API’s learning curve and subsequent start up costs, as well as its feature sets, before you commit to any.</p>
<p>I think that VBA is probably the best suited for a pure beginner because it works the same way as the Macro functionality in the Microsoft Office tools, just with an AutoCAD Object model instead. That means that almost anyone who has played with Macros in Excel will be able to get something programmed inside of AutoCAD.</p>
<p>AutoLISP/ Visual LISP is a little more complex to learn in my opinion because it’s so unique but it was adapted to work with AutoCAD so it integrates really nicely.</p>
<p>Both VBA and Visual LISP are built into AutoCAD, both are composed using built in (free) development IDE’s and as I just mentioned both are easy to learn and use, VBA being the easiest. That tells me that these languages are perfect if:</p>
<ul>
<li>You want to just get things done quickly and effectively
<ul>
<li>You can be a master of computer science and still make really good use of VBA and LISP inside of AutoCAD</li>
<li>Short learning curve means you get results quicker</li>
</ul>
</li>
<li>You are on a tight budget 
<ul>
<li>No need to buy Visual Studio </li>
<li>All the tools you need come free with AutoCAD</li>
<li>Short learning curve means more time making real money</li>
</ul>
</li>
<li>You are a beginner programmer
<ul>
<li>Short learning curve means results come quicker and you realize the concepts quicker also</li>
</ul>
</li>
</ul>
<p>Coming in 3rd place for learning curve is .NET. .NET streamlines very complex programming through lots of really cool language and Object Model development, from Microsoft. In my opinion, it is the best programming toolset out there by a mile. The integration between the all of the latest programming technologies is just breath taking-ly cool; it’s just so nice to program with. I have to say though, if you are going to utilize the native AutoCAD .NET API, which wraps the underlying ObjectARX API making it very similar, then you may find the learning curve rather steep. The API is rather low level and even though it’s .NET it can be very easy to crash if you don’t do things right. That said, one great feature of .NET is its COM Interop capabilities – this allows you to easily utilize the VBA Object Model from VB.NET (or C# if you so desire) making VB.NET almost as easy to learn as I mentioned VBA is above. One extra point about .NET that in order to developer with it, you need Visual Studio, and the professional license costs $$$’s.</p>
<p>Finally, ObjectARX coming in last on my learning and cost curve. ObjectARX utilizes the C++ language, which is still very popular in the programming world and is still moving forward with the likes of Microsoft and Visual Studio. The code is compiled into native machine code, making it super fast, very capable but very difficult to work with compared to .NET. It’s low level accessibility is its power, but also its pain. If you don’t know C++, I recommend that you don’t bother trying to learn ObjectARX as it takes real experience to work with and that is going to cost you time, effort, and money to get up and running with it – instead I recommend you use .NET. That said, if your application requires real performance and some really special functionality, then ObjectARX is what you need, for sure.</p>
<p>Read Part 3 <a href="http://adndevblog.typepad.com/autocad/2012/07/the-right-tools-for-the-job-autocad-part-3.html">here</a></p>
