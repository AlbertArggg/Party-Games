using UnityEngine;

namespace Other
{
    public class GlobalManagers : MonoBehaviour
    {
        public static GlobalManagers Instance { get; set; }
        
        [field: SerializeField] public NetworkRunnerController NetworkRunnerController { get; private set; }
        [SerializeField] private GameObject _ddol;
        
        private void Awake()
        {
            if(Instance == null) Instance = this;
            else Destroy(_ddol);
        }
    }
}