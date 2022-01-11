using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FightEmulator
{
    public class BaseRole
    {
        #region Dynamic Data
        private BaseAttribute m_hp;
        private BaseAttribute m_attack;
        private BaseAttribute m_defence;
        #endregion

        #region Static Data
        private RoleConfiger roleCfg;
        #endregion

        #region Action

        /// <summary>
        /// 出生
        /// </summary>
        /// <param name="cfg"></param>
        /// <param name="pos"></param>
        protected virtual void Spawn(RoleConfiger cfg, Vector3 pos)
        {
        }

        /// <summary>
        /// 死亡
        /// </summary>
        protected virtual void Dead() { }

        /// <summary>
        /// 思考决策
        /// </summary>
        protected virtual void Think() { }

        /// <summary>
        /// 移动
        /// </summary>
        protected virtual void Move() { }

        /// <summary>
        /// 攻击
        /// </summary>
        protected virtual void Attack() { }

        /// <summary>
        /// 受伤
        /// </summary>
        protected virtual void BeInjured(int damages) { }
        #endregion
    }
}
