---
layout: "post"
title: "Developer Registered Symbol 登録の廃止"
date: "2017-01-23 00:15:05"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2017/01/abolish-developer-registered-symbol-registration.html "
typepad_basename: "abolish-developer-registered-symbol-registration"
typepad_status: "Publish"
---

<p><br /><strong>Registered Developer Symbol (RDS)</strong> をご存知でしょうか？ <a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb096ff187970d-pi" style="float: right;"><img alt="Objectarrx_logo" class="asset  asset-image at-xid-6a0167607c2431970b01bb096ff187970d img-responsive" src="/assets/image_331806.jpg" style="width: 150px; margin: 0px 0px 5px 5px;" title="Objectarrx_logo" /></a></p>
<p>RDS &#0160;は AutoCAD の ObjectARX アドインの開発で使用されるもので、AutoCAD 1999年に発売が開始された AutoCAD 2000で、ObjectARX SDK for AutoCAD 2000 に含まれる ObjectARX Wizard に初めて導入されました。その実態は、さまざまな識別で利用される任意の英数字 4文字です。</p>
<p>現在でも、最新の AutoCAD 2017 用の ObjectARX Wizard の画面に RDS を指定する入力項目が残されています。</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d256f9ca970c-pi" style="display: inline;"><img alt="Rds_on_oarx_wizard" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b8d256f9ca970c image-full img-responsive" src="/assets/image_313594.jpg" title="Rds_on_oarx_wizard" /></a></p>
<p><strong>Registered Developer Symbol (RDS) の推奨用途：</strong></p>
<p style="padding-left: 30px;">RDS には、複数の用途が推奨されていました。その代表例が ObjectARX を使ったカスタム コマンドの登録時に指定するコマンド グループ名での使用です。</p>
<p style="padding-left: 30px;">アドイン アプリケーションがカスタム コマンドを登録する際に、複数のアドインが同じ名前でカスタム コマンドを登録してしまうと、AutoCAD は最後にロードしたアドインに定義されるカスタム コマンドを実行するようになります。これは、AutoCAD が実行可能なコマンドをコマンドスタックで管理しているために発生する現象です。</p>
<p style="padding-left: 30px;">同じ名前のカスタム コマンドが複数の ObjectARX アドインで定義されていると、AutoCAD の起動後、早い段階でロードされたアドインに定義されるコマンドを呼び出すことが出来なくなってしまいます。このような場面では、アドインが定義したコマン ドグループ名とともにコマンド名を呼び出すことで、確実にアドイン毎のカスタム コマンドを実行する方法が提供されています。</p>
<p style="padding-left: 30px;">例えば、CCC コマンド グループでコマンドを定義したアドイン、BBB コマンド グループでコマンドを定義したアドイン、AAA コマンド グループでコマンドを定義したアドインの順で AutoCAD のロードした場合、それぞれに TEST1 コマンドが定義されていると、TEST1 と入力して実行されるのは、常に最後にロードされた AAA コマンド グループを持つアドインの TEST1 コマンドです。ただし、BBB.TEST1 や CCC.TEST1 のようにピリオドで結合してコマンドを入力すると、ロードした順序に関係なく、特定のアドインが定義した TEST1 コマンドを実行することが出来ます。</p>
<p style="padding-left: 30px;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb096ff40f970d-pi" style="display: inline;"><img alt="Command_stack" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01bb096ff40f970d image-full img-responsive" src="/assets/image_17674.jpg" title="Command_stack" /></a></p>
<p style="padding-left: 30px;">ただし、もし、複数のアドインで同じコマンド グループが使われた場合には、コマンド グループを含めたカスタム コメントの呼び出しも利用することが出来ません。</p>
<p style="padding-left: 30px;">同様に、RDS は ObjectARX でカスタムクラスを定義する際にも、クラス名や DXF 名（オブジェクト名）のプリフィックスに使用することが推奨されていました。特に、AcDbObject クラスや AcDbEntity クラス等からクラス派生してカスタム オブジェクトを定義した場合、カスタムオブジェクトを容易に識別する手段に利用出来ます。また、カスタム オブジェクトがプロキシ オブジェクト化してしまった場合でも、アプリケーション名の中に表示させて、カスタム オブジェクトを開発した企業名を想定させることも出来ます。</p>
<p style="padding-left: 30px;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb096ff3e2970d-pi" style="display: inline;"><img alt="Proxy_notice" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01bb096ff3e2970d img-responsive" src="/assets/image_555906.jpg" title="Proxy_notice" /></a></p>
<p style="padding-left: 30px;">残念ながら、この推奨でも、異なる企業で同じ名前の RDS 4 文字が使用された場合には、あまり意味のない結果となることが予想されます。</p>
<p><strong>RDS の目的：</strong></p>
<p style="padding-left: 30px;">上記、RDS の推奨例でも、全く異なる組織が同じ RDS を利用してしまうと、本来の目的を達成出来ないことになります。このような重複を抑止する目的で、ObjectARX で開発を始める開発者が RDS をオンラインで事前登録して、過去に登録された RDS を避ける仕組みが用意されました。この RDS オンライン登録サイトは、 http://www.autodesk.com/symbreg/index.htm です。この登録を世界的に用いることで、国をまたいでも、RDS の重複を避けることが出来るはずでした。</p>
<p><strong>RDS 登録の廃止：</strong></p>
<p style="padding-left: 30px;">ただ、問題がありました。RDS の登録が必ずしも必須だったわけではなかったので、ObjectARX を利用する開発者が、固有の RDS しなかったという問題です。また、RDS で指定可能な英数字が 4 文字に限定されていたこともあり、期待した RDS が既に登録済みで登録出来ない、といった問題も存在しました。さらに問題だったのは、登録済みの RDS が、今日でも有効に使われいるか、確認する手立てがなかった点です。</p>
<p style="padding-left: 30px;">そこで、2016 年 12 月、オートデスクは RDS &#0160;のオンライン登録の廃止を決定した次第です。事実、その後、コマンド グループ名の重複によるコマンド実行時の不具合や、カスタム オブジェクト識別で問題は報告されていない状態です。この決定を受けて、オンライン登録ページ（http://www.autodesk.com/symbreg/index.htm）も既に閉鎖されています。</p>
<p><strong>ご注意：</strong></p>
<p style="padding-left: 30px;">今回の決定は、RDS の新規登録ページの閉鎖のみである点にご注意ください。既に登録した RDS をお持ちであれば、従来通り、その RDS 4 文字をお使いいただくことが出来ます。また、RDS を登録していなくても、任意の 4 文字を指定すれば、ObjectARX Wizard を継続して利用することも出来ます。</p>
<p style="padding-left: 30px;">もちろん、カスタム オブジェクトの作成や利用、カスタム クラスを利用するリアクタなど、ObjectARX の各種機能が使用出来なくなるわけではなりませんのでご注意ください。従来のコードも、そのまま動作します。</p>
<ul style="list-style-type: circle;">
<li>AutoCAD .NET API では、アセンブリ情報がコマンドグループとして活用されています。例えば、AutoCAD 2017 用の BIM 360 Glue Addin の場合には、ARX[ARX管理] コマンドで識別されるコマンド グループ名が、<em>&#0160;&quot;BIM360GlueAutoCAD2017Addin, Version=4.37.6853.0, Culture=neutral, PublicKeyToken=null</em>&quot; になっています。この文字列には半角スペースが含まれるため、コマンド グループ名とコマンド名をピリオドで結合して入力・実行することは出来ませんが、このコマンド グループ名を見れば、他のアドインと競合する可能性がかなり低いことがご理解いただけるはずです。</li>
</ul>
<p>By Toshiaki Isezaki</p>
