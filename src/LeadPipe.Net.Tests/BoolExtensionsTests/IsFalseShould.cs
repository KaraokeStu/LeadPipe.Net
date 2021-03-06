// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IsFalseShould.cs" company="Lead Pipe Software">
//   Copyright (c) Lead Pipe Software All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using LeadPipe.Net.Extensions;
using NUnit.Framework;

namespace LeadPipe.Net.Tests.BoolExtensionsTests
{
	/// <summary>
	/// BoolExtensions IsFalse tests.
	/// </summary>
	[TestFixture]
	public class IsFalseShould
	{
		#region Public Methods

		/// <summary>
		/// Tests to make sure true is returned if the Boolean value is false.
		/// </summary>
		[Test]
		public void ReturnTrueGivenBoolIsFalse()
		{
			var falseBool = false;

			Assert.IsTrue(falseBool.IsFalse());
		}

		/// <summary>
		/// Tests to make sure false is returned if the Boolean value is true.
		/// </summary>
		[Test]
		public void ReturnFalseGivenBoolIsFalse()
		{
			var trueBool = true;

			Assert.IsFalse(trueBool.IsFalse());
		}

		#endregion
	}
}