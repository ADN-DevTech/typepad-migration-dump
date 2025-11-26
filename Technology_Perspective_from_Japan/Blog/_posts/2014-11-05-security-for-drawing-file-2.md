---
layout: "post"
title: "図面ファイルのセキュリティ － デジタル署名"
date: "2014-11-05 01:12:01"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2014/11/security-for-drawing-file-2.html "
typepad_basename: "security-for-drawing-file-2"
typepad_status: "Publish"
---

<p><a href="http://adndevblog.typepad.com/technology_perspective/2014/10/security-for-drawing-file.html" target="_blank"><strong>前回</strong></a>は、図面ファイルにパスワード設定する方法をご紹介しました。これとは別に、AutoCAD にはデジタル署名を利用した図面ファイルの編集を検出する機能があります。別の言い方をするなら、図面ファイルにデジタル署名を埋め込むことで、第三者による図面の改ざんを検出することが出来るようになります。</p>
<p>デジタル署名を利用するためには、デジタル証明書、または、<a href="http://ja.wikipedia.org/wiki/%E5%85%AC%E9%96%8B%E9%8D%B5%E8%A8%BC%E6%98%8E%E6%9B%B8" target="_blank"><strong>公開鍵証明書</strong></a> とも呼ばれる&#0160;<strong>デジタルID</strong>&#0160;を取得する必要があります。デジタル ID とは、日本の商習慣で用いられている「印鑑」と同等の意味合いをコンピュータ上で代替するもの、と考えると分かり易いかもしれません。</p>
<p>AutoCAD で SECURITYOPTIONS[セキュリティオプション] コマンドから [セキュリティ オプション] ダイアログを開き、[デジタル署名] タブを開こうとした際、お使いのコンピュータ上にデジタル ID がインストールされていないと、次のようなメッセージがダイアログ上に表示されます。</p>
<p style="text-align: center;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb07a46f31970d-pi" style="display: inline;"><img alt="Get_pki_dialog_on_op" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01bb07a46f31970d image-full img-responsive" src="/assets/image_742454.jpg" title="Get_pki_dialog_on_op" /></a></p>
<p>なお、[セキュリティ オプション] ダイアログは、SAVEAS[図面に名前を付けて保存] コマンドの [図面に名前を付けて保存] ダイアログ右上からアクセスすることも出来ます。</p>
<p>デジタル ID の発行は、いくつかの認証機関がおこなっています。 このダイアログの [ID を取得する] ボタンをクリックすると、認証機関の 1 つであるシマンテック社（旧ベリサイン）の Web ページにジャンプします。デジタル ID には、個人を証明するものから企業などの団体を証明するものまで、その用途によってさまざまです。</p>
<p>ここでは、図面ファイルの改ざんを検出する目的で、シマンテックの Web サイトから個人を証明する Cass1 認証の体験版デジタル ID を取得して、AutoCAD で利用する方法をご紹介します。体験版の デジタル ID と言っても、無償で25日間利用することが出来るので、その機能を十分にご理解いただけるはずです。</p>
<p>まず、シマンテックの <a href="http://www.symantec.com/ja/jp/page.jsp?id=pki-class1-trial" target="_blank"><strong>こちら</strong></a> のページ内容を参照してみてください。ID の申請は英語のページでおこなうことになりますが、その手順や入力項目の説明が記載されているので安心です。内容を確認したら、ページに記載された手順に沿って、Web ページへの入力を進めてください。入力すべき項目は、実質、Email アドレスだけです。最後に Submit ボタンをクリックする、申請時に入力した Email アドレス宛に2通のメールが届くはずです。それらの内容から、デジタル ID をお使いのコンピュータにインストールします。もちろん、この手順も先のページで日本語で説明されています。</p>
<p>無事、デジタル ID のインストールが完了すると、Web ブラウザでインストールされた証明書を確認することが出来ます。次のスクリーンショットは、Internet Explorer で表示した証明書です。</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c6fea860970b-pi" style="display: inline;"><img alt="Class1_digital_id_on_ie" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b7c6fea860970b image-full img-responsive" src="/assets/image_961939.jpg" style="display: block; margin-left: auto; margin-right: auto;" title="Class1_digital_id_on_ie" /></a></p>
<p>この状態で AutoCAD を起動して、<a href="http://help.autodesk.com/cloudhelp/2015/JPN/AutoCAD-Core/files/GUID-2199A941-E183-4CAC-914C-E4538468DE64.htm" target="_blank">SECURITYOPTIONS[セキュリティオプション] コマンド</a>で&#0160;[セキュリティ オプション] ダイアログを開き、[デジタル署名] タブを表示させると、正しくデジタル ID が表示されてくるはずです。</p>
<p style="text-align: center;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c6ff346d970b-pi" style="display: inline;"><img alt="Digital_id_on_op" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b7c6ff346d970b image-full img-responsive" src="/assets/image_634039.jpg" title="Digital_id_on_op" /></a></p>
<p>これで、図面ファイルにデジタル署名を与える環境が整いました。それでは、実際に操作して図面に署名を加えてましょう。図面を保存する前に、[セキュリティ オプション] ダイアログの [デジタル署名] タブにある「図面を保存した後にデジタル署名をアタッチ」にチェックしてから、署名情報のタイムスタンプの選択し、必要に応じてコメントを記入します。これで図面ファイルを保存すると、その図面ファイルにデジタル署名がアタッチされます。</p>
<p>デジタル署名がアタッチされた図面を開くと、次のようなダイアログが開いて、アタッチされたデジタル署名が有効であることを示します。このダイアログは、同時に、デジタル署名のアタッチ後に図面ファイルへの編集・改ざんがおこなわれていないことを示しています。</p>
<p style="text-align: center;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c6feb99f970b-pi" style="display: inline;"><img alt="Digi_sig_valid" class="asset  asset-image at-xid-6a0167607c2431970b01b7c6feb99f970b img-responsive" src="/assets/image_925779.jpg" style="width: 420px;" title="Digi_sig_valid" /></a></p>
<p>デジタル署名がアタッチされた図面を少しでも編集して保存すると、図面が改ざんされたと判断されてます。そのような図面ファイルを開いた際には、デジタル署名が無効であることを示すダイアログが表示されます。</p>
<p style="text-align: center;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c6feb98b970b-pi" style="display: inline;"><img alt="Digi_sig_invalid" class="asset  asset-image at-xid-6a0167607c2431970b01b7c6feb98b970b img-responsive" src="/assets/image_930086.jpg" style="width: 420px;" title="Digi_sig_invalid" /></a></p>
<p>ここまでの流れを動画にしていますのでご覧下さい。なお、図面を開いた際に表示されるダイアログは、<strong>システム変数&#0160;SIGWARN</strong> が <strong>1</strong> （既定値）に設定されている場合に表示されます。また、図面ファイルでのデジタル署名の有無や、その正当性は<a href="http://help.autodesk.com/cloudhelp/2015/JPN/AutoCAD-Core/files/GUID-C28D46C3-4C3D-4D37-86B9-E0D235F82EC6.htm" title="図面ファイルにアタッチされたデジタル署名に関する情報を表示します。">SIGVALIDATE[デジタル署名検証]</a>&#0160;コマンドでいつもでチェックすることが出来ます。</p>
<p style="text-align: center;"><iframe allowfullscreen="" frameborder="0" height="281" src="http://www.youtube.com/embed/jS8bRxrR-Bw?feature=oembed" width="500"></iframe>&#0160;&#0160;</p>
<p>さて、AutoCAD API を利用してデジタル署名を設定する処理は、パスワード保護を設定する場合と似ています。AutoCAD .NET API でデジタル署名を設定して図面ファイルを保存する方法については、次のドキュメントをご参照ください。&#0160;</p>
<p style="padding-left: 30px;"><strong><span class="asset  asset-generic at-xid-6a0167607c2431970b01bb08b4a400970d img-responsive"><a href="http://adndevblog.typepad.com/files/qa-9315.pdf" target="_blank">QA-9315 AutoCAD .NET API でデジタル署名を設定するには ?</a></span></strong></p>
<p style="padding-left: 30px;"><strong><span class="asset  asset-generic at-xid-6a0167607c2431970b01b8d19a0669970c img-responsive"><a href="http://adndevblog.typepad.com/files/qa-9316.pdf" target="_blank">QA-9316 AutoCAD .NET API でデジタル署名が設定されているかチェックする方法は ?</a></span></strong></p>
<p>By Toshiaki Isezaki</p>
<p>&#0160;</p>
