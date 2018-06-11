using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hont
{
    [CreateAssetMenu(fileName = "TestGuideStep_ExtratorBlood Asset", menuName = "Guide Asset/TestGuideStep_ExtratorBlood Asset", order = 207)]
    public class TestGuideStep_ExtratorBloodTips : GuideListItem
    {
        [Tooltip("目标分数，当血迹为目标值则弹出提示，否则直接跳过")]
        public int bloodScore = 0;
        [Tooltip("弹出提示的前延迟时间")]
        public float delayTime = 1f;
        public string tipsText = "请开始提取血迹";


        public override IEnumerator Execute()
        {
            yield return new WaitForSeconds(delayTime);

            if (GuideModuleExample.ExtractBloodScore == bloodScore)
                Debug.Log(tipsText);

            yield return null;
        }

        public override bool Listening()
        {
            return true;
        }
    }
}
