---
layout: "post"
title: "Minor updates to Warnamo and Capturefinery"
date: "2020-05-15 18:38:12"
author: "Kean Walmsley"
categories:
  - "Autodesk"
  - "Dynamo"
  - "Generative design"
original_url: "https://www.keanw.com/2020/05/minor-updates-to-warnamo-and-capturefinery.html "
typepad_basename: "minor-updates-to-warnamo-and-capturefinery"
typepad_status: "Publish"
---

<p>I’ve made some minor tweaks to a couple of my Dynamo packages, this week. One was to make sure <a href="https://github.com/KeanW/Capturefinery" target="_blank">Capturefinery</a> – a tool that generates screenshots and animated GIFs for Refinery optimisation runs – works with the shipping release of Generative Design for Revit 2021 (it now does, but needed to look in a new directory structure). This is available from Capturefinery 0.9.11.</p><p>The other was to add a feature to <a href="https://github.com/KeanW/Warnamo" target="_blank">Warnamo</a> – a tool that lists the warnings and errors in your Dynamo graph – to let you export the error list to Excel. This is available from Warnamo 0.1.3. The UI change is very subtle – there’s now a tiny button that says CSV in the dialog’s title bar.</p><p><a href="https://through-the-interface.typepad.com/.a/6a00d83452464869e20263ec1a4a44200c-pi" target="_blank"><img width="300" height="473" title="Warnamo, now with CSV export" style="margin: 0px auto 30px; border: 0px currentcolor; border-image: none; float: none; display: block; background-image: none;" alt="Warnamo, now with CSV export" src="/assets/image_982498.jpg" border="0"></a></p><p>You can’t see it? I said it was subtle. :-) Here’s another screenshot with it being hovered over.</p><p><a href="https://through-the-interface.typepad.com/.a/6a00d83452464869e20264e2dac0f1200d-pi" target="_blank"><img width="300" height="475" title="Hovering over the CSV button" style="margin: 0px auto 30px; border: 0px currentcolor; border-image: none; float: none; display: block; background-image: none;" alt="Hovering over the CSV button" src="/assets/image_551206.jpg" border="0"></a></p><p>Clicking on this button asks you to select a file location and then exports a CSV file you can load (for instance) in Excel.</p><p><a href="https://through-the-interface.typepad.com/.a/6a00d83452464869e20263ec1a4a2d200c-pi" target="_blank"><img width="500" height="843" title="Error list" style="margin: 0px auto 1px; border: 0px currentcolor; border-image: none; float: none; display: block; background-image: none;" alt="Error list" src="/assets/image_291126.jpg" border="0"></a></p><p>Thanks to Jan Boonen for suggesting this feature. If anyone has any other thoughts on where to take this tool, please do <a href="https://github.com/KeanW/Warnamo/issues" target="_blank">submit an issue via the GitHub repo</a>.</p>
