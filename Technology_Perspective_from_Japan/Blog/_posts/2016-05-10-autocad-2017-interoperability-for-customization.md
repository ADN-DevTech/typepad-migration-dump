---
layout: "post"
title: "AutoCAD 2017 のカスタマイズ互換性"
date: "2016-05-10 03:33:03"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2016/05/autocad-2017-interoperability-for-customization.html "
typepad_basename: "autocad-2017-interoperability-for-customization"
typepad_status: "Publish"
---

<p>今回は、カスタマイズを含む AutoCAD 2017 の互換性についてご案内します。AutoCAD 2017 自体の新機能については、次のブログ記事をご確認ください。</p>
<p style="padding-left: 30px;"><strong><a href="http://adndevblog.typepad.com/technology_perspective/2016/03/new-features-on-autocad-2017-part1.html" rel="noopener" target="_blank">AutoCAD 2017 の新機能 ～ その1</a></strong></p>
<p style="padding-left: 30px;"><strong><a href="http://adndevblog.typepad.com/technology_perspective/2016/03/new-features-on-autocad-2017-part2.html" rel="noopener" target="_blank">AutoCAD 2017 の新機能 ～ その2</a></strong></p>
<p style="padding-left: 30px;"><strong><a href="http://adndevblog.typepad.com/technology_perspective/2016/04/new-features-on-autocad-2017-part3.html" rel="noopener" target="_blank">AutoCAD 2017 の新機能 ～ その3</a></strong></p>
<p style="padding-left: 30px;"><strong><a href="http://adndevblog.typepad.com/technology_perspective/2016/04/new-features-on-autocad-2017-part4.html" rel="noopener" target="_blank">AutoCAD 2017 の新機能 ～ その4</a></strong></p>
<p><strong>サポート OS</strong></p>
<p style="padding-left: 30px;">AutoCAD 2017 がサポートする OS 詳細は次のとおりです。AutoCAD 2017 も、従来どおり、32 ビット版と 64 ビット版がサポートされていて、両プラットフォーム用のモジュールが 1 つのインストーラに同梱されています。プラットフォームによって、自動的に適切な AutoCAD がインストールされるようになっています（32 ビット版 Windows には 32 ビット版 AutoCAD、64&#0160;ビット版 Windows には 64 ビット版 AutoCAD）。</p>
<p style="padding-left: 30px;"><strong>Windows 7 SP1</strong></p>
<p style="padding-left: 60px;">Enterprise、Ultimate、Professional、Home Premium 各エディションの 32 ビット、及び 64 ビット版</p>
<p style="padding-left: 30px;"><strong>Windows 8.1</strong>&#0160;</p>
<p style="padding-left: 60px;">Windows 8.1、Pro、Enterprise 各エディションの 32 ビット、及び 64 ビット版</p>
<p style="padding-left: 60px;">※<a href="https://www.microsoft.com/ja-jp/download/details.aspx?id=42327">KB2919355</a> <a href="https://www.microsoft.com/ja-jp/download/details.aspx?id=42327">Update</a>&#0160;の適用が前提</p>
<p style="padding-left: 30px;"><strong>Windows 10</strong></p>
<p style="padding-left: 60px;">Home、Pro、Enterprise、Education の各エディションの 32 ビット、及び 64 ビット版</p>
<p style="padding-left: 30px;">近年販売されているコンピュータは、高解像度のディスプレイ解像度を持つものが多くあり、ユーザ インタフェースの文字が小さくなる傾向があります。この問題を改善する目的で、Windows にデスクトップ スケール という機能がありますが、AutoCAD 利用時には125% デスクトップ スケール (120 DPI) が推奨されている点に注意してください。これ以上のスケールを利用した場合、ユーザ インタフェースによって、文字の表示が大きめに表示される場合があります。</p>
<p><strong>図面ファイル形式</strong></p>
<p style="padding-left: 30px;">AutoCAD 2017、AutoCAD LT 2017 は、図面ファイル形式の内部に改変を加える必要性がなかったため、引き続き 2013 DWG/DXF ファイル形式を採用しています。過去の図面ファイル形式の改変についての理由については、過去のブログ記事 <strong><a href="http://adndevblog.typepad.com/technology_perspective/2014/08/reason-for-updating-drawing-format.html" rel="noopener" target="_blank">図面ファイル形式の更新について</a></strong>&#0160;を参照してみてください。&#0160;</p>
<p style="padding-left: 30px;"><strong> <a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c84efbc3970b-pi" style="display: inline;"><img alt="Interoperability" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b7c84efbc3970b image-full img-responsive" src="/assets/image_239644.jpg" title="Interoperability" /></a></strong>もちろん、旧バージョンとのデータ交換に必要な下位図面ファイル形式の保存機能も備えています。</p>
<p style="padding-left: 30px;"><strong>図面を開く：</strong></p>
<p style="padding-left: 60px;">すべての AutoCAD バージョンで作成した DWG ファイル<br />すべての AutoCAD バージョンで作成した DXF ファイル</p>
<p style="padding-left: 30px;"><strong>図面の保存：</strong></p>
<p style="padding-left: 60px;">R14、2000、2004、2007、2010 形式の DWG ファイル<br />R12、2000、2004、2007、2010 形式の DXF ファイル</p>
<p style="padding-left: 30px;">旧バージョンで読み込める図面ファイル形式で図面を保存した場合の制限事項については、オンライン ヘルプ <strong><a href="http://help.autodesk.com/view/ACD/2017/JPN/?guid=GUID-C36C0AF2-6925-4E7F-BDAD-F57897D837B2" rel="noopener" target="_blank">概要 - 旧図面ファイル形式で図面を保存する</a></strong>&#0160;をご確認ください。</p>
<p><strong>アドイン アプリケーションの互換性</strong></p>
<p style="padding-left: 30px;">AutoCAD 2017 は、引き続き、AutoLISP/Visual LISP、ActiveX オートメーション（COM）、ObjectARX、.NET API、JavaScript API の 5 &#0160;つの AutoCAD API をサポートします。ただし、 原則、バイナリ非互換リリースとなるため、旧バージョン用に各種 AutoCAD API 作成されたアドイン アプリケーションは、必要に応じて移植作業が必要になります。</p>
<p style="padding-left: 30px;"><strong>ObjectARX</strong></p>
<p style="padding-left: 30px;">ObjectARX SDK for AutoCAD 2017 を参照して、新しい開発環境となる Visual Studio 2015 で再ビルドする必要があります。旧バージョンで作成した Visual Studio プロジェクトは、Visual Studio 2015 で開くことで自動的にアップグレードされて、「プラットフォーム ツールセット」が <strong>Visual Studio 2015(v140)</strong> に変更されます。</p>
<p style="padding-left: 30px;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c853f520970b-pi" style="display: inline;"><img alt="Upgrade_arx_project_for_vs2015" class="asset  asset-image at-xid-6a0167607c2431970b01b7c853f520970b img-responsive" src="/assets/image_943497.jpg" style="width: 400px;" title="Upgrade_arx_project_for_vs2015" /></a></p>
<p style="padding-left: 30px;">あとは、リンクするスタティック リンク ライブラリを&#0160;<strong>*</strong><strong>21.lib</strong>&#0160;に変更するだけです。例えば、AutoCAD 2016 時に Visual Studio 2012 Update 4 で参照していた&#0160;ac1st20.lib は、ac1st21.lib に置き換えてください。</p>
<p style="padding-left: 30px;">ObjectARX でカスタム オブジェクトを定義していて、COM サーバーとしてオブジェクト、メソッド、プロパティを COM で公開している場合は、.idl ファイルでインポートしているタイプライブラリも&#0160;<strong>axdb21enu.tlb</strong> ないし、<strong>axdb21jpn.tlb</strong> に書き換える必要があります。</p>
<p style="padding-left: 30px;">2017 で廃止、変更されたクラスや関数については、<strong><a href="http://www.autodesk.com/objectarx" rel="noopener" target="_blank">http://www.autodesk.com/objectarx</a></strong> （2018 年8月以降 <strong><a href="https://www.autodesk.com/developer-network/platform-technologies/autocad/objectarx" rel="noopener" target="_blank">https://www.autodesk.com/developer-network/platform-technologies/autocad/objectarx</a></strong>&#0160;が利用可能）からダウンロード可能な ObjectARX SDK for AutoCAD 2017 のインストール後、docs フォルダの Reference Guide（<strong>arxref.chm</strong>）の<strong> ObjectARX Migration Guide</strong> セクションを確認してみてください。</p>
<p style="padding-left: 30px;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb08f78f9e970d-pi" style="display: inline;"><img alt="Objectarx_migration_guide" class="asset  asset-image at-xid-6a0167607c2431970b01bb08f78f9e970d img-responsive" src="/assets/image_561769.jpg" title="Objectarx_migration_guide" /></a></p>
<p style="padding-left: 30px;">なお、正式にサポートされる Visual Studio 2015 は RTM バージョンとなりますが、インターネット接続のあるコンピュータで Visual Studio 2015 をインストールした場合、Update が自動的にダウンロードされて適用されてしまいます。</p>
<p style="padding-left: 30px;">もし、MSDN から Visual Studio 2015 RTM の ISO のイメージを入手可能な場合には、<strong><a href="https://msdn.microsoft.com/ja-jp/library/e2h7fzkw.aspx">https://msdn.microsoft.com/ja-jp/library/e2h7fzkw.aspx</a></strong>&#0160;に記載のコマンド ライン パラメータ <strong>/NoRefresh /noweb</strong> を用いて、RTM のままインストールすることも可能です。</p>
<p style="padding-left: 30px;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d1ddb987970c-pi" style="display: inline;"><img alt="Vs2015rtm_install" class="asset  asset-image at-xid-6a0167607c2431970b01b8d1ddb987970c img-responsive" src="/assets/image_860302.jpg" style="width: 400px;" title="Vs2015rtm_install" /></a></p>
<p style="padding-left: 30px;"><strong>.NET API</strong></p>
<p style="padding-left: 30px;">前バージョン用に作成されたアドイン アプリケーションは、そのままロードして実行できるはずです。</p>
<p style="padding-left: 30px;">可能であれば、AutoCAD 2017 のアセンブリ ファイルを参照して再ビルドすることをお勧めします。もちろん、一部のクラスやメソッド、プロパティが変更されている場合がありますので、その場合には、ソースコードに適切な変更を加える必要があります。</p>
<p style="padding-left: 30px;">2017 で廃止、変更されたクラスやメソッド、プロパティについては、<strong><a href="http://www.autodesk.com/objectarx" rel="noopener" target="_blank">http://www.autodesk.com/objectarx</a></strong>&#0160;からダウンロード可能な&#0160;ObjectARX SDK for AutoCAD 2017 のインストール後、docs フォルダの Managed Class Reference Guide（<strong>arxmgd.chm</strong>）の ObjectARX Migration Guide セクションを確認してみてください。</p>
<p style="padding-left: 30px;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb08f78f89970d-pi" style="display: inline;"><img alt="Dotnetapi_migration_guide" class="asset  asset-image at-xid-6a0167607c2431970b01bb08f78f89970d img-responsive" src="/assets/image_550577.jpg" title="Dotnetapi_migration_guide" /></a></p>
<p style="padding-left: 30px;"><strong>AutoLISP/ActiveX オートメーション（COM）/JavaScript</strong></p>
<p style="padding-left: 30px;">前バージョン用に作成されたアドイン アプリケーションは、そのままロードして実行できるはずです。</p>
<p style="padding-left: 30px;">ActiveX オートメーション（COM）をお使いの場合には、可能であれば、新しいバージョンのタイプライブラリを参照しなおしてテストすることをお勧めします。タイプライブラリの詳細は、AutoCAD 2017 の<strong><a href="http://help.autodesk.com/view/ACD/2017/JPN/?guid=GUID-927E71C2-E515-438E-9D7A-246D97BEF93F" rel="noopener" target="_blank">オンラインヘルプ</a></strong>をご確認ください。</p>
<p style="padding-left: 30px;">VBA をお使いの場合、VBA コンポーネントは<a href=" http://www.autodesk.com/vba-download" rel="noopener" target="_blank"> h<strong>ttp://www.autodesk.com/vba-download</strong></a>&#0160;から参照可能な Autodesk &#0160;Knowledge Network 記事からダウンロードすることが出来ます。<strong><a href="http://www.autodesk.com/vba-download-jpn" rel="noopener" target="_blank">http://www.autodesk.com/vba-download-jpn</a>&#0160;</strong>を参照すれば、日本語の&#0160;Autodesk &#0160;Knowledge Network 記事を表示させることも出来ます。</p>
<p style="padding-left: 30px;">※ その他、アドイン アプリケーションの互換性に関する情報は、AutoCAD 2017 の<strong><a href="http://help.autodesk.com/view/ACD/2017/JPN/?guid=GUID-E69C877D-F84B-4282-807D-E084C931D533" rel="noopener" target="_blank">オンライン ヘルプ</a></strong>をご参照ください。&#0160;</p>
<p><strong>メニュー ボタン</strong></p>
<p style="padding-left: 30px;">&#0160;旧バージョンでカスタム ボタンの背景を透明に表現する目的で使用できた RGB 値、192,192,192 の色指定は、仕様変更によって AutoCAD 2017 で無効になっています。このため、AutoCAD 2017 以降のバージョンで透過性を持つカスタム ボタンのイメージを利用するためには、BMP ファイル形式ではなく、標準で透過性をサポートする PNG ファイルをお使いください。</p>
<p style="padding-left: 30px;">PNG ファイルへの変換手順は、AutoCAD 2017 オンライン ヘルプの<strong><a href="http://help.autodesk.com/view/ACD/2017/JPN/?guid=GUID-94A0EA14-5165-4D84-A2F1-F44D33F80BCC" rel="noopener" target="_blank">記事</a></strong>をご確認ください。この方法で、ダークテーマとライトテーマの両方を 1 つの画像ファイルでサポート出来るようになります。</p>
<p><strong>デバッグ時の注意</strong></p>
<p style="padding-left: 30px;"><strong>ObjectARX</strong> <strong>プロジェクト</strong></p>
<p style="padding-left: 30px;">ObjectARX プロジェクトを Visual Studio 2015 で開き、デバッグ モードで AutoCAD 2017 を起動する際に、例外エラーが 2 回発生することが報告されています。この現象は、すでに Microsoft 社にも通知して調査を依頼していますが、2016年5月6日の段階で修正にいたっていません。</p>
<p style="padding-left: 30px;">取り急ぎ、例外エラーが表示されたら、[継続(C)] ボタンをクリックすることで AutoCAD を起動してデバッグセッションを開始、通常通りデバッグすることが出来ます。</p>
<p style="padding-left: 30px;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c8540e05970b-pi" style="display: inline;"><img alt="Objectarx_debug_error" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b7c8540e05970b image-full img-responsive" src="/assets/image_833585.jpg" title="Objectarx_debug_error" /></a></p>
<p style="padding-left: 30px;"><strong>&#0160;.NET API プロジェクト <br /><span style="background-color: #ffff00;">下記↓でご案内している設定は AutoCAD 2017.1 Update 以降で不要になっています。<br />2017.1 以降でこの設定を施すとエディット コンティニュが動作しないことがあります。ご注意ください。</span></strong></p>
<p style="padding-left: 30px;">.NET API プロジェクトを Visual Studio 2015 で開き、デバッグ モードで AutoCAD 2017 を起動した際、ビルドしたアセンブリをロードしなくても、起動した AutoCAD 上で STYLE コマンドなどを実行すると、「AutoCAD Application は動作を停止しました」 と表示されて AutoCAD が起動できません。これは、マルチスレッドをサポートしない AutoCAD に対して、Visual Studio の エディット&amp;コンティニュ機能を動作させるため、Visual Studio がメインスレッド以外のスレッドを作成して acdb*.dll アセンブリなどをロードしようとしたために発生する現象です。</p>
<p style="padding-left: 30px;">この問題を回避するには、次の 2 つのいずれかをプロジェクト設定に加えてください。</p>
<p style="padding-left: 30px;">1.&#0160;[ツール] &gt;&gt; [オプション] メニューから&#0160;[オプション] ダイアログを表示させ、[デバッグ] &gt;&gt; [全般] の 「マネージ互換モードの使用」 を有効にする。</p>
<p style="padding-left: 30px;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c8541328970b-pi" style="display: inline;"><img alt="Enable_dotNet_debug1" class="asset  asset-image at-xid-6a0167607c2431970b01b7c8541328970b img-responsive" src="/assets/image_258891.jpg" style="width: 700px;" title="Enable_dotNet_debug1" /></a></p>
<p style="padding-left: 30px;">2.プロジェクトのプロパティを表示させ、[デバッグ]タブの「ネイティブ コードのデバッグを有効にする」を有効にする。</p>
<p style="padding-left: 30px;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c8541377970b-pi" style="display: inline;"><img alt="Enable_dotNet_debug2" class="asset  asset-image at-xid-6a0167607c2431970b01b7c8541377970b img-responsive" src="/assets/image_639249.jpg" style="width: 700px;" title="Enable_dotNet_debug2" /></a></p>
<p><strong>Windows 10 での ObjectARX Wizard の問題</strong></p>
<p style="padding-left: 30px;">Windows 10 にインストールされた Visual Studio 2015 に、<a href="http://www.autodesk.com/developautocad" rel="noopener" target="_blank">http://www.autodesk.com/developautocad</a> または、<a href="http://www.autodesk.co.jp/developautocad" rel="noopener" target="_blank">http://www.autodesk.co.jp/developautocad</a>&#0160;からダウンロードした&#0160;<a href="http://images.autodesk.com/adsk/files/ObjectARXWizards-2017.zip" onclick="return openPopup(this.href,null,null);">ObjectARX 2017 Wizard</a>&#0160;をインストールした際、<strong>ObjectARX/DBX Project テンプレート</strong>を指定して新規プロジェクトを作成しようとしても、何も処理されません。</p>
<p style="padding-left: 30px;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d1dde283970c-pi" style="display: inline;"><img alt="Objectarx_wizard_failed_on_window10" class="asset  asset-image at-xid-6a0167607c2431970b01b8d1dde283970c img-responsive" src="/assets/image_724662.jpg" style="width: 700px;" title="Objectarx_wizard_failed_on_window10" /></a></p>
<p style="padding-left: 30px;">これは、ObjectARX &#0160;Wizard のインストーラ（ObjectARXWizard.msi）に、デジタル署名が施されていないために起こる現象です。この問題は、Windows 10 上でコマンド プロンプトを管理者モードで起動して、次のように、<strong><a href="https://msdn.microsoft.com/ja-jp/library/cc759262%28v=ws.10%29.aspx" rel="noopener" target="_blank">msiexec</a></strong> を使って ObjectARX Wizard をインストールすることで回避することが出来ます。</p>
<p style="padding-left: 30px;"><strong>msiexec /i ObjectARXWizards.msi</strong></p>
<p style="padding-left: 30px;">コマンド プロンプトを管理者モードで起動するには、スタート ボタンから [Windows システム ツール] &gt;&gt; [コマンド プロンプト] を見つけて、マウスの右ボタン メニューから [その他] &gt;&gt; [管理者として実行] を選択してください。</p>
<p>By Toshiaki Isezaki&#0160;</p>
