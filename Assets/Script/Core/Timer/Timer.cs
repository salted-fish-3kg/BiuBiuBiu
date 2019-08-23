using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Knight.Tools.Timer
{
    /// <summary>
    /// 定时器
    /// Delayer延时执行 doTimes=-1则无限执行
    /// </summary>
    public class Timer : MonoSingleton<Timer>
    {
        static Dictionary<int, Delay> delays = new Dictionary<int, Delay>();
        static List<int> removeList = new List<int>();
        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (delays.Count <= 0) return;
            int[] keys  = new int[delays.Count];
            delays.Keys.CopyTo(keys, 0);
            for (int i = 0; i < keys.Length; i++)
            {
                Delay delay;
                if (!delays.TryGetValue(keys[i], out delay)) return;
                if(Time.time < delay.delayTime) continue;
                if (delay.doTimes < 1)
                {
                    removeList.Add(keys[i]);
                    continue;
                }
                delay.delayTime += delay.intervalTime;
                delay.Doing();
                delay.doTimes--;
            }
            Remove();
        }
        private void Remove()
        {
            if (removeList.Count <= 0) return;
            for (int i = removeList.Count - 1; i >= 0; i--)
            {
                if (!delays.ContainsKey(removeList[i])) return;
                delays.Remove(removeList[i]);
            }
            removeList.Clear();
            Debug.Log("Remove");
        }
        protected override void Initialization()
        {
            base.Initialization();
        }
        /// <summary>
        /// name和返回的int 都可以用作查询依据
        /// </summary>
        /// <param name="name"></param>
        /// <param name="delayTime">延迟时间</param>
        /// <param name="doTimes">执行次数 -1无限执行</param>
        /// <param name="intervalTime">间隔时间</param>
        /// <param name="action">funciton</param>
        /// <returns>序号</returns>
        public static int Delayer(string name, float delayTime, int doTimes, float intervalTime, Action action)
        {
            int nums = 0;
            while (delays.ContainsKey(nums))
            {
                nums++;
            }
            Delay delay = new Delay(name, delayTime, doTimes, intervalTime, action);
            delays.Add(nums, delay);
            return nums;
        }
        public static void RemoveDelayer(int num)
        {
            if (delays.ContainsKey(num)) removeList.Add(num);
        }
        public static void RemoveDelayer(string name)
        {
            foreach (var item in delays)
            {
                if (item.Value.name == name)
                {
                    removeList.Add(item.Key);
                    return;
                }
            }
        }
        class Delay
        {
            internal string name;
            internal float delayTime;
            internal int doTimes;
            internal float intervalTime;
            internal Action action;
            public Delay(string name, float delayTime, int doTimes, float intervalTime, Action action)
            {
                this.name = name;
                this.delayTime = delayTime + Time.time;
                this.doTimes = doTimes;
                this.intervalTime = intervalTime;
                this.action = action;
            }

            internal void Doing()
            {
                if (action == null) return;
                action();
            }
        }
    }
}