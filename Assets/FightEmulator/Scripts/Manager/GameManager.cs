using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace FightEmulator
{
    public class GameManager : MonoBehaviour
    {
        private GameManager instance;
        public GameManager Instance { get => instance; }

        #region Unity Functions
        private void Awake()
        {
            instance = this;
        }
        #endregion
    }
}
