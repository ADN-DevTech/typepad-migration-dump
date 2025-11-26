---
layout: "post"
title: "Data Exchange API によるデータ交換"
date: "2023-02-20 00:59:14"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2023/02/data-exchange-api.html "
typepad_basename: "data-exchange-api"
typepad_status: "Publish"
---

<p>頻繁におこなわれる関係者間の設計・デザイン コラボレーションでは、使用する製品やツールが持つ互換性のないファイル形式が障壁になることがあります。業種や製品、ツールを超えて、シームレスにコラボレーションすることが、これまで以上に重要になってきています。そこでオートデスクが登場させたのが Autodesk Platform Services（旧 Forge）のData Exchange API（ベータ）です。Data Exchange を使用すると、アプリや業界を問わず、データのロックを解除し、適切なタイミングで適切な関係者と適切な文脈でデータを共有する柔軟性を得ることができます。「データのロックを解除し」とは、ファイル内の情報を個々に取り出したり、書き込んだりしたり出来るよう、<a href="https://adndevblog.typepad.com/technology_perspective/2022/10/benefits-of-granular-data-on-data-exchange.html" rel="noopener" target="_blank">粒状データ</a>化することを意味します。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02b751962005200c-pi" style="display: inline;"><img alt="Unlock_data" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02b751962005200c image-full img-responsive" src="/assets/image_795797.jpg" title="Unlock_data" /></a></p>
<p>例えば、Revit で病院を設計している建築事務所が、Inventor で病院の階段の手すりを製造している製作所と、どのようにやり取りするかを考えてみましょう。アーキテクトは、モデルのジオメトリ（例えば、階段の寸法）やメタデータ（例えば、壁の材料特性）などの情報を製作者と共有する必要がありますが、知的財産を保護するために、病院モデル全体や特定の要素を共有したくない場合があります。ファブリケーターは、自分たちの仕事に関連するデータのみを、自分たちが使うアプリで受け取り、受け取ったデータを加工することなく、手すりの設計と製造により集中できるようにしたいはずです。</p>
<p style="text-align: center;"><iframe allowfullscreen="" frameborder="0" height="270" src="//www.youtube.com/embed/1gLFE5lo3Mw" width="480"></iframe></p>
<p>両社は Data Exchange（データ交換）を活用することで、日頃使用している特定のアプリ間でコラボレーションを強化することが出来ます。データ交換は、データのサブセットを格納する中立的な安全なコンテナとして機能し、エコシステムに接続されている任意の数のアプリと共有することが出来るようになります。この例では、アーキテクトは、ファブリケーターと共有したいデータのサブセットを、選択したアプリから指定することができます。ファブリケーターは、そのデータを使用するアプリ内で利用することが出来ます。アーキテクトがモデルを更新すると、そのモデルから派生したデータ交換は自動的に更新され、ファブリケーターは上流設計プロセスで行われた変更を取り込むことができます。</p>
<p>By Toshiaki Isezaki</p>
