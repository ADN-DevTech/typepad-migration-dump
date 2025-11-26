---
layout: "post"
title: "ストレージ サービスへの A360 オンライン ビューワーの統合"
date: "2015-10-05 00:03:17"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2015/10/a360-online-viewer-integration-to-storage-service.html "
typepad_basename: "a360-online-viewer-integration-to-storage-service"
typepad_status: "Publish"
---

<p>現在では、非常に多くのストレージ サービスがクラウドを使って提供されて、さまざまな種類のファイルをクラウド ストレージにアップロードして、遠隔地からアクセスしたり、第三者と共有したりすることが出来ます。</p>
<p>これらストレージ サービスにもプレビュー機能が用意されているので、PDF ファイルを含む Office 系のファイルをそのまま Web ブラウザだけで表示することが出来ます。ただし、CAD で作成されたデザイン ファイルのプレビューとなると、実装されているストレージ サービスは多くありません。そこで、オートデスクの登場です。</p>
<p>さまざまなデザイン ファイルを変換してストリーミング配信出来る&#0160;&#0160;<a href="https://360.autodesk.com/viewer" target="_blank"><strong>A360 Online Viewer</strong></a>&#0160;が、<a href="https://www.box.com/ja_JP/home/" target="_blank"><strong>Box</strong></a>&#0160;と&#0160;<strong><a href="https://www.dropbox.com/ja/" target="_blank">Dropbox</a></strong>、<strong><a href="https://www.google.com/intl/ja_jp/drive/" target="_blank">Google ドライブ</a>&#0160;</strong>ストレージ サービスに統合されています。</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d1674aed970c-pi" style="display: inline;"><img alt="Box_dropbox_google_drive" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b8d1674aed970c image-full img-responsive" src="/assets/image_98447.jpg" title="Box_dropbox_google_drive" /></a></p>
<p><strong>A360 Online Viewer からのアクセス</strong></p>
<p>いずれのクラウド サービス アカウントをお持ちでストレージ内に 2D 図面や 3D モデルを保存している場合には、A360 Online Viewer 側からストレージ サービスにサインインして、デザイン ファイルを表示して閲覧することが出来ます。</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb08815c76970d-pi" style="display: inline;"><img alt="A360_online_viewer" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01bb08815c76970d image-full img-responsive" src="/assets/image_700605.jpg" title="A360_online_viewer" /></a></p>
<p>表示したデザイン データは、もちろん、分解表示したり、オブジェクトの一時的な表示/非表示コントロール、プロパティ表示などさまざまに操作することが出来ます。関係者とリアルタイムにコラボレーション出来る <a href="http://adndevblog.typepad.com/technology_perspective/2015/04/new-capabilitie-a360-viewer.html" target="_blank"><strong>Live Review</strong></a> 機能も同様です。</p>
<p><strong>Box ストレージからの A360 Online Viewer</strong></p>
<p>Box アカウントをお持ちであれば、無償で設定可能な Box アプリを追加することで、Box ストレージ内から直接 A360 Online Viewer を利用できるようになっています。</p>
<p>Box にサインイン後、アカウントの [アプリケーション] メニューから Autodesk A360 Viewer を検索するか、直接&#0160;<a href="https://app.box.com/services/autodesk_a360_viewer" target="_blank"><strong>https://app.box.com/services/autodesk_a360_viewer</strong></a>&#0160;を開いて、[追加] ボタンでアプリを追加することが出来ます。</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb087c527a970d-pi" style="display: inline;"><img alt="Box_app" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01bb087c527a970d image-full img-responsive" src="/assets/image_934455.jpg" title="Box_app" /></a></p>
<p>この設定が完了したら、Box ストレージ上のデザイン ファイル上でマウス右ボタンでコンテキスト メニューを表示させて、[その他の操作] &gt;&gt; [Autodesk A360 Viewer] を選択するだけです。</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb087c54f1970d-pi" style="display: inline;"><img alt="Launch_a360_viewer" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01bb087c54f1970d image-full img-responsive" src="/assets/image_507342.jpg" title="Launch_a360_viewer" /></a><br />A360 Online Viewer が Web ブラウザ上に表示されて、選択されたファイルが Online Viewer が使用する AWS ストレージにアップロードされ、ファイル変換後に表示されます。表示後に利用できる機能は、A360 Online Viewer と同等です。</p>
<p>A360 Online Viewer については、次のブログ記事をご確認ください。</p>
<p style="padding-left: 30px;"><a href="http://adndevblog.typepad.com/technology_perspective/2015/03/easy-way-to-test-a360-viewer.html" target="_blank"><strong>簡単に A360 ビューワー機能を評価する方法</strong></a></p>
<p style="padding-left: 30px;"><strong><a href="http://adndevblog.typepad.com/technology_perspective/2015/04/new-capabilitie-a360-viewer.html" target="_blank">A360 ビューワーの新機能</a></strong></p>
<p>なお、A360 Online Viewer は <a href="http://adndevblog.typepad.com/technology_perspective/2015/09/about-view-and-data-api.html" target="_blank"><strong>View and Data API</strong> </a>で実装されていて、アップロードされたファイルには Temporary ポリシーが適用されます。つまり、A360 Online Viewer 上にアップロードされたファイルは、30日間後に自動的に削除されます（Box ストレージからは削除されません）。</p>
<p>By Toshiaki Isezaki&#0160;</p>
