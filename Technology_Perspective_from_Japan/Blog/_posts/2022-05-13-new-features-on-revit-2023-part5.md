---
layout: "post"
title: "Autodesk Revit 2023 の新機能 ～ その5"
date: "2022-05-13 01:20:50"
author: "Ryuji Ogasawara"
categories:
  - "API カスタマイズ"
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2022/05/new-features-on-revit-2023-part5.html "
typepad_basename: "new-features-on-revit-2023-part5"
typepad_status: "Publish"
---

<p><a href="https://adndevblog.typepad.com/technology_perspective/2022/05/new-features-on-revit-2023-part4.html">前回の記事</a>に引き続き Autodesk Revit 2023 の新機能として、今回は、構造設計に関する新機能と機能改善をご紹介致します。</p>
<hr />
<p><strong>BIM コーディネーションの構造解析モデリング</strong></p>
<p>Revit 2023 では、構造解析モデリングの機能が大規模に見直され、解析用モデリングに新しいアプローチが導入されました。<br />解析用モデルは、BIM データの重要な要素であり、建設モデルや解析ソフトウェアの構造設計と連携するワークフローの一部です。</p>
<p>新しいアプローチにより、次のような利点がもたらされます。</p>
<ul>
<li>BIM のコンテキストで解析用モデリングを行う際の精度と汎用性の向上。</li>
<li>Revit から実行できる解析を中心としたワークフロー。</li>
<li>解析用モデルの自律性を活用した効果的なコラボレーション。</li>
<li>Revit ドキュメントの成果物の解析用モデル データによる補完。</li>
<li>BIM コンプライアンスのための解析用モデルの品質管理。</li>
</ul>
<p><span style="text-decoration: underline;">物理モデルの作成時に、解析要素が自動的に作成されないようになりました。</span><br /><span style="text-decoration: underline;">自律的な解析用モデルは、ノードで相互接続された部材、パネル、リンクなどの要素によって表されます。</span></p>
<p><a class="asset-img-link" href="/assets/image_869975.jpg" style="display: inline;"><img alt="Revit 2023 Part5-1" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0278807caf34200d image-full img-responsive" src="/assets/image_869975.jpg" title="Revit 2023 Part5-1" /></a></p>
<p><br /><strong>正確な構造解析モデリングの自動化</strong></p>
<p>オンデマンドで設定可能なルールを使用して、接続された解析用モデルを自動的に作成します。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02942faa6568200c-pi" style="display: inline;"><img alt="Revit2023-05-02" class="asset  asset-image at-xid-6a0167607c2431970b02942faa6568200c img-responsive" src="/assets/image_341045.jpg" title="Revit2023-05-02" /></a></p>
<p>構造解析用モデルの自動化により、自動的に作成されたモデルが常に一貫性を持ち、接続されるため、面倒な修正を行う必要がなくなります。</p>
<p>ビジュアル スクリプトを使用して解析用モデルの作成のルールを簡単に調整およびカスタマイズでき、プロジェクト タイプ固有のモデリング要件に対応できます。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02942faa656e200c-pi" style="display: inline;"><img alt="Revit2023-05-03" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02942faa656e200c image-full img-responsive" src="/assets/image_912087.jpg" title="Revit2023-05-03" /></a></p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02942faa6578200c-pi" style="display: inline;"><img alt="Revit2023-05-04" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02942faa6578200c image-full img-responsive" src="/assets/image_946129.jpg" title="Revit2023-05-04" /></a></p>
<p><br /><strong>ライブラリベースの接合設計の自動化</strong></p>
<p>ライブラリベースの接合設計の自動化を使用すると、設計意図をより迅速にモデリングでき、さらに構造エンジニアリングと製造ルールを埋め込んで反復を減らすことができます。</p>
<p>設計ニーズに合わせてルールをカスタマイズし、鉄骨建築のコストや施工性をより迅速かつ正確に見積もることができます。</p>
<p>Revit 2023 には、定義された適用範囲に基づいて鉄骨接合を配置するためのサンプル ルールが用意されています。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02942faa6583200c-pi" style="display: inline;"><img alt="Revit2023-05-05" class="asset  asset-image at-xid-6a0167607c2431970b02942faa6583200c img-responsive" src="/assets/image_470398.jpg" title="Revit2023-05-05" /></a></p>
<p>また、定義済みの接合のテーブルを含む一部の地域規格もサポートされており、サンプルの鉄骨接合ライブラリのコンテンツを提供します。これらの接合ライブラリはカスタマイズ可能で、地域のニーズに合わせて調整や拡張を行うことができます。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0278807caf4d200d-pi" style="display: inline;"><img alt="Revit2023-05-06" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0278807caf4d200d image-full img-responsive" src="/assets/image_454328.jpg" title="Revit2023-05-06" /></a></p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0278807caf54200d-pi" style="display: inline;"><img alt="Revit2023-05-07" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0278807caf54200d image-full img-responsive" src="/assets/image_669856.jpg" title="Revit2023-05-07" /></a></p>
<p>&#0160;</p>
<p><strong>適合鉄筋の伝播</strong></p>
<p>形状駆動鉄筋またはフリー フォーム鉄筋をコピーして、ソース ホストから目的のホストに適合させます。鉄筋は、鉄筋の拘束を一致させることで適合します。</p>
<p>鉄筋をソース ホストから目的のホストにコピーする場合、リボンの新しいコマンドを使用して、面またはホストごとに位置合わせすることができます。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0282e15536fc200b-pi" style="display: inline;"><img alt="Revit2023-05-08" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0282e15536fc200b image-full img-responsive" src="/assets/image_378778.jpg" title="Revit2023-05-08" /></a></p>
<p class="asset-video"><iframe allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture" allowfullscreen="" frameborder="0" height="400" src="https://www.youtube.com/embed/aAXbuP5tOlA?feature=oembed" width="712"></iframe></p>
<p><br /><strong>3D ビューでの有効なソリッド鉄筋の表示</strong></p>
<p>3D ビューの詳細レベルが[詳細]に設定されている場合、鉄筋はソリッドとして表示されます。<br />3D ビューでは鉄筋カプラー要素は常にソリッドとして表示されます。</p>
<p class="asset-video"><iframe allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture" allowfullscreen="" frameborder="0" height="400" src="https://www.youtube.com/embed/Jqyk25fAzEI?feature=oembed" width="712"></iframe></p>
<p><br /><strong>リンク モデルの 3D ビューで鉄筋をソリッドとして表示する</strong></p>
<p>要素のモデリング中に、リンク モデルの鉄筋との衝突を回避します。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0282e1553679200b-pi" style="display: inline;"><img alt="Revit2023-05-09" class="asset  asset-image at-xid-6a0167607c2431970b0282e1553679200b img-responsive" src="/assets/image_541982.jpg" title="Revit2023-05-09" /></a></p>
<p><br /><strong>変位鉄筋表現</strong></p>
<p>ドキュメントを読みやすく、また理解しやすくするため、鉄筋を変位できるようになりました。</p>
<p class="asset-video"><iframe allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture" allowfullscreen="" frameborder="0" height="400" src="https://www.youtube.com/embed/tldMk4KP_jA?feature=oembed" width="712"></iframe></p>
<p><br /><strong>鉄筋のマルチ引出線タグ</strong></p>
<p>同じタグを使用して、複数の鉄筋要素にタグを付けることができます。タグの配置中、または既存のタグにホスト(引出線)を追加する場合は、[ホストを追加/削除]オプションを使用します。</p>
<p>タグ引出線の外観をカスタマイズしたり、タグ引出線を一緒に調整したり、個々のタグ引出線の表示/非表示を変更することもできます。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0282e1553680200b-pi" style="display: inline;"><img alt="Revit2023-05-10" class="asset  asset-image at-xid-6a0167607c2431970b0282e1553680200b img-responsive" src="/assets/image_131828.jpg" title="Revit2023-05-10" /></a></p>
<p>By Ryuji Ogasawara</p>
