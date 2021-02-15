using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Extensions.Logging
{
    public static class LoggingEvents
    {
        // BASE
        public const int OperationRigthsError = 50;
        public const int EntityRightsError = 51;
        public const int BadRequestError = 400;
        public const int AuthorizationError = 401;
        // CRUD
        public const int GenerateItems = 1000;
        public const int ListItems = 1001;
        public const int GetItem = 1002;
        public const int InsertItem = 1003;
        public const int UpdateItem = 1004;
        public const int DeleteItem = 1005;
        public const int GetItemNotFound = 1400;
        public const int UpdateItemNotFound = 1401;

        // Ehealth
        public const int CommonEhealthServiceError = 4000;
        public const int EhealthInvalidClient = 4400;
        public const int EhealthNonRightError = 4401;
        public const int EhealthAccessDenied = 4402;
        public const int EhealthTokenExpired = 4403;
        public const int EhealthNotFoundError = 4404;
        public const int EhealthInvalidGrant = 4405;
        public const int EhealthValidationError = 4422;
        public const int EhealthRateLimitError = 4429;
        public const int EhealthServiceError = 4500;
        #region Services

        #region SynchronizationService

        // information
        public const int SyncServiceGetStarted = 9100;
        public const int SyncServiceGetFinished = 9101;
        public const int SyncServiceProcessingReceivedDataStarted = 9102;
        public const int SyncServiceProcessingReceivedDataFinished = 9103;
        public const int SyncServiceProcessingReceivedDataProgress = 9104;
        public const int SyncServiceGetPendingChangesStarted = 9110;
        public const int SyncServiceSendStarted = 9111;
        public const int SyncServiceSendFinished = 9112;
        // errors
        public const int CommonSyncServiceError = 9500;
        public const int SyncServiceGetError = 9501;
        public const int SyncServiceGetPendingChagnesError = 9511;
        public const int SyncServiceSendError = 9512;
        public const int SyncServiceAuthenticationError = 9513;
        public const int SyncServiceGetShortMessageError = 9514;
        public const int SyncServiceSetProcessedError = 9515;
        public const int SyncServicePostPendingChangesDataError = 9516;
        public const int SyncServiceGetMessageError = 9517;
        public const int SyncServiceProcessingDataError = 9518;

        #endregion SynchronizationService

        #region Ehealth Integration Service
        // information
        public const int EhealthIntegrationStarted = 10000;
        public const int EhealthIntegrationFinished = 10001;

        public const int EhealthDivisionIntegrationStarted = 10010;
        public const int EhealthEmployeeIntegrationStarted = 10011;
        public const int EhealthDeclarationRequestIntegrationStarted = 10012;
        public const int EhealthContractIntegrationStarted = 10013;
        public const int EhealthDeclarationIntegrationStarted = 10014;
        public const int EhealthHealthCareServicesIntegrationStarted = 10015;
        public const int EhealthPatientsIntegrationStarted = 10016;



        public const int EhealthIntegrationSyncDivisionRequested = 10020;
        public const int EhealthIntegrationSyncEmployeeRequested = 10021;
        public const int EhealthIntegrationSyncDeclarationRequestRequested = 10022;
        public const int EhealthIntegrationSynContractsRequested = 10023;
        public const int EhealthIntegrationSyncDeclationsRequested = 10024;
        public const int EhealthIntegrationSyncEhealthEmployeeCareServiceRequested = 10025;


        public const int EhealthIntegrationShortDivisionsSaveStarted = 10030;
        public const int EhealthIntegrationShortEmployessSaveStarted = 10031;
        public const int EhealthIntegrationShortDeclarationRequestSaveStarted = 10032;
        public const int EhealthIntegrationShortContractsSaveStarted = 10033;
        public const int EhealthIntegrationShortDeclarationsSaveStarted = 10034;
        public const int EhealthIntegrationShortEhealthEmployeeCareServicesSaveStarted = 10035;


        public const int EhealthIntegrationShortDivisionsSaveFinished = 10040;
        public const int EhealthIntegrationShortEmployeesSaveFinished = 10041;
        public const int EhealthIntegrationShortDeclarationRequestsSaveFinished = 10042;
        public const int EhealthIntegrationShortContractsSaveFinished = 10043;
        public const int EhealthIntegrationShortDeclarationsSaveFinished = 10044;


        public const int EhealthIntegrationFullDivisionsSaveStarted = 10050;
        public const int EhealthIntegrationFullEmployessSaveStarted = 10051;
        public const int EhealthIntegrationFullDeclarationRequestStarted = 10052;
        public const int EhealthIntegrationFullContractsSaveStarted = 10053;
        public const int EhealthIntegrationFullDeclarationsSaveStarted = 10054;


        public const int EhealthIntegrationFullDivisionsSaveFinished = 10060;
        public const int EhealthIntegrationFullEmployeesSaveFinished = 10061;
        public const int EhealthIntegrationFullRequestIntegrationFinished = 10062;
        public const int EhealthIntegrationFullContractsSaveFinished = 10063;
        public const int EhealthIntegrationFullDeclarationsSaveFinished = 10064;

        public const int EhealthDivisionIntegrationFinished = 100100;
        //warnings
        public const int EhealthIntegrationServiceDivisionSeedWarning = 10300;
        //errors
        public const int EhealthIntegrationServiceAuthError = 10600;
        public const int EhealthIntegrationServiceSyncRequestError = 10601;
        public const int EhealthIntegrationDivisionSeedError = 10602;
        #endregion

        #endregion Services
    }
}
