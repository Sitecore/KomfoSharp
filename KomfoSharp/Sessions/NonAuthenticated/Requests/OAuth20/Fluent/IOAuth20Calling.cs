// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IOAuth20Calling.cs" company="Sitecore A/S">
//  Copyright (C) 2015 by Sitecore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace KomfoSharp.Sessions.NonAuthenticated.Requests.OAuth20.Fluent
{

  /// <summary>
  /// Defines the call to OAuth20 requests builder.
  /// </summary>
  public interface IOAuth20Calling
  {
    /// <summary>
    /// Gets the OAuth20 requests builder.
    /// </summary>
    /// <value>
    /// The OAuth20 requests builder.
    /// </value>
    IOAuth20Called OAuth20 { get; }
  }
}