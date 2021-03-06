// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AdHocSpecification.cs" company="Lead Pipe Software">
//   Copyright (c) Lead Pipe Software All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Linq.Expressions;

namespace LeadPipe.Net.Specifications
{
	/// <summary>
	/// An implementation of an ad-hoc specification that can take criteria via the constructor.
	/// </summary>
	/// <remarks>
	/// <para>
	/// This technique was lifted from Microsoft Spain's implementation in their N-Layered .NET 4.0 Sample. Honestly,
	/// I'm not sure if I like it yet or not. Principally due to the fact that the specifications are now tucked away in
	/// a single static class and IoC is right out the window. Is it handy? I suppose. Is it good design? Hmmm...
	/// </para>
	/// </remarks>
	/// <typeparam name="T">
	/// The expression type.
	/// </typeparam>
	public class AdHocSpecification<T> : Specification<T>
	{
		#region Constants and Fields

		/// <summary>
		/// The matching expression.
		/// </summary>
		private readonly Expression<Func<T, bool>> matchingExpression;

		#endregion

		#region Constructors and Destructors

		/// <summary>
		/// Initializes a new instance of the <see cref="AdHocSpecification{T}"/> class.
		/// </summary>
		/// <param name="matchingExpression">
		/// The matching criteria.
		/// </param>
		public AdHocSpecification(Expression<Func<T, bool>> matchingExpression)
		{
			Guard.Will.ThrowArgumentNullException("matchingExpression").When(matchingExpression == null);

			this.matchingExpression = matchingExpression;
		}

		#endregion

		#region Public Methods

		/// <summary>
		/// Returns the satisfaction expression.
		/// </summary>
		/// <returns>
		/// The satisfaction expression.
		/// </returns>
		public override Expression<Func<T, bool>> SatisfiedBy()
		{
			return this.matchingExpression;
		}

		#endregion
	}
}