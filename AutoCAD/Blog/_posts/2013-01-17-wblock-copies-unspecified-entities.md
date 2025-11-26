---
layout: "post"
title: "wblock() copies unspecified entities"
date: "2013-01-17 09:08:36"
author: "Augusto Goncalves"
categories:
  - ".NET"
  - "Augusto Goncalves"
  - "AutoCAD"
  - "ObjectARX"
original_url: "https://adndevblog.typepad.com/autocad/2013/01/wblock-copies-unspecified-entities.html "
typepad_basename: "wblock-copies-unspecified-entities"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/augusto-goncalves.html">Augusto Goncalves</a></p>  <p>The wblock() function copies some other entities/objects to the newly created database, which I have not specified in the parameters. For example, I'm getting some block definitions in the new database, and a dimension style other than the 'STANDARD' dimension style. Why does this occur?</p>  <p>It can happen that Database::wblock() copies some other objects to the newly created database other than the specified objects. This is what occurs inside of wblock():</p>  <p>When AutoCAD has to WBLOCK a drawing, it creates a new database. This new database uses the same settings as the original database. This means that it will contain all variables from the original database; every AutoCAD variable stored in the drawing database gets copied. These are some of the variables that reference database objects:</p>  <p>CELTYPE, CLAYER, CMLSTYLE, TEXTSTYLE, DIMSTYLE...</p>  <p>All these variables specify a symboltable / dictionary entry which is currently active: current text style, current dimension style, current layer, current linetype, current mline style.</p>  <p>Because the WBLOCK'ed database has the same values for these variables, AutoCAD has to copy the referenced symbol table / dictionary entries. This explains why, for example, a dimension style other than 'STANDARD' is copied to the new database. If this current dimension style uses user-defined blocks for the arrowheads, the names of the arrowhead blocks are stored in the variables DIMBLK1 and DIMBLK2. In this case, AutoCAD copies the arrowhead blocks to the new database. Another example is if the current layer in your original database is another layer besides '0'; you will get a new layer in your WBLOCK'ed database. The same is true for text styles, mline styles, and so on. </p>  <p>If this AutoCAD behavior is not acceptable, you can set all these variables back to their default values. This means you have to set the current text style and dimension style to 'STANDARD', the current layer to '0', and so on. Even if you set all these 'referencing' variables back to the default values, it may happen that you copy some unexpected objects to the new database. This can happen when you change the default symbol table / dictionary entries (if you use user-defined arrowheads in the 'STANDARD' dimension style, the arrowheads blocks is copied). </p>
