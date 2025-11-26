---
layout: "post"
title: "AutoCAD 2020 VBA エディタからのオンラインヘルプ参照"
date: "2019-08-26 00:03:48"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2019/08/docs-reference-from-autocad-2020-vba-editor.html "
typepad_basename: "docs-reference-from-autocad-2020-vba-editor"
typepad_status: "Publish"
---

<p>AutoCAD 2020 日本語版をインストール後に VBA エディタを<strong><a href="https://knowledge.autodesk.com/ja/support/autocad/downloads/caas/downloads/downloads/JPN/content/download-the-microsoft-visual-basic-for-applications-module-vba.html" rel="noopener" target="_blank">ダウンロード</a></strong>、インストールして、VBA エディタで開発をすすめる際、オブジェクト ブラウザ上で調べたい AutoCAD オブジェクトなどを選択して F1 キーを押したり、<img border="0" src="/assets/image_165579.jpg" /> ボタンをクリックすると、AutoCAD の ActiveX リファレンスを開くことが出来ます。</p>
<p>ただし、AutoCAD 2020 では、この操作で表示されるオンライン ヘルプが英語版のオンライン ヘルプになってしまっていました。</p>
<p><img alt="Vba-enu-help" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0240a4a3c493200d image-full img-responsive" src="/assets/image_227163.jpg" title="Vba-enu-help" /></p>
<p>これは、製品インストーラを構成する上での問題から決定された仕様変更なのですが、次の手順で日本語版のオンライン ヘルプをインストールすると、同様の操作で日本語版のオンライン ヘルプを表示させることが出来るようになります。</p>
<ol>
<li><strong><a href="https://adndevblog.typepad.com/files/acadauto.zip" rel="noopener" target="_blank">こちら</a></strong> から acadauto.zip ファイルをダウンロード</li>
<li>ダウンロードした ZIP ファイルを任意の場所に含まれる acad_aag.chm と acadauto.chm ファイルを展開</li>
<li>Windows エクスプローラーで C:\Program Files\Common Files\Autodesk Shared フォルダを表示して、acad_aag.chm と acadauto.chm ファイルを見つけてバックアップ（ファイル名を変更したり、別のフォルダにコピー）</li>
<li>2. で展開した&#0160;acad_aag.chm ファイルと acadauto.chm ファイルを C:\Program Files\Common Files\Autodesk Shared フォルダに移動</li>
</ol>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a4c86fc1200b-pi" style="display: inline;"><img alt="Vba-jpn-help" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0240a4c86fc1200b image-full img-responsive" src="/assets/image_709659.jpg" title="Vba-jpn-help" /></a></p>
<p>なお、ZIP ファイルから展開したオンライン ヘルプを開いた際、ウィンドウ右のコンテンツ領域が真っ白で何も表示されない場合には、ファイルのプロパティから「許可する」にチェックを入れて、ロックを解除するようにしてください。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a4a3cbb1200d-pi" style="display: inline;"><img alt="Property" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0240a4a3cbb1200d img-responsive" src="/assets/image_338087.jpg" title="Property" /></a></p>
<p>当初あったインストーラ上の課題も解決出来るようなので、この問題は、将来の AutoCAD 2020 Update で修正されるものと思います。ご不便おかけして申し訳ございません。</p>
<p>By Toshiaki Isezaki</p>
