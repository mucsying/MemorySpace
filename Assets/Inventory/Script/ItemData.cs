using UnityEngine;

namespace Inventory
{
	[CreateAssetMenu(menuName = "Custom/Item Data", fileName = "ItemData", order = 0)]
	public class ItemData : ScriptableObject
	{
		public string itemDescription;
		public Sprite itemIcon;
	}
}
