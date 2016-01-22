// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IAdsCalling.cs" company="Sitecore A/S">
//  Copyright (C) 2015 by Sitecore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace KomfoSharp.Sessions.Authenticated.Requests.Ads.Fluent
{
  /// <summary>
  /// Defines the call to ads requests.
  /// </summary>
  public interface IAdsCalling
  {
    /// <summary>
    /// Gets the ads requests builder.
    /// </summary>
    /// <value>
    /// The ads requests builder.
    /// </value>
    IAdsCalled Ads { get; }
  }
}