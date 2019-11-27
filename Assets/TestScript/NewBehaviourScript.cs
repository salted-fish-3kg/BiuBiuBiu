using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Knight.Tools.Timer;
using Knight.Character;
using Knight.GameController;
using Knight.Option;
using Knight.Core;
using Knight.Character.Enemy;
namespace Knight.TestSprict
{
    public class CharacterCreater : MonoBehaviour
    {
        Subject GameStart;
        // Use this for initialization
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {

        }
        public int[] TwoSum(int[] nums, int target)
        {
            for (int i = 0; i < nums.Length - 1; i++)
            {
                for (int j = i + 1; j < nums.Length; j++)
                {
                    if (nums[i] + nums[j] == target)
                        return new int[] { nums[i], nums[j] };
                }
            }
            return null;
        }
        void Test()
        {
            CharacterManager.Instance.ChoiceCharacter(CharacterManager.CharacterName.tank);
            GameCore.Instance.Pause = true;
        }
        void RemoveTest()
        {
            CanPlaceFlowers(new int[] { 1, 0, 0, 0, 1, 0, 0 }, 2);
        }
        private void OnGUI()
        {
            if (GUILayout.Button("RegSubject")) RegSubject();
            if (GUILayout.Button("remove")) Notify();
            if (GUILayout.Button("notify")) MapManager.Instance.InitMapComponentCoroutine();
        }
        public bool CanPlaceFlowers(int[] flowerbed, int n)
        {
            int nums = 0;
            for (int i = 0; i < flowerbed.Length; i++)
            {
                Debug.Log("//" + i);
                if (flowerbed[i] == 1)
                {
                    i++;
                    continue;
                }
                if (!(i < 1 || flowerbed[i - 1] == 0)) continue;
                if (i < flowerbed.Length - 1 && flowerbed[i + 1] == 1)
                {
                    Debug.Log("/" + i);
                    i += 2;
                    continue;
                }
                Debug.Log(i + "/ture");
                nums++;
                i++;
            }
            Debug.Log(nums);
            if (nums == n) return true;
            return false;
        }
        private void RegSubject()
        {
            Subject gameStart = Message.AttachSubject("GameStart");
        }
        private void RegOb()
        {
            Message.AttachObseverEvent("Core", ListenEvent);
        }
        private void ListenEvent(object[] data)
        {
            Debug.Log(data[0]);
            Debug.Log(data[1]);
        }
        private void Notify()
        {
            GameStart.Notify();
        }
        public int nums;
        public GameObject table;
        public LineRenderer liner;
        private void Sim()
        {
            Vector3[] agent = new Vector3[nums];
            for (int i = 0; i < nums; i++)
            {
                int count = GetAgent();
                Vector3 _temp = new Vector3(i / 5f, count / 10f, 0);
                agent[i] = _temp;
            }
            liner.positionCount = nums;
            liner.SetPositions(agent);
        }
        private int GetAgent()
        {
            int chance = 2;
            int count = 1;
            while (Random.Range(1, 101) >= chance)
            {
                if (count >= 50)
                {
                    chance += 2;
                }
                count++;
            }
            Debug.Log(count);
            return count;
        }
    }
}