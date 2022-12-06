using UnityEngine;
using DG.Tweening;

namespace AttackAction
{
    public class GameManager : MonoBehaviour
    {
        /// <summary>ゲームの残り時間</summary>
        [SerializeField]
        private float _remainTime = 60f;

        [SerializeField]
        private Player _player;

        [SerializeField]
        private ReadyView _readyView;

        [SerializeField]
        private TimeView _timeView;

        [SerializeField]
        private FinishView _finishView;

        [SerializeField]
        private CanvasGroup _gameOverView;

        /// <summary>ゲーム中か否か</summary>
        private bool _isInGame;

        private Enemy[] _enemies;

        private void Awake()
        {
            // プレイヤーコンポーネントの更新を止めて、操作不能にする
            _player.enabled = false;

            // シーン内にいる全ての敵を取得
            // TIPS: FindObjectsOfTypeは重い処理なので、本来なら敵を管理する配列や管理クラスがあることが望ましい
            _enemies = FindObjectsOfType<Enemy>();

            // ゲーム開始前演出を実行
            _readyView.PlayReady(0.5f, () =>
            {
                _readyView.PlayStart();
                BeginGame();
            });
        }

        /// <summary>
        /// ゲーム開始時の処理
        /// </summary>
        private void BeginGame()
        {
            // ゲーム中フラグをtrueにして時間カウントを開始
            _isInGame = true;
            // プレイヤーを操作可能にする
            _player.enabled = true;
        }

        /// <summary>
        /// 更新処理
        /// </summary>
        private void Update()
        {
            if (!_isInGame)
            {
                // ゲーム中フラグがfalseなら何もしない
                return;
            }

            if (IsFinish())
            {
                // ゲームクリア判定がtrueならゲーム終了
                EndGame();
                return;
            }

            // 時間をカウントダウンしてUIを更新
            _remainTime = Mathf.Max(_remainTime - Time.deltaTime, 0f);
            _timeView.SetTime(_remainTime);

            if(_remainTime <= 0f)
            {
                // 残り時間が0になったらゲームオーバー処理
                GameOver();
            }
        }

        /// <summary>
        /// ゲームクリアしているか否かを取得
        /// </summary>
        private bool IsFinish()
        {
            foreach(var enemy in _enemies)
            {
                if (!enemy.IsDead)
                {
                    // いずれかの敵がまだ生きていたらゲームクリアではないと判定して、即座にfalseで処理を抜ける
                    return false;
                }
            }
            // 全ての敵を倒していたらtrue
            return true;
        }

        /// <summary>
        /// ゲーム終了処理
        /// </summary>
        private void EndGame()
        {
            // ゲーム中フラグをtrueにして時間カウントを停止
            _isInGame = false;
            // プレイヤーを操作不能にする
            _player.enabled = false;

            // ゲームクリアUIを表示
            // 引数にクリア時の時間を渡す
            _finishView.Play(_timeView.GetTime());
        }

        private void GameOver()
        {
            // ゲーム中フラグをtrueにして時間カウントを停止
            _isInGame = false;
            // プレイヤーを操作不能にする
            _player.enabled = false;

            // ゲームオーバーUIをフェードイン
            _gameOverView.DOFade(1f, 0.2f);
        }
    }
}
