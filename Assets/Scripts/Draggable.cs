using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Alchentimist
{
    public abstract class Draggable : MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler
    {
        protected bool m_isBeingDragged;

        public void OnDrag(PointerEventData eventData)
        {
            DragHandler.Instance.UpdateObjectPosition();
        }

        public abstract void OnEndDrag(PointerEventData eventData);

        public abstract void OnBeginDrag(PointerEventData eventData);

    }
}
