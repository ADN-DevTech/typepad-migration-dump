---
layout: "post"
title: "Design Automation API for AutoCAD 概説"
date: "2019-06-19 01:57:39"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2019/06/design-automation-api-for-autocad.html "
typepad_basename: "design-automation-api-for-autocad"
typepad_status: "Publish"
---

<p>過去、このブログでも、何度か <strong>AutoCAD I/O</strong> についてご案内してきました。2016 年 6月の Forge の正式リリース時には、<strong>Design Automation API</strong> の名前に名称変更されています。まだ Public Beta という扱いですが、Design Automation API も v3（バージョン ３）になり、扱うことが出来るコアエンジンも、AutoCAD に加え、Revit、Inventor、3ds MAX の 4 種類に増加しています。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a45990c6200c-pi" style="display: inline;"><img alt="Forge_history" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0240a45990c6200c image-full img-responsive" src="/assets/image_908101.jpg" title="Forge_history" /></a></p>
<p>今回は、改めて Design Automation API for AutoCAD の目的や機能を振り返り、現時点で正式リリースされている <a href="https://forge.autodesk.com/en/docs/design-automation/v2/developers_guide/overview/" rel="noopener" target="_blank"><strong>Design Automation API for AutoCAD v2</strong></a> と、年末に予定されているされている <strong><a href="https://forge.autodesk.com/en/docs/design-automation/v3/developers_guide/overview/" rel="noopener" target="_blank">Design Automation API for AutoCAD v3</a></strong> を網羅する概要に触れ、理解を深めていただく機会としたい意向です。</p>
<hr />
<p><strong>歴史</strong></p>
<p style="padding-left: 40px;">現在の Design Automation API for AutoCAD は、もともと AutoCAD I/O&#0160; と呼ばれていたもので、オートデスクがクラウドに本腰を入れだした直後の 2013 年後半から 2014 年初頭にかけて実装されだした Web サービス API の 1 つです。当時は、AutoCAD I/O ではなく、まだ、AutoCAD Core Engine Service の名称で呼称されていました。</p>
<p style="padding-left: 40px;">その後、Forge の登場で Design Automation API&#0160; に変更され、2017 年秋の Forge DevCon Las Vegas で、同 API への Inventor、Revit、3ds Max エンジンの追加がアナウンスされた後、それらと区別する意味で、Design Automation API for AutoCAD の名称に落ち着いた経緯があります。現在ある Forge Platform API の中でも最古参の API ということになります。</p>
<p style="padding-left: 40px;"><strong>AutoCAD Core Engine Service</strong>(2014年) <br />&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160;&gt; <strong>AutoCAD I/O</strong>(2015～16年)&#0160;<br />&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &gt; <strong>Design Autimation API</strong> (2016～17年) <br />&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160;&#0160;&#0160;&#0160;&gt; <strong>Design Autimation API for AutoCAD</strong> (2017年～）&#0160;</p>
<hr />
<p><strong>目的</strong></p>
<p style="padding-left: 40px;">コアエンジンの種類に限らず、Design Automation API は、クラウド上で稼働するコアエンジン（v2 では AutoCAD のみ、v3 では AutoCAD、Inventor、Revit、3ds MAX）にアドイン（プラグイン） をロードさせて、反復タスクを自動化するバッチ処理を実現することを目的としています。</p>
<p style="padding-left: 40px;"><strong><a href="https://adndevblog.typepad.com/technology_perspective/2018/10/use-of-desktop-products-on-the-shared-server.html" rel="noopener" target="_blank">デスクトップ製品の共有サーバー（クラウド）利用について ~ アップデート</a></strong> で触れているとおり、クラウド上の仮想環境に AutoCAD や Inventor、Revit、3ds MAX などのデスクトップ製品をインストールして、独自に開発した Web ページなどを介して間接的に製品を利用する方法は許可されていないため、Web インタフェースと連携したシステムを構築する際には唯一、正式にサポートされた形態となります。</p>
<p style="padding-left: 40px;">Design Automation API for AutoCAD では、DWG ファイル ベースの図面生成や、既存図面の編集や情報抽出、あるいは、数量の拾い出しなどをバッチ処理させるのが一般的な利用方法になるかと思います。そのため、AutoCAD Core Engine Service の頃から、実現内容に大きな変化はありません。ちなみに、2013 年当時に想定されていたシナリオは次のとおりです。もちろん、これらは現在も有効です。</p>
<ul style="list-style-type: circle;">
<li>オンラインの DWG 設定システム（フォント、画層、寸法スタイルなど）があり、指定した設定値に基づく DWG ドキュメントをエンドユーザに提供したい。</li>
<li>非常に沢山の図面ファイルに対して、表題ブロックを定期的に更新したい。</li>
<li>設計プロジェクトの日報を毎日作成するために、プロジェクト内のすべての図面を印刷したい。</li>
<li>ScriptPro や他の市販スクリプトツールを使った DWG のバッチ処理用にサーバーがあり、サーバー設定やメインテナンスの労力で生産性が低下している。</li>
</ul>
<hr />
<p><strong>メカニズム</strong></p>
<p style="padding-left: 40px;">Design Automation API for AutoCAD で実際にクラウド上で稼働するコアエンジンとは、通常の Windows 版 AutoCAD にも同梱されている <strong>AcCoreConsole.exe</strong> です。</p>
<p style="padding-left: 40px;">AcCoreConsole.exe については<strong><a href="https://adndevblog.typepad.com/technology_perspective/2013/06/console-version-of-autocad.html" rel="noopener" target="_blank"> コンソール バージョンの AutoCAD</a></strong> で詳しくご案内していますので、必要に応じてご確認ください。重要なのは、AcCoreConsole.exe が通常の AutoCAD（<strong>acad.exe</strong>）から、ユーザ インタフェースの実装を削除した軽量版 AutoCAD であり、ダイアログ ボックスなどのユーザ インタフェースを表示しないのであれば、起動後に標準コマンドを実行することが出来る、という点です。また、同じく、ユーザ インタフェースを表示しないコマンドであれば、アドイン アプリケーションに実装したカスタム コマンドも実行可能です。</p>
<p style="padding-left: 40px;">AcCoreConsole.exe は軽量版 AutoCAD であるので、その利用には AutoCAD をある程度理解しておく必要があります。実際、独自のバッチ処理を実装したい場合にも、アドイン アプリケーションでカスタム アプリケーションを開発する方法の他、AutoCAD がもともと持っている標準コマンドを組み合わせて、<strong><a href="http://help.autodesk.com/view/ACD/2020/JPN/?guid=GUID-95BB6824-0700-4019-9672-E6B502659E9E" rel="noopener" target="_blank">スクリプト</a></strong> として実行させることも出来てしまいます。</p>
<p style="padding-left: 40px;">標準コマンドをスクリプトで実装する場合には、コマンド実行中に、ダイアログ ボックスが表示されないよう配慮する必要もあります。このため、目的のコマンド実行前に EXPERT などのシステム変数を設定したり、ハイフン（-）付きでのコマンド呼び出しを利用しなければなりません。また、標準コマンドが再定義されている点も考慮して、ピリオド（.）付きでのコマンド呼び出しも検討する必要があるかもしれません。AutoCAD API によるカスタム コマンド利用を考えていない場合は、次のオンラインヘルプが参考になるはずです。</p>
<p style="padding-left: 80px;"><strong><a href="http://help.autodesk.com/view/ACD/2020/JPN/?guid=GUID-A02E1B47-AED7-4308-A6A3-94368852C2D8" rel="noopener" target="_blank">概要 - ダイアログ ボックスとコマンド ラインを切り替える</a></strong></p>
<p style="padding-left: 80px;"><a href="http://help.autodesk.com/view/ACD/2020/JPN/?guid=GUID-E2B53E4D-28AE-4F6E-96E4-6FABE3FF823C" rel="noopener" target="_blank"><strong>REDEFINE[再定義] コマンド</strong></a></p>
<p style="padding-left: 40px;">Design Automation API for AutoCAD で実行させたい内容（アドイン アプリケーション、または、スクリプト）は、事前のローカル コンピュータにインストールした AutoCAD の AcCoreConsole.exe で事前にテストすることが出来ます。</p>
<hr />
<p><strong>概念と用語</strong></p>
<p style="padding-left: 40px;">Design Automation API を利用する際には、次の概念を理解する必要があります。</p>
<ul style="list-style-type: circle;">
<li><strong>Activity（アクティビティ）</strong><br />Design Automation API に処理させるジョブ（実行）単位で、後述の WorkItem に関連付けられて指定されます。</li>
<li><strong>WorkItem（ワークアイテム）</strong><br />Design Automation API で扱う処理単位で、関連付けされた Activity の内容を実行します。Activity によっては WorkItem 側で Activity 用の入力パラメータと出力パラメータを指定します。パラメータには、通常、編集対象の既存 DWG ファイルの入手先 URL と、編集後に DWG ファイルを保存するための URL を指定します。</li>
<li><strong>AppPackage（App パッケージ）</strong><br />Design Automation API に渡して、実行させるアドイン アプリケーションです。ObjectARX または .NET API、AutoLISP（除く Visual LISP 関数）で作成されて、自動ローダーでロード処理の詳細を提供する必要があります。<strong><a href="http://help.autodesk.com/view/ACD/2020/JPN/?guid=GUID-5E50A846-C80B-4FFD-8DD3-C20B22098008" rel="noopener" target="_blank">自動ローダー</a></strong>とは、<strong><a href="http://help.autodesk.com/view/ACD/2020/JPN/?guid=GUID-BC76355D-682B-46ED-B9B7-66C95EEF2BD0" rel="noopener" target="_blank">PackageContents.xml ファイル</a></strong>を利用して AutoCAD へのアドイン ロードを実行する仕組みです。</li>
<li><strong>Engine（エンジン）</strong><br />クラウド上で稼働し、WorkItem を処理する AcCoreConsole.exe です。エンジンには、AutoCAD バージョンに準じた AcCoreConsole.exe をエンジンバージョンとして指定して実行させることが出来ます。現時点で v2 で利用可能なエンジンは AutoCAD 2015 Core Engine(Longbow) ID:20.0、AutoCAD 2016 Core Engine(Maestro) ID:20.1、AutoCAD 2017 Core Engine(Nautilus)ID:21.0、AutoCAD 2018 Core Engine(Omega) ID:22.0、AutoCAD 2019 Core Engine(Pi) ID:23.0 、v3 では、Autodesk.AutoCAD+20_1（AutoCAD 2016）、Autodesk.AutoCAD+21（AutoCAD 2017）、Autodesk.AutoCAD+22（AutoCAD 2018）、Autodesk.AutoCAD+23（AutoCAD 2019）、Autodesk.AutoCAD+23_1（AutoCAD 2020）になっています。</li>
</ul>
<p style="padding-left: 40px;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a463fb77200c-pi" style="display: inline;"><img alt="Workflow" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0240a463fb77200c image-full img-responsive" src="/assets/image_795512.jpg" title="Workflow" /></a></p>
<hr />
<p><strong>手順と使用する endpoint</strong></p>
<p style="padding-left: 40px;">Design Automation API for AutoCAD の一般的なワークフローは、次のとおりです。PlotToPDF のような既定の Actvity を実行する場合には、1.、3.、4. の処理は不要です。</p>
<ol class="arabic simple">
<li>AppPackage に割り当てられ、Activity に関連付けられるアドイン アプリケーション、または、スクリプトを特定のクラウド&#0160; ストレージにアップロードする。</li>
<li>WorkItem で処理、参照されるファイルを指定のクラウド ストレージにアップロードする。</li>
<li><a class="reference external" href="https://forge.autodesk.com/en/docs/design-automation/v3/reference/http/appbundles-POST" rel="noopener" target="_blank">POST appbundles</a>&#0160;を使って新しい AppPackage を作成する。</li>
<li><a class="reference external" href="https://forge.autodesk.com/en/docs/design-automation/v3/reference/http/activities-POST" rel="noopener" target="_blank">POST activities</a> を使って Activity を作成する。</li>
<li><a class="reference external" href="https://forge.autodesk.com/en/docs/design-automation/v3/reference/http/workitems-POST" rel="noopener" target="_blank">POST workitems</a>&#0160;endpoint を使用して、Activity のジョブをコアエンジンに送信する。 WorkItem は、WorkItemに渡された Activity に関連付けられている AppPackages に定義されているアドイン アプリケーションやスクリプトの実行を開始します。</li>
<li><a class="reference external" href="https://forge.autodesk.com/en/docs/design-automation/v3/reference/http/workitems-id-GET" rel="noopener" target="_blank">GET workitems/:id</a>をポーリングして WorkItem の現在のステータスをチェックして、正常終了するか否かを確認する。</li>
<li>指定されたクラウド ストレージから処理されたファイルをダウンロードする。</li>
</ol>
<p style="padding-left: 40px;">※ 上記でリンクしている endpoint は v3 のものです。</p>
<hr />
<p><strong>よくある質問</strong></p>
<ul>
<li>Design Automation API for AutoCAD は、AcCoreConsole.exe のプロンプトのみを持つ CAD 製品なのでしょうか？</li>
<li>いいえ、Design Automation API for AutoCAD は開発者向けの API であり、エンドユーザ（設計者）が利用する CAD 製品ではありません。</li>
</ul>
<p>&#0160;</p>
<ul>
<li>Design Automation API for AutoCAD を購入、オンプレミス サーバーにインストールして利用することは出来ますか？</li>
<li>いいえ、他の Forge Platform API と同様に、オートデスクが AWS 上に構築したクラウドの利用が前提となります。オンプレミス化は出来ません。また、Forge はクラウド クレジットによる重量課金制をとるサブスクリプション モデルを採用しています。利用権を永久に買い取ることは出来ません。</li>
</ul>
<p>&#0160;</p>
<ul>
<li>Design Automation API for AutoCAD を利用すると、AutoCAD Web（<a href="https://web.autocad.com/">https://web.autocad.com</a>）のカスタマイズが出来るのですか？</li>
<li>いいえ、Design Automation API for AutoCAD は、オートデスクはクラウド上に用意した AutoCAD コアエンジンに、3rd party の開発者が開発した AutoCAD 用のアドイン アプリケーションをロード、実行さて、バッチ処理をおこなうものです。AutoCAD Web のカスタマイズや同等のクラウド サービスを構築する API ではありません。</li>
</ul>
<p>&#0160;</p>
<ul>
<li>Design Automation API for AutoCAD で処理する DWG ファイルの入力や、アドインで処理された DWG ファイルの出力にオートデスク以外のクラウド ストレージを利用することは出来ますか？</li>
<li>はい、当該ストレージ サービスが、3-legged 認証/認可をサポートしていれば可能なす。ローカル コンピュータを出力先として指定することは出来ません。</li>
</ul>
<p>&#0160;</p>
<ul>
<li>Design Automation API for AutoCAD で AutoCAD Mechanical や AutoCAD Map 3D&#0160; など、AutoCAD ベースの業種別製品で作成したデータや機能を扱うことは出来ますか？</li>
<li>AutoCAD ベースの業種別製品で作成したカスタム オブジェクトは、オブジェクト イネーブラを使って認識することが出来ます。ただし、同カスタム オブジェクトを新規に作成したり、編集することは出来ません。利用可能な機能は、あくまで AutoCAD 単体の標準機能が対象となります。ただし、一部、Bing Map を利用する機能など、オートデスクが利用する外部コンポーネントのライセンス上、利用出来ないものがありますので、事前にテストしていただくことをお勧めしています。AutoCAD Civil 3D コンポーネントは Design Automation API for AutoCAD で <a href="https://adndevblog.typepad.com/technology_perspective/2020/07/design-automation-for-civil-3d-public-beta.html" rel="noopener" target="_blank">Beta 実装</a>しています。</li>
</ul>
<hr />
<p>Design Automation API for AutoCAD v2 についても言及してきましたが、Design Automation API for AutoCAD v3 では、異なる endpoint を利用することになるため、残念ながら、いずれかのバージョンで作成した Forge アプリの相互互換性はありませんのでご注意ください。ただ、基本的な考え方は踏襲しているので、目的や概念、用語は一緒です。また、<span style="background-color: #ffffff;">これから Design Automation API for AutoCAD の利用を検討されている場合には、Design Automation API for AutoCAD v3 の使用を<strong>強く</strong>お勧めしています。今年末を予定している v3 の正式リリース後、しばらくの移行期間を経て、v2 の閉鎖を検討しているためです。&#0160;</span></p>
<p><span style="background-color: #ffffff;">Design Autonation API&#0160; for AutoCAD（AcCoreConsole.exe）で実行させる AppBundle では、アドイン実装内で現在の図面を切り替える実装は許可、サポートされていませんのでご注意ください。この処理には、OPEN コマンドや NEW コマンドの実行、あるいは、アプリケーション実行コンテキストが必要な既存図面オープン、新規図面作成の API 実装が含まれます。また、この制限には、Database.ReadDwgFile メソッド/AcDbDatabase::readDwgFile() の使用も同様にサポートされていません。Design Automation API for AutoCAD（AcCoreConsole.exe）では、AcCoreConsole.exe /i オプションのみがサポートされています。</span></p>
<p><span style="background-color: #ffff00;"><strong><a href="https://adndevblog.typepad.com/technology_perspective/2019/10/design-automation-api-v3-release.html" rel="noopener" style="background-color: #ffff00;" target="_blank">Design Automation API v3 は 2019年10月28日に正式リリースされました。</a></strong></span></p>
<p>By Toshiaki Isezaki</p>
