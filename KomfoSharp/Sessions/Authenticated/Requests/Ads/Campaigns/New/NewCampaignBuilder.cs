// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NewCampaignBuilder.cs" company="Sitecore A/S">
//  Copyright (C) 2015 by Sitecore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace KomfoSharp.Sessions.Authenticated.Requests.Ads.Campaigns.New
{
  using KomfoSharp.Model;
  using KomfoSharp.Sessions.Authenticated.Requests.Ads.Campaigns.New.Fluent;
  using KomfoSharp.Sessions.Authenticated.Requests.Ads.Fluent;
  using KomfoSharp.Sessions.Fluent;

  /// <summary>
  /// Represents the new campaign builder.
  /// </summary>
  public class NewCampaignBuilder : INewCampaignBuilder, IExternalCampaignKeyCalled, IExternalCampaignIdCalled, INameCalled, IDescriptionCalled
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="NewCampaignBuilder"/> class.
    /// </summary>
    public NewCampaignBuilder()
    {
      this.Campaign = new Campaign();
    }

    /// <summary>
    /// Gets the campaign.
    /// </summary>
    /// <value>
    /// The campaign.
    /// </value>
    protected Campaign Campaign { get; private set; }

    /// <summary>
    /// Specifies the external campaign key.
    /// </summary>
    /// <param name="externalCampaignKey">The external campaign key.</param>
    /// <returns>
    /// The result of the call.
    /// </returns>
    public IExternalCampaignKeyCalled ExternalCampaignKey(string externalCampaignKey)
    {
      this.Campaign.ExtCampaignKey= externalCampaignKey;
      return this;
    }

    /// <summary>
    /// Specifies the external campaign ID.
    /// </summary>
    /// <param name="externalCampaignId">The external campaign identifier.</param>
    /// <returns>
    /// The result of the call.
    /// </returns>
    IExternalCampaignIdCalled IExternalCampaignIdCalling.ExternalCampaignId(string externalCampaignId)
    {
      this.Campaign.ExtCampaignId = externalCampaignId;
      return this;
    }

    /// <summary>
    /// Specifies the name.
    /// </summary>
    /// <param name="name">The name.</param>
    /// <returns>
    /// The result of the call.
    /// </returns>
    INameCalled INameCalling<INameCalled>.Name(string name)
    {
      this.Campaign.Name = name;
      return this;
    }

    /// <summary>
    /// Specifies the description.
    /// </summary>
    /// <param name="description">The description.</param>
    /// <returns>
    /// The result of the call.
    /// </returns>
    IDescriptionCalled IDescriptionCalling<IDescriptionCalled>.Description(string description)
    {
      this.Campaign.Description = description;
      return this;
    }

    /// <summary>
    /// Creates the new campaign.
    /// </summary>
    /// <returns>
    /// The new campaign.
    /// </returns>
    Campaign ICreateCalling<Campaign>.Create()
    {
      return this.Campaign;
    }
  }
}