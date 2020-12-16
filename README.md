# CCK Generator

[cluster](https://cluster.mu/)のCluster Creator Kitをコードから生成します。

## 内容説明

ゲームワールド制作テンプレートのCluster Creator Kit部分をコードから生成します。

コードから生成する事により、下記の利点を得ることが出来ます。

・各処理の見通しの改善

・テキスト比較での差分確認の容易性

・gitによるバージョン管理の容易化

・コメント付加による可読性の向上

・処理複製の容易化


## 使い方

「Assets/CCK_Generator/Eidtor/」直下の「HandGunCreator.cs」のように定義できます。

「PrototypePrefabs」内のプレファブから、実際に配置するプレファブを生成します。


## テンプレートの一覧

### シューター
`Assets/ClusterCreatorKitTemplate/Shooter/Scenes/Shooter.unity`

銃で的を撃って破壊できるテンプレートです。
銃にはハンドガン、マシンガン、チャージガンの3種類があります。

### プログレッション
`Assets/ClusterCreatorKitTemplate/Progression/Scenes/Progression.unity`

「鉱石を集めてレベルを上げて、貯まったお金で強いアビリティを買って、もっと鉱石を集める」ゲームのテンプレートです。
プレイヤーの経験値とレベル、鉱石の所持個数、プレイヤーアビリティの効果と購入・付け外しなどを実装しています。

## ライセンス

本プロジェクトは [Creative Commons Attribution 4.0 International](https://creativecommons.org/licenses/by/4.0/) で提供されています。
本プロジェクトの全部もしくは一部を使用して作成されたコンテンツを [cluster](https://cluster.mu/) で公開する場合は Cluster, Inc. のクレジット表示は不要です。

