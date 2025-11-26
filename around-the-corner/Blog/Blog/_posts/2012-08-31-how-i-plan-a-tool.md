---
layout: "post"
title: "How I Plan a Tool…"
date: "2012-08-31 02:08:00"
author: "Kristine Middlemiss"
categories:
  - "3ds Max"
  - "FBX"
  - "Kristine Middlemiss"
  - "Maya"
  - "MotionBuilder"
  - "Mudbox"
  - "SoftImage"
original_url: "https://around-the-corner.typepad.com/adn/2012/08/how-i-plan-a-tool.html "
typepad_basename: "how-i-plan-a-tool"
typepad_status: "Publish"
---

<p>This post isn’t super technical but more of peak into my thought process when creating tools, I think it is worth sharing and I would love to have other’s feedback if you have other thoughts that work for you.</p>
<p>I was once asked by an attendee from one of my Maya API trainings if I could share some wisdom with them about writing a tool. How do I plan my work? How do I start thinking about the way I am going to code it? They find they come across the &quot;blank canvas&quot; syndrome when starting to code plug-ins and tools.</p>
<p style="text-align: center;">&#0160; <a class="asset-img-link" href="http://around-the-corner.typepad.com/.a/6a0163057a21c8970d0177442650f7970d-pi" style="display: inline;"><img alt="Blank_canvas_sm" border="0" class="asset  asset-image at-xid-6a0163057a21c8970d0177442650f7970d" src="/assets/image_cb62e3.jpg" title="Blank_canvas_sm" /></a></p>
<p>So this is my view, Cyrille for example might have a completely different approach, which would fall under the “to each their own” category! ;) (Maybe he will share his views with you ;))</p>
<p>I am not really a professional artist or an Autodesk application super user per say, so generally I get my tool ideas from people that do fall into those categories.  You soon discover what really drives the creation of a tool is the need for the tool! If there is no need no one will likely use the tool and that’s not what we are looking for in tool creation.</p>
<p><strong>Determining the need…</strong><br />Let’s say that some functionality is missing our Autodesk product or that some functionality needs to be quicker/more efficient or a feature needs to be more accessible, and then a custom tool would be the solution to all of these problems. It’s not mandatory for you too to solve any one of these things but if you want your tool to actually get used then it’s a good idea! ;)</p>
<p>Once the need is determined from someone that would benefit from your tool, you will then need to start understanding the user’s needs.</p>
<p><strong>Understanding the user’s needs…</strong><br />The first thing you should do is to write out the tools in words, meaning your enter your tool this happens then that happens, etc. Because I can guarantee you at some point in the functionality of the tool you will make an assumption of something that is not exactly what the user needs, and let me tell you it’s way easier to change something when your planning then at that end after you have coded it.<br />For example if your tool needs a user interface, it would make sense to either drawing out the UI by hand or using a WSIWIG (What you see is what you get) tool like Qt Designer to mock up what you’re thinking then check back with the requester and see if that’s actually what they are thinking, because you would be surprised how people describe things and actually mean something completely different.</p>
<p>Back in my computer science education days, we had to even draw out a UML(Unified Modeling Language) chart of what happens when and where, I can’t say I actually do this as formally as this now, but I definitely do some chicken scratch drawing of this in some form or another, so when I start coding it is truly is easier, as all the logical thinking is already done, and I have worked through the what will “happen here” stages.</p>
<p>After I have the master plan in place and it aligns with what the users action need, I then figure out how to do it in the software application (MotionBuilder, Maya, FBX, etc.), then I can write the code for it.</p>
<p><strong>Understanding the software…</strong><br />In certain application such as Autodesk MotionBuilder, the SDK commands are really reflective of the UI usage, such as several actions in the UI is usually several Python calls. So implementing the tool can be a little more straightforward in this case, including understanding the limitation of the SDK.&#0160; However, this is not the case in all applications such as Autodesk Maya; the API architecture is very different from the UI usage so more thought would have to be put in to understanding the API for a Maya plug-in and since Maya is very powerful, there are not a lot of obvious limitations, they fall more into small workflows things that can be trickier to spot when planning.</p>
<p>The key to planning is breaking down the problem into little bits, once you realize what steps need to be done then you can find this functionality in the API of your choice class documentation which can take some time. I usually start with the name of the functionality and look for that word in the search section for the class documentation, and kind of start from there.</p>
<p>So the important thing to remember is talk to people see what they need and want, then PLAN, PLAN, PLAN by making sure you understand the problem people want you to solve because if you don’t it makes coming up with&#0160;the solution a lot more challenging :)</p>
<p>Enjoy,</p>
<p>Kristine</p>
