---
layout: "post"
title: "Revit API 開発者向けブログ The Building Coder と Revit 2026.2 アップデート"
date: "2025-08-15 01:00:31"
author: "Ryuji Ogasawara"
categories:
  - "API カスタマイズ"
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2025/08/jeremys-retirement-continuation-of-the-blog-revit-sdk-20262.html "
typepad_basename: "jeremys-retirement-continuation-of-the-blog-revit-sdk-20262"
typepad_status: "Publish"
---

<p>Revit API でアドイン開発を経験されている方で、<a href="https://thebuildingcoder.typepad.com/"><strong>The Building Coder</strong></a> というブログをご存知の方もいらっしゃるかと思いますが、そのブログの管理者であり、約 17 年間 Revit API 開発者コミュニティの最前線で活躍した <strong>Jeremy Tammik</strong> 氏が、正式に引退しました。</p>
<p>多くの AEC および Revit アドイン開発者にとって、The Building Coder ブログは、Jeremy の深い専門知識とテクニカル Tips の共有、コミュニティとの交流、そして複雑な Revit API の解説を参照いただける場となってきました。</p>
<p>The Building Coder は、今後、<strong>Developer Advocacy &amp; Support</strong> チームで引き継ぎますが、Jeremy にもブログに不定期で寄稿したいただく予定です。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02e861080af4200d-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="Revit2026_07_01" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02e861080af4200d image-full img-responsive" src="/assets/image_669899.jpg" title="Revit2026_07_01" /></a></p>
<hr />
<p><strong>Revit 2026.2 SDK リリース</strong></p>
<p>Revit 2026.2 の最新リリースに合わせて、Revit 2026.2 SDK アップデートも公開しました。<br />最新の Revit SDK バージョンは、<a href="https://aps.autodesk.com/developer/overview/revit">aps.autodesk.com/developer/overview/revit</a> または直接ダウンロード リンク <a href="https://autodesk-adn-transfer.s3.us-west-2.amazonaws.com/ADN+Extranet/Revit/REVIT_2026_2_SDK.msi">Revit 2026.2 SDK</a> からダウンロードできます。</p>
<p><br /><strong>アドイン マネージャ: UI でのアドインのグループ化</strong></p>
<p>RevitAddInUtility API に小さいながらも便利な改善が導入され、Revit のアドイン マネージャ内でツールをより適切に整理できるようになりました。</p>
<p>新しいオプションフラグ:</p>
<pre><code>RevitAddInManifestSettings.UnifyInAddInManager</code></pre>
<p>開発者は、アドイン マネージャと(管理者)アドイン マネージャ UI の両方で、複数のアドインを 1 行にグループ化できます。</p>
<p>同じマニフェスト ファイル内に複数の外部コマンドまたはアプリケーションが定義されている場合、このフラグを有効にすると、それらを 1 つの項目として表示することでユーザー インターフェイスを簡素化でき、乱雑さや混乱の可能性を回避できます。</p>
<p>グループ化を有効にしたサンプルマニフェスト:</p>
<pre><code>
&lt;?xml version=&quot;1.0&quot; encoding=&quot;utf-8&quot;?&gt;
&lt;RevitAddIns&gt;
  &lt;AddIn Type=&quot;DBApplication&quot;&gt;
    &lt;Name&gt;SampleApplication&lt;/Name&gt;
    &lt;FullClassName&gt;SampleApplication.Application&lt;/FullClassName&gt;
    &lt;Assembly&gt;SampleApplication.dll&lt;/Assembly&gt;
    &lt;ClientId&gt;C96B32A3-98C6-4B47-99DA-562E64689C6F&lt;/ClientId&gt;
    &lt;VendorId&gt;Autodesk&lt;/VendorId&gt;
  &lt;/AddIn&gt;
  &lt;ManifestSettings&gt;
    <strong>&lt;UnifyInAddInManager&gt;True&lt;/UnifyInAddInManager&gt;</strong>
  &lt;/ManifestSettings&gt;
&lt;/RevitAddIns&gt;
</code></pre>
<p>この設定を有効にすると(True)、ユーザーは個々のコマンドのオン/オフを切り替えることができなくなり、グループ全体が単一のユニットとして管理されます。</p>
<p>&#0160;</p>
<p><strong>MEP API: 製造アセンブリタイプのリセット</strong></p>
<p>Revit 2026.2 では、アセンブリ タイプ内で FabricationPart 要素を編成する方法が変更されました。</p>
<p>Revit 2026 RTM では、ジオメトリと変換の類似性に基づいて、複数の製造パーツが同じアセンブリタイプを共有できました。しかし、ユーザーからのフィードバックに基づき、この動作は元に戻されました。以前のバージョンと同様に、各製造パーツには独自のアセンブリタイプが割り当てられるようになりました。</p>
<p>この変更をサポートするために、新しい方法が導入されました。</p>
<pre><code>FabricationPartType.ResetAssemblyTypes(Document doc)</code></pre>
<p>この方法を使用すると、モデル内の製造部品のすべてのアセンブリタイプ定義を手動でリセットできます。この方法は、移行ワークフロー中や、製造要素間の一貫性が重要な場合に使用します。</p>
<hr />
<p><strong>CefSharp の削除</strong></p>
<p><a href="https://adndevblog.typepad.com/technology_perspective/2025/05/revit-2026-api-update.html">こちらのブログ記事</a>で既にご案内しておりますが、CefSharp への依存関係は、Revit 2026 リリース以降、完全に削除されました。この変更は Revit 2026 SDK の初期リリースで既に発表されていますが、多くの既存のアドインに影響を与えるため、改めてご案内させていただきます。</p>
<p>Revit 内で HTML ベースのユーザーインターフェースをレンダリングするためにアドインが CefSharp に依存している場合は、代替ソリューションへの移行をご検討ください。推奨されるアプローチは、積極的にサポートされており、デスクトップアプリケーションで Web コンテンツをホストするための最新の機能を提供する <strong>Microsoft Edge WebView2</strong> を採用することです。</p>
<p>注: Revit 2026 以降では、必要な WebView2 DLL がすでに Revit インストールに含まれているため、エンド ユーザーは追加のランタイム インストールを行う必要はありません。</p>
<hr />
<p>By Ryuji Ogasawara</p>
