using System;
using System.Collections.Generic;
using UnityEngine;

namespace Onoty3D.CharacterParticleAsset.Scripts
{
    [ExecuteInEditMode]
    [RequireComponent(typeof(ParticleSystem))]
    public class CharacterParticle : MonoBehaviour
    {
        #region Property

        [Tooltip("characters to emit")]
        [SerializeField]
        private string _text;

        public string Text
        {
            get
            {
                return this._text;
            }

            set
            {
                this._text = value;
                this._charactorIndex = 0;
            }
        }

        [SerializeField]
        private FontInformation _fontInformation;

        public FontInformation FontInformation
        {
            get
            {
                return this._fontInformation;
            }

            set
            {
                this._fontInformation = value;
            }
        }

        #endregion Property

        #region Field

        private ParticleSystem _particleSystem;
        private ParticleSystemRenderer _particleSystemRenderer;
        private int _charactorIndex = 0;

        private List<Vector4> _customData = new List<Vector4>();

        #endregion Field

        #region Event Function

        private void Start()
        {
            this._particleSystem = this.GetComponent<ParticleSystem>();
            this._particleSystemRenderer = this.GetComponent<ParticleSystemRenderer>();

#if UNITY_5_5
        this._particleSystemRenderer.EnableVertexStreams(ParticleSystemVertexStreams.Custom1);
        this._particleSystemRenderer.EnableVertexStreams(ParticleSystemVertexStreams.Custom2);
#else //Higher Unity 5.5
            this._particleSystemRenderer.SetActiveVertexStreams(
            new List<ParticleSystemVertexStream>(
            new ParticleSystemVertexStream[]
            {
                    ParticleSystemVertexStream.Position,
                    ParticleSystemVertexStream.Color,
                    ParticleSystemVertexStream.UV,
                    ParticleSystemVertexStream.Custom1XY,
                    ParticleSystemVertexStream.Custom1XYZW,
            }));
#endif
        }

        private void LateUpdate()
        {
            if (string.IsNullOrEmpty(this._text))
            {
                return;
            }

            if (this._fontInformation == null)
            {
                return;
            }

            var particleCount = this._particleSystem.particleCount;
            this._particleSystem.GetCustomParticleData(this._customData, ParticleSystemCustomData.Custom1);
            var data = default(Vector4);
            var detail = default(FontInformation.CharacterDetail);
            for (int i = 0; i < particleCount; i++)
            {
                data = this._customData[i];

                //新規発生パーティクルの場合のみ文字情報セット
                if (data.y == 0) //yの値を新規かどうかのフラグ代わりに
                {
                    this._charactorIndex %= this._text.Length;

                    //文字情報取得
                    this._fontInformation.TryGetCharacterDetail(this._text[this._charactorIndex], out detail);

                    if (detail != null)
                    {
                        this._customData[i] = detail.Area;
                    }

                    this._charactorIndex++;
                }
            }
            this._particleSystem.SetCustomParticleData(this._customData, ParticleSystemCustomData.Custom1);
        }

        #endregion Event Function

        #region Public Method

        [Obsolete("use Text property")]
        public void SetText(string text)
        {
            this._text = text;
        }

        [Obsolete("use FontInformation property")]
        public FontInformation GetFontInformation()
        {
            return this._fontInformation;
        }

        #endregion Public Method
    }
}