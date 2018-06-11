using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hont
{
    public abstract class GuideBase : ScriptableObject
    {
        /// <summary>
        /// 是否允许循环重复触发该引导。
        /// </summary>
        public abstract bool AllowRepleat { get; }
        /// <summary>
        /// 当进入到这一步时，该函数循环执行，判断是否可执行接下来的步骤，直到返回True。
        /// </summary>
        /// <returns></returns>
        public abstract bool Listening();
        /// <summary>
        /// 当引导被脚本加载时所有步骤统一触发。
        /// </summary>
        public virtual void Initialization() { }
        /// <summary>
        /// 执行当前引导步骤。
        /// </summary>
        public abstract IEnumerator Execute();
    }
}
