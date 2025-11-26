---
layout: "post"
title: "AutoCAD JavaScript API TIPS ～ 2 Paper.js編"
date: "2015-01-23 23:58:41"
author: "Ryuji Ogasawara"
categories:
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2015/01/autocad-javascript-api-tips-2-paperjs.html "
typepad_basename: "autocad-javascript-api-tips-2-paperjs"
typepad_status: "Publish"
---

<p>この連載では、AutoCAD JavaScript API と連携してご利用いただける HTML5 ベースのオープンソースライブラリをご紹介いたします。</p>
<p>前回は、AutoCAD JavaScript API をご活用いただく上で知っておいて頂きたい前提知識についてご説明いたしました。</p>
<ul>
<li><a href="http://adndevblog.typepad.com/technology_perspective/2015/01/autocad-2015-javascript-api-usage1.html" target="_blank" title="AutoCAD JavaScript API TIPS ～ 1 前提知識編">AutoCAD JavaScript API TIPS ～ 1 前提知識編</a></li>
</ul>
<p>今回は Paper.js をご紹介いたします。</p>
<ul>
<li><a href="http://paperjs.org/" target="_blank" title="Paper.js">Paper.js</a></li>
</ul>
<p>Paper.js は、HTML5 Canvas 上で、ベクタ・ラスタ形式のグラフィックスを扱えるオープンソースのグラフィックスライブラリです。<br />Adobe Illustrator や Adobe Photoshop などのソフトウェアと同じように、ベジェ曲線で図形を描いたり、ビットマップ画像（ラスタイメージ）を配置して操作することができ、レイヤ構造、グループ化、パスのセグメント編集といったような構造をもつグラフィックス編集機能を備えています。</p>
<p>このライブラリは、Adobe Illustrator の JavaScript プラグインとして10年以上の歴史ある Scriptographer が元となっております。HTML5 Canvas の簡易的なラッパーというわけではなく、 Web ビジュアライゼーションのための様々な機能を提供しています。</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d0c758e7970c-pi" style="display: inline;"><img alt="Paperjs_examples" class="asset  asset-image at-xid-6a0167607c2431970b01b8d0c758e7970c img-responsive" src="/assets/image_534813.jpg" title="Paperjs_examples" /></a></p>
<p>もし HTML5 Canvas についてご存知のない方・ご興味のある方がいらっしゃいましたら、こちらの解説サイトをご参照ください。</p>
<ul>
<li><a href="http://www.html5.jp/canvas/" target="_blank" title="HTML5 Canvas">HTML5 Canvas</a></li>
</ul>
<p>たとえば、スケッチパネルを表示して、ベジェ曲線によって描いたスケッチを、平面図上にオブジェクトとして取り込むといったこともできます。</p>
<p>&#0160; <a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c73dd84d970b-pi" style="display: inline;"><img alt="2015-01-24 17-44-28" class="asset  asset-image at-xid-6a0167607c2431970b01b7c73dd84d970b img-responsive" src="/assets/image_261381.jpg" title="2015-01-24 17-44-28" /></a></p>
<p>Canvas 上に描いたパスは JSON 文字列として AutoCAD のプログラムに渡すことができます。C# プログラムでは、Newtonsoft.Json.Linq.JArray クラス等でパースして、パス配列を取得することができますので、これを元にポリラインを生成することができます。</p>
<p>ご興味のある方は、ぜひ一度お試しください。</p>
<p>By Ryuji Ogasawara</p>
<p>&#0160;</p>
