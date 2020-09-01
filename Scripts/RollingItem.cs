using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollingItem : MonoBehaviour
{
    public UIPanel panel;
    public DigitParents parents;

    private int itemHeight;
    private int itemCount;

    /// <summary>
    /// Child Transform Index
    /// </summary>
    private int curIdx = 0;
    private int preIdx = 0;

    /// <summary>
    /// panel clip height area
    /// </summary>
    private float clipHeight;

    private int loop;
    private float smoothTime;

    private float dest;

    /// <summary>
    /// The smaller the value, the slower, and the larger the value, the faster
    /// </summary>
    private float destRatio;

    /// <summary>
    /// item is in panel clip area 
    /// </summary>
    private System.Action<int> clipAreaAction;
    private bool isClipAreaAction = false;

    /// <summary>
    /// Mathf.SmoothDamp.. Temp Ref Value
    /// </summary>
    private float velocity;

    public void Start()
    {
        if (parents != null)
        {
            itemHeight = parents.itemHeight;
            itemCount = parents.transform.childCount;
        }

        if (panel != null)
            clipHeight = (panel.finalClipRegion.w / itemHeight + 1) * itemHeight;
    }

    public void SetProperty(int loop, float smoothTime, float destRatio)
    {
        this.loop = loop;
        this.smoothTime = smoothTime;
        this.destRatio = destRatio;
    }

    public void ResetAfterAction()
    {
        if (isClipAreaAction)
            SwitchContents(preIdx);
        isClipAreaAction = false;
    }

    public void RollingStart(int idx)
    {
        ResetAfterAction();
        curIdx = idx;
        SetDest(loop);
        StartCoroutine(RollingCoroutine());
    }

    private IEnumerator RollingCoroutine()
    {
        var offset = dest - panel.transform.localPosition.y;
        var destWeight = dest + offset * destRatio;

        while (!IsDestination())
        {
            if (IsClipArea())
            {
                if (clipAreaAction != null)
                    clipAreaAction(curIdx);
            }
            SetPanelPostion(GetSmoothDamp(destWeight));
            yield return new WaitForFixedUpdate();
        }

        preIdx = curIdx;
        SwitchPositionFromTarget(curIdx);
        yield return null;
    }

    private void SwitchPositionFromTarget(int idx)
    {
        dest = idx * itemHeight;
        for (int i = 0; i < itemCount; ++i)
        {
            var child = GetChild(i);
            child.localPosition = new Vector3(child.localPosition.x, -(i * itemHeight), child.localPosition.z);
        }
        panel.transform.localPosition = new Vector3(0, dest, 0);
        panel.clipOffset = new Vector2(panel.clipOffset.x, -panel.transform.localPosition.y);
    }

    private void SetPanelPostion(float y)
    {
        panel.transform.localPosition = new Vector3(0, y, 0);
        panel.clipOffset = new Vector2(panel.clipOffset.x, -panel.transform.localPosition.y);
    }

    private float GetSmoothDamp(float destWeight)
    {
        return Mathf.SmoothDamp(panel.transform.localPosition.y, destWeight, ref velocity, smoothTime);
    }

    public void SetTargetImmediately(int idx)
    {
        ResetAfterAction();
        SwitchPositionFromTarget(idx);
        curIdx = preIdx = idx;
    }

    private bool IsClipArea()
    {
        if (panel.transform.localPosition.y > dest - clipHeight)
            return true;
        return false;
    }

    public bool IsFinish()
    {
        return curIdx == preIdx;
    }

    private bool IsDestination()
    {
        if (panel.transform.localPosition.y < dest)
            return false;
        return true;
    }

    private void SetDest(int loop)
    {
        dest = panel.transform.localPosition.y + (itemHeight * itemCount * loop) + GetItemOffset();
    }

    private int GetItemOffset()
    {
        var temp = curIdx - preIdx;
        if (temp < 0)
            temp = itemCount + temp;
        return temp * itemHeight;
    }

    private Transform GetChild(int target)
    {
        return parents.transform.GetChild(target);
    }

    public void SetAction()
    {
        clipAreaAction = (int idx) =>
        {
            SwitchContents(idx);
            isClipAreaAction = true;
            clipAreaAction = null;
        };
    }

    private void SwitchContents(int idx)
    {
        var item = GetChild(idx);
        var toggle = item.GetComponent<RollingItemActionWidget>();
        if (toggle != null)
            toggle.Toggle();
    }
}
