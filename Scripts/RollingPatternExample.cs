using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollingPatternExample : Rolling
{
    public override void Start()
    {
        if (parentTf != null)
        {
            var items = parentTf.GetComponentsInChildren<RollingItem>();
            if (items != null)
            {
                rollingList = new List<RollingItem>();
                for (int i = 0; i < items.Length / 2; ++i)
                {
                    rollingList.Add(items[i]);
            
                        rollingList.Add(items[items.Length - 1 - i]);
                }   
                if (reverse)
                    rollingList.Reverse();
            }
        }
    }
}
