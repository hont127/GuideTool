using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hont
{
    public abstract class GuideListItem : GuideBase
    {
        public override bool AllowRepleat { get { return false; } }
    }
}
