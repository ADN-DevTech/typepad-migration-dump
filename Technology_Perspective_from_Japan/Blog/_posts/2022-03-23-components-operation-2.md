---
layout: "post"
title: "AutoCAD コンポーネント間運用の例 ～ その２"
date: "2022-03-23 00:07:20"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2022/03/components_operation-2.html "
typepad_basename: "components_operation-2"
typepad_status: "Publish"
---

<p><a href="https://adndevblog.typepad.com/technology_perspective/2013/03/migrate-autocad-api-addon-apps.html" rel="noopener" target="_blank">AutoCAD API を使ったアドオン アプリケーションの移植性</a> でもご案内したことがありますが、移植性の容易さから、特に明確な理由がない限り、プログラム開発を生業にしているプロフェッショナル開発者の方には、AutoCAD .NET API の利用をお勧めしています。決して、他の AutoCAD API が無くなってしまうとか、使用をお勧めしない、とかいうことではありません。それだけ、AutoCAD .NET API が高機能で柔軟性を備え、メンテナンスコストのも優れている、という意味です。</p>
<p>ただ、難易度が高い ObjectARX には、.NET API にない高度な機能が存在するのも事実です。よく説明で触れるのは、ObjectARX でしか定義することが出来ない<strong>カスタム オブジェクト</strong>です。これ以外にも、ObjectARX がサポートしていて、AutoCAD .NET API でサポート出来ていないものがあります。キーボードの特定キー押下やマウスボタンのクリックを検出する <strong>OS レベルのイベント処理</strong>です。</p>
<p>Windows などの&#0160; GUI ベースの OS が、<a href="https://ja.wikipedia.org/wiki/%E3%82%A4%E3%83%99%E3%83%B3%E3%83%88_(%E3%83%97%E3%83%AD%E3%82%B0%E3%83%A9%E3%83%9F%E3%83%B3%E3%82%B0)#%E3%82%A4%E3%83%99%E3%83%B3%E3%83%88%E3%83%89%E3%83%AA%E3%83%96%E3%83%B3" rel="noopener" target="_blank">イベント ドリブン（event driven）</a>な処理体系を持っているのはご存じと思います。例えば、Windows の使用中には、常にイベントの発生を知らせるメッセージが各アプリケーションに通知されています。アプリケーションは、特定のメッセージに反応して各種処理をしています。ウィンドウ上でマウスの左ボタンをクリックしたか、右ボタンをクリックしたかも、異なるメッセージで通知してくれます。開発用の監視ツールでは、このようなメッセージを監視することも出来ます。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02942f9c30cc200c-pi" style="display: inline;"><img alt="Spy++" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02942f9c30cc200c image-full img-responsive" src="/assets/image_16026.jpg" title="Spy++" /></a></p>
<p>AutoCAD 上で OS レベルのイベント メッセージを処理するイベント ハンドラを登録する ObjectARX 関数に、メッセージ監視の開始をトリガする acedRegisterFilterWinMsg() 関数と、メッセージ監視を終了する acedRemoveFilterWinMsg() 関数が用意されています。</p>
<p>これらを使用すると、ZOOM や PAN といったコマンド操作を抑止しながら、長尺帳票を扱うようなことが出来るようになります。（この例では列車ダイヤ図）</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0282e14715de200b-pi" style="display: inline;"><img alt="Model-layout" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0282e14715de200b image-full img-responsive" src="/assets/image_610965.jpg" title="Model-layout" /></a></p>
<p>具体的には、モデル空間の長尺な図形情報を、レイアウト上に用意した 3 つの異なる浮動ビューポートでモデル空間の図形領域を特定サイズで表示、メインの浮動ビューポート上で左ボタンを押しながら移動させたマウスの位置を取得して、他の 2 つの浮動ビューポートの表示位置をリアルタイムに変化させることが可能になります。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0278806e729b200d-pi" style="display: inline;"><img alt="Daiya" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0278806e729b200d image-full img-responsive" src="/assets/image_463955.jpg" title="Daiya" /></a></p>
<p>AutoCAD .NET API では、&#0160;作図領域上にあるクロスヘアカーソル位置の座標は、InputPointMonitor イベントで取得することが出来ます。</p>
<p style="padding-left: 40px;"><a href="https://www.autodesk.co.jp/support/technical/article/caas/tsarticles/ts/4kX7QLvQEZAirtj3lJcTkH.html" rel="noopener" target="_blank">AutoCAD .NET API ：クロスヘア カーソル位置座標の取得</a></p>
<p>ただ、マウス ボタンやキーボード キーのイベントを同時に処理することは出来ないため、ObjectARX の acedRegisterFilterWinMsg() 関数と acedRemoveFilterWinMsg() 関数を利用出来れば便利です。</p>
<p>このような<span style="text-decoration: underline;">やむを得ない場面</span>では、AutoCAD のインストール フォルダから ObjectARX 関数をエクスポーズしている DLL ファイルを探し出し、<a href="https://docs.microsoft.com/ja-jp/dotnet/standard/native-interop/pinvoke" rel="noopener" target="_blank"><strong>プラットフォーム呼び出し（P/Invoke）</strong></a>を使って .NET API コードで ObjectARX のグローバル関数を呼び出すことが可能です。</p>
<p>詳細は、下記の Autodesk Network Knowledge 記事を確認してみてください。</p>
<p style="padding-left: 40px;"><a href="https://www.autodesk.co.jp/support/technical/article/caas/tsarticles/ts/4cUowiK4MM8MhbjfBEaRxs.html" rel="noopener" target="_blank">AutoCAD .NET API ：ObjectARX 関数の P/Invoke</a></p>
<p style="padding-left: 40px;"><a href="https://www.autodesk.co.jp/support/technical/article/caas/tsarticles/ts/62m52YTUDjQQj0zLGWO9kT.html" rel="noopener" target="_blank">AutoCAD .NET API ：マウス左ボタン クリックの検出</a></p>
<p style="padding-left: 40px;"><a href="https://www.autodesk.co.jp/support/technical/article/caas/tsarticles/ts/31xpA0AUln8DKr4HaBKcFP.html" rel="noopener" target="_blank">AutoCAD .NET API ：キーボード ボタン押下の検出</a></p>
<p>ObjectARX は .NET Framework に管理されないアンマネージ コード、AutoCAD .NET API は .NET Framework 下のマネージ コードです。ガベージ コレクション等、メモリ管理に違いもありますので、あらゆるケースで期待した結果が保証されるものはありませんが、テストしてみる価値はあるかと思います。</p>
<p>By Toshiaki Isezaki</p>
