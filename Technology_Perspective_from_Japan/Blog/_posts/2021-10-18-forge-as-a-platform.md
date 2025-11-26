---
layout: "post"
title: "AU 2021：プラットフォームとしての Forge と経緯"
date: "2021-10-18 00:23:49"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "その他カスタマイズ"
  - "クラウドサービス"
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2021/10/forge-as-a-platform.html "
typepad_basename: "forge-as-a-platform"
typepad_status: "Publish"
---

<p>先々週、先週と地域別に開催してきた Autodesk University 2021 では、<a href="https://events-platform.autodesk.com/event/autodesk-university-2021/planning/UGxhbm5pbmdfNjY5ODYx" rel="noopener" target="_blank">Forge キーノート</a>だけでなく、ジェネラル キーノート&#0160;<a href="https://events-platform.autodesk.com/event/autodesk-university-2021/planning/UGxhbm5pbmdfNjY5ODYw" rel="noopener" target="_blank">#1&amp;#2</a>、<a href="https://events-platform.autodesk.com/event/autodesk-university-2021/planning/UGxhbm5pbmdfNjY5ODU5" rel="noopener" target="_blank">#3</a> やインダストリーキーノートのすべてが Forge に言及していたことにお気づきと思います。訴求点は、オートデスクのクラウド プラットフォームとしての Forge の存在です。</p>
<p>今回は、クラウド プラットフォーム Forge に至る、ここまでの旅路（journey）を、現在の Forge を交えて、簡単に振り返ってみたいと思います。</p>
<p>Autodesk Forge は、2015 年秋の Autodesk University Las Vegas でアナウンスを経て、翌年、2016 年 6 月に San Francisco で開催された <a href="https://forge.autodesk.com/devcon-2016" rel="noopener" target="_blank">Forge DevCon 2016</a>（Forge Developer Conference）で正式にリリースされています。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b026bdef98a84200c-pi" style="display: inline;"><img alt="Devcon_2018" border="0" class="asset  asset-image at-xid-6a0167607c2431970b026bdef98a84200c image-full img-responsive" src="/assets/image_430102.jpg" title="Devcon_2018" /></a></p>
<p>この際に登場したのが、開発者向けのプラットフォームとして Forge が知られるようになった <a href="https://adndevblog.typepad.com/technology_perspective/2016/06/about-changes-of-forge-platform-api.html" rel="noopener" target="_blank"><strong>API 群</strong></a>です。今日ではオートデスクの活発な開発者エコシステムを形成しつつ、デジタル トランスフォーメーションを実現する目的で数多く利用されています。</p>
<p>現在の Forge で構築されたソリューションを<a href="https://adndevblog.typepad.com/technology_perspective/2021/03/efficient-demo-sites-to-know-forge.html" rel="noopener" target="_blank">タイプ別のご紹介</a>するなら、Visual Insight と呼ばれ、ビューアを利用したダッシュボードやデジタルツインに代表される Viewer ソリューション、BIM 360 などの SaaS やストレージとの統合ソリューション、デザインデータの抽出や作成、編集を自動化する自動化ソリューション（Design Automation）を挙げることが出来ます。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0282e129de4a200b-pi" style="display: inline;"><img alt="Current_solutions" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0282e129de4a200b image-full img-responsive" src="/assets/image_211029.jpg" title="Current_solutions" /></a></p>
<p>これら Forge ソリューションは、各種 CAD、BIM などのデータをファイルとして扱う前提で作られています。データ ファイルはツール別に独自の形式を持っているため、他のツール/ソフトウェアでは、ファイルに含まれるすべてのデータを把握することが難しい場面があります。</p>
<p>Forge DevCon 2016 時、ファイルによる差を発展的に解決する新たな手法として、クラウドを包括的なデータ リポジトリとして活用する <a href="https://adndevblog.typepad.com/technology_perspective/2017/11/consider-about-forge-hfdm.html" rel="noopener" target="_blank">HFDM</a> が提案されました。HFDM のコンセプトは、その後も社内外からの多くのフィードバックを経て熟成されていきます。</p>
<p>その後、2018 年の Autodesk University Las Vegas では、<a href="https://adndevblog.typepad.com/technology_perspective/2018/11/devcon-2018-forge-roadmap.html" rel="noopener" target="_blank">Data at the Center</a> のコンセプトが共有がありきて、データ ファイルを HFDM で置き換えるのではなく、両者をコンバージェンス（収束、あるいは、融合）させるコンセプトに変化してしてきています。同時に、ツールありきのデータ（ファイル）ではなく、データありきのツールへ、ソフトウェア サービスの形態をシフトさせる点も示唆されています。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0282e129d614200b-pi" style="display: inline;"><img alt="Data_at_the_center" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0282e129d614200b image-full img-responsive" src="/assets/image_733665.jpg" title="Data_at_the_center" /></a></p>
<p>2019 年 Autodesk University Las Vegas では、Data at the Center を実現し、HDFM を置き換える Forge Data のコンセプトが&#0160;<a href="https://www.autodesk.com/autodesk-university/class/Future-Data-Forge-Data-Platform-2019" rel="noopener" target="_blank">The Future of Data: Forge Data Platform</a>&#0160;クラスで紹介されています。</p>
<p>オンライン開催だった昨年 2020 年の Autodesk University では、キーノートで Data at the Center も実現をコンバージェンス（Convergence）のキーワードで強調しつつ、<a href="https://www.autodesk.com/autodesk-university/class/Forge-Road-Map-Fusion-360-Forge-Data-2020" rel="noopener" target="_blank">Fusion on Forge Data</a> クラスでは、実装段階の Forge Data を、まずは Fusion 360 へ、次に他のオートデスク製品に試験的に採用していくことが示されます。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0282e129d943200b-pi" style="display: inline;"><img alt="Adoption_order" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0282e129d943200b image-full img-responsive" src="/assets/image_398584.jpg" title="Adoption_order" /></a></p>
<p>そして今年、2021 年の Autodesk University を迎えたことになります。</p>
<p>キーノートで語られているのは、クラウド上のデータ ファイルを粒状レベルに分解して利用する Forge Data を全面的に採用していくことで、短い時間でコラボレーション・コミュニケーションが可能になるだけでなく、データありきのツール環境、つまり、データ プラットフォームを実現しようとする点です。また、ファイルという可搬性を維持しつつ、将来に向けた新しい運用を可能にする柔軟性も併せ持つことにもなります。</p>
<p>ジェネラルキーノートでは、Autodesk Construction Cloud を介して、 Revit プロジェクトから一部の要素を抽出して Inventor に渡す例や、<a href="https://powerautomate.microsoft.com/ja-jp/" rel="noopener" target="_blank">Microsoft Power Automate</a> を介して一部のデータを Excel に記入する例また、Fusion 360 のコンポーネントにライフサイクル データを付与して、<a href="https://ja.wikipedia.org/wiki/Product_Information_Management" rel="noopener" target="_blank">Product Information Management</a>（PIM）を実現する例が紹介されています。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b026bdef98f76200c-pi" style="display: inline;"><img alt="Forge_data" border="0" class="asset  asset-image at-xid-6a0167607c2431970b026bdef98f76200c image-full img-responsive" src="/assets/image_864501.jpg" title="Forge_data" /></a></p>
<p>旅路は、まだ道半ばですが、Data at the Center コンセプトが、より具体化してきた状況をご確認いただけるはずです。API として、3rd party の皆様にお使いいただけるのは、まだ先になりますが、オートデスクが考えている壮大な取り組みをご理解ください。</p>
<p>Forge が多くのキーノートで触れられた点に違和感を感じる方も多いかと思います。Forge は開発者向けプラットフォームであるだけでなく、今後、オートデスク製品の基盤、プラットフォームとなるが故の現れです。</p>
<p>By Toshiaki Isezaki</p>
