
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

//---------------------------------------
//継承用のクラス
//---------------------------------------

namespace CombatGimmick
{
    //---------------------------------------
    //武器の種類
    public enum WeaponType
    {
        Melee, Ranged
    }
    //---------------------------------------

    public class Weapon : UdonSharpBehaviour
    {
        //---------------------------------------
        public virtual void DealDamage(float dmg, GameObject target)
        {

        }
        //---------------------------------------
        public virtual void AutoBuild()
        {

        }
        public virtual bool AutoBuild(PlayerManager pm)
        {
            return true;
        }
        public virtual bool AutoBuild(PlayerManager pm, GameManager gm)
        {
            return true;
        }
        //---------------------------------------
        public virtual void Initialize()
        {

        }
        //---------------------------------------
    }
}


