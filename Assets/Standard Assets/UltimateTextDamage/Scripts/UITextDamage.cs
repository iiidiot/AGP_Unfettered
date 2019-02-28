using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
#if UTD_TEXT_MESH_PRO
using TMPro;
#endif

namespace Guirao.UltimateTextDamage
{
    public class UITextDamage : MonoBehaviour
    {
        public event Action< UITextDamage , Transform > eventOnEnd;

#if UTD_TEXT_MESH_PRO
        public TextMeshProUGUI UsedLabel
#else
        public Text UsedLabel
#endif
        {
            get
            {
#if UTD_TEXT_MESH_PRO
                return labelTMP;
#else
                return label;
#endif
            }
        }

        [Tooltip("Does this item text will face the camera? (Only in world space)")]
        public bool autoFaceCamera = false;

        [ Header( "The Text ui of the item" )]
#if UTD_TEXT_MESH_PRO
        public TextMeshProUGUI labelTMP;
#else
        public Text label;
#endif


        public Canvas Canvas { get; set; }
        public float Offset { get; set; }
        public Camera Cam { get; set; }
        public float limitPercentTop { get; set; }
        public float limitPercentBottom { get; set; }
        public float limitPercentLeft { get; set; }
        public float limitPercentRight { get; set; }
        public bool followsTarget { get; set; }
        public Transform currentTransformFollow { get; set; }

        private RectTransform rectTransform;
        private Transform toFollow;
        private Vector3 initialPosition;
        private Quaternion initialRotation;

        private bool firstTime;

        /// <summary>
        /// Start Monobehaviour
        /// </summary>
        void Start( )
        {
            rectTransform = transform as RectTransform;
        }

        /// <summary>
        /// Shows the ui text
        /// </summary>
        /// <param name="text">string that will be filled</param>
        /// <param name="transform">transform in world space where the text will be positioned</param>
        public void Show( string text , Transform transform )
        {
            if( UsedLabel != null )
            {
                UsedLabel.text = text;
            }
            Offset = 0;
            toFollow = transform;
            firstTime = true;
            gameObject.SetActive( true );
        }

        private void OnValidate( )
        {
#if UTD_TEXT_MESH_PRO
            labelTMP = GetComponentInChildren<TextMeshProUGUI>( );
#else
            label =GetComponentInChildren<Text>( );
#endif
        }

        /// <summary>
        /// LateUpdate from Monobehaviour
        /// </summary>
        void LateUpdate( )
        {
            if( !toFollow ) return;

            UpdatePosition( );
        }

        /// <summary>
        /// Animation event, must call this at the end of the text animation.
        /// </summary>
        public void End( )
        {
            if( eventOnEnd != null ) eventOnEnd( this , currentTransformFollow );
            gameObject.SetActive( false );
        }

        /// <summary>
        /// Updates the position of the text
        /// </summary>
        private void UpdatePosition( )
        {
            Vector3 uiWorldPos = toFollow.position;
            Quaternion rot = toFollow.rotation;

            if( firstTime )
            {
                initialPosition = uiWorldPos;
                initialRotation = toFollow.rotation;
            }

            if( Canvas.renderMode == RenderMode.WorldSpace )
            {
                if( !followsTarget )
                {
                    uiWorldPos = initialPosition;
                    rot = initialRotation;
                }

                transform.position = uiWorldPos + Vector3.up * Offset;
                transform.rotation = rot;

                if( autoFaceCamera )
                {
                    Cam = Camera.main;
                    if( Cam != null )
                    {
                        transform.LookAt( Cam.transform , Vector3.up );
                    }
                    else
                    {
                        Debug.LogError( "There is no Camera assigned in the UltimateTextDamageManager." );
                    }
                }
                transform.localRotation *= Quaternion.Euler( 0 , 180 , 0 );
            }
            else
            {
                if( !followsTarget )
                {
                    uiWorldPos = initialPosition;
                }


                Vector2 screenPoint;

                screenPoint = Cam.WorldToScreenPoint( uiWorldPos );

                Vector2 output;
                RectTransformUtility.ScreenPointToLocalPointInRectangle( transform.parent as RectTransform , screenPoint + Vector2.up * Offset , Canvas.renderMode == RenderMode.ScreenSpaceOverlay ? null : Canvas.worldCamera , out output );
                rectTransform.anchoredPosition3D = output;
            }

            transform.SetAsLastSibling( );

            firstTime = false;
        }
    }
}
