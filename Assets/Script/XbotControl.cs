using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XbotControl : Entity
{

    private void Awake()
    {
        SetAwakeMethod(3, 2, 30, eEnemyKind.Xbot);
    }

    protected void Start()
    {

        SetStartMethod();
        _colorSkin = InGameManager._instance.SetRandomColorState();
            //DefineEnum.eColor.Red;

        surface_meshRenderer.material = InGameManager._instance.GetColorMaterial((int)_colorSkin);
    }

    void AttackAniEvent(float damage)
    {
        PlayerStatus.HP -= damage;
        InGameManager._instance.PlayHitSound();
    }

    public void AttackAniEvent1(float damage) { AttackAniEvent(damage); }
    public void AttackAniEvent2(float damage) { AttackAniEvent(damage); }
    public void AttackAniEvent3(float damage) { AttackAniEvent(damage); }




}




