// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ITwitterCalling.cs" company="Sitecore A/S">
//  Copyright (C) 2015 by Sitecore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace KomfoSharp.Sessions.Authenticated.Requests.Twitter.Fluent
{
  /// <summary>
  /// Defines the call to Twitter requests.
  /// </summary>
  public interface ITwitterCalling
  {
    /// <summary>
    /// Gets the Twitter requests builder.
    /// </summary>
    /// <value>
    /// The Twitter requests builder.
    /// </value>
    ITwitterCalled Twitter { get; }
  }
}