// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ITokenCalling.cs" company="Sitecore A/S">
//  Copyright (C) 2015 by Sitecore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace KomfoSharp.Sessions.Authenticated.Fluent
{
  using KomfoSharp.Model;

  /// <summary>
  /// Defines the call to specify <see cref="Token"/> in the current session builder.
  /// </summary>
  public interface ITokenCalling
  {
    /// <summary>
    /// Specifies the <see cref="Token"/> in the current session builder.
    /// </summary>
    /// <param name="token">The token.</param>
    /// <returns>
    /// The current session builder.
    /// </returns>
    ITokenCalled Token(Token token);
  }
}