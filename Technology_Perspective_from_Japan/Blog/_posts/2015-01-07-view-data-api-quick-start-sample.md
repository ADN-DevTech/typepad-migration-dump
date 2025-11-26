---
layout: "post"
title: "View & Data API クイック スタート サンプル"
date: "2015-01-07 01:00:54"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2015/01/view-data-api-quick-start-sample.html "
typepad_basename: "view-data-api-quick-start-sample"
typepad_status: "Publish"
---

<p>View and Data API の開発を始める場合、<a href="http://adndevblog.typepad.com/technology_perspective/2014/10/a360-view-data-service-api-startup-guide.html" target="_blank"><strong>A360 View and Data サービス API 利用の手引き</strong></a> で紹介した内容をトレースすることで、その手順を理解することが出来ます。この手順を実際に Step By Step にトレースすることが出来る <a href="https://github.com/Developer-Autodesk/LmvQuickStart" target="_blank"><strong>LmvQuickStart</strong></a>&#0160;サンプルが存在します。</p>
<p>このサンプルは、デベロッパ ポータル（http://developer.autodesk.com ）の&#0160;<a href="http://adndevblog.typepad.com/technology_perspective/2014/11/api-console-usage.html" target="_blank"><strong>API コンソール</strong></a>&#0160;と共に利用することで、アクセス トークンを用いてビジュアルに View and Data API を体験することが出来ます。今回は、このサンプルの利用方法をご紹介します。</p>
<p><a href="https://github.com/Developer-Autodesk/LmvQuickStart" target="_blank"><strong>こちら</strong> </a>から GitHub にポストされているソースコード一式を ZIP ファイル形式でダウンロードして任意の位置に展開すると、LmvQuickStart-master フォルダ直下に <strong>index.html</strong> ファイルが存在しているはずです。この index.html&#0160;ファイルが、サンプル本体です。WebGL をサポートする Web ブラウザで、このファイルを開いてみてください。</p>
<p>Step1 となっている部分では、View and Data API を使った 3D モデルや 2D 図面の表示と操作をサンプルで試すことが出来ます。<span style="color: #fdeee0; background-color: #c080ff;"><strong> Try </strong></span>&#0160;ボタンをクリックすると、別のページが開いてドロップダウン リストからサンプル ファイルとファイルに含まれる 3D モデルと 2D シートなどを任意に選択して表示することが出来ます。</p>
<p style="text-align: center;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d0b279a7970c-pi" style="display: inline;"><img alt="LmvQuickStart" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b8d0b279a7970c image-full img-responsive" src="/assets/image_238961.jpg" title="LmvQuickStart" /></a></p>
<p>Step1 と同じように独自のモデルを View and Data API で表示させる手順は、Step2 以降の手順でトレースすることが出来ます。 まず、Step2 では、View and Data API&#0160;で利用するアクセス トークンを得るために使用する Consumer Key とConsumer Secret の取得についての説明がなされています。</p>
<p>登録した Consumer Key とConsumer Secret が手元にあれば、<a href="http://adndevblog.typepad.com/technology_perspective/2014/11/api-console-usage.html" target="_blank"><strong>API コンソール</strong></a>からアクセス トークン（Access Token）を動的に取得して、LmvQuickStart サンプル上で流用できます。具体的な流れを動画にまとめてみましたので、参考までに確認してみてください。</p>
<p style="text-align: center;"><iframe allowfullscreen="" frameborder="0" height="281" src="http://www.youtube.com/embed/FvkOWB4XHn8?feature=oembed" width="500"></iframe>&#0160;&#0160;</p>
<p>Step9 では、アップロードしたファイルが外部参照ファイルを持っている場合の参照設定を施す部分なので、この動画では省略していますが、それ以外の手順は、実際にプログラム上で指定する内容と同じです。バケットとファイルの関係や、アップロードしたファイルをエンコードして URN を識別子している点、また、アップロードしたファイルをストリーミング配信用に変換（Translate）する点などに注意してください。実際の処理では任意ですが、アップロードしたファイルが変換処理に対応しているかは、Step6 の手順でチェックできます。</p>
<p>このサンプルでは、実際にアップロードしたファイルを表示することが出来ます。一度、一連の手順を確認してみてください。&#0160;</p>
<p>By Toshiaki Isezaki</p>
<p>&#0160;</p>
