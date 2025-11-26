---
layout: "post"
title: "AutoCAD 雑学：コマンドと AutoCAD API"
date: "2021-08-04 00:03:58"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2021/08/autocad-trivia-command-and-autocad-api.html "
typepad_basename: "autocad-trivia-command-and-autocad-api"
typepad_status: "Publish"
---

<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b026bdee382a5200c-pi" style="display: inline;"><img alt="Title" border="0" class="asset  asset-image at-xid-6a0167607c2431970b026bdee382a5200c image-full img-responsive" src="/assets/image_193048.jpg" title="Title" /></a></p>
<p>今回は意外に奥の深い AutoCAD の[「コマンド」についてご紹介しましょう。</p>
<p>AutoCAD は汎用 CAD ソフトウェアとして、さまざまな業種で利用されています。これを可能にしているのが、業種や環境にあわせて AutoCAD をカスタマイズする <a href="https://adndevblog.typepad.com/technology_perspective/2021/02/autocad-features-which-are-not-available-on-autocad-lt-api.html" rel="noopener" target="_blank"><strong>API</strong>（<strong>A</strong>pplication <strong>P</strong>rogramming <strong>I</strong>nterface）</a> の存在です。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b026bdee2c6c0200c-pi" style="display: inline;"><img alt="Api" border="0" class="asset  asset-image at-xid-6a0167607c2431970b026bdee2c6c0200c image-full img-responsive" src="/assets/image_610709.jpg" title="Api" /></a></p>
<p>一般的な AutoCAD API 利用の目的は、AutoCAD の機能拡張モジュールであるアドイン（別名：アドオン、プラグイン） アプリケーションを作成して、独自のカスタム コマンドを作成することにあります。AutoCAD には、CAD ソフトウェアに必要な標準コマンドが数多く用意されていますが、あくまで汎用的な作図やモデリング、編集、集計、管理に必要なコマンド群になってしまいます。そこをカバーするのが、カスタム コマンドという訳です。</p>
<p>このカスタム コマンドですが、AutoCAD API の導入時期によって、大きく分けて 2 種類のタイプがあります。最も古い AutoLISP 方式で登録する <strong>AutoLISP コマンド（Defun コマンド）</strong>と、AuroCAD R13 で導入された<strong>ネイティブ コマンド</strong>です。「ネイティブ」と呼ばれるのは、現在、ほとんどの標準コマンドがネイティブ コマンドとして定義されているためです。</p>
<p>AutoCAD API でカスタム コマンドを定義出来るのは、ObjectARX、AutoCAD .NET API 、AutoLISP と JavaScript API の&#0160; 4 API です。残念ながら、ActiveX オートメーション（COM API）ではカスタム コマンドを定義することは出来ません。VBA 環境で VBA マクロを定義するのみです。（VBA マクロの実行には VBARUN コマンド、または、-VBARUN コマンドの実行が必要です。）なお、AutoLISP では、当初、ネイティブ コマンドは定義することが出来ませんでしたが、現在ではそれも可能です。</p>
<p>AutoLISP コマンドも、ネイティブ コマンドも、定義したアドインが <strong><a href="https://adndevblog.typepad.com/technology_perspective/2013/09/auto-loading-for-autocad-addon-apps.html" rel="noopener" target="_blank">AutoCAD にロード</a></strong>されていれば、リボン ボタンやツールバー ボタンに割り当ててボタンをクリックして実行したり、プロンプト ウィンドウにコマンド名を入力して実行することが出来ます。</p>
<p>さらにネイティブ コマンドでは、標準コマンドと同じく、AutoCAD のよるコマンド スタックと呼ばれるメカニズムで管理されることになります。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0282e1131d2c200b-pi" style="display: inline;"><img alt="Command_stack" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0282e1131d2c200b image-full img-responsive" src="/assets/image_568624.jpg" title="Command_stack" /></a></p>
<p>コマンド スタックでは、最後にロードしたアドインの定義コマンドが上位になり、コマンドの実行時には、入力されたコマンド名をスタックの上位から検索、最初に見つかったコマンドを実行します。この場合、異なるアドインに同じコマンド名が定義されていると、下位のアドインの定義コマンドは実行出来なくなってしまう恐れがあります。</p>
<p>このような状況を極力避ける目的で、ネイティブ コマンドを定義する際には、コマンドにグループ名を同時に指定することが推奨されています。コマンド グループがわかっていれば、コマンド グループ名＋ピリオド(<strong>.</strong>) ＋コマンド名のかたちでコマンドを入力することも可能です。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0278803aa8a7200d-pi" style="display: inline;"><img alt="Group_command" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0278803aa8a7200d img-responsive" src="/assets/image_668153.jpg" title="Group_command" /></a></p>
<p>さて、ネイティブ コマンド タイプで定義したカスタム コマンドの利点は何でしょう？</p>
<p>答えは、多様なコマンドの振る舞いを指定出来る点です。ネイティブ&#0160; コマンド定義では、<strong>コマンド フラグ</strong>を指定することで、他のコマンドの実行中に ’（シングルクォーテーション）＋コマンド名 の入力でコマンドを割り込ませる（別名：割り込みコマンド）&#0160;<strong>Transparent フラグ&#0160;</strong>や、レイアウト（ペーパー空間）がアクティブの時だけ実行出来る <strong>NoTileMode フラグ</strong>、コマンドの実行前に選択しておいたオブジェクト選択を利用する <strong>UsePickSet フラグ</strong>など、<a href="https://help.autodesk.com/view/OARX/2022/JPN/?guid=GUID-F77E8FE0-8034-4704-93BD-F717608F8223" rel="noopener" target="_blank"><strong>多彩な指定</strong></a>をおこなうことが出来ます。</p>
<p>また、AutoCAD の特性であるコマンド実行コンテキストの指定も、ネイティブ コマンド定義時にコマンド フラグで指定します。コマンド実行コンテキストは AutoCAD カスタマイズでは重要な要素なので、次のブログ記事で詳しくご紹介しています。</p>
<p style="padding-left: 40px;"><a href="https://adndevblog.typepad.com/technology_perspective/2013/11/understandin-command-context-on-autocad.html" rel="noopener" target="_blank"><strong>AutoCAD API が持つコマンド実行コンテキストの理解</strong></a></p>
<p>ネイティブ コマンドも良い事づくめではありません。ネイティブ コマンドとして定義したカスタム コマンドの中で、別のネイティブ コマンドを呼び出す場合、コマンド呼び出しの深さ（ネスト）に制限がかかります。AutoLISP コマンドの利点（回避策として）も含め、次の Autodesk Knowledge Network 記事も確認してみてください。</p>
<p style="padding-left: 40px;"><a href="https://www.autodesk.co.jp/support/technical/article/caas/tsarticles/ts/2bpArQmrEpepaaufCsKIHU.html" rel="noopener" target="_blank"><strong>「コマンドは 4 レベルより深くネストできません。」エラーについて</strong></a></p>
<p>もちろん、AutoLISP コマンドの利点には AutoLISP 関数を定義出来る、という点もあります。（「コマンド」ではありませんが。）</p>
<p>By Toshiaki Isezaki</p>
