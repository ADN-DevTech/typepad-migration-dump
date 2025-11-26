---
layout: "post"
title: "Visual Studioを利用してInventor iLogicのデバッグ実行する方法"
date: "2022-01-12 00:04:27"
author: "Takehiro Kato"
categories:
  - "API カスタマイズ"
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2022/01/debugging-ilogic-with-visual-studio.html "
typepad_basename: "debugging-ilogic-with-visual-studio"
typepad_status: "Publish"
---

<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0282e13e2170200b-pi" style="display: inline;"><img alt="Title" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0282e13e2170200b image-full img-responsive" src="/assets/image_816689.jpg" title="Title" /></a></p>
<p>ご存知のように、InventorのiLogicエディタには、ステップ実行や変数の値を参照する等のデバッグを行うための機能が無いため、実行中のTraceログを出力するなどの間接的な方法以外にはiLogicをデバッグを行うことが出来ません。</p>
<p>このため、新規にiLogicを作成する場合や、運用中に発生したエラーの調査を行う場合等、どこで何が起きているのかの現象の確認、原因および修正方法の調査を行うのに、苦慮することがあるかと思います。</p>
<p>&#0160;</p>
<p>今回の記事では、Inventor iLogicをVisual Studioでデバッグ実行する方法をご紹介します。</p>
<p>&#0160;</p>
<p>前提条件：</p>
<ol>
<li>Inventor 2019以降 (Inventor 2018ではデバッグ実行をすることは出来ません）</li>
<li>Visual Studio 2017 または 2019 (Community、 Professional 、または Enterprise エディション。Visual Studio Codeは対象外)&#0160;</li>
</ol>
<p>&#0160;</p>
<p>設定方法：</p>
<p>この記事では、Visual Studio 2019 Professionalでの設定を例に説明をします（Visual Studio 2017でも多少のGUIの違いはありますが設定内容は同じです）。</p>
<p>&#0160; &#0160; &#0160;&#0160;&#0160;&#0160;1．Visual Studioを起動し、Visual Basic のクラスライブラリ(.NET Framework) プロジェクトを新規作成<a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02942f92780d200c-pi"><img alt="CreateVBProject" class="asset  asset-image at-xid-6a0167607c2431970b02942f92780d200c img-responsive" src="/assets/image_361076.jpg" style="display: block; margin-left: auto; margin-right: auto;" title="CreateVBProject" /></a></p>
<p>&#0160;&#0160;&#0160; &#0160; &#0160; 2．メニューから「デバッグ」- 「ウィンドウ」-「例外設定」を選択し、例外設定パネルで、「Common Language Runtime Exceptions」の設定をOnに変更<a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0282e13d5597200b-pi"><img alt="ExceptionSettingMenu" class="asset  asset-image at-xid-6a0167607c2431970b0282e13d5597200b img-responsive" src="/assets/image_805586.jpg" style="display: block; margin-left: auto; margin-right: auto;" title="ExceptionSettingMenu" /></a></p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02942f927831200c-pi"><img alt="ExceptionSetting" class="asset  asset-image at-xid-6a0167607c2431970b02942f927831200c img-responsive" src="/assets/image_932629.jpg" style="display: block; margin-left: auto; margin-right: auto;" title="ExceptionSetting" /></a><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0282e13d5597200b-pi" style="display: inline;"><br /></a></p>
<p>&#0160;&#0160;&#0160; &#0160;&#0160;&#0160;&#0160;3.メニューから「デバッグ」- 「オプション」を選択してオプションウィンドウを起動し以下の設定を変更</p>
<p>&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160;&#0160;オプションウィンドウの「デバッグ」-「全般」配下の「元のバージョンと完全に一致するソースファイルを必要とする」オプションのチェックを外す。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0282e13d55c2200b-pi"><img alt="RequireSouceFile" class="asset  asset-image at-xid-6a0167607c2431970b0282e13d55c2200b img-responsive" src="/assets/image_788943.jpg" style="display: block; margin-left: auto; margin-right: auto;" title="RequireSouceFile" /></a></p>
<p>&#0160;&#0160;&#0160; &#0160;&#0160;&#0160;&#0160;「従来のC#およびVBの式エバリュエータを使用する」をチェック</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02942f927854200c-pi"><img alt="UseCSVBEvaluator" class="asset  asset-image at-xid-6a0167607c2431970b02942f927854200c img-responsive" src="/assets/image_463109.jpg" style="display: block; margin-left: auto; margin-right: auto;" title="UseCSVBEvaluator" /></a></p>
<p>&#0160;</p>
<p>&#0160;</p>
<p>デバッグ実行：</p>
<p>&#0160;&#0160;&#0160; &#0160;&#0160;&#0160;&#0160;1．Inventorを起動し、iLogicを含むInventorファイルを開く</p>
<p>&#0160;&#0160;&#0160; &#0160;&#0160;&#0160;&#0160;2．Visual Studioのメニューから、「デバッグ」-「プロセスにアタッチ」を選択し、アタッチ先に「自動マネージド(v4.6、v4.5、v4.0)コード」を選択し、ダイアログのプロセス一覧から「Inventor.exe」を選択</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02788064bb9a200d-pi"><img alt="Attache" class="asset  asset-image at-xid-6a0167607c2431970b02788064bb9a200d img-responsive" src="/assets/image_639361.jpg" style="display: block; margin-left: auto; margin-right: auto;" title="Attache" /></a></p>
<p>&#0160;&#0160;&#0160; &#0160;&#0160;&#0160;&#0160;3．InventorのiLogicエディタで、実行するiLogicに Break 命令を記入し、iLogicを実行</p>
<p>&#0160;&#0160;&#0160; &#0160; &#0160; 4．Visual Studioで、pdbが存在しない例外により実行がストップする場合がありますので、不要な例外のハンドリングは無視するように設定して、F10キーなどで継続して実行していくと、Visual StudioにInventorが実行中のiLogicコードが表示され、デバッグ実行が可能となります。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02942f927960200c-pi"><img alt="OnDebugging" class="asset  asset-image at-xid-6a0167607c2431970b02942f927960200c img-responsive" src="/assets/image_528139.jpg" style="display: block; margin-left: auto; margin-right: auto;" title="OnDebugging" /></a></p>
<p>Visual Studioに表示されたコード上では、Break Pointの設定、ステップ実行、ローカル変数の値の参照といったVisual Studioでのデバッグ機能を利用することが可能です。</p>
<p>&#0160;</p>
<p>制限事項：</p>
<p>Visual StudioでiLogicのデバッグを行う手順を紹介しましたが、幾つかの制限事項がある点にご留意ください。</p>
<ol>
<li>デバッグ実行中はInventorのエディタはすべての操作を受け付けない状態となります</li>
<li>Visual Studioに表示されているソースコードは、実行しているiLogicを元にしたソースコードですが、iLogicの記述とは多少の差異があります</li>
<li>Visual Stuidoに表示されているソースコードを編集できません。変更をする場合は、元のiLogicを編集するようにしてください</li>
</ol>
<p>&#0160;</p>
<p>いかがでしたでしょうか？Visual Studioを利用する必要があったり、プロセスへのアタッチ等の多少の手間はかかりますが、Traceログの出力する方法と比較すると、ステップ実行や変数の値の参照などにより、iLogicの開発や保守が容易に行えるようになるかと思いますので、是非ご活用ください。</p>
<p>&#0160;</p>
<p>By Takehiro Kato</p>
