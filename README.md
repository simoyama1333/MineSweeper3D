﻿#MineSweeper3D
解説など

#目的
三日でこんなもん作れますよというUnityの技術ポートフィリオ
それほど複雑でないので、Unity初学者の勉強にもなると思われる

#プレイ
[MineSweeper3D](https://developer.cloud.unity3d.com/share/b1rzZO0s1X/webgl/)
環境によってはプレイできないことがあります。
スマホだとまともに操作はできないがロードは完了、MacbookProだとプレイすらできなかった。

#マインスイーパーのアルゴリズム解説

　動きは2Dとほとんど変わらないので2Dベースで説明する。
　マインスイーパーはまず最初にフィールドを形成し（9x9など）それをカバーで覆い、機雷情報は最初のカバー開封後に設置する。というのも一手目で爆死する可能性があるのは、ただでさえ後半は運が絡むので、ゲーム性の観点から排除する必要がある。
　フィールドの初期化は0埋めが良い。3x3で例えるなら以下の状態。
000
000
000
 見た目としては開いていないので以下のようになる。xは未開封を意味する。
xxx
xxx
xxx
　機雷の設置は、設置した時にそのマスの全方向（2Dなら周囲8マス）に+1する。例は以下。機雷はM表記。
000
111
1M1
　マス目の開封は機雷の場合は爆死処理、0以外の場合はそのマス目の開封である。0の場合は全方向を開けつつ、上下左右に0探索を行う。探索したマス目が0の場合、同様の処理を行い、探索を続ける。このような0探索で、0を開けた時の連鎖的開封が実装されている。
 
 0探索の例　右上を開封した場合　全方向開封は探索後に行う
xxx
xxx
xxx
↓
xx0
xxx
xxx
↓
x10
xx0
xxx
↓
x10
x10
xx0
↓
...


　クリア条件は、右クリックで立てた旗の位置と爆弾の位置の合致である。
　マインスイーパーはこれだけのシンプルなアルゴリズムによって成り立っている。3Dの場合にもアルゴリズムが変わることはない。

　#実装解説
　私のプログラムの実装の大まかな解説である。

　MineField.csにフィールド情報のクラスを実装している。マス目はintではなく、C++のPairに似たようなクラスを作り、それで構成した。というのは、-1(機雷),0,1,2などの情報の他に、開封済み、旗立て済みなどの情報も入れる必要があるからだ。Pair.firstに前者の情報、Pair.secondに後者の情報を入れる。Tripleクラスがあるが、これには座標情報を入れ、MineFieldManagerの機雷の位置リストや旗位置情報リストで用いる。
    MineFieldManager.csが処理の本体で、フィールド情報の格納や開封（カバー消去）や探索処理、クリア判定を行っている。機雷の位置リストや旗位置情報リストを持ち、これらの比較によってクリア判定をしている。
　GameFieldCreatorはマスの覆いや文字列や機雷の3Dの実体を生成するクラスである。
　ObjInfoはカバーが持つ情報である。座標情報をカバーが有している。カバーの消去を呼ぶ、カバーに旗を立てるなどの処理を行う。
　MouseManagerではクリック時の処理（マウスクリックでレイを飛ばして当たったカバーを消去及び旗立て）、視点移動が行われる。
　

　難易度を選んでstage.sceneに遷移
　↓
　 MineFieldManagerのstartでフィールド情報の生成、GameFieldCreatorのstartでフィールドにカバーが生成される
　↓
　最初にどこかのカバーが開封されると、そのオブジェクトが有しているObjInfoからMineFieldManagerにアクセスし、機雷のセットが行われる。その後、オブジェクトの座標を渡して開封が行われる。二回目以降は機雷のセットは行われず、開封のみである。
　↓
　旗を立てた時にクリア条件判定が行われる。
　


　#ライセンス
　MITライセンスです。
　