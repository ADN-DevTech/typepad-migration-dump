---
layout: "post"
title: "File naming while generating ProE formats"
date: "2012-08-22 22:37:32"
author: "Xiaodong Liang"
categories:
  - "Inventor"
  - "Xiaodong Liang"
original_url: "https://adndevblog.typepad.com/manufacturing/2012/08/file-naming-while-generating-proe-formats.html "
typepad_basename: "file-naming-while-generating-proe-formats"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/manufacturing/xiaodong-liang.html" target="_self">Xiaodong Liang</a></p>  <p><strong>Issue</strong>    <br />A recent case reports that the name of the result ProE file is not what the user sets, no matter with UI or API. </p>  <p>Assume we have a part named:&#160; </p>  <p>DIN 3771 - 34,5 x 2,65 - N - NBR 70(7)39,86.ipt</p>  <p>Save as to Pro/E file, the result file name is:</p>  <p>4_5_x_2_65_-_N_-_NBR_70_7_39_86.g</p>  <p><strong>Solution</strong>    <br />Because of some limitations, the saved .g file name will include only “a-z, A-Z, 0-9, -, _”. Other characters will be replaced by ‘_’. Also, the saved .g file name size will be limited to <strong>31</strong> characters. So, some characters are trimmed.</p>
