using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAbility
{
    void CastAbility(Vector3 targetPosition);
    int GetManaCost();
    void ShowAbilityIcon();
    void DisableAbilityIcon();
}
