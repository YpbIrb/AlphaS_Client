using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.EEGProcessing
{

    // Get eeg perameter values from OV scenary through TCP stream 
    public class OpenVibeEEGHandler : IEEGHandler
    {


        public override float GetCurrValue()
        {
            //Для тестировани, пока возвращает рандомные значения от 8 до 12
            float res = Random.Range(8.0f, 10.0f);

            return res;
            //throw new System.NotImplementedException();
        }

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