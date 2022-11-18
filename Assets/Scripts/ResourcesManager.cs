using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ResourcesManager : Singleton<ResourcesManager>
{
    Dictionary<Type, ResourceType<dynamic>> m_typeToResources;

    protected override void Awake()
    {
        base.Awake();
        m_typeToResources = new Dictionary<Type, ResourceType<dynamic>>();
    }

    public int AddResource<T>(T p_object, Path p_path)
    {
        ResourceType<dynamic> resourcesFromType;

        bool alreadyContainsADictionary = m_typeToResources.ContainsKey(typeof(T));
        if (!alreadyContainsADictionary)
        {
            resourcesFromType = new ResourceType<dynamic>();
            m_typeToResources.Add(typeof(T), resourcesFromType);
        }
        else { resourcesFromType = m_typeToResources[typeof(T)]; }

        return resourcesFromType.AddResource(p_object, p_path);
    }

    public dynamic GetResourcesByID(int p_ID, Type p_type)
    {
        return m_typeToResources[p_type].GetResourceByID(p_ID);
    }

    public dynamic GetResourceByPath(string p_path, Type p_type) {
        return m_typeToResources[p_type].GetResourceByPath(p_path);
    }

    public bool IsResourceAlreadyAdded(int p_ID, Type p_type)
    {
        bool alreadyContainsADictionary = m_typeToResources.ContainsKey(p_type);
        if (!alreadyContainsADictionary) { return false; }
        return (m_typeToResources[p_type].Contains(p_ID));
    }
    public bool IsResourceAlreadyAdded(string p_path, Type p_type)
    {
        bool alreadyContainsADictionary = m_typeToResources.ContainsKey(p_type);
        if (!alreadyContainsADictionary) { return false; }
        return (m_typeToResources[p_type].Contains(p_path));
    }
}

internal class ResourceType<T>
{
    static int s_ID;

    Dictionary<int, T> m_resourcesByID;
    Dictionary<string, T> m_resourcesByPath;

    public ResourceType()
    {
        m_resourcesByID = new Dictionary<int, T>();
        m_resourcesByPath = new Dictionary<string, T>();
    }

    public T GetResourceByID(int p_ID) { return m_resourcesByID[p_ID]; }
    public T GetResourceByPath(string p_path) { return m_resourcesByPath[p_path]; }
    public int AddResource(T p_resource, Path path) {
        if (m_resourcesByPath.ContainsKey(path.path)) { return -1; }
        else {
            path.id = s_ID++;
            m_resourcesByID.Add(path.id, p_resource);
            m_resourcesByPath.Add(path.path, p_resource);
            return path.id;
        }
    }

    public bool Contains(int p_id) { return m_resourcesByID.ContainsKey(p_id); }
    public bool Contains(string p_path) { return m_resourcesByPath.ContainsKey(p_path); }

}