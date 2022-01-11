using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FightEmulator
{
    /// <summary>
    /// 角色属性的基础类
    /// </summary>
    public class BaseAttribute
    {
        private long base_value;
        private long extra_value;
        private long base_factor;
        private long total_factor;

        public long BaseValue { get => base_value; set => base_value = value; }
        public long ExtraValue { get => extra_value; set => extra_value = value; }
        public long BaseFactor { get => base_factor; set => base_factor = value; }
        public long TotalFactor { get => total_factor; set => total_factor = value; }
        public long TotalValue { get => Mathf.FloorToInt((base_value * (1 + base_factor / GameConst.denominator) + extra_value) * (1 + total_factor / GameConst.denominator)); }
    }
}
