CREATE DATABASE IF NOT EXISTS OkAppDB;
-- dotnet ef migrations script > script.sql
-- https://stackoverflow.com/questions/39644544/can-i-generate-script-of-a-migration-with-ef-code-first-and-net-core
-- to generate the script
CREATE TABLE IF NOT EXISTS `__EFMigrationsHistory` (
    `MigrationId` varchar(95) NOT NULL,
    `ProductVersion` varchar(32) NOT NULL,
    CONSTRAINT `PK___EFMigrationsHistory` PRIMARY KEY (`MigrationId`)
);

ALTER DATABASE OkAppDB CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci;

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20190521025246_InitialDbCollation', '2.2.1-servicing-10028');

CREATE TABLE `AbpAuditLogs` (
    `Id` bigint NOT NULL AUTO_INCREMENT,
    `TenantId` int NULL,
    `UserId` bigint NULL,
    `ServiceName` varchar(256) NULL,
    `MethodName` varchar(256) NULL,
    `Parameters` varchar(1024) NULL,
    `ReturnValue` longtext NULL,
    `ExecutionTime` datetime(6) NOT NULL,
    `ExecutionDuration` int NOT NULL,
    `ClientIpAddress` varchar(64) NULL,
    `ClientName` varchar(128) NULL,
    `BrowserInfo` varchar(512) NULL,
    `Exception` varchar(2000) NULL,
    `ImpersonatorUserId` bigint NULL,
    `ImpersonatorTenantId` int NULL,
    `CustomData` varchar(2000) NULL,
    CONSTRAINT `PK_AbpAuditLogs` PRIMARY KEY (`Id`)
);

CREATE TABLE `AbpBackgroundJobs` (
    `Id` bigint NOT NULL AUTO_INCREMENT,
    `CreationTime` datetime(6) NOT NULL,
    `CreatorUserId` bigint NULL,
    `JobType` varchar(512) NOT NULL,
    `JobArgs` longtext NOT NULL,
    `TryCount` smallint NOT NULL,
    `NextTryTime` datetime(6) NOT NULL,
    `LastTryTime` datetime(6) NULL,
    `IsAbandoned` bit NOT NULL,
    `Priority` tinyint unsigned NOT NULL,
    CONSTRAINT `PK_AbpBackgroundJobs` PRIMARY KEY (`Id`)
);

CREATE TABLE `AbpEditions` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `CreationTime` datetime(6) NOT NULL,
    `CreatorUserId` bigint NULL,
    `LastModificationTime` datetime(6) NULL,
    `LastModifierUserId` bigint NULL,
    `IsDeleted` bit NOT NULL,
    `DeleterUserId` bigint NULL,
    `DeletionTime` datetime(6) NULL,
    `Name` varchar(32) NOT NULL,
    `DisplayName` varchar(64) NOT NULL,
    CONSTRAINT `PK_AbpEditions` PRIMARY KEY (`Id`)
);

CREATE TABLE `AbpEntityChangeSets` (
    `Id` bigint NOT NULL AUTO_INCREMENT,
    `BrowserInfo` varchar(512) NULL,
    `ClientIpAddress` varchar(64) NULL,
    `ClientName` varchar(128) NULL,
    `CreationTime` datetime(6) NOT NULL,
    `ExtensionData` longtext NULL,
    `ImpersonatorTenantId` int NULL,
    `ImpersonatorUserId` bigint NULL,
    `Reason` varchar(256) NULL,
    `TenantId` int NULL,
    `UserId` bigint NULL,
    CONSTRAINT `PK_AbpEntityChangeSets` PRIMARY KEY (`Id`)
);

CREATE TABLE `AbpLanguages` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `CreationTime` datetime(6) NOT NULL,
    `CreatorUserId` bigint NULL,
    `LastModificationTime` datetime(6) NULL,
    `LastModifierUserId` bigint NULL,
    `IsDeleted` bit NOT NULL,
    `DeleterUserId` bigint NULL,
    `DeletionTime` datetime(6) NULL,
    `TenantId` int NULL,
    `Name` varchar(10) NOT NULL,
    `DisplayName` varchar(64) NOT NULL,
    `Icon` varchar(128) NULL,
    `IsDisabled` bit NOT NULL,
    CONSTRAINT `PK_AbpLanguages` PRIMARY KEY (`Id`)
);

CREATE TABLE `AbpLanguageTexts` (
    `Id` bigint NOT NULL AUTO_INCREMENT,
    `CreationTime` datetime(6) NOT NULL,
    `CreatorUserId` bigint NULL,
    `LastModificationTime` datetime(6) NULL,
    `LastModifierUserId` bigint NULL,
    `TenantId` int NULL,
    `LanguageName` varchar(10) NOT NULL,
    `Source` varchar(128) NOT NULL,
    `Key` varchar(256) NOT NULL,
    `Value` longtext NOT NULL,
    CONSTRAINT `PK_AbpLanguageTexts` PRIMARY KEY (`Id`)
);

CREATE TABLE `AbpNotifications` (
    `Id` char(36) NOT NULL,
    `CreationTime` datetime(6) NOT NULL,
    `CreatorUserId` bigint NULL,
    `NotificationName` varchar(96) NOT NULL,
    `Data` longtext NULL,
    `DataTypeName` varchar(512) NULL,
    `EntityTypeName` varchar(250) NULL,
    `EntityTypeAssemblyQualifiedName` varchar(512) NULL,
    `EntityId` varchar(96) NULL,
    `Severity` tinyint unsigned NOT NULL,
    `UserIds` longtext NULL,
    `ExcludedUserIds` longtext NULL,
    `TenantIds` longtext NULL,
    CONSTRAINT `PK_AbpNotifications` PRIMARY KEY (`Id`)
);

CREATE TABLE `AbpNotificationSubscriptions` (
    `Id` char(36) NOT NULL,
    `CreationTime` datetime(6) NOT NULL,
    `CreatorUserId` bigint NULL,
    `TenantId` int NULL,
    `UserId` bigint NOT NULL,
    `NotificationName` varchar(96) NULL,
    `EntityTypeName` varchar(250) NULL,
    `EntityTypeAssemblyQualifiedName` varchar(512) NULL,
    `EntityId` varchar(96) NULL,
    CONSTRAINT `PK_AbpNotificationSubscriptions` PRIMARY KEY (`Id`)
);

CREATE TABLE `AbpOrganizationUnitRoles` (
    `Id` bigint NOT NULL AUTO_INCREMENT,
    `CreationTime` datetime(6) NOT NULL,
    `CreatorUserId` bigint NULL,
    `TenantId` int NULL,
    `RoleId` int NOT NULL,
    `OrganizationUnitId` bigint NOT NULL,
    `IsDeleted` bit NOT NULL,
    CONSTRAINT `PK_AbpOrganizationUnitRoles` PRIMARY KEY (`Id`)
);

CREATE TABLE `AbpOrganizationUnits` (
    `Id` bigint NOT NULL AUTO_INCREMENT,
    `CreationTime` datetime(6) NOT NULL,
    `CreatorUserId` bigint NULL,
    `LastModificationTime` datetime(6) NULL,
    `LastModifierUserId` bigint NULL,
    `IsDeleted` bit NOT NULL,
    `DeleterUserId` bigint NULL,
    `DeletionTime` datetime(6) NULL,
    `TenantId` int NULL,
    `ParentId` bigint NULL,
    `Code` varchar(95) NOT NULL,
    `DisplayName` varchar(128) NOT NULL,
    CONSTRAINT `PK_AbpOrganizationUnits` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_AbpOrganizationUnits_AbpOrganizationUnits_ParentId` FOREIGN KEY (`ParentId`) REFERENCES `AbpOrganizationUnits` (`Id`) ON DELETE RESTRICT
);

CREATE TABLE `AbpTenantNotifications` (
    `Id` char(36) NOT NULL,
    `CreationTime` datetime(6) NOT NULL,
    `CreatorUserId` bigint NULL,
    `TenantId` int NULL,
    `NotificationName` varchar(96) NOT NULL,
    `Data` longtext NULL,
    `DataTypeName` varchar(512) NULL,
    `EntityTypeName` varchar(250) NULL,
    `EntityTypeAssemblyQualifiedName` varchar(512) NULL,
    `EntityId` varchar(96) NULL,
    `Severity` tinyint unsigned NOT NULL,
    CONSTRAINT `PK_AbpTenantNotifications` PRIMARY KEY (`Id`)
);

CREATE TABLE `AbpUserAccounts` (
    `Id` bigint NOT NULL AUTO_INCREMENT,
    `CreationTime` datetime(6) NOT NULL,
    `CreatorUserId` bigint NULL,
    `LastModificationTime` datetime(6) NULL,
    `LastModifierUserId` bigint NULL,
    `IsDeleted` bit NOT NULL,
    `DeleterUserId` bigint NULL,
    `DeletionTime` datetime(6) NULL,
    `TenantId` int NULL,
    `UserId` bigint NOT NULL,
    `UserLinkId` bigint NULL,
    `UserName` varchar(256) NULL,
    `EmailAddress` varchar(256) NULL,
    CONSTRAINT `PK_AbpUserAccounts` PRIMARY KEY (`Id`)
);

CREATE TABLE `AbpUserLoginAttempts` (
    `Id` bigint NOT NULL AUTO_INCREMENT,
    `TenantId` int NULL,
    `TenancyName` varchar(64) NULL,
    `UserId` bigint NULL,
    `UserNameOrEmailAddress` varchar(255) NULL,
    `ClientIpAddress` varchar(64) NULL,
    `ClientName` varchar(128) NULL,
    `BrowserInfo` varchar(512) NULL,
    `Result` tinyint unsigned NOT NULL,
    `CreationTime` datetime(6) NOT NULL,
    CONSTRAINT `PK_AbpUserLoginAttempts` PRIMARY KEY (`Id`)
);

CREATE TABLE `AbpUserNotifications` (
    `Id` char(36) NOT NULL,
    `TenantId` int NULL,
    `UserId` bigint NOT NULL,
    `TenantNotificationId` char(36) NOT NULL,
    `State` int NOT NULL,
    `CreationTime` datetime(6) NOT NULL,
    CONSTRAINT `PK_AbpUserNotifications` PRIMARY KEY (`Id`)
);

CREATE TABLE `AbpUserOrganizationUnits` (
    `Id` bigint NOT NULL AUTO_INCREMENT,
    `CreationTime` datetime(6) NOT NULL,
    `CreatorUserId` bigint NULL,
    `TenantId` int NULL,
    `UserId` bigint NOT NULL,
    `OrganizationUnitId` bigint NOT NULL,
    `IsDeleted` bit NOT NULL,
    CONSTRAINT `PK_AbpUserOrganizationUnits` PRIMARY KEY (`Id`)
);

CREATE TABLE `AbpUsers` (
    `Id` bigint NOT NULL AUTO_INCREMENT,
    `CreationTime` datetime(6) NOT NULL,
    `CreatorUserId` bigint NULL,
    `LastModificationTime` datetime(6) NULL,
    `LastModifierUserId` bigint NULL,
    `IsDeleted` bit NOT NULL,
    `DeleterUserId` bigint NULL,
    `DeletionTime` datetime(6) NULL,
    `AuthenticationSource` varchar(64) NULL,
    `UserName` varchar(256) NOT NULL,
    `TenantId` int NULL,
    `EmailAddress` varchar(256) NOT NULL,
    `Name` varchar(64) NOT NULL,
    `Surname` varchar(64) NOT NULL,
    `Password` varchar(128) NOT NULL,
    `EmailConfirmationCode` varchar(328) NULL,
    `PasswordResetCode` varchar(328) NULL,
    `LockoutEndDateUtc` datetime(6) NULL,
    `AccessFailedCount` int NOT NULL,
    `IsLockoutEnabled` bit NOT NULL,
    `PhoneNumber` varchar(32) NULL,
    `IsPhoneNumberConfirmed` bit NOT NULL,
    `SecurityStamp` varchar(128) NULL,
    `IsTwoFactorEnabled` bit NOT NULL,
    `IsEmailConfirmed` bit NOT NULL,
    `IsActive` bit NOT NULL,
    `NormalizedUserName` varchar(256) NOT NULL,
    `NormalizedEmailAddress` varchar(256) NOT NULL,
    `ConcurrencyStamp` varchar(128) NULL,
    CONSTRAINT `PK_AbpUsers` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_AbpUsers_AbpUsers_CreatorUserId` FOREIGN KEY (`CreatorUserId`) REFERENCES `AbpUsers` (`Id`) ON DELETE RESTRICT,
    CONSTRAINT `FK_AbpUsers_AbpUsers_DeleterUserId` FOREIGN KEY (`DeleterUserId`) REFERENCES `AbpUsers` (`Id`) ON DELETE RESTRICT,
    CONSTRAINT `FK_AbpUsers_AbpUsers_LastModifierUserId` FOREIGN KEY (`LastModifierUserId`) REFERENCES `AbpUsers` (`Id`) ON DELETE RESTRICT
);

CREATE TABLE `AbpFeatures` (
    `Id` bigint NOT NULL AUTO_INCREMENT,
    `CreationTime` datetime(6) NOT NULL,
    `CreatorUserId` bigint NULL,
    `TenantId` int NULL,
    `Name` varchar(128) NOT NULL,
    `Value` varchar(2000) NOT NULL,
    `Discriminator` longtext NOT NULL,
    `EditionId` int NULL,
    CONSTRAINT `PK_AbpFeatures` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_AbpFeatures_AbpEditions_EditionId` FOREIGN KEY (`EditionId`) REFERENCES `AbpEditions` (`Id`) ON DELETE CASCADE
);

CREATE TABLE `AbpEntityChanges` (
    `Id` bigint NOT NULL AUTO_INCREMENT,
    `ChangeTime` datetime(6) NOT NULL,
    `ChangeType` tinyint unsigned NOT NULL,
    `EntityChangeSetId` bigint NOT NULL,
    `EntityId` varchar(48) NULL,
    `EntityTypeFullName` varchar(192) NULL,
    `TenantId` int NULL,
    CONSTRAINT `PK_AbpEntityChanges` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_AbpEntityChanges_AbpEntityChangeSets_EntityChangeSetId` FOREIGN KEY (`EntityChangeSetId`) REFERENCES `AbpEntityChangeSets` (`Id`) ON DELETE CASCADE
);

CREATE TABLE `AbpRoles` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `CreationTime` datetime(6) NOT NULL,
    `CreatorUserId` bigint NULL,
    `LastModificationTime` datetime(6) NULL,
    `LastModifierUserId` bigint NULL,
    `IsDeleted` bit NOT NULL,
    `DeleterUserId` bigint NULL,
    `DeletionTime` datetime(6) NULL,
    `TenantId` int NULL,
    `Name` varchar(32) NOT NULL,
    `DisplayName` varchar(64) NOT NULL,
    `IsStatic` bit NOT NULL,
    `IsDefault` bit NOT NULL,
    `NormalizedName` varchar(32) NOT NULL,
    `ConcurrencyStamp` varchar(128) NULL,
    `Description` longtext NULL,
    CONSTRAINT `PK_AbpRoles` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_AbpRoles_AbpUsers_CreatorUserId` FOREIGN KEY (`CreatorUserId`) REFERENCES `AbpUsers` (`Id`) ON DELETE RESTRICT,
    CONSTRAINT `FK_AbpRoles_AbpUsers_DeleterUserId` FOREIGN KEY (`DeleterUserId`) REFERENCES `AbpUsers` (`Id`) ON DELETE RESTRICT,
    CONSTRAINT `FK_AbpRoles_AbpUsers_LastModifierUserId` FOREIGN KEY (`LastModifierUserId`) REFERENCES `AbpUsers` (`Id`) ON DELETE RESTRICT
);

CREATE TABLE `AbpSettings` (
    `Id` bigint NOT NULL AUTO_INCREMENT,
    `CreationTime` datetime(6) NOT NULL,
    `CreatorUserId` bigint NULL,
    `LastModificationTime` datetime(6) NULL,
    `LastModifierUserId` bigint NULL,
    `TenantId` int NULL,
    `UserId` bigint NULL,
    `Name` varchar(256) NOT NULL,
    `Value` varchar(2000) NULL,
    CONSTRAINT `PK_AbpSettings` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_AbpSettings_AbpUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `AbpUsers` (`Id`) ON DELETE RESTRICT
);

CREATE TABLE `AbpTenants` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `CreationTime` datetime(6) NOT NULL,
    `CreatorUserId` bigint NULL,
    `LastModificationTime` datetime(6) NULL,
    `LastModifierUserId` bigint NULL,
    `IsDeleted` bit NOT NULL,
    `DeleterUserId` bigint NULL,
    `DeletionTime` datetime(6) NULL,
    `TenancyName` varchar(64) NOT NULL,
    `Name` varchar(128) NOT NULL,
    `ConnectionString` varchar(1024) NULL,
    `IsActive` bit NOT NULL,
    `EditionId` int NULL,
    CONSTRAINT `PK_AbpTenants` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_AbpTenants_AbpUsers_CreatorUserId` FOREIGN KEY (`CreatorUserId`) REFERENCES `AbpUsers` (`Id`) ON DELETE RESTRICT,
    CONSTRAINT `FK_AbpTenants_AbpUsers_DeleterUserId` FOREIGN KEY (`DeleterUserId`) REFERENCES `AbpUsers` (`Id`) ON DELETE RESTRICT,
    CONSTRAINT `FK_AbpTenants_AbpEditions_EditionId` FOREIGN KEY (`EditionId`) REFERENCES `AbpEditions` (`Id`) ON DELETE RESTRICT,
    CONSTRAINT `FK_AbpTenants_AbpUsers_LastModifierUserId` FOREIGN KEY (`LastModifierUserId`) REFERENCES `AbpUsers` (`Id`) ON DELETE RESTRICT
);

CREATE TABLE `AbpUserClaims` (
    `Id` bigint NOT NULL AUTO_INCREMENT,
    `CreationTime` datetime(6) NOT NULL,
    `CreatorUserId` bigint NULL,
    `TenantId` int NULL,
    `UserId` bigint NOT NULL,
    `ClaimType` varchar(256) NULL,
    `ClaimValue` longtext NULL,
    CONSTRAINT `PK_AbpUserClaims` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_AbpUserClaims_AbpUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `AbpUsers` (`Id`) ON DELETE CASCADE
);

CREATE TABLE `AbpUserLogins` (
    `Id` bigint NOT NULL AUTO_INCREMENT,
    `TenantId` int NULL,
    `UserId` bigint NOT NULL,
    `LoginProvider` varchar(128) NOT NULL,
    `ProviderKey` varchar(256) NOT NULL,
    CONSTRAINT `PK_AbpUserLogins` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_AbpUserLogins_AbpUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `AbpUsers` (`Id`) ON DELETE CASCADE
);

CREATE TABLE `AbpUserRoles` (
    `Id` bigint NOT NULL AUTO_INCREMENT,
    `CreationTime` datetime(6) NOT NULL,
    `CreatorUserId` bigint NULL,
    `TenantId` int NULL,
    `UserId` bigint NOT NULL,
    `RoleId` int NOT NULL,
    CONSTRAINT `PK_AbpUserRoles` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_AbpUserRoles_AbpUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `AbpUsers` (`Id`) ON DELETE CASCADE
);

CREATE TABLE `AbpUserTokens` (
    `Id` bigint NOT NULL AUTO_INCREMENT,
    `TenantId` int NULL,
    `UserId` bigint NOT NULL,
    `LoginProvider` varchar(128) NULL,
    `Name` varchar(128) NULL,
    `Value` varchar(512) NULL,
    `ExpireDate` datetime(6) NULL,
    CONSTRAINT `PK_AbpUserTokens` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_AbpUserTokens_AbpUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `AbpUsers` (`Id`) ON DELETE CASCADE
);

CREATE TABLE `AbpEntityPropertyChanges` (
    `Id` bigint NOT NULL AUTO_INCREMENT,
    `EntityChangeId` bigint NOT NULL,
    `NewValue` varchar(512) NULL,
    `OriginalValue` varchar(512) NULL,
    `PropertyName` varchar(96) NULL,
    `PropertyTypeFullName` varchar(192) NULL,
    `TenantId` int NULL,
    CONSTRAINT `PK_AbpEntityPropertyChanges` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_AbpEntityPropertyChanges_AbpEntityChanges_EntityChangeId` FOREIGN KEY (`EntityChangeId`) REFERENCES `AbpEntityChanges` (`Id`) ON DELETE CASCADE
);

CREATE TABLE `AbpPermissions` (
    `Id` bigint NOT NULL AUTO_INCREMENT,
    `CreationTime` datetime(6) NOT NULL,
    `CreatorUserId` bigint NULL,
    `TenantId` int NULL,
    `Name` varchar(128) NOT NULL,
    `IsGranted` bit NOT NULL,
    `Discriminator` longtext NOT NULL,
    `RoleId` int NULL,
    `UserId` bigint NULL,
    CONSTRAINT `PK_AbpPermissions` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_AbpPermissions_AbpRoles_RoleId` FOREIGN KEY (`RoleId`) REFERENCES `AbpRoles` (`Id`) ON DELETE CASCADE,
    CONSTRAINT `FK_AbpPermissions_AbpUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `AbpUsers` (`Id`) ON DELETE CASCADE
);

CREATE TABLE `AbpRoleClaims` (
    `Id` bigint NOT NULL AUTO_INCREMENT,
    `CreationTime` datetime(6) NOT NULL,
    `CreatorUserId` bigint NULL,
    `TenantId` int NULL,
    `RoleId` int NOT NULL,
    `ClaimType` varchar(256) NULL,
    `ClaimValue` longtext NULL,
    CONSTRAINT `PK_AbpRoleClaims` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_AbpRoleClaims_AbpRoles_RoleId` FOREIGN KEY (`RoleId`) REFERENCES `AbpRoles` (`Id`) ON DELETE CASCADE
);

CREATE INDEX `IX_AbpAuditLogs_TenantId_ExecutionDuration` ON `AbpAuditLogs` (`TenantId`, `ExecutionDuration`);

CREATE INDEX `IX_AbpAuditLogs_TenantId_ExecutionTime` ON `AbpAuditLogs` (`TenantId`, `ExecutionTime`);

CREATE INDEX `IX_AbpAuditLogs_TenantId_UserId` ON `AbpAuditLogs` (`TenantId`, `UserId`);

CREATE INDEX `IX_AbpBackgroundJobs_IsAbandoned_NextTryTime` ON `AbpBackgroundJobs` (`IsAbandoned`, `NextTryTime`);

CREATE INDEX `IX_AbpEntityChanges_EntityChangeSetId` ON `AbpEntityChanges` (`EntityChangeSetId`);

CREATE INDEX `IX_AbpEntityChanges_EntityTypeFullName_EntityId` ON `AbpEntityChanges` (`EntityTypeFullName`, `EntityId`);

CREATE INDEX `IX_AbpEntityChangeSets_TenantId_CreationTime` ON `AbpEntityChangeSets` (`TenantId`, `CreationTime`);

CREATE INDEX `IX_AbpEntityChangeSets_TenantId_Reason` ON `AbpEntityChangeSets` (`TenantId`, `Reason`);

CREATE INDEX `IX_AbpEntityChangeSets_TenantId_UserId` ON `AbpEntityChangeSets` (`TenantId`, `UserId`);

CREATE INDEX `IX_AbpEntityPropertyChanges_EntityChangeId` ON `AbpEntityPropertyChanges` (`EntityChangeId`);

CREATE INDEX `IX_AbpFeatures_EditionId_Name` ON `AbpFeatures` (`EditionId`, `Name`);

CREATE INDEX `IX_AbpFeatures_TenantId_Name` ON `AbpFeatures` (`TenantId`, `Name`);

CREATE INDEX `IX_AbpLanguages_TenantId_Name` ON `AbpLanguages` (`TenantId`, `Name`);

CREATE INDEX `IX_AbpLanguageTexts_TenantId_Source_LanguageName_Key` ON `AbpLanguageTexts` (`TenantId`, `Source`, `LanguageName`, `Key`);

CREATE INDEX `IX_AbpNotificationSubscriptions_NotificationName_EntityTypeName~` ON `AbpNotificationSubscriptions` (`NotificationName`, `EntityTypeName`, `EntityId`, `UserId`);

CREATE INDEX `IX_AbpNotificationSubscriptions_TenantId_NotificationName_Entit~` ON `AbpNotificationSubscriptions` (`TenantId`, `NotificationName`, `EntityTypeName`, `EntityId`, `UserId`);

CREATE INDEX `IX_AbpOrganizationUnitRoles_TenantId_OrganizationUnitId` ON `AbpOrganizationUnitRoles` (`TenantId`, `OrganizationUnitId`);

CREATE INDEX `IX_AbpOrganizationUnitRoles_TenantId_RoleId` ON `AbpOrganizationUnitRoles` (`TenantId`, `RoleId`);

CREATE INDEX `IX_AbpOrganizationUnits_ParentId` ON `AbpOrganizationUnits` (`ParentId`);

CREATE INDEX `IX_AbpOrganizationUnits_TenantId_Code` ON `AbpOrganizationUnits` (`TenantId`, `Code`);

CREATE INDEX `IX_AbpPermissions_TenantId_Name` ON `AbpPermissions` (`TenantId`, `Name`);

CREATE INDEX `IX_AbpPermissions_RoleId` ON `AbpPermissions` (`RoleId`);

CREATE INDEX `IX_AbpPermissions_UserId` ON `AbpPermissions` (`UserId`);

CREATE INDEX `IX_AbpRoleClaims_RoleId` ON `AbpRoleClaims` (`RoleId`);

CREATE INDEX `IX_AbpRoleClaims_TenantId_ClaimType` ON `AbpRoleClaims` (`TenantId`, `ClaimType`);

CREATE INDEX `IX_AbpRoles_CreatorUserId` ON `AbpRoles` (`CreatorUserId`);

CREATE INDEX `IX_AbpRoles_DeleterUserId` ON `AbpRoles` (`DeleterUserId`);

CREATE INDEX `IX_AbpRoles_LastModifierUserId` ON `AbpRoles` (`LastModifierUserId`);

CREATE INDEX `IX_AbpRoles_TenantId_NormalizedName` ON `AbpRoles` (`TenantId`, `NormalizedName`);

CREATE INDEX `IX_AbpSettings_UserId` ON `AbpSettings` (`UserId`);

CREATE INDEX `IX_AbpSettings_TenantId_Name` ON `AbpSettings` (`TenantId`, `Name`);

CREATE INDEX `IX_AbpTenantNotifications_TenantId` ON `AbpTenantNotifications` (`TenantId`);

CREATE INDEX `IX_AbpTenants_CreatorUserId` ON `AbpTenants` (`CreatorUserId`);

CREATE INDEX `IX_AbpTenants_DeleterUserId` ON `AbpTenants` (`DeleterUserId`);

CREATE INDEX `IX_AbpTenants_EditionId` ON `AbpTenants` (`EditionId`);

CREATE INDEX `IX_AbpTenants_LastModifierUserId` ON `AbpTenants` (`LastModifierUserId`);

CREATE INDEX `IX_AbpTenants_TenancyName` ON `AbpTenants` (`TenancyName`);

CREATE INDEX `IX_AbpUserAccounts_EmailAddress` ON `AbpUserAccounts` (`EmailAddress`);

CREATE INDEX `IX_AbpUserAccounts_UserName` ON `AbpUserAccounts` (`UserName`);

CREATE INDEX `IX_AbpUserAccounts_TenantId_EmailAddress` ON `AbpUserAccounts` (`TenantId`, `EmailAddress`);

CREATE INDEX `IX_AbpUserAccounts_TenantId_UserId` ON `AbpUserAccounts` (`TenantId`, `UserId`);

CREATE INDEX `IX_AbpUserAccounts_TenantId_UserName` ON `AbpUserAccounts` (`TenantId`, `UserName`);

CREATE INDEX `IX_AbpUserClaims_UserId` ON `AbpUserClaims` (`UserId`);

CREATE INDEX `IX_AbpUserClaims_TenantId_ClaimType` ON `AbpUserClaims` (`TenantId`, `ClaimType`);

CREATE INDEX `IX_AbpUserLoginAttempts_UserId_TenantId` ON `AbpUserLoginAttempts` (`UserId`, `TenantId`);

CREATE INDEX `IX_AbpUserLoginAttempts_TenancyName_UserNameOrEmailAddress_Resu~` ON `AbpUserLoginAttempts` (`TenancyName`, `UserNameOrEmailAddress`, `Result`);

CREATE INDEX `IX_AbpUserLogins_UserId` ON `AbpUserLogins` (`UserId`);

CREATE INDEX `IX_AbpUserLogins_TenantId_UserId` ON `AbpUserLogins` (`TenantId`, `UserId`);

CREATE INDEX `IX_AbpUserLogins_TenantId_LoginProvider_ProviderKey` ON `AbpUserLogins` (`TenantId`, `LoginProvider`, `ProviderKey`);

CREATE INDEX `IX_AbpUserNotifications_UserId_State_CreationTime` ON `AbpUserNotifications` (`UserId`, `State`, `CreationTime`);

CREATE INDEX `IX_AbpUserOrganizationUnits_TenantId_OrganizationUnitId` ON `AbpUserOrganizationUnits` (`TenantId`, `OrganizationUnitId`);

CREATE INDEX `IX_AbpUserOrganizationUnits_TenantId_UserId` ON `AbpUserOrganizationUnits` (`TenantId`, `UserId`);

CREATE INDEX `IX_AbpUserRoles_UserId` ON `AbpUserRoles` (`UserId`);

CREATE INDEX `IX_AbpUserRoles_TenantId_RoleId` ON `AbpUserRoles` (`TenantId`, `RoleId`);

CREATE INDEX `IX_AbpUserRoles_TenantId_UserId` ON `AbpUserRoles` (`TenantId`, `UserId`);

CREATE INDEX `IX_AbpUsers_CreatorUserId` ON `AbpUsers` (`CreatorUserId`);

CREATE INDEX `IX_AbpUsers_DeleterUserId` ON `AbpUsers` (`DeleterUserId`);

CREATE INDEX `IX_AbpUsers_LastModifierUserId` ON `AbpUsers` (`LastModifierUserId`);

CREATE INDEX `IX_AbpUsers_TenantId_NormalizedEmailAddress` ON `AbpUsers` (`TenantId`, `NormalizedEmailAddress`);

CREATE INDEX `IX_AbpUsers_TenantId_NormalizedUserName` ON `AbpUsers` (`TenantId`, `NormalizedUserName`);

CREATE INDEX `IX_AbpUserTokens_UserId` ON `AbpUserTokens` (`UserId`);

CREATE INDEX `IX_AbpUserTokens_TenantId_UserId` ON `AbpUserTokens` (`TenantId`, `UserId`);

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20200312225416_InitMigration', '2.2.1-servicing-10028');

ALTER TABLE `AbpUsers` ADD `DateOfBirth` datetime(6) NULL;

ALTER TABLE `AbpUsers` ADD `LanguageId` int NOT NULL DEFAULT 0;

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20200323035224_AddLanguage', '2.2.1-servicing-10028');


