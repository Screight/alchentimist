using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Alchentimist
{
    public interface DragDropableArea : IPointerEnterHandler, IPointerExitHandler, IDropHandler
    {
        public void OnPointerEnter(PointerEventData eventData);

        public void OnPointerExit(PointerEventData eventData);

        public void OnDrop(PointerEventData eventData);

    }
}
