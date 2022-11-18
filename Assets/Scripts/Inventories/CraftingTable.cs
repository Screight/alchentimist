using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Alchentimist
{

    public class CraftingTable : DynamicItemList, DragDropableArea 
    {
        List<Ingredient> m_ingredients;

        protected override void Awake()
        {
            base.Awake();
            m_ingredients = new List<Ingredient>();
        }

        public void AddIngredient(Ingredient p_ingredient)
        {
            ItemSpace itemSpace;
            for(int i = 0;i < m_itemSpaces.Count; i++)
            {
                itemSpace = m_itemSpaces[i];
                if (itemSpace.IsEmpty)
                {
                    m_itemSpaces[i].SetIsEmptyTo(false);
                    m_ingredients.Add(p_ingredient);
                    p_ingredient.transform.SetParent(itemSpace.Transform);
                    p_ingredient.transform.localPosition = Vector3.zero;
                    p_ingredient.transform.localScale = Vector3.one;
                    return;
                }
            }
            Destroy(p_ingredient.gameObject);
        }

        public void OnDrop(PointerEventData eventData)
        {
            Debug.Log("Potato");

            Ingredient ingredient = DragHandler.Instance.ObjectBeingDragged as Ingredient;
            DragHandler.Instance.ObjectBeingDragged = null;
            if (ingredient == null) { return; }

            string path = ingredient.Data.iconPath.path;
            AddIngredient(ingredient);
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
