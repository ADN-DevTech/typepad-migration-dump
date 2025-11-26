---
layout: "post"
title: "AutoCAD JavaScript API TIPS ～ 4 D3.js編"
date: "2015-02-06 23:38:37"
author: "Ryuji Ogasawara"
categories:
  - "API カスタマイズ"
original_url: "https://adndevblog.typepad.com/technology_perspective/2015/02/autocad-javascript-api-tips-4-d3js.html "
typepad_basename: "autocad-javascript-api-tips-4-d3js"
typepad_status: "Publish"
---

<p>この連載では、AutoCAD JavaScript API と連携してご利用いただける HTML5 ベースのオープンソースライブラリをご紹介いたします。</p>
<p>前回は、HTML5 Canvas 上で、アイソメトリックな図形を描画できるグラフィックスライブラリ Isomerについてご紹介いたしました。</p>
<ul>
<li><a href="http://adndevblog.typepad.com/technology_perspective/2015/01/autocad-2015-javascript-api-usage1.html" target="_blank" title="AutoCAD JavaScript API TIPS ～ 1 前提知識編">AutoCAD JavaScript API TIPS ～ 1 前提知識編</a></li>
<li><a href="http://adndevblog.typepad.com/technology_perspective/2015/01/autocad-javascript-api-tips-2-paperjs.html" target="_blank" title="AutoCAD JavaScript API TIPS ～ 2 Paper.js編">AutoCAD JavaScript API TIPS ～ 2 Paper.js編</a></li>
<li><a href="http://adndevblog.typepad.com/technology_perspective/2015/01/autocad-javascript-api-tips-3-isomer.html" target="_blank" title="AutoCAD JavaScript API TIPS ～ 3 Isomer編">AutoCAD JavaScript API TIPS ～ 3 Isomer編</a></li>
</ul>
<p>今回は D3.js をご紹介いたします。</p>
<ul>
<li><a href="http://d3js.org/" target="_blank" title="D3.js">D3.js</a></li>
</ul>
<p>D3.js は、HTML、SVG、CSSを使用したデータを視覚化するためのオープンソースのグラフィックスライブラリです。<br />様々なデータ表現のためのコンポーネントが用意されており、特定のフレームワークに縛られることなく、モダンブラウザ上でデータをビジュアライズすることができます。</p>
<p>データドリブン （データ駆動型）の設計思想に基づいて開発されており、任意のデータからHTMLのテーブルを作成したり、同時にグラフやチャートを生成することができます。データを更新すれば、そのデータから生成されたドキュメントは対話的・同期的に更新させることができます。</p>
<p style="text-align: center;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb07ebf9ff970d-pi" style="display: inline;"><img alt="D3_1" class="asset  asset-image at-xid-6a0167607c2431970b01bb07ebf9ff970d img-responsive" src="/assets/image_33769.jpg" title="D3_1" /></a></p>
<p>今回は、AutoCADの図面データベースのグラフィカルオブジェクトの一覧から、オブジェクトをタイプ別に集計してパイチャートで表示するサンプル（ADNチームのKean Walmsleyが作成したサンプルプロジェクト）をお見せしたいと思います。</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb07ebfa04970d-pi" style="display: inline;"><img alt="Autocad_d3" class="asset  asset-image at-xid-6a0167607c2431970b01bb07ebfa04970d img-responsive" src="/assets/image_474547.jpg" style="display: block; margin-left: auto; margin-right: auto;" title="Autocad_d3" /></a></p>
<p>AutoCADのプログラムでは、モデル空間上に配置されているブロックテーブルレコードのオブジェクト配列を取得し、LINQを使用してオブジェクトタイプ毎にグループ化して数量をカウントしています。その集計結果をJSON形式に変換して、AutoCAD JavaScript APIを通じてJavaScriptにデータを渡しています。</p>
<p>D3.jsのグラフコンポーネントの生成はシンプルで、オプションを指定することでラベル、エフェクト、マウスイベントのコールバックなどをカスタマイズすることができます。</p>
<p>たとえば、モデル空間上のブロック図形に対してカスタム属性を定義しておき、このカスタム属性に基づいてデータを集計してグラフで表示するといった使い方も考えられます。またコンポーネントに対してのユーザー操作（クリック等）に応じて、図面上でこれを強調表示したり、あるいは編集するといったこともできるでしょう。</p>
<p>ご興味のある方は、ぜひ一度お試しください。</p>
<p>By Ryuji Ogasawara</p>
