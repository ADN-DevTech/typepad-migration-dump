---
layout: "post"
title: "AutoCAD の隠しシステム変数"
date: "2016-02-01 00:29:57"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2016/02/hidden-system-variable-on-autocad.html "
typepad_basename: "hidden-system-variable-on-autocad"
typepad_status: "Publish"
---

<p>以前の&#0160;<strong><a href="http://adndevblog.typepad.com/technology_perspective/2013/05/command_and_system_variable.html" target="_blank">ブログ記事</a>&#0160;</strong>でも言及していますが、AutoCAD をカスタマイズする際には、システム変数を知ることが、カスタマイズ対象の機能がコントロール可能か否か、また、どの程度のコントロールが可能かを知る目安になります。 システム変数の一覧や各機能はバージョン毎に更新されるオンライヘルプで参照することが出来ます。例えば、AutoCAD のシステム変数は、<a href="http://help.autodesk.com/view/ACD/2016/JPN/?url=/view/ACD/2016/JPN/files/alphabetical-list-of-sysvars.html" target="_blank">こちら</a> から参照することが可能です。また、新規に登場したシステム変数は削除されたシステム変数など、バージョン間の違いもオンラインヘルプから参照することが出来ます。&#0160;</p>
<ul>
<li><a href="http://help.autodesk.com/cloudhelp/2016/JPN/AutoCAD-Core/files/GUID-B93A458E-1A7F-4090-A8CF-87A31C24E404.htm" target="_blank">新しいコマンドとシステム変数</a></li>
<li><a href="http://help.autodesk.com/cloudhelp/2016/JPN/AutoCAD-Core/files/GUID-4FEBA606-95E0-4DC4-A116-257ED86DCD58.htm" target="_blank">更新されたコマンドとシステム変数</a></li>
<li><a href="http://help.autodesk.com/cloudhelp/2016/JPN/AutoCAD-Core/files/GUID-5647ACE3-412E-48A6-8C1B-6B0EE09F5E3F.htm" target="_blank">廃止されたコマンドとシステム変数</a></li>
</ul>
<p>オンラインヘルプに記載されているシステム変数は、そのバージョンで正式にサポートされているものとなります。AutoCAD API には、システム変数へのアクセスをする関数やメソッドが用意されているので、それらにアクセスして環境を整えたり、環境の設定内容をカスタマイズ プログラム側から把握することが出来るわけです。</p>
<p>さて、このシステム変数ですが、オンラインヘルプにも記載されていないものが存在します。つまり、正式にはサポートはされていないものの、何かしら重宝するかもしれないシステム変数があります。今後の提供され続けるか、実際に役に立つかは別として、今回は、それらに触れてみたいと思います。</p>
<p><strong>&#0160;_PKSER</strong></p>
<p style="padding-left: 30px;">使用中のシリアル番号を表示します。シリアル番号は、インストール時に入力します。通常、インストール後に気にする必要はありませんが、ライセンス管理者などがソフトウェア資産管理などでシリアル番号が知りたくなった場合には、このシステム変数をチェックすることで、すぐに値を確認することが出来ます。</p>
<p style="padding-left: 30px;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c80efb9c970b-pi" style="display: inline;"><img alt="_pkser" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b7c80efb9c970b image-full img-responsive" src="/assets/image_305053.jpg" title="_pkser" /></a>&#0160;</p>
<p style="padding-left: 30px;">なお、シリアル番号は、<strong><a href="http://help.autodesk.com/view/ACD/2016/JPN/?guid=GUID-CDBAD44E-F661-430C-A99E-192B83D41C10" target="_blank">ABOUT[バージョン情報]</a>&#0160;</strong>コマンドから [製品のライセンス情報] ダイアログを表示させて確認することも出来ます。</p>
<p style="padding-left: 30px;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb08b3c7b8970d-pi" style="display: inline;"><img alt="App_info" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01bb08b3c7b8970d image-full img-responsive" src="/assets/image_329768.jpg" title="App_info" /></a></p>
<p><strong>&#0160;_VERNUM</strong></p>
<p style="padding-left: 30px;">AutoCAD はオートデスクの開発チームが日々、開発をしています。具体的には、新機能や既存の機能の修正や改良などについて、それをコントロールするプログラムを新しく作成したり、書き換えたりしています。この方法は、ソフトウェア開発では一般的ですが、作成したプログラムが正しく期待通りに動作するか、確認する必要があります。このため、開発チームは、毎週、新機能や修正部分のプログラムを組み込んだテスト用の AutoCAD を作成していて、毎回作成される AutoCAD を「ビルド番号」 と呼ばれる開発用語で区別しています。</p>
<p style="padding-left: 30px;">_VERNUM システム変数は、このビルド番号を返します。一般ユーザは、当然、毎週作成される AutoCAD を利用することは出来ませんが、製品出荷直後の AutoCAD とサービスパックの適用後の AutoCAD で、ビルド番号が変化していることに気が付くはずです。つまり、サービスパックやエクステンションが適用されているのか否か、ビルド番号を参照することで把握することが出来ます。次の画像は、AutoCAD 2016 SP1 上で _VERNUM の値を表示させたものです。</p>
<p style="padding-left: 30px;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb08b3c959970d-pi" style="display: inline;"><img alt="_vernum" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01bb08b3c959970d image-full img-responsive" src="/assets/image_815725.jpg" title="_vernum" /></a></p>
<p style="padding-left: 30px;">ここ数バージョンの AutoCAD では、<strong><a href="http://help.autodesk.com/view/ACD/2016/JPN/?guid=GUID-CDBAD44E-F661-430C-A99E-192B83D41C10" target="_blank">ABOUT[バージョン情報]</a>&#0160;</strong>コマンドの [バージョン情報] ダイアログでも、このビルド番号を表示させることが出来ます。&#0160;</p>
<p style="padding-left: 30px;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb08b3c970970d-pi" style="display: inline;"><img alt="About" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01bb08b3c970970d image-full img-responsive" src="/assets/image_409114.jpg" title="About" /></a></p>
<p><strong>*_TOOLPALETTEPATH</strong></p>
<p style="padding-left: 30px;">ツールパレットの検索バスを返すシステム変数です。セミコロンで区切ることで、新しいパスを追加して設定することも出来ます。</p>
<p style="padding-left: 30px;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c80efe43970b-pi" style="display: inline;"><img alt="_toolpalettepath" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b7c80efe43970b image-full img-responsive" src="/assets/image_78811.jpg" title="_toolpalettepath" /></a></p>
<p style="padding-left: 30px;">ツールパレットの検索バスは、OPTIONS[オプション] コマンドで表示させることが出来る [オプション] ダイアログで、[ファイル] タブからも参照、設定することが出来ます。</p>
<p style="padding-left: 30px;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c80efddc970b-pi" style="display: inline;"><img alt="Options" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b7c80efddc970b image-full img-responsive" src="/assets/image_780955.jpg" title="Options" /></a></p>
<p>繰り返しますが、これらのシステム変数は公式にはサポートされていないシステム変数です。今後のバージョンで削除される可能性もありますので、その点はご留意ください。&#0160;</p>
<p>By Toshiaki Isezaki</p>
