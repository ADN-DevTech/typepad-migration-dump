---
layout: "post"
title: "Floppy Friday #16"
date: "2021-02-12 07:15:00"
author: "Kean Walmsley"
categories:
  - "AJAX"
  - "Retro computing"
original_url: "https://www.keanw.com/2021/02/floppy-friday-16.html "
typepad_basename: "floppy-friday-16"
typepad_status: "Publish"
---

<p>This week’s <a href="https://www.youtube.com/playlist?list=PLMPZ0Qz6RKGpOJuHbVY5aVYQntaDEMXZp" rel="noopener" target="_blank">Floppy Friday</a> is all about Indiana Jones games. Well, two of them, anyway. First we look at <a href="https://en.wikipedia.org/wiki/Indiana_Jones_in_the_Lost_Kingdom" rel="noopener" target="_blank">Indiana Jones in the Lost Kingdom</a> from 1985 and then we play <a href="https://en.wikipedia.org/wiki/Indiana_Jones_and_the_Last_Crusade_%28video_game%29#The_Action_Game" rel="noopener" target="_blank">Indiana Jones and the Last Crusade – The Action Game</a> from 1989. They’re very different games, which I’m sure you’ll see from <a href="https://youtu.be/ydZ5lYKObNU" rel="noopener" target="_blank">this episode</a>.</p>
<p style="text-align: center;">&#0160;</p>
<p style="text-align: center;"><iframe allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture" allowfullscreen="" frameborder="0" height="283" src="https://www.youtube.com/embed/ydZ5lYKObNU" width="500"></iframe></p>
<p style="text-align: center;">&#0160;</p>
<p>I think I’ve managed to fix the focus-related issue from last week (this happened when there was a major change in luminosity with the screen), so hopefully that’s better. Feedback welcome!</p>
<p>I’ve also made some progress with repairing the second of my two “breadbin” C64 machines: I received a new (old stock) <a href="https://en.wikipedia.org/wiki/MOS_Technology_VIC-II" rel="noopener" target="_blank">VIC-II</a> chip from China, and it means the system can now basically run a game, albeit without any video output: it’s quite typical to try a cartridge game called <a href="https://en.wikipedia.org/wiki/Kick_(video_game)" rel="noopener" target="_blank">Kickman</a>, as it bypasses some of the chips in the C64 which makes it more likely to work in the case of an internal hardware failure.</p>
<p>I posted <a href="https://www.lemon64.com/forum/viewtopic.php?p=938917#938917" rel="noopener" target="_blank">what I’d found on the Lemon 64 forum</a>, and someone kindly replied with the clue that the remaining problem is likely to be with the RF modulator. They suggested I have a look inside and squirt some contact cleaner (and I’m happy to have gotten hold of some <a href="https://caig.com/deoxit-d-series/" rel="noopener" target="_blank">DeoxIT</a>, which is apparently the gold standard in that regard).</p>
<p>When I looked inside the RF modulator – which involved peeling back a solder-connected metal lid – I found an interesting surprise: a loose screw had found its way in, and was almost certainly guilty of shorting out some of the electronic components.</p>
<p><a href="https://through-the-interface.typepad.com/.a/6a00d83452464869e2026bdebcab68200c-pi" rel="noopener" target="_blank"><img alt="I have a screw loose" border="0" height="375" src="/assets/image_89229.jpg" style="margin: 30px auto; border: 0px currentcolor; float: none; display: block; background-image: none;" title="I have a screw loose" width="500" /></a></p>
<p>Ha! Removing it didn’t fix the problem, but it certainly explains what may have caused the computer to fail. I’ve gone ahead and ordered an <a href="https://videogameperfection.com/products/c64-svideo-bypass/" rel="noopener" target="_blank">RF replacement module</a>, which I’m hoping will help me get this particular system up and running. We’ll see!</p>
