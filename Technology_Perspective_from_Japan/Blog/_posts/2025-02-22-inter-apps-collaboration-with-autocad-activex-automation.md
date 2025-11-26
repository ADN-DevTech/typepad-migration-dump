---
layout: "post"
title: "AutoCAD ActiveX オートメーションによるアプリ間連携"
date: "2025-02-22 23:58:59"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2025/02/inter-apps-collaboration-with-autocad-activex-automation.html "
typepad_basename: "inter-apps-collaboration-with-autocad-activex-automation"
typepad_status: "Draft"
---

<p>図面内のブロック集計結果は、<a href="https://adndevblog.typepad.com/technology_perspective/2025/04/autocad-low-code-solution-data-extraction.htm" rel="noopener" target="_blank">AutoCAD ノーコード ソリューション：データ書き出し</a> でご紹介した方法で、ブロック属性やジオメトリのプロパティ情報を EXCEL ファイルの書き出すことが出来ます。</p>
<p>データ書き出し機能で作成される EXCEL ファイルは至ってシンプルはものなので、テーブルの書式設定やセルのスタイルなどの脚色は一切ありません。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3cd1859200c-pi" style="display: inline;"><img alt="Data_extraction" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3cd1859200c img-responsive" src="/assets/image_105164.jpg" title="Data_extraction" /></a></p>
<p>もし、EXCEL への情報書き出しの機能自体をカスタマイズしたい場合には、API を使ったカスタマイズが必要です。ただ、このようなカスタマイズを計画する場合、注意が必要になる点があります。まずは、AutoCAD を主体に、どのような API を利用出来るのかを考察してみます。</p>
<p>ご存じのように、AutoCAD には 5 つの API が用意されています。登場順に AutoLISP、ObjectARX、ActiveX オートメーション、.NET API、JavaScript API です。いずれも AutoCAD にロードして利用するアドイン（別名 アドオン、プラグイン）アプリの開発に使用されます。一方、EXCEL などのオートデスク外の他社製品と親和性が高く、API を使ったコントロールが可能なのは、ActiveX オートメーションになります。</p>
<p>ActiveX オートメーションは、Mircrosoft の提供するコンポーネント テクノロジ である COM (Component Object Model) を利用した API です。ActiveX オートメーションの仕様には、アプリの持つさまざまな機能を公開する <strong>COM サーバー</strong> と、公開された機能を参照して再利用する（プログラムして再利用する）<strong>COM クライアント</strong> の 2 つの能力が用意されています。</p>
<p>AutoCAD の場合、AutoCAD が COM サーバーの役割を持ち、COM クライアント機能を持った AutoCAD 内の VBA 環境で AutoCAD 自身の機能を利用する（プログラムして再利用する）ことができます。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02e860fabd3b200d-pi" style="display: inline;"><img alt="Self_access" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02e860fabd3b200d image-full img-responsive" src="/assets/image_485484.jpg" title="Self_access" /></a></p>
<p>ActiveX オートメーション（COM）は Windows 上では共通の仕様であるため、他の Windows アプリが COM サーバー仕様に従って情報を公開していれば、COM クライアントである AutoCAD VBA プログラムからそのアプリをコントロールすることができます。</p>
<p>そして、Microsoft EXCEL は、自身の機能を COM サーバー として公開しているので、EXCEL 内部の VBA から自身の機能をプログラムでコントロールすることができます。同様に、AutoCAD VBA からも EXCEL をプログラムでコントロール出来る理屈です。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02e860fabea2200d-pi" style="display: inline;"><img alt="Inter_access" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02e860fabea2200d img-responsive" src="/assets/image_577231.jpg" title="Inter_access" /></a></p>
<p>COM サーバーが持つ情報を公開しているのが、タイプ ライブラリ と呼ばれるファイルです。COM クライアントからタイプ ライブラリを参照することで、COM サーバーを参照する手続きが完了します。AutoCAD VBA 内の VBA プロジェクトは、すぐに AutoCAD のオブジェクトを扱えるように、AutoCAD のタイプ ライブラリが最初から参照された状態で作成されます。</p>
<p>同じように、EXCEL の VBA のプロジェクトは、EXCEL のタイプ ライブラリが自動的に参照設定されています。ただし、上図のように、異なるソフトウェアの COM サーバーを参照する場合は、明示的に相手のタイプ ライブラリを参照設定する必要があります。参照設定は、VBA エディタの [ツール] メニューから [参照設定] メニューを選択しておこないます。</p>
<p>つまり、ActiveX オートメーションを使えば、AutoCAD VBA から AutoCAD 図面の情報を拾い出して EXCEL を起動、拾い出したデータをシート上の書き出し、テーブル書式設定したり、グラフ化したり出来ます。同じように、EXCEL VBA からシート上の数値を読み取って AutoCAD を起動、数値を反映したジオメトリを作図するようなことが可能です。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02e860e3b8d9200b-pi" style="display: inline;"><img alt="Type_library" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02e860e3b8d9200b img-responsive" src="/assets/image_187487.jpg" title="Type_library" /></a></p>
<ul>
<li>例）<a href="https://forums.autodesk.com/t5/autodesk-community-tips-adnopun/autocad-activex-excel-vba-karano-li-yong/ta-p/12859434" rel="noopener" target="_blank">AutoCAD ActiveX：Excel VBA からの利用</a></li>
</ul>
<p>ActiveX オートメーションは Windows 上でしか利用出来ませんが、コントロールしたいアプリが ActiveX オートメーションに対応していれば（ここでは COM サーバー機能を持つという意味）、COM サーバーと COM クライアントの仕組みで相互にコントロール出来ることがわかります。</p>
<p>注意が必要なのは、コントロールしたいアプリの公開内容（オブジェクト、メソッド、プロパティ）は、そのアプリのドキュメントを参照する必要がある、という点です。AutoCAD VBA から EXCEL をコントロールする際には、<a href="https://learn.microsoft.com/ja-jp/office/vba/api/overview/excel" rel="noopener" target="_blank">Excel Visual Basic for Applications (VBA) リファレンス</a> などの参照が必須です。また、AutoCAD と同じように、EXCEL のバージョンによってタイプ ライブラリ名や公開情報が変わる可能性がある点にも気をくばる必要があります。</p>
<p>By Toshiaki Isezaki</p>
