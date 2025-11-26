---
layout: "post"
title: "Suite 製品のカスケーディング ライセンス ～ その2"
date: "2013-10-16 00:51:38"
author: "Toshiaki Isezaki"
categories:
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2013/10/cascading-license-part2.html "
typepad_basename: "cascading-license-part2"
typepad_status: "Publish"
---

<p><strong><a href="http://adndevblog.typepad.com/technology_perspective/2013/10/cascading-license-part1.html" target="_blank">前回</a></strong> は、ネットワーク ライセンスを運用している環境で、Suite 製品と単体製品を1つのライセンス マネージャで管理した際の カスケーディング ライセンス について、その基本的な考え方をご紹介しました。</p>
<p>今回は、カスケーディング ライセンスを利用する環境で期待したようにライセンスが利用できない、といった問題を起こさないように、カスケーディング ライセンス固有の <strong>ライセンス共有</strong> の振る舞いについてご紹介します。まずは、同一バージョン同士のライセンスを結合した基本的な振る舞いから説明しましょう。&#0160;</p>
<p><strong>最新バージョン利用時のライセンス共有</strong></p>
<p>ここでは、AutoCAD 2014 1 ライセンスと AutoCAD Design Suite 2014 Premium エディション 1 ライセンスのライセンス ファイルを結合して運用していると仮定します。AutoCAD Design Suite 2014 Premium エディションには、AutoCAD 2014、AutoCAD Raster Design 2014、SketchBook Designer 2014、Showcase 2014、Mudbox 2014、3ds Max Design 2014 の製品が同梱されていて、ライセンス ファイル上では 1 &#0160;つのフィーチャコードで管理されています。この状態で、2 つのクライアント コンピュータには、AutoCAD Design Suite 2014 Premium エディションがインストールされています。この条件下では、ライセンス共有 の振る舞いは次のようなかたちで実行されます。</p>
<ol>
<li>クライアント コンピュータ A で AutoCAD Design Suite に含まれる AutoCAD 2014 を起動すると、ライセンス マネージャがプールするライセンスから、最初に AutoCAD 2014 のライセンスが消費されます。</li>
<li>同じクライアント コンピュータ A で AutoCAD Design Suite に含まれる 3ds Max Design 2014 を起動すると、ライセンス マネージャがプールするライセンスから、AutoCAD Design Suite 2014 Premium エディション のライセンスが消費されます。</li>
<li>ここで、クライアント コンピュータ A 上で実行されている AutoCAD 2014 は、同じコンピュータ上で実行されている&#0160;AutoCAD Design Suite 2014 Premium エディションにも含まれていることがライセンス マネージャによって 2 分以内に判定されます。つまり、同じコンピュータ上での実行なので、AutoCAD 2014 も&#0160;3ds Max Design 2014 も&#0160;AutoCAD Design Suite 2014 Premium エディションのライセンスで実行可能です。この判定によって、不要となる AutoCAD 2014 単体のライセンスは、ライセンス マネージャに返却されます。この振る舞いが<strong> ライセンス共有</strong> と呼ばれる機能です。</li>
<li>次に、クライアント コンピュータ B で AutoCAD Design Suite に含まれる AutoCAD 2014 を起動すると、クライアント コンピュータ A から返却されている AutoCAD 2014 のライセンスが消費されることになります。</li>
</ol>
<p style="text-align: center;">&#0160;<iframe frameborder="0" height="344" src="http://www.youtube.com/embed/CKjL_tITcyI?feature=oembed" width="459"></iframe>&#0160;</p>
<p>このようにライセンス共有は、所有するライセンスを合理的に運用するための振る舞いということができます。ライセンス共有の機能は既定のもので、この機能を無効にいしたりすることが出来ません。</p>
<p>続いて、Subscription 契約をしているライセンス運用を考えてみます。Suite 製品や単体製品などの区別にかかわらず、Subscription 契約を締結いただいたライセンスには、最新バージョンに加えて過去 3 バージョン前までの同一製品の使用権限が、<a href="http://www.autodesk.co.jp/subscription/licensing" target="_blank">Subscription 特典として付与</a>&#0160;されます。例えば、AutoCAD 2014 をお持ちで Subscription 契約を締結いただいた場合には、AutoCAD 2013、2012、2011 バージョンの使用権限が提供されることになります。このとき、旧バージョンは、Subscription センター（Web サイト）からダウンロードで入手していただくことになります。</p>
<p>この考え方は Suite 製品でも同様ですが、Suite 製品は日本では 2012 シリーズから導入されたパッケージ製品ですが、米国で Autodesk Design Suite 2011 が販売されてたたため、各バージョン用にフィーチャコードが用意されています。この場合、Subscription 契約を持つ AutoCAD Design Suite 2014 Premium エディションのライセンス ファイルには、AutoCAD Design Suite 2014 Premium のフィーチャ コードと Autodesk Design Suite 2013 Premium、Autodesk Design Suite 2012、 Autodesk Design Suite 2011&#0160;の各フィーチャコードが埋め込まれた状態っで発行されることになります。</p>
<table align="left" border="0" cellpadding="0" cellspacing="0" class="auto-style3" style="margin-left: 4.85pt; margin-right: 4.85pt; width: 100%; background: #f2f2f2;">
<tbody>
<tr style="mso-yfti-irow: 0; mso-yfti-firstrow: yes; mso-yfti-lastrow: yes;">
<td style="width: 435.1pt; padding: 0mm 5.4pt 0mm 5.4pt;" valign="top" width="580">
<p class="auto-style6" style="mso-margin-top-alt: auto; mso-margin-bottom-alt: auto; mso-pagination: widow-orphan; layout-grid-mode: char; mso-element: frame; mso-element-frame-hspace: 7.1pt; mso-element-wrap: around; mso-element-anchor-horizontal: margin; mso-element-top: 26.3pt; mso-height-rule: exactly;">SERVER isezakt-vaio 4025C2E644E8<br />USE_SERVER<br />VENDOR adskflex port=2080<br />PACKAGE 85690DSPRM_F adskflex 1.000 COMPONENTS=&quot;<strong>86119DSPRM_2014_0F</strong> \<br /><strong>85969DSPRM_2013_0F 85784DSPRM_2012_0F 85655DSPRM_2011_0F</strong>&quot; \<br />OPTIONS=SUITE SUPERSEDE ISSUED=29-Sep-2013 SIGN=&quot;0772 6E40 \<br />D648 5425 826B 2ED6 F64E 336A 0B41 AD77 7AA3 8855 4ABE 5D87 \<br />5E05 0F10 7E63 E95B 93B5 8899 112B 6D3E 5F47 3EDD 543F C24C \<br />F184 2022 CE6F D23F&quot; SIGN2=&quot;0DA4 31F0 EA1E ACDF 18B6 23FA 000F \<br />E91C 7290 890D C9A8 9E01 3B94 F58C AE8C 1584 E1F1 3973 06DC \<br />AAF4 218F 439B 1FA5 E451 FAF5 08CE C83D 414D 50A6 94C2&quot;<br />INCREMENT 85690DSPRM_F adskflex 1.000 permanent 2 \<br /> VENDOR_STRING=commercial:permanent SUPERSEDE DUP_GROUP=UH \<br /> SUITE_DUP_GROUP=UHV ISSUED=29-Sep-2013 BORROW=4320 \<br />SN=399-99999966 SIGN=&quot;0790 7443 536A 810C F008 E4C5 0038 1EAC \<br />3DD1 95A6 731C 381A 2962 AD2D 4BA0 137B C4BA C06B C4A7 94A3 \<br />13FC 1D77 AF33 4C8C 7482 3510 33CD A757 A752 245D&quot; SIGN2=&quot;053B \<br />64F0 6D69 FE15 25F6 51DF 5AD9 9B46 E532 6B37 F44C A4F9 A0F2 \<br />8F0B 1C41 1817 F1BA 9377 4D30 CF4E 66F5 9DB8 7D53 432F 35A8 \<br />01D3 75B4 BF8D 28E9 3A10&quot;</p>
</td>
</tr>
</tbody>
</table>
<p>なお、AutoCAD Design Suite 以外の Design Suite 製品で、旧バージョンに該当する Design Suite 製品が存在していない場合には、その Design Suite 2014 Premium に含まれている製品で 2011 バージョン時の単体製品のフィーチャ コードが埋め込まれて発行されるようになります。</p>
<p>さて、本題に戻りましょう。ここで、旧バージョンの使用権限を行使したカスケーディング ライセンスの運用を考えてみます、つまり、ライセンス共有がどのように実施されるのか、とうのが論点となります。&#0160;</p>
<p><strong>旧バージョン利用時のライセンス共有できないケース</strong></p>
<p>結論から言ってしまうと、Subscription 特典を利用して旧 3 バージョンまでの使用権限を持つライセンスを運用する場面では、旧バージョン利用時のライセンス共有は利用できません。</p>
<p>ここでは、それぞれ Subscription 契約を持つ AutoCAD 2014 1 ライセンスと、AutoCAD Design Suite 2014 Premium エディション 2 ライセンスのライセンス ファイルを結合して運用していると仮定します。</p>
<p>前述のとおり、AutoCAD Design Suite 2014 Premium エディションには、AutoCAD 2014、AutoCAD Raster Design 2014、SketchBook Designer 2014、Showcase 2014、Mudbox 2014、3ds Max Design 2014 の製品が同梱されています。また、少しややこしいのですが、使用権限を持つ&#0160;Autodesk Design Suite 2013 Premium エディションには、AutoCAD 2013、AutoCAD Raster Design 2013、SketchBook Designer 2013、Showcase 2013、Mudbox 2013、3ds Max Design 2013 の各製品が、また、&#0160;Autodesk Design Suite 2012 Premium エディションには、AutoCAD 2012、SketchBook Designer 2012、Showcase 2012、Mudbox 2012、3ds Max Design 2012 の各製品がそれぞれ同梱されています。Autodesk Design Suite 2012 以前には、AutoCAD Raster Design は含まれていませんでした。更に、Autodesk Design Suite 2011 は日本では未発売でした。</p>
<p>この状態で、2 つのクライアント コンピュータには、AutoCAD Design Suite 2014 Premium エディションがインストールされています。また、1つのクライアント コンピュータには、AutoCAD Design Suite 2014 Premium エディションに加えて、プロジェクトの遂行で必要となっている&#0160;Autodesk Design Suite 2013 Premium エディションに含まれる AutoCAD 2013 がインストールされています。</p>
<p>実際の振る舞いは次のようになります。</p>
<ol>
<li>クライアント コンピュータ B で AutoCAD Design Suite 2014 に含まれる AutoCAD 2014 を起動すると、ライセンス マネージャがプールするライセンスから、最初に AutoCAD 2014 のライセンスが消費されます。</li>
<li>クライアント コンピュータ A で AutoCAD Design Suite 2014 に含まれる 3ds Max Design 2014 を起動すると、ライセンス マネージャがプールするライセンスから、&#0160;AutoCAD Design Suite 2014 のライセンスが消費されます。</li>
<li>続いて、同じクライアント コンピュータ A で Autodesk Design Suite 2013 に含まれる 3ds Max Design 2013 を起動します。最新バージョン同士では、ここでライセンス共有が発生しますが、起動したのは旧バージョンの 3ds Max Design 2013 であるため、代りに、ライセンス マネージャがプールするライセンスから、&#0160;AutoCAD Design Suite&#0160;2014 のライセンスが消費されることになります。</li>
</ol>
<p style="text-align: center;"><iframe frameborder="0" height="344" src="http://www.youtube.com/embed/oBMYLR6RgfY?feature=oembed" width="459"></iframe>&#0160;</p>
<p>ライセンス共有が適用されるケースは、Subscription の加入有無にかかわらず、最新バージョンでのカスケーディング ライセンス利用時と考えてください。</p>
<p>このように、Subscription 契約をお持ちで、旧バージョン 3 世代までの使用権限を行使しながらの運用を検討されている場合には、最大でいくつのライセンスが利用できるのか、ライセンスに不足が発生しないのか、など、十分に検討していただくことをお勧めします。AutoCAD Design Suite 以外の Suite 製品については、次の Autodesk Knowledge Network をご一読いただくことをお勧めします。</p>
<p style="padding-left: 30px;"><strong><a href="https://knowledge.autodesk.com/ja/support/autocad/troubleshooting/caas/sfdcarticles/sfdcarticles/kA230000000u0j0.html" target="_blank">ADLM: 単体製品パッケージライセンスにおける旧バージョンライセンスの使用について</a></strong><br /><br /><strong><a href="https://knowledge.autodesk.com/ja/support/autocad/troubleshooting/caas/sfdcarticles/sfdcarticles/kA230000000tziU.html" target="_blank">ADLM: 共通の「旧バージョン」製品を含む2つ以上のSuiteライセンスを結合して管理している場合、どのライセンスが優先的に使用されますか？</a></strong>&#0160;<br /><br /><strong><a href="https://knowledge.autodesk.com/ja/support/autocad/troubleshooting/caas/sfdcarticles/sfdcarticles/kA230000000u0b0.html" target="_blank">サブスクリプションセンターにおける旧バージョン製品の入手方法</a></strong></p>
<p>By Toshiaki Isezaki</p>
<p>&#0160;</p>
