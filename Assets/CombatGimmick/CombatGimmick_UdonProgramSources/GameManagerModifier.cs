
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace CombatGimmick
{
    public enum GameManagerModifierType
    {
        SetScore, SetTicket, SetTimer, SetFlagCarrier, SetConquest
    }

    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    public class GameManagerModifier : UdonSharpBehaviour
    {
        //---------------------------------------
        [Tooltip("変更したい数値の種類")] [SerializeField] GameManagerModifierType gameManagerModifierType;
        [Tooltip("変更したい数値の種類")] [SerializeField] int SetValue = 1;
        //---------------------------------------
        //[Header("AutoBuild時に自動で設定")]
        [HideInInspector] [Tooltip("GameManager")] public GameManager gameManager;
        //---------------------------------------
        public override void Interact()
        {
            if (gameManagerModifierType == GameManagerModifierType.SetScore)
            {
                gameManager.SetSyncedMaxScore(SetValue);
                return;
            }
            if (gameManagerModifierType == GameManagerModifierType.SetTicket)
            {
                gameManager.SetSyncedMaxReviveTicket(SetValue);
                return;
            }
            if (gameManagerModifierType == GameManagerModifierType.SetTimer)
            {
                gameManager.SetSyncedBattleDuration(SetValue);
                return;
            }
            if (gameManagerModifierType == GameManagerModifierType.SetFlagCarrier)
            {
                gameManager.SetSyncedMaxFlagScore(SetValue);
                return;
            }
            if (gameManagerModifierType == GameManagerModifierType.SetConquest)
            {
                gameManager.SetSyncedMaxConquestTime(SetValue);
                return;
            }
        }
        //---------------------------------------
        public bool AutoBuild(GameManager _gameManager)
        {
            gameManager = _gameManager;

            if (gameManagerModifierType == GameManagerModifierType.SetScore && SetValue <= 0)
            {
                Debug.Log("<color=#ff0000>Warning:</color> " + this.gameObject.name + " 目標スコアは0より大きくしてください.", this.gameObject);
                return false;
            }
            if (gameManagerModifierType == GameManagerModifierType.SetTicket && SetValue <= 0)
            {
                Debug.Log("<color=#ff0000>Warning:</color> " + this.gameObject.name + " 残機数は0より大きくしてください.", this.gameObject);
                return false;
            }
            if (gameManagerModifierType == GameManagerModifierType.SetTimer && SetValue <= 0)
            {
                Debug.Log("<color=#ff0000>Warning:</color> " + this.gameObject.name + " 制限時間は0より大きくしてください.", this.gameObject);
                return false;
            }

            return true;
        }
        //---------------------------------------
    }
}
