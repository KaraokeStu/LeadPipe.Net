﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StateNotRegisteredException.cs" company="Lead Pipe Software">
//   Copyright (c) Lead Pipe Software All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Globalization;

namespace LeadPipe.Net.FiniteStateMachine
{
	/// <summary>
	/// An base exception for the domain layer.
	/// </summary>
	[Serializable]
	public class StateNotRegisteredException : Exception
	{
		#region Constructors and Destructors

		/// <summary>
		/// Initializes a new instance of the <see cref="StateNotRegisteredException"/> class.
		/// </summary>
		public StateNotRegisteredException()
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="StateNotRegisteredException"/> class. 
		/// </summary>
		/// <param name="message">
		/// The exception message.
		/// </param>
		public StateNotRegisteredException(string message)
			: base(message)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="StateNotRegisteredException"/> class.
		/// </summary>
		/// <param name="message">
		/// The exception message.
		/// </param>
		/// <param name="innerException">
		/// The inner exception.
		/// </param>
		public StateNotRegisteredException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="StateNotRegisteredException"/> class.
		/// </summary>
		/// <param name="transition">
		/// The transition.
		/// </param>
		/// <param name="state">
		/// The state.
		/// </param>
		public StateNotRegisteredException(IFiniteStateTransition transition, IFiniteState state)
			: base(
				string.Format(
					CultureInfo.CurrentCulture, "The {0} transition returned an unregistered {1} state.", transition.Name, state.Name))
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="StateNotRegisteredException"/> class.
		/// </summary>
		/// <param name="transition">
		/// The transition.
		/// </param>
		/// <param name="state">
		/// The state.
		/// </param>
		/// <param name="innerException">
		/// The inner exception.
		/// </param>
		public StateNotRegisteredException(
			IFiniteStateTransition transition, IFiniteState state, Exception innerException)
			: base(
				string.Format(
					CultureInfo.CurrentCulture, "The {0} transition returned an unregistered {1} state.", transition.Name, state.Name),
				innerException)
		{
		}

		#endregion
	}
}