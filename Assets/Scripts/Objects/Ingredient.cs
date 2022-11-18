using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Alchentimist
{
    public class Ingredient : Draggable
    {
        IngredientData m_data;
        CanvasGroup m_canvasGroup;

        private void Awake()
        {
            m_canvasGroup = GetComponent<CanvasGroup>();
        }

        public override void OnEndDrag(PointerEventData eventData)
        {
            m_canvasGroup.blocksRaycasts = true;
            return;
            DragHandler.Instance.ObjectBeingDragged = null;
            m_isBeingDragged = false;

            CraftingTable craftingTable = DragHandler.Instance.FocusedArea as CraftingTable;

            if(craftingTable != null)
            {
                craftingTable.AddIngredient(m_data);
            }
        }

        public override void OnBeginDrag(PointerEventData eventData)
        {
            DragHandler.Instance.ObjectBeingDragged = this;
            m_canvasGroup.blocksRaycasts = false;
            m_isBeingDragged = true;
        }

        public IngredientData Data {
            get { return m_data; }
            set { m_data = value; }
        }

    }
}