using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using LitJson;

namespace Inventory
{
	public class BagManager : MonoBehaviour
	{
		/// <summary>
		/// BagManager为单例模式
		/// 不应该使用构造器来获取BagManager的实例，而应该使用此静态变量。
		/// </summary>
		public static BagManager Instance { get; private set; }
		
		public GameObject playerBag;
		public Text description;
		public GameObject itemGridPrefab;
		
		/// <summary>
		/// 储存了当前玩家所选择的背包中的物体的名字。
		/// 可以通过获取这个值来得知玩家当前使用的物体名。
		/// 如果为“”表示未选择任何物体
		/// </summary>
		public string currentSelection;

		private readonly List<string> _itemNames = new List<string>();
		

		private void Awake()
		{
			if (Instance == null)
			{
				Instance = this;
			}
			else
			{
				Destroy(gameObject);
			}
		}
		
		/// <summary>
		/// 将该物体放入背包中,如果同名物体已经存在，则会在Console打印信息，并不会将其放入背包中。
		/// 该函数要求存在Assets/Inventory/ScriptableObject/itemName.asset文件,否则不会产生实际效果。
		/// </summary>
		/// <param name="itemName">物体的名称，即gameObject.name</param>
		public void PutInBag(string itemName)
		{
			if (!_itemNames.Contains(itemName))
			{
				_itemNames.Add(itemName);
				GameObject newItem = Instantiate(itemGridPrefab, playerBag.transform);
				newItem.name = itemName;
			}
			else
			{
				Debug.Log("<Info>重复的物品名称:" + itemName);
			}
		}

		/// <summary>
		/// 从bagManager物品名称列表中移除某个名称。
		/// 该函数用于背包模块内部调用，不需要其他模块主动调用该函数。
		/// 如果要将物体从背包中移除，请调用TakeOutBag(string itemName)函数
		/// </summary>
		/// <param name="removeName">要移除的物品名称</param>
		public void RemoveFromList(string removeName)
		{
			_itemNames.Remove(removeName);
		}
		
		/// <summary>
		/// 显示背包，当需要修改背包显示状态时调用该函数，不需要直接调用背包物体
		/// 该函数会调用背包物体的SetActive函数。
		/// </summary>
		/// <param name="show">是否显示背包</param>
		public void ShowBag(bool show)
		{
			playerBag.SetActive(show);
		}
		
		/// <summary>
		/// 将物体从背包中移除。这个操作会摧毁背包中对应的物品格，同时将光标设置为默认状态。
		/// 应该在物体被成功使用后调用该函数。
		/// </summary>
		/// <param name="itemName">需要移除的物体的名字，即gameObject.name</param>
		public void TakeOutBag(string itemName)
		{
			Destroy(GameObject.Find(itemName));
			_itemNames.Remove(itemName);
			Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
		}
        
        /// <summary>
        /// 保存背包的信息，在保存游戏时调用即可。
        /// </summary>
		public void SaveBag()
		{
			string saveString = JsonMapper.ToJson(_itemNames);
			File.WriteAllText(Application.persistentDataPath + "/InventorySave/playerBag.json",saveString);
		}
		
        public void LoadBag()
        {
	         
        }
	}
}
