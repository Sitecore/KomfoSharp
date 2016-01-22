// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IWithHashingCalling.cs" company="Sitecore A/S">
//  Copyright (C) 2015 by Sitecore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace KomfoSharp.Sessions.Authenticated.Requests.Ads.CustomAudiences.CustomAudience.Users.Update.Fluent
{
  /// <summary>
  /// Defines the call to enable hashing.
  /// </summary>
  public interface IWithHashingCalling
  {
    /// <summary>
    /// Enables the hashing.
    /// </summary>
    /// <param name="isHashedAlready">if set to <c>true</c>; the data is hashed already; otherwise, the data will be hashed.</param>
    /// <returns>
    /// The result of the call.
    /// </returns>
    IWithHashingCalled WithHashing(bool isHashedAlready = false);
  }
}