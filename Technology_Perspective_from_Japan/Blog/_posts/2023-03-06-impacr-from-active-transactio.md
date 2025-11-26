---
layout: "post"
title: "Design Automation API：アクティブ トランザクションの影響"
date: "2023-03-06 00:27:35"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2023/03/impacr-from-active-transactio.html "
typepad_basename: "impacr-from-active-transactio"
typepad_status: "Publish"
---

<div class="zd-indent">
<p dir="auto">AutoCAD Mechanical には他の AutoCAD ベースの業種別ツールセットにはないアクティブ トランザクション機構が存在します。Design Automation API for AutoCAD を利用する際にも、扱う図面によっては、このアクティブ トランザクションの影響を受けるケースがあります。Design Automation API for AutoCAD の処理対象図面に AutoCAD Mechanical オブジェクトが存在する場合です。</p>
<p dir="auto"><a href="https://adndevblog.typepad.com/technology_perspective/2022/09/design-automation-api-and-object-enabler.html" rel="noopener" target="_blank">Design Automation API for AutoCAD とオブジェクト イネーブラ</a>&#0160;でご紹介したとおり、Design Automation API for AutoCAD 環境には AutoCAD Mechanical 用のオブジェクト イネーブラが用意されているので、WorkItem 実行時に図面に AutoCAD Mechanical オブジェクトを検出すると、同オブジェクト イネーブラがロードされるようになります。</p>
</div>
<div class="zd-indent">
<p dir="auto">AutoCAD Mechanical 用のオブジェクト イネーブラがロードされると、この時点で<strong>アクティブ トランザクション</strong>（<a href="https://adndevblog.typepad.com/technology_perspective/2023/02/active_transaction_on_acm.html" rel="noopener" target="_blank">AutoCAD Mechanical のアクティブ トランザクション </a> 参照）が有効化されてしまい、アドイン側の標準トランザクションのコミットが遅延する結果になってしまいます。</p>
<p dir="auto"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02b75197735b200c-pi" style="display: inline;"><img alt="Differences" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02b75197735b200c image-full img-responsive" src="/assets/image_92251.jpg" title="Differences" /></a></p>
<p dir="auto">明示的に AutoCAD Mechanical の機能やオブジェクトを使用した経験がなくても、AutoCAD Mechanical のテンプレート図面から図面を新規作成していたり、AutoCAD Mechanical Desktop 時代からの古い対図面資産を運用していたり、また、それら図面を INSERT コマンドで図面挿入したりしていると、ディクショナリ領域に AutoCAD Mechanical のカスタム オブジェクトが埋め込まれてしまいますのでご注意ください。</p>
<p dir="auto">Design Automation API for AutoCAD 環境に AutoCAD Mechanical 用のオブジェクト イネーブラがロードされているかは、WorkItem のレポートログで見出すことが出来ます。次の場合には、AutoCAD Mechanical オブジェクト イネーブラがロードされています。つまり、アクティブ トランザクションが有効になっています。</p>
<blockquote>
<p dir="auto">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; :<br />[02/25/2023 10:15:13] Version Number: S.51.Z.113 (UNICODE)<br />[02/25/2023 10:15:14] LogFilePath has been set to the working folder.<br /><strong>[02/25/2023 10:15:14] Loading Mechanical modules.....</strong><br />[02/25/2023 10:15:14] Loader application completed<br />[02/25/2023 10:15:14] Regenerating model.<br /><strong>[02/25/2023 10:15:14] Auditing Mechanical Data...</strong><br /><strong>[02/25/2023 10:15:14] Number of errors found: 0 Number of errors fixed: 0</strong><br /><strong>[02/25/2023 10:15:14] Auditing Mechanical Data complete.</strong><br />[02/25/2023 10:15:14] This drawing was created in language &#39;CHT&#39; with translation information. Before working with this drawing you should translate it into system language &#39;ENU&#39;. Working with the untranslated drawing may delete the translation information.<br />[02/25/2023 10:15:14] AutoCAD menu utilities loaded.<br />[02/25/2023 10:15:14] Command:<br />[02/25/2023 10:15:14] Command:<br />&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; :</p>
</blockquote>
<p dir="auto">AutoCAD Mechanical オブジェクト イネーブラがロードされた環境で、WorkItem で処理した図面のオブジェクト表示が意図しないものになっていたり、特定領域のオブジェクト選択が期待した結果になっていないような場合には、アクティブ トランザクションによってグラフィックス更新が阻害されている可能性があります。<a href="https://adndevblog.typepad.com/technology_perspective/2023/02/active_transaction_on_acm.html" rel="noopener" target="_blank">AutoCAD Mechanical のアクティブ トランザクション</a> でご案内した対応を試してみてください。</p>
<p dir="auto">なお、Design Automation API for AutoCAD 環境でオブジェクト イネーブラを無効化させることは出来ません。また、図面でに埋め込まれた AutoCAD Mechanical のカスタム オブジェクトをアドイン側の実装で除去するのは難しいため、そのような試行はお勧めしていません。<a href="https://help.autodesk.com/view/ACD/2023/JPN/?guid=GUID-86C80CA1-F237-4AE6-8A43-2E9CA06A03A8" rel="noopener" target="_blank">EXPORTTOAUTOCAD</a> コマンド業種別ツールセットのカスタム オブジェクトを削除することをお薦めします。</p>
</div>
<p>By Toshiaki Isezaki</p>
