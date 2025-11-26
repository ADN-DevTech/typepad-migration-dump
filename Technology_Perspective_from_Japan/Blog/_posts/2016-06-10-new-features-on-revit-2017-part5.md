---
layout: "post"
title: "Revit 2017 の新機能 ～ その5"
date: "2016-06-10 01:53:46"
author: "Ryuji Ogasawara"
categories:
  - "API カスタマイズ"
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2016/06/new-features-on-revit-2017-part5.html "
typepad_basename: "new-features-on-revit-2017-part5"
typepad_status: "Publish"
---

<p>これまで4回にわたり、Revit 2017 の新機能をご紹介してまいりました。</p>
<p><iframe allowfullscreen="" frameborder="0" height="281" src="https://www.youtube.com/embed/qskNcgGPCeQ?feature=oembed" width="500"></iframe>&#0160;</p>
<p>今回は、Revit アドインのセキュリティ強化を目的とした新機能「デジタル署名」について解説いたします。</p>
<p><strong><span style="font-size: 14pt;">アドインのデジタル署名</span></strong><br />Revit 2017 から、実行ファイルまたはアドインをアプリケーションにロードしようとすると、そのアドインのセキュリティ証明書がデジタル署名とともにチェックされるようになりました。</p>
<p>デジタル署名は、特定のファイルに追加される暗号化された署名情報です。作成者を識別し、デジタル署名が適用された後にファイルが変更されていないかどうかを確認します。</p>
<p>たとえば、アドインに悪意のあるプログラムが追加されたり、ウイルスやマルウェア等に感染した場合など、アドインに何らかの変更が加わった際に、アプリケーションの起動時にそれを検知して、ユーザーにセキュリティ警告を促す仕組みです。</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d1f55623970c-pi" style="display: inline;"><img alt="Security_dialog_1" class="asset  asset-image at-xid-6a0167607c2431970b01b8d1f55623970c img-responsive" src="/assets/image_193992.jpg" title="Security_dialog_1" /></a></p>
<ul>
<li>[常にロード]<br /> ファイルは信頼できる既知の発行元から送信されています。これ以降、このファイルまたはこの発行元によって署名されているその他のファイルは、この警告が表示されずにロードされます。<br /><br /></li>
<li>[1 回だけロードする]<br />現在のファイルを信頼してすぐにロードします。今後ファイルを再びロードする場合、警告は再表示されます。<br /><br /></li>
<li>[ロードしない]<br />ファイルをロードする要求をキャンセルします。</li>
</ul>
<p>AutoCAD 2016 では既にこの仕組みは導入されており、アドイン アプリケーション開発者への要求事項として、アドインにデジタル署名を施すことを推奨しております。</p>
<p><strong>AutoCAD 2016 のセキュリティとアドインのデジタル署名</strong><br /><a href="http://adndevblog.typepad.com/technology_perspective/2015/08/security-on-autocad-2016-and-digital-signature-to-addin.html">http://adndevblog.typepad.com/technology_perspective/2015/08/security-on-autocad-2016-and-digital-signature-to-addin.html</a></p>
<p>このデジタル署名を識別することで、信頼できる認証局(CA)によって発行されたルート証明書まで遡って信頼性を確認することができます。</p>
<p>&#0160;</p>
<p><span style="font-size: 14pt;"><strong>デジタル署名の作成手順</strong></span><br />アドインへのデジタル署名の方法は、Autodesk 独自の方法ではなく、一般的なデジタル署名の方法となります。詳細な解説は下記の開発者用ガイドをご参照ください。</p>
<p><strong>Revit アドインにデジタル署名を追加する</strong><br /><a href="http://help.autodesk.com/view/RVT/2017/JPN/?guid=GUID-6D11F443-AC95-4B5B-A896-DD745BA0A46">http://help.autodesk.com/view/RVT/2017/JPN/?guid=GUID-6D11F443-AC95-4B5B-A896-DD745BA0A46</a></p>
<p>まずはじめに、アドインに署名を行うためには、サード パーティの証明機関 (CA) からデジタル証明書を取得していただく必要がございます。</p>
<p>デジタル証明書を、 Certificate (cer) または、 Personal Information Exchange (pfx) ファイル形式で取得いたしましたら、署名ツールを利用して、アドイン DLLにデジタル署名を行います。</p>
<p>署名ツールは、Visual Studio インストール時に同梱されている Microsoft 社のコマンドラインツール SignTool.exe を利用することができます。</p>
<p><strong>SignTool.exe (署名ツール)</strong><br /><a href="https://msdn.microsoft.com/ja-jp/library/8s9b9yaz(v=vs.110).aspx">https://msdn.microsoft.com/ja-jp/library/8s9b9yaz(v=vs.110).aspx</a></p>
<p>&#0160;</p>
<p><span style="font-size: 14pt;"><strong>テスト・社内使用のためのデジタル署名</strong></span><br />アドイン開発時のテストや、社内使用のために独自のデジタル証明書を作成することもできます。</p>
<p>デジタル証明書をサード パーティの証明機関から取得するのではなく、Microsoft 社のツールを利用してテスト用の自己署名証明書を作成し、Windows 証明書ストアにデジタル証明書を追加することで、セキュリティ警告ダイアログの表示を回避することができます。</p>
<p><strong>テストや社内使用のために独自の証明書を作成する</strong><br /><a href="http://help.autodesk.com/view/RVT/2017/JPN/?guid=GUID-B9A067F4-234F-47F8-A5EE-0D84A93FA98E">http://help.autodesk.com/view/RVT/2017/JPN/?guid=GUID-B9A067F4-234F-47F8-A5EE-0D84A93FA98E</a></p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d1f5561e970c-pi" style="display: inline;"><img alt="Digitally_signed" class="asset  asset-image at-xid-6a0167607c2431970b01b8d1f5561e970c img-responsive" src="/assets/image_256083.jpg" title="Digitally_signed" /></a></p>
<p>アドイン開発者の方は、ぜひデジタル署名の導入をご検討ください。</p>
<p>By Ryuji Ogasawara</p>
