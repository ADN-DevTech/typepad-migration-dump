---
layout: "post"
title: "AutoCAD Web：AutoLISP 機能"
date: "2022-03-09 00:27:50"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2022/02/autocad-webautolisp-capabilities.html "
typepad_basename: "autocad-webautolisp-capabilities"
typepad_status: "Publish"
---

<p>着々と<a href="https://help.autodesk.com/view/ACADWEB/JPN/?guid=AutoCAD_Web_Help_Whats_new_html" rel="noopener" target="_blank">進化</a>し続けている <a href="https://web.autocad.com/" rel="noopener" target="_blank"><strong>AutoCAD Web アプリ</strong></a>、最近 AutoLISP 機能が追加されてましたので、簡単にご紹介しておきます。</p>
<p style="padding-left: 40px;"><span style="background-color: #ffff00;">※AutoCAD Web アプリの AutoLISP 機能の利用には、<a href="https://www.autodesk.co.jp/products/autocad/overview?term=1-YEAR&amp;tab=subscription#toolsets" rel="noopener" style="background-color: #ffff00;" target="_blank">AutoCAD Plus（AutoCAD including specialized toolsets）</a>サブスクリプションが必要になりました。</span></p>
<p>ここでいう AutoLISP 機能とは、デスクトップ版 AutoCAD で作成済の AutoLISP ファイル（.lsp）を実行する機能を指します。AutoCAD Web 自体に AutoLISP の編集やデバッグ機能はありません。また、実行出来る機能はプラットフォーム非依存な関数に限定されます。(vl-*) あるいは (vla-*) などの Windows 固有関数は使用出来ません。また、Web ブラウザをプラットフォームにするため、ポップアップにあたる DCL ファイルを使ったダイアログ表示も利用することが出来ませんが、コマンド&#0160; プロンプトを介したリスト操作によるオブジェクト作成、操作/編集や情報取得などが可能です。</p>
<p>あらかじめ用意した .lsp があれば、サインイン中のアカウントにアップロードして、開いたドキュメントにロード後、AutoLISP 内に定義したコマンドをコマンド プロンプトに入力して使用することが出来ます。アカウントにアップロードは毎回おこなう必要はありません。</p>
<p>例えば、選択したオブジェクトを TrueColor 色（RGB:81, 203, 61） に色変更する、SetTrueColor コマンドを定義した ChColor.lsp ファイルの実行手順は次のとおりです。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0278807393b5200d-pi" style="display: inline;"><img alt="Web_AutoLISP" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0278807393b5200d image-full img-responsive" src="/assets/image_392793.jpg" title="Web_AutoLISP" /></a></p>
<p>AutoCAD Web の「プロパティ」パネルでは、カラーインデックス色を 1（赤）～ 9（グレー）までしか指定できませんが、この AutoLISP は次のように DXF グループコード 420 を直接リスト操作して、RGB:81, 203, 61 を指定することは出来ます。</p>
<div>
<blockquote>
<div><span style="font-size: 10pt;">(defun TrueColor-make ( r g b / &#0160;)</span></div>
<div><span style="font-size: 10pt;">&#0160;(+ (lsh (fix r) 16)</span></div>
<div><span style="font-size: 10pt;">&#0160; &#0160; (lsh (fix g) 8)</span></div>
<div><span style="font-size: 10pt;">&#0160; &#0160; (fix b)</span></div>
<div><span style="font-size: 10pt;">&#0160;)</span></div>
<div><span style="font-size: 10pt;">)</span></div>
<div><span style="font-size: 10pt;">(defun C:STC (/)</span></div>
<div><span style="font-size: 10pt;">&#0160; (setq ename (entsel &quot;\nオブジェクトを選択:&quot;))</span></div>
<div><span style="font-size: 10pt;">&#0160; (setq datalist (entget (car ename)))</span></div>
<div><span style="font-size: 10pt;">&#0160; (setq oldcolor (assoc 420 datalist))</span></div>
<div><span style="font-size: 10pt;">&#0160; (setq newcolor (cons 420 (TrueColor-make 81 203 61)))</span></div>
<div><span style="font-size: 10pt;">&#0160; (if (= oldcolor nil)</span></div>
<div><span style="font-size: 10pt;">&#0160; &#0160; (setq newlist (append datalist (list newcolor)))</span></div>
<div><span style="font-size: 10pt;">&#0160; &#0160; (setq newlist (subst newcolor oldcolor datalist))</span></div>
<div><span style="font-size: 10pt;">&#0160; )</span></div>
<div><span style="font-size: 10pt;">&#0160; (entmod newlist)</span></div>
<div><span style="font-size: 10pt;">&#0160; (princ)</span></div>
<div><span style="font-size: 10pt;">)</span></div>
</blockquote>
</div>
<ul>
<li>AutoCAD Web のユーザ インタフェースから RGB 色を指定することは（今のところ）出来ません。</li>
</ul>
<p>AutoLISP 式は、コマンド&#0160; プロンプトに直接入力して、内容を評価することも可能です。次画面では、オブジェクトを選択して拡張エンティティデータをリストから確認しています。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0282e1472041200b-pi" style="display: inline;"><img alt="Xdata" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0282e1472041200b image-full img-responsive" src="/assets/image_129022.jpg" title="Xdata" /></a></p>
<p>定義したコマンドをボタンなどのユーザ インタフェースに割り当てることは出来ませんが、「LISP を管理」から、ロードを自動化設定することは可能です。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02942f9c3fb6200c-pi" style="display: inline;"><img alt="Autoload" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02942f9c3fb6200c image-full img-responsive" src="/assets/image_794808.jpg" title="Autoload" /></a></p>
<p>初期実装ではありますが、ぜひ、お試しください。</p>
<ul>
<li>オートデスクでクラウドと API というと「<a href="https://adndevblog.typepad.com/technology_perspective/2018/05/about-autodesk-forge.html" rel="noopener" target="_blank">Forge</a>」を思い浮かべる方もいらっしゃるかと思いますが、ここでご紹介した AutoCAD Web と AutoLISP の関係は、あくまで AutoCAD Web の機能拡張であって Forge とは特に関係ありません。</li>
</ul>
<p>By Toshiaki Isezaki</p>
