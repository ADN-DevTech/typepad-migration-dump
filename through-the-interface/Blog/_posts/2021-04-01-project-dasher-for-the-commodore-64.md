---
layout: "post"
title: "Project Dasher for the Commodore 64"
date: "2021-04-01 07:09:45"
author: "Kean Walmsley"
categories:
  - "Retro computing"
original_url: "https://www.keanw.com/2021/04/project-dasher-for-the-commodore-64.html "
typepad_basename: "project-dasher-for-the-commodore-64"
typepad_status: "Publish"
---

<p>I’ve been having soooo much fun with my <a href="https://www.youtube.com/playlist?list=PLMPZ0Qz6RKGpOJuHbVY5aVYQntaDEMXZp" rel="noopener" target="_blank">Floppy Fridays</a> that I couldn’t resist trying my hand at developing some code for the Commodore 64. It meant dusting off my BASIC skills – and even dipping into some Assembler for some of the <a href="https://c64.ch/programming/" rel="noopener" target="_blank">more advanced effects</a> – but I think the results are worth it.</p>
<p>After some months of behind-the-scenes effort, I can proudly unveil my (and Autodesk Research’s) first project for the C64. Although I found out last week that John Walker – Autodesk’s founder and former CEO – also <a href="https://www.keanw.com/2021/03/floppy-friday-22.html" rel="noopener" target="_blank">developed a number of programs for the C64</a> back when he was exploring its capabilities in the late 1980s. Ah well, we were only about 30 years late to the party. :-P</p>
<p>Anyway, without further ado, here it is. I give you… Project Dasher for the Commodore 64!</p>
<p style="text-align: center;"><br /></p>
<p style="text-align: center;"><iframe allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture" allowfullscreen="" frameborder="0" height="283" src="https://www.youtube.com/embed/oMrM5lm7aAs" title="YouTube video player" width="500"></iframe></p>
<p><br /></p>
<p>This preview video probably raises a bunch of questions about the project. The current <a href="https://dasher360.com" rel="noopener" target="_blank">Project Dasher</a> is clearly highly dependent on connectivity to allow it to access 3D models and sensor data. While it’s possible to connect a C64 to the interwebs – you can do so via a <a href="http://www.go4retro.com/products/64nic/" rel="noopener" target="_blank">64NIC+ Ethernet card</a> – it’s not very common, seeing as the commercialisation of the C64 happened a decade or so before the Internet became much of a thing. So we decided to support network access of data but also a mode where you can store sensor data on a series of floppy disks.</p>
<p>Once a project has been configured properly you can load the model from a single floppy (or at least a small number of pre-defined views, as loading a full BIM in memory on the C64 wasn’t practical) and then as the user hovers over an area of interest (such as a sensor dot) the program will ask the user to insert a particular floppy disk that’s been indexed as containing the relevant time-series data.</p>
<p>Storage capacity is clearly an issue: with only 170kB per floppy, we really didn’t want to force someone to use 10s or 100s of floppies per sensor. So along with adopting some highly innovative encoding schemes, we’ve defaulted to highly coarse data out of necessity. So expect the data to get a little pixelated, as you zoom in, but then anyone still using a C64 is used to seeing pixels. ;-)</p>
<p>We’re also aware of the fact that many C64 users don’t use a mouse: that’s why we’ve made sure you can move the cursor around and perform the most important functions using your favourite joystick.</p>
<p>If you’re interested in giving this a try, please post a comment or send me an email and I’ll send you a .d64 file that you can write to a physical floppy or load directly into an emulator such as <a href="https://vice-emu.sourceforge.io" rel="noopener" target="_blank">VICE</a>. Some work is still needed, but it’s enough to get a sense of the program’s direction.</p>
<p>If this proves popular, I’m hopeful it will inspire some of our product teams to invest in similar porting efforts. With <a href="https://www.pagetable.com/?p=547" rel="noopener" target="_blank">roughly 12.5 million C64s sold</a>, that seems a pretty significant market with what I expect to be a highly loyal user-base. Who’s interested?</p>
