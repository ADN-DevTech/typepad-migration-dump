---
layout: "post"
title: "Design Automation API：コアエンジン ライフサイクル ポリシー"
date: "2023-03-27 00:12:33"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2023/03/design-automation-api-core-engine-lifecycle-policy.html "
typepad_basename: "design-automation-api-core-engine-lifecycle-policy"
typepad_status: "Publish"
---

<p>サブスクリプションでオートデスク製品を利用する際、使用可能な製品バージョンがサブスクリプション特典として<a href="https://www.autodesk.co.jp/support/account/manage/versions/previous-versions" rel="noopener" target="_blank">明記</a>されています。</p>
<p>また、2020年の公開された<a href="https://blogs.autodesk.com/technology-manager-community/2020/10/26/expanding-previous-version-access-to-5-versions-back/" rel="noopener" target="_blank">ブログ記事（英語- 現在削除されています）</a>では、4 つ前、5 つ前までのバージョンにアクセスする方法がアナウンスされています。</p>
<p>AutoCAD コアエンジンのみをサポートしていた Design Automation API v2&#0160; を経て、AutoCAD、Revit、Inventor、3ds Max の 4 つのコアエンジンをサポートする Design Automation API v3 が2019 年10月に導入されて以来、コアエンジンにライフサイクル ポリシーが策定されていませんでした。</p>
<p>今回、<a href="https://www.autodesk.co.jp/support/account/manage/versions/support-lifecycle" rel="noopener" target="_blank">前バージョンの製品 | サポート ライフサイクル</a> や前述の製品アクセスに準拠するかたちで、Design Automation API の 4 つのコアエンジン共通のライフサイクル ポリシーが策定され、APS ポータルの&#0160;<a href="https://aps.autodesk.com/en/docs/design-automation/v3/developers_guide/engine-lifecycle/" rel="noopener" target="_blank">Engine Lifecycle Policy</a>&#0160;ページで公開されました。</p>
<p><a class="asset-img-link" href="https://aps.autodesk.com/en/docs/design-automation/v3/developers_guide/engine-lifecycle/" rel="noopener" style="display: inline;" target="_blank"><img alt="Lifecycle_page" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02b6852fa775200d image-full img-responsive" src="/assets/image_12688.jpg" title="Lifecycle_page" /></a></p>
<p>要約すると次のような内容となります。</p>
<ol>
<li>コアエンジンは、対応するデスクトップ製品の最初のリリースから 4 年間サポートされます。</li>
<li>4 年後にコアエンジン バージョンは非推奨となりますが、さらに 2 年間は利用可能で、その後削除されます。</li>
<li>非推奨のエンジンバージョンでは、新しい AppBundle や Activity の登録は出来なくなりますが、登録済の AppBundle/Activity を参照する WorkItem は機能し続けます。</li>
<li>削除されたコアエンジン バージョンを参照する WorkItem は実行出来ません。</li>
</ol>
<p style="padding-left: 40px;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02b7519a9fd7200c-pi" style="display: inline;"><img alt="Da_engine_lifecycle" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02b7519a9fd7200c image-full img-responsive" src="/assets/image_493708.jpg" title="Da_engine_lifecycle" /></a></p>
<p>各コアエンジン バージョンの非推奨期日と廃止期日については、&#0160;<a href="https://aps.autodesk.com/en/docs/design-automation/v3/developers_guide/engine-lifecycle/" rel="noopener" target="_blank">Engine Lifecycle Policy</a> ページをご確認ください。</p>
<p>今後は、デスクトップ製品同様、アプリケーションの定期的な移行計画をお持ちいただければと思います。</p>
<p>By Toshiaki Isezaki</p>
