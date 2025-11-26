---
layout: "post"
title: "Design Automation API: サポート エンジン一覧の取得"
date: "2025-05-25 21:30:46"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2025/05/design-automation-api-obtaining-supported-engines.html "
typepad_basename: "design-automation-api-obtaining-supported-engines"
typepad_status: "Publish"
---

<p>Design Automation API でアドイン実行に使用するコアエンジンは、<a href="https://adndevblog.typepad.com/technology_perspective/2023/03/design-automation-api-core-engine-lifecycle-policy.html" rel="noopener" target="_blank">Design Automation API：コアエンジン ライフサイクル ポリシー</a> に基づき、対応するデスクトップ製品の新バージョン リリースに同期して、毎年、利用可能なコアエンジン バージョンが変化します。</p>
<p>利用出来るコアエンジン バージョンは、<a href="https://aps.autodesk.com/en/docs/design-automation/v3/reference/http/engines-GET/" rel="noopener" target="_blank">GET engines</a> エンドポイントで照会することが可能ですが、コアエンジンの増加によって注意すべき点があります。具体的には、現在、GET engines エンドポイントの一度の呼び出しは、利用可能なすべてのコアエンジン バージョンが返されない状態になっています。</p>
<p>例えば、<a href="https://aps.autodesk.com/en/docs/oauth/v2/reference/http/gettoken-POST/" rel="noopener" target="_blank">POST token</a> エンドポイントで得た 2-legged アクセストークンを指定して、<a href="https://www.postman.com/" rel="noopener" target="_blank">Postman</a> で GET engines エンドポイントを呼び出すと、 次のようになります。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02e860ea85be200b-pi" style="display: inline;"><img alt="Response1" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02e860ea85be200b image-full img-responsive" src="/assets/image_784939.jpg" title="Response1" /></a></p>
<p>レスポンス ステータスが 200 OK（正常終了）を示しているにもかかわらず、data フィールドで返される配列内のコアエンジン名は、あきらかに少ないように見えます。</p>
<pre>{
    &quot;paginationToken&quot;: &quot;3eyJOYW1lX2xhYmVsIjp7IlMiOiJJbnZlbnRvci4yMDIxIn0sIlJlY2VpdmVyIjp7IlMiOiJldmVyeW9uZSJ9LCJPd25lciI6eyJTIjoiQXV0b2Rlc2sifX0%3D&quot;,
    &quot;data&quot;: [
        &quot;Autodesk.3dsMax+2020&quot;,
        &quot;Autodesk.3dsMax+2023&quot;,
        &quot;Autodesk.Inventor+2022&quot;,
        &quot;Autodesk.Inventor+2024&quot;,
        &quot;Autodesk.AutoCAD+24_1&quot;,
        &quot;Autodesk.Inventor+2026&quot;,
        &quot;Autodesk.3dsMax+2025&quot;,
        &quot;Autodesk.Revit+2023&quot;,
        &quot;Autodesk.Inventor+2025&quot;,
        &quot;Autodesk.Test+Updated&quot;,
        &quot;Autodesk.Revit+2026&quot;,
        &quot;Autodesk.AutoCAD+25_1&quot;,
        &quot;Autodesk.AutoCAD+24&quot;,
        &quot;Autodesk.AutoCAD+24_2&quot;,
        &quot;Autodesk.AutoCAD+24_3&quot;,
        &quot;Autodesk.Revit+2024&quot;,
        &quot;Autodesk.AutoCAD+25_0&quot;,
        &quot;Autodesk.Revit+2021&quot;,
        &quot;Autodesk.Inventor+2025_Net8&quot;,
        &quot;Autodesk.Inventor+2021&quot;
    ]
}</pre>
<div>
<div>
<div>
<p>このように、エンドポイント呼び出しのレスポンスサイズが大きくなると、システム全体のパフォーマンス低下につながってしまうため、最大数が制限されるものがあります。通常、そのようなエンドポイントには <u><a href="https://nam11.safelinks.protection.outlook.com/?url=https%3A%2F%2Fqiita.com%2Fsilane1001%2Fitems%2Fc26bc45826de35f150e0&amp;data=05%7C02%7Ctoshiaki.isezaki%40autodesk.com%7C72da44f692474907a0c508dd718e6e7b%7C67bff79e7f914433a8e5c9252d2ddc1d%7C0%7C0%7C638791578727254900%7CUnknown%7CTWFpbGZsb3d8eyJFbXB0eU1hcGkiOnRydWUsIlYiOiIwLjAuMDAwMCIsIlAiOiJXaW4zMiIsIkFOIjoiTWFpbCIsIldUIjoyfQ%3D%3D%7C0%7C%7C%7C&amp;sdata=AEhuM3fKNYBhlzb7nWt7ej77MZENjAubUrVtEnzO1B8%3D&amp;reserved=0" rel="noopener" target="_blank">pagination 機能</a></u> が用意されていて、複数回に渡る呼び出しで全件データを得られるようになっています。</p>
</div>
</div>
</div>
<p>GET engines エンドポイントの場合、初回呼出しで得られたレスポンス JSON に &quot;paginationToken&quot; フィールド値が設定されていることで、pagination 機能 が適用されて複数ページでレスポンスが得られる、つまり、以降の呼び出しで残りのコアエンジン名が返されることがわかります。<a href="https://aps.autodesk.com/en/docs/design-automation/v3/reference/http/engines-GET/" rel="noopener" target="_blank">GET engines</a> エンドポイントのリファレンスには、次のようにクエリー パラメータでページ指定出来ることが説明されています。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02e8610131db200d-pi" style="display: inline;"><img alt="Pagenation" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02e8610131db200d image-full img-responsive" src="/assets/image_39202.jpg" title="Pagenation" /></a></p>
<p>そこで、https://developer.api.autodesk.com/da/us-east/v3/engines<strong><span style="color: #0000ff;">?page=</span></strong><span style="color: #0000ff;">3eyJOYW1lX2xhYmVsIjp7IlMiOiJJbnZlbnRvci4yMDIxIn0sIlJlY2VpdmVyIjp7IlMiOiJldmVyeW<br />9uZSJ9LCJPd25lciI6eyJTIjoiQXV0b2Rlc2sifX0%3D</span>&#0160;のように、&quot;page&quot; クエリー パラメータに初回に返された &quot;paginationToken&quot; フィールド値を指定して GET engines エンドポイントを呼び出すと、残りのコアエンジン名を得ることが出来ます。この際、返されるレスポンスの &quot;paginationToken&quot; フィールド値が null になっていて、これ以上のページがなことがわかります。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02e861017566200d-pi" style="display: inline;"><img alt="Response2" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02e861017566200d image-full img-responsive" src="/assets/image_884677.jpg" title="Response2" /></a></p>
<pre>{
    &quot;paginationToken&quot;: null,
    &quot;data&quot;: [
        &quot;Autodesk.3dsMax+2024&quot;,
        &quot;Autodesk.Revit+2025&quot;,
        &quot;Autodesk.Revit+2018&quot;,
        &quot;Autodesk.Test+Latest&quot;,
        &quot;Autodesk.Inventor+2023&quot;,
        &quot;Autodesk.Fusion+2601_00&quot;,
        &quot;Autodesk.Fusion+2506_00&quot;,
        &quot;Autodesk.Revit+2019&quot;,
        &quot;Autodesk.3dsMax+2021&quot;,
        &quot;Autodesk.Fusion+Latest&quot;,
        &quot;Autodesk.Revit+2022&quot;,
        &quot;Autodesk.3dsMax+2022&quot;,
        &quot;Autodesk.Revit+2020&quot;
    ]
}</pre>
<p>なお、コアエンジン バージョンの削除期日は、利用者の使用状況を見て多少調整される場合があります。また、エンドポイントによって pagination の指定方法が変わる場合がありますのでご注意ください。</p>
<p>By Toshiaki Isezaki</p>
