---
layout: "post"
title: "Inventor 2022 新機能～ その3"
date: "2021-04-25 20:01:00"
author: "Takehiro Kato"
categories:
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2021/04/inventor-2022-whats-new-part3.html "
typepad_basename: "inventor-2022-whats-new-part3"
typepad_status: "Publish"
---

<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0278802344df200d-pi" style="display: inline;"><img alt="Autodesk-inventor-badge-1024" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0278802344df200d image-full img-responsive" src="/assets/image_852887.jpg" title="Autodesk-inventor-badge-1024" /></a></p>
<p>&#0160;</p>
<p>前回の記事に引き続きInventor 2022の新機能をご紹介したいと思います。</p>
<p>&#0160;</p>
<p><strong><span style="text-decoration: underline;"><span style="font-size: 15pt;">アセンブリ</span></span></strong></p>
<ul>
<li>新しい「簡易化」コマンド</li>
</ul>
<p>既存の「シュリンクラップ」の機能が強化され「簡略化」という名称になりました。また、「シュリンクラップ」と「代替をシュリンクラップ」の2つのコマンドが「簡易化」コマンドに統合されるとともに、プリセットに対応したため、簡略化の設定を保存し再利用がをすることが出来ます。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b026bdecd4dfc200c-pi" style="display: inline;"><img alt="図8" class="asset  asset-image at-xid-6a0167607c2431970b026bdecd4dfc200c img-responsive" src="/assets/image_955901.jpg" title="図8" /></a></p>
<p>&#0160;</p>
<p>また、出力形式では、「新しいパーツ」「代替」に加えて「Revitファイル」出力が追加されました。</p>
<p>さらに、フィーチャ認識が改善および拡張され、深さでポケットをフィルタし、エンボス加工されたフィーチャ、トンネル、およびキャビティ(空間)を除去できるようになりました。</p>
<p>&#0160;</p>
<ul>
<li>拘束の機能強化</li>
</ul>
<p>&#0160;&#0160;&#0160;&#0160;拘束状態を、ブラウザに、[●]完全に拘束されている状態、[○]完全拘束されていない状態、[－]不明な状態（再構築が必要）で表示し、拘束状況を把握できるようになりました。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b026bdecd4e7f200c-pi" style="display: inline;"><img alt="図9" class="asset  asset-image at-xid-6a0167607c2431970b026bdecd4e7f200c img-responsive" src="/assets/image_370218.jpg" title="図9" /></a></p>
<p>&#0160;</p>
<ul>
<li>インスタンスプロパティ</li>
</ul>
<p>アセンブリには、同じコンポーネントを複数配置することが出来ます。それらはすべて同じコンポーネントを元に作成されており、各インスタンス毎に異なる情報、例えばiPropertyなど、を付加することが出来ませんでした。</p>
<p>&#0160;</p>
<p>Inventor2022で追加された、インスタンスにプロパティ機能により、個々のコンポーネントに情報を付加できるようになりました。このプロパティは、親アセンブリに格納され、元となるコンポーネントファイルには影響しません。</p>
<p>&#0160;</p>
<p>インスタンス プロパティは、カスタム iProperty をオーバーライドすることができ、バルーン、引出線注記、パーツ一覧などの注記で使用できます。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b026bdecd4eb9200c-pi" style="display: inline;"><img alt="図10" class="asset  asset-image at-xid-6a0167607c2431970b026bdecd4eb9200c img-responsive" src="/assets/image_843311.jpg" title="図10" /></a></p>
<p>&#0160;</p>
<ul>
<li>代替の更新</li>
</ul>
<p>軽量化のために代替表示を利用した場合、従来は代替部品が最新の状態になっているか「更新を確認」する操作が必要でしたが、Inventor2022では「代替部品」が最新ではない場合に、更新マークが表示され状況がわかるようになりました。</p>
<p><img src="/assets/GUID-76C0E89F-C8BF-4917-BF73-2C6AF9C382B7.png" /></p>
<p>&#0160;</p>
<ul>
<li>チューブ＆パイプの機能強化</li>
</ul>
<p>1 つまたは複数の配管を&#0160;<span class="ph xterm" id="GUID-06B6A572-3DC4-4085-AD7A-02093E8D544B__GUID-7BBCB2FE-D252-4577-AD7D-BF7DDE4E4766"><a class="xref" href="https://help.autodesk.com/view/INVNTOR/2022/JPN/?guid=GUID-BF5A33C3-D5AF-4AFA-A19B-25F1644DBCBA#GUID-BF5A33C3-D5AF-4AFA-A19B-25F1644DBCBA__WS73099CC142F48755F31EC6111B3CB055B-3D1B" shape="rect">ISOGEN.pcf ファイル</a>&#0160;</span>に保存するときに、角度属性がエクスポートされるようになりました。</p>
<p><img src="/assets/GUID-893BBF3F-F9D5-4FAF-A65D-902CF796C9A2.png" /></p>
<p>&#0160;</p>
<p><strong><span style="text-decoration: underline;"><span style="font-size: 15pt;">パーツ</span></span></strong></p>
<p>&#0160;</p>
<ul>
<li>フィレットのプロパティパネル化</li>
</ul>
<p>フィレット コマンドがプロパティ パネル に移動し、[フィレット]、[面フィレット]、[フルラウンド フィレット]の 3 つのコマンドに分割されました。</p>
<p>2021でのフィレットは、読み込み時に各フィレットタイプにマイグレーションされます。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b026bdecd4f01200c-pi" style="display: inline;"><img alt="図11" class="asset  asset-image at-xid-6a0167607c2431970b026bdecd4f01200c img-responsive" src="/assets/image_574170.jpg" title="図11" /></a></p>
<p>&#0160;</p>
<p>選択ツールはプロパティパネルから独立したツールパレットになり、ドラッグすることで使いやすい位置に配置できるようになりました。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0263e9a007fb200b-pi" style="display: inline;"><img alt="図12" class="asset  asset-image at-xid-6a0167607c2431970b0263e9a007fb200b img-responsive" src="/assets/image_563475.jpg" title="図12" /></a></p>
<p>&#0160;</p>
<p>&#0160;</p>
<p><strong><span style="text-decoration: underline;"><span style="font-size: 15pt;">図面</span></span></strong></p>
<p>&#0160;</p>
<p><iframe allowfullscreen="" frameborder="0" height="270" src="//www.youtube.com/embed/NujMpmHhPQ8" width="480"></iframe></p>
<p><span style="font-size: 8pt;">※日本語字幕付きの動画は<a href="https://l.facebook.com/l.php?u=https%3A%2F%2Fautodesk.wistia.com%2Fmedias%2F1i2isnsx9n%3Ffbclid%3DIwAR2mzM0H8BYWQLx6quHDiSG1GW-Y7057iI_xFDfYoKiZg_3ENKITrsG6BM4&amp;h=AT0WpE8wNEkckluRydCBWbqkAqDS66gRUt3wNqgMiOsjQsjCDWkTwAebrR4E7HgL-XWhi-QS_kRRIoxQd0oWRQ7fARH7DmGGPwC_8hzyX1wtYi2gWoyQeREU0kRSILad3-8B&amp;__tn__=-UK-R&amp;c[0]=AT2xRe2Q3sOrbTgpZZbqOtNSdeN32zw0ACgZnGnGCmfoKeYNjwkWGS0KbhJWOJmrbZVIe6wZ0_ON9hbVlIZvmnoIB2dxDapjMVC1FgSsCzn1pWG8hdD1XMWxHSrVBPajwYDuLrGzUTvnFwVqG93CSTs67Z39gkRnwyt1nUT6HdWoM6c">こちら</a>をご参照ください</span></p>
<p>&#0160;</p>
<ul>
<li>陰影付きの図面ビューの機能強化</li>
</ul>
<p>陰影付きの図面ビューでは、モデルのアクティブな照明スタイルが使用されるようになりました。</p>
<p><img src="/assets/GUID-FD2C0CF6-B4DF-4089-8AFD-96281FE671BC.gif" /></p>
<p>&#0160;</p>
<ul>
<li>図面ビューのオプション</li>
</ul>
<p>デザイン ビューのオプションが追加され、カメラ ビューと 3D 注記を抽出できるようになりました。</p>
<p><img src="/assets/GUID-CE9563A8-9DB1-4D48-B184-82E3265AFFDB.png" /></p>
<p>&#0160;</p>
<ul>
<li>寸法の機能強化</li>
</ul>
<p>寸法と交差する場合、補助線を使用して中心線と中心マークが分割されるようになりました。</p>
<p>この機能強化により、寸法値が読み取りやすくなります。</p>
<p><img src="/assets/GUID-9358A94B-A1AE-43DC-B3D9-BC22437DC2BC.gif" /></p>
<p>&#0160;</p>
<p>&#0160;</p>
<p>いかがでしたでしょうか。次回も引き続き、Inventor 2022の新機能をご紹介したいと思います。</p>
<p>&#0160;</p>
<p>By Takehiro Kato</p>
