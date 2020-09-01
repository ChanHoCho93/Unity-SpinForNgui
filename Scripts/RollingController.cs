using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollingController : MonoBehaviour
{
    public enum RollingType
    {
        ResetRolling, ResetImmediately,
        IncrementOne, IncrementTen,
        AllFastOrder, AllSlowOrder, AllSameTime,
        SwitchImageOrder, SwitchImageSame,
        Pattern1

    }

    public Rolling rolling;
    public RollingPatternExample rollingPattern;
    public long curNum;

    public void Update()
    {
        if (rolling.IsRollingFinish())
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
                RollingCommand(RollingType.ResetRolling);
            if (Input.GetKeyDown(KeyCode.Alpha2))
                RollingCommand(RollingType.ResetImmediately);
            if (Input.GetKeyDown(KeyCode.Alpha3))
                RollingCommand(RollingType.IncrementOne);
            if (Input.GetKeyDown(KeyCode.Alpha4))
                RollingCommand(RollingType.IncrementTen);
            if (Input.GetKeyDown(KeyCode.Alpha5))
                RollingCommand(RollingType.AllFastOrder);
            if (Input.GetKeyDown(KeyCode.Alpha6))
                RollingCommand(RollingType.AllSlowOrder);
            if (Input.GetKeyDown(KeyCode.Alpha7))
                RollingCommand(RollingType.AllSameTime);
            if (Input.GetKeyDown(KeyCode.Alpha8))
                RollingCommand(RollingType.SwitchImageOrder);
            if (Input.GetKeyDown(KeyCode.Alpha9))
                RollingCommand(RollingType.SwitchImageSame);
            if (Input.GetKeyDown(KeyCode.Q))
                RollingCommand(RollingType.Pattern1);
        }
    }

    public void RollingCommand(RollingType type)
    {
        switch (type)
        {
            case RollingType.ResetRolling:
                curNum = 0;
                rolling.SetItemProperty(1, 0.1f, 0.1f, 0.1f);
                RollingStart(0, false, true);
                break;
            case RollingType.ResetImmediately:
                curNum = 0;
                rolling.ImmediatelyTarget(0);
                break;
            //
            case RollingType.IncrementOne:
                curNum++;
                rolling.SetItemProperty(0, 0.1f, 0.1f, 0.1f);
                RollingStart(curNum, false, false);
                break;
            case RollingType.IncrementTen:
                curNum += 10;
                rolling.SetItemProperty(0, 0.1f, 0.1f, 0.1f);
                RollingStart(curNum, false, false);
                break;
            //

            case RollingType.AllFastOrder:
                curNum = (long)Random.Range(0, 9999999999);
                rolling.SetItemProperty(7, 0.1f, 0.1f, 0.1f);
                RollingStart(curNum, false, true);
                break;
            case RollingType.AllSlowOrder:
                curNum = (long)Random.Range(0, 9999999999);
                rolling.SetItemProperty(7, 0.1f, 0.5f, 0.01f);
                RollingStart(curNum, false, true);
                break;
            //
            case RollingType.AllSameTime:
                curNum = (long)Random.Range(0, 9999999999);
                rolling.SetItemProperty(7, 0f, 0.1f, 0.1f);
                RollingStart(curNum, false, true);
                break;
            //
            case RollingType.SwitchImageOrder:
                curNum = (long)Random.Range(0, 9999999999);
                rolling.SetItemProperty(7, 0.2f, 0.1f, 0.1f);
                RollingStart(curNum, true, true);
                break;
            case RollingType.SwitchImageSame:
                curNum = (long)Random.Range(0, 9999999999);
                rolling.SetItemProperty(7, 0f, 0.5f, 0.01f);
                RollingStart(curNum, true, true);
                break;
            //
            case RollingType.Pattern1:
                curNum = (long)Random.Range(0, 9999999999);
                rollingPattern.SetItemProperty(7, 0.1f, 0.5f, 0.01f);
                RollingStart(curNum, false, true);
                break;
            default:
                break;
        }
    }

    public void ResetRolling()
    {
        if (!rolling.IsRollingFinish()) return;

        curNum = 0;
        rolling.SetItemProperty(1, 0.1f, 0.1f, 0.1f);
        RollingStart(0, false, true);
    }

    public void ResetImmediately()
    {
        if (!rolling.IsRollingFinish()) return;

        curNum = 0;
        rolling.ImmediatelyTarget(0);
    }

    public void IncrementOne()
    {
        if (!rolling.IsRollingFinish()) return;

        curNum++;
        rolling.SetItemProperty(0, 0.1f, 0.1f, 0.1f);
        RollingStart(curNum, false, false);
    }

    public void IncrementTen()
    {
        if (!rolling.IsRollingFinish()) return;

        curNum += 10;
        rolling.SetItemProperty(0, 0.1f, 0.1f, 0.1f);
        RollingStart(curNum, false, false);
    }

    public void AllFastOrder()
    {
        if (!rolling.IsRollingFinish()) return;

        curNum = (long)Random.Range(0, 9999999999);
        rolling.SetItemProperty(7, 0.1f, 0.1f, 0.1f);
        RollingStart(curNum, false, true);
    }

    public void AllSlowOrder()
    {
        if (!rolling.IsRollingFinish()) return;

        curNum = (long)Random.Range(0, 9999999999);
        rolling.SetItemProperty(7, 0.1f, 0.5f, 0.01f);
        RollingStart(curNum, false, true);
    }

    public void AllSameTime()
    {
        if (!rolling.IsRollingFinish()) return;

        curNum = (long)Random.Range(0, 9999999999);
        rolling.SetItemProperty(7, 0f, 0.3f, 0.1f);
        RollingStart(curNum, false, true);
    }

    public void SwitchImageOrder()
    {
        if (!rolling.IsRollingFinish()) return;

        curNum = (long)Random.Range(0, 9999999999);
        rolling.SetItemProperty(7, 0.2f, 0.1f, 0.1f);
        RollingStart(curNum, true, true);
    }

    public void SwitchImageSame()
    {
        if (!rolling.IsRollingFinish()) return;

        curNum = (long)Random.Range(0, 9999999999);
        rolling.SetItemProperty(7, 0f, 0.5f, 0.01f);
        RollingStart(curNum, true, true);
    }

    public void Pattern1()
    {
        if (!rolling.IsRollingFinish()) return;

        curNum = (long)Random.Range(0, 9999999999);
        rollingPattern.SetItemProperty(7, 0.1f, 0.5f, 0.01f);
        RollingStart(curNum, false, true);
    }

    public void RollingStart(long num, bool isSwitchAction, bool fillZero)
    {
        rolling.RollingStart(num, isSwitchAction, fillZero);
    }
}
