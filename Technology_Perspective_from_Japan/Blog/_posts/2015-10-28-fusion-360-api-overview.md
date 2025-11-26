---
layout: "post"
title: "Fusion 360 API：概要"
date: "2015-10-28 00:42:37"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2015/10/fusion-360-api-overview.html "
typepad_basename: "fusion-360-api-overview"
typepad_status: "Publish"
---

<p>Fusion 360 が日本語化されましたので、これを機に数回に渡って Fusion 360 API について現在の状況をご紹介していきたいと思います。</p>
<p><strong>Fusion 360 API のコンセプト</strong></p>
<p>Fusion 360 API は、ユーザ インタフェース（User Interface）を介して&#0160;Fusion 360 を操作するのと同じように、API （Application Programming Interface）で Fusion 360 をコントロールすることを目的に開発されています。その意味では、&quot;Fusion エンジン&quot; を駆動させる方法に、UI と API の 2 種類があると考えることが出来ます。</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d16bb0ac970c-pi" style="display: inline;"><img alt="Fusion_360_api" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b8d16bb0ac970c image-full img-responsive" src="/assets/image_799218.jpg" title="Fusion_360_api" /></a></p>
<p>なお、Fusion 360 はクラウド サービスであるため、その機能は定期的なアップデートで順次強化されていきます。このため、現時点では、Fusion 360 が持つすべての機能が API 化されている訳ではありません。現在のところ、モデリング機能に特化した部分の API の機能実装に注力している状態です。</p>
<p>Fusion 360 API &#0160;は、COM API を利用したオートデスクのデスクトップ製品である Inventor や AutoCAD と同様、オブジェクトモデルに沿ってプログラムできるようにデザインされています。ただ、Fusion 360 が Windows だけでなく、Mac にも対応しているため、API で利用しているテクノロジが COM（Component Object Model）ではないことに注意してください。</p>
<ul>
<li>COM は Windows プラットフォーム用に&#0160;Microsoft 社が提唱したアーキテクチャです。オートデスク製品の API における利用テクノロジについては、ブログ記事 &#0160;<a href="http://adndevblog.typepad.com/technology_perspective/2013/11/texhnologies-for-apis-on-autodesk-products.html" target="_blank"><strong>オートデスク製品の API が利用するテクノロジ</strong></a> を参照してみてください。</li>
</ul>
<p><a href="http://help.autodesk.com/cloudhelp/ENU/Fusion-360-API/images/Fusion.pdf" target="_blank"><strong>オブジェクト モデル</strong></a> が示す各オブジェクトは、Fusion 360 上で扱うオブジェクトに対応しています。Fusion データに例えてみると、押し出しフィーチャは ExtrudeFeature オブジェクトに、フィレット フィーチャが FilletFeature オブジェクトに対応しています。</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c7e1e3d2970b-pi" style="display: inline;"><img alt="Object_model" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b7c7e1e3d2970b image-full img-responsive" src="/assets/image_718671.jpg" title="Object_model" /></a></p>
<p>オブジェクトには、メソッドやプロパティなどが関連付けられていて、それらを順次呼び出すことで、つまり、プログラムすることで、一連の処理を自動化したり、カスタムな機能を作成することができます。&#0160;</p>
<p><strong>Fusion 360 API で作成可能なモジュール</strong>&#0160;</p>
<p>Fusion 360 API で作成することが出来るのは、Fusion 360 内部のロードして動作させる&#0160;<strong>スクリプト</strong>&#0160;と&#0160;<strong>アドイン</strong>&#0160;の2つの形態です。スクリプトもアドインも、実装できる内容に大きな違いはありません。両者の唯一の違いは、いまのところ、アドインだけが Fusion 360 起動時の処理を自動化できるという点のみです。もちろん、将来的にもっと大きな違いが出てくる可能性はあります。</p>
<p>オートデスクとしては、Fusion 360 上で比較的よく作業する反復操作を自動化したい一般設計者を、スクリプトのターゲット ユーザとして考えています。ちょうど、Inventor や AutoCAD の VBA マクロのような利用方法を想定していると言えます。</p>
<p>アドインは、Fusion 360 を高度に使いこなしたい上級ユーザや、開発を業務にしているプロフェッショナル デベロッパをターゲット ユーザと考えています。特に、有償販売を意図する開発時には、アドインを選択することをお勧めしています。&#0160;</p>
<p><strong>Fusion 360 API で利用可能な開発言語と環境</strong>&#0160;</p>
<p>Fusion 360 API で選択することが可能な開発言語は、<strong>JavaScript</strong>、<strong>Python</strong>、<strong>C++</strong> の 3 つです。開発言語に VB や C# が存在しないのは、Fusion 360 が Mac をサポートしているためです。Mac には、C# や VB.NET を動作させるのに必要な .NET Framework の正式なバージョンがまだありません。</p>
<p>その意味では、JavaScript と Python には、Windows と Mac の両プラットフォームで利用可能な開発環境が整っています。同時に、Fusion 360 がクラウドを利用する次世代の CAD/CAM であることを考慮して、JavaScript と Python の利用が一般的な&#0160;Web 開発者を取り込んでいきたい意向も反映されています。</p>
<p>実際のプログラミングで使用するエディタやデバッグツールも、Web 開発で一般的なオープン ソースを利用するように努めています。これらは選択する言語別に異なります。</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c7e1e894970b-pi" style="display: inline;"><img alt="Open_source_and_standard" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b7c7e1e894970b image-full img-responsive" src="/assets/image_145213.jpg" title="Open_source_and_standard" /></a></p>
<p><strong>Fusion 360 API の窓口</strong></p>
<p>Fusion 360 は、スクリプトとアドインのいずれのモジュールの開発、実行、デバックをコントロールする共通したアクセス ポイントを提供します。ファイルメニュー内の 「スクリプトとアドイン ...」メニューから表示することが出来る [スクリプトとアドイン] ダイアログです。</p>
<p>&#0160;[スクリプトとアドイン] ダイアログには、[スクリプト] と [アドイン] の 2 つのタブがあり、モジュール別にタブを切り替えて利用することになります。</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c7e1eaca970b-pi" style="display: inline;"><img alt="Access_point" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b7c7e1eaca970b image-full img-responsive" src="/assets/image_353726.jpg" title="Access_point" /></a></p>
<p>いずれのタブでも、次の処理をキックすることが可能です。</p>
<ul>
<li>スクリプト、または アドイン モジュールの新規作成（含む、開発言語の選択）</li>
<li>既存スクリプト、または アドイン モジュールの編集</li>
<li>スクリプト、または アドイン モジュールの実行</li>
<li>スクリプト、または アドイン モジュールのデバッグ（除く C++）</li>
<li>アドインの Fusion 360 起動時の実行指定</li>
</ul>
<p><strong>アドインやスクリプトの入手と販売</strong></p>
<p>オートデスクのデスクトップ製品には、アドイン アプリケーションや電子コンテンツのマーケット プレイスである<a href="https://apps.autodesk.com/ja" target="_blank"><strong> Autodesk App ストア</strong></a>が用意されています。Fusion 360 も例外ではありません。今後、無償版、有償版、体験版/試用版 をも含め、Fusion 360 用のアドインが多く登場するはずです。</p>
<p><strong>Autodesk App ストア&#0160;</strong>に公開された Fusion 360 アドインは、<strong><a href="https://apps.autodesk.com/FUSION/ja/Home/Index" target="_blank">https://apps.autodesk.com/FUSION/ja/Home/Index</a>&#0160;</strong>から直接入手することが出来ます。ダウンロードしたモジュールはインストーラ モジュールとして提供されているので、そのままインストールして&#0160;[スクリプトとアドイン] ダイアログから実行することが出来ます。</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb0886d62e970d-pi" style="display: inline;"><img alt="Fusion_app_store" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01bb0886d62e970d image-full img-responsive" src="/assets/image_771656.jpg" title="Fusion_app_store" /></a></p>
<p>また、Fusion 360 用に作成したアドインを Autodesk App ストアに公開することも出来ます。公開は無償で、公開方法は、<strong><a href="http://www.autodesk.co.jp/developapps" target="_blank">http://www.autodesk.co.jp/developapps</a></strong> に記載されています。</p>
<p>次回は、開発言語毎の特徴と違いなどをご紹介します。</p>
<p>By Toshiaki Isezaki</p>
<p>&#0160;</p>
