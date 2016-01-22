// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CampaignCustomAudiencesEndpoint.cs" company="Sitecore A/S">
//  Copyright (C) 2015 by Sitecore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace KomfoSharp.Configuration.Endpoints
{
  using System;

  /// <summary>
  /// Represents the "komfoSharp/services/endpoints/campaigns/{campaign_id}/customaudiences" endpoint.
  /// </summary>
  [Serializable]
  public class CampaignCustomAudiencesEndpoint : EndpointBase
  {
    /// <summary>
    /// Defines the parameters names.
    /// </summary>
    public static class Parameters
    {
      /// <summary>
      /// The audience identifier parameter.
      /// </summary>
      public const string CampaignId = "campaign_id";
    }
  }
}