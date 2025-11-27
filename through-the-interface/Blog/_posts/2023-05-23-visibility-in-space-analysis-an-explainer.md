---
layout: "post"
title: "Visibility in Space Analysis: an explainer"
date: "2023-05-23 12:06:00"
author: "Kean Walmsley"
categories:
  - "Autodesk Research"
  - "Generative design"
original_url: "https://www.keanw.com/2023/05/visibility-in-space-analysis-an-explainer.html "
typepad_basename: "visibility-in-space-analysis-an-explainer"
typepad_status: "Publish"
---

<p>Our resident spatial-analyst-in-chief, Rhys Goldstein, has gone and done it again. After his <a href="https://towardsdatascience.com/a-short-and-direct-walk-with-pascals-triangle-26a86d76f75f" rel="noopener" target="_blank">very well-received Medium post explaining grid-based pathfinding</a> was published in <a href="https://towardsdatascience.com/" rel="noopener" target="_blank">Towards Data Science</a>, Rhys has now had a <a href="https://towardsdatascience.com/a-quick-and-clear-look-at-grid-based-visibility-bf63769fbc78" rel="noopener" target="_blank">follow-up post published that explains our work on visibility</a>. And it’s fantastic!</p>
<p>Assessing visibility and pathfinding in 2D and 3D spaces are really cornerstones of our work to assess how occupant-centric prospective building designs are likely to be. This is a critical capability for us in our day-to-day work, but it&#39;s also very interesting from an algorithmic perspective and Rhys&#39;s article helps create awareness of these techniques with people working in adjacent areas.</p>
<p>In his post Rhys explains the novel, grid-based approach to testing visibility that was implemented in <a href="https://www.keanw.com/2019/03/the-space-analysis-package-for-dynamo-and-refinery-is-now-available.html" rel="noopener" target="_blank">our Space Analysis Dynamo package</a> – something that is very different from the more traditional ray casting/Isovist technique that’s common in our industry – and shows how the technique can be replicated using a few lines of Python code or even by copying a formula into Excel.</p>
<p>I went ahead and followed Rhys’s instructions, and sure enough in less than 2 minutes had grid-based visibility working in an Excel spreadsheet. Such fun!</p>
<p><a href="https://through-the-interface.typepad.com/.a/6a00d83452464869e202b751815952200b-pi" rel="noopener" target="_blank"><img alt="Fun in Excel" height="673" src="/assets/image_816332.jpg" style="margin: 30px auto; float: none; display: block;" title="Fun in Excel" width="499" /></a></p>
<p>Complete instructions on how to do this yourself are included in <a href="https://towardsdatascience.com/a-quick-and-clear-look-at-grid-based-visibility-bf63769fbc78" rel="noopener" target="_blank">Rhys’s post</a>. This method uses interpolation between the grid cells to calculate visibility values.</p>
<p>One of the core techniques used in Space Analysis relates to “path counting”, especially in the context of pathfinding through a grid. We&#39;re also able to use path counting for visibility: it’s very interesting that this approach has so many potential applications, and all therefore <a href="https://www.keanw.com/2022/06/a-paper-on-our-space-analysis-algorithm-in-the-journal-of-artificial-intelligence-research.html" rel="noopener" target="_blank">inspired by Pascal&#39;s Triangle</a>.</p>
<p>To grossly summarise the approach, I’ve borrowed some of Rhys’s animations that explain the process in three steps. (Just as in the above example, visibility is being calculated from the top left of the grid.)</p>
<p>1) Count the grid paths from the top left corner.</p>
<p><span style="font-size: 10pt;"><a href="https://through-the-interface.typepad.com/.a/6a00d83452464869e202b751a5b81c200c-pi" rel="noopener" target="_blank"><img alt="Counting grid paths" height="301" src="/assets/image_645698.jpg" style="margin: 30px auto; float: none; display: block;" title="Counting grid paths" width="500" /></a></span></p>
<p>2) Count the grid paths again but ignoring obstacles.</p>
<p><a href="https://through-the-interface.typepad.com/.a/6a00d83452464869e202b7518157b1200b-pi" rel="noopener" target="_blank"><img alt="Now ignoring obstacles" height="301" src="/assets/image_180104.jpg" style="margin: 30px auto; float: none; display: block;" title="Now ignoring obstacles" width="500" /></a></p>
<p>3) Divide the first numbers by the second to get the visibility level.</p>
<p><a href="https://through-the-interface.typepad.com/.a/6a00d83452464869e202b7518157d4200b-pi" rel="noopener" target="_blank"><img alt="Divide the two" height="301" src="/assets/image_669655.jpg" style="margin: 30px auto; float: none; display: block;" title="Divide the two" width="500" /></a></p>
<p>I hope this post motivates you to head on over to <a href="https://towardsdatascience.com/a-quick-and-clear-look-at-grid-based-visibility-bf63769fbc78" rel="noopener" target="_blank">Rhys&#39;s excellent article</a> to learn more. Grid-based techniques such as this are underused in our industry, and I fully expect them to become more popular as people become aware of their potential.</p>
