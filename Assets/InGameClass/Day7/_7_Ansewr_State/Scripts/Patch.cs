using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StateAnswer
{
    public class Patch : MonoBehaviour
    {
        private void OnDisable()
        {
            WayPointManager.Singleton.ResetInstance();
        }
    }
}
