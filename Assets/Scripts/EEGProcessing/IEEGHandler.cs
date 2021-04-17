using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.EEGProcessing
{

    
    public abstract class IEEGHandler : MonoBehaviour
    {

        // return last recieved value for EEG parameter
        public abstract float GetCurrValue();

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }


}
