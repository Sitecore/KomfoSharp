// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Token.cs" company="Sitecore A/S">
//  Copyright (C) 2015 by Sitecore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace KomfoSharp.Model
{
  using System;
  using KomfoSharp.Provider.Json.Converters;
  using Newtonsoft.Json;

  /// <summary>
  /// Represents the token.
  /// </summary>
  [Serializable]
  public class Token
  {
    /// <summary>
    /// Gets or sets the access token.
    /// </summary>
    /// <value>
    /// The access token.
    /// </value>
    /// <remarks>
    /// Only alpha numeric characters allowed. Maximal size is 250 characters.
    /// </remarks>
    [JsonProperty("access_token")]
    public string AccessToken { get; set; }

    /// <summary>
    /// Gets or sets the expires in.
    /// </summary>
    /// <value>
    /// The expires in.
    /// </value>
    /// <remarks>
    /// Specifies the time the <see cref="AccessToken"/> is valid.
    /// </remarks>
    [JsonProperty("expires_in")]
    [JsonConverter(typeof(SecondsToTimeSpanConverter))]
    public TimeSpan ExpiresIn { get; set; }
  }
}