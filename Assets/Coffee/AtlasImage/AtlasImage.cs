﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

namespace Mobcast.Coffee.UIExtensions
{
	/// <summary>
	/// Atlas image.
	/// </summary>
	public class AtlasImage : Image
	{
		[SerializeField] private string m_SpriteName;
		[SerializeField] private SpriteAtlas m_SpriteAtlas;
		private string _lastSpriteName = "";


		/// <summary>スプライト名.アトラス内に同じ名前のSpriteがない場合、AtlasImageはデフォルトスプライトを表示します.</summary>
		public string spriteName
		{
			get { return m_SpriteName; }
			set
			{
				if (m_SpriteName != value)
				{
					m_SpriteName = value;
					SetAllDirty();
				}
			}
		}

		/// <summary>アトラス.AtlasMakerで作成されたアトラスアセットを取得・設定します.</summary>
		public SpriteAtlas spriteAtlas
		{
			get { return m_SpriteAtlas; }
			set
			{
				if (m_SpriteAtlas != value)
				{
					m_SpriteAtlas = value;
					SetAllDirty();
				}
			}
		}

		/// <summary>
		/// Sets the material dirty.
		/// </summary>
		public override void SetMaterialDirty()
		{
			//Animationからスプライト変更するための処理.
			//Animationやスクリプトによって、「sprite」を変更した場合、スプライト名に反映.
			if (_lastSpriteName == spriteName && sprite)
			{
				m_SpriteName = sprite.name.Replace("(Clone)", "");
			}

			if (_lastSpriteName != spriteName)
			{
				_lastSpriteName = spriteName;
				sprite = spriteAtlas ? spriteAtlas.GetSprite(spriteName) : null;
			}

			base.SetMaterialDirty();
		}


		protected AtlasImage()
			: base()
		{
		}

		/// <summary>
		/// Raises the populate mesh event.
		/// </summary>
		/// <param name="toFill">To fill.</param>
		protected override void OnPopulateMesh(VertexHelper toFill)
		{
			if (!overrideSprite)
			{
				toFill.Clear();
				return;
			}
			base.OnPopulateMesh(toFill);
		}
	}
}