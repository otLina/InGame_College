using UnityEngine;

namespace FlyweightExample
{
    [CreateAssetMenu(fileName = "EnemyParam",
                     menuName = "ScriptableObjects/EnemyParam",
                     order = 1)]
    public class EnemyParam : ScriptableObject
    {
        public string Name;
        public float Speed;
        public float Angle;
        public Color BodyColor;
        public int MaxHp;

#if DUMMY
        // ここでは説明用に処理を簡略しています。
        // ScriptableObjectは読み取り専用なので
        // public readonlyとすべきですが、
        // Unity Editorから値の修正ができたり、
        // スクリプトで設定できると便利なので
        // 各パラメータ毎に以下の様な処理を設定します。
        [SerializeField]
        private string _name = "";
        public string Name
        {
            get { return _name; }
#if UNITY_EDITOR
            set { _name = value; }
#endif
        }
#endif
    }
}
