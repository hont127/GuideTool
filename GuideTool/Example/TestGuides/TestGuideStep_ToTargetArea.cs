using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hont
{
    [CreateAssetMenu(fileName = "TestGuideStep_ToTargetArea Asset", menuName = "Guide Asset/TestGuideStep_ToTargetArea Asset", order = 207)]
    public class TestGuideStep_ToTargetArea : GuideListItem
    {
        bool mIsEnterTargetArea = false;


        public override void Initialization()
        {
            base.Initialization();

            mIsEnterTargetArea = false;
        }

        public override IEnumerator Execute()
        {
            yield return null;
        }

        public override bool Listening()
        {
            return mIsEnterTargetArea;
        }

        public void TriggeredEnterTargetArea()
        {
            mIsEnterTargetArea = true;
        }

        public void TriggeredExitTargetArea()
        {
            mIsEnterTargetArea = false;
        }
    }
}
