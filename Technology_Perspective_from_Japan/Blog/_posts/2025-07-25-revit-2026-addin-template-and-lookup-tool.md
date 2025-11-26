---
layout: "post"
title: "Revit 2026 Visual Studio アドインテンプレートと新しい RevitLookup ツール"
date: "2025-07-25 01:40:12"
author: "Ryuji Ogasawara"
categories:
  - "API カスタマイズ"
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2025/07/revit-2026-addin-template-and-lookup-tool.html "
typepad_basename: "revit-2026-addin-template-and-lookup-tool"
typepad_status: "Publish"
---

<p>今回は、Revit アドインを開発する上で便利なツールをご紹介いたします。</p>
<p><strong>Visual Studio Add-in Template</strong></p>
<p>Revit アドイン開発に対応した Visual Studio のプロジェクトテンプレートが、弊社エンジニアによりリリースされております。</p>
<ul>
<li><a href="https://github.com/jeremytammik/VisualStudioRevitAddinWizard" rel="noopener" target="_blank">Visual Studio Revit Add-in Templates</a></li>
</ul>
<p>このプロジェクトテンプレートをインストールすることで、Visual Studio の新規プロジェクトの作成から、プロジェクトの初期設定やアドインマニフェストファイル、スケルトンコードを自動生成することができます。</p>
<p>Visual Studio プロジェクトを作成後、デバッグモードで実行すると、Revit が自動的に起動し、アドインがロードされ、外部コマンドまたは外部アプリケーションをデバッグすることができます。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02e860efb54b200b-pi" style="display: inline;"><img alt="Revit2026_05_01" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02e860efb54b200b image-full img-responsive" src="/assets/image_64185.jpg" title="Revit2026_05_01" /></a></p>
<p><strong>Revit 2026 対応版</strong>については、下記のブログ記事でビルド済みの ZIP ファイルを公開致しました。</p>
<ul>
<li><a href="https://adndevblog.typepad.com/technology_perspective/2019/05/visual-studio-add-in-wizards-for-revit-2020-installation.html" rel="noopener" target="_blank">Revit Visual Studio .NET Add-in Wizards の入手方法</a></li>
</ul>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02e860efb54f200b-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="Revit2026_05_02" class="asset  asset-image at-xid-6a0167607c2431970b02e860efb54f200b img-responsive" src="/assets/image_941317.jpg" title="Revit2026_05_02" /></a></p>
<hr />
<p><strong>新しい RevitLookup ツール</strong></p>
<p>RevitLookup ツールは、Revit の UI 上から、アプリケーション、ドキュメント、ファミリ、ジオメトリ等の、各オブジェクトのデータベース構造を確認できるアドインです。</p>
<p>GitHub のオープンソースプロジェクトとなっており、2022年からは、サードパーティーの開発者の方（Nice3point氏）が、コントリビューターとなっておりましたが、コミュニティ主導のプロジェクトとして、新たに Looup Foundation に移管されました。</p>
<ul>
<li><a href="https://github.com/lookup-foundation/RevitLookup">RevitLookup</a></li>
</ul>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02e860efb58c200b-pi" style="display: inline;"><img alt="Revit2026_05_03" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02e860efb58c200b image-full img-responsive" src="/assets/image_906675.jpg" title="Revit2026_05_03" /></a></p>
<p>Revit 2024 バージョンでは、RevitLookup ツールの<strong>コード全体が完全にスクラッチで書き直され、ユーザーインターフェイスが再設計</strong>されました。<br />また、Revit 2025 バージョンでは、.NET 8 への移行に伴い、ソースコードの移植も実施されました。<br />さらに、この新しい RevitLookup ツールの <strong>Revit 2021 - Revit 2023 バージョン版もリリース済み</strong>です。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3b472ca200b-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="Revit2025_07_03" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3b472ca200b image-full img-responsive" src="/assets/image_25097.jpg" title="Revit2025_07_03" /></a></p>
<p>なお、旧 RevitLookup ツール（Revit 2023 バージョンまでサポート）については、下記の記事にてご案内しております。</p>
<ul>
<li><a href="https://adndevblog.typepad.com/technology_perspective/2019/06/revit-lookup-tool-for-revit-2018-2023.html" rel="noopener" target="_blank">RevitLookup ツール for Revit 2018-2023 の入手方法と機能について</a></li>
</ul>
<p>新しい RevitLookup ツールの更新内容と新機能の詳細は、下記のブログ記事に掲載されております。他の記事でもマイナーアップデートの詳細をご確認いただけます。</p>
<ul>
<ul>
<li><a href="https://thebuildingcoder.typepad.com/blog/2025/04/lookup-foundation-revitlookup-and-da4r-2026.html">Revit 2026 and RevitLookup 2026</a></li>
<li><a href="https://thebuildingcoder.typepad.com/blog/2024/04/revit-2025-and-revitlookup-2025.html" rel="noopener" target="_blank">Revit 2025 and RevitLookup 2025</a></li>
<li><a href="https://thebuildingcoder.typepad.com/blog/2023/04/revit-2024-and-revitlookup-2024.html" rel="noopener" target="_blank">Revit 2024 and RevitLookup 2024</a></li>
</ul>
</ul>
<p>&#0160;</p>
<p>ここでは、主な変更点と新機能に絞ってご紹介いたします。Revit 2026 版リリースに伴うアップデートの詳細は、<a href="https://thebuildingcoder.typepad.com/blog/2025/04/lookup-foundation-revitlookup-and-da4r-2026.html">こちら</a>をご参照ください。</p>
<p><strong>UI の刷新</strong></p>
<p>全く新しいユーザーインターフェースで再構築され、テーマの選択、コンテキストメニュー、ツールチップなども更新されました。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3b472d1200b-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="Revit2025_07_04" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3b472d1200b img-responsive" src="/assets/image_662287.jpg" title="Revit2025_07_04" /></a></p>
<p><strong>ダッシュボード</strong></p>
<p>Dashboard から、新しい様々な機能を利用できるようになりました。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3b472df200b-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="Revit2025_07_06" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3b472df200b image-full img-responsive" src="/assets/image_295666.jpg" title="Revit2025_07_06" /></a></p>
<p><strong>新しいコマンドの追加</strong></p>
<ul>
<li>Snoop Point</li>
<li>Snoop Sub-Element</li>
<li>Snoop UI Application</li>
</ul>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02dad0c49e05200d-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="Revit2025_07_05" class="asset  asset-image at-xid-6a0167607c2431970b02dad0c49e05200d img-responsive" src="/assets/image_117533.jpg" title="Revit2025_07_05" /></a></p>
<p><strong>要素の削除</strong></p>
<p>RevitLookup ツールから要素を削除できるようになりました。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3b472f6200b-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="Revit2025_07_07" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3b472f6200b image-full img-responsive" src="/assets/image_309141.jpg" title="Revit2025_07_07" /></a></p>
<p><strong>要素パラメータ値の編集</strong></p>
<p>RevitLookup ツールから要素のパラメータ値を編集できるようになりました。String、Double、Int、ElementId がサポートされています。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3b0b901200c-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="Revit2025_07_08" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3b0b901200c image-full img-responsive" src="/assets/image_3786.jpg" title="Revit2025_07_08" /></a></p>
<p><strong>要素に保存されている拡張ストレージのカスタムデータの確認</strong></p>
<p>Element.GetEntity()メソッドに移動しました。</p>
<p><strong>メモリ診断</strong></p>
<p>各要素に割り当てられたマネージド メモリのサイズを表示できます。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3b472fd200b-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="Revit2025_07_09" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3b472fd200b image-full img-responsive" src="/assets/image_404909.jpg" title="Revit2025_07_09" /></a></p>
<p><strong>Component Manager</strong></p>
<p>AdWindows.dll を通じて、Revit のリボン&#0160; UI がどのように構築されているかを確認できます。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3b4730e200b-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="Revit2025_07_10" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3b4730e200b image-full img-responsive" src="/assets/image_40440.jpg" title="Revit2025_07_10" /></a></p>
<p><strong>Performance Adviser</strong></p>
<p>ドキュメントのパフォーマンスの問題を調査します。</p>
<p><strong>Registry</strong></p>
<p>拡張ストレージのスキーマ、ダイナミックアップデータ、サービスを確認できます。</p>
<p><strong>Units</strong></p>
<p>ビルトインパラメータ、ビルトインカテゴリ、APS（旧 Forge）のスキーマ（単位）を確認できます。</p>
<p><strong>イベントモニター</strong></p>
<p>Revit で検知可能な全てのイベントを追跡します。<br />RevitAPI.dll と RevitAPIUI.dll ライブラリに対応しています。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3b0b92e200c-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="Revit2025_07_11" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3b0b92e200c image-full img-responsive" src="/assets/image_974319.jpg" title="Revit2025_07_11" /></a></p>
<p><strong>プロジェクト内のビジュアル検索</strong></p>
<ul>
<li>要素を表示</li>
<li>面を表示（Revit 2023~）</li>
<li>ソリッドを表示（Revit 2023~）</li>
<li>エッジを表示（Revit 2023~）</li>
</ul>
<p><strong>OTA アップデート</strong></p>
<p>RevitLookup アップデートはプラグインから直接利用できるようになりました。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3b47339200b-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="Revit2025_07_12" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3b47339200b image-full img-responsive" src="/assets/image_725071.jpg" title="Revit2025_07_12" /></a></p>
<hr />
<p>ぜひ Revit アドイン開発にご活用ください。</p>
<p>By Ryuji Ogasawara</p>
