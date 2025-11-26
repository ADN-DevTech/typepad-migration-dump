---
layout: "post"
title: "Redefining Help Shortcut Key with CUI API"
date: "2020-06-15 18:59:00"
author: "Madhukar Moogala"
categories:
  - ".NET"
  - "Madhukar Moogala"
original_url: "https://adndevblog.typepad.com/autocad/2020/06/redefining-help-shortcut-key-with-cui-api.html "
typepad_basename: "redefining-help-shortcut-key-with-cui-api"
typepad_status: "Publish"
---

<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js?skin=sunburst"></script><p>By <a href="http://adndevblog.typepad.com/autocad/Madhukar-Moogala.html" target="_self">Madhukar 
Moogala</a></p><p>There is common need to change shortcut assigned for Help from F1 to some other key, if you look at keypad layout F1 and ESC keys are adjacent to each other, designers often use ESC to cancel running command, or deselect the objects from selection. As the F1 key is adjacent chances of pressing this key accidently is quite high, F1 pops up a Help window, which in said cases is counter productive.</p><p>To avoid this, we can assign F1 to a Cancel command and CTRL+ALT+F1 to Help.</p><p><br></p><p><br></p>
<pre class="prettyprint"> public static void RedefineHelpKey()
{
string mainCuiFile = (string)Application.GetSystemVariable("MENUNAME");
mainCuiFile += ".cuix";
var doc = Application.DocumentManager.MdiActiveDocument;
var db = doc.Database;
var ed = doc.Editor;
CustomizationSection cs = new CustomizationSection(mainCuiFile);
AcceleratorCollection acCollection = cs.MenuGroup.Accelerators;

//This is to get Help menu macro.
var macros = from MacroGroup mg in cs.MenuGroup.MacroGroups
        from MenuMacro mm in mg.MenuMacros
        where mm.ElementID.Equals("ID_Help") || mm.ElementID.Equals("ID_Cancel")
        select mm;
foreach(MenuMacro menuMacro in macros)
{
if (menuMacro.ElementID.Equals("ID_Help"))
{

MenuAccelerator macHelp = new MenuAccelerator(menuMacro,
                            /*ShortCutKeyCombination*/"CTRL+ALT+H",
                                cs.MenuGroup);
if (acCollection.Contains(macHelp))
{
    ed.WriteMessage($"\n True MenuAccelerator Contains.");
}
else
{
    acCollection.Add(macHelp);
}
                    

}
if (menuMacro.ElementID.Equals("ID_Cancel"))
{
//Assigning Cancel to F1
MenuAccelerator macCancel = new MenuAccelerator(menuMacro,
                                        /*ShortCutKeyCombination*/"F1",
                                        cs.MenuGroup);
if (acCollection.Contains(macCancel))
{
    ed.WriteMessage($"\n True MenuAccelerator Contains.");
}
else
{
    acCollection.Add(macCancel);
}                 

}
}


//This will create backup CUIX!
cs.Save(true);
            

}            	
</pre>
<p>Note:</p>
<p>If the changes don't reflect immediately, a restart of AutoCAD is needed, this is because, we are attempting to change the main CUI. </p>
<p>A utility function to list shortcut keys we have redefined </p>
<pre class="prettyprint">
[CommandMethod("LISTMAC")]
public static void ListMenuAccelerators()
{
var doc = Application.DocumentManager.MdiActiveDocument;
var db = doc.Database;
var ed = doc.Editor;
string mainCuiFile = (string)Application.GetSystemVariable("MENUNAME");
mainCuiFile += ".cuix";
CustomizationSection cs = new CustomizationSection(mainCuiFile);
AcceleratorCollection acCollection = cs.MenuGroup.Accelerators;
var q = from MenuAccelerator menuAcltr in acCollection
        where menuAcltr.Name.Contains("Help") || 
	      menuAcltr.Name.Contains("Cancel")
        select menuAcltr;
if (q != null && q.ToList().Count > 0)
{


    foreach (var m in q.ToList())
    {
        ed.WriteMessage($"\n Name: {m.Name}\n\tAccerlerator ShortcutKey: {m.AcceleratorShortcutKey}");
    }

                
}
}
</pre>



<p>Demo</p>
<iframe width="640" height="620" src="https://screencast.autodesk.com/Embed/Timeline/261c7e29-4d96-460e-99ec-c7d42cc7452a" frameborder="0" allowfullscreen webkitallowfullscreen></iframe>
