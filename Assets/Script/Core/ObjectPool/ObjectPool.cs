using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// AddInPool 添加进入对象池
/// </summary>
namespace Knight.Core
{
    public class ObjectPool : MonoSingleton<ObjectPool>
    {
        static Dictionary<string, List<IRecycleObject>> pools = new Dictionary<string, List<IRecycleObject>>();
        static Dictionary<string, Prefab> prefabs = new Dictionary<string, Prefab>();
        /// <summary>
        /// 回收对象
        /// </summary>
        /// <param name="name"></param>
        /// <param name="obj"></param>
        public static void RecycleObject(string name, GameObject obj)
        {
            if (!pools.ContainsKey(name)) return;
            IRecycleObject recycleObject = obj.GetComponent<IRecycleObject>();
            if (recycleObject == null) return;
            recycleObject.RecycleObject();
            pools[name].Add(recycleObject);
        }
  /// <summary>
  /// 
  /// </summary>
  /// <param name="name">名字</param>
  /// <param name="prefab">预制体</param>
  /// <param name="initPrefab">预制体初始化</param>
        public static void AddNewObjectPool(string name, GameObject prefab, InitPrefab initPrefab)
        {
            if (prefabs.ContainsKey(name)) return;
            IRecycleObject recycleObject = prefab.GetComponent<IRecycleObject>();
            if (recycleObject == null) return;

            AddPrefab(name, prefab, initPrefab);
            List<IRecycleObject> pool = new List<IRecycleObject>();
            pool.Add(recycleObject);
            pools.Add(name,pool);
        }
        /// <summary>
        /// 从对象池中获取对象并激活
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static GameObject GetObject(string name)
        {
            GameObject obj;
            IRecycleObject recycleObject;
            if (!pools.ContainsKey(name)) return null;
            if (pools[name].Count <= 1) ExpandPool(name);
            recycleObject = pools[name][pools[name].Count - 1];
            obj =recycleObject.GetGameObject();
            recycleObject.Reset();
            pools[name].Remove(recycleObject);
            return obj;
        }
        /// <summary>
        /// 保存预制体
        /// </summary>
        /// <param name="name"></param>
        /// <param name="obj"></param>
        private static void AddPrefab(string name, GameObject obj, InitPrefab initPrefab)
        {

            obj.SetActive(false);
            Prefab _prefab = new Prefab();
            _prefab.prefab = obj;
            _prefab.InitPrefab = initPrefab;
            initPrefab(obj);
            prefabs.Add(name, _prefab);
        }
        /// <summary>
        /// 扩充对象池
        /// </summary>
        /// <param name="name"></param>
        private static void ExpandPool(string name)
        {
            GameObject _obj = Instantiate(prefabs[name].prefab) as GameObject;
            _obj.name = name;
            prefabs[name].InitPrefab(_obj);
            RecycleObject(name, _obj);
        }
        public delegate void InitPrefab(GameObject prefab);
        class Prefab
        {
            public GameObject prefab;
            public InitPrefab InitPrefab;
        }
    }
    internal interface IRecycleObject
    {
        void RecycleObject();
        void Reset();
        GameObject GetGameObject();
    }
}