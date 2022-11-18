using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Alchentimist
{
    public class DragHandler : Singleton<DragHandler>
    {
        Draggable m_objectBeingDragged;
        DragDropableArea m_focusedArea;

        public void UpdateObjectPosition() { m_objectBeingDragged.transform.position = Input.mousePosition; }

        public Draggable ObjectBeingDragged {
            get {return m_objectBeingDragged; }
            set {
                if(value != null) {
                    value.transform.SetParent(transform);
                    value.transform.localScale = new Vector3(1, 1, 1);
                    value.GetComponent<CanvasGroup>().blocksRaycasts = false;
                }
                else{
                    m_objectBeingDragged.GetComponent<CanvasGroup>().blocksRaycasts = true;
                }
                m_objectBeingDragged = value;
            }
        }

        public DragDropableArea FocusedArea
        {
            get { return m_focusedArea; }
            set { m_focusedArea = value; }
        }

    }
}
