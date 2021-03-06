// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ToFriendlyNameShould.cs" company="Lead Pipe Software">
//   Copyright (c) Lead Pipe Software All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using LeadPipe.Net.Extensions;
using NUnit.Framework;

namespace LeadPipe.Net.Tests.StringExtensionsTests
{
	/// <summary>
	/// StringExtensions ToFriendlyName tests.
	/// </summary>
	[TestFixture]
	public class ToFriendlyNameShould
	{
		#region Public Methods

		/// <summary>
		/// Tests to make sure a proper friendly name is returned.
		/// </summary>
		/// <param name="inputString">The input string.</param>
		/// <param name="friendlyName">The title case string.</param>
		[TestCase("ThisIsMyThing", "This Is My Thing")]
		[TestCase("APISpecification", "API Specification")]
		public void ReturnInputAsFriendlyName(string inputString, string friendlyName)
		{
			var convertedInput = inputString.ToFriendlyName();

			Assert.IsTrue(convertedInput.Equals(friendlyName));
		}

		#endregion
	}
}