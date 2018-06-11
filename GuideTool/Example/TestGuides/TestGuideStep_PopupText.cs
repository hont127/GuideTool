using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hont
{
    [CreateAssetMenu(fileName = "TestGuideStep_PopupText Asset", menuName = "Guide Asset/TestGuideStep_PopupText Asset", order = 207)]
    public class TestGuideStep_PopupText : GuideListItem
    {
        [Tooltip("弹出提示的前延迟时间")]
        public float delayTime = 1f;
        public string tipsText = "恭喜所有得分项均已完成!";


        public override IEnumerator Execute()
        {
            yield return new WaitForSeconds(delayTime);

            Debug.Log(tipsText);

            yield return null;
        }

        public override bool Listening()
        {
            return true;
        }
    }
}
