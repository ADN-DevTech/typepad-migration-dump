---
layout: "post"
title: "TLS 1.2 への Forge システム アップグレードについて"
date: "2018-07-10 18:25:05"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2018/07/upcoming-forge-system-upgrade-tls-12.html "
typepad_basename: "upcoming-forge-system-upgrade-tls-12"
typepad_status: "Publish"
---

<div class="blog__content--full">
<div class="blog__body node__body">
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b022ad37ff8b1200d-pi" style="display: inline;"><img alt="Security-and-technology_0" border="0" class="asset  asset-image at-xid-6a0167607c2431970b022ad37ff8b1200d image-full img-responsive" src="/assets/image_122035.jpg" title="Security-and-technology_0" /></a></p>
<p>&lt;本記事は&#0160;<a href="https://forge.autodesk.com/blog/upcoming-forge-system-upgrade-tls-12" rel="noopener noreferrer" target="_blank">h</a><a href="https://forge.autodesk.com/blog/upcoming-forge-system-upgrade-tls-12" rel="noopener noreferrer" target="_blank">ttps://forge.autodesk.com/blog/upcoming-forge-system-upgrade-tls-12</a> の和訳版です&gt;</p>
<p>オートデスクのセキュリティチームは、TLS 1.0 と TLS 1.1 の継続サポートがセキュリティ上のリスクを引き起こすと強く感じ続けてけてきました。そこで、セキュリティとデータの整合性に関する業界のベストプラクティスに対応する目的で、オートデスクは Forgeプラットフォーム を TLS 1.2 に移行するとともに、2018 年 7 月 31 日に TLS 1.0、及び TLS 1. 1のサポートを中止することを決定するに至りました。</p>
<p>デベロッパの皆様には、Production インスタンスの中断を防ぐため、7 月 31 日以前に下記にご案内するアクションが必要となります。稼働中のアプリ、ないしサービスが TLS 1.0、あるいは TLS 1.1 を使用している場合には、アプリケーションは 2018 年 7 月 3 1 日以降、Forge Platform API を呼び出すことができなくなります。 7 月 31 日までに TLS バージョンを 1.2 に更新するようお願いいたします。</p>
<p><strong>TLS とは?</strong></p>
<p>TLS は&#0160; &quot;Transport Layer Security&quot; の略で、アプリケーション間通信でプライバシーとデータの整合性を提供するプロトコルです。 今日最も広く使われているセキュリティプロトコルでもあり、Web ブラウザやネットワーク上でデータを安全にやり取りする必要の多くのアプリケーションで使用されています。 TLS は、リモートエンドポイントへの接続が、暗号化とエンドポイントのアイデンティティ検証を通して目的のエンドポイントであることを保証します。 これまでの TLS のバージョンは、TLS 1.0、 1.1、と最新の 1.2 です。</p>
<p>Forge Platform API の接続は、セキュリティの重要なコンポーネントとして TLS を使用しています。 HTTPS（Web）と STARTTLS SMTP（電子メール）も TLSを セキュリティの主要コンポーネントとして使用します。</p>
<p><strong>なぜ TLS を変更するのか?</strong></p>
<p>オートデスクは信用が企業の一番の価値であることを認識していて、最新のセキュリティ プロトコルを使用してお客様のセキュリティを継続的に向上させることに常に注力しています。 2018 年 7 月 31 日、Autodesk Forge は最高のセキュリティ基準を維持し、顧客データの安全性を促進するために、TLS 1.2 以降の暗号化プロトコルを必須とするため変更を決定しました。</p>
<p><strong>お客様はどのように影響を受けますか？</strong></p>
<p>オートデスクが TLS 1.0/1.1 を無効にすると、TLS 1.0/1.1 に依存する Forge アプリケーションからのインバウンド接続は失敗してしまいます。 これはすべての Forge Web サービスに影響を与えることになるため重要です。</p>
<p><strong>どう対応する必要がありますか？</strong></p>
<p>チェックリスト：TLS 1.2へのシステムアップグレードの準備</p>
<ul>
<li>Web サイトがホストされている場合は、ホスティング プロバイダが TLS 1.2 をサポートしているかどうかを確認してください。</li>
<li>Web サイトをホストする場合は、システムがすでに TLS 1.2 をサポートしているかどうかを確認してください。</li>
<li>お使いのホスト環境が TLS 1.2 をサポートしていない場合は、アップグレードを手配/リクエストしてください。</li>
<li>使用可能な TLS の最新バージョンを常に使用するよう、システムをコーディングしてください。 また、特定の（古い）バージョンのハードコーディングは避けてください。</li>
</ul>
<p><strong>技術情報</strong></p>
<p>技術情報セクションは高度に技術的な性質のものであり、次のいずれかによってレビューする必要があります:</p>
<ul>
<li>お使いの Web[ ホスティング企業</li>
<li>お使いのソフトウェア プロバイダ</li>
<li>社内 Web プログラマ/システム管理者</li>
</ul>
<p><strong>API（インバウンド）統合</strong></p>
<p>下記のバージョンが記載されていない場合は、通常、TLS 1.2 と互換性がないことを意味します。 ご使用のシステム、または、環境がリストされていない場合は、互換性についてシステム/環境のドキュメントを参照してください。 可能な限り多くを列挙するよう努めましたが、すべてを記載することは出来ませんのでご了承ください。</p>
<table border="1" cellpadding="0" cellspacing="0">
<tbody>
<tr>
<td valign="top">
<p>Java 8 (1.8) 以降</p>
</td>
<td style="width: 80%;" valign="top">
<p>既定値で TLS 1.1 または、それ以降と互換性があります。</p>
</td>
</tr>
<tr>
<td valign="top">
<p>Java 7 (1.7)</p>
</td>
<td valign="top">
<p>HttpsURLConnection の https.protocols Java システムプロパティを使用して、TLS 1.1、および、TLS 1.2 を有効にします。 非 HttpsURLConnection 接続で TLS 1.1、および、TLS 1.2 を有効にするには、アプリケーションソースコード内に作成された SSLSocket、および、SSLEngine インスタンスで有効なプロトコルを設定します。 より新しい Oracle Java バージョンへのアップグレードが実現できない場合、IBM Java への切り替えは効果的な回避策となる可能性があります。</p>
</td>
</tr>
<tr>
<td valign="top">
<p>Java 8 (IBM)</p>
</td>
<td valign="top">
<p>既定値で TLS 1.1以降と互換性があります。 アプリケーション、または、アプリケーションによって呼び出されるライブラリが SSLContext.getinstance（ &quot;TLS&quot;）を使用する場合は、com.ibm.jsse2.overrideDefaultTLS = true を設定する必要があります。</p>
</td>
</tr>
<tr>
<td valign="top">
<p>Java 7 以上、Java 6.0.1 サービスリフレッシュ 1（J9 VM2.6）以上、Java 6 サービスリフレッシュ10 以上</p>
</td>
<td valign="top">
<p><a href="https://www.ibm.com/support/knowledgecenter/SSYKE2_6.0.0/com.ibm.java.security.component.60.doc/security-component/jsse2Docs/overrideSSLprotocol.html">IBMのドキュメント</a>&#0160;で推奨されているように、HttpsURLConnection の https.protocols Java システム・プロパティと SSLSocket、および SSLEngine 接続の com.ibm.jsse2.overrideDefaultProtocol Java システム・プロパティを使用して、TLS 1.2 を有効化してください。 また、<a href="https://www.ibm.com/support/knowledgecenter/SSYKE2_8.0.0/com.ibm.java.security.component.80.doc/security-component/jsse2Docs/matchsslcontext_tls.html">com.ibm.jsse2.overrideDefaultTLS=true</a> を設定する必要があります。</p>
</td>
</tr>
<tr>
<td valign="top">
<p>.NET 4.6 以降</p>
</td>
<td valign="top">
<p>既定値で TLS 1.1 以降と互換性があります。</p>
</td>
</tr>
<tr>
<td valign="top">
<p>.NET 4.5 から 4.5.2</p>
</td>
<td valign="top">
<p>.NET 4.5、4.5.1、および、4.5.2 では、既定値で TLS 1.1 と TLS 1.2 は有効になっていません。次のように、これらを有効化する 2 つのオプションが存在しています。</p>
<p>オプション1：</p>
<p>.NET アプリケーションは、System.Net.ServicePointManager.SecurityProtocolをSecurityProtocolType.Tls12 とSecurityProtocolType.Tls11 を有効にするように設定することで、TLS 1.1 と TLS 1.2 をソフトウェアコードで直接有効にすることができます。次の例は、その C＃コード例です。</p>
<p>System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;</p>
<p>オプション2：</p>
<p>次のレジストリキーを設定することで、ソースコードを変更せずに TLS 1.2を既定で有効にすることができます。</p>
<p>下記のキーを見つけて SchUseStrongCrypto DWORD 値を 1 に設定します。キーが存在しない場合は作成してください。</p>
<p style="padding-left: 30px;">&quot;HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\.NETFramework\v4 .030319&quot;</p>
<p style="padding-left: 30px;">&quot;HKEY_LOCAL_MACHINE\SOFTWARE\Wow6432Node\Microsoft\.NETFramework\v4.0.30319&quot;</p>
<p>これらのレジストリキーのバージョン番号は 4.0.30319 ですが、.NET 4.5、4.5.1、および、4.5.2 の Framework でもこれらの値が使用されます。ただし、これらのレジストリキーを使用すると、既定で、そのシステム上にインストールされているすべての .NET 4.0、4.5、4.5.1、および、4.5.2 アプリケーションで TLS 1.2 が有効になります。したがって、この変更を本番サーバーに展開する前にこの変更をテストすることをお勧めします。これは、レジストリ インポート ファイルとしても利用できます。ただし、これらのレジストリ値は、System.Net.ServicePointManager.SecurityProtocol 値を設定する.NETアプリケーションには影響しません。</p>
</td>
</tr>
<tr>
<td valign="top">
<p>.NET 4.0</p>
</td>
<td valign="top">
<p>.NET 4.0 では、既定値で TLS 1.2 は有効になっていません。 既定で TLS 1.2 を有効化するには、.NET Framework 4.5 または、それ以降のバージョンをインストールし、次の2つのレジストリキー、</p>
<p style="padding-left: 30px;">&quot;HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\.NETFramework\v4.0.30319 &quot;</p>
<p style="padding-left: 30px;">&quot;HKEY_LOCAL_MACHINE\SOFTWARE\Wow6432Node\Microsoft\.NETFramework\v4.0.30319 &quot;</p>
<p>の SchUseStrongCrypto DWORD 値を 1 に設定します。もし、キーが存在しない場合は作成してください。</p>
<p>ただし、これらのレジストリキーを使用すると、既定システム上にインストールされているすべての .NET 4.0、4.5、4.5.1、および、4.5.2アプリケーションで TLS 1.2 が有効になる場合があります。 この変更を運用サーバーに展開する前に、この変更をテストすることをお勧めします。 これは、レジストリ インポート ファイルとしても利用できます。ただし、これらのレジストリ値は、 System.Net.ServicePointManager.SecurityProtocol 値を設定する .NE Tアプリケーションには影響しません。</p>
</td>
</tr>
<tr>
<td valign="top">
<p>Python 2.7.9以降</p>
</td>
<td valign="top">
<p>既定値で TLS 1.1 以降と互換性があります。</p>
</td>
</tr>
<tr>
<td valign="top">
<p>Ruby 2.0.0</p>
</td>
<td valign="top">
<p>TLS 1.2 は、OpenSSL 1.0.1 以降で使用すると既定で有効になります。 ：SSLSontext の ssl_versionで :TLSv1_2 (preferred) シンボルを使用すると、TLS 1.0 以前のバージョンが無効になっていることを確認できます。 ：TLSv1.2 には、OpenSSL 1.0.1c 以降が必要です。</p>
<p>&#0160;</p>
</td>
</tr>
<tr>
<td valign="top">&#0160;</td>
<td valign="top">
<p>TLS 1.2 は、OpenSSL 1.0.1 以降の使用が必須となります。PHP_CURL が HTTP 接続を行うために使用する OpenSSL エクステンションです。PHP_CURL OpenSSL エクステンション機能は、TLSv1.2を サポートしている必要があります。cURL の openssl_version 情報を調べるには、run: php -r &#39;echo json_encode(curl_version(), JSON_PRETTY_PRINT);&#39; を実行してください。</p>
</td>
</tr>
<tr>
<td valign="top">
<p>Node.js</p>
</td>
<td valign="top">
<p><a href="https://nodejs.org/api/tls.html">https://nodejs.org/api/tls.html</a>&#0160;をご確認ください。TLSv1.2 には、OpenSSL 1.0.1c 以降が必要です。 Node.js はシステム提供のOpenSSL を使用します。</p>
</td>
</tr>
<tr>
<td valign="top">
<p>Microsoft WinINet</p>
<p>Windows Server 2012 R2 以上</p>
<p>Windows 8.1以降</p>
</td>
<td valign="top">
<p>既定値で TLS 1.1 以降と互換性があります。</p>
</td>
</tr>
<tr>
<td valign="top">
<p>Microsoft WinINet</p>
<p>Windows Server 2008 R2〜2012</p>
<p>Windows 7 および 8</p>
</td>
<td valign="top">
<p>Internet Explorer 11 がインストールされている場合は、既定で互換性があります。 Internet Explorer 8、9、または、10がインストールされている場合は、互換性のために TLS 1.2 をユーザまたは管理者が有効にする必要があります。</p>
</td>
</tr>
<tr>
<td valign="top">
<p>Microsoft Secure Channel (Schannel)</p>
<p>Windows Server 2012 R2 以降</p>
<p>Windows 8.1 以降</p>
</td>
<td valign="top">
<p>既定値で TLS 1.1 以降と互換性があります。</p>
</td>
</tr>
<tr>
<td valign="top">
<p>Microsoft Secure Channel (Schannel)</p>
<p>Windows Server 2012</p>
<p>Windows 8</p>
</td>
<td valign="top">
<p>TLS 1.1 と TLS 1.2 は既定値では無効化されていますが、アプリケーションで有効化している場合には利用出来ます。 TLS 1.1 および TLS 1.2 は、レジストリ内で既定で有効にすることができます。</p>
</td>
</tr>
<tr>
<td valign="top">
<p>Microsoft Secure Channel (Schannel)</p>
<p>Windows Server 2008 R2</p>
<p>Windows 7</p>
</td>
<td valign="top">
<p>Internet Explorer 11 がインストールされている場合、クライアントモードで既定で互換性があります。 Internet Explorer 11 がインストールされていない場合、または、Salesforce がこのタイプのシステムで実行されているサービスに接続する必要がある場合、レジストリ内でTLS 1.1 と TLS 1.2 を既定で有効にできます。</p>
<p>&#0160;</p>
</td>
</tr>
<tr>
<td valign="top">
<p>Microsoft WinHTTP と Webio</p>
<p>Windows Server 2012 R2 以降</p>
<p>Windows 8.1 以降</p>
</td>
<td valign="top">
<p>既定値で TLS 1.1 と TLS 1.2 と互換性があります。</p>
</td>
</tr>
<tr>
<td valign="top">
<p>Microsoft WinHTTP と Webio</p>
<p>Windows Server 2008 R2 SP1 と 2012</p>
<p>Windows 7 SP1</p>
</td>
<td valign="top">
<p><a href="https://support.microsoft.com/en-us/kb/3140245">KB3140245</a>&#0160;を適用することで、Webio は既定で互換性を持ちます。TLS 1.2 を有効化するにはレジストリ設定で WinHTTP を構成できます。</p>
</td>
</tr>
<tr>
<td valign="top">
<p>OpenSSL 1.0.1 and higher</p>
</td>
<td valign="top">
<p>既定値で TLS 1.1 以降と互換性があります。</p>
</td>
</tr>
<tr>
<td valign="top">
<p>Mozilla NSS 3.15.1 and higher</p>
</td>
<td valign="top">
<p>既定値で TLS 1.1 以降と互換性があります。</p>
</td>
</tr>
<tr>
<td valign="top">
<p>iOS 4.21 or higher</p>
</td>
<td valign="top">
<p>既定値で TLS 1.1 以降と互換性があります。</p>
</td>
</tr>
<tr>
<td valign="top">
<p>Android 5.0 (Lollipop) and higher</p>
</td>
<td valign="top">
<p>既定値で TLS 1.1 以降と互換性があります。</p>
</td>
</tr>
<tr>
<td valign="top">
<p>Android 4.4 (KitKat) to 4.4.4</p>
</td>
<td valign="top">
<p>TLS 1.1 以降と互換性があります。 ただし、Android 4.4.x 搭載のデバイスの中には、TLS 1.1 以降をサポートしていないものもあります。</p>
</td>
</tr>
<tr>
<td valign="top">
<p>Google Chrome 38 以降r</p>
</td>
<td valign="top">
<p>既定値で TLS 1.1 以降と互換性があります。</p>
</td>
</tr>
<tr>
<td valign="top">
<p>Firefox 27 以降</p>
</td>
<td valign="top">
<p>既定値で TLS 1.1 以降と互換性があります。</p>
</td>
</tr>
<tr>
<td valign="top">
<p>Microsoft Edge</p>
</td>
<td valign="top">
<p>既定値で TLS 1.1 以降と互換性があります。</p>
</td>
</tr>
<tr>
<td valign="top">
<p>Microsoft Internet Explorer Desktop および mobile IE version 11</p>
</td>
<td valign="top">
<p>既定値で TLS 1.1 以降と互換性があります。</p>
<p>「より強力なセキュリティが必要です」というエラーメッセージが表示された場合は、インターネットオプションの詳細設定で TLS 1.0 の TLS 1.0 設定をオフにする必要があります。&#0160;</p>
</td>
</tr>
<tr>
<td valign="top">
<p>Microsoft Internet Explorer Desktop IE versions 8、9、10</p>
</td>
<td valign="top">
<p>Windows 7 以降を使用している場合にのみ互換性がありますが、既定では使用できません。</p>
<p>Windows Vista、XP 以前のバージョンは互換性がなく、TLS 1.1 または TLS 1.2 をサポートするように構成することはできません。</p>
<p>&#0160;</p>
</td>
</tr>
<tr>
<td valign="top">
<p>OS X 10.9 (Mavericks) 以降用の Apple Safari Desktop Safari versions 7 以降</p>
</td>
<td valign="top">
<p>既定値で TLS 1.1 以降と互換性があります。</p>
</td>
</tr>
<tr>
<td valign="top">
<p>iOS 5 以降用の Apple Safari Mobile Safari versions 5 以降&#0160;</p>
</td>
<td valign="top">
<p>既定値で TLS 1.1 以降と互換性があります。</p>
</td>
</tr>
</tbody>
</table>
<p>ご不便をお掛けして大変申し訳ございません。</p>
<p>ご質問がございましたら、<a href="mailto:forge.help@autodesk.com">forge.help@autodesk.com</a> までメールでお問い合わせください。～ Forge チーム</p>
<p>クレジット：<br />&#0160; * 本記事は <a data-aura-rendered-by="48:68;a" href="https://help.salesforce.com/articleView?id=000221207&amp;type=1" rel="noopener noreferrer" target="_blank">knowledge article</a>&#0160;を参考にしています。</p>
<p>By Toshiaki Isezaki</p>
</div>
</div>
