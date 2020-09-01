using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Rolling : MonoBehaviour
{
    public Transform parentTf;
    public List<RollingItem> rollingList;

    protected bool reverse = true;
    protected int roundCount = 7;

    /// <summary>
    /// Interval at each item
    /// </summary>
    protected float interval = 0.05f;

    /// <summary>
    /// Approximate time to arrive.
    /// </summary>
    protected float smoothTime = 0.5f;

    /// <summary>
    /// Deceleration setting value when stopping, the smaller the slower
    /// </summary>
    protected float destRatio = 0.1f;

    public virtual void Start()
    {
        if (parentTf != null)
        {
            var items = parentTf.GetComponentsInChildren<RollingItem>();
            if (items != null)
            {
                rollingList = new List<RollingItem>(items);
                if (reverse)
                    rollingList.Reverse();
            }
        }
        SetItemProperty();
    }

    public virtual void SetItemProperty()
    {
        SetItemProperty(roundCount, interval, smoothTime, destRatio);
    }

    public void SetItemProperty(int roundCount, float itemInterval, float smoothTime, float destRatio)
    {
        if (rollingList != null)
        {
            for (int i = 0; i < rollingList.Count; ++i)
                rollingList[i].SetProperty(roundCount, smoothTime + itemInterval * i, destRatio);
        }
    }

    public bool IsRollingFinish()
    {
        for (int i = 0; i < rollingList.Count; ++i)
        {
            if (!rollingList[i].IsFinish())
                return false;
        }
        return true;
    }

    public virtual void RollingStart(List<int> numList)
    {
        if (rollingList.Count >= numList.Count)
        {
            for (int i = 0; i < numList.Count; ++i)
            {
                rollingList[i].RollingStart(numList[i]);
            }
        }
    }

    public virtual void RollingStart<T>(T num, bool isAction)
    {
        var numList = GetNumList(num);
        if (rollingList.Count >= numList.Count)
        {
            for (int i = 0; i < numList.Count; ++i)
            {
                if (isAction)
                    rollingList[i].SetAction();
                rollingList[i].RollingStart(numList[i]);
            }
        }
    }

    public virtual void RollingStart<T>(T num, bool isAction, bool fillEmptyZero)
    {
        var numList = GetNumList(num, fillEmptyZero);
        if (rollingList.Count >= numList.Count)
        {
            for (int i = 0; i < numList.Count; ++i)
            {
                if (isAction)
                    rollingList[i].SetAction();
                rollingList[i].RollingStart(numList[i]);
            }
        }
    }

    public void ImmediatelyTarget(int idx)
    {
        for (int i = 0; i < rollingList.Count; ++i)
        {
            rollingList[i].SetTargetImmediately(idx);
        }
    }

    public List<int> GetNumList<T>(T num, bool fillEmptyZero = true)
    {
        var str = num.ToString();
        var count = 0;
        var numList = new List<int>();

        if (fillEmptyZero)
        {
            count = rollingList.Count - str.Length;
            for (int i = 0; i < count; ++i)
                numList.Add(0);
        }

        count = str.Length;
        var temp = 0;
        for (int i = 0; i < count; ++i)
        {
            if (int.TryParse(str[i].ToString(), out temp))
                numList.Add(temp);
        }

        if (reverse)
            numList.Reverse();
        return numList;
    }
}
