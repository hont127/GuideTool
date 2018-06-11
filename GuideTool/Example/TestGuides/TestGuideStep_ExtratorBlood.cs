using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hont
{
    [CreateAssetMenu(fileName = "TestGuideStep_ExtratorBlood Asset", menuName = "Guide Asset/TestGuideStep_ExtratorBlood Asset", order = 207)]
    public class TestGuideStep_ExtratorBlood : GuideListItem
    {
        public int bloodScore = 10;
        public string tipsText = "提取血迹完成";


        public override IEnumerator Execute()
        {
            Debug.Log(tipsText);
            yield return null;
        }

        public override bool Listening()
        {
            return GuideModuleExample.ExtractBloodScore >= bloodScore;
        }
    }
}
