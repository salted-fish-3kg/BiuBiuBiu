using Knight.Character;
using UnityEngine;
namespace Knight.Skill
{
    public abstract class SkillProjectile : MonoBehaviour
    {
        int damage;
        float coolTime;
        [SerializeField]
        internal SkillData _skillData;
        ISkillEffect[] skillEffects;
        ISkillSelect skillSelect;
        public SkillData CurrentSkillData
        {
            get { return _skillData; }
            set
            {
                _skillData = value;
                InitProjectile();
            }
        }
        private void InitProjectile()
        {
            skillEffects = SkillFactory.CreatSkillEffect(_skillData);
            skillSelect = SkillFactory.CreatSkillSelect(_skillData);
        }
        protected void GetTarget()
        {
            skillSelect.GetTarget(_skillData,transform);
        }
        protected void ImpactEffect()
        {
            for (int i = 0; i < skillEffects.Length; i++)
            {
                skillEffects[0].Effect(_skillData,transform);
            }
        }
        protected virtual void ReleseEnd()
        {
            SkillFactory.CollectEffectCalculater(skillEffects, _skillData);
            SkillFactory.CollectSelectCalculater(skillSelect, _skillData);
        }
    }
}