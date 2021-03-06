﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ISearcher.cs" company="Lead Pipe Software">
//   Copyright (c) Lead Pipe Software All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using Lucene.Net.Analysis.Standard;
using Lucene.Net.Index;
using Lucene.Net.Search;
using Lucene.Net.Store;
using Lucene.Net.Util;

namespace LeadPipe.Net.Lucene
{
    /// <summary>
    /// Clears the search index.
    /// </summary>
	public class SearchIndexClearer : ISearchIndexClearer
	{
        /// <summary>
        /// Clears the entire index.
        /// </summary>
        /// <param name="luceneVersion">The lucene version.</param>
        /// <param name="fsDirectory">The fs directory.</param>
        /// <param name="maxFieldLength">Maximum length of the field.</param>
		public virtual void ClearIndex(Version luceneVersion, FSDirectory fsDirectory, IndexWriter.MaxFieldLength maxFieldLength)
		{
			var analyzer = new StandardAnalyzer(luceneVersion);

			using (var indexWriter = new IndexWriter(fsDirectory, analyzer, maxFieldLength))
			{
				indexWriter.DeleteAll();

				analyzer.Close();
			}
		}

        /// <summary>
        /// Clears an item from the index.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="luceneVersion">The lucene version.</param>
        /// <param name="fsDirectory">The fs directory.</param>
        /// <param name="maxFieldLength">Maximum length of the field.</param>
        public void ClearIndex(string id, Version luceneVersion, FSDirectory fsDirectory, IndexWriter.MaxFieldLength maxFieldLength)
        {
            var analyzer = new StandardAnalyzer(luceneVersion);

            using (var indexWriter = new IndexWriter(fsDirectory, analyzer, maxFieldLength))
            {
                var searchQuery = new TermQuery(new Term("Key", id));

                indexWriter.DeleteDocuments(searchQuery);

                analyzer.Close();
            }
        }
	}
}