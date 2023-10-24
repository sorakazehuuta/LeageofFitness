
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace CombatGimmick
{
    public enum DropItemType
    {
        Master, Loot, Stationary, Cloned
    }
    public enum DropItemEffectType
    {
        HitPoint, Shield, ReviveTicket, Ammo
    }
    public enum DropItemPickUpType
    {
        Touch, Interact
    }

    [ExecuteInEditMode]
    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    public class DropItem : UdonSharpBehaviour
    {
        //---------------------------------------
        [Tooltip("アイテムの種類")] public DropItemType dropItemType;
        [Tooltip("アイテム効果の種類")] public DropItemEffectType dropItemEffectType;
        [Tooltip("アイテム回収の方法")] public DropItemPickUpType dropItemPickUpType;
        [Tooltip("効果の数値")] public float EffectValue = 1;
        [Tooltip("弾薬の種類(Ammoの場合のみ適用)")] public int AmmoType;
        //---------------------------------------
        [Header("----スポーン・デスポーン設定----")]
        [Tooltip("自然消滅するまでの秒数")] public float DespawnTimer = 60;
        [Tooltip("設定秒数ごとにアイテムをスポーン")] public float SpawnInterval = 10;
        [Tooltip("スポーン地点")] public Transform[] ItemSpawnPoints;
        //---------------------------------------
        [Header("その他(任意項目.無しでも動作可能)")]
        [Tooltip("0以上なら出現場所をランダムでずらす")] public float RandomizeValue = 0.5f;
        [Tooltip("取得時の効果音")] public AudioClip itemTakeClip;
        [Tooltip("次にアイテム取得できるまでの時間")] public float TakeItemInterval = 1.0f;
        //---------------------------------------
        //[Header("AutoBuild時に自動で設定")]
        [HideInInspector] [Tooltip("PlayerManager")] public PlayerManager playerManager;
        //---------------------------------------
        bool InBattle;    //戦闘中はtrue
        AudioSource audioSource;
        Collider collider;
        float lastTakeItemTime;
        //---------------------------------------
        private void Start()
        {
            collider = this.GetComponent<Collider>();
            if (dropItemPickUpType == DropItemPickUpType.Touch) { collider.isTrigger = true; }

            audioSource = this.GetComponent<AudioSource>();
        }
        //---------------------------------------
        public bool IsMasterObject()
        {
            if (dropItemType == DropItemType.Master) { return true; }
            else { return false; }
        }
        //---------------------------------------
        public bool IsLootObject()
        {
            if (dropItemType == DropItemType.Loot) { return true; }
            else { return false; }
        }
        //---------------------------------------
        public bool IsStationaryObject()
        {
            if (dropItemType == DropItemType.Stationary) { return true; }
            else { return false; }
        }
        //---------------------------------------
        public bool IsClonedObject()
        {
            if (dropItemType == DropItemType.Cloned) { return true; }
            else { return false; }
        }
        //---------------------------------------
        public override void OnPlayerTriggerEnter(VRCPlayerApi player)
        {
            if (!Utilities.IsValid(Networking.LocalPlayer) || player != Networking.LocalPlayer) { return; }
            if (IsMasterObject()) { return; } //原本のオブジェクトは入手不可

            if (Utilities.IsValid(Networking.LocalPlayer) && player == Networking.LocalPlayer) { TakeItem(); }
        }
        //---------------------------------------
        public override void Interact()
        {
            TakeItem();
        }
        //---------------------------------------
        public void TakeItem()
        {
            if (IsMasterObject()) { return; } //原本のオブジェクトは入手不可
            if (!playerManager.playerHitBox) { return; }
            if (Time.time - lastTakeItemTime < TakeItemInterval) { return; }

            lastTakeItemTime = Time.time;

            if (dropItemEffectType == DropItemEffectType.HitPoint) { playerManager.playerHitBox.TakeDamage(-EffectValue, true); }   //マイナスダメージは回復扱い
            if (dropItemEffectType == DropItemEffectType.Shield) { playerManager.playerHitBox.GrantShield(EffectValue); }
            if (dropItemEffectType == DropItemEffectType.ReviveTicket) { playerManager.playerHitBox.GrantReviveTicket(EffectValue); }
            if (dropItemEffectType == DropItemEffectType.Ammo) { playerManager.GetAmmo(AmmoType, (int)EffectValue); }

            if (itemTakeClip) { audioSource.PlayOneShot(itemTakeClip, 1.0f); }

            if (IsLootObject())
            {
                collider.enabled = false;
                const float delay = 1;
                SendCustomEventDelayedSeconds("DelayedSecond_DeactivateObject", delay);
                return;
            }

            if (IsClonedObject())
            {
                collider.enabled = false;
                const float delay = 1;
                SendCustomEventDelayedSeconds("DelayedSecond_DespawnItem", delay);
                return;
            }
        }
        //---------------------------------------
        public void StartBattle()
        {
            if (dropItemType == DropItemType.Cloned) { return; }
            if (dropItemType == DropItemType.Stationary) { return; }
            if (dropItemType == DropItemType.Loot)
            {
                collider.enabled = true;
                this.gameObject.SetActive(true);
                return;
            }
            
            //原本のアイテムであれば、一定間隔でコピーオブジェクトの生成を開始
            if (SpawnInterval > 0)
            {
                InBattle = true;
                SpawnItem();
                SendCustomEventDelayedSeconds("DelayedSecond_SpawnItem", SpawnInterval);
                return;
            }
        }
        //---------------------------------------
        public void EndBattle()
        {
            //クローンアイテムはゲーム終了時に消去する
            //GameManagerからメソッドを呼び出すのと同じフレームで削除してはいけないので、ディレイを挟む
            //(foreachの処理中に配列の内容を削除してはいけない)
            if (IsClonedObject())
            {
                const float delay = 1.0f;
                SendCustomEventDelayedSeconds("DelayedSecond_DespawnItem", delay);
            }

            //原本のアイテムであれば、コピーオブジェクトの生成を停止
            else { InBattle = false; }
        }
        //---------------------------------------
        public void DelayedSecond_SpawnItem()
        {
            if (InBattle && IsMasterObject() && SpawnInterval > 0)
            {
                SpawnItem();
                SendCustomEventDelayedSeconds("DelayedSecond_SpawnItem", SpawnInterval);
            }
        }
        //---------------------------------------
        public void SpawnItem()
        {
            GameObject newItem = VRCInstantiate(this.gameObject);

            int r = Random.Range(0, ItemSpawnPoints.Length);
            newItem.transform.parent = this.transform.parent;

            if (RandomizeValue > 0)
            {
                Vector3 rand = new Vector3(Random.Range(-RandomizeValue, RandomizeValue), 0, Random.Range(-RandomizeValue, RandomizeValue));
                newItem.transform.SetPositionAndRotation(ItemSpawnPoints[r].position + rand, ItemSpawnPoints[r].rotation);
            }
            else { newItem.transform.SetPositionAndRotation(ItemSpawnPoints[r].position, ItemSpawnPoints[r].rotation); }

            newItem.GetComponent<DropItem>().MarkAsCloned();    //コピー後のフラグを立てる

            if (DespawnTimer > 0) { newItem.GetComponent<DropItem>().SendCustomEventDelayedSeconds("DelayedSecond_DespawnItem", DespawnTimer); }
        }
        //---------------------------------------
        public void MarkAsCloned()
        {
            dropItemType = DropItemType.Cloned;
        }
        //---------------------------------------
        public void DelayedSecond_DeactivateObject()
        {
            this.gameObject.SetActive(false);
        }
        //---------------------------------------
        public void DelayedSecond_DespawnItem()
        {
            DespawnItem();
        }
        //---------------------------------------
        public void DespawnItem()
        {
            //ドロップアイテムを消去する
            Destroy(this.gameObject);
        }
        //---------------------------------------
        public bool AutoBuild(PlayerManager _playerManager)
        {
            playerManager = _playerManager;

            if(IsClonedObject())
            {
                Debug.Log("<color=#ff0000>Warning:</color> " + this.gameObject.name + " DropItemTypeはCloned.以外を選択してください.", this.gameObject);
                return false;
            }

            if (_playerManager.DefaultAmmo.Length >= 1)
            {
                if (AmmoType < 0 || _playerManager.DefaultAmmo.Length <= AmmoType)
                {
                    Debug.Log("<color=#ff0000>Warning:</color> " + this.gameObject.name + " AmmoTypeの値は0以上かつ, PlayerManagerのDefaultAmmoの要素数より小さくしてください.", this.gameObject);
                    return false;
                }
            }

            return true;
        }
        //---------------------------------------
    }
}

