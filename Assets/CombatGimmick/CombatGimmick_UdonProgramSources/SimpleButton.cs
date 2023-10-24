
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace CombatGimmick
{
    public enum SwitchType
    {
        Interact, TriggerCollider
    }

    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    public class SimpleButton : UdonSharpBehaviour
    {
        //---------------------------------------
        [Tooltip("発動したいUdonBehaviourをつけたオブジェクト")] [SerializeField] GameObject EventTarget;
        [Tooltip("発動する関数名")] [SerializeField] string EventName;
        [Tooltip("発動する関数名")] [SerializeField] SwitchType switchType;
        //---------------------------------------
        [Header("任意項目.無しでも動作可能")]
        [Tooltip("操作時の効果音")] [SerializeField] AudioClip ButtonPressAudioClip;
        //---------------------------------------
        AudioSource audioSource;
        //---------------------------------------
        private void Start()
        {
            audioSource = this.GetComponent<AudioSource>();
        }
        //---------------------------------------
        public override void Interact()
        {
            if(switchType == SwitchType.Interact)
            {
                if (EventTarget && EventTarget.GetComponent<UdonSharpBehaviour>())
                {
                    EventTarget.GetComponent<UdonSharpBehaviour>().SendCustomEvent(EventName);
                }

                if (ButtonPressAudioClip) { audioSource.PlayOneShot(ButtonPressAudioClip, 1.0f); }
            }
        }
        //---------------------------------------
        public void OnTriggerEnter(Collider other)
        {
            if (switchType == SwitchType.TriggerCollider)
            {
                if (EventTarget && EventTarget.GetComponent<UdonSharpBehaviour>())
                {
                    EventTarget.GetComponent<UdonSharpBehaviour>().SendCustomEvent(EventName);
                }

                if (ButtonPressAudioClip) { audioSource.PlayOneShot(ButtonPressAudioClip, 1.0f); }
            }
        }
        //---------------------------------------
    }
}

