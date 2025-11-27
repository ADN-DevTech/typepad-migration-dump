---
layout: "post"
title: "Determine whether a part is a content center part or not"
date: "2012-06-05 00:01:40"
author: "Xiaodong Liang"
categories:
  - "Inventor"
  - "Xiaodong Liang"
original_url: "https://adndevblog.typepad.com/manufacturing/2012/06/determine-whether-a-part-is-a-content-center-part-or-not.html "
typepad_basename: "determine-whether-a-part-is-a-content-center-part-or-not"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/manufacturing/xiaodong-liang.html" target="_self">Xiaodong Liang</a></p>  <p>Before 2011, you can use iProperties to check. Something needed to be aware:&#160; there are 6 Property Sets for Standard or Custom: 1~4 are the property sets of a common part. 5th is [Content Library Component Properties], 6th is [ContentCenter]. The [Content Library Component Properties] (B9600981-DEE8-4547-8D7C-E525B3A1727A) can be always found in all CC parts. However, it’s Component_type property is not used by CC anymore and is also not set in current version. However it might work for older components coming from older releases. So to distinguish, you need to use the 6th [ContentCenter]. If it is a Custom part, there is only one property within this property set. The name is “IsCustomPart”. While if it is a Standard CC part, there are some properties to indicate the information of the CC such as family, member etc.. </p>  <p>From Inventor 2011, API provided a direct flag to indicate if the part is Standard or Custom:    <br />PartComponentDefinition.IsContentMember</p>
