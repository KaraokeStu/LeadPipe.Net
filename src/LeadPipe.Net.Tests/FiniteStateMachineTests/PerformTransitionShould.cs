// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PerformTransitionShould.cs" company="Lead Pipe Software">
//   Copyright (c) Lead Pipe Software All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.Linq;
using LeadPipe.Net.FiniteStateMachine;
using Moq;
using NUnit.Framework;

namespace LeadPipe.Net.Tests.FiniteStateMachineTests
{
	/// <summary>
	/// PerformTransition method tests.
	/// </summary>
	[TestFixture]
	public class PerformTransitionShould
	{
		/// <summary>
		/// The machine.
		/// </summary>
		private FiniteStateMachine.FiniteStateMachine machine;

		/// <summary>
		/// The start transition.
		/// </summary>
		private IFiniteStateTransition startTransition;

		/// <summary>
		/// The open transition.
		/// </summary>
		private IFiniteStateTransition openTransition;

		/// <summary>
		/// The close transition.
		/// </summary>
		private IFiniteStateTransition closeTransition;

		/// <summary>
		/// The open state.
		/// </summary>
		private IFiniteState openState;

		/// <summary>
		/// The closed state.
		/// </summary>
		private IFiniteState closedState;

		#region Public Methods

		/// <summary>
		/// Tests to make sure that the current state changes when the transition is available.
		/// </summary>
		[Test]
		public void ChangeCurrentStateGivenAvailableTransition()
		{
			// Act
			this.machine.PerformTransition(this.closeTransition);

			// Assert
			Assert.IsTrue(this.machine.CurrentState.Name.Equals("Closed"));
		}

		/// <summary>
		/// Tests to make sure that state changes are captured in history.
		/// </summary>
		[Test]
		public void AddEntriesToHistory()
		{
			// Act
			this.machine.PerformTransition(this.closeTransition, "Close 1");
			this.machine.PerformTransition(this.openTransition, "Open 2");
			this.machine.PerformTransition(this.closeTransition, "Close 3");
			this.machine.PerformTransition(this.openTransition, "Open 4");
			this.machine.PerformTransition(this.closeTransition, "Close 5");
			this.machine.PerformTransition(this.openTransition, "Open 6");

			this.machine.History.Entries.ToList().ForEach(x => Debug.WriteLine(string.Format("{0}-{1}: {2} ({3}), {4}", x.EntryNumber, x.EntryDate, x.StateCode, x.ReasonCode, x.Comments)));

			// Assert
			Assert.IsTrue(this.machine.History.Entries.Count() == 7);
		}

		/// <summary>
		/// Tests to make sure that the current state changes when using a transition with a delegate.
		/// </summary>
		[Test]
		public void ChangeCurrentStateGivenTransitionWithDelegate()
		{
			// Arrange
			this.openState.RemoveTransition(this.closeTransition);

			this.closeTransition = new FiniteStateTransition(
				2,
				"Close",
				new FiniteStateMachineTransitionReason("2", "Closed"),
				this.closedState,
				() => this.closedState);

			this.openState.RegisterTransition(this.closeTransition);

			// Act
			this.machine.PerformTransition(this.closeTransition);

			// Assert
			Assert.IsTrue(this.machine.CurrentState.Name.Equals("Closed"));
		}

		/// <summary>
		/// Tests to make sure that the current state does not change if the transition says it can't transition.
		/// </summary>
		[Test]
		[ExpectedException(typeof(ApplicationException))]
		public void NotChangeStateGivenTransitionCanTransitionReturnsFalse()
		{
			// Arrange
			var mock = new Mock<IFiniteStateTransition>();

			mock.Setup(x => x.Name).Returns("AlwaysFalse");
			mock.Setup(x => x.Reason).Returns(new FiniteStateMachineTransitionReason("5", "Just Testing"));
			mock.Setup(x => x.EndState).Returns(this.closedState);
			mock.Setup(x => x.CanTransition()).Returns(false);

			this.openState.RemoveTransition(this.closeTransition);
			this.openState.RegisterTransition(mock.Object);

			// Act
			this.machine.PerformTransition(mock.Object);

			// Assert
			Assert.IsTrue(this.machine.CurrentState.Name.Equals("Open"));
		}

		/// <summary>
		/// Tests to make sure that the current state does not change if the transition says it can't transition via delegate.
		/// </summary>
		[Test]
		[ExpectedException(typeof(ApplicationException))]
		public void NotChangeStateGivenTransitionCanTransitionDelegateReturnsFalse()
		{
			// Arrange
			this.openState.RemoveTransition(this.closeTransition);

			this.closeTransition = new FiniteStateTransition(
				2,
				"Close",
				new FiniteStateMachineTransitionReason("2", "Closed"),
				this.closedState,
				() => this.closedState,
				() => false);

			this.openState.RegisterTransition(this.closeTransition);

			// Act
			this.machine.PerformTransition(this.closeTransition);

			// Assert
			Assert.IsTrue(this.machine.CurrentState.Name.Equals("Open"));
		}

		/// <summary>
		/// Tests to make sure that an exception is thrown when the transition returns a state that isn't registered.
		/// </summary>
		[Test]
		[ExpectedException(typeof(StateNotRegisteredException))]
		public void ThrowExceptionGivenTransitionReturnsUnregisteredState()
		{
			// Arrange
			var unregisteredState = new FiniteState(0, "Unregistered");

			this.openState.RemoveTransition(this.closeTransition);

			this.closeTransition = new FiniteStateTransition(
				2,
				"Close",
				new FiniteStateMachineTransitionReason("2", "Closed"),
				unregisteredState,
				() => unregisteredState);

			this.openState.RegisterTransition(this.closeTransition);

			// Act
			this.machine.PerformTransition(this.closeTransition);
		}

		/// <summary>
		/// Tests to make sure that an exception is thrown when the transition is not available.
		/// </summary>
		[Test]
		[ExpectedException(typeof(TransitionNotAvailableException))]
		public void ThrowExceptionGivenUnavailableTransition()
		{
			// Act
			this.machine.PerformTransition(this.openTransition);
		}

		/// <summary>
		/// Tests to make sure that an exception is thrown when the transition is null.
		/// </summary>
		[Test]
		[ExpectedException(typeof(ArgumentNullException))]
		[Ignore("Currently broken due to the generic implementation.")]
		public void ThrowExceptionGivenNullTransition()
		{
			// Act
			this.machine.PerformTransition((FiniteStateTransition)null);
		}

		#endregion

		/// <summary>
		/// Runs before each test.
		/// </summary>
		[SetUp]
		public void TestSetup()
		{
			// Create our states...
			this.openState = new FiniteState(0, "Open");
			this.closedState = new FiniteState(1, "Closed");

			// Create our transitions...
			this.startTransition = new FiniteStateTransition(0, "New", new FiniteStateMachineTransitionReason("0", "New"), this.openState);

			this.openTransition = new FiniteStateTransition(1, "Open", new FiniteStateMachineTransitionReason("1", "Opened"), this.closedState, this.openState);
			this.closeTransition = new FiniteStateTransition(2, "Close", new FiniteStateMachineTransitionReason("2", "Closed"), this.openState, this.closedState);

			// Add our transitions to our states...
			this.openState.RegisterTransition(this.closeTransition);
			this.closedState.RegisterTransition(this.openTransition);

			// Create our machine...
			this.machine = new FiniteStateMachine.FiniteStateMachine(this.startTransition);

			// Add our states...
			this.machine.RegisterState(this.openState);
			this.machine.RegisterState(this.closedState);
		}
	}
}