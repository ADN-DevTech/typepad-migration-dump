---
layout: "post"
title: "AutoCAD OEM について"
date: "2014-09-24 00:21:55"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2014/09/about-autocad-oem.html "
typepad_basename: "about-autocad-oem"
typepad_status: "Publish"
---

<p><strong><a href="http://adndevblog.typepad.com/technology_perspective/2014/07/whta-is-trusteddwg.html" rel="noopener" target="_blank">TrustedDWG</a></strong> を説明するページに <a href="http://www.autodesk.co.jp/dwg" rel="noopener" target="_blank">http://www.autodesk.co.jp/dwg</a> があります。その説明の中に、このような記述があると思います。</p>
<div>
<p style="padding-left: 30px;"><strong>TrustedDWG とは</strong><br />TrustedDWG は、オートデスク製品または RealDWG で作成された、信頼できる DWG ファイルです。たとえば TrustedDWG を AutoCAD で開くと、画面右下に TrustedDWG のアイコンが表示され他社のソフトウェアで作成された DWG ファイルと判別ができます。<br /><br />オートデスクが開発した DWG テクノロジーは、AutoCAD や AutoCAD ベースの製品の中核を成すファイル形式であり、AutoCAD や AutoCAD ベースの製品で作成したデザインデータを保存するための効率的かつ正確で信頼できる手段です。DWG ファイルは多くの設計業界で流通しているので、 DWG ファイル形式は世界で最も利用されているデザインデータ形式といえます。<br /><br />AutoCAD 2007 以降の AutoCAD には TrustedDWG のチェック機能が搭載されています。<span style="background-color: #ffff00;">オートデスク製や RealDWG のライセンスを受けたデベロッパーが開発したアプリケーション以外で作成された DWG ファイルを開く際、注意を促すメッセージが表示されます。</span></p>
</div>
<p>ここには RealDWG という SDK にしか言及がありませんが、オートデスクが 3rd Party デベロッパにライセンス提供する開発ツールには、もう 1 つ&#0160;<strong>AutoCAD OEM</strong> があります。今回は、この AutoCAD OEM が具体的にどのようなものなのか、簡単のご紹介しておきます。</p>
<p style="text-align: center;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d06eab9d970c-pi" style="display: inline;"><img alt="Autocad-oem-2015-badge-1024px" class="asset  asset-image at-xid-6a0167607c2431970b01b8d06eab9d970c img-responsive" src="/assets/image_744816.jpg" title="Autocad-oem-2015-badge-1024px" /></a></p>
<p>AutoCAD OEM は、第三者によるカスタマイズが前提となる、オートデスクが開発した CAD ツールキットで、CAD エンジン とも評されます。オートデスク自身が開発しているので、その機能的な内容は、一般に市販されている AutoCAD と 98% 程度共通です。</p>
<p><strong>OEM</strong>（Original Equipment Manufacturer）という言葉をご存じの方も多いと思います。日本語では「相手先ブランド名製造」などのように紹介されるのが一般的のようです。実際に製品を製造したメーカー ブランドではなく、3rd Party（第三者）、つまり、相手先のメーカー名（ブランド名）で製品が出荷される、という解釈が分かり易いと思います。AutoCAD OEM に当てはめると、AutoCAD OEM という CAD エンジンを開発したオートデスクが、3rd Party デベロッパに CAD エンジンを提供して、そのデベロッパが付加価値を付けて、デベロッパの会社名で CAD 製品を販売する、といった説明になるかと思います。</p>
<p>AutoCAD OEM は、オートデスク自らも利用しています。その代表例が、DWG ビューワである DWG TrueView と AutoCAD LT です。DWG TrueView は、AutoCAD から作図や編集機能の一切を削除した状態で提供されている無償ツールです。また、AutoCAD LT の場合は、AutoCAD から 3D 機能や API カスタマイズの機能を削除した 2D 専用の CAD ソフトウェアです。このように、AutoCAD OEM を利用すると、AutoCAD の持つ機能を削除して、別の製品に仕立てることが出来ます。</p>
<p style="text-align: center;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d06eac52970c-pi" style="display: inline;"><img alt="What_is_aoem" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b8d06eac52970c image-full img-responsive" src="/assets/image_318954.jpg" title="What_is_aoem" /></a></p>
<p>もちろん、3rd Party が固有の機能、つまり、アドイン アプリケーションやメニュー カスタマイズを施して、製品を販売するようなことも可能です。AutoCAD のカスタマイズと異なるのは、AutoCAD OEM 製の 3rd Party 製品では、アドインが CAD 本体に組み込まれて、一体製品として販売することが出来るという点です。別途、AutoCAD を購入する必要はありません。&#0160;</p>
<p style="text-align: center;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d06eaca7970c-pi" style="display: inline;"><img alt="Differences" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b8d06eaca7970c image-full img-responsive" src="/assets/image_343127.jpg" title="Differences" /></a></p>
<p>AutoCAD OEM 特有のカスタマイズ項目も存在します。CAD の起動時に表示されるスプラッシュ スクリーンや起動アイコン、ウィンドウ タイトルといったブランディングに関わるすべてのリソースを変更することが出来ます。これによって、AutoCAD に非常によく似た別の CAD 製品を開発、販売することが出来るようになります。</p>
<p style="text-align: center;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb0789c157970d-pi" style="display: inline;"><img alt="Oem_customization" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01bb0789c157970d image-full img-responsive" src="/assets/image_439729.jpg" title="Oem_customization" /></a></p>
<p style="text-align: center;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d06eb29a970c-pi" style="display: inline;"><img alt="Resource_customize" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b8d06eb29a970c image-full img-responsive" src="/assets/image_773786.jpg" title="Resource_customize" /></a></p>
<p>実際に開発する際には、OEM MakeWizard とうツールでスケルトン ソフトウェアの作成を自動的に作成できます。もし、AutoCAD 用に開発した ObjectARX や .NET API、AutoLISP などのアドイン資産があれば、OEM MakeWizard を用いてアドインを OEM 製品に組み込むことが出来ます。</p>
<p style="text-align: center;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb0789c768970d-pi" style="display: inline;"><img alt="Oem_makewizard" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01bb0789c768970d image-full img-responsive" src="/assets/image_175487.jpg" title="Oem_makewizard" /></a></p>
<p>AutoCAD OEM にアドインを組み込む場合には、AutoCAD OEM 専用の ObjectARX SDK を利用したり、<strong>バインド</strong> という処理で、アドインを OEM 製品専用にする必要があります。OEM 製の 3rd Party 製品（CAD ソフトウェア）には、この処理を経たアドインしかロードすることが出来ないようになっています。市販の AutoCAD 用アドインを、完成した OEM 製品に流用することは出来ません。</p>
<p>少し記述が古くなりますが、<a href="http://www.autodesk.co.jp/developoem" rel="noopener" target="_blank"><strong>http://www.autodesk.co.jp/developoem</strong></a> には事例も記載されていますので、興味お持ちの方は、ぜひ、ご参照ください。</p>
<p>もう 1 点、AutoCAD OEM にはライセンス システムはありません。AutoCAD をはじめとしたオートデスク製品には、FLEXnet Publisher というライセンス システムが組み込まれていますが、類似した仕組みは 3rd Party 側で独自開発するなどの工夫が必要になります。</p>
<p>注意していただきたいのは、AutoCAD OEM のライセンス提供には、事前にオートデスクの審査が必要となる点です。AutoCAD OEM が AutoCAD や AutoCAD LT の競合製品を開発する能力を持っているため、というのがその理由です。また、価格も、販売計画などのビジネス プランや、AutoCAD OEM の使用機能の数によって変化するよう設定されています。</p>
<p>このように、AutoCAD OEM は、開発者にとって非常に魅力的な開発ツールキットです。ただし、オートデスクや販売店が、一般ユーザに販売されることはありません。販売は、<strong><a href="https://www.techsoft3d.com/jp/" rel="noopener" target="_blank">Tech Soft 3D</a>&#0160;</strong>社が一括して担当しています。詳細をお知りになりたい場合には、まずは、Tech Soft 3D 社にコンタクトしてみてください。</p>
<p>By Toshiaki Isezaki</p>
