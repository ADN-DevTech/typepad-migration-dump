---
layout: "post"
title: "Design Automation API for AutoCAD 利用の注意点"
date: "2020-12-14 00:51:01"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2020/12/notes-on-using-da4a.html "
typepad_basename: "notes-on-using-da4a"
typepad_status: "Publish"
---

<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0263e97dc663200b-pi" style="display: inline;"><img alt="Accoreconsole_da4a" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0263e97dc663200b image-full img-responsive" src="/assets/image_551200.jpg" title="Accoreconsole_da4a" /></a></p>
<p>AutoCAD 2013 以降、AutoCAD のインストール フォルダには、<strong>accoreconsole.exe</strong> という<a href="https://adndevblog.typepad.com/technology_perspective/2013/06/console-version-of-autocad.html" rel="noopener" target="_blank">コンソール版の AutoCAD</a> がインストールされています。現在では、Design Automation API for AutoCAD でコアエンジンとして使用されていて、同環境での実際の使用前にアドイン アプリケーションのテストで<a href="https://adndevblog.typepad.com/technology_perspective/2019/07/test-addin-on-accoreconsoleexe.html" rel="noopener" target="_blank">使用</a>することも出来ます。</p>
<p>AcCoreConsole.exe は UI のない AutoCAD という位置づけで説明することが多いのですが、いくつか制限もありますので、ここでご案内しておきたいと思います。</p>
<ul>
<li>&#0160;OPEN コマンドや NEW コマンドの実行、あるいは、<a href="https://adndevblog.typepad.com/technology_perspective/2013/11/understandin-command-context-on-autocad.html" rel="noopener" target="_blank">アプリケーション実行コンテキスト</a>が必要な既存図面オープン、新規図面作成の API 実装は未サポート。</li>
<li>&#0160;Database.ReadDwgFile メソッド/AcDbDatabase::readDwgFile() の使用は未サポート。図面のメモリ展開は、AcCoreConsole.exe /i オプションを推奨。（機能として動作しますが、Design Automation 環境ではログ出力の遷移で問題が報告されています。）</li>
<li>&#0160;PDF などの印刷処理では、AcCoreConsole.exe にロードしたアドインが新規に作成したレイアウトは、UI 表示時のレイアウト初期化処理が実行されないため、未サポート。ただし、初期化済のレイアウトはファイルへの「印刷」処理は可能。</li>
<li>ローカル環境で実行される AcCoreConsole.exe へのアドイン ロードでは自動ローダー（PackageContents.xml）は未サポート。</li>
<li>Design Automation API は、WorkItem 実行時に実行環境となる仮想マシン（AMI）を確立して、コアエンジンを実行し、WorkItem 終了時にシャットダウンするクラウド コンピューティング環境です。この際、仮想マシンに使用するのは英語版の Windows OS です。R12 DXF など、UNICODE 未対応データの読み込み、または、書き込みで問題が発生する場合があります。代替として、UNICODE に対応した 2007 形式以降の DWG/DXF ファイルの利用をお勧めします。</li>
</ul>
<p>上記制限（最後を除く）は、Design Automation 環境でも継承されることになります。加えて、次の点が Design Automation 使用時に追加されることとなります。</p>
<ul>
<li>素材ファイルのクラウド ストレージからのダウンロードと成果ファイルのクラウド ストレージへのアップロード時間を含め、Design Automation API for AutoCAD 上で動作する WorkItem の処理時間は、既定値が 100 秒、指定により、最大 900 秒を許容。この点は、Design Automation API for AutoCAD ドキュメントにも<a href="https://forge.autodesk.com/en/docs/design-automation/v3/developers_guide/rate-limits/da-rate-limits/" rel="noopener" target="_blank">記載されています</a>。</li>
<li>既定で使用可能な日本語 TrueType フォントは、MS ゴシック、MS P ゴシック、MS UI ゴシック MS 明朝、MS P 明朝、游 明朝、メイリオ、メイリオ UI のみです。</li>
<li>これ以外の TrueType フォントを使用する場合には、ApppBundle への TrueType フォントファイルの同梱と、TrueType フォント マップ&#0160; ファイル（.fmp）の実行環境への配置が必要です。ただし、この方法は Autodesk.AutoCAD+24 コアエンジン バージョン以降でのみ有効です。</li>
<li>Design Automation API の実行環境には、特定のソフトウェアやドライバのインストールは、残念ながら許可されていません。このため、Design Automation API でのパラメータ渡しには、JSON か、通常の I/O 処理で利用される XML や Text 形式のファイル利用を推奨/サポートしています。</li>
<li>Autodesk.AutoCAD+24 コアエンジン バージョン以降、AppBundle でアドイン ファイルの自動ロード定義に使用する PackageContents.xml において、ロード タイミングを起動時ロードを指定する LoadOnAutoCADStartup にしていると、パフォーマンス低下抑止のため、コアエンジンへのロードが実行されません。代替として、コマンド呼び出し時にアドインをロードさせる LoadOnCommandInvocation をお使いください。</li>
<li>DWG/DXF ファイルの処理時には、図面破損の無いことが前提となります。デスクトップ版 AutoCAD で開いた際に AUDIT コマンドでエラーが表示されたり、非 TrustedDWG がレポートされる場合、WorkItem 実行時にエラー中断する場合があります。</li>
</ul>
<p>By Toshiaki Isezaki</p>
