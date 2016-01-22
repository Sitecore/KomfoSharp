// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NewCustomAudienceRequest.cs" company="Sitecore A/S">
//  Copyright (C) 2015 by Sitecore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace KomfoSharp.Sessions.Authenticated.Requests.Ads.CustomAudiences.New
{
  using System;

  /// <summary>
  /// Represents the new custom audience request.
  /// </summary>
  [Serializable]
  public class NewCustomAudienceRequest : INewCustomAudienceRequest
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="NewCustomAudienceRequest"/> class.
    /// </summary>
    public NewCustomAudienceRequest()
    {
      this.Configuration = new NewCustomAudienceRequestConfiguration();
    }

    /// <summary>
    /// Gets the configuration.
    /// </summary>
    /// <value>
    /// The configuration.
    /// </value>
    public NewCustomAudienceRequestConfiguration Configuration { get; private set; }
  }
}