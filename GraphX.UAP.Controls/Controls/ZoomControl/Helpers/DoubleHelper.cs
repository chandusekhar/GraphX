﻿/*************************************************************************************

   Extended WPF Toolkit

   Copyright (C) 2007-2013 Xceed Software Inc.

   This program is provided to you under the terms of the Microsoft Public
   License (Ms-PL) as published at http://wpftoolkit.codeplex.com/license 

   For more features, controls, and fast professional support,
   pick up the Plus Edition at http://xceed.com/wpf_toolkit

   Stay informed: follow @datagrid on Twitter or Like http://facebook.com/datagrids

  ***********************************************************************************/

using System;
using System.Runtime.InteropServices;
using GraphX.Measure;
using Point = Windows.Foundation.Point;
using Rect = Windows.Foundation.Rect;
using Size = Windows.Foundation.Size;

namespace GraphX.Controls
{
  internal static class DoubleHelper
  {
    public static bool AreVirtuallyEqual( double d1, double d2 )
    {
      if( double.IsPositiveInfinity( d1 ) )
        return double.IsPositiveInfinity( d2 );

      if( double.IsNegativeInfinity( d1 ) )
        return double.IsNegativeInfinity( d2 );

      if( IsNaN( d1 ) )
        return IsNaN( d2 );

      var n = d1 - d2;
      var d = ( Math.Abs( d1 ) + Math.Abs( d2 ) + 10 ) * 1.0e-15;
      return ( -d < n ) && ( d > n );
    }

    public static bool AreVirtuallyEqual( Size s1, Size s2 )
    {
      return ( AreVirtuallyEqual( s1.Width, s2.Width )
          && AreVirtuallyEqual( s1.Height, s2.Height ) );
    }

    public static bool AreVirtuallyEqual( Point p1, Point p2 )
    {
      return ( AreVirtuallyEqual( p1.X, p2.X )
          && AreVirtuallyEqual( p1.Y, p2.Y ) );
    }

    public static bool AreVirtuallyEqual( Rect r1, Rect r2 )
    {
      return ( AreVirtuallyEqual( r1.TopLeft(), r2.TopLeft() )
          && AreVirtuallyEqual( r1.BottomRight(), r2.BottomRight() ) );
    }

    public static bool AreVirtuallyEqual( Vector v1, Vector v2 )
    {
      return ( AreVirtuallyEqual( v1.X, v2.X )
          && AreVirtuallyEqual( v1.Y, v2.Y ) );
    }


    public static bool IsNaN( double value )
    {
      // used reflector to borrow the high performance IsNan function 
      // from the WPF MS.Internal namespace
      var t = new NanUnion {DoubleValue = value};

        var exp = t.UintValue & 0xfff0000000000000;
      var man = t.UintValue & 0x000fffffffffffff;

      return ( exp == 0x7ff0000000000000 || exp == 0xfff0000000000000 ) && ( man != 0 );
    }

    #region NanUnion Nested Types

    [StructLayout( LayoutKind.Explicit )]
    private struct NanUnion
    {
      [FieldOffset( 0 )]
      internal double DoubleValue;
      [FieldOffset( 0 )]
      internal UInt64 UintValue;
    }

    #endregion
  }
}
