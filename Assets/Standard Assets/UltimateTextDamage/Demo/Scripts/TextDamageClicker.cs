using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Guirao.UltimateTextDamage
{
    public class TextDamageClicker : MonoBehaviour
    {
        public UltimateTextDamageManager textManager;
        public Transform overrideTransform;

        private void OnMouseUpAsButton( )
        {
            if( Random.value < 0.1f )
            {
                string randomStatus = "BLEEDING";
                int r = Random.Range( 0 , 2 );
                if( r == 0 )
                    randomStatus = "STUN";
                else
                    randomStatus = "CURSED";
                textManager.Add( randomStatus , overrideTransform != null ? overrideTransform : transform , "status" );
            }
            else
            {
                textManager.Add( Random.Range( 450f , 2000f ).ToString( "0" ) , overrideTransform != null ? overrideTransform : transform );
            }
            
        }

        public bool autoclicker = true;
        public float clickRate = 1;

        float lastTimeClick;
        private void Update( )
        {
            if( !autoclicker )
                return;

            if( Time.time - lastTimeClick >= 1f / clickRate )
            {
                lastTimeClick = Time.time; //+ Random.value * 0.5f;
                OnMouseUpAsButton( );
            }
        }
    }
}
