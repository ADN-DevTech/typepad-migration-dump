---
layout: "post"
title: "Displaying Modal and Modeless forms in AutoCAD .NET"
date: "2012-06-28 14:20:21"
author: "Madhukar Moogala"
categories:
  - ".NET"
  - "AutoCAD"
  - "Stephen Preston"
original_url: "https://adndevblog.typepad.com/autocad/2012/06/displaying-modal-and-modeless-forms-in-autocad-net.html "
typepad_basename: "displaying-modal-and-modeless-forms-in-autocad-net"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/stephen-preston.html">Stephen Preston</a></p>  <p>Another in my occasional series of common beginner mistakes that take forever and a day to debug <img style="border-bottom-style: none; border-right-style: none; border-top-style: none; border-left-style: none" class="wlEmoticon wlEmoticon-smile" alt="Smile" src="/assets/image_64921.jpg" />.</p>  <p>Sometimes what we already know gets in the way. If you want to display a form in .NET, you use Form.ShowDialog. Right?</p>  <p>Wrong! If you do, you’re likely to find AutoCAD hanging or displaying other unexpected behavior – particularly if you’re doing this in conjunction with VBA macros. Instead display your dialogs using the AutoCAD .NET APIs provided for the purpose:</p>  <ul>   <li>Application.ShowModalDialog</li>    <li>Application.ShowModelessDialog</li> </ul>  <p>And if you’re working with modeless dialogs, consider using a palette instead. Kean Walmsley explains why that’s a good idea <a href="http://through-the-interface.typepad.com/through_the_interface/2007/07/using-a-palette.html">here</a>.</p>
