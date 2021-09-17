#region File Description
//-----------------------------------------------------------------------------
// Animation.cs
//
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------
#endregion

using System;
using Microsoft.Xna.Framework.Graphics;

namespace UTAS
{

    class Animation
    {

        public Texture2D Texture
        {
            get { return texture; }
        }
        Texture2D texture;

        public float FrameTime
        {
            get { return frameTime; }
        }
        float frameTime;
        
        public bool IsLooping
        {
            get { return isLooping; }
        }
        bool isLooping;
        
        public int FrameCount
        {
            // Assume square frames.
            get { return Texture.Width / FrameHeight; }
        }
        
        public int FrameWidth
        {
            // Assume square frames.
            get { return Texture.Height; }
        }
        
        public int FrameHeight
        {
            get { return Texture.Height; }
        }

  
        public Animation(Texture2D texture, float frameTime, bool isLooping)
        {
            this.texture = texture;
            this.frameTime = frameTime;
            this.isLooping = isLooping;
        }
    }
}