---
layout: "post"
title: "AutoCAD 2016 のカスタマイズ互換性"
date: "2015-04-22 03:28:21"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2015/04/autocad-2016-interoperability-for-customization.html "
typepad_basename: "autocad-2016-interoperability-for-customization"
typepad_status: "Publish"
---

<p>今回は、AutoCAD 2016 の API カスタマイズの互換性についてご紹介しておきます。AutoCAD 2016 は、いままでの製品サイクルと若干異なり、アドイン アプリケーションと図面ファイル形式の互換性が、いままでの製品サイクルと若干異なっています。</p>
<p style="text-align: center;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb082177c8970d-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="Compatibilities" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01bb082177c8970d image-full img-responsive" src="/assets/image_161046.jpg" title="Compatibilities" /></a></p>
<p><strong>図面ファイル形式</strong></p>
<p><strong><a href="http://adndevblog.typepad.com/technology_perspective/2015/03/new-features-on-autocad-2016-part1.html" target="_blank">AutoCAD 2016 の新機能 ～ その1</a>&#0160;</strong>でもご案内したとおり、AutoCAD 2016/AutoCAD LT 2016 が採用する図面ファイル形式は、従来と同じ 2013&#0160;図面ファイル形式（DWG、DXF）となります。従来は、3 世代毎に図面ファイル形式を変更していたので意外に感じられる方もいらっしゃると思いますが、新機能による図面ファイル情報の拡張が必要なかった、というのがその理由です。なお、AutoCAD が「なぜ図面ファイル形式を変更するのか」という疑問について、過去のブログ記事&#0160;<a href="http://adndevblog.typepad.com/technology_perspective/2014/08/reason-for-updating-drawing-format.html" target="_blank"><strong>図面ファイル形式の更新について</strong></a>&#0160;で触れていますので、興味をお持ちの方はご確認ください。</p>
<p>図面ファイル形式に変更はありませんが、図面ファイルに関して注意すべき変更が加えられています。この変更は、API カスタマイズされている方にも影響を及ぼす可能性がありますので、ここで簡単に触れておきたいと思います。</p>
<p style="padding-left: 30px;"><strong>パスワード保護機能</strong></p>
<p style="padding-left: 30px;">AutoCAD 2016 では図面ファイル形式の変更はないのですが、図面パスワードの機能が削除されている点にご注意ください。この機能は、DWG ファイルを保存する際にパスワード設定を施して、その図面を開く際に適切なパスワードを入力しないと、図面を開くことが出来ない、というものです。</p>
<p style="padding-left: 30px;">パスワード保護機能は、図面ファイルのセキュリティを向上させる目的で、AutoCAD 2004 の新機能として登場したものです。機能廃止に理由については、AutoCAD 2016 のオンラインヘルプで <a href="http://help.autodesk.com/view/ACD/2016/JPN/?guid=GUID-4E32143C-AA81-4A4E-9191-B32C4AA259EE" target="_blank"><strong>説明</strong></a> されています。</p>
<p id="GUID-4E32143C-AA81-4A4E-9191-B32C4AA259EE__GUID-18B27039-A324-43A5-8E83-B51C81E0D247" style="padding-left: 60px;"><em>AutoCAD 2016 ベースの製品から、AutoCAD ファイルにパスワードを追加する機能が削除されました。これは、この機能が現代のセキュリティ標準を満たしていないためです。引き続きパスワードで保護されたファイルを開くことはできますが、新しいパスワードを追加することはできません。</em></p>
<p id="GUID-4E32143C-AA81-4A4E-9191-B32C4AA259EE__GUID-1D382E91-FBCE-4B5C-B38A-5D1631CCAF00" style="padding-left: 60px;"><em>オンラインで遭遇する脅威の複雑さは近年ますます増大しており、それに伴いセキュリティ要件も高まっています。次のいずれかの代替策を検討して、機密情報を含む図面ファイルを保護することをお勧めします。</em></p>
<ul style="padding-left: 60px;">
<li><em>図面ファイルを PDF ファイルとして出力し、この PDF にパスワードを追加する</em></li>
<li><em>図面を ZIP ファイルにパッケージ化し、安全な外部ユーティリティを使用してパスワードを追加する</em></li>
<li><em>たとえば、256-bit AES テクノロジまたはこれと同等のテクノロジを使用するサードパーティのパスワードおよび暗号化ユーティリティを使用する</em></li>
<li><em>ネットワーク権限により図面を保護する</em></li>
<li><em>Autodesk 360 またはその他のクラウド プロバイダの権限により図面を保護する&#0160;</em></li>
</ul>
<p style="padding-left: 30px;">なお、 以前のバージョンでパスワードを設定した図面ファイルを AutoCAD 2016 で開く場合には、従来と同じようにパスワード入力を促すダイアログが表示されます。もちろん、正しいパスワードを入力しないと、図面ファイルを開くことは出来ません。ただし、正しいパスワードで開いた図面ファイルを、AutoCAD 2016 で保存してしまうと、設定されていたパスワードは削除されます。AutoCAD 2016 のパスワード入力ダイアログには、この動作についての警告メッセージが表示されます。</p>
<p style="padding-left: 30px; text-align: center;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c77d7995970b-pi" style="display: inline;"><img alt="Password" class="asset  asset-image at-xid-6a0167607c2431970b01b7c77d7995970b img-responsive" src="/assets/image_455379.jpg" title="Password" /></a></p>
<p style="padding-left: 30px;">過去に記載したブログ記事 <a href="http://adndevblog.typepad.com/technology_perspective/2014/10/security-for-drawing-file.html" target="_blank"><strong>図面ファイルのセキュリティ － パスワード保護</strong></a>&#0160;のように、API カスタマイズでパスワード保護機能を利用されてい場合には、新規にパスワード処理することは出来ません。</p>
<p style="padding-left: 30px;">※&#0160;<a href="http://adndevblog.typepad.com/technology_perspective/2014/11/security-for-drawing-file-2.html" target="_blank"><strong>図面ファイルのセキュリティ － デジタル署名</strong></a> は、AutoCAD 2016 でも利用出来ます。</p>
<p style="padding-left: 30px;"><strong>レンダリング エンジン</strong></p>
<p style="padding-left: 30px;">従来の NVIDIA mental ray® から&#0160;AutoCAD 2016 で導入された新しいレンダリング エンジンは、RapidRT と呼ばれるオートデスク製のレンダリング エンジンです。mental ray とは互換性はありませんので、以前のバージョンで設定して、図面に保存されているレンダリング設定は、AutoCAD 2016 上では無視されます。</p>
<p style="padding-left: 30px;">RapidRT では、mental ray に比べて露出オーバーな状態でレンダリングされるので、レンダリング時には、必ず、露出を調整することが推奨されています。この調整には、新しいイメージ ベース照明（IBL）の利用も含まれます。新旧 2 つのレンダリング エンジンの違いについては、AutoCAD 2016 のオンライン ヘルプから&#0160;<a href="http://help.autodesk.com/view/ACD/2016/JPN/?guid=GUID-7DB68AE5-0E8C-4A3B-B921-58B1EB6482A9" target="_blank"><strong>現在のリリースと旧リリースのレンダリングの違い</strong></a> をご参照ください。</p>
<p style="padding-left: 30px;">さて、新しいレンダリング エンジンを採用した AutoCAD 2016 には、残念ながら、RapidRT が適用されていないコマンドが存在します。<a href="http://help.autodesk.com/view/ACD/2016/JPN/?guid=GUID-4B806FDB-C6BC-41FF-879B-148F44E09D14" target="_blank"><strong>ANIPATH[移動パス アニメーション]</strong></a> コマンドです。このコマンドは、レンダリング品質や表示スタイルを含むアニメーション動画を作成します。ただし、レンダリング品質を選択してアニメーション作成をおこなった場合、利用されるレンダリング エンジンは mental ray となります。将来的に、このコマンドも RapidRT 対応になるはずですが、このコマンドのために、AutoCAD 2016 は mental ray と RapidRT の 2 つのレンダリング エンジンを搭載することになっています。</p>
<p style="padding-left: 30px;">2 つのレンダリング エンジンの切り替えは、<strong>隠しシステム変数 RENDERENGINE</strong> でコントロールすることが可能です。このシステム変数の値が 1 の場合には RapidRT、0 の場合には mental ray を AutoCAD が利用します。下記は、システム変数 RENDERENGINE の値によって変化する [ビジュアライズ] リボンタブの違いです。</p>
<p style="padding-left: 30px;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb08217e35970d-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="Ribbon_per_renderer" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01bb08217e35970d image-full img-responsive" src="/assets/image_244201.jpg" title="Ribbon_per_renderer" /></a></p>
<p style="padding-left: 30px;">ANIPATH コマンドでレンダリング品質のアニメーション動画を作成する場合には、RENDERENGINE システム変数の値を 0 にして、mental ray 環境でレンダリング品質を変更することが出来ます。なお、mental ray レンダリング エンジンと&#0160;隠しシステム変数 RENDERENGINE は、将来のバージョンで削除される予定です。あくまで過渡的なものであるため、ANIPATH コマンドでレンダリング品質のアニメーション動画を作成する意外には、使用はお勧めしません。</p>
<p style="padding-left: 30px;">なお、ObjectARX や AutoCAD .NET API でレンダリング処置を実装されている場合、mental ray 用と RapidRT 用の API は全く異なります。このため、RapidRT 環境でも、カスタマイズ モジュールは mental ray の実装をそのまま実行することが出来ます。</p>
<p><strong>API カスタマイズ資産</strong>&#0160;</p>
<p>先にご紹介した図にあるように、AutoCAD 2016 は AutoCAD 2015 の API とバイナリ互換を持ちます。このため、AutoCAD 2015 用のアドイン アプリケーションをお持ちの場合には、そのまま AutoCAD 2016 にロードして実行することが出来ます。</p>
<p>ObjectARX や AutoCAD .NET API を用いて新たに AutoCAD 2016 でビルドされる場合には、開発環境は次のとおりです。</p>
<ul id="GUID-450FD531-B6F6-4BAE-9A8C-8230AAC48CB4__UL_53707A76018046AAB6401C90D9482498">
<li>Microsoft Visual Studio 2012 Update 4</li>
<li>Microsoft .NET Framework 4.5&#0160;</li>
</ul>
<p>Visual Studio バージョンについては、ObjectARX アプリケーションのサポート環境として利用される場合を想定しているため、Visual Studio バージョンが出力可能な .NET Framework バージョンとは異なり、Visual Studio バージョンに含まれる Visual C++ バージョンを基準として設定している点に注意してください。&#0160;</p>
<p>パッケージ ファイル（PackageContents.xml）や、システム レジストリを参照するインストーラは、AutoCAD 2016 のマイナーバージョン番号 R20.1 を参照するよう、変更を加える必要があります。</p>
<p><strong>カスタム モジュールへのデジタル署名&#0160;</strong></p>
<p>AutoCAD 2016 では、API カスタマイズしたモジュール（ファイル）に対するセキィリティ レベルが強化されています。SECURITYOPTIONS[セキュリティオプション] コマンドで表示される&#0160;[セキィリティ オプション] ダイアログが一新され、カスタマイズ ファイルのロードセキィリティを一括管理することが可能になっています。</p>
<p style="text-align: center;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d107036d970c-pi" style="display: inline;"><img alt="Security_options" class="asset  asset-image at-xid-6a0167607c2431970b01b8d107036d970c img-responsive" src="/assets/image_177694.jpg" style="width: 450px;" title="Security_options" /></a></p>
<p>不正なマルウェアやウィルスから設計環境を守るため、このダイアログで設定するセキュリティ レベルは、「高」に設定すべきです。ただし、カスタマイズ モジュールを開発する開発者にも、セキュリティ維持への作業が必要になります。それが、AutoLISP ファイルを含むカスタマイズ モジュールへのデジタル署名です。このデジタル署名によって開発元を明確にするだけでなく、モジュールが不正に変更されているかをチェックすることが出来ます。</p>
<p>もし、カスタマイズ モジュールにデジタル署名が埋め込まれていない場合、AutoCAD は、ファイルのロード時に次のような警告メッセージを表示して、ユーザに注意を促します。</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb08218145970d-pi" style="display: inline;"><img alt="No_signature_warning" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01bb08218145970d image-full img-responsive" src="/assets/image_476277.jpg" title="No_signature_warning" /></a></p>
<p>デジタル署名は認証局から購入する必要があるため、社内利用用途のカスタマイズなど、必ずしも必須となるものではありません。逆に、有償アプリケーションを販売しているプロフェッショナル デベロッパには、必須となるはずです。</p>
<p>カスタム モジュールへのデジタル署名については、AutoCAD 2016 のオンライン ヘルプから&#0160;カスタム <a href="http://help.autodesk.com/view/ACD/2016/JPN/?guid=GUID-0A93626D-8389-45FC-969B-B86A2F37D691" target="_blank"><strong>プログラム ファイルにデジタル署名を行う</strong></a> から読み進んでいただくことをお勧めします。</p>
<p>By Toshiaki Isezaki</p>
