---
layout: "post"
title: "Forge DevCon Las Vegas 2018 アップデート: AU に見る Forge"
date: "2018-11-15 19:28:18"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2018/11/-forge-devcon-las-vegas-2018-forge-seen-in-au.html "
typepad_basename: "-forge-devcon-las-vegas-2018-forge-seen-in-au"
typepad_status: "Publish"
---

<p>Autodesk University Las Vegas 2018 も今日で 3 日間の会期を終えようとしています。ここでは、この 3 日で感じられた<em><strong> Forge の立ち位置</strong></em> についてレポートしておきたいと思います。</p>
<p>まず、11 月 13 日初日には General Keynote が、続いて 14 日には、午前に建設業向けに&#0160; Architecture Engineering And Construction Keynote が、午後には製造業向けに&#0160;Manufacturing And Product Design Keynote が開催されています。</p>
<p>これらの Keynote（基調講演）で伝えられたメッセージを簡単にまとめると「<strong>Automation（自動化）</strong>」になるかと思います。ちなみに、Autodesk University に 1 日先立って開催が始まった Forge DevCon Las Vegas のメッセージは、先のブログ記事&#0160;<a href="https://adndevblog.typepad.com/technology_perspective/2018/11/forge-devcon-las-vegas-2018-update.html" rel="noopener noreferrer" target="_blank"><strong>Forge DevCon Las Vegas 2018 アップデート</strong> </a>でお伝えしたように「<strong>Data at the Center</strong>」でした。</p>
<p>&#0160;<a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b022ad37a8996200c-pi" style="display: inline;"><img alt="Keywords" border="0" class="asset  asset-image at-xid-6a0167607c2431970b022ad37a8996200c image-full img-responsive" src="/assets/image_972473.jpg" title="Keywords" /></a></p>
<p>実は、<strong><a href="https://adndevblog.typepad.com/technology_perspective/2017/11/devcon-2017-forge-in-au2017-keynote.html" rel="noopener noreferrer" target="_blank">昨年の Autodesk University の General Keynote</a></strong> でも「<strong>Automation（自動化）</strong>」がキーワードとして挙げられています。各過程をシームレスに高度に連携させるには、議論対象のデータを中心となるクラウドに置いて、生産性向上のための自動化につなげていこう、という考えです。</p>
<p>Forge が基盤技術/プラットフォーム テクノロジであることを考えると、オートデスクが現在、引き続き目指すところは、設計/デザインから施工/製造の過程において、設計者、エンジニア、製造業者をシームレスにつなぐ高度な「<strong>Automation（自動化）</strong>」であり、それを実現するのが、クラウドの利用と、クラウド上のデータ プラットフォームを提供する Forge の「<strong>Data at the Center</strong>」であると捉えることが出来ます。</p>
<p>Automation/自動化の具体例については、既に YouTube で公開されている <strong><a href="https://www.youtube.com/watch?v=pgDTndsVzqY" rel="noopener noreferrer" target="_blank">動画</a></strong> を参照してみてください。 少子高齢化でも生産性向上を成し遂げている事例として、日本の<strong><a href="https://www.daiwahouse.co.jp/" rel="noopener noreferrer" target="_blank">大和ハウス工業株式会社</a></strong>が取り上げられていて、とても興味深く見ることが出来ます。</p>
<p class="asset-video"><iframe allow="accelerometer; autoplay; encrypted-media; gyroscope; picture-in-picture" allowfullscreen="" frameborder="0" height="281" src="https://www.youtube.com/embed/pgDTndsVzqY?feature=oembed" width="500"></iframe></p>
<p>なお、今回の動画は YouTube 上で公開されているので、YouTube 側の設定を変更すれば日本語字幕を表示させることが出来ます。試してみてください。妙な翻訳になってしまう場面もありますが、大筋で内容は把握いただけるものと思います。大和ハウス工業の自動化に言及が及ぶのは、<strong><em>48:50</em></strong>&#0160;を過ぎたあたりです。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b022ad37a8b95200c-pi" style="display: inline;"><img alt="Youtube_keynote" border="0" class="asset  asset-image at-xid-6a0167607c2431970b022ad37a8b95200c image-full img-responsive" src="/assets/image_382586.jpg" title="Youtube_keynote" /></a></p>
<p>印象的だったのは、Autodesk University の 3 つの Keynote のうち、どの Keynote でも、繰り返し&#0160; Forge が取り上げられていた点です。&#0160;</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b022ad3a0af80200d-pi" style="display: inline;"><img alt="Ketnotes" border="0" class="asset  asset-image at-xid-6a0167607c2431970b022ad3a0af80200d image-full img-responsive" src="/assets/image_127519.jpg" title="Ketnotes" /></a></p>
<p>もともと、Forge はオートデスクのクラウド サービスを開発する過程で生まれた機能を、機能別に Web サービス API として公開したものです（下図左）。この状態から、今後、Data at the Center を実現する&#0160;<strong><a href="https://adndevblog.typepad.com/technology_perspective/2017/11/consider-about-forge-hfdm.html" rel="noopener noreferrer" target="_blank">HFDM</a></strong>&#0160;を含む各種 SDK を先に開発し、オートデスク製品に活かしていこう（下図右）、という方向に Forge の立場が変わりつつあることがわかります。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b022ad3a0b316200d-pi" style="display: inline;"><img alt="Forge_direction" border="0" class="asset  asset-image at-xid-6a0167607c2431970b022ad3a0b316200d image-full img-responsive" src="/assets/image_408762.jpg" title="Forge_direction" /></a><br />Data at the Center の素材としての Forge はまだ開発途中であるため、今回は大々的に喧伝されませんでしたが、General Keynote でも登壇し、HFDM の早期評価デベロッパである Atkins 社が&#0160;<a href="https://autodeskuniversity.smarteventscloud.com/connect/sessionDetail.ww?SESSION_ID=229879" rel="noopener noreferrer" target="_blank">AS229879 - Sketch to feasability via HFDM </a>クラスで Data at the Center を実現する Forge App Framewok SDK について言及していました。これらを詳細にご案内できるには、もう少し時間がかかるようです。</p>
<p>視点は変わりますが、Autodesk University の展示会場 <em>EXPO</em> でも、今年は数多くの &quot;Forge&quot; を見ることが出来ました。<strong><a href="https://forge.autodesk.com/systemsintegrators" rel="noopener noreferrer" target="_blank">Forge System Integrator </a></strong>各社のブースや、System Integrator にはなっていないものの、<strong><a href="https://ja.wikipedia.org/wiki/ISV" rel="noopener noreferrer" target="_blank">ISV</a></strong> として Forge を使ったソリューションを提供している企業のブースです。&#0160;</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b022ad37a897c200c-pi" style="display: inline;"><img alt="Booths" border="0" class="asset  asset-image at-xid-6a0167607c2431970b022ad37a897c200c image-full img-responsive" src="/assets/image_978073.jpg" title="Booths" /></a></p>
<p>こちらは既存の Forge を使ったアプリケーション/ソリューションの展示ですが、BIM 360 との統合など、Forge がオートデスクのクラウド サービスとの連携カスタマイズのプラットフォームである点が認知され始めている証と思います。</p>
<p>By Toshiaki Isezaki</p>
