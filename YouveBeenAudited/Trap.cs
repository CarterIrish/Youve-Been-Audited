using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System;

namespace YouveBeenAudited
{
    /// <summary>
    /// Type of traps
    /// </summary>
    public enum TrapType
    {
        Spikes,
        Glue,
        Bomb
    }

    /// <summary>
    /// Contains information all traps require.
    /// </summary>
    /// <seealso cref="YouveBeenAudited.GameObject" />
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
        /// Gets the damage amnt.
        /// </summary>
        /// <value>
        /// The damage amnt.
        /// </value>
        public int DamageAmnt { get => _damageAmnt; }

        /// <summary>
        /// Gets the cost of trap.
        /// </summary>
        /// <value>
        /// The cost.
        /// </value>
        public int Cost { get => _cost; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is active.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is active; otherwise, <c>false</c>.
        /// </value>
        public bool IsActive { get => _isActive; set => _isActive = value; }

        /// <summary>
        /// Gets the trap type.
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
        /// <param name="texture">The texture.</param>
        /// <param name="cost">The cost.</param>
        /// <param name="damageAmnt">The damage amnt.</param>
        /// <param name="tileHeight">Height of the tile.</param>
        public Trap(int x, int y, Texture2D texture, int cost, int damageAmnt, int tileHeight) : base(x, y, texture)
        {
            _damageAmnt = damageAmnt;
            _cost = cost;
            _position.Width = tileHeight;
            _position.Height = tileHeight;
        }

        /// <summary>
        /// Checks the collision between provided object and this instance.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns>True if collides with an object</returns>
        public bool CheckCollisions(GameObject obj)
        {
            if (obj is IDamageable && Position.Intersects(new Rectangle(obj.Position.X, obj.Position.Y, 55, 100)))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Does the effect of this trap onto character.
        /// </summary>
        /// <param name="e">The character.</param>
        public virtual void DoEffect(Character e)
        { }

        /// <summary>
        /// Resolves the collisions between trap and walls to keep trap in bounds.
        /// </summary>
        /// <param name="walls">The walls.</param>
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