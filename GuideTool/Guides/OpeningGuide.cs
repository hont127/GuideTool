using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hont
{
    [CreateAssetMenu(fileName = "Opening Guide Asset", menuName = "Guide Asset/Opening Guide Asset", order = 207)]
    public class OpeningGuide : GuideBase
    {
        public override bool AllowRepleat { get { return false; } }


        public override IEnumerator Execute()
        {
            Debug.Log("引导系统初始化中...");
            yield return new WaitForSeconds(1f);
            Debug.Log("引导系统开始运行!");
        }

        public override bool Listening()
        {
            return true;
        }
    }
}
