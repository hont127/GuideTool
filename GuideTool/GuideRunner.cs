using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hont
{
    public sealed class GuideRunner : MonoBehaviour
    {
        public List<GuideBase> guideList;


        IEnumerator Start()
        {
            for (int i = guideList.Count - 1; i >= 0; i--)
            {
                var guide = guideList[i];

                guide.Initialization();
            }

            while (true)
            {
                for (int i = guideList.Count - 1; i >= 0; i--)
                {
                    var guide = guideList[i];

                    if (guide.Listening())
                    {
                        StartCoroutine(ExecuteGuide(i));
                    }
                }

                yield return null;
            }
        }

        IEnumerator ExecuteGuide(int targetGuideIndex)
        {
            var guide = guideList[targetGuideIndex];

            guideList.RemoveAt(targetGuideIndex);

            yield return guide.Execute();

            if (guide.AllowRepleat)
                guideList.Add(guide);
        }
    }
}
