﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UserGrant.cs" company="Lead Pipe Software">
//   Copyright (c) Lead Pipe Software All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using LeadPipe.Net.Domain;
using LeadPipe.Net.Extensions;

namespace LeadPipe.Net.Authorization
{
    /// <summary>
	/// The user grant.
	/// </summary>
	public class UserGrant : PersistableObject<Guid>, IEntity, IValidatableObject
	{
		#region Public Properties

		/// <summary>
		/// Gets or sets the granted activity.
		/// </summary>
		public virtual Activity Activity { get; set; }

		/// <summary>
		/// Gets or sets the granted activity group.
		/// </summary>
		public virtual ActivityGroup ActivityGroup { get; set; }

		/// <summary>
		/// Gets the effective activities.
		/// </summary>
		public virtual IList<Activity> EffectiveActivities
		{
			get
			{
				var effectiveActivities = new List<Activity>();

				if (this.Activity != null)
				{
					effectiveActivities.Add(this.Activity);
				}
				else if (this.ActivityGroup.IsNotNull() && this.ActivityGroup.Activities.Any())
				{
					effectiveActivities.AddRange(this.ActivityGroup.Activities);
				}
				else if (this.Role.IsNotNull())
				{
					if (this.Role.Activities.Any())
					{
						effectiveActivities.AddRange(this.Role.Activities);
					}

					if (this.Role.ActivityGroups.Any())
					{
						foreach (ActivityGroup activityGroup in this.Role.ActivityGroups)
						{
							if (activityGroup.Activities.Any())
							{
								effectiveActivities.AddRange(activityGroup.Activities);
							}
						}
					}
				}

				return effectiveActivities.Distinct().ToList();
			}
		}

		/// <summary>
		/// Gets or sets the grant's expiration date.
		/// </summary>
		public virtual DateTime? ExpirationDate { get; set; }

		/// <summary>
		/// Gets or sets the granting user.
		/// </summary>
		public virtual string GrantingUser { get; set; }

		/// <summary>
		/// Gets the natural id.
		/// </summary>
		public virtual string Key
		{
			get
			{
				return this.Sid.ToString();
			}
		}

		/// <summary>
		/// Gets or sets the role.
		/// </summary>
		public virtual Role Role { get; set; }

		/// <summary>
		/// Gets or sets the user.
		/// </summary>
		[Required]
		public virtual User User { get; set; }

		#endregion

		#region Public Methods and Operators

		/// <summary>
		/// Determines whether the specified object is valid.
		/// </summary>
		/// <param name="validationContext">The validation context.</param>
		/// <returns>
		/// A collection that holds failed-validation information.
		/// </returns>
		public virtual IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
		{
			if (this.Activity.IsNull() && this.ActivityGroup.IsNull() && this.Role.IsNull())
			{
				yield return
					new ValidationResult("Either an Activity, Activity Group or a Role is required to create a user grant.");
			}
		}

		#endregion
	}
}