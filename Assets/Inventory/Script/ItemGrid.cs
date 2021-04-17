using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace Inventory
{
	public class ItemGrid : MonoBehaviour
	{
		private BagManager _bagManager;
		private const string ItemDataPathPrefix = "Assets/Inventory/ScriptableObject/";
		private ItemData _itemData;

		private void Start()
		{
			_bagManager = BagManager.Instance;
			string itemDataPath = ItemDataPathPrefix + name + ".asset";
			if (File.Exists(itemDataPath))
			{
				_itemData = AssetDatabase.LoadAssetAtPath<ItemData>(itemDataPath);
				GetComponent<Image>().sprite = _itemData.itemIcon;
				
			}
			else
			{
				Debug.LogError("<Error>找不到文件：" + itemDataPath);
				_bagManager.RemoveFromList(name);
				Destroy(gameObject);
			}
			
		}

		public void ClickListener()
		{
			if (_bagManager.currentSelection == name)
			{
				_bagManager.currentSelection = "";
				_bagManager.description.text = "";
				Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
			}
			else
			{
				_bagManager.currentSelection = name;
				_bagManager.description.text = _itemData.itemDescription;
				//Cursor.SetCursor(null, _hotSpot, _cursorMode);
			}
		}
	}
}
