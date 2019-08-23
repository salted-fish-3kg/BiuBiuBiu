using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Knight.Core;
using System;

namespace Knight.Character.Enemy
{
    /// <summary>
    /// 敌人管理器
    ///     敌人列表
    ///     敌人加载
    ///     敌人生成
    /// </summary>
    public class EnemyManager : MonoSingleton<EnemyManager>
    {
        static Dictionary<string, List<Enemy>> _enemys = new Dictionary<string, List<Enemy>>();
        public void CreatEnemy(string name)
        {
            List<Enemy> enemies;
            GameObject obj;
            if (_enemys.TryGetValue(name, out enemies))
                obj = ObjectPool.GetObject(name);
            else
                obj = LoadNewEnemy(name);
        }
        GameObject LoadNewEnemy(string name)
        {
            GameObject obj = Tools.Tools.LoadPrefab<GameObject>("Prefab/Enemy/" + name);
            obj.name = name;
            obj.tag = "Enemy";
            List<Enemy> enemies = new List<Enemy>();
            _enemys.Add(name, enemies);
            ObjectPool.AddNewObjectPool(name, obj, InitPrefab);
            return obj;
        }
        public Enemy GetEnemy(string name)
        {
            string[] names = name.Split('_');
            int count = int.Parse(names[1]);
            List<Enemy> enemies;
            if (!_enemys.TryGetValue(names[0], out enemies)) return null;
            if (!(count < enemies.Count)) return null;
            return enemies[count];

        }
        private void InitPrefab(GameObject prefab)
        {
            prefab.tag = "Enemy";
            Enemy enemy = prefab.AddComponent<Enemy>();
            _enemys[prefab.name].Add(enemy);
            prefab.name = prefab.name + "_" + _enemys[prefab.name].Count;
        }
    }
}