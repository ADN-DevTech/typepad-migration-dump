---
layout: "post"
title: "Revit 2026 新機能 ～ その4"
date: "2025-05-09 00:48:11"
author: "Ryuji Ogasawara"
categories:
  - "API カスタマイズ"
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2025/05/new-features-on-revit-2026-part4.html "
typepad_basename: "new-features-on-revit-2026-part4"
typepad_status: "Publish"
---

<p>今回は、Autodesk Revit 2026 の 構造設計分野に関連する新機能をご紹介いたします。</p>
<hr />
<p><strong>パラメトリック鉄筋クランク</strong></p>
<p>クランク付き鉄筋を、鉄筋が密集した領域で干渉を防ぎながら簡単にモデリングできるようになりました。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3d29713200c-pi" style="display: inline;"><img alt="Revit2026_05_01" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3d29713200c image-full img-responsive" src="/assets/image_176855.jpg" title="Revit2026_05_01" /></a></p>
<p>フックと同様に、クランクは鉄筋のパラメトリック終端です。</p>
<p>形状駆動鉄筋とフリー フォーム鉄筋にクランクを追加することができます。</p>
<p>プロジェクト内の既存の鉄筋や、配置中の鉄筋に対し、鉄筋の終端にクランクを割り当てたり、クランクが既に割り当てられている鉄筋形状を変更することができます。</p>
<hr />
<p><strong>解析モデルの自動化へのクイック アクセス</strong></p>
<p>解析モデルの自動化に素早く直感的にアクセスして使用できます。また、自動化は必要に応じてカスタマイズできます。</p>
<ul>
<li>建物の物理要素から解析要素を実行: これは、「ワンクリック」で解析モデルを自動化するプロセスです。</li>
<li>建物の解析要素から物理要素を実行: 物理要素を選択して、[実行]をクリックするだけで使用できます。</li>
<li>自動化のカスタマイズ: 必要に応じて自動化をカスタマイズします。</li>
</ul>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3d2971e200c-pi" style="display: inline;"><img alt="Revit2026_05_02" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3d2971e200c img-responsive" src="/assets/image_315526.jpg" title="Revit2026_05_02" /></a></p>
<hr />
<p><strong>ポイント トゥ ポイントの鉄鋼のモデリング</strong></p>
<p>正確な始端および終端のクリック点から開始する鉄骨要素ジオメトリを作成するには、構造設定の新しいグローバル オプションを使用します。</p>
<p>このオプションは、[管理]タブ [構造設定]パネル (構造設定) [構造用鋼設定]タブ [自動短縮] [自動結合時に鉄骨要素の自動短縮を無効化]をオンにすると使用できます。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3d2972b200c-pi" style="display: inline;"><img alt="Revit2026_05_03" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3d2972b200c image-full img-responsive" src="/assets/image_713224.jpg" title="Revit2026_05_03" /></a></p>
<p>Revit API を利用して鉄骨を自動配置する場合などは、自動短縮処理に影響されずに目的の位置に配置できるようになります。</p>
<p>ポイント トゥ ポイント モデリングでは、鉄骨要素の正確性を視覚的に確認できる、正確なジオメトリを定義します。</p>
<p>また、新しいグローバル オプションにより、既存のプロジェクトのすべての鉄骨要素のジオメトリをさかのぼって変更します。</p>
<p>ポイント トゥ ポイント モデリングでは、自動結合の動作は変更されません。</p>
<p>このオプションは、次の内容に対して使用できます。</p>
<ul>
<li>構造用鋼材</li>
<li>構造用鉄骨柱</li>
<li>構造用鋼製トラス</li>
<li>鉄鋼断面を使用した梁システム</li>
<li>鉄骨要素を使用したブレース</li>
</ul>
<hr />
<p><strong>形状駆動ファミリの鉄骨固有のパラメータ</strong></p>
<p>正確な重量パラメータと塗装面積パラメータが、構造鉄骨要素で使用できるようになりました。</p>
<p>これらのパラメータは、Revit の形状駆動ファミリを使用して作成される鉄骨要素に対応しています。</p>
<p>これにより、鉄骨製造要素の既存の塗装面積パラメータおよび正確な重量パラメータと、これらの新しいパラメータを同じように利用して、同じ方法で計算できるようになりました。</p>
<ul>
<li>正確な重量: このパラメータは、切断、切り欠き、短縮などを考慮して、要素の正確な重量を出力します。使用される式は、体積に材料密度を乗算したものです。</li>
<li>塗装面積: このパラメータは要素の外部表面積の合計を出力するもので、推定塗装量の計算に使用されます。使用される式は、周長 x カット長です。</li>
</ul>
<hr />
<p>By Ryuji Ogasawara</p>
