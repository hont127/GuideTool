using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hont
{
    public class GuideModuleExample : MonoBehaviour
    {
        public static bool IsTakeFlashlight;
        public static bool IsTakeClottonSwab;

        public static int ExtractBloodScore = 0;
        public static int ExtractFootprintScore = 0;

        public GameObject player;


        void OnGUI()
        {
            if (GUILayout.Button("拿手电筒"))
                IsTakeFlashlight = true;

            if (GUILayout.Button("收回手电筒"))
                IsTakeFlashlight = false;

            if (GUILayout.Button("拿棉花棒"))
                IsTakeClottonSwab = true;

            if (GUILayout.Button("收回棉花棒"))
                IsTakeClottonSwab = false;

            if (GUILayout.Button("模拟玩家进入目标区域"))
                player.transform.position = Vector3.zero;

            if (GUILayout.Button("获得提取血迹10分"))
                ExtractBloodScore = 10;

            if (GUILayout.Button("提取血迹分数重置"))
                ExtractBloodScore = 0;

            if (GUILayout.Button("获得提取脚印10分"))
                ExtractFootprintScore = 10;

            if (GUILayout.Button("提取脚印分数重置"))
                ExtractFootprintScore = 0;
        }
    }
}
