using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Alchentimist
{
    public class DragHandler : Singleton<DragHandler>
    {
        Draggable m_objectBeingDragged;
        DragDropableArea m_focusedArea;

        public Draggable ObjectBeingDragged {
            get { return m_objectBeingDragged; }
            set { m_objectBeingDragged = value;}
        }

        public DragDropableArea FocusedArea
        {
            get { return m_focusedArea; }
            set { m_focusedArea = value; }
        }

    }
}
