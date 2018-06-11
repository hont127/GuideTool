using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hont
{
    [CreateAssetMenu(fileName = "Guide List Asset", menuName = "Guide Asset/Guide List Asset", order = 207)]
    public sealed class GuideList : GuideBase
    {
        [Serializable]
        public struct GuideStep
        {
            public GuideBase guide;
            [Multiline]
            public string description;
        }

        public List<GuideStep> guideList = new List<GuideStep>();

        public override bool AllowRepleat { get { return false; } }


        public override void Initialization()
        {
            base.Initialization();

            for (int i = 0, iMax = guideList.Count; i < iMax; i++)
            {
                var guideStep = guideList[i];

                guideStep.guide.Initialization();
            }
        }

        public override bool Listening()
        {
            if (guideList.Count > 0) return guideList[0].guide.Listening();

            return false;
        }

        public override IEnumerator Execute()
        {
            for (int i = 0, iMax = guideList.Count; i < iMax;)
            {
                var guideStep = guideList[i];

                if (guideStep.guide.Listening())
                {
                    yield return guideStep.guide.Execute();

                    i++;
                }
                else
                {
                    yield return null;
                }
            }
        }
    }
}
