using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FightEmulator
{
    /// <summary>
    /// 一场战斗的抽象类
    /// 管理着：战斗的敌我双方的角色对象信息，战斗场地信息
    /// </summary>
    public class Fight
    {
        #region data
        private List<BaseRole> list_player_side;
        private List<BaseRole> list_enemy_side;
        #endregion
    }
}

