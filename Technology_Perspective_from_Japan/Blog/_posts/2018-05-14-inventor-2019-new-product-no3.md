---
layout: "post"
title: "Inventor 2019 の新機能 その３"
date: "2018-05-14 02:30:00"
author: "Shigelazu Saitou"
categories:
  - "API カスタマイズ"
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2018/05/inventor-2019-new-product-no3.html "
typepad_basename: "inventor-2019-new-product-no3"
typepad_status: "Publish"
---

<p><a href="http://adndevblog.typepad.com/technology_perspective/2018/04/inventor-2019-new-product-no1.html">先々回</a>は、Inventor 2019製品の「サブスクリプション特典」による「クラウド利用による「共有ビュー」によるコラボレーションデータの共有」や「Autodesk Drive クラウドストレージによる共有」及び「アセンブリ」、<a href="http://adndevblog.typepad.com/technology_perspective/2018/05/inventor-2019-new-product-no2.html">先回</a>は「パーツ」「スケッチ」「iLogicの機能拡張」についてお届けしましたが、今回は引き続き Invento2019 製品の「スプラインの強化」「穴フィーチャ」「カラースキーム」「アセンブリ・図面ビューのパフォーマンスの向上」についてご紹介させていただきたいと思います。<br />
( 本内容は、一部 Developer Days 2018 ( 2018/02 開催 ) の内容を含みます )</p>

<p><br />
<strong>１．スプラインの強化</p>

<p>１－１．補間スプラインの長さが変更されないようにロックする<br />
</strong><br />
スプラインに一般寸法を追加すると、スプラインの長さ全体が変更されないように長さ寸法が追加されます。<br />
コンポーネントを移動またはシフトすると、形状はそれに合わせて変化し続けますが、スプラインの全長は同じままになります。<br />
<a class="asset-img-link"  style="display: inline;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b0224df300e22200b-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b0224df300e22200b image-full img-responsive" alt="Inventor2019_New3_Fig1" title="Inventor2019_New3_Fig1" src="/assets/image_420908.jpg" border="0" /></a><br /></p>

<p><strong>２．「穴」フィーチャーコマンド</strong></p>

<p>新しい[穴]コマンドは、ユーザ インタラクションから穴の配置を推定することで、さらに高速かつスマートになりました。<br />
最初にスケッチを作成しなくても、寸法拘束および同心円拘束を適用できます。その結果、コマンドを終了しなくてもフィーチャ編集とスケッチ編集の間で切り替わります。</p>

<p><strong>生産性の向上</strong><br />
新しい[穴]コマンドでは、次の生産性向上機能が用意されています。</p>

<p><strong>高速化	</strong><br />
効率的なワークフローにより、クリック回数、マウス移動、コンテキスト切り替えが減少し、速度や生産性が向上します。<br />
その他のパフォーマンス向上は、ねじまたはクリアランス データのロードによって実現されます。</p>

<p><strong>機能性向上</strong><br />
配置の選択タイプを簡素化しました。<br />
必要なものを選択する必要はなくなり、操作から推定されます。</p>

<p><strong>堅牢なモデリング</strong><br />
先にスケッチを作成することなく複数の穴を追加できます。</p>

<p><strong>柔軟性</strong><br />
穴を配置するための高速な線分オフセット寸法と、同心円拘束、基となるスケッチを作成するコマンドがあります。</p>

<p><strong>シームレスなワークフロー</strong><br />
穴定義とスケッチの間を移動して、プロパティを追加または修正したり切り替えます。<br />
必要なものを、必要なときだけ	コンテキスト内寸法は、ミニ ツールバーから置き換わり、モデルを操作するときにパラメータに直接アクセスできます。</p>

<p><strong>ユーザ インタフェースの拡張</strong></p>

<p>[穴]コマンドは、ワークフローを合理化し、簡単に再利用するための穴のプリセットを提供します。<br />
<a class="asset-img-link"  style="display: inline;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b0223c84862ed200c-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b0223c84862ed200c image-full img-responsive" alt="Inventor2019_New3_Fig2" title="Inventor2019_New3_Fig2" src="/assets/image_428972.jpg" border="0" /></a><br /></p>

<p><strong>３．カラースキームの強化</p>

<p>カラースキームーエディタの直接利用</strong></p>

<p>Inventor 2019 の一部としてカラー スキーム エディタがインストールされます。<br />
[アプリケーション オプション] > [色]タブからアクセスできます。<br />
<a class="asset-img-link"  style="display: inline;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b0224e03709ce200d-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b0224e03709ce200d img-responsive" alt="Inventor2019_New3_Fig3" title="Inventor2019_New3_Fig3" src="/assets/image_574487.jpg" /></a><br /><br />
<strong>独自のカスタムカラースキームの作成が可能</strong></p>

<p>カスタマイズ内容は Application Options xml ファイルに保存されます。<br />
<a class="asset-img-link"  style="display: inline;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b0224df300f3e200b-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b0224df300f3e200b image-full img-responsive" alt="Inventor2019_New3_Fig4" title="Inventor2019_New3_Fig4" src="/assets/image_697840.jpg" border="0" /></a><br /></p>

<p><strong>４．アセンブリ・図面ビューのパフォーマンスの向上</p>

<p>アセンブリ</strong></p>

<p>大規模アセンブリでのマウスホイール使用時のズーム・回転性能が向上<br />
インプレース編集操作<br />
アセンブリのグローバル/リビルトすべてのパフォーマンスが大幅に改善<br />
パターンコンポーネントフィーチャー選択の性能が向上<br />
アセンブリのエリア選択の反応が大幅に向上<br />
セクションビューのグラフィック表示の反応が大幅に向上<br />
大規模アセンブリのポジションリプリゼンテーション表示の向上<br />
大規模アセンブリのデザインビューの切り替え表示の向上<br />
アダプティブ・フレキシブル コンポーネントの拘束駆動の性能の向上<br />
Frame Generatorに古いメンバーを含むAssembly Global 更新のパフォーマンス<br />
<p class="asset-video"><iframe width="500" height="281" src="https://www.youtube.com/embed/6yMbICuac_U?feature=oembed" frameborder="0" allow="autoplay; encrypted-media" allowfullscreen></iframe></p><br /></p>

<p><strong>図面ビュー</strong></p>

<p>キューブ利用の図面ビューの反映表示速度が大幅に向上<br />
投影ビューへの大規模アセンブリの作成配置速度が大幅に向上<br />
投影ビューのプレビュー速度が向上<br />
大規模アセンブリの配置時・図面を開いた時・ビューの再計算など<br />
図面ビューへの大規模アセンブリの修正後の反映速度が大幅に向上<br />
４つの図面ビューの再配置の速度が大幅に向上<br />
<br /><p class="asset-video"><iframe width="500" height="281" src="https://www.youtube.com/embed/zl9MYl-82r8?feature=oembed" frameborder="0" allow="autoplay; encrypted-media" allowfullscreen></iframe></p><br /></p>

<p>今回は、Inventor 2019製品の「スプラインの強化」「穴フィーチャ」「カラースキーム」「アセンブリ・図面ビューのパフォーマンスの向上」についてお届けしました。</p>

<p>By Shigekazu Saito.</p>
