using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class PlayerStatus
{
    //const float oneUnitHP = 10;
    static float currentHp=0;
    static float maxHp=-1;
    static float unitHp=-1;

    public static float HP { get { return currentHp; } set { currentHp = value; } }
    public static float MaxHP { get { return maxHp; } set { maxHp = value; } }
    public static void SetUnitHP(float value) { unitHp = value; }
    
    public static void SetHPUnitLine(GameObject HpUnitsParentObj) 
    {
        if (maxHp == -1 || unitHp == -1)
            return;


        float scaleX =
            1 // (unitHp/unitHp)
            / (maxHp / unitHp)
            *10; //
                 //* 10; // 한줄에 10개의 칸 있기에 1개의 칸을 10개의 칸의 너비만큼 늘린다.
        //foreach (Transform childTrans in HpUnitsParentObj.transform)
        //{
        //    childTrans.gameObject.transform.localScale = new Vector3(scaleX, 1, 1);
        //    childTrans.gameObject.transform.localPosition = new Vector3(temp.x * scaleX, temp.y, temp.z);
        //}

        for (int i = 0; i < HpUnitsParentObj.transform.childCount; i++)
        {
            Vector3 temp = new Vector3(scaleX, 1, 1);

            HpUnitsParentObj.transform.GetChild(i).localScale = new Vector3(scaleX,1,1);
            HpUnitsParentObj.transform.GetChild(i).localPosition =
                new Vector3(
                     (30 + 60 * i)*scaleX - 300, // 왜 +300 으로 추가된건지 모르겠다.
                    HpUnitsParentObj.transform.GetChild(i).localPosition.y,
                    HpUnitsParentObj.transform.GetChild(i).localPosition.z);

        }
    }
    
}
