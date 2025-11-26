---
layout: "post"
title: "Revit 2022 の新機能 その5"
date: "2021-05-14 01:02:45"
author: "Ryuji Ogasawara"
categories:
  - "API カスタマイズ"
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2021/05/new-features-on-revit-2022-part5.html "
typepad_basename: "new-features-on-revit-2022-part5"
typepad_status: "Publish"
---

<p>Revit 2022 の新機能と改良された機能をシリーズでご紹介しております。</p>
<ul>
<li><a href="https://adndevblog.typepad.com/technology_perspective/2021/04/new-features-on-revit-2022-part1.html">Revit 2022 の新機能 その1</a></li>
<li><a href="https://adndevblog.typepad.com/technology_perspective/2021/04/new-features-on-revit-2022-part2.html">Revit 2022 の新機能 その2</a></li>
<li><a href="https://adndevblog.typepad.com/technology_perspective/2021/04/new-features-on-revit-2022-part3.html">Revit 2022 の新機能 その3</a></li>
<li><a href="https://adndevblog.typepad.com/technology_perspective/2021/04/new-features-on-revit-2022-part4.html">Revit 2022 の新機能 その4</a></li>
</ul>
<p>今回は、MEP 設計分野の新機能と機能向上の内容となります。</p>
<hr />
<p><strong>システム解析で部屋またはスペースを使用する</strong></p>
<p>エネルギー解析用モデルの作成時に、[エネルギー設定]ダイアログで[部屋またはスペースを使用]モードを選択できるようになりました。</p>
<p>モデルに部屋またはスペースが含まれている場合は、このモードを選択します。この方法では、モデル内の部屋やスペースに基づいて建物モデルで定義されている容積を使用します。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0282e102014c200b-pi" style="display: inline;"><img alt="Revit2022-05-01" class="asset  asset-image at-xid-6a0167607c2431970b0282e102014c200b img-responsive" src="/assets/image_351356.jpg" title="Revit2022-05-01" /></a></p>
<p>また、gbXML に書き出すワークフローが新しいダイアログに統合されました。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0282e1020153200b-pi" style="display: inline;"><img alt="Revit2022-05-02" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0282e1020153200b image-full img-responsive" src="/assets/image_365170.jpg" title="Revit2022-05-02" /></a></p>
<hr />
<p><strong>システム解析負荷レポート</strong></p>
<p>システム解析を実行して HVAC システムの負荷とサイズ設定ワークフローを選択すると、新しい負荷レポートには、空調システムのサイズを変更するための負荷と湿り空気に関する詳細情報が含まれます。負荷レポートの詳細は下記のページをご参照ください。</p>
<ul>
<li><a href="https://help.autodesk.com/view/RVT/2022/JPN/?guid=GUID-EFFCBAEA-88B8-412C-B1DB-D0DFCBE15810">システム解析: 負荷レポート</a></li>
</ul>
<p class="asset-video"><iframe allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture" allowfullscreen="" frameborder="0" height="400" src="https://www.youtube.com/embed/vjjCJeIrlPU?feature=oembed" width="712"></iframe></p>
<hr />
<p><strong>面積あたりの熱容量</strong></p>
<p>新しいプロジェクト単位、[面積あたりの熱容量]が、壁、床、天井、屋根、舗装、および解析用サーフェスに追加されました。<br />[面積あたりの熱容量]を使用すると、これらの要素の面積あたりの熱量を表現できます。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b027880299abf200d-pi" style="display: inline;"><img alt="Revit2022-05-03" class="asset  asset-image at-xid-6a0167607c2431970b027880299abf200d img-responsive" src="/assets/image_133270.jpg" title="Revit2022-05-03" /></a></p>
<hr />
<p><strong>パネル集計表の自動シェーディング</strong></p>
<p>パネル設定に基づいて、パネル集計表テンプレートで自動シェーディングを有効にすることができます。</p>
<p>[テンプレート オプションを設定]ダイアログ ボックスの[回路テーブル]ペインで、パネル集計表テンプレートの自動シェーディングを有効にします。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b026bded1ae88200c-pi" style="display: inline;"><img alt="Revit2022-05-04" class="asset  asset-image at-xid-6a0167607c2431970b026bded1ae88200c img-responsive" src="/assets/image_590869.jpg" title="Revit2022-05-04" /></a></p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b027880299aed200d-pi" style="display: inline;"><img alt="Revit2022-05-05" class="asset  asset-image at-xid-6a0167607c2431970b027880299aed200d img-responsive" src="/assets/image_108361.jpg" title="Revit2022-05-05" /></a></p>
<p class="asset-video"><iframe allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture" allowfullscreen="" frameborder="0" height="400" src="https://www.youtube.com/embed/V0CP6bH-vwg?feature=oembed" width="712"></iframe></p>
<hr />
<p><strong>システム解析の設定点で ASHRAE の値を使用する</strong></p>
<p>システム解析の設定点で、ASHRAE 90.1 の値が使用されるようになりました。</p>
<hr />
<p>次回は、Revit API の新機能についてご紹介致します。</p>
<p>By Ryuji Ogasawara</p>
