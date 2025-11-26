---
layout: "post"
title: "AutoCAD オンラインヘルプと API ドキュメント"
date: "2014-07-16 02:32:12"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2014/07/autocad-onlinehelp-and-api-document.html "
typepad_basename: "autocad-onlinehelp-and-api-document"
typepad_status: "Publish"
---

<p>今年もデスクトップ製品に数多くの新バージョンが 2015 シリーズ製品としてリリースされています。当然、新機能を含めたオンランヘルプが用意されています。</p>
<p>AutoCAD の場合、AutoCAD を起動中に [F1] キーを押すと、AutoCAD 内でオンラインを参するためウィンドウが表示されます。コマンドの実行中に [F1] キーを押せば、実行中のコマンドやシステム変数の内容を表示するコンテキスト ヘルプ（状況依存ヘルプ）機能があります。</p>
<p>AutoCAD 2015 では、オンラインヘルプ中に「検索」リンクがある場合、リボンメニューに配置されたコマンド ボタンの場所を矢印アニメーションで知らせてくれる便利な機能も用意されています。</p>
<p style="text-align: center;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01a3fd1e2333970b-pi" style="display: inline;"><img alt="Let_command_location" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01a3fd1e2333970b image-full img-responsive" src="/assets/image_728585.jpg" title="Let_command_location" /></a></p>
<p>さて、AutoCAD 2015 オンラインヘルプですが、AutoCAD ウィンドウ内からだけでなく、お使いの Web ブラウザからも直接参照することが出来ます。URL は、<a href="http://help.autodesk.com/view/ACD/2015/JPN/" target="_blank">http://help.autodesk.com/view/ACD/2015/JPN/</a> です。AutoCAD 内部でオンライヘルプを開いた場合、ウィンドウが邪魔になってコマンド操作が出来ない場合があるので、そんな場合にはブラウザでオンラインヘルプを開いて、参照しながら作業を進めることが出来るはずです。</p>
<p>AutoCAD 2015 のオンラインヘルプ内には、開発者用のトピックがまとめられたページが用意されています。<a href="http://help.autodesk.com/view/ACD/2015/JPN/%20" target="_blank">ヘルプ画面トップ</a>の「<strong><a href="http://help.autodesk.com/view/ACD/2015/JPN/files/homepage_dev.htm" target="_blank">開発者ホームページ</a></strong>」をクリックしてみてください。&#0160;</p>
<p style="text-align: center;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01a3fd1e1f5e970b-pi" style="display: inline;"><img alt="Acad2015_online_help" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01a3fd1e1f5e970b image-full img-responsive" src="/assets/image_158585.jpg" title="Acad2015_online_help" /></a>&#0160;</p>
<p>あまり知られてていないようですが、開発者ホームページには、従来からの AutoLISP 開発者ガイド、リファレンスのような&#0160;<a href="http://help.autodesk.com/cloudhelp/2015/JPN/AutoCAD-AutoLISP/files/GUID-265AADB3-FB89-4D34-AA9D-6ADF70FF7D4B.htm">AutoCAD 2015 AutoLISP ドキュメント</a>&#0160;に加えて、日本語の&#0160;<strong>AutoCAD .NET API 開発者用ガイド</strong> が含まれています。このガイドには、.NET API の仕組みや開発手順に加えて、主要なクラスやメソッド、プロパティについてのサンプル コードが多数記載されています。よく、サンプルプログラムのリクエストをいただきますが、多くは、このオンラインヘルプの中に記載されていますので是非参照してみてください。</p>
<p><a href="http://help.autodesk.com/cloudhelp/2015/JPN/AutoCAD-NET/files/GUID-4E1AAFA9-740E-4097-800C-CAED09CDFF12.htm">AutoCAD 2015 .NET API 開発者用ガイド</a></p>
<p>なお、.NET API のリファレンスは、残念ながらオンラインヘルプには含まれないので注意してください。AutoCAD .NET API リファレンスは、<a href="http://www.autodesk.com/objectarx" target="_blank">http://www.autodesk.com/objectarx</a> から無償ダウンロード可能な ObjectARX SDK に含まれています。こちらは、英語版のみの提供で、ObjectARX SDK インストール後に&#0160;docs フォルダの&#0160;<strong>arxmgd.chm</strong> です。</p>
<p style="text-align: center;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01a511cda8cd970c-pi" style="display: inline;"><img alt="Acad2015_developer_help" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01a511cda8cd970c image-full img-responsive" src="/assets/image_789140.jpg" title="Acad2015_developer_help" /></a>&#0160;</p>
<p>最後に、AutoCAD 2015 の VBA モジュールですが、今年も <a href="http://www.autodesk.com/vba-download" target="_blank">http://www.autodesk.com/vba-download</a> からのダウンロード提供になっています。昨年までの AutoCAD バージョンでは、VBA が利用する ActiveX オートメーション関連の一部ドキュメントは、VBA モジュールをインストールすることで旧来の .chm ファイル形式がインストールされていました。</p>
<p>今回、それら VBA/ActiveX オートメーション関連の日本語ドキュメントも、AutoCAD 2015 オンラインヘルプに統合されていて、直接参照できるようになしました。もちろん、ヘルプトピック内には、サンプルコードも含まれています。AutoCAD .NET API で COM を利用する場面では、オンライヘルプにオブジェクト モデルを含む開発者用ガイドやリファレンスを参照したケースがありますが、この部分は大分改善されるはずです。</p>
<p><a href="http://help.autodesk.com/cloudhelp/2015/JPN/AutoCAD-ActiveX/files/GUID-36BF58F3-537D-4B59-BEFE-2D0FEF5A4443.htm">AutoCAD 2015 ActiveX 開発者用ガイド</a></p>
<p><a href="http://help.autodesk.com/view/ACD/2015/JPN/?guid=GUID-5D302758-ED3F-4062-A254-FB57BAB01C44" target="_blank">AutoCAD 2015 AcitveX リファレンス</a></p>
<p><a href="オブジェクト%20モデル" target="_blank">オブジェクト モデル</a></p>
<p>開発者により身近になった AutoCAD 2015 のオンライン ヘルプをご活用ください。</p>
<p>By Toshiaki Isezaki&#0160;</p>
<p>&#0160;</p>
