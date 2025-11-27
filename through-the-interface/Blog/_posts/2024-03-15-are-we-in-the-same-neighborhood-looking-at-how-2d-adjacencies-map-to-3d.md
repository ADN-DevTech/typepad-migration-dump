---
layout: "post"
title: "Are we in the same neighborhood? Looking at how 2D adjacencies map to 3D"
date: "2024-03-15 17:11:57"
author: "Kean Walmsley"
categories:
  - "Autodesk Research"
  - "Generative design"
original_url: "https://www.keanw.com/2024/03/are-we-in-the-same-neighborhood-looking-at-how-2d-adjacencies-map-to-3d.html "
typepad_basename: "are-we-in-the-same-neighborhood-looking-at-how-2d-adjacencies-map-to-3d"
typepad_status: "Publish"
---

<p>Rhys Goldstein has had <a href="https://towardsdatascience.com/a-sharp-and-solid-outline-of-3d-grid-neighborhoods-1b0f264e7c11" rel="noopener" target="_blank">one more excellent article</a> published in Towards Data Science. This is the third - and he says final, I hope he’s wrong - article in the series.</p>
<p>Here they are, along with my accompanying posts:</p>
<ul>
<li><a href="https://towardsdatascience.com/a-short-and-direct-walk-with-pascals-triangle-26a86d76f75f" rel="noopener" target="_blank">A Short and Direct Walk with Pascal’s Triangle</a>
<ul>
<li><a href="https://www.keanw.com/2022/11/explaining-how-path-counting-helps-simulate-natural-navigation.html" rel="noopener" target="_blank">Explaining how path counting helps simulate natural navigation</a></li>
</ul>
</li>
<li><a href="https://towardsdatascience.com/a-quick-and-clear-look-at-grid-based-visibility-bf63769fbc78" rel="noopener" target="_blank">A Quick and Clear Look at Grid-Based Visibility</a>
<ul>
<li><a href="https://www.keanw.com/2023/05/visibility-in-space-analysis-an-explainer.html" rel="noopener" target="_blank">Visibility in Space Analysis: an explainer</a></li>
</ul>
</li>
<li><a href="https://medium.com/towards-data-science/a-sharp-and-solid-outline-of-3d-grid-neighborhoods-1b0f264e7c11" rel="noopener" target="_blank">A Sharp and Solid Outline of 3D Grid Neighborhoods</a>
<ul>
<li>This post. :-)</li>
</ul>
</li>
</ul>
<p>In this latest installment, Rhys explores one particular aspect of what it means to go from a 2D pathfinding environment to one focused on 3D.</p>
<p>When looking at adjacent positions within a 2D grid, it’s possible to have 4-, 6-, 8-, 12- and 16-neighborhoods:</p>
<p><a href="/assets/image_425742.jpg" rel="noopener" target="_blank"><img alt="2D neighborhoods" border="0" height="310" src="/assets/image_425742.jpg" style="display: block; margin: 30px auto;" title="2D neighborhoods" width="500" /></a></p>
<p>These map very differently to 3D, where we end up with 6-, 18-, 26-, 50-, and 74-neighborhoods!</p>
<p><a href="/assets/image_554023.jpg" rel="noopener" target="_blank"><img alt="3D neighborhoods" border="0" height="371" src="/assets/image_554023.jpg" style="display: block; margin: 30px auto;" title="3D neighborhoods" width="500" /></a></p>
<p>It seems no-one had previously connected the 2D neighborhoods with their 3D equivalents, which was Rhys’s goal from this article.</p>
<p>Here they are, one-by-one. Let’s start with the rectangular neighborhoods.</p>
<p>A 2D 4-neighborhood is a section of the 3D 6-neighborhood:</p>
<p><a href="/assets/image_533945.jpg" rel="noopener" target="_blank"><img alt="Neighbors 6" border="0" height="166" src="/assets/image_533945.jpg" style="display: block; margin: 30px auto;" title="Neighbors 4 to 6" width="240" /></a></p>
<p>A 2D 8-neighborhood is a section of the 3D 26-neighborhood:</p>
<p><a href="/assets/image_49705.jpg" rel="noopener" target="_blank"><img alt="Neighbors 26" border="0" height="317" src="/assets/image_49705.jpg" style="display: block; margin: 30px auto;" title="Neighbors 8 to 26" width="242" /></a></p>
<p>A 2D 16-neighborhood is a section of the 3D 74-neighborhood:</p>
<p><a href="/assets/image_293859.jpg" rel="noopener" target="_blank"><img alt="Neighbors 74" border="0" height="469" src="/assets/image_293859.jpg" style="display: block; margin: 30px auto;" title="Neighbors 16 to 74" width="393" /></a></p>
<p>Now for the triangular neighborhoods.</p>
<p>A 2D 6-neighborhood is a section of the 3D 18-neighborhood:</p>
<p><a href="/assets/image_536131.jpg" rel="noopener" target="_blank"><img alt="Neighbors 18" border="0" height="317" src="/assets/image_536131.jpg" style="display: block; margin: 30px auto;" title="Neighbors 6 to 18" width="317" /></a></p>
<p>A 2D 12-neighborhood is a section of the 3D 50-neighborhood:</p>
<p><a href="/assets/image_384785.jpg" rel="noopener" target="_blank"><img alt="Neighbors 50." border="0" height="620" src="/assets/image_384785.jpg" style="display: block; margin: 30px auto;" title="Neighbors 12 to 50" width="469" /></a></p>
<p>Things get much more complex beyond these first 5, of course. I really don't blame Rhys for stopping there. :-)</p>
<p>If you're interested in seeing some code that uses these various 3D neighborhoods to solve a visibility problem, be sure to check out the Python source in Rhys's fascinating post.</p>
