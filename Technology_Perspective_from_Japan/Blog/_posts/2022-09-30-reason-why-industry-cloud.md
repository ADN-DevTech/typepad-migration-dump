---
layout: "post"
title: "なぜインダストリー クラウドなのか？"
date: "2022-09-30 00:16:23"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2022/09/reason-why-industry-cloud.html "
typepad_basename: "reason-why-industry-cloud"
typepad_status: "Publish"
---

<p>すでにクラウド ベースの Fusion Team や Autodesk Construction Cloud（ACC）があるのに、なぜ、インダストリー クラウドが必要になのでしょうか？</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02a308dfd9a2200c-pi" style="display: inline;"><img alt="Aps_industry_cloud" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02a308dfd9a2200c image-full img-responsive" src="/assets/image_469908.jpg" title="Aps_industry_cloud" /></a></p>
<p>理由は「粒状データ」を扱う共通した「プラットフォーム」を提供するためです。</p>
<p>昨年、<a href="https://adndevblog.typepad.com/technology_perspective/2021/10/forge-as-a-platform.html" rel="noopener" target="_blank">AU 2021：プラットフォームとしての Forge と経緯</a> の記事でもご紹介したことがありますが、オートデスクが「データ」に注目/改革して「プラットフォーム」を構築しようとしている点について、改めてご案内したいと思います。</p>
<hr />
<p>クラウドの採用で、異なる場所にいる関係者とのデータ共有、コラボレーションといった作業が飛躍的に向上しています。更に、旧名 Autodesk Forge の各種 API を使って、そのようなプロセスやワークフローをカスタマイズしたり、タスクを自動化したりする環境を併用することで、更なる生産性の向上につなげることが可能になっています。</p>
<p>ただ、扱うデザイン データ自体はデータを作成した製品によって左右されてしまい、コラボレーション時の相互コミュニケーションで問題を生じてしまうケースがあるのも事実です。また、データは常に「ファイル」として扱われるため、デザイン データのほんの一部の情報を共有したい場合でも、大きなサイズのファイルを丸ごとコラボレーションに利用する必要があり、利便性に欠ける例も散見されています。最近では、ファイルサイズが肥大化しがちな BIM、3D データの活用で、その傾向が顕著です。</p>
<p>Forge を使ったクラウド環境では、約 70 種類のデザイン ファイルを変換、Web ブラウザにストリーミング配信する仕組みを導入することで、ファイル形式の差を打ち消すアプローチを提供していますが、ファイル主体の処理は変わりありません。</p>
<p>例えば、Inventor で作成したアセンブリ ファイルを共有したい場合、USB ドライブなどの物理的なメディアを利用しなくても、クラウドを介して関係者に渡すことが出来ます。ただ、完全なデータの解釈には、受け手側が Inventor を持っていることが暗黙の了解となってしまいます。もし、受け手側が Revit しか持っていない場合には、アセンブリ ファイルを開いて、その中から必要な情報を抽出することは出来ません。その逆も然りです。<a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02a308df669f200c-pi" style="display: inline;"><img alt="File_interoperability" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02a308df669f200c image-full img-responsive" src="/assets/image_995431.jpg" title="File_interoperability" /></a></p>
<p>そこで、ファイル形式とサイズの問題を解決すべく考え出されたのが「Forge Data」と<span style="text-decoration: underline;">言われた</span>アーキテクチャと「粒状データ」です。</p>
<p>言い換えるなら、クラウドにアップロードされたファイルを、扱うべき最小単位に分解して「粒状化」することで、本当に必要なデータのみを自由に利用し合える環境の構築です。これによって「ファイル形式」を超えて、齟齬なくコミュニケーションすることが可能になります。また、Forge 登場時に考えられていた <a href="https://adndevblog.typepad.com/technology_perspective/2017/11/consider-about-forge-hfdm.html" rel="noopener" target="_blank">HFDM</a> が持つデータ更新のリアルタイム性実現も期待することが出来ます。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02a30d4cc0d6200b-pi" style="display: inline;"><img alt="Extract_data" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02a30d4cc0d6200b image-full img-responsive" src="/assets/image_476927.jpg" title="Extract_data" /></a></p>
<p>「粒状データ」の利点は他にもあります。<span style="text-decoration: underline;">例として</span>、設計変更が頻繁に起こりがちな「仕掛り中」の状態で、クラウドを使ってデザイン データを管理することを考えてみましょう。</p>
<p>1 GB ある Revit プロジェクト（.rvt ファイル）があると仮定します。このプロジェクトに、ドアのファミリ インスタンス タイプを変える変更を加えたとします。このファイルを BIM 360 やクラウド ストレージにアップロードすると、新しいバージョンが作られます。<span style="text-decoration: underline;">仮に</span>、類似した小さな変更を 99 回加えて、都度、クラウド ストレージにアップロードすることを考えると、最初のバージョンも含めて合計100 GB のファイルがクラウド上に保持されることになります。（下図：左）</p>
<p>一方、最初のバージョンをクラウド上で「粒状化」して、粒状データの書き込みや読み込みの機能が Revit で利用出来るようになると仮定すると、ドア インスタンスに加えた<span style="text-decoration: underline;">変更だけ</span>をクラウド上に反映することが出来るようになります。このような変更を 100 回繰り返したとしても、全体のデータのサイズは 100 GBと比較するまでもなく小さいはずです。（下図：右）</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02a30d4cf749200b-pi" style="display: inline;"><img alt="Versioning" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02a30d4cf749200b image-full img-responsive" src="/assets/image_603029.jpg" title="Versioning" /></a></p>
<p>粒状データを使ったデータ管理では、変更をアップロードした際の「スナップショット」をバージョンとして管理することになります。</p>
<p>現時点で上記のようなデータを扱える CAD 製品も存在しています。Fusion 360 です。</p>
<hr />
<p>こういった環境を業種別に分けて提供するのは、業界別に扱うデータの粒状レベル、内容、種類などの違いがあるためです。</p>
<p>Autodesk Platform Services では、このアーキテクチャを <strong>Cloud Information Model</strong> と呼んでいます。</p>
<p>業界別には、Product Information Model（Fusion Data）、（暫定的に）AEC Information Model と M&amp;E Information Model としています。 もちろん、これらを実現するのが、インダストリー クラウドとしてアナウンスされた、製造業向けの <strong>Autodesk Fusion™</strong>、建設業向けの <strong>Autodesk Forma™</strong>、メディア・エンターテイメント業向けの <strong>Autodesk Flow™</strong> になります。</p>
<p>オートデスクは、Autodesk Platform Services の API 群を使い、既存のデスクトップ製品やクラウド サービスへの機能追加や新製品の導入で、今後数年をかけてインダストリー クラウドを形作っていく計画です。もちろん、その過程で、3rd party の開発者がカスタム粒状データを扱えるようになっていくはずです。</p>
<p>By Toshiaki Isezaki</p>
