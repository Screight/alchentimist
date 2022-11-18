using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Alchentimist
{
    public class Ingredient : Draggable
    {
        IngredientData m_data;
        IngredientModel m_model;

        private void Awake()
        {
            m_model = new IngredientModel(this.gameObject);
        }

        public override void OnEndDrag(PointerEventData eventData)
        {
        }

        public override void OnBeginDrag(PointerEventData eventData)
        {
            Draggable draggable = Instantiate(gameObject).GetComponent<Draggable>();
            DragHandler.Instance.ObjectBeingDragged = draggable;
        }

        public IngredientData Data {
            get { return m_data; }
            set { m_data = value; }
        }

        public IngredientModel Model { get { return m_model; } }

    }
}