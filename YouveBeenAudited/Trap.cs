using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System;
using System.Runtime.CompilerServices;

namespace YouveBeenAudited
{
    public enum TrapType
    {
        Spikes,
        Glue,
        Bomb
    }

    /// <summary>
    /// Purpose: To hold generic information that all traps have.
    /// </summary>
    internal class Trap : GameObject
    {
        #region Fields

        protected TrapType _type;

        protected int _damageAmnt;

        protected int _cost;
        protected bool _isActive;

        #endregion Fields

        #region Properties


        /// <summary>
        /// Gets the damage amount of trap.
        /// </summary>
        public int DamageAmnt { get => _damageAmnt; }

        /// <summary>
        /// Gets the cost of trap.
        /// </summary>
        public int Cost { get => _cost; }

        /// <summary>
        /// Gets or sets the active state of trap.
        /// </summary>
        public bool IsActive { get => _isActive; set => _isActive = value; }

        /// <summary>
        /// Gets the type of trap.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        public TrapType Type { get => _type; }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Initializes a new instance of the <see cref="Trap"/> class.
        /// </summary>
        /// <param name="x">The x coord.</param>
        /// <param name="y">The y coord.</param>
        /// <param name="texture">The texture of trap.</param>
        /// <param name="cost">The cost of trap.</param>
        /// <param name="damageAmnt">The damage amnt of trap.</param>
        public Trap(int x, int y, Texture2D texture, int cost, int damageAmnt, int tileHeight) : base(x, y, texture)
        {
            _damageAmnt = damageAmnt;
            _cost = cost;
            _position.Width = tileHeight;
            _position.Height = tileHeight;
        }

        /// <summary>
        /// Checks the collision with another game object
        /// </summary>
        /// <param name="obj">Object to check collisions with</param>
        /// <returns>True if collision detected</returns>
        public bool CheckCollisions(GameObject obj)
        {
            if (obj is IDamageable && Position.Intersects(new Rectangle(obj.Position.X, obj.Position.Y, 55, 100)))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Does the effect of a trap on another object.
        /// </summary>
        /// <param name="e">The object to perform effect on.</param>
        public virtual void DoEffect(Character e)
        { }

        public void ResolveCollisions(List<GameObject> walls)
        {
            List<Rectangle> intersections = new List<Rectangle>();
            Rectangle trapRect = new Rectangle(Position.X, Position.Y, Position.Width, Position.Height);
            Rectangle overlapRect;

            // Find the collisions
            foreach (GameObject wall in walls)
            {
                if (trapRect.Intersects(wall.Position))
                {
                    intersections.Add(wall.Position);
                }
            }

            // X collisions
            foreach (Rectangle r in intersections)
            {
                overlapRect = Rectangle.Intersect(trapRect, r);
                if (overlapRect.Height > overlapRect.Width)
                {
                    int xdiff = Math.Sign(trapRect.X - r.X);
                    trapRect.X += (xdiff * overlapRect.Width);
                }
            }

            // Y collisions
            foreach (Rectangle r in intersections)
            {
                overlapRect = Rectangle.Intersect(trapRect, r);
                if (overlapRect.Height < overlapRect.Width)
                {
                    int ydiff = Math.Sign(trapRect.Y - r.Y);
                    trapRect.Y += (ydiff * overlapRect.Height);
                }
            }

            _position.X = trapRect.X;
            _position.Y = trapRect.Y;
        }

        #endregion Methods
    }
}