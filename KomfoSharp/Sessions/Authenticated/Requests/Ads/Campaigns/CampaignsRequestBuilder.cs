// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CampaignsRequestBuilder.cs" company="Sitecore A/S">
//  Copyright (C) 2015 by Sitecore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace KomfoSharp.Sessions.Authenticated.Requests.Ads.Campaigns
{
  using System;
  using KomfoSharp.Configuration.Providers;
  using KomfoSharp.Sessions.Authenticated.Requests.Ads.Campaigns.Campaign.CustomAudiences;
  using KomfoSharp.Sessions.Authenticated.Requests.Ads.Campaigns.Campaign.CustomAudiences.Fluent;
  using KomfoSharp.Sessions.Authenticated.Requests.Ads.Campaigns.Campaign.Fluent;
  using KomfoSharp.Sessions.Authenticated.Requests.Ads.Campaigns.Fluent;
  using KomfoSharp.Sessions.Authenticated.Requests.Ads.Campaigns.New;
  using KomfoSharp.Sessions.Authenticated.Requests.Ads.Campaigns.New.Fluent;
  using KomfoSharp.Sessions.Authenticated.Requests.Ads.Fluent;
  using KomfoSharp.Sessions.Fluent;

  /// <summary>
  /// Represents the campaigns request builder.
  /// </summary>
  public class CampaignsRequestBuilder : BaseRequestBuilder, ICampaignsRequestBuilder, ICampaignsCalled, Fluent.IWithPollingCalled, ICampaignIdCalled
  {
    /// <summary>
    /// The campaigns request
    /// </summary>
    private ICampaignsRequest campaignsRequest;

    /// <summary>
    /// Initializes a new instance of the <see cref="CampaignsRequestBuilder"/> class.
    /// </summary>
    /// <param name="configurationProvider">The configuration provider.</param>
    public CampaignsRequestBuilder(IConfigurationProvider configurationProvider)
      : base(configurationProvider)
    {
    }

    /// <summary>
    /// Gets the campaigns request.
    /// </summary>
    /// <value>
    /// The campaigns request.
    /// </value>
    protected ICampaignsRequest CampaignsRequest
    {
      get
      {
        return this.campaignsRequest ?? (this.campaignsRequest = new CampaignsRequest());
      }
    }

    /// <summary>
    /// Gets or sets the campaign identifier value.
    /// </summary>
    /// <value>
    /// The campaign identifier value.
    /// </value>
    protected string CampaignIdValue { get; set; }

    /// <summary>
    /// Gets the campaign custom audiences requests builder.
    /// </summary>
    /// <value>
    /// The campaign custom audiences requests builder.
    /// </value>
    ICustomAudiencesCalled ICustomAudiencesCalling<ICustomAudiencesCalled>.CustomAudiences
    {
      get
      {
        return new CampaignCustomAudiencesRequestBuilder(this.ConfigurationProvider, this.CampaignIdValue);
      }
    }

    /// <summary>
    /// Enables the polling.
    /// </summary>
    /// <param name="pollingSetupFunc">The polling setup function.</param>
    /// <returns>
    /// The result of the call.
    /// </returns>
    public Fluent.IWithPollingCalled WithPolling(Func<IWithPollingBuilder, ICreateCalling<PollingRequestConfiguration>> pollingSetupFunc = null)
    {
      this.CampaignsRequest.Configuration.Polling = this.BuildPollingRequestConfiguration(pollingSetupFunc);
      return this;
    }

    /// <summary>
    /// Creates the campaigns request.
    /// </summary>
    /// <returns>
    /// The campaigns request.
    /// </returns>
    public ICampaignsRequest Create()
    {
      return this.CampaignsRequest;
    }

    /// <summary>
    /// Specifies the campaign ID.
    /// </summary>
    /// <param name="campaignId">The campaign identifier.</param>
    /// <returns>The result of the call.</returns>
    public ICampaignIdCalled CampaignId(string campaignId)
    {
      this.CampaignIdValue = campaignId;
      return this;
    }

    /// <summary>
    /// Adds new entity.
    /// </summary>
    /// <param name="entitySetupFunc">The entity setup function.</param>
    /// <returns>
    /// The result of the call.
    /// </returns>
    public INewCalled New(Func<INewCampaignBuilder, ICreateCalling<Model.Campaign>> entitySetupFunc)
    {
      return new NewCampaignRequestBuilder(this.ConfigurationProvider, entitySetupFunc(new NewCampaignBuilder()).Create());
    }

    /// <summary>
    /// Adds new entity.
    /// </summary>
    /// <returns>
    /// The result of the call.
    /// </returns>
    public INewCalled New(Model.Campaign entity)
    {
      return new NewCampaignRequestBuilder(this.ConfigurationProvider, entity);
    }
  }
}