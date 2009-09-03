//-----------------------------------------------------------------------
// <copyright file="GvideoSearchClient.cs" company="iron9light">
// Copyright (c) 2009 iron9light
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
// </copyright>
// <author>iron9light@gmail.com</author>
//-----------------------------------------------------------------------

namespace Google.API.Search
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;

    /// <summary>
    /// The client for video search.
    /// </summary>
    public class GvideoSearchClient : GSearchClient
    {
        /// <summary>
        /// Search video.
        /// </summary>
        /// <param name="keyword">The keyword.</param>
        /// <param name="resultCount">The count of result itmes.</param>
        /// <returns>The result items.</returns>
        /// <remarks>Now, the max count of items Google given is <b>32</b>.</remarks>
        public IList<IVideoResult> Search(string keyword, int resultCount)
        {
            return this.Search(keyword, resultCount, SortType.GetDefault());
        }

        /// <summary>
        /// Search video.
        /// </summary>
        /// <param name="keyword">The keyword.</param>
        /// <param name="resultCount">The count of result itmes.</param>
        /// <param name="sortBy">The way to order results.</param>
        /// <returns>The result items.</returns>
        /// <remarks>Now, the max count of items Google given is <b>32</b>.</remarks>
        public IList<IVideoResult> Search(string keyword, int resultCount, [Optional] string sortBy)
        {
            if (keyword == null)
            {
                throw new ArgumentNullException("keyword");
            }

            GSearchCallback<GvideoResult> gsearch =
                (start, resultSize) => this.GSearch(keyword, start, resultSize, sortBy);
            var results = SearchUtility.Search(gsearch, resultCount);
            return results.ConvertAll(item => (IVideoResult)item);
        }

        internal SearchData<GvideoResult> GSearch(string keyword, int start, string resultSize, string sortBy)
        {
            if (keyword == null)
            {
                throw new ArgumentNullException("keyword");
            }

            var responseData =
                this.GetResponseData(
                    service =>
                    service.VideoSearch(
                        this.AcceptLanguage, this.ApiKey, keyword, resultSize, start, sortBy));
            return responseData;
        }
    }
}