---
layout: "post"
title: "Revit 2026 新機能 ～ その2"
date: "2025-04-18 00:48:28"
author: "Ryuji Ogasawara"
categories:
  - "API カスタマイズ"
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2025/04/new-features-on-revit-2026-part2.html "
typepad_basename: "new-features-on-revit-2026-part2"
typepad_status: "Publish"
---

<p>今回は、Autodesk Revit 2026 の建築設計分野に関連する新機能をご紹介いたします。<a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02e860fef1a8200d-pi" style="display: inline;"></a></p>
<hr />
<p><strong>部屋ごと及びセグメントごとに壁を作成する</strong></p>
<p>壁セグメントを使用する部屋の中または部屋の境界を指定して、簡単に意匠壁を作成できるようになりました。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02e860e7fb97200b-pi" style="display: inline;"><img alt="Revit2026_03_01" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02e860e7fb97200b image-full img-responsive" src="/assets/image_468494.jpg" title="Revit2026_03_01" /></a></p>
<p>部屋の境界となる壁と柱で形成される閉じた領域を選択すると、部屋の中に新しい壁をワン クリックで作成できます。<br />または、壁と柱の既存のセグメントを選択して、新しい壁を作成します。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02e860e7fbcb200b-pi" style="display: inline;"><img alt="Revit2026_03_02-1" class="asset  asset-image at-xid-6a0167607c2431970b02e860e7fbcb200b img-responsive" src="/assets/image_777785.jpg" title="Revit2026_03_02-1" /></a></p>
<p>新しく作成された壁は対象の壁または柱の面に隣接するため、躯体や仕上げに複数の壁を作成する際の効率と生産性が向上します。</p>
<hr />
<p><strong>躯体レイヤのない複層構造が可能に</strong></p>
<p>複層構造における躯体レイヤの要件は必須ではなくなり、躯体レイヤの削除や躯体境界の外側へのレイヤの移動が容易になりました。</p>
<p>躯体レイヤを削除したり、躯体レイヤの外側にレイヤを移動して、既定の結合を改善したり、壁仕上げや床仕上げの表示コントロールをサポートします。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02e860e7fba7200b-pi" style="display: inline;"><img alt="Revit2026_03_03" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02e860e7fba7200b img-responsive" src="/assets/image_407004.jpg" title="Revit2026_03_03" /></a></p>
<hr />
<p><strong>複層構造のレイヤの優先度をカスタマイズする</strong></p>
<p>壁、床、屋根、天井、スラブ、地形ソリッドなど、すべての複数レイヤ要素の要素タイプ レベルから、レイヤ機能とは独立してレイヤの優先度をカスタマイズできるようになりました。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3d146ae200c-pi" style="display: inline;"><img alt="Revit2026_03_04" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3d146ae200c img-responsive" src="/assets/image_210328.jpg" title="Revit2026_03_04" /></a></p>
<hr />
<p><strong>シート上のビューの位置と自動配置</strong></p>
<p>[修正 | ビューポート]の[配置とビュー]パネルを使用して、シートのビューの位置の保存、修正、管理を行えるようになりました。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3d146bb200c-pi" style="display: inline;"><img alt="Revit2026_03_05" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3d146bb200c img-responsive" src="/assets/image_812058.jpg" title="Revit2026_03_05" /></a></p>
<p>シート間でビューを位置合わせするには、[ビューのアンカー]オプションを使用します。</p>
<ul>
<li>ビューの原点(既定): ビューの原点を基準にしてシート上のビュー位置を固定および維持します。この方法は、シート間で平面図の方向ビューを揃える場合に便利です。</li>
<li>[中心]/[左上]/[右上]/[右下]/[左下]オプションは、指定したビューのアンカーを基準にしてシート上のビュー位置を固定および維持します。この方法は、タイトル ブロックのコーナー付近など、毎回シート上の特定の場所にビューを配置する場合に便利です。</li>
</ul>
<p>[シート上のビューの位置]は、同様の平面方向ビューが多数存在し、それらをシート間で位置合わせして調整を維持する場合に役立ちます。また、複数のシートに対して同じオフセットで配置する一般的なレイアウト(建物の壁セクションなど)の場合も同様です。</p>
<p>既存の[シート上でビューをスワップする]機能は、新しいワークフローに統合されています。<br /><br />[保存位置]に割り当てられたビューを入れ替えることができます。位置を無効にするプロンプトが表示され、[保存位置]の割り当てをいつでも解除または再割り当てできます。</p>
<hr />
<p>By Ryuji Ogasawara</p>
