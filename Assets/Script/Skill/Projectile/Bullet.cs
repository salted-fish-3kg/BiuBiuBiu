using Knight.Core;
using Knight.GameController;
using Knight.Skill;
using Knight.Tools;
using Knight.Tools.Timer;
using UnityEngine;

public class Bullet : SkillProjectile, IRecycleObject
{

    Transform player;
    Transform tower;
    int delayID = -1;
    public GameObject GetGameObject()
    {
        return gameObject;
    }

    public void RecycleObject()
    {
        gameObject.SetActive(false);
        if (delayID == -1) return;
        Timer.RemoveDelayer(delayID);
        delayID = -1;
    }

    public void Reset()
    {
        if (tower == null) Start();
        transform.rotation = tower.rotation;
        transform.position = (tower.position - tower.up * 0.5f);
        gameObject.SetActive(true);
        delayID = Timer.Delayer(_skillData.GetName, 1, 1, 0, () =>
        {
            ObjectPool.RecycleObject(_skillData.GetName, this.gameObject);
        });
    }
    // Use this for initialization
    void Start()
    {
        player = GameCore.Instance.Player;
        tower = player.FindChildByName("Tower");
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += (-6 * transform.up * Time.deltaTime);

    }
    protected override void ReleseEnd()
    {
        base.ReleseEnd();
    }
}
