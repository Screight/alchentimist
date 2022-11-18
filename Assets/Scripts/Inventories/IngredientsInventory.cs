using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Alchentimist
{
    public class IngredientsInventory : DynamicItemList, DragDropableArea
    {

        public void OnDrop(PointerEventData eventData)
        {
            Debug.Log("");
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            DragHandler.Instance.FocusedArea = this;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            DragHandler.Instance.FocusedArea = null;
        }
    }
}
