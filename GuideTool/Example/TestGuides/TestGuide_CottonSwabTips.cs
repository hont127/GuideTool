using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hont
{
    [CreateAssetMenu(fileName = "TestGuide_CottonSwabTips Asset", menuName = "Guide Asset/TestGuide_CottonSwabTips Asset", order = 207)]
    public class TestGuide_CottonSwabTips : GuideBase
    {
        public string tipContent = "棉花棒可提取已经照亮的指纹或脚印";
        bool mIsEnterGuide;

        public override bool AllowRepleat { get { return true; } }


        public override IEnumerator Execute()
        {
            mIsEnterGuide = true;
            Debug.Log(tipContent);

            yield return null;
        }

        public override bool Listening()
        {
            if (mIsEnterGuide && !GuideModuleExample.IsTakeClottonSwab)
                mIsEnterGuide = false;
            //上一次的状态重置。

            return !mIsEnterGuide && GuideModuleExample.IsTakeClottonSwab;
        }
    }
}
