---
layout: "post"
title: "Forge Online Training - Design Automation Inventor 収録公開"
date: "2022-10-12 01:15:32"
author: "Takehiro Kato"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2022/10/forge-online-training-design-automation-inventor-materials.html "
typepad_basename: "forge-online-training-design-automation-inventor-materials"
typepad_status: "Publish"
---

<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02a2eed7c914200d-pi" style="display: inline;"><img alt="image from adndevblog.typepad.com" border="0" class="asset  asset-image at-xid-6a025d9b32eb0b200c02acc60f6b59200b image-full img-responsive" src="/assets/image_762189.jpg" title="image from adndevblog.typepad.com" /></a></p>
<p>2022 年 10月 5日に、オートデスクがクラウド上の仮想環境に用意した Inventor コアエンジンを利用して、Inventorファイルの作成や編集、情報の収集などの処理を自動化する Design Automation API for Inventor を把握いただくオンライン トレーニングを開催しました。</p>
<p>当日は、補足も交え、Learn Forge コンテンツを利用しています。Web サーバー実装に Node.js と Forge SDK 使って Forge アプリを構築しています。</p>
<p>使用したプレゼンテーション資料（PDF ファイル）は、次のリンクからダウンロードすることが出来ます。ページ中には後日参照可能な数多くのリンクが埋め込まれていますので、このページに記載した収録動画とともにご確認ください。</p>
<p><a href="https://adndevblog.typepad.com/files/forge-training---design-automation-autocad.pdf" rel="noopener" target="_blank"></a><a href="https://adndevblog.typepad.com/files/forge-training---design-automation-inventor.pdf">Forge Training - Design Automation Inventorをダウンロード</a></p>
<p>&#0160;</p>
<p><strong>前提：</strong></p>
<ul>
<li>実習では Node.js と VS Code を利用します。次のブログ記事を事前にご確認の上、必要となるツールや環境のインストールをお願いします。<br /><a href="https://adndevblog.typepad.com/technology_perspective/2017/01/development-environment-for-forge.html">Forge の開発環境 - Technology Perspective from Japan (typepad.com)</a></li>
<li>Forgeのデベロッパキー（Client Id と Client Secret）の取得が必要です。また、キーの取得には、Autodesk ID が必要となります。Autodesk ID、アクセスキーをお持ちでない場合には、次のブログ記事に沿 って、それらを事前に取得しておくようお願いします。<a href="https://adndevblog.typepad.com/technology_perspective/2016/07/app-registration-and-getting-keys-for-forge-api.html">Forge API を利用するアプリの登録とキーの取得 - Technology Perspective from Japan (typepad.com)</a></li>
<li>HTML、CSS、JavaScript、RESTful API の概要を把握されていると理解が深まります。もし、Web 開発が初めての場合には、<a href="https://developer.mozilla.org/ja/docs/Learn/Getting_started_with_the_web">ウェブ入門 - ウェブ開発を学ぶ | MDN (mozilla.org)</a>、<a href="https://adndevblog.typepad.com/technology_perspective/2022/02/need-to-know-before-forge-development.html">Forge 開発に際して... - Technology Perspective from Japan (typepad.com)</a> をご一読いただくことをお勧めします。</li>
<li>Inventor アドインの開発経験がない方は、下記のブログ記事で公開している「<a href="https://adndevblog.typepad.com/technology_perspective/2022/05/autodesk-inventor-2023-api_trainingmaterial.html">Autodesk Inventor 2023 の APIトレーニングマテリアル</a>」からトレーニングマテリアルを取得し、演習を実施いただけると理解が深まります。</li>
</ul>

<!--more-->

<p><strong>はじめに</strong></p>
<p><iframe allowfullscreen="" frameborder="0" height="270" src="//www.youtube.com/embed/-YXtbzDXd4Y" width="480"></iframe></p>
<p>&#0160;</p>
<p><strong>Design Automation APIの理解</strong></p>
<p><a href="https://nam11.safelinks.protection.outlook.com/?url=https%3A%2F%2Fwww.youtube.com%2Fwatch%3Fv%3DItg_BJMsTbk&amp;data=05%7C01%7Ctakehiro.kato%40autodesk.com%7Cbaa0ca8b4c7242177fdb08daab56570a%7C67bff79e7f914433a8e5c9252d2ddc1d%7C0%7C0%7C638010684549617817%7CUnknown%7CTWFpbGZsb3d8eyJWIjoiMC4wLjAwMDAiLCJQIjoiV2luMzIiLCJBTiI6Ik1haWwiLCJXVCI6Mn0%3D%7C3000%7C%7C%7C&amp;sdata=k9W9RVoqFpdizlNwijEVXwINc%2FgfETgMOvSDcrW3%2BaE%3D&amp;reserved=0"><iframe allowfullscreen="" frameborder="0" height="270" src="//www.youtube.com/embed/Itg_BJMsTbk" width="480"></iframe></a></p>
<p>&#0160;</p>
<p><strong>サーバの作成</strong></p>
<p><a href="https://nam11.safelinks.protection.outlook.com/?url=https%3A%2F%2Fwww.youtube.com%2Fwatch%3Fv%3DhuiQqc3Rw58&amp;data=05%7C01%7Ctakehiro.kato%40autodesk.com%7Cbaa0ca8b4c7242177fdb08daab56570a%7C67bff79e7f914433a8e5c9252d2ddc1d%7C0%7C0%7C638010684549617817%7CUnknown%7CTWFpbGZsb3d8eyJWIjoiMC4wLjAwMDAiLCJQIjoiV2luMzIiLCJBTiI6Ik1haWwiLCJXVCI6Mn0%3D%7C3000%7C%7C%7C&amp;sdata=UBO0DZjAS292bdqJ5PhnjqwL8tJm8nMq0tVgUcmmscU%3D&amp;reserved=0"><iframe allowfullscreen="" frameborder="0" height="270" src="//www.youtube.com/embed/huiQqc3Rw58" width="480"></iframe></a></p>
<p>&#0160;</p>
<p><strong>基本アプリのUI</strong></p>
<p><a href="https://nam11.safelinks.protection.outlook.com/?url=https%3A%2F%2Fwww.youtube.com%2Fwatch%3Fv%3DmqYd2_MkiLE&amp;data=05%7C01%7Ctakehiro.kato%40autodesk.com%7Cbaa0ca8b4c7242177fdb08daab56570a%7C67bff79e7f914433a8e5c9252d2ddc1d%7C0%7C0%7C638010684549617817%7CUnknown%7CTWFpbGZsb3d8eyJWIjoiMC4wLjAwMDAiLCJQIjoiV2luMzIiLCJBTiI6Ik1haWwiLCJXVCI6Mn0%3D%7C3000%7C%7C%7C&amp;sdata=IU08fXA6aWMt8h3kwua0CPFhxmeHFla6bX6crz%2FIIQw%3D&amp;reserved=0"><iframe allowfullscreen="" frameborder="0" height="270" src="//www.youtube.com/embed/mqYd2_MkiLE" width="480"></iframe></a></p>
<p>&#0160;</p>
<p><strong>プラグインを準備する</strong></p>
<p><a href="https://nam11.safelinks.protection.outlook.com/?url=https%3A%2F%2Fwww.youtube.com%2Fwatch%3Fv%3Dsiws3nsoqMY&amp;data=05%7C01%7Ctakehiro.kato%40autodesk.com%7Cbaa0ca8b4c7242177fdb08daab56570a%7C67bff79e7f914433a8e5c9252d2ddc1d%7C0%7C0%7C638010684549617817%7CUnknown%7CTWFpbGZsb3d8eyJWIjoiMC4wLjAwMDAiLCJQIjoiV2luMzIiLCJBTiI6Ik1haWwiLCJXVCI6Mn0%3D%7C3000%7C%7C%7C&amp;sdata=a4BSpKO%2FHcJqeiflF1LOgtJdB%2BVcTuoUQVSZNt4aU2g%3D&amp;reserved=0"><iframe allowfullscreen="" frameborder="0" height="270" src="//www.youtube.com/embed/siws3nsoqMY" width="480"></iframe></a></p>
<p>&#0160;</p>
<p><strong>AppBundleActivityの作成、WorkItemを実行</strong></p>
<p><a href="https://nam11.safelinks.protection.outlook.com/?url=https%3A%2F%2Fwww.youtube.com%2Fwatch%3Fv%3Dv_DrOJjLx4c&amp;data=05%7C01%7Ctakehiro.kato%40autodesk.com%7Cbaa0ca8b4c7242177fdb08daab56570a%7C67bff79e7f914433a8e5c9252d2ddc1d%7C0%7C0%7C638010684549617817%7CUnknown%7CTWFpbGZsb3d8eyJWIjoiMC4wLjAwMDAiLCJQIjoiV2luMzIiLCJBTiI6Ik1haWwiLCJXVCI6Mn0%3D%7C3000%7C%7C%7C&amp;sdata=pNBkRYI4kO3a0Lljh9RjAf2JNiA2h2q%2FRy%2FQEPaILPw%3D&amp;reserved=0"><iframe allowfullscreen="" frameborder="0" height="270" src="//www.youtube.com/embed/v_DrOJjLx4c" width="480"></iframe></a></p>
<p>&#0160;</p>
<p><strong>Forge Design AutomationでiLogicを実行する</strong></p>
<p><a href="https://nam11.safelinks.protection.outlook.com/?url=https%3A%2F%2Fwww.youtube.com%2Fwatch%3Fv%3Df5frNzADZyk&amp;data=05%7C01%7Ctakehiro.kato%40autodesk.com%7Cbaa0ca8b4c7242177fdb08daab56570a%7C67bff79e7f914433a8e5c9252d2ddc1d%7C0%7C0%7C638010684549617817%7CUnknown%7CTWFpbGZsb3d8eyJWIjoiMC4wLjAwMDAiLCJQIjoiV2luMzIiLCJBTiI6Ik1haWwiLCJXVCI6Mn0%3D%7C3000%7C%7C%7C&amp;sdata=sTpQYwsBd0aaUoyDDru8%2B5aHajFskgv3To8qidLnsxs%3D&amp;reserved=0"><iframe allowfullscreen="" frameborder="0" height="270" src="//www.youtube.com/embed/f5frNzADZyk" width="480"></iframe></a></p>
<p>&#0160;</p>
<p><strong>コストについて</strong></p>
<p><a href="https://nam11.safelinks.protection.outlook.com/?url=https%3A%2F%2Fwww.youtube.com%2Fwatch%3Fv%3DPpVRqvSx-OE&amp;data=05%7C01%7Ctakehiro.kato%40autodesk.com%7Cbaa0ca8b4c7242177fdb08daab56570a%7C67bff79e7f914433a8e5c9252d2ddc1d%7C0%7C0%7C638010684549617817%7CUnknown%7CTWFpbGZsb3d8eyJWIjoiMC4wLjAwMDAiLCJQIjoiV2luMzIiLCJBTiI6Ik1haWwiLCJXVCI6Mn0%3D%7C3000%7C%7C%7C&amp;sdata=KjuXLJ%2BgvpqBWkD%2Bds62bAs62Z%2BTQLkbKML%2BP3JNSxs%3D&amp;reserved=0"><iframe allowfullscreen="" frameborder="0" height="270" src="//www.youtube.com/embed/PpVRqvSx-OE" width="480"></iframe></a></p>
<p>By Takehiro Kato</p>
