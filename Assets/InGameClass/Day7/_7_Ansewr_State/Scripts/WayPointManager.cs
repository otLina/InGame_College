using System.Collections.Generic;
using UnityEngine;
using System.Linq; // 'OrderBy'によるソートで使用

namespace StateAnswer
{
    public sealed class WayPointManager
    {
        /// <summary>
        /// CheckPointManagerクラスのインスタンス'instance'を作成します。
        /// </summary>
        private static WayPointManager instance;

        public void ResetInstance()
        {
            instance = null;
        }

        // チェックポイントのリスト
        private List<GameObject> waypoints = new List<GameObject>();

        // チェックポイントのリストのpublicプロパティ
        public List<GameObject> Waypoints { get { return waypoints; } }

        /// <summary>
        /// シングルトンが存在しない場合は作成し、
        /// "WayPoint"タグが設定されているオブジェクトをリストに追加
        /// </summary>
        public static WayPointManager Singleton
        {
            get
            {
                if (instance == null)
                {
                    instance = new WayPointManager();
                    instance.waypoints.AddRange(
                        GameObject.FindGameObjectsWithTag("WayPoint"));

                    instance.waypoints = instance.waypoints.
                        OrderBy(waypoint => waypoint.name).ToList();
                }
                return instance;
            }
        }
    }
}