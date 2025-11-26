---
layout: "post"
title: "アプリ作成時のコラボレーター指定"
date: "2023-07-12 00:08:34"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2023/07/specifying-collaborators-feature-when-creating-app.html "
typepad_basename: "specifying-collaborators-feature-when-creating-app"
typepad_status: "Publish"
---

<p>Autodesk Platform Services（APS、旧 Forge）を利用してアプリを開発するには、<a href="https://adndevblog.typepad.com/technology_perspective/2016/07/app-registration-and-getting-keys-for-forge-api.html" rel="noopener" target="_blank">APS API を利用するアプリの登録とキーの取得</a> の手順でデベロッパーキーを取得（アプリの登録）する必要があります。</p>
<p>APS アプリ開発を複数の共同開発者とおこなう場合、APS ポータルにサインインしてデベロッパーキーを取得した開発者（所有者、Owner）、取得したデベロッパーキー（Client ID と Client Secret）を他の開発者に共有する必要があります。</p>
<p>このデベロッパーキーの共有作業は、長らく、メールなどを用いてオフラインでおこなっていました。Forge から Autodesk Platform Services への名称変更にともなうポータル サイトのデザイン更新後、新しい「Collaborator」機能を使ってデベロッパーキーを共有出来るようになっています。</p>
<p>Applications 画面から作成したアプリ（Application）画面を表示させると、最下部に「Collaborator」欄が表示されるます。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c1b2574754200d-pi" style="display: inline;"><img alt="No_collaborators" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c1b2574754200d image-full img-responsive" src="/assets/image_98514.jpg" title="No_collaborators" /></a></p>
<p><span style="color: #40a0ff; font-family: arial, helvetica, sans-serif;">＋ Add collaborator&#0160;</span>をクリックすると、次の画面に遷移して、デベロッパーキーを共有したい共同開発者のメール アドレス（Autodesk ID）を指定出来ます。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c1b257478a200d-pi" style="display: inline;"><img alt="Invide" class="asset  asset-image at-xid-6a0167607c2431970b02c1b257478a200d img-responsive" src="/assets/image_106550.jpg" style="display: block; margin-left: auto; margin-right: auto;" title="Invide" /></a></p>
<p>この時、共有する共同開発者の権限を指定することも可能です。具体的には、Client ID と Client Secret の閲覧のみの <strong>Viewer</strong> 権限と、Client Secret の再生成や利用 API の変更、アプリ名や説明の変更が可能な <strong>Editor</strong> 権限の 2 つから権限を指定することが出来ます。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02b751a8d769200c-pi" style="display: inline;"><img alt="Collaborator_permission" class="asset  asset-image at-xid-6a0167607c2431970b02b751a8d769200c img-responsive" src="/assets/image_621816.jpg" style="display: block; margin-left: auto; margin-right: auto;" title="Collaborator_permission" /></a></p>
<p>[Add collaborator] 画面で共同開発者のメール アドレス（Autodesk ID）を入力後、<span style="background-color: #111111; color: #ffffff;">&#0160;Send invite&#0160;</span> をクリックすると、Invitation（招待）メールが入力したメール アドレス宛に送られます。</p>
<p>デベロッパーキーの共有を完了するには、Invitation（招待）メールを受け取った開発者が、メール中の&#0160;<span style="color: #ffffff; background-color: #111111;">&#0160;Accept invite </span>（招待に同意）をクリックする必要があります。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02b751a8d81b200c-pi" style="display: inline;"><img alt="Invitation_email" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02b751a8d81b200c image-full img-responsive" src="/assets/image_757917.jpg" title="Invitation_email" /></a></p>
<p>招待された開発者が <span style="color: #ffffff; background-color: #111111;">&#0160;Accept invite </span>は で同意すると、APS ポータルへのサインインを求められます。サインインが完了すると、デベロッパーキー所有者が作成したアプリ（Application）が Applications 画面に表示されるようになります。これによって、Client ID や Client Secret などを確認することが出来ます。もちろん、Editor 権限の場合には、一部内容を変更したり、他の共同開発者をコラボレーターとして招待したりすることが可能です。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c1a6cb634f200b-pi" style="display: inline;"><img alt="Shared_app" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c1a6cb634f200b image-full img-responsive" src="/assets/image_371993.jpg" title="Shared_app" /></a></p>
<p>なお、共同開発者が招待に同意すると、招待した開発者には、同意を知らせる通知メールが送られます。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02b751a8d84d200c-pi" style="display: inline;"><img alt="Accepted" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02b751a8d84d200c image-full img-responsive" src="/assets/image_708091.jpg" title="Accepted" /></a></p>
<p>デベロッパーキーを共有している全ての開発者の当該アプリ（Application）画面では、「Collaborator」欄に共同開発者の一覧が表示されるようになります。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c1a6cb6338200b-pi" style="display: inline;"><img alt="Added_collaborators" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c1a6cb6338200b image-full img-responsive" src="/assets/image_939213.jpg" title="Added_collaborators" /></a></p>
<p>共同開発者を複数招待してデベロッパーキーを共有した場合でも、APS の課金対象者は、デベロッパーキーを取得（アプリの登録）した所有者（Owner）になります。</p>
<p>By Toshiaki Isezaki</p>
