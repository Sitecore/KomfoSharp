// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ITokensCalling.cs" company="Sitecore A/S">
//  Copyright (C) 2015 by Sitecore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace KomfoSharp.Sessions.NonAuthenticated.Requests.OAuth20.Tokens.Fluent
{
  /// <summary>
  /// Defines the call to tokens request builder.
  /// </summary>
  public interface ITokensCalling
  {
    /// <summary>
    /// Gets the tokens request builder.
    /// </summary>
    /// <value>
    /// The tokens request builder.
    /// </value>
    ITokensCalled Tokens { get; }
  }
}