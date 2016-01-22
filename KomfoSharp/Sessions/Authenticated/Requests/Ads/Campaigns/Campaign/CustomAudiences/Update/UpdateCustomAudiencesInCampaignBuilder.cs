// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UpdateCustomAudiencesInCampaignBuilder.cs" company="Sitecore A/S">
//  Copyright (C) 2015 by Sitecore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace KomfoSharp.Sessions.Authenticated.Requests.Ads.Campaigns.Campaign.CustomAudiences.Update
{
  using KomfoSharp.Sessions.Authenticated.Requests.Ads.Campaigns.Campaign.CustomAudiences.Update.Fluent;
  using KomfoSharp.Sessions.Fluent;

  /// <summary>
  /// Represents the update custom audiences in campaign builder.
  /// </summary>
  public class UpdateCustomAudiencesInCampaignBuilder : IUpdateCustomAudiencesInCampaignBuilder, ICustomAudienceIdCalled
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateCustomAudiencesInCampaignBuilder"/> class.
    /// </summary>
    public UpdateCustomAudiencesInCampaignBuilder()
    {
      this.Configuration = new UpdateCustomAudiencesInCampaignConfiguration();
    }

    /// <summary>
    /// Gets the configuration.
    /// </summary>
    /// <value>
    /// The configuration.
    /// </value>
    protected UpdateCustomAudiencesInCampaignConfiguration Configuration { get; private set; }

    /// <summary>
    /// Specifies the custom audience ID.
    /// </summary>
    /// <param name="customAudienceId">The custom audience identifier.</param>
    /// <returns>
    /// The result of the call.
    /// </returns>
    public ICustomAudienceIdCalled CustomAudienceId(string customAudienceId)
    {
      this.Configuration.CustomAudienceId = customAudienceId;
      return this;
    }

    /// <summary>
    /// Creates the update campaign custom audiences configuration.
    /// </summary>
    /// <returns>
    /// The update campaign custom audiences configuration.
    /// </returns>
    UpdateCustomAudiencesInCampaignConfiguration ICreateCalling<UpdateCustomAudiencesInCampaignConfiguration>.Create()
    {
      return this.Configuration;
    }
  }
}