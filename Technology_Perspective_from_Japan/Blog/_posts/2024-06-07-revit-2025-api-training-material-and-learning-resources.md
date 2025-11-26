---
layout: "post"
title: "Revit 2025 API トレーニングマテリアルとアドイン開発の学習リソース"
date: "2024-06-07 00:18:48"
author: "Ryuji Ogasawara"
categories:
  - "API カスタマイズ"
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2024/06/revit-2025-api-training-material-and-learning-resources.html "
typepad_basename: "revit-2025-api-training-material-and-learning-resources"
typepad_status: "Publish"
---

<p><strong>更新（2024/08/22）</strong></p>
<p>Revit API トレーニング マテリアルについて、エラー等問題が見つかりましたら、API サポートケースからお問い合わせ頂きますようお願い致します。</p>
<hr />
<p><strong>本文</strong></p>
<p>Revit は、アドインをロードすることで機能を拡張することができます。アドインの開発には、.NET&#0160; をベースとするクラスライブラリを作成し、Revit API を呼び出すプログラムを実装します。主な開発言語は C# または VB.NET となっております。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3b14081200c-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="Revit2025_08_02" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3b14081200c image-full img-responsive" src="/assets/image_514034.jpg" title="Revit2025_08_02" /></a></p>
<p>Revit API は、基本的には、Revit 製品の UI 操作で実行可能な機能に対応しており、<strong>様々な作業を自動化</strong>したり、お客様のワークフローに応じて<strong>処理を組み合わせて、効率化・合理化</strong>することができます。</p>
<p>さらに、API 固有の仕組みとして、カスタムデータを要素やドキュメントに格納する<strong>拡張ストレージ</strong>、任意のカスタムジオメトリを作成して表示する <strong>DirectShape </strong>、一時的なグラフィックスを表示する <strong>DirectContext3D </strong>、モデルの変更時にカスタム処理を割り込ませる<strong>ダイナミック モデル アップデータ</strong>などもございます。</p>
<p>今回は、Revit アドイン開発にご興味のある方に向けて、SDK、トレーニングマテリアルと学習リソースをご紹介いたします。</p>
<p><strong>Revit .NET SDK</strong></p>
<p>Revit API リファレンスや Visual Studio プロジェクトの完全なサンプルコードは、Revit .NET SDK に同梱されています。</p>
<p>下記のページから、該当バージョンの SDK をダウンロードしてご利用ください。</p>
<ul>
<li><a href="https://aps.autodesk.com/ja/developer/overview/revit" rel="noopener" target="_blank">Revit APIs</a></li>
</ul>
<p style="padding-left: 40px;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02dad0c51a4c200d-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="Revit2025_08_01" class="asset  asset-image at-xid-6a0167607c2431970b02dad0c51a4c200d img-responsive" src="/assets/image_87677.jpg" title="Revit2025_08_01" /></a></p>
<hr />
<p><strong>Revit API 開発者用ガイド</strong></p>
<p>Revit API の解説は、下記の「 Revit API 開発者用ガイド」にてご参照いただけます。全てを網羅しているわけではありませんが、スニペットが多数掲載されておりますので、API の利用方法を把握いただく際に役立ちます。</p>
<ul>
<li><a href="https://help.autodesk.com/view/RVT/2025/JPN/?guid=Revit_API_Revit_API_Developers_Guide_html" rel="noopener" target="_blank">Revit API 開発者用ガイド</a></li>
</ul>
<p style="padding-left: 40px;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3b140a7200c-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="Revit2025_08_03" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3b140a7200c image-full img-responsive" src="/assets/image_363003.jpg" title="Revit2025_08_03" /></a></p>
<hr />
<p><strong>Revit API トレーニング マテリアル</strong></p>
<p>Revit API を利用したアドイン開発にご興味のある方を対象とした、Revit API トレーニング マテリアル（日本語版）を公開致しました。</p>
<p>Revit 2022 - 2025、各バージョン毎に API の変更内容を反映しています。次のリンクからダウンロードいただけます。</p>
<ul>
<li><span class="asset  asset-generic at-xid-6a0167607c2431970b02c8d3b4ef9f200b img-responsive"><a href="https://adndevblog.typepad.com/files/revit_2025_api_training.zip">Revit_2025_API_Trainingをダウンロード</a></span></li>
<li><span class="asset  asset-generic at-xid-6a0167607c2431970b02c8d3b4ef9f200b img-responsive"> <span class="asset  asset-generic at-xid-6a0167607c2431970b02dad0c51b23200d img-responsive"><a href="https://adndevblog.typepad.com/files/revit_2024_api_training.zip">Revit_2024_API_Trainingをダウンロード</a></span></span></li>
<li><span class="asset  asset-generic at-xid-6a0167607c2431970b02c8d3b4ef9f200b img-responsive"><span class="asset  asset-generic at-xid-6a0167607c2431970b02dad0c51b23200d img-responsive"> <span class="asset  asset-generic at-xid-6a0167607c2431970b02c8d3b4efa6200b img-responsive"><a href="https://adndevblog.typepad.com/files/revit_2023_api_training.zip">Revit_2023_API_Trainingをダウンロード</a></span></span></span></li>
<li><span class="asset  asset-generic at-xid-6a0167607c2431970b02c8d3b4ef9f200b img-responsive"><span class="asset  asset-generic at-xid-6a0167607c2431970b02dad0c51b23200d img-responsive"><span class="asset  asset-generic at-xid-6a0167607c2431970b02c8d3b4efa6200b img-responsive"> <span class="asset  asset-generic at-xid-6a0167607c2431970b02c8d3b14147200c img-responsive"><a href="https://adndevblog.typepad.com/files/revit_2022_api_training.zip">Revit_2022_API_Trainingをダウンロード</a></span></span></span></span></li>
</ul>
<p>これから Revit API の習得を目指す方は、チュートリアル教材としてご利用ください。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02dad0c51add200d-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="Revit2025_08_04" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02dad0c51add200d image-full img-responsive" src="/assets/image_792145.jpg" title="Revit2025_08_04" /></a></p>
<p>このトレーニングマテリアルは、英語版トレーニングマテリアルをベースとした日本語版となっております。また、日本語版の Revit に含まれるテンプレートで作成したプロジェクトで演習が行えるようになっております。</p>
<p>トレーニングマテリアルの内容は以下の通りです。</p>
<ul>
<li>Labs フォルダ：<br />入門実習、UI 実習、ファミリ API 実習の3つのコース内容について、C# の実習ドキュメントと完成した Visual Studio プロジェクトが格納されています。<br /><br /></li>
<li>Presentation フォルダ<br />クラス トレーニング実施時の PowerPoint プレゼンテーションの PDF 版です。<br /><br /></li>
<li>Sample Drawing フォルダ<br />実習トレーニング中で利用する Revit プロジェクトファイルが格納されています。</li>
</ul>
<p>なお、注意点ですが、演習解答サンプルのUI実習プロジェクトは入門実習プロジェクトに依存しています。入門実習プロジェクトを先にビルドして下さい。</p>
<hr />
<p><strong>Revit API &amp; BIM セミナー</strong></p>
<p>2018年に開催した「Revit API &amp; BIM セミナー」の収録動画とプレゼンテーション資料を公開しております。</p>
<p>最新の API の変更点や改善点、新機能については別途、アップデートいただく必要がございますが、基本的な仕組みや考え方は共通しています。</p>
<ul>
<li><a href="https://adndevblog.typepad.com/technology_perspective/2018/12/revit-api-bim-seminar-summary.html" rel="noopener" target="_blank">Revit API &amp; BIM セミナーのサマリー</a></li>
</ul>
<p style="padding-left: 40px;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3b4f01b200b-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="Revit2025_08_05" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3b4f01b200b img-responsive" src="/assets/image_950354.jpg" title="Revit2025_08_05" /></a></p>
<hr />
<p><strong>Visual Studio アドインテンプレートと RevitLookup ツール</strong></p>
<p>Revit アドインを開発する上で便利なツールとして「Visual Studio アドインテンプレート」と「RevitLookup ツール」がございます。</p>
<p>インストール方法や機能について、次のブログ記事でご案内しています。</p>
<ul>
<li><a href="https://adndevblog.typepad.com/technology_perspective/2024/05/revit-2025-addin-template-and-lookup-tool.html" rel="noopener" target="_blank">Revit 2025 Visual Studio アドインテンプレートと新しい RevitLookup ツール</a></li>
</ul>
<p style="padding-left: 40px;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3b4f04f200b-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="Revit2025_07_03" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3b4f04f200b img-responsive" src="/assets/image_335776.jpg" title="Revit2025_07_03" /></a></p>
<hr />
<p><strong>The Building Coder Blog</strong></p>
<p>弊社エンジニア Jeremy Tammik のブログ「The Building Coder」では、Revit API に関するディスカッションと知見を記事にまとめています。トピック毎に整理されています。</p>
<ul>
<li><a href="https://thebuildingcoder.typepad.com/" rel="noopener" target="_blank">The Building Coder</a></li>
<li><a href="https://thebuildingcoder.typepad.com/blog/about-the-author.html#5" rel="noopener" target="_blank">トピックページ</a></li>
</ul>
<p style="padding-left: 40px;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3b141e8200c-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="Revit2025_08_06" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3b141e8200c img-responsive" src="/assets/image_865371.jpg" title="Revit2025_08_06" /></a></p>
<hr />
<p><strong>Autodesk Developer Network (ADN)</strong></p>
<p>オートデスクは、デスクトップ製品開発者向けの有償サポート プログラムである Autodesk Developer Network、通称、ADN を主催しています。</p>
<p>API によるアドイン開発で発生する技術的な問い合わせを行うための専用アカウントをご提供し、Web フォーム経由で質問、回答を得ることが出来ます。また、開発作業で利用することを前提に、デスクトップ製品（日本語版）を提供しています。詳細については、次のリンクをご参照ください。</p>
<ul>
<li><a href="https://adndevblog.typepad.com/technology_perspective/2017/12/adn-faq.html" rel="noopener" target="_blank">ADN よくある質問</a></li>
<li><a href="https://aps.autodesk.com/adn" rel="noopener" target="_blank">ADN ポータルページ</a></li>
</ul>
<p style="padding-left: 40px;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02dad0c60238200d-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="Revit2025_08_08" class="asset  asset-image at-xid-6a0167607c2431970b02dad0c60238200d img-responsive" src="/assets/image_564653.jpg" title="Revit2025_08_08" /></a></p>
<hr />
<p>ぜひ Revit アドイン開発にご活用ください。</p>
<p>By Ryuji Ogasawara</p>
