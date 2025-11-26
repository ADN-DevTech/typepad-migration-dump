---
layout: "post"
title: "Suite 製品のカスケーディング ライセンス ～ その1"
date: "2013-10-02 01:21:05"
author: "Toshiaki Isezaki"
categories:
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2013/10/cascading-license-part1.html "
typepad_basename: "cascading-license-part1"
typepad_status: "Publish"
---

<p>今日はネットワーク ライセンスで利用することが出来る カスケーディング ライセンスについて、その基本的な考え方について紹介していきましょう。カスケーディング ライセンスは、Suite 製品に含まれる製品を効率的に使用する目的で導入されたライセンスの取得方法です。カスケーディング ライセンスの前提となっているのは、ライセンス マネージャ上で複数製品のライセンスを管理するためにおこなう <strong><a href="http://tech.autodesk.jp/faq/faq/adsk_result_dd.asp?QA_ID=2876" target="_blank">ライセンス結合</a></strong> と呼ばれる作業です。</p>
<p>実際のカスケーディング ライセンスに話を移す前に、簡単にライセンス結合について説明しておきます。ここでは、シングル サーバー構成で AutoCAD 2014 を 1 ライセンス管理していたと仮定しましょう（実際には 1 ライセンスはあり得ませんが）。そこへ、AutoCAD Design Suite 2014 Premium エディションをネットワーク ライセンスで購入して、同じライセンス マネージャで管理する場合を考えてみます。ネットワーク ライセンスでは、サーバー上のライセンス マネージャが管理しているのはライセンス ファイルと呼ばれるファイルです。最初に管理していた AutoCAD 2014 のライセンス ファイルには、次のような情報が書き込まれています。</p>
<table align="left" border="0" cellpadding="0" cellspacing="0" class="auto-style3" style="margin-left: 4.85pt; margin-right: 4.85pt; width: 100%; background: #f2f2f2;">
<tbody>
<tr style="mso-yfti-irow: 0; mso-yfti-firstrow: yes; mso-yfti-lastrow: yes;">
<td style="width: 435.1pt; padding: 0mm 5.4pt 0mm 5.4pt;" valign="top" width="580">
<p class="auto-style6" style="mso-margin-top-alt: auto; mso-margin-bottom-alt: auto; mso-pagination: widow-orphan; layout-grid-mode: char; mso-element: frame; mso-element-frame-hspace: 7.1pt; mso-element-wrap: around; mso-element-anchor-horizontal: margin; mso-element-top: 26.3pt; mso-height-rule: exactly;"><span class="auto-style2" style="font-size: 10pt;">SERVER isezakt-vaio 4025C2E644E8</span><br class="auto-style2" /><span class="auto-style3" style="font-size: 10pt;"> <span class="auto-style5">USE_SERVER</span><br class="auto-style5" /> <span class="auto-style5">VENDOR adskflex port=2080</span><br class="auto-style5" /> <span class="auto-style5">INCREMENT <strong>86063ACD_2014_0F</strong> adskflex 1.000 permanent 1 \</span><br class="auto-style5" /><span class="auto-style5"> VENDOR_STRING=commercial:permanent SUPERSEDE DUP_GROUP=UH \</span><br class="auto-style5" /> <span class="auto-style5">ISSUED=23-Sep-2013 BORROW=4320 SN=399-99999966 SIGN=&quot;1B37 E06D \</span><br class="auto-style5" /> <span class="auto-style5">DF77 6FBE 65A0 EF16 5512 45CF 5DA1 E9DA 9270 4BE9 E723 BECD \</span><br class="auto-style5" /> <span class="auto-style5">DB82 0019 3BED 12D4 91BD C103 D6F8 3A1B 047E 4507 3F3F 326C \</span><br class="auto-style5" /> <span class="auto-style5">0AF1 9671 F4F8 1E86&quot; SIGN2=&quot;00F3 7045 D646 F408 2BE6 E424 B2C3 \</span><br class="auto-style5" /> <span class="auto-style5">AA8C 83B4 CB9A 038D B1E8 A26C 17E5 3893 1A8C 1412 27C4 37D7 \</span><br class="auto-style5" /> <span class="auto-style5">0F71 88AE 87AC 2586 6D26 59D2 EAF6 7D09 712C E1AF BEE5&quot;</span></span></p>
</td>
</tr>
</tbody>
</table>
<p>そこへ同じライセンス マネージャで AutoCAD Design Suite 2014 ライセンスを管理すべく、同じマシン名と MAC アドレス（物理アドレス）でオートデスクに依頼してライセンス ファイル発行を依頼して入手したとします。その際のライセンス ファイルは次のようになります。</p>
<table align="left" border="0" cellpadding="0" cellspacing="0" class="auto-style3" style="margin-left: 4.85pt; margin-right: 4.85pt; width: 100%; background: #f2f2f2;">
<tbody>
<tr style="mso-yfti-irow: 0; mso-yfti-firstrow: yes; mso-yfti-lastrow: yes;">
<td style="width: 435.1pt; padding: 0mm 5.4pt 0mm 5.4pt;" valign="top" width="580">
<p class="auto-style6" style="mso-margin-top-alt: auto; mso-margin-bottom-alt: auto; mso-pagination: widow-orphan; layout-grid-mode: char; mso-element: frame; mso-element-frame-hspace: 7.1pt; mso-element-wrap: around; mso-element-anchor-horizontal: margin; mso-element-top: 26.3pt; mso-height-rule: exactly;"><span style="font-size: 10pt;">SERVER isezakt-vaio 4025C2E644E8</span><br /><span style="font-size: 10pt;">USE_SERVER</span><br /><span style="font-size: 10pt;">VENDOR adskflex port=2080</span><br /><span style="font-size: 10pt;">INCREMENT <strong>86119DSPRM_2014_0F</strong> adskflex 1.000 permanent 1 \ VENDOR_STRING=commercial:permanent SUPERSEDE DUP_GROUP=UH \ ISSUED=23-Sep-2013 BORROW=4320 SN=399-99999966 SIGN=&quot;04AA D7E5 \ 0757 65F8 7AE0 C431 9DC0 2360 E94C 9681 62FA 0DF9 D422 73AF \ D44D 1522 E538 1E17 A6E6 2EB0 0CDA 5C7B EEC9 346E E81F 84A8 \ C23C 0808 5843 B873&quot; SIGN2=&quot;15BE B6ED F767 F80A 15E7 5AB0 98F1 \ F0EC FA2E 8CF8 A100 CA0A 9986 2D9E DE8D 07C9 1232 058A 874B \ 31C5 5DDE E96D 55EE 4E45 D4CD DED2 A897 739B 4A73 07CE&quot;</span></p>
</td>
</tr>
</tbody>
</table>
<p>2つのライセンス ファイルで<strong>太字</strong>で示したのが、個々の製品を示す <strong>フィーチャ コード</strong> と呼ばれるコードです。そして、この 2 つのライセンス ファイルを正しく結合したものが次のライセンス ファイルです。</p>
<table align="left" border="0" cellpadding="0" cellspacing="0" class="auto-style3" style="margin-left: 4.85pt; margin-right: 4.85pt; width: 100%; background: #f2f2f2;">
<tbody>
<tr style="mso-yfti-irow: 0; mso-yfti-firstrow: yes; mso-yfti-lastrow: yes;">
<td style="width: 435.1pt; padding: 0mm 5.4pt 0mm 5.4pt;" valign="top" width="580">
<p class="auto-style6" style="mso-margin-top-alt: auto; mso-margin-bottom-alt: auto; mso-pagination: widow-orphan; layout-grid-mode: char; mso-element: frame; mso-element-frame-hspace: 7.1pt; mso-element-wrap: around; mso-element-anchor-horizontal: margin; mso-element-top: 26.3pt; mso-height-rule: exactly;"><span style="font-size: 10pt;">SERVER isezakt-vaio 4025C2E644E8</span><br /><span style="font-size: 10pt;">USE_SERVER</span><br /><span style="font-size: 10pt;">VENDOR adskflex port=2080</span><br /><span style="font-size: 10pt;">INCREMENT <strong>86063ACD_2014_0F</strong> adskflex 1.000 permanent 1 \</span><br /><span style="font-size: 10pt;"> VENDOR_STRING=commercial:permanent SUPERSEDE DUP_GROUP=UH \</span><br /><span style="font-size: 10pt;"> ISSUED=23-Sep-2013 BORROW=4320 SN=399-99999966 SIGN=&quot;1B37 E06D \</span><br /><span style="font-size: 10pt;">DF77 6FBE 65A0 EF16 5512 45CF 5DA1 E9DA 9270 4BE9 E723 BECD \</span><br /><span style="font-size: 10pt;">DB82 0019 3BED 12D4 91BD C103 D6F8 3A1B 047E 4507 3F3F 326C \</span><br /><span style="font-size: 10pt;">0AF1 9671 F4F8 1E86&quot; SIGN2=&quot;00F3 7045 D646 F408 2BE6 E424 B2C3 \</span><br /><span style="font-size: 10pt;">AA8C 83B4 CB9A 038D B1E8 A26C 17E5 3893 1A8C 1412 27C4 37D7 \</span><br /><span style="font-size: 10pt;">0F71 88AE 87AC 2586 6D26 59D2 EAF6 7D09 712C E1AF BEE5&quot;</span><br /><br /><span style="font-size: 10pt;">INCREMENT <strong>86119DSPRM_2014_0F</strong> adskflex 1.000 permanent 1 \</span><br /><span style="font-size: 10pt;">VENDOR_STRING=commercial:permanent SUPERSEDE DUP_GROUP=UH \</span><br /><span style="font-size: 10pt;">ISSUED=23-Sep-2013 BORROW=4320 SN=399-99999966 SIGN=&quot;04AA D7E5 \</span><br /><span style="font-size: 10pt;">0757 65F8 7AE0 C431 9DC0 2360 E94C 9681 62FA 0DF9 D422 73AF \</span><br /><span style="font-size: 10pt;">D44D 1522 E538 1E17 A6E6 2EB0 0CDA 5C7B EEC9 346E E81F 84A8 \</span><br /><span style="font-size: 10pt;">C23C 0808 5843 B873&quot; SIGN2=&quot;15BE B6ED F767 F80A 15E7 5AB0 98F1 \</span><br /><span style="font-size: 10pt;">F0EC FA2E 8CF8 A100 CA0A 9986 2D9E DE8D 07C9 1232 058A 874B \</span><br /><span style="font-size: 10pt;">31C5 5DDE E96D 55EE 4E45 D4CD DED2 A897 739B 4A73 07CE&quot;</span></p>
</td>
</tr>
</tbody>
</table>
<p>これで、2 つの製品を 1 つのライセンス マネージャで管理することが出来るようになりました。それでは、実施の動きを見てみましょう。AutoCAD Design Suite 2014 Premium エディションには、AutoCAD、Showcase、Raster Design、Mudbox、SketchBook Designer、3ds Max Design の 6 つの製品が同梱されています。</p>
<p>ライセンス マネージャがインストールされているサーバー コンピュータには、3 つのクライアント コンピュータがネットワークで接続されていて、3 台ともに AutoCAD Design Suite 2014 Premium エディションがインストールされていると仮定します。</p>
<ol>
<li>1 台目のコンピュータで AutoCAD 2014 を起動すると、ライセンス マネージャがプールするライセンスから、最初に AutoCAD 2014 のライセンスが消費されます。</li>
<li>次に、2 台めのコンピュータで AutoCAD 2014 を実行すると、AutoCAD 2014 のライセンスは既に消費されているため、AutoCAD Design Suite 2014 のライセンスを使って AutoCAD 2014 が起動します。</li>
<li>続いて、3 台めのコンピュータで 3ds Max Design を起動しようとすると、AutoCAD Design Suite のライセンスは既に消費されているため、3ds Max Design を起動することは出来ません。</li>
<li>2 台めのコンピュータが AutoCAD 2014 をシャットダウンすると、AutoCAD Design Suite 2014 のライセンスが返却されて、3 台めの 3ds Max Design を起動できるようになります。</li>
</ol>
<p style="text-align: center;"><iframe frameborder="0" height="344" src="http://www.youtube.com/embed/3WJ8uYvEgI8?feature=oembed" width="459"></iframe>&#0160;</p>
<p>非常に単純な例ですが、これが基本的なカスケーディング ライセンスの考え方です。AutoCAD 2014 を起動する際に、単体の AutoCAD 2014 のライセンスから利用されているため、AutoCAD Design Suite 内に含まれる製品の利用率を高めることが出来ます。逆に AutoCAD 2014 を起動した際にAutoCAD Design Suite ライセンスから消費してしまうと、3ds Max Design などの製品利用率が下がってしまうことになります。</p>
<p>カスケーディング ライセンスの消費順序は、あらかじめオートデスクによって決められています。バージョンによって Suite 製品の構成が変化していることもあり、バージョン毎に順序が異なる場合があります。正確な情報は、次の Autodesk Knowledge Network を参照してみてください。</p>
<p><strong><a href="https://knowledge.autodesk.com/ja/support/autocad/troubleshooting/caas/sfdcarticles/sfdcarticles/kA230000000tzhe.html" target="_blank">ADLM: Suite製品に含まれるすべての製品についてライセンスのカスケーディング機能はサポートされますか ?</a></strong></p>
<p>カスケーディング ライセンスでは、基本的に価格帯の高い製品のライセンスを、最後に利用するように設計されています。ここでご紹介した AutoCAD 2014 と AutoCAD Design Suite 2014&#0160;Premium エディションの場合には、AutoCAD 2014 単体製品のほうが安価であるため、最初にライセンスが利用されるようになっているわけです。</p>
<p>さて、このトピックでは、最新バージョンである AutoCAD 2014 と AutoCAD Design Suite 2014 のライセンス結合時の動きを説明しましたが、実際にはもう少し複雑な利用環境が存在します。それが、Subscription 契約特典である過去3バージョンの使用権限を提供するパッケージ ライセンスを利用した際の問題です。これについては、別の機会に詳しくご紹介したいと思います。</p>
<p>By Toshiaki Isezaki</p>
<p>&#0160;</p>
