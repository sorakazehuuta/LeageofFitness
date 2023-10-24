using UnityEngine;

namespace VketTools.Utilities
{
    [CreateAssetMenu]
    public class VketItemInfo : ScriptableObject
    {
        public enum ItemType : byte
        {
            None,
            Pickup,
            AvatarPedestal,
        }
        
        [SerializeField] public ItemType _itemType;
        [SerializeField] public string _bluePrintID;
        [SerializeField] public int _selectTemplateIndex;
        [SerializeField] public string _itemName;
        [SerializeField] public string _itemPrice;
    }
}
