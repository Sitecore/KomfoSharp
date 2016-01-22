// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ICustomAudiencesResponse.cs" company="Sitecore A/S">
//  Copyright (C) 2015 by Sitecore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace KomfoSharp.Sessions.Authenticated.Requests.Ads.CustomAudiences
{
  using System.Collections.Generic;

  /// <summary>
  /// Defines the response to the custom audiences request.
  /// </summary>
  public interface ICustomAudiencesResponse
  {
    /// <summary>
    /// Gets the data.
    /// </summary>
    /// <value>
    /// The data.
    /// </value>
    IEnumerable<Model.CustomAudience> Data { get; }
  }
}