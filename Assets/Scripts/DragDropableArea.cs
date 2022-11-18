using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Alchentimist
{
    public abstract class DragDropableArea : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IDropHandler
    {
        public void OnPointerEnter(PointerEventData eventData)
        {
            DragHandler.Instance.FocusedArea = this;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            DragHandler.Instance.FocusedArea = null;
        }

        public abstract void OnDrop(PointerEventData eventData);

    }
}
