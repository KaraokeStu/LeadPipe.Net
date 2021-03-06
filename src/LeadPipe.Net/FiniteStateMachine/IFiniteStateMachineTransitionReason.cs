// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IFiniteStateMachineTransitionReason.cs" company="Lead Pipe Software">
//   Copyright (c) Lead Pipe Software All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;

namespace LeadPipe.Net.FiniteStateMachine
{
	/// <summary>
	/// The finite state machine transition reason.
	/// </summary>
	public interface IFiniteStateMachineTransitionReason : IComparable
	{
		#region Public Properties

		/// <summary>
		/// Gets or sets the code.
		/// </summary>
		string Code { get; set; }

		/// <summary>
		/// Gets or sets the description.
		/// </summary>
		string Description { get; set; }

		#endregion
	}
}