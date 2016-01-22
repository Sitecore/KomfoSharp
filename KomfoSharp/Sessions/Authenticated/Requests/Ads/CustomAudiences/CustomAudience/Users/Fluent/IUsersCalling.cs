// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IUsersCalling.cs" company="Sitecore A/S">
//  Copyright (C) 2015 by Sitecore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace KomfoSharp.Sessions.Authenticated.Requests.Ads.CustomAudiences.CustomAudience.Users.Fluent
{
  /// <summary>
  /// Defines the call to the custom audience users requests.
  /// </summary>
  public interface IUsersCalling
  {
    /// <summary>
    /// Gets the users requests builder.
    /// </summary>
    /// <value>
    /// The users requests builder.
    /// </value>
    IUsersCalled Users { get; }
  }
}