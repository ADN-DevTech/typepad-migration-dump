---
layout: "post"
title: "ACC：新しいカスタム統合 UI"
date: "2024-02-21 02:10:53"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2024/02/acc-new-custom-integration-ui.html "
typepad_basename: "acc-new-custom-integration-ui"
typepad_status: "Publish"
---

<p>Autodesk Platform Services（APS）を使ったアプリが Autodesk Construction Cloud（ACC）ストレージ（Autodesk Docs）にアクセスするためには、同アプリが使用している Client ID を事前に ACC 側に登録しておく必要があります。ACC の&#0160; Account Admin が登録する事になるこの作業は「カスタム統合」と呼ばれています。</p>
<p>カスタム統合は、APS（旧 Forge）アプリが&#0160; BIM 360ストレージ（BIM 360 Docs）にアクセスする際にも必要な作業でした。</p>
<ul>
<li><a href="https://adndevblog.typepad.com/technology_perspective/2018/03/forge-and-bim-360-integration-tips.html" rel="noopener" target="_blank">Forge アプリを BIM 360 と連携するための「カスタム統合機能」の注意点</a></li>
</ul>
<p>カスタム統合でアプリの Client ID を登録するた めのユーザ インタフェースは、BIM 360 側にしか実装されていなかったため、ACC にアクセスする際にも、このユーザ インタフェースを利用する必要がありました。</p>
<ul>
<li><a href="https://adndevblog.typepad.com/technology_perspective/2023/06/custom-integration-steps-for-acc-and-aps-integration.html" rel="noopener" target="_blank">Autodesk Construction Cloud と APS 統合で必要なカスタム統合</a></li>
</ul>
<p>今回、新しく ACC 側にカスタム統合ユーザ インタフェースが実装されましたので、ご紹介しておきたいと思います。</p>
<ul>
<li><a href="https://help.autodesk.com/view/DOCS/JPN/?guid=Custom_Integrations" rel="noopener" target="_blank">https://help.autodesk.com/view/DOCS/JPN/?guid=Custom_Integrations</a></li>
</ul>
<hr />
<p><strong>カスタム統合の追加</strong></p>
<p>Account Admin アカウントで ACC にサインインすると、左手に「カスタム統合」が表示されます。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3a68113200c-pi" style="display: inline;"><img alt="Acc_custom_integration" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3a68113200c image-full img-responsive" src="/assets/image_347234.jpg" title="Acc_custom_integration" /></a></p>
<p>「カスタム統合」ページが表示されたら、<span style="background-color: #0696d7; color: #ffffff;"> [＋ カスタム統合を追加]&#0160;</span> をクリックすると、カスタム統合を開始することが出来ます。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02e860db5960200b-pi" style="display: inline;"><img alt="Add_custom_integration" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02e860db5960200b image-full img-responsive" src="/assets/image_272521.jpg" title="Add_custom_integration" /></a></p>
<p>[カスタム統合機能を追加] 画面が表示されたら、登録する Client ID とカスタム統合名（アプリ名）、説明を入力して右下の [追加] をクリックするだけです。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3aa5d7b200b-pi" style="display: inline;"><img alt="Custom_integration" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3aa5d7b200b image-full img-responsive" src="/assets/image_14985.jpg" title="Custom_integration" /></a></p>
<hr />
<p><strong>カスタム統合のステータス変更</strong></p>
<p>カスタム統合して ACC にアクセスしているアプリのアクセスを一時的に遮断（アクセス不可に）したい場合には、カスタム統合名をクリックして（①）、右手に表示される「ステータス」ドロップダウンを「アクティブ」から「非アクティブ」に変更します（②）。</p>
<p>もちろん、逆に「非アクティブ」から「アクティブ」することで、アプリのアクセスを復活させることも可能です。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3aac75a200d-pi" style="display: inline;"><img alt="Custom_integration_status" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3aac75a200d image-full img-responsive" src="/assets/image_430051.jpg" title="Custom_integration_status" /></a></p>
<hr />
<p><strong>カスタム統合の削除</strong></p>
<p>登録したカスタム統合を削除して、恒久的にアプリのアクセスを停止することが出来ます。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3aac789200d-pi" style="display: inline;"><img alt="Delee_custom_integration" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3aac789200d image-full img-responsive" src="/assets/image_387979.jpg" title="Delee_custom_integration" /></a></p>
<hr />
<p>ACC 側のカスタム統合は、BIM 360 側のカスタム統合よりも簡素化されていますが、どちらのユーザ インタフェースで登録しても、双方に統合情報が反映・表示されます。（反映表示にページの再ロードが必要な場合があります。）</p>
<p>By Toshiaki Isezaki</p>
