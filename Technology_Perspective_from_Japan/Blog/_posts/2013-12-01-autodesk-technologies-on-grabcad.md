---
layout: "post"
title: "GrabCAD でのオートデスク テクノロジ"
date: "2013-12-01 14:54:35"
author: "Toshiaki Isezaki"
categories:
  - "その他カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2013/12/autodesk-technologies-on-grabcad.html "
typepad_basename: "autodesk-technologies-on-grabcad"
typepad_status: "Publish"
---

<p><a href="https://grabcad.com/" target="_blank"><strong>GrabCAD</strong></a> をご存じでしょうか?</p>
<p>GrabCAD（<a href="https://grabcad.com/">https://grabcad.com/</a>）は、CAD 製品の名称ではなく、米国で設立され、メカニカル設計エンジニア向けの 3D モデルの公開、共有の場を提供する <strong>オープン エンジニアリング</strong> サイトです。GrabCAD の&#0160;Workbench という環境を使って、さまざまなデスクトップ CAD 製品で作成された 3D モデルを表示、閲覧ができます。また、GrabCAD のアカウントを作成すれば、コメントを書き込んだりしながら、コラボレーションしたり、他のユーザが作成したモデルをダウンロードすることで、自身のモデルに再利用することも可能です。もちろん、オートデスクの CAD 製品で作成されたモデルも多数アップロードされて、公開されています。</p>
<p>GrabCAD は Web ブラウザで利用することになりますが、標準ビューワーで投稿された 3D モデルを表示する場合には、<a href="http://ja.wikipedia.org/wiki/WebGL" target="_blank"><strong>WebGL</strong></a> が有効な Web ブラウザを使用する必要があります。一般には、<a href="http://www.google.com/intl/ja/chrome/browser/" target="_blank"><strong>Google Chrome</strong></a> や <a href="http://www.mozilla.jp/firefox/" target="_blank"><strong>Mozilla Firefox</strong></a> などを利用することになるかと思います。</p>
<p>このビューワー機能は、ツリー構造でモデルの構成を把握できるばかりでなく、構成アイテム毎に表示/非表示を切り替え、アセンブリを分解表示や断面表示をさせることもできます。</p>
<p>GrabCAD で公開したり、自身で作成したプロジェクトで管理できるのは、3Dモデルだけではありません。2D 図面ファイルも、同じようにアップロードすることが出来ます。</p>
<p style="text-align: left;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b019b01ec689f970b-pi" style="display: inline;"><img alt="GrabCAD_Viewwe" border="0" class="asset  asset-image at-xid-6a0167607c2431970b019b01ec689f970b image-full img-responsive" src="/assets/image_680039.jpg" style="display: block; margin-left: auto; margin-right: auto;" title="GrabCAD_Viewwe" /></a></p>
<p style="text-align: left;">この GrabCAD とオートデスクはパートナーシップを結んでいて、Workbench にビューワーを超えた編集機能を提供しています。それが、Fusion 360 と AutoCAD 360 Web（Beta） です。このツールを駆使して、GrabCAD 上に作成したプロジェクト管理している 3D モデルや図面を、直接編集することもできるようになります。</p>
<p>以前の<a href="http://adndevblog.typepad.com/technology_perspective/2013/05/autocad-360.html" target="_blank"><strong>ポスト</strong></a>でもご紹介しましたが、AutoCAD 360 Web（Beta）は、Adobe Flash で構成されていた AutoCAD WS Web の後継バージョンです。AutoCAD WS Web との違いは、AutoCAD 360 Web が&#0160;<a href="http://ja.wikipedia.org/wiki/HTML5" target="_blank"><strong>HTML5</strong></a> で作成されているという点です。このため、GrabCAD のような外部サイトとも親和性が高く、Workbench 内部で利用することができるわけです。AutoCAD 360 Web（Beta）は HTML5 化と同時にユーザ インタフェースを一新しているため、利用者から操作性などのユーザ フィードバックを得て、近い将来、適切なかたちで &quot;Beta&quot; を外す予定です。</p>
<p>GrabCAD で利用できる Fusion 360 は、オートデスクが <a href="http://autodesk.com/tryfusion360" target="_self">http://autodesk.com/tryfusion360</a>&#0160;から提供する Fusion 360 の振る舞いと少し異なる部分があります。編集対象となる 3D モデルは Autodesk 360 のストレージ領域ではなく、GrabCAD のクラウドに保存される点です。この部分は、前述の AutoCAD 360 Web（Beta）も同様ですが、Fusion 360 は Web ブラウザではなく、ハードウェア アクセラレーションを利用する都合上、独立した小さなアプリケーションとして提供されています。このため、GrabCAD 専用に、Fusion 360 for GrabCAD が用意されていて、Workbench 内からインストールすることが出来ます。</p>
<p style="text-align: center;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b019b01ebf774970c-pi" style="display: inline;"><img alt="Fusion_for_GrabCAD" border="0" class="asset  asset-image at-xid-6a0167607c2431970b019b01ebf774970c image-full img-responsive" src="/assets/image_653982.jpg" title="Fusion_for_GrabCAD" /></a></p>
<p>GrabCAD の Workbench に作成したプロジェクト上のファイルについて、AutoCAD 360 Web と Fusion 360 で編集する様子を記録した簡単な動画を作成しましたので、実際にご確認ください。</p>
<p style="text-align: center;"><iframe allowfullscreen="" frameborder="0" height="281" src="http://www.youtube.com/embed/ORgP34A_jLk?feature=oembed" width="500"></iframe>&#0160;</p>
<p>GrabCAD 内で利用する AutoCAD 360 Web も Fusion 360 も、一応、90日間のトライアルの位置づけを持っています。ただ、前者は Beta、後者も正式には有償提供されていない状態なので、現段階では、あまり気にしていただく必要はないのかも知れません。</p>
<p>なお、Fusion 360 for GrabCAD エディタ内の機能は、オートデスク サイトから入手できる <a href="http://autodesk.com/tryfusion360%20" target="_blank">Fusion 360</a>&#0160;と機能的な差異はありません。ただし、Fusion 360 で作成してダウンロードした .F3D ファイルを、GrabCAD 側でアップロードした際には、GrabCAD の標準ビューワーでそのまま表示させることが出来ません。そのような場合、次のようなエラーが表示されるので、一旦、[Open with Autodesk Fusion 360] メニューから、対象モデルを Fusion 360 for GrabCAD で開いて、Save コマンドで保存してください。この処理で&#0160;GrabCAD の標準ビューワー上にモデルを表示させることが出来るようになります。</p>
<p style="text-align: center;">&#0160;</p>
<p style="text-align: center;"><img alt="Open_with_Fsuion" border="0" class="asset  asset-image at-xid-6a0167607c2431970b019b01ec1a29970b image-full img-responsive" src="/assets/image_112107.jpg" title="Open_with_Fsuion" /></p>
<p style="text-align: center;"><img alt="Viewer_Error" border="0" class="asset  asset-image at-xid-6a0167607c2431970b019b01ec0fb4970b image-full img-responsive" src="/assets/image_2714.jpg" title="Viewer_Error" /></p>
<p>さて、オートデスクが運営しているサイト以外で、オートデスクのテクノロジが利用されている点を不思議に思う方もいらっしゃるのではないでしょうか？</p>
<p>ビジネス面の考え方はさておき、ここではカスタマイズの視点で捉えていただくと面白いと思います。つまり、オートデスクのクラウド テクノロジ を再利用することが可能である、ということです。これらは、COM を利用したコンポーネント テクノロジを指しているのではありません。今後、Web サービス API といった形で、認証をはじめ、さまざまなオートデスクが持つクラウド テクノロジを使っていただけるのではないかと想像しています。</p>
<p>そんなお話が出来る時期が早く来ることを期待しています。</p>
<p>By Toshiaki Isezaki</p>
<p>&#0160;</p>
