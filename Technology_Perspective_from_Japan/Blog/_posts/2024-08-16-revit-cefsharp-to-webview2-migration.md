---
layout: "post"
title: "Revit アドイン埋め込み Web ブラウザ（CefSharp）の変更について"
date: "2024-08-16 01:00:13"
author: "Ryuji Ogasawara"
categories:
  - "API カスタマイズ"
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2024/08/revit-cefsharp-to-webview2-migration.html "
typepad_basename: "revit-cefsharp-to-webview2-migration"
typepad_status: "Publish"
---

<p><strong>免責事項</strong></p>
<p>この記事の内容（当社の製品およびサービスに関する計画済みまたは将来的な開発努力に関する記述）は、将来の Revit 製品、サービス、または機能が将来利用可能になることを約束または保証することを意図したものではなく、単に当社の現在の計画を反映したものであり、現在当社が把握している要素に基づくものです。</p>
<hr />
<p>現在、Revit には Chromium ブラウザをコアとした <strong>CefSharp</strong> が埋め込まれており、多くのオートデスク製アドインやサードパーティー製の外部アドインがこれを利用しています。</p>
<p>今回、より堅牢でシームレスな Web ブラウジングを実現することを目的として、Revit 開発チームは、次のメジャーリリースにおいて、CefSharp から <strong>WebView2</strong> に移行する計画を発表しました。</p>
<p>原文は、<a href="https://feedback.autodesk.com/">Autodesk Feedback Community</a> の Revit Preview Feedback プロジェクトから、User Forums -&gt; API General -&gt; 「Upgrade to WebView2」のスレッドをご確認ください。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3bc70a1200b-pi" style="display: inline;"><img alt="Revit_Preview_Top" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3bc70a1200b image-full img-responsive" src="/assets/image_872689.jpg" title="Revit_Preview_Top" /></a></p>
<p><strong>なぜこの変更が重要なのか？</strong></p>
<p>WebView2 は Microsoft Edge（Chromium ベース）が提供する Web コントロールで、開発者はネイティブアプリケーションで Web コンテンツをホストすることができます。<br />これまで使用されていた CefSharp と比較して、パフォーマンス、互換性、サポートが向上しています。</p>
<p>これにより、すべての Windows デバイスで一貫したレンダリングが保証され、セキュリティが向上し、最新の Web テクノロジを使用できるようになります。</p>
<hr />
<p><strong>次期メジャーリリースの変更点</strong></p>
<p>Revit は、次のメジャーリリースにて、配布パッケージから全ての CefSharp バイナリを削除します。</p>
<p>Revit アドインは、標準のサードパーティコンポーネントとして CefSharp を使用し続けることができますが、その場合は、アドインが CefSharp のバイナリをアドインのパッケージ内に同梱して配布いただく必要がございます。</p>
<p>ただし、1つの Revit セッションで、複数のアドインから、それぞれ異なる CefSharp のバージョンが読み込まれた場合、潜在的な問題が発生する可能性があることから、今後は、Revit に組み込みの WebView2 を使用することをお勧めします。</p>
<hr />
<p><strong>サードパーティーの開発者の皆様へのお願い</strong></p>
<p>WebView2 への移行は、可能な限りシームレスに行えるように設計されています。<br />しかしながら、重要な変更と同様に、細かな調整が発生する可能性があります。</p>
<p>CefSharp をご利用頂いているアドイン開発者の方々におかれましては、既存の機能が WebView2 でも想定通りに動作するかどうか、事前にテスト頂きますようお願い致します。</p>
<p>アドインが CefSharp をベースに開発されている場合、WebView2 と互換性を持つように開発コンポーネントを変更する必要があるかもしれません。</p>
<p>開発チームへのお問い合わせにつきましては、User Forums -&gt; API General -&gt; 「Upgrade to WebView2」のスレッドをご利用ください。</p>
<hr />
<p>By Ryuji Ogasawara</p>
