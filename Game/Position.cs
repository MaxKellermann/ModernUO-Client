﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ClassicUO.Game
{
    public struct Position
    {
        public static Position Invalid = new Position(0xFFFF, 0xFFFF);

        private float _offsetX, _offsetY;

        public Position(in ushort x, in ushort y, in sbyte z = 0)
            : this()
        {
            X = x;
            Y = y;
            Z = z;

            _offsetX = _offsetY = OffsetZ = 0;
        }

        public ushort X { get; set; }
        public ushort Y { get; set; }
        public sbyte Z { get; set; }


        public float OffsetX { get => _offsetX % 1.0f; }
        public float OffsetY { get => _offsetY % 1.0f; }
        public float OffsetZ { get; set; }

        public void SetOffset(in float x, in float y, in float z)
        {
            _offsetX = x; _offsetY = y; OffsetZ = z;
        }

        public bool HasOffset => _offsetX != 0.0f || _offsetY != 0.0f || OffsetZ != 0.0f;

        public static bool operator ==(in Position p1, in Position p2) { return p1.X == p2.X && p1.Y == p2.Y && p1.Z == p2.Z; }
        public static bool operator !=(in Position p1, in Position p2) { return p1.X != p2.X || p1.Y != p2.Y || p1.Z != p2.Z; }
        public static Position operator +(in Position p1, in Position p2) { return new Position((ushort)(p1.X + p2.X), (ushort)(p1.Y + p2.Y), (sbyte)(p1.Z + p2.Z)); }
        public static Position operator -(in Position p1, in Position p2) { return new Position((ushort)(p1.X - p2.X), (ushort)(p1.Y - p2.Y), (sbyte)(p1.Z - p2.Z)); }
        public static Position operator *(in Position p1, in Position p2) { return new Position((ushort)(p1.X * p2.X), (ushort)(p1.Y * p2.Y), (sbyte)(p1.Z * p2.Z)); }
        public static Position operator /(in Position p1, in Position p2) { return new Position((ushort)(p1.X / p2.X), (ushort)(p1.Y / p2.Y), (sbyte)(p1.Z / p2.Z)); }

        public int DistanceTo(in Position position) { return Math.Max(Math.Abs(position.X - X), Math.Abs(position.Y - Y)); }
        public double DistanceToSqrt(in Position position)
        {
            int a = position.X - X;
            int b = position.Y - Y;
            return Math.Sqrt(a * a + b * b);
        }

        public override int GetHashCode() { return X ^ Y ^ Z; }
        public override bool Equals(object obj) { return obj is Position && this == (Position)obj; }
        public override string ToString() { return $"{X}.{Y}.{Z}"; }
        public static Position Parse(in string str)
        {
            string[] args = str.Split('.');
            return new Position(ushort.Parse(args[0]), ushort.Parse(args[1]), sbyte.Parse(args[2]));
        }
    }
}
