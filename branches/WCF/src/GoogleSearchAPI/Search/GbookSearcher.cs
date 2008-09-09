﻿/**
 * GbookSearcher.cs
 *
 * Copyright (C) 2008,  iron9light
 *
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 * THE SOFTWARE.
 */

using System;
using System.Collections.Generic;

namespace Google.API.Search
{
    /// <summary>
    /// Utility class for Google Book Search service.
    /// </summary>
    public static class GbookSearcher
    {
        //private static int s_Timeout = 0;

        ///// <summary>
        ///// Get or set the length of time, in milliseconds, before the request times out.
        ///// </summary>
        //public static int Timeout
        //{
        //    get
        //    {
        //        return s_Timeout;
        //    }
        //    set
        //    {
        //        if (s_Timeout < 0)
        //        {
        //            throw new ArgumentOutOfRangeException("value");
        //        }
        //        s_Timeout = value;
        //    }
        //}

        internal static SearchData<GbookResult> GSearch(string keyword, int start, ResultSize resultSize, bool fullViewOnly, string library)
        {
            if (keyword == null)
            {
                throw new ArgumentNullException("keyword");
            }

            var responseData = SearchUtility.GetResponseData(
                service => service.BookSearch(
                               keyword,
                               resultSize.GetString(),
                               start,
                               fullViewOnly.GetString(),
                               library)
                );

            return responseData;
        }

        /// <summary>
        /// Search books.
        /// </summary>
        /// <param name="keyword">The keyword.</param>
        /// <param name="resultCount">The count of result itmes.</param>
        /// <returns>The result items.</returns>
        /// <remarks>Now, the max count of items Google given is <b>32</b>.</remarks>
        /// <example>
        /// This is the c# code example.
        /// <code>
        /// IList&lt;IBookResult&gt; results = GbookSearcher.Search("Grimm's Fairy Tales", 10);
        /// foreach(IBookResult result in results)
        /// {
        ///     Console.WriteLine("{0} [by {1} - {2} - {3} pages] {4}", result.Title, result.Authors, result.PublishedYear, result.PageCount, result.BookId);
        /// }
        /// </code>
        /// </example>
        public static IList<IBookResult> Search(string keyword, int resultCount)
        {
            return Search(keyword, resultCount, false, null);
        }

        /// <summary>
        /// Search books.
        /// </summary>
        /// <param name="keyword">The keyword.</param>
        /// <param name="resultCount">The count of result itmes.</param>
        /// <param name="fullViewOnly">Whether to restrict the search to "full view" books.</param>
        /// <returns>The result items.</returns>
        /// <remarks>Now, the max count of items Google given is <b>32</b>.</remarks>
        /// <example>
        /// This is the c# code example.
        /// <code>
        /// IList&lt;IBookResult&gt; results = GbookSearcher.Search("love", 4, true);
        /// foreach(IBookResult result in results)
        /// {
        ///     Console.WriteLine("{0} [by {1} - {2} - {3} pages] {4}", result.Title, result.Authors, result.PublishedYear, result.PageCount, result.BookId);
        /// }
        /// </code>
        /// </example>
        public static IList<IBookResult> Search(string keyword, int resultCount, bool fullViewOnly)
        {
            return Search(keyword, resultCount, fullViewOnly, null);
        }

        /// <summary>
        /// Search books.
        /// </summary>
        /// <param name="keyword">The keyword.</param>
        /// <param name="resultCount">The count of result itmes.</param>
        /// <param name="fullViewOnly">Whether to restrict the search to "full view" books.</param>
        /// <param name="library">The specified user-defined library. If it not null, the search will restrict the search to this library.</param>
        /// <returns>The result items.</returns>
        /// <remarks>Now, the max count of items Google given is <b>32</b>.</remarks>
        /// <example>
        /// This is the c# code example.
        /// <code>
        /// IList&lt;IBookResult&gt; results = GbookSearcher.Search("Cookbook", 32, fales, null);
        /// foreach(IBookResult result in results)
        /// {
        ///     Console.WriteLine("{0} [by {1} - {2} - {3} pages] {4}", result.Title, result.Authors, result.PublishedYear, result.PageCount, result.BookId);
        /// }
        /// </code>
        /// </example>
        public static IList<IBookResult> Search(string keyword, int resultCount, bool fullViewOnly, string library)
        {
            if (keyword == null)
            {
                throw new ArgumentNullException("keyword");
            }

            GSearchCallback<GbookResult> gsearch = (start, resultSize) => GSearch(keyword, start, resultSize, fullViewOnly, library);
            List<GbookResult> results = SearchUtility.Search(gsearch, resultCount);
            return results.ConvertAll<IBookResult>(item => (IBookResult)item);
        }
    }
}
