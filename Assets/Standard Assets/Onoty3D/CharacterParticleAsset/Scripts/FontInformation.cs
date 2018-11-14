using System;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

namespace Onoty3D.CharacterParticleAsset.Scripts
{
    [CreateAssetMenu(fileName = "fontInformation", menuName = "Onoty3D/Character Particle/Font Information")]
    public class FontInformation : ScriptableObject
    {
        #region CharacterInfo

        public class CharacterDetail
        {
            /// <summary>
            /// テクスチャ上の文字の矩形情報
            /// </summary>
            public Vector4 Area { get; set; }

            public CharacterDetail(XmlAttributeCollection attributes, int baseSize, int textureHeight)
            {
                var x = int.Parse(attributes.GetNamedItem("x").Value);
                var y = int.Parse(attributes.GetNamedItem("y").Value);
                var width = int.Parse(attributes.GetNamedItem("width").Value);
                var height = int.Parse(attributes.GetNamedItem("height").Value);
                y = textureHeight - (y + height);

                var xOffset = (baseSize - width) / 2;
                var yOffset = baseSize - (height + int.Parse(attributes.GetNamedItem("yoffset").Value));

                //テクスチャ上の文字の矩形情報
                var area = new Vector4
                    (
                        (x << 12) | y, //始点x, 始点y
                        (width << 12) | height, //幅, 高さ
                        (xOffset << 12) | yOffset,
                        1 / (float)baseSize
                    );
                this.Area = area;
            }
        }

        #endregion CharacterInfo

        #region Property

        [Tooltip("configuration file(xml)")]
        [SerializeField]
        private TextAsset _fontXml;

        public TextAsset FontXml
        {
            get
            {
                return this._fontXml;
            }

            set
            {
                this._fontXml = value;
                this.SetCharacterDic();
            }
        }

        #endregion Property

        #region Field

        private Dictionary<char, CharacterDetail> _characterDic = new Dictionary<char, CharacterDetail>();

        #endregion Field

        #region Event Function

        private void OnEnable()
        {
            this.SetCharacterDic();
        }

        private void OnValidate()
        {
            this.SetCharacterDic();
        }

        #endregion Event Function

        #region Public Method

        [Obsolete("use FontXml property")]
        public void SetXml(TextAsset xml)
        {
            this._fontXml = xml;
            this.SetCharacterDic();
        }

        public bool TryGetCharacterDetail(char character, out CharacterDetail detail)
        {
            return this._characterDic.TryGetValue(character, out detail);
        }

        #endregion Public Method

        #region Private Method

        private void SetCharacterDic()
        {
            this._characterDic.Clear();

            if (this._fontXml == null)
            {
                return;
            }

            //xmlの各node,attributeは存在することを前提で処理書きます

            var document = new XmlDocument();

            document.LoadXml(_fontXml.text);
            var common = document.GetElementsByTagName("common");
            var attributes = (common.Item(0) as XmlNode).Attributes;

            var baseSize = int.Parse(attributes.GetNamedItem("lineHeight").Value);

            var textureHeight = int.Parse(attributes.GetNamedItem("scaleH").Value);

            var characters = document.GetElementsByTagName("char");
            int id;
            foreach (XmlNode character in characters)
            {
                attributes = character.Attributes;
                id = int.Parse(attributes.GetNamedItem("id").Value);
                //Debug.Log((char)id);
                this._characterDic.Add((char)id, new CharacterDetail(attributes, baseSize, textureHeight));
            }
        }

        #endregion Private Method
    }
}