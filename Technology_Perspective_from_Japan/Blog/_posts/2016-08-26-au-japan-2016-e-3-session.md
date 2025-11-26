---
layout: "post"
title: "AU Japan 2016：Forge トラック SCSK 事例"
date: "2016-08-26 02:31:09"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2016/08/au-japan-2016-e-3-session.html "
typepad_basename: "au-japan-2016-e-3-session"
typepad_status: "Publish"
---

<p>Autodesk University Japan 2016 の Forge トラック E-3 セッションでは、SCSK 株式会社・阿部さんに、Forge Platform API の 1 つである Design Automation API の事例をご案内いただきます。Design Automation API という名称は、あまり馴染みがないかもしれませんが、以前、 AutoCAD I/O と呼ばれていた API です。</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d2155bf8970c-pi" style="float: left;"><img alt="Autocad-badge-128px-hd" class="asset  asset-image at-xid-6a0167607c2431970b01b8d2155bf8970c img-responsive" src="/assets/image_310854.jpg" style="margin: 0px 5px 5px 0px;" title="Autocad-badge-128px-hd" /></a>AutoCAD&#0160;2013 以降のバージョンでは、AutoCAD のインストール フォルダに <strong>acad.exe</strong> と <strong>accoreconsole.exe</strong> の 2 つの実行ファイルがインストールされます。前者をダブルクリックして起動すると、ユーザ インタフェースを持つ通常の AutoCAD が起動しますが、後者の場合には、ユーザ インタフェースを持たない AutoCAD のコアモジュールが Windows のコマンド プロンプトで起動します。ユーザ インタフェースを持たない accoreconsole.exe は、実行時に消費するメモリ量も少ないので、acad.exe に比べると非常に軽快に動作します。accoreconsole.exe の詳細は過去のブログ記事 <strong><a href="http://adndevblog.typepad.com/technology_perspective/2013/06/console-version-of-autocad.html" target="_blank">コンソール バージョンの AutoCAD</a></strong>&#0160;をご参照ください。</p>
<p>重要なのは、accoreconsole.exe が AutoCAD の基本機能を持ち、AutoCAD コマンドを実行できるだけでなく、アドインをロードして実行する能力を持つ点です。ただ、accoreconsole.exe は AutoCAD パッケージに含まれるので、その利用範囲が<strong>S oftware License Agreement</strong>、通称、<strong>SLA</strong> と呼ばれる「<strong>使用許諾およびサービス契約</strong>」に&#0160;<strong><a href="http://adndevblog.typepad.com/technology_perspective/2016/08/use-of-desktop-products-on-the-shared-server.html" target="_blank">制約</a>&#0160;</strong>されてしまいます。つまり、共有サーバーにインストールして不特定のエンドユーザに運用させることは出来ません。</p>
<p>この制約にとらわれず、accoreconsole.exe をクラウド上で実行させて、AutoCAD アドインを実行させるのが&#0160;Design Automation API が提供する機能です。実行させるアドインにはバッチ処理をさせることになりますが、処理内容はアドインの実装次第です。DWG ファイルから PDF ファイルへのファイル変換（図面出力）や、表題欄の書き換えや部品の集計、部品表の作図など、さまざま処理をクラウドで実現することが出来ます。</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c88bbe13970b-pi" style="float: left;"><img alt="Forge" class="asset  asset-image at-xid-6a0167607c2431970b01b7c88bbe13970b img-responsive" src="/assets/image_276073.jpg" style="margin: 0px 5px 5px 0px;" title="Forge" /></a>E-3 セッションでは、SCSK 株式会社が AutoCAD アドインで実現していた自動作図の機能を、Design Automation API に移植して利用する手法や実際の成果をご確認いただくことが出来ます。セッション後半では、ご参加いただいた方にスマートフォンの Web ブラウザを使って、実際に図面作成のサービスをお試しいただけるそうです。もちろん、クラウド上で自動製図された図面は、Web ブラウザ上に Forge Viewer で表示されることになります。</p>
<p>Design Automation API を使ったシステムはタブレットやスマートフォンでも運用できるので、例えば、外回りの営業マンが顧客の要望に合わせて図面を作成するような見積りシステムなども構築できます。</p>
<p>SIer として多くの CAD 関連の受託開発をされている阿部さんの目線で見た、Autodesk Forge が実現する &quot;繋がる&quot; システム構築をご確認ください。</p>
<p>By Toshiaki Isezaki</p>
