---
layout: "post"
title: "AcCoreConsole.exe でのアドイン テスト"
date: "2019-07-22 00:03:36"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2019/07/test-addin-on-accoreconsoleexe.html "
typepad_basename: "test-addin-on-accoreconsoleexe"
typepad_status: "Publish"
---

<p>Design Automation API for AutoCAD で利用するカスタム アクティビティでは、AppPackage（App パッケージ）にAutoCAD 用に作成したアドイン アプリケーションを指定することが出来ます。指定用可能なアドイン アプリケーションは、ObjecrARX（.crx ファイル）、.NET API（.dll ファイル）、AutoLISP で作成されたもので、いずれも、ダイアログ ボックスやバルーン通知など、ユーザ インタフェースを表示しない実装になっている必要があります。</p>
<p>Design Automation API for AutoCAD へは、<strong><a href="https://knowledge.autodesk.com/ja/search-result/caas/CloudHelp/cloudhelp/2019/JPN/AutoCAD-Customization/files/GUID-BC76355D-682B-46ED-B9B7-66C95EEF2BD0-htm.html" rel="noopener" target="_blank">PackageContents.xml ファイル</a></strong>を使った AppPackage bundle（App パッケージ バンドル）一式を ZIP 圧縮して、事前にアップロードする必要があります。ただ、クラウド上の AutoCAD コアエンジン（accoreconsole.exe）でのテストに、毎回、App パッケージををアップロードするのは少し手間がかかりすぎです。また、実装したアドイン アプリケーション自体の不具合で、アップロードした App パッケージが期待した動作をしない場合も考えられます。</p>
<p>そのような点も考慮して、実際に Design Automation API for AutoCAD を使ったテストの前に、ローカル コンピュータ上で、アドインのロード、動作を事前にテストすることをお勧めしています。</p>
<hr />
<p><strong>AppPackage bundle（App パッケージ バンドル）のロードテスト</strong></p>
<p style="padding-left: 40px;">App パッケージ バンドルは、ZIP 圧縮前のフォルダ構造を特定の位置に配置することで、AutoCAD に自動認識させることが出来ます。<strong><a href="https://knowledge.autodesk.com/ja/search-result/caas/CloudHelp/cloudhelp/2019/JPN/AutoCAD-Customization/files/GUID-BC76355D-682B-46ED-B9B7-66C95EEF2BD0-htm.html" rel="noopener" target="_blank">PackageContents.xml ファイル</a> </strong>に含まれるアドインがのサポート バージョンやプラットフォームが一致した場合、自動ロードする<strong><a href="http://help.autodesk.com/view/ACD/2020/JPN/?guid=GUID-5E50A846-C80B-4FFD-8DD3-C20B22098008" rel="noopener" target="_blank">自動ローダー</a> </strong>メカニズムを利用して、クラウド上のコアエンジンにアドイン アプリケーションをロードします。</p>
<p style="padding-left: 40px;">このため、ZIP 圧縮前の状態であれば、通常のパッケージ バンドルとして <strong>AutoCAD</strong> で自動ロードをテストすることが出来ます。このテストで、パッケージ バンドルの PackageContents.xml ファイル の内容が正しいか否かをチェックすることが出来ます。残念ながら、AutoCAD に同梱されている <strong>AcCoreConsole.exe 自体はパッケージ バンドルを認識しない</strong>ため、<span style="text-decoration: underline;">AcCoreConsole.exe で自動ローダーのテストをおこなうことは出来ません</span>。</p>
<p><strong>AcCoreConsole.exe へのロードテスト</strong></p>
<p style="padding-left: 40px;">AcCoreConsole.exe で自動ローダーのテストは出来ませんが、App パッケージ バンドルで指定したアドイン アプリケーション ファイルを手動でロード テストすることは出来ます。ここで利用するのは、<strong><a href="http://help.autodesk.com/view/ACD/2020/JPN/?guid=GUID-D954BCC1-C4F0-488B-8F60-1D02D68940E0" rel="noopener" target="_blank">NETLOAD コマンド</a></strong>（.NET API の .dll ファイル）、<strong><a href="http://help.autodesk.com/view/ACD/2020/JPN/?guid=GUID-47621BB1-F29D-4A69-9C99-A6E1495FBA38" rel="noopener" target="_blank">APPLOAD コマンド</a></strong>（ObjectARX の .crx ファイル、AutoLISP の .lsp ファイル）です。最近の AutoCAD に同梱される AcCoreConsole.exe では、システム変数 FILEDIA が 0（ゼロ）に設定されて、ファイル ダイアログが表示されないようになっています。アドイン ファイルをロード指定する際には、フルパスを入力、指定してください。</p>
<p style="padding-left: 40px;">もし、手動でのロードが出来ない場合には、次の点をご確認ください。</p>
<ul>
<li>アドイン ファイルが AutoCAD の「<strong><a href="http://help.autodesk.com/view/ACD/2020/JPN/?guid=GUID-8A82DF69-D460-4B40-B3C2-BFEAD4D6A93C" rel="noopener" target="_blank">信頼する場所</a></strong>」に指定されたいずれかのパスに配置されていること。</li>
<li>アドインが .NET API を利用している場合、AcCoreMgd.dll と Acdbmgd.dll のみをアセンブリ参照していること。</li>
<li>アドインがターゲットとしている AutoCAD バージョンに同梱されている AcCoreConsole.exe であること。</li>
</ul>
<p><strong>AcCoreConsole.exe でのアドイン実行テスト</strong></p>
<p style="padding-left: 40px;">カスタム アクティビティを利用する場合、実行単位はロードしたアドイン アプリケーションのカスタム コマンドやスクリプトになるはずです。もし、テストしている環境が Windows である場合、実行テストの際に Windows の管理者権限が必要になる場合があります。例えば、SAVE コマンドでインストール フォルダ上に生成された図面を保存する場合などです。</p>
<p style="padding-left: 40px;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a466bb26200c-pi" style="display: inline;"><img alt="Cmd_standard" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0240a466bb26200c image-full img-responsive" src="/assets/image_407313.jpg" title="Cmd_standard" /></a></p>
<p style="padding-left: 40px;">このケースでは、実行時に管理者権限がないと、SAVE コマンドはインストール フォルダに図面を保存することが出来ません。このような場面では、AcCoreConsole.exe を起動するコマンド プロンプトを「管理者として実行」するようにしてください。</p>
<p style="padding-left: 40px;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a466bb33200c-pi" style="display: inline;"><img alt="Cmd_as_admin" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0240a466bb33200c image-full img-responsive" src="/assets/image_108792.jpg" title="Cmd_as_admin" /></a></p>
<p style="padding-left: 40px;">カスタム コマンドの処理が中断してしまうようなら、エラーや警告を示すダイアログが表示するような処理になっていないか、再度、ご確認ください。</p>
<p><strong>パラメータ取得時のテスト</strong></p>
<p style="padding-left: 40px;">Design Automation API を使って App パッケージ バンドル内の AutoCAD アドインを実行する際には、AutoCAD 上でユーザ対話入力によって得ていた入力値を、なんらかの方法で渡す必要が出てくる場合があります。画面上で確認することが出来ないので、渡せる種類も数値や文字に限定されてしまうことになりますが、代替として、Design Automation API は JSON ファイルでパラメータを渡す方法をサポートしています。</p>
<p style="padding-left: 40px;">実際には WorkItem（ワークアイテム）の作成時にパラメータを指定することで、クラウドのアドイン作業領域に JSON ファイルが保存されます。パラメータを保存する JSON ファイル名は Actiibty（アクティビティ）登録時に決めておくことが出来るので、ローカル コンピュータの作業フォルダに、想定される JSON ファイルをダミーとして保存しておけば、AutoCAD や AcCoreConsole での実行時に JSON ファイルを読み込んでテストすることも可能です。</p>
<hr />
<p>一定程度、ローカル コンピュータ上での処理テストが終わっていれば、既存アドインの Design Automation API for AutoCAD への移植と実装も楽なはずです。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a46ea977200c-pi" style="display: inline;"><img alt="Addin_transition" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0240a46ea977200c image-full img-responsive" src="/assets/image_835511.jpg" title="Addin_transition" /></a></p>
<p>なお、<span style="background-color: #ffffff;">Design Autonation API&#0160; for AutoCAD（AcCoreConsole.exe）で実行させる AppBundle では、アドイン実装内で現在の図面を切り替える実装は許可、サポートされていませんのでご注意ください。この処理には、OPEN コマンドや NEW コマンドの実行、あるいは、アプリケーション実行コンテキストが必要な既存図面オープン、新規図面作成の API 実装が含まれます。また、この制限には、Database.ReadDwgFile メソッド/AcDbDatabase::readDwgFile() の使用も同様にサポートされていません。Design Automation API for AutoCAD（AcCoreConsole.exe）では、AcCoreConsole.exe /i オプションのみがサポートされています。</span></p>
<p>By Toshiaki Isezaki</p>
