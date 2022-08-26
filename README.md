# mft2022_c
An Unity project for MFT2022 TeamC.

## 使い方
1. カメラの画角をできるだけマットの真上になるように設置し、画角をマットが画面いっぱいに表示されるように調整する。
2. Asset/Scripts/MarkerDetector.csの「selectCamera」変数を変更し、実行画面に使用しているカメラの映像が表示されるようにする。
3. Asset/Scripts/Main.csの「cubeNum」を接続するtoioキューブの台数と同じにする。
4. toioキューブをペアリングモードにする。
5. プログラムを実行する。

## 設定用パラメータ一覧
### MarkerDetector.cs
- int selectCamera (8行目)
  - マーカー検出に使用するカメラを選択するためのパラメータ。
  - カメラを接続するたびに変わるため毎回設定が必要。

### Main.cs
- int cubeNum (9行目)
  - 接続するtoioキューブの台数。
- int move (119行目)
  - 待機中に移動するかどうかの決定を行うための乱数。
  - Random.Range()の上限を変更することで移動する確率を変更可能。
