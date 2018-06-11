using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hont
{
    [CreateAssetMenu(fileName = "TestGuideStep_ExtratorFootprint Asset", menuName = "Guide Asset/TestGuideStep_ExtratorFootprint Asset", order = 207)]
    public class TestGuideStep_ExtratorFootprint : GuideListItem
    {
        public int footprintScore = 10;
        public string tipsText = "提取脚印完成";


        public override IEnumerator Execute()
        {
            Debug.Log(tipsText);
            yield return null;
        }

        public override bool Listening()
        {
            return GuideModuleExample.ExtractFootprintScore >= footprintScore;
        }
    }
}
