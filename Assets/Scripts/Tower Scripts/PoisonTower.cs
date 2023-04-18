using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonBasicTower : BasicTower
{
    [SerializeField] private PoisonDebuffFactory statusFactory;
    [SerializeField] private BaseBuffSO statusList;
    //SAve
    private BaseBuffSO createStatus() {
        BaseBuffSO status = statusFactory.CreateBuff();
        return status;
    }

    public override void Shoot() {
        var target = CheckForTarget();
        var currentDamage = statsManager.getStat(StatsManager.StatTypes.Damage).Value;
        GameObject bulletFired = Instantiate(BulletPrefab, shootPoint.transform.position, shootPoint.transform.rotation);
        ProjectileBullet bullet = bulletFired.GetComponent<ProjectileBullet>();
        if (bullet != null) {
            bullet.SetStatus(createStatus());
            bullet.SetDamage(currentDamage);
            bullet.Seek(target.transform);
        }
    }
}
