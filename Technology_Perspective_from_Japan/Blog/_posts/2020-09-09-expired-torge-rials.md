---
layout: "post"
title: "Forge トライアルの終了で起こること"
date: "2020-09-09 00:24:12"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2020/09/expired-torge-rials.html "
typepad_basename: "expired-torge-rials"
typepad_status: "Publish"
---

<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0263e9628e1f200b-pi" style="display: inline;"><img alt="Forge_trial" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0263e9628e1f200b image-full img-responsive" src="/assets/image_662363.jpg" title="Forge_trial" /></a></p>
<p><span style="background-color: #ffff00;">Autodesk Platform Services（旧 Autodesk Forge）の課金制度は、2022年11月7日に Autodesk Flex に移行しています。詳しくは、<strong><a href="https://adndevblog.typepad.com/technology_perspective/2022/11/flex-token-adoption-into-aps-on-11-7.html" rel="noopener" style="background-color: #ffff00;" target="_blank">11月7日 APS へ Flex トークンを導入</a></strong> の記事をご確認ください。このため、本記事でご紹介している <strong>Forge Account Details</strong> には、サブスクリプションの状態は表示されなくなっています。</span></p>
<p>Autodesk ID&#0160;&#0160;を使っては Forge ポータルから初めて<strong><a href="https://adndevblog.typepad.com/technology_perspective/2016/07/app-registration-and-getting-keys-for-forge-api.html" rel="noopener" target="_blank">アプリ登録（Client ID/Secret を取得）</a></strong>すると、90 日間有効な 100 クラウドクレジットがアカウントに付与されて、Forge トライアル期間が始まります。</p>
<p>2020年9月14日以降、90日間が経過、または、付与されたすべてのクラウドクレジットを消費し尽してトライアル期間が終了すると、Forge API の呼び出しに対してエラーを返すようになります。具体的には、403 エラーとともに、“You are not allowed to use this API because your Forge Trial is expired.”（Forgeトライアルの有効期限が切れているため、この API は使用できません）が返されるようになります。</p>
<pre class="code-snippet more-toggle--slide snippet-container js-more-toggle more-toggle" data-toggle-end-color="rgba(250, 250, 250, 1)" data-toggle-height="400" data-toggle-less="Hide Code" data-toggle-more="Show Code"><code class="language-json code-overflow-x hljs " id="snippet-1">{
   &quot;<span class="hljs-attribute">developerMessage</span>&quot;: <span class="hljs-value"><span class="hljs-string">&quot;Your are not allowed to use this API because your Forge trial is expired&quot;</span></span>,
   &quot;<span class="hljs-attribute">errorCode</span>&quot;: <span class="hljs-value"><span class="hljs-string">&quot;ERR-004&quot;</span> 
</span>}</code></pre>
<p>アカウント自体が無効になっていると、&quot;Developer Status is not Active&quot; エラーを返す場合もあります。</p>
<pre class="code-snippet more-toggle--slide snippet-container js-more-toggle more-toggle" data-toggle-end-color="rgba(250, 250, 250, 1)" data-toggle-height="400" data-toggle-less="Hide Code" data-toggle-more="Show Code"><code class="language-json code-overflow-x hljs " id="snippet-0">{
   &quot;<span class="hljs-attribute">fault</span>&quot;:<span class="hljs-value">{
      &quot;<span class="hljs-attribute">faultstring</span>&quot;:<span class="hljs-string">&quot;Developer Status is not Active&quot;</span>,
      &quot;<span class="hljs-attribute">detail</span>&quot;:{
         &quot;<span class="hljs-attribute">errorcode</span>&quot;:<span class="hljs-string">&quot;keymanagement.service.DeveloperStatusNotActive&quot;</span>
      }
   }
</span>}</code></pre>
<p>Forge トライアル終了前に事前に営業担当者からメールでコンタクトがあるはずですが、継続してお使いの Client ID/Secret をお使いいただく場合には、Forge サブスクリプションをご購入いただく必要があります。</p>
<p>お使いのアカウント（Autodesk ID）が Forge トライアル期間にあるか否か確認するには、Forge ポータルのサインインしてから、<strong>Forge Account Details</strong> をクリックしてみてください。トライアル期間内であれば、表示されるページに「<strong>IN PROGRESS</strong>」と表示されます。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0263e963b493200b-pi" style="display: inline;"><img alt="Intrial" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0263e963b493200b image-full img-responsive" src="/assets/image_921375.jpg" title="Intrial" /></a></p>
<p>もし、アカウントが Forge トライアルを終了している場合には、同じページ上で「<strong>EXPIRED</strong>」と表示されます。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b026be40fd43a200d-pi" style="display: inline;"><img alt="Expired" border="0" class="asset  asset-image at-xid-6a0167607c2431970b026be40fd43a200d image-full img-responsive" src="/assets/image_554333.jpg" title="Expired" /></a></p>
<p>同様に、アカウントが Forge サブスクリプションに加入していれば、「<strong>SUBSCRIBED</strong>」と表示されます。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b026bde9106b2200c-pi" style="display: inline;"><img alt="Subscribed" border="0" class="asset  asset-image at-xid-6a0167607c2431970b026bde9106b2200c image-full img-responsive" src="/assets/image_590593.jpg" title="Subscribed" /></a></p>
<p>Forge サブスクリプションの購入については、以前ご紹介したブログ記事を改定して、<strong><a href="https://adndevblog.typepad.com/technology_perspective/2020/02/important-about-forge-charge-of-cost-revised.html" rel="noopener" target="_blank">【重要】Forge サブスクリプション</a></strong>で詳細をご案内していますので、ご確認をお願いしたします。<a href="https://forge.autodesk.com/blog/forge-pricing-explained-3-what-does-each-forge-api-cost"></a>Forge トライアルが終了してしまい Forge API アクセス が遮断されてしまい、Forge サブスクリプションの購入によって再アクティブ化をご希望の場合は、<strong><a href="https://adndevblog.typepad.com/technology_perspective/2020/02/important-about-forge-charge-of-cost-revised.html" rel="noopener" target="_blank">【重要】Forge サブスクリプション</a></strong>でご案内している Web フォームから、または、 <a href="mailto:forge.sales@autodesk.com">forge.sales@autodesk.com</a> までメールにてご相談ください。</p>
<ul>
<li>課金対象になっていない OAuth API、Data Management API、WebHooks API、BIM 360 API のみをお使いの場合でも、継続した API アクセスを維持していただくために、Forge サブスクリプションのご購入は必要となりますのでご注意ください。</li>
</ul>
<p>By Toshiaki Isezaki</p>
