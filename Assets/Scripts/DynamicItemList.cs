using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Alchentimist
{
    public class ItemSpace
    {
        bool m_isEmpty;
        GameObject m_gO;

        public ItemSpace(GameObject p_gO)
        {
            m_isEmpty = true;
            m_gO = p_gO;
        }

        public void SetIsEmptyTo(bool p_value) {
            m_isEmpty = p_value;
        }

        public bool IsEmpty
        {
            get { return m_isEmpty; }
            set { m_isEmpty = value; }
        }
        public Transform Transform { get { return m_gO.transform; } }

    }

    public abstract class DynamicItemList : MonoBehaviour
    {
        [SerializeField] GameObject m_blueprint;
        [SerializeField] protected int m_numberOfSpaces;

        protected List<ItemSpace> m_itemSpaces;

        protected virtual void Awake()
        {
            m_itemSpaces = new List<ItemSpace>();

            for(int i = 0; i < m_numberOfSpaces; i++)
            {
                GameObject gO = Instantiate(m_blueprint, transform);
                ItemSpace itemSpace = new ItemSpace(gO);
                m_itemSpaces.Add(itemSpace);
            }
        }

        public void InitializeSpacesFromList(List<Draggable> p_list)
        {
            ItemSpace itemSpace;

            Draggable draggable;

            for (int i = 0; i < p_list.Count; i++)
            {
                if(i >= m_itemSpaces.Count) { return; }

                draggable = p_list[i];

                itemSpace = m_itemSpaces[i];

                if (i >= m_itemSpaces.Count) { return; }
                draggable.transform.SetParent(itemSpace.Transform);
                draggable.transform.localScale = new Vector3(1, 1, 1);
                draggable.transform.localPosition = Vector3.zero;
                itemSpace.IsEmpty = false;
            }
        }

        public void SetSpacesFromList(List<Draggable> p_list)
        {
            for(int i = 0; i < p_list.Count; i++)
            {

            }
        }

    }

}