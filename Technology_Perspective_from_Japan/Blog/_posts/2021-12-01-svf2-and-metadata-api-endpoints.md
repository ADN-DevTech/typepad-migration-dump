---
layout: "post"
title: "SVF2 とメタデータ"
date: "2021-12-01 00:01:25"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2021/12/svf2-and-metadata-api-endpoints.html "
typepad_basename: "svf2-and-metadata-api-endpoints"
typepad_status: "Publish"
---

<h2><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0278805b61a3200d-pi" style="display: inline;"><img alt="Svf1_vs_svf2" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0278805b61a3200d image-full img-responsive" src="/assets/image_836911.jpg" title="Svf1_vs_svf2" /></a><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0278805b2668200d-pi" style="display: inline;"><br /></a></h2>
<p><a href="https://adndevblog.typepad.com/technology_perspective/2021/07/start-svf2-operation.html" rel="noopener" target="_blank"><strong>SVF2 の正式サポート</strong></a>にあたって、<a href="https://adndevblog.typepad.com/technology_perspective/2020/11/utilizeing-meta-data.html" rel="noopener" target="_blank"><strong>メタデータ</strong></a>（プロパティ）へのアクセスを支援する機能が追加されています。まずは、SVF と SVF2 のメタデータの違いについてご案内します。</p>
<p>SVF2（Streaming Vector Format 2）形式では、個々のインスタンスを持つ代わりに、同等形状のジオメトリを共有参照して使用メモリの低減を図っています。SVF や SVF2 に関わらず、内包する個々のオブジェクトは dbId（objectId）を使用して識別することで、メタデータを取得、作業することができます。</p>
<p><a href="https://adndevblog.typepad.com/technology_perspective/2020/11/utilizeing-meta-data.html" rel="noopener" target="_blank"><strong>Model Derivative API：メタデータの活用</strong></a> のブログ記事でもご紹介したとおり、このとき、<strong>externalId</strong> は Model Derivative API が変換したシード ファイル（ソースとなったデザイン ファイル）の形式が持つ不変、かつ、一意な ID である点に変わりはありません。例えば、Revit では要素のUniqueID プロパティ、また、AutoCAD ではハンドル番号が該当します。</p>
<p>ただし、Inventor のデザイン ファイル形式など、すべてのシード ファイルが externalId の元になる不変なオブジェクト識別子を持っているとは限りません。</p>
<p>このため、一部、オブジェクトと外部データをリンクするような場合、 dbId（objectId）を使用するケースが存在します。（Model Derivative API での変換を繰り返さなければ、Forge Viewer で表示する際の dbid は、同じジオメトリに対して同じになります。）</p>
<p>SVF2 のベータ版が登場した際、SVF の dbId とメタデータを提供していましたが、SVF2 とのコンセプト上の違いから、SVF と SVF2 運用時の dbId の不一致が指摘され、仕様の変更を検討することになりました。特に、BIM 360 を利用して SVF が持つ dbId を利用していた場合、ワークフローが期待した動作にならない問題が報告されました。</p>
<p>そこで、SVF と SVF2 形式の間で、開発者が選択することが出来る管理オプションが追加されることになった経緯があります。</p>
<p><strong>Model Derivative API</strong> の観点では、それぞれの形式が相互に排他的なものと考えるコンセプトを提供しています。</p>
<ul>
<li>変換時に SVF 形式を指定した場合、メタデータ API の endpoint は&#0160; &quot;SVF データ &quot; と dbId を提供します。&#0160;</li>
<li>変換時に SVF2 出力を指定した場合、メタデータ API の endpoint は &quot;SVF2 データ &quot; と dbId を提供します。&#0160;</li>
</ul>
<p>Forge Viewer が SVF2 をロードした場合、Forge Viewer API 内部でメタデータを参照する際には SVF2 の dbId を使用します。一方、 Forge Viewer が SVF をフォールバックした場合（SVF2 をロードしようとして失敗し、代わりに、SVF をロードした場合）、内部でメタデータを参照する際には SVF の dbid を使用します。Forge Viewer はメタデータを取得するために、メタデータ API endpoint そのものは使用しないため、識別子上の競合は発生することはありません。</p>
<p><strong>BIM 360 Docs&#0160;</strong>の観点から見ると、もう少し複雑になります。&#0160;</p>
<p>外部データ接続の観点から、開発者が <span style="background-color: #ffff00;">externalId を使用していた場合、 externalId&#0160; は常に正確であるため問題は起こりません。</span>しかし、SVF の&#0160; dbId を使って外部データと接続している場合、後のBIM 360 の変換処理で SVF2 を生成した場合、初回の SVF 変換の結果とミスマッチが発生してしまいます。</p>
<p>そこで、必要に応じて SVF メタデータの生成を支援するために、一部の endpoint にオプションの追加パラメータを公開しました。変更された endpoint の詳細は、こちらの&#0160;<strong><a href="https://forge.autodesk.com/en/docs/model-derivative/v2/change_history/changelog/#release-date-2021-08-26" rel="noopener" target="_blank">Changelog</a></strong> を参照してください。</p>
<p>ここで登場する 2 つの主な endpoint は、BIM 360 の ドキュメント URN を使用して呼び出されるメタデータの endpoint です。変換出力は SVF2 になっている状態でも、SVF の dbId 指向のメタデータにアクセスする必要がある場合、これらの endpoint を使用して、&quot;<strong>x-ads-derivative-format</strong>&quot; というヘッダーパラメータに新しい &quot;fallback &quot;値を設定することで、endpoint に SVF 形式のメタデータを返させることが出来るようになります。</p>
<p><a class="asset-img-link" href="https://forge.autodesk.com/en/docs/model-derivative/v2/reference/http/job-POST/" rel="noopener" style="display: inline;" target="_blank"><img alt="Post_job" border="0" class="asset  asset-image at-xid-6a0167607c2431970b026bdf047810200c image-full img-responsive" src="/assets/image_398483.jpg" title="Post_job" /></a></p>
<p>なお、これは利便性のために提供されているものであり、最終的には SVF2 メタデータに移行することが望ましいでしょう。（また、<span style="background-color: #ffff00;">可能な限りデータのリンクに externalId を使用することが理想的です。</span>）</p>
<p>Forge Viewer v7.46 を題材に、SVF と SVF2 間の dbId のマッピング方法を紹介するブログ記事 <strong><a href="https://forge.autodesk.com/blog/temporary-workaround-mapping-between-svf1-and-svf2-ids" rel="noopener" target="_blank">Temporary workaround for mapping between SVF1 and SVF2 IDs</a></strong>、Forge Viewer v7.46 の <strong><a href="(https://forge.autodesk.com/en/docs/viewer/v7/change_history/changelog_v7" rel="noopener" target="_blank">Changelog</a></strong> でも併せて確認してみてください。</p>
<p>By Toshiaki Isezaki</p>
