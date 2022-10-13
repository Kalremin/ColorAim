using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MutantControl : Entity
{

    private void Awake()
    {
        SetAwakeMethod(2, 2, 50, eEnemyKind.Mutant);
        _colorSkin = DefineEnum.eColor.None;
    }

    protected void Start()
    {
        SetStartMethod();
        
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

