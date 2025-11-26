---
layout: "post"
title: "Forge Viewer の AutoCAD Civil 3D DWG サポート"
date: "2017-07-05 00:08:26"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2017/07/autocad-civil-3d-dwg-support-on-forge-viewer.html "
typepad_basename: "autocad-civil-3d-dwg-support-on-forge-viewer"
typepad_status: "Publish"
---

<p>デザインファイルをクラウドにアップロードしたファイルを変換して、 Web ブラウザ上の Forge Viewer にストリーミングで表示するのが、現在、よく利用されている代表的な Forge の仕組みです。</p>
<p>ここで利用するのが、Forge Platform API で用意されているがアプリの認証/認可で利用される OAuth API（Authentication API）、デザイン ファイルのアップロードを含むストレージ アクセスに利用する Data Management API、アップロードしたファイルを表示用に変換する Model Derivative API、最終的にビューアに表示させるための JavaScript ライブラリである Viewer です。</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d2918a49970c-pi" style="float: right;"><img alt="名称未設定 2" class="asset  asset-image at-xid-6a0167607c2431970b01b8d2918a49970c img-responsive" src="/assets/image_15099.jpg" style="width: 120px; margin: 0px 0px 5px 5px;" title="名称未設定 2" /></a>AutoCAD の DWG 図面の変換や表示は、当初から Forge でサポートされていましたが、AutoCAD Civil3D などの業種別製品には、カスタム オブジェクトと呼ばれる業種別製品固有のオブジェクト（図形要素）含まれてるため、いままで、それらの情報の変換と配信は除外されてきました。</p>
<p>今回、AutoCAD Civil 3D の DWG ファイルに含まれるカスタム オブジェクトのメタデータも、正しく変換、及び、配信出来るようになっています。例えば、基線（コリドーの 3D パスの中心線）や左側/右側の線形データを得ることが出来ます。</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d291928b970c-pi" style="display: inline;"><img alt="Civil_props" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b8d291928b970c image-full img-responsive" src="/assets/image_769386.jpg" title="Civil_props" /></a></p>
<p>この機能は、Forge Viewer をベースにした A360 などのクラウド サービス内に組み込まれたビューアや、オートデスクの Web サイト <a href="http://www.autodesk.co.jp" rel="noopener" target="_blank">http://www.autodesk.co.jp</a> からアクセス可能な&#0160;<strong><a href="https://a360.autodesk.com/viewer/" rel="noopener" target="_blank">オンラインビューア</a>&#0160;</strong>&#0160;でも利用することが出来ます。</p>
<p>By Toshiaki Isezaki</p>
