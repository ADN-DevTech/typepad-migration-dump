---
layout: "post"
title: "Revit SDK Sample&rsquo;s Assembly Reference Updater"
date: "2017-06-16 01:10:00"
author: "Madhukar Moogala"
categories: []
original_url: "https://adndevblog.typepad.com/aec/2017/06/revit-sdk-samples-assembly-reference-updater.html "
typepad_basename: "revit-sdk-samples-assembly-reference-updater"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/aec/madhukar-moogala.html" target="_self">Madhukar Moogala</a></p><p>While I was learning Revit API through SDK samples, I see there is a huge Samples folder with about 180 project files.</p><p>I thought it will be good idea to quickly update the references of each project to correct Revit install folder.</p><p>There is an executable which does this and is included in SDK samples folder, for some reason it is not working for me.</p><p>I created a simple python script, which creates an .User object file having Projects references directed to Revit path.</p> <pre style='color:#d1d1d1;background:#000000;'><span style='color:#9999a9; '>#This simple app creates .user file corresponding to every .csproj | .vbproj</span>
<span style='color:#9999a9; '>##With following text</span>
<span style='color:#9999a9; '>#&lt;?xml version="1.0" encoding="utf-8"?></span>
<span style='color:#9999a9; '>#&lt;Project ToolsVersion="14.0" xmlns="</span><span style='color:#6070ec; '>http://schemas.microsoft.com/developer/msbuild/2003</span><span style='color:#9999a9; '>"></span>
<span style='color:#9999a9; '>#  &lt;PropertyGroup></span>
<span style='color:#9999a9; '>#    &lt;ReferencePath>D:\Program Files\Autodesk\Revit 2018\&lt;/ReferencePath></span>
<span style='color:#9999a9; '>#  &lt;/PropertyGroup></span>
<span style='color:#9999a9; '>#&lt;/Project></span>
<span style='color:#9999a9; '>#Created by moogalm(@galakar), ADN, Autodesk.</span>

<span style='color:#e66170; font-weight:bold; '>import</span> os<span style='color:#d2cd86; '>,</span>shutil
<span style='color:#e66170; font-weight:bold; '>import</span> xml<span style='color:#d2cd86; '>.</span>etree<span style='color:#d2cd86; '>.</span>ElementTree <span style='color:#e66170; font-weight:bold; '>as</span> ET
revitSampleDir <span style='color:#d2cd86; '>=</span> <span style='color:#e66170; font-weight:bold; '>input</span><span style='color:#d2cd86; '>(</span><span style='color:#00c4c4; '>"Revit SDK Samples Folder, for E.g </span><span style='color:#008080; '>\"</span><span style='color:#00c4c4; '>D:</span><span style='color:#008080; '>\R</span><span style='color:#00c4c4; '>evit2018</span><span style='color:#008080; '>\S</span><span style='color:#00c4c4; '>amples</span><span style='color:#008080; '>\"</span><span style='color:#00c4c4; '> :"</span><span style='color:#d2cd86; '>)</span><span style='color:#9999a9; '>#"D:\\Revit 2018 SDK\\Samples"</span>
userFile <span style='color:#d2cd86; '>=</span> revitSampleDir<span style='color:#00dddd; '>+</span><span style='color:#00c4c4; '>"</span><span style='color:#008080; '>\\</span><span style='color:#00c4c4; '>user.txt"</span><span style='color:#9999a9; '>#"D:\\Revit 2018 SDK\\Samples\\user.txt"</span>
revitInstallDir <span style='color:#d2cd86; '>=</span> <span style='color:#e66170; font-weight:bold; '>input</span><span style='color:#d2cd86; '>(</span><span style='color:#00c4c4; '>"Revit Install Folder :"</span><span style='color:#d2cd86; '>)</span><span style='color:#9999a9; '>#"D:\\Program Files\\Autodesk\\Revit 2018\\"</span>
project <span style='color:#d2cd86; '>=</span> ET<span style='color:#d2cd86; '>.</span>Element<span style='color:#d2cd86; '>(</span><span style='color:#00c4c4; '>"Project"</span><span style='color:#d2cd86; '>,</span>ToolsVersion<span style='color:#d2cd86; '>=</span><span style='color:#00c4c4; '>"14.0"</span><span style='color:#d2cd86; '>,</span>xmlns<span style='color:#d2cd86; '>=</span><span style='color:#00c4c4; '>"http://schemas.microsoft.com/developer/msbuild/2003"</span><span style='color:#d2cd86; '>)</span>
propertyGroup <span style='color:#d2cd86; '>=</span> ET<span style='color:#d2cd86; '>.</span>SubElement<span style='color:#d2cd86; '>(</span>project<span style='color:#d2cd86; '>,</span><span style='color:#00c4c4; '>"PropertyGroup"</span><span style='color:#d2cd86; '>)</span>
referencePath <span style='color:#d2cd86; '>=</span> ET<span style='color:#d2cd86; '>.</span>SubElement<span style='color:#d2cd86; '>(</span>propertyGroup<span style='color:#d2cd86; '>,</span><span style='color:#00c4c4; '>"ReferencePath"</span><span style='color:#d2cd86; '>)</span><span style='color:#d2cd86; '>.</span>text <span style='color:#d2cd86; '>=</span> revitInstallDir
tree <span style='color:#d2cd86; '>=</span> ET<span style='color:#d2cd86; '>.</span>ElementTree<span style='color:#d2cd86; '>(</span>project<span style='color:#d2cd86; '>)</span>
tree<span style='color:#d2cd86; '>.</span>write<span style='color:#d2cd86; '>(</span>userFile<span style='color:#d2cd86; '>,</span> encoding<span style='color:#d2cd86; '>=</span><span style='color:#00c4c4; '>'utf-8'</span><span style='color:#d2cd86; '>,</span>xml_declaration<span style='color:#d2cd86; '>=</span>True<span style='color:#d2cd86; '>,</span>method<span style='color:#d2cd86; '>=</span> <span style='color:#00c4c4; '>'xml'</span><span style='color:#d2cd86; '>)</span>
<span style='color:#e66170; font-weight:bold; '>for</span> root<span style='color:#d2cd86; '>,</span> dirs<span style='color:#d2cd86; '>,</span> files <span style='color:#e66170; font-weight:bold; '>in</span> os<span style='color:#d2cd86; '>.</span>walk<span style='color:#d2cd86; '>(</span>revitSampleDir<span style='color:#d2cd86; '>)</span><span style='color:#d2cd86; '>:</span>
    <span style='color:#e66170; font-weight:bold; '>for</span> filename <span style='color:#e66170; font-weight:bold; '>in</span> files<span style='color:#d2cd86; '>:</span>
             <span style='color:#e66170; font-weight:bold; '>if</span> filename<span style='color:#d2cd86; '>.</span>endswith<span style='color:#d2cd86; '>(</span><span style='color:#d2cd86; '>(</span><span style='color:#00c4c4; '>'.csproj'</span><span style='color:#d2cd86; '>,</span> <span style='color:#00c4c4; '>'.vbproj'</span><span style='color:#d2cd86; '>)</span><span style='color:#d2cd86; '>)</span><span style='color:#d2cd86; '>:</span>               
                shutil<span style='color:#d2cd86; '>.</span>copy<span style='color:#d2cd86; '>(</span>userFile<span style='color:#d2cd86; '>,</span>os<span style='color:#d2cd86; '>.</span>path<span style='color:#d2cd86; '>.</span>join<span style='color:#d2cd86; '>(</span>root<span style='color:#d2cd86; '>,</span>filename <span style='color:#00dddd; '>+</span><span style='color:#00c4c4; '>".user"</span><span style='color:#d2cd86; '>)</span><span style='color:#d2cd86; '>)</span>
                <span style='color:#e66170; font-weight:bold; '>print</span><span style='color:#d2cd86; '>(</span>os<span style='color:#d2cd86; '>.</span>path<span style='color:#d2cd86; '>.</span>join<span style='color:#d2cd86; '>(</span>root<span style='color:#d2cd86; '>,</span>filename <span style='color:#00dddd; '>+</span><span style='color:#00c4c4; '>".user"</span><span style='color:#d2cd86; '>)</span><span style='color:#d2cd86; '>)</span>
</pre>
