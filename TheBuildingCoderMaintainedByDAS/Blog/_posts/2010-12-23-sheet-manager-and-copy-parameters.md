---
layout: "post"
title: "Sheet Manager and Copy Parameters"
date: "2010-12-23 06:00:00"
author: "Jeremy Tammik"
categories:
  - "External"
  - "Parameters"
  - "User Interface"
  - "Utilities"
original_url: "https://thebuildingcoder.typepad.com/blog/2010/12/sheet-manager-and-copy-parameters.html "
typepad_basename: "sheet-manager-and-copy-parameters"
typepad_status: "Publish"
---

<p>I have started settling back into normal life after the extensive conference tour and a whole month of non-stop travelling.
I still have not been able to start preparing Christmas, though, except for buying lots and lots of food to carry me and my grown-up  kids and their partners through the coming days.
I need to start thinking about presents now as well, I guess.

<p>I just had a meeting with my colleagues and heard of Adam's traumatic return journey from Milano to the UK this weekend. 
He finally made it, but it really was a bad journey.
The DevDay conference in the UK with Gary and Adam taking over all the presentations went very well, though.
Congratulations and thanks to Gary and Adam on managing that so competently!

<p>Meanwhile, on the Revit API front, I recently mentioned the 

<a href="http://thebuildingcoder.typepad.com/blog/2010/12/%D0%B4%D0%BE%D0%BF%D0%BE%D0%BB%D0%BD%D0%B5%D0%BD%D0%B8%D1%8F-%D0%B4%D0%BB%D1%8F-%D1%80%D0%B5%D0%B2%D0%B8%D1%82-add-ins-for-revit.html">
Russian add-in utilities</a>

provided by 

<a href="http://kartautodeskuser.blogspot.com">
Артур Кураков alias Arthur Kurakov</a>.

Fedor added a 

<a href="http://thebuildingcoder.typepad.com/blog/2010/12/%D0%B4%D0%BE%D0%BF%D0%BE%D0%BB%D0%BD%D0%B5%D0%BD%D0%B8%D1%8F-%D0%B4%D0%BB%D1%8F-%D1%80%D0%B5%D0%B2%D0%B8%D1%82-add-ins-for-revit.html#comment-6a00e553e1689788330148c687124b970c">
comment</a> to point out that we missed the most exciting add-in, the sheet manager, so here is a little follow-up from Arthur to rectify that:

<h4>Sheet Manager &ndash; Менеджер листов</h4>

<p>This add-in organises sheets to albums. 
It controls its own numbering in each album. 
It uses a text shared parameter created by the user for numbering.  
In addition, the user can add specific parameters, which must be of the same value in each album. 
Sheet Manager monitors changes to these parameters and, if this parameter was specified to album, changes it to specified value. 
If it was parameter of album sheets numbering, the Sheet Manager controls renumbering of the other one.  
It allows user to move sheets up and down in albums. 
You can drop sheets from album to album by changing the appropriate parameter. 
Sheet Manager manipulates standard Revit parameters, so it easy to create standard Revit schedules and use other parameter based functions.</p>

<object width="480" height="385"><param name="movie" value="http://www.youtube.com/v/Acn7bR1WgO8?fs=1&amp;hl=ru_RU"></param><param name="allowFullScreen" value="true"></param><param name="allowscriptaccess" value="always"></param><embed src="http://www.youtube.com/v/Acn7bR1WgO8?fs=1&amp;hl=ru_RU" type="application/x-shockwave-flash" allowscriptaccess="always" allowfullscreen="true" width="480" height="385"></embed></object>

<p>Actually, there is even more:

<h4>Copy Parameters &ndash; Копирование параметров</h4>

<p>Arthur created a new add-in named 

<a href="http://kartautodeskuser.blogspot.com/2010/12/blog-post_20.html">"Копирование параметров" (copy parameters)</a>. 
It copies instance parameters from one instance to others. 
It allows you to select which instance parameters you want to copy. 

<p>The user interface is minimal and intuitive.
There are only three buttons with the following Russian labels:

<ul>
<li>"Выбрать все" -  select all
<li>"Отменить выбор" - select none.
<li>"Копировать параметры" - copy parameters.
</ul>

<object width="425" height="344"><param name="movie" value="http://www.youtube.com/v/fyC-6Jm5ZtI?hl=ru&amp;fs=1"><param name="allowFullScreen" value="true"><param name="allowscriptaccess" value="always"><embed src="http://www.youtube.com/v/fyC-6Jm5ZtI?hl=ru&amp;fs=1" type="application/x-shockwave-flash" allowscriptaccess="always" allowfullscreen="true" width="425" height="344"></embed></object>

<p>Finally, here is a link to Arthur's Russian post about the 

<a href="http://kartautodeskuser.blogspot.com/2010/12/blog-post.html">
DevTV Visual Studio template files to create a new Revit add-in project</a>.

<p>Please note that I also posted a

<a href="http://thebuildingcoder.typepad.com/blog/2010/10/revit-2011-devtv.html">
DevTV template update</a>.
