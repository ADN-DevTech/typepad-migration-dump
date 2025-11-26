---
layout: "post"
title: "Fusion 360 アップデートと日本語化"
date: "2015-10-09 06:57:58"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2015/10/fusion-360-update-and-localization.html "
typepad_basename: "fusion-360-update-and-localization"
typepad_status: "Publish"
---

<p>9月20日と27日の2回に分けて、Fuison 360 がアップデートされました。20日の更新では、シミュレーション機能が導入されて新しいワークスペースが用意されたことに加え、20日、27日の更新で、正式に日本語化されたユーザ インタフェースの採用が開始されています。</p>
<p>Fsuion 360 は定期的に新機能が導入されつつあるクラウド サービスなので、英語版で登場した機能のすべてが、すぐに日本語で表示されない点に注意してください。今回の日本語化にあたっても、まずは新機能であるシミュレーション機能以外が日本語化されています。ここで日本語化されていない機能についても、順次、今後のアップデートで日本語化されてくるはずです。現在、日本語されているワークスペースと付随するツールバーは次のとおりです。</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d15cc572970c-pi" style="display: inline;"><img alt="Localized_fusion_workspace" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b8d15cc572970c image-full img-responsive" src="/assets/image_936101.jpg" title="Localized_fusion_workspace" /></a></p>
<p>なお、Fusion 360 API の操作部分は以前から実装されていたので、今回の更新で、インタフェースも日本語化されています。</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c7d1418c970b-pi" style="display: inline;"><img alt="Api_menu" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b7c7d1418c970b image-full img-responsive" src="/assets/image_428780.jpg" title="Api_menu" /></a></p>
<p>さて、今回の新機能の目玉は、シミュレーション機能の実装です。モデリングした 3D モデルをメッシュ化して、荷重や拘束条件、材質を設定することで、Fusion 360 上でアニメーションを含めたシミュレーションをおこなうことが出来ます。作成された結果は、A360 オンラインビューワーでも表示させることが出来ます。&#0160;</p>
<p>&#0160;<a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c7d13f8a970b-pi" style="display: inline;"><img alt="Sim_animation" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b7c7d13f8a970b img-responsive" src="/assets/image_304870.jpg" title="Sim_animation" /></a></p>
<p>従来の CAD や CAM の機能や、レンダリングやアニメーションに代表されるプレゼンテーション機能も含め、今回のシミュレーション機能の導入で、Fusion 360 &#0160;の機能も多彩なものに変化してきています。2D 図面化の強化も含め、Fusion 360 は、今後、製造系製品に必要な機能を集約して利用するプラットフォームとして機能強化されるはずです。日本語化だけでなく、本体機能の向上にもご期待ください。</p>
<p>さて、もう1つご紹介しておきたい機能があります。A360 オンライン ビューワーのブログ記事でもご案内した <a href="http://adndevblog.typepad.com/technology_perspective/2015/04/new-capabilitie-a360-viewer.html" target="_blank"><strong>ライブレビュー</strong></a><a href="http://adndevblog.typepad.com/technology_perspective/2015/04/new-capabilitie-a360-viewer.html" target="_blank"><strong>（</strong></a><strong><a href="http://adndevblog.typepad.com/technology_perspective/2015/04/new-capabilitie-a360-viewer.html" target="_blank"><strong>Live Review</strong></a>）</strong>です。この機能は、現在、プレビュー機能として評価することを目的に実装されているので、既定値で機能がオフに設定されています。ライブレビューの機能を有効にするには、Fusion 360 画面右上のアカウント メニューから、[基本設定] ダイアログを表示させて、「プレビュー」カテゴリの「ライブ レビュー」項目にチェックを入れる必要があります。</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb087726e0970d-pi" style="display: inline;"><img alt="Live_review_option" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01bb087726e0970d image-full img-responsive" src="/assets/image_658434.jpg" title="Live_review_option" /></a></p>
<p>ライブレビューがオンになると、メニューに [ライブ レビュー セッション ...] メニューが表示されます。これで、ライブレビューが利用出来るようになります。</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb08758b90970d-pi" style="display: inline;"><img alt="Live_review_menu" class="asset  asset-image at-xid-6a0167607c2431970b01bb08758b90970d img-responsive" src="/assets/image_465694.jpg" title="Live_review_menu" /></a></p>
<p>A360 オンライン ビューワー同士のライブレビューでは、Web ブラウザを利用する複数のレビューワー間で、画面操作やオブジェクト選択などの動きを同期しながらチャットすることが出来ます。ただし、表示している 3D モデル自身を編集する機能はありませんでした。</p>
<p>Fusion 360 のライブレビューでは、ライブレビュー セッション用に作成される URL を、メール等で レビューワーに伝えることで開始されます。セッションが確立されると、Fusion 360 上の編集操作がリアルタイムで レビューワーの Web ブラウザ画面に伝わります。チーム設計をおこなっている場合には、相互コミュニケーションが劇的に改善する画期的な機能であり、Fusion 360 がクラウドの利点を備えた CAD であることを改めて印象付けるものと言えます。</p>
<p>次の動画は、ライブレビューを使った簡単なデモです。ぜひ、お試しください。</p>
<p><iframe allowfullscreen="" frameborder="0" height="281" src="https://www.youtube.com/embed/6v1eUFb6PEg?feature=oembed" width="500"></iframe>&#0160;</p>
<p>今回の更新に先立って、Fusion 360 のデータパネルとして使われている A360 Team も更新されています。</p>
<p>By Toshiaki Isezaki</p>
