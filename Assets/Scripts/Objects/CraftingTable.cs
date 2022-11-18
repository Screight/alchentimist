using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Alchentimist
{
    public class CraftingTable : DragDropableArea 
    {
        List<IngredientData> m_ingredientsData;

        [SerializeField] GameObject[] m_ingredients;
        List<IngredientModel> m_ingredientsModel;

        private void Awake()
        {
            m_ingredientsModel = new List<IngredientModel>();
            for(int i = 0;i < m_ingredients.Length; i++)
            {
                m_ingredientsModel.Add(new IngredientModel(m_ingredients[i]));
            }
        }

        public void AddIngredient(IngredientData p_data)
        {
            Debug.Log(p_data.ToString());
            m_ingredientsData.Add(p_data);
        }

        public override void OnDrop(PointerEventData eventData)
        {
            Ingredient ingredient = DragHandler.Instance.ObjectBeingDragged as Ingredient;

            if(ingredient == null) { return; }

            string path = ingredient.Data.iconPath.path;
            Sprite sprite = ResourcesManager.Instance.GetResourceByPath(path, typeof(Sprite));

            m_ingredientsModel[0].m_icon.sprite = sprite;
        }
    }
}
