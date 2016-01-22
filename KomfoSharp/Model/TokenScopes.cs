// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TokenScopes.cs" company="Sitecore A/S">
//  Copyright (C) 2015 by Sitecore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace KomfoSharp.Model
{
  using System;
  using System.Runtime.Serialization;

  /// <summary>
  /// Defines the constants for the scopes that are allowed for the access token.
  /// </summary>
  [Flags]
  public enum TokenScopes
  {
    /// <summary>
    /// No scopes
    /// </summary>
    [EnumMember(Value = "")]
    None = 0,

    /// <summary>
    /// The twitter followers scope
    /// </summary>
    [EnumMember(Value = "twitter_followers")]
    TwitterFollowers = 1,

    /// <summary>
    /// The advertising scope
    /// </summary>
    [EnumMember(Value = "advertising")]
    Advertising = 2,

    /// <summary>
    /// All scopes
    /// </summary>
    [EnumMember(Value = "twitter_followers,advertising")]
    All = TwitterFollowers + Advertising
  }
}