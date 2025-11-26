---
layout: "post"
title: "AutoCAD 雑学：カスタム オブジェクトとオブジェクト イネーブラ"
date: "2022-09-07 00:10:56"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2022/09/autocad-trivia-custom-object-and-object-enabler.html "
typepad_basename: "autocad-trivia-custom-object-and-object-enabler"
typepad_status: "Publish"
---

<p><a href="https://knowledge.autodesk.com/ja/support/autocad/learn-explore/caas/CloudHelp/cloudhelp/2023/JPN/AutoCAD-Core/files/GUID-6515268E-3D71-4CBC-8D3C-2059CFAA4E38-htm.html?us_oa=akn-us&amp;us_si=b690f31f-a243-4b88-804b-134e0c995de7&amp;us_st=%E6%A6%82%E8%A6%81%20-%20%E3%82%AB%E3%82%B9%E3%82%BF%E3%83%A0%20%E3%82%AA%E3%83%96%E3%82%B8%E3%82%A7%E3%82%AF%E3%83%88%E3%81%A8%E3%83%97%E3%83%AD%E3%82%AD%E3%82%B7%20%E3%82%AA%E3%83%96%E3%82%B8%E3%82%A7%E3%82%AF%E3%83%88" rel="noopener" target="_blank">概要 - カスタム オブジェクトとプロキシ オブジェクト | AutoCAD 2023 | Autodesk Knowledge Network</a> の内容について具体的に補足したいと思います。</p>
<p>以前、<a href="https://adndevblog.typepad.com/technology_perspective/2022/03/autocad-trivia-components.html" rel="noopener" target="_blank">AutoCAD 雑学：AutoCAD コンポーネント</a> でも触れていますが、AutoCAD API の 1 つである ObjectARX には、他の AutoCAD API にないカスタム オブジェクトの作成（定義）機能が用意されています。</p>
<p>カスタム オブジェクトとは、線分や円、ポリライン、寸法や画層など、AutoCAD が図面ファイル（.dwg/.dxf）に保存するのと同じように、独自の見た目や振る舞いを持たせたオブジェクトを指します。特定の業種で扱うオブジェクトを用意する手段として非常に有用であるため、オートデスクも AutoCAD Plus（AutoCAD including specialized toolsets）の業種別ツールセット（旧 AutoCAD Mechanical や Plant3D など）、AutoCAD Civil3D などで使用しています。もちろん、3rd party の開発者がカスタム オブジェクトを定義することも出来ます。</p>
<p>AutoCAD 上でカスタム オブジェクト扱う際には、拡張子 <strong>.dbx</strong> を持つコンポーネントがカスタム オブジェクトの外観や振る舞いを定義する<strong>オブジェクト イネーブラ</strong>と、拡張子<strong> .arx</strong> を持つコンポーネントがオブジェクト イネーブラを参照して作図・編集をおこなうコマンドを定義するアドイン アプリケーションが、共に AutoCAD にロードされて使用されることになります。</p>
<p>.dbx や .arx ファイル（コンポーネント）によっては、AutoCAD の実行中に APPLOAD コマンドでロード解除出来てしまうものがあります。カスタム オブジェクトには、もう 1 つの形態となるプロキシ オブジェクトが存在します。</p>
<p>カスタム オブジェクトとプロキシオブジェクトの関係を正しく理解するために、カスタム オブジェクトを含む図面の編集中に、.dbx や .arx ファイルがロード解除されてしまったら何が起こるのか、具体的に説明していきたいと思います。</p>
<hr />
<p><strong>コマンド コンポーネント（.arx ファイル）のロード解除</strong></p>
<p style="padding-left: 40px;">ここでは、LINE（線分）や CIRCLE（円）などのように分類出来るカスタム オブジェクトの種類を、MYENTITY（AcDbMyEntity クラス） として話を進めていきます。</p>
<p style="padding-left: 40px;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02a2eed2a299200d-pi" style="display: inline;"><img alt="Myentity" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02a2eed2a299200d image-full img-responsive" src="/assets/image_157669.jpg" title="Myentity" /></a></p>
<p style="padding-left: 40px;">カスタム オブジェクト MYENTITY を作図・編集するにはコマンドが必要です。つまり、作図や編集には .arx ファイルがロードされている必要があります。</p>
<p style="padding-left: 40px;">ここで、MYENTITY を作図・編集するにはコマンドを実装する .arx ファイルをロード解除してみます。ロード解除後には同コマンドが使えなくなるので、MYENTITY オブジェクトを新規に作図したり、既存のオブジェクトを編集することが出来なくなります。</p>
<p style="padding-left: 40px;">ただし、オブジェクト イネーブラ（.dbx ファイル）はロードされたままなので、AutoCAD は MYENTITY オブジェクトを正しく認識して、例えば、LIST コマンドや [プロパティ パレット] に MYENTITY オブジェクトの情報を正しく表示することが出来ます。</p>
<p style="padding-left: 40px;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02a30d49fa87200b-pi" style="display: inline;"><img alt="Custom_object" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02a30d49fa87200b image-full img-responsive" src="/assets/image_308264.jpg" title="Custom_object" /></a></p>
<p><strong>オブジェクト イネーブラ コンポーネント（.dbx ファイル）のロード解除</strong></p>
<p style="padding-left: 40px;">カスタム オブジェクト MENTITY の外観や振る舞いを定義する<strong>オブジェクト イネーブラ、</strong>.dbx ファイルをロード解除すると、どうなるでしょう？</p>
<p style="padding-left: 40px;">.dbx ファイルをロード解除してしまうと、AutoCAD は MYENTITY オブジェクトを正しく認識することが出来なくなってしまいます。この状態のオブジェクトを<strong>プロキシ オブジェクト</strong>と呼んでいます。プロキシ オブジェクトは、AutoCAD にとって「内容不明なオブジェクト」です。</p>
<p style="padding-left: 40px;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02a2eed29ad8200d-pi" style="display: inline;"><img alt="Proxy_object" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02a2eed29ad8200d image-full img-responsive" src="/assets/image_659794.jpg" title="Proxy_object" /></a></p>
<p style="padding-left: 40px;">AutoCAD はプロキシ オブジェクトの内容を把握出来ないため、LIST コマンドや [プロパティ パレット] で MYENTITY オブジェクトの情報を表示することが出来ないことになります。（多くの他の編集コマンドも同様）</p>
<p style="padding-left: 40px;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02a30d4a0238200b-pi" style="display: inline;"><img alt="Proxy" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02a30d4a0238200b image-full img-responsive" src="/assets/image_683189.jpg" title="Proxy" /></a></p>
<p style="padding-left: 40px;">このような状態は AutoCAD を利用するエンドユーザにとっても好ましいものではないため、AutoCAD は状態をエンドユーザに通知します。このとき表示されるのが、[プロキシ情報] ダイアログです。</p>
<p style="padding-left: 40px;">システム変数 <a href="https://help.autodesk.com/view/ACD/2023/JPN/?guid=GUID-A1A272D8-F3E3-4B84-AF23-1AFEF732DA03" rel="noopener" target="_blank">PROXYNOTICE</a> の値が 1 に設定されていると、カスタム オブジェクトがプロキシ オブジェクトになった際に表示します。オブジェクト イネーブラがない AutoCAD で、該当するカスタム オブジェクトを含む図面を開いた際にも表示されます。</p>
<p style="padding-left: 40px;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02a30d49fbe5200b-pi" style="display: inline;"><img alt="Proxy_notice" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02a30d49fbe5200b img-responsive" src="/assets/image_22845.jpg" title="Proxy_notice" /></a></p>
<p><strong>オブジェクト イネーブラ コンポーネント（.dbx ファイル）の再ロード</strong></p>
<p style="padding-left: 40px;">プロキシ オブジェクトを含む図面を開いているときに、対応するオブジェクト イネーブラ（.dbx ファイル）を再度ロードしたらどうでしょう？</p>
<p style="padding-left: 40px;">AutoCAD はプロキシ オブジェクトの内容をそのまま「フリーズさせて」保持しているので、正しい .dbx ファイルがロードされると、MYENTITY オブジェクトの状態を復活させます。プロキシ オブジェクトになったからと言って、MYENTITY オブジェクトの内部データ（座標や値）を破棄してしまうことはありなせん。</p>
<p style="padding-left: 40px;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02a2eed29adf200d-pi" style="display: inline;"><img alt="Custom_object2" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02a2eed29adf200d image-full img-responsive" src="/assets/image_619779.jpg" title="Custom_object2" /></a></p>
<hr />
<p>オブジェクト イネーブラは、ObjectARX を使って作成されたアドイン アプリケーションです。他のアドイン アプリケーション同様、使用する AutoCAD バージョンにあったものを利用する必要があります。</p>
<p>By Toshiaki Isezaki</p>
