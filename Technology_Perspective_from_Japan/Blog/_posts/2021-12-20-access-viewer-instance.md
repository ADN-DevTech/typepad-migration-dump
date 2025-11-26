---
layout: "post"
title: "コンソールの利用と Viewer インスタンスへのアクセス"
date: "2021-12-20 00:01:24"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2021/12/access_viewer_instance.html "
typepad_basename: "access_viewer_instance"
typepad_status: "Publish"
---

<p>Forge Viewer を使った開発作業時、よく Viewer 情報を得たい場合があります。JavaScript コードを使ってメソッドやプロパティを呼び出すことも出来ますが、そういった手間すら省いて値を参照したいこともあるかと思います。</p>
<p>そのような場面では、JavaScript コード内と同じように、グローバル変数（プロパティ）を Web ブラウザの<strong><a href="https://adndevblog.typepad.com/technology_perspective/2018/10/about-developer-tool-on-web-browser.html" rel="noopener" target="_blank">デベロッパー ツール</a></strong>のデバッグ コンソールに入力して値を確認することが出来ます。例えば、<span style="font-family: &#39;comic sans ms&#39;, sans-serif;"><a href="https://forge.autodesk.com/en/docs/viewer/v7/reference/globals/LMV_VIEWER_VERSION/" rel="noopener" target="_blank">LMV_VIEWER_VERSION</a></span> を入力すると、Web ページで現在表示に使用している Viewer インスタンスのバージョンを表示させることが出来ます。</p>
<p>この表示には、自身で作成した Forge Viewer を持つ Web ページだけでなく、Autodesk Viewer のようなオンライン ビューアでも利用が可能です。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0282e1365813200b-pi" style="display: inline;"><img alt="LMV_VIEWER_VERSION" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0282e1365813200b image-full img-responsive" src="/assets/image_188331.jpg" title="LMV_VIEWER_VERSION" /></a></p>
<p>ドキュメント化されていないグローバル プロパティも存在します。Viewer インスタンスを格納する <strong>NOP_VIEWER</strong> です。例えば、デバッグ コンソールに <span style="font-family: &#39;comic sans ms&#39;, sans-serif;">NOP_VIEWER.getSelection()</span> と入力して現在選択中のオブジェクトの dbid 配列を表示させたり、 <span style="font-family: &#39;comic sans ms&#39;, sans-serif;">NOP_VIEWER.setTheme(&quot;dark-theme&quot;)</span> と入してテーマカラーを変更するようなことも可能です。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0282e136587d200b-pi" style="display: inline;"><img alt="NOP_VIEWER" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0282e136587d200b image-full img-responsive" src="/assets/image_300032.jpg" title="NOP_VIEWER" /></a></p>
<p>もちろん、変数への代入や参照も可能なので、AutoCAD のコマンド プロンプトに AutoLISP 式を入力して対話的に処理内容を確認するように、 Viewer インスタンスと対話しながら動作確認することが出来て便利です。</p>
<p>By Toshiaki Isezaki</p>
