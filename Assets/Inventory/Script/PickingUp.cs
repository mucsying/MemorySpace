using UnityEngine;

namespace Inventory
{
	public class PickingUp : MonoBehaviour
	{
		private BagManager _bagManager;

		private void Start()
		{
			_bagManager = BagManager.Instance;
		}

		public void PickUp(string itemName)
		{
			_bagManager.PutInBag(itemName);
		}

		private void OnMouseDown()
		{
			PickUp(name);
		}
	}
}
