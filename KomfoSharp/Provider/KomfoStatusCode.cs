// --------------------------------------------------------------------------------------------------------------------
// <copyright file="KomfoStatusCode.cs" company="Sitecore A/S">
//  Copyright (C) 2015 by Sitecore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace KomfoSharp.Provider
{
  using System;
  using System.Net;

  /// <summary>
  /// Contains the values of error codes defined for Komfo API.
  /// </summary>
  [Serializable]
  public enum KomfoStatusCode
  {
    /// <summary>
    /// All API call must be performed over HTTPS. Any valid API call over HTTP would return <see cref="HttpStatusCode.BadRequest"/>.
    /// </summary>
    SecureConnection = 1,

    /// <summary>
    /// When invoking not existing API call - a <see cref="HttpStatusCode.NotFound"/> error message would be returned.
    /// </summary>
    InvalidApiCall = 2,

    /// <summary>
    /// Invoking an invalid HTTP method would return <see cref="HttpStatusCode.NotFound"/>.
    /// </summary>
    InvalidHttpMethod = 3,

    /// <summary>
    /// Invoking a method with invalid or missing required parameter will result in <see cref="HttpStatusCode.BadRequest"/>.
    /// </summary>
    MissingOrInvalidRequiredParameter = 4,

    /// <summary>
    /// Invoking an API method with invalid (or missing) <c>access_token</c> will result in <see cref="HttpStatusCode.Unauthorized"/>.
    /// </summary>
    InvalidAccessToken = 5,

    /// <summary>
    /// Invoking API many times exceeding the Komfo API rate limits will result in <c>(HttpStatusCode)429</c> (Too Many Requests).
    /// </summary>
    RateLimit = 6,

    /// <summary>
    /// When there is an internal issue this error would be returned <see cref="HttpStatusCode.InternalServerError"/>.
    /// </summary>
    InternalServerError = 7,

    /// <summary>
    /// Invoking "oauth20/tokens" endpoint with invalid <c>client_id</c> and <c>client_secret</c> will result in <see cref="HttpStatusCode.BadRequest"/>.
    /// </summary>
    InvalidClientCredentials = 50,

    /// <summary>
    /// If the limit of 100 handles in the request has been exceeded then <see cref="HttpStatusCode.BadRequest"/> is returned.
    /// </summary>
    MoreThan100TwitterHandles = 100,

    /// <summary>
    /// <c>since</c> parameter must be earlier than <c>until</c>: <see cref="HttpStatusCode.BadRequest"/>.
    /// </summary>
    SinceGreaterThanUntil = 101,

    /// <summary>
    /// Will result in <see cref="HttpStatusCode.BadRequest"/>.
    /// </summary>
    SinceAndUntilMustBeWithinTheLast30Days = 102,

    /// <summary>
    /// Will result in <see cref="HttpStatusCode.BadRequest"/>.
    /// </summary>
    AtLeastOneInvalidValueIsPassedInIds = 103,

    /// <summary>
    /// No active Ad Account in the client.
    /// </summary>
    ThereIsNoActiveAdAccountInClient = 200,

    /// <summary>
    /// The Facebook general error.
    /// </summary>
    FacebookGeneralError = 201,

    /// <summary>
    /// The ad account access token not valid
    /// </summary>
    AdAccountAccessTokenNotValid = 202,

    /// <summary>
    /// More than 5000 entries in a group.
    /// </summary>
    MoreThan5000EmailsInGroup = 231,

    /// <summary>
    /// The Custom Audience does not exist or you don't have access to it.
    /// </summary>
    AudienceIdIsNotValidCustomAudience = 232,

    /// <summary>
    /// When creating a Custom Audience with Facebook IDs the payload.app_id must be provided
    /// </summary>
    MissingFacebookApp = 233,

    /// <summary>
    /// The Custom Audience is participating in active advertisements and cannot be deleted
    /// </summary>
    CustomAudienceParticipateInActiveAdvertisements = 234,

    /// <summary>
    /// The Campaign does not exist or you don't have access to it.
    /// </summary>
    CampaignIdIsNotValidCampaign = 250,

    /// <summary>
    /// The Custom Audience already exists in this Campaign.
    /// </summary>
    CustomAudienceIsAddedMultipleTimesToCampaign = 251,

    /// <summary>
    /// The Custom Audience does not exist in this Campaign.
    /// </summary>
    CustomAudienceDoesNotExistInCampaignButIsRequestedToBeRemoved = 252
  }
}