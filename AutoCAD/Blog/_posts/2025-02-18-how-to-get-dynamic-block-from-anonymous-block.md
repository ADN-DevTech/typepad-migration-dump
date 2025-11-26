---
layout: "post"
title: "How To Get Dynamic Block from Anonymous Block"
date: "2025-02-18 16:38:00"
author: "Madhukar Moogala"
categories:
  - ".NET"
  - "AutoCAD"
  - "Madhukar Moogala"
original_url: "https://adndevblog.typepad.com/autocad/2025/02/how-to-get-dynamic-block-from-anonymous-block.html "
typepad_basename: "how-to-get-dynamic-block-from-anonymous-block"
typepad_status: "Publish"
---

<p>     <script src="https://cdn.jsdelivr.net/gh/google/code-prettify@master/loader/run_prettify.js?skin=sunburst"></script>   </p>   <p>By <a href="http://adndevblog.typepad.com/autocad/Madhukar-Moogala.html" target="_self">Madhukar Moogala</a></p>    <p>     A common request from developers is how to retrieve the original Dynamic Block reference from a given anonymous     block. This is useful when working with AutoCAD’s Dynamic Blocks, as modifying or identifying the source Dynamic     Block definition can be critical in automation and customization workflows.      AutoCAD stores a reference to the Dynamic Block inside an anonymous block using extended entity data (XData). The     following C# function demonstrates how to extract this reference.   </p>   <pre class="prettyprint">
    <code>
      public static Handle GetDynamicBlockHandleFromAnonymousBlock(BlockTableRecord btr)
      {
          if (!btr.IsAnonymous)
              return ObjectId.Null.Handle;
          Handle btrHand = btr.ObjectId.Handle;
          ResultBuffer rb = btr.GetXDataForApplication("AcDbBlockRepBTag");
          if (rb == null)
              return ObjectId.Null.Handle;
          foreach (TypedValue tv in rb)
          {
              if (tv.TypeCode == 1005 && tv.Value is string strValue)
                  {
                  long nHandle = Convert.ToInt64(strValue, 16);
                  return new Handle(nHandle);
                  }
          }
          return ObjectId.Null.Handle;
      }
    </code>
  </pre>
