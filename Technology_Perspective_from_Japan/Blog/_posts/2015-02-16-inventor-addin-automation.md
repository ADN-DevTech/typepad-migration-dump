---
layout: "post"
title: "Inventorのカスタムアドインの Automation 公開機能"
date: "2015-02-16 01:30:00"
author: "Shigelazu Saitou"
categories:
  - "API カスタマイズ"
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2015/02/inventor_addin_automation.html "
typepad_basename: "inventor_addin_automation"
typepad_status: "Publish"
---

<p>今週は、Inventor製品のカスタムアドインの Automation 機能を使ってカスタムメソッドを公開する事で、他の実行環境より利用できる手法をご紹介させていただきます。</p>

<p>これは、カスタマイズアドイン相互間でそれぞれのカスタマイズ関数やファンクションの相互利用を構築できるメリットがあり、カスタマイズシステム内でアドインの為の共通関数やファンクションをモジュール化したライブラリもアドインで構築できるなど、コーディングの効率化を始め、デバック作業の簡素化にも役立つメカニズムと考えます。</p>

<p>まず、Inventor製品向けのAPIによるカスタマイズは、<a href="http://adndevblog.typepad.com/technology_perspective/2013/02/inventor-api-入門.html">Inventor API 入門</a> でもお知らせしておりますように、Autodesk Inventor オブジェクトモデルを使った、VBAIDEによるVBAマクロ や Visual Studio環境を使った Inventorアドイン や 外部EXE実行環境内からの操作　の他に、無料の読み込み専用の アペレンテスサーバー による カスタマイズシステムの構築が可能です。</p>

<p>この中でも、外部EXE実行環境内からの操作として、<a href="http://adndevblog.typepad.com/technology_perspective/2013/03/inventor-api-入門その４-外部実行プログラムより-inventor製品-を制御する方法.html">Inventor API 入門　その４ 外部実行プログラムより Inventor製品 を制御する方法</a> で 「Autodesk Inventor Object Library」として一般に公開されている <span class="asset  asset-generic at-xid-6a0167607c2431970b01b8d0d35ffc970c img-responsive"><a href="http://adndevblog.typepad.com/files/inventor2015objectmodel.pdf">Inventor2015ObjectModel</a></span> をハンドリングしたカスタマイズシステムを作成いただけます。</p>

<p>カスタマイズに際して、以下の 日本語版の API プログラミングヘルプ がご利用いただけます。</p>

<p><a href="http://adndevblog.typepad.com/technology_perspective/2014/10/japanese-inventor-2015-programming-api-help.html">日本語版 Inventor 2015 API プログラミング用ヘルプ</a><br />
<a href="http://tech.autodesk.jp/faq/faq/adsk_result_dd.asp?QA_ID=9268">QA-9268 日本語版 Inventor 2015 API Help</a></p>

<p><br />
実際にカスタマイズシステムの設計や作成をしていくのに、カスタマイズアドイン内から他のカスタマイズアドイン内のメソッドやファンクションを直接利用したり、外部の実行EXEから起動されているInventor内のカスタムアドイン内のメソッドやファンクションを直接利用したい要望は必ず発生しているはずです。</p>

<p>マクロなどは、Inventor.VBAProjects.InventorVBAComponentsコレクション内のInventorVBAComponentオブジェクトの中から、InventorVBAMemberによりモジュール内のマクロにアクセスし、Executeメソッドにて他のマクロを実行する方法は存在します。</p>

<p>一方、カスタムアドインの場合、カスタムアドイン側で Automation メソッドを使い独自のインターフェースとして公開する事で、カスタマイズアドイン相互間でのそれぞれの関数やファンクションの相互利用を始め、外部実行EXE環境からもInventor製品が起動している状態であれば、カスタムアドイン内で公開されたインターフェースを通して関数やファンクションがご利用いただけます。</p>

<p><br />
<a class="asset-img-link"  style="display: inline;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c749ff7a970b-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b01b7c749ff7a970b img-responsive" alt="2015-02-16ExternalVBEXE" title="2015-02-16ExternalVBEXE" src="/assets/image_340444.jpg" /></a><br /><br />
<a class="asset-img-link"  style="display: inline;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb07eda239970d-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b01bb07eda239970d img-responsive" alt="2015-02-16ExternalCSharpEXE" title="2015-02-16ExternalCSharpEXE" src="/assets/image_878276.jpg" /></a><br /></p>

<p>以下の QA-9517 は、Automation 機能を使い公開されているカスタマイズアドインを持つ起動済みInventor製品に対して、外部EXE実行環境よりカスタマイズアドイン内のカスタマイズ関数とファンクションを実行させる記事となっています。<br />
（ VB.Net と C#.Net のサンプルプロジェクト ）</p>

<p><a href="http://tech.autodesk.jp/faq/faq/adsk_result_dd.asp?QA_ID=9517">QA-9517 Inventor2015 で 外部EXEからカスタムアドインのメソッドの実行処理<br />
</a></p>

<p>尚、Inventor製品のAPIご利用が初めての方は、以下の QA 情報がご利用いただけます。</p>

<p><a href="http://tech.autodesk.jp/faq/faq/adsk_result_dd.asp?QA_ID=8945">QA-8945 Autodesk Inventor 2015 の APIトレーニングテキスト</a><br />
<a href="http://tech.autodesk.jp/faq/faq/adsk_result_dd.asp?QA_ID=8946"><br />
QA-8946 Autodesk Inventor 2015 の APIトレーニングテキスト(実習ガイドと演習＆回答)</a></p>

<p><a href="http://tech.autodesk.jp/faq/faq/adsk_result_dd.asp?QA_ID=8947">QA-8947 Autodesk Inventor 2015 の APIトレーニングテキスト(演習と実習用ファイル)</a></p>

<p>By Shigekazu Saito</p>
