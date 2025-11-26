---
layout: "post"
title: "Inventor 2023 新機能～ その3"
date: "2022-04-18 01:02:40"
author: "Takehiro Kato"
categories:
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2022/04/inventor-2023-whats-new-part3.html "
typepad_basename: "inventor-2023-whats-new-part3"
typepad_status: "Publish"
---

<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0278807514bb200d-pi"><img alt="Part2Title" class="asset  asset-image at-xid-6a0167607c2431970b0278807514bb200d img-responsive" src="/assets/image_87731.jpg" style="display: block; margin-left: auto; margin-right: auto;" title="Part2Title" /></a></p>
<p>&#0160;</p>
<p>前回の記事に引き続きInventor 2023の新機能をご紹介したいと思います。</p>
<p>新機能の紹介にあたり、Inventor 2023新機能を以下の6つの領域に分類してご紹介します。</p>
<ol>
<li>一般</li>
<li>相互運用性</li>
<li>パーツ</li>
<li>アセンブリ</li>
<li>パフォーマンス</li>
<li>図面</li>
</ol>
<p>&#0160;</p>
<p>今回の記事では、「3. パーツ」と「4. アセンブリ」について解説をしたいと思います。</p>
<p>なお、「1. 一般」と「2. 相互運用性」については、前回の記事をご参照ください。</p>
<div class="blogcardfu" style="width: auto; max-width: 9999px; border: 1px solid #E0E0E0; border-radius: 3px; margin: 10px 0; padding: 15px; line-height: 1.4; text-align: left; background: #FFFFFF;"><a href="https://adndevblog.typepad.com/technology_perspective/2022/04/inventor-2023-whats-new-part2.html" rel="noopener" style="display: block; text-decoration: none;" target="_blank"><span class="blogcardfu-image" style="float: right; width: 200px; padding: 0 0 0 10px; margin: 0 0 5px 5px;"><img src="/assets/image_740102.jpg" style="width: 100%; height: auto; max-height: 200px; min-width: 0; border: 0 none; margin: 0;" width="200" /></span><br style="display: none;" /><span class="blogcardfu-title" style="font-size: 112.5%; font-weight: bold; color: #333333; margin: 0 0 5px 0;">Inventor 2023 新機能～ その2</span><br /><span class="blogcardfu-content" style="font-size: 87.5%; font-weight: 400; color: #666666;">前回の記事に引き続きInventor 2023の新機能をご紹介したいと思います。 新機能の紹介にあたり、Inventor 2023新機能を以下の6つの領域に分類してご紹介します。 一般 相互運用性 パーツ アセンブリ パフォーマンス 図面 今回の記事では、「1. 一般」と「2. 相互運用性」について、ご紹介をしたいと思います...</span><br /><span style="clear: both; display: block; overflow: hidden; height: 0;">&#0160;</span></a></div>
<p>&#0160;</p>
<p><span style="text-decoration: underline;"><span style="font-size: 14pt;"><strong>3．パーツ</strong></span></span></p>
<ul>
<li><strong><span style="font-size: 14pt;">マーク コマンド&#0160;</span></strong></li>
</ul>
<p>スケッチ テキストまたはジオメトリを使用してマーク フィーチャを追加できるようになりました。</p>
<p><a class="asset-img-link" href="https://help.autodesk.com/cloudhelp/2023/JPN/Inventor-WhatsNew/images/GUID-7E43FB6B-4A7F-4FF8-A12D-334A49457551.gif" style="display: inline;"><img alt="image from help.autodesk.com" class="asset  asset-image at-xid-6a025d9b32eb0b200c0282e14db142200b img-responsive" src="/assets/image_828966.jpg" title="image from help.autodesk.com" /></a></p>
<p>マーク フィーチャを使用して、レーザー マーキング、エッチング、および彫り込みを表すコンテンツを作成します。マーク フィーチャは 2D および 3D で表示され、フラット パターン DXF/DWG エクスポート内でエクスポートできます。</p>
<p>&#0160;</p>
<p>マーク フィーチャのモデリングおよびエクスポートの動作は、マーク スタイルによってコントロールされます。各マーク スタイルは、DXF または DWG ファイル内のエクスポートされるマーク画層にマッピングされます。</p>
<p>カスタムのマーク スタイルを定義し、それらをマーク フィーチャ内の選択セットに割り当て、マーク ジオメトリを DXF または DWG ファイルの特定の画層にエクスポートすることができます。</p>
<p>&#0160;</p>
<ul>
<li><strong><span style="font-size: 14pt;">スケッチ</span></strong></li>
</ul>
<p><strong class="ph b">投影されたジオメトリ</strong></p>
<p><a class="asset-img-link" href="https://help.autodesk.com/cloudhelp/2023/JPN/Inventor-WhatsNew/images/GUID-3CDE05D8-B481-491D-8F0E-137F1A6C84EE.png" style="display: inline;"><img alt="image from help.autodesk.com" border="0" class="asset  asset-image at-xid-6a025d9b32eb0b200c02942fa2d10e200c img-responsive" src="/assets/image_236771.jpg" title="image from help.autodesk.com" /></a></p>
<p>モデル ツリー内で上位のフィーチャを編集し、そのフィーチャのジオメトリを別のスケッチに投影すると破断または孤立してしまった、という経験があるかもしれません。これらのスケッチに移動して、破断投影を修復または削除する必要がありました。場合により、アクセスする必要があるジオメトリを見つけるのが困難でした。</p>
<p>現在は、破断投影エッジをワンクリックで選択できるようになりました。破断したジオメトリがあるスケッチを編集し、スケッチ ブラウザ ノードまたは空のキャンバス空間を右クリックして[破断投影を選択]をクリックし、[Delete]キーを使用するか、破断投影を置き換えます。</p>
<p>&#0160;</p>
<p><strong class="ph b">モデル スケッチ テキスト</strong></p>
<p><a class="asset-img-link" href="https://help.autodesk.com/cloudhelp/2023/JPN/Inventor-WhatsNew/images/GUID-D0736F97-2201-4E22-9443-C1F90204749B.png" style="display: inline;"><img alt="image from help.autodesk.com" class="asset  asset-image at-xid-6a025d9b32eb0b200c027880750f81200d img-responsive" src="/assets/image_161034.jpg" title="image from help.autodesk.com" /></a></p>
<p>標準 iProperty およびカスタム iProperty をパーツ モデル スケッチ テキストで使用できるようになりました。iProperty はアクティブなモデルからのみ取得されます。</p>
<p>この機能は、アセンブリ スケッチ テキストでは使用できません。</p>
<p>&#0160;</p>
<ul>
<li><strong><span style="font-size: 14pt;">シート メタル パーツ</span></strong></li>
</ul>
<p><strong>複数のコマンドについて、ブラウザでの拡張情報を表示</strong></p>
<p>オプション、[ブラウザ内のフィーチャ ノード名の後に拡張情報を表示] ([アプリケーション オプション] &gt; [パーツ]タブから使用可能)および[拡張名を表示] (モデル ブラウザ ツールのメニューから使用可能)は、以下のコマンドをサポートするように機能強化されました。</p>
<ul>
<li>面</li>
<li>コンター フランジ</li>
<li>フランジ</li>
</ul>
<p><a class="asset-img-link" href="https://help.autodesk.com/cloudhelp/2023/JPN/Inventor-WhatsNew/images/GUID-6B0FE59A-0030-4E8B-BE0F-F316EE1A17C9.png" style="display: inline;"><img alt="image from help.autodesk.com" border="0" class="asset  asset-image at-xid-6a025d9b32eb0b200c02942fa2d150200c img-responsive" src="/assets/image_408788.jpg" title="image from help.autodesk.com" /></a></p>
<p>&#0160;</p>
<p><strong>コンターフランジの非対称押し出しオプションの追加</strong></p>
<p>コンターフランジを押し出す際の新しい非対称オプションが追加されました。</p>
<p>&#0160;</p>
<p><strong>フラットパターンのDWG/ DXFオプションの追加</strong></p>
<p>フラットパターンをDWG / DXFにエクスポートする際に、スプライン曲線を場合にスプラインが円弧から一定程度逸脱した場合に、スプラインを接線円弧に置き換えるかどうかを指定するオプションが追加されました。</p>
<p>&#0160;</p>
<p><strong class="ph b">シート メタル テンプレート</strong></p>
<p><a class="asset-img-link" href="https://help.autodesk.com/cloudhelp/2023/JPN/Inventor-WhatsNew/images/GUID-7B283D58-A090-4F82-81A7-2974086789C1.png" style="display: inline;"><img alt="image from help.autodesk.com" border="0" class="asset  asset-image at-xid-6a025d9b32eb0b200c02942fa2d1ba200c img-responsive" src="/assets/image_740319.jpg" title="image from help.autodesk.com" /></a></p>
<p>アセンブリに新しいコンポーネントを作成する際に、新しいシート メタル パーツ ファイルの作成に使用するテンプレートを選択または参照するための新しい[シート メタル テンプレート]オプションが追加されました。</p>
<p>&#0160;</p>
<ul>
<li><strong><span style="font-size: 14pt;">フィーチャの機能強化</span></strong></li>
</ul>
<p><strong>フィレットの機能強化</strong></p>
<p>すべてのフィレット タイプで公差が使用できるようになりました。</p>
<p><a class="asset-img-link" href="https://help.autodesk.com/cloudhelp/2023/JPN/Inventor-WhatsNew/images/GUID-3FE9B955-6885-4C1F-BD52-6D70C18E8AFA.png" style="display: inline;"><img alt="image from help.autodesk.com" class="asset  asset-image at-xid-6a025d9b32eb0b200c0282e14db71c200b img-responsive" src="/assets/image_751674.jpg" title="image from help.autodesk.com" /></a></p>
<p><strong>パーツ フィーチャの関係</strong></p>
<p>[関係]コマンドが省略されたフィーチャに対して機能するようになりました。省略されたフィーチャを右クリックし、[関係]を選択します。</p>
<p><a class="asset-img-link" href="https://help.autodesk.com/cloudhelp/2023/JPN/Inventor-WhatsNew/images/GUID-EC0A70CC-E6E7-4ADC-B045-6052B7A8B423.png" style="display: inline;"><img alt="image from help.autodesk.com" class="asset  asset-image at-xid-6a025d9b32eb0b200c02942fa2d60d200c img-responsive" src="/assets/image_587666.jpg" title="image from help.autodesk.com" /></a></p>
<p>&#0160;</p>
<p><strong>押し出しと回転の機能強化</strong></p>
<p>プロパティ パネルのすべてのオプションが押し出しと回転フィーチャのプリセットに保持されるようになりました。</p>
<p><a class="asset-img-link" href="https://help.autodesk.com/cloudhelp/2023/JPN/Inventor-WhatsNew/images/GUID-3C3D3D25-2C92-4A77-A9C9-044B7318E0B1.png" style="display: inline;"><img alt="image from help.autodesk.com" class="asset  asset-image at-xid-6a025d9b32eb0b200c0278807513b7200d img-responsive" src="/assets/image_430035.jpg" title="image from help.autodesk.com" /></a></p>
<p>&#0160;</p>
<p><strong>表示設定ショートカット</strong></p>
<p>[Alt]+[V]を使用して、次のフィーチャの表示/非表示を切り替えることができるようになりました。</p>
<ul style="list-style-type: square;">
<li>ソリッド</li>
<li>作業フィーチャ(平面、軸、点)</li>
<li>スケッチ</li>
</ul>
<p>&#0160;</p>
<p><span style="font-size: 14pt;"><strong><span style="text-decoration: underline;">4．アセンブリ</span></strong></span></p>
<ul>
<li><strong><span style="font-size: 14pt;">アセンブリ部品表の機能強化</span></strong></li>
</ul>
<p>新しい[部品表の設定]オプションが導入され、数量ゼロの表示と項目番号シーケンスをコントロールできるようになりました。</p>
<p><a class="asset-img-link" href="https://help.autodesk.com/cloudhelp/2023/JPN/Inventor-WhatsNew/images/GUID-24236F50-7F20-4188-BFF9-6F9C75508C52.png" style="display: inline;"><img alt="image from help.autodesk.com" class="asset  asset-image at-xid-6a025d9b32eb0b200c02788075142c200d img-responsive" src="/assets/image_813315.jpg" title="image from help.autodesk.com" /></a></p>
<p>&#0160;</p>
<p><strong>新しい[部品表の設定]オプション</strong></p>
<p>モデル状態で省略されたコンポーネントは、部品表では数量 0 として表示され、項目番号に影響を与えます。省略されたコンポーネントの部品表動作をコントロールするために、新しい[部品表の設定]オプションが[部品表]に追加されました。[部品表の設定]をクリックしてから、有効または無効にします。</p>
<ul>
<li>部品表内の省略されたコンポーネントを非表示</li>
<li>自動的に項目に順次番号付け</li>
</ul>
<p>&#0160;</p>
<ul>
<li><strong><span style="font-size: 14pt;">親アセンブリの iProperty を引出線注記に挿入</span></strong></li>
</ul>
<p>標準 iProperty、カスタム iProperty、およびアタッチされたパーツの親に属するインスタンス プロパティを含めることができるようになりました。</p>
<p><a class="asset-img-link" href="https://help.autodesk.com/cloudhelp/2023/JPN/Inventor-WhatsNew/images/GUID-7ECF2F0E-8778-498F-AE9C-D1733A5C328E.png" style="display: inline;"><img alt="image from help.autodesk.com" class="asset  asset-image at-xid-6a025d9b32eb0b200c0282e14db7c5200b img-responsive" src="/assets/image_589841.jpg" title="image from help.autodesk.com" /></a></p>
<p>&#0160;</p>
<ul>
<li><strong><span style="font-size: 14pt;">チューブ＆パイプの機能強化</span></strong></li>
</ul>
<p>[プロパティ]ダイアログの[ルート]パネルでは、ダイアログを閉じた後も最後に使用した値が保持されるようになりました。</p>
<p>次の設定の値が記憶されます。</p>
<ul style="list-style-type: square;">
<li>自動ルート</li>
<li>自動ルートをスケッチに変換</li>
<li>自動寸法</li>
<li>自動拘束</li>
</ul>
<p><a class="asset-img-link" href="https://help.autodesk.com/cloudhelp/2023/JPN/Inventor-WhatsNew/images/GUID-47EBB5E4-27D2-4E7A-9F0A-C7A24F33DA03.png" style="display: inline;"><img alt="image from help.autodesk.com" class="asset  asset-image at-xid-6a025d9b32eb0b200c02942fa2d6b8200c img-responsive" src="/assets/image_591554.jpg" title="image from help.autodesk.com" /></a></p>
<p>&#0160;</p>
<ul>
<li><strong><span style="font-size: 14pt;">その他</span></strong></li>
</ul>
<p><strong>代替モデル状態</strong></p>
<p>代替モデル状態のユーザ インタフェースおよび製品内メッセージの更新されました。</p>
<p><a class="asset-img-link" href="https://help.autodesk.com/cloudhelp/2023/JPN/Inventor-WhatsNew/images/GUID-7477E8B1-9156-4478-A3C7-BD9D67B0B655.png" style="display: inline;"><img alt="image from help.autodesk.com" class="asset  asset-image at-xid-6a025d9b32eb0b200c027880751496200d img-responsive" src="/assets/image_311730.jpg" title="image from help.autodesk.com" /></a></p>
<p>代替モデル状態がアクティブな場合、次のコマンドは無効になります。</p>
<ul class="ul" id="GUID-607939CE-1BCC-4753-860F-A8D914A2DC67__UL_0DA73268FB234C079D937CC9DE12137F">
<li class="li" id="GUID-607939CE-1BCC-4753-860F-A8D914A2DC67__LI_C3A93C6C2E444DE1B2FB3F8F13D35444">リボン内
<ul class="ul" id="GUID-607939CE-1BCC-4753-860F-A8D914A2DC67__UL_425E694C2C814224AF775ED635800161">
<li class="li" id="GUID-607939CE-1BCC-4753-860F-A8D914A2DC67__LI_543446B809024C24BDD58C35D50D9345">iLogic コンポーネントを配置</li>
<li class="li" id="GUID-607939CE-1BCC-4753-860F-A8D914A2DC67__LI_0EBB1E7B314641EDA53D992CBF61ED7F">干渉解析</li>
<li class="li" id="GUID-607939CE-1BCC-4753-860F-A8D914A2DC67__LI_17D5517E028F43658298AE1605E2717B">アクティブ接触ソルバ</li>
<li class="li" id="GUID-607939CE-1BCC-4753-860F-A8D914A2DC67__LI_D884F5A58A9B41FE8CA2F151F7B8332F">溶接に変換</li>
</ul>
</li>
<li class="li" id="GUID-607939CE-1BCC-4753-860F-A8D914A2DC67__LI_E39C6B89048C4192AF9FA25AF4EAAB1B">コンポーネントのコンテキスト メニュー
<ul class="ul" id="GUID-607939CE-1BCC-4753-860F-A8D914A2DC67__UL_C8CC3FA45AB34CC68C1EA296511A8D30">
<li class="li" id="GUID-607939CE-1BCC-4753-860F-A8D914A2DC67__LI_5D77966A28144D0BB430CFD5A6968CE1">[下の階層に移動]/[上の階層に移動]</li>
<li class="li" id="GUID-607939CE-1BCC-4753-860F-A8D914A2DC67__LI_B68DF57A7C1E49B897C1C6B8512EC0EF">コンテンツ センターから置き換え...</li>
</ul>
</li>
</ul>
<p>&#0160;</p>
<p>[ダイナミック シミュレーション]では、モデル状態(代替)に動作部品がないことを説明するメッセージが表示されます。</p>
<p>&#0160;</p>
<p>[フレーム解析]では、代替以外のモデル状態に切り替える必要があることを説明するメッセージが表示されます。</p>
<p>&#0160;</p>
<p><strong>拘束オプション</strong></p>
<p>拘束を編集するときに拘束を直接省略できるようになりました。</p>
<p>&#0160;</p>
<p><strong>ストーリーボードの名前を変更</strong></p>
<p>ストーリーボードの名前を作成時またはプレゼンテーションの編集時に変更できるようになりました。</p>
<p>&#0160;</p>
<p><strong>ビデオへのパブリッシュ</strong></p>
<p>[ビデオ解像度]設定に新しいオプションである[フレーム レート]が追加されました。</p>
<p>&#0160;</p>
<p>今回の記事は以上となります、次回も引き続き、Inventor 2023の新機能をご紹介したいと思います。</p>
<p>&#0160;</p>
<p>By Takehiro Kato</p>
<p>&#0160;</p>
