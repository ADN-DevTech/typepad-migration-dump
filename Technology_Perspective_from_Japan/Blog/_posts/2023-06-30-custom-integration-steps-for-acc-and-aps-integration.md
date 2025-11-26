---
layout: "post"
title: "Autodesk Construction Cloud と APS 統合で必要なカスタム統合"
date: "2023-06-30 00:39:48"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2023/06/custom-integration-steps-for-acc-and-aps-integration.html "
typepad_basename: "custom-integration-steps-for-acc-and-aps-integration"
typepad_status: "Publish"
---

<p>APS アプリが Autodesk Construction Cloud にアクセス（データ アクセス、および、<a href="https://aps.autodesk.com/developer/overview/autodesk-construction-cloud" rel="noopener" target="_blank">ACC API</a> アクセス）する場合には、BIM 360 との統合と同じく、Provisioning と呼ばれる「カスタム統合」の手続きで、事前に使用する Client ID を Autodesk Construction Cloud&#0160; に登録しておく必要があります。なお、使用するユーザインタフェースは、BIM 360 Admin（管理者）と共通になっています。</p>
<p style="padding-left: 40px;">ご参考：<a href="https://adndevblog.typepad.com/technology_perspective/2018/03/forge-and-bim-360-integration-tips.html" rel="noopener" target="_blank">Forge アプリを BIM 360 と連携するための「カスタム統合機能」の注意点</a></p>
<hr />
<ol>
<li><a href="https://adndevblog.typepad.com/technology_perspective/2016/07/app-registration-and-getting-keys-for-forge-api.html" rel="noopener" target="_blank">APS API を利用するアプリの登録とキーの取得</a> の手順で、ACC 統合アプリ開発に使用するデベロッパーキーを取得（アプリの登録）します。</li>
<li>Autodesk Construction Cloud の Account Admin（アカウント管理者） 権限をお持ちの方が、Autodesk Construction Cloud にサインインして、[BIM 360 管理者] をクリックし ます。<br /><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c1a6cc3539200b-pi" style="display: inline;"><img alt="Acc_account_admin" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c1a6cc3539200b image-full img-responsive" src="/assets/image_315140.jpg" title="Acc_account_admin" /></a></li>
<li>BIM 360 管理者ページから「アプリ」をクリックします。<br /><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c1b2581839200d-pi" style="display: inline;"><img alt="App" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c1b2581839200d image-full img-responsive" src="/assets/image_761082.jpg" title="App" /></a></li>
<li>Account Admin ページ上部から「設定」をクリックします。<br /><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c1b2581840200d-pi" style="display: inline;"></a> <a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c1a6cc3561200b-pi" style="display: inline;"><img alt="Settings" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c1a6cc3561200b image-full img-responsive" src="/assets/image_671962.jpg" title="Settings" /></a></li>
<li>遷移したページ上部の「カスタム統合」をクリックします。<br /><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02b751a9a489200c-pi" style="display: inline;"><img alt="Custom_integration" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02b751a9a489200c image-full img-responsive" src="/assets/image_595473.jpg" title="Custom_integration" /></a></li>
<li>カスタム統合ページ左手から「カスタム統合機能を追加」をクリックします。<br /><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02b751a9a491200c-pi" style="display: inline;"><img alt="Add_custom_integration" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02b751a9a491200c image-full img-responsive" src="/assets/image_930048.jpg" title="Add_custom_integration" /></a></li>
<li>アクセス権の選択 ページが表示されたら、「BIM 360 Account Administration」 と「Document Management」にチェックして [次へ] をクリックします。&#0160;<br /><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c1b2581a96200d-pi" style="display: inline;"><img alt="Select_permissions" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c1b2581a96200d image-full img-responsive" src="/assets/image_809314.jpg" title="Select_permissions" /></a></li>
<li>次のページが表示されたら、「私は開発者です」にチェックして [次へ] をクリックします。&#0160;<br /><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c1a6cc37ce200b-pi" style="display: inline;"><img alt="I_am_developer" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c1a6cc37ce200b image-full img-responsive" src="/assets/image_274226.jpg" title="I_am_developer" /></a></li>
<li>次のページでは、表示される BIM 360 アカウント ID をメモし、その横のチェックボックスをオンにしてください。また、「クライアント ID」と「アプリ名」のフィールドに、1. で取得したアプリのうち、Client ID と入力したアプリ名を入力します。<br /><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c1a6cc3803200b-pi" style="display: inline;"><img alt="Client_id_to_integrate" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c1a6cc3803200b image-full img-responsive" src="/assets/image_903367.jpg" title="Client_id_to_integrate" /></a></li>
<li>少し下側にスクロールして [保存] をクリックしてカスタム統合の設定を保存します。<a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c1b2581afc200d-pi" style="display: inline;"><img alt="Save" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c1b2581afc200d image-full img-responsive" src="/assets/image_264651.jpg" title="Save" /></a></li>
</ol>
<hr />
<p>これでカスタム統合した Client ID を持つ APS アプリが、ACC にアクセス出来るようになります。</p>
<p>By Toshiaki Isezaki</p>
