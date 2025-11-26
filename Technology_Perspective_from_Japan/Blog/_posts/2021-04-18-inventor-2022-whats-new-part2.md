---
layout: "post"
title: "Inventor 2022 新機能～ その2"
date: "2021-04-18 20:01:00"
author: "Takehiro Kato"
categories:
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2021/04/inventor-2022-whats-new-part2.html "
typepad_basename: "inventor-2022-whats-new-part2"
typepad_status: "Publish"
---

<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0278802344df200d-pi" style="display: inline;"><img alt="Autodesk-inventor-badge-1024" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0278802344df200d image-full img-responsive" src="/assets/image_852887.jpg" title="Autodesk-inventor-badge-1024" /></a></p>
<p>&#0160;</p>
<p>これまで、Inventorを利用しながら以下のようなことをしたいと思ったことはありませんでしょうか。</p>
<ul>
<li>複数のバージョンや、複数のオプションを表示したい。</li>
<li>一時的にコンポーネントを削除したい。</li>
<li>複数のポジション、位置、コンフィグレーションで表示したい</li>
<li>シミュレーション向けやエクスポートするために、ファイルサイズを減らすなどをしたシンプルなバージョンを作成したい。</li>
</ul>
<p>&#0160;</p>
<p>これらを実現するために、iFeatures、 iParts、iAssembly、iLogic、シュリンクラップ代替 LOD’s (Level of details representations)、 派生パーツやポジション リプレゼンテーション等の機能を活用してきたのではないかと思います。</p>
<p>&#0160;</p>
<p>Inventor 2022では、これら既存機能のワークフローをよりシンプルな形に置き換え、かつ新しいワークフローを実現できる新規機能『モデル状態』を提供します。</p>
<p>&#0160;</p>
<p>今回の記事では、Inventor 2022の新機能のうちモデル状態（Model State）について、ご紹介していきたいと思います。</p>
<p>&#0160;</p>
<p><span style="font-size: 8pt;">Inventor 2022の新機能の概要はこちらの動画をご参照ください</span></p>
<p><iframe allowfullscreen="" frameborder="0" height="270" src="//www.youtube.com/embed/oe6DOpyP-HA" width="480"></iframe></p>
<p><span style="font-size: 8pt;">※日本語字幕付きの動画は<a href="https://videos.autodesk.com/zencoder/content/dam/autodesk/www/products/autodesk-inventor-family/fy22/whats-new/videos/inventor-2022-whats-new-video-overview-ja.mp4">こちら</a>をご参照ください</span></p>
<p>&#0160;</p>
<p><strong><span style="text-decoration: underline;"><span style="font-size: 15pt;">モデル状態 概要</span></span></strong></p>
<p>1つのドキュメント内に、パートまたはアセンブリの複数の表現を作成することができるようになりました。</p>
<p>これにより、例えば「製造プロセスごとの形状」、「簡略化レベルの切り替え」、「製品ファミリ作成」、「アジャスト可能部品またはフレキシブルな部品の表現」などを1つのドキュメントで表現することが可能となります。</p>
<p>この新しいワークフローにより、設計、エンジニアリング、製造、モデルバリエーションを管理する簡便な方法を提供します。</p>
<p>1ファイル内に、複数の設計バリエーションが含めることができるため、表現を変えて別のファイルへの出力すること、その結果として発生した複数のファイルを管理するなどの作業が不要となります。</p>
<p>&#0160;</p>
<p>「モデル状態」機能を用いて、以下のような内容を状態毎に設定をすることがが可能です。</p>
<ul>
<li>パラメーター値</li>
<li>フィーチャ</li>
<li>コンポーネント</li>
<li>部品表</li>
<li>iProperty</li>
<li>パラメーター</li>
<li>部品表</li>
<li>材料/色</li>
</ul>
<p>&#0160;</p>
<p><iframe allowfullscreen="" frameborder="0" height="270" src="//www.youtube.com/embed/FSQ-KX4P9IA" width="480"></iframe></p>
<p><span style="font-size: 8pt;">※日本語字幕付きの動画は<a href="https://autodesk.wistia.com/medias/bc4vpq61gm?fbclid=IwAR2R_PAn_q1ptEQ9r5vgAvJvpM5a4rkakeD2XjXXda6J-VSFIcv_agMuJPU">こちら</a>をご参照ください</span></p>
<p>&#0160;</p>
<p><span style="text-decoration: underline;"><strong><span style="font-size: 15pt;">製品ファミリ作成</span></strong></span></p>
<p>モデル状態を用いることで、製品ファミリを作成することが可能となります。</p>
<p>iPartsでも同様のことを行うことができますが、何が異なるのでしょうか？</p>
<p>モデル状態とiPartの違いは、モデル状態メンバのバリエーションがコンポーネントのファイル内に保存されることにあります。iPartのように追加のメンバーファイルの作成や管理をする必要はありません。</p>
<p>各メンバは、メンバ自身のフィーチャやパーツ、パラメータ値、iPropertyを持つことができ、それぞれをファミリ全体として変更することも、個別のメンバで変更することもできます。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0263e99e4d53200b-pi" style="display: inline;"><img alt="図2" class="asset  asset-image at-xid-6a0167607c2431970b0263e99e4d53200b img-responsive" src="/assets/image_320784.jpg" title="図2" /></a></p>
<p>&#0160;</p>
<p>ただし、iPartが完全に必要なくなったというわけではありません。Inventor 2022でも、既存のiPartはサポートされており、利用することができます。</p>
<p>例えば、コンテンツセンターのコンポーネントを作成するには、iPartを使用する必要があります。</p>
<p>また、Vault Basicでデータを管理し、個々のメンバの変更をトラックしたい場合はする場合には、iPartsを利用するのが最適解となります。</p>
<p>&#0160;</p>
<p>なお、モデル状態とiPart、 iAssemblyは両方使用することはできません。</p>
<p>以前のバージョンでiPartファクトリを使用しているファイルが有った場合はモデル状態は表示されません。</p>
<p>&#0160;</p>
<p><span style="text-decoration: underline;"><strong><span style="font-size: 15pt;">フレキシブルな部品の表現</span></strong></span></p>
<p>アセンブリ作成時に編集する必要があるパート、例えば、アセンブリでフィットするようにシートメタルブラケットに角度をつけるを等、がある場合があります。</p>
<p>この場合パート番号は同じですが、製造ラインでどのような外見となるかではなく、アセンブル時にどのような状態になるかを見たくなります。</p>
<p><br />アセンブリされた角度でのコンポーネントを表すように、ブラケットの角度を制御するパラメータを上書したモデル状態がオンザフライで作成されます。</p>
<p>これにより、追加のメンバーファイルを生成せずに部品番号を同じ状態にしたままにできます。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02788023875a200d-pi" style="display: inline;"><img alt="図3" class="asset  asset-image at-xid-6a0167607c2431970b02788023875a200d img-responsive" src="/assets/image_849662.jpg" title="図3" /></a></p>
<p>&#0160;</p>
<p><span style="text-decoration: underline;"><strong><span style="font-size: 15pt;">製造プロセス／組立</span></strong></span></p>
<p>モデル状態により、製造プロセスの異なるステージ、例えば、キャスト→マシン→ドリル→仕上げ、でコンポーネントがどのような姿なのか？を可視化することが出来ます。</p>
<p>以前までならば、このような場合には、派生パーツを使用したかと思いますが、これはすなわち派生パーツのファイルを作成することにほかならず、煩雑なファイル管理が必要となってしまいます。モデル状態により、ディスク上の1ファイルで、派生関係なしに、コンポーネントの状態を表現することが出来ます。</p>
<p>&#0160;</p>
<p>また、シートメタルコンポーネントでは、モデル状態に、切断と折り曲げプロセスの各状態での、折り曲げフィーチャーやフラットパターンを表す状態を表現を持つことが出来ます。</p>
<p><br />溶接アセンブリモデル状態は、溶接準備、溶接、マシニングフィーチャを持つことが可能です。</p>
<p>&#0160;</p>
<p>また、モデル状態により、容易に製造のステージをコピーし編集することが出来ます。例えば、CNCプログラミングでは、ツールパスに不必要なフィーチャを除いたしたバージョンが必要となるでしょう。この場合も単純に、モデル状態をコピーし、不要なコンポーネントとを削除するだけです。</p>
<p>&#0160;</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0263e99e587f200b-pi" style="display: inline;"><img alt="図4" class="asset  asset-image at-xid-6a0167607c2431970b0263e99e587f200b img-responsive" src="/assets/image_675813.jpg" title="図4" /></a></p>
<p>&#0160;</p>
<p><span style="font-size: 15pt;"><strong><span style="text-decoration: underline;">モデル状態によるトップダウンの簡略化</span></strong></span></p>
<p>シミュレーションを行うために、簡素化をしたい場合があると思います。また、IPを保護するために、パートナーに設計を共有する前に簡易化したい場合があるかと思います。<br /><br /></p>
<p>また、BIMプロジェクトでRevitに設計データを渡す際に簡易化をしたい場合がよくあります。簡略化されたジオメトリ情報を含む派生パートファイル（以前まではシュリンクラップ、または、代替LODとして知られています）でアセンブリモデルを代替で入れ替える場合にも、モデル状態はを利用することができます。</p>
<p>&#0160;</p>
<p>代替モデルはPCの負荷を減らします。例えば工場のレイアウトモデルに、製造マシンをコピーして複数の配置したい場合などです。トップレベルアセンブリに10000のコンポーネントモデルを5回ロードする代わりに（メモリには、50000のコンポーネントがロードされます）代替を配置することで、メモリには5つのコンポーネントがロードされるのみとなります。</p>
<p>代替は派生のため、マスタアセンブリへのリンクは残されます。マスタアセンブリを更新した場合、代替も更新されます。</p>
<p><br />そして、モデル状態では、さらにこれ以上のことが可能となります。</p>
<p><iframe allowfullscreen="" frameborder="0" height="270" src="//www.youtube.com/embed/0oZWYjku5qw" width="480"></iframe></p>
<p><span style="font-size: 8pt;">※日本語字幕付きの動画は<a href="https://autodesk.wistia.com/medias/nw52syf9m8?fbclid=IwAR2xENuE6LnJe3YoQuA-tYbSnvCQdHxv_pOZvkS6fKaxUPeYvzlEn6DX_OQ">こちら</a>をご参照ください</span></p>
<p>&#0160;</p>
<p><span style="text-decoration: underline; font-size: 15pt;"><strong>モデル状態によるボトムアップの簡略化</strong></span></p>
<p>簡易化されたモデルでは必要が無いフィーチャーを除外するといった用途で、パートファイルのモデル状態を使用することが出来ます。また、パートファイルのモデル状態は、簡易化されたモデルだけで必要となるフィーチャーを追加する（最小矩形など）といった用途でも使用することが出来ます。</p>
<p>パートファイルのモデル状態は、生産性ツールの”モデル状態をリンク”を用いてアセンブリファイルのモデル状態ともリンクすることが出来ます。</p>
<p>これにより、アセンブリレベルでのモデル状態の変更により、コンポーネントレベルでのモデル状態の変更がトリガーされ、アクティブなアセンブリ内のすべてのパーツおよびサブアセンブリに、選択したモデル状態リプレゼンテーションが適用されます。</p>
<p>&#0160;</p>
<p><span style="font-size: 15pt;"><strong><span style="text-decoration: underline;">図面の簡略化とモデル状態</span></strong></span></p>
<p>”ボトムアップ”モデル状態による簡略化は、特に大きいアセンブリから図面を作成する際に役立ちます。</p>
<p>大きなアセンブリから図面を作成する際に課題となるのはInventorが図面に表示するエッジを計算することにあります。実際に大きなモデルから作成された図面は、多くの重なったエッジがあり、Inventorは多くの計算を必要とします。</p>
<p>アセンブリ全体が1つの塊として単純化されるのではなく（代替ワークフローの場合のように）、簡易化されたモデル状態を用いることで、各パーツは簡易化されます。</p>
<p>代替モデルから図面Viewを作成するのとは異なり、簡易化されたモデル状態を参照する図面を作成した場合は、より高速な図面のロードが可能となり、またアイテム番号のバルーンで識別をしたり、引き出し線で個々のコンポーネントからデータを引き出すことが出来ます。</p>
<p>&#0160;</p>
<p><span style="font-size: 15pt;"><strong><span style="text-decoration: underline;">コンフィグレーションとモデル状態</span></strong></span></p>
<p>パートのモデル状態により、複数の設計オプションを1パートファイル内に持つことが出来ます。iPartsの代わりとして使用できます。</p>
<p>また、アセンブリのモデル状態はコンポーネントの除外や、パターンをコントロールすることが出来、かつサブコンポーネントのモデル状態とリンクさせることが出来ます。これにより、モデル状態は、iAssemblyの代わりとして使用できます。</p>
<p>Inventor 2022でもiAssemblyは継続してサポートされており、以前のバージョンのiAssemblyはInventor 2022にマイグレーションされます。iAssemblyとは異なり、全てのモデル状態メンバの情報はコンポーネントファイルに含まれているため、ファイルの管理は最小限で済みます。</p>
<p>&#0160;</p>
<p>また、iLogicおよびInventorのAPIはアセンブリとパートのモデル状態をサポートしています。Vaultもモデル状態をサポートしており、Vault Professionalのアイテムマスターにより、異なる部品番号を持つ複数のモデル状態を含むファイルを、個々に追跡することが出来ます。</p>
<p>&#0160;</p>
<p><span style="text-decoration: underline; font-size: 15pt;"><strong>設計オプションとモデル状態</strong></span></p>
<p>最終版のデザインまでの道のりは、決して平坦な道のりではありません。</p>
<p>様々なアイデアや要望を取り入れながら進んて行く必要がありますが、多岐にわたるすべての設計を、管理、コーディネートしていくことは、悩みの種です。</p>
<p>モデル状態を使用することで、新しい多岐にわたるデザインのモデル状態簡単に作成することができます。それぞれのオプションをプロジェクトの進捗とともに定義、テスト評価することができます。</p>
<p>設計オプションが除外されれば、対応するモデル状態を削除でき、最終的な設計だけを残すことが出来ます。</p>
<p>全てのモデル状態のデータはアセンブリまたはパートファイル内に存在するため、追加のファイルなどを作成する必要はなく、ファイル管理などは不要となります。</p>
<p>&#0160;</p>
<p><span style="text-decoration: underline; font-size: 15pt;"><strong>BOMとモデル状態</strong></span></p>
<p>アセンブリファイルでは、パターンやサプレッションを用いて、モデル状態によりBOMでのコンポーネントの数量をコントロールすることが可能です。</p>
<p>モデル状態は、アセンブリBOM及びパーツリストに反映されます。</p>
<p>完全に除外されたコンポーネントはBOM/パーツリストには、数量が0(Zero)のラインアイテムとして表示されます。</p>
<p>&#0160;</p>
<p><span style="text-decoration: underline; font-size: 15pt;"><strong>図面とパーツリストでのモデル状態</strong></span><br />モデル状態は図面からも利用することができます。</p>
<p>アセンブリファイル内のモデル状態を参照してパーツリストを配置することができます。このパーツリストには、アセンブリのモデル状態のBOMの状態が反映されます。</p>
<p>同じシート内であっても、各View毎に異なるモデル状態を参照することができます。</p>
<p>全てのViewが同じファイルを参照することになるため、ファイル参照のオーバーヘッドが削減されるとともに、設計の更新が容易かつ迅速に行うことができます。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0263e99e5ad8200b-pi" style="display: inline;"><img alt="図6" class="asset  asset-image at-xid-6a0167607c2431970b0263e99e5ad8200b img-responsive" src="/assets/image_420774.jpg" title="図6" /></a></p>
<p>&#0160;</p>
<p><span style="font-size: 15pt;"><strong><span style="text-decoration: underline;">Vaultでのモデル状態のサポート</span></strong></span><br />モデル状態を持つコンポネントは、これまでと同様にVaultで管理することができます。</p>
<p>Vaultを用いることで以下のようなことが可能です。</p>
<ul>
<li>InventorのVautlアドインからのモデル状態を持つファイルのチェックイン</li>
<li>Vaultから「モデル状態」ファイルを開いて配置</li>
<li>異なる部品番号のモデル状態にアイテムを割り当て</li>
<li>アイテムのプロパティをモデル状態iPropertyにマップ</li>
</ul>
<p>&#0160;</p>
<p>Vaultにあるコンポーネントを開く際に、どのモデル状態を表示するのかを選択することができます。指定したモデル状態に必要となるファイルだけがローカルのワークスペースにダウンロードされ、そのモデル状態で使われていないコンポーネントは、ダウンロードされません。</p>
<p>個々のモデル状態は自身の部品番号を持つことができます。モデル状態は、コンポーネントファイルにキャプチャーされます。一つのファイル内に、複数のコンポーネントを表現することができます。</p>
<p>ただ、WindowsファイルやVault Basicを用いた場合、このように1ファイル内に複数のコンポーネントが含まれていると、個々のメンバーをトラックすることが難しくなります。そのような場合はiPartを利用したワークフローが適しています。</p>
<p>一方でVault Professionalでは、モデル状態毎にアイテムを簡単に作成できるため、個々のメンバを追跡することが容易となります。</p>
<p>Vaultでのモデル状態サポートについては、Vaultの新機能の照会をご参照ください。</p>
<div class="blogcardfu" style="width: auto; max-width: 9999px; border: 1px solid #E0E0E0; border-radius: 3px; margin: 10px 0; padding: 15px; line-height: 1.4; text-align: left; background: #FFFFFF;"><a href="https://help.autodesk.com/view/VAULT/2022/JPN/?guid=GUID-DB0B0B5A-8203-4788-8BAC-94947CEB02BD" rel="noopener" style="display: block; text-decoration: none;" target="_blank"><span class="blogcardfu-image" style="float: right; width: 300px; padding: 0 0 0 10px; margin: 0 0 5px 5px;"><img src="/assets/image_490974.jpg" style="width: 100%; height: auto; max-height: 300px; min-width: 0; border: 0 none; margin: 0;" width="300" /></span><br style="display: none;" /><span class="blogcardfu-title" style="font-size: 112.5%; font-weight: bold; color: #333333; margin: 0 0 5px 0;">モデル状態と Vault (2022 の新機能)</span><br /><span class="blogcardfu-content" style="font-size: 87.5%; font-weight: 400; color: #666666;">Vault 2022 では、モデル状態と呼ばれる、Inventor で要望の多かった機能がサポートされています。この新しいワークフローは、デザインのエンジニアリング、管理、および製造を行う上で便利な方法です…</span><br /><span style="clear: both; display: block; overflow: hidden; height: 0;">&#0160;</span></a></div>
<p>&#0160;</p>
<p>なお、余談となりますが、以前のリリースまでですと、Inventorのインストール時にVaultのアドイン機能（Vault add-inConnectivity.InventorAddin.EdmAddin.dll）が合わせてインストールされていましたが、Vualt 2022からは、Vault Client と一緒にすべての Vault CAD Add-in がインストールされるように変更されておりますので、InventorをVaultと合わせてご利用のお客様はこの点にご留意ください。</p>
<div class="blogcardfu" style="width: auto; max-width: 9999px; border: 1px solid #E0E0E0; border-radius: 3px; margin: 10px 0; padding: 15px; line-height: 1.4; text-align: left; background: #FFFFFF;"><a href="https://help.autodesk.com/view/VAULT/2022/JPN/?guid=GUID-D0765603-0F54-4B35-B3A1-797A4CF0471D" rel="noopener" style="display: block; text-decoration: none;" target="_blank"><span class="blogcardfu-image" style="float: right; width: 300px; padding: 0 0 0 10px; margin: 0 0 5px 5px;"><img src="/assets/image_490974.jpg" style="width: 100%; height: auto; max-height: 300px; min-width: 0; border: 0 none; margin: 0;" width="300" /></span><br style="display: none;" /><span class="blogcardfu-title" style="font-size: 112.5%; font-weight: bold; color: #333333; margin: 0 0 5px 0;">インストールの更新 (2022 の新機能)</span><br /><span class="blogcardfu-content" style="font-size: 87.5%; font-weight: 400; color: #666666;">Autodesk Account を使用した Vault とそのアドインのインストールが簡略化されました。Vault 2022 リリースでは、Vault Client と一緒にすべての Vault CAD Add-in がインストール...</span><br /><span style="clear: both; display: block; overflow: hidden; height: 0;">&#0160;</span></a></div>
<p>&#0160;</p>
<p><span style="font-size: 15pt;"><strong><span style="text-decoration: underline;">モデル状態のiLogic と API</span></strong></span></p>
<p>モデル状態はiLogic及びInventor APIサポートされています。</p>
<p>詳細はiLogicエディタのスニペットまたは、ディベロッパー向けのヘルプをご参照ください。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0263e99e58c2200b-pi" style="display: inline;"><img alt="図5" class="asset  asset-image at-xid-6a0167607c2431970b0263e99e58c2200b img-responsive" src="/assets/image_256542.jpg" title="図5" /></a></p>
<p>&#0160;</p>
<p><span style="text-decoration: underline;"><strong><span style="font-size: 15pt;">モデル状態ではできないこと</span></strong></span></p>
<p>Inventor 2022のモデル状態によるワークフローにより、これまでの多くの既存の機能が実現可能ですが、例外もあります。</p>
<p>モデル状態は表示、すなわちビューリプレゼンテーションおよびポジション リプレゼンテーション、とは切り離されています。</p>
<p>ビューリプレゼンテーションはどのコンポーネントが表示状態か、色、カメラViewをコントロールします。</p>
<p>Inventor 2022では、図面Viewを作成する際に、モデルのカメラViewを参照する機能が追加されました。</p>
<div class="blogcardfu" style="width: auto; max-width: 9999px; border: 1px solid #E0E0E0; border-radius: 3px; margin: 10px 0; padding: 15px; line-height: 1.4; text-align: left; background: #FFFFFF;"><a href="https://help.autodesk.com/view/INVNTOR/2022/JPN/?guid=GUID-A9F8C0BD-6766-493E-983C-B1A8FE9F67E0" rel="noopener" style="display: block; text-decoration: none;" target="_blank"><span class="blogcardfu-image" style="float: right; width: 100px; padding: 0 0 0 10px; margin: 0 0 5px 5px;"><img src="/assets/image_513464.jpg" style="width: 100%; height: auto; max-height: 100px; min-width: 0; border: 0 none; margin: 0;" width="100" /></span><br style="display: none;" /><span class="blogcardfu-title" style="font-size: 112.5%; font-weight: bold; color: #333333; margin: 0 0 5px 0;">図面ビューのオプション (2022 の新機能)</span><br /><span class="blogcardfu-content" style="font-size: 87.5%; font-weight: 400; color: #666666;">デザイン ビューのオプションが追加され、カメラ ビューと 3D 注記を抽出できるようになりました。ベース ビューを作成するときに、[ビュー設定]オプションをクリックして、保存済みカメラ ビューと 3D 注記を含めます。投影ビューの 3D 注記を抽出するには、ビュー内をダブルクリックしてビューの編集をアクティブにし、[ビュー設定]で[3...</span><br /><span style="clear: both; display: block; overflow: hidden; height: 0;">&#0160;</span></a></div>
<p>&#0160;</p>
<p>ポジション リプレゼンテーションは、拘束の除外を含む拘束をコントロールします。これにより、同じアセンブリを異なる位置に表示したり、意図的に制約を緩和したりすることができます制約が緩和されたサブアセンブリは「フレキシブル」にすることができます。</p>
<p>つまり、含まれているアセンブリ内で自由に移動でき、最上位のアセンブリの制約の影響を受ける可能性があります。</p>
<p>ビューリプレゼンテーションおよびポジション リプレゼンテーションは、「モデルの状態をリンクする」などの自動化されたツールを使用してトップダウンに「リンク」することはできませんが、手動でリンクすることはできます。</p>
<p>&#0160;</p>
<p>トップレベルアセンブリのビューリプレゼンテーションを変更すると、コンポーネントのビューリプレゼンテーションの変更がトリガーされ、トップレベルアセンブリのポジション リプレゼンテーションを変更すると、サブアセンブリのポジション リプレゼンテーションの変更がトリガーされる可能性があります。</p>
<p>&#0160;</p>
<p>同じデザインの中に、モデル状態、ビューリプレゼンテーション、ポジション リプレゼンテーションを独立して持つことが出来ます。</p>
<p>&#0160;</p>
<p><span style="text-decoration: underline; font-size: 15pt;"><strong>モデル状態のマイグレーション</strong></span></p>
<p>以前のバージョンで詳細レベルを設定しいる場合、Inventor2022ではモデル状態に変換されます。但し以下の詳細レベルは無くなり引き継がれません。</p>
<ul>
<li>すべてのコンポーネントを省略</li>
<li>すべてのパーツを省略</li>
<li>すべてのコンテンツセンターを省略</li>
</ul>
<p>&#0160;</p>
<p>また、旧アセンブリの詳細レベルを読み込み「モデル状態」に変換した後にパーツ一覧や部品表の表示を行うとBOM情報が各「モデル状態」に無く、マスターを参照する状態になるためBOM委任の警告が表示され、継続するには「はい」を選択するとソースモデルの状態がアクティブになりBOMを表示します。</p>
<p>&#0160;</p>
<p><span style="text-decoration: underline; font-size: 15pt;"><strong>Viewer でのモデル状態</strong></span></p>
<p>Inventorのネイティブファイルを参照することのできるViewerには、Inventor Read Only ModeとInventor Viewがありますが、それぞれのViewerでのモデル状態への対応が異なります。</p>
<p>&#0160;</p>
<p>1．Inventor Read Only Mode</p>
<p>Inventor Read-only Modeは、<strong><span style="text-decoration: underline;">ライセンスを使用せず</span></strong>に Inventor のユーザ インタフェースで、Inventorファイル（.ipt、.iam、.idw、.dwg、.ipn）を表示することのできるツールです。</p>
<p>Inventor Read Only Modeではトップレベルのデータファイル（Inventor Read-only Modeで開いたファイル）のモデル状態を切り替えて参照することが可能です（配下のコンポーネントのモデル状態の切り替えは出来ません）。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0278802347e5200d-pi" style="display: inline;"><img alt="Inventor view only mode" class="asset  asset-image at-xid-6a0167607c2431970b0278802347e5200d img-responsive" src="/assets/image_585629.jpg" title="Inventor view only mode" /></a></p>
<p>&#0160;</p>
<p>Inventor Read-only Modeは、Inventor のインストーラに含まれており、<a href="https://www.autodesk.com/products/inventor/free-trial?_ga=2.263445450.771700468.1618186572-1803922208.1580713421">www.autodesk.com/products/inventor/free-trial</a> から Inventor 体験版をダウンロードすることでこのモードを使用することができます。Inventor の完全な機能への体験版アクセスは時間で制限されていますが、Inventor Read-only Mode には有効期限がありません。</p>
<p>&#0160;</p>
<p>なお、Inventor 2022よりAutodesk Accountから、カスタムインストールでInventor Read-only modeのみをインストールできるようになっておりますので、Inventorのサブスクリプション契約があり、ビューアーを社内で広めたい場合などには、このカスタムインストール機能をご活用いただければと思います。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0263e99e0ea5200b-pi" style="display: inline;"><img alt="カスタムインストール" class="asset  asset-image at-xid-6a0167607c2431970b0263e99e0ea5200b img-responsive" src="/assets/image_893093.jpg" title="カスタムインストール" /></a></p>
<p>&#0160;</p>
<p>Autodesk Accountからのカスタムインストールの詳細については、以下のオンラインヘルプをご参照ください。</p>
<div class="blogcardfu" style="width: auto; max-width: 9999px; border: 1px solid #E0E0E0; border-radius: 3px; margin: 10px 0; padding: 15px; line-height: 1.4; text-align: left; background: #FFFFFF;"><a href="https://help.autodesk.com/view/INVNTOR/2022/JPN/?guid=GUID-31247E59-1F19-4E69-92BA-74C72CE7BBC7" rel="noopener" style="display: block; text-decoration: none;" target="_blank"><span class="blogcardfu-image" style="float: right; width: 100px; padding: 0 0 0 10px; margin: 0 0 5px 5px;"><img src="/assets/image_513464.jpg" style="width: 100%; height: auto; max-height: 100px; min-width: 0; border: 0 none; margin: 0;" width="100" /></span><br style="display: none;" /><span class="blogcardfu-title" style="font-size: 112.5%; font-weight: bold; color: #333333; margin: 0 0 5px 0;">配置を作成するには</span><br /><span class="blogcardfu-content" style="font-size: 87.5%; font-weight: 400; color: #666666;">1 つまたは複数の配置を作成して、ネットワーク上の複数台のコンピュータに Inventor ソフトウェアをインストールするプロセスを合理化します。<br />配置を作成するには<br />配置を作成し、その配置を使用してネットワーク上の複数のコンピュータに Inventor ソフトウェアをインストールするには、「Autodesk Account から配置を作成するには」に記載されている手順を確認して実行します。<br />Inventor の配置をカスタマイズするには…</span><br /><span style="clear: both; display: block; overflow: hidden; height: 0;">&#0160;</span></a></div>
<p>&#0160;</p>
<p>２．Inventor View</p>
<p>Inventor Viewは、Inventorのファイル（.ipt、.iam、.idw、.dwg）を参照することのできる、Inventorとは別に提供されているツールです。</p>
<p>Inventor Viewでは、モデル状態を切り替えての参照は出来ず、最後にファイルに保存された状態のモデル状態名が、ブラウザのファイル名の右に()付きで表示され、画面には保存をおこなった時点でのモデル状態が表示されます。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b027880234602200d-pi" style="display: inline;"><img alt="Inventor view" class="asset  asset-image at-xid-6a0167607c2431970b027880234602200d img-responsive" src="/assets/image_629116.jpg" title="Inventor view" /></a></p>
<p>&#0160;</p>
<p>Inventor ViewはInventorとは別のインストーラでのインストールが必要となり、Inventor 2022のファイルに対応したInventor Viewのインストーラは以下のページからダウンロードすることが可能です。</p>
<div class="blogcardfu" style="width: auto; max-width: 9999px; border: 1px solid #E0E0E0; border-radius: 3px; margin: 10px 0; padding: 15px; line-height: 1.4; text-align: left; background: #FFFFFF;"><a href="https://knowledge.autodesk.com/support/inventor/troubleshooting/caas/downloads/content/inventor-view-2022.html" rel="noopener" style="display: block; text-decoration: none;" target="_blank"><span class="blogcardfu-image" style="float: right; width: 100px; padding: 0 0 0 10px; margin: 0 0 5px 5px;"><img src="/assets/image_513464.jpg" style="width: 100%; height: auto; max-height: 100px; min-width: 0; border: 0 none; margin: 0;" width="100" /></span><br style="display: none;" /><span class="blogcardfu-title" style="font-size: 112.5%; font-weight: bold; color: #333333; margin: 0 0 5px 0;">Inventor View 2022 | Inventor 2022</span><br /><span class="blogcardfu-content" style="font-size: 87.5%; font-weight: 400; color: #666666;">Autodesk Inventor View 2022 スタンドアロン<br /><br />高品質な製品を提供するための継続的な取り組みとして、オートデスクは Autodesk Inventor View 2022 のスタンドアロン バージョンをリリースしました....</span><br /><span style="clear: both; display: block; overflow: hidden; height: 0;">&#0160;</span></a></div>
<p>Inventor Viewのインストール要件は、Inventor 本体と同様に、Service Pack と更新プログラムが適用されたWindows 10 の 64 ビット版、また、NET Framework Version 4.8が必要となります。</p>
<p>詳細なインストール要件については、ダウンロードサイトにある、各言語毎のリリースノートをご確認ください。</p>
<p>&#0160;</p>
<p><span style="font-size: 15pt;"><strong><span style="text-decoration: underline;">まとめ</span></strong></span></p>
<p>かなり長くなってしまいましたが、本記事では、Inventor 2022で新しく追加された『モデル状態』について解説をしました。</p>
<p>実は、Inventor 2022で導入されたモデル状態は、Inventorの初期リリース以降で最も大きな根本的な構造に対する変更となり、Inventorのコードベースに対しては非常に大きな変更がされています。</p>
<p>&#0160;</p>
<p>一方で、利用する側の視点から見ると、これまで複数のファイルに分割して実現していた内容が、一つのファイルに含めることができるため、管理の手間を削減し既存ワークフローを簡易化や、新しいワークフローを実現できることをご理解いただけたのではないかと思います。</p>
<p>&#0160;</p>
<p>是非、皆さんの業務でInventor 2022のモデル状態を活用してみてください。</p>
<p>&#0160;</p>
<p>次回の記事でも、引き続きInventor 2022の新規機能を紹介したいと思います。</p>
<p>&#0160;</p>
<p>by Takehiro Kato</p>
<p><span style="font-size: 8pt;">※本記事は英語の<a href="https://blogs.autodesk.com/inventor/2021/03/24/whats-new-2022-model-states">ブログ記事</a> (https://blogs.autodesk.com/inventor/2021/03/24/whats-new-2022-model-states)を参考に日本語で加筆・再構成をおこなったものとなります。</span></p>
