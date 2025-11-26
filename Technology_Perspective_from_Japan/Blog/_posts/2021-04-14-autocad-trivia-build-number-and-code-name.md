---
layout: "post"
title: "AutoCAD 雑学：ビルド番号と開発コードネーム"
date: "2021-04-14 00:01:47"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2021/04/autocad-trivia-build-number-and-code-name.html "
typepad_basename: "autocad-trivia-build-number-and-code-name"
typepad_status: "Publish"
---

<p>AutoCAD の登場から 39 年を迎え、先日、AutoCAD 2022 がリリースされました。</p>
<p>AutoCAD 自身の開発では、正式なバージョン リリースまで「ビルド」と呼ばれる結合試作を繰り返して、新機能をテスト・順次組み込んでいきます。また、ビルドした数を「ビルド番号」と呼び、製品バージョンの識別で使用されています。</p>
<p>そして、このビルド番号は、オンラインヘルプなどのドキュメントに記載されていないシステム変数 <strong>_VERNUM</strong>&#0160; の値にも反映されています。今回リリースを終えた AutoCAD 2022 で _VERNUM を調べてみると、<strong>S.51.0.0</strong> という値を返すことがわかります。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0278801e9985200d-pi" style="display: inline;"><img alt="_vernum" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0278801e9985200d image-full img-responsive" src="/assets/image_147555.jpg" title="_vernum" /></a></p>
<p>言うまでもなく、これは AutoCAD 2022 のリリース版が 51 回ビルドされてきたことを意味しています。このビルド番号は、今後、リリースされるだろう Update の適用によって変化します。</p>
<p>もう１つ、ビルド番号の最初にあるアルファベットですが、これはバージョン毎の開発コードネームの頭文字を表しています。歴代の開発コードネームは次のとおりです。</p>
<table cellpadding="0" cellspacing="0" style="height: 500px; width: 298px; margin-left: auto; margin-right: auto;" width="300">
<tbody>
<tr>
<td style="border-color: #29c9cf; background-color: #4872f0; text-align: center; height: 5px; width: 147px;" width="170">
<p><span style="color: #ffffff;"><strong>リリース名</strong></span></p>
</td>
<td style="border-color: #29c9cf; background-color: #4872f0; text-align: center; height: 5px; width: 151px;" width="170">
<p><span style="color: #ffffff;"><strong>開発コードネーム</strong></span></p>
</td>
</tr>
<tr style="background-color: #bff5f3;">
<td style="text-align: center; width: 147px;" width="170">
<p><span style="font-size: 10pt;"><strong>AutoCAD R14.0</strong></span></p>
</td>
<td style="text-align: center; width: 151px;" width="170">
<p><span style="font-size: 10pt;"><strong>S</strong>edona</span></p>
</td>
</tr>
<tr style="background-color: #bff5f3;">
<td style="text-align: center; width: 147px;" width="170">
<p><span style="font-size: 10pt;"><strong>AutoCAD R14.01</strong></span></p>
</td>
<td style="text-align: center; width: 151px;" width="170">
<p><span style="font-size: 10pt;"><strong>P</strong>inetop</span></p>
</td>
</tr>
<tr style="background-color: #bff5f3;">
<td style="text-align: center; width: 147px;" width="170">
<p><span style="font-size: 10pt;"><strong>AutoCAD 2000</strong></span></p>
</td>
<td style="text-align: center; width: 151px;" width="170">
<p><span style="font-size: 10pt;"><strong>T</strong>ahoe</span></p>
</td>
</tr>
<tr style="background-color: #bff5f3;">
<td style="text-align: center; width: 147px;" width="170">
<p><span style="font-size: 10pt;"><strong>AutoCAD 2000i</strong></span></p>
</td>
<td style="text-align: center; width: 151px;" width="170">
<p><span style="font-size: 10pt;"><strong>B</strong>anff</span></p>
</td>
</tr>
<tr style="background-color: #bff5f3;">
<td style="text-align: center; width: 147px;" width="170">
<p><span style="font-size: 10pt;"><strong>AutoCAD 2002</strong></span></p>
</td>
<td style="text-align: center; width: 151px;" width="170">
<p><span style="font-size: 10pt;"><strong>K</strong>irkland</span></p>
</td>
</tr>
<tr style="background-color: #bff5f3;">
<td style="text-align: center; width: 147px;" width="170">
<p><span style="font-size: 10pt;"><strong>AutoCAD 2004</strong></span></p>
</td>
<td style="text-align: center; width: 151px;" width="170">
<p><span style="font-size: 10pt;"><strong>R</strong>ed Deer</span></p>
</td>
</tr>
<tr style="background-color: #bff5f3;">
<td style="text-align: center; width: 147px;" width="170">
<p><span style="font-size: 10pt;"><strong>AutoCAD 2005</strong></span></p>
</td>
<td style="text-align: center; width: 151px;" width="170">
<p><span style="font-size: 10pt;"><strong>N</strong>eo</span></p>
</td>
</tr>
<tr style="background-color: #bff5f3;">
<td style="text-align: center; width: 147px;" width="170">
<p><span style="font-size: 10pt;"><strong>AutoCAD 2006</strong></span></p>
</td>
<td style="text-align: center; width: 151px;" width="170">
<p><span style="font-size: 10pt;"><strong>R</strong>io</span></p>
</td>
</tr>
<tr style="background-color: #bff5f3;">
<td style="text-align: center; width: 147px;" width="170">
<p><span style="font-size: 10pt;"><strong>AutoCAD 2007</strong></span></p>
</td>
<td style="text-align: center; width: 151px;" width="170">
<p><span style="font-size: 10pt;"><strong>P</strong>ostrio</span></p>
</td>
</tr>
<tr style="background-color: #bff5f3;">
<td style="text-align: center; width: 147px;" width="170">
<p><span style="font-size: 10pt;"><strong>AutoCAD 2008</strong></span></p>
</td>
<td style="text-align: center; width: 151px;" width="170">
<p><span style="font-size: 10pt;"><strong>S</strong>pago</span></p>
</td>
</tr>
<tr style="background-color: #bff5f3;">
<td style="text-align: center; width: 147px;" width="170">
<p><span style="font-size: 10pt;"><strong>AutoCAD 2009</strong></span></p>
</td>
<td style="text-align: center; width: 151px;" width="170">
<p><span style="font-size: 10pt;"><strong>R</strong>aptor</span></p>
</td>
</tr>
<tr style="background-color: #bff5f3;">
<td style="text-align: center; width: 147px;" width="170">
<p><span style="font-size: 10pt;"><strong>AutoCAD 2010</strong></span></p>
</td>
<td style="text-align: center; width: 151px;" width="170">
<p><span style="font-size: 10pt;"><strong>G</strong>ator</span></p>
</td>
</tr>
<tr style="background-color: #bff5f3;">
<td style="text-align: center; width: 147px;" width="170">
<p><span style="font-size: 10pt;"><strong>AutoCAD 2011</strong></span></p>
</td>
<td style="text-align: center; width: 151px;" width="170">
<p><span style="font-size: 10pt;"><strong>H</strong>ammer</span></p>
</td>
</tr>
<tr style="background-color: #bff5f3;">
<td style="text-align: center; width: 147px;" width="170">
<p><span style="font-size: 10pt;"><strong>AutoCAD 2012</strong></span></p>
</td>
<td style="text-align: center; width: 151px;" width="170">
<p><span style="font-size: 10pt;"><strong>I</strong>ronman</span></p>
</td>
</tr>
<tr style="background-color: #bff5f3;">
<td style="text-align: center; width: 147px;" width="170">
<p><span style="font-size: 10pt;"><strong>AutoCAD 2013</strong></span></p>
</td>
<td style="text-align: center; width: 151px;" width="170">
<p><span style="font-size: 10pt;"><strong>J</strong>aws</span></p>
</td>
</tr>
<tr style="background-color: #bff5f3;">
<td style="text-align: center; width: 147px;" width="170">
<p><span style="font-size: 10pt;"><strong>AutoCAD 2014</strong></span></p>
</td>
<td style="text-align: center; width: 151px;" width="170">
<p><span style="font-size: 10pt;"><strong>K</strong>eystone</span></p>
</td>
</tr>
<tr style="background-color: #bff5f3;">
<td style="text-align: center; width: 147px;" width="170">
<p><span style="font-size: 10pt;"><strong>AutoCAD 2015</strong></span></p>
</td>
<td style="text-align: center; width: 151px;" width="170">
<p><span style="font-size: 10pt;"><strong>L</strong>ongbow</span></p>
</td>
</tr>
<tr style="background-color: #bff5f3;">
<td style="text-align: center; width: 147px;" width="170">
<p><span style="font-size: 10pt;"><strong>AutoCAD 2016</strong></span></p>
</td>
<td style="text-align: center; width: 151px;" width="170">
<p><span style="font-size: 10pt;"><strong>M</strong>aestro</span></p>
</td>
</tr>
<tr style="background-color: #bff5f3;">
<td style="text-align: center; width: 147px;" width="170">
<p><span style="font-size: 10pt;"><strong>AutoCAD 2017</strong></span></p>
</td>
<td style="text-align: center; width: 151px;" width="170">
<p><span style="font-size: 10pt;"><strong>N</strong>autilus</span></p>
</td>
</tr>
<tr style="background-color: #bff5f3;">
<td style="text-align: center; width: 147px;" width="170">
<p><span style="font-size: 10pt;"><strong>AutoCAD 2018</strong></span></p>
</td>
<td style="text-align: center; width: 151px;" width="170">
<p><span style="font-size: 10pt;"><strong>O</strong>mega</span></p>
</td>
</tr>
<tr style="background-color: #bff5f3;">
<td style="text-align: center; width: 147px;" width="170">
<p><span style="font-size: 10pt;"><strong>AutoCAD 2019</strong></span></p>
</td>
<td style="text-align: center; width: 151px;" width="170">
<p><span style="font-size: 10pt;"><strong>P</strong>i</span></p>
</td>
</tr>
<tr style="background-color: #bff5f3;">
<td style="text-align: center; width: 147px;" width="170">
<p><span style="font-size: 10pt;"><strong>AutoCAD 2020</strong></span></p>
</td>
<td style="text-align: center; width: 151px;" width="170">
<p><span style="font-size: 10pt;"><strong>Q</strong>ubit</span></p>
</td>
</tr>
<tr style="background-color: #bff5f3;">
<td style="text-align: center; width: 147px;" width="170">
<p><span style="font-size: 10pt;"><strong>AutoCAD 2021</strong></span></p>
</td>
<td style="text-align: center; width: 151px;" width="170">
<p><span style="font-size: 10pt;"><strong>R</strong>ogue</span></p>
</td>
</tr>
<tr style="background-color: #bff5f3;">
<td style="text-align: center; width: 147px;" width="170">
<p><span style="font-size: 10pt;"><strong>AutoCAD 2022</strong></span></p>
</td>
<td style="text-align: center; width: 151px;" width="170">
<p><span style="font-size: 10pt;"><strong>S</strong>equoia</span></p>
</td>
</tr>
<tr>
<td style="text-align: center; border-style: solid; border-color: #f2e9e9; background-color: #f2f0f0; width: 147px;">
<p><span style="font-size: 10pt;"><strong>AutoCAD 2023</strong></span></p>
</td>
<td style="text-align: center; border-style: solid; border-color: #f2e9e9; background-color: #f2f0f0; width: 151px;">
<p><span style="font-size: 10pt;"><strong>T</strong>uring(2022年追記)</span></p>
</td>
</tr>
<tr>
<td style="text-align: center; border-style: solid; border-color: #f2e9e9; background-color: #f2f0f0; width: 147px;">
<p><span style="font-size: 10pt;"><strong>AutoCAD 2024</strong></span></p>
</td>
<td style="text-align: center; border-style: solid; border-color: #f2e9e9; background-color: #f2f0f0; width: 151px;">
<p><span style="font-size: 10pt;"><strong>U</strong>rsa(2023年追記)</span></p>
</td>
</tr>
<tr>
<td style="text-align: center; border-style: solid; border-color: #f2e9e9; background-color: #f2f0f0; width: 147px;">
<p><span style="font-size: 10pt;"><strong>AutoCAD 2025</strong></span></p>
</td>
<td style="text-align: center; border-style: solid; border-color: #f2e9e9; background-color: #f2f0f0; width: 151px;">
<p><span style="font-size: 10pt;"><strong>V</strong>enn(2024年追記)</span></p>
</td>
</tr>
</tbody>
</table>
<p>ビルド番号は、<a href="https://help.autodesk.com/view/ACD/2022/JPN/?guid=GUID-CDBAD44E-F661-430C-A99E-192B83D41C10" rel="noopener" target="_blank"><strong>ABOUT[バージョン情報]</strong></a> コマンドで表示されるダイアログにも表示されるようになっています。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0278801e9789200d-pi" style="display: inline;"><img alt="About_2022" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0278801e9789200d image-full img-responsive" src="/assets/image_594507.jpg" title="About_2022" /></a></p>
<p>AutoCAD 使用時にビルド番号を意識する必要性はあまりないように思いますが、アドイン アプリケーションを開発している場合には、プログラムから _VERNUM 値を取得してバージョンを判断する、といった利用方法も可能かと思います。</p>
<p>ちょっとした情報として覚えておくといいかもしれません。</p>
<p>By Toshiaki Isezaki</p>
