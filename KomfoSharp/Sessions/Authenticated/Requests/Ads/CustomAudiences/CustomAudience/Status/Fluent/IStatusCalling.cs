// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IStatusCalling.cs" company="Sitecore A/S">
//  Copyright (C) 2015 by Sitecore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace KomfoSharp.Sessions.Authenticated.Requests.Ads.CustomAudiences.CustomAudience.Status.Fluent
{
  /// <summary>
  /// Defines the call to the custom audience status request builder.
  /// </summary>
  public interface IStatusCalling
  {
    /// <summary>
    /// Gets the status request builder.
    /// </summary>
    /// <value>
    /// The status request builder.
    /// </value>
    IStatusCalled Status { get; }
  }
}