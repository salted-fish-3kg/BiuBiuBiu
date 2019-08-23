using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Knight.Tools;
using Knight.Option;
using Knight.Skill;

namespace Knight.Character
{
    public class CharacterControl : MonoBehaviour
    {
        // Use this for initialization
        Transform tower;
        public Transform GetTower
        {
            get { return tower; }
        }
        Transform chassis;
        public Action<Vector3> moveTo;
        public Action<Vector3> direction;
        public Action<bool> fire;
        public Action<bool> skill_1;
        public Action<bool> skill_2;
        public Action<bool> skill_3;
        [SerializeField]
        CharacterStatus status;
        SkillManager SkillManager;
        private void Awake()
        {
        }
        private void OnEnable()
        {
        }
        public void CharacterControlInit()
        {
            tower = transform.FindChildByName("Tower");
            chassis = transform.FindChildByName("Chassis");
            if (tower == null || chassis == null) this.enabled = false;
            Option_Input.Instance.direction = LookDirection;
            Option_Input.Instance.move = MoveTo;
            Option_Input.Instance.fire_1 = Fire;
            SkillManager = GetComponent<SkillManager>();
        }
        void Start()
        {
        }
        void MoveTo(Vector3 vector3)
        {
            if (vector3.magnitude <= 0.25f) return;
            if (moveTo != null) moveTo(vector3);
            transform.position += (vector3 * status.moveSpeed * Time.deltaTime);
            chassis.eulerAngles = new Vector3(0, 0, Vector3.SignedAngle(Vector3.up, vector3, Vector3.forward) + 180);
        }
        void LookDirection(Vector3 direction)
        {
            if (this.direction != null) this.direction(direction);
            tower.eulerAngles = new Vector3(0, 0, Vector3.SignedAngle(Vector3.up, direction, Vector3.forward) + 180);
        }
        void Fire(bool fire)
        {
            if (fire)
            {
                SkillManager.RelsaseSkill("Bullet");
            }
        }
        void Skill_1(bool fire)
        {
            if (skill_1 != null) skill_1(fire);

        }
        void Skill_2(bool fire)
        {
            if (skill_2 != null) skill_2(fire);
        }
        void Skill_3(bool fire)
        {
            if (skill_3 != null) skill_3(fire);
        }
    }
}