---
layout: "post"
title: "Revit 2022 の新機能 その2"
date: "2021-04-16 01:08:20"
author: "Ryuji Ogasawara"
categories:
  - "API カスタマイズ"
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2021/04/new-features-on-revit-2022-part2.html "
typepad_basename: "new-features-on-revit-2022-part2"
typepad_status: "Publish"
---

<p>Revit 2022 の新機能と改良された機能をシリーズでご紹介しております。<br />今回も<a href="https://adndevblog.typepad.com/technology_perspective/2021/04/new-features-on-revit-2022-part1.html">前回に</a>引き続き、プラットフォームに共通の新機能と機能向上の内容となります。</p>
<hr />
<p><strong>集計表の CSV 書き出しのサポート</strong></p>
<p>Revit で集計表を .csv ファイルとして書き出す機能がサポートされるようになりました。</p>
<p>集計表をシートに追加すると、CAD 形式および CSV 形式として書き出すことができます。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0278802393b5200d-pi" style="display: inline;"><img alt="Revit2022-02-01" class="asset  asset-image at-xid-6a0167607c2431970b0278802393b5200d img-responsive" src="/assets/image_927441.jpg" title="Revit2022-02-01" /></a></p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0278802393bb200d-pi" style="display: inline;"><img alt="Revit2022-02-02" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0278802393bb200d img-responsive" src="/assets/image_156157.jpg" title="Revit2022-02-02" /></a></p>
<hr />
<p><strong>シート間で集計表を分割する</strong></p>
<p>集計表を複数のセグメントに分割し、プロジェクト内のシートに各セグメントを配置します。</p>
<p>非常に長い集計表で作業する場合、集計表をセグメントに分割すると便利です。このリリースより前のリリースでは、分割集計表ですべてのセグメントを同じシートに配置する必要がありました。このリリースでは、[分割して配置]機能を使用して集計表を分割し、セグメントのシートを指定できるようになりました。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0263e99e59a8200b-pi" style="display: inline;"><img alt="Revit2022-02-03" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0263e99e59a8200b image-full img-responsive" src="/assets/image_252887.jpg" title="Revit2022-02-03" /></a></p>
<p>集計表は、選択したシート間で均等の高さのセグメントに分割されるか、指定したカスタム高さの集計表セグメントに分割されます。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0263e99e5b4e200b-pi" style="display: inline;"><img alt="Revit2022-02-04" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0263e99e5b4e200b image-full img-responsive" src="/assets/image_841305.jpg" title="Revit2022-02-04" /></a></p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0263e99e59b7200b-pi" style="display: inline;"><img alt="Revit2022-02-05" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0263e99e59b7200b image-full img-responsive" src="/assets/image_209123.jpg" title="Revit2022-02-05" /></a></p>
<hr />
<p><strong>集計キーの共有パラメータ</strong></p>
<p>集計キーで共有パラメータを使用して、モデル内の要素のプロパティを設定および変更します。</p>
<p>予めファミリに共有パラメータを設定しておきます。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0278802393ce200d-pi" style="display: inline;"><img alt="Revit2022-02-06" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0278802393ce200d image-full img-responsive" src="/assets/image_68895.jpg" title="Revit2022-02-06" /></a></p>
<p>集計表を作成します。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0263e99e59bc200b-pi" style="display: inline;"><img alt="Revit2022-02-07" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0263e99e59bc200b image-full img-responsive" src="/assets/image_47349.jpg" title="Revit2022-02-07" /></a></p>
<p>カテゴリに関連付けられている共有インスタンス パラメータは、そのカテゴリの集計キーを作成するときに、使用可能なフィールドとして表示されるようになりました。集計キーを作成するときに、共有パラメータをカテゴリに追加することもできます。<br /><br /><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b026bdecba778200c-pi" style="display: inline;"><img alt="Revit2022-02-08" border="0" class="asset  asset-image at-xid-6a0167607c2431970b026bdecba778200c image-full img-responsive" src="/assets/image_900153.jpg" title="Revit2022-02-08" /></a><br />集計表に行を追加して、集計キー毎に共有パラメータの値を設定します。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0263e99e59c7200b-pi" style="display: inline;"><img alt="Revit2022-02-09" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0263e99e59c7200b image-full img-responsive" src="/assets/image_973323.jpg" title="Revit2022-02-09" /></a><br />ファミリインスタンスのプロパティパネル上で、コンボボックスを通じて集計キーのリストから1つ選択することができます。集計キーを選択すると、集計表で設定した共有パラメータの値セットが自動的に割り当てられます。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0263e99e59cd200b-pi" style="display: inline;"><img alt="Revit2022-02-10" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0263e99e59cd200b image-full img-responsive" src="/assets/image_415672.jpg" title="Revit2022-02-10" /></a></p>
<p>集計キーによってジオメトリを操作したり、ファミリの表示をコントロールすることができます。ファミリで共有パラメータを使用し、集計キーに共有パラメータを含めて、集計キーを使用してファミリ ジオメトリをコントロールします。</p>
<hr />
<p><strong>マルチカテゴリ集計とマテリアル集計のカテゴリ リストの拡張</strong></p>
<p>これまでマルチカテゴリ集計やマテリアル集計では使用できなかったシステム カテゴリとサブカテゴリを使用できるようになりました。</p>
<p>この機能強化により、[集計表/数量]および[マテリアル集計]のマルチカテゴリ集計に追加のシステム カテゴリが含まれるようになり、Revit の集計機能が改善されました。マルチカテゴリ集計を作成するときに、より多くのカテゴリとサブカテゴリから選択できるようになりました。この改善により、システム カテゴリの集計機能が強化されました。</p>
<hr />
<p><strong>集計表にワークセット パラメータを追加する</strong></p>
<p>ワークシェアリング モデルで集計表を作成する際に、ワークセット パラメータを追加できるようになりました。<br />この更新では、要素のワークセットを管理する新しい方法が提供されています。</p>
<hr />
<p><strong>集計表ビューでファミリとタイプでフィルタする</strong></p>
<p>集計表ビューとマテリアル集計ビューで、ファミリ パラメータとタイプ パラメータを使用してフィルタします。<br />これにより、単一カテゴリ集計表またはマルチカテゴリ集計表でのファミリのフィルタリングが向上します。</p>
<hr />
<p><strong>既定のカラー スキームの改善</strong></p>
<p>新しい既定のパステル カラー スキームが追加されました。</p>
<p>以前は、自動化されたカラー スキームで暗く強い色が使用されており、印刷すると、ビュー内の他の要素が見づらくなることがありました。<br />カラー スキームが更新され、薄いパステル カラーが使用されるようになりました。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b026bdecba824200c-pi" style="display: inline;"><img alt="Revit2022-02-11" class="asset  asset-image at-xid-6a0167607c2431970b026bdecba824200c img-responsive" src="/assets/image_836124.jpg" title="Revit2022-02-11" /></a></p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0263e99e5a4e200b-pi" style="display: inline;"><img alt="Revit2022-02-12" class="asset  asset-image at-xid-6a0167607c2431970b0263e99e5a4e200b img-responsive" src="/assets/image_203259.jpg" title="Revit2022-02-12" /></a></p>
<hr />
<p><strong>改訂の番号付けの拡張機能</strong></p>
<p>カスタムの改訂番号付けシーケンスを作成し、プロジェクトの改訂に異なるシーケンスを割り当てます。</p>
<p>改訂の番号付けの機能強化には、次が含まれます。</p>
<ul>
<li>複数の番号付けシーケンスを作成およびカスタマイズできます。プロジェクト内の改訂に異なる番号付けシーケンスが適用されるため、モデル内の改訂を管理する際の柔軟性が向上します。</li>
<li>数値シーケンスに使用する最小桁数を指定します。たとえば、最小桁を 1 に設定すると、シーケンスは 1、2、3、4 のようになります。最小桁を 3 に設定すると、シーケンスは 001、002、003、004 のようになります。</li>
<li>プロジェクト標準の転送を使用して、カスタム番号付けシーケンスを 1 つのプロジェクトから別のプロジェクトに転送します。</li>
</ul>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b026bdecba8de200c-pi" style="display: inline;"><img alt="Revit2022-02-13" class="asset  asset-image at-xid-6a0167607c2431970b026bdecba8de200c img-responsive" src="/assets/image_551640.jpg" title="Revit2022-02-13" /></a></p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b026bdecba8e2200c-pi" style="display: inline;"><img alt="Revit2022-02-14" border="0" class="asset  asset-image at-xid-6a0167607c2431970b026bdecba8e2200c image-full img-responsive" src="/assets/image_974301.jpg" title="Revit2022-02-14" /></a></p>
<hr />
<p><strong>追加のモデル カテゴリ</strong></p>
<p>モデル内の要素をより詳細にコントロールできるように、8 つの新しいモデル カテゴリが追加されました。既存のインフラストラクチャ カテゴリに新しい機能が追加されました。</p>
<p>次のモデル カテゴリがプロジェクトおよびファミリで使用するために追加されました。<br />新しいカテゴリはすべてビューで切り取り可能で、タグ付けすることができ、マテリアル集計で使用できます。これらのカテゴリは、専門分野と表示/グラフィックスの設定に関して一般モデルとして動作します。</p>
<ul>
<li>給食設備</li>
<li>医療設備</li>
<li>消火</li>
<li>垂直動線</li>
<li>音声映像機器</li>
<li>標識</li>
<li>ハードスケープ</li>
<li>一時的な構造</li>
</ul>
<p>ロード可能なファミリは、次の既存のインフラストラクチャ カテゴリを使用して作成できます。</p>
<ul>
<li>道路</li>
<li>橋台</li>
<li>軸受</li>
<li>橋脚</li>
<li>橋梁フレーム</li>
<li>橋梁ケーブル</li>
<li>橋床版</li>
<li>振動管理</li>
<li>伸縮継手</li>
<li>構造緊張材</li>
</ul>
<hr />
<p>次回は、建築設計分野の新機能と改良された機能についてご紹介致します。</p>
<p>By Ryuji Ogasawara</p>
