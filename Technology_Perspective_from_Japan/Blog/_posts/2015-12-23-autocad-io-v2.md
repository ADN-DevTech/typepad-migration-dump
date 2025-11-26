---
layout: "post"
title: "AutoCAD I/O V2"
date: "2015-12-23 01:57:38"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2015/12/autocad-io-v2.html "
typepad_basename: "autocad-io-v2"
typepad_status: "Publish"
---

<p><span style="background-color: #ffff00;">ご注意：AutoCAD I/O&#0160;は2016年6月に Design Automation API &#0160;に名称変更されました。</span></p>
<p>このブログ上では、<a href="http://adndevblog.typepad.com/technology_perspective/2014/12/autocad-io-web-service.html" target="_blank">A<strong>utoCAD I/O Web サービス</strong></a>、<strong><a href="http://adndevblog.typepad.com/technology_perspective/2015/02/autocad-io-sample.html" target="_blank">AutoCAD I/O サンプル</a></strong>、の各タイトルで、ちょうど1年くらい前に AutoCAD I/O についてご紹介しています。今回、API のバージョンが V2 に上がりましたので、改めて AutoCAD I/O の仕組みと最新のサンプルをご案内したいと思います。バージョンアップといっても、日本ではまだ利用されている方も少ないので、まだ AutoCAD I/O 自体を知ってただく必要があるのです。</p>
<p>AutoCAD I/O とは、一言で説明するなら「<strong>クラウド上で動作する AutoCAD</strong>」です。実際に動作するのは、ユーザインタフェースを持ち、図面を表示する AutoCAD（acad.exe）ではなく、ユーザインタフェースを持たないコアモジュール <strong>accoreconsle.exe</strong> です。accoreconsole.exe についても過去のブログ記事 <strong><a href="http://adndevblog.typepad.com/technology_perspective/2013/06/console-version-of-autocad.html">コンソール バージョンの AutoCAD</a>&#0160;</strong>でご紹介していますが、このコアモジュールは現在でも AutoCAD とともにインストールされているので、単体でテストすることが可能です。</p>
<p>ユーザインタフェースを持たない AutoCAD、accoreconsle.exe も ObjectARX や AutoCAD .NET API で作成されたアドイン アプリケーションをロードして実行することが出来ます。唯一の制限は、accoreconsole.exe で動作させるアドイン アプリケーションには、ダイアログ ボックスやポップアップ ウィンドウなど、一切のユーザ インタフェースを実装できない点です。あくまで、コマンド プロンプトで動作する内容をアドイン化する必要があります。</p>
<p>AutoCAD I/O は、一般ユーザが利用するサービスではなく、開発者向けの実行環境です。独自に開発したアドイン アプリケーションをクラウド上にアップロードしてバッチ処理で DWG ファイルを生成したり、既存のストレージから AutoCAD I/O の動作環境に DWG ファイルをダウンロードして、図面を修正、変更して、指定したストレージにアップロード（保存）することが出来ます。</p>
<p>アドイン アプリケーションやパラメータを AutoCAD I/O に受け渡す処理には、<a href="https://en.wikipedia.org/wiki/Open_Data_Protocol" target="_blank"><strong>Open Data Protocol</strong> (<strong>OData</strong>)</a> と呼ばれるプロトコルを使用します。AutoCAD I/O で OData を利用する際には、次の概念を理解する必要があります。</p>
<ul>
<li><strong>Activity（アクティビティ）</strong><br />AutoCAD I/O に処理させるジョブで、後述の WorkItem に関連付けられて指定されます。<br /><br /></li>
<li><strong>WorkItem（ワークアイテム）</strong><br />関連付けされた Activity の内容によって、入力と出力のパラメータを指定することが出来ます。パラメータには、通常、編集対象の既存 DWG ファイルの入手先 URLと、編集後に DWG ファイルを保存するための URL を指定します。<br /><br /></li>
<li><strong>AppPackage（App パッケージ）</strong><br />AutoCAD I/O に渡して、実行させるアドイン アプリケーションです。ObjectARX か .NET API で作成されて、自動ローダーでロードさせる仕組みも提供する必要があります。<strong><a href="http://help.autodesk.com/view/ACD/2016/JPN/?guid=GUID-5E50A846-C80B-4FFD-8DD3-C20B22098008" target="_blank">自動ローダー</a></strong>とは、<strong><a href="http://help.autodesk.com/view/ACD/2016/JPN/?guid=GUID-BC76355D-682B-46ED-B9B7-66C95EEF2BD0" target="_blank">PackageContents.xml ファイル</a></strong>を利用して AutoCAD へのアドイン ロードを実行する仕組みです。&#0160;</li>
</ul>
<p>もちろん、AutoCAD I/O での処理をは一瞬で完了するわけではありません。クラウド上の AutoCAD I/O が Activity で指定した処理を完了したか否か、REST API を使った<strong><a href="https://ja.wikipedia.org/wiki/%E3%83%9D%E3%83%BC%E3%83%AA%E3%83%B3%E3%82%B0_%28%E6%83%85%E5%A0%B1%29" target="_blank">ポーリング</a></strong>でトラックする必要があります。</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb08a280b9970d-pi" style="display: inline;"><img alt="Autocad_io" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01bb08a280b9970d image-full img-responsive" src="/assets/image_10343.jpg" title="Autocad_io" /></a></p>
<p>さて、<strong><a href="http://www.jigsawify.com/" target="_blank">http://www.jigsawify.com/</a></strong>&#0160;から利用することが出来る、AutoCAD I/O を使った <strong>Jigsawify</strong> というユニークなサンプルがあります。</p>
<p>Jigsawify は、ページ中央にドラッグ&amp;ドロップしたラスター画像ファイルから輪郭を抽出、新規生成した図面ファイル上に貼り付け、指定サイズでジグソーパズルを作成します。作成後の図面は、DWG ファイルか DXF ファイルとしてダウンロードすることが出来ます。例えば、TrustedDWG のアイコンを指定すると、次のような DWG ファイルを生成することが出来ます。パズルの各ピースを作成している部分も含め、CRX アプリケーションが処理しています。</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c7fdf922970b-pi" style="display: inline;"><img alt="Jigsawify" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b7c7fdf922970b image-full img-responsive" src="/assets/image_389457.jpg" title="Jigsawify" /></a></p>
<p>Jigsawify はオートデスクの &#0160;Kean Walmsley が作成したもので、彼のブログ <a href="http://through-the-interface.typepad.com/through_the_interface/2015/07/jigsawifycom-creating-custom-jigsaw-puzzles-using-autocad-io.html" target="_blank">Through the Interface</a>&#0160;で詳細に説明されています。Kean は、現在、オートデスクで Software Architect として働いていますが、以前 、ADN の開発サポート チームに在籍したこともあり、何度も来日していたので、ご存じの方もいらっしゃるかと思います。</p>
<p>彼は AutoCAD API&#0160;の大家でもあり、彼のブログにも開発のヒントになる沢山のサンプルが記載されています。ぜひ、いくつかのキーワードでインターネット検索してみてください。Jigsawify のソース コードも&#0160;<a href="https://github.com/KeanW/jigsawify" target="_blank"><strong>https://github.com/KeanW/jigsawify</strong> </a>ですべて公開されていますので、前述の概念がどのように実装されているか参考になるはずです。</p>
<p>By Toshiaki Isezaki&#0160;</p>
