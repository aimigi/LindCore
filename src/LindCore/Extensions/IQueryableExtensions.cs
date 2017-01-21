//===================================================================================
// Microsoft Developer & Platform Evangelism
//=================================================================================== 
// THIS CODE AND INFORMATION ARE PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND, 
// EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE IMPLIED WARRANTIES 
// OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR PURPOSE.
//===================================================================================
// Copyright (c) Microsoft Corporation.  All Rights Reserved.
// This code is released under the terms of the MS-LPL license, 
// http://microsoftnlayerapp.codeplex.com/license
//===================================================================================
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Globalization;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace System.Linq
{

    /// <summary>
    /// Class for IQuerable extensions methods
    /// <remarks>
    /// Include method in IQueryable ( base contract for IObjectSet ) is 
    /// intended for mock Include method in ObjectQuery{T}.
    /// Paginate solve not parametrized queries issues with skip and take L2E methods
    /// </remarks>
    /// </summary>
    public static class IQueryableExtensions
    {
        /// <summary>
        /// 按或进行位运算
        /// 作者：仓储大叔
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="source"></param>
        /// <param name="selector"></param>
        /// <returns></returns>
        public static int BinaryOr<TSource>(this IEnumerable<TSource> source, Func<TSource, int> selector)
        {
            int result = 0;
            foreach (var item in source)
            {
                result |= selector(item);
            }
            return result;
        }
        /// <summary>
        /// 按或进行位运算
        /// 作者：仓储大叔
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="source"></param>
        /// <param name="selector"></param>
        /// <returns></returns>
        public static long BinaryOr<TSource>(this IEnumerable<TSource> source, Func<TSource, long> selector)
        {
            long result = 0;
            foreach (var item in source)
            {
                result |= selector(item);
            }
            return result;
        }
    }
}
