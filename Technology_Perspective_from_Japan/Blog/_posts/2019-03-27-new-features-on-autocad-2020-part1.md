---
layout: "post"
title: "AutoCAD 2020 の新機能 ～ その1"
date: "2019-03-27 06:52:10"
author: "Toshiaki Isezaki"
categories:
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2019/03/new-features-on-autocad-2020-part1.html "
typepad_basename: "new-features-on-autocad-2020-part1"
typepad_status: "Publish"
---

<p>AutoCAD・AutoCAD LT の新バージョンとなる AutoCAD 2020と AutoCAD LT 2020 がリリースされました。AutoCAD の誕生から 37 年を経て、昨年から引き続き Only One AutoCADとして、業種別ツールキット（AutoCAD のみ）と AutoCAD Web アプリ、AutoCAD モバイル アプリ を含めた利点を訴求したリリースとなります。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a474cc8a200d-pi" style="display: inline;"><img alt="Only One. AutoCAD" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0240a474cc8a200d image-full img-responsive" src="/assets/image_786617.jpg" title="Only One. AutoCAD" /></a></p>
<p>なお、<span style="text-decoration: underline;">AutoCAD 用に</span>提供されるのは、昨年同様、次の業種別ツールセットです。AutoCAD サブスクリプション契約をお持ちのお客様は、必要に応じて、Autodesk Accounts ページから無償でダウンロード、AutoCAD にインストールしてお使いいただくことが出来ます。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a4453db3200c-pi" style="display: inline;"><img alt="Industries_toolsets" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0240a4453db3200c image-full img-responsive" src="/assets/image_452799.jpg" title="Industries_toolsets" /></a></p>
<p>それでは、今年も複数回にわたって、新機能や改良された機能についてご案内していきます。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a4453ba1200c-pi" style="display: inline;"><img alt="Autocad_history" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0240a4453ba1200c image-full img-responsive" src="/assets/image_121690.jpg" title="Autocad_history" /></a></p>
<p>まずは、サポートされる動作環境です。今回のリリースでは、近年販売されている Windows コンピュータの 64 ビット化に鑑みて、64 ビット版のみを提供するようになっています。<strong> <a href="https://knowledge.autodesk.com/ja/search-result/caas/simplecontent/content/autocad-and-autocad-lt-32-bit-discontinuation.html" rel="noopener" target="_blank">32 ビット Windows OS 環境で動作する 32 ビット版 AutoCAD 2020、AutoCAD LT 2020 の提供はありませんのでご注意ください。</a></strong></p>
<ul>
<li><strong>Windows 7</strong> <strong>SP1</strong><strong> ＋ </strong><a href="https://support.microsoft.com/ja-jp/help/4019990/update-for-the-d3dcompiler-47-dll-component-on-windows">KB4019990</a> <a href="https://support.microsoft.com/ja-jp/help/4019990/update-for-the-d3dcompiler-47-dll-component-on-windows">Update</a>
<ul>
<li>下記エディションの 64 ビット版のみ</li>
<li>Enterprise、Ultimate、Professional、Home Premium</li>
</ul>
</li>
<li><strong>Windows 8.1</strong> ＋ <a href="https://www.microsoft.com/ja-jp/download/details.aspx?id=42327">KB2919355</a> <a href="https://www.microsoft.com/ja-jp/download/details.aspx?id=42327">Update</a>
<ul>
<li>下記エディションの 64 ビット版のみ</li>
<li>（標準）、Pro、Enterprise</li>
</ul>
</li>
<li><strong>Windows 10</strong> <strong>Anniversary Update</strong> (version 1803 以降)
<ul>
<li>下記エディションの 64 ビット版のみ</li>
<li>Home、Pro、Enterprise、Education</li>
</ul>
</li>
<li><strong>32</strong><strong> ビット版の提供はなし</strong></li>
</ul>
<p>続いて、旧リーリスとなる AutoCAD 2019 との互換性についてです。図面ファイル形式は、AutoCAD 2019 同様、引き続き <strong>2018 図面ファイル形式&#0160;</strong>をサポートします。本リリースの API は、AutoLISP、ActiveX オートメーション（COM）、ObjectARX、.NET API、JavaScript の API をサポートしていますが、AutoCAD 2019 とのバイナリ互換リリースとなっているため、そのまま AutoCAD 2020 でも動作するはずです。もちろん、Visual LISP エディタ、VBA エディタも用意されています。VBA エディタは<strong> <a href="http://www.autodesk.com/vba-download">http://www.autodesk.com/vba-download</a></strong>&#0160;から別途ダウンロードする必要があります。なお、API は、従来通り、AutoCAD LT ではサポートされていませんのでご注意ください。&#0160;</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a4453d22200c-pi" style="display: inline;"><img alt="Compatibilities" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0240a4453d22200c image-full img-responsive" src="/assets/image_265006.jpg" title="Compatibilities" /></a></p>
<p>ここからは、AutoCAD 2020 と AutoCAD LT 2020 に共通した機能として提供される具体的な内容についてご案内していきます。</p>
<p><strong>新しいダークテーマ</strong></p>
<p>昨秋販売を開始した <strong><a href="https://adndevblog.typepad.com/technology_perspective/2018/11/releasing-autocad-2019-for-mac-japanese.html" rel="noopener" target="_blank">AutoCAD for Mac 日本語版</a></strong> との名称統一のため、従来、OPTIONS[オプション] コマンドで「<strong>配色パターン</strong>」の名称で提供されてきたユーザ インタフェースの色調設定を「<strong>カラーテーマ</strong>」の名称に改めています。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a4936903200b-pi" style="display: inline;"><img alt="Color_theme" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0240a4936903200b image-full img-responsive" src="/assets/image_834583.jpg" title="Color_theme" /></a></p>
<p>暗い色調の「<strong>ダーク テーマ</strong>」については、新たに青味がかった色合いに変更されると同時に、タイトルバーも含めたコントラスト差が少なったため、目に優しく、作図領域により集中しやすくなっています。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a46ec69b200d-pi" style="display: inline;"><img alt="Dark_theme" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0240a46ec69b200d image-full img-responsive" src="/assets/image_278607.jpg" title="Dark_theme" /></a></p>
<p>また、リボン タブやボタンの選択時、従来よりも明瞭さや鮮明さを改善して見やすくなっています。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a46ec6a6200d-pi" style="display: inline;"><img alt="Dark_theme_select" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0240a46ec6a6200d image-full img-responsive" src="/assets/image_508017.jpg" title="Dark_theme_select" /></a></p>
<p>透過色を持つ PNG 画像ファイルを用いてリボン インタフェースやツールバー上にカスタム ボタンを配置して利用されている場合には、新しいダークテーマの背景色にボタン背景色に変更する手間を省くことが可能です。&#0160;</p>
<p>余談となりますが、<strong><a href="https://web.autocad.com/" rel="noopener" target="_blank"> AutoCAD Web アプリ（https://web.autocad.com）</a></strong> アプリでもダーク テーマを選択することも出来るようになっています。Web ブラウザを最大化してタイトルバーが見えないようにすると、AutoCAD 2020 や LT 2020 と同じ色調で利用出来るようになります。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a4999f7c200b-pi" style="display: inline;"><img alt="AutoCAD Web ダークテーマ" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0240a4999f7c200b image-full img-responsive" src="/assets/image_809902.jpg" title="AutoCAD Web ダークテーマ" /></a></p>
<p><strong>パフォーマンス</strong></p>
<p>新しいインストール テクノロジにより、ソリッド ステート ドライブ(SSD) へのインストール時間が大幅に短縮されます(通常の約半分)。また、外部参照、ブロック、およびサポートファイルへのネットワークアクセス時間が改善されています。サポートファイルには、ハッチング、ツールパレット、フォント、線種、テンプレートファイル、標準仕様ファイルなどに関するファイルがあります。</p>
<p>GRAPHICSCONFIG[ハードウェア パフォーマンスの調整]&#0160; コマンドで調整可能なグラフィックス パフォーマンスの設定では、「中間モード」が更新されて、いくつかの表示パラメータが自動的にリセットされて表示が最適化されるようになりました。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a46ec36d200d-pi" style="display: inline;"><img alt="Graphics_config" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0240a46ec36d200d image-full img-responsive" src="/assets/image_460450.jpg" title="Graphics_config" /></a></p>
<p><strong>クイック計測ツール</strong></p>
<p>MEASUREGEOM[ジオメトリ計測] コマンドに新しく [クイック] オプションが追加されています。このオプションを使用すると、マウス カーソルをホバーするだけで近接するジオメトリの、またはジオメトリ間の寸法、距離、角度を瞬時に計測、一時グラフィックスとして表示してくれるので、すばやく周囲の状態を把握することができます。</p>
<p>&#0160;</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a492fb6a200b-pi" style="display: inline;"><img alt="Quick_measure" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0240a492fb6a200b image-full img-responsive" src="/assets/image_795064.jpg" title="Quick_measure" /></a></p>
<p><strong><a href="https://adndevblog.typepad.com/technology_perspective/2019/03/new-features-on-autocad-2020-part2.html" rel="noopener" target="_blank">次回</a></strong>は、作図や図面編集に直接関係する機能をご案内していきます。</p>
<p>By Toshiaki Isezaki</p>
