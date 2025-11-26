---
layout: "post"
title: "Revit 2026 API アップデート"
date: "2025-05-23 01:18:04"
author: "Ryuji Ogasawara"
categories:
  - "API カスタマイズ"
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2025/05/revit-2026-api-update.html "
typepad_basename: "revit-2026-api-update"
typepad_status: "Publish"
---

<p>今回は Revit 2026 API のアップデートについて、ご案内いたします。</p>
<hr />
<p><strong>Revit 2026 SDK</strong></p>
<p>Revit API リファレンスや Visual Studio プロジェクトの完全なサンプルコードは、Revit .NET SDK に同梱されています。</p>
<p>下記のページから、2026 バージョンの Revit .NET SDK をダウンロードしてご利用ください。</p>
<ul>
<li><a href="https://aps.autodesk.com/ja/developer/overview/revit" rel="noopener" target="_blank">Revit APIs</a></li>
</ul>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02e860eadd66200b-pi" style="display: inline;"><img alt="Revit2026_06_01" class="asset  asset-image at-xid-6a0167607c2431970b02e860eadd66200b img-responsive" src="/assets/image_651113.jpg" title="Revit2026_06_01" /></a></p>
<hr />
<p><strong>Revit 2026 API 開発者用ガイド</strong></p>
<p>Revit 2026 API の解説は、下記の「 Revit API 開発者用ガイド」にてご参照いただけます。<br />全てを網羅しているわけではありませんが、スニペットが多数掲載されておりますので、API の利用方法を把握いただく際に役立ちます。</p>
<ul>
<li><a href="https://help.autodesk.com/view/RVT/2026/JPN/?guid=Revit_API_Revit_API_Developers_Guide_html" rel="noopener" target="_blank">Revit API 開発者用ガイド</a></li>
</ul>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3d42b8f200c-pi" style="display: inline;"><img alt="Revit2026_06_02" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3d42b8f200c img-responsive" src="/assets/image_274767.jpg" title="Revit2026_06_02" /></a></p>
<hr />
<p><strong>Revit 2026 API 新機能と変更点</strong></p>
<p>詳細につきましては、SDK に同梱されている「Revit Platform API Changes and Additions.docx」、または、RevitAPI.chm 内の「Major changes and renovations to the Revit API」セクションをご参照ください。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02e86101c969200d-pi" style="display: inline;"><img alt="Revit2026_06_03" class="asset  asset-image at-xid-6a0167607c2431970b02e86101c969200d img-responsive" src="/assets/image_640085.jpg" title="Revit2026_06_03" /></a></p>
<p>下記、Revit 2026 での変更内容の中で、特にご注意いただきたい点を記載いたします。</p>
<p style="padding-left: 40px;"><br /><strong>CefSharp から WebView2 への移行</strong></p>
<p style="padding-left: 40px;">下記のブログ記事にてご案内しておりました通り、Revit 2026 以降、すべての CefSharp バイナリが Revit 配布パッケージから削除されました。</p>
<ul>
<li>
<ul>
<li><a href="https://adndevblog.typepad.com/technology_perspective/2024/08/revit-cefsharp-to-webview2-migration.html" rel="noopener" target="_blank">Revit アドイン埋め込み Web ブラウザ（CefSharp）の変更について</a></li>
</ul>
</li>
</ul>
<p style="padding-left: 40px;">Revit アドインでは、サード パーティ コンポーネントとして CefSharp を引き続き使用できますが、CefSharp の使用を継続するには、アドインに CefSharp バイナリを同梱いただきますようお願いいたします。</p>
<p style="padding-left: 40px;">オートデスクは、起動中の Revit セッション中に、複数のアドインによって、異なる CefSharp のバージョンがロードされることにより引き起こされる潜在的な問題を回避するために、WebView2 を使用することをお勧めします。</p>
<p style="padding-left: 40px;"><br /><strong>アドイン依存関係の分離</strong></p>
<p style="padding-left: 40px;">アドイン開発者向けに、Revit や他のアドインとは別のアセンブリ ロード コンテキストでアドインを実行できる新しいオプションが導入されました。</p>
<ul>
<li>
<ul>
<li>これにより、アドインは、同じセッション内で、Revit や他のアドインによってロードされた依存アセンブリから分離されます。</li>
<li>これにより、アドインの競合が原因となって Revit セッションで発生するアセンブリ バージョンの競合が低減または排除されます。</li>
</ul>
</li>
</ul>
<p style="padding-left: 40px;">この機能の導入は、このリリースではオプションです。開発者は、アドイン マニフェストごとにオプション「UseRevitContext」を false に設定できます(指定したマニフェスト内のすべての登録アドインに適用されます)。これが指定されていないアドインの既定値は「true」です。</p>
<p style="padding-left: 40px;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02e86101c96c200d-pi" style="display: inline;"><img alt="Revit2026_06_04" class="asset  asset-image at-xid-6a0167607c2431970b02e86101c96c200d img-responsive" src="/assets/image_223581.jpg" title="Revit2026_06_04" /></a></p>
<p style="padding-left: 40px;">このオプションを使用した場合、同じフォルダからロードされたすべてのアドインが同じ外部コンテキストに配置されます。異なるフォルダのインストール場所からのアドインは、異なるコンテキストに移動します。</p>
<p style="padding-left: 40px;">開発者は、必要に応じてアドインのカスタム ContextName を設定することもできます。これを使用すると、同じコンテキスト名を使用する異なるフォルダ場所のアドインが同じコンテキストにロードされます。</p>
<p style="padding-left: 40px;">アドイン開発者は、アドインのロード前に、依存関係をロードする Revit や他のアドインに依存できなくなる可能性があるため、独自のアドインに対してロードする依存関係が正確かつ完全であることを確認するようお勧めします。</p>
<ul>
<li>
<ul>
<li><a href="https://help.autodesk.com/view/RVT/2026/JPN/?guid=Revit_API_Revit_API_Developers_Guide_Introduction_Add_In_Integration_Add_in_Dependency_Isolation_html" rel="noopener" target="_blank">アドイン依存関係の分離</a></li>
</ul>
</li>
</ul>
<p style="padding-left: 40px;"><br /><strong>アドイン マネージャ</strong></p>
<p style="padding-left: 40px;">Revit 2025.3 以降の更新プログラムで使用可能なアドイン マネージャを使用すると、リストからアドインを無効または有効にすることができます。</p>
<p style="padding-left: 40px;">アドイン マネージャを使用すると、すべてのアドインのオン/オフを切り替えたり、1 つまたは複数のアドインを選択的に有効にすることができます。</p>
<p style="padding-left: 40px;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02e860eadd6d200b-pi" style="display: inline;"><img alt="Revit2026_06_06" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02e860eadd6d200b image-full img-responsive" src="/assets/image_22249.jpg" title="Revit2026_06_06" /></a></p>
<p style="padding-left: 40px;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02e86101c971200d-pi" style="display: inline;"><img alt="Revit2026_06_05" class="asset  asset-image at-xid-6a0167607c2431970b02e86101c971200d img-responsive" src="/assets/image_463905.jpg" title="Revit2026_06_05" /></a></p>
<p style="padding-left: 40px;">詳細につきましては、下記のページをご参照ください。</p>
<ul>
<li>
<ul>
<li><a href="https://help.autodesk.com/view/RVT/2026/JPN/?guid=GUID-97276239-B101-4ECE-B30A-3CCD7174EEC4" rel="noopener" target="_blank">アドイン マネージャ</a></li>
</ul>
</li>
</ul>
<hr />
<p>By Ryuji Ogasawara</p>
