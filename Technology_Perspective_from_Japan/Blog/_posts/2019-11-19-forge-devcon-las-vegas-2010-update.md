---
layout: "post"
title: "Forge DevCon Las Vegas 2019 アップデート"
date: "2019-11-19 03:07:44"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2019/11/forge-devcon-las-vegas-2010-update.html "
typepad_basename: "forge-devcon-las-vegas-2010-update"
typepad_status: "Publish"
---

<p>昨日、米国ラスベガスの現地時間 11 月 18 日に Forge DevCon 2019 ありました。翌日から始まる Autodesk University 2019 に参加される方も含め、日本からも多くの方にご参加いただいています。</p>
<p>8 時から開催されたキーノートでは、「<strong>Digital Transformation</strong>」 をキーワードに、Visualization、Collaboration、Automate を Micro Service、API を駆使した実例として、<strong><a href="https://www.dpr.com/construction/expertise/collaborative-virtual-building" rel="noopener" target="_blank">DPR Construction</a></strong>、<strong><a href="https://www.usa.skanska.com/what-we-deliver/services/innovation/vdc--bim/" rel="noopener" target="_blank">SKANSKA</a></strong>、<strong><a href="https://www.cadshare.com/" rel="noopener" target="_blank">CADShare</a></strong> の代表の方から、それぞれの事例が紹介されました。もちろん、Forge が組み込まれたソリューションをお使いです。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a4ec7141200b-pi" style="display: inline;"><img alt="Digital_transformation" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0240a4ec7141200b image-full img-responsive" src="/assets/image_172097.jpg" title="Digital_transformation" /></a></p>
<p>なお、用語として特定の機能や API を掘り下げることはありませんでしたが、日本での Forge DevCon Japan のキーノートにもあった、Design Automation API v3、BIM 360 API の機能追加、Forge Viewer の高速化や大規模モデルの基盤となる OTG などが動画、あるいは口頭で紹介されています。</p>
<p class="asset-video"><iframe allow="accelerometer; autoplay; encrypted-media; gyroscope; picture-in-picture" allowfullscreen="" frameborder="0" height="450" src="https://www.youtube.com/embed/c8-AxaoHDlk?start=18&amp;feature=oembed" width="800"></iframe></p>
<p>なぜか、ひっそりと感が否めない印象を持ってしまいましたが、BIM 360 API の機能追加の一貫として、当日付けで Model Coordination API が Public Beta として公開されています。これについては、Forge ポータルの BIM 360 API の <strong><a href="https://forge.autodesk.com/en/docs/bim360/v1/overview/introduction/" rel="noopener" target="_blank">Introduction</a></strong> で触れられています。</p>
<ul>
<li>The BIM 360 Model Coordination API (beta) provides full access to the set of services used by the BIM 360 Model Coordination web application. It enables users to detect and manage the issues which arise when 3D models from different design disciplines are combined into a unified project coordination space.</li>
</ul>
<p>少しわかりにくいかもしれませんが、従来の Navisworks が持っていて BIM 360 が引き継いでいる Clash Detective（干渉チェック）の API です。BIM 360 Docs 上で確認したクラッシュ レポートを照会したり、独自の Forge アプリから干渉チェック機能を使ってクラッシュ レポートを作成することが出来ます。ご注意いただきたいのは、干渉チェック用のデータは、BIM 360 Docs 上のプロジェクトに格納されている Revit プロジェクト（.rvt ファイル）同士（と、おそらく DWG ファイル同士も）である必要がある点です。クラッシュ レポートの結果から、Forge Viewer 上で視覚化することは出来ますが、OSS Bucket などにあるデータを対象にすることは出来ません。</p>
<p><strong><a href="https://forge.autodesk.com/en/docs/bim360/v1/reference/http/mc-modelset-service-v3-create-model-set-POST/" rel="noopener" target="_blank">API Reference</a></strong> には、各種 endpoint 群の詳細が新設されていますので、興味をお持ちの方はご確認ください。<a href="https://forge.autodesk.com/code-samples" rel="noopener" target="_blank"><strong>Code Samples</strong></a> にも、まもなく、サンプルが記載されるはずです。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a4ec796b200b-pi" style="display: inline;"><img alt="Mc_api" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0240a4ec796b200b image-full img-responsive" src="/assets/image_597218.jpg" title="Mc_api" /></a></p>
<p>なお、次世代の データ管理プラットフォームとして昨年紹介されていた「Data at the Center」のコンセプトは、「Forge Data」に名前を変えて開発が進められています。日進月歩と言える最新の Web 開発のテクノロジを投入しながら、設計・製造・施工分野で受け入れられるデータ プラットフォームの作製には、もう少し時間がかかるようです。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a4ec9803200b-pi" style="display: inline;"><img alt="Forge_data" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0240a4ec9803200b image-full img-responsive" src="/assets/image_199984.jpg" title="Forge_data" /></a></p>
<p>最後に、昨年に引き続き、Forge DevCon 当日にのみ開催された The Village には、応用技術 株式会社がメインスポンサーとしてブース展示をされています。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a49e8334200c-pi" style="display: inline;"><img alt="Village" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0240a49e8334200c image-full img-responsive" src="/assets/image_915571.jpg" title="Village" /></a></p>
<p>キーノートでも、応用技術の <strong><a href="https://tobim.net/" rel="noopener" target="_blank">to BIM</a></strong> のロゴが紹介されていた効果もあって、BIM ソリューションを目指してたくさんの方が立ち寄られていました。海外、米国でも BIM が注目の的になっているのは言うまでもありませんが、日本発の BIM 開発ビジネスが十分に通用することが証明された気がして、個人的にとてもうれしく感じた次第です。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a4c7b8f5200d-pi" style="display: inline;"><img alt="Main_sponsor" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0240a4c7b8f5200d image-full img-responsive" src="/assets/image_44486.jpg" title="Main_sponsor" /></a></p>
<p>By Toshiaki Isezaki</p>
