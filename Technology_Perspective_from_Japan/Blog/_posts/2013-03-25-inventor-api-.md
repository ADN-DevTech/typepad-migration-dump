---
layout: "post"
title: "Inventor API 入門 パーツファイル内の マルチボディの シングルサーフェスボディ化"
date: "2013-03-25 02:30:00"
author: "Shigelazu Saitou"
categories:
  - "API カスタマイズ"
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2013/03/inventor-api-%E5%85%A5%E9%96%80-%E3%83%91%E3%83%BC%E3%83%84%E3%83%95%E3%82%A1%E3%82%A4%E3%83%AB%E5%86%85%E3%81%AE-%E3%83%9E%E3%83%AB%E3%83%81%E3%83%9C%E3%83%87%E3%82%A3%E3%81%AE-%E3%82%B7%E3%83%B3%E3%82%B0%E3%83%AB%E3%82%B5%E3%83%BC%E3%83%95%E3%82%A7%E3%82%B9%E3%83%9C%E3%83%87%E3%82%A3%E5%8C%96.html "
typepad_basename: "inventor-api-入門-パーツファイル内の-マルチボディの-シングルサーフェスボディ化"
typepad_status: "Publish"
---

<p>Inventor製品では、既に Inventor2010製品よりパーツファイルの設計機能が拡張され、アセンブリファイルを作成しコンポーネントとして配置する手順を取らなくても、パーツファイル内で複数の機能別に作成されたボディを組み合わせて機能を持たせる<strong>「マルチボディによるパーツファイル設計機能」</strong>が備わっており、パーツファイル内に複数で構成されるマルチボディ群を 一つのサーフェスボディとして設計＆認識させる事で、設計機能を保ったまま<strong>「単一のサーフェスボディとしてのパーツファイル」</strong>として作成する事ができます。</p>

<p><strong>マルチボディ化の利点</strong>は、パーツファイル内に多機能の設計を維持したままカプセル化する状態の為に、実際の設計時における不用意なアセンブリファイルを作成する必要が無く、全体の<strong>アセンブリのコンポーネント構成点数（コンポーネントファイル数）が減少</strong>する事により、パーツファイルをはじめとして構成される<strong>全体の設計管理が簡単</strong>になり、コンポーネントとして組み込んだ<strong>アセンブリファイルを開く際のロード時間が短縮</strong>される他、APIにより<strong>単一のサーフェスボディとしてのハンドリングが可能</strong>となります。</p>

<p>今回、APIを通してパーツファイル内に複数の「押し出し」を作成し、マルチボディとして管理構成されるボディ群を「結合」処理し、単一のサーフェスボディとする処理をご紹介します。<br />
<iframe width="500" height="281" src="http://www.youtube.com/embed/d1oiyYpbCco?feature=oembed" frameborder="0" allowfullscreen></iframe><br />
(画質は"HD"でご覧いただけます)<br />
</p>
