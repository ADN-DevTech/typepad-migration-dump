---
layout: "post"
title: "図面ファイルのセキュリティ － パスワード保護"
date: "2014-10-29 23:56:14"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2014/10/security-for-drawing-file.html "
typepad_basename: "security-for-drawing-file"
typepad_status: "Publish"
---

<p>クラウド サービスをご紹介する際に、必ず、話題になるのがセキュリティです。この場合、矛先はクラウドに向かっているのですが、今日は、クラウド自身のセキュリティではなく、図面ファイル自身のセキュリティについて、AutoCAD が持つ図面ファイル保護の機能と API 上での扱いをご案内しておきたいと思います。</p>
<p>まず、ご紹介するのは、最も分かり易いファイル保護の1つである、図面ファイルのパスワード保護機能です。</p>
<p>図面ファイルを保存する際にパスワードを与えて保存することで、次回、その図面ファイルを開く際にパスワード入力が必要になります。正しいパスワードを入力しないと、図面ファイルを開いて表示や編集をおこなうことが出来ません。</p>
<p>パスワードの設定は、SAVEAS コマンド等で表示される [図面に名前を付けて保存] ダイアログ右上にある「セキュリティ オプション」から設定することが出来ます。</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb07a24378970d-pi" style="display: inline;"><img alt="Savaas" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01bb07a24378970d image-full img-responsive" src="/assets/image_250076.jpg" title="Savaas" /></a></p>
<p>「セキィリティ オプション」選択すると、[セキィリティ オプション] ダイアログが開くので、ここで任意のパスワードを入力することが出来ます。[OK] ボタンでダイアログを閉じる際には、再度パスワードの入力を求められます。</p>
<p style="text-align: center;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c6fd09c3970b-pi" style="display: inline;"><img alt="Security_option" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b7c6fd09c3970b image-full img-responsive" src="/assets/image_767092.jpg" title="Security_option" /></a></p>
<p>その後、名前を付けて図面ファイルを保存すると、パスワード保護された図面ファイルとして保存されることになります。注意が必要なのは、パスワード保護した図面の保存直後には、起動中の AutoCAD に設定したパスワードが「キャッシュ」として残っているため、すぐに図面ファイルを開いても、パスワードを聞かれずに図面をが開いてしまう点です。確認をするためには、いったん、AutoCAD を終了してから図面ファイルを開いてみてください。</p>
<p>キャッシュのない状態では、次のようなダイアログが表示されて、パスワードの入力を促します。</p>
<p style="text-align: center;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb07a2440f970d-pi" style="display: inline;"><img alt="Enter_password" class="asset  asset-image at-xid-6a0167607c2431970b01bb07a2440f970d img-responsive" src="/assets/image_621718.jpg" style="width: 320px;" title="Enter_password" /></a></p>
<p>ここで誤ったパスワードを入力すると、エラーが表示されるので、再度、パスワード入力をおこなうか、処理をキャンセルすることしか出来ません。</p>
<p style="text-align: center;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb07a24432970d-pi" style="display: inline;"><img alt="Password_error" class="asset  asset-image at-xid-6a0167607c2431970b01bb07a24432970d img-responsive" src="/assets/image_848724.jpg" style="width: 200px;" title="Password_error" /></a></p>
<p>API からパスワード保護を設定したり、パスワード保護された図面を開く処理を実装するのは、いたって簡単です。例えば、AutoCAD .NET API で図面ファイルを保存する場合には、Database.SaveAs メソッドを利用するます。この部分については、AutoCAD のオンライヘルプに記述がありますので、まずは&#0160;<a href="http://help.autodesk.com/view/ACD/2015/JPN/?guid=GUID-CBB74093-BB22-49CC-B54A-D33D7E92694C" target="_self"><strong>こちら</strong></a>&#0160;のページをご参照ください。このページで利用されている SaveAs メソッドの第 4 パラメータ &quot;SecurityParameters&quot; が、セキュリティ設定を担う部分です。ここにパスワードを設定することになります。</p>
<p>パスワード保護された図面ファイルを API で開く場合には、DocumentCollectionExtension.Open&#0160;メソッドを利用します。Open メソッドには 3つのオーバーロードがありますが、このうちの1つに、パスワードをパラメータに設定ｄけきるものがあります。</p>
<p>具体的なパスワード保護設定と、図面ファイルのオープンについては、次のドキュメントで解説していますので、必要に応じてご参照ください。</p>
<p style="padding-left: 30px;"><strong><span class="asset  asset-generic at-xid-6a0167607c2431970b01b7c80fe9ec970b img-responsive"><a href="http://adndevblog.typepad.com/files/qa-9309.pdf" target="_blank">QA-9309 AutoCAD .NET API でパスワード保護された図面を扱う</a></span></strong></p>
<p>何かと便利なセキィリティ機能ですが、次の点にはご注意ください。</p>
<ul>
<li>パスワードを忘れてしまった場合には、残念ながら API でも図面ファイルを開くことができなくなります。オートデスクが解析してパスワードをはずすようなことも出来ません。</li>
<li>パスワード保護された図面ファイルを A360 クラウドにアップロードしても、A360 が図面ファイルにアクセスできなくなるため、プレビュー生成や図面表示機能を利用できなくなってしまいます。</li>
</ul>
<p>パスワード保護は、図面流出時のセキュリティを保護するもっとも容易な方法です。長所短所をご理解いただいた上で、適宜、お使いください。</p>
<p><span style="background-color: #ffff00;">ご注意：パスワード保護の機能は、AutoCAD 2016 で削除されています。詳細は、<strong><a href="http://adndevblog.typepad.com/technology_perspective/2015/04/autocad-2016-interoperability-for-customization.html" style="background-color: #ffff00;" target="_blank">AutoCAD 2016 のカスタマイズ互換性</a></strong>&#0160;のブログ記事をご確認ください。</span></p>
<p>By Toshiaki Isezaki</p>
