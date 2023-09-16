# 2023_TYRANNO_CUP

### ゲームについて
#### タイトル
劇的！カタヤブーリ先生の脳トレパズル 

#### 概要
条件追加パズルゲーム

### 企画書

#### デモ動画

### フォルダ管理について


基本Assetの中しかいじらないです。  

```
Assets
├── DownLoadAssets
│   ├── DOTween
│   ├── Photon
│   └── UniRx
└── MyAssets
    ├── Commmons
    │   ├── Animations
    │   ├── アニメーション入れるとこ
    │   ├── Fonts
    │   ├── フォント入れるとこ
    │   ├── Musics
    │   │   ├── SEs
    │   │   ├── 効果音を入れるとこ
    │   │   ├── BGMs
    │   │   └── 音楽入れるところ
    │   ├── 曲入れるとこ
    │   ├── Prefabs
    │   ├── プレファブ入れるとこ
    │   ├── Scripts
    │   ├── コード入れるとこ
    │   ├── Sprites
    │   ├── 画像入れるとこ
    │   └── 同階層の他のフォルダの中身も大体こんな感じです
    ├── Develops
    │   └── 個人の開発シーンの置き場
    ├── Field
    ├── Result
    └── Title

```
DownLoadAssetsの中には開発には使用するけど、入れてるだけで使用できるもの（DOTWeenだったり）を入れておくフォルダです。  
あとは大量にファイルがあるアセットとかもDownLoadAssetsに入れてください。
必要になったらPrefab、ScriptをコピーしてMyAssetに移動させて使用すること。

### クラス図

### コーディングルール
- 命名規則
    - bool型には頭には動詞、及び助動詞をつける（疑問文ぽくする）
    - メソッド名は動詞始まりにする（だいたいどんな動きをするメソッドかの説明）
    - ローカル変数、引数はキャメルケース、その他は基本パスカルケース
    - クラス名が動詞始まりはそんなに良くないので、基本的には名詞で（一つのクラスに複数の処理を持たせたい場合はかなりアバウトな命名にするといいかも）
>参考  
>[【Unity】変数やメソッドのネーミングについてかるくまとめてみた](https://www.hanachiru-blog.com/entry/2019/03/28/230933)  
>[Unityの命名規則とエディター設定](https://am1tanaka.hatenablog.com/entry/2019/12/06/101055)  
>[C# のコーディング規則](https://learn.microsoft.com/ja-jp/dotnet/csharp/fundamentals/coding-style/coding-conventions)
  
- 変数名は長すぎてもダメだし短すぎて伝わらないのもダメ、伝わらないくらいなら長い方がいいくらいの心持ちでお願いします（伝わらなければ死だと思ってください）
>参考  
>[変数名単語帳](https://unitylab.wiki.fc2.com/wiki/%E5%A4%89%E6%95%B0%E5%90%8D%E5%8D%98%E8%AA%9E%E5%B8%B3)  
  
- マジックナンバー(この数字がどういった数字なのかわからない)をなるべく避ける（プログラマは魔法使いではないので自分の書いた数字だったらわかると思わないようにしてください、そもそも他の人がわかりません）
>参考  
>[マジックナンバー :「分かりそう」で「分からない」でも「分かった」気になれるIT用語辞典](https://wa3.i-3-i.info/word12868.html)
  
- 変数、メソッドは基本的にprivate。publicはクラス外で使いそうなもののみ付けること。（privateってつけることで他クラスから参照されてないことが保証されるのでそのクラスの処理を見るだけですむ）
>参考  
>[【Unity入門】publicやprivateなどアクセス修飾子の説明](https://mogi0506.com/unity-accessmodifier/)
  
- Inspectorで変更を加えたいprivate変数には [SerializeField] を使う。 
>参考  
>[【初心者Unity】［SerializeField］ってなに？](https://tech.pjin.jp/blog/2021/12/23/unity-serializefield)
  
- publicな値にはGetter、Setterをつけましょう。外部からSetしていいかダメかをしっかり明示するのが大事です。
>参考
>[【Unityメモ】get setの使い方](https://note.com/08_14/n/n0fe88efe0159)
  
- コミットメッセージも統一しましょう「何をしたかをざっくり熟語で : こちらにより具体的にかく」日本語でお願いします。（統一しとかないと読み方にメモリ()を使わなきゃいけないので）
>参考  
>[僕が考える最強のコミットメッセージの書き方](https://qiita.com/konatsu_p/items/dfe199ebe3a7d2010b3e)
  
- どういった処理をするのか説明する summuryコメントアウト を付ける（関数、クラスの上つけるようにしましょう）
>参考  
>[＜Summary＞コメントの有用性](https://qiita.com/Disk_MJM/items/c24f51b894fdcf2170d6)

### git操作

>参考  
>[Gitコマンド一覧](https://qiita.com/fukumone/items/73e1a9a62c5e4454263b)

#### プルリク作るまでの流れ 
- 上から順に実行すれば大体オッケー
```
$ git add .
```
- 現在の変更をaddする（ステージングする）
```
$ git branch
```
- 作業前に自分の今いるブランチを確認（mainは絶対だめ）
```
$ git status
``` 
- pushするファイルを確認
```
$ git commit -m "コミットメッセージ"
```
- コミットメッセージを書く
```
$ git push origin ブランチ名
```
- そのブランチにプッシュする
```
Githubの方に行って`Pull requests`に行く、または黄色い表示が出てくるのでそれの緑のボタンを押す。
```
```
Writeに作業内容等を書いて`Create Pull request
```
