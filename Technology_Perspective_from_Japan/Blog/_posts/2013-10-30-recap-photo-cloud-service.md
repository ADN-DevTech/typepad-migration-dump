---
layout: "post"
title: "ReCap 360 クラウドサービス（旧名 ReCap Photo）"
date: "2013-10-30 03:17:21"
author: "Toshiaki Isezaki"
categories:
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2013/10/recap-photo-cloud-service.html "
typepad_basename: "recap-photo-cloud-service"
typepad_status: "Publish"
---

<p>今回は Autodesk ReCap 360（旧名 ReCap Photo）というクラウド サービスを紹介しましょう。Autodesk ReCap 360 サービスは、デジタルカメラを使い、可能な範囲で対象物を上下左右、周囲 360 度撮影した複数の画像を利用して、3D モデルを作成するサービスです、生成された 3D モデルは、メッシュモデルとしてダウンロードしたり、点群データとしてダウンロードして、デスクトップ製品などで再利用することが可能です。</p>
<p>Autodesk ReCap 360 サービスには、<strong><a href="http://recap360.autodesk.com" target="_blank">http://recap360.autodesk.com</a>&#0160;</strong>からアクセスして、どなたでも無償で体験いただくことが出来ます。ただし、現段階ではまだ正式なものではないので、残念ながら表示される Web ページの内容は英語のみになります。また、このサービスもオートデスクのクラウド サービスである Autodesk 360 の 1 つなので、利用の際には無償で取得可能な<a href="http://tech.autodesk.jp/faq/faq/adsk_result_dd.asp?QA_ID=6625" target="_blank">&#0160;<strong>Autodesk ID</strong></a>&#0160;を用いてサービスにログインする必要があります。</p>
<p>サービスを提供するページでは、表示や操作に、以前、<strong><a href="http://adndevblog.typepad.com/technology_perspective/2013/03/autodesk-360-%E3%81%8C%E4%BD%BF%E7%94%A8%E3%81%99%E3%82%8B%E3%83%86%E3%82%AF%E3%83%8E%E3%83%AD%E3%82%B8.html" target="_blank">Autodesk 360 が使用するテクノロジとコンテンツ</a></strong> で紹介した&#0160;WebGL を使うため、アクセスの際には WebGL が利用できる Web ブラウザを使っていただく必要があります（推奨ブラウザは、<strong><a href="https://www.google.co.jp/url?sa=t&amp;rct=j&amp;q=&amp;esrc=s&amp;source=web&amp;cd=1&amp;cad=rja&amp;ved=0CDQQFjAA&amp;url=https%3A%2F%2Fwww.google.com%2Fintl%2Fja%2Fchrome%2F&amp;ei=zo1jUpSUCInOkQX-nYGwAw&amp;usg=AFQjCNHmW7pSEcLCdQKO7rQ8Y0LfgOD_YQ&amp;bvm=bv.54934254,d.dGI" target="_blank">Google Chrome</a></strong> になっています）。</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b019b0029a73a970d-pi" style="float: right;"><img alt="ReCapPhotoWeb" border="0" class="asset  asset-image at-xid-6a0167607c2431970b019b0029a73a970d image-full" src="/assets/image_245175.jpg" style="margin: 0px 0px 5px 5px;" title="ReCapPhotoWeb" /></a></p>
<p>&#0160;</p>
<p>利用方法は至って簡単です。 まず、3D モデル化したい対象物をデジタルカメラで撮影しなければなりません。ReCap Photo は、いわば、写真画像を立体合成するテクノロジであるため、撮影時には出来るだけ周囲360度からの撮影を心がける必要があります。</p>
<p>対処物が白く陰影が分かりにくかったり、鏡面仕上げで周囲の状況が反射しているもの、あるいは透明な素材で構成されたものなどは、残念ながら、画像合成できない場合がありますので注意してください。また、あまり広角にならず、対象物がフレームに収まるようにしてください。ちょうど、撮影方法を説明している動画が YouTube で公開されていますので、そちらをご参照ください。</p>
<p style="text-align: center;"><iframe frameborder="0" height="281" src="http://www.youtube.com/embed/RYM7uZeiXH0?feature=oembed" width="500"></iframe>&#0160;</p>
<p>写真を撮影するのが面倒な方には、ログイン後に表示される画面から [example] タイルをクリックすると、撮影済のデータをダウンロードすることができます。この画像を使って、Autodesk ReCap Photo で 3D モデルの生成をお試しいただくことも可能です。</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b019b0029c288970d-pi" style="display: inline;"><img alt="Example" border="0" class="asset  asset-image at-xid-6a0167607c2431970b019b0029c288970d image-full" src="/assets/image_188829.jpg" title="Example" /></a><br />このサンプルからダウンロードした Warrior を使って 3D モデルを生成してみましょう。モデルの生成は、クラウドならではの演算サービスに該当するもので、クライアントコンピュータで実施するとすれば、相応の時間を必要とするはずです。</p>
<p>画面左に表示される [new project] をクリックして、新しいプロジェクトを始めてみましょう。プロジェクト名とともに、生成された 3D モデルをダウンロードする際のファイル形式も、ここで指定しておく必要があります。</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b019b002968ee970c-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="ExportFile" class="asset  asset-image at-xid-6a0167607c2431970b019b002968ee970c" src="/assets/image_182513.jpg" style="width: 430px; display: block; margin-left: auto; margin-right: auto;" title="ExportFile" /></a></p>
<p>[next] で次の画面に移動すると、[Add Files] の画面が表示されます。ここで [drag photo here] 領域にドラッグ&amp;ドロップ操作で撮影した画像ファイル（ここではダウンロードした画像ファイル）をすべてアップロード指定します。[Preview] ページで不要な画像ファイルを除外したり、不足気味の部分の画像ファイルを指定することもできます。</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b019b0029d41b970d-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="Preview" class="asset  asset-image at-xid-6a0167607c2431970b019b0029d41b970d" src="/assets/image_234684.jpg" style="width: 430px; display: block; margin-left: auto; margin-right: auto;" title="Preview" /></a><br />あとは [next] で生成された結果を通知するメールを待つだけです。メールが届いたら、指定したプロジェクトが画面上に表示されているはずです。そのタイルをクリックすることで、Web ブラウザ上で3D モデルの状態を確認することが出来ます。</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b019b00296d98970c-pi" style="display: inline;"><img alt="GeneratedProject" border="0" class="asset  asset-image at-xid-6a0167607c2431970b019b00296d98970c image-full" src="/assets/image_602529.jpg" title="GeneratedProject" /></a></p>
<p>ReCap Photo 上でのモデルの確認には、WebGL をサポートした Web ブラウザが必要です。前述のとおり、この機能に対応している Google Chrome が推奨されていますが、Firefox でも利用できるようです。</p>
<p style="text-align: center;"><iframe allowfullscreen="" frameborder="0" height="281" src="http://www.youtube.com/embed/klYzWresRqQ?feature=oembed" width="500"></iframe>&#0160;</p>
<p>ただし、あまり高い品質で生成した場合には、メッシュ数が多くなって Web ブラウザ上での表示は無理なケースもあるようです。そのような場合には、プロジェクト生成時に指定したファイル形式で、3D モデルファイルをダウンロードして、デスクトップ製品で確認したり、場合によっては修正して 2 次的に再利用します。</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b019b0029de01970d-pi" style="display: inline;"><img alt="Download" border="0" class="asset  asset-image at-xid-6a0167607c2431970b019b0029de01970d" src="/assets/image_922376.jpg" style="display: block; margin-left: auto; margin-right: auto;" title="Download" /></a><br />ダウンロードしたファイルは各ファイル形式をサポートするデスクトップ製品で表示したり、編集したりすることが出来ます。Autodesk&#0160;Mudbox などメッシュモデルを編集できる機能があれば、メッシュ編集後に必要な箇所だけを抽出して、エンターテイメント系のソフトウェアで別のシーンに挿入して再利用できるはずです。</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b019b0069b67e970c-pi" style="display: inline;"><img alt="Mudbox" border="0" class="asset  asset-image at-xid-6a0167607c2431970b019b0069b67e970c image-full" src="/assets/image_634278.jpg" title="Mudbox" /></a><br /><br /></p>
<p style="text-align: center;"><iframe frameborder="0" height="344" src="http://www.youtube.com/embed/6BQ9ZkVJoGk?feature=oembed" width="459"></iframe>&#0160;</p>
<p>CAD 系のソフトウェアであれば、<strong><a href="http://adndevblog.typepad.com/technology_perspective/2013/03/autocad-2014-%E3%81%AE%E6%96%B0%E6%A9%9F%E8%83%BD-%E3%81%9D%E3%81%AE-1.html" target="_blank">以前</a></strong>、紹介した Autodesk ReCap などで点群範囲を編集して、AutoCAD、Revit、Inventor などの製品にインポートして再利用できます。次の動画は、ReCap 上での表示範囲の編集の模様を録画したものです。</p>
<p style="text-align: center;">&#0160;<iframe frameborder="0" height="344" src="http://www.youtube.com/embed/oVCEWGY9jps?feature=oembed" width="459"></iframe>&#0160;</p>
<p style="text-align: left;">ReCap での編集と指定領域のエクスポート後、設定した領域だけを別の形式でファイル出力して AutoCAD にインポートしていた模様が次の動画です。</p>
<p style="text-align: center;"><iframe allowfullscreen="" frameborder="0" height="281" src="http://www.youtube.com/embed/NfuphkVTKRg?feature=oembed" width="500"></iframe>&#0160;</p>
<p style="text-align: left;">このように、Autodesk 360 の演算処理サービスには、レンダリング画像生成やシミュレーション以外にも、ユニークなサービスが存在しています。この機会にお試しください。</p>
<p style="text-align: left;">By Toshiaki Isezaki</p>
<p>&#0160;</p>
