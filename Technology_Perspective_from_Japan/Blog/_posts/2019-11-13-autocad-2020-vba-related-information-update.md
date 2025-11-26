---
layout: "post"
title: "AutoCAD 2020 VBA 関連情報アップデート"
date: "2019-11-13 00:01:56"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2019/11/autocad-2020-vba-related-information-update.html "
typepad_basename: "autocad-2020-vba-related-information-update"
typepad_status: "Publish"
---

<p>AutoCAD 2020 の VBA 開発環境で、2 点ほど情報がありますので、この場でご案内しておきたいと思います。</p>
<hr />
<p><strong>VBA エディタからの日本語オンライン ヘルプ参照について</strong></p>
<p style="padding-left: 40px;"><a href="https://adndevblog.typepad.com/technology_perspective/2019/08/docs-reference-from-autocad-2020-vba-editor.html" rel="noopener" target="_blank"><strong>AutoCAD 2020 VBA エディタからのオンラインヘルプ参照</strong></a> でご案内した問題、「VBA エディタのオブジェクト ブラウザ上で調べたい AutoCAD オブジェクトなどを選択、 F1 キーや、<img border="0" src="/assets/image_165579.jpg" /> ボタンをクリックして表示される AutoCAD の ActiveX リファレンスが英語ドキュメントになってしまう。」の更新情報です。</p>
<p style="padding-left: 40px;">先のブログ記事で回避策をご紹介していましたが、最近リリースされた AutoCAD 2020.1.1 Update で修正されています。いまのところリリースノートでは言及されていないようですが、先のブログ記事で手動で回避策を施さなくても、<strong>AutoCAD 2020.1.1 Update</strong> と <strong><a href="http://www.autodesk.com/vba-download" rel="noopener noreferrer" target="_blank">&#0160;http://www.autodesk.com/vba-download</a></strong> からダウンロード可能な <strong>VBA Enabler</strong>、別名：VBA エディタ（<a href="http://download.autodesk.com/us/support/files/autocad/vba-module/autocad_2020_1_1_acvbainstaller_win_64bit_dlm.sfx.exe">autocad_2020_1_1_acvbainstaller_win_64bit_dlm.sfx.exe</a>）の<span style="text-decoration: underline;"><strong>両者を</strong></span>適用することで、VBA エディタからのアクションで日本語 ActiveX リファレンスを表示させることが出来るようになります。</p>
<p style="padding-left: 40px;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a4c2916e200d-pi" style="display: inline;"><img alt="Ja_documents" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0240a4c2916e200d image-full img-responsive" src="/assets/image_570539.jpg" title="Ja_documents" /></a></p>
<p style="padding-left: 40px;">既に VBA Enabler をインストールされている場合は、アンインストール後、<strong><a href="http://www.autodesk.com/vba-download" rel="noopener noreferrer" target="_blank">http://www.autodesk.com/vba-download</a></strong> からダウンロードした新しい VBA Enabler をインストールしてください。なお、コントロールパネルの [プログラムと機能] の [Autodesk AutoCAD 2020 VBA Enabler] で確認できる新しい VBA Enabler のバージョンは、<strong>23.1.103.0</strong>&#0160;とになります。もし、この値が <strong>23.1.47.0</strong> と表示される場合には、AutoCAD 2020 のリリース時に提供を開始した古い VBA Enabler がインストールされていることを意味します。</p>
<p style="padding-left: 40px;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a4c60946200d-pi" style="display: inline;"><img alt="Vba_enabler_version" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0240a4c60946200d image-full img-responsive" src="/assets/image_993529.jpg" title="Vba_enabler_version" /></a></p>
<p style="padding-left: 40px;">AutoCAD 2020.1.1 Update は、AutoCAD デスクトップアプリの マイ アップデート からか、Autodesk Accounts（<strong><a href="https://accounts.autodesk.com/">https://accounts.autodesk.com/</a></strong>）ページから、製品とダウンロードを管理 &gt;&gt; 製品に更新 リンクから入手することが出来ます。</p>
<p style="padding-left: 40px;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a4c3ee9e200d-pi" style="display: inline;"><img alt="Downloads" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0240a4c3ee9e200d image-full img-responsive" src="/assets/image_849679.jpg" title="Downloads" /></a></p>
<hr />
<p><strong>タイプライブラリ名について</strong></p>
<p style="padding-left: 40px;">何名かの方から、「AutoCAD を何度インストールしても、インストールされるタイプライブラリが AutoCAD 2019 Type Library になってしまう。」と質問を受けました。</p>
<p style="padding-left: 40px;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a4995867200c-pi" style="display: inline;"><img alt="Vba" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0240a4995867200c image-full img-responsive" src="/assets/image_680861.jpg" title="Vba" /></a></p>
<p style="padding-left: 40px;">確認をしたところ、AutoCAD 2020 はタイプライブラリを acax23enu.tlb のファイル名で C:\Program Files\Common Files\Autodesk Shared フォルダにインストールするものの、タイプライブラリ名の「変更ミス」があったため、VBA エディタなどに表示される表示名が AutoCAD 2019 Type Library のまま製品がリリースされてしまった、ということがわかりました。</p>
<p style="padding-left: 40px;">このため、このまま利用し続けても問題なく、AutoCAD 2020 の ActiveX オートメーション（COM API）をお使いいただくことが出来ますので、ご安心ください。</p>
<hr />
<p>By Toshiaki Isezaki</p>
