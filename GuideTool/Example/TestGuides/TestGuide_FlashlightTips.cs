using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hont
{
    [CreateAssetMenu(fileName = "TestGuide_FlashlightTips Asset", menuName = "Guide Asset/TestGuide_FlashlightTips Asset", order = 207)]
    public class TestGuide_FlashlightTips : GuideBase
    {
        public string tipContent = "手电筒可照亮指纹或脚印，并用其他工具进行提取";
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
            if (mIsEnterGuide && !GuideModuleExample.IsTakeFlashlight)
                mIsEnterGuide = false;
            //上一次的状态重置。

            return !mIsEnterGuide && GuideModuleExample.IsTakeFlashlight;
        }
    }
}
