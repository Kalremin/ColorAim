using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class DefineEnum
{
    public enum eScenesState
    {
        TitleScene =0,
        InGameScene
    }

    public enum eGameState
    {
        Ready =0,
        Play,
        Pause,
        End,
        Result
    }

    public enum eEnemyState
    {
        Idle=0,
        Walk,
        Run,
        Attack,
        Death,
        Reset,
        Pause
    }

    public enum eColor
    {
        None=-1,
        Red=0,
        Yellow,
        Green,
        Blue,
        Purple
    }

    public enum eGunState
    {
        Idle=0,
        Fire,
        Reload
    }

    public enum eHitSound
    {
        HittedXbot = 0,
        HittedMutant,
        HittedItem,
        HittedPlayer
    }

    public enum eGunSound
    {
        FirePistol = 0,
        ReloadPistol,
        FireRifle,
        ReloadRifle
    }

    public enum eGunKind
    {
        Pistol,
        Rifle
    }

}

