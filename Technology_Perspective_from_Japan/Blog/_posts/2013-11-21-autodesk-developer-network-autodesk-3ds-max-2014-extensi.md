---
layout: "post"
title: "3ds Max 2014 Pythonスクリプティング"
date: "2013-11-21 16:27:49"
author: "Akira Kudo"
categories: []
original_url: "https://adndevblog.typepad.com/technology_perspective/2013/11/autodesk-developer-network%E3%81%AE%E5%B7%A5%E8%97%A4-%E6%9A%81%E3%81%A7%E3%81%99%E4%BB%8A%E5%9B%9E%E3%81%AF%E5%85%88%E6%97%A5%E3%83%AA%E3%83%AA%E3%83%BC%E3%82%B9%E3%81%95%E3%82%8C%E3%81%BE%E3%81%97%E3%81%9Fautodesk-3ds-max-2014-extensi.html "
typepad_basename: "autodesk-developer-networkの工藤-暁です今回は先日リリースされましたautodesk-3ds-max-2014-extensi"
typepad_status: "Publish"
---

<p style="text-align: left;">Autodesk Developer Networkの工藤　暁です。今回は先日リリースされましたAutodesk® 3ds Max® 2014 Extension新機能の一つであるPythonスクリプティングについて補足させて頂きます。</p>
<p style="text-align: left;">今回のPythonスクリプティング実装に際し、MAXScriptの今後を御心配戴いているお客様もいらっしゃいますが、このPythonサポート が MAXScript に取って代わるものではありません。PythonがMAXScriptより優れている下記の様な作業において選択肢が増えただけです。</p>
<ul>
<li>OSとの連携</li>
<li>PyQt 又は PySideを使用した洗練されたUIの使用</li>
<li>NumPyや PyCUDAを使用したハイパフォーマンスな計算処理</li>
<li>インターネット接続: アップロード/ダウンロード</li>
<li>その他</li>
</ul>
<p style="text-align: left;">又、下記の様な共通機能へのアクセスを提供する“MaxPlus”と呼ばれるAPIモジュールを提供します。シーングラフの走査</p>
<ul>
<li>オブジェクトの作成</li>
<li>ファイルのオープン / クローズ</li>
<li>メッシュの操作</li>
<li>パラメータの取得 / 設定</li>
<li>簡単なレンダリング</li>
</ul>
<p style="text-align: left;">私見ではありますが、カスタムプラグインも開発可能なMayaのPython実装よりも、バッチやユーティリティー機能による処理を行うMotionBuilderのPython実装に似ていると思います。これらのサンプルプログラム等につきましては先日御紹介しました<a href="http://adndevblog.typepad.com/technology_perspective/2013/10/autodesk-3ds-max-2014-extension-python%E3%82%B9%E3%82%AF%E3%83%AA%E3%83%97%E3%83%86%E3%82%A3%E3%83%B3%E3%82%B0%E7%92%B0%E5%A2%83%E3%81%AE%E5%BE%A1%E7%B4%B9%E4%BB%8B.html">ブログ</a>を御参照下さい。</p>
<p style="text-align: left;">残念ながら、初めての実装ということもあり以下の機能が現在存在しません。</p>
<ul>
<li>エディターやデバッガー、コンソール、マクロレコーダー</li>
<li>使用が限られたＡＰＩの公開</li>
<li>マルチスレッド対応</li>
<li>標準Pythonディストリビューション以外のディストリビューションモジュール</li>
</ul>
<p style="text-align: left;">ご意見ご希望等御座いましたら、お気軽に御問い合わせ下さい。</p>
<p style="text-align: left;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b019b017383b6970d-pi" style="display: inline;"><img alt="Test" border="0" class="asset  asset-image at-xid-6a0167607c2431970b019b017383b6970d image-full img-responsive" src="/assets/image_805530.jpg" title="Test" /></a><br /><br /><br /><br /></p>
