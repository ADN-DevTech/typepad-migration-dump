---
layout: "post"
title: "AutoCAD 2022 の新機能 ～ その3"
date: "2021-03-29 00:04:30"
author: "Toshiaki Isezaki"
categories:
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2021/03/new-features-on-autocad-2022-part3.html "
typepad_basename: "new-features-on-autocad-2022-part3"
typepad_status: "Publish"
---

<p>今回は、AutoCAD 2022 の新機能から、クラウドを使った AutoCAD の新機能、改良機能をご紹介したいと思います。<a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b026bdec0d343200c-pi" style="display: inline;"></a></p>
<hr />
<p><strong>現在の図面を共有</strong></p>
<p>[図面を共有] の機能は、現在の図面のコピーへのリンクを共有し、AutoCAD Web アプリで表示または編集することができます。関連するすべての DWG 外部参照とイメージが含まれます。</p>
<p>[共有] は、AutoCAD デスクトップの ETRANSMIT[e-トランスミット] と同じように動作します。共有ファイルには、外部参照やフォント ファイルなど、関連するすべての従属ファイルが含まれます。 リンクを知っているすべてのユーザは、AutoCAD Web アプリで図面にアクセスできます。リンクは、作成後 7 日で有効期限が切れます。受信者の権限は、<strong>表示のみ</strong>と<strong>編集と保存が可能</strong>の 2 つのレベルから選択できます。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b027880187eb8200d-pi" style="display: inline;"><img alt="View_only1" border="0" class="asset  asset-image at-xid-6a0167607c2431970b027880187eb8200d image-full img-responsive" src="/assets/image_126019.jpg" title="View_only1" /></a></p>
<p>図面の共有時、「表示のみ」を選択して URL を生成、共同作業者に伝えた場合、表示される図面は表示専用となります。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b027880187ecb200d-pi" style="display: inline;"><img alt="View_only2" border="0" class="asset  asset-image at-xid-6a0167607c2431970b027880187ecb200d image-full img-responsive" src="/assets/image_301934.jpg" title="View_only2" /></a></p>
<p>同様に、図面の共有時に「コピーを編集して保存」を選択して URL を生成、共同作業者に伝えた場合、表示される図面は編集、保存が可能となります。後述するトレース機能はこの方法で共有した図面で使用することが出来ます。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0263e9934cdd200b-pi" style="display: inline;"><img alt="Share2" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0263e9934cdd200b image-full img-responsive" src="/assets/image_996361.jpg" title="Share2" /></a></p>
<p>従来の <a href="https://adndevblog.typepad.com/technology_perspective/2018/04/new-features-on-autocad-2019-part2.html#shared_view" rel="noopener" target="_blank"><strong>共有ビュー</strong> </a>との違いは、<strong>Autodesk Viewer</strong>（Autodesk ビューア、<a href="https://viewer.autodesk.com/"><strong>https://viewer.autodesk.com/</strong></a>）使用の有無です。共有ビューは DWG ファイルを変換して読み取り専用（表示専用）で共有するの対し、[図面を共有] は、DWG を使用してのコラボレーションを実現しています。</p>
<hr />
<p><strong>トレース</strong></p>
<p>トレース 機能は、「コピーを編集して保存」を選択して&#0160; [図面を共有]&#0160;した場合に使用できる新しいコラボレーション機能です。トレースは、既存の図面を変更する恐れなしに、AutoCAD Web アプリ、AutoCAD モバイル アプリで図面の変更を共同で行うための安全な空間を提供します。トレースは、たとえば図面上に配置された共同作業が可能な仮想のトレーシング ペーパーのようなもので、共同作業者が図面にフィードバックを追加することができます。</p>
<p>Web およびモバイル アプリでトレースを作成し、図面を共同作業者に送信または共有して、共同作業者がトレースとその内容を表示できるようにすることができます。</p>
<p>機能は、使用しているアプリのバージョンによって若干異なります。トレースは、デスクトップ、Web、またはモバイル アプリで表示できますが、トレースを作成または編集できるのは、Web およびモバイルを使用している場合のみです。</p>
<p style="text-align: center;"><iframe allowfullscreen="" frameborder="0" height="270" src="//www.youtube.com/embed/XWeXeykLZ0Q" width="480"></iframe></p>
<p style="text-align: left;">トレース情報は図面に保存されます。トレース情報の削除は、デスクトップ、Web、モバイル アプリのいずれでもおこなうことが出来ます。</p>
<p style="text-align: left;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0278801f122c200d-pi"><img alt="Delete_trace" class="asset  asset-image at-xid-6a0167607c2431970b0278801f122c200d img-responsive" src="/assets/image_58335.jpg" style="display: block; margin-left: auto; margin-right: auto;" title="Delete_trace" /></a></p>
<hr />
<p><strong>Autodesk Docs へプッシュ</strong></p>
<p>[Autodesk Docs にプッシュ]を使用すると、図面レイアウトを PDF として Autodesk Docs にプッシュすることで、現場とコラボレーションすることも可能となります。複数の図面のレイアウトを選択し、それらを PDF として Autodesk Docs の選択したプロジェクト フォルダにアップロード出来ます。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0263e9934c8b200b-pi" style="display: inline;"><img alt="Docs_push1" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0263e9934c8b200b image-full img-responsive" src="/assets/image_662479.jpg" title="Docs_push1" /></a></p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b027880187eb0200d-pi" style="display: inline;"><img alt="Docs_push2" border="0" class="asset  asset-image at-xid-6a0167607c2431970b027880187eb0200d image-full img-responsive" src="/assets/image_63212.jpg" title="Docs_push2" /></a></p>
<p><strong>Autodesk Docs とは</strong></p>
<p>オートデスクは、BIM 360 プラットフォームを BIM 360 や PlanGrid、BuildingConnectedといった製品の統合プラットフォームとして、数年をかけて Autodesk Construction Cloud（ACC） に移行していく予定です。もちろん、BIM 360 がすぐに使用できなくなるということではありません。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0263e999d76b200b-pi" style="display: inline;"><img alt="Storage_history" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0263e999d76b200b image-full img-responsive" src="/assets/image_83988.jpg" title="Storage_history" /></a></p>
<p>Autodesk Construction Cloud は統合プラットフォームであるため、ACC、BIM 360、PlanDrid などのプロジェクトを透過的に運用することが求められます。Autodesk Docs は、BIM 360 のドキュメント管理層として使用されている BIM 360 Docs の ACC 版と捉えることが出来ます。</p>
<p style="padding-left: 40px;">※ Autodesk Docs は Autodesk Drive とは異なるプロジェクト ベースのドキュメント管理サービスです。</p>
<p style="padding-left: 40px;">※ AEC コレクションのサブスクライバは、ACC のドキュメント管理を担う Autodesk Docs を使用可能です。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0263e99a5933200b-pi" style="display: inline;"><img alt="Autodesk_docs" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0263e99a5933200b image-full img-responsive" src="/assets/image_868462.jpg" title="Autodesk_docs" /></a></p>
<hr />
<p>ここでご紹介した機能は、[コラボレート] リボンタブの [図面を共有]、[Autodesk Docs にプッシュ]、[トレースパネル] として表示されるボタンからアクセスします。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b026bdec0d343200c-pi" style="display: inline;"><img alt="Collaborate" border="0" class="asset  asset-image at-xid-6a0167607c2431970b026bdec0d343200c image-full img-responsive" src="/assets/image_900663.jpg" title="Collaborate" /></a></p>
<p>これら機能の使用には、AutoCAD をインストールしたコンピュータがインターネットに接続されている必要があります。AutoCAD 起動時にインターネット接続が確立できないと、次のダイアログが表示されます。コンピュータにキャッシュ データが残っていない場合には、[コラボレート] リボンタブに [図面を共有]、[Autodesk Docs にプッシュ]、[トレースパネル] のボタンが表示されない場合があります。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b026bdec6ee58200c-pi" style="display: inline;"><img alt="No_internet" border="0" class="asset  asset-image at-xid-6a0167607c2431970b026bdec6ee58200c image-full img-responsive" src="/assets/image_854699.jpg" title="No_internet" /></a></p>
<p>By Toshiaki Isezaki</p>
