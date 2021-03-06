﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ISearchIndexClearer.cs" company="Lead Pipe Software">
//   Copyright (c) Lead Pipe Software All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using Lucene.Net.Index;
using Lucene.Net.Store;
using Lucene.Net.Util;

namespace LeadPipe.Net.Lucene
{
	/// <summary>
	/// Clears the search index.
	/// </summary>
	public interface ISearchIndexClearer
	{
        /// <summary>
        /// Clears the entire index.
        /// </summary>
        /// <param name="luceneVersion">The lucene version.</param>
        /// <param name="fsDirectory">The fs directory.</param>
        /// <param name="maxFieldLength">Maximum length of the field.</param>
		void ClearIndex(Version luceneVersion, FSDirectory fsDirectory, IndexWriter.MaxFieldLength maxFieldLength);

        /// <summary>
        /// Clears an item the index.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="luceneVersion">The lucene version.</param>
        /// <param name="fsDirectory">The fs directory.</param>
        /// <param name="maxFieldLength">Maximum length of the field.</param>
        void ClearIndex(string id, Version luceneVersion, FSDirectory fsDirectory, IndexWriter.MaxFieldLength maxFieldLength);
	}
}